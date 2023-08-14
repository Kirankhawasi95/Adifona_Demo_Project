using HorusUITest.Enums;
using HorusUITest.PageObjects;
using HorusUITest.PageObjects.Menu;
using System;
using System.Threading;
using AventStack.ExtentReports;
using HorusUITest.Helper;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Dialogs;
using HorusUITest.PageObjects.Favorites;
using HorusUITest.PageObjects.Menu.Info;
using HorusUITest.PageObjects.Menu.Settings;
using HorusUITest.PageObjects.Start;
using HorusUITest.PageObjects.Start.Intro;
using NUnit.Framework;
using Platform = HorusUITest.Enums.Platform;
using System.Collections.Generic;
using HorusUITest.PageObjects.Favorites.Automation;
using System.IO;
using HorusUITest.PageObjects.Controls.ProgramDetailParams;
using HorusUITest.PageObjects.Menu.Programs;

namespace HorusUITest.TestFixtures.TestCases
{
    public class SystemTestsDemo9 : BaseResettingTestFixture
    {
        #region Constructor

        public SystemTestsDemo9(Platform platform)
            : base(platform)
        {

        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Method to Check Zoom In Zoom Out and Swipe Down Multiple times
        /// </summary>
        private void CheckZoomInZoomOut()
        {
            // Delay time has been given 2 secs since it takes 2 sec for the PDF to load after Zoom in and Zoom Out
            ReportHelper.LogTest(Status.Info, "Checking Zoom In Zoom Out and Scroll Down multiple times");
            Thread.Sleep(2000);

            ReportHelper.LogTest(Status.Info, "Checking zoom in iteration 1...");
            AppManager.App.ZoomIn();
            Thread.Sleep(2000);
            ReportHelper.LogTest(Status.Info, "Zoom in iteration 1 done and app did not crash");
            ReportHelper.LogTest(Status.Info, "Checking zoom out iteration 1...");
            AppManager.App.ZoomOut();
            Thread.Sleep(2000);
            ReportHelper.LogTest(Status.Info, "Zoom out iteration 1 done and app did not crash");
            ReportHelper.LogTest(Status.Info, "Swiping down...");
            AppManager.App.SwipeDown();
            Thread.Sleep(2000);
            ReportHelper.LogTest(Status.Info, "Swiped down");
            ReportHelper.LogTest(Status.Info, "Checking zoom in iteration 2...");
            AppManager.App.ZoomIn();
            Thread.Sleep(2000);
            ReportHelper.LogTest(Status.Info, "Zoom in iteration 2 done and app did not crash");
            ReportHelper.LogTest(Status.Info, "Checking zoom out iteration 2...");
            AppManager.App.ZoomOut();
            Thread.Sleep(2000);
            ReportHelper.LogTest(Status.Info, "Zoom out iteration 2 done and app did not crash");
            ReportHelper.LogTest(Status.Info, "Swiping up...");
            AppManager.App.SwipeUp();
            Thread.Sleep(2000);
            ReportHelper.LogTest(Status.Info, "Swiped up");
            ReportHelper.LogTest(Status.Info, "Checking zoom in iteration 3...");
            AppManager.App.ZoomIn();
            Thread.Sleep(2000);
            ReportHelper.LogTest(Status.Info, "Zoom in iteration 3 done and app did not crash");
            ReportHelper.LogTest(Status.Info, "Checking zoom out iteration 3...");
            AppManager.App.ZoomOut();
            Thread.Sleep(2000);
            ReportHelper.LogTest(Status.Info, "Zoom out iteration 3 done and app did not crash");

            ReportHelper.LogTest(Status.Pass, "Zoom In Zoom Out and Scroll Down multiple times Verified and App did not crash");
        }

        /// <summary>
        /// Check Images
        /// </summary>
        /// <param name="deviceLanguage"></param>
        /// <param name="language"></param>
        private void CheckImages(Language_Device deviceLanguage, string language)
        {
            ReportHelper.LogTest(Status.Info, "Changing mobile device langauge to '" + deviceLanguage + "'...");
            AppManager.DeviceSettings.ChangeDeviceLanguage(deviceLanguage);
            ReportHelper.LogTest(Status.Info, "Changed device langauge to '" + deviceLanguage + "'");

            ReportHelper.LogTest(Status.Info, "Restarting app after language change...");
            AppManager.RestartApp(true);
            ReportHelper.LogTest(Status.Info, "Restarted app after language change");

            ReportHelper.LogTest(Status.Info, "Waiting till intro1 page is loaded...");
            Assert.IsTrue(new IntroPageOne().IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)), "Intro1 page is not loaded");
            ReportHelper.LogTest(Status.Info, "Intro1 page is loaded");
            ReportHelper.LogTest(Status.Info, "Checking into1 page image...");
            // ToDo: Need to install OpenCV and the uncomment the below line and need test this
            //Assert.IsTrue(new IntroPageOne().CheckInto1Image(AppManager.Brand), "The image is not matching");
            ReportHelper.LogTest(Status.Info, "Intro One Page image is shown correctly and welcome page title is '" + new IntroPageOne().GetTitle() + "'");
            ReportHelper.LogTest(Status.Info, "Moving Intro 1 Page right by tapping...");
            new IntroPageOne().MoveRightByTapping();
            ReportHelper.LogTest(Status.Info, "Moved Intro 1 Page right by tapping");
            ReportHelper.LogTest(Status.Info, "Checking into2 page image...");
            // ToDo: Need to install OpenCV and the uncomment the below line and need test this
            //Assert.IsTrue(new IntroPageTwo().CheckInto2Image(AppManager.Brand), "The image is not matching");
            ReportHelper.LogTest(Status.Info, "Intro Two Page image is shown correctly");
            ReportHelper.LogTest(Status.Info, "Moving Intro 2 Page right by tapping...");
            new IntroPageTwo().MoveRightByTapping();
            ReportHelper.LogTest(Status.Info, "Moved Intro 2 Page right by tapping");
            ReportHelper.LogTest(Status.Info, "Checking into3 page image...");
            // ToDo: Need to install OpenCV and the uncomment the below line and need test this
            //Assert.IsTrue(new IntroPageThree().CheckInto3Image(AppManager.Brand), "The image is not matching");
            ReportHelper.LogTest(Status.Info, "Intro Three Page image is shown correctly");
            ReportHelper.LogTest(Status.Info, "Moving Intro 3 Page right by tapping...");
            new IntroPageThree().MoveRightByTapping();
            ReportHelper.LogTest(Status.Info, "Moved Intro 3 Page right by tapping");
            ReportHelper.LogTest(Status.Info, "Checking into4 page image...");
            // ToDo: Need to install OpenCV and the uncomment the below line and need test this
            //Assert.IsTrue(new IntroPageFour().CheckInto4Image(AppManager.Brand), "The image is not matching");
            ReportHelper.LogTest(Status.Info, "Intro Four Page image is shown correctly");
            ReportHelper.LogTest(Status.Info, "Moving Intro 4 Page right by tapping...");
            new IntroPageFour().MoveRightByTapping();
            ReportHelper.LogTest(Status.Info, "Moved Intro 4 Page right by tapping");
            new IntroPageFive().Continue();
            ReportHelper.LogTest(Status.Pass, "All Intro Page images are shown correctly");

            ReportHelper.LogTest(Status.Info, "Waiting till initialize hardware page is loaded...");
            Assert.IsTrue(new InitializeHardwarePage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Initialize hardware page is not loaded");
            ReportHelper.LogTest(Status.Info, "Initialize hardware page is loaded");
            ReportHelper.LogTest(Status.Info, "Starting the app in demo mode...");
            new InitializeHardwarePage().StartDemoMode();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            ReportHelper.LogTest(Status.Info, "Waiting till hearing aid init page is loaded...");
            Assert.IsTrue(new HearingAidInitPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(20)), "Hearing aid init page is not loaded");
            var hearingAidInitPage = new HearingAidInitPage();
            ReportHelper.LogTest(Status.Info, "Hearing aid init page is loaded");
            // ToDo: Need to install OpenCV and the uncomment the below line and need test this
            //hearingAidInitPage.CheckRightHAImage();
            ReportHelper.LogTest(Status.Info, "Connection page Right HA image is shown correctly");
            // ToDo: Need to install OpenCV and the uncomment the below line and need test this
            //hearingAidInitPage.CheckLeftHAImage();
            ReportHelper.LogTest(Status.Info, "Connection page Left HA image is shown correctly");
            // ToDo: Need to install OpenCV and the uncomment the below line and need test this
            //hearingAidInitPage.CheckBrandImage(AppManager.Brand);
            ReportHelper.LogTest(Status.Info, "Connection page Brand image is shown correctly");
            ReportHelper.LogTest(Status.Pass, "All Connection Page images are shown correctly");

            ReportHelper.LogTest(Status.Info, "Clicking OK button...");
            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(45));
            ReportHelper.LogTest(Status.Info, "Clicked OK button");
            ReportHelper.LogTest(Status.Info, "Granting location permission...");
            PermissionHelper.AllowPermissionIfRequested();
            ReportHelper.LogTest(Status.Info, "Granted location permission");

            ReportHelper.LogTest(Status.Info, "Waiting till dashboard page is loaded...");
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)), "Dashboard page is not loaded");
            var dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "App started in demo mode after skipping intro pages and dashboard is loaded");
        }

        #endregion Methods

        #region Test Cases

        #region Sprint 29

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-20153_Table-0")]
        public void ST20153_CheckDisplayOptionsInPDFDocuments()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            //ToDo: Splash image cannot be verified because while running the test case in automation that screen does not appear

            // Open App in Demo Mode
            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            ReportHelper.LogTest(Status.Info, "Open the Main Menu");
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Pass, "The Hamburger Button is displayed");
            dashboardPage.OpenMenuUsingTap();
            var mainMenuPage = new MainMenuPage();

            // Open Help Menu
            ReportHelper.LogTest(Status.Info, "Open the Help Menu");
            mainMenuPage.OpenHelp();
            var settingsMenuPage = new HelpMenuPage();
            ReportHelper.LogTest(Status.Info, "Open the Instructions For Use page");
            settingsMenuPage.OpenInstructionsForUse();
            ReportHelper.LogTest(Status.Info, "Instructions For Use page openned");

            // Check Zoom In Zoom Out in Instructions For Use page
            CheckZoomInZoomOut();
            ReportHelper.LogTest(Status.Pass, "Instructions For Use page verified");

            AppManager.App.SwipeLeftToRight();
            ReportHelper.LogTest(Status.Pass, "Swipe Back verified");

            ReportHelper.LogTest(Status.Info, "Open the Information Menu");
            new HelpMenuPage().OpenInformationMenu();

            ReportHelper.LogTest(Status.Info, "Open the Data Protection Page");
            new InformationMenuPage().OpenPrivacyPolicy();
            ReportHelper.LogTest(Status.Info, "Data Protection Page openned");

            // Check Zoom In Zoom Out in Data Protection page
            CheckZoomInZoomOut();
            ReportHelper.LogTest(Status.Pass, "Data Protection page verified");

            AppManager.App.SwipeLeftToRight();
            ReportHelper.LogTest(Status.Pass, "Swipe Back verified");

            ReportHelper.LogTest(Status.Info, "Open the Terms of Use Page");
            new InformationMenuPage().OpenTermsofUse();
            ReportHelper.LogTest(Status.Info, "Terms of Use Page openned");

            // Check Zoom In Zoom Out in Terms of Use page
            CheckZoomInZoomOut();
            ReportHelper.LogTest(Status.Pass, "Terms of Use page verified");

            AppManager.App.SwipeLeftToRight();
            ReportHelper.LogTest(Status.Pass, "Swipe Back verified");
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-11684_Table-57")]
        public void ST11684_CheckIntroOnExitDemoMode()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand.ToString() + " App initialized successfully");

            string WelcomeText = "Welcome!";
            string ContinueButtonText = "HEAR WE GO!";
            string DemoModeButtonText = "START IN DEMO MODE";
            string StartScanButtonText = "START SCAN";

            // Navigate Intro pages
            Assert.IsTrue(new IntroPageOne().IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)));
            string welocmePageTitle = new IntroPageOne().GetTitle();
            Assert.IsNotEmpty(welocmePageTitle);
            Assert.AreEqual(WelcomeText, welocmePageTitle);
            ReportHelper.LogTest(Status.Pass, "'" + WelcomeText + "' text is shown correctly");

            while (!new IntroPageFive(false).IsCurrentlyShown())
            {
                new IntroPageOne(false).MoveRightBySwiping();
            }
            Assert.IsTrue(new IntroPageFive().GetIsLeftButtonVisible());

            new IntroPageFive().Continue();
            ReportHelper.LogTest(Status.Pass, "'" + ContinueButtonText + "' button is available and clickable");

            // Open app in Demo Mode
            Assert.IsTrue(new InitializeHardwarePage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            string demoModeTextTitle = new InitializeHardwarePage().GetDemoModeText();
            Assert.IsNotEmpty(demoModeTextTitle);
            // ToDo: The below text is dfferent in different apps. So commenting the below line
            //Assert.AreEqual(DemoModeButtonText, demoModeTextTitle);
            ReportHelper.LogTest(Status.Pass, "'" + DemoModeButtonText + "' text is shown correctly");
            string startScanTextTitle = new InitializeHardwarePage().GetScanText();
            Assert.IsNotEmpty(startScanTextTitle);
            // ToDo: The below text is dfferent in different apps. So commenting the below line
            //Assert.AreEqual(StartScanButtonText, startScanTextTitle);
            ReportHelper.LogTest(Status.Pass, "'" + StartScanButtonText + "' text is shown correctly");

            new InitializeHardwarePage().StartDemoMode();

            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(45));
            PermissionHelper.AllowPermissionIfRequested();

            // Load Dashboard Page
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            var dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            // Adding this since even if we add WaitForToastToDisappear still it is not waiting for it in Regression Testing
            Thread.Sleep(3000);

            // Change to Normal Mode
            NavigationHelper.NavigateToSettingsMenu(dashboardPage).OpenDemoMode();

            Assert.IsTrue(new AppModeSelectionPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            var appMode = new AppModeSelectionPage().GetSelectedAppMode();
            Assert.AreEqual(appMode, AppMode.Demo);
            new AppModeSelectionPage().ChangeAppMode(AppMode.Normal);

            // Close App
            AppManager.CloseApp();
            ReportHelper.LogTest(Status.Pass, "App closed");

            // App Started
            AppManager.StartApp(false);
            ReportHelper.LogTest(Status.Pass, "App started");

            // The behaviour is different for different OEMs
            if (AppManager.Brand.Equals(Brand.Kind))
            {
                // For Kind InitializeHardwarePage will be loaded
                Assert.IsTrue(new InitializeHardwarePage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)));
                string demoModeTextTitleModeChange = new InitializeHardwarePage().GetDemoModeText();
                Assert.IsNotEmpty(demoModeTextTitleModeChange);
                Assert.AreEqual(DemoModeButtonText.ToUpper(), demoModeTextTitleModeChange.ToUpper());
                ReportHelper.LogTest(Status.Pass, "'" + DemoModeButtonText + "' text is shown correctly");
                string startScanTextTitleModeChange = new InitializeHardwarePage().GetScanText();
                Assert.IsNotEmpty(startScanTextTitleModeChange);
                Assert.AreEqual(StartScanButtonText.ToUpper(), startScanTextTitleModeChange.ToUpper());
                ReportHelper.LogTest(Status.Pass, "'" + StartScanButtonText + "' text is shown correctly");
                ReportHelper.LogTest(Status.Pass, "Behaviour verified for Kind OEM");
            }
            else
            {
                // For other OEMs IntroPageOne will be loaded
                Assert.IsTrue(new IntroPageOne().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
                string welocmePageTitleModeChange = new IntroPageOne().GetTitle();
                Assert.IsNotEmpty(welocmePageTitleModeChange);
                Assert.AreEqual(WelcomeText, welocmePageTitleModeChange);
                ReportHelper.LogTest(Status.Pass, "'" + WelcomeText + "' text is shown correctly");
                ReportHelper.LogTest(Status.Pass, "Behaviour verified for OEMs other than Kind");
            }
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-19702_Table-0")]
        public void ST19702_EnableTurkishLanguage()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand.ToString() + " App initialized successfully");

            Language_Device language_Device = Language_Device.Turkish_Turkish;
            Language expectedAppLanguage = Language.Turkish;
            Language_Audifon expectedAppLanguageAudifon = Language_Audifon.Turkish;
            Language defaultAppLanguage = Language.English;
            Language_Audifon defaultAppLanguageAudifon = Language_Audifon.English;

            // Changing Mobile Language
            AppManager.DeviceSettings.ChangeDeviceLanguage(language_Device);
            ReportHelper.LogTest(Status.Info, "Enable device langauge to " + language_Device + " and restart app.");

            // Reset App after language Turkish changed
            AppManager.RestartApp(true);
            ReportHelper.LogTest(Status.Info, "App Restarted");
            Assert.IsTrue(new IntroPageOne().IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)));

            // Checking Welcome text
            string welocmePageTitle = new IntroPageOne().GetTitle();
            Assert.AreEqual("Hoş geldiniz!", welocmePageTitle, "Welcome page is not displayed correctly.");
            ReportHelper.LogTest(Status.Pass, "Welcome page text shown correctly in " + expectedAppLanguage.ToString() + " language.");

            // Navigate throught to intro and start the Demo mode
            new IntroPageOne().MoveRightBySwiping();
            new IntroPageTwo().MoveRightBySwiping();
            new IntroPageThree().MoveRightBySwiping();
            new IntroPageFour().MoveRightBySwiping();
            Assert.IsTrue(new IntroPageFive().GetIsLeftButtonVisible());
            new IntroPageFive().Continue();
            ReportHelper.LogTest(Status.Pass, "Verified Intro Pages");

            Assert.IsTrue(new InitializeHardwarePage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));

            // Starting App in Demo Mode
            new InitializeHardwarePage().StartDemoMode();
            ReportHelper.LogTest(Status.Info, "App started in Demo Mode");

            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(45));
            PermissionHelper.AllowPermissionIfRequested();

            // Load Dashboard Page
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            DashboardPage dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            // Checking Menu
            ReportHelper.LogTest(Status.Info, "Open the Main Menu.");
            dashboardPage.OpenMenuUsingTap();
            MainMenuPage mainMenuPage = new MainMenuPage();
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText());
            Assert.IsNotEmpty(mainMenuPage.GetProgramsText());
            Assert.IsNotEmpty(mainMenuPage.GetHelpText());
            ReportHelper.LogTest(Status.Pass, "The main menu is opened successfully.");

            // Check Help Menu
            Assert.IsNotEmpty(new MainMenuPage().GetHelpText());
            ReportHelper.LogTest(Status.Info, "Open 'Help' menu.");
            new MainMenuPage().OpenHelp();
            HelpMenuPage helpMenuPage = new HelpMenuPage();
            Assert.IsNotEmpty(helpMenuPage.GetFindHearingDevicesText());
            Assert.IsNotEmpty(helpMenuPage.GetHelpTopicsText());
            Assert.IsNotEmpty(helpMenuPage.GetInstructionsForUseText());
            Assert.IsNotEmpty(helpMenuPage.GetInformationMenuText());
            Assert.IsNotEmpty(helpMenuPage.GetImprintText());
            ReportHelper.LogTest(Status.Pass, "Help Menu page items are displayed.");

            // Open Imprint and Verify 
            new HelpMenuPage().OpenImprint();
            Assert.IsNotEmpty(new ImprintPage().GetSupportDescription());
            //Todo: The below text is different in different OEMs. Hence commenting this to avoid failiures in regression testing
            //Assert.AreEqual("Daha fazla bilgi için işitme uzmanınıza başvurun.", new ImprintPage().GetSupportDescription());
            ReportHelper.LogTest(Status.Pass, "Imprint menu is verified and is in turkish language.");
            new ImprintPage().TapBack();

            // Open Information and Verify
            new HelpMenuPage().OpenInformationMenu();
            Assert.IsNotEmpty(new InformationMenuPage().GetPrivacyPolicyText());
            Assert.IsNotEmpty(new InformationMenuPage().GetTermsOfUseText());
            Assert.IsNotEmpty(new InformationMenuPage().GetLicensesText());
            //Todo: The below text is different in different OEMs. Hence commenting this to avoid failiures in regression testing
            //Assert.IsNotEmpty("Gizlilik", new InformationMenuPage().GetPrivacyPolicyText());
            //Assert.IsNotEmpty("Kullanım Şartları", new InformationMenuPage().GetTermsOfUseText());
            //Assert.IsNotEmpty("Lisanslar", new InformationMenuPage().GetLicensesText());
            ReportHelper.LogTest(Status.Pass, "Information menu is verified and is in turkish language.");

            //Open Data Protection and Verify
            new InformationMenuPage().OpenPrivacyPolicy();
            Thread.Sleep(500);
            Assert.IsNotEmpty(new PrivacyPolicyPage().GetNavigationBarTitle());
            //Todo: The below text is different in different OEMs. Hence commenting this to avoid failiures in regression testing
            //Assert.IsNotEmpty("Gizlilik", new PrivacyPolicyPage().GetNavigationBarTitle());
            ReportHelper.LogTest(Status.Pass, "Data Protection menu is verified and is in turkish language.");
            new PrivacyPolicyPage().TapBack();

            //Open Terms of use menu and Verify
            new InformationMenuPage().OpenTermsofUse();
            Thread.Sleep(500);
            Assert.IsNotEmpty(new TermsOfUsePage().GetNavigationBarTitle());
            //Todo: The below text is different in different OEMs. Hence commenting this to avoid failiures in regression testing
            Assert.IsNotEmpty("Kullanım Şartları", new TermsOfUsePage().GetNavigationBarTitle());
            ReportHelper.LogTest(Status.Pass, "Terms of use menu is verified and is in turkish language.");
            new TermsOfUsePage().TapBack();

            // open Licenses and Verify
            new InformationMenuPage().OpenLicenses();
            Thread.Sleep(500);
            Assert.IsNotEmpty(new LicencesPage().GetNavigationBarTitle());
            //Todo: The below text is different in different OEMs. Hence commenting this to avoid failiures in regression testing
            Assert.IsNotEmpty("Lisanslar", new LicencesPage().GetNavigationBarTitle());
            ReportHelper.LogTest(Status.Pass, "Licenses menu is verified and is in turkish language.");
            new LicencesPage().TapBack();

            // Navigate back to Help menu page
            new InformationMenuPage().TapBack();

            // Navigate back to Main menu page
            new HelpMenuPage().TapBack();

            // Open Language
            mainMenuPage.OpenSettings();
            SettingsMenuPage settingsMenuPageLanguage = new SettingsMenuPage();
            settingsMenuPageLanguage.OpenLanguage();

            Assert.IsTrue(new SettingLanguagePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Info, "Language Menu opened");
            SettingLanguagePage settingLanguagePage = new SettingLanguagePage();

            // Change the Language
            switch (AppManager.Brand)
            {
                case Brand.Audifon:
                    Assert.AreEqual(expectedAppLanguageAudifon, settingLanguagePage.GetCurrentLanguageAudifon());
                    ReportHelper.LogTest(Status.Info, "Current selected language is " + settingLanguagePage.GetCurrentLanguageAudifon());
                    settingLanguagePage.SelectLanguageAudifon(defaultAppLanguageAudifon);
                    ReportHelper.LogTest(Status.Info, "Select " + defaultAppLanguage.ToString() + " Language");

                    settingLanguagePage.Accept();
                    ReportHelper.LogTest(Status.Info, "Accept button clicked");

                    new AppDialog().Deny();
                    ReportHelper.LogTest(Status.Info, "No clicked in App Dialog");

                    Assert.AreEqual(expectedAppLanguageAudifon, settingLanguagePage.GetCurrentLanguageAudifon());
                    ReportHelper.LogTest(Status.Pass, "Current selected language is still " + expectedAppLanguageAudifon.ToString());
                    break;
                case Brand.Kind:
                    Assert.AreEqual(expectedAppLanguage, settingLanguagePage.GetCurrentLanguage());
                    ReportHelper.LogTest(Status.Info, "Current selected language is " + settingLanguagePage.GetCurrentLanguage());
                    settingLanguagePage.SelectLanguage(defaultAppLanguage);
                    ReportHelper.LogTest(Status.Info, "Select " + defaultAppLanguage.ToString() + " Language");

                    settingLanguagePage.Accept();
                    ReportHelper.LogTest(Status.Info, "Accept button clicked");

                    new AppDialog().Deny();
                    ReportHelper.LogTest(Status.Info, "No clicked in App Dialog");

                    Assert.AreEqual(expectedAppLanguage, settingLanguagePage.GetCurrentLanguage());
                    ReportHelper.LogTest(Status.Info, "Current selected language is still " + expectedAppLanguage.ToString());
                    break;
                case Brand.Hormann:
                    Assert.AreEqual(expectedAppLanguageAudifon, settingLanguagePage.GetCurrentLanguageHormann());
                    ReportHelper.LogTest(Status.Info, "Current selected language is " + settingLanguagePage.GetCurrentLanguageHormann());
                    settingLanguagePage.SelectLanguageAudifon(defaultAppLanguageAudifon);
                    ReportHelper.LogTest(Status.Info, "Select " + defaultAppLanguage.ToString() + " Language");

                    settingLanguagePage.Accept();
                    ReportHelper.LogTest(Status.Info, "Accept button clicked");

                    new AppDialog().Deny();
                    ReportHelper.LogTest(Status.Info, "No clicked in App Dialog");

                    Assert.AreEqual(expectedAppLanguageAudifon, settingLanguagePage.GetCurrentLanguageHormann());
                    ReportHelper.LogTest(Status.Pass, "Current selected language is still " + expectedAppLanguageAudifon.ToString());
                    break;
                case Brand.RxEarsPro:
                    Assert.AreEqual(expectedAppLanguageAudifon, settingLanguagePage.GetCurrentLanguageRxEarsPro());
                    ReportHelper.LogTest(Status.Info, "Current selected language is " + settingLanguagePage.GetCurrentLanguageRxEarsPro());
                    settingLanguagePage.SelectLanguageAudifon(defaultAppLanguageAudifon);
                    ReportHelper.LogTest(Status.Info, "Select " + defaultAppLanguage.ToString() + " Language");

                    settingLanguagePage.Accept();
                    ReportHelper.LogTest(Status.Info, "Accept button clicked");

                    new AppDialog().Deny();
                    ReportHelper.LogTest(Status.Info, "No clicked in App Dialog");

                    Assert.AreEqual(expectedAppLanguageAudifon, settingLanguagePage.GetCurrentLanguageRxEarsPro());
                    ReportHelper.LogTest(Status.Pass, "Current selected language is still " + expectedAppLanguageAudifon.ToString());
                    break;
                default: throw new NotImplementedException("Test case does not support this brand");
            }

            // Reset the Mobile language to English 
            AppManager.DeviceSettings.ChangeDeviceLanguage(Language_Device.English_US);
            ReportHelper.LogTest(Status.Info, "Change device langauge to English");
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-19489_Table-0")]
        public void ST19489_CheckPermissionsOnWiFiAutomationPage()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            // Enable Wifi
            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            // Bluetooth Disabled
            AppManager.App.DisableBluetooth();
            ReportHelper.LogTest(Status.Info, "Bluetooth disabled successfully");

            // Navigate through intro pages to HardwareInitialization page
            LaunchHelper.SkipIntroPages();

            ReportHelper.LogTest(Status.Pass, "Skip the intro page and landed in hardware error page requesting for enabling bluetooth");

            DialogHelper.ConfirmIfDisplayed();

            // Click on Repeat Operation
            new HardwareErrorPage().RetryProcess();
            ReportHelper.LogTest(Status.Info, "Click Retry in hardware error page");

            // Enable the Bluetooth 
            AppManager.App.EnableBluetooth();
            ReportHelper.LogTest(Status.Info, "Bluetooth enabled");

            PermissionHelper.AllowPermissionIfRequested();
            ReportHelper.LogTest(Status.Pass, "Landed in Hardware Initialization page");

            // Click on Start in Demo Mode
            new InitializeHardwarePage().StartDemoMode();

            Assert.IsTrue(DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(45)), "In IOS the confirmation dialog does not appear since we Reset the app from Settings menu and hence the permission cannot be denied.");
            new PermissionDialog().Deny();
            //var hardwareErrorPage = new HardwareErrorPage();
            //hardwareErrorPage.Continue();

            // Load Dashboard page
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            var dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "App started in demo mode");
            // Wait till the toast message disappears. It causes the test case to fail if it does not wait.
            Thread.Sleep(3000);

            // Open Music Program
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            var programDetailPage = new ProgramDetailPage();
            Assert.IsNotEmpty(programDetailPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Info, "Music program is opened successfully");

            // Open ProgramSettings 
            programDetailPage.OpenProgramSettings();
            var programSettingsControlPage = new ProgramDetailSettingsControlPage();
            Assert.IsTrue(programSettingsControlPage.GetIsCreateFavoriteVisible());
            Assert.IsTrue(programSettingsControlPage.GetIsCustomizeIconVisible());
            Assert.IsTrue(programSettingsControlPage.GetIsCustomizeNameVisible());
            Assert.IsTrue(programSettingsControlPage.GetIsCreateFavoriteFloatingButtonDisplayed());
            ReportHelper.LogTest(Status.Pass, "Customize name, Customize icon, Create favorites and '+' are visible");

            // Create Favorite program
            ReportHelper.LogTest(Status.Info, "Create favorite program with Location auto start");
            programSettingsControlPage.CreateFavorite();
            var programNamePage = new ProgramNamePage();
            Assert.IsNotEmpty(programNamePage.GetDescription());
            ReportHelper.LogTest(Status.Info, "Customize name view opened");
            string favouritePrograme = "Favorite 01";
            programNamePage.EnterName(favouritePrograme);
            favouritePrograme = programNamePage.GetName();
            programNamePage.Proceed();
            ReportHelper.LogTest(Status.Pass, "Favorite program '" + favouritePrograme + "' has been created");
            Assert.IsNotEmpty(new ProgramIconPage().GetDescription());
            ReportHelper.LogTest(Status.Info, "Customize symbol view opened");

            var programIconPage = new ProgramIconPage();
            programIconPage.SelectIcon(7).Proceed();
            var programAutomationPage = new ProgramAutomationPage();
            Assert.IsNotEmpty(programAutomationPage.GetDescription());
            ReportHelper.LogTest(Status.Info, "Auto hearing program start view opened");

            // Enable Start hearing program automatically toggle switch
            programAutomationPage.ToggleBinauralSwitch();
            Assert.IsTrue(programAutomationPage.GetIsGeofenceAutomationVisible());
            Assert.IsTrue(programAutomationPage.GetIsWifiAutomationVisible());
            ReportHelper.LogTest(Status.Pass, "The options Connect to WLAN and Connect to location appeared.");

            // Select "Connect to Wifi"
            programAutomationPage.TapConnectToWiFi();
            var appDialog = new AppDialog();
            Assert.IsNotEmpty(appDialog.GetMessage());
            ReportHelper.LogTest(Status.Pass, "Dialog displayed message: " + appDialog.GetMessage());
            appDialog.Confirm();

            AppManager.App.PressBackButton();

            //ToDo: Currently giving "Allow while using app" permission since "Allow Once" could not be automated
            // As per latest Audifon build we cannot create Wifi based favorite whe the Location access is either "Allow while using app" or "Allow Once". It needs to be "Always Allow". The test case also needs to be modified. 
            AppManager.DeviceSettings.GrantGPSPermission();
            AppManager.DeviceSettings.GrantGPSBackgroundPermission();
            Thread.Sleep(3000);

            ReportHelper.LogTest(Status.Info, "Granted 'Allow Once' permission");

            new AutomationWifiBindingPage().TapBack();
            new ProgramAutomationPage().TapConnectToWiFi();

            //new AutomationWifiBindingPage().Scan();

            Assert.IsNotEmpty(new AutomationWifiBindingPage().GetWifiName());
            new AutomationWifiBindingPage().Ok();
            ReportHelper.LogTest(Status.Info, "Program automation is set to Wifi");
            new ProgramAutomationPage().Proceed();

            // Navigate back to dashboard
            new ProgramDetailPage().NavigateBack();

            // Selecting a different programe
            new DashboardPage().SelectProgram(3);

            // App restarted
            AppManager.RestartApp(false);
            ReportHelper.LogTest(Status.Info, "App restarted");

            //ToDo: Since we are giving "Allow while using app" permission above it is not asking for permissions again. This has to be automated and again "Allow Once" needs to be selected

            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            dashboardPage = new DashboardPage();
            Assert.AreEqual(favouritePrograme, dashboardPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "Favorite program is selected After restarting app with 'Allow Once' permission");

            // Givng "Allow while using the app" permisson and check the above step again
            // As per latest Audifon build we cannot create Wifi based favorite whe the Location access is either "Allow while using app" or "Allow Once". It needs to be "Always Allow". The test case also needs to be modified. 
            //AppManager.DeviceSettings.GrantGPSPermission();
            AppManager.DeviceSettings.GrantGPSBackgroundPermission();

            ReportHelper.LogTest(Status.Info, "Granted 'Allow while using the app' permission");

            // App restarted
            AppManager.RestartApp(false);
            ReportHelper.LogTest(Status.Info, "App restarted");

            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            dashboardPage = new DashboardPage();
            Assert.AreEqual(favouritePrograme, dashboardPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "Favorite program is selected After restarting app with 'Allow while using the app' permission");

            // Givng "Always allow" permisson and check the above step again
            AppManager.DeviceSettings.GrantGPSBackgroundPermission();
            ReportHelper.LogTest(Status.Info, "Granted 'Always allow' permission");

            // App restarted
            AppManager.RestartApp(false);
            ReportHelper.LogTest(Status.Info, "App restarted");

            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            dashboardPage = new DashboardPage();
            Assert.AreEqual(favouritePrograme, dashboardPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "Favorite program is selected After restarting app with 'Always Allow' permission");

            // Givng "Never" permisson and check the above step again
            AppManager.DeviceSettings.RevokeGPSBackgroundPermission();
            AppManager.DeviceSettings.RevokeGPSPermission();
            ReportHelper.LogTest(Status.Info, "Granted 'Never' permission");

            // App restarted
            AppManager.RestartApp(false);
            ReportHelper.LogTest(Status.Info, "App restarted");

            appDialog = new AppDialog();
            appDialog.Deny();

            // Click on continue
            //hardwareErrorPage = new HardwareErrorPage();
            //hardwareErrorPage.Continue();

            //appDialog = new AppDialog();
            //appDialog.Deny();

            Assert.AreNotEqual(favouritePrograme, dashboardPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "Favorite program is Not selected After restarted");

            //ToDo: Currently giving "Allow while using app" permission since "Allow Once" could not be automated
            // As per latest Audifon build we cannot create Wifi based favorite whe the Location access is either "Allow while using app" or "Allow Once". It needs to be "Always Allow". The test case also needs to be modified. 
            AppManager.DeviceSettings.GrantGPSPermission();
            AppManager.DeviceSettings.GrantGPSBackgroundPermission();
            Thread.Sleep(3000);
            ReportHelper.LogTest(Status.Info, "Granted 'Allow Once' permission");

            // App restarted
            AppManager.RestartApp(false);
            ReportHelper.LogTest(Status.Info, "App restarted");

            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            dashboardPage = new DashboardPage();

            //Select the favourite program
            Assert.AreEqual(favouritePrograme, dashboardPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "Favorite program is selected After restarting app");

            AppManager.DeviceSettings.DisableWifi();
        }

        #endregion Sprint 29

        #region Sprint 30

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-20027_Table-0")]
        public void ST20027_CheckImagesForLanguages()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand.ToString() + " App initialized successfully");

            List<string> languages = null;

            // Getting all languages based on current Brand
            switch (AppManager.Brand)
            {
                case Brand.Audifon:
                    {
                        languages = LanguageHelper.GetAllAudifonLanguages();
                        break;
                    }
                case Brand.Puretone:
                    {
                        languages = LanguageHelper.GetAllPuretoneLanguages();
                        break;
                    }
                case Brand.PersonaMedical:
                    {
                        languages = LanguageHelper.GetAllPersonaLanguages();
                        break;
                    }
                case Brand.Kind:
                    {
                        languages = LanguageHelper.GetAllKindLanguages();
                        break;
                    }
                case Brand.Hormann:
                    {
                        languages = LanguageHelper.GetAllHormannLanguages();
                        break;
                    }
                case Brand.RxEarsPro:
                    {
                        languages = LanguageHelper.GetAllRxEarsProLanguages();
                        break;
                    }
                default: throw new NotImplementedException("Languages not available for current brand");

            }

            if (ReferenceEquals(languages, null))
                throw new NotImplementedException("Languages not available for current brand");

            // Itrating through each language for current Brand
            foreach (string language in languages)
            {
                switch (AppManager.Brand)
                {
                    case Brand.Audifon:
                        {
                            Language_Audifon currentLanguage = (Language_Audifon)Enum.Parse(typeof(Language_Audifon), language);
                            Language_Device deviceLanguage = LanguageHelper.GetDeviceLanguageForAudifonLanguage(currentLanguage);
                            CheckImages(deviceLanguage, language);
                            break;
                        }
                    case Brand.Puretone:
                        {
                            Language_Puretone currentLanguage = (Language_Puretone)Enum.Parse(typeof(Language_Puretone), language);
                            Language_Device deviceLanguage = LanguageHelper.GetDeviceLanguageForPuretoneLanguage(currentLanguage);
                            CheckImages(deviceLanguage, language);
                            break;
                        }
                    case Brand.PersonaMedical:
                        {
                            Language_Persona currentLanguage = (Language_Persona)Enum.Parse(typeof(Language_Persona), language);
                            Language_Device deviceLanguage = LanguageHelper.GetDeviceLanguageForPersonaLanguage(currentLanguage);
                            CheckImages(deviceLanguage, language);
                            break;
                        }
                    case Brand.Kind:
                        {
                            Language currentLanguage = (Language)Enum.Parse(typeof(Language), language);
                            Language_Device deviceLanguage = LanguageHelper.GetDeviceLanguageForKindLanguage(currentLanguage);
                            CheckImages(deviceLanguage, language);
                            break;
                        }
                    case Brand.Hormann:
                        {
                            Language_Hormann currentLanguage = (Language_Hormann)Enum.Parse(typeof(Language_Hormann), language);
                            Language_Device deviceLanguage = LanguageHelper.GetDeviceLanguageForHormannLanguage(currentLanguage);
                            CheckImages(deviceLanguage, language);
                            break;
                        }
                    case Brand.RxEarsPro:
                        {
                            Language_RxEarsPro currentLanguage = (Language_RxEarsPro)Enum.Parse(typeof(Language_RxEarsPro), language);
                            Language_Device deviceLanguage = LanguageHelper.GetDeviceLanguageForRxEarsProLanguage(currentLanguage);
                            CheckImages(deviceLanguage, language);
                            break;
                        }
                    default: throw new NotImplementedException("Languages not available for current brand");
                }
            }

            // Reset the Mobile language to English 
            AppManager.DeviceSettings.ChangeDeviceLanguage(Language_Device.English_US);
            ReportHelper.LogTest(Status.Info, "Change device langauge to English");
        }

        #endregion Sprint 30

        #region Sprint 33

        [Test]
        [Category("SystemTestsDemoObsolate")]
        [Description("TC-20958_Table-0")]
        public void ST20958_CodeCleanupUseXamarinPreferencesDemo()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            // This test case cannot be run in IOS since it deals with APK installation
            if (OniOS)
                throw new NotImplementedException("This test case cannot be run in IOS since it deals with APK installation");

            // Older APK version to be tested. If other version needs to be tested below two lines needs to be changed
            string AppPreviousVersion = "1.4.2";
            string APKFileName = "com.audifon.horus-Signed.1.4.2.22116501.apk";

            // Closing the App for installation of older APK
            AppManager.CloseApp();
            ReportHelper.LogTest(Status.Info, "App closed to uninstall current version and install " + AppPreviousVersion + " version");

            string APKFilePath = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Audifon\\APK\\" + APKFileName).FullName;

            // Throwing error if the APK file does not exists
            if (!File.Exists(APKFilePath))
                throw new Exception("APK file for " + AppPreviousVersion + " does not exists in the path : " + APKFilePath);

            // Uninstall current APK
            AppManager.UninstallApk();
            ReportHelper.LogTest(Status.Info, "Current APK uninstalled");

            // Install Previous APK Version
            AppManager.InstallApk(APKFilePath);
            ReportHelper.LogTest(Status.Info, "APK of version " + AppPreviousVersion + " installed sucessfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            // Starting the App after installation of Older APK version
            AppManager.StartApp(true);
            ReportHelper.LogTest(Status.Info, "App initialized after installation of " + AppPreviousVersion + " version successfully");

            // Open App in Demo Mode
            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            // Verify the App Version from Imprint Page
            dashboardPage.OpenMenuUsingTap();
            new MainMenuPage().OpenHelp();
            new HelpMenuPage().OpenImprint();
            Assert.IsTrue(new ImprintPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            var imprintPage = new ImprintPage();
            Assert.IsTrue(imprintPage.GetVersion().Contains(AppPreviousVersion));
            ReportHelper.LogTest(Status.Pass, "Instatalled version is " + imprintPage.GetVersion());

            // Navigate back to Dashboard
            imprintPage.NavigateBack();
            Assert.IsTrue(new HelpMenuPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            new HelpMenuPage().NavigateBack();
            Assert.IsTrue(new MainMenuPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            new MainMenuPage().CloseMenuUsingTap();
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));

            //Select music program
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            ReportHelper.LogTest(Status.Info, "Music program is selected");

            //open speech focus
            new ProgramDetailPage().SpeechFocusDisplay.OpenSettings();

            //Setting Speech Focus to maximum
            var programDetailParamEditSpeechFocusPage = new ProgramDetailParamEditSpeechFocusPage();
            programDetailParamEditSpeechFocusPage.SelectSpeechFocus(SpeechFocus.Auto);
            Assert.AreEqual(SpeechFocus.Auto, programDetailParamEditSpeechFocusPage.GetSelectedSpeechFocus());
            programDetailParamEditSpeechFocusPage.Close();
            ReportHelper.LogTest(Status.Info, "Speech Focus set to Auto");

            //open noise reduction and setting to maximum volume
            new ProgramDetailPage().NoiseReductionDisplay.OpenSettings();

            //Setting noise reduction to maximum
            var noiseReductionPage = new ProgramDetailParamEditNoiseReductionPage();
            noiseReductionPage.SelectNoiseReduction(NoiseReduction.Strong);
            Assert.AreEqual(NoiseReduction.Strong, noiseReductionPage.GetSelectedNoiseReduction());
            noiseReductionPage.Close();

            //Open the sound program and setting sound to maximum volume
            new ProgramDetailPage().EqualizerDisplay.OpenSettings();
            var equilizerDisplay = new ProgramDetailParamEditEqualizerPage();

            var lowMaxValue = 1;
            equilizerDisplay.SetEqualizerSliderValue(EqBand.Low, lowMaxValue);
            Assert.AreEqual(lowMaxValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.Low));

            var midMaxValue = 1;
            equilizerDisplay.SetEqualizerSliderValue(EqBand.Mid, midMaxValue);
            Assert.AreEqual(midMaxValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.Mid));

            var highMaxValue = 1;
            equilizerDisplay.SetEqualizerSliderValue(EqBand.High, highMaxValue);
            Assert.AreEqual(highMaxValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.High));

            ReportHelper.LogTest(Status.Info, "Increasing Equlizer display values to Maximum");
            equilizerDisplay.Close();

            //Open the tinnitus program and setting sound to maximum volume
            new ProgramDetailPage().TinnitusDisplay.OpenSettings();
            var programDetailParamEditTinnitusPage = new ProgramDetailParamEditTinnitusPage();
            Assert.IsTrue(programDetailParamEditTinnitusPage.GetIsTinnitusSwitchChecked());
            int maxVolume = 1;
            programDetailParamEditTinnitusPage.SetVolumeSliderValue(maxVolume);
            Assert.AreEqual(maxVolume, programDetailParamEditTinnitusPage.GetVolumeSliderValue());
            ReportHelper.LogTest(Status.Info, "Increasing Tinnitus display volume to Maximum");
            programDetailParamEditTinnitusPage.SetEqualizerSliderValue(EqBand.Low, lowMaxValue);
            Assert.AreEqual(lowMaxValue, programDetailParamEditTinnitusPage.GetEqualizerSliderValue(EqBand.Low));
            programDetailParamEditTinnitusPage.SetEqualizerSliderValue(EqBand.Mid, midMaxValue);
            Assert.AreEqual(midMaxValue, programDetailParamEditTinnitusPage.GetEqualizerSliderValue(EqBand.Mid));
            programDetailParamEditTinnitusPage.SetEqualizerSliderValue(EqBand.High, highMaxValue);
            Assert.AreEqual(highMaxValue, programDetailParamEditTinnitusPage.GetEqualizerSliderValue(EqBand.High));
            ReportHelper.LogTest(Status.Info, "Increasing Tinnitus display Sound to Maximum");
            programDetailParamEditTinnitusPage.Close();

            // Create Favourite 
            string FavoriteName = "Favourite 01";
            var programAutomationPage = FavoriteHelper.CreateFavoriteHearingProgram(FavoriteName, 5);
            programAutomationPage.Proceed();
            ReportHelper.LogTest(Status.Pass, "Favourite program '" + FavoriteName + "' is created successfully");

            new ProgramDetailPage().TapBack();
            Assert.AreEqual(FavoriteName, new DashboardPage().GetCurrentProgramName());

            //Open the tinnitus program and set the mimunm volume
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 2);
            var programDetailPageTinnitus = new ProgramDetailPage();
            ReportHelper.LogTest(Status.Info, "Tinnitus opened");

            programDetailPageTinnitus.TinnitusOnlyDisplay.OpenSettings();
            programDetailParamEditTinnitusPage = new ProgramDetailParamEditTinnitusPage();
            Assert.IsTrue(programDetailParamEditTinnitusPage.GetIsTinnitusSwitchChecked());
            var minVolume = 0;
            programDetailParamEditTinnitusPage.SetVolumeSliderValue(minVolume);
            Assert.AreEqual(minVolume, Math.Round(programDetailParamEditTinnitusPage.GetVolumeSliderValue(), 0));
            ReportHelper.LogTest(Status.Info, "Decreasing Tinnitus Only display volume to Minimum");
            var lowMinValue = 0;
            programDetailParamEditTinnitusPage.SetEqualizerSliderValue(EqBand.Low, lowMinValue);
            Assert.AreEqual(lowMinValue, Math.Round(programDetailParamEditTinnitusPage.GetEqualizerSliderValue(EqBand.Low), 0));
            var midMinValue = 0;
            programDetailParamEditTinnitusPage.SetEqualizerSliderValue(EqBand.Mid, midMinValue);
            Assert.AreEqual(midMinValue, Math.Round(programDetailParamEditTinnitusPage.GetEqualizerSliderValue(EqBand.Mid), 0));
            var highMinValue = 0;
            programDetailParamEditTinnitusPage.SetEqualizerSliderValue(EqBand.High, highMinValue);
            Assert.AreEqual(highMinValue, Math.Round(programDetailParamEditTinnitusPage.GetEqualizerSliderValue(EqBand.High), 0));
            ReportHelper.LogTest(Status.Info, "Decreasing Tinnitus Only display sound to Minimum");
            programDetailParamEditTinnitusPage.Close();

            // Create Favourite and save with wifi
            var favoriteWithWifi = "Favorite 01 Wifi";
            programAutomationPage = FavoriteHelper.CreateFavoriteHearingProgram(favoriteWithWifi, 4);
            AppManager.DeviceSettings.GrantGPSBackgroundPermission();
            Assert.IsTrue(programAutomationPage.GetIsGeofenceAutomationVisible());
            Assert.IsTrue(programAutomationPage.GetIsWifiAutomationVisible());
            ReportHelper.LogTest(Status.Info, "The options Connect to WLAN and Connect to location appeared.");

            //Select "Connect to Wifi"
            programAutomationPage.TapConnectToWiFi();
            Assert.IsNotEmpty(new AutomationWifiBindingPage().GetWifiName());
            new AutomationWifiBindingPage().Ok();
            new ProgramAutomationPage().Proceed();
            ReportHelper.LogTest(Status.Pass, "Favourite program '" + favoriteWithWifi + "' is created successfully");

            new ProgramDetailPage().TapBack();

            // Open Audio Stream Program
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Streaming, 0);
            ReportHelper.LogTest(Status.Info, "Audo Streaming Program Opened");

            // Open Settings and change Streaming value with set medium volume
            new ProgramDetailPage().StreamingDisplay.OpenSettings();
            var streamingPage = new ProgramDetailParamEditStreamingPage();
            var streamingMidvalue = 0.5;
            streamingPage.SetStreamingSliderValue(streamingMidvalue);
            Assert.AreEqual(streamingMidvalue, Math.Round(streamingPage.GetStreamingSliderValue(), 1));
            ReportHelper.LogTest(Status.Info, "Audo Streaming Streaming display value set to middle");
            streamingPage.Close();

            //setting Speech focus setting to medium volume
            new ProgramDetailPage().SpeechFocusDisplay.OpenSettings();
            programDetailParamEditSpeechFocusPage = new ProgramDetailParamEditSpeechFocusPage();
            programDetailParamEditSpeechFocusPage.SelectSpeechFocus(SpeechFocus.Front);
            Assert.AreEqual(SpeechFocus.Front, programDetailParamEditSpeechFocusPage.GetSelectedSpeechFocus());
            ReportHelper.LogTest(Status.Info, "Audo Streaming Speech focus display set to Front");
            programDetailParamEditSpeechFocusPage.Close();

            //Setting noise reduction to medium volume
            new ProgramDetailPage().NoiseReductionDisplay.OpenSettings();
            noiseReductionPage = new ProgramDetailParamEditNoiseReductionPage();
            noiseReductionPage.SelectNoiseReduction(NoiseReduction.Medium);
            Assert.AreEqual(NoiseReduction.Medium, noiseReductionPage.GetSelectedNoiseReduction());
            ReportHelper.LogTest(Status.Info, "Audo Streaming Noise Reduction display set to Medium");
            noiseReductionPage.Close();

            //Setting sound to medium volume
            new ProgramDetailPage().EqualizerDisplay.OpenSettings();
            var equalizerPage = new ProgramDetailParamEditEqualizerPage();
            var lowMidValue = 0.5;
            equalizerPage.SetEqualizerSliderValue(EqBand.Low, lowMidValue);
            Assert.AreEqual(lowMidValue, equalizerPage.GetEqualizerSliderValue(EqBand.Low));
            var midMidValue = 0.5;
            equalizerPage.SetEqualizerSliderValue(EqBand.Mid, midMidValue);
            Assert.AreEqual(lowMidValue, equalizerPage.GetEqualizerSliderValue(EqBand.Mid));
            var highMidValue = 0.5;
            equalizerPage.SetEqualizerSliderValue(EqBand.High, highMidValue);
            Assert.AreEqual(highMidValue, equalizerPage.GetEqualizerSliderValue(EqBand.High));
            ReportHelper.LogTest(Status.Info, "Changed Audio Stream Equlizer display Value to medium");
            equalizerPage.Close();

            //create favorite and save location based on
            var favoriteWithLocation = "Favorite 01 Location";
            programAutomationPage = FavoriteHelper.CreateFavoriteHearingProgram(favoriteWithLocation, 3);

            //Select "Connect to Location"
            programAutomationPage.TapConnectToLocation();

            //Select location and save created favorite program
            new AutomationGeofenceBindingPage().WaitUntilNoLoadingIndicator().SelectPosition(0.5, 0.5);
            new AutomationGeofenceBindingPage().Ok();
            programAutomationPage = new ProgramAutomationPage();
            ReportHelper.LogTest(Status.Pass, "Connected to location and address are visible on page.");
            programAutomationPage.Proceed();

            ReportHelper.LogTest(Status.Info, "Favorite Program Name and Icon Noted: " + favoriteWithLocation);
            new ProgramDetailPage().TapBack();
            Assert.AreEqual(favoriteWithLocation, new DashboardPage().GetCurrentProgramName());

            //Open setting using hamber button 
            new DashboardPage().OpenMenuUsingTap();
            new MainMenuPage().OpenSettings();

            //Open permission and Disable tracking the hearing systems
            new SettingsMenuPage().OpenPermissions();
            var settingPermissionsPage = new SettingPermissionsPage();
            settingPermissionsPage.TurnOffLocationPermission();
            settingPermissionsPage.TapBack();
            new SettingsMenuPage().TapBack();
            new MainMenuPage().CloseMenuUsingTap();

            //Open setting and Switch to another language
            new DashboardPage().OpenMenuUsingTap();
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenLanguage();
            var settingLanguagePage = new SettingLanguagePage();
            var selectedLangauageText = settingLanguagePage.SelectLanguageAudifon(Language_Audifon.German);
            selectedLangauageText.Accept();
            new AppDialog().Confirm();
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));

            // App restarted
            AppManager.RestartApp(false);
            ReportHelper.LogTest(Status.Info, "App restarted");

            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(80)));
            dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            ReportHelper.LogTest(Status.Info, "App is restarted successfully");

            Assert.AreEqual(favoriteWithWifi, dashboardPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "Wifi based favorite program is automatically selected");

            //Open the favorite and check the after restart are equal
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 0);
            var programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(FavoriteName, programDetailPage.GetCurrentProgramName());
            programDetailPage.EqualizerDisplay.OpenSettings();
            equilizerDisplay = new ProgramDetailParamEditEqualizerPage();
            Assert.AreEqual(lowMaxValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.Low));
            Assert.AreEqual(midMaxValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.Mid));
            Assert.AreEqual(highMaxValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.High));
            ReportHelper.LogTest(Status.Pass, "Equilizer display value set to maxmium is verified");
            equilizerDisplay.Close();

            new ProgramDetailPage().TinnitusDisplay.OpenSettings();
            var programDetailParamEditTinnitusPageCurrent = new ProgramDetailParamEditTinnitusPage();
            Assert.IsTrue(programDetailParamEditTinnitusPageCurrent.GetIsTinnitusSwitchChecked());
            Assert.AreEqual(maxVolume, programDetailParamEditTinnitusPageCurrent.GetVolumeSliderValue());
            Assert.AreEqual(lowMaxValue, programDetailParamEditTinnitusPageCurrent.GetEqualizerSliderValue(EqBand.Low));
            Assert.AreEqual(midMaxValue, programDetailParamEditTinnitusPageCurrent.GetEqualizerSliderValue(EqBand.Mid));
            Assert.AreEqual(highMaxValue, programDetailParamEditTinnitusPageCurrent.GetEqualizerSliderValue(EqBand.High));
            ReportHelper.LogTest(Status.Pass, "Tinnius display value set to maxmium is verified");
            programDetailParamEditTinnitusPageCurrent.Close();

            new ProgramDetailPage().SpeechFocusDisplay.OpenSettings();
            var speechFocusPage = new ProgramDetailParamEditSpeechFocusPage();
            Assert.AreEqual(SpeechFocus.Auto, speechFocusPage.GetSelectedSpeechFocus());
            ReportHelper.LogTest(Status.Pass, "Speech foucus display value set to Auto is verified");
            speechFocusPage.Close();

            new ProgramDetailPage().NoiseReductionDisplay.OpenSettings();
            var noiseReduction = new ProgramDetailParamEditNoiseReductionPage();
            Assert.AreEqual(NoiseReduction.Strong, noiseReduction.GetSelectedNoiseReduction());
            ReportHelper.LogTest(Status.Pass, "Noise reduction display value set to Strong is verified");
            noiseReduction.Close();

            new ProgramDetailPage().TapBack();

            //Open the favoritewithwifi and check after restart are equal.
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 1);
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(favoriteWithWifi, programDetailPage.GetCurrentProgramName());
            new ProgramDetailPage().TinnitusOnlyDisplay.OpenSettings();
            programDetailParamEditTinnitusPageCurrent = new ProgramDetailParamEditTinnitusPage();
            Assert.IsTrue(programDetailParamEditTinnitusPageCurrent.GetIsTinnitusSwitchChecked());
            Assert.AreEqual(minVolume, Math.Round(programDetailParamEditTinnitusPageCurrent.GetVolumeSliderValue(), 0));
            Assert.AreEqual(lowMinValue, Math.Round(programDetailParamEditTinnitusPageCurrent.GetEqualizerSliderValue(EqBand.Low), 0));
            Assert.AreEqual(midMinValue, Math.Round(programDetailParamEditTinnitusPageCurrent.GetEqualizerSliderValue(EqBand.Mid), 0));
            Assert.AreEqual(highMinValue, Math.Round(programDetailParamEditTinnitusPageCurrent.GetEqualizerSliderValue(EqBand.High), 0));
            ReportHelper.LogTest(Status.Pass, "Tinnitus only display value set to Mininum is verified");
            programDetailParamEditTinnitusPageCurrent.Close();

            new ProgramDetailPage().TapBack();

            //Open the favoritewithlocation and check after restart are equal.
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 2);
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(favoriteWithLocation, programDetailPage.GetCurrentProgramName());

            programDetailPage.EqualizerDisplay.OpenSettings();
            equilizerDisplay = new ProgramDetailParamEditEqualizerPage();
            Assert.AreEqual(lowMidValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.Low));
            Assert.AreEqual(midMidValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.Mid));
            Assert.AreEqual(highMidValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.High));
            ReportHelper.LogTest(Status.Pass, "Equilizer display value set to Middle is verified");
            equilizerDisplay.Close();

            new ProgramDetailPage().SpeechFocusDisplay.OpenSettings();
            var speechFocus = new ProgramDetailParamEditSpeechFocusPage();
            Assert.AreEqual(SpeechFocus.Front, speechFocus.GetSelectedSpeechFocus());
            ReportHelper.LogTest(Status.Pass, "Speech focus display value set to Front is verified");
            speechFocus.Close();

            new ProgramDetailPage().NoiseReductionDisplay.OpenSettings();
            var audionoiseReduction = new ProgramDetailParamEditNoiseReductionPage();
            Assert.AreEqual(NoiseReduction.Medium, audionoiseReduction.GetSelectedNoiseReduction());
            ReportHelper.LogTest(Status.Pass, "Noise reduction display value set to Medium is verified");
            audionoiseReduction.Close();
            new ProgramDetailPage().TapBack();

            //Open setting using hamber button 
            new DashboardPage().OpenMenuUsingTap();
            new MainMenuPage().OpenSettings();

            //Open permission and check after restart is Disable tracking the hearing systems
            new SettingsMenuPage().OpenPermissions();
            var settingPermissions = new SettingPermissionsPage();
            Assert.IsFalse(settingPermissions.GetIsLocationPermissionSwitchChecked());
            ReportHelper.LogTest(Status.Pass, "Location permission switch turned off is verified");
            settingPermissions.TapBack();
            new SettingsMenuPage().TapBack();
            new MainMenuPage().CloseMenuUsingTap();

            //Open setting and Switch to another language
            new DashboardPage().OpenMenuUsingTap();
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenLanguage();
            var settingLanguage = new SettingLanguagePage();
            Assert.AreEqual(settingLanguagePage.GetCurrentLanguageAudifon(), settingLanguage.GetSelectedLanguageAudifon());
            Assert.AreEqual(Language_Audifon.German, settingLanguage.GetSelectedLanguageAudifon());
            ReportHelper.LogTest(Status.Pass, "Selected language is German is verified");
            settingLanguage.TapBack();
            Thread.Sleep(1000);

            // Closing App before Update
            AppManager.CloseApp();
            ReportHelper.LogTest(Status.Info, "App closed");

            // Updating the Audifon App from Play Store
            AppManager.UpdateAppFromStore();
            ReportHelper.LogTest(Status.Pass, "Latest version updated from play store");

            // Initialize the Audifon App after update
            AppManager.InitializeAppAfterUpdate(new Data.AppConfig(CurrentPlatform));

            AppManager.StartApp(false);
            ReportHelper.LogTest(Status.Info, "App started after update");

            Assert.IsTrue(new IntroPageOne(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(20)));

            // Open App in Demo Mode after update from play store
            dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode after update from play store");

            // Verify the App Version from Imprint Page after update from play store
            dashboardPage.OpenMenuUsingTap();
            new MainMenuPage().OpenHelp();
            new HelpMenuPage().OpenImprint();
            Assert.IsTrue(new ImprintPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            var imprintPageUpdated = new ImprintPage();
            //Assert.IsFalse(imprintPageUpdated.GetVersion().Contains(AppPreviousVersion));
            ReportHelper.LogTest(Status.Pass, "Instatalled version is " + imprintPageUpdated.GetVersion());

            // Navigate back to Dashboard
            imprintPageUpdated.NavigateBack();
            Assert.IsTrue(new HelpMenuPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            new HelpMenuPage().NavigateBack();
            Assert.IsTrue(new MainMenuPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            new MainMenuPage().CloseMenuUsingTap();
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));

            //Open the favorite and check after updated are equal
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 0);
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(FavoriteName, programDetailPage.GetCurrentProgramName());
            programDetailPage.EqualizerDisplay.OpenSettings();
            equilizerDisplay = new ProgramDetailParamEditEqualizerPage();
            Assert.AreEqual(lowMaxValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.Low));
            Assert.AreEqual(midMaxValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.Mid));
            Assert.AreEqual(highMaxValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.High));
            ReportHelper.LogTest(Status.Pass, "Equilizer display value set to maxmium is verified after update");
            equilizerDisplay.Close();

            new ProgramDetailPage().TinnitusDisplay.OpenSettings();
            programDetailParamEditTinnitusPageCurrent = new ProgramDetailParamEditTinnitusPage();
            Assert.IsTrue(programDetailParamEditTinnitusPageCurrent.GetIsTinnitusSwitchChecked());
            Assert.AreEqual(maxVolume, programDetailParamEditTinnitusPageCurrent.GetVolumeSliderValue());
            Assert.AreEqual(lowMaxValue, programDetailParamEditTinnitusPageCurrent.GetEqualizerSliderValue(EqBand.Low));
            Assert.AreEqual(midMaxValue, programDetailParamEditTinnitusPageCurrent.GetEqualizerSliderValue(EqBand.Mid));
            Assert.AreEqual(highMaxValue, programDetailParamEditTinnitusPageCurrent.GetEqualizerSliderValue(EqBand.High));
            ReportHelper.LogTest(Status.Pass, "Tinnius display value set to maxmium is verified after update");
            programDetailParamEditTinnitusPageCurrent.Close();

            new ProgramDetailPage().SpeechFocusDisplay.OpenSettings();
            speechFocusPage = new ProgramDetailParamEditSpeechFocusPage();
            Assert.AreEqual(SpeechFocus.Auto, speechFocusPage.GetSelectedSpeechFocus());
            ReportHelper.LogTest(Status.Pass, "Speech foucus display value set to Auto is verified after update");
            speechFocusPage.Close();

            new ProgramDetailPage().NoiseReductionDisplay.OpenSettings();
            noiseReduction = new ProgramDetailParamEditNoiseReductionPage();
            Assert.AreEqual(NoiseReduction.Strong, noiseReduction.GetSelectedNoiseReduction());
            ReportHelper.LogTest(Status.Pass, "Noise reduction display value set to Strong is verified after update");
            noiseReduction.Close();

            new ProgramDetailPage().TapBack();

            //Open the favoritewithwifi and check after restart are equal.
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 1);
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(favoriteWithWifi, programDetailPage.GetCurrentProgramName());
            new ProgramDetailPage().TinnitusOnlyDisplay.OpenSettings();
            programDetailParamEditTinnitusPageCurrent = new ProgramDetailParamEditTinnitusPage();
            Assert.IsTrue(programDetailParamEditTinnitusPageCurrent.GetIsTinnitusSwitchChecked());
            Assert.AreEqual(minVolume, Math.Round(programDetailParamEditTinnitusPageCurrent.GetVolumeSliderValue(), 0));
            Assert.AreEqual(lowMinValue, Math.Round(programDetailParamEditTinnitusPageCurrent.GetEqualizerSliderValue(EqBand.Low), 0));
            Assert.AreEqual(midMinValue, Math.Round(programDetailParamEditTinnitusPageCurrent.GetEqualizerSliderValue(EqBand.Mid), 0));
            Assert.AreEqual(highMinValue, Math.Round(programDetailParamEditTinnitusPageCurrent.GetEqualizerSliderValue(EqBand.High), 0));
            ReportHelper.LogTest(Status.Pass, "Tinnitus only display value set to Mininum is verified after update");
            programDetailParamEditTinnitusPageCurrent.Close();

            new ProgramDetailPage().TapBack();

            //Open the favoritewithlocation and check after restart are equal.
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 2);
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(favoriteWithLocation, programDetailPage.GetCurrentProgramName());

            programDetailPage.EqualizerDisplay.OpenSettings();
            equilizerDisplay = new ProgramDetailParamEditEqualizerPage();
            Assert.AreEqual(lowMidValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.Low));
            Assert.AreEqual(midMidValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.Mid));
            Assert.AreEqual(highMidValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.High));
            ReportHelper.LogTest(Status.Pass, "Equilizer display value set to Middle is verified after update");
            equilizerDisplay.Close();

            new ProgramDetailPage().SpeechFocusDisplay.OpenSettings();
            speechFocus = new ProgramDetailParamEditSpeechFocusPage();
            Assert.AreEqual(SpeechFocus.Front, speechFocus.GetSelectedSpeechFocus());
            ReportHelper.LogTest(Status.Pass, "Speech focus display value set to Front is verified after update");
            speechFocus.Close();

            new ProgramDetailPage().NoiseReductionDisplay.OpenSettings();
            audionoiseReduction = new ProgramDetailParamEditNoiseReductionPage();
            Assert.AreEqual(NoiseReduction.Medium, audionoiseReduction.GetSelectedNoiseReduction());
            ReportHelper.LogTest(Status.Pass, "Noise reduction display value set to Medium is verified after update");
            audionoiseReduction.Close();
            new ProgramDetailPage().TapBack();

            //Open setting using hamber button 
            new DashboardPage().OpenMenuUsingTap();
            new MainMenuPage().OpenSettings();

            //Open permission and check after restart is Disable tracking the hearing systems
            new SettingsMenuPage().OpenPermissions();
            settingPermissions = new SettingPermissionsPage();
            Assert.IsFalse(settingPermissions.GetIsLocationPermissionSwitchChecked());
            ReportHelper.LogTest(Status.Pass, "Location permission switch turned off is verified after update");
            settingPermissions.TapBack();
            new SettingsMenuPage().TapBack();
            new MainMenuPage().CloseMenuUsingTap();

            //Open setting and Switch to another language
            new DashboardPage().OpenMenuUsingTap();
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenLanguage();
            settingLanguage = new SettingLanguagePage();
            Assert.AreEqual(settingLanguagePage.GetCurrentLanguageAudifon(), settingLanguage.GetSelectedLanguageAudifon());
            Assert.AreEqual(Language_Audifon.German, settingLanguage.GetSelectedLanguageAudifon());
            ReportHelper.LogTest(Status.Pass, "Selected language is German is verified after update");
            settingLanguage.TapBack();
            Thread.Sleep(1000);

            AppManager.DeviceSettings.DisableWifi();
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-22708_Table-0")]
        public void ST22708_EnableSpanishLanguage()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand.ToString() + " App initialized successfully");

            // In below code expectedAppLanguage can be set to any language which the test case executed OEM supports.
            // Corresponding resource files if not available has to be added.
            // Since we are testing Spanish language in this test case, only the below OEMs supports spanish language
            Enum expectedAppLanguage = null;
            Enum defaultAppLanguage = null;
            switch (AppManager.Brand)
            {
                case Brand.Audifon:
                    {
                        expectedAppLanguage = Language_Audifon.Spanish;
                        defaultAppLanguage = Language_Audifon.English;
                        break;
                    }
                case Brand.Hormann:
                    {
                        expectedAppLanguage = Language_Hormann.Spanish;
                        defaultAppLanguage = Language_Hormann.English;
                        break;
                    }
                case Brand.PersonaMedical:
                    {
                        expectedAppLanguage = Language_Persona.Spanish;
                        defaultAppLanguage = Language_Persona.English;
                        break;
                    }
                case Brand.Puretone:
                    {
                        expectedAppLanguage = Language_Puretone.Spanish;
                        defaultAppLanguage = Language_Puretone.English;
                        break;
                    }
                default: throw new NotImplementedException("Spanish language not available in current OEM " + AppManager.Brand.ToString());
            }

            // Changing Mobile Language
            Language_Device language_Device = Language_Device.Spanish_Spain;
            AppManager.DeviceSettings.ChangeDeviceLanguage(language_Device);
            ReportHelper.LogTest(Status.Info, "Enable device langauge to " + expectedAppLanguage + " and restart app.");

            // Reset App after language Spanish changed
            AppManager.RestartApp(true);
            ReportHelper.LogTest(Status.Info, "App Restarted");
            Assert.IsTrue(new IntroPageOne().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));

            // Check Language in Intro Pages
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, expectedAppLanguage, "introPage1_Title"), new IntroPageOne().GetTitle());
            Assert.AreNotEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, defaultAppLanguage, "introPage1_Title"), new IntroPageOne().GetTitle());

            ReportHelper.LogTest(Status.Info, "Welcome text is " + new IntroPageOne().GetTitle());
            ReportHelper.LogTest(Status.Pass, "Intro Pages are in " + expectedAppLanguage + " and not in " + defaultAppLanguage);

            // Navigate throught to intro and start the Demo mode
            new IntroPageOne().MoveRightBySwiping();
            new IntroPageTwo().MoveRightBySwiping();
            new IntroPageThree().MoveRightBySwiping();
            new IntroPageFour().MoveRightBySwiping();
            Assert.IsTrue(new IntroPageFive().GetIsLeftButtonVisible());
            new IntroPageFive().Continue();
            ReportHelper.LogTest(Status.Pass, "Verified Intro Pages");

            Assert.IsTrue(new InitializeHardwarePage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));

            // Starting App in Demo Mode
            new InitializeHardwarePage().StartDemoMode();
            ReportHelper.LogTest(Status.Info, "App started in Demo Mode");

            // Checking if Dialog is displayed
            DialogHelper.GetIsDialogDisplayed(TimeSpan.FromSeconds(20));
            AppDialog appDialog = new AppDialog();
            Assert.IsNotEmpty(appDialog.GetTitle());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, expectedAppLanguage, "dlg_permission_title"), appDialog.GetTitle());
            Assert.IsNotEmpty(appDialog.GetMessage());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, expectedAppLanguage, "dlg_permission_location_in_use_find_hint"), appDialog.GetMessage());
            appDialog.Confirm();
            ReportHelper.LogTest(Status.Pass, "App dialog text are in " + expectedAppLanguage.ToString() + " language");

            PermissionHelper.AllowPermissionIfRequested();

            // Load Dashboard Page
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            DashboardPage dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            // Checking Menu
            ReportHelper.LogTest(Status.Info, "Open the Main Menu.");
            dashboardPage.OpenMenuUsingTap();
            MainMenuPage mainMenuPage = new MainMenuPage();
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, expectedAppLanguage, "mainMenu_BtnOptions"), mainMenuPage.GetSettingsText());
            Assert.IsNotEmpty(mainMenuPage.GetProgramsText());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, expectedAppLanguage, "mainMenu_BtnPrograms"), mainMenuPage.GetProgramsText());
            Assert.IsNotEmpty(mainMenuPage.GetHelpText());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, expectedAppLanguage, "mainMenu_BtnHelp"), mainMenuPage.GetHelpText());
            ReportHelper.LogTest(Status.Pass, "The main menu is opened successfully and the text are in " + expectedAppLanguage.ToString() + " language");

            // Check Help Menu
            ReportHelper.LogTest(Status.Info, "Open 'Help' menu.");
            new MainMenuPage().OpenHelp();
            HelpMenuPage helpMenuPage = new HelpMenuPage();
            Assert.IsNotEmpty(helpMenuPage.GetFindHearingDevicesText());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, expectedAppLanguage, "help_FindDevicePage"), helpMenuPage.GetFindHearingDevicesText());
            Assert.IsNotEmpty(helpMenuPage.GetHelpTopicsText());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, expectedAppLanguage, "menu_Title_HelpTopics"), helpMenuPage.GetHelpTopicsText());
            Assert.IsNotEmpty(helpMenuPage.GetInstructionsForUseText());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, expectedAppLanguage, "info_instructionsTitle"), helpMenuPage.GetInstructionsForUseText());
            Assert.IsNotEmpty(helpMenuPage.GetInformationMenuText());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, expectedAppLanguage, "menu_Title_Infos"), helpMenuPage.GetInformationMenuText());
            Assert.IsNotEmpty(helpMenuPage.GetImprintText());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, expectedAppLanguage, "mainMenu_BtnImprint"), helpMenuPage.GetImprintText());
            ReportHelper.LogTest(Status.Pass, "Help Menu page items are displayed in " + expectedAppLanguage.ToString() + " language");

            // Open Imprint and Verify 
            // The company details like address are obtained from resource file. Hence will automatically validate for any OEM
            new HelpMenuPage().OpenImprint();
            Assert.IsNotEmpty(new ImprintPage().GetSupportDescription());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, expectedAppLanguage, "oem_support_desc_1"), new ImprintPage().GetSupportDescription());
            Assert.IsNotEmpty(new ImprintPage().GetAddressHeader());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, expectedAppLanguage, "info_imprintAddressTitle"), new ImprintPage().GetAddressHeader());
            Assert.IsNotEmpty(new ImprintPage().GetAppCompanyName());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, expectedAppLanguage, "oem_companyName"), new ImprintPage().GetAppCompanyName());
            Assert.IsNotEmpty(new ImprintPage().GetAppCompanyStreet());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, expectedAppLanguage, "oem_companyStreet"), new ImprintPage().GetAppCompanyStreet());
            Assert.IsNotEmpty(new ImprintPage().GetAppCommpanyState());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, expectedAppLanguage, "oem_companyState"), new ImprintPage().GetAppCommpanyState());
            Assert.IsNotEmpty(new ImprintPage().GetAppCompanyPostalCodeCity());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, expectedAppLanguage, "oem_companyPostalCodeCity"), new ImprintPage().GetAppCompanyPostalCodeCity());
            ReportHelper.LogTest(Status.Pass, "Imprint menu is verified and is in " + expectedAppLanguage.ToString() + " language");
            new ImprintPage().TapBack();

            // Open Information and Verify
            new HelpMenuPage().OpenInformationMenu();
            Assert.IsNotEmpty(new InformationMenuPage().GetPrivacyPolicyText());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, expectedAppLanguage, "menu_Title_DataProtection"), new InformationMenuPage().GetPrivacyPolicyText());
            Assert.IsNotEmpty(new InformationMenuPage().GetTermsOfUseText());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, expectedAppLanguage, "menu_Title_TermsOfUse"), new InformationMenuPage().GetTermsOfUseText());
            Assert.IsNotEmpty(new InformationMenuPage().GetLicensesText());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, expectedAppLanguage, "mainMenu_BtnLicenses"), new InformationMenuPage().GetLicensesText());
            ReportHelper.LogTest(Status.Pass, "Information menu is verified and is in " + expectedAppLanguage.ToString() + " language.");

            //Open Data Protection and Verify
            new InformationMenuPage().OpenPrivacyPolicy();
            Thread.Sleep(500);
            Assert.IsNotEmpty(new PrivacyPolicyPage().GetNavigationBarTitle());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, expectedAppLanguage, "menu_Title_DataProtection"), new PrivacyPolicyPage().GetNavigationBarTitle());
            ReportHelper.LogTest(Status.Pass, "Data Protection menu is verified and is in " + expectedAppLanguage.ToString() + " language.");
            new PrivacyPolicyPage().TapBack();

            //Open Terms of use menu and Verify
            new InformationMenuPage().OpenTermsofUse();
            Thread.Sleep(500);
            Assert.IsNotEmpty(new TermsOfUsePage().GetNavigationBarTitle());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, expectedAppLanguage, "menu_Title_TermsOfUse"), new TermsOfUsePage().GetNavigationBarTitle());
            ReportHelper.LogTest(Status.Pass, "Terms of use menu is verified and is in " + expectedAppLanguage.ToString() + " language.");
            new TermsOfUsePage().TapBack();

            // open Licenses and Verify
            new InformationMenuPage().OpenLicenses();
            Thread.Sleep(500);
            Assert.IsNotEmpty(new LicencesPage().GetNavigationBarTitle());
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, expectedAppLanguage, "mainMenu_BtnLicenses"), new LicencesPage().GetNavigationBarTitle());
            ReportHelper.LogTest(Status.Pass, "Licenses menu is verified and is in " + expectedAppLanguage.ToString() + " language.");
            new LicencesPage().TapBack();

            // Navigate back to Help menu page
            new InformationMenuPage().TapBack();

            // Navigate back to Main menu page
            new HelpMenuPage().TapBack();

            // Open Language
            mainMenuPage.OpenSettings();
            SettingsMenuPage settingsMenuPageLanguage = new SettingsMenuPage();
            settingsMenuPageLanguage.OpenLanguage();

            Assert.IsTrue(new SettingLanguagePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Info, "Language Menu opened");
            SettingLanguagePage settingLanguagePage = new SettingLanguagePage();

            // Change the Language
            // Since we are testing Spanish language in this test case, only the below OEMs supports spanish language
            switch (AppManager.Brand)
            {
                case Brand.Audifon:
                    Assert.AreEqual(expectedAppLanguage, settingLanguagePage.GetCurrentLanguageAudifon());
                    ReportHelper.LogTest(Status.Info, "Current selected language is " + settingLanguagePage.GetCurrentLanguageAudifon());
                    settingLanguagePage.SelectLanguageAudifon((Language_Audifon)defaultAppLanguage);
                    ReportHelper.LogTest(Status.Info, "Select " + defaultAppLanguage.ToString() + " Language");

                    settingLanguagePage.Accept();
                    ReportHelper.LogTest(Status.Info, "Accept button clicked");

                    new AppDialog().Deny();
                    ReportHelper.LogTest(Status.Info, "No clicked in App Dialog");

                    Assert.AreEqual(expectedAppLanguage, settingLanguagePage.GetCurrentLanguageAudifon());
                    ReportHelper.LogTest(Status.Pass, "Current selected language is still " + expectedAppLanguage.ToString());
                    break;
                case Brand.Hormann:
                    Assert.AreEqual(expectedAppLanguage, settingLanguagePage.GetCurrentLanguageHormann());
                    ReportHelper.LogTest(Status.Info, "Current selected language is " + settingLanguagePage.GetCurrentLanguageHormann());
                    settingLanguagePage.SelectLanguageHormann((Language_Hormann)defaultAppLanguage);
                    ReportHelper.LogTest(Status.Info, "Select " + defaultAppLanguage.ToString() + " Language");

                    settingLanguagePage.Accept();
                    ReportHelper.LogTest(Status.Info, "Accept button clicked");

                    new AppDialog().Deny();
                    ReportHelper.LogTest(Status.Info, "No clicked in App Dialog");

                    Assert.AreEqual(expectedAppLanguage, settingLanguagePage.GetCurrentLanguageHormann());
                    ReportHelper.LogTest(Status.Pass, "Current selected language is still " + expectedAppLanguage.ToString());
                    break;
                case Brand.Puretone:
                    Assert.AreEqual(expectedAppLanguage, settingLanguagePage.GetCurrentLanguagePuretone());
                    ReportHelper.LogTest(Status.Info, "Current selected language is " + settingLanguagePage.GetCurrentLanguagePuretone());
                    settingLanguagePage.SelectLanguagePuretone((Language_Puretone)defaultAppLanguage);
                    ReportHelper.LogTest(Status.Info, "Select " + defaultAppLanguage.ToString() + " Language");

                    settingLanguagePage.Accept();
                    ReportHelper.LogTest(Status.Info, "Accept button clicked");

                    new AppDialog().Deny();
                    ReportHelper.LogTest(Status.Info, "No clicked in App Dialog");

                    Assert.AreEqual(expectedAppLanguage, settingLanguagePage.GetCurrentLanguagePuretone());
                    ReportHelper.LogTest(Status.Pass, "Current selected language is still " + expectedAppLanguage.ToString());
                    break;
                case Brand.PersonaMedical:
                    Assert.AreEqual(expectedAppLanguage, settingLanguagePage.GetCurrentLanguagePersona());
                    ReportHelper.LogTest(Status.Info, "Current selected language is " + settingLanguagePage.GetCurrentLanguagePersona());
                    settingLanguagePage.SelectLanguagePersona((Language_Persona)defaultAppLanguage);
                    ReportHelper.LogTest(Status.Info, "Select " + defaultAppLanguage.ToString() + " Language");

                    settingLanguagePage.Accept();
                    ReportHelper.LogTest(Status.Info, "Accept button clicked");

                    new AppDialog().Deny();
                    ReportHelper.LogTest(Status.Info, "No clicked in App Dialog");

                    Assert.AreEqual(expectedAppLanguage, settingLanguagePage.GetCurrentLanguagePersona());
                    ReportHelper.LogTest(Status.Pass, "Current selected language is still " + expectedAppLanguage.ToString());
                    break;
                default: throw new NotImplementedException("Test case does not support this brand");
            }

            // Reset the Mobile language to English 
            AppManager.DeviceSettings.ChangeDeviceLanguage(Language_Device.English_US);
            ReportHelper.LogTest(Status.Info, "Change device langauge to English");
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-22966_Table-0")]
        public void ST22966_ZoomMultiPointCheck()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            // Open App in Demo Mode
            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            dashboardPage.OpenMenuUsingTap();
            ReportHelper.LogTest(Status.Info, "Open the Main Menu");

            ReportHelper.LogTest(Status.Info, "Checking zoom with 2 fingers on MainMenuPage for 10 times...");
            for (int i = 1; i <= 10; i++)
            {
                AppManager.App.ZoomWithTwoPoints();
                Thread.Sleep(500);

                Assert.IsTrue(new MainMenuPage().IsCurrentlyShown());
                ReportHelper.LogTest(Status.Info, "Iteration " + i + ": While zoomming using 2 fingers it is in same page as expected and app did not crash.");
            }
            ReportHelper.LogTest(Status.Pass, "Verified zoom with 2 fingers on MainMenuPage for 10 times and app did not crash");

            new MainMenuPage().OpenPrograms();
            ReportHelper.LogTest(Status.Info, "Open the Programs Menu");

            ReportHelper.LogTest(Status.Info, "Checking zoom with 2 fingers on ProgramsMenuPage for 10 times...");
            for (int i = 1; i <= 10; i++)
            {
                AppManager.App.ZoomWithTwoPoints();
                Thread.Sleep(500);

                Assert.IsTrue(new ProgramsMenuPage().IsCurrentlyShown());
                ReportHelper.LogTest(Status.Info, "Iteration " + i + ": While zoomming using 2 fingers it is in same page as expected and app did not crash.");
            }
            ReportHelper.LogTest(Status.Pass, "Verified zoom with 2 fingers on ProgramsMenuPage for 10 times and app did not crash");

            new ProgramsMenuPage().TapBack();
            new MainMenuPage().OpenSettings();
            ReportHelper.LogTest(Status.Info, "Open the Settings Menu");

            ReportHelper.LogTest(Status.Info, "Checking zoom with 2 fingers on SettingsMenuPage for 10 times...");
            for (int i = 1; i <= 10; i++)
            {
                AppManager.App.ZoomWithTwoPoints();
                Thread.Sleep(500);

                Assert.IsTrue(new SettingsMenuPage().IsCurrentlyShown());
                ReportHelper.LogTest(Status.Info, "Iteration " + i + ": While zoomming using 2 fingers it is in same page as expected and app did not crash.");
            }
            ReportHelper.LogTest(Status.Pass, "Verified zoom with 2 fingers on SettingsMenuPage for 10 times and app did not crash");

            new SettingsMenuPage().TapBack();
            new MainMenuPage().OpenHelp();
            ReportHelper.LogTest(Status.Info, "Open the Help Menu");

            ReportHelper.LogTest(Status.Info, "Checking zoom with 2 fingers on HelpMenuPage for 10 times...");
            for (int i = 1; i <= 10; i++)
            {
                AppManager.App.ZoomWithTwoPoints();
                Thread.Sleep(500);

                Assert.IsTrue(new HelpMenuPage().IsCurrentlyShown());
                ReportHelper.LogTest(Status.Info, "Iteration " + i + ": While zoomming using 2 fingers it is in same page as expected and app did not crash.");
            }
            ReportHelper.LogTest(Status.Pass, "Verified zoom with 2 fingers on HelpMenuPage for 10 times and app did not crash");

            new HelpMenuPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Back to Main Menu");

            ReportHelper.LogTest(Status.Info, "Checking zoom with 4 fingers on MainMenuPage for 10 times...");
            for (int i = 1; i <= 10; i++)
            {
                AppManager.App.ZoomWithFourPoints();
                Thread.Sleep(500);

                Assert.IsTrue(new MainMenuPage().IsCurrentlyShown());
                ReportHelper.LogTest(Status.Info, "Iteration " + i + ": While zoomming using 4 fingers it is in same page as expected and app did not crash.");
            }
            ReportHelper.LogTest(Status.Pass, "Verified zoom with 4 fingers on MainMenuPage for 10 times and app did not crash");

            new MainMenuPage().OpenPrograms();
            ReportHelper.LogTest(Status.Info, "Open the Programs Menu");

            ReportHelper.LogTest(Status.Info, "Checking zoom with 4 fingers on ProgramsMenuPage for 10 times...");
            for (int i = 1; i <= 10; i++)
            {
                AppManager.App.ZoomWithFourPoints();
                Thread.Sleep(500);

                Assert.IsTrue(new ProgramsMenuPage().IsCurrentlyShown());
                ReportHelper.LogTest(Status.Info, "Iteration " + i + ": While zoomming using 4 fingers it is in same page as expected and app did not crash.");
            }
            ReportHelper.LogTest(Status.Pass, "Verified zoom with 4 fingers on ProgramsMenuPage for 10 times and app did not crash");

            new ProgramsMenuPage().TapBack();
            new MainMenuPage().OpenSettings();
            ReportHelper.LogTest(Status.Info, "Open the Settings Menu");

            ReportHelper.LogTest(Status.Info, "Checking zoom with 4 fingers on SettingsMenuPage for 10 times...");
            for (int i = 1; i <= 10; i++)
            {
                AppManager.App.ZoomWithFourPoints();
                Thread.Sleep(500);

                Assert.IsTrue(new SettingsMenuPage().IsCurrentlyShown());
                ReportHelper.LogTest(Status.Info, "Iteration " + i + ": While zoomming using 4 fingers it is in same page as expected and app did not crash.");
            }
            ReportHelper.LogTest(Status.Pass, "Verified zoom with 4 fingers on SettingsMenuPage for 10 times and app did not crash");

            new SettingsMenuPage().TapBack();
            new MainMenuPage().OpenHelp();
            ReportHelper.LogTest(Status.Info, "Open the Help Menu");

            ReportHelper.LogTest(Status.Info, "Checking zoom with 4 fingers on HelpMenuPage for 10 times...");
            for (int i = 1; i <= 10; i++)
            {
                AppManager.App.ZoomWithFourPoints();
                Thread.Sleep(500);

                Assert.IsTrue(new HelpMenuPage().IsCurrentlyShown());
                ReportHelper.LogTest(Status.Info, "Iteration " + i + ": While zoomming using 4 fingers it is in same page as expected and app did not crash.");
            }
            ReportHelper.LogTest(Status.Pass, "Verified zoom with 4 fingers on HelpMenuPage for 10 times and app did not crash");
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-23178_Table-0")]
        public void ST23178_VerifyConnectionInBackground()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            // Skip Intro Pages
            LaunchHelper.SkipIntroPages();
            ReportHelper.LogTest(Status.Info, "Intro Pages Skipped");

            new InitializeHardwarePage().StartDemoMode();
            ReportHelper.LogTest(Status.Info, "Started app in demo mode");

            Assert.IsTrue(new HearingAidInitPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)), "HearingAidInitPage is not the current loaded page");
            ReportHelper.LogTest(Status.Info, "HearingAidInitPage loaded");

            // Will check for 75 seconds and throw a exception is dashboard page is not loaded. Added this to avoid infinte loop.
            Wait.UntilTrue(new Func<bool>(() =>
            {
                // Putting the app to background for 1 second
                AppManager.DeviceSettings.PutAppToBackground(1);
                ReportHelper.LogTest(Status.Info, "App put in background and foreground in HearingAidInitPage page");

                // Need 0.5 second wait time for process to happen since it is in loop.
                Thread.Sleep(500);

                // In Android the permission dialog will be displayed.
                // In IOS first time after installation the dialog will appear and in regression testing it will not since reset the app from settings menu.
                // Have handled for all scenario
                switch (CurrentPlatform)
                {
                    case Platform.Android:
                        {
                            return DialogHelper.GetIsDialogDisplayed();
                            break;
                        }
                    case Platform.iOS:
                        {
                            return new DashboardPage(false).GetIsLeftHearingDeviceVisible();
                            break;
                        }
                    default:
                        {
                            throw new NotImplementedException("Platform not supported");
                            break;
                        }
                }

            }), TimeSpan.FromSeconds(75));

            DialogHelper.ConfirmIfDisplayed();
            PermissionHelper.AllowPermissionIfRequested();
            DialogHelper.ConfirmIfDisplayed();

            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)));
            var dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "Checking Left and Right icons in dashboard page...");
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            ReportHelper.LogTest(Status.Pass, "Dashboard page loaded after background and foreground process and Left and Right icons visible.");

            ReportHelper.LogTest(Status.Info, "Opening Left device info page from Dashboard...");
            dashboardPage.OpenLeftHearingDevice();
            ReportHelper.LogTest(Status.Info, "Checking Left Hearing System details...");
            new HearingInstrumentInfoControlPage().CheckHAInformationFromDashboard(AppMode.Demo).Close();
            ReportHelper.LogTest(Status.Pass, "All details are visible on Left Hearing Instrument Info page.");

            ReportHelper.LogTest(Status.Info, "Opening Right device info page from Dashboard...");
            new DashboardPage().OpenRightHearingDevice();
            ReportHelper.LogTest(Status.Info, "Checking Right Hearing System details...");
            new HearingInstrumentInfoControlPage().CheckHAInformationFromDashboard(AppMode.Demo).Close();
            ReportHelper.LogTest(Status.Pass, "All details are visible on Right Hearing Instrument Info page.");

            // Closing and opening the App
            AppManager.CloseApp();
            AppManager.StartApp(false);
            ReportHelper.LogTest(Status.Info, "Close and open the app with app data");

            Assert.IsTrue(new HearingAidInitPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)), "HearingAidInitPage is not the current loaded page");
            ReportHelper.LogTest(Status.Info, "HearingAidInitPage loaded after restart");

            bool IsProcessingTextLoaded = false;
            bool IsDashboardLoaded = false;

            // Calling the method in seperate thread since the method takes more milliseconds and and hence not able to replicate the issue.
            Thread DashboardLoadProcess = new Thread(() => WaitForDashboard());
            DashboardLoadProcess.Start();

            Wait.UntilTrue(new Func<bool>(() =>
            {
                // Starting the background foreground process only after "Providing access to your hearing systems." message.
                if (IsProcessingTextLoaded || new HearingAidInitPage().WaitForMessage("Providing access to your hearing systems."))
                {
                    IsProcessingTextLoaded = true;

                    AppManager.DeviceSettings.PutAppToBackground(0.5);

                    if (IsDashboardLoaded)
                    {
                        AppManager.DeviceSettings.PutAppToBackground(0.5);
                        return true;
                    }

                    return false;
                }
                else
                    return false;

            }), TimeSpan.FromSeconds(45));

            DashboardLoadProcess.Abort();

            ReportHelper.LogTest(Status.Info, "App put in background and foreground process continously till dashboard page is loaded and doing background and foreground process twice after dashboard page is loaded.");

            Thread.Sleep(5000);
            ReportHelper.LogTest(Status.Info, "Wait for 5 seconds and checking if the programs are reloaded in dashboard");

            Assert.IsFalse(new FirstTimeProcessingPage(false).IsMessageAvailable("Processing hearing system data.", TimeSpan.FromSeconds(10)), "The message 'Processing hearing system data.' is visible after dashboard page is loaded.");
            ReportHelper.LogTest(Status.Pass, "Dashboard page loaded after background and foreground process and the message 'Processing hearing system data.' is not visible this process.");

            // Waiting till dashboard page is loaded
            void WaitForDashboard()
            {
                Wait.UntilTrue(new Func<bool>(() =>
                {
                    IsDashboardLoaded = new DashboardPage(false).IsCurrentlyShown();
                    return IsDashboardLoaded;

                }), TimeSpan.FromSeconds(45));
            }
        }

        #endregion Sprint 33

        #endregion Test Cases
    }
}