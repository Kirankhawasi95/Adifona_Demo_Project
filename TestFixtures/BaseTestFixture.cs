using System.Diagnostics;
using AventStack.ExtentReports;
using HorusUITest.Adapters;
using HorusUITest.Configuration;
using HorusUITest.Enums;
using HorusUITest.Helper;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Platform = HorusUITest.Enums.Platform;

namespace HorusUITest.TestFixtures
{
    [TestFixture(Platform.Android, Category = "Android")]
    [TestFixture(Platform.iOS, Category = "iOS")]
    public abstract class BaseTestFixture
    {
        protected Platform CurrentPlatform { get; }

        protected bool OnAndroid => CurrentPlatform == Platform.Android;

        protected bool OniOS => CurrentPlatform == Platform.iOS;

        protected static int LastCounter { get; set; } = 0;

        protected static int NewCounter => ++LastCounter;

        protected Stopwatch testWatch = new Stopwatch();

        protected Stopwatch fixtureWatch = new Stopwatch();

        public static UART getUART = new UART();

        public BaseTestFixture(Platform platform)
        {
            CurrentPlatform = platform;
        }

        [OneTimeSetUp]
        public virtual void BeforeAll()
        {
            Output.Immediately($"Preparing test fixture: {TestContext.CurrentContext.Test.Name}");
            fixtureWatch.Restart();
        }

        [SetUp]
        public virtual void BeforeEachTest()
        {
            Output.Immediately($"Starting test #{NewCounter}: {TestContext.CurrentContext.Test.Name} on {CurrentPlatform}");
            // getUART.InitializeUART();
            testWatch.Restart();

            ReportHelper.CreateTest(TestContext.CurrentContext.Test.Name, "Test started successfully on '" + AppManager.Brand + "' OEM");

            // Adding Testcase names for renaming the file in Report if only one test case is excecuted. If Multiple then the name will be its class name
            if (!ReportHelper.TestCaseNames.Contains(TestContext.CurrentContext.Test.MethodName))
                ReportHelper.TestCaseNames.Add(TestContext.CurrentContext.Test.MethodName);
        }

        [TearDown]
        public virtual void AfterEachTest()
        {
            try   //HACK: Deliberately failing the test if the result is inconclusive, because TFS 2015 won't show the message associated with an inconclusive result.
            {
                if (Env.ReportInconclusiveAsFailed && TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Inconclusive)
                    Assert.Fail("Inconclusive test result. Original message: " + TestContext.CurrentContext.Result.Message);
            }
            catch
            {
                //Do nothing.
            }
            Output.Immediately($"Finished test #{LastCounter}: {TestContext.CurrentContext.Test.Name} on {CurrentPlatform} - {TestContext.CurrentContext.Result.Outcome.Status} after {(int)testWatch.Elapsed.TotalSeconds} seconds");
            switch (TestContext.CurrentContext.Result.Outcome.Status)
            {
                case TestStatus.Passed:
                    {
                        //ReportHelper.AttachSuccessScreenshot();

                        if (ReportHelper.GetTestStatus() == Status.Fail)
                            ReportHelper.CreateTicket();
                        else
                            ReportHelper.LogTest(Status.Pass, "Test passed successfully");

                        break;
                    }
                case TestStatus.Inconclusive:
                    {
                        //ReportHelper.AttachInconclusiveScreenshot();

                        break;
                    }
                case TestStatus.Failed:
                    {
                        ReportHelper.LogTest(Status.Fail, "Test Case failed with Message: <br>" + TestContext.CurrentContext.Result.Message + "<br><br>Stack Trace:<br>" + TestContext.CurrentContext.Result.StackTrace);

                        // Internally it only works if this is enabled
                        ReportHelper.CreateTicket();

                        // Mobile language change in IOS is not implemented. Hence doing it only for Android
                        if (CurrentPlatform == Platform.Android)
                        {
                            // Mobile language changed to English. Since in Regression Testing if the smart phone language change fails the other test cases gets impacted 
                            AppManager.DeviceSettings.ChangeDeviceLanguage(Language_Device.English_US);
                        }

                        break;
                    }
            }

            ReportHelper.AttachmentFileNames = string.Empty;
        }

        [OneTimeTearDown]
        public virtual void AfterAll()
        {
            Output.Immediately($"Finished test fixture: {TestContext.CurrentContext.Test.Name} after {(int)fixtureWatch.Elapsed.TotalSeconds} seconds");
        }
    }
}