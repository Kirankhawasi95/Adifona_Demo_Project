using System;
using HorusUITest.Enums;
using HorusUITest.Helper;
using HorusUITest.PageObjects;
using AventStack.ExtentReports;
using HorusUITest.PageObjects.Dialogs;
using HorusUITest.PageObjects.Favorites;
using HorusUITest.PageObjects.Menu;
using HorusUITest.PageObjects.Menu.Settings;
using HorusUITest.PageObjects.Start;
using NUnit.Framework;
using HorusUITest.PageObjects.Controls.ProgramDetailParams;
using HorusUITest.PageObjects.Start.Intro;
using HorusUITest.PageObjects.Menu.Info;
using HorusUITest.PageObjects.Controls;
using System.Threading;
using System.Linq;
using HorusUITest.PageObjects.Favorites.Automation;

namespace HorusUITest.TestFixtures.TestCases
{
    public class SystemTestsDemo8 : BaseResettingTestFixture
    {
        #region Constructor

        public SystemTestsDemo8(Platform platform) : base(platform)
        {

        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Change Device Language
        /// </summary>
        /// <param name="language_Device"></param>
        /// <param name="expectedWelcomePageTitle"></param>
        /// <param name="expectedAppLanguage"></param>
        /// <returns></returns>
        private string ChangeDeviceLanguageAndVerifyInApp(Language_Device language_Device, string expectedWelcomePageTitle, Language_Audifon expectedAppLanguage)
        {
            ReportHelper.LogTest(Status.Info, "Changing mobile device langauge to '" + language_Device + "'...");
            AppManager.DeviceSettings.ChangeDeviceLanguage(language_Device);
            ReportHelper.LogTest(Status.Info, "Changed device langauge to '" + language_Device + "'");

            ReportHelper.LogTest(Status.Info, "Restarting app after language change...");
            AppManager.RestartApp(true);
            ReportHelper.LogTest(Status.Info, "Restarted app after language change");

            ReportHelper.LogTest(Status.Info, "Waiting till intro1 page is loaded...");
            Assert.IsTrue(new IntroPageOne().IsShowingBeforeTimeout(TimeSpan.FromSeconds(30)), "Intro1 page is not loaded");
            ReportHelper.LogTest(Status.Info, "Intro1 page is loaded");
            string welocmePageTitle = new IntroPageOne().GetTitle();
            ReportHelper.LogTest(Status.Info, "Checking intro1 page title...");
            Assert.IsNotEmpty(welocmePageTitle, "Intro1 page title is empty");
            ReportHelper.LogTest(Status.Info, "Intro1 page title is not empty");
            ReportHelper.LogTest(Status.Info, "Checking intro1 page title shown in '" + expectedAppLanguage + "' correctly...");
            Assert.AreEqual(expectedWelcomePageTitle, welocmePageTitle, "Intro1 page title not shown in '" + expectedAppLanguage + "' correctly");
            ReportHelper.LogTest(Status.Pass, "Intro1 page title shown in '" + expectedAppLanguage + "' correctly");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "App started in demo mode after skipping intro pages and dashboard is loaded");

            ReportHelper.LogTest(Status.Info, "Opening language page from through main menu from dashboard using tap...");
            NavigationHelper.NavigateToSettingsMenu(dashboardPage).OpenLanguage();
            ReportHelper.LogTest(Status.Info, "Opened language page from through main menu from dashboard using tap");
            ReportHelper.LogTest(Status.Info, "Waiting till language page is loaded...");
            Assert.IsTrue(new SettingLanguagePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)), "Language page is not loaded");
            var settingLanguagePage = new SettingLanguagePage();
            ReportHelper.LogTest(Status.Info, "Language page is loaded");
            ReportHelper.LogTest(Status.Info, "Checking if app langauge is set to '" + expectedAppLanguage + "'");
            Assert.AreEqual(expectedAppLanguage, settingLanguagePage.GetCurrentLanguageAudifon());
            ReportHelper.LogTest(Status.Pass, "App langauge is set to '" + expectedAppLanguage + "' and it is verified");

            return welocmePageTitle;
        }

        /// <summary>
        /// Verify if Text has Special Charecters
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static bool hasSpecialChar(string input)
        {
            string specialChar = @"|!#$%&/()=?»«@£§€{}.-;~`'<>_,1234567890";
            foreach (var item in specialChar)
            {
                if (input.Contains(item)) return true;
            }
            return false;
        }

        #endregion Methods

        #region Test Cases

        #region Sprint 14

        /// <summary>
        /// Test step 'Click multiple times in rapid succession' is not implemented after confirmation from Audifon team.
        /// </summary>
        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-11992_Table-51")]
        public void ST11992_CheckOpeningOfMenuItems()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            ReportHelper.LogTest(Status.Info, "Open the Main Menu.");
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            dashboardPage.OpenMenuUsingTap();
            var mainMenuPage = new MainMenuPage();
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText());
            Assert.IsNotEmpty(mainMenuPage.GetProgramsText());
            Assert.IsNotEmpty(mainMenuPage.GetHelpText());
            ReportHelper.LogTest(Status.Pass, "The main menu is opened successfully.");

            ReportHelper.LogTest(Status.Info, "Open the 'Settings' menu item from Main Menu.");
            mainMenuPage.OpenSettings();
            var settingsMenuPage = new SettingsMenuPage();
            Assert.IsNotEmpty(settingsMenuPage.GetMyHearingAidsText());
            Assert.IsNotEmpty(settingsMenuPage.GetPermissionsText());
            Assert.IsNotEmpty(settingsMenuPage.GetLanguageText());
            Assert.IsNotEmpty(settingsMenuPage.GetDemoModeText());
            ReportHelper.LogTest(Status.Pass, "The Settings menu is opened all items are visible successfully.");

            ReportHelper.LogTest(Status.Info, "Navigate back to Main Menu.");
            settingsMenuPage.NavigateBack();
            Assert.IsNotEmpty(new MainMenuPage().GetSettingsText());
            Assert.IsNotEmpty(new MainMenuPage().GetProgramsText());
            Assert.IsNotEmpty(new MainMenuPage().GetHelpText());
            ReportHelper.LogTest(Status.Pass, "Navigated back successfully to Main Menu.");

            ReportHelper.LogTest(Status.Info, "Tap 'Settings' and 'Help' simultanously. Repeat action 5 times");
            for (int i = 1; i < 6; i++)
            {
                ReportHelper.LogTest(Status.Info, "MultiTouch Iteration : " + i);
                new MainMenuPage().TapSettingsAndHelpSimultanously();

                if (new SettingsMenuPage(false).IsCurrentlyShown())
                {
                    ReportHelper.LogTest(Status.Pass, "Settings menu page is visible after simultaneous click action.");
                    new SettingsMenuPage().NavigateBack();
                }
                else if (new HelpMenuPage(false).IsCurrentlyShown())
                {
                    ReportHelper.LogTest(Status.Pass, "Help menu page is visible after simultaneous click action.");
                    new HelpMenuPage().NavigateBack();
                }

                Assert.IsTrue(new MainMenuPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
                ReportHelper.LogTest(Status.Pass, "Main Menu is displayed after back navigation.");
            }
            Assert.IsNotEmpty(new MainMenuPage().GetHelpText());
            ReportHelper.LogTest(Status.Info, "Open 'Help' menu.");
            new MainMenuPage().OpenHelp();
            var helpMenuPage = new HelpMenuPage();
            Assert.IsNotEmpty(helpMenuPage.GetFindHearingDevicesText());
            Assert.IsNotEmpty(helpMenuPage.GetHelpTopicsText());
            Assert.IsNotEmpty(helpMenuPage.GetInstructionsForUseText());
            Assert.IsNotEmpty(helpMenuPage.GetInformationMenuText());
            Assert.IsNotEmpty(helpMenuPage.GetImprintText());
            ReportHelper.LogTest(Status.Pass, "Help Menu page items are displayed.");
        }

        #endregion Sprint 14

        #region Sprint 15

        /// <summary>
        /// As discussed with Audifon team the changing of OS language on iOS is not possible by automation.
        /// Hence this test case is only applicable for Android
        /// </summary>
        [Test]
        //[Ignore("It is not possible to automate change in OS language in iOS.")]
        [Category("SystemTestsDemo")]
        [Description("TC-12469_Table-42")]
        public void ST12469_ChangeDefaultLanguage()
        {
            ChangeDeviceLanguageAndVerifyInApp(Language_Device.German_Germany, "Herzlich willkommen!", Language_Audifon.German);
            ChangeDeviceLanguageAndVerifyInApp(Language_Device.English_US, "Welcome!", Language_Audifon.English);
            ChangeDeviceLanguageAndVerifyInApp(Language_Device.Spanish_Spain, "¡Bienvenido!", Language_Audifon.Spanish);
            ChangeDeviceLanguageAndVerifyInApp(Language_Device.German_Switzerland, "Herzlich willkommen!", Language_Audifon.German);
            ChangeDeviceLanguageAndVerifyInApp(Language_Device.Spanish_Mexico, "¡Bienvenido!", Language_Audifon.Spanish);
            ChangeDeviceLanguageAndVerifyInApp(Language_Device.Italian_Italy, "Benvenuto!", Language_Audifon.Italian);

            ReportHelper.LogTest(Status.Info, "Change device langauge to a language which is not part of app.");
            string welcomePageTitle = ChangeDeviceLanguageAndVerifyInApp(Language_Device.Hindi_India, "Welcome!", Language_Audifon.English);
            Assert.AreNotEqual("Benvenuto!", welcomePageTitle);
            AppManager.DeviceSettings.ChangeDeviceLanguage(Language_Device.English_US);
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-12510_Table-41")]
        public void ST12510_CheckFonts()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");
            // Wait till the toast message disappears. It causes the test case to fail if it does not wait.
            Thread.Sleep(3000);

            // Open Main Menu
            new DashboardPage().OpenMenuUsingTap();
            var mainMenuPage = new MainMenuPage();
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText());
            ReportHelper.LogTest(Status.Pass, "The main menu is opened.");

            //Open Settings
            mainMenuPage.OpenSettings();
            new SettingsMenuPage().OpenLanguage();
            ReportHelper.LogTest(Status.Pass, "Language menu is opened.");

            // Checking for all the Languages
            // ToDo: Not to check for All languages and test for laguages suggested to be checked by Audifon
            SettingLanguagePage settingLanguagePage = new SettingLanguagePage();

            switch (AppManager.Brand)
            {
                case Brand.Audifon:
                    {
                        foreach (var stringLanguage in Enum.GetNames(typeof(Language_Audifon)))
                        {
                            Language_Audifon enumLanguage = (Language_Audifon)Enum.Parse(typeof(Language_Audifon), stringLanguage);
                            string languageText = settingLanguagePage.GetLanguageAudifonText(enumLanguage);
                            Assert.True(char.IsUpper(languageText[0]));
                        }

                        break;
                    }
                case Brand.Kind:
                    {
                        foreach (var stringLanguage in Enum.GetNames(typeof(Language)))
                        {
                            Language enumLanguage = (Language)Enum.Parse(typeof(Language), stringLanguage);
                            string languageText = settingLanguagePage.GetLanguageText(enumLanguage);
                            Assert.True(char.IsUpper(languageText[0]));
                        }

                        break;
                    }
                case Brand.Hormann:
                    {
                        foreach (var stringLanguage in Enum.GetNames(typeof(Language_Hormann)))
                        {
                            Language_Hormann enumLanguage = (Language_Hormann)Enum.Parse(typeof(Language_Hormann), stringLanguage);
                            string languageText = settingLanguagePage.GetLanguageHormannText(enumLanguage);
                            Assert.True(char.IsUpper(languageText[0]));
                        }

                        break;
                    }
                case Brand.PersonaMedical:
                    {
                        foreach (var stringLanguage in Enum.GetNames(typeof(Language_Persona)))
                        {
                            Language_Persona enumLanguage = (Language_Persona)Enum.Parse(typeof(Language_Persona), stringLanguage);
                            string languageText = settingLanguagePage.GetLanguagePersonaText(enumLanguage);
                            Assert.True(char.IsUpper(languageText[0]));
                        }

                        break;
                    }
                case Brand.Puretone:
                    {
                        foreach (var stringLanguage in Enum.GetNames(typeof(Language_Puretone)))
                        {
                            Language_Puretone enumLanguage = (Language_Puretone)Enum.Parse(typeof(Language_Puretone), stringLanguage);
                            string languageText = settingLanguagePage.GetLanguagePuretoneText(enumLanguage);
                            Assert.True(char.IsUpper(languageText[0]));
                        }

                        break;
                    }
                case Brand.RxEarsPro:
                    {
                        foreach (var stringLanguage in Enum.GetNames(typeof(Language_RxEarsPro)))
                        {
                            Language_RxEarsPro enumLanguage = (Language_RxEarsPro)Enum.Parse(typeof(Language_RxEarsPro), stringLanguage);
                            string languageText = settingLanguagePage.GetLanguageRxEarsProText(enumLanguage);
                            Assert.True(char.IsUpper(languageText[0]));
                        }

                        break;
                    }
            }

            ReportHelper.LogTest(Status.Info, "All languages are starting with upper case");

            new SettingLanguagePage().TapBack();
            new SettingsMenuPage().TapBack();
            new MainMenuPage().CloseMenuUsingTap();

            Wait.UntilTrue(() => dashboardPage.GetIsLeftHearingDeviceVisible(), TimeSpan.FromSeconds(3));
            var currentProgramName = dashboardPage.GetCurrentProgramName();
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            ReportHelper.LogTest(Status.Pass, "Navigated successfully to " + currentProgramName + " page");

            //Verifying the program detail page 
            new ProgramDetailPage().ProgramDetailPageUiCheck();

            //Verifying the text not contains special characters within the detail tiles ("LANGUAGE FOCUS", "SOUND", etc.).
            var programDetailPage = new ProgramDetailPage();
            Assert.IsFalse(hasSpecialChar(programDetailPage.SpeechFocusDisplay.GetTitle()));
            Assert.IsFalse(hasSpecialChar(programDetailPage.TinnitusDisplay.GetTitle()));
            Assert.IsFalse(hasSpecialChar(programDetailPage.NoiseReductionDisplay.GetTitle()));
            Assert.IsFalse(hasSpecialChar(programDetailPage.EqualizerDisplay.GetTitle()));
            ReportHelper.LogTest(Status.Pass, "All texts verified");

            // ToDo: Need to verify overlapping of text in Speech Focus, Noise Reduction and equilizer display in all the languages in Audifon

            //Speech Focus
            programDetailPage.SpeechFocusDisplay.OpenSettings();
            var speechFocusPage = new ProgramDetailParamEditSpeechFocusPage();
            Assert.IsNotEmpty(speechFocusPage.GetCloseButtonText());
            ReportHelper.LogTest(Status.Pass, "Close button spelling is correct in Speech Focus page");
            speechFocusPage.Close();

            //Noise Reduction
            Wait.UntilTrue(() => new ProgramDetailPage().GetIsNoiseReductionDisplayVisible(), TimeSpan.FromSeconds(3));
            new ProgramDetailPage().NoiseReductionDisplay.OpenSettings();
            var noiseReductionPage = new ProgramDetailParamEditNoiseReductionPage();
            Assert.IsNotEmpty(noiseReductionPage.GetCloseButtonText()); ;
            ReportHelper.LogTest(Status.Pass, "Close button spelling is correct in Noise Reduction page");
            noiseReductionPage.Close();

            //Tinnitus
            Wait.UntilTrue(() => programDetailPage.GetIsTinnitusDisplayVisible(), TimeSpan.FromSeconds(3));
            programDetailPage.TinnitusDisplay.OpenSettings();
            var tinnitusPage = new ProgramDetailParamEditTinnitusPage();
            Assert.IsNotEmpty(tinnitusPage.GetCloseButtonText());
            ReportHelper.LogTest(Status.Pass, "Close button spelling is correct in Tinnitus page");
            tinnitusPage.Close();

            //Sound
            Wait.UntilTrue(() => programDetailPage.GetIsEqualizerDisplayVisible(), TimeSpan.FromSeconds(3));
            programDetailPage.EqualizerDisplay.OpenSettings();
            ProgramDetailParamEditEqualizerPage ProgramDetailParamEditEqualizerPage = new ProgramDetailParamEditEqualizerPage();
            Assert.IsNotEmpty(ProgramDetailParamEditEqualizerPage.GetCloseButtonText());
            ReportHelper.LogTest(Status.Pass, "Close button spelling is correct in Sound page");
            ProgramDetailParamEditEqualizerPage.Close();

            //Wait to close 
            Wait.UntilTrue(() => programDetailPage.GetIsEqualizerDisplayVisible(), TimeSpan.FromSeconds(3));

            new ProgramDetailPage().ProgramDetailPageUiCheck();

            new ProgramDetailPage().OpenProgramSettings();
            Assert.True(new ProgramDetailSettingsControlPage().GetIsCreateFavoriteFloatingButtonDisplayed());
            ReportHelper.LogTest(Status.Pass, "Program Settings page opened successfully");

            // Create a favorite with 20 charecters and verify overlappings. Capital W ocupies the most space so creating favorite with 20 W and checking overlapping.
            var myFavNameLong = "WWWWWWWWWWWWWWWWWWWW";
            new ProgramDetailSettingsControlPage().CreateFavorite();
            var programNamePage = new ProgramNamePage();
            new ProgramNamePage().EnterName(myFavNameLong).Proceed();
            new ProgramIconPage().SelectIcon(2).Proceed();

            var programAutomationPage = new ProgramAutomationPage();
            Assert.IsFalse(programAutomationPage.GetIsAutomationSwitchChecked());
            programAutomationPage.TurnOnAutomation();
            Assert.IsTrue(programAutomationPage.GetIsGeofenceAutomationVisible());
            Assert.IsTrue(programAutomationPage.GetIsWifiAutomationVisible());

            // Grant Full Access
            AppManager.DeviceSettings.GrantGPSBackgroundPermission();
            Thread.Sleep(5000);

            programAutomationPage.TapConnectToWiFi();

            var wifiName = new AutomationWifiBindingPage().GetWifiName();
            Assert.IsNotEmpty(wifiName);
            new AutomationWifiBindingPage().Ok();

            new ProgramAutomationPage().Proceed();

            ReportHelper.LogTest(Status.Pass, "Favorite with name '" + myFavNameLong + "' created");

            Assert.IsFalse(new ProgramDetailPage().CheckForProgramNameOverlapping(), "Program name is overlapping");
            ReportHelper.LogTest(Status.Pass, "Program name is not overlapping");

            new ProgramDetailPage().OpenProgramSettings();

            Assert.IsFalse(new ProgramDetailSettingsControlPage().CheckForAutoHearingProgramStartOverlapping(), "Auto Hearing Program Start text and Enabled text are overlapping");
            ReportHelper.LogTest(Status.Pass, "Auto Hearing Program Start text and Enabled text are not overlapping");

            new ProgramDetailSettingsControlPage().SetAutoStart();

            Assert.IsTrue(new ProgramAutomationPage().GetIsAutomationSwitchChecked());
            ReportHelper.LogTest(Status.Pass, "Auto Hearing Program Start is Enabled");

            new ProgramAutomationPage().Proceed();

            new ProgramDetailPage().OpenProgramSettings();

            //Try to Create hearing Program
            var myFavName = "Favorite 01";
            new ProgramDetailSettingsControlPage().CreateFavorite();
            programNamePage = new ProgramNamePage();
            programNamePage.EnterName(myFavName);
            ReportHelper.LogTest(Status.Info, "Program name " + myFavName + " entered");

            // Verify UI
            Assert.IsNotEmpty(programNamePage.GetNavigationBarTitle());
            ReportHelper.LogTest(Status.Pass, "Customize name title displays as expected");
            new ProgramNamePage().Proceed();
            ReportHelper.LogTest(Status.Info, "Next button clicked");

            // Verify UI
            Assert.IsNotEmpty(new ProgramIconPage().GetNavigationBarTitle());
            ReportHelper.LogTest(Status.Pass, "Customize the icon title displays as expected");
            new ProgramIconPage().Proceed();
            ReportHelper.LogTest(Status.Info, "Next button clicked");

            // Verify UI
            Assert.IsNotEmpty(new ProgramAutomationPage().GetNavigationBarTitle());
            ReportHelper.LogTest(Status.Info, "Auto hearing program start title displays as expected");

            // Verify UI
            Assert.IsNotEmpty(new ProgramAutomationPage().GetProceedButtonText());
            ReportHelper.LogTest(Status.Info, "Save Favorite button displays as expected");
            Thread.Sleep(200);

            // Verify UI
            Assert.IsNotEmpty(new ProgramAutomationPage().GetCancelButtonText());
            ReportHelper.LogTest(Status.Info, "Cancel button displays as expected");

            new NavigationBar().TapBack();

            // Verify UI
            Thread.Sleep(300);
            Assert.IsNotEmpty(new ProgramIconPage().GetNavigationBarTitle());
            ReportHelper.LogTest(Status.Info, "Customize the icon title displays as expected");

            Assert.IsNotEmpty(new ProgramIconPage().GetProceedButtonText());
            ReportHelper.LogTest(Status.Info, "Next button displays as expected in Customize the Icon page");

            Assert.IsNotEmpty(new ProgramIconPage().GetCancelButtonText());
            ReportHelper.LogTest(Status.Info, "Cancel button displays as expected in Customize the Icon");

            new NavigationBar().TapBack();

            Assert.IsNotEmpty(new ProgramNamePage().GetProceedButtonText());
            ReportHelper.LogTest(Status.Info, "Next button displays as expected in Customize Name page");

            Assert.IsNotEmpty(new ProgramNamePage().GetCancelButtonText());
            ReportHelper.LogTest(Status.Info, "Cancel button displays as expected in Customize Name Page");

            AppManager.DeviceSettings.DisableWifi();
        }

        #endregion Sprint 15

        #region Sprint 16

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-15020_Table-32")]
        public void ST15020_CheckTinnitusOnlyTileParameter()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            // Open the program details view of the music program
            Wait.UntilTrue(() => dashboardPage.GetIsLeftHearingDeviceVisible(), TimeSpan.FromSeconds(3));
            var currentProgramName = dashboardPage.GetCurrentProgramName();
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            ReportHelper.LogTest(Status.Pass, "Navigated successfully to " + currentProgramName + " page");

            // Verify UI
            new ProgramDetailPage().ProgramDetailPageUiCheck();

            // Check the tile with the title "Tinnitus".
            new ProgramDetailPage().TinnitusDisplay.GetTitle();

            // Click on the tinnitus tile.
            string tinnitusStatus = new ProgramDetailPage().TinnitusDisplay.GetValue();
            new ProgramDetailPage().TinnitusDisplay.OpenSettings();
            Assert.True(new ProgramDetailParamEditTinnitusPage().GetIsTinnitusSwitchChecked());

            // Get Slider Value
            double volSliderValue = new ProgramDetailParamEditTinnitusPage().GetVolumeSliderValue();
            double equalizerSliderValueLow = new ProgramDetailParamEditTinnitusPage().GetEqualizerSliderValue(EqBand.Low);
            double equalizerSliderValueMedium = new ProgramDetailParamEditTinnitusPage().GetEqualizerSliderValue(EqBand.Mid);
            double equalizerSliderValueHigh = new ProgramDetailParamEditTinnitusPage().GetEqualizerSliderValue(EqBand.High);

            // Turn off noise.
            new ProgramDetailParamEditTinnitusPage().TurnOffTinnitus();
            Assert.False(new ProgramDetailParamEditTinnitusPage().GetIsTinnitusSwitchChecked());

            // Verify the sliders are not adjustible and with same value.
            Assert.AreEqual(volSliderValue, new ProgramDetailParamEditTinnitusPage().GetVolumeSliderValue());
            Assert.AreEqual(equalizerSliderValueLow, new ProgramDetailParamEditTinnitusPage().GetEqualizerSliderValue(EqBand.Low));
            Assert.AreEqual(equalizerSliderValueMedium, new ProgramDetailParamEditTinnitusPage().GetEqualizerSliderValue(EqBand.Mid));
            Assert.AreEqual(equalizerSliderValueHigh, new ProgramDetailParamEditTinnitusPage().GetEqualizerSliderValue(EqBand.High));

            ReportHelper.LogTest(Status.Pass, "Slider Values Verified");

            // Click on "Close".
            new ProgramDetailParamEditTinnitusPage().Close();

            // Verify Tinnitus status is changed
            Assert.AreNotEqual(tinnitusStatus, new ProgramDetailPage().TinnitusDisplay.GetValue());

            // Adjust the sliders
            string tinnitusStatustwo = new ProgramDetailPage().TinnitusDisplay.GetValue();
            new ProgramDetailPage().TinnitusDisplay.OpenSettings();
            new ProgramDetailParamEditTinnitusPage().TurnOnTinnitus();
            new ProgramDetailParamEditTinnitusPage().SetVolumeSliderValue(0.9);
            new ProgramDetailParamEditTinnitusPage().SetEqualizerSliderValue(EqBand.Low, 0.9);
            new ProgramDetailParamEditTinnitusPage().Close();
            Assert.AreNotEqual(tinnitusStatustwo, new ProgramDetailPage().TinnitusDisplay.GetValue());

            // Restart the App
            AppManager.RestartApp(true);
            ReportHelper.LogTest(Status.Info, "App Restarted");

            dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            Wait.UntilTrue(() => dashboardPage.GetIsLeftHearingDeviceVisible(), TimeSpan.FromSeconds(3));
            var programName = dashboardPage.GetCurrentProgramName();
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            ReportHelper.LogTest(Status.Pass, "Navigated successfully to " + programName + " page");
            new ProgramDetailPage().TinnitusDisplay.GetTitle();

            new ProgramDetailPage().TinnitusDisplay.OpenSettings();
            Assert.True(new ProgramDetailParamEditTinnitusPage().GetIsTinnitusSwitchChecked());
            new ProgramDetailParamEditTinnitusPage().Close();

            // Open Tinnitus Page
            string tinnitusStatusthree = new ProgramDetailPage().TinnitusDisplay.GetValue();
            new ProgramDetailPage().OpenProgramSettings();
            Assert.True(new ProgramDetailSettingsControlPage().GetIsCreateFavoriteFloatingButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "Program Settings page opened successfully");

            // Create hearing Program
            var myFavName = "Favorite 01";
            new ProgramDetailSettingsControlPage().CreateFavorite();
            var programNamePage = new ProgramNamePage();
            programNamePage.EnterName(myFavName);
            ReportHelper.LogTest(Status.Info, "Program name entered");

            // Verify UI
            Assert.IsNotEmpty(programNamePage.GetNavigationBarTitle());
            ReportHelper.LogTest(Status.Info, "Customize name title displays as expected");
            new ProgramNamePage().Proceed();
            ReportHelper.LogTest(Status.Info, "Next button clicked");

            Assert.IsNotEmpty(new ProgramIconPage().GetNavigationBarTitle());
            ReportHelper.LogTest(Status.Info, "Customize the icon title displays as expected");
            new ProgramIconPage().Proceed();
            ReportHelper.LogTest(Status.Info, "Next button clicked");

            Assert.IsNotEmpty(new ProgramAutomationPage().GetNavigationBarTitle());
            ReportHelper.LogTest(Status.Info, "Auto hearing program start title displays as expected");

            Assert.IsNotEmpty(new ProgramAutomationPage().GetProceedButtonText());
            new ProgramAutomationPage().Proceed();
            ReportHelper.LogTest(Status.Info, "Save Favorite button clicked successfully");
            Thread.Sleep(200);

            Assert.AreEqual(myFavName, new ProgramDetailPage().GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "Favorite name " + myFavName + " verified");
            Assert.AreEqual(tinnitusStatusthree, new ProgramDetailPage().TinnitusDisplay.GetValue());
            string tinnitusStatusFour = new ProgramDetailPage().TinnitusDisplay.GetValue();

            new ProgramDetailPage().TinnitusDisplay.OpenSettings();

            // Check Noise is On and sliders are in center position
            Assert.True(new ProgramDetailParamEditTinnitusPage().GetIsTinnitusSwitchChecked());
            Assert.AreEqual(0.5, new ProgramDetailParamEditTinnitusPage().GetEqualizerSliderValue(EqBand.Low));
            Assert.AreEqual(0.5, new ProgramDetailParamEditTinnitusPage().GetEqualizerSliderValue(EqBand.Mid));
            Assert.AreEqual(0.5, new ProgramDetailParamEditTinnitusPage().GetEqualizerSliderValue(EqBand.High));

            new ProgramDetailParamEditTinnitusPage().SetVolumeSliderValue(0.7);
            new ProgramDetailParamEditTinnitusPage().SetEqualizerSliderValue(EqBand.Low, 0.4);
            new ProgramDetailParamEditTinnitusPage().TurnOffTinnitus();
            new ProgramDetailParamEditTinnitusPage().Close();
            Assert.AreNotEqual(tinnitusStatusFour, new ProgramDetailPage().TinnitusDisplay.GetValue());

            new ProgramDetailPage().NavigateBack();

            // Open Tinnitus Page
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 2);
            ReportHelper.LogTest(Status.Pass, "Navigated successfully to " + currentProgramName + " page");
            new ProgramDetailPage().TinnitusOnlyDisplay.OpenSettings();

            new ProgramDetailParamEditTinnitusPage().TurnOffTinnitus();
            new ProgramDetailParamEditTinnitusPage().SetVolumeSliderValue(0.7);
            new ProgramDetailParamEditTinnitusPage().SetEqualizerSliderValue(EqBand.Low, 0.4);
            Assert.AreEqual(0.7, Math.Round(new ProgramDetailParamEditTinnitusPage().GetVolumeSliderValue(), 1));
            Assert.AreEqual(0.4, Math.Round(new ProgramDetailParamEditTinnitusPage().GetEqualizerSliderValue(EqBand.Low), 1));
            new ProgramDetailParamEditTinnitusPage().Close();

            // Restart the application and navigate back to the Tinnitus program detail view.
            AppManager.RestartApp(true);
            ReportHelper.LogTest(Status.Info, "App Restarted");

            dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            Wait.UntilTrue(() => dashboardPage.GetIsLeftHearingDeviceVisible(), TimeSpan.FromSeconds(3));
            var newProgramName = dashboardPage.GetCurrentProgramName();
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 2);
            ReportHelper.LogTest(Status.Pass, "Navigated successfully to " + newProgramName + " page");

            // Create a favorite from the tinnitus listening program.
            new ProgramDetailPage().OpenProgramSettings();
            Assert.True(new ProgramDetailSettingsControlPage().GetIsCreateFavoriteFloatingButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "Program Settings page opened successfully");

            // Create hearing Program
            var myNewFavName = "Favorite 02";
            new ProgramDetailSettingsControlPage().CreateFavorite();
            programNamePage = new ProgramNamePage();
            programNamePage.EnterName(myNewFavName);
            ReportHelper.LogTest(Status.Info, "Program name entered");

            Assert.IsNotEmpty(programNamePage.GetNavigationBarTitle());
            ReportHelper.LogTest(Status.Info, "Customize name title displays as expected");
            new ProgramNamePage().Proceed();
            ReportHelper.LogTest(Status.Info, "Next button clicked");

            Assert.IsNotEmpty(new ProgramIconPage().GetNavigationBarTitle());
            ReportHelper.LogTest(Status.Info, "Customize the icon title displays as expected");
            new ProgramIconPage().Proceed();
            ReportHelper.LogTest(Status.Info, "Next button clicked");

            Assert.IsNotEmpty(new ProgramAutomationPage().GetNavigationBarTitle());
            ReportHelper.LogTest(Status.Info, "Auto hearing program start title displays as expected");

            Assert.IsNotEmpty(new ProgramAutomationPage().GetProceedButtonText());
            new ProgramAutomationPage().Proceed();
            ReportHelper.LogTest(Status.Info, "Save Favorite button clicked successfully");
            Thread.Sleep(200);

            Assert.AreEqual(myNewFavName, new ProgramDetailPage().GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "Favorite name " + myNewFavName + " verified");

            new ProgramDetailPage().TinnitusOnlyDisplay.OpenSettings();

            // Check Noise is On and sliders are in center position
            Assert.True(new ProgramDetailParamEditTinnitusPage().GetIsTinnitusSwitchChecked());
            Assert.AreEqual(0.5, new ProgramDetailParamEditTinnitusPage().GetEqualizerSliderValue(EqBand.Low));
            Assert.AreEqual(0.5, new ProgramDetailParamEditTinnitusPage().GetEqualizerSliderValue(EqBand.Mid));
            Assert.AreEqual(0.5, new ProgramDetailParamEditTinnitusPage().GetEqualizerSliderValue(EqBand.High));

            new ProgramDetailParamEditTinnitusPage().SetVolumeSliderValue(0.7);
            new ProgramDetailParamEditTinnitusPage().SetEqualizerSliderValue(EqBand.Low, 0.4);
            new ProgramDetailParamEditTinnitusPage().Close();
        }

        #endregion Sprint 16

        #region Sprint 17

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-12783_Table-37")]
        public void ST12783_AppLaunchScreenFormat()
        {
            ReportHelper.LogTest(Status.Info, "App started in Demo Mode");

            double thresholdValue = 0.999; //threadshold value for image comparision score

            /*
             Screen orientation change should not be allowed and an InvalidElementStateException should be thrown.
             So handling this exception while trying to change screen orientation. 
            */
            ReportHelper.LogTest(Status.Info, "Change Screen Orientation and handle InvalidStaleElementException");

            // Change Screen Orientation to Portrait
            try
            {
                AppManager.App.Driver.Orientation = OpenQA.Selenium.ScreenOrientation.Portrait;
            }
            catch (OpenQA.Selenium.InvalidElementStateException)
            {
                ReportHelper.LogTest(Status.Pass, "'InvalidElementStateException thrown as Expected");
            }

            // Capture the screen shot of welcome page in portrait mode
            var welcomePageScreenShot = AppManager.App.TakeScreenshot().AsBase64EncodedString;
            ReportHelper.LogTest(Status.Info, "Welcome screen shots taken in Potrait Mode");

            // Navigate throught to intro and start the Demo mode
            new IntroPageOne().MoveRightBySwiping();
            new IntroPageTwo().MoveRightBySwiping();
            new IntroPageThree().MoveRightBySwiping();
            new IntroPageFour().MoveRightBySwiping();
            ReportHelper.LogTest(Status.Info, "Into Pages swiped in Potrait Mode");

            // Verify the controls
            Assert.IsTrue(new IntroPageFive().GetIsLeftButtonVisible());
            Assert.IsTrue(new IntroPageFive().GetIsLeftButtonVisible());

            ReportHelper.LogTest(Status.Pass, "'Here we go' page is displayed correctly.");

            new IntroPageFive().Continue();
            ReportHelper.LogTest(Status.Info, "Tap 'Continue' accept all dialogs and check is 'Start search' page is visible.");

            var initializeHardwarePage = new InitializeHardwarePage();
            Assert.IsNotEmpty(initializeHardwarePage.GetDemoModeText());
            Assert.IsNotEmpty(initializeHardwarePage.GetScanText());
            ReportHelper.LogTest(Status.Pass, "'Start Search' page is displayed correctly having 'Start Search' and 'Demo Mode' option. Now tap 'Start Search'");

            initializeHardwarePage.StartDemoMode();

            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(45));
            PermissionHelper.AllowPermissionIfRequested();

            Assert.IsTrue(new DashboardPage().IsCurrentlyShown());
            ReportHelper.LogTest(Status.Pass, "Dashboard page loaded in Potrait Mode");

            // Capture the screen shot of dashboard page in portrait mode
            var dashBoardPageScreenShot = AppManager.App.TakeScreenshot().AsBase64EncodedString;
            ReportHelper.LogTest(Status.Info, "Dashboard screen shots taken in Potrait Mode");

            // Delete App data and relaunch the app
            AppManager.RestartApp(true);
            ReportHelper.LogTest(Status.Info, "App restarted");

            Assert.IsTrue(new IntroPageOne().IsCurrentlyShown());

            // Change Screen Orientation to Landscape
            try
            {
                AppManager.App.Driver.Orientation = OpenQA.Selenium.ScreenOrientation.Landscape;
            }
            catch (OpenQA.Selenium.InvalidElementStateException)
            {
                ReportHelper.LogTest(Status.Pass, "'Exception thrown as Expected");
            }

            Thread.Sleep(300);

            // Take screen shot of Welcome page in Landscape Mode
            var welcomePageScreenShotRestart = AppManager.App.TakeScreenshot().AsBase64EncodedString;
            ReportHelper.LogTest(Status.Info, "Welcome screen shots taken in Landscape Mode");

            // Comparing Welcome page in Portrait and Landscape modes
            // ToDo: Need to install OpenCV and the uncomment the below two lines and need test this
            //double welcomePageScore = ImageComparison.GetImageSimilarityScore(welcomePageScreenShot, welcomePageScreenShotRestart);
            //Assert.LessOrEqual(welcomePageScore, thresholdValue, "There are visual differences in signal strength.");
            ReportHelper.LogTest(Status.Pass, "No visual differences in signal strength");

            // Navigate throught to intro and start the Demo mode
            new IntroPageOne().MoveRightBySwiping();
            new IntroPageTwo().MoveRightBySwiping();
            new IntroPageThree().MoveRightBySwiping();
            new IntroPageFour().MoveRightBySwiping();
            ReportHelper.LogTest(Status.Info, "Into Pages swiped in Landscape Mode");

            // Verify the controls
            Assert.IsTrue(new IntroPageFive().GetIsLeftButtonVisible());
            Assert.IsTrue(new IntroPageFive().GetIsLeftButtonVisible());

            ReportHelper.LogTest(Status.Pass, "'Here we go' page is displayed correctly.");

            new IntroPageFive().Continue();
            ReportHelper.LogTest(Status.Info, "Tap 'Continue' accept all dialogs and check is 'Start search' page is visible.");

            initializeHardwarePage = new InitializeHardwarePage();
            Assert.IsNotEmpty(initializeHardwarePage.GetDemoModeText());
            Assert.IsNotEmpty(initializeHardwarePage.GetScanText());
            ReportHelper.LogTest(Status.Pass, "'Start Search' page is displayed correctly having 'Start Search' and 'Demo Mode' option. Now tap 'Start Search'");

            initializeHardwarePage.StartDemoMode();

            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(45));
            PermissionHelper.AllowPermissionIfRequested();

            ReportHelper.LogTest(Status.Info, "App Re-started in Demo Mode");

            Assert.IsTrue(new DashboardPage().IsCurrentlyShown());
            ReportHelper.LogTest(Status.Pass, "Dashboard page loaded in Landscape Mode");

            // Capture the screen shot of dashboard page in Landscape Mode
            var dashBoardPageScreenShotRestart = AppManager.App.TakeScreenshot().AsBase64EncodedString;
            ReportHelper.LogTest(Status.Info, "Dashboard screen shots taken in Landscape Mode");

            // Comparing Dashboard page in Portraid and Landscape modes
            // ToDo: Need to install OpenCV and the uncomment the below two lines and need test this
            //double dashboardPageScore = ImageComparison.GetImageSimilarityScore(dashBoardPageScreenShot, dashBoardPageScreenShotRestart);
            //Assert.GreaterOrEqual(dashboardPageScore, thresholdValue, "There are visual differences in Dashboard.");
            ReportHelper.LogTest(Status.Pass, "There are no visual differences for portrait and landscape modes");
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-15639_Table-20")]
        public void ST15639_MoveLegalInformations()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");
            // Wait till the toast message disappears. It causes the test case to fail if it does not wait.
            Thread.Sleep(3000);

            foreach (string stringLanguage in Enum.GetNames(typeof(Language_Audifon)))
            {
                Language_Audifon enumLanguage = (Language_Audifon)Enum.Parse(typeof(Language_Audifon), stringLanguage);
                string lang = enumLanguage.ToString();

                // ToDo: Not to check for All languages and test for laguages suggested to be checked by Audifon. Currently we are checking for German. This has to be changed to check for all languahes
                if (lang.Equals("English") || lang.Equals("German"))
                {
                    // Open Languages Page
                    new DashboardPage().OpenMenuUsingTap();
                    new MainMenuPage().OpenSettings();
                    new SettingsMenuPage().OpenLanguage();

                    // Set the current language from the Language_Audifon and iterate through each langiage
                    new SettingLanguagePage().SelectLanguageAudifon(enumLanguage);
                    new SettingLanguagePage().Accept();
                    new AppDialog().Confirm();

                    // Checking for Languages other than English
                    if (lang.Equals("German"))
                    {
                        ReportHelper.LogTest(Status.Pass, "Language changed to " + enumLanguage.ToString());

                        //Open Mainmenu and Verify
                        new DashboardPage().OpenMenuUsingTap();
                        Assert.IsNotEmpty(new MainMenuPage().GetHelpText());
                        ReportHelper.LogTest(Status.Pass, "The main menu is opened.");

                        //Open Help and Verify
                        new MainMenuPage().OpenHelp();
                        Assert.AreNotEqual("Information", new HelpMenuPage().GetInformationMenuText());
                        Assert.AreNotEqual("Imprint", new HelpMenuPage().GetImprintText());
                        ReportHelper.LogTest(Status.Pass, "Help menu is verified.");

                        //Open Imprint and Verify
                        new HelpMenuPage().OpenImprint();
                        Assert.AreNotEqual("Please contact your hearing care professional.", new ImprintPage().GetSupportDescription());
                        ReportHelper.LogTest(Status.Pass, "Imprint menu is verified.");
                        new ImprintPage().TapBack();

                        //Open Information and Verify
                        new HelpMenuPage().OpenInformationMenu();
                        Assert.AreNotEqual("Data protection", new InformationMenuPage().GetPrivacyPolicyText());
                        Assert.AreNotEqual("Terms of use", new InformationMenuPage().GetTermsOfUseText());
                        Assert.AreNotEqual("Licenses", new InformationMenuPage().GetLicensesText());
                        ReportHelper.LogTest(Status.Pass, "Information menu is verified.");

                        //Open Data Protection and Verify
                        new InformationMenuPage().OpenPrivacyPolicy();
                        Thread.Sleep(500);
                        Assert.AreNotEqual("Data protection", new PrivacyPolicyPage().GetNavigationBarTitle());
                        ReportHelper.LogTest(Status.Pass, "Data Protection menu is verified.");
                        new PrivacyPolicyPage().TapBack();

                        //Open Terms of use menu and Verify
                        new InformationMenuPage().OpenTermsofUse();
                        Thread.Sleep(500);
                        Assert.AreNotEqual("Terms of use", new TermsOfUsePage().GetNavigationBarTitle());
                        ReportHelper.LogTest(Status.Pass, "Terms of use menu is verified.");
                        new TermsOfUsePage().TapBack();

                        //open Licenses and Verify
                        new InformationMenuPage().OpenLicenses();
                        Thread.Sleep(500);
                        Assert.AreNotEqual("Licenses", new LicencesPage().GetNavigationBarTitle());
                        ReportHelper.LogTest(Status.Pass, "Licenses menu is verified.");
                        new LicencesPage().TapBack();

                        //Navigate back to Help menu page
                        new InformationMenuPage().TapBack();

                        //Navigate back to Main menu page
                        new HelpMenuPage().TapBack();

                        //Navigate to Dashboard
                        new MainMenuPage().CloseMenuUsingTap();
                    }

                    // Checking for English
                    if (lang.Equals("English"))
                    {
                        ReportHelper.LogTest(Status.Pass, "Language changed to " + enumLanguage.ToString());

                        //Open Mainmenu and Verify
                        new DashboardPage().OpenMenuUsingTap();
                        Assert.IsNotEmpty(new MainMenuPage().GetHelpText());
                        ReportHelper.LogTest(Status.Pass, "The main menu is verified.");

                        //Open Help and Verify
                        new MainMenuPage().OpenHelp();
                        Assert.AreEqual("Information", new HelpMenuPage().GetInformationMenuText());
                        Assert.AreEqual("Imprint", new HelpMenuPage().GetImprintText());
                        ReportHelper.LogTest(Status.Pass, "Help menu is verified.");

                        //Open Imprint and Verify
                        new HelpMenuPage().OpenImprint();
                        if (AppManager.Brand == Brand.Audifon)
                            Assert.AreEqual("Please contact your hearing care professional.", new ImprintPage().GetSupportDescription());
                        ReportHelper.LogTest(Status.Pass, "Imprint menu is verified.");
                        Assert.IsTrue(new ImprintPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
                        new ImprintPage().TapBack();

                        //Open Information and Verify
                        new HelpMenuPage().OpenInformationMenu();
                        Assert.AreEqual("Data protection", new InformationMenuPage().GetPrivacyPolicyText());
                        Assert.AreEqual("Terms of use", new InformationMenuPage().GetTermsOfUseText());
                        Assert.AreEqual("Licenses", new InformationMenuPage().GetLicensesText());
                        ReportHelper.LogTest(Status.Pass, "Information menu is verified.");

                        //Open Data Protection and Verify
                        new InformationMenuPage().OpenPrivacyPolicy();
                        Thread.Sleep(500);
                        Assert.AreEqual("Data protection", new PrivacyPolicyPage().GetNavigationBarTitle());
                        ReportHelper.LogTest(Status.Pass, "Data Protection menu is verified.");
                        new PrivacyPolicyPage().TapBack();

                        //Open Terms of use menu and Verify
                        new InformationMenuPage().OpenTermsofUse();
                        Thread.Sleep(500);
                        Assert.AreEqual("Terms of use", new TermsOfUsePage().GetNavigationBarTitle());
                        ReportHelper.LogTest(Status.Pass, "Terms of use menu is verified.");
                        new TermsOfUsePage().TapBack();

                        //Open Licenses and Verify
                        new InformationMenuPage().OpenLicenses();
                        Thread.Sleep(500);
                        Assert.AreEqual("Licenses", new LicencesPage().GetNavigationBarTitle());
                        ReportHelper.LogTest(Status.Pass, "Licenses menu is verified.");
                        new LicencesPage().TapBack();

                        //Navigate back to Help Menu page
                        new InformationMenuPage().TapBack();

                        //Navigate back to Main menu page
                        new HelpMenuPage().TapBack();

                        //Naviagate bak to Dashboard
                        new MainMenuPage().CloseMenuUsingTap();
                    }
                }
            }
        }

        #endregion Sprint 17

        #region Sprint 20

        [Test]
        //[Ignore("It is not possible to automate change in OS language in iOS.")]
        [Category("SystemTestsDemo")]
        [Description("TC-17928_Table-0")]
        public void ST17928_ChangeRussianLanguageMenu()
        {
            Language_Device language_Device = Language_Device.Russian_Russia;
            Language_Audifon expectedAppLanguage = Language_Audifon.Russian;

            // Changing Mobile Language
            AppManager.DeviceSettings.ChangeDeviceLanguage(language_Device);
            ReportHelper.LogTest(Status.Info, "Change device langauge to " + language_Device + " and restart app.");

            // Reset App after language change
            AppManager.RestartApp(true);
            ReportHelper.LogTest(Status.Info, "App Restarted");
            Assert.IsTrue(new IntroPageOne().IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)));

            // Checking Welcome text
            string welocmePageTitle = new IntroPageOne().GetTitle();
            Assert.AreNotEqual(string.Empty, welocmePageTitle, "Welcome page is not displayed correctly.");
            ReportHelper.LogTest(Status.Pass, "Welcome page text shown correctly.");

            // Navigate throught to intro and start the Demo mode
            new IntroPageOne().MoveRightBySwiping();
            new IntroPageTwo().MoveRightBySwiping();
            new IntroPageThree().MoveRightBySwiping();
            new IntroPageFour().MoveRightBySwiping();
            Assert.IsTrue(new IntroPageFive().GetIsLeftButtonVisible());
            new IntroPageFive().Continue();

            Assert.IsTrue(new InitializeHardwarePage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Pass, "Verified Into Pages");

            // Start App in Demo Mode
            new InitializeHardwarePage().StartDemoMode();
            ReportHelper.LogTest(Status.Info, "App started in Demo Mode");

            Assert.IsTrue(DialogHelper.GetIsDialogDisplayed(TimeSpan.FromSeconds(40)));
            DialogHelper.Confirm();
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
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            dashboardPage.OpenMenuUsingTap();
            MainMenuPage mainMenuPage = new MainMenuPage();
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText());
            Assert.IsNotEmpty(mainMenuPage.GetProgramsText());
            Assert.IsNotEmpty(mainMenuPage.GetHelpText());
            ReportHelper.LogTest(Status.Pass, "The main menu is opened successfully.");

            // Checking Settings Menu
            ReportHelper.LogTest(Status.Info, "Open the 'Settings' menu item from Main Menu.");
            mainMenuPage.OpenSettings();
            SettingsMenuPage settingsMenuPage = new SettingsMenuPage();
            Assert.IsNotEmpty(settingsMenuPage.GetMyHearingAidsText());
            Assert.IsNotEmpty(settingsMenuPage.GetPermissionsText());
            Assert.IsNotEmpty(settingsMenuPage.GetLanguageText());
            Assert.IsNotEmpty(settingsMenuPage.GetDemoModeText());
            ReportHelper.LogTest(Status.Pass, "The Settings menu is opened all items are visible successfully.");

            // Back to Main Menu
            ReportHelper.LogTest(Status.Info, "Navigate back to Main Menu.");
            settingsMenuPage.NavigateBack();

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

            // Back to Main Menu
            ReportHelper.LogTest(Status.Info, "Navigate back to Main Menu.");
            helpMenuPage.NavigateBack();

            // Open Language
            mainMenuPage.OpenSettings();
            SettingsMenuPage settingsMenuPageLanguage = new SettingsMenuPage();
            settingsMenuPageLanguage.OpenLanguage();

            Assert.IsTrue(new SettingLanguagePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            SettingLanguagePage settingLanguagePage = new SettingLanguagePage();
            Assert.AreEqual(expectedAppLanguage, settingLanguagePage.GetCurrentLanguageAudifon());
            ReportHelper.LogTest(Status.Info, "Language Menu opened");

            // Change the Language to English
            settingLanguagePage.SelectLanguageAudifon(Language_Audifon.English);
            ReportHelper.LogTest(Status.Info, "Select English Language");

            settingLanguagePage.Accept();
            ReportHelper.LogTest(Status.Info, "Accept button clicked");

            new AppDialog().Deny();
            ReportHelper.LogTest(Status.Info, "No clicked in App Dialog");

            // Reset the Mobile language to English 
            AppManager.DeviceSettings.ChangeDeviceLanguage(Language_Device.English_US);
            ReportHelper.LogTest(Status.Info, "Change device langauge to English");
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-17959_Table-0")]
        public void ST17959_CheckVersionBuildNumber()
        {
            // Expected Data
            // ToDo: The test case needs to be tested in older version. The version in which App has to be tested can be changed here
            string AppVersion = "Version: 1.4.2";

            // ToDo: The build number varies from Mobile to Mobile. The Logic for Build Number specified in the test case does not match the Build Number in the App 
            //string AppBuildNumber = "Build number: " + DateTime.Now.Year.ToString().Substring(2, 2) + DateTime.Now.DayOfYear.ToString() + "301";
            string AppBuildNumber = "Build number: 22083501";

            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            // Load Dashboard Page
            DashboardPage dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            // Load Menu Page
            dashboardPage.OpenMenuUsingTap();
            MainMenuPage mainMenuPage = new MainMenuPage();

            // Load Help Page
            mainMenuPage.OpenHelp();
            HelpMenuPage helpMenuPage = new HelpMenuPage();

            // Load Imprint Page
            helpMenuPage.OpenImprint();
            ReportHelper.LogTest(Status.Info, "Imprint page opened");

            ImprintPage imprintPage = new ImprintPage();

            // Verify Version
            string ActualAppVersion = imprintPage.GetVersion();
            // ToDo: App version changes from build to build. So cannot change it in test case. Can only check if it is empty
            //Assert.AreEqual(AppVersion, ActualAppVersion, "App Version Mismatch.");
            Assert.IsNotEmpty(ActualAppVersion);
            ReportHelper.LogTest(Status.Pass, "App Version verified");

            // Verify Build Number
            string ActualAppBuildNumber = imprintPage.GetBuildNumber();
            // ToDo: App build number changes from build to build. So cannot change it in test case. Can only check if it is empty
            //Assert.AreEqual(AppBuildNumber, ActualAppBuildNumber, "App Build Number Mismatch.");
            Assert.IsNotEmpty(ActualAppBuildNumber);
            ReportHelper.LogTest(Status.Pass, "App Build Number verified");

            // Verify other details
            Assert.IsNotEmpty(imprintPage.GetAddressHeader());
            Assert.IsNotEmpty(imprintPage.GetAppCompanyName());
            Assert.IsNotEmpty(imprintPage.GetAppCompanyStreet());
            Assert.IsNotEmpty(imprintPage.GetAppCompanyPostalCodeCity());
            Assert.IsNotEmpty(imprintPage.GetAppCommpanyState());
            ReportHelper.LogTest(Status.Pass, "Contact info is displayed correctly on Imprint page");

            imprintPage.NavigateBack();

            new HelpMenuPage().NavigateBack();

            new MainMenuPage().CloseMenuUsingTap();

            // ToDo: Need to loop all Languages. Currently we support for German so we have added only German
            {
                Language_Audifon expectedAppLanguage = Language_Audifon.German;

                new DashboardPage().OpenMenuUsingTap();

                new MainMenuPage().OpenSettings();

                new SettingsMenuPage().OpenLanguage();

                Assert.IsTrue(new SettingLanguagePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
                ReportHelper.LogTest(Status.Info, "Language Menu opened");

                // Change the Language to Current Language
                new SettingLanguagePage().SelectLanguageAudifon(expectedAppLanguage);
                ReportHelper.LogTest(Status.Info, "Select " + expectedAppLanguage + " Language");

                new SettingLanguagePage().Accept();

                new AppDialog().Confirm();

                new DashboardPage().OpenMenuUsingTap();

                new MainMenuPage().OpenHelp();

                new HelpMenuPage().OpenImprint();
                ReportHelper.LogTest(Status.Info, "Imprint page opened in " + expectedAppLanguage + " language");

                // Verify Ipmrint details
                Assert.IsNotEmpty(new ImprintPage().GetAddressHeader());
                Assert.IsNotEmpty(new ImprintPage().GetAppCompanyName());
                Assert.IsNotEmpty(new ImprintPage().GetAppCompanyStreet());
                Assert.IsNotEmpty(new ImprintPage().GetAppCompanyPostalCodeCity());
                Assert.IsNotEmpty(new ImprintPage().GetAppCommpanyState());
                ReportHelper.LogTest(Status.Pass, "Contact info is displayed correctly on Imprint page in " + expectedAppLanguage + " language");

                new ImprintPage().NavigateBack();

                new HelpMenuPage().NavigateBack();

                new MainMenuPage().CloseMenuUsingTap();
            }
        }

        #endregion Sprint 20

        #region Sprint 21

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-19053_Table-0")]
        public void ST19053_VerifySendMailFunctionality()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            ReportHelper.LogTest(Status.Info, "Navigate to setting menu");
            var settingsMenuPage = NavigationHelper.NavigateToSettingsMenu(dashboardPage);

            ReportHelper.LogTest(Status.Info, "Show developement stuff");
            settingsMenuPage.ShowDevelopmentStuff();
            Assert.IsTrue(settingsMenuPage.GetIsDevelopmentStuffVisible());
            Assert.IsNotEmpty(settingsMenuPage.GetAppResetText());
            Assert.IsNotEmpty(settingsMenuPage.GetConnectionErrorPageText());
            Assert.IsNotEmpty(settingsMenuPage.GetHardwareErrorPageText());
            Assert.IsNotEmpty(settingsMenuPage.GetLogsText());
            ReportHelper.LogTest(Status.Info, "Developement stuff is visible successfully on setting page");

            ReportHelper.LogTest(Status.Info, "Open Logs menu");
            settingsMenuPage.OpenLogs();
            Assert.IsTrue(new LogPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(30)));
            Assert.IsTrue(new LogPage().GetIsClearLogButtonVisible());
            Assert.IsTrue(new LogPage().GetIsSendLogByEmailButtonVisible());
            ReportHelper.LogTest(Status.Info, "'Send Logs by Email' and 'Clear Logs' button are visible on Logs page");

            ReportHelper.LogTest(Status.Info, "Click on 'Send Mail' and verify the app does not crash");
            //new LogPage().SendLogByEmail();

            //ToDo: Verify the email client opened

            AppManager.DeviceSettings.DisableWifi();
        }

        #endregion Sprint 21

        #endregion Test Cases
    }
}