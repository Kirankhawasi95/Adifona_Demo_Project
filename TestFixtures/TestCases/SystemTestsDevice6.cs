using System;
using System.Threading;
using AventStack.ExtentReports;
using HorusUITest.Configuration;
using HorusUITest.Enums;
using HorusUITest.Helper;
using HorusUITest.PageObjects;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Controls.ProgramDetailParams;
using HorusUITest.PageObjects.Dialogs;
using HorusUITest.PageObjects.Favorites;
using HorusUITest.PageObjects.Favorites.Automation;
using HorusUITest.PageObjects.Menu;
using HorusUITest.PageObjects.Menu.Help;
using HorusUITest.PageObjects.Menu.Settings;
using HorusUITest.PageObjects.Start;
using HorusUITest.PageObjects.Start.Intro;
using NUnit.Framework;
using Platform = HorusUITest.Enums.Platform;

namespace HorusUITest.TestFixtures.TestCases
{
    public class SystemTestsDevice6 : BaseResettingTestFixture
    {
        #region Constructor

        public SystemTestsDevice6(Platform platform)
            : base(platform)
        {

        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Verify Noise Reduction Tail and Check Name for Lewi Device
        /// </summary>
        /// <param name="language"></param>
        private void VerifyNoiseReductionTailAndChecknameFiveOptions(string language)
        {
            //Open Music Program
            var currentProgramName = new DashboardPage().GetCurrentProgramName();
            ReportHelper.LogTest(Status.Info, "Opening Music program from main menu using tap...");
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            ReportHelper.LogTest(Status.Info, "Opened Music program from main menu using tap");
            ReportHelper.LogTest(Status.Pass, "Navigated successfully to '" + currentProgramName + "' program page");

            // Get Texts
            ReportHelper.LogTest(Status.Info, "Opening noise reduction display settings...");
            new ProgramDetailPage().NoiseReductionDisplay.OpenSettings();
            ReportHelper.LogTest(Status.Info, "Opened noise reduction display settings");
            string noiseOff = new ProgramDetailParamEditNoiseReductionPage().GetNoiseReductionName(NoiseReduction.Off);
            string noiseLow = new ProgramDetailParamEditNoiseReductionPage().GetNoiseReductionName(NoiseReduction.Light);
            string noiseMedium = new ProgramDetailParamEditNoiseReductionPage().GetNoiseReductionName(NoiseReduction.Medium);
            string noiseHigh = new ProgramDetailParamEditNoiseReductionPage().GetNoiseReductionName(NoiseReduction.Speech);
            string noiseBoost = new ProgramDetailParamEditNoiseReductionPage().GetNoiseReductionName(NoiseReduction.Strong);

            // Checking for English
            if (language.Equals("English"))
            {
                ReportHelper.LogTest(Status.Info, "Checking if noise reduction slider names are in English language...");
                if (noiseOff.Equals("Off") && noiseLow.Equals("Low") && noiseMedium.Equals("Medium") && noiseHigh.Equals("High") && noiseBoost.Equals("Boost"))
                    ReportHelper.LogTest(Status.Pass, "Noise Reduction slider names are in English language");
                else
                    Assert.Fail("Noise Reduction slider name are not in English language");
            }

            // Checking for German
            if (language.Equals("German"))
            {
                ReportHelper.LogTest(Status.Info, "Checking if noise reduction slider names are in German language...");
                if (noiseOff.Equals("Aus") && noiseLow.Equals("Leicht") && noiseMedium.Equals("Mittel") && noiseHigh.Equals("Hoch") && noiseBoost.Equals("Boost"))
                    ReportHelper.LogTest(Status.Pass, "Noise Reduction slider names are in German language");
                else
                    Assert.Fail("Noise Reduction slider name are not in German language");
            }

            //Closing Music
            ReportHelper.LogTest(Status.Info, "Closing noise reduction display settings...");
            new ProgramDetailParamEditNoiseReductionPage().Close();
            ReportHelper.LogTest(Status.Info, "Closed noise reduction display settings");

            ReportHelper.LogTest(Status.Info, "Tapping back to dashboard page...");
            new ProgramDetailPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Tapped back to dashboard page");

            //Open Audio Streaming
            var currentProgramName1 = new DashboardPage().GetCurrentProgramName();
            ReportHelper.LogTest(Status.Info, "Opening Audio Streaming program from main menu using tap...");
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Streaming, 0);
            ReportHelper.LogTest(Status.Info, "Opened Audio Streaming program from main menu using tap");
            ReportHelper.LogTest(Status.Pass, "Navigated successfully to '" + currentProgramName1 + "' program page");

            // Get Texts
            ReportHelper.LogTest(Status.Info, "Opening noise reduction display settings...");
            new ProgramDetailPage().NoiseReductionDisplay.OpenSettings();
            ReportHelper.LogTest(Status.Info, "Opened noise reduction display settings");
            string noiseOffStreaming = new ProgramDetailParamEditNoiseReductionPage().GetNoiseReductionName(NoiseReduction.Off);
            string noiseLowStreaming = new ProgramDetailParamEditNoiseReductionPage().GetNoiseReductionName(NoiseReduction.Light);
            string noiseMediumStreaming = new ProgramDetailParamEditNoiseReductionPage().GetNoiseReductionName(NoiseReduction.Medium);
            string noiseHighStreaming = new ProgramDetailParamEditNoiseReductionPage().GetNoiseReductionName(NoiseReduction.Speech);
            string noiseBoostStreaming = new ProgramDetailParamEditNoiseReductionPage().GetNoiseReductionName(NoiseReduction.Strong);

            // Checking for English
            if (language.Equals("English"))
            {
                ReportHelper.LogTest(Status.Info, "Checking if noise reduction slider names are in English language...");
                if (noiseOffStreaming.Equals("Off") && noiseLowStreaming.Equals("Low") && noiseMediumStreaming.Equals("Medium") && noiseHighStreaming.Equals("High") && noiseBoostStreaming.Equals("Boost"))
                    ReportHelper.LogTest(Status.Pass, "Noise Reduction slider names are in English language");
                else
                    Assert.Fail("Noise Reduction slider name are not in English language");
            }

            // Checking for German
            if (language.Equals("German"))
            {
                ReportHelper.LogTest(Status.Info, "Checking if noise reduction slider names are in German language...");
                if (noiseOffStreaming.Equals("Aus") && noiseLowStreaming.Equals("Leicht") && noiseMediumStreaming.Equals("Mittel") && noiseHighStreaming.Equals("Hoch") && noiseBoostStreaming.Equals("Boost"))
                    ReportHelper.LogTest(Status.Pass, "Noise Reduction slider names are in German language");
                else
                    Assert.Fail("Noise Reduction slider name are not in German language");
            }

            ReportHelper.LogTest(Status.Info, "Closing noise reduction display settings...");
            new ProgramDetailParamEditNoiseReductionPage().Close();
            ReportHelper.LogTest(Status.Info, "Closed noise reduction display settings");
        }

        /// <summary>
        /// Verify Noise Reduction Tail and Check Name for Risa Device
        /// </summary>
        /// <param name="language"></param>
        private void VerifyNoiseReductionTailAndChecknameThreeOptions(string language)
        {
            //Open Music Program
            var currentProgramName = new DashboardPage().GetCurrentProgramName();
            ReportHelper.LogTest(Status.Info, "Opening Music program from main menu using tap...");
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            ReportHelper.LogTest(Status.Info, "Opened Music program from main menu using tap");
            ReportHelper.LogTest(Status.Pass, "Navigated successfully to '" + currentProgramName + "' program page");

            // Get Texts
            ReportHelper.LogTest(Status.Info, "Opening noise reduction display settings...");
            new ProgramDetailPage().NoiseReductionDisplay.OpenSettings();
            ReportHelper.LogTest(Status.Info, "Opened noise reduction display settings");
            string noiseOff = new ProgramDetailParamEditNoiseReductionPage().GetNoiseReductionName(NoiseReduction.Off);
            string noiseMedium = new ProgramDetailParamEditNoiseReductionPage().GetNoiseReductionName(NoiseReduction.Medium);
            string noiseBoost = new ProgramDetailParamEditNoiseReductionPage().GetNoiseReductionName(NoiseReduction.Strong);

            // Checking for English
            if (language.Equals("English"))
            {
                ReportHelper.LogTest(Status.Info, "Checking if noise reduction slider names are in English language...");
                if (noiseOff.Equals("Off") && noiseMedium.Equals("Medium") && noiseBoost.Equals("Boost"))
                    ReportHelper.LogTest(Status.Pass, "Noise Reduction slider names are in English language");
                else
                    Assert.Fail("Noise Reduction slider name are not in English language");
            }

            // Checking for German
            if (language.Equals("German"))
            {
                ReportHelper.LogTest(Status.Info, "Checking if noise reduction slider names are in German language...");
                if (noiseOff.Equals("Aus") && noiseMedium.Equals("Mittel") && noiseBoost.Equals("Boost"))
                    ReportHelper.LogTest(Status.Pass, "Noise Reduction slider names are in German language");
                else
                    Assert.Fail("Noise Reduction slider name are not in German language");
            }

            //Closing Music
            ReportHelper.LogTest(Status.Info, "Closing noise reduction display settings...");
            new ProgramDetailParamEditNoiseReductionPage().Close();
            ReportHelper.LogTest(Status.Info, "Closed noise reduction display settings");

            ReportHelper.LogTest(Status.Info, "Tapping back to dashboard page...");
            new ProgramDetailPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Tapped back to dashboard page");

            //Open Audio Streaming
            var currentProgramName1 = new DashboardPage().GetCurrentProgramName();
            ReportHelper.LogTest(Status.Info, "Opening Audio Streaming program from main menu using tap...");
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Streaming, 0);
            ReportHelper.LogTest(Status.Info, "Opened Audio Streaming program from main menu using tap");
            ReportHelper.LogTest(Status.Pass, "Navigated successfully to '" + currentProgramName1 + "' program page");

            // Get Texts
            ReportHelper.LogTest(Status.Info, "Opening noise reduction display settings...");
            new ProgramDetailPage().NoiseReductionDisplay.OpenSettings();
            ReportHelper.LogTest(Status.Info, "Opened noise reduction display settings");
            string noiseOffStreaming = new ProgramDetailParamEditNoiseReductionPage().GetNoiseReductionName(NoiseReduction.Off);
            string noiseMediumStreaming = new ProgramDetailParamEditNoiseReductionPage().GetNoiseReductionName(NoiseReduction.Medium);
            string noiseBoostStreaming = new ProgramDetailParamEditNoiseReductionPage().GetNoiseReductionName(NoiseReduction.Strong);

            // Checking for English
            if (language.Equals("English"))
            {
                ReportHelper.LogTest(Status.Info, "Checking if noise reduction slider names are in English language...");
                if (noiseOffStreaming.Equals("Off") && noiseMediumStreaming.Equals("Medium") && noiseBoostStreaming.Equals("Boost"))
                    ReportHelper.LogTest(Status.Pass, "Noise Reduction slider names are in English language");
                else
                    Assert.Fail("Noise Reduction slider name are not in English language");
            }

            // Checking for German
            if (language.Equals("German"))
            {
                ReportHelper.LogTest(Status.Info, "Checking if noise reduction slider names are in German language...");
                if (noiseOffStreaming.Equals("Aus") && noiseMediumStreaming.Equals("Mittel") && noiseBoostStreaming.Equals("Boost"))
                    ReportHelper.LogTest(Status.Pass, "Noise Reduction slider names are in German language");
                else
                    Assert.Fail("Noise Reduction slider name are not in German language");
            }

            ReportHelper.LogTest(Status.Info, "Closing noise reduction display settings...");
            new ProgramDetailParamEditNoiseReductionPage().Close();
            ReportHelper.LogTest(Status.Info, "Closed noise reduction display settings");
        }

        /// <summary>
        /// Common method to Connect to Hearing Aid
        /// </summary>
        /// <param name="firstHearingAid"></param>
        /// <param name="secondHearingAid"></param>
        private void connectHearingAid(string firstHearingAid, string secondHearingAid)
        {
            ReportHelper.LogTest(Status.Info, "Checking if initialize hardware page is loaded...");
            Assert.IsTrue(new InitializeHardwarePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Initialize hardware page not loaded");
            var initializeHardwarePage = new InitializeHardwarePage();
            ReportHelper.LogTest(Status.Info, "Initialize hardware page is loaded");
            ReportHelper.LogTest(Status.Info, "Checking demo mode text...");
            Assert.IsNotEmpty(initializeHardwarePage.GetDemoModeText(), "Demo mode text is empty");
            ReportHelper.LogTest(Status.Info, "Demo mode text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking start scan text...");
            Assert.IsNotEmpty(initializeHardwarePage.GetScanText(), "Start scan text is empty");
            ReportHelper.LogTest(Status.Info, "Start scan text is not empty");
            ReportHelper.LogTest(Status.Info, "Clicking start scan...");
            initializeHardwarePage.StartScan();
            ReportHelper.LogTest(Status.Info, "Clicked start scan");

            ReportHelper.LogTest(Status.Info, "Granting required permission...");
            DialogHelper.ConfirmIfDisplayed();
            PermissionHelper.AllowPermissionIfRequested();
            ReportHelper.LogTest(Status.Info, "Required permission granted");

            ReportHelper.LogTest(Status.Info, "Checking if select hearing aids page is loaded...");
            Assert.IsTrue(new SelectHearingAidsPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Select hearing aids page not loaded");
            var selectHearingAidsPage = new SelectHearingAidsPage();
            ReportHelper.LogTest(Status.Info, "Select hearing aids page is loaded");
            ReportHelper.LogTest(Status.Info, "Checking description text...");
            Assert.IsNotEmpty(selectHearingAidsPage.GetDescription(), "Description text is empty");
            ReportHelper.LogTest(Status.Info, "Description text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking cancel text...");
            Assert.IsNotEmpty(selectHearingAidsPage.GetCancelText(), "Cancel text is empty");
            ReportHelper.LogTest(Status.Info, "Cancel text is not empty");

            if (!selectHearingAidsPage.GetIsDeviceFound(firstHearingAid))
                selectHearingAidsPage.WaitUntilDeviceFound(firstHearingAid);

            if (secondHearingAid != null)
                selectHearingAidsPage.WaitUntilDeviceFound(secondHearingAid);
            if (!selectHearingAidsPage.GetIsDeviceFound(secondHearingAid))
                selectHearingAidsPage.WaitUntilDeviceFound(secondHearingAid);

            selectHearingAidsPage.WaitUntilDeviceListNotChanging();
            selectHearingAidsPage.SelectDevicesExclusively(firstHearingAid, secondHearingAid);
            Assert.IsTrue(selectHearingAidsPage.GetIsDeviceSelected(firstHearingAid));
            if (secondHearingAid != null)
                selectHearingAidsPage.GetIsDeviceSelected(secondHearingAid);

            ReportHelper.LogTest(Status.Info, "Checking connect button text...");
            Assert.IsNotEmpty(selectHearingAidsPage.GetConnectButtonText(), "Connect button text is empty");
            ReportHelper.LogTest(Status.Info, "Connect button text is not empty");

            ReportHelper.LogTest(Status.Info, "Clicking connect button...");
            selectHearingAidsPage.Connect();
            ReportHelper.LogTest(Status.Info, "Clicked connect button");

            ReportHelper.LogTest(Status.Info, "Trying to connect hearing aid Left '" + firstHearingAid + "' and Right '" + secondHearingAid + "'");
        }

        #endregion Methods

        #region Test Cases

        #region Sprint 18

        [Test]
        [Category("SystemTestsDeviceManual")]
        [Description("TC-15411_Table-22")]
        public void ST15411_ReviseContentAndFunctionErrorPages()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            var firstHearingAid = SelectHearingAid.GetLeftHearingAid();
            var secondHearingAid = SelectHearingAid.GetRightHearingAid();

            AppManager.App.DisableBluetooth();
            ReportHelper.LogTest(Status.Info, "Bluetooth Disabled");

            LaunchHelper.VerifyIntroPages();

            Assert.IsTrue(new HardwareErrorPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "HardwareErrorPage is not the current loaded page");
            HardwareErrorPage hardwareErrorPage = new HardwareErrorPage();

            ReportHelper.LogTest(Status.Info, "Checking bluetooth error page...");
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Hardware_BluetoothNotActivatedTitle"), hardwareErrorPage.GetPageTitle());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Hardware_BluetoothNotActivatedMessage"), hardwareErrorPage.GetPageMessage());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Hardware_ResolutionRetryTitle"), hardwareErrorPage.GetTitleOfItem(0));
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Hardware_ResolutionRetryMessage"), hardwareErrorPage.GetMessageOfItem(0));
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Hardware_ResolutionGotoSettingsTitle"), hardwareErrorPage.GetTitleOfItem(1));
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Hardware_ResolutionGotoSettingsMessage"), hardwareErrorPage.GetMessageOfItem(1));
            ReportHelper.LogTest(Status.Pass, "Bluetooth error page verified");

            AppManager.App.EnableBluetooth();
            ReportHelper.LogTest(Status.Info, "Bluetooth Enabled");

            if (OnAndroid)
                hardwareErrorPage.RetryProcess();

            connectHearingAid(firstHearingAid, secondHearingAid);

            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(45));
            PermissionHelper.AllowPermissionIfRequested();
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)), "DashboardPage is not the current loaded page");
            var dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            ReportHelper.LogTest(Status.Info, "Dashboard loaded after bluetooth error page verification");

            // Restart App
            AppManager.RestartApp(true);
            ReportHelper.LogTest(Status.Info, "App restarted with resetting app data");

            LaunchHelper.VerifyIntroPages();
            connectHearingAid(firstHearingAid, secondHearingAid);

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Right Hearing Aid turned off");

            Assert.IsTrue(new HearingAidConnectionErrorPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(130)), "HearingAidConnectionErrorPage is not the current loaded page");

            var hearingAidConnectionErrorPage = new HearingAidConnectionErrorPage();

            ReportHelper.LogTest(Status.Info, "Checking connection error page header for right hearing system...");
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_RightInstrumentErrorTitle"), hearingAidConnectionErrorPage.GetPageTitle());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_RightInstrumentOnlyErrorMessage"), hearingAidConnectionErrorPage.GetPageMessage());
            ReportHelper.LogTest(Status.Pass, "Connection error page header for right hearing system verified");

            ReportHelper.LogTest(Status.Info, "Checking connection error page for all options...");
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionRetryTitle"), hearingAidConnectionErrorPage.GetTitleOfItem(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionRetryTitle")));
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionRetryMessage"), hearingAidConnectionErrorPage.GetMessageOfItem(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionRetryTitle")));
            // Pair option is not available in iOS
            if (OnAndroid)
            {
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionPairTitle"), hearingAidConnectionErrorPage.GetTitleOfItem(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionPairTitle")));
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionPairMessage"), hearingAidConnectionErrorPage.GetMessageOfItem(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionPairTitle")));
            }
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionBackToSelectionTitle"), hearingAidConnectionErrorPage.GetTitleOfItem(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionBackToSelectionTitle")));
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionBackToSelectionMessage").Replace("\r", string.Empty).Replace("\n", string.Empty).Replace(" ", string.Empty), hearingAidConnectionErrorPage.GetMessageOfItem(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionBackToSelectionTitle")).Replace("\r", string.Empty).Replace("\n", string.Empty).Replace(" ", string.Empty));
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionUseOneInstrumentOnlyTitle"), hearingAidConnectionErrorPage.GetTitleOfItem(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionUseOneInstrumentOnlyTitle")));
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionUseOneInstrumentOnlyMessage"), hearingAidConnectionErrorPage.GetMessageOfItem(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionUseOneInstrumentOnlyTitle")));
            ReportHelper.LogTest(Status.Pass, "Connection error page for all options verified");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn on Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Right Hearing Aid turned on");

            hearingAidConnectionErrorPage.BackToHearingAidSelectionPage(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionBackToSelectionTitle"));
            ReportHelper.LogTest(Status.Info, "Back to Hearing Aid Selecton Page");

            connectHearingAid(firstHearingAid, secondHearingAid);

            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(45));
            PermissionHelper.AllowPermissionIfRequested();
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)), "DashboardPage is not the current loaded page");
            dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            ReportHelper.LogTest(Status.Info, "Dashboard loaded after clicking back to hearing system clicked");

            // Restart App
            AppManager.RestartApp(true);
            ReportHelper.LogTest(Status.Info, "App restarted with resetting app data");

            //Again Disconnect and Connect Right hearing device with "Use Only one hearing system" option
            LaunchHelper.VerifyIntroPages();
            connectHearingAid(firstHearingAid, secondHearingAid);

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Right Hearing Aid turned off");

            Assert.IsTrue(new HearingAidConnectionErrorPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(130)), "HearingAidConnectionErrorPage is not the current loaded page");

            hearingAidConnectionErrorPage = new HearingAidConnectionErrorPage();
            hearingAidConnectionErrorPage.UseOnlyOneHearingSystem(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionUseOneInstrumentOnlyTitle"));
            ReportHelper.LogTest(Status.Info, "Use only Left Hearing Aid to connect");

            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(45));
            PermissionHelper.AllowPermissionIfRequested();
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)), "DashboardPage is not the current loaded page");
            dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsFalse(new DashboardPage().GetIsRightHearingDeviceVisible());
            ReportHelper.LogTest(Status.Info, "Already on dashboard page coming from 'Use only one hearing system' workflow");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn on Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Right Hearing Aid turned on");

            // Restart App
            AppManager.RestartApp(true);
            ReportHelper.LogTest(Status.Info, "App restarted with resetted app data");

            LaunchHelper.VerifyIntroPages();
            connectHearingAid(firstHearingAid, secondHearingAid);

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off Left Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Left Hearing Aid turned off");

            Assert.IsTrue(new HearingAidConnectionErrorPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(130)), "HearingAidConnectionErrorPage is not the current loaded page");

            hearingAidConnectionErrorPage = new HearingAidConnectionErrorPage();

            ReportHelper.LogTest(Status.Info, "Checking connection error page header for left hearing system...");
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_LeftInstrumentErrorTitle"), hearingAidConnectionErrorPage.GetPageTitle());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_LeftInstrumentOnlyErrorMessage"), hearingAidConnectionErrorPage.GetPageMessage());
            ReportHelper.LogTest(Status.Pass, "Connection error page header for right hearing system verified");

            ReportHelper.LogTest(Status.Info, "Checking connection error page for all options...");
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionRetryTitle"), hearingAidConnectionErrorPage.GetTitleOfItem(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionRetryTitle")));
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionRetryMessage"), hearingAidConnectionErrorPage.GetMessageOfItem(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionRetryTitle")));
            // Pair option is not available in iOS
            if (OnAndroid)
            {
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionPairTitle"), hearingAidConnectionErrorPage.GetTitleOfItem(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionPairTitle")));
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionPairMessage"), hearingAidConnectionErrorPage.GetMessageOfItem(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionPairTitle")));
            }
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionBackToSelectionTitle"), hearingAidConnectionErrorPage.GetTitleOfItem(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionBackToSelectionTitle")));
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionBackToSelectionMessage").Replace("\r", string.Empty).Replace("\n", string.Empty).Replace(" ", string.Empty), hearingAidConnectionErrorPage.GetMessageOfItem(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionBackToSelectionTitle")).Replace("\r", string.Empty).Replace("\n", string.Empty).Replace(" ", string.Empty));
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionUseOneInstrumentOnlyTitle"), hearingAidConnectionErrorPage.GetTitleOfItem(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionUseOneInstrumentOnlyTitle")));
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionUseOneInstrumentOnlyMessage"), hearingAidConnectionErrorPage.GetMessageOfItem(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionUseOneInstrumentOnlyTitle")));
            ReportHelper.LogTest(Status.Pass, "Connection error page for all options verified");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn on Left Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Left Hearing Aid turned on");

            hearingAidConnectionErrorPage.BackToHearingAidSelectionPage(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionBackToSelectionTitle"));
            ReportHelper.LogTest(Status.Info, "Back to Hearing Aid Selecton Page");

            connectHearingAid(firstHearingAid, secondHearingAid);

            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(45));
            PermissionHelper.AllowPermissionIfRequested();
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)), "DashboardPage is not the current loaded page");
            dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            ReportHelper.LogTest(Status.Info, "Already on dashboard page coming from 'back to hearing systems' workflow");

            // Restart App
            AppManager.RestartApp(true);
            ReportHelper.LogTest(Status.Info, "App restarted with resetted app data");

            //Again Disconnect and Connect Left hearing device with "Use Only one hearing system" option
            LaunchHelper.VerifyIntroPages();
            connectHearingAid(firstHearingAid, secondHearingAid);

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off Left Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Left Hearing Aid turned off");

            Assert.IsTrue(new HearingAidConnectionErrorPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(130)), "HearingAidConnectionErrorPage is not the current loaded page");

            hearingAidConnectionErrorPage = new HearingAidConnectionErrorPage();
            hearingAidConnectionErrorPage.UseOnlyOneHearingSystem(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionUseOneInstrumentOnlyTitle"));
            ReportHelper.LogTest(Status.Info, "Use only Right Hearing Aid to connect");

            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(45));
            PermissionHelper.AllowPermissionIfRequested();
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)), "DashboardPage is not the current loaded page");
            dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            Assert.IsFalse(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(new DashboardPage().GetIsRightHearingDeviceVisible());
            ReportHelper.LogTest(Status.Info, "Already on dashboard page coming from 'Use only one hearing system' workflow");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn on Left Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Left Hearing Aid turned on");

            // App Closed
            AppManager.App.CloseApp();
            ReportHelper.LogTest(Status.Info, "App Closed");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off Both Left and Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Both Hearing Aid turned off");

            AppManager.StartApp(false);
            ReportHelper.LogTest(Status.Info, "App Started with stored connection data");

            Assert.IsTrue(new HearingAidConnectionErrorPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(130)), "HearingAidConnectionErrorPage is not the current loaded page");

            hearingAidConnectionErrorPage = new HearingAidConnectionErrorPage();

            ReportHelper.LogTest(Status.Info, "Checking connection error page find hearing system text...");
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionGotoFindHearingAidTitle"), hearingAidConnectionErrorPage.GetTitleOfItem(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionGotoFindHearingAidTitle")));
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionGotoFindHearingAidMessage"), hearingAidConnectionErrorPage.GetMessageOfItem(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionGotoFindHearingAidTitle")));
            ReportHelper.LogTest(Status.Pass, "Connection error page find hearing system text verified");

            hearingAidConnectionErrorPage.FindHearingAid(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionGotoFindHearingAidTitle"));

            ReportHelper.LogTest(Status.Info, "Checking find hearing systems page text...");
            FindDevicesPage findDevicesPage = new FindDevicesPage();
            Assert.IsTrue(findDevicesPage.GetIsMapViewSelected());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "help_FindDevicePage"), findDevicesPage.GetNavigationBarTitle());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "selectDevices_SideLeft"), findDevicesPage.GetLeftDeviceText());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "selectDevices_SideRight"), findDevicesPage.GetRightDeviceText());
            ReportHelper.LogTest(Status.Pass, "Find hearing systems page text verified");
            findDevicesPage.TapBack();

            hearingAidConnectionErrorPage = new HearingAidConnectionErrorPage();
            hearingAidConnectionErrorPage.RetryConnection();
            ReportHelper.LogTest(Status.Info, "Retry connection done");
            Assert.IsTrue(new HearingAidConnectionErrorPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(130)), "HearingAidConnectionErrorPage is not the current loaded page");
            ReportHelper.LogTest(Status.Info, "Again connection error page loaded");

            // Restart App
            AppManager.RestartApp(false);
            ReportHelper.LogTest(Status.Info, "App Restarted with stored connection data");

            Assert.IsTrue(new HearingAidConnectionErrorPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(130)), "HearingAidConnectionErrorPage is not the current loaded page");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn on Both Left and Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Both Hearing Aid turned on");

            hearingAidConnectionErrorPage = new HearingAidConnectionErrorPage();
            hearingAidConnectionErrorPage.BackToHearingAidSelectionPage(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionBackToSelectionTitle"));
            ReportHelper.LogTest(Status.Info, "Back to Hearing Aid Selecton Page");

            connectHearingAid(firstHearingAid, secondHearingAid);

            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)), "DashboardPage is not the current loaded page");
            dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            ReportHelper.LogTest(Status.Info, "Already on dashboard page coming from 'back to hearing aid selction' workflow");

            AppManager.DeviceSettings.DisableWifi();
        }

        [Test]
        [Category("SystemTestsDeviceSpecificConfig")]
        [Description("TC-16169_Table-11")]
        public void ST16169_RenamingSliderLabelsInNoiseFilter()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            // Connecting to Binuaral Lewi device Device configured in Audifit 5 with normal configuration
            var LeftHearingAidLewi = SelectHearingAid.GetLeftHearingAid(LeftHearingAid.Microgenisis_Lewi_R_Left_068845);
            var RightHearingAidLewi = SelectHearingAid.GetRightHearingAid(RightHearingAid.Microgenisis_Lewi_R_Right_068844);

            // Connecting to Binuaral Risa device Device configured in Audifit 5 with normal configuration
            var LeftHearingAidRisa = SelectHearingAid.GetLeftHearingAid(LeftHearingAid.Microgenisis_Risa_R_Left_068826);
            var RightHearingAidRisa = SelectHearingAid.GetRightHearingAid(RightHearingAid.Microgenisis_Risa_R_Right_068822);

            var germanLanguage = Language_Audifon.German;
            var englishLanguage = Language_Audifon.English;

            // Start App
            ReportHelper.LogTest(Status.Info, "App started in Hearing Aid Mode");

            Thread.Sleep(500);

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTimeAndVerify(LeftHearingAidLewi, RightHearingAidLewi);

            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            ReportHelper.LogTest(Status.Pass, "Start view is displayed correctly containg L and R device icons.");

            // Open Mainmenu
            dashboardPage.OpenMenuUsingTap();
            var mainMenuPage = new MainMenuPage();
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText());
            ReportHelper.LogTest(Status.Pass, "The main menu is opened.");

            // Open Settings
            mainMenuPage.OpenSettings();
            var settingsMenuPage = new SettingsMenuPage();
            Assert.IsNotEmpty(settingsMenuPage.GetMyHearingAidsText());
            Assert.IsNotEmpty(settingsMenuPage.GetPermissionsText());
            Assert.IsNotEmpty(settingsMenuPage.GetLanguageText());
            Assert.IsNotEmpty(settingsMenuPage.GetDemoModeText());
            ReportHelper.LogTest(Status.Pass, "The Settings menu is opened all items are visible.");

            // Open Language menu
            settingsMenuPage.OpenLanguage();
            var settingLanguagePage = new SettingLanguagePage();
            var initialLangauge = settingLanguagePage.GetCurrentLanguageAudifon(); // preset language before changing
            ReportHelper.LogTest(Status.Info, "App is preset to " + initialLangauge + " language");
            ReportHelper.LogTest(Status.Pass, "The Language menu is opened and it contained all available languages for Audifon app");

            // Select German Language
            settingLanguagePage.SelectLanguageAudifon(germanLanguage);
            var selectedLanguageGerman = settingLanguagePage.GetSelectedLanguageAudifon();
            Assert.AreEqual(germanLanguage, selectedLanguageGerman);
            settingLanguagePage.Accept();
            var appDialog = new AppDialog();
            Assert.IsNotEmpty(appDialog.GetMessage());
            Assert.IsNotEmpty(appDialog.GetDenyButtonText());
            Assert.IsNotEmpty(appDialog.GetConfirmButtonText());
            appDialog.Confirm();
            ReportHelper.LogTest(Status.Pass, "Langauage is changed as German");

            // Verify Text in German Language
            VerifyNoiseReductionTailAndChecknameFiveOptions("German");

            new ProgramDetailPage().TapBack();

            // Open Mainmenu
            new DashboardPage().OpenMenuUsingTap();
            mainMenuPage = new MainMenuPage();
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText());
            ReportHelper.LogTest(Status.Pass, "The main menu is opened.");

            // Open Settings
            mainMenuPage.OpenSettings();
            settingsMenuPage = new SettingsMenuPage();
            Assert.IsNotEmpty(settingsMenuPage.GetMyHearingAidsText());
            Assert.IsNotEmpty(settingsMenuPage.GetPermissionsText());
            Assert.IsNotEmpty(settingsMenuPage.GetLanguageText());
            Assert.IsNotEmpty(settingsMenuPage.GetDemoModeText());
            ReportHelper.LogTest(Status.Pass, "The Settings menu is opened all items are visible.");

            // Open Language menu
            settingsMenuPage.OpenLanguage();
            settingLanguagePage = new SettingLanguagePage();
            var initialLangaugeEnglish = settingLanguagePage.GetCurrentLanguageAudifon(); // preset language before changing
            ReportHelper.LogTest(Status.Info, "App is preset to " + initialLangaugeEnglish + " language");
            ReportHelper.LogTest(Status.Pass, "The Language menu is opened and it contained all available languages for Audifon app");

            // Select English Language
            settingLanguagePage.SelectLanguageAudifon(englishLanguage);
            var selectedLanguageEnglish = settingLanguagePage.GetSelectedLanguageAudifon();
            Assert.AreEqual(englishLanguage, selectedLanguageEnglish);
            settingLanguagePage.Accept();
            appDialog = new AppDialog();
            Assert.IsNotEmpty(appDialog.GetMessage());
            Assert.IsNotEmpty(appDialog.GetDenyButtonText());
            Assert.IsNotEmpty(appDialog.GetConfirmButtonText());
            appDialog.Confirm();
            ReportHelper.LogTest(Status.Pass, "Language is changed as English");

            // Verify Text in English Language
            VerifyNoiseReductionTailAndChecknameFiveOptions("English");

            new ProgramDetailPage().TapBack();

            // App mode changed to Demo mode form normal Mode
            new DashboardPage().OpenMenuUsingTap();
            mainMenuPage = new MainMenuPage();
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText());
            ReportHelper.LogTest(Status.Pass, "The main menu is opened.");

            // Open Settings
            mainMenuPage.OpenSettings();
            settingsMenuPage = new SettingsMenuPage();
            Assert.IsNotEmpty(settingsMenuPage.GetMyHearingAidsText());
            Assert.IsNotEmpty(settingsMenuPage.GetPermissionsText());
            Assert.IsNotEmpty(settingsMenuPage.GetLanguageText());
            Assert.IsNotEmpty(settingsMenuPage.GetDemoModeText());
            ReportHelper.LogTest(Status.Pass, "The Settings menu is opened all items are visible.");

            // Open App mode selection page
            settingsMenuPage.OpenDemoMode();

            new AppModeSelectionPage().SelectAppMode(AppMode.Demo);
            new AppModeSelectionPage().Accept();
            DialogHelper.Confirm();
            ReportHelper.LogTest(Status.Info, "App changed to Demo Mode");

            // Verify Text in English Language
            VerifyNoiseReductionTailAndChecknameFiveOptions("English");

            new ProgramDetailPage().TapBack();

            // Open Mainmenu
            new DashboardPage().OpenMenuUsingTap();
            mainMenuPage = new MainMenuPage();
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText());
            ReportHelper.LogTest(Status.Pass, "The main menu is opened.");

            // Open Settings
            mainMenuPage.OpenSettings();
            settingsMenuPage = new SettingsMenuPage();
            Assert.IsNotEmpty(settingsMenuPage.GetMyHearingAidsText());
            Assert.IsNotEmpty(settingsMenuPage.GetPermissionsText());
            Assert.IsNotEmpty(settingsMenuPage.GetLanguageText());
            Assert.IsNotEmpty(settingsMenuPage.GetDemoModeText());
            ReportHelper.LogTest(Status.Pass, "The Settings menu is opened all items are visible.");

            // Open Language menu
            settingsMenuPage.OpenLanguage();
            settingLanguagePage = new SettingLanguagePage();
            var initialLangaugeGermanSecond = settingLanguagePage.GetCurrentLanguageAudifon(); // preset language before changing
            ReportHelper.LogTest(Status.Info, "App is preset to " + initialLangaugeGermanSecond + " language");
            ReportHelper.LogTest(Status.Pass, "The Language menu is opened and it contained all available languages for Audifon app");

            // Select German Language
            settingLanguagePage.SelectLanguageAudifon(germanLanguage);
            var selectedLanguageGermanSecond = settingLanguagePage.GetSelectedLanguageAudifon();
            Assert.AreEqual(germanLanguage, selectedLanguageGermanSecond);
            settingLanguagePage.Accept();
            appDialog = new AppDialog();
            Assert.IsNotEmpty(appDialog.GetMessage());
            Assert.IsNotEmpty(appDialog.GetDenyButtonText());
            Assert.IsNotEmpty(appDialog.GetConfirmButtonText());
            appDialog.Confirm();
            ReportHelper.LogTest(Status.Pass, "Langauage is changed as German");

            // Verify Text in German Language
            VerifyNoiseReductionTailAndChecknameFiveOptions("German");

            new ProgramDetailPage().TapBack();

            // App mode changed to Normal mode from Demo Mode
            new DashboardPage().OpenMenuUsingTap();
            mainMenuPage = new MainMenuPage();
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText());
            ReportHelper.LogTest(Status.Pass, "The main menu is opened.");

            // Open Settings
            mainMenuPage.OpenSettings();
            settingsMenuPage = new SettingsMenuPage();
            Assert.IsNotEmpty(settingsMenuPage.GetMyHearingAidsText());
            Assert.IsNotEmpty(settingsMenuPage.GetPermissionsText());
            Assert.IsNotEmpty(settingsMenuPage.GetLanguageText());
            Assert.IsNotEmpty(settingsMenuPage.GetDemoModeText());
            ReportHelper.LogTest(Status.Pass, "The Settings menu is opened all items are visible.");

            // Open App mode selection page
            settingsMenuPage.OpenDemoMode();

            // App Mode changed to Normal mode from Demo Mode and Connecting Risa device manually
            new AppModeSelectionPage().SelectAppMode(AppMode.Normal);
            new AppModeSelectionPage().Accept();
            DialogHelper.Confirm();
            Assert.IsTrue(new IntroPageOne().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Info, "App changed to Normal Mode");

            Thread.Sleep(500);
            var dashboardPageDevice = LaunchHelper.StartAppWithHearingAidsForTheFirstTimeAndVerify(LeftHearingAidRisa, RightHearingAidRisa);

            Assert.IsTrue(dashboardPageDevice.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPageDevice.GetIsRightHearingDeviceVisible());
            ReportHelper.LogTest(Status.Pass, "Start view is displayed correctly containg L and R device icons.");

            // Open Mainmenu
            dashboardPageDevice.OpenMenuUsingTap();
            mainMenuPage = new MainMenuPage();
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText());
            ReportHelper.LogTest(Status.Pass, "The main menu is opened.");

            // Open Settings
            mainMenuPage.OpenSettings();
            settingsMenuPage = new SettingsMenuPage();
            Assert.IsNotEmpty(settingsMenuPage.GetMyHearingAidsText());
            Assert.IsNotEmpty(settingsMenuPage.GetPermissionsText());
            Assert.IsNotEmpty(settingsMenuPage.GetLanguageText());
            Assert.IsNotEmpty(settingsMenuPage.GetDemoModeText());
            ReportHelper.LogTest(Status.Pass, "The Settings menu is opened all items are visible.");

            // Open Language menu
            settingsMenuPage.OpenLanguage();
            settingLanguagePage = new SettingLanguagePage();
            var initialLangaugeRisa = settingLanguagePage.GetCurrentLanguageAudifon(); // preset language before changing
            ReportHelper.LogTest(Status.Info, "App is preset to " + initialLangaugeRisa + " language");
            ReportHelper.LogTest(Status.Pass, "The Language menu is opened and it contained all available languages for Audifon app");

            // Select English Language
            settingLanguagePage.SelectLanguageAudifon(englishLanguage);
            var selectedLanguageRisa = settingLanguagePage.GetSelectedLanguageAudifon();
            Assert.AreEqual(englishLanguage, selectedLanguageRisa);
            settingLanguagePage.Accept();
            appDialog = new AppDialog();
            Assert.IsNotEmpty(appDialog.GetMessage());
            Assert.IsNotEmpty(appDialog.GetDenyButtonText());
            Assert.IsNotEmpty(appDialog.GetConfirmButtonText());
            appDialog.Confirm();
            ReportHelper.LogTest(Status.Pass, "Langauage is changed as English");

            // Verify Text in English Language
            VerifyNoiseReductionTailAndChecknameThreeOptions("English");

            new ProgramDetailPage().TapBack();

            // Open Mainmenu
            new DashboardPage().OpenMenuUsingTap();
            mainMenuPage = new MainMenuPage();
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText());
            ReportHelper.LogTest(Status.Pass, "The main menu is opened.");

            // Open Settings
            mainMenuPage.OpenSettings();
            settingsMenuPage = new SettingsMenuPage();
            Assert.IsNotEmpty(settingsMenuPage.GetMyHearingAidsText());
            Assert.IsNotEmpty(settingsMenuPage.GetPermissionsText());
            Assert.IsNotEmpty(settingsMenuPage.GetLanguageText());
            Assert.IsNotEmpty(settingsMenuPage.GetDemoModeText());
            ReportHelper.LogTest(Status.Pass, "The Settings menu is opened all items are visible.");

            // Open Language menu
            settingsMenuPage.OpenLanguage();
            settingLanguagePage = new SettingLanguagePage();
            var initialLangaugeRisa1 = settingLanguagePage.GetCurrentLanguageAudifon(); // preset language before changing
            ReportHelper.LogTest(Status.Info, "App is preset to " + initialLangaugeRisa1 + " language");
            ReportHelper.LogTest(Status.Pass, "The Language menu is opened and it contained all available languages for Audifon app");

            // Select German Language
            settingLanguagePage.SelectLanguageAudifon(germanLanguage);
            var selectedLanguageRisa1 = settingLanguagePage.GetSelectedLanguageAudifon();
            Assert.AreEqual(germanLanguage, selectedLanguageRisa1);
            settingLanguagePage.Accept();
            appDialog = new AppDialog();
            Assert.IsNotEmpty(appDialog.GetMessage());
            Assert.IsNotEmpty(appDialog.GetDenyButtonText());
            Assert.IsNotEmpty(appDialog.GetConfirmButtonText());
            appDialog.Confirm();
            ReportHelper.LogTest(Status.Pass, "Langauage is changed as German");

            // Verify Text in German Language
            VerifyNoiseReductionTailAndChecknameThreeOptions("German");

            new ProgramDetailPage().TapBack();

            // App mode changed to Demo mode form normal Mode
            new DashboardPage().OpenMenuUsingTap();
            mainMenuPage = new MainMenuPage();
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText());
            ReportHelper.LogTest(Status.Pass, "The main menu is opened.");

            // Open Settings
            mainMenuPage.OpenSettings();
            settingsMenuPage = new SettingsMenuPage();
            Assert.IsNotEmpty(settingsMenuPage.GetMyHearingAidsText());
            Assert.IsNotEmpty(settingsMenuPage.GetPermissionsText());
            Assert.IsNotEmpty(settingsMenuPage.GetLanguageText());
            Assert.IsNotEmpty(settingsMenuPage.GetDemoModeText());
            ReportHelper.LogTest(Status.Pass, "The Settings menu is opened all items are visible.");

            // Open App mode selection page
            settingsMenuPage.OpenDemoMode();

            new AppModeSelectionPage().SelectAppMode(AppMode.Demo);
            new AppModeSelectionPage().Accept();
            DialogHelper.Confirm();
            ReportHelper.LogTest(Status.Info, "App changed to Demo Mode");

            VerifyNoiseReductionTailAndChecknameFiveOptions("German");

            new ProgramDetailPage().TapBack();

            // Open Mainmenu
            new DashboardPage().OpenMenuUsingTap();
            mainMenuPage = new MainMenuPage();
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText());
            ReportHelper.LogTest(Status.Pass, "The main menu is opened.");

            // Open Settings
            mainMenuPage.OpenSettings();
            settingsMenuPage = new SettingsMenuPage();
            Assert.IsNotEmpty(settingsMenuPage.GetMyHearingAidsText());
            Assert.IsNotEmpty(settingsMenuPage.GetPermissionsText());
            Assert.IsNotEmpty(settingsMenuPage.GetLanguageText());
            Assert.IsNotEmpty(settingsMenuPage.GetDemoModeText());
            ReportHelper.LogTest(Status.Pass, "The Settings menu is opened all items are visible.");

            // Open Language menu
            settingsMenuPage.OpenLanguage();
            settingLanguagePage = new SettingLanguagePage();
            var initialLangaugeRisa2 = settingLanguagePage.GetCurrentLanguageAudifon(); // preset language before changing
            ReportHelper.LogTest(Status.Info, "App is preset to " + initialLangaugeRisa2 + " language");
            ReportHelper.LogTest(Status.Pass, "The Language menu is opened and it contained all available languages for Audifon app");

            // Select English Language
            settingLanguagePage.SelectLanguageAudifon(englishLanguage);
            var selectedLanguageRisa2 = settingLanguagePage.GetSelectedLanguageAudifon();
            Assert.AreEqual(englishLanguage, selectedLanguageRisa2);
            settingLanguagePage.Accept();
            appDialog = new AppDialog();
            Assert.IsNotEmpty(appDialog.GetMessage());
            Assert.IsNotEmpty(appDialog.GetDenyButtonText());
            Assert.IsNotEmpty(appDialog.GetConfirmButtonText());
            appDialog.Confirm();
            ReportHelper.LogTest(Status.Pass, "Langauage is changed as English");

            // Verify Text in English Language
            VerifyNoiseReductionTailAndChecknameFiveOptions("English");
        }

        [Test]
        [Category("SystemTestsDeviceManual")]
        [Description("TC-15993_Table-12")]
        public void ST15993_AveragStateOfChargeForBatteryDisplay()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var firstHearingAid = SelectHearingAid.GetLeftHearingAid();
            var secondHearingAid = SelectHearingAid.GetRightHearingAid();

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTimeAndVerify(firstHearingAid, secondHearingAid);

            // Open the hearing aid information page for left hearing aid
            ReportHelper.LogTest(Status.Info, "Open Left Hearing Device information page");
            dashboardPage.OpenLeftHearingDevice();
            ReportHelper.LogTest(Status.Pass, "Left Hearing Device information page is opened");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off Left Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Left Hearing Aid turned off");

            DialogHelper.Confirm();
            ReportHelper.LogTest(Status.Pass, "Alert message displayed after turning off left hearing device");

            // Close the left hearing aid information page
            new HearingInstrumentInfoControlPage().Close();

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn on Left Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Left Hearing Aid turned on");

            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(25));
            Assert.IsTrue(new DashboardPage().GetIsLeftHearingDeviceVisible());

            new DashboardPage().OpenLeftHearingDevice();
            new HearingInstrumentInfoControlPage().CheckHAInformationFromDashboard(AppMode.Normal);
            ReportHelper.LogTest(Status.Pass, "Device details for turning on left hearing device is displayed");

            // Close the left hearing aid information page
            new HearingInstrumentInfoControlPage().Close();

            // Open right hearing aid information page
            ReportHelper.LogTest(Status.Info, "Open Right Hearing Device information page");
            new DashboardPage().OpenRightHearingDevice();
            ReportHelper.LogTest(Status.Pass, "Right Hearing Device information page is opened");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Right Hearing Aid turned off");

            DialogHelper.Confirm();
            ReportHelper.LogTest(Status.Pass, "Alert message displayed after turning off right hearing device");

            // Close the right hearing aid information page
            new HearingInstrumentInfoControlPage().Close();

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn on Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Right Hearing Aid turned on");

            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(25));
            Assert.IsTrue(new DashboardPage().GetIsRightHearingDeviceVisible());

            new DashboardPage().OpenRightHearingDevice();
            new HearingInstrumentInfoControlPage().CheckHAInformationFromDashboard(AppMode.Normal);
            ReportHelper.LogTest(Status.Pass, "Device details for turning on right hearing device is displayed");

            // Close the right hearing aid information page
            new HearingInstrumentInfoControlPage().Close();

            // Open Mainmenu
            new DashboardPage().OpenMenuUsingTap();
            var mainMenuPage = new MainMenuPage();
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText());
            ReportHelper.LogTest(Status.Pass, "The main menu is opened.");

            // Open Settings
            mainMenuPage.OpenSettings();
            var settingsMenuPage = new SettingsMenuPage();
            Assert.IsNotEmpty(settingsMenuPage.GetMyHearingAidsText());
            Assert.IsNotEmpty(settingsMenuPage.GetPermissionsText());
            Assert.IsNotEmpty(settingsMenuPage.GetLanguageText());
            Assert.IsNotEmpty(settingsMenuPage.GetDemoModeText());
            ReportHelper.LogTest(Status.Pass, "The Settings menu is opened all items are visible.");

            settingsMenuPage.OpenMyHearingAids();

            // Get current device status
            ReportHelper.LogTest(Status.Info, "Capture current status of Left hearing device from Hearing systems page");
            new HearingSystemManagementPage().LeftDeviceTabClick();
            string currentStatusLeft = new HearingSystemManagementPage().GetLeftDeviceState();
            ReportHelper.LogTest(Status.Pass, "Left hearing device status captured.");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off Left Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Left Hearing Aid turned off");

            DialogHelper.Confirm();
            ReportHelper.LogTest(Status.Pass, "Alert message displayed after turning off left hearing device");

            ReportHelper.LogTest(Status.Info, "Capture status of Left hearing device after disconnecting the left device ");
            string newStatusDisconnect = new HearingSystemManagementPage().GetLeftDeviceState();
            ReportHelper.LogTest(Status.Pass, "Disconnected Left hearing device status captured.");

            // Verify the status change after disconnecting
            Assert.AreNotEqual(currentStatusLeft, newStatusDisconnect);
            ReportHelper.LogTest(Status.Pass, "Left hearing device Status changed to Disconnected from Connected");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn on Left Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Left Hearing Aid turned on");

            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(30));

            ReportHelper.LogTest(Status.Info, "Capture status of left hearing device after turning on the left device ");
            string newStatusConnect = new HearingSystemManagementPage().GetLeftDeviceState();
            ReportHelper.LogTest(Status.Pass, "connected Left hearing device status captured.");

            // Verify the status change after connecting
            Assert.AreEqual(currentStatusLeft, newStatusConnect);
            ReportHelper.LogTest(Status.Pass, "Left hearing device Status changed to Connected from Disconnected");

            // Get current device status
            ReportHelper.LogTest(Status.Info, "Capture current status of Right hearing device from Hearing systems page");
            new HearingSystemManagementPage().RightDeviceTabClick();
            string currentStatusRight = new HearingSystemManagementPage().GetRightDeviceState();
            ReportHelper.LogTest(Status.Pass, "Right hearing device status captured.");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Right Hearing Aid turned off");

            DialogHelper.Confirm();
            ReportHelper.LogTest(Status.Pass, "Alert message displayed after turning off right hearing device");

            ReportHelper.LogTest(Status.Info, "Capture status of Right hearing device after disconnecting the left device ");
            string newStatusDisconnectRight = new HearingSystemManagementPage().GetRightDeviceState();
            ReportHelper.LogTest(Status.Pass, "Disconnected Right hearing device status captured.");

            // Verify the status change after disconnecting
            Assert.AreNotEqual(currentStatusRight, newStatusDisconnectRight);
            ReportHelper.LogTest(Status.Pass, "Right hearing device Status changed to Disconnected from Connected");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn on Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Right Hearing Aid turned on");

            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(30));

            ReportHelper.LogTest(Status.Info, "Capture status of Right hearing device after turning on the left device ");
            string newStatusConnectRight = new HearingSystemManagementPage().GetRightDeviceState();
            ReportHelper.LogTest(Status.Pass, "connected Right hearing device status captured.");

            // Verify the status change after connecting
            Assert.AreEqual(currentStatusRight, newStatusConnectRight);
            ReportHelper.LogTest(Status.Pass, "Right hearing device Status changed to connected from disconnected");

            // Navigate back to Dashboard
            new HearingSystemManagementPage().TapBack();
            new SettingsMenuPage().TapBack();
            new MainMenuPage().CloseMenuUsingTap();

            Assert.IsTrue(new DashboardPage().GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(new DashboardPage().GetIsRightHearingDeviceVisible());

            ReportHelper.LogTest(Status.Pass, "Both hearing device icon are visible on dashboard page");
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-16698_Table-6")]
        public void ST16698_VerifyLocationPermissionOnApp()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var firstHearingAid = SelectHearingAid.GetLeftHearingAid();
            var secondHearingAid = SelectHearingAid.GetRightHearingAid();

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTimeAndVerify(firstHearingAid, secondHearingAid);

            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            ReportHelper.LogTest(Status.Pass, "Start view is displayed correctly containing L and R device icons.");

            //Open Music Program
            ReportHelper.LogTest(Status.Info, "Open Music program..");
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            var programDetailPage = new ProgramDetailPage();
            Assert.IsNotEmpty(programDetailPage.GetCurrentProgramName());
            Assert.IsTrue(programDetailPage.GetIsBinauralSettingsButtonVisible());
            Assert.IsTrue(programDetailPage.GetIsSettingsButtonDisplayed());
            ReportHelper.LogTest(Status.Pass, "Music program is opened successfully");

            //Open ProgramSettings (gear icon)
            ReportHelper.LogTest(Status.Info, "Open Program settings and verify the view.");
            programDetailPage.OpenProgramSettings();
            var programSettingsControlPage = new ProgramDetailSettingsControlPage();
            Assert.IsTrue(programSettingsControlPage.GetIsCreateFavoriteVisible());
            Assert.IsTrue(programSettingsControlPage.GetIsCustomizeIconVisible());
            Assert.IsTrue(programSettingsControlPage.GetIsCustomizeNameVisible());
            Assert.IsTrue(programSettingsControlPage.GetIsCreateFavoriteFloatingButtonDisplayed());
            ReportHelper.LogTest(Status.Pass, "Customize name, Customize icon, Create favorites and '+' are visible correctly.");

            programSettingsControlPage.NavigateBack();

            Assert.IsTrue(new ProgramDetailPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));

            ReportHelper.LogTest(Status.Info, "Create a favorite and provide a name and icon.");
            var favoriteProgramOne = "fav-1";
            var programAutomationPage = FavoriteHelper.CreateFavoriteHearingProgram(favoriteProgramOne, 2);

            ReportHelper.LogTest(Status.Info, "Set a geo location for program autostart.");

            AppManager.DeviceSettings.GrantGPSBackgroundPermission();
            Thread.Sleep(5000);

            // Enable Wifi
            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            programAutomationPage.TapConnectToLocation();

            // As after granting 'Allow always' permission need to reopen Map to select location
            Thread.Sleep(3000);
            new AutomationGeofenceBindingPage().NavigateBack();
            new ProgramAutomationPage().TapConnectToLocation();

            //select location
            new AutomationGeofenceBindingPage().WaitUntilNoLoadingIndicator().SelectPosition(0.5, 0.7);
            Thread.Sleep(1000);
            new AutomationGeofenceBindingPage().Ok();

            programAutomationPage = new ProgramAutomationPage();
            Assert.IsTrue(programAutomationPage.GetIsAutomationSwitchChecked());
            Assert.IsNotNull(programAutomationPage.GeofenceAutomation.GetValue());
            programAutomationPage.Proceed();

            Assert.IsTrue(new ProgramDetailPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            Assert.AreEqual(favoriteProgramOne, new ProgramDetailPage().GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "Favorite is saved with location and autostart.");

            ReportHelper.LogTest(Status.Info, "Select any other program and restart the app.");
            new ProgramDetailPage().SelectProgram(2);
            Assert.AreNotEqual(favoriteProgramOne, new ProgramDetailPage().GetCurrentProgramName());

            AppManager.RestartApp(false);
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            //Assert.AreEqual(favoriteProgramOne, new DashboardPage().GetCurrentProgramName());
            ReportHelper.LogTest(Status.Info, "App is restarted and favorite program is set as default.");
            new DashboardPage().OpenCurrentProgram();
            Assert.IsTrue(new ProgramDetailPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));

            //Revoking the app permission on runtime results in app closing abruptly
            //Discussed with Audifon team to skip this step
            //AppManager.DeviceSettings.RevokeGPSBackgroundPermission();

            ReportHelper.LogTest(Status.Info, "Create second favorite program with no autostart.");
            var favoriteProgramTwo = "fav-2";
            programAutomationPage = FavoriteHelper.CreateFavoriteHearingProgram(favoriteProgramTwo, 3);
            programAutomationPage.Proceed();

            Assert.IsTrue(new ProgramDetailPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            Assert.AreEqual(favoriteProgramTwo, new ProgramDetailPage().GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "Second favorite is saved successfully.");

            ReportHelper.LogTest(Status.Info, "Open program setting for second favorite program.");
            new ProgramDetailPage().OpenProgramSettings();
            programSettingsControlPage = new ProgramDetailSettingsControlPage();
            Assert.IsTrue(programSettingsControlPage.GetIsCreateFavoriteVisible());
            Assert.IsTrue(programSettingsControlPage.GetIsCustomizeIconVisible());
            Assert.IsTrue(programSettingsControlPage.GetIsCustomizeNameVisible());
            Assert.IsFalse(programSettingsControlPage.GetIsAutoStartEnabled());
            Assert.IsTrue(programSettingsControlPage.GetIsDeleteFavoriteVisible());
            ReportHelper.LogTest(Status.Pass, "Auto-start is disabled for second favorite and other options are displayed successfully.");
            programSettingsControlPage.NavigateBack();

            Assert.IsTrue(new ProgramDetailPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            new ProgramDetailPage().NavigateBack();
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 0);

            ReportHelper.LogTest(Status.Info, "Open program setting for first favorite program.");
            new ProgramDetailPage().OpenProgramSettings();
            programSettingsControlPage = new ProgramDetailSettingsControlPage();
            Assert.IsTrue(programSettingsControlPage.GetIsCreateFavoriteVisible());
            Assert.IsTrue(programSettingsControlPage.GetIsCustomizeIconVisible());
            Assert.IsTrue(programSettingsControlPage.GetIsCustomizeNameVisible());
            Assert.IsTrue(programSettingsControlPage.GetIsAutoStartEnabled());
            Assert.IsTrue(programSettingsControlPage.GetIsDeleteFavoriteVisible());
            ReportHelper.LogTest(Status.Pass, "Auto-start is enabled for first favorite and other options are displayed successfully.");
            programSettingsControlPage.NavigateBack();

            AppManager.DeviceSettings.DisableWifi();
        }

        /// <summary>
        /// Below test case has a step to disable GPS before starting the app.
        /// After disabling GPS the HA cannot be connected and skipping this step.
        /// Discussed with Audifon team.
        /// </summary>
        [Test]
        [Category("SystemTestsDeviceManual")]
        [Description("TC-16730_Table-5")]
        public void ST16730_VerifyFindDevicesPageWhenHADisconnected()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            double thresholdValue = 0.999; //threadshold value for image comparison score

            var firstHearingAid = SelectHearingAid.Left();
            var secondHearingAid = SelectHearingAid.Right();

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTimeAndVerify(firstHearingAid.Name, secondHearingAid.Name);

            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            ReportHelper.LogTest(Status.Pass, "Start view is displayed correctly containing L and R device icons.");

            //Changing the hearing program
            string lastName = null;
            for (int i = 0; i < dashboardPage.GetNumberOfPrograms(); i++)
            {
                dashboardPage.SelectProgram(i);
                Assert.IsTrue(dashboardPage.GetIsProgramSelected(i), $"Failed to select a hearing program {i}.");
                string newName = dashboardPage.GetCurrentProgramName();
                Assert.AreNotEqual(lastName, newName, "Duplicate hearing program names found.");
                lastName = newName;
            }
            ReportHelper.LogTest(Status.Pass, "Changing the hearing program on Start view is successful.");

            ReportHelper.LogTest(Status.Info, "Change the volume on Start view to Zero.");
            dashboardPage.SetVolumeSliderValue(0.5);
            var initialVolume = dashboardPage.GetVolumeSliderValue();
            ReportHelper.LogTest(Status.Info, "Captured Intial value");
            dashboardPage.DecreaseVolume();
            Thread.Sleep(500);
            ReportHelper.LogTest(Status.Info, "Volume decreased");
            dashboardPage.SetVolumeSliderValue(0.5);
            Thread.Sleep(500);
            ReportHelper.LogTest(Status.Info, "Volume set to Zero again");
            dashboardPage.DecreaseVolume();
            Thread.Sleep(500);
            dashboardPage.DecreaseVolume();
            Thread.Sleep(2000);
            ReportHelper.LogTest(Status.Info, "Volume decreased twice");
            Assert.AreNotEqual(initialVolume, dashboardPage.GetVolumeSliderValue());
            ReportHelper.LogTest(Status.Pass, "Change in volume is successfull on Start view.");
            // Reset the volume to default
            dashboardPage.SetVolumeSliderValue(0.5);

            //TODO -: Changing program and volume from HA is not automated.
            // The verfication of the change is only done by the app not from HA

            ReportHelper.LogTest(Status.Info, "Navigate to program detail view of Noise Comfort program.");
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            var programDetailPage = new ProgramDetailPage();
            Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsEqualizerDisplayVisible());
            ReportHelper.LogTest(Status.Pass, "Noise Comfort program is opened. All UI elements are visible");
            ReportHelper.LogTest(Status.Info, "Change program settings of parameters of Noise Comfort program.");

            //Noise Reduction
            var noiseComfortNoiseReduction = NoiseReduction.Off;
            programDetailPage.NoiseReductionDisplay.OpenSettings();
            new ProgramDetailParamEditNoiseReductionPage().SelectNoiseReduction(noiseComfortNoiseReduction);
            new ProgramDetailParamEditNoiseReductionPage().Close();
            Assert.AreEqual(noiseComfortNoiseReduction.ToString(), new ProgramDetailPage().NoiseReductionDisplay.GetValue());
            ReportHelper.LogTest(Status.Pass, "Changed setting for Noise Reduction.");

            //Speech Focus
            var noiseComfortSpeechFocus = SpeechFocus.Off;
            new ProgramDetailPage().SpeechFocusDisplay.OpenSettings();
            new ProgramDetailParamEditSpeechFocusPage().SelectSpeechFocus(noiseComfortSpeechFocus);
            new ProgramDetailParamEditSpeechFocusPage().Close();
            Assert.AreEqual(noiseComfortSpeechFocus.ToString(), new ProgramDetailPage().SpeechFocusDisplay.GetValue());
            ReportHelper.LogTest(Status.Pass, "Changed setting for Speech Focus.");

            //Sound
            var noiseComfortequalizerBand = EqBand.Mid;
            var noiseComfortValue = 1;
            new ProgramDetailPage().EqualizerDisplay.OpenSettings();
            new ProgramDetailParamEditEqualizerPage().SetEqualizerSliderValue(noiseComfortequalizerBand, noiseComfortValue);
            new ProgramDetailParamEditEqualizerPage().Close();
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(noiseComfortValue, programDetailPage.EqualizerDisplay.GetEqualizerSliderValue(noiseComfortequalizerBand));
            ReportHelper.LogTest(Status.Pass, "Changed setting for Equalizer.");

            //Changing the hearing program
            ReportHelper.LogTest(Status.Info, "Verify the swithcing of programs from program detail view.");
            lastName = null;
            for (int i = 0; i < programDetailPage.GetNumberOfVisibiblePrograms(); i++)
            {
                programDetailPage.SelectProgram(i);
                string newName = programDetailPage.GetCurrentProgramName();
                Assert.AreNotEqual(lastName, newName, "Switching of program was unsuccessfull.");
                lastName = newName;
            }
            ReportHelper.LogTest(Status.Pass, "Changing the hearing program on Program detail view is successful.");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off and turn on Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Right Hearing Aid turned off and turned on");

            DialogHelper.ConfirmIfDisplayed();
            //Assert.IsTrue(DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(60)));
            ReportHelper.LogTest(Status.Pass, "Pop up appears successfully after HA disconnected and is confirmed.");

            Thread.Sleep(5000);
            Assert.IsTrue(new ProgramDetailPage().GetIsRightHearingDeviceVisible());
            ReportHelper.LogTest(Status.Info, "Right Device Settings Loaded");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off and turn on Left Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Left Hearing Aid turned off and turned on");

            DialogHelper.ConfirmIfDisplayed();
            //Assert.IsTrue(DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(60)));
            ReportHelper.LogTest(Status.Pass, "Pop up appears successfully after HA disconnected and is confirmed.");

            Thread.Sleep(5000);
            Assert.IsTrue(new ProgramDetailPage().GetIsLeftHearingDeviceVisible());
            ReportHelper.LogTest(Status.Info, "Left Device Settings Loaded");

            ReportHelper.LogTest(Status.Info, "Navigate to Find Hearing Aids and verify the functionality.");

            // ToDo: In some devices when device is diconnected it goes to Dashboard Page but in some devices it goes to Basic Program
            bool IsProgramDetailPage = new ProgramDetailPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(2));
            if (IsProgramDetailPage)
                new ProgramDetailPage().TapBack();

            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            NavigationHelper.NavigateToHelpMenu(new DashboardPage()).OpenFindHearingDevices();
            new FindDevicesPage().CheckFindDevicesPage(ChannelMode.Binaural, firstHearingAid, secondHearingAid);

            //Verify Near Field for Right HA
            var baseLineImageRight = AppManager.App.TakeScreenshot().AsBase64EncodedString;
            ReportHelper.LogTest(Status.Info, "Verifying the Near Field Signal for Right Device");
            ReportHelper.LogTest(Status.Info, "Taking baseline screeshot for Right device now");
            Assert.IsNotEmpty(baseLineImageRight);
            ReportHelper.LogTest(Status.Info, "Baseline screenshot is taken Move the device now");

            //MANUAL -> Move device away from HA in order to make Near Field view change
            ReportHelper.LogTest(Status.Info, "ALERT: Please Move Right Hearing Aid Away within 10 seconds");
            Thread.Sleep(10000);

            ReportHelper.LogTest(Status.Info, "Taking new screeshot now");

            // ToDo: Need to install OpenCV and the uncomment the below three lines and need test this
            //double scoreRight = ImageComparison.GetImageSimilarityScore(baseLineImageRight, AppManager.App.TakeScreenshot().AsBase64EncodedString);
            //ReportHelper.LogTest(Status.Info, "Image similarity score for Right device is " + scoreRight);
            //Assert.Less(scoreRight, thresholdValue, "There are no visual differences in signal strength for Right Device.");
            ReportHelper.LogTest(Status.Pass, "Check successful, On moving the device away, signal strength change is visible for Right HA.");

            //Verify Near Field for Left HA
            Assert.IsTrue(new FindDevicesPage().SelectLeftDevice().GetIsLeftDeviceSelected());
            Assert.IsTrue(new FindDevicesPage().GetIsNearFieldViewSelected());
            ReportHelper.LogTest(Status.Info, "Verifying the Near Field Signal for Left HA");
            ReportHelper.LogTest(Status.Info, "Taking baseline screeshot for Left device now");
            var baseLineImageLeft = AppManager.App.TakeScreenshot().AsBase64EncodedString;
            Assert.IsNotEmpty(baseLineImageLeft);
            ReportHelper.LogTest(Status.Info, "Baseline screenshot is taken Move the device now");

            //MANUAL -> Move device away from HA in order to make Near Field view to change
            ReportHelper.LogTest(Status.Info, "ALERT: Please Move Left Hearing Aid Away within 10 seconds");
            Thread.Sleep(10000);

            ReportHelper.LogTest(Status.Info, "Taking new screeshot now");

            // ToDo: Need to install OpenCV and the uncomment the below three lines and need test this
            //var scoreLeft = ImageComparison.GetImageSimilarityScore(baseLineImageRight, AppManager.App.TakeScreenshot().AsBase64EncodedString);
            //ReportHelper.LogTest(Status.Info, "Image similarity score for Left device is " + scoreLeft);
            //Assert.Less(scoreLeft, thresholdValue, "There are no visual differences in signal strength for Left Device.");
            ReportHelper.LogTest(Status.Pass, "Check successful, signal strength is visible for Left HA.");

            AppManager.DeviceSettings.DisableWifi();
        }

        [Test]
        [Category("SystemTestsDeviceManual")]
        [Description("TC-16544_Table-7")]
        public void ST16544_UseAndroidBoundService()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var firstHearingAid = SelectHearingAid.Left();
            var secondHearingAid = SelectHearingAid.Right();

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTimeAndVerify(firstHearingAid.Name, secondHearingAid.Name);

            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            ReportHelper.LogTest(Status.Pass, "Start view is displayed correctly containing L and R device icons.");

            //Changing the hearing program
            string lastName = null;
            for (int i = 0; i < dashboardPage.GetNumberOfPrograms(); i++)
            {
                dashboardPage.SelectProgram(i);
                Assert.IsTrue(dashboardPage.GetIsProgramSelected(i), $"Failed to select a hearing program {i}.");
                string newName = dashboardPage.GetCurrentProgramName();
                Assert.AreNotEqual(lastName, newName, "Duplicate hearing program names found.");
                lastName = newName;
            }
            ReportHelper.LogTest(Status.Pass, "Changing the hearing program on Start view is successful.");

            ReportHelper.LogTest(Status.Info, "Change the volume on Start view.");
            var initialVolume = dashboardPage.GetVolumeSliderValue();
            dashboardPage.DecreaseVolume();
            dashboardPage.SetVolumeSliderValue(0.5);
            dashboardPage.DecreaseVolume().DecreaseVolume();
            //Assert.AreNotEqual(initialVolume, new DashboardPage().GetVolumeSliderValue());
            ReportHelper.LogTest(Status.Pass, "Change in volume is successfull on Start view.");

            //TODO -: Changing program and volume from HA is not automated.
            // The verfication of the change is only done by the app not from HA

            ReportHelper.LogTest(Status.Info, "Navigate to program detail view of Noise Comfort program.");
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            var programDetailPage = new ProgramDetailPage();
            Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsEqualizerDisplayVisible());
            ReportHelper.LogTest(Status.Pass, "Noise Comfort program is opened. All UI elements are visible");
            ReportHelper.LogTest(Status.Info, "Change program settings of parameters of Noise Comfort program.");

            //Noise Reduction
            var noiseComfortNoiseReduction = NoiseReduction.Off;
            programDetailPage.NoiseReductionDisplay.OpenSettings();
            new ProgramDetailParamEditNoiseReductionPage().SelectNoiseReduction(noiseComfortNoiseReduction);
            new ProgramDetailParamEditNoiseReductionPage().Close();
            Assert.AreEqual(noiseComfortNoiseReduction.ToString(), new ProgramDetailPage().NoiseReductionDisplay.GetValue());
            ReportHelper.LogTest(Status.Pass, "Changed setting for Noise Reduction.");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off and turn on Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Right Hearing Aid turned of and turned on");

            DialogHelper.ConfirmIfDisplayed();
            //Assert.IsTrue(DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(60)));
            ReportHelper.LogTest(Status.Pass, "Pop up appears successfully after HA disconnected and is confirmed.");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off and trun on Left Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Left Hearing Aid turned off and turned on");

            DialogHelper.ConfirmIfDisplayed();
            //Assert.IsTrue(DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(60)));
            ReportHelper.LogTest(Status.Pass, "Pop up appears successfully after HA disconnected and is confirmed.");

            ReportHelper.LogTest(Status.Info, "Both Hearing Aids turned off and turned on");

            // Restart the App
            ReportHelper.LogTest(Status.Info, "Restart app and disconnect HA and verify the behaviour.");
            AppManager.RestartApp(false);

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off Both Left and Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Left and Right Hearing Aid turned off");

            Assert.IsTrue(new HearingAidInitPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)));
            Assert.IsTrue(new HearingAidInitPage().LeftHearingAid.IsVisible);
            Assert.IsTrue(new HearingAidInitPage().RightHearingAid.IsVisible);

            // In IOS it takes 120 secs to go to Connection Error Page when HA is disconnected in the start
            Assert.IsTrue(new HearingAidConnectionErrorPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(130)));
            var hardwareErrorPage = new HearingAidConnectionErrorPage();
            Assert.IsNotEmpty(hardwareErrorPage.GetPageTitle());
            Assert.IsNotEmpty(hardwareErrorPage.GetPageMessage());
            ReportHelper.LogTest(Status.Pass, "Hardware Error page is shown correctly.");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn on Both Left and Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Left and Right Hearing Aid turned on");

            Thread.Sleep(2000);
            hardwareErrorPage.RetryConnection();
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            Assert.IsTrue(new DashboardPage().GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(new DashboardPage().GetIsRightHearingDeviceVisible());
            dashboardPage = new DashboardPage();

            //The test step to change and verify the volume when the app is in background is not automated.
            ReportHelper.LogTest(Status.Info, "Change the volume on Start view.");
            initialVolume = dashboardPage.GetVolumeSliderValue();
            dashboardPage.DecreaseVolume();
            dashboardPage.SetVolumeSliderValue(0.5);
            dashboardPage.DecreaseVolume().DecreaseVolume();

            ReportHelper.LogTest(Status.Info, "Change the program on Start view.");
            dashboardPage.SelectProgram(3);
            string programName = dashboardPage.GetCurrentProgramName();

            ReportHelper.LogTest(Status.Info, "Now putting app in background for 10 sec and again verfiy the change in Volume and Program.");
            AppManager.DeviceSettings.PutAppToBackground(10);
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));

            Assert.AreNotEqual(initialVolume, new DashboardPage().GetVolumeSliderValue());
            Assert.AreEqual(programName, new DashboardPage().GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "App is in foreground again. Does not crash. Volume and Program name changes are visible.");

            ReportHelper.LogTest(Status.Info, "Navigate to Settings and switch to Demo Mode.");
            NavigationHelper.NavigateToSettingsMenu(new DashboardPage()).OpenDemoMode();
            Assert.IsTrue(new AppModeSelectionPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            var appMode = new AppModeSelectionPage().GetSelectedAppMode();
            Assert.AreEqual(appMode, AppMode.Normal);
            new AppModeSelectionPage().ChangeAppMode(AppMode.Demo);

            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            dashboardPage = new DashboardPage();
            ReportHelper.LogTest(Status.Pass, "App is started successfully in Demo mode.");
        }

        /// <summary>
        /// This testcase involves automating MFi settings on ios which are outside the app
        /// Discussed with Audifon team to skip the test steps
        /// </summary>
        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-15100_Table-28")]
        public void ST15100_VerifyConnectionByMFI()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var firstHearingAid = SelectHearingAid.GetLeftHearingAid();
            var secondHearingAid = SelectHearingAid.GetRightHearingAid();

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTimeAndVerify(firstHearingAid, secondHearingAid);

            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            // Adding this since even if we add WaitForToastToDisappear still it is not waiting for it in Regression Testing
            Thread.Sleep(3000);
            ReportHelper.LogTest(Status.Pass, "Start view is displayed correctly containing L and R device icons.");

            ReportHelper.LogTest(Status.Info, "Navigate to Settings and switch to Demo Mode.");
            NavigationHelper.NavigateToSettingsMenu(new DashboardPage()).OpenDemoMode();
            Assert.IsTrue(new AppModeSelectionPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            var appMode = new AppModeSelectionPage().GetSelectedAppMode();
            Assert.AreEqual(appMode, AppMode.Normal);
            new AppModeSelectionPage().ChangeAppMode(AppMode.Demo);

            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            // Adding this since even if we add WaitForToastToDisappear still it is not waiting for it in Regression Testing
            Thread.Sleep(3000);
            ReportHelper.LogTest(Status.Pass, "App is started successfully in Demo mode.");

            ReportHelper.LogTest(Status.Info, "Navigate to Settings and switch to Normal Mode.");
            NavigationHelper.NavigateToSettingsMenu(new DashboardPage()).OpenDemoMode();
            Assert.IsTrue(new AppModeSelectionPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            appMode = new AppModeSelectionPage().GetSelectedAppMode();
            Assert.AreEqual(appMode, AppMode.Demo);
            new AppModeSelectionPage().ChangeAppMode(AppMode.Normal);

            // After setting to normal mode the app lands in InitializeHardwarePage only for Kind
            if (AppManager.Brand == Brand.Kind)
            {
                Assert.IsTrue(new InitializeHardwarePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
                ReportHelper.LogTest(Status.Pass, "InitializeHardwarePage Page is displayed correctly after changing app mode.");

                dashboardPage = LaunchHelper.ReconnectHearingAidsFromStartScan(firstHearingAid, secondHearingAid);
            }
            else
            {
                Assert.IsTrue(new IntroPageOne().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
                ReportHelper.LogTest(Status.Pass, "Welcome Page is displayed correctly after changing app mode.");

                dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTimeAndVerify(firstHearingAid, secondHearingAid);
            }

            //Test steps to put app in background and navigate to iPhone system setting is not automated 

            ReportHelper.LogTest(Status.Info, "Started the app in Normal mode connecting two HA.");

            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            ReportHelper.LogTest(Status.Pass, "App is started successfully in Normal mode.");

            ReportHelper.LogTest(Status.Info, "Opening Left device info page from Dashboard.");
            dashboardPage.OpenLeftHearingDevice();
            new HearingInstrumentInfoControlPage().CheckHAInformationFromDashboard(AppMode.Normal).Close();
            ReportHelper.LogTest(Status.Pass, "All details are visible on Left Hearing Instrument Info page.");

            ReportHelper.LogTest(Status.Info, "Opening Right device info page from Dashboard.");
            new DashboardPage().OpenRightHearingDevice();
            new HearingInstrumentInfoControlPage().CheckHAInformationFromDashboard(AppMode.Normal).Close();
            ReportHelper.LogTest(Status.Pass, "All details are visible on Right Hearing Instrument Info page.");
        }

        #endregion Sprint 18

        #endregion Test Cases
    }
}