using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium.Appium.ImageComparison;

namespace HorusUITest.Helper
{
    public static class ImageComparison
    {
        private static string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        private static string CurrentTest => $"{TestContext.CurrentContext.Test.Name}({AppManager.Platform})";
        private static string CurrentTime => DateTime.Now.ToString("yyyy.MM.dd_HH.mm.ss");

        /// <summary>
        /// Score is 0.999999701976776 or greater means images are exactly same
        /// </summary>
        /// <param name="baselineImage"></param>
        /// <param name="imageToValidate"></param>
        /// <returns></returns>
        public static double GetImageSimilarityScore(string baselineImage, string imageToValidate)
        {
            SimilarityMatchingOptions matchingOptions = new SimilarityMatchingOptions
            {
                Visualize = true
            };
            SimilarityMatchingResult matchingResult = AppManager.App.Driver.GetImagesSimilarity(baselineImage, imageToValidate, matchingOptions);

            var fileName = $"VISUALISATION_{CurrentTest}_{CurrentTime}.png";
            string folderPath = AppDataPath + "\\Audifon\\ScreenShots";
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            fileName = Path.Combine(folderPath, fileName);

            SaveVisualDifferences(matchingResult, fileName);
            return matchingResult.Score;

        }

        private static void SaveVisualDifferences(this ComparisonResult comparisonResult, string fileName)
        {
            var resultProperty = typeof(ComparisonResult).GetProperty("Result", BindingFlags.Instance | BindingFlags.NonPublic);
            var result = (Dictionary<string, object>)resultProperty.GetValue(comparisonResult);
            var visualization = result["visualization"];
            var imageByteArray = visualization is Dictionary<string, object> dict ? ((object[])dict["data"]).Select(y => (byte)((long)y)).ToArray() : Convert.FromBase64String(visualization.ToString());
            File.WriteAllBytes(fileName, imageByteArray);
            //ReportHelper.test.AddScreenCaptureFromPath(fileName, "Visual Differences");
            ReportHelper.AddVisualDifferenceToReport(fileName, "Visual Differences");
        }

        public static string GetBase64StringForImage(string imgPath)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(imgPath);
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }
    }
}
