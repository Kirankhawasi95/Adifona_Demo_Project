using System;
using System.Collections.Generic;
using System.Linq;
using HorusUITest.Enums;
using HorusUITest.Helper;
using HorusUITest.PageObjects;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Dialogs;
using HorusUITest.PageObjects.Favorites;
using HorusUITest.PageObjects.Favorites.Automation;
using HorusUITest.PageObjects.Menu;
using HorusUITest.PageObjects.Menu.Programs;
using HorusUITest.PageObjects.Menu.Settings;
using HorusUITest.PageObjects.Start;
using NUnit.Framework;
using System.Threading;
using HorusUITest.PageObjects.Controls.ProgramDetailParams;
using AventStack.ExtentReports;

namespace HorusUITest.TestFixtures.TestCases
{
    public class SystemTestsDemo5 : BaseResettingTestFixture
    {
        #region Constructor

        public SystemTestsDemo5(Platform platform) : base(platform)
        {

        }

        #endregion Constructor

        #region Test Cases

        #region Sprint 6

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-10795_Table-67")]
        public void ST10795_LanguageTranslatabilityVerification()
        {
            var changedLanguage = Language_Audifon.Russian;

            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            dashboardPage.OpenMenuUsingTap();
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenLanguage();
            var settingLanguagePage = new SettingLanguagePage();

            #region Getting app Text in initial app language

            var initialLangauge = settingLanguagePage.GetCurrentLanguageAudifon();
            ReportHelper.LogTest(Status.Info, "App is preset to " + initialLangauge + " langauge.");

            try
            {
                if (initialLangauge.Equals(changedLanguage))
                    throw new Exception();
            }
            catch (Exception)
            {
                ReportHelper.LogTest(Status.Fail, "Preset Language : " + initialLangauge + " Changed Langauge : " + changedLanguage);
                throw;
            }

            settingLanguagePage.NavigateBack();
            new SettingsMenuPage().NavigateBack();
            new MainMenuPage().CloseMenuUsingTap();
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            var intialLanguageTextList = GetTextOfAllPages();
            ReportHelper.LogTest(Status.Info, "Got all the texts of '" + initialLangauge + "' language");

            #endregion Getting app Text in initial app language

            #region Getting app Text in after changing app language

            NavigationHelper.NavigateToSettingsMenu(new DashboardPage()).OpenLanguage();
            new SettingLanguagePage().SelectLanguageAudifon(changedLanguage).Accept();
            DialogHelper.ConfirmIfDisplayed();
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            ReportHelper.LogTest(Status.Info, "App language is changed to " + changedLanguage + " langauge.");
            var changedLanguageTextList = GetTextOfAllPages();
            ReportHelper.LogTest(Status.Info, "Got all the texts of '" + changedLanguage + "' language");
            var lengthOfList = changedLanguageTextList.Count();

            #endregion Getting app Text in after changing app language

            // Create favorite and check program automation
            var favoriteProgram = "Favorite 01";
            new DashboardPage().OpenCurrentProgram();
            var programAutomationPage = FavoriteHelper.CreateFavoriteHearingProgram(favoriteProgram, 5);
            Assert.IsTrue(programAutomationPage.GetIsGeofenceAutomationVisible());
            Assert.IsTrue(programAutomationPage.GetIsWifiAutomationVisible());
            ReportHelper.LogTest(Status.Info, "Program automation turned on, WIFI and Location option visible");

            // Enable Wifi
            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(10000);

            // Give "Always Allow" Location Permission before creating favorite based on Wifi. Latest Audifon build requires this permission to create favorite based on wifi
            AppManager.DeviceSettings.GrantGPSBackgroundPermission();

            programAutomationPage.TapConnectToWiFi();

            var connectedWifiName = new AutomationWifiBindingPage().GetWifiName();
            new AutomationWifiBindingPage().Ok();
            var actualWifiName = new ProgramAutomationPage().WifiAutomation.GetValue();
            Assert.AreEqual(connectedWifiName, actualWifiName);
            ReportHelper.LogTest(Status.Info, "Program automation is set to Wifi");
            new ProgramAutomationPage().Proceed();

            #region Verification of Texts

            var unchangedWords = new List<string>();
            for (int i = 0; i < lengthOfList; i++)
            {
                if (intialLanguageTextList[i].Count() == changedLanguageTextList[i].Count())
                {
                    for (int j = 0; j < changedLanguageTextList[i].Count(); j++)
                    {
                        if (intialLanguageTextList[i][j].Length >= 2 && Equals(intialLanguageTextList[i][j], changedLanguageTextList[i][j]))
                        {
                            unchangedWords.Add(intialLanguageTextList[i][j]);
                        }
                        continue;
                    }
                }
                continue;
            }
            ReportHelper.LogTest(Status.Pass, "Language texts verified");

            #endregion Verification of Texts

            //Check autostart of favorite program after enabling/disabling wifi does not work correctly
            //Hence is not Asserted due to a bug
            // Issue is reported and discussed with Audifon team
            new ProgramDetailPage().NavigateBack();
            new DashboardPage().SelectProgram(2); //Need to change as current program will be favorite always
            var currentProgramName = new DashboardPage().GetCurrentProgramName();
            Assert.AreNotEqual(favoriteProgram, currentProgramName);

            AppManager.DeviceSettings.DisableWifi();
            Thread.Sleep(5000); //For Synchronization

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(10000); //For Synchronization
            new DashboardPage().OpenCurrentProgram();

            //Assert.AreEqual(favoriteProgram, new DashboardPage().GetCurrentProgramName());

            //Create Favorites
            ReportHelper.LogTest(Status.Info, "Creating favorite with name 'Favorite 02'...");
            FavoriteHelper.CreateFavoriteHearingProgramWithoutCheck("Favorite 02", 1).Proceed();
            ReportHelper.LogTest(Status.Pass, "Favorite with name 'Favorite 02' created");
            ReportHelper.LogTest(Status.Info, "Creating favorite with name 'Favorite 03'...");
            FavoriteHelper.CreateFavoriteHearingProgramWithoutCheck("Favorite 03", 3).Proceed();
            ReportHelper.LogTest(Status.Pass, "Favorite with name 'Favorite 03' created");
            ReportHelper.LogTest(Status.Info, "Creating favorite with name 'Favorite 04'...");
            FavoriteHelper.CreateFavoriteHearingProgramWithoutCheck("Favorite 04", 4).Proceed();
            ReportHelper.LogTest(Status.Pass, "Favorite with name 'Favorite 04' created");

            //Check message after create favorite limit is reached
            var numberOfPrograms = new ProgramDetailPage().GetNumberOfVisibiblePrograms();
            new ProgramDetailPage().OpenProgramSettings();

            // ToDo: Need to Get the Snack Bar Text for IOS App
            if (OniOS)
                throw new NotImplementedException("Need to impliment functionality to get the Snack Bar Text for IOS App");
            string snackBarText = new ProgramDetailSettingsControlPage().GetSnackBarText();
            Assert.IsNotEmpty(snackBarText);
            ReportHelper.LogTest(Status.Pass, "Message appears when we try to create more than 4 favorites.");

            //Get delete favotite message
            new ProgramDetailSettingsControlPage().DeleteFavorite();
            var deleteFavoriteMessage = new AppDialog().GetMessage();
            Assert.IsNotEmpty(deleteFavoriteMessage);

            AppManager.DeviceSettings.RevokeGPSPermission();
            AppManager.StartApp(false);
            ReportHelper.LogTest(Status.Info, "Revoke GPS permission and started app again.");

            //Change language to verify text again
            //DialogHelper.DenyIfDisplayed(TimeSpan.FromSeconds(30));
            //new HardwareErrorPage().StartDemoMode();
            //DialogHelper.DenyIfDisplayed(TimeSpan.FromSeconds(30));

            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(20));

            //Setting the location to "change to precise"
            PermissionHelper.AllowPermissionIfRequested();
            new DashboardPage().WaitForToastToDisappear();
            Thread.Sleep(2000);
            NavigationHelper.NavigateToSettingsMenu(new DashboardPage()).OpenLanguage();

            Wait.UntilTrue(() => new SettingLanguagePage().GetIsAcceptButtonVisible(), TimeSpan.FromSeconds(20));

            new SettingLanguagePage().SelectLanguageAudifon(initialLangauge).Accept();
            DialogHelper.ConfirmIfDisplayed();
            ReportHelper.LogTest(Status.Info, "App language is changed to " + initialLangauge + " langauge.");
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 1);

            new ProgramDetailPage().OpenProgramSettings();
            Assert.AreNotEqual(snackBarText, new ProgramDetailSettingsControlPage().GetSnackBarText());
            ReportHelper.LogTest(Status.Pass, "Create favorite error mesaage is changed as per language.");

            //Delete Favorite
            new ProgramDetailSettingsControlPage().DeleteFavorite();
            Assert.AreNotEqual(deleteFavoriteMessage, new AppDialog().GetMessage());
            new AppDialog().Confirm();
            Assert.Less(new ProgramDetailPage().GetNumberOfVisibiblePrograms(), numberOfPrograms);

            //Revoking permisssion during runtime results in app getting closed abruptly hence skipped this
            //AppManager.DeviceSettings.RevokeGPSPermission();

            // 'Contact' and 'Find Store' are no longer present in the Main menu hence skipped the step
            var uniqueWords = unchangedWords.Distinct().ToList();
            uniqueWords.ForEach(word => ReportHelper.LogTest(Status.Info, $"{word}"));

            // Disable Wifi
            AppManager.DeviceSettings.DisableWifi();

            List<List<string>> GetTextOfAllPages()
            {
                List<List<string>> textOfAllPagesList = new List<List<string>>();

                //Open Hearing Aids Info
                new DashboardPage().OpenLeftHearingDevice();

                textOfAllPagesList.Add(new HearingInstrumentInfoControlPage().GetAllTextOfPage());
                Thread.Sleep(500);
                new HearingInstrumentInfoControlPage().Close();

                new DashboardPage().OpenRightHearingDevice();
                textOfAllPagesList.Add(new HearingInstrumentInfoControlPage().GetAllTextOfPage());
                Thread.Sleep(500);
                new HearingInstrumentInfoControlPage().Close();

                //Change App Mode
                NavigationHelper.NavigateToSettingsMenu(new DashboardPage()).OpenDemoMode();
                new AppModeSelectionPage().SelectAppMode(AppMode.Normal).Accept();
                textOfAllPagesList.Add(new AppDialog().GetAllTextOfPage());
                new AppDialog().Confirm();

                // Intro Pages
                // After setting to normal mode the app lands in InitializeHardwarePage only for Kind
                if (AppManager.Brand != Brand.Kind)
                    LaunchHelper.SkipIntroPages();
                new InitializeHardwarePage().StartScan();

                //Hardware Scan Page
                DialogHelper.ConfirmIfDisplayed();
                PermissionHelper.AllowPermissionIfRequested();
                Assert.IsTrue(new SelectHearingAidsPage().GetIsScanning());
                textOfAllPagesList.Add(new SelectHearingAidsPage().GetAllTextOfPage());
                new SelectHearingAidsPage().Cancel();

                //Start app in demo mode
                new InitializeHardwarePage().StartDemoMode();
                new DashboardPage().WaitUntilProgramInitFinished().OpenMenuUsingTap();

                // Check help menu
                //Need to check number of headers and menu items
                new MainMenuPage().OpenHelp();
                Thread.Sleep(500);
                textOfAllPagesList.Add(new HelpMenuPage().GetAllTextOfPage());
                Thread.Sleep(3000);
                new HelpMenuPage().NavigateBack();

                //Check Programs menu
                //Need to check number of headers and menu items
                new MainMenuPage().OpenPrograms();
                textOfAllPagesList.Add(new ProgramsMenuPage().GetAllTextOfPage());
                new ProgramsMenuPage().SelectMainProgram(0);
                new DashboardPage().OpenCurrentProgram();

                //Check Auotmatic program
                textOfAllPagesList.Add(new ProgramDetailPage().GetAllTextOfPage());

                //Check Music program
                var programDetailPage = new ProgramDetailPage();
                programDetailPage.SelectProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
                Wait.UntilTrue(() => programDetailPage.GetIsEqualizerDisplayVisible(), TimeSpan.FromSeconds(3));
                Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible());
                Assert.IsTrue(programDetailPage.GetIsTinnitusDisplayVisible());
                textOfAllPagesList.Add(programDetailPage.GetAllTextOfPage());

                //Speech Focus
                programDetailPage.SpeechFocusDisplay.OpenSettings();
                var speechFocusPage = new ProgramDetailParamEditSpeechFocusPage();
                Assert.IsNotEmpty(speechFocusPage.GetTitle());
                textOfAllPagesList.Add(speechFocusPage.GetAllTextOfPage());
                speechFocusPage.SelectSpeechFocus(SpeechFocus.Auto);
                speechFocusPage.SelectSpeechFocus(SpeechFocus.Front);
                speechFocusPage.Close();

                //Noise reduction 
                new ProgramDetailPage().NoiseReductionDisplay.OpenSettings();
                var noiseReductionPage = new ProgramDetailParamEditNoiseReductionPage();
                noiseReductionPage.SelectNoiseReduction(NoiseReduction.Strong);
                noiseReductionPage.SelectNoiseReduction(NoiseReduction.Medium);
                textOfAllPagesList.Add(noiseReductionPage.GetAllTextOfPage());
                noiseReductionPage.Close();

                //Sound(Equalizer)
                new ProgramDetailPage().EqualizerDisplay.OpenSettings();
                var equalizerPage = new ProgramDetailParamEditEqualizerPage();
                equalizerPage.SetEqualizerSliderValue(EqBand.Low, 1);
                equalizerPage.SetEqualizerSliderValue(EqBand.Mid, 0);
                equalizerPage.SetEqualizerSliderValue(EqBand.High, 1);
                textOfAllPagesList.Add(equalizerPage.GetAllTextOfPage());
                equalizerPage.Close();

                //Check Tinnitus Program
                programDetailPage = new ProgramDetailPage();
                programDetailPage.SelectProgramFromProgramsMenu(MainMenuTypes.Preset, 2);
                Wait.UntilTrue(() => programDetailPage.GetIsTinnitusOnlyDisplayVisible(), TimeSpan.FromSeconds(3));
                textOfAllPagesList.Add(programDetailPage.GetAllTextOfPage());
                programDetailPage.TinnitusOnlyDisplay.OpenSettings();

                //Tinnitus
                var tinnitusPage = new ProgramDetailParamEditTinnitusPage();
                tinnitusPage.SetVolumeSliderValue(1);
                tinnitusPage.SetEqualizerSliderValue(EqBand.Low, 1);
                tinnitusPage.SetEqualizerSliderValue(EqBand.Mid, 0);
                tinnitusPage.SetEqualizerSliderValue(EqBand.High, 1);
                textOfAllPagesList.Add(tinnitusPage.GetAllTextOfPage());
                tinnitusPage.Close();

                // Program Settings 
                new ProgramDetailPage().OpenProgramSettings();
                var programDetailSettingsControlPage = new ProgramDetailSettingsControlPage();
                Assert.IsNotEmpty(programDetailSettingsControlPage.GetDescription());
                textOfAllPagesList.Add(programDetailSettingsControlPage.GetAllTextOfPage());

                //Customize Name view
                Assert.IsTrue(programDetailSettingsControlPage.GetIsCustomizeNameVisible());
                programDetailSettingsControlPage.CustomizeName();
                var programNamePage = new ProgramNamePage();
                Assert.IsNotEmpty(programNamePage.GetName());
                textOfAllPagesList.Add(programNamePage.GetAllTextOfPage());
                programNamePage.NavigateBack();

                //Customize Icon view
                Assert.IsTrue(new ProgramDetailSettingsControlPage().GetIsCustomizeIconVisible());
                new ProgramDetailSettingsControlPage().CustomizeIcon();
                textOfAllPagesList.Add(new ProgramIconPage().GetAllTextOfPage());
                new ProgramIconPage().NavigateBack();

                //Create favorite
                Assert.IsTrue(new ProgramDetailSettingsControlPage().GetIsCreateFavoriteVisible());
                new ProgramDetailSettingsControlPage().CreateFavorite();
                AppManager.DeviceSettings.HideKeyboard();
                programNamePage = new ProgramNamePage();
                Assert.IsTrue(programNamePage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));
                textOfAllPagesList.Add(programNamePage.GetAllTextOfPage());
                programNamePage.NavigateBack();
                DialogHelper.ConfirmIfDisplayed();
                new ProgramDetailSettingsControlPage().NavigateBack();

                //Audio Streaming program
                new ProgramDetailPage().OpenMenuUsingTap();
                new MainMenuPage().OpenPrograms();
                new ProgramsMenuPage().SelectStreamingProgram(0);
                programDetailPage = new ProgramDetailPage();
                textOfAllPagesList.Add(programDetailPage.GetAllTextOfPage());

                //Streaming
                programDetailPage.StreamingDisplay.OpenSettings();
                textOfAllPagesList.Add(new ProgramDetailParamEditStreamingPage().GetAllTextOfPage());
                new ProgramDetailParamEditStreamingPage().Close();
                //Do we need to check other parameters view again ?

                // Program Settings 
                new ProgramDetailPage().OpenProgramSettings();
                programDetailSettingsControlPage = new ProgramDetailSettingsControlPage();
                textOfAllPagesList.Add(programDetailSettingsControlPage.GetAllTextOfPage());
                programDetailSettingsControlPage.NavigateBack();

                //Navigate back on dashboard page
                new ProgramDetailPage().NavigateBack();
                return textOfAllPagesList;
            }
        }

        #endregion Sprint 6

        #region Sprint 7

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-9684_Table-82")]
        public void ST9684_DemoModeAdjustSoundInThreeChannels()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            //Open Music Program
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            var programDetailPage = new ProgramDetailPage();
            Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsTinnitusDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsEqualizerDisplayVisible());
            ReportHelper.LogTest(Status.Pass, "Music program page is visible");

            //Check default settings 
            Assert.IsNotEmpty(programDetailPage.SpeechFocusDisplay.GetValue());
            Assert.IsNotEmpty(programDetailPage.NoiseReductionDisplay.GetValue());
            Assert.IsNotEmpty(programDetailPage.TinnitusDisplay.GetValue());
            ReportHelper.LogTest(Status.Pass, "Speech focus, Noise reduction and Tinnitus with their default settings  are visible on Music program Page.");

            //Check volume slider
            var actualSliderValue = programDetailPage.GetVolumeSliderValue();
            programDetailPage.IncreaseVolume().IncreaseVolume().IncreaseVolume();
            Assert.AreNotEqual(actualSliderValue, programDetailPage.GetVolumeSliderValue());
            ReportHelper.LogTest(Status.Pass, "Slider is moved using '+' button");

            actualSliderValue = programDetailPage.GetVolumeSliderValue();
            programDetailPage.DecreaseVolume().DecreaseVolume().DecreaseVolume();
            Assert.AreNotEqual(actualSliderValue, programDetailPage.GetVolumeSliderValue());
            ReportHelper.LogTest(Status.Pass, "Slider is moved using '-' button");

            //Open equalizer display
            programDetailPage.EqualizerDisplay.OpenSettings();
            var equilizerDisplay = new ProgramDetailParamEditEqualizerPage();
            var initialLowValue = equilizerDisplay.GetEqualizerSliderValue(EqBand.Low);
            var initialMidValue = equilizerDisplay.GetEqualizerSliderValue(EqBand.Mid);
            var initialHighValue = equilizerDisplay.GetEqualizerSliderValue(EqBand.High);
            ReportHelper.LogTest(Status.Info, "Sound menu is opened and note is made of existing settings.");

            var lowValue = 1;
            equilizerDisplay.SetEqualizerSliderValue(EqBand.Low, lowValue);
            Assert.AreEqual(lowValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.Low));

            var midValue = 1;
            equilizerDisplay.SetEqualizerSliderValue(EqBand.Mid, midValue);
            Assert.AreEqual(midValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.Mid));

            var highValue = 0;
            equilizerDisplay.SetEqualizerSliderValue(EqBand.High, highValue);
            Assert.AreEqual(highValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.High));

            ReportHelper.LogTest(Status.Pass, "Three sliders are visible and can be moved upward and downward.");
            equilizerDisplay.Close();

            Assert.IsTrue(new ProgramDetailPage().GetIsEqualizerDisplayVisible());
            ReportHelper.LogTest(Status.Pass, "Sound menu is closed.");

            //GetEqualizerSliderValue sometimes is returning double values
            //Ex -: Set value is 0 but on getting same returns 0.01009d.
            //So had to cast to int to avoid Assert failure
            Assert.AreEqual(lowValue, (int)programDetailPage.EqualizerDisplay.GetEqualizerSliderValue(EqBand.Low));
            Assert.AreEqual(midValue, (int)programDetailPage.EqualizerDisplay.GetEqualizerSliderValue(EqBand.Mid));
            Assert.AreEqual(highValue, (int)programDetailPage.EqualizerDisplay.GetEqualizerSliderValue(EqBand.High));
            ReportHelper.LogTest(Status.Pass, "The pictogram for the sound changed according to the settings made.");

            //RestartApp
            AppManager.RestartApp(false);
            dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            ReportHelper.LogTest(Status.Info, "App is restarted successfully");
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            programDetailPage = new ProgramDetailPage();
            programDetailPage.EqualizerDisplay.OpenSettings();
            equilizerDisplay = new ProgramDetailParamEditEqualizerPage();

            //Check Sound settings
            Assert.AreEqual(initialLowValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.Low));
            Assert.AreEqual(initialMidValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.Mid));
            Assert.AreEqual(initialHighValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.High));
            ReportHelper.LogTest(Status.Pass, "Sound settings are set to default values");
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-10621_Table-75")]
        public void ST10621_AppCrashWithBlurEffects()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            //Open MainMenu
            dashboardPage.OpenMenuUsingTap();
            var mainMenuPage = new MainMenuPage();
            Assert.IsNotEmpty(mainMenuPage.GetProgramsText());
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText());
            Assert.IsNotEmpty(mainMenuPage.GetHelpText());
            ReportHelper.LogTest(Status.Pass, "App does not crash and Settings, Programs, Help are visible on Main Menu Page.");

            //Close Mainmenu
            mainMenuPage.CloseMenuUsingTap();
            Assert.IsTrue(new DashboardPage().IsCurrentlyShown());
            ReportHelper.LogTest(Status.Pass, "Dashboard page is visible");

            //Open Music Program
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            var programDetailPage = new ProgramDetailPage();
            Assert.IsTrue(programDetailPage.GetIsBinauralSettingsButtonVisible());
            Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsTinnitusDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsEqualizerDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsSettingsButtonDisplayed());

            //Audifon app v1.4 to change volume by slider first we need to use increse or decrese button.
            //1.3 its working normally
            programDetailPage.DecreaseVolume();
            var value = 0.2;
            programDetailPage.SetVolumeSliderValue(value);
            //Warn.Equals(value, programDetailPage.GetVolumeSliderValue());
            ReportHelper.LogTest(Status.Pass, "Setting icon, Volume control,Speech focus, Noise reduction, Tinnitus and Sound menu with sliders  are visible on Music program Page.");

            //Open Sound menu and moving sliders (low, mid and high)
            programDetailPage.EqualizerDisplay.OpenSettings();
            var equilizerDisplay = new ProgramDetailParamEditEqualizerPage();
            Assert.IsTrue(equilizerDisplay.IsCurrentlyShown());
            ReportHelper.LogTest(Status.Pass, "Sound menu is opened.");

            double lowValue = 1;
            equilizerDisplay.SetEqualizerSliderValue(EqBand.Low, lowValue);
            Assert.AreEqual(Math.Round(lowValue, 0), Math.Round(equilizerDisplay.GetEqualizerSliderValue(EqBand.Low), 0));

            double midValue = 0;
            equilizerDisplay.SetEqualizerSliderValue(EqBand.Mid, midValue);
            Assert.AreEqual(Math.Round(midValue, 0), Math.Round(equilizerDisplay.GetEqualizerSliderValue(EqBand.Mid), 0));

            double highValue = 1;
            equilizerDisplay.SetEqualizerSliderValue(EqBand.High, highValue);
            Assert.AreEqual(Math.Round(highValue, 0), Math.Round(equilizerDisplay.GetEqualizerSliderValue(EqBand.High), 0));
            ReportHelper.LogTest(Status.Pass, "Low, Mid, High sliders can be moved and app does not crash.");

            //Close Sound menu
            equilizerDisplay.Close();
            Assert.IsTrue(new ProgramDetailPage().GetIsEqualizerDisplayVisible());
            ReportHelper.LogTest(Status.Pass, "Sound menu is closed.");

            //GetEqualizerSliderValue sometimes is returning double values
            //Ex -: Set value is 0 but on getting same returns 0.01009.
            //So had to cast to int to avoid Assert failure
            Assert.AreEqual(lowValue, (int)programDetailPage.EqualizerDisplay.GetEqualizerSliderValue(EqBand.Low));
            Assert.AreEqual(midValue, (int)programDetailPage.EqualizerDisplay.GetEqualizerSliderValue(EqBand.Mid));
            Assert.AreEqual(highValue, (int)programDetailPage.EqualizerDisplay.GetEqualizerSliderValue(EqBand.High));
            ReportHelper.LogTest(Status.Pass, "The pictogram for the sound changed according to the settings made.");
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-10950_Table-65")]
        public void ST10950_ViewSupportInformation()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            dashboardPage.CheckStartView(dashboardPage);

            dashboardPage.OpenMenuUsingTap();
            new MainMenuPage().OpenHelp();

            //Submenu 'Notice' is no longer implemented,  the step is skipped due to this
            var helpMenuPage = new HelpMenuPage();
            Assert.IsNotEmpty(helpMenuPage.GetFindHearingDevicesText());
            Assert.IsNotEmpty(helpMenuPage.GetHelpTopicsText());
            Assert.IsNotEmpty(helpMenuPage.GetInstructionsForUseText());
            Assert.IsNotEmpty(helpMenuPage.GetInformationMenuText());
            Assert.IsNotEmpty(helpMenuPage.GetImprintText());
            ReportHelper.LogTest(Status.Pass, "Help Menu page items are displayed.");

        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-10898_Table-66")]
        public void ST10898_CreateFavourites()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            //Open Music Program
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            var programDetailPage = new ProgramDetailPage();
            Assert.IsTrue(programDetailPage.GetIsSettingsButtonDisplayed());


            //Open ProgramSettings (gear icon)
            programDetailPage.OpenProgramSettings();
            var programSettingsControlPage = new ProgramDetailSettingsControlPage();
            Assert.IsTrue(programSettingsControlPage.GetIsCreateFavoriteVisible());
            Assert.IsTrue(programSettingsControlPage.GetIsCustomizeIconVisible());
            Assert.IsTrue(programSettingsControlPage.GetIsCustomizeNameVisible());
            Assert.IsTrue(programSettingsControlPage.GetIsCreateFavoriteFloatingButtonDisplayed());
            ReportHelper.LogTest(Status.Pass, "Customize name, Customize icon, Create favorites and '+' are visible");

            //Create Favorites
            var favouriteProgramName = "Test";
            programSettingsControlPage.CreateFavoriteFloatingButton();
            var programNamePage = new ProgramNamePage();
            Assert.IsNotEmpty(programNamePage.GetDescription());
            programNamePage.EnterName("Test").Proceed();
            ReportHelper.LogTest(Status.Pass, "Name for the new favorite is created");
            var programIconPage = new ProgramIconPage();
            Assert.IsNotEmpty(programIconPage.GetIconTitle());
            programIconPage.SelectIcon(0).Proceed();
            ReportHelper.LogTest(Status.Pass, "First icon for the new favorite is selected");
            var programAutomationPage = new ProgramAutomationPage();
            Assert.IsNotEmpty(programAutomationPage.GetDescription());
            programAutomationPage.Proceed();
            ReportHelper.LogTest(Status.Pass, "Favourite program is created successfully.");
            new ProgramDetailPage().NavigateBack();
            var favouriteProgramIcon = new DashboardPage().GetProgramIcon(4);

            //Close App
            AppManager.CloseApp();

            //Start App
            AppManager.StartApp(false);

            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            ReportHelper.LogTest(Status.Info, "App is closed and started successfully");

            Assert.AreEqual(favouriteProgramIcon, new DashboardPage().GetProgramIcon(4));
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 0);
            Assert.AreEqual(favouriteProgramName, new ProgramDetailPage().GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, " Created favourite program is available.");
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-11416_Table-61")]
        public void ST11416_VerifySettingsMenuPage()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            dashboardPage.CheckStartView(dashboardPage);

            dashboardPage.OpenMenuUsingTap();
            new MainMenuPage().OpenSettings();

            //The 'Developement Stuff' is moved from Help menu to Setting menu

            var settingsMenuPage = new SettingsMenuPage();
            Assert.IsFalse(settingsMenuPage.GetIsDevelopmentStuffVisible());
            Assert.IsNotEmpty(settingsMenuPage.GetMyHearingAidsText());
            Assert.IsNotEmpty(settingsMenuPage.GetPermissionsText());
            Assert.IsNotEmpty(settingsMenuPage.GetLanguageText());
            Assert.IsNotEmpty(settingsMenuPage.GetDemoModeText());
            ReportHelper.LogTest(Status.Pass, "Development stuff is not visible on Settings menu page.");

            var menuItemsList = settingsMenuPage.MenuItems.GetAllVisible();
            for (int i = 0; i < menuItemsList.Count; i++)
            {
                settingsMenuPage.MenuItems.Open(i, IndexType.Relative);
                Thread.Sleep(500);
                Assert.AreEqual(menuItemsList[i], settingsMenuPage.GetNavigationBarTitle());
                ReportHelper.LogTest(Status.Info, "On " + settingsMenuPage.GetNavigationBarTitle() + " Page");
                AppManager.App.PressBackButton();
            }
            ReportHelper.LogTest(Status.Pass, "Settings Menu items are displayed.");
        }

        #endregion Sprint 7

        #endregion Test Cases
    }
}