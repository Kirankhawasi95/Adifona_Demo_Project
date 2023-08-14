using HorusUITest.Enums;
using HorusUITest.Helper;
using HorusUITest.PageObjects;
using HorusUITest.PageObjects.Dialogs;
using HorusUITest.PageObjects.Menu;
using HorusUITest.PageObjects.Menu.Help;
using HorusUITest.PageObjects.Menu.Programs;
using HorusUITest.PageObjects.Menu.Settings;
using NUnit.Framework;
using System.Threading;
using Platform = HorusUITest.Enums.Platform;
using HorusUITest.PageObjects.Start.Intro;
using System;
using OpenQA.Selenium;
using HorusUITest.PageObjects.Controls;
using AventStack.ExtentReports;
using HorusUITest.PageObjects.Start;

namespace HorusUITest.TestFixtures.TestCases
{
    public class SystemTestsDemo4 : BaseResettingTestFixture
    {
        #region Constructor

        public SystemTestsDemo4(Platform platform) : base(platform)
        {

        }

        #endregion Constructor

        #region Test Cases

        #region Sprint 5

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-6984_Table-95")]
        public void ST6984_ShowMainMenu()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode after skipping intro pages and dashboard is loaded");

            //Check the start view
            dashboardPage.CheckStartView(dashboardPage);

            //Check Main menu page
            ReportHelper.LogTest(Status.Info, "Opening main menu using tap...");
            dashboardPage.OpenMenuUsingTap();
            var mainMenuPage = new MainMenuPage();
            ReportHelper.LogTest(Status.Info, "Opened main menu using tap");

            ReportHelper.LogTest(Status.Info, "Checking programs text...");
            Assert.IsNotEmpty(mainMenuPage.GetProgramsText(), "Programs text is empty");
            ReportHelper.LogTest(Status.Info, "Programs text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking settings text...");
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText(), "Settings text is empty");
            ReportHelper.LogTest(Status.Info, "Settings text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking help text...");
            Assert.IsNotEmpty(mainMenuPage.GetHelpText(), "Help text is empty");
            ReportHelper.LogTest(Status.Info, "Help text is not empty");
            ReportHelper.LogTest(Status.Pass, "Settings, Programs, Help are visible on Main Menu Page.");

            //Checking navigation into indivisual program using navbar buttons
            ReportHelper.LogTest(Status.Info, "Opening settings menu from main menu...");
            mainMenuPage.OpenSettings();
            ReportHelper.LogTest(Status.Info, "Opened settings menu from main menu");
            ReportHelper.LogTest(Status.Info, "Checking settings menu navigation bar title...");
            Assert.IsNotEmpty(new SettingsMenuPage().GetNavigationBarTitle(), "Settings menu navigation bar title is empty");
            ReportHelper.LogTest(Status.Info, "Settings menu navigation bar title is not empty");
            ReportHelper.LogTest(Status.Info, "Tapping back from settings menu...");
            new SettingsMenuPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Tapped back from settings menu");

            ReportHelper.LogTest(Status.Info, "Opening programs menu from main menu...");
            new MainMenuPage().OpenPrograms();
            ReportHelper.LogTest(Status.Info, "Opened programs menu from main menu");
            ReportHelper.LogTest(Status.Info, "Checking programs menu navigation bar title...");
            Assert.IsNotEmpty(new ProgramsMenuPage().GetNavigationBarTitle(), "Programs menu navigation bar title is empty");
            ReportHelper.LogTest(Status.Info, "Programs menu navigation bar title is not empty");
            ReportHelper.LogTest(Status.Info, "Tapping back from programs menu...");
            new ProgramsMenuPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Tapped back from programs menu");

            ReportHelper.LogTest(Status.Info, "Opening help menu from main menu...");
            new MainMenuPage().OpenHelp();
            ReportHelper.LogTest(Status.Info, "Opened help menu from main menu");
            ReportHelper.LogTest(Status.Info, "Checking help menu navigation bar title...");
            Assert.IsNotEmpty(new HelpMenuPage().GetNavigationBarTitle(), "Help menu navigation bar title is empty");
            ReportHelper.LogTest(Status.Info, "Help menu navigation bar title is not empty");
            ReportHelper.LogTest(Status.Info, "Tapping back from help menu...");
            new HelpMenuPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Tapped back from help menu");

            //Checking navigation into indivisual program using android back button
            ReportHelper.LogTest(Status.Info, "Opening settings menu from main menu...");
            mainMenuPage.OpenSettings();
            ReportHelper.LogTest(Status.Info, "Opened settings menu from main menu");
            ReportHelper.LogTest(Status.Info, "Checking settings menu navigation bar title...");
            Assert.IsNotEmpty(new SettingsMenuPage().GetNavigationBarTitle(), "Settings menu navigation bar title is empty");
            ReportHelper.LogTest(Status.Info, "Settings menu navigation bar title is not empty");
            ReportHelper.LogTest(Status.Info, "Pressing back button from settings menu...");
            AppManager.App.PressBackButton();
            ReportHelper.LogTest(Status.Info, "Pressed back button from settings menu");

            ReportHelper.LogTest(Status.Info, "Opening programs menu from main menu...");
            new MainMenuPage().OpenPrograms();
            ReportHelper.LogTest(Status.Info, "Opened programs menu from main menu");
            ReportHelper.LogTest(Status.Info, "Checking programs menu navigation bar title...");
            Assert.IsNotEmpty(new ProgramsMenuPage().GetNavigationBarTitle(), "Programs menu navigation bar title is empty");
            ReportHelper.LogTest(Status.Info, "Programs menu navigation bar title is not empty");
            ReportHelper.LogTest(Status.Info, "Pressing back button from programs menu...");
            AppManager.App.PressBackButton();
            ReportHelper.LogTest(Status.Info, "Pressed back button from programs menu");

            ReportHelper.LogTest(Status.Info, "Opening help menu from main menu...");
            new MainMenuPage().OpenHelp();
            ReportHelper.LogTest(Status.Info, "Opened help menu from main menu");
            ReportHelper.LogTest(Status.Info, "Checking help menu navigation bar title...");
            Assert.IsNotEmpty(new HelpMenuPage().GetNavigationBarTitle(), "Help menu navigation bar title is empty");
            ReportHelper.LogTest(Status.Info, "Help menu navigation bar title is not empty");
            ReportHelper.LogTest(Status.Info, "Pressing back button from help menu...");
            AppManager.App.PressBackButton();
            ReportHelper.LogTest(Status.Info, "Pressed back button from help menu");

            ReportHelper.LogTest(Status.Pass, "Navigation to indivisual items of Main menu is successful.");

            ReportHelper.LogTest(Status.Info, "Closing main menu using tap...");
            new MainMenuPage().CloseMenuUsingTap();
            ReportHelper.LogTest(Status.Info, "Closed main menu using tap");

            new DashboardPage().CheckStartView(new DashboardPage());
            ReportHelper.LogTest(Status.Pass, "Start view is displayed after closing Main menu");

            ReportHelper.LogTest(Status.Info, "Opening main menu using tap...");
            new DashboardPage().OpenMenuUsingTap();
            try
            {
                AppManager.App.PressBackButton();
                ReportHelper.LogTest(Status.Pass, "Pressed back button in Android");
            }
            catch (NoSuchElementException)
            {
                //Case of iOS where there is no back button
                new MainMenuPage().CloseMenuUsingTap();
                ReportHelper.LogTest(Status.Pass, "Closed main menu using tap in iOS");
            }

            new DashboardPage().CheckStartView(new DashboardPage());
            ReportHelper.LogTest(Status.Pass, "Start view is displayed after closing Main menu using OS back button");

            ReportHelper.LogTest(Status.Info, "Opening main menu using swipe...");
            new DashboardPage().OpenMenuUsingSwipe();
            Thread.Sleep(500);
            ReportHelper.LogTest(Status.Info, "Opened main menu using swipe");
            ReportHelper.LogTest(Status.Info, "Closing main menu using swipe...");
            new MainMenuPage().CloseMenuUsingSwipe();
            Thread.Sleep(500);
            ReportHelper.LogTest(Status.Info, "Closed main menu using swipe");

            // Added this condition because for Samsung Galaxy Tab A7 model when menu is closed it is not going to dashboard but to Programs Menu
            // ToDo: Need to check why it works in Manual Testng and not working when CloseMenuUsingSwipe() method is called
            bool IsDashboardPage = new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(3));
            if (!IsDashboardPage)
            {
                // For Samsung Galaxy Tab A7
                new ProgramsMenuPage().TapBack();
                new DashboardPage().CheckStartView(new DashboardPage());
            }
            else
                new DashboardPage().CheckStartView(new DashboardPage());

            ReportHelper.LogTest(Status.Pass, "Open and close menu using swipe is successfull");
        }

        /// <summary>
        /// The step to deactivate all data connection and verify Online help is skipped
        /// As Online Help option is deprecated so this test scenario is no longer relevant
        /// </summary>
        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-7236_Table-94")]
        public void ST7236_CheckHelpTopics()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode after skipping intro pages and dashboard is loaded");

            //Check Main menu page
            ReportHelper.LogTest(Status.Info, "Opening main menu using tap...");
            dashboardPage.OpenMenuUsingTap();
            var mainMenuPage = new MainMenuPage();
            ReportHelper.LogTest(Status.Info, "Opened main menu using tap");

            ReportHelper.LogTest(Status.Info, "Checking programs text...");
            Assert.IsNotEmpty(mainMenuPage.GetProgramsText(), "Programs text is empty");
            ReportHelper.LogTest(Status.Info, "Programs text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking settings text...");
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText(), "Settings text is empty");
            ReportHelper.LogTest(Status.Info, "Settings text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking help text...");
            Assert.IsNotEmpty(mainMenuPage.GetHelpText(), "Help text is empty");
            ReportHelper.LogTest(Status.Info, "Help text is not empty");
            ReportHelper.LogTest(Status.Pass, "Main Menu Page is displayed.");

            //Check help submenu
            ReportHelper.LogTest(Status.Info, "Opening help menu from main menu...");
            mainMenuPage.OpenHelp();
            var helpMenuPage = new HelpMenuPage();
            ReportHelper.LogTest(Status.Info, "Opened help menu from main menu...");
            ReportHelper.LogTest(Status.Info, "Checking find hearing device text...");
            Assert.IsNotEmpty(helpMenuPage.GetFindHearingDevicesText(), "Find hearing device text is empty");
            ReportHelper.LogTest(Status.Info, "Find hearing device text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking help topics text...");
            Assert.IsNotEmpty(helpMenuPage.GetHelpTopicsText(), "Help topics text is empty");
            ReportHelper.LogTest(Status.Info, "Help topics text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking instructions for use text...");
            Assert.IsNotEmpty(helpMenuPage.GetInstructionsForUseText(), "Instructions for use text is empty");
            ReportHelper.LogTest(Status.Info, "Instructions for use text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking information menu text...");
            Assert.IsNotEmpty(helpMenuPage.GetInformationMenuText(), "Information menu text is empty");
            ReportHelper.LogTest(Status.Info, "Information menu text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking imprint text...");
            Assert.IsNotEmpty(helpMenuPage.GetImprintText(), "Imprint menu text is empty");
            ReportHelper.LogTest(Status.Info, "Imprint menu text is not empty");
            ReportHelper.LogTest(Status.Pass, "Help submenu is displayed");

            //Check navigation into each help topics and verify title.
            //Cannot check for content as no unique locator is present 
            ReportHelper.LogTest(Status.Info, "Opening help topics menu...");
            helpMenuPage.OpenHelpTopics();
            var helpTopicsPage = new HelpTopicsPage();
            ReportHelper.LogTest(Status.Info, "Opened help topics menu");
            var numberOfTopics = helpTopicsPage.MenuItems.CountAllVisible();

            ReportHelper.LogTest(Status.Info, "Opening each menu item in help topics and verify...");
            for (int i = 0; i < numberOfTopics; i++)
            {
                new HelpTopicsPage().MenuItems.Open(i, IndexType.Relative);
                Thread.Sleep(500);
                ReportHelper.LogTest(Status.Info, "Opened menu item '" + helpTopicsPage.GetNavigationBarTitle() + "' page");
                ReportHelper.LogTest(Status.Info, "Checking navigation bar title for '" + helpTopicsPage.GetNavigationBarTitle() + "' page...");
                Assert.IsNotEmpty(new NavigationBar().GetNavigationBarTitle(), "Navigation bar title for '" + helpTopicsPage.GetNavigationBarTitle() + "' page is empty");
                ReportHelper.LogTest(Status.Info, "Navigation bar title for '" + helpTopicsPage.GetNavigationBarTitle() + "' page is not empty");
                ReportHelper.LogTest(Status.Info, "Navigating back...");
                new NavigationBar().NavigateBack();
                ReportHelper.LogTest(Status.Info, "Navigated back");
            }

            ReportHelper.LogTest(Status.Pass, "Opened each item in help topics and is verified");
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-7549_Table-89")]
        public void ST7549_CheckNearFieldSearch()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode after skipping intro pages and dashboard is loaded");
            // Wait till the toast message disappears. It causes the test case to fail if it does not wait.
            Thread.Sleep(3000);

            dashboardPage.CheckStartView(dashboardPage);

            //Check Main menu page
            ReportHelper.LogTest(Status.Info, "Opening main menu using tap...");
            dashboardPage.OpenMenuUsingTap();
            ReportHelper.LogTest(Status.Info, "Opened main menu using tap");

            ReportHelper.LogTest(Status.Info, "Opening help menu from main menu...");
            new MainMenuPage().OpenHelp();
            ReportHelper.LogTest(Status.Info, "Opened help menu from main menu");

            //Open Find hearing devices page
            ReportHelper.LogTest(Status.Info, "Opening find hearing device page...");
            new HelpMenuPage().OpenFindHearingDevices();
            FindDevicesPage findDevicesPage = new FindDevicesPage();
            ReportHelper.LogTest(Status.Info, "Opened find hearing device page");

            //Check Map view
            ReportHelper.LogTest(Status.Info, "Checking if map view is selected...");
            Assert.IsTrue(findDevicesPage.GetIsMapViewSelected(), "Map view is not selected");
            ReportHelper.LogTest(Status.Info, "Map view is selected");
            ReportHelper.LogTest(Status.Info, "Checking toggle view button text...");
            Assert.IsNotEmpty(findDevicesPage.GetToggleViewButtonText(), "Toggle view button text is empty");
            ReportHelper.LogTest(Status.Info, "Toggle view button text is not empty");

            //Switch to Near field search
            //Test step to verify the level with no signal strength is skipped as no unique locator is present for the element
            ReportHelper.LogTest(Status.Info, "Selecting near field view...");
            findDevicesPage.SelectNearFieldView();
            ReportHelper.LogTest(Status.Info, "Selected near field view");

            //Check Left device
            ReportHelper.LogTest(Status.Info, "Checking if left device tab is selected...");
            Assert.IsTrue(findDevicesPage.GetIsLeftDeviceSelected(), "Left device tab is not selected");
            ReportHelper.LogTest(Status.Info, "Left device tab is selected");
            ReportHelper.LogTest(Status.Info, "Checking left device tab text...");
            Assert.IsNotEmpty(findDevicesPage.GetLeftDeviceText(), "Left device tab text is empty");
            ReportHelper.LogTest(Status.Info, "Left device tab text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right device tab text...");
            Assert.IsNotEmpty(findDevicesPage.GetRightDeviceText(), "Right device tab text is empty");
            ReportHelper.LogTest(Status.Info, "Right device tab text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking if near field view is selected...");
            Assert.IsTrue(findDevicesPage.GetIsNearFieldViewSelected(), "Near field view is not selected");
            ReportHelper.LogTest(Status.Info, "Near field view is selected");

            //Check Right device
            ReportHelper.LogTest(Status.Info, "Selecting right device tab...");
            findDevicesPage.SelectRightDevice();
            ReportHelper.LogTest(Status.Info, "Selected right device tab");
            ReportHelper.LogTest(Status.Info, "Checking if right device tab is selected...");
            Assert.IsTrue(findDevicesPage.GetIsRightDeviceSelected(), "Right device tab is not selected");
            ReportHelper.LogTest(Status.Info, "Right device tab is selected");
            ReportHelper.LogTest(Status.Info, "Checking left device tab text...");
            Assert.IsNotEmpty(findDevicesPage.GetLeftDeviceText(), "Left device tab text is empty");
            ReportHelper.LogTest(Status.Info, "Left device tab text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right device tab text...");
            Assert.IsNotEmpty(findDevicesPage.GetRightDeviceText(), "Right device tab text is empty");
            ReportHelper.LogTest(Status.Info, "Right device tab text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking if near field view is selected...");
            Assert.IsTrue(findDevicesPage.GetIsNearFieldViewSelected(), "Near field view is not selected");
            ReportHelper.LogTest(Status.Info, "Near field view is selected");

            ReportHelper.LogTest(Status.Pass, "Both hearing devices are selectable and the view is diaplayed correctly");

            AppManager.DeviceSettings.DisableWifi();
        }

        #endregion Sprint 5

        #region Sprint 6

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-9540_Table-83")]
        public void ST9540_ExitDemoMode()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode after skipping intro pages and dashboard is loaded");
            // Wait till the toast message disappears. It causes the test case to fail if it does not wait.
            Thread.Sleep(3000);

            //Menu opened
            ReportHelper.LogTest(Status.Info, "Opening main menu using tap...");
            dashboardPage.OpenMenuUsingTap();
            var mainMenuPage = new MainMenuPage();
            ReportHelper.LogTest(Status.Info, "Opened main menu using tap");

            ReportHelper.LogTest(Status.Info, "Checking programs text...");
            Assert.IsNotEmpty(mainMenuPage.GetProgramsText(), "Programs text is empty");
            ReportHelper.LogTest(Status.Info, "Programs text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking settings text...");
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText(), "Settings text is empty");
            ReportHelper.LogTest(Status.Info, "Settings text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking help text...");
            Assert.IsNotEmpty(mainMenuPage.GetHelpText(), "Help text is empty");
            ReportHelper.LogTest(Status.Info, "Help text is not empty");
            ReportHelper.LogTest(Status.Pass, "Settings, Programs, Help are visible on Main Menu Page.");

            //Settings opened
            ReportHelper.LogTest(Status.Info, "Opening settings menu from main menu...");
            mainMenuPage.OpenSettings();
            var settingsMenuPage = new SettingsMenuPage();
            ReportHelper.LogTest(Status.Info, "Opened settings menu from main menu");
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
            ReportHelper.LogTest(Status.Pass, "HearingAids, Permissions, Language, DemoMode are visible on Main Menu Page.");

            ReportHelper.LogTest(Status.Info, "Opening demo mode page...");
            settingsMenuPage.OpenDemoMode();
            var appModeSelectionPage = new AppModeSelectionPage();
            Assert.IsTrue(appModeSelectionPage.IsCurrentlyShown());
            ReportHelper.LogTest(Status.Info, "Opened demo mode page");
            ReportHelper.LogTest(Status.Info, "Checking demo mode navigation bar text...");
            Assert.IsNotEmpty(appModeSelectionPage.GetNavigationBarTitle(), "Demo mode navigation bar text is empty");
            ReportHelper.LogTest(Status.Info, "Demo mode navigation bar text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking demo mode text...");
            Assert.IsNotEmpty(appModeSelectionPage.GetAppModeText(AppMode.Demo), "Demo mode text is empty");
            ReportHelper.LogTest(Status.Info, "Demo mode text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking normal mode text...");
            Assert.IsNotEmpty(appModeSelectionPage.GetAppModeText(AppMode.Normal), "Normal mode text is empty");
            ReportHelper.LogTest(Status.Info, "Normal mode text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking if demo mode is selected...");
            Assert.AreEqual(AppMode.Demo, appModeSelectionPage.GetSelectedAppMode(), "Demo mode is not selected");
            ReportHelper.LogTest(Status.Info, "Demo mode is selected");
            ReportHelper.LogTest(Status.Info, "Switching to normal mode...");
            appModeSelectionPage.ChangeAppModeOnly(AppMode.Normal);
            ReportHelper.LogTest(Status.Info, "Switched to normal mode");
            ReportHelper.LogTest(Status.Info, "Clicking accept...");
            appModeSelectionPage.Accept();
            ReportHelper.LogTest(Status.Info, "Clicked accept");
            ReportHelper.LogTest(Status.Info, "Checking if dialog is displayed...");
            Assert.IsTrue(DialogHelper.GetIsDialogDisplayed(TimeSpan.FromSeconds(2)), "Dialog is not displayed");
            ReportHelper.LogTest(Status.Info, "Dialog is displayed");
            ReportHelper.LogTest(Status.Info, "Clicking deny...");
            DialogHelper.Deny();
            ReportHelper.LogTest(Status.Info, "Clicked deny");
            ReportHelper.LogTest(Status.Info, "Normal mode changed cancelled");
            ReportHelper.LogTest(Status.Info, "Clicking accept again...");
            appModeSelectionPage.Accept();
            ReportHelper.LogTest(Status.Info, "Clicked accept again");
            ReportHelper.LogTest(Status.Info, "Checking if dialog is displayed again...");
            Assert.IsTrue(DialogHelper.GetIsDialogDisplayed(TimeSpan.FromSeconds(2)), "Dialog is not displayed again");
            ReportHelper.LogTest(Status.Info, "Dialog is displayed again");
            ReportHelper.LogTest(Status.Info, "Clicking confirm...");
            DialogHelper.Confirm();
            ReportHelper.LogTest(Status.Info, "Clicked confirm");

            // After setting to normal mode the app lands in InitializeHardwarePage only for Kind
            if (AppManager.Brand == Brand.Kind)
            {
                ReportHelper.LogTest(Status.Info, "Checking if initialize hardware page is loaded...");
                Assert.IsTrue(new InitializeHardwarePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Initialize hardware page is not loaded");
                ReportHelper.LogTest(Status.Info, "Initialize hardware page is loaded");
            }
            else
            {
                //Welcome Screen displayed
                ReportHelper.LogTest(Status.Info, "Checking if intro1 page is loaded...");
                Assert.IsTrue(new IntroPageOne().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Intro1 page is not loaded");
                Assert.IsTrue(new IntroPageOne().IsCurrentlyShown());
                ReportHelper.LogTest(Status.Info, "Intro1 page is loaded");

                Assert.IsNotEmpty(new IntroPageOne().GetTitle());
            }

            ReportHelper.LogTest(Status.Info, "Normal mode confirmed");
            Thread.Sleep(300);

            ReportHelper.LogTest(Status.Info, "Restarting app with app data...");
            AppManager.RestartApp(false);
            ReportHelper.LogTest(Status.Info, "Restarting app with app data");

            if (AppManager.Brand == Brand.Kind)
            {
                ReportHelper.LogTest(Status.Info, "Checking if initialize hardware page is loaded after restart...");
                Assert.IsTrue(new InitializeHardwarePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Initialize hardware page is not loaded after restart");
                ReportHelper.LogTest(Status.Info, "Initialize hardware page is loaded after restart");
            }
            else
            {
                ReportHelper.LogTest(Status.Info, "Checking if intro1 page is loaded after restart...");
                Assert.IsTrue(new IntroPageOne().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Intro1 page is not loaded after restart");
                Assert.IsTrue(new IntroPageOne().IsCurrentlyShown());
                ReportHelper.LogTest(Status.Info, "Intro1 page is loaded after restart");
            }
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-7971_Table-86")]
        public void ST7971_LanguageSelectionTest()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode after skipping intro pages and dashboard is loaded");
            // Wait till the toast message disappears. It causes the test case to fail if it does not wait.
            Thread.Sleep(3000);

            dashboardPage.CheckStartView(dashboardPage);

            //Open Main menu page
            ReportHelper.LogTest(Status.Info, "Opening main menu using tap...");
            dashboardPage.OpenMenuUsingTap();
            ReportHelper.LogTest(Status.Info, "Opened main menu using tap");

            var settingsMenuText = new MainMenuPage().GetSettingsText();

            ReportHelper.LogTest(Status.Info, "Opening settings menu from main menu...");
            new MainMenuPage().OpenSettings();
            var settingsMenuPage = new SettingsMenuPage();
            ReportHelper.LogTest(Status.Info, "Opened settings menu from main menu");

            //Getting text of submenu items before language change
            var hearingSystemText = settingsMenuPage.GetMyHearingAidsText();
            var permissionsText = settingsMenuPage.GetPermissionsText();
            var languageText = settingsMenuPage.GetLanguageText();
            var demoModeText = settingsMenuPage.GetDemoModeText();

            ReportHelper.LogTest(Status.Info, "Opening language page...");
            settingsMenuPage.OpenLanguage();
            var settingLanguagePage = new SettingLanguagePage();
            ReportHelper.LogTest(Status.Info, "Opened language page");

            //Open and check the language page
            var presetLanguageText = settingLanguagePage.GetCurrentLanguageText(); // preset language before changing
            var selectedLangauageText = settingLanguagePage.GetSelectedLanguageText();
            ReportHelper.LogTest(Status.Info, "Checking if language page currently " + presetLanguageText + " language is selected...");
            Assert.AreEqual(selectedLangauageText, presetLanguageText);
            ReportHelper.LogTest(Status.Info, "Language page currently " + presetLanguageText + " language is selected");
            ReportHelper.LogTest(Status.Info, "Checking language page title...");
            Assert.IsNotEmpty(settingLanguagePage.GetTitle(), "Language page title text is empty");
            ReportHelper.LogTest(Status.Info, "Language page title text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking language page current language text...");
            Assert.IsNotEmpty(settingLanguagePage.GetCurrentLanguageText(), "Language page current language text is empty");
            ReportHelper.LogTest(Status.Info, "Language page current language text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking if accept button is visible...");
            Assert.IsTrue(settingLanguagePage.GetIsAcceptButtonVisible(), "Accept button is not visible");
            ReportHelper.LogTest(Status.Info, "Accept button is visible");
            ReportHelper.LogTest(Status.Pass, "Language Page check is completed");

            //Check back navigation by android button and app button
            ReportHelper.LogTest(Status.Info, "Navigating back to settings menu...");
            settingLanguagePage.NavigateBack();
            Thread.Sleep(500);
            new SettingsMenuPage().IsCurrentlyShown();
            ReportHelper.LogTest(Status.Info, "Navigated back to settings menu");

            ReportHelper.LogTest(Status.Info, "Opening language page...");
            new SettingsMenuPage().OpenLanguage();
            Thread.Sleep(500);
            ReportHelper.LogTest(Status.Info, "Opened language page");

            ReportHelper.LogTest(Status.Info, "Pressing android back button...");
            AppManager.App.PressBackButton();
            Thread.Sleep(500);
            ReportHelper.LogTest(Status.Info, "Pressed android back button");

            ReportHelper.LogTest(Status.Info, "Checking if settings menu page is loaded after android back button press...");
            Assert.IsTrue(new SettingsMenuPage().IsCurrentlyShown(), "Settings menu page is not loaded after android back button press");
            ReportHelper.LogTest(Status.Info, "Settings menu page is loaded after android back button press");

            ReportHelper.LogTest(Status.Info, "Opening language page...");
            new SettingsMenuPage().OpenLanguage();
            ReportHelper.LogTest(Status.Info, "Opened language page");
            ReportHelper.LogTest(Status.Pass, "Settings page is visible after back navigation from language page");

            //Check language is not changed after navigating back 
            settingLanguagePage = new SettingLanguagePage();
            ReportHelper.LogTest(Status.Info, "Selecting language to German...");
            selectedLangauageText = settingLanguagePage.SelectLanguageAudifon(Language_Audifon.German).GetSelectedLanguageText();
            ReportHelper.LogTest(Status.Info, "Selected language to German");
            ReportHelper.LogTest(Status.Info, "Navigating back to settings menu...");
            settingLanguagePage.NavigateBack();
            ReportHelper.LogTest(Status.Info, "Navigated back to settings menu");
            ReportHelper.LogTest(Status.Info, "Opening language page...");
            new SettingsMenuPage().OpenLanguage();
            Thread.Sleep(1000);
            settingLanguagePage = new SettingLanguagePage();
            ReportHelper.LogTest(Status.Info, "Opened language page");
            ReportHelper.LogTest(Status.Info, "Checking if language change is adopted just by selecting a language and navigating back...");
            Assert.AreNotEqual(selectedLangauageText, settingLanguagePage.GetSelectedLanguage());
            ReportHelper.LogTest(Status.Pass, "Language change is not adopted just by selecting a language and navigating back");

            //Check language is not changed
            ReportHelper.LogTest(Status.Info, "Selecting language to French...");
            selectedLangauageText = settingLanguagePage.SelectLanguageAudifon(Language_Audifon.French).GetSelectedLanguageText();
            ReportHelper.LogTest(Status.Info, "Selected language to French");
            ReportHelper.LogTest(Status.Info, "Clicking accept...");
            settingLanguagePage.Accept();
            Thread.Sleep(500);
            ReportHelper.LogTest(Status.Info, "Clicked accept...");
            ReportHelper.LogTest(Status.Info, "Pressing android back button...");
            AppManager.App.PressBackButton();
            Thread.Sleep(500);
            ReportHelper.LogTest(Status.Info, "Pressed android back button");
            //iOS scenario where there is no back button to close dialog
            if (DialogHelper.GetIsDialogDisplayed())
            {
                ReportHelper.LogTest(Status.Info, "Clicking deny...");
                DialogHelper.Deny();
                ReportHelper.LogTest(Status.Info, "Clicked deny...");
            }
            ReportHelper.LogTest(Status.Info, "Navigating back to settings menu...");
            new SettingLanguagePage().NavigateBack();
            ReportHelper.LogTest(Status.Info, "Navigated back to settings menu");
            ReportHelper.LogTest(Status.Info, "Checking if settings menu is loaded...");
            Assert.IsTrue(new SettingsMenuPage().IsCurrentlyShown(), "Settings menu is not loaded");
            settingsMenuPage = new SettingsMenuPage();
            ReportHelper.LogTest(Status.Info, "Settings menu is loaded");

            //Getting text of settings submenu items after language change
            var changedHearingSystemText = settingsMenuPage.GetMyHearingAidsText();
            var changedPermissionsText = settingsMenuPage.GetPermissionsText();
            var changedLanguageText = settingsMenuPage.GetLanguageText();
            var changedDemoModeText = settingsMenuPage.GetDemoModeText();

            ReportHelper.LogTest(Status.Info, "Opening language page...");
            new SettingsMenuPage().OpenLanguage();
            Thread.Sleep(1000);
            ReportHelper.LogTest(Status.Info, "Opened language page");

            ReportHelper.LogTest(Status.Info, "Checking if language change is not adopted if we do not confim the dialog...");
            Assert.AreNotEqual(selectedLangauageText, settingLanguagePage.GetSelectedLanguage());
            ReportHelper.LogTest(Status.Pass, "Language change is not adopted if we do not confim the dialog");

            ReportHelper.LogTest(Status.Info, "Checking hearing system text before language change...");
            Assert.AreEqual(hearingSystemText, changedHearingSystemText, "Hearing system text before language change is not same");
            ReportHelper.LogTest(Status.Info, "Hearing system text before language change is same");
            ReportHelper.LogTest(Status.Info, "Checking permissions text before language change...");
            Assert.AreEqual(permissionsText, changedPermissionsText, "Permissions text before language change is not same");
            ReportHelper.LogTest(Status.Info, "Permissions text before language change is same");
            ReportHelper.LogTest(Status.Info, "Checking language text before language change...");
            Assert.AreEqual(languageText, changedLanguageText, "Language text before language change is not same");
            ReportHelper.LogTest(Status.Info, "Language text before language change is same");
            ReportHelper.LogTest(Status.Info, "Checking demo mode text before language change...");
            Assert.AreEqual(demoModeText, changedDemoModeText, "Demo mode text before language change is not same");
            ReportHelper.LogTest(Status.Info, "Demo mode text before language change is same");

            //Check if language change is adopted
            ReportHelper.LogTest(Status.Info, "Selecting language to German...");
            selectedLangauageText = settingLanguagePage.SelectLanguageAudifon(Language_Audifon.German).GetSelectedLanguageText();
            ReportHelper.LogTest(Status.Info, "Selected language to German");
            ReportHelper.LogTest(Status.Info, "Clicking accept...");
            settingLanguagePage.Accept();
            ReportHelper.LogTest(Status.Info, "Clicked accept");
            ReportHelper.LogTest(Status.Info, "Clicking confirm...");
            new AppDialog().Confirm();
            ReportHelper.LogTest(Status.Info, "Clicked confirm");
            ReportHelper.LogTest(Status.Info, "Changed language from '" + presetLanguageText + "' to '" + selectedLangauageText + "'");
            ReportHelper.LogTest(Status.Info, "Waiting till dashboard page is loaded...");
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Dashboard page is not loaded");
            ReportHelper.LogTest(Status.Info, "Dashboard page is loaded");

            //Restart app
            ReportHelper.LogTest(Status.Info, "Restarting app with app data...");
            AppManager.RestartApp(false);
            ReportHelper.LogTest(Status.Info, "Restarted app with app data");

            ReportHelper.LogTest(Status.Info, "Waiting till dashboard page is loaded...");
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)), "Dashboard page is not loaded");
            ReportHelper.LogTest(Status.Info, "Dashboard page is loaded");

            ReportHelper.LogTest(Status.Info, "Opening settings menu from main menu...");
            new DashboardPage().WaitUntilProgramInitFinished().OpenMenuUsingTap();
            ReportHelper.LogTest(Status.Info, "Opened settings menu from main menu");

            var changedSettingsMenuText = new MainMenuPage().GetSettingsText();

            ReportHelper.LogTest(Status.Info, "Checking settings menu text after language change...");
            Assert.AreNotEqual(settingsMenuText, changedSettingsMenuText, "Settings menu text after language change is same");
            ReportHelper.LogTest(Status.Info, "Settings menu text after language change is not same");

            ReportHelper.LogTest(Status.Info, "Opening settings menu from main menu...");
            new MainMenuPage().OpenSettings();
            settingsMenuPage = new SettingsMenuPage();
            ReportHelper.LogTest(Status.Info, "Opened settings menu from main menu");

            changedHearingSystemText = settingsMenuPage.GetMyHearingAidsText();
            changedPermissionsText = settingsMenuPage.GetPermissionsText();
            changedLanguageText = settingsMenuPage.GetLanguageText();
            changedDemoModeText = settingsMenuPage.GetDemoModeText();

            ReportHelper.LogTest(Status.Info, "Checking hearing system text after language change...");
            Assert.AreNotEqual(hearingSystemText, changedHearingSystemText, "Hearing system text after language change is same");
            ReportHelper.LogTest(Status.Info, "Hearing system text after language change is not same");
            ReportHelper.LogTest(Status.Info, "Checking permissions text after language change...");
            Assert.AreNotEqual(permissionsText, changedPermissionsText, "Permissions text after language change is same");
            ReportHelper.LogTest(Status.Info, "Permissions text after language change is not same");
            ReportHelper.LogTest(Status.Info, "Checking language text after language change...");
            Assert.AreNotEqual(languageText, changedLanguageText, "Language text after language change is same");
            ReportHelper.LogTest(Status.Info, "Language text after language change is not same");
            ReportHelper.LogTest(Status.Info, "Checking demo mode text after language change...");
            Assert.AreNotEqual(demoModeText, changedDemoModeText, "Demo mode text after language change is same");
            ReportHelper.LogTest(Status.Info, "Demo mode text after language change is not same");

            ReportHelper.LogTest(Status.Info, "Opening language page...");
            new SettingsMenuPage().OpenLanguage();
            settingLanguagePage = new SettingLanguagePage();
            ReportHelper.LogTest(Status.Info, "Opened language page");

            ReportHelper.LogTest(Status.Info, "Checking if current language and previous language are same...");
            Assert.AreNotEqual(presetLanguageText, selectedLangauageText, "Current language and previous language are same");
            ReportHelper.LogTest(Status.Info, "Current language and previous language are not same");

            ReportHelper.LogTest(Status.Info, "Checking if change from " + presetLanguageText + " to " + selectedLangauageText + " language is successfully adopted...");
            Assert.AreEqual(settingLanguagePage.GetCurrentLanguageAudifon(), settingLanguagePage.GetSelectedLanguageAudifon());
            ReportHelper.LogTest(Status.Info, "Change from " + presetLanguageText + " to " + selectedLangauageText + " language is successfully adopted");
        }

        [Test]
        [Category("SystemTestsDemoLongStandBy")]
        [Description("TC-7691_Table-88")]
        public void ST7691_StandByModeBehaviourCheck()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            // ToDo: Currently we get timeout error for 20 mins. So keeping it as 20 secs
            int appBackgroundDuration = 20;

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode after skipping intro pages and dashboard is loaded");

            // Navigate to find hearing Aids
            ReportHelper.LogTest(Status.Info, "Opening main menu using tap...");
            dashboardPage.OpenMenuUsingTap();
            ReportHelper.LogTest(Status.Info, "Opened main menu using tap");

            ReportHelper.LogTest(Status.Info, "Opening help menu from main menu...");
            new MainMenuPage().OpenHelp();
            ReportHelper.LogTest(Status.Info, "Opened help menu from main menu");

            ReportHelper.LogTest(Status.Info, "Opening find hearing device page...");
            new HelpMenuPage().OpenFindHearingDevices();
            FindDevicesPage findDevicesPage = new FindDevicesPage();
            Assert.IsTrue(findDevicesPage.IsCurrentlyShown());
            ReportHelper.LogTest(Status.Info, "Opened find hearing device page");

            ReportHelper.LogTest(Status.Info, "Checking if map view is selected...");
            Assert.IsTrue(findDevicesPage.GetIsMapViewSelected(), "Map view is not selected");
            ReportHelper.LogTest(Status.Info, "Map view is selected");
            ReportHelper.LogTest(Status.Info, "Checking toggle view button text...");
            Assert.IsNotEmpty(findDevicesPage.GetToggleViewButtonText(), "Toggle view button text is empty");
            ReportHelper.LogTest(Status.Info, "Toggle view button text is not empty");
            ReportHelper.LogTest(Status.Pass, "Successfully navigated to 'Find Hearing Devices' page");

            ReportHelper.LogTest(Status.Info, "Selecting near field view...");
            findDevicesPage.SelectNearFieldView();
            ReportHelper.LogTest(Status.Info, "Selected near field view");

            ReportHelper.LogTest(Status.Info, "Checking if left device tab is selected...");
            Assert.IsTrue(findDevicesPage.GetIsLeftDeviceSelected(), "Left device tab is not selected");
            ReportHelper.LogTest(Status.Info, "Left device tab is selected");
            ReportHelper.LogTest(Status.Info, "Checking left device tab text...");
            Assert.IsNotEmpty(findDevicesPage.GetLeftDeviceText(), "Left device tab text is empty");
            ReportHelper.LogTest(Status.Info, "Left device tab text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right device tab text...");
            Assert.IsNotEmpty(findDevicesPage.GetRightDeviceText(), "Right device tab text is empty");
            ReportHelper.LogTest(Status.Info, "Right device tab text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking if near field view is selected...");
            Assert.IsTrue(findDevicesPage.GetIsNearFieldViewSelected(), "Near field view is not selected");
            ReportHelper.LogTest(Status.Info, "Near field view is selected");
            ReportHelper.LogTest(Status.Pass, "Switched to Near Field View");

            ReportHelper.LogTest(Status.Info, "Putting the app to background and foreground for '" + appBackgroundDuration + "' mins...");
            AppManager.DeviceSettings.PutAppToBackground(appBackgroundDuration);
            ReportHelper.LogTest(Status.Info, "Put the app to background and foreground for '" + appBackgroundDuration + "' mins");
            ReportHelper.LogTest(Status.Info, "Checking if find device page is visible...");
            Assert.IsTrue(new FindDevicesPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Info, "Find device page is visible");
            ReportHelper.LogTest(Status.Pass, "App is in foreground and can still be used");

            AppManager.DeviceSettings.DisableWifi();
        }

        #endregion Sprint 6

        #endregion Test Cases
    }
}