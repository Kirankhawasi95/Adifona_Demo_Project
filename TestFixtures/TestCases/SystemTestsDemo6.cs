using System;
using HorusUITest.Enums;
using HorusUITest.Helper;
using HorusUITest.PageObjects;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Dialogs;
using HorusUITest.PageObjects.Favorites;
using HorusUITest.PageObjects.Favorites.Automation;
using HorusUITest.PageObjects.Menu;
using HorusUITest.PageObjects.Menu.Help;
using HorusUITest.PageObjects.Menu.Settings;
using NUnit.Framework;
using System.Threading;
using HorusUITest.PageObjects.Controls.ProgramDetailParams;
using AventStack.ExtentReports;

namespace HorusUITest.TestFixtures.TestCases
{
    public class SystemTestsDemo6 : BaseResettingTestFixture
    {
        #region Constructor

        public SystemTestsDemo6(Platform platform) : base(platform)
        {

        }

        #endregion Constructor

        #region Test Cases

        #region Sprint 8

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-11516_Table-58")]
        public void ST11516_LanguageSelection()
        {
            ReportHelper.LogTest(Status.Info, Brand.Audifon + " App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");
            // Wait till the toast message disappears. It causes the test case to fail if it does not wait.
            Thread.Sleep(3000);

            //Gatering text of dashboard page in initial langauge
            // Tinnitus and Audio Streaming remains unchnaged
            var initialCurrentProgramText = dashboardPage.GetCurrentProgramName();
            var initialDemoModeLabelText = dashboardPage.GetDemoModeLabelText();

            //Open Mainmenu
            dashboardPage.OpenMenuUsingTap();
            var mainMenuPage = new MainMenuPage();
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText());
            ReportHelper.LogTest(Status.Pass, "The main menu is opened.");

            //Open Settings
            mainMenuPage.OpenSettings();
            var settingsMenuPage = new SettingsMenuPage();
            Assert.IsNotEmpty(settingsMenuPage.GetMyHearingAidsText());
            Assert.IsNotEmpty(settingsMenuPage.GetPermissionsText());
            Assert.IsNotEmpty(settingsMenuPage.GetLanguageText());
            Assert.IsNotEmpty(settingsMenuPage.GetDemoModeText());
            ReportHelper.LogTest(Status.Pass, "The Settings menu is opened all items are visible.");

            //Open Language menu
            settingsMenuPage.OpenLanguage();
            var settingLanguagePage = new SettingLanguagePage();
            var initialLangauge = settingLanguagePage.GetSelectedLangaugeGeneric(AppManager.Brand); // preset language before changing
            ReportHelper.LogTest(Status.Info, "App is preset to " + initialLangauge + " language");

            switch (AppManager.Brand)
            {
                case Brand.Audifon:
                    foreach (Language_Audifon language in Enum.GetValues(typeof(Language_Audifon)))
                    {
                        Assert.IsNotEmpty(settingLanguagePage.GetLanguageAudifonText(language));
                    }
                    break;
                case Brand.Kind:
                    foreach (Language language in Enum.GetValues(typeof(Language)))
                    {
                        Assert.IsNotEmpty(settingLanguagePage.GetLanguageText(language));
                    }
                    break;
                case Brand.PersonaMedical:
                    foreach (Language_Persona language in Enum.GetValues(typeof(Language_Persona)))
                    {
                        Assert.IsNotEmpty(settingLanguagePage.GetLanguagePersonaText(language));
                    }
                    break;
                case Brand.Puretone:
                    foreach (Language_Puretone language in Enum.GetValues(typeof(Language_Puretone)))
                    {
                        Assert.IsNotEmpty(settingLanguagePage.GetLanguagePuretoneText(language));
                    }
                    break;
                case Brand.Hormann:
                    foreach (Language_Hormann language in Enum.GetValues(typeof(Language_Hormann)))
                    {
                        Assert.IsNotEmpty(settingLanguagePage.GetLanguageHormannText(language));
                    }
                    break;
                case Brand.RxEarsPro:
                    foreach (Language_RxEarsPro language in Enum.GetValues(typeof(Language_RxEarsPro)))
                    {
                        Assert.IsNotEmpty(settingLanguagePage.GetLanguageRxEarsProText(language));
                    }
                    break;
                default: throw new NotImplementedException("Brand not inplemented.");
            }
            ReportHelper.LogTest(Status.Pass, "The Language menu is opened and it contained all available languages in the app");

            // Select a language
            Enum changedLanguage;
            if (initialLangauge.ToString().Equals("English"))
            {
                switch (AppManager.Brand)
                {
                    case Brand.Audifon:
                        settingLanguagePage.SelectLanguageAudifon(Language_Audifon.German);
                        changedLanguage = Language_Audifon.German;
                        break;
                    case Brand.Kind:
                        settingLanguagePage.SelectLanguage(Language.Dutch);
                        changedLanguage = Language.Dutch;
                        break;
                    case Brand.Puretone:
                        settingLanguagePage.SelectLanguagePuretone(Language_Puretone.Spanish);
                        changedLanguage = Language_Puretone.Spanish;
                        break;
                    case Brand.PersonaMedical:
                        settingLanguagePage.SelectLanguagePersona(Language_Persona.Spanish);
                        changedLanguage = Language_Persona.Spanish;
                        break;
                    case Brand.Hormann:
                        settingLanguagePage.SelectLanguageHormann(Language_Hormann.Spanish);
                        changedLanguage = Language_Hormann.Spanish;
                        break;
                    case Brand.RxEarsPro:
                        settingLanguagePage.SelectLanguageRxEarsPro(Language_RxEarsPro.English);
                        changedLanguage = Language_RxEarsPro.English;
                        break;
                    default: throw new NotImplementedException("Brand not inplemented.");
                }
                ReportHelper.LogTest(Status.Info, changedLanguage + " language is selected");
            }
            else
            {
                //If preset language is not English then setting to english
                //As English is common in all OEMs, no switch case based on brand
                settingLanguagePage.SelectLanguageAudifon(Language_Audifon.English);
                changedLanguage = Language_Audifon.English;
                ReportHelper.LogTest(Status.Info, "English language is selected");
            }
            var selectedLanguage = settingLanguagePage.GetSelectedLangaugeGeneric(AppManager.Brand);
            Assert.AreEqual(changedLanguage, settingLanguagePage.GetSelectedLangaugeGeneric(AppManager.Brand));
            settingLanguagePage.Accept();

            //Check app dialog
            var appDialog = new AppDialog();
            Assert.IsNotEmpty(appDialog.GetMessage());
            Assert.IsNotEmpty(appDialog.GetDenyButtonText());
            Assert.IsNotEmpty(appDialog.GetConfirmButtonText());
            ReportHelper.LogTest(Status.Pass, "App shows confirmation dialog.");
            appDialog.Deny();
            Assert.AreNotEqual(selectedLanguage, initialLangauge);
            ReportHelper.LogTest(Status.Pass, "Langauage is not changed if we deny app dialog.");

            //When preset was German select language English and confirm
            //if (initialLangauge.Equals(changedLanguage))
            //{
            //    changedLanguage = Language_Audifon.English;
            //}
            settingLanguagePage.Accept();
            DialogHelper.ConfirmIfDisplayed();

            //Redirected to dashboard page after language change
            dashboardPage = new DashboardPage();
            Assert.AreNotEqual(initialCurrentProgramText, dashboardPage.GetCurrentProgramName());
            Assert.AreNotEqual(initialDemoModeLabelText, dashboardPage.GetDemoModeLabelText());

            ReportHelper.LogTest(Status.Info, "App language is set to " + changedLanguage + " language");
            ReportHelper.LogTest(Status.Pass, "Dashboard page items are visible in " + changedLanguage + " language");

            ReportHelper.LogTest(Status.Info, "Navigate to Language page again");
            NavigationHelper.NavigateToSettingsMenu(dashboardPage).OpenLanguage();
            settingLanguagePage = new SettingLanguagePage();
            Assert.AreEqual(changedLanguage, settingLanguagePage.GetCurrentLangaugeGeneric(AppManager.Brand));
            ReportHelper.LogTest(Status.Pass, "On SettingsLanguagePage selected language is " + changedLanguage);

            //Switch back to preset language
            ReportHelper.LogTest(Status.Info, initialLangauge.ToString());
            switch (AppManager.Brand)
            {
                case Brand.Audifon:
                    settingLanguagePage.SelectLanguageAudifon((Language_Audifon)initialLangauge);
                    break;
                case Brand.Kind:
                    settingLanguagePage.SelectLanguage((Language)initialLangauge);
                    break;
                case Brand.PersonaMedical:
                    settingLanguagePage.SelectLanguagePersona((Language_Persona)initialLangauge);
                    break;
                case Brand.Puretone:
                    settingLanguagePage.SelectLanguagePuretone((Language_Puretone)initialLangauge);
                    break;
                case Brand.Hormann:
                    settingLanguagePage.SelectLanguageHormann((Language_Hormann)initialLangauge);
                    break;
                case Brand.RxEarsPro:
                    settingLanguagePage.SelectLanguageRxEarsPro((Language_RxEarsPro)initialLangauge);
                    break;
                default: throw new NotImplementedException("Brand not inplemented.");
            }
            ReportHelper.LogTest(Status.Info, initialLangauge + " language is selected again");
            settingLanguagePage.Accept();
            DialogHelper.ConfirmIfDisplayed();
            dashboardPage = new DashboardPage();
            Assert.AreEqual(initialCurrentProgramText, dashboardPage.GetCurrentProgramName());
            Assert.AreEqual(initialDemoModeLabelText, dashboardPage.GetDemoModeLabelText());
            ReportHelper.LogTest(Status.Info, "App language changed back to " + initialLangauge + " language");
            ReportHelper.LogTest(Status.Pass, "Dashboard page items are visible in " + initialLangauge + " language");
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-11470_Table-59")]
        public void ST11470_WiFiAndCellularConnectivity()
        {
            // ToDo: check behavior for lower android versions

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            AppManager.DeviceSettings.EnableMobileData();
            ReportHelper.LogTest(Status.Info, "Mobile data enabled");

            //Open Music Program
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            var programDetailPage = new ProgramDetailPage();
            Assert.IsNotEmpty(programDetailPage.GetCurrentProgramName());
            Assert.IsTrue(programDetailPage.GetIsBinauralSettingsButtonVisible());
            Assert.IsTrue(programDetailPage.GetIsSettingsButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "Music program is opened successfully");

            //Open ProgramSettings (gear icon)
            programDetailPage.OpenProgramSettings();
            var programSettingsControlPage = new ProgramDetailSettingsControlPage();
            Assert.IsTrue(programSettingsControlPage.GetIsCreateFavoriteVisible());
            Assert.IsTrue(programSettingsControlPage.GetIsCustomizeIconVisible());
            Assert.IsTrue(programSettingsControlPage.GetIsCustomizeNameVisible());
            Assert.IsTrue(programSettingsControlPage.GetIsCreateFavoriteFloatingButtonDisplayed());
            ReportHelper.LogTest(Status.Pass, "Customize name, Customize icon, Create favorites and '+' are visible");

            //Create Favorite program
            ReportHelper.LogTest(Status.Info, "Create favorite program with Location auto start");
            programSettingsControlPage.CreateFavorite();
            var programNamePage = new ProgramNamePage();
            Assert.IsNotEmpty(programNamePage.GetDescription());
            ReportHelper.LogTest(Status.Pass, "Customize name view opened");
            programNamePage.EnterName("Favorite 01");
            var favouriteProgramName_1 = programNamePage.GetName();
            programNamePage.Proceed();
            ReportHelper.LogTest(Status.Pass, "Name for the new hearing program  is created");
            Assert.IsNotEmpty(new ProgramIconPage().GetDescription());
            ReportHelper.LogTest(Status.Pass, "Customize symbol view opened");

            //Disable Wifi and select icon
            ReportHelper.LogTest(Status.Info, "Disable Wifi and check if dialog with error comes.");
            AppManager.DeviceSettings.DisableWifi();
            ReportHelper.LogTest(Status.Info, "Wifi disabled");
            var programIconPage = new ProgramIconPage();
            programIconPage.SelectIcon(0).Proceed();
            var programAutomationPage = new ProgramAutomationPage();
            Assert.IsNotEmpty(programAutomationPage.GetDescription());
            ReportHelper.LogTest(Status.Pass, "Auto hearing program start view opened");

            //Enable Start hearing program automatically toggle switch
            programAutomationPage.ToggleBinauralSwitch();
            Assert.IsTrue(programAutomationPage.GetIsGeofenceAutomationVisible());
            Assert.IsTrue(programAutomationPage.GetIsWifiAutomationVisible());
            ReportHelper.LogTest(Status.Pass, "The options Connect to WLAN and Connect to location appeared.");

            //Select "Connect to Wifi"
            programAutomationPage.TapConnectToWiFi();
            var appDialog = new AppDialog();
            Assert.IsNotEmpty(appDialog.GetMessage());
            ReportHelper.LogTest(Status.Pass, "App displayed message:" + appDialog.GetMessage());
            appDialog.Confirm();

            //disconnect cellular data and select "Connect to location"
            ReportHelper.LogTest(Status.Info, "Disable cellualar data and check if dialog with error comes");
            AppManager.DeviceSettings.DisableMobileData();
            ReportHelper.LogTest(Status.Info, "Mobile data disabled");
            programAutomationPage.TapConnectToLocation();
            ReportHelper.LogTest(Status.Info, "Dialog appears stating No internet connection");

            //Enable Wifi and select "Connect to wifi"
            ReportHelper.LogTest(Status.Info, "Enable Wifi and check if wifi network is visible.");

            AppManager.DeviceSettings.EnableWifi();
            // This was incresed from 5000 to 10000 because in Samsung A7 Tab it is taking more time to connect to Wifi 
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Wifi enabled");

            // Give "Always Allow" Location Permission before creating favorite based on Wifi. Latest Audifon build requires this permission to create favorite based on wifi
            AppManager.DeviceSettings.GrantGPSBackgroundPermission();

            programAutomationPage.TapConnectToWiFi();
            var automationWifiBindingPage = new AutomationWifiBindingPage();
            Assert.IsNotEmpty(automationWifiBindingPage.GetDescription());
            ReportHelper.LogTest(Status.Pass, "Connect to Wifi view opened");
            Assert.IsNotEmpty(automationWifiBindingPage.GetWifiName());
            ReportHelper.LogTest(Status.Pass, "Wifi network found is:" + automationWifiBindingPage.GetWifiName());

            ReportHelper.LogTest(Status.Info, "Navigate back and disconnect Wifi");
            automationWifiBindingPage.TapBack();

            // For Samsung A7 Tab we do not have sim slot. So Mobile data will not be available. So we have commented wifi disable code so that it will work.
            // The same will be applicable to the device which we do testing without sim card put in the mobile phone.
            // The Test Step has been modified after confirmation from Audifon Team
            //AppManager.DeviceSettings.DisableWifi();
            //ReportHelper.LogTest(Status.Info, "Wifi disabled");

            Assert.IsNotEmpty(new ProgramAutomationPage().GetDescription());
            ReportHelper.LogTest(Status.Pass, "Navigated back to Auto hearing program start");

            ReportHelper.LogTest(Status.Info, "Enable cellualar data and set a map location for autostart.");

            AppManager.DeviceSettings.EnableMobileData();
            Thread.Sleep(5000);
            ReportHelper.LogTest(Status.Info, "Mobile data enabled");

            // Grant Full Access
            AppManager.DeviceSettings.GrantGPSBackgroundPermission();
            Thread.Sleep(5000);

            programAutomationPage.TapConnectToLocation();

            var automationGeofenceBindingPage = new AutomationGeofenceBindingPage();
            Assert.IsNotEmpty(automationGeofenceBindingPage.GetOkButtonText());
            ReportHelper.LogTest(Status.Pass, "Connect to location view opened");

            // As after granting 'Allow always' permission need to reopen Map to select location
            Thread.Sleep(3000);
            new AutomationGeofenceBindingPage().NavigateBack();
            new ProgramAutomationPage().TapConnectToLocation();
            Thread.Sleep(3000); // Wait till the map gets loaded

            //Select location and save created favorite program
            new AutomationGeofenceBindingPage().WaitUntilNoLoadingIndicator().SelectPosition(0.3, 0.2).Ok();

            programAutomationPage = new ProgramAutomationPage();
            Assert.IsNotEmpty(programAutomationPage.GeofenceAutomation.GetValue());
            ReportHelper.LogTest(Status.Pass, "Location is set as autostart for favorite");
            programAutomationPage.Proceed();
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(favouriteProgramName_1, programDetailPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "The saved favorite program is displayed in program view");

            //Revoking permisssion during runtime results in app getting closed abruptly hence skipped this
            //skipping the step as app restarts and progress is lost
            //if we restart app a dialog to appears to enable permission
            //AppManager.DeviceSettings.RevokeGPSPermission();

            ReportHelper.LogTest(Status.Info, "Create another favorite program with Wifi auto start");
            new ProgramDetailPage().OpenProgramSettings();
            new ProgramDetailSettingsControlPage().CreateFavorite();
            programNamePage = new ProgramNamePage();
            programNamePage.EnterName("Favorite 02");
            var favouriteProgramName_2 = programNamePage.GetName();
            programNamePage.Proceed();
            programIconPage = new ProgramIconPage();
            programIconPage.SelectIcon(1).Proceed();
            programAutomationPage = new ProgramAutomationPage();
            programAutomationPage.ToggleBinauralSwitch();

            // Enable Wifi
            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);
            ReportHelper.LogTest(Status.Info, "Wifi enabled");

            new ProgramAutomationPage().TapConnectToWiFi();
            ReportHelper.LogTest(Status.Info, "Enable Wifi and set it as autostart");

            //Select wifi network and confirm with OK button
            automationWifiBindingPage = new AutomationWifiBindingPage();
            Assert.IsNotEmpty(automationWifiBindingPage.GetDescription());
            ReportHelper.LogTest(Status.Pass, "Connect to Wifi view opened");
            var connectedWifiNetwork = automationWifiBindingPage.GetWifiName();
            Assert.IsNotEmpty(connectedWifiNetwork);
            ReportHelper.LogTest(Status.Pass, "Wifi network found is:" + automationWifiBindingPage.GetWifiName());
            automationWifiBindingPage.Ok();
            programAutomationPage = new ProgramAutomationPage();
            Assert.AreEqual(connectedWifiNetwork, programAutomationPage.WifiAutomation.GetValue());
            Assert.IsNotEmpty(programAutomationPage.GetProceedButtonText());
            ReportHelper.LogTest(Status.Pass, "Wifi is set as program auto-start");
            programAutomationPage.Proceed();

            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(favouriteProgramName_2, programDetailPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "The saved favorite program is displayed in program view");
            programDetailPage.NavigateBack();

            //Check both created favorites programs are displayed correctly
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 0);
            Assert.AreEqual(favouriteProgramName_1, new ProgramDetailPage().GetCurrentProgramName());
            programDetailPage.NavigateBack();
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 1);
            Assert.AreEqual(favouriteProgramName_2, new ProgramDetailPage().GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "The programs are displayed correctly.");

            AppManager.DeviceSettings.DisableMobileData();
            AppManager.DeviceSettings.DisableWifi();
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-10984_Table-62")]
        public void ST10984_SwitchButtonRevise()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            dashboardPage.CheckStartView(dashboardPage);

            //Open Mainmenu
            dashboardPage.OpenMenuUsingTap();
            var mainMenuPage = new MainMenuPage();
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText());
            Assert.IsNotEmpty(mainMenuPage.GetProgramsText());
            Assert.IsNotEmpty(mainMenuPage.GetHelpText());
            ReportHelper.LogTest(Status.Pass, "The main menu is opened.");

            //Open Settings
            mainMenuPage.OpenSettings();
            var settingsMenuPage = new SettingsMenuPage();
            Assert.IsNotEmpty(settingsMenuPage.GetMyHearingAidsText());
            Assert.IsNotEmpty(settingsMenuPage.GetPermissionsText());
            Assert.IsNotEmpty(settingsMenuPage.GetLanguageText());
            Assert.IsNotEmpty(settingsMenuPage.GetDemoModeText());
            ReportHelper.LogTest(Status.Pass, "The Settings menu is opened.");

            //Open Permissions
            settingsMenuPage.OpenPermissions();
            var settingPermissionsPage = new SettingPermissionsPage();
            Assert.IsTrue(settingPermissionsPage.GetIsLocationPermissionSwitchChecked());
            settingPermissionsPage.TurnOffLocationPermission();
            ReportHelper.LogTest(Status.Pass, "Location Permission Switch is Off");
            settingPermissionsPage.NavigateBack();

            new SettingsMenuPage().OpenPermissions();
            Assert.IsFalse(new SettingPermissionsPage().GetIsLocationPermissionSwitchChecked());
            ReportHelper.LogTest(Status.Pass, "Location Permission Switch is verified");
            new SettingPermissionsPage().NavigateBack();
            new SettingsMenuPage().NavigateBack();

            //Close Mainmenu
            new MainMenuPage().CloseMenuUsingTap();
            Assert.IsTrue(new DashboardPage().IsCurrentlyShown());
            ReportHelper.LogTest(Status.Pass, "Dashboard page is visible");

            //Check Tinnitus Program
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 2);
            var programDetailPage = new ProgramDetailPage();
            Wait.UntilTrue(() => programDetailPage.GetIsTinnitusOnlyDisplayVisible(), TimeSpan.FromSeconds(3));
            new ProgramDetailPage().ProgramDetailPageUiCheck();

            programDetailPage.TinnitusOnlyDisplay.OpenSettings();
            var programDetailParamEditTinnitusPage = new ProgramDetailParamEditTinnitusPage();
            Assert.IsNotEmpty(programDetailParamEditTinnitusPage.GetTinnitusSwitchTitle());
            var noiseSwitchValue = programDetailParamEditTinnitusPage.GetIsTinnitusSwitchChecked();
            Assert.IsTrue(noiseSwitchValue);
            programDetailParamEditTinnitusPage.ToggleTinnitusSwitch();
            Assert.AreEqual(noiseSwitchValue, programDetailParamEditTinnitusPage.GetIsTinnitusSwitchChecked());
            ReportHelper.LogTest(Status.Pass, "Noise switch cannot be switched off for Tinnitus only.");

            Assert.IsNotEmpty(programDetailParamEditTinnitusPage.GetEqualizerTitle());
            Assert.IsNotEmpty(programDetailParamEditTinnitusPage.GetEqualizerSliderTitle(EqBand.Low));
            Assert.IsNotEmpty(programDetailParamEditTinnitusPage.GetEqualizerSliderTitle(EqBand.Mid));
            Assert.IsNotEmpty(programDetailParamEditTinnitusPage.GetEqualizerSliderTitle(EqBand.High));
            Assert.IsNotEmpty(programDetailParamEditTinnitusPage.GetCloseButtonText());
            programDetailParamEditTinnitusPage.Close();
            Assert.IsTrue(new ProgramDetailPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Info, "Settings for Tinnitus program displayed successfully.");

            //Create favorites
            var programAutomationPage = FavoriteHelper.CreateFavoriteHearingProgram("Favorite 01", 5);

            programAutomationPage.NavigateBack();
            new ProgramIconPage().NavigateBack();
            new ProgramNamePage().Proceed();
            new ProgramIconPage().Proceed();

            Assert.IsTrue(new ProgramAutomationPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            Assert.IsFalse(new ProgramAutomationPage().GetIsAutomationSwitchChecked());
            ReportHelper.LogTest(Status.Pass, "Program Automation Switch is checked");

            new ProgramAutomationPage().NavigateBack();
            new ProgramIconPage().NavigateBack();
            AppManager.DeviceSettings.HideKeyboard();
            new ProgramNamePage().NavigateBack();

            DialogHelper.ConfirmIfDisplayed();

            new ProgramDetailSettingsControlPage().TapBack();

            new ProgramDetailPage().TapBack();

            //Reopen Permissions
            dashboardPage.OpenMenuUsingTap();
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenPermissions();
            Assert.IsTrue(new SettingPermissionsPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            Assert.IsFalse(settingPermissionsPage.GetIsLocationPermissionSwitchChecked());//Earlier step we had turned off the switch
            ReportHelper.LogTest(Status.Pass, "Location Permission Switch is in disabled state");
        }

        #endregion Sprint 8

        #region Sprint 9

        [TestCase(Language_Audifon.German)]
        [TestCase(Language.English)]
        [Category("SystemTestsDemo")]
        [Description("TC-10969_Table-64")]
        public void ST10969_CheckMenuItems(Language_Audifon appLanguage)
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "App started in demo mode");
            // Adding this since even if we add WaitForToastToDisappear still it is not waiting for it in Regression Testing
            Thread.Sleep(3000);

            //Check the initial app language
            NavigationHelper.NavigateToSettingsMenu(dashboardPage).OpenLanguage();
            var initialLanguage = new SettingLanguagePage().GetCurrentLanguageAudifon();

            //change langauge if not equal, else continue
            if (!appLanguage.Equals(initialLanguage))
            {
                new SettingLanguagePage().SelectLanguageAudifon(appLanguage).Accept();
                DialogHelper.ConfirmIfDisplayed();
            }
            else
            {
                //Navigate back to dashboard page
                new SettingLanguagePage().NavigateBack();
                new SettingsMenuPage().NavigateBack();
                new MainMenuPage().CloseMenuUsingTap();
            }
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            new DashboardPage().OpenMenuUsingTap();

            // The test step mentions to check 'Contact', 'Kind Shop', 'Report problem' which have been removed.
            // Skipped the test steps involving these.

            //Setting Menu
            new MainMenuPage().OpenSettings();
            CheckMenuItemsAndTitle(new SettingsMenuPage());
            new SettingsMenuPage().NavigateBack();

            //Help Menu
            new MainMenuPage().OpenHelp();
            CheckMenuItemsAndTitle(new HelpMenuPage());

            //Submenu in Help
            new HelpMenuPage().OpenHelpTopics();
            CheckMenuItemsAndTitle(new HelpTopicsPage());
            new HelpTopicsPage().NavigateBack();

            new HelpMenuPage().OpenInformationMenu();
            CheckMenuItemsAndTitle(new InformationMenuPage());

            AppManager.DeviceSettings.DisableWifi();

            void CheckMenuItemsAndTitle(BaseSubMenuPage menuPage)
            {
                ReportHelper.LogTest(Status.Info, "Checking " + menuPage.GetType().Name + "...");
                var menuItemsList = menuPage.MenuItems.GetAllVisible();
                for (int i = 0; i < menuItemsList.Count; i++)
                {
                    Thread.Sleep(500);
                    menuPage.MenuItems.Open(i, IndexType.Relative);
                    Thread.Sleep(500);
                    DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(1));
                    Thread.Sleep(1000);
                    Assert.AreEqual(menuItemsList[i], menuPage.GetNavigationBarTitle());
                    ReportHelper.LogTest(Status.Info, "On " + menuPage.GetNavigationBarTitle() + " Page");
                    Thread.Sleep(2000);
                    menuPage.NavigateBack();
                }
                ReportHelper.LogTest(Status.Pass, "All menu items checked for " + menuPage.GetType().Name);
            }
        }

        #endregion Sprint 9

        #endregion Test Cases
    }
}