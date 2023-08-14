using System;
using System.Threading;
using AventStack.ExtentReports;
using HorusUITest.Enums;
using HorusUITest.Helper;
using HorusUITest.PageObjects;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Dialogs;
using HorusUITest.PageObjects.Favorites;
using HorusUITest.PageObjects.Favorites.Automation;
using HorusUITest.PageObjects.Menu;
using HorusUITest.PageObjects.Menu.Help;
using HorusUITest.PageObjects.Menu.Info;
using HorusUITest.PageObjects.Menu.Programs;
using HorusUITest.PageObjects.Menu.Settings;
using HorusUITest.PageObjects.Start;
using NUnit.Framework;

namespace HorusUITest.TestFixtures.TestCases
{
    public class SystemTestsDemo3 : BaseResettingTestFixture
    {
        #region Constructor

        public SystemTestsDemo3(Platform platform)
            : base(platform)
        {

        }

        #endregion Constructor

        #region Test Cases

        #region Sprint 3

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-6533_Table-99")]
        public void ST6533_ChangeProgramNameAndIcon()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "App started in demo mode after skipping intro pages and dashboard is loaded");

            ReportHelper.LogTest(Status.Info, "Selecting first program...");
            dashboardPage.SelectProgram(2);
            var programName = dashboardPage.GetCurrentProgramName();
            ReportHelper.LogTest(Status.Info, "Selected first program");
            ReportHelper.LogTest(Status.Info, "Opening selected program...");
            dashboardPage.OpenCurrentProgram();
            ReportHelper.LogTest(Status.Info, "Opened selected program which is '" + programName + "'");

            ReportHelper.LogTest(Status.Info, "Waiting till program details page is loaded...");
            var programDetailPage = new ProgramDetailPage();
            ReportHelper.LogTest(Status.Info, "Program details page is loaded");
            ReportHelper.LogTest(Status.Info, "Checking selected program and opened program are same...");
            Assert.AreEqual(programName, programDetailPage.GetCurrentProgramName(), "Selected program and opened program are not same");
            ReportHelper.LogTest(Status.Pass, "Selected program and opened program are same");

            string ProgramNameEdited = "Program Name Edited";
            ReportHelper.LogTest(Status.Info, "Opening selected program settings...");
            programDetailPage.OpenProgramSettings();
            var programDetailSettingsControlPage = new ProgramDetailSettingsControlPage();
            ReportHelper.LogTest(Status.Info, "Opened selected program settings");
            ReportHelper.LogTest(Status.Info, "Clicking custimize name link...");
            programDetailSettingsControlPage.CustomizeName();
            var programNamePage = new ProgramNamePage();
            ReportHelper.LogTest(Status.Info, "Clicked custimize name link and program name page loaded");
            ReportHelper.LogTest(Status.Info, "Entering program name...");
            programNamePage.EnterName(ProgramNameEdited);
            ReportHelper.LogTest(Status.Info, "Entered program name");
            ReportHelper.LogTest(Status.Info, "Clicking proceed button in program name page...");
            programNamePage.Proceed();
            ReportHelper.LogTest(Status.Info, "Clicked proceed button in program name page");
            ReportHelper.LogTest(Status.Info, "Checking if program name is changed...");
            Assert.AreEqual(new ProgramDetailPage().GetCurrentProgramName(), ProgramNameEdited, "Open program is not '" + ProgramNameEdited + "'");
            ReportHelper.LogTest(Status.Pass, "Program name changed from '" + programName + "' to " + ProgramNameEdited);

            ReportHelper.LogTest(Status.Info, "Opening selected program settings...");
            new ProgramDetailPage().OpenProgramSettings();
            programDetailSettingsControlPage = new ProgramDetailSettingsControlPage();
            ReportHelper.LogTest(Status.Info, "Opened selected program settings");
            ReportHelper.LogTest(Status.Info, "Clicking custimize icon link...");
            programDetailSettingsControlPage.CustomizeIcon();
            var programIconPage = new ProgramIconPage();
            ReportHelper.LogTest(Status.Info, "Clicked custimize icon link and program icon page loaded");
            ReportHelper.LogTest(Status.Info, "Selecting program icon...");
            programIconPage.SelectIcon(1);
            ReportHelper.LogTest(Status.Info, "Selected program icon");
            ReportHelper.LogTest(Status.Info, "Clicking proceed button in program icon page...");
            programIconPage.Proceed();
            ReportHelper.LogTest(Status.Info, "Clicked proceed button in program icon page");
            ReportHelper.LogTest(Status.Pass, "Program icon has been changed");

            string FavoriteProgramName = "Favorite 01";
            FavoriteHelper.CreateFavoriteHearingProgram(FavoriteProgramName, 2).Proceed();
            ReportHelper.LogTest(Status.Info, "Checking if favorite created...");
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(programDetailPage.GetCurrentProgramName(), FavoriteProgramName, "Open program is not '" + FavoriteProgramName + "'");
            ReportHelper.LogTest(Status.Pass, "Favorite created with name '" + FavoriteProgramName + "'");

            FavoriteProgramName = "Favorite 01 Edited";
            ReportHelper.LogTest(Status.Info, "Opening favorite program settings...");
            programDetailPage.OpenProgramSettings();
            programDetailSettingsControlPage = new ProgramDetailSettingsControlPage();
            ReportHelper.LogTest(Status.Info, "Opened favorite program settings");
            ReportHelper.LogTest(Status.Info, "Changing favorite program name...");
            ReportHelper.LogTest(Status.Info, "Clicking custimize name link...");
            programDetailSettingsControlPage.CustomizeName();
            programNamePage = new ProgramNamePage();
            ReportHelper.LogTest(Status.Info, "Clicked custimize name link and program name page loaded");
            ReportHelper.LogTest(Status.Info, "Entering program name...");
            programNamePage.EnterName(FavoriteProgramName);
            ReportHelper.LogTest(Status.Info, "Entered program name");
            ReportHelper.LogTest(Status.Info, "Clicking proceed button in program name page...");
            programNamePage.Proceed();
            ReportHelper.LogTest(Status.Info, "Clicked proceed button in program name page");
            ReportHelper.LogTest(Status.Info, "Checking if program name is changed...");
            Assert.AreEqual(new ProgramDetailPage().GetCurrentProgramName(), FavoriteProgramName, "Open program is not '" + FavoriteProgramName + "'");
            ReportHelper.LogTest(Status.Pass, "Program name changed to '" + FavoriteProgramName + "'");

            ReportHelper.LogTest(Status.Info, "Opening favorite program settings...");
            new ProgramDetailPage().OpenProgramSettings();
            programDetailSettingsControlPage = new ProgramDetailSettingsControlPage();
            ReportHelper.LogTest(Status.Info, "Opened favorite program settings");
            ReportHelper.LogTest(Status.Info, "Clicking custimize icon link...");
            programDetailSettingsControlPage.CustomizeIcon();
            programIconPage = new ProgramIconPage();
            ReportHelper.LogTest(Status.Info, "Clicked custimize icon link and program icon page loaded");
            ReportHelper.LogTest(Status.Info, "Selecting program icon...");
            programIconPage.SelectIcon(3);
            ReportHelper.LogTest(Status.Info, "Selected program icon");
            ReportHelper.LogTest(Status.Info, "Clicking proceed button in program icon page...");
            programIconPage.Proceed();
            ReportHelper.LogTest(Status.Info, "Clicked proceed button in program icon page");
            ReportHelper.LogTest(Status.Pass, "Program icon has been changed");

            string ProgramNameCancel = "Favorite 01 Cancel";
            ReportHelper.LogTest(Status.Info, "Opening favorite program settings...");
            new ProgramDetailPage().OpenProgramSettings();
            programDetailSettingsControlPage = new ProgramDetailSettingsControlPage();
            ReportHelper.LogTest(Status.Info, "Opened favorite program settings");
            ReportHelper.LogTest(Status.Info, "Changing favorite program name...");
            ReportHelper.LogTest(Status.Info, "Clicking custimize name link...");
            programDetailSettingsControlPage.CustomizeName();
            programNamePage = new ProgramNamePage();
            ReportHelper.LogTest(Status.Info, "Clicked custimize name link and program name page loaded");
            ReportHelper.LogTest(Status.Info, "Entering program name...");
            programNamePage.EnterName(ProgramNameCancel);
            ReportHelper.LogTest(Status.Info, "Entered program name");
            ReportHelper.LogTest(Status.Info, "Clicking cancel button in program name page...");
            programNamePage.Cancel();
            ReportHelper.LogTest(Status.Info, "Clicked cancel button in program name page");
            programDetailSettingsControlPage = new ProgramDetailSettingsControlPage();
            ReportHelper.LogTest(Status.Pass, "Program name change cancelled while trying to change it to " + ProgramNameCancel + "' and navigated back to program settings page automatically after cancel");

            ReportHelper.LogTest(Status.Info, "Clicking custimize icon link...");
            programDetailSettingsControlPage.CustomizeIcon();
            programIconPage = new ProgramIconPage();
            ReportHelper.LogTest(Status.Info, "Clicked custimize icon link and program icon page loaded");
            programIconPage.SelectIcon(4);
            ReportHelper.LogTest(Status.Info, "Clicking cancel button in program icon page...");
            programIconPage.Cancel();
            ReportHelper.LogTest(Status.Info, "Clicked cancel button in program icon page");
            programDetailSettingsControlPage = new ProgramDetailSettingsControlPage();
            ReportHelper.LogTest(Status.Pass, "Favorite program icon change cancelled and navigated back to program settings page automatically after cancel");

            ReportHelper.LogTest(Status.Info, "Navigating back from program settings page...");
            programDetailSettingsControlPage.NavigateBack();
            ReportHelper.LogTest(Status.Info, "Navigated back from program settings page");
            ReportHelper.LogTest(Status.Info, "Navigating back from program details page...");
            new ProgramDetailPage().NavigateBack();
            ReportHelper.LogTest(Status.Info, "Navigated back from program details page");

            ReportHelper.LogTest(Status.Info, "Closing app...");
            AppManager.CloseApp();
            ReportHelper.LogTest(Status.Info, "App Closed");

            ReportHelper.LogTest(Status.Info, "Starting app with app data...");
            AppManager.StartApp(false);
            ReportHelper.LogTest(Status.Info, "Started app with app data");

            ReportHelper.LogTest(Status.Info, "Waiting till dashboard is loaded after restart with app data...");
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)), "Dashboard page not loaded");
            new DashboardPage().WaitUntilProgramInitFinished();
            Assert.IsTrue(new DashboardPage().IsCurrentlyShown());
            new DashboardPage().WaitForToastToDisappear();
            Assert.IsTrue(new DashboardPage().GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(new DashboardPage().GetIsRightHearingDeviceVisible());
            Assert.IsTrue(new DashboardPage().GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "Dashboard is loaded after restart with app data");

            ReportHelper.LogTest(Status.Info, "Selecting first program again...");
            new DashboardPage().SelectProgram(2);
            ReportHelper.LogTest(Status.Info, "Selected first program again");

            ReportHelper.LogTest(Status.Info, "Verify the program in dashboard...");
            Assert.AreEqual(ProgramNameEdited, new DashboardPage().GetCurrentProgramName(), "Expected Program Name on Dashboard is '" + ProgramNameEdited + "' but was '" + new DashboardPage().GetCurrentProgramName() + "'");
            ReportHelper.LogTest(Status.Pass, "Program in dashboard is verified");
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-6363_Table-110")]
        public void ST6363_ApplyAutomaticProgramStart()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            var favoriteWithWifi = "Favorite 01 Wifi";
            var favoriteWithLocation = "Favorite 01 Location";

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode after skipping intro pages and dashboard is loaded");

            ReportHelper.LogTest(Status.Info, "Opening Automatic program...");
            dashboardPage.OpenCurrentProgram();
            var programDetailPage = new ProgramDetailPage();
            ReportHelper.LogTest(Status.Info, "Opened Automatic program");
            new ProgramDetailPage().ProgramDetailPageUiCheck();

            ReportHelper.LogTest(Status.Info, "Opening Music program...");
            programDetailPage.SelectProgram(1);
            ReportHelper.LogTest(Status.Info, "Opened Music program");
            new ProgramDetailPage().ProgramDetailPageUiCheck();
            var programAutomationPage = FavoriteHelper.CreateFavoriteHearingProgram(favoriteWithLocation, 3);

            ReportHelper.LogTest(Status.Info, "Granting 'Always Allow' access...");
            AppManager.DeviceSettings.GrantGPSBackgroundPermission();
            Thread.Sleep(5000);
            ReportHelper.LogTest(Status.Info, "Granted 'Always Allow' access");

            ReportHelper.LogTest(Status.Info, "Tapping Connect to Location and wait for automation geofence binding page to be loaded...");
            programAutomationPage.TapConnectToLocation();
            Assert.IsTrue(new AutomationGeofenceBindingPage().IsCurrentlyShown(), "Automation geofence binding page not loaded");
            ReportHelper.LogTest(Status.Info, "Tapped Connect to Location and automation geofence binding page loaded");

            ReportHelper.LogTest(Status.Info, "Disabling location first time...");
            AppManager.DeviceSettings.DisableLocation();
            DialogHelper.DenyIfDisplayed(TimeSpan.FromSeconds(3));
            ReportHelper.LogTest(Status.Info, "Location disabled first time");
            ReportHelper.LogTest(Status.Info, "Enabling location first time...");
            AppManager.DeviceSettings.EnableLocation();
            ReportHelper.LogTest(Status.Info, "Location enabled first time");

            ReportHelper.LogTest(Status.Info, "Checking if app has crashed after disabling and enabling GPS...");
            Assert.IsTrue(new AutomationGeofenceBindingPage().IsCurrentlyShown(), "App has crashed after disabling and enabling GPS");
            ReportHelper.LogTest(Status.Pass, "App has not crashed after disabling and enabling GPS");

            /* ToDo: Code to be removed
            // As after granting 'Allow always' permission need to reopen Map to select location
            Thread.Sleep(3000);
            new AutomationGeofenceBindingPage().NavigateBack();
            new ProgramAutomationPage().TapConnectToLocation();
            */

            Thread.Sleep(3000); // Wait till map gets loaded

            ReportHelper.LogTest(Status.Info, "Selecting map location...");
            new AutomationGeofenceBindingPage().WaitUntilNoLoadingIndicator().SelectPosition(0.5, 0.5);
            Thread.Sleep(1000);
            new AutomationGeofenceBindingPage().Ok();
            ReportHelper.LogTest(Status.Pass, "Map location is selected successfully app does not crash");

            //Checking view of program automation page after setting map location
            programAutomationPage = new ProgramAutomationPage();
            Assert.IsTrue(programAutomationPage.GeofenceAutomation.GetIsValueSet());
            ReportHelper.LogTest(Status.Pass, "Connected to location and address are visible on page.");
            Assert.IsTrue(programAutomationPage.GeofenceAutomation.GetIsDeleteIconVisible());
            Assert.IsTrue(programAutomationPage.GeofenceAutomation.GetIsSettingsIconVisible());
            ReportHelper.LogTest(Status.Pass, "Delete and Settings buttons are visible on page");
            programAutomationPage.Proceed();

            //Check Favorite program view on program detail page
            new ProgramDetailPage().ProgramDetailPageUiCheck();
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(favoriteWithLocation, programDetailPage.GetCurrentProgramName());

            //Switching to Music Program
            programDetailPage.SelectProgram(1);
            new ProgramDetailPage().ProgramDetailPageUiCheck();

            //Creating favorite with wifi automation
            programAutomationPage = FavoriteHelper.CreateFavoriteHearingProgram(favoriteWithWifi, 5);
            Assert.IsTrue(programAutomationPage.GetIsGeofenceAutomationVisible());
            Assert.IsTrue(programAutomationPage.GetIsWifiAutomationVisible());
            ReportHelper.LogTest(Status.Info, "Program automation turned on, WIFI and Location option visible");

            programAutomationPage.TapConnectToWiFi();
            var wifiName1 = new AutomationWifiBindingPage().GetWifiName();
            Assert.IsNotEmpty(wifiName1);
            new AutomationWifiBindingPage().Ok();

            ReportHelper.LogTest(Status.Info, "Porgram Automation is set to wifi");

            //Setting and verifying a program automation wifi 
            programAutomationPage.TapConnectToWiFi();
            var automationWifiBindingPage = new AutomationWifiBindingPage();

            AppManager.DeviceSettings.DisableWifi();
            Thread.Sleep(2000); //Need to add sleep time as scan button dosent appears if we toggle wifi quickly
            ReportHelper.LogTest(Status.Info, "Wifi disabled first time");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(10000); //Waiting for wifi to re-connect
            ReportHelper.LogTest(Status.Info, "Wifi enabled first time");

            automationWifiBindingPage.Scan();
            Thread.Sleep(2000);
            var wifiName2 = automationWifiBindingPage.GetWifiName();
            Assert.IsNotEmpty(wifiName2);
            ReportHelper.LogTest(Status.Info, "Device connected to wifi again");
            automationWifiBindingPage.Ok();
            ReportHelper.LogTest(Status.Pass, "App has not crashed after enabling and disabling Wifi");

            //Checking view of program automation page after setting Wifi
            programAutomationPage = new ProgramAutomationPage();
            Assert.IsTrue(programAutomationPage.IsCurrentlyShown(), "Program automation page not visible after setting wifi");
            Assert.IsTrue(programAutomationPage.WifiAutomation.GetIsValueSet());
            ReportHelper.LogTest(Status.Pass, "Connected Wifi Name is visible on program automation page");
            Assert.IsTrue(programAutomationPage.WifiAutomation.GetIsDeleteIconVisible());
            Assert.IsTrue(programAutomationPage.WifiAutomation.GetIsSettingsIconVisible());
            ReportHelper.LogTest(Status.Pass, "Delete and Settings buttons are visible on page");
            programAutomationPage.Proceed();

            // Checking detail view of favorite program
            new ProgramDetailPage().ProgramDetailPageUiCheck();
            programDetailPage = new ProgramDetailPage();
            Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsEqualizerDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible());
            programDetailPage.NavigateBack();

            //Checking automatic start behaviour with app in foreground
            dashboardPage = new DashboardPage();

            AppManager.DeviceSettings.DisableWifi();
            dashboardPage.WaitForPage(TimeSpan.FromSeconds(8));
            Thread.Sleep(2000); //For Synchronization
            ReportHelper.LogTest(Status.Info, "Wifi disabled second time");

            Assert.AreEqual(favoriteWithLocation, dashboardPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "Favorite program is selected as per location auto-start settings, When app is in foreground");

            // Location services should not be disabled and enabled for Auto select of Location based favorite
            AppManager.DeviceSettings.DisableLocation();
            DialogHelper.DenyIfDisplayed(TimeSpan.FromSeconds(3));
            ReportHelper.LogTest(Status.Info, "Location disabled second time");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(10000); //Waiting for wifi to re-connect
            ReportHelper.LogTest(Status.Info, "Wifi enabled second time");

            // Bug 21700 Fix: when Location is disabled Wifi based favorite should not be selected automatically.
            Assert.AreNotEqual(favoriteWithWifi, dashboardPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "Favorite program is selected as per Wifi auto-start settings, When app is in foreground");

            // Location services should not be disabled and enabled for Auto select of Location based favorite
            AppManager.DeviceSettings.EnableLocation();
            ReportHelper.LogTest(Status.Info, "Location enabled second time");

            //Checking automatic start behaviour after restarting app
            AppManager.RestartApp(false);
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            dashboardPage = new DashboardPage();
            Thread.Sleep(3000); //For Synchronization
            //If location are wifi both are available then autostart with wifi will be given priority
            Assert.AreEqual(favoriteWithWifi, dashboardPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "Favorite program is selected as per Wifi auto-start settings, After restarting app");

            AppManager.DeviceSettings.DisableWifi();
        }

        #endregion Sprint 3

        #region Sprint 4

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-6518_Table-102")]
        public void ST6518_ShowProgramsMenu()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode after skipping intro pages and dashboard is loaded");

            ReportHelper.LogTest(Status.Info, "Opening first program using tap via main menu from dashboard...");
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 0);
            ReportHelper.LogTest(Status.Info, "Opened first program using tap via main menu from dashboard");

            ReportHelper.LogTest(Status.Info, "Opening main menu from program details using tap...");
            new ProgramDetailPage().OpenMenuUsingTap();
            ReportHelper.LogTest(Status.Info, "Opened main menu from program details using tap");
            ReportHelper.LogTest(Status.Info, "Opening programs menu from main menu...");
            new MainMenuPage().OpenPrograms();
            ReportHelper.LogTest(Status.Info, "Opened programs menu from main menu");
            ReportHelper.LogTest(Status.Info, "Tapping back from programs menu...");
            new ProgramsMenuPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Tapped back from programs menu");
            ReportHelper.LogTest(Status.Info, "Opening settings menu from main menu...");
            new MainMenuPage().OpenSettings();
            ReportHelper.LogTest(Status.Info, "Opened settings menu from main menu");
            ReportHelper.LogTest(Status.Info, "Tapping back from settings menu...");
            new SettingsMenuPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Tapped back from settings menu");
            ReportHelper.LogTest(Status.Info, "Opening help menu from main menu...");
            new MainMenuPage().OpenHelp();
            ReportHelper.LogTest(Status.Info, "Opened help menu from main menu");
            ReportHelper.LogTest(Status.Info, "Opening information menu from help menu...");
            new HelpMenuPage().OpenInformationMenu();
            ReportHelper.LogTest(Status.Info, "Opened information menu from help menu");
            ReportHelper.LogTest(Status.Info, "Opening license page from information menu...");
            new InformationMenuPage().OpenLicenses();
            ReportHelper.LogTest(Status.Info, "Opened license page from information menu");
            ReportHelper.LogTest(Status.Info, "Waiting till license page is loaded...");
            Assert.IsTrue(new LicencesPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)), "License page is not loaded");
            ReportHelper.LogTest(Status.Info, "License page is loaded");
            ReportHelper.LogTest(Status.Info, "Tapping back from license page...");
            new LicencesPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Tapped back from license page");
            ReportHelper.LogTest(Status.Info, "Tapping back from information menu...");
            new InformationMenuPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Tapped back from information menu");
            ReportHelper.LogTest(Status.Info, "Opening imprint page from help menu...");
            new HelpMenuPage().OpenImprint();
            ReportHelper.LogTest(Status.Info, "Opened imprint page from help menu");
            ReportHelper.LogTest(Status.Info, "Waiting till imprint page is loaded...");
            Assert.IsTrue(new ImprintPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)), "Imprint page is not loaded");
            ReportHelper.LogTest(Status.Info, "Imprint page is loaded");
            ReportHelper.LogTest(Status.Info, "Tapping back from imprint page...");
            new ImprintPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Tapped back from imprint page");
            ReportHelper.LogTest(Status.Info, "Tapping back from help menu...");
            new HelpMenuPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Tapped back from help menu");
            ReportHelper.LogTest(Status.Info, "Closing main menu using tap...");
            new MainMenuPage().CloseMenuUsingTap();
            ReportHelper.LogTest(Status.Info, "Closed main menu using tap");
            ReportHelper.LogTest(Status.Info, "Tapping back from program details page...");
            new ProgramDetailPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Tapped back from program details page");

            ReportHelper.LogTest(Status.Info, "Opening second program using tap via main menu from dashboard...");
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            ReportHelper.LogTest(Status.Info, "Opened second program using tap via main menu from dashboard");
            ReportHelper.LogTest(Status.Info, "Tapping back from program details page...");
            new ProgramDetailPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Tapped back from program details page");

            ReportHelper.LogTest(Status.Info, "Opening third program using tap via main menu from dashboard...");
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 2);
            ReportHelper.LogTest(Status.Info, "Opened third program using tap via main menu from dashboard");

            string FavoriteProgramName = "Favorite 01";
            ReportHelper.LogTest(Status.Info, "Creating favorite using third program...");
            FavoriteHelper.CreateFavoriteHearingProgram(FavoriteProgramName, 2).Proceed();
            Assert.AreEqual(new ProgramDetailPage().GetCurrentProgramName(), FavoriteProgramName, "Open program is not '" + FavoriteProgramName + "'");
            ReportHelper.LogTest(Status.Info, "Created favorite using third program");
            ReportHelper.LogTest(Status.Info, "Tapping back from program details page...");
            new ProgramDetailPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Tapped back from program details page");

            ReportHelper.LogTest(Status.Info, "Opening third program using tap via main menu from dashboard again...");
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 2);
            ReportHelper.LogTest(Status.Info, "Opened third program using tap via main menu from dashboard again");
            ReportHelper.LogTest(Status.Info, "Tapping back from program details page...");
            new ProgramDetailPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Tapped back from program details page");

            ReportHelper.LogTest(Status.Info, "Opening favorite program using tap via main menu from dashboard...");
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 0);
            ReportHelper.LogTest(Status.Info, "Opened favorite program using tap via main menu from dashboard");
            ReportHelper.LogTest(Status.Info, "Opening program settings from program details page...");
            new ProgramDetailPage().OpenProgramSettings();
            ReportHelper.LogTest(Status.Info, "Opened program settings from program details page");
            ReportHelper.LogTest(Status.Info, "Deleting favorite program '" + FavoriteProgramName + "'...");
            new ProgramDetailSettingsControlPage().DeleteFavoriteAndConfirm();
            ReportHelper.LogTest(Status.Info, "Deleted favorite program '" + FavoriteProgramName + "'");
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-6980_Table-96")]
        public void ST6980_DisplaySettingsMenu()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode after skipping intro pages and dashboard is loaded");

            ReportHelper.LogTest(Status.Info, "Opening main menu using tap...");
            dashboardPage.OpenMenuUsingTap();
            var mainMenuPage = new MainMenuPage();
            ReportHelper.LogTest(Status.Info, "Opened main menu using tap");
            ReportHelper.LogTest(Status.Info, "Checking main menu text...");
            ReportHelper.LogTest(Status.Info, "Checking programs text...");
            Assert.IsNotEmpty(mainMenuPage.GetProgramsText(), "Programs text is empty");
            ReportHelper.LogTest(Status.Info, "Programs text is not empty and is verified");
            ReportHelper.LogTest(Status.Info, "Checking settings text...");
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText(), "Settings text is empty");
            ReportHelper.LogTest(Status.Info, "Settings text is not empty and is verified");
            ReportHelper.LogTest(Status.Info, "Checking help text...");
            Assert.IsNotEmpty(mainMenuPage.GetHelpText(), "Help text is empty");
            ReportHelper.LogTest(Status.Info, "Help text is not empty and is verified");
            ReportHelper.LogTest(Status.Pass, "Main menu text is verified");

            ReportHelper.LogTest(Status.Info, "Opening settings menu...");
            mainMenuPage.OpenSettings();
            var settingsMenuPage = new SettingsMenuPage();
            ReportHelper.LogTest(Status.Info, "Opened settings menu");
            ReportHelper.LogTest(Status.Info, "Checking settings menu text...");
            ReportHelper.LogTest(Status.Info, "Checking hearing aid text...");
            Assert.IsNotEmpty(settingsMenuPage.GetMyHearingAidsText(), "Hearing aid text is empty");
            ReportHelper.LogTest(Status.Info, "Hearing aid text is not empty and is verified");
            ReportHelper.LogTest(Status.Info, "Checking permissions text...");
            Assert.IsNotEmpty(settingsMenuPage.GetPermissionsText(), "Permissions text is empty");
            ReportHelper.LogTest(Status.Info, "Permissions text is not empty and is verified");
            ReportHelper.LogTest(Status.Info, "Checking language text...");
            Assert.IsNotEmpty(settingsMenuPage.GetLanguageText(), "Language text is empty");
            ReportHelper.LogTest(Status.Info, "Language text is not empty and is verified");
            ReportHelper.LogTest(Status.Info, "Checking demo mode text...");
            Assert.IsNotEmpty(settingsMenuPage.GetDemoModeText(), "Demo mode text is empty");
            ReportHelper.LogTest(Status.Info, "Demo mode text is not empty and is verified");
            ReportHelper.LogTest(Status.Pass, "Settings menu text is verified");

            ReportHelper.LogTest(Status.Info, "Checking navigation to the Settings menu items and back...");

            #region Navigate back using back button of app

            ReportHelper.LogTest(Status.Info, "Opening my hearing aid page...");
            settingsMenuPage.OpenMyHearingAids();
            ReportHelper.LogTest(Status.Info, "Opened my hearing aid page");
            ReportHelper.LogTest(Status.Info, "Waiting till my hearing aid page is loaded...");
            Assert.IsTrue(new HearingSystemManagementPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)), "My hearing aid page is not loaded");
            ReportHelper.LogTest(Status.Info, "My hearing aid page is loaded");
            ReportHelper.LogTest(Status.Info, "Tapping back from my hearing aid page using navigation bar...");
            new HearingSystemManagementPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Tapped back from my hearing aid page using navigation bar");

            ReportHelper.LogTest(Status.Info, "Opening permissions page...");
            new SettingsMenuPage().OpenPermissions();
            ReportHelper.LogTest(Status.Info, "Opened permissions page");
            ReportHelper.LogTest(Status.Info, "Waiting till permissions page is loaded...");
            Assert.IsTrue(new SettingPermissionsPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)), "Permissions page is not loaded");
            ReportHelper.LogTest(Status.Info, "Permissions page is loaded");
            ReportHelper.LogTest(Status.Info, "Tapping back from permissions page using navigation bar...");
            new SettingPermissionsPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Tapped back from permissions page using navigation bar");

            ReportHelper.LogTest(Status.Info, "Opening language page...");
            new SettingsMenuPage().OpenLanguage();
            ReportHelper.LogTest(Status.Info, "Opened language page");
            ReportHelper.LogTest(Status.Info, "Waiting till language page is loaded...");
            Assert.IsTrue(new SettingLanguagePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)), "Language page is not loaded");
            ReportHelper.LogTest(Status.Info, "Language page is loaded");
            ReportHelper.LogTest(Status.Info, "Tapping back from language page using navigation bar...");
            new SettingLanguagePage().TapBack();
            ReportHelper.LogTest(Status.Info, "Tapped back from language page using navigation bar");

            ReportHelper.LogTest(Status.Info, "Opening demo mode page...");
            new SettingsMenuPage().OpenDemoMode();
            ReportHelper.LogTest(Status.Info, "Opened demo mode page");
            ReportHelper.LogTest(Status.Info, "Waiting till demo mode page is loaded...");
            Assert.IsTrue(new AppModeSelectionPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)), "Demo mode page is not loaded");
            ReportHelper.LogTest(Status.Info, "Demo mode page is loaded");
            ReportHelper.LogTest(Status.Info, "Tapping back from demo mode page using navigation bar...");
            new AppModeSelectionPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Tapped back from demo mode page using navigation bar");

            #endregion Navigate back using back button of app

            ReportHelper.LogTest(Status.Info, "Waiting till settings menu page is loaded...");
            Assert.IsTrue(new SettingsMenuPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)), "Settings menu page is not loaded");
            settingsMenuPage = new SettingsMenuPage();
            ReportHelper.LogTest(Status.Info, "Settings menu page is loaded");

            #region Navigate back using android back button

            ReportHelper.LogTest(Status.Info, "Opening my hearing aid page...");
            settingsMenuPage.OpenMyHearingAids();
            ReportHelper.LogTest(Status.Info, "Opened my hearing aid page");
            ReportHelper.LogTest(Status.Info, "Waiting till my hearing aid page is loaded...");
            Assert.IsTrue(new HearingSystemManagementPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)), "My hearing aid page is not loaded");
            ReportHelper.LogTest(Status.Info, "My hearing aid page is loaded");
            ReportHelper.LogTest(Status.Info, "Tapping back from my hearing aid page using android back button...");
            AppManager.App.PressBackButton();
            ReportHelper.LogTest(Status.Info, "Tapped back from my hearing aid page using android back button");

            ReportHelper.LogTest(Status.Info, "Opening permissions page...");
            new SettingsMenuPage().OpenPermissions();
            ReportHelper.LogTest(Status.Info, "Opened permissions page");
            ReportHelper.LogTest(Status.Info, "Waiting till permissions page is loaded...");
            Assert.IsTrue(new SettingPermissionsPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)), "Permissions page is not loaded");
            ReportHelper.LogTest(Status.Info, "Permissions page is loaded");
            ReportHelper.LogTest(Status.Info, "Tapping back from permissions page using android back button...");
            AppManager.App.PressBackButton();
            ReportHelper.LogTest(Status.Info, "Tapped back from permissions page using android back button");

            ReportHelper.LogTest(Status.Info, "Opening language page...");
            new SettingsMenuPage().OpenLanguage();
            ReportHelper.LogTest(Status.Info, "Opened language page");
            ReportHelper.LogTest(Status.Info, "Waiting till language page is loaded...");
            Assert.IsTrue(new SettingLanguagePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)), "Language page is not loaded");
            ReportHelper.LogTest(Status.Info, "Language page is loaded");
            ReportHelper.LogTest(Status.Info, "Tapping back from language page using android back button...");
            AppManager.App.PressBackButton();
            ReportHelper.LogTest(Status.Info, "Tapped back from language page using android back button");

            ReportHelper.LogTest(Status.Info, "Opening demo mode page...");
            new SettingsMenuPage().OpenDemoMode();
            ReportHelper.LogTest(Status.Info, "Opened demo mode page");
            ReportHelper.LogTest(Status.Info, "Waiting till demo mode page is loaded...");
            Assert.IsTrue(new AppModeSelectionPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)), "Demo mode page is not loaded");
            ReportHelper.LogTest(Status.Info, "Demo mode page is loaded");
            ReportHelper.LogTest(Status.Info, "Tapping back from demo mode page using android back button...");
            AppManager.App.PressBackButton();
            ReportHelper.LogTest(Status.Info, "Tapped back from demo mode page using android back button");

            #endregion Navigate back using android back button

            ReportHelper.LogTest(Status.Pass, "Navigation to the Settings menu items and back is successful");

            ReportHelper.LogTest(Status.Info, "Checking back navigation to the main menu...");
            ReportHelper.LogTest(Status.Info, "Tapping back to main menu from settings menu...");
            new SettingsMenuPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Tapped back to main menu from settings menu");
            ReportHelper.LogTest(Status.Info, "Waiting till main menu page is loaded...");
            Assert.IsTrue(new MainMenuPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)), "Man menu page is not loaded");
            mainMenuPage = new MainMenuPage();
            ReportHelper.LogTest(Status.Info, "Main menu page is loaded");
            ReportHelper.LogTest(Status.Pass, "Back navigation to the main menu is successful");

            ReportHelper.LogTest(Status.Info, "Closing main menu using tap...");
            mainMenuPage.CloseMenuUsingTap();
            ReportHelper.LogTest(Status.Info, "Closed main menu using tap");
            ReportHelper.LogTest(Status.Info, "Waiting till dashboard page is loaded...");
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)), "Dashboard page is not loaded");
            ReportHelper.LogTest(Status.Info, "Dashboard page is loaded");
            ReportHelper.LogTest(Status.Pass, "Start view Dashboard page is displayed");
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-6530_Table_101")]
        public void ST6530_AppPermissionCheck()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            LaunchHelper.VerifyIntroPages();

            ReportHelper.LogTest(Status.Info, "Starting app in demo mode after skipping intro pages...");
            new InitializeHardwarePage().StartDemoMode();
            ReportHelper.LogTest(Status.Info, "App started in demo mode after skipping intro pages");

            ReportHelper.LogTest(Status.Info, "Checking if app dialog is diaplayed...");
            Assert.IsTrue(DialogHelper.GetIsDialogDisplayed(TimeSpan.FromSeconds(45)), "Dialog not displayed. IOS Reset does not revoke the permssions so in iOS Text case execution dialog will not appear and will fail");
            var appDialog = new AppDialog();
            ReportHelper.LogTest(Status.Info, "App dialog is diaplayed");
            ReportHelper.LogTest(Status.Info, "Checking app dialog information...");
            ReportHelper.LogTest(Status.Info, "Checking app dialog title...");
            Assert.IsNotEmpty(appDialog.GetTitle(), "App dialog title is empty");
            ReportHelper.LogTest(Status.Info, "App dialog title is not empty");
            ReportHelper.LogTest(Status.Info, "Checking app dialog message...");
            Assert.IsNotEmpty(appDialog.GetMessage(), "App dialog message is empty");
            ReportHelper.LogTest(Status.Info, "App dialog message is not empty");
            ReportHelper.LogTest(Status.Pass, "App dialog infomation is verified");
            ReportHelper.LogTest(Status.Info, "Clicking OK button...");
            appDialog.Confirm();
            ReportHelper.LogTest(Status.Info, "Clicked OK button");

            ReportHelper.LogTest(Status.Info, "Deny location permission...");
            new PermissionDialog().Deny();
            ReportHelper.LogTest(Status.Info, "Location permission denied");

            ReportHelper.LogTest(Status.Info, "Grant location permission...");
            AppManager.DeviceSettings.GrantGPSPermission();
            ReportHelper.LogTest(Status.Info, "Location permission granted");

            ReportHelper.LogTest(Status.Info, "Waiting till dashboard page is loaded...");
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)), "Dashboard page not loaded");
            var dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            ReportHelper.LogTest(Status.Pass, "Dashboard page loaded");

            ReportHelper.LogTest(Status.Info, "Quiting app and launch again to check GPS permission are not queried again...");
            ReportHelper.LogTest(Status.Info, "Closing app...");
            AppManager.CloseApp();
            ReportHelper.LogTest(Status.Info, "App closed");

            //Launch app without deleting app data
            //Assuming that GPS permission is already granted and is not revoked after quitting app

            ReportHelper.LogTest(Status.Info, "Starting app with app data...");
            AppManager.StartApp(false);
            ReportHelper.LogTest(Status.Info, "Started app with app data");

            if (new AppDialog(false).IsCurrentlyShown())
            {
                ReportHelper.LogTest(Status.Fail, "After restart, app queries again for already granted permissions");
                Assert.Fail();
            }

            new DashboardPage().WaitUntilProgramInitFinished();
            ReportHelper.LogTest(Status.Pass, "After restart, app does not query for already granted permissions");

            ReportHelper.LogTest(Status.Info, "Quiting app, revoke all permission and launch again to check GPS permission is queried");
            ReportHelper.LogTest(Status.Info, "Closing app...");
            AppManager.CloseApp();
            ReportHelper.LogTest(Status.Info, "App closed");

            ReportHelper.LogTest(Status.Info, "Revoking location permission...");
            AppManager.DeviceSettings.RevokeGPSPermission();
            Thread.Sleep(200);
            ReportHelper.LogTest(Status.Info, "Location permission revoked");

            ReportHelper.LogTest(Status.Info, "Starting app with app data...");
            AppManager.StartApp(false);
            ReportHelper.LogTest(Status.Info, "Started app with app data");

            //The GPS permission was revoked, so a dialog appears and asks for premission
            ReportHelper.LogTest(Status.Info, "Checking if dialog appears after revoking location permission...");
            Assert.IsTrue(DialogHelper.GetIsDialogDisplayed(TimeSpan.FromSeconds(45)), "Dialog did not appear after revoking location permission");
            appDialog = new AppDialog();
            ReportHelper.LogTest(Status.Info, "Dialog appears after revoking location permission");

            ReportHelper.LogTest(Status.Info, "Checking app dialog information...");
            ReportHelper.LogTest(Status.Info, "Checking app dialog title...");
            Assert.IsNotEmpty(appDialog.GetTitle(), "App dialog title is empty");
            ReportHelper.LogTest(Status.Info, "App dialog title is not empty");
            ReportHelper.LogTest(Status.Info, "Checking app dialog message...");
            Assert.IsNotEmpty(appDialog.GetMessage(), "App dialog message is empty");
            ReportHelper.LogTest(Status.Info, "App dialog message is not empty");
            ReportHelper.LogTest(Status.Info, "Checking app dialog confirm button...");
            Assert.IsNotEmpty(appDialog.GetConfirmButtonText(), "App dialog confirmation button text is empty");
            ReportHelper.LogTest(Status.Info, "App dialog confirmation button text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking app dialog deny button...");
            Assert.IsNotEmpty(appDialog.GetDenyButtonText(), "App dialog deny button text is empty");
            ReportHelper.LogTest(Status.Info, "App dialog deny button text is not empty");
            ReportHelper.LogTest(Status.Pass, "App dialog infomation is verified");
            ReportHelper.LogTest(Status.Info, "Clicking OK button...");
            appDialog.Confirm();
            ReportHelper.LogTest(Status.Info, "Clicked OK button");
            Thread.Sleep(1000);

            ReportHelper.LogTest(Status.Info, "Grant location permission...");
            AppManager.DeviceSettings.GrantGPSPermission();
            ReportHelper.LogTest(Status.Info, "Location permission granted");
            ReportHelper.LogTest(Status.Info, "Tapping android back button...");
            AppManager.App.PressBackButton();
            ReportHelper.LogTest(Status.Info, "Tapped android back button");

            ReportHelper.LogTest(Status.Info, "Wating till dashboard page is loaded...");
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)), "Dashboard page is not loaded");
            dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "Dashboard page is loaded");

            ReportHelper.LogTest(Status.Info, "Opening main menu using tap...");
            dashboardPage.OpenMenuUsingTap();
            var mainMenuPage = new MainMenuPage();
            ReportHelper.LogTest(Status.Info, "Opened main menu using tap");

            ReportHelper.LogTest(Status.Info, "Checking help text...");
            Assert.IsNotEmpty(mainMenuPage.GetHelpText(), "Help text is empty");
            ReportHelper.LogTest(Status.Info, "Help text is not empty");

            ReportHelper.LogTest(Status.Info, "Opening help menu...");
            mainMenuPage.OpenHelp();
            ReportHelper.LogTest(Status.Info, "Opened help menu");

            ReportHelper.LogTest(Status.Info, "Opening find hearing device page...");
            new HelpMenuPage().OpenFindHearingDevices();
            ReportHelper.LogTest(Status.Info, "Opened find hearing device page");

            ReportHelper.LogTest(Status.Info, "Navigating back from find hearing device page...");
            new FindDevicesPage().NavigateBack();
            ReportHelper.LogTest(Status.Info, "Navigated back from find hearing device page");

            ReportHelper.LogTest(Status.Info, "Navigating back from help menu...");
            new HelpMenuPage().NavigateBack();
            ReportHelper.LogTest(Status.Info, "Navigated back from help menu");

            ReportHelper.LogTest(Status.Info, "Opening settings menu...");
            new MainMenuPage().OpenSettings();
            var settingsMenuPage = new SettingsMenuPage();
            ReportHelper.LogTest(Status.Info, "Opened settings menu");
            ReportHelper.LogTest(Status.Info, "Checking my hearing aid text...");
            Assert.IsNotEmpty(settingsMenuPage.GetMyHearingAidsText(), "My hearing aid text is empty");
            ReportHelper.LogTest(Status.Info, "My hearing aid text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking permission text...");
            Assert.IsNotEmpty(settingsMenuPage.GetPermissionsText(), "Permission text is empty");
            ReportHelper.LogTest(Status.Info, "Permission text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking language text...");
            Assert.IsNotEmpty(settingsMenuPage.GetLanguageText(), "Location text is empty");
            ReportHelper.LogTest(Status.Info, "Location text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking demo mode text...");
            Assert.IsNotEmpty(settingsMenuPage.GetDemoModeText(), "Demo mode text is empty");
            ReportHelper.LogTest(Status.Info, "Demo mode text is not empty");

            ReportHelper.LogTest(Status.Info, "Opening my hearing device page...");
            settingsMenuPage.OpenMyHearingAids();
            ReportHelper.LogTest(Status.Info, "Opened my hearing device page");
            new HearingSystemManagementPage().CheckHAInformationFromSettings(AppMode.Demo, Side.Left);
            new HearingSystemManagementPage().CheckHAInformationFromSettings(AppMode.Demo, Side.Right);
            ReportHelper.LogTest(Status.Pass, "Opened Hearing Aids and Left and Right hearing systems information is visible");
            ReportHelper.LogTest(Status.Info, "Navigating back from my hearing device page...");
            new HearingSystemManagementPage().NavigateBack();
            ReportHelper.LogTest(Status.Info, "Navigated back from my hearing device page");

            ReportHelper.LogTest(Status.Info, "Opening permissions page...");
            new SettingsMenuPage().OpenPermissions();
            SettingPermissionsPage settingPermissionsPage = new SettingPermissionsPage();
            ReportHelper.LogTest(Status.Info, "Opened permissions page");
            ReportHelper.LogTest(Status.Info, "Toggling location switch...");
            settingPermissionsPage.ToggleLocationSwitch();
            ReportHelper.LogTest(Status.Info, "Toggled location switch");
            ReportHelper.LogTest(Status.Info, "Toggling location switch again...");
            settingPermissionsPage.ToggleLocationSwitch();
            ReportHelper.LogTest(Status.Info, "Toggled location switch again");
            ReportHelper.LogTest(Status.Pass, "Permissions page is visible, Location switch can be turned on and off.");

            AppManager.DeviceSettings.DisableWifi();
        }

        #endregion Sprint 4

        #endregion Test Cases
    }
}