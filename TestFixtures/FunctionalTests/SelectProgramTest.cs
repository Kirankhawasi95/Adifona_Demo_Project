using System;
using System.Collections.Generic;
using System.Threading;
using HorusUITest.Adapters;
using HorusUITest.Configuration;
using HorusUITest.Data;
using HorusUITest.Enums;
using HorusUITest.Helper;
using HorusUITest.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Android;
using Platform = HorusUITest.Enums.Platform;
using OpenQA.Selenium.Appium;



namespace HorusUITest.TestFixtures.FunctionalTests
{
    public class SelectProgramTest : BaseResettingTestFixture
    {
        public SelectProgramTest(Platform platform)
            : base(platform)
        {
        }
        private readonly IOSDriver<AppiumWebElement> iOSDriver;
        private readonly AndroidDriver<AppiumWebElement> AndroidDriver;

        [Test]
        [Category("FunctionalTest")]
        // [Platform("Android")]
        public void SelectActiveProgramTest()
        {
#if! TEST_FROM_IDE
            //Select devices for Test Run
            AndroidPhone androidPhone = SelectPhone.Android(AndroidPhoneName.Audifon_Nokia_7_plus);
            IosPhone iosPhone = SelectPhone.Ios(IosPhoneName.Audifon_iPhone_8);

            //Configure App for Test on desired platform
            AppManager.InitializeApp(new AppConfig(CurrentPlatform, androidPhone, iosPhone));
            AppManager.StartApp(true);
#endif

            //Startup and connection
            Output.Immediately("The app has started.");
            Output.TestStep("Proceeding to dashboard in Demo Mode");

            var dashboardPage = LaunchHelper.StartAppInDemoModeForTheFirstTime().Page;
            
            //Check Number of Programs
            Output.TestStep("Checking number of programs on dashboard page");
            Assert.AreEqual(4, dashboardPage.GetNumberOfPrograms(), "Number of programs on Dashboard Page does not meet the precondition for this test, expected to be 4 programs");
            //var initialProgram = dashboardPage.GetCurrentProgramName();

            //Changing the hearing program
            Output.TestStep("Changing the hearing program and verifying that each program has a unique name", 2);
            string lastName = null;
            //List<string> programNameList = new List<string>();
            string[] programNameList = new string[8];
            for (int i = 0; i < 4; i++)
            {
                dashboardPage.SelectProgram(i);
                Assert.IsTrue(dashboardPage.GetIsProgramSelected(i), $"Failed to select a hearing program {i}.");
                string newName = dashboardPage.GetCurrentProgramName();
                Assert.AreNotEqual(lastName, newName, "Duplicate hearing program names found.");
                lastName = newName;
                programNameList[i] = newName;
            }
            
            Output.TestStep("Open program details for first program from dashboard page");
            dashboardPage.OpenCurrentProgram();
            var programDetailPage = new ProgramDetailPage();

            //Output.TestStep("Test program slider on navigation bar");
            //TODO: sliding implementieren(Step:6 zweiter Teil)

            Output.TestStep("Check program switching via program bar on top of program detail page");
            for (int j = 0; j < 4; j++)
            {
                programDetailPage.SelectProgram(j);
                Assert.AreEqual(programNameList[j], programDetailPage.GetCurrentProgramName(), $"Actual program name does not match expected program name'{programNameList[0]}'");
            }
            
            Output.TestStep("Lock device for 3 seconds and after this unlock device again");
            if (OniOS)
            {
                IosApp actualAppleTestDevice = new IosApp(iOSDriver);
                actualAppleTestDevice.LockDevice(5);
            }
            else
            {
                AndroidApp actualAndroidTestDevice = new AndroidApp(AndroidDriver,AppManager.Phone.ToString());
                actualAndroidTestDevice.LockDevice(5);
            }
            
            Output.TestStep("Check for visibility of all elements on screen after lock-unlock device");
            for (int k = 0; k < 2; k++)
            {
                Output.TestStep("Performing loop test to check for shown elements on program detail pages");
                Output.TestStep("Check program detail elements for fourth program - wireless streaming");
                programDetailPage.SelectProgram(3);
                Assert.IsTrue(programDetailPage.GetIsEqualizerDisplayVisible(), "Expected equalizer display to be on page but was not found");
                Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible(), "Expected noise reduction display to be on page but was not found");
                Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible(), "Expected speech focus display to be on page but was not found");
                Assert.IsTrue(programDetailPage.GetIsStreamingDisplayVisible(), "Expected streaming display to be on page but was not found");
                Assert.IsTrue(programDetailPage.GetIsBinauralSettingsButtonVisible(), "Expected binaural settings button to be on page but was not found");
                Assert.IsTrue(programDetailPage.GetIsLeftHearingDeviceVisible(), "Expected left hearing device display to be on page but was not found");
                Assert.IsTrue(programDetailPage.GetIsRightHearingDeviceVisible(), "Expected right hearing device display to be on page but was not found");

                Output.TestStep("Check program detail elements for third program - tinnitus only");
                programDetailPage.SelectProgram(2);
                Assert.IsTrue(programDetailPage.GetIsTinnitusOnlyDisplayVisible(), "Expected tinnitus only display to be on page but was not found");
                Assert.IsTrue(programDetailPage.GetIsBinauralSettingsButtonVisible(), "Expected binaural settings button to be on page but was not found");
                Assert.IsTrue(programDetailPage.GetIsLeftHearingDeviceVisible(), "Expected left hearing device display to be on page but was not found");
                Assert.IsTrue(programDetailPage.GetIsRightHearingDeviceVisible(), "Expected right hearing device display to be on page but was not found");

                Output.TestStep("Check program detail elements for second program - music");
                programDetailPage.SelectProgram(1);
                Assert.IsTrue(programDetailPage.GetIsEqualizerDisplayVisible(), "Expected equalizer display to be on page but was not found");
                Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible(), "Expected noise reduction display to be on page but was not found");
                Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible(), "Expected speech focus display to be on page but was not found");
                Assert.IsTrue(programDetailPage.GetIsTinnitusDisplayVisible(), "Expected streaming display to be on page but was not found");
                Assert.IsTrue(programDetailPage.GetIsBinauralSettingsButtonVisible(), "Expected binaural settings button to be on page but was not found");
                Assert.IsTrue(programDetailPage.GetIsLeftHearingDeviceVisible(), "Expected left hearing device display to be on page but was not found");
                Assert.IsTrue(programDetailPage.GetIsRightHearingDeviceVisible(), "Expected right hearing device display to be on page but was not found");

                Output.TestStep("Check program detail elements for first program - automatic");
                programDetailPage.SelectProgram(0);
                Assert.IsTrue(programDetailPage.GetIsAutoDisplayVisible(), "Expected automatic display to be on page but was not found");
                Assert.IsTrue(programDetailPage.GetIsBinauralSettingsButtonVisible(), "Expected binaural settings button to be on page but was not found");
                Assert.IsTrue(programDetailPage.GetIsLeftHearingDeviceVisible(), "Expected left hearing device display to be on page but was not found");
                Assert.IsTrue(programDetailPage.GetIsRightHearingDeviceVisible(), "Expected right hearing device display to be on page but was not found");
            }
            Output.TestStep("Close app - end Test");
            AppManager.CloseApp();
        }
    }
}