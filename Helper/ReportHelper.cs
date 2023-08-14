using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.Text.Json;
using NUnit.Framework;
using System.Net.Mail;
using System.Linq;
using HorusUITest.Data;

namespace HorusUITest.Helper
{
    public static class ReportHelper
    {
        #region Variables

        private static ExtentReports objExtentReports = null;
        private static ExtentTest objExtentTest = null;
        private static ExtentHtmlReporter objExtentHtmlReporter = null;
        public static string AttachmentFileNames = string.Empty;
        public static string jSessionId = string.Empty;
        public static List<string> TestCaseNames = new List<string>();
        
        private static string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static string EmailSMPTServer = "smtp.office365.com";
        private static string EmailUserName = "support.audifon@mgtechsoft.com";
        private static string EmailPassword = "MailBlast@123";
        private static string EmailDefaultID = AppConfig.DefaultReportEmailRecipient;

        // Jira Configurations
        private static string JiraUrl = "https://mgdevops.com/dev_jira";
        private static string JiraUserName = "Ranjith.Babu@mgtechsoft.com";
        private static string JiraPassword = "Welcome@2021";

        // This variable when set as true creates Jira Ticket when test case fails
        private static bool EnableCreateTicket = false;
        // This variable when set as true will generate HTML Report and send it in email
        private static bool EnableReporting = AppConfig.DefaultEnableReporting;

        #endregion Variables

        #region Methods

        #region Attachment Creation Methods

        private static string CurrentTest => $"{TestContext.CurrentContext.Test.Name}({AppManager.Platform})";
        private static string CurrentTime => DateTime.Now.ToString("yyyy.MM.dd_HH.mm.ss");

        public static void AttachScreenshot(string name = null, string description = null)
        {
            if (AppManager.IsAppInitialized)
            {
                if (name == null)
                    name = $"{CurrentTest}_{CurrentTime}";
                if (!name.EndsWith(".png"))
                    name += ".png";

                string FolderPath = AppDataPath + "\\Audifon\\ScreenShots";
                if (!Directory.Exists(FolderPath))
                    Directory.CreateDirectory(FolderPath);
                name = Path.Combine(FolderPath, name);

                AppManager.App.TakeScreenshot().SaveAsFile(name, OpenQA.Selenium.ScreenshotImageFormat.Png);
                TestContext.AddTestAttachment(name, description);
                Output.Immediately($"Attached screenshot {name}");

                if (EnableReporting)
                    objExtentTest.AddScreenCaptureFromPath(name, $"{CurrentTest}_{CurrentTime}");

                AttachmentFileNames += name + "|";
            }
            else
            {
                Output.Immediately($"Tried to take a screenshot, but the app was not initialized.");
            }
        }

        public static void AttachFailureScreenshot(string name = null, string description = null)
        {
            if (name == null)
                name = $"FAILED_{CurrentTest}_{CurrentTime}";
            AttachScreenshot(name, description);
        }

        public static void AttachSuccessScreenshot(string name = null, string description = null)
        {
            if (name == null)
                name = $"PASSED_{CurrentTest}_{CurrentTime}";
            AttachScreenshot(name, description);
        }

        public static void AttachInconclusiveScreenshot(string name = null, string description = null)
        {
            if (name == null)
                name = $"INCONCLUSIVE_{CurrentTest}_{CurrentTime}";
            AttachScreenshot(name, description);
        }

        #endregion Attachment Creation Methods

        #region Jira Methods

        public static void CreateTicket()
        {
            string EnableText = GetJenkinsParameterValueByKey("CreateTicket", EnableCreateTicket.ToString().ToLower()).ToLower();
            if (EnableText == "true")
            {
                string TestCaseID = CurrentTest;
                if (TestContext.CurrentContext.Test.Properties.ContainsKey("Description") && TestContext.CurrentContext.Test.Properties["Description"].ToList().Count > 0)
                    TestCaseID = TestContext.CurrentContext.Test.Properties["Description"].ToList()[0].ToString();

                string SaveFilePath = InitializeTicketEntryLog();

                if (!CheckTicketEntryLogExists(TestCaseID, SaveFilePath))
                {
                    string issueKey = CreateJiraTicket(TestCaseID);
                    Output.Immediately($"Jira Ticket Created {issueKey}");

                    if (AttachmentFileNames != string.Empty)
                    {
                        foreach (string Attachment in AttachmentFileNames.Trim('|').Split('|'))
                        {
                            if (Attachment.Trim() != string.Empty)
                                UploadJiraAttachments(issueKey, Attachment);
                        }

                        Output.Immediately($"Attachment added to the Defect: {issueKey}");
                    }

                    InsertTicketEntryLog(SaveFilePath, TestCaseID, issueKey);
                    Output.Immediately($"Entry Added in Text File for Test Case: {TestCaseID}");
                }
            }
        }

        private static string InitializeTicketEntryLog()
        {
            string SaveFolderPath = AppDataPath + "\\Audifon\\Logs";
            if (!Directory.Exists(SaveFolderPath))
                Directory.CreateDirectory(SaveFolderPath);

            string SaveFilePath = SaveFolderPath + "\\TestLogs.txt";
            if (!File.Exists(SaveFilePath))
            {
                File.WriteAllText(SaveFilePath, string.Empty);

                using (StreamWriter sw = File.AppendText(SaveFilePath))
                {
                    sw.WriteLine("Test Case ID,Description");
                }
            }

            return SaveFilePath;
        }

        private static bool CheckTicketEntryLogExists(string TestCaseID, string TestCasesSavePath)
        {
            bool IsDataFound = false;

            using (StreamReader sr = File.OpenText(TestCasesSavePath))
            {
                string s = string.Empty;
                while ((s = sr.ReadLine()) != null)
                {
                    if (s != string.Empty && s.Split(',').Length > 0 && s.Split(',')[0] != string.Empty && s.Split(',')[0] == TestCaseID)
                    {
                        IsDataFound = true;
                        break;
                    }
                }
            }

            return IsDataFound;
        }

        private static string CreateJiraTicket(string TestCaseID)
        {
            string issueKey = string.Empty;

            string Summary = !ReferenceEquals(CurrentTest, null) ? "[" + TestCaseID + "] " + CurrentTest : string.Empty;
            string Description = !ReferenceEquals(TestContext.CurrentContext.Result.Message, null) ? TestContext.CurrentContext.Result.Message : GetMessagesByStatus(Status.Fail);
            Description = Description.Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("---->", string.Empty).Replace(":", string.Empty).Replace("\"", "'").Replace("-----------^", string.Empty);
            Description = Description.Replace("`", string.Empty).Replace("<", string.Empty).Replace(">", string.Empty).Replace("\\", string.Empty);
            string Priority = !ReferenceEquals(TestContext.CurrentContext.Result.Message, null) ? "Critical" : "High";

            string JiraJson = @"{""fields"":{""project"":{""key"": ""QT""},""summary"": ""{0}"",
                               ""description"": ""{1}"",""issuetype"": {""name"": ""Bug""}, ""priority"":{""name"": ""{2}""}}}";
            string JiraJsonWithError = JiraJson.Replace("{0}", Summary).Replace("{1}", Description).Replace("{2}", Priority);

            string JiraTicketAttachment = string.Format("{0}/rest/api/2/issue/", JiraUrl);

            WebRequest request = WebRequest.Create(JiraTicketAttachment);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", "Basic " + GetEncodedCredentials(JiraUserName, JiraPassword));
            request.Headers.Add("X-Atlassian-Token", "nocheck");

            byte[] data = Encoding.UTF8.GetBytes(JiraJsonWithError);
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();
            }

            WebResponse response = request.GetResponse();

            var reader = new StreamReader(response.GetResponseStream());
            string JsonResponse = reader.ReadToEnd();

            // Getting the Defect ID
            var sData = JsonSerializer.Deserialize<Dictionary<string, string>>(JsonResponse);
            issueKey = sData["key"].ToString();

            request.Abort();

            return issueKey;
        }

        private static void UploadJiraAttachments(string issueKey, string FilePath)
        {
            // Uploading the png image of error screen as an attachment to the defect created
            string JiraUrlAttachment = string.Format("{0}/rest/api/2/issue/{1}/attachments", JiraUrl, issueKey);

            String boundary = String.Format("----------{0:N}", Guid.NewGuid());
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            FileInfo fi = new FileInfo(FilePath);

            using (FileStream stream1 = File.Open(FilePath, FileMode.Open))
            {
                var data = new byte[stream1.Length];
                stream1.Read(data, 0, data.Length);
                stream1.Close();
                writer.WriteLine("--{0}", boundary);
                writer.WriteLine("Content-Disposition: form-data; name=\"file\"; filename=\"{0}\"", fi.Name);
                writer.WriteLine("Content-Type: application/octet-stream");
                writer.WriteLine();
                writer.Flush();
                stream.Write(data, 0, data.Length);
                writer.WriteLine();
            }

            writer.WriteLine("--" + boundary + "--");
            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            WebRequest request = WebRequest.Create(JiraUrlAttachment);
            request.Method = "POST";
            request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
            request.Headers.Add("Authorization", "Basic " + GetEncodedCredentials(JiraUserName, JiraPassword));
            request.Headers.Add("X-Atlassian-Token", "nocheck");
            request.ContentLength = stream.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                stream.WriteTo(requestStream);
                requestStream.Close();
            }

            WebResponse response = request.GetResponse();

            request.Abort();
        }

        private static void InsertTicketEntryLog(string SaveFilePath, string TestCaseID, string issueKey)
        {
            using (StreamWriter sw = File.AppendText(SaveFilePath))
            {
                sw.WriteLine(TestCaseID + "," + issueKey);
            }
        }

        private static string GetEncodedCredentials(string JiraUserName, string JiraPassword)
        {
            string mergedCredentials = string.Format("{0}:{1}", JiraUserName, JiraPassword);
            byte[] byteCredentials = UTF8Encoding.UTF8.GetBytes(mergedCredentials);
            return Convert.ToBase64String(byteCredentials);
        }

        #endregion Jira Methods

        #region Report Methods

        public static void InitializeReport()
        {
            TestCaseNames = new List<string>();

            if (EnableReporting)
            {
                string SaveFolderPath = AppDataPath + "\\Audifon\\Reports";
                if (!Directory.Exists(SaveFolderPath))
                    Directory.CreateDirectory(SaveFolderPath);

                string FileName = "Index";

                string FilePath = SaveFolderPath + "\\" + FileName + ".html";
                objExtentHtmlReporter = new ExtentHtmlReporter(FilePath);
                objExtentReports = new ExtentReports();

                string SprintName = GetJenkinsParameterValueByKey("SprintName", string.Empty);
                string AppName = GetJenkinsParameterValueByKey("AppName", string.Empty);
                string MobileName = GetJenkinsParameterValueByKey("MobileName", string.Empty);
                string CategoryName = GetJenkinsParameterValueByKey("CategoryName", string.Empty);
                string HearingAidName = GetJenkinsParameterValueByKey("HearingAid", string.Empty);
                string EmailIDs = GetJenkinsParameterValueByKey("EmailID", EmailDefaultID);

                if (SprintName != string.Empty)
                    objExtentReports.AddSystemInfo("Sprint", SprintName);
                if (AppName != string.Empty)
                    objExtentReports.AddSystemInfo("App", AppName);
                if (MobileName != string.Empty)
                    objExtentReports.AddSystemInfo("Mobile", MobileName);
                if (CategoryName != string.Empty)
                    objExtentReports.AddSystemInfo("Category", CategoryName);
                if (HearingAidName != string.Empty && HearingAidName != "DemoMode")
                    objExtentReports.AddSystemInfo("Hearing Aid", HearingAidName);
                if (EmailIDs.Trim(',') != string.Empty)
                    objExtentReports.AddSystemInfo("Email Users", EmailIDs.Trim(',').Replace(",", "<br>"));

                objExtentReports.AttachReporter(objExtentHtmlReporter);
            }
        }

        public static void CreateTest(string TestName, string StartText)
        {
            if (EnableReporting)
                objExtentTest = objExtentReports.CreateTest(TestName).Info(StartText);
        }

        public static void LogTest(Status status, string LogText)
        {
            if (EnableReporting)
                objExtentTest.Log(status, LogText);

            if (status == Status.Pass)
                Output.TestStep(LogText);
            else
                Output.Immediately(LogText);

            if (status == Status.Fail || status == Status.Error)
                AttachFailureScreenshot(description: LogText);
        }

        public static Status GetTestStatus()
        {
            Status objStatus = Status.Pass;
            //for (int i = 0; i < test.Model.LogContext.Count; i++)
            //{
            //    if (test.Model.LogContext.Get(i).Status == Status.Fail)
            //    {
            //        objStatus = Status.Fail;
            //        break;
            //    }
            //}
            return objStatus;
        }

        private static string GetMessagesByStatus(Status objStatus)
        {
            int j = 0;
            string Messages = string.Empty;
            //for (int i = 0; i < test.Model.LogContext.Count; i++)
            //{
            //    if (test.Model.LogContext.Get(i).Status == objStatus)
            //    {
            //        j++;
            //        Messages += j + ") " + test.Model.LogContext.Get(i).Details + ". ";
            //    }
            //}
            return Messages;
        }

        public static void AddVisualDifferenceToReport(string FileName, string DifferenceType)
        {
            if (EnableReporting)
                objExtentTest.AddScreenCaptureFromPath(FileName, DifferenceType);
        }

        public static void CloseReport()
        {
            if (EnableReporting)
            {
                objExtentReports.Flush();

                SendMail();
            }
        }

        private static void SendMail()
        {
            string SprintName = GetJenkinsParameterValueByKey("SprintName", string.Empty);
            string AppName = GetJenkinsParameterValueByKey("AppName", string.Empty);
            string MobileName = GetJenkinsParameterValueByKey("MobileName", string.Empty);
            string CategoryName = GetJenkinsParameterValueByKey("CategoryName", string.Empty);
            string HearingAidName = GetJenkinsParameterValueByKey("HearingAid", string.Empty);
            string EmailIDs = GetJenkinsParameterValueByKey("EmailID", EmailDefaultID);

            SmtpClient smtp = new SmtpClient();
            MailMessage message = new MailMessage();

            // Renaming the Report File. If multple testcase from one class is excecuted then the file name will be its class name. 
            string FileName = TestContext.CurrentContext.Test.ClassName.Substring(TestContext.CurrentContext.Test.ClassName.LastIndexOf('.') + 1);
            // Naming the file by jenkins category name if more than one class has same category and if is executed from jenkins 
            if (CategoryName != string.Empty)
                FileName = CategoryName;
            // If single test case is excecuted the file name will be the test case name
            if (TestCaseNames.Count == 1)
                FileName = TestCaseNames[0];

            string DefaultPathToAttachment = AppDataPath + "\\Audifon\\Reports\\Index.html";
            RenameFile(DefaultPathToAttachment, FileName + ".html");

            string PathToAttachment = AppDataPath + "\\Audifon\\Reports\\" + FileName + ".html";
            string PathToAttachmentXAML = AppDataPath + "\\Audifon\\Reports\\test.xml";

            string TestType = "Android";
            if (AppManager.Platform == Enums.Platform.iOS)
                TestType = "iOS";

            try
            {
                smtp.UseDefaultCredentials = false;
                smtp.Host = EmailSMPTServer;
                smtp.Port = 587;
                smtp.Credentials = new NetworkCredential(EmailUserName, EmailPassword);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = true;

                message.From = new MailAddress(EmailUserName);
                message.To.Add(new MailAddress(EmailDefaultID));
                if (EmailIDs.IndexOf(',') != -1)
                {
                    string[] arrEmailIDs = EmailIDs.Trim(',').Split(',');
                    foreach (string EmailID in arrEmailIDs)
                    {
                        if (EmailID.Trim() != string.Empty)
                            message.To.Add(new MailAddress(EmailID));
                    }
                }
                else
                    message.To.Add(new MailAddress(EmailIDs));

                message.Subject = "Test Suit Execution Report Report for " + TestType;

                string MessageBody = string.Empty;
                MessageBody = "Hi User,<br><br>The Test Suit Execution Report has been attached to this email.{{Details}}<br><br>Regards,<br>Jenkins Admin<br><br><br>Note: This is an auto-generated mail. Please do not reply.";

                string Details = string.Empty;
                Details += "<br><br>Platform: " + TestType;
                if (SprintName != string.Empty && AppName != string.Empty && MobileName != string.Empty && CategoryName != string.Empty)
                {
                    Details += "<br><br>Sprint Name: " + SprintName;
                    Details += "<br>App Name: " + AppName;
                    Details += "<br>Mobile Name: " + MobileName;
                    Details += "<br>Category Name: " + CategoryName;
                    if (HearingAidName != string.Empty)
                        Details += "<br>Hearing Aid: " + HearingAidName;
                }

                MessageBody = MessageBody.Replace("{{Details}}", Details);

                message.Body = MessageBody;
                message.IsBodyHtml = true;

                if (File.Exists(PathToAttachment))
                {
                    message.Attachments.Add(new Attachment(PathToAttachment));
                    if (File.Exists(PathToAttachmentXAML))
                        message.Attachments.Add(new Attachment(PathToAttachmentXAML));

                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                Output.Immediately("Message: " + ex.Message + ". Stack Trace: " + ex.StackTrace);
            }
            finally
            {
                if (!ReferenceEquals(message.Attachments, null))
                {
                    for (int i = message.Attachments.Count - 1; i >= 0; i--)
                    {
                        message.Attachments[i].Dispose();
                    }
                    message.Attachments.Clear();
                    message.Attachments.Dispose();
                }
                message.Dispose();
                smtp.Dispose();
            }

            if (File.Exists(PathToAttachment))
            {
                // These Report Folders will be created only when excecuted from Jenkns
                if (SprintName != string.Empty && AppName != string.Empty && MobileName != string.Empty && CategoryName != string.Empty)
                {
                    string TestResultsDirectoryPath = AppDataPath + "\\Audifon\\TestResults\\" + SprintName + "\\" + MobileName + "\\" + AppName + "\\" + CategoryName;
                    if (HearingAidName != string.Empty && HearingAidName != "DemoMode")
                        TestResultsDirectoryPath = AppDataPath + "\\Audifon\\TestResults\\" + SprintName + "\\" + MobileName + "\\" + AppName + "\\" + CategoryName + "\\" + HearingAidName;

                    if (!Directory.Exists(TestResultsDirectoryPath))
                        Directory.CreateDirectory(TestResultsDirectoryPath);

                    string TestResultsFilePath = TestResultsDirectoryPath + "\\" + Path.GetFileName(PathToAttachment);

                    if (File.Exists(TestResultsFilePath))
                        File.Delete(TestResultsFilePath);

                    File.Copy(PathToAttachment, TestResultsFilePath);

                    Output.Immediately("Reports copied to TestResults folder");
                }
            }

            if (File.Exists(PathToAttachment))
                File.Delete(PathToAttachment);

            if (File.Exists(PathToAttachmentXAML))
                File.Delete(PathToAttachmentXAML);

            string EmailMsg = "Email Sent to User with Report as Attachment: " + EmailIDs + " for " + TestType;
            Output.Immediately(EmailMsg);
        }

        #endregion Report Methods

        #region Common Methods

        public static string GetJenkinsParameterValueByKey(string Key, string DefaultValue)
        {
            return TestContext.Parameters.Get(Key, DefaultValue);
        }

        private static void RenameFile(string filePath, string newName)
        {
            if (File.Exists(filePath))
            {
                FileInfo fileInfo = new FileInfo(filePath);

                if (File.Exists(fileInfo.Directory.FullName + "\\" + newName))
                    File.Delete(fileInfo.Directory.FullName + "\\" + newName);

                fileInfo.MoveTo(fileInfo.Directory.FullName + "\\" + newName);
            }
        }

        #endregion Common Methods

        #endregion Methods
    }
}