namespace HorusUITest.TestFixtures.TestCases
{
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
    using HorusUITest.PageObjects.Menu.Help.InstructionsForUse;
    using HorusUITest.PageObjects.Menu.Info;
    using HorusUITest.PageObjects.Menu.Settings;
    using HorusUITest.PageObjects.Start;
    using HorusUITest.PageObjects.Start.Intro;
    using NUnit.Framework;
    using System;
    using System.Threading;

    public class SystemTestsDevice9 : BaseResettingTestFixture
    {
        #region Constructor

        public SystemTestsDevice9(Platform platform)
            : base(platform)
        {

        }

        #endregion Constructor

        #region Test Cases

        #region Sprint 34

        [Test]
        [Category("SystemTestsDeviceSpecificConfig")]
        [Description("TC-21726_Table-0")]
        public void ST21726_IncludeOEMHormann()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            //ToDo: Need to check Splash Screen. Currently while execution of test case the splash screen does not appear

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            // Check Menu Items
            //CheckMenuItems(Language_Hormann.English);

            // Commenting the language to German since it is removed from the app.
            //// Change the language to German
            //new DashboardPage().OpenMenuUsingTap();
            //new MainMenuPage().OpenSettings();
            //new SettingsMenuPage().OpenLanguage();
            //var settingLanguagePage = new SettingLanguagePage();
            //settingLanguagePage.SelectLanguageHormann(Language_Hormann.German);
            //settingLanguagePage.Accept();
            //new AppDialog().Confirm();
            //Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            //ReportHelper.LogTest(Status.Pass, "Language changed to German");

            // Change to Normal Mode
            new DashboardPage().OpenMenuUsingTap();
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenDemoMode();
            var appModeSelectionPage = new AppModeSelectionPage();
            appModeSelectionPage.ChangeAppModeOnly(AppMode.Normal);
            appModeSelectionPage.Accept();
            new AppDialog().Confirm();
            Assert.IsTrue(new IntroPageOne(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Pass, "Into page loaded");

            // Connect to Risa R Hearing System
            var LeftHearingAidNameRisa = SelectHearingAid.GetLeftHearingAid(LeftHearingAid.Microgenisis_Risa_R_Left_068821);
            var RightHearingAidNameRisa = SelectHearingAid.GetRightHearingAid(RightHearingAid.Microgenisis_Risa_R_Right_068818);

            dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(LeftHearingAidNameRisa, RightHearingAidNameRisa).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + LeftHearingAidNameRisa + "' and Right '" + RightHearingAidNameRisa + "'");
            ReportHelper.LogTest(Status.Info, "Dashboard page loaded");

            // Checking HA Info Page
            dashboardPage.OpenLeftHearingDevice();
            new HearingInstrumentInfoControlPage().CheckHAInformationFromDashboard(AppMode.Normal).Close();
            new DashboardPage().OpenRightHearingDevice();
            new HearingInstrumentInfoControlPage().CheckHAInformationFromDashboard(AppMode.Normal).Close();
            ReportHelper.LogTest(Status.Pass, "All details are visible on Left and Right Hearing Instrument Info page.");

            // Create Favourite
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            string FavoriteProgramName = "Favourite 01";
            ReportHelper.LogTest(Status.Info, "Checking Settings Icon color of Music Program...");
            var programDetailPage = new ProgramDetailPage();
            // Color slightly varies in IOS
            if (OniOS)
                Assert.AreEqual(programDetailPage.GetSettingsIconColor(), "#971B26");
            else
                Assert.AreEqual(programDetailPage.GetSettingsIconColor(), "#9D1823");
            ReportHelper.LogTest(Status.Pass, "Settings Icon color verified");
            programDetailPage.OpenProgramSettings();
            var programSettingsControlPage = new ProgramDetailSettingsControlPage();
            programSettingsControlPage.CreateFavorite();
            var programNamePage = new ProgramNamePage();
            programNamePage.EnterName(FavoriteProgramName).Proceed();
            var programIconPage = new ProgramIconPage();
            programIconPage.SelectIcon(24).Proceed();
            var programAutomationPage = new ProgramAutomationPage();
            programAutomationPage.Proceed();
            ReportHelper.LogTest(Status.Pass, "Check if newly created Favourite is available and its name is '" + FavoriteProgramName + "'");
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(FavoriteProgramName, programDetailPage.GetCurrentProgramName(), "Expected to be on ProgramDetailPage of '" + FavoriteProgramName + "' but was " + programDetailPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "Favourite program '" + FavoriteProgramName + "' is created successfully");
            ReportHelper.LogTest(Status.Info, "Checking Settings Icon color of Favorite Program...");
            Assert.AreEqual(programDetailPage.GetSettingsIconColor(), "#838383");
            ReportHelper.LogTest(Status.Pass, "Settings Icon color verified");
            programDetailPage.TapBack();

            CheckMenuItems(Language_Hormann.English, LeftHearingAidNameRisa, RightHearingAidNameRisa);

            // Start App Again
            AppManager.RestartApp(true);
            ReportHelper.LogTest(Status.Info, "App Started again after reset of the data");

            // Connect to Lewi R Hearing System
            var LeftHearingAidNameLewi = SelectHearingAid.GetLeftHearingAid();
            var RightHearingAidNameLewi = SelectHearingAid.GetRightHearingAid();

            dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(LeftHearingAidNameLewi, RightHearingAidNameLewi).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + LeftHearingAidNameLewi + "' and Right '" + RightHearingAidNameLewi + "'");
            ReportHelper.LogTest(Status.Info, "Dashboard page loaded");

            // Change the language to Portuguese
            ReportHelper.LogTest(Status.Info, "Changing language to Portuguese...");
            new DashboardPage().OpenMenuUsingTap();
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenLanguage();
            var settingLanguagePage = new SettingLanguagePage();
            settingLanguagePage.SelectLanguageHormann(Language_Hormann.Portuguese);
            settingLanguagePage.Accept();
            new AppDialog().Confirm();
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Pass, "Language changed to Portuguese");

            // Create Favourite
            dashboardPage = new DashboardPage();
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            FavoriteProgramName = "Favourite 02";
            programDetailPage = new ProgramDetailPage();
            programDetailPage.OpenProgramSettings();
            programSettingsControlPage = new ProgramDetailSettingsControlPage();
            programSettingsControlPage.CreateFavorite();
            programNamePage = new ProgramNamePage();
            programNamePage.EnterName(FavoriteProgramName).Proceed();
            programIconPage = new ProgramIconPage();
            programIconPage.SelectIcon(25).Proceed();
            programAutomationPage = new ProgramAutomationPage();
            programAutomationPage.Proceed();
            ReportHelper.LogTest(Status.Pass, "Check if newly created Favourite is available and its name is '" + FavoriteProgramName + "'");
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(FavoriteProgramName, programDetailPage.GetCurrentProgramName(), "Expected to be on ProgramDetailPage of '" + FavoriteProgramName + "' but was " + programDetailPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "Favourite program '" + FavoriteProgramName + "' is created successfully");
            ReportHelper.LogTest(Status.Info, "Checking Settings Icon color of Favorite Program...");
            Assert.AreEqual(programDetailPage.GetSettingsIconColor(), "#838383");
            ReportHelper.LogTest(Status.Pass, "Settings Icon color verified");
            programDetailPage.TapBack();

            CheckMenuItems(Language_Hormann.Portuguese, LeftHearingAidNameLewi, RightHearingAidNameLewi);

            // Commenting the language to Dutch since it is removed from the app.
            //// Change the language to Dutch
            //new DashboardPage().OpenMenuUsingTap();
            //new MainMenuPage().OpenSettings();
            //new SettingsMenuPage().OpenLanguage();
            //settingLanguagePage = new SettingLanguagePage();
            //settingLanguagePage.SelectLanguageHormann(Language_Hormann.Dutch);
            //settingLanguagePage.Accept();
            //new AppDialog().Confirm();
            //Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            //ReportHelper.LogTest(Status.Pass, "Language changed to Dutch");

            //CheckMenuItems(Language_Hormann.Dutch);

            // Change the language to Spanish
            ReportHelper.LogTest(Status.Info, "Changing language to Spanish...");
            new DashboardPage().OpenMenuUsingTap();
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenLanguage();
            settingLanguagePage = new SettingLanguagePage();
            settingLanguagePage.SelectLanguageHormann(Language_Hormann.Spanish);
            settingLanguagePage.Accept();
            new AppDialog().Confirm();
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Pass, "Language changed to Spanish");

            CheckMenuItems(Language_Hormann.Spanish, LeftHearingAidNameLewi, RightHearingAidNameLewi);

            AppManager.DeviceSettings.DisableWifi();

            void CheckMenuItems(Language_Hormann language, string LeftHearingAidName, string RightHearingAidName)
            {
                new DashboardPage().OpenMenuUsingTap();

                //Setting Menu
                new MainMenuPage().OpenSettings();
                CheckMenuItemsTitle(new SettingsMenuPage(), language);
                new SettingsMenuPage().NavigateBack();

                //Help Menu
                new MainMenuPage().OpenHelp();
                CheckMenuItemsTitle(new HelpMenuPage(), language);

                //Submenu in Help
                new HelpMenuPage().OpenHelpTopics();
                CheckMenuItemsTitle(new HelpTopicsPage(), language);
                new HelpTopicsPage().NavigateBack();

                // Information Menu in Help
                new HelpMenuPage().OpenInformationMenu();
                CheckMenuItemsTitle(new InformationMenuPage(), language);

                new InformationMenuPage().NavigateBack();

                // Inprint Page in Help. Comparing the text directly from resource file
                new HelpMenuPage().OpenImprint();

                ReportHelper.LogTest(Status.Info, "Checking Imprint Page in " + language + "...");
                var imprintPage = new ImprintPage();
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "menu_Title_AppInfo"), imprintPage.GetAppHeader());
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "info_imprintAddressTitle"), imprintPage.GetAddressHeader());
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "oem_companyName"), imprintPage.GetAppCompanyName());
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "oem_companyStreet"), imprintPage.GetAppCompanyStreet());
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "oem_companyPostalCodeCity"), imprintPage.GetAppCompanyPostalCodeCity());
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "oem_companyState"), imprintPage.GetAppCommpanyState());
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "info_imprintSupportTitle"), imprintPage.GetSupportHeader());
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "oem_support_desc_1"), imprintPage.GetSupportDescription());
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "oem_website_url"), imprintPage.GetSupportWebsite());
                ReportHelper.LogTest(Status.Pass, "Imprint page text verified in " + language);
                imprintPage.NavigateBack();

                new HelpMenuPage().NavigateBack();

                new MainMenuPage().OpenSettings();

                new SettingsMenuPage().OpenMyHearingAids();

                ReportHelper.LogTest(Status.Info, "Checking My Hearing Systems Page in " + language + "...");
                HearingSystemManagementPage hearingSystemManagementPage = new HearingSystemManagementPage();
                hearingSystemManagementPage.LeftDeviceTabClick();
                Assert.IsTrue(hearingSystemManagementPage.GetIsLeftTabSelected());
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "manageHA_LabelName"), hearingSystemManagementPage.GetLeftDeviceNameTitle());
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "manageHA_LabelSerial"), hearingSystemManagementPage.GetLeftDeviceSerialTitle());
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "manageHA_LabelState"), hearingSystemManagementPage.GetLeftDeviceStateTitle());
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "manageHA_LabelModel"), hearingSystemManagementPage.GetLeftDeviceTypeTitle());
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "manageHA_Disconnect").ToUpper(), hearingSystemManagementPage.GetDisconnectDevicesTitle().ToUpper());
                hearingSystemManagementPage.RightDeviceTabClick();
                Assert.IsTrue(hearingSystemManagementPage.GetIsRightTabSelected());
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "manageHA_LabelName"), hearingSystemManagementPage.GetRightDeviceNameTitle());
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "manageHA_LabelSerial"), hearingSystemManagementPage.GetRightDeviceSerialTitle());
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "manageHA_LabelState"), hearingSystemManagementPage.GetRightDeviceStateTitle());
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "manageHA_LabelModel"), hearingSystemManagementPage.GetRightDeviceTypeTitle());
                ReportHelper.LogTest(Status.Pass, "My Hearing Systems page text verified in " + language);

                ReportHelper.LogTest(Status.Pass, "Menu Items verified sucessfully in " + language);

                hearingSystemManagementPage.DisconnectDevices();

                ReportHelper.LogTest(Status.Info, "Checking disconnection message in " + language + "...");
                Assert.IsTrue(DialogHelper.GetIsDialogDisplayed(TimeSpan.FromSeconds(5)));
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "dialog_disconnect_hearingaids_title"), new AppDialog().GetTitle());
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "dialog_disconnect_hearingaids_desc"), new AppDialog().GetMessage());
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "dialog_disconnect_hearingaids_btn_disconnect").ToUpper(), new AppDialog().GetConfirmButtonText().ToUpper());
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "bt_Cancel").ToUpper(), new AppDialog().GetDenyButtonText().ToUpper());
                new AppDialog().Confirm();

                // Disconnecting Message does not appear in IOS
                if (OnAndroid)
                    Assert.IsTrue(hearingSystemManagementPage.VerifyDisconnectingText(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "manageHA_DisconnectingHearingSystems"), TimeSpan.FromSeconds(5)));

                ReportHelper.LogTest(Status.Pass, "Disconnection message verified sucessfully in " + language);

                ReportHelper.LogTest(Status.Info, "Checking InitializeHardwarePage texts in " + language + "...");
                Assert.IsTrue(new InitializeHardwarePage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)));
                var initializeHardwarePage = new InitializeHardwarePage();
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "start_btStartScanning").ToUpper(), initializeHardwarePage.GetScanText().ToUpper());
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "welcome_btDemoMode").ToUpper(), initializeHardwarePage.GetDemoModeText().ToUpper());
                ReportHelper.LogTest(Status.Pass, "InitializeHardwarePage texts verified sucessfully in " + language);

                initializeHardwarePage.StartScan();

                ReportHelper.LogTest(Status.Info, "Checking SelectHearingAidsPage texts in " + language + "...");
                var selectHearingAidsPage = new SelectHearingAidsPage();
                if (!selectHearingAidsPage.GetIsDeviceFound(LeftHearingAidName))
                    selectHearingAidsPage.WaitUntilDeviceFound(LeftHearingAidName);

                if (RightHearingAidName != null)
                    selectHearingAidsPage.WaitUntilDeviceFound(RightHearingAidName);
                if (!selectHearingAidsPage.GetIsDeviceFound(RightHearingAidName))
                    selectHearingAidsPage.WaitUntilDeviceFound(RightHearingAidName);

                selectHearingAidsPage.WaitUntilDeviceListNotChanging();
                selectHearingAidsPage.SelectDevicesExclusively(LeftHearingAidName, RightHearingAidName);
                Assert.IsTrue(selectHearingAidsPage.GetIsDeviceSelected(LeftHearingAidName));
                if (RightHearingAidName != null)
                    selectHearingAidsPage.GetIsDeviceSelected(RightHearingAidName);

                ReportHelper.LogTest(Status.Info, "App is started and Hearing Aids are detected in Scan. Left '" + LeftHearingAidName + "' and Right '" + RightHearingAidName + "'");

                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "selectDevices_Description").Replace("\r", string.Empty).Replace("\n", string.Empty).Replace(" ", string.Empty), selectHearingAidsPage.GetDescription().Replace("\r", string.Empty).Replace("\n", string.Empty).Replace(" ", string.Empty));
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "selectDevices_btCancelScan").ToUpper(), selectHearingAidsPage.GetCancelText().ToUpper());
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "selectDevices_btConnectDevices").ToUpper(), selectHearingAidsPage.GetConnectButtonText().ToUpper());
                ReportHelper.LogTest(Status.Pass, "SelectHearingAidsPage texts verified sucessfully in " + language);

                selectHearingAidsPage.Connect();

                Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
                dashboardPage = new DashboardPage();

                dashboardPage.WaitUntilProgramInitFinished();
                Assert.IsTrue(dashboardPage.IsCurrentlyShown());
                dashboardPage.WaitForToastToDisappear();

                Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());

                // ToDo: Need to Disable and Enable Location. Since location Disable and Enable has not been implemented in iOS it has been skipped 
            }

            void CheckMenuItemsTitle(BaseSubMenuPage menuPage, Language_Hormann language)
            {
                ReportHelper.LogTest(Status.Info, "Checking " + menuPage.GetType().Name + " in " + language);
                var menuItemsList = menuPage.MenuItems.GetAllVisible();
                for (int i = 0; i < menuItemsList.Count; i++)
                {
                    Thread.Sleep(500);
                    menuPage.MenuItems.Open(i, IndexType.Relative);
                    Thread.Sleep(500);
                    // ToDo: The text for Imprint is different in menu and Imprint page title in Spanish
                    if (language == Language_Hormann.Spanish && menuItemsList[i] == LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "mainMenu_BtnImprint"))
                        Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language, "info_imprintTitle"), menuPage.GetNavigationBarTitle());
                    else
                        Assert.AreEqual(menuItemsList[i], menuPage.GetNavigationBarTitle());
                    ReportHelper.LogTest(Status.Pass, "On " + menuPage.GetNavigationBarTitle() + " Page");
                    Thread.Sleep(2000);
                    menuPage.NavigateBack();
                }
                ReportHelper.LogTest(Status.Pass, "All menu items checked for " + menuPage.GetType().Name + " in " + language);
            }
        }

        [Test]
        [Category("SystemTestsDeviceManual")]
        [Description("TC-20580_Table-0")]
        public void ST20580_UpdateNuGetPackagesForSolution()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            var favoriteWithWifi = "Favorite 01 Wifi";
            var favoriteWithLocation = "Favorite 01 Location";

            var LeftHearingAidName = SelectHearingAid.GetLeftHearingAid();
            var RightHearingAidName = SelectHearingAid.GetRightHearingAid();

            Assert.IsTrue(new IntroPageOne().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Pass, "Intro page loaded");

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(LeftHearingAidName, RightHearingAidName).Page;

            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + LeftHearingAidName + "' and Right '" + RightHearingAidName + "'");
            ReportHelper.LogTest(Status.Info, "Dashboard page loaded");

            int programsCount = dashboardPage.GetNumberOfPrograms();

            ReportHelper.LogTest(Status.Info, "Checking programs in dashboard page...");
            for (int i = 0; i < programsCount; i++)
            {
                var dashboardPagePrograms = new DashboardPage().SelectProgram(i);
                Thread.Sleep(1000);
                Assert.IsNotEmpty(dashboardPagePrograms.GetCurrentProgramName());
                Assert.IsTrue(dashboardPagePrograms.GetIsProgramSelected(i), $"Failed to select a hearing program {i}.");
            }
            ReportHelper.LogTest(Status.Info, "Programs verified in dashboard page");

            ReportHelper.LogTest(Status.Info, "Checking programs in main menu page...");
            int programsCountMenu = 0;
            for (int i = 0; i < programsCount; i++)
            {
                programsCountMenu++;
                if (i == 3)
                    new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Streaming, 0);
                else
                    new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Preset, i);
                Thread.Sleep(2000);
                Assert.IsNotEmpty(new ProgramDetailPage().GetCurrentProgramName());
                new ProgramDetailPage().TapBack();
            }
            Assert.AreEqual(programsCount, programsCountMenu, "Programs count does not match");
            ReportHelper.LogTest(Status.Info, "Programs verified in main menu page");

            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 0);

            ReportHelper.LogTest(Status.Info, "Checking programs in program details page...");
            int programsCountProgramDetails = 0;
            for (int i = 0; i < programsCount; i++)
            {
                programsCountProgramDetails++;
                var programDetailPageCurrent = new ProgramDetailPage().SelectProgram(i);
                Thread.Sleep(1000);
                Assert.IsNotEmpty(programDetailPageCurrent.GetCurrentProgramName());
            }
            Assert.AreEqual(programsCount, programsCountProgramDetails, "Programs count does not match");
            new ProgramDetailPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Programs verified in program details page");
            ReportHelper.LogTest(Status.Pass, "Programs selected from dashboard, main menu and slider inside program details page. The app did not crash");

            ReportHelper.LogTest(Status.Info, "Changing program via rocker switch and checking...");
            string currentProgramName = new DashboardPage().GetCurrentProgramName();
            ReportHelper.LogTest(Status.Info, "Program selected before pressing rocker switch is '" + currentProgramName + "'");

            ReportHelper.LogTest(Status.Info, "ALERT: Change the program via rocker switch within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Program changed via rocker switch");

            Assert.AreNotEqual(currentProgramName, new DashboardPage().GetCurrentProgramName(), "Program not changed");
            ReportHelper.LogTest(Status.Info, "Program selected after pressing rocker switch is '" + new DashboardPage().GetCurrentProgramName() + "'");
            ReportHelper.LogTest(Status.Pass, "Program changed via rocker switch and app did not crash");

            ReportHelper.LogTest(Status.Info, "Changing volume via rocker switch and checking...");
            double Volume = new DashboardPage().GetVolumeSliderValue();
            ReportHelper.LogTest(Status.Info, "Volume before pressing rocker switch is '" + Volume + "'");

            ReportHelper.LogTest(Status.Info, "ALERT: Change the volume via rocker switch for both left and right hearing system within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Volume changed via rocker switch for both left and right hearing system");

            double currentVolume = new DashboardPage().GetVolumeSliderValue();
            ReportHelper.LogTest(Status.Info, "Volume after pressing rocker switch is '" + currentVolume + "'");
            Assert.AreNotEqual(Volume, currentVolume, "Volume not changed");
            ReportHelper.LogTest(Status.Pass, "Volume changed via rocker switch for both left and right hearing system and app did not crash");

            ReportHelper.LogTest(Status.Info, "Checking if volume changed in dashboard and the volume inside binaural setting the are matching...");
            new DashboardPage().IncreaseVolume();
            new DashboardPage().IncreaseVolume();
            currentVolume = new DashboardPage().GetVolumeSliderValue();

            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 0);
            ReportHelper.LogTest(Status.Info, "Openned Basic Program");
            new ProgramDetailPage().OpenBinauralSettings();
            var programDetailParamEditBinauralPage = new ProgramDetailParamEditBinauralPage();

            var singleVolumeValue = programDetailParamEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Single);
            Assert.AreEqual(currentVolume, singleVolumeValue, "Volume changed in dashboard and the volume inside binaural setting the are not matching");
            ReportHelper.LogTest(Status.Info, "Volume changed in dashboard and the volume inside binaural setting are matching");
            programDetailParamEditBinauralPage.TurnOnBinauralSeparation();
            Assert.AreEqual(currentVolume, programDetailParamEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Left), "Volume changed in dashboard and the left volume inside binaural setting the are not matching");
            Assert.AreEqual(currentVolume, programDetailParamEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Right), "Volume changed in dashboard and the right volume inside binaural setting the are not matching");
            ReportHelper.LogTest(Status.Info, "Volume changed in dashboard and the volume for left and right inside binaural setting are matching");
            programDetailParamEditBinauralPage.TurnOffBinauralSeparation();

            ReportHelper.LogTest(Status.Info, "Checking if volume changed in single matches with right and left...");
            programDetailParamEditBinauralPage.IncreaseVolume(VolumeChannel.Single);
            programDetailParamEditBinauralPage.IncreaseVolume(VolumeChannel.Single);
            var currtntSingleVolumeValue = programDetailParamEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Single);
            programDetailParamEditBinauralPage.TurnOnBinauralSeparation();
            Assert.AreEqual(currtntSingleVolumeValue, programDetailParamEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Left), "Volume changed single mode and the left volume inside binaural setting the are not matching");
            Assert.AreEqual(currtntSingleVolumeValue, programDetailParamEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Right), "Volume changed via single mode and the right volume inside binaural setting the are not matching");
            programDetailParamEditBinauralPage.TurnOffBinauralSeparation();
            ReportHelper.LogTest(Status.Info, "Volume changed in single matches with right and left in binaural setting page");

            programDetailParamEditBinauralPage.Close();
            new ProgramDetailPage().TapBack();
            Assert.AreEqual(currtntSingleVolumeValue, new DashboardPage().GetVolumeSliderValue(), "Volume not changed in dashboard page");
            ReportHelper.LogTest(Status.Pass, "Volume changed in single matches with right and left in binaural setting page and app did not crash");

            ReportHelper.LogTest(Status.Info, "Changing the paremeter of Music program and checking...");
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 2);
            ReportHelper.LogTest(Status.Info, "Openned Music Program");

            ReportHelper.LogTest(Status.Info, "Changing and checking Speech Focus...");
            new ProgramDetailPage().SpeechFocusDisplay.OpenSettings();
            var speechFocusValue = SpeechFocus.Off;
            new ProgramDetailParamEditSpeechFocusPage().SelectSpeechFocus(speechFocusValue);
            new ProgramDetailParamEditSpeechFocusPage().Close();
            Assert.AreEqual(speechFocusValue.ToString(), new ProgramDetailPage().SpeechFocusDisplay.GetValue());
            ReportHelper.LogTest(Status.Info, "Verified Speech Focus");

            ReportHelper.LogTest(Status.Info, "Changing and checking Noise Reduction...");
            new ProgramDetailPage().NoiseReductionDisplay.OpenSettings();
            var noiseReductionValue = NoiseReduction.Off;
            new ProgramDetailParamEditNoiseReductionPage().SelectNoiseReduction(noiseReductionValue);
            new ProgramDetailParamEditNoiseReductionPage().Close();
            Assert.AreEqual(noiseReductionValue.ToString(), new ProgramDetailPage().NoiseReductionDisplay.GetValue());
            ReportHelper.LogTest(Status.Info, "Verified Noise Reduction");
            ReportHelper.LogTest(Status.Pass, "Program parameters changed and app did not crash");

            ReportHelper.LogTest(Status.Info, "Changing Program settings and checking...");
            new ProgramDetailPage().OpenProgramSettings();
            var programDetailSettingsControlPage = new ProgramDetailSettingsControlPage();
            Assert.IsTrue(programDetailSettingsControlPage.GetIsCustomizeNameVisible());
            ReportHelper.LogTest(Status.Info, "Customize Name option is visible");
            Assert.IsTrue(programDetailSettingsControlPage.GetIsCustomizeIconVisible());
            ReportHelper.LogTest(Status.Info, "Customize Icon option is visible");
            Assert.IsTrue(programDetailSettingsControlPage.GetIsCreateFavoriteVisible());
            ReportHelper.LogTest(Status.Info, "Create Hearing Program option is visible");

            ReportHelper.LogTest(Status.Info, "Changing Program name and checking...");
            programDetailSettingsControlPage.CustomizeName();
            string ProgramName = "Music Edited";
            var programNamePage = new ProgramNamePage();
            programNamePage.EnterName(ProgramName);
            programNamePage.Proceed();
            ReportHelper.LogTest(Status.Info, "Program name changed from 'Music' to " + ProgramName);
            Assert.AreEqual(new ProgramDetailPage().GetCurrentProgramName(), ProgramName, "Open program is not '" + ProgramName + "'");
            ReportHelper.LogTest(Status.Info, "Verified program name after edit");

            ReportHelper.LogTest(Status.Info, "Changing Program icon and checking...");
            new ProgramDetailPage().OpenProgramSettings();
            programDetailSettingsControlPage = new ProgramDetailSettingsControlPage();
            programDetailSettingsControlPage.CustomizeIcon();
            var programIconPage = new ProgramIconPage();
            programIconPage.SelectIcon(1);
            programIconPage.Proceed();
            ReportHelper.LogTest(Status.Info, "Program icon changed to Second Icon and verified");

            new ProgramDetailPage().TapBack();
            ReportHelper.LogTest(Status.Pass, "Program settings edited and app did not crash");

            ReportHelper.LogTest(Status.Info, "Checking Help menu...");
            new DashboardPage().OpenMenuUsingTap();
            new MainMenuPage().OpenHelp();

            ReportHelper.LogTest(Status.Info, "Checking Instructions for use page...");
            new HelpMenuPage().OpenInstructionsForUse();
            Assert.IsNotEmpty(new InstructionsForUsePage().GetNavigationBarTitle());
            new InstructionsForUsePage().TapBack();
            ReportHelper.LogTest(Status.Info, "Instructions for use page verified");

            ReportHelper.LogTest(Status.Info, "Checking Information menu...");
            new HelpMenuPage().OpenInformationMenu();
            CheckMenuItemsTitle(new InformationMenuPage());
            new InformationMenuPage().NavigateBack();
            ReportHelper.LogTest(Status.Info, "Information menu verified");

            ReportHelper.LogTest(Status.Info, "Checking Help topics menu...");
            new HelpMenuPage().OpenHelpTopics();
            CheckMenuItemsTitle(new HelpTopicsPage());
            new HelpTopicsPage().NavigateBack();
            ReportHelper.LogTest(Status.Info, "Help topics menu verified");

            ReportHelper.LogTest(Status.Info, "Checking Imprint page...");
            new HelpMenuPage().OpenImprint();
            Assert.IsNotEmpty(new ImprintPage().GetNavigationBarTitle());
            new ImprintPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Imprint page verified");

            ReportHelper.LogTest(Status.Info, "Checking Find hearing systems page...");
            new HelpMenuPage().OpenFindHearingDevices();
            var findDevicesPage = new FindDevicesPage();
            Assert.IsTrue(findDevicesPage.GetIsMapViewSelected());
            Assert.IsTrue(findDevicesPage.GetIsLeftDeviceSelected());
            Assert.IsTrue(findDevicesPage.GetIsHearingAidVisibleOnMap(LeftHearingAidName, Side.Left.ToString()));
            Assert.IsNotEmpty(findDevicesPage.GetToggleViewButtonText());
            Assert.IsNotEmpty(findDevicesPage.GetRightDeviceText());
            findDevicesPage.SelectRightDevice();
            Assert.IsTrue(findDevicesPage.GetIsRightDeviceSelected());
            Assert.IsTrue(findDevicesPage.GetIsHearingAidVisibleOnMap(RightHearingAidName, Side.Right.ToString()));
            findDevicesPage.SelectLeftDevice();
            findDevicesPage.SelectNearFieldView();
            Assert.IsTrue(findDevicesPage.GetIsNearFieldViewSelected());
            findDevicesPage.SelectRightDevice();
            findDevicesPage.SelectLeftDevice();
            findDevicesPage.SelectMapView();
            findDevicesPage.TapBack();
            new HelpMenuPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Find hearing systems page verified");
            ReportHelper.LogTest(Status.Pass, "Help menu verified and app did not crash");

            ReportHelper.LogTest(Status.Info, "Checking Settings menu page...");
            new MainMenuPage().OpenSettings();
            var settingsMenuPage = new SettingsMenuPage();
            ReportHelper.LogTest(Status.Info, "Checking development stuff...");
            settingsMenuPage.ShowDevelopmentStuff();
            Assert.IsTrue(settingsMenuPage.GetIsDevelopmentStuffVisible());
            Assert.IsNotEmpty(settingsMenuPage.GetAppResetText());
            Assert.IsNotEmpty(settingsMenuPage.GetConnectionErrorPageText());
            Assert.IsNotEmpty(settingsMenuPage.GetHardwareErrorPageText());
            Assert.IsNotEmpty(settingsMenuPage.GetLogsText());
            ReportHelper.LogTest(Status.Info, "Developement stuff is visible successfully and verified");

            ReportHelper.LogTest(Status.Info, "Checking Language page...");
            settingsMenuPage.OpenLanguage();
            Assert.IsTrue(new SettingLanguagePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)));
            ReportHelper.LogTest(Status.Info, "Language page verified");
            new SettingLanguagePage().TapBack();

            ReportHelper.LogTest(Status.Info, "Checking App Selection page...");
            new SettingsMenuPage().OpenDemoMode();
            Assert.IsTrue(new AppModeSelectionPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)));
            ReportHelper.LogTest(Status.Info, "App Selection page verified");
            new AppModeSelectionPage().TapBack();

            ReportHelper.LogTest(Status.Info, "Checking My Hearing System page...");
            new SettingsMenuPage().OpenMyHearingAids();
            new HearingSystemManagementPage().CheckHAInformationFromSettings(AppMode.Normal, Side.Left, Side.Right).TapBack();
            ReportHelper.LogTest(Status.Info, "My Hearing System page verified");

            ReportHelper.LogTest(Status.Pass, "Settings menu verified and app did not crash");

            ReportHelper.LogTest(Status.Info, "Checking of disabling and enabling track hearing system permessions...");
            new SettingsMenuPage().OpenPermissions();
            new SettingPermissionsPage().TurnOffLocationPermission();
            Assert.IsFalse(new SettingPermissionsPage().GetIsLocationPermissionSwitchChecked());
            ReportHelper.LogTest(Status.Info, "Track hearing system permessions disabled");
            new SettingPermissionsPage().TapBack();
            new SettingsMenuPage().TapBack();
            new MainMenuPage().OpenHelp();
            new HelpMenuPage().OpenFindHearingDevices();
            Assert.IsTrue(DialogHelper.GetIsDialogDisplayed(TimeSpan.FromSeconds(2)), "Permission dialog not displayed");
            ReportHelper.LogTest(Status.Info, "Confirmation is visible after disabling Track hearing system permessions");
            var appDialog = new AppDialog();
            Assert.IsNotEmpty(appDialog.GetTitle());
            Assert.IsNotEmpty(appDialog.GetMessage());
            Assert.AreEqual(appDialog.GetDenyButtonText().ToUpper(), LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "gblNotification_ActivateGeoTrackingCancel").ToUpper());
            Assert.AreEqual(appDialog.GetConfirmButtonText().ToUpper(), LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "gblNotification_ActivateGeoTrackingOk").ToUpper());
            ReportHelper.LogTest(Status.Info, "Confirmation dialog verified");
            appDialog.Confirm();
            ReportHelper.LogTest(Status.Info, "Track hearing system permissions enabled");
            Assert.IsTrue(new FindDevicesPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)));
            ReportHelper.LogTest(Status.Info, "Find hearing system page visible");
            new FindDevicesPage().TapBack();
            new HelpMenuPage().TapBack();
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenPermissions();
            ReportHelper.LogTest(Status.Info, "Checking if track hearing system permissions is enabled...");
            Assert.IsTrue(new SettingPermissionsPage().GetIsLocationPermissionSwitchChecked(), "Track hearing system permissions not enabled");
            ReportHelper.LogTest(Status.Info, "Track hearing system permissions enabled");
            new SettingPermissionsPage().TapBack();
            new SettingsMenuPage().TapBack();
            new MainMenuPage().CloseMenuUsingTap();
            ReportHelper.LogTest(Status.Pass, "Disabling and enabling track hearing system permessions verified");

            ReportHelper.LogTest(Status.Info, "Creating favorite for Audio Streaming program...");
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Streaming, 0);
            new ProgramDetailPage().OpenProgramSettings();
            ReportHelper.LogTest(Status.Info, "Creating wifi based favorite...");
            new ProgramDetailSettingsControlPage().CreateFavorite();
            new ProgramNamePage().EnterName(favoriteWithWifi);
            new ProgramNamePage().Proceed();
            new ProgramIconPage().SelectIcon(5);
            new ProgramIconPage().Proceed();
            new ProgramAutomationPage().TurnOnAutomation();
            ReportHelper.LogTest(Status.Info, "Checking back navigation...");
            new ProgramAutomationPage().TapBack();
            new ProgramIconPage().TapBack();
            new ProgramNamePage().TapBack();
            DialogHelper.DenyIfDisplayed();
            new ProgramNamePage().EnterName(favoriteWithWifi);
            new ProgramNamePage().Proceed();
            new ProgramIconPage().SelectIcon(5);
            new ProgramIconPage().Proceed();
            new ProgramAutomationPage().TurnOnAutomation();
            ReportHelper.LogTest(Status.Info, "Back navigation verified");
            // Grant Full Access
            AppManager.DeviceSettings.GrantGPSBackgroundPermission();
            Thread.Sleep(5000);
            ReportHelper.LogTest(Status.Info, "'Always allow permission granted'");
            new ProgramAutomationPage().TapConnectToWiFi();
            new AutomationWifiBindingPage().Ok();
            new ProgramAutomationPage().Proceed();
            Assert.AreEqual(favoriteWithWifi, new ProgramDetailPage().GetCurrentProgramName(), "Wifi based favorite not created");
            ReportHelper.LogTest(Status.Info, "Wifi based favorite created and verified");
            new ProgramDetailPage().TapBack();

            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Streaming, 0);
            new ProgramDetailPage().OpenProgramSettings();
            ReportHelper.LogTest(Status.Info, "Creating location based favorite...");
            new ProgramDetailSettingsControlPage().CreateFavorite();
            new ProgramNamePage().EnterName(favoriteWithLocation);
            new ProgramNamePage().Proceed();
            new ProgramIconPage().SelectIcon(6);
            new ProgramIconPage().Proceed();
            new ProgramAutomationPage().TurnOnAutomation();
            new ProgramAutomationPage().TapConnectToLocation();
            // As after granting 'Allow always' permission need to reopen Map to select location
            Thread.Sleep(3000);
            new AutomationGeofenceBindingPage().TapBack();
            new ProgramAutomationPage().TapConnectToLocation();
            Thread.Sleep(3000); // Wait time needed for map to load
            new AutomationGeofenceBindingPage().WaitUntilNoLoadingIndicator().SelectPosition(0.5, 0.5).Ok();
            new ProgramAutomationPage().Proceed();
            Assert.AreEqual(favoriteWithLocation, new ProgramDetailPage().GetCurrentProgramName(), "Location based favorite not created");
            ReportHelper.LogTest(Status.Info, "Location based favorite created and verified");
            new ProgramDetailPage().TapBack();

            AppManager.RestartApp(false);
            ReportHelper.LogTest(Status.Info, "App restarted with app data");

            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            dashboardPage = new DashboardPage();
            Thread.Sleep(3000); //For Synchronization
            //If location are wifi both are available then autostart with wifi will be given priority
            Assert.AreEqual(favoriteWithWifi, dashboardPage.GetCurrentProgramName(), "Auto start with wifi program failed");
            Assert.Greater(dashboardPage.GetNumberOfPrograms(), programsCount, "Program count did not increase after creation of favorite");
            ReportHelper.LogTest(Status.Pass, "Created wifi and location based favorite for Audio Streaming program and is verified including auto start");

            int AndroidVersion = 0;
            if (OnAndroid)
                AndroidVersion = Convert.ToInt32(AppManager.DeviceSettings.GetDeviceOSVersion());

            // Ask every time option is available only for Android version greater than Android 10. Hence Step 15, 16 and 17 has been skipped for Android 10
            // For Android 11 and iOS the behaviour is currently different and hence a bug needs to be created.  
            if (OniOS || AndroidVersion > 10)
            {
                AppManager.CloseApp();
                ReportHelper.LogTest(Status.Info, "App closed");

                ReportHelper.LogTest(Status.Info, "ALERT: Change the location permession to 'Ask every time' for " + AppManager.Brand + " app within 15 seconds");
                Thread.Sleep(15000);
                ReportHelper.LogTest(Status.Info, "Location permession changed to 'Ask every time' for " + AppManager.Brand + " app");

                AppManager.StartApp(false);
                ReportHelper.LogTest(Status.Info, "App started with app data");

                // ToDo: Currently this confirmation dialog does not appear for iOS
                Assert.IsTrue(DialogHelper.GetIsDialogDisplayed(TimeSpan.FromSeconds(40)), "Dialog with OK not displayed");
                Assert.AreEqual(new AppDialog().GetConfirmButtonText().ToUpper(), LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "gblNotification_GeneralOK").ToUpper());
                new AppDialog().Confirm();
                ReportHelper.LogTest(Status.Info, "Pressed OK");

                Assert.IsNotEmpty(new PermissionDialog().GetAllowText());
                Assert.IsNotEmpty(new PermissionDialog().GetDenyText());
                PermissionHelper.DenyPermissionIfRequested();
                ReportHelper.LogTest(Status.Info, "Location Permission Denied");

                // ToDo: Currently this confirmation dialog does not appear for Android 11 and iOS
                Assert.IsTrue(DialogHelper.GetIsDialogDisplayed(TimeSpan.FromSeconds(2)), "Dialog with CANCEL and SETTINGS not displayed");
                Assert.AreEqual(new AppDialog().GetConfirmButtonText().ToUpper(), LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "bt_Settings").ToUpper());
                Assert.AreEqual(new AppDialog().GetDenyButtonText().ToUpper(), LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "bt_Cancel").ToUpper());
                new AppDialog().Deny();
                ReportHelper.LogTest(Status.Info, "Pressed CANCEL");
                AppManager.DeviceSettings.PutAppToBackground(2);
                // ToDo: Currently this confirmation dialog does not appear for iOS after putting the app in background and foreground
                Assert.IsTrue(DialogHelper.GetIsDialogDisplayed(TimeSpan.FromSeconds(2)), "Dialog with CANCEL and SETTINGS not displayed after putting the app in background and foreground");
                ReportHelper.LogTest(Status.Info, "App put in background and foreground dailog is visible again");
                new AppDialog().Confirm();
                ReportHelper.LogTest(Status.Info, "Pressed SETTINGS");
                Thread.Sleep(1000);

                if (OnAndroid)
                {
                    AppManager.DeviceSettings.GrantGPSPermission();
                    AppManager.DeviceSettings.GrantGPSBackgroundPermission();
                    Thread.Sleep(5000);
                    ReportHelper.LogTest(Status.Info, "'Always Allow' location permission granted");
                    AppManager.App.PressBackButton();
                }
                else
                {
                    ReportHelper.LogTest(Status.Info, "ALERT: Change the location permession to 'Always Allow' for " + AppManager.Brand + " app within 15 seconds and tap back to dashboard");
                    Thread.Sleep(15000);
                    ReportHelper.LogTest(Status.Info, "Location permession changed to 'Always Allow' for " + AppManager.Brand + " app and tapped back to dashboard");
                }

                Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
                Assert.IsFalse(DialogHelper.GetIsDialogDisplayed(TimeSpan.FromSeconds(2)), "Dialog again displayed with CANCEL and SETTINGS after granting permission");
                Thread.Sleep(3000); //For Synchronization
                Assert.AreEqual(favoriteWithWifi, new DashboardPage().GetCurrentProgramName(), "Auto start with wifi program failed");
                ReportHelper.LogTest(Status.Info, "Wifi based program has been selected as auto start program again in dashboard");
            }

            // Disable Bluetooth
            AppManager.DeviceSettings.DisableBluetooth();
            ReportHelper.LogTest(Status.Info, "Bluetooth disabled");
            Thread.Sleep(2000);
            // ToDo: For iOS when the stacked message is shown assert fails on AppDialog. Hence not asserting it.
            new AppDialog(false).Confirm();
            new AppDialog(false).Confirm();
            ReportHelper.LogTest(Status.Pass, "Bluetooth disabled and connection to Hearing Aid Intrupted and app does not crash");

            // Enable Bluetooth
            AppManager.DeviceSettings.EnableBluetooth();
            ReportHelper.LogTest(Status.Info, "Bluetooth enabled");
            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(25));
            dashboardPage = new DashboardPage();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            ReportHelper.LogTest(Status.Pass, "Bluetooth enabled and left and right hearing system are connected and visible");

            AppManager.DeviceSettings.DisableWifi();
            ReportHelper.LogTest(Status.Info, "Wifi disabled");

            ReportHelper.LogTest(Status.Info, "Creating favorite for Audio Streaming program after wifi is disabled...");
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Streaming, 0);
            new ProgramDetailPage().OpenProgramSettings();
            ReportHelper.LogTest(Status.Info, "Creating wifi based favorite...");
            new ProgramDetailSettingsControlPage().CreateFavorite();
            new ProgramNamePage().EnterName("New Wifi Favorite");
            new ProgramNamePage().Proceed();
            new ProgramIconPage().SelectIcon(5);
            new ProgramIconPage().Proceed();
            new ProgramAutomationPage().TurnOnAutomation();
            new ProgramAutomationPage().TapConnectToWiFi();
            Assert.IsTrue(DialogHelper.GetIsDialogDisplayed(TimeSpan.FromSeconds(2)), "Dialog not displayed for no wifi connection");
            DialogHelper.Confirm();
            ReportHelper.LogTest(Status.Info, "Dialog displayed for no wifi connection");
            new ProgramAutomationPage().CancelAndConfirm();
            new ProgramDetailSettingsControlPage().TapBack();
            new ProgramDetailPage().TapBack();
            ReportHelper.LogTest(Status.Pass, "Favorite for Audio Streaming program is not getting created after wifi is disabled and dialog appears");

            ReportHelper.LogTest(Status.Info, "Deleting favorites...");
            ReportHelper.LogTest(Status.Info, "Deleting wifi based favorite...");
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 0);
            new ProgramDetailPage().OpenProgramSettings();
            new ProgramDetailSettingsControlPage().DeleteFavoriteAndConfirm();
            ReportHelper.LogTest(Status.Info, "Wifi based favorite deleted");
            new ProgramDetailPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Deleting location based favorite...");
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 0);
            new ProgramDetailPage().OpenProgramSettings();
            new ProgramDetailSettingsControlPage().DeleteFavoriteAndConfirm();
            ReportHelper.LogTest(Status.Info, "Location based favorite deleted");
            new ProgramDetailPage().TapBack();
            Assert.AreEqual(dashboardPage.GetNumberOfPrograms(), programsCount, "Program count did not reduce after deletion of favorite");
            ReportHelper.LogTest(Status.Pass, "All the favorites deleted and program count reduced");

            AppManager.RestartApp(false);
            ReportHelper.LogTest(Status.Info, "App restarted with app data");

            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            Assert.AreNotEqual(favoriteWithWifi, dashboardPage.GetCurrentProgramName(), "Auto start with wifi program loaded");
            ReportHelper.LogTest(Status.Pass, "Wifi based program not available and also not selected");

            ReportHelper.LogTest(Status.Info, "Disconnecting hearing system from settings menu");
            new DashboardPage().OpenMenuUsingTap();
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenMyHearingAids();
            new HearingSystemManagementPage().DisconnectDevices();
            Assert.IsTrue(DialogHelper.GetIsDialogDisplayed(TimeSpan.FromSeconds(2)), "Disconnection dialog not displayed");
            Assert.IsNotEmpty(new AppDialog().GetTitle());
            Assert.IsNotEmpty(new AppDialog().GetMessage());
            Assert.AreEqual(new AppDialog().GetDenyButtonText().ToUpper(), LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "gblNotification_ActivateGeoTrackingCancel").ToUpper());
            Assert.AreEqual(new AppDialog().GetConfirmButtonText().ToUpper(), LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "dialog_disconnect_hearingaids_btn_disconnect").ToUpper());
            new AppDialog().Deny();
            ReportHelper.LogTest(Status.Info, "Pressed Deny");
            Assert.IsTrue(new HearingSystemManagementPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)));
            ReportHelper.LogTest(Status.Info, "Still in My hearing systems page");
            new HearingSystemManagementPage().DisconnectDevices();
            DialogHelper.ConfirmIfDisplayed();
            ReportHelper.LogTest(Status.Info, "Pressed Accept");
            Assert.IsTrue(new InitializeHardwarePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Info, "Hardware init page loaded");
            ReportHelper.LogTest(Status.Pass, "Hearing system diaconnected from settings menu");

            void CheckMenuItemsTitle(BaseSubMenuPage menuPage)
            {
                ReportHelper.LogTest(Status.Info, "Checking " + menuPage.GetType().Name + "...");
                var menuItemsList = menuPage.MenuItems.GetAllVisible();
                for (int i = 0; i < menuItemsList.Count; i++)
                {
                    Thread.Sleep(500);
                    menuPage.MenuItems.Open(i, IndexType.Relative);
                    Thread.Sleep(500);
                    Assert.AreEqual(menuItemsList[i], menuPage.GetNavigationBarTitle());
                    ReportHelper.LogTest(Status.Info, "On " + menuPage.GetNavigationBarTitle() + " Page");
                    Thread.Sleep(2000);
                    menuPage.NavigateBack();
                }
                ReportHelper.LogTest(Status.Info, "All menu items checked for " + menuPage.GetType().Name);
            }
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-24095_Table-0")]
        public void ST24095_VerifyDashboardUIBackground()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            string LeftHearingAidName = SelectHearingAid.GetLeftHearingAid();
            string RightHearingAidName = SelectHearingAid.GetRightHearingAid();

            // Navigate throught to intro and start the Demo mode
            new IntroPageOne().MoveRightBySwiping();
            new IntroPageTwo().MoveRightBySwiping();
            new IntroPageThree().MoveRightBySwiping();
            new IntroPageFour().MoveRightBySwiping();
            Assert.IsTrue(new IntroPageFive().GetIsLeftButtonVisible());
            new IntroPageFive().Continue();
            ReportHelper.LogTest(Status.Info, "Into Pages skipped");

            Assert.IsTrue(new InitializeHardwarePage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));

            new InitializeHardwarePage().StartScan();
            ReportHelper.LogTest(Status.Info, "Performed Start Scan");

            DialogHelper.ConfirmIfDisplayed();
            PermissionHelper.AllowPermissionIfRequested();

            var selectHearingAidsPage = new SelectHearingAidsPage();

            selectHearingAidsPage.WaitUntilDeviceFound(LeftHearingAidName);
            if (!selectHearingAidsPage.GetIsDeviceFound(LeftHearingAidName))
                selectHearingAidsPage.WaitUntilDeviceFound(LeftHearingAidName);

            if (RightHearingAidName != null)
                selectHearingAidsPage.WaitUntilDeviceFound(RightHearingAidName);
            if (!selectHearingAidsPage.GetIsDeviceFound(RightHearingAidName))
                selectHearingAidsPage.WaitUntilDeviceFound(RightHearingAidName);

            selectHearingAidsPage.WaitUntilDeviceListNotChanging();
            selectHearingAidsPage.SelectDevicesExclusively(LeftHearingAidName, RightHearingAidName);
            selectHearingAidsPage.GetIsDeviceSelected(LeftHearingAidName);
            if (RightHearingAidName != null)
                selectHearingAidsPage.GetIsDeviceSelected(RightHearingAidName);

            selectHearingAidsPage.Connect();
            ReportHelper.LogTest(Status.Info, "Connect button clicked");

            AppManager.DeviceSettings.PutAppToBackground(90);
            ReportHelper.LogTest(Status.Info, "putting the app in backgroung for 90 secs");

            DialogHelper.ConfirmIfDisplayed();
            PermissionHelper.AllowPermissionIfRequested();

            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)));
            var dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "Dashboard page loaded");

            int expectedProgramCount = 4;
            ReportHelper.LogTest(Status.Info, "Checking if all " + expectedProgramCount + " programs are visible in dashboard...");
            Assert.AreEqual(expectedProgramCount, dashboardPage.GetNumberOfPrograms(), "All " + expectedProgramCount + " programs not visible in dashboard");
            ReportHelper.LogTest(Status.Pass, "All " + expectedProgramCount + " programs visible in dashboard");
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-24124_Table-0")]
        public void ST24124_VerifyBatteryLevelTranslation()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            string LeftHearingAidName = SelectHearingAid.GetLeftHearingAid();
            string RightHearingAidName = SelectHearingAid.GetRightHearingAid();

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(LeftHearingAidName, RightHearingAidName).Page;

            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + LeftHearingAidName + "' and Right '" + RightHearingAidName + "'");
            ReportHelper.LogTest(Status.Info, "Dashboard page loaded");

            ReportHelper.LogTest(Status.Pass, "Checking Loading Level text in " + Language_Audifon.English + " language...");
            new DashboardPage().OpenLeftHearingDevice();
            Assert.AreEqual(new HearingInstrumentInfoControlPage().GetBatteryLevelTitle(), LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "device_BatteryLevelValue"));
            Assert.IsNotEmpty(new HearingInstrumentInfoControlPage().GetBatteryLevelValue());
            new HearingInstrumentInfoControlPage().Close();
            new DashboardPage().OpenRightHearingDevice();
            Assert.AreEqual(new HearingInstrumentInfoControlPage().GetBatteryLevelTitle(), LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "device_BatteryLevelValue"));
            Assert.IsNotEmpty(new HearingInstrumentInfoControlPage().GetBatteryLevelValue());
            new HearingInstrumentInfoControlPage().Close();
            ReportHelper.LogTest(Status.Pass, "Loading Level text verified in " + Language_Audifon.English + " language");

            new DashboardPage().OpenMenuUsingTap();
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenLanguage();

            Assert.IsTrue(new SettingLanguagePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Info, "Language Menu opened");
            new SettingLanguagePage().SelectLanguageAudifon(Language_Audifon.Spanish);
            new SettingLanguagePage().Accept();
            new AppDialog().Confirm();
            ReportHelper.LogTest(Status.Info, "App language changed to " + Language_Audifon.Spanish);
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Pass, "Checking Loading Level text in " + Language_Audifon.Spanish + " language...");
            new DashboardPage().OpenLeftHearingDevice();
            Assert.AreEqual(new HearingInstrumentInfoControlPage().GetBatteryLevelTitle(), LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.Spanish, "device_BatteryLevelValue"));
            Assert.IsNotEmpty(new HearingInstrumentInfoControlPage().GetBatteryLevelValue());
            new HearingInstrumentInfoControlPage().Close();
            new DashboardPage().OpenRightHearingDevice();
            Assert.AreEqual(new HearingInstrumentInfoControlPage().GetBatteryLevelTitle(), LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.Spanish, "device_BatteryLevelValue"));
            Assert.IsNotEmpty(new HearingInstrumentInfoControlPage().GetBatteryLevelValue());
            new HearingInstrumentInfoControlPage().Close();
            ReportHelper.LogTest(Status.Pass, "Loading Level text verified in " + Language_Audifon.Spanish + " language");

            new DashboardPage().OpenMenuUsingTap();
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenLanguage();

            Assert.IsTrue(new SettingLanguagePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Info, "Language Menu opened");
            new SettingLanguagePage().SelectLanguageAudifon(Language_Audifon.Portuguese);
            new SettingLanguagePage().Accept();
            new AppDialog().Confirm();
            ReportHelper.LogTest(Status.Info, "App language changed to " + Language_Audifon.Portuguese);
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Pass, "Checking Loading Level text in " + Language_Audifon.Portuguese + " language...");
            new DashboardPage().OpenLeftHearingDevice();
            Assert.AreEqual(new HearingInstrumentInfoControlPage().GetBatteryLevelTitle(), LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.Portuguese, "device_BatteryLevelValue"));
            Assert.IsNotEmpty(new HearingInstrumentInfoControlPage().GetBatteryLevelValue());
            new HearingInstrumentInfoControlPage().Close();
            new DashboardPage().OpenRightHearingDevice();
            Assert.AreEqual(new HearingInstrumentInfoControlPage().GetBatteryLevelTitle(), LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.Portuguese, "device_BatteryLevelValue"));
            Assert.IsNotEmpty(new HearingInstrumentInfoControlPage().GetBatteryLevelValue());
            new HearingInstrumentInfoControlPage().Close();
            ReportHelper.LogTest(Status.Pass, "Loading Level text verified in " + Language_Audifon.Portuguese + " language");
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-24153_Table-0")]
        public void ST24153_VerifyHardwareErrorPageTranslation()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            // Accept or Deny for Find Neaby device permission dialog appears only for Android 12 after we click on Start Scan. Hence this test case will be executed only for Android 12
            if (OnAndroid)
            {
                int AndroidVersion = 0;
                AndroidVersion = Convert.ToInt32(AppManager.DeviceSettings.GetDeviceOSVersion());
                if (AndroidVersion == 12)
                {
                    CheckHardwareErrorPage(Language_Device.Spanish_Spain, Language_Audifon.Spanish);
                    CheckHardwareErrorPage(Language_Device.Portuguese_Portugal, Language_Audifon.Portuguese);
                    AppManager.DeviceSettings.ChangeDeviceLanguage(Language_Device.English_US);
                    AppManager.RestartApp(true);
                }
            }

            void CheckHardwareErrorPage(Language_Device language_Device, Language_Audifon language_Audifon)
            {
                AppManager.DeviceSettings.ChangeDeviceLanguage(language_Device);

                AppManager.RestartApp(true);

                LaunchHelper.VerifyIntroPages();

                Assert.IsTrue(new InitializeHardwarePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
                new InitializeHardwarePage().StartScan();
                ReportHelper.LogTest(Status.Info, "Performed Start Scan");

                // Deny Find Neaby device Permession
                DialogHelper.ConfirmIfDisplayed();
                PermissionHelper.DenyPermissionIfRequested();
                ReportHelper.LogTest(Status.Info, "Find Neaby device permission denied");

                Assert.IsTrue(new HardwareErrorPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Hardware Error Page not loaded");
                ReportHelper.LogTest(Status.Info, "Hardware Error Page loaded");

                // Texts for the keys error_Hardware_NoHearingAidDiscoveryPermissionTitle, error_Hardware_ResolutionAllowAccessNearbyDevicesTitle and error_Hardware_ResolutionAllowAccessNearbyDevicesMessage needs to be verified are available in HardwareErrorPage for which Deny has to be clicked.
                ReportHelper.LogTest(Status.Info, "Checking texts in HardwareErrorPage in " + language_Audifon + " language...");
                Assert.AreEqual(new HardwareErrorPage().GetPageTitle(), LanguageHelper.GetResourceTextByKey(AppManager.Brand, language_Audifon, "error_Hardware_NoHearingAidDiscoveryPermissionTitle"));
                Assert.AreEqual(new HardwareErrorPage().GetTitleOfItem(0), LanguageHelper.GetResourceTextByKey(AppManager.Brand, language_Audifon, "error_Hardware_ResolutionAllowAccessNearbyDevicesTitle"));
                Assert.AreEqual(new HardwareErrorPage().GetMessageOfItem(0), LanguageHelper.GetResourceTextByKey(AppManager.Brand, language_Audifon, "error_Hardware_ResolutionAllowAccessNearbyDevicesMessage"));
                ReportHelper.LogTest(Status.Pass, "Texts in HardwareErrorPage verified in " + language_Audifon + " language");

                AppManager.CloseApp();
            }
        }

        #endregion Sprint 34

        #region Sprint 13_1.5.0

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-24352_Table-0")]
        public void ST24352_StartScanAfterDisconnectWithLocationFavorite()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            var firstHearingAid = SelectHearingAid.GetLeftHearingAid();
            var secondHearingAid = SelectHearingAid.GetRightHearingAid();

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(firstHearingAid, secondHearingAid).Page;
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + firstHearingAid + "' and Right '" + secondHearingAid + "'");
            dashboardPage.CheckStartView(dashboardPage);
            ReportHelper.LogTest(Status.Info, "App started in device mode after skipping intro pages and dashboard is loaded");
            int numberOfProgramsOnDashboard = dashboardPage.GetNumberOfPrograms();
            ReportHelper.LogTest(Status.Info, "Checking number of programs...");
            Assert.AreEqual(4, numberOfProgramsOnDashboard, "Number of programs is not 4");
            ReportHelper.LogTest(Status.Pass, "Number of programs is 4");

            ReportHelper.LogTest(Status.Info, "Opening wireless streaming program detail page from dashboard...");
            dashboardPage.OpenProgram(3);
            var programDetailPage = new ProgramDetailPage();
            Assert.IsTrue(programDetailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), "Expected to be on streaming program detail page but wasn't.");
            ReportHelper.LogTest(Status.Info, "Opened wireless streaming program detail page from dashboard");

            ReportHelper.LogTest(Status.Info, "Create a favorite program based on location...");
            string FavoriteName = "Favorite 01 Location";
            FavoriteHelper.CreateFavoriteHearingProgram(FavoriteName, 14);
            FavoriteHelper.SelectLocationAndCreateFavorite(FavoriteName, 0.5, 0.5);
            ReportHelper.LogTest(Status.Pass, "Favorite created with name '" + FavoriteName + "' based on location");

            ReportHelper.LogTest(Status.Info, "Checking if the number of programs is increased to 5...");
            Assert.AreEqual(5, new ProgramDetailPage().GetNumberOfVisibiblePrograms(), "The total number of hearing programs doesn't include the favorite for the base program.");
            ReportHelper.LogTest(Status.Pass, "Number of programs is increased to 5");

            ReportHelper.LogTest(Status.Info, "Tapping back from program details page...");
            new ProgramDetailPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Tapped back from program details page");

            ReportHelper.LogTest(Status.Info, "Selecting different program...");
            new DashboardPage().SelectProgram(1);
            ReportHelper.LogTest(Status.Info, "Selected different program");

            ReportHelper.LogTest(Status.Info, "Restarting the app with app data...");
            AppManager.RestartApp(false);
            ReportHelper.LogTest(Status.Info, "Restarted the app with app data");

            ReportHelper.LogTest(Status.Info, "Waiting till dashboard page is loaded...");
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)), "Dashboard page is not loaded");
            Thread.Sleep(3000); // Wait till the notification disappears. In iOS is causes the test case to fail
            dashboardPage = new DashboardPage();
            ReportHelper.LogTest(Status.Info, "Dashboard page is loaded after restart with app data");

            ReportHelper.LogTest(Status.Info, "Navigating to my hearing aid page from main menu in dashboard page using tap...");
            NavigationHelper.NavigateToSettingsMenu(dashboardPage).OpenMyHearingAids();
            var hearingSystemsPage = new HearingSystemManagementPage();
            ReportHelper.LogTest(Status.Info, "Navigated to my hearing aid page from main menu in dashboard page using tap");

            ReportHelper.LogTest(Status.Info, "Tapping left tab...");
            hearingSystemsPage.LeftDeviceTabClick();
            ReportHelper.LogTest(Status.Info, "Tapped left tab");
            ReportHelper.LogTest(Status.Info, "Checking if left tab is selected...");
            Assert.IsTrue(hearingSystemsPage.GetIsLeftTabSelected(), "Left tab is not selected");
            ReportHelper.LogTest(Status.Info, "Left tab is selected");
            ReportHelper.LogTest(Status.Info, "Checking left tab text...");
            Assert.IsNotEmpty(hearingSystemsPage.GetLeftTabText(), "Left tab text is empty");
            ReportHelper.LogTest(Status.Info, "Left tab text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking if left UDI is visible...");
            Assert.IsTrue(hearingSystemsPage.GetIsLeftUdiVisible(), "Left UDI is not visible");
            ReportHelper.LogTest(Status.Info, "Left UDI is visible");

            string leftName = hearingSystemsPage.GetLeftDeviceName();
            string leftSerial = hearingSystemsPage.GetLeftDeviceSerial();
            string leftState = hearingSystemsPage.GetLeftDeviceState();
            string leftType = hearingSystemsPage.GetLeftDeviceType();
            string leftNameTitle = hearingSystemsPage.GetLeftDeviceNameTitle();
            string leftTypeTitle = hearingSystemsPage.GetLeftDeviceTypeTitle();
            string leftSerialTitle = hearingSystemsPage.GetLeftDeviceSerialTitle();
            string leftStateTitle = hearingSystemsPage.GetLeftDeviceStateTitle();
            string leftUdiTitle = hearingSystemsPage.GetLeftDeviceUdiTitle();
            string leftUdi = hearingSystemsPage.GetLeftDeviceUdi();

            ReportHelper.LogTest(Status.Info, "Tapping right tab...");
            hearingSystemsPage.RightDeviceTabClick();
            ReportHelper.LogTest(Status.Info, "Tapped right tab");
            ReportHelper.LogTest(Status.Info, "Checking if right tab is selected...");
            Assert.IsTrue(hearingSystemsPage.GetIsRightTabSelected(), "Right tab is not selected");
            ReportHelper.LogTest(Status.Info, "Right tab is selected");
            ReportHelper.LogTest(Status.Info, "Checking right tab text...");
            Assert.IsNotEmpty(hearingSystemsPage.GetRightTabText(), "Right tab text is empty");
            ReportHelper.LogTest(Status.Info, "Right tab text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking if right UDI is visible...");
            Assert.IsTrue(hearingSystemsPage.GetIsRightUdiVisible(), "Right UDI is not visible");
            ReportHelper.LogTest(Status.Info, "Right UDI is visible");

            string rightName = hearingSystemsPage.GetRightDeviceName();
            string rightSerial = hearingSystemsPage.GetRightDeviceSerial();
            string rightState = hearingSystemsPage.GetRightDeviceState();
            string rightType = hearingSystemsPage.GetRightDeviceType();
            string rightNameTitle = hearingSystemsPage.GetRightDeviceNameTitle();
            string rightTypeTitle = hearingSystemsPage.GetRightDeviceTypeTitle();
            string rightSerialTitle = hearingSystemsPage.GetRightDeviceSerialTitle();
            string rightStateTitle = hearingSystemsPage.GetRightDeviceStateTitle();
            string rightUdiTitle = hearingSystemsPage.GetRightDeviceUdiTitle();
            string rightUdi = hearingSystemsPage.GetRightDeviceUdi();

            ReportHelper.LogTest(Status.Info, "Checking info of the left device...");
            ReportHelper.LogTest(Status.Info, "Checking left device name...");
            Assert.IsNotEmpty(leftName, "Left device name is empty");
            ReportHelper.LogTest(Status.Info, "Left device name is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left device serial...");
            Assert.IsNotEmpty(leftSerial, "Left device serial is empty");
            ReportHelper.LogTest(Status.Info, "Left device serial is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left device state...");
            Assert.IsNotEmpty(leftState, "Left device state is empty");
            ReportHelper.LogTest(Status.Info, "Left device state is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left device type...");
            Assert.IsNotEmpty(leftType, "Left device type is empty");
            ReportHelper.LogTest(Status.Info, "Left device type is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left device UDI...");
            Assert.IsNotEmpty(leftUdi, "Left device UDI is empty");
            ReportHelper.LogTest(Status.Info, "Left device UDI is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left device name title...");
            Assert.IsNotEmpty(leftNameTitle, "Left device name title is empty");
            ReportHelper.LogTest(Status.Info, "Left device name title is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left device serial title...");
            Assert.IsNotEmpty(leftSerialTitle, "Left device serial title is empty");
            ReportHelper.LogTest(Status.Info, "Left device serial title is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left device state title...");
            Assert.IsNotEmpty(leftStateTitle, "Left device state title is empty");
            ReportHelper.LogTest(Status.Info, "Left device state title is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left device type title...");
            Assert.IsNotEmpty(leftTypeTitle, "Left device type title is empty");
            ReportHelper.LogTest(Status.Info, "Left device type title is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left device UDI title...");
            Assert.IsNotEmpty(leftUdiTitle, "Left device UDI title is empty");
            ReportHelper.LogTest(Status.Info, "Left device UDI title is not empty");
            ReportHelper.LogTest(Status.Info, "Info of the left device is verified");

            ReportHelper.LogTest(Status.Info, "Checking info of the right device...");
            ReportHelper.LogTest(Status.Info, "Checking right device name...");
            Assert.IsNotEmpty(rightName, "Right device name is empty");
            ReportHelper.LogTest(Status.Info, "Right device name is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right device serial...");
            Assert.IsNotEmpty(rightSerial, "Right device serial is empty");
            ReportHelper.LogTest(Status.Info, "Right device serial is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right device state...");
            Assert.IsNotEmpty(rightState, "Right device state is empty");
            ReportHelper.LogTest(Status.Info, "Right device state is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right device type...");
            Assert.IsNotEmpty(rightType, "Right device type is empty");
            ReportHelper.LogTest(Status.Info, "Right device type is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right device UDI...");
            Assert.IsNotEmpty(rightUdi, "Right device UDI is empty");
            ReportHelper.LogTest(Status.Info, "Right device UDI is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right device name title...");
            Assert.IsNotEmpty(rightNameTitle, "Right device name title is empty");
            ReportHelper.LogTest(Status.Info, "Right device name title is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right device serial title...");
            Assert.IsNotEmpty(rightSerialTitle, "Right device serial title is empty");
            ReportHelper.LogTest(Status.Info, "Right device serial title is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right device state title...");
            Assert.IsNotEmpty(rightStateTitle, "Right device state title is empty");
            ReportHelper.LogTest(Status.Info, "Right device state title is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right device type title...");
            Assert.IsNotEmpty(rightTypeTitle, "Right device type title is empty");
            ReportHelper.LogTest(Status.Info, "Right device type title is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right device UDI title...");
            Assert.IsNotEmpty(rightUdiTitle, "Right device UDI title is empty");
            ReportHelper.LogTest(Status.Info, "Right device UDI title is not empty");
            ReportHelper.LogTest(Status.Info, "Info of the right device is verified");

            ReportHelper.LogTest(Status.Pass, "All info is displayed correctly for connected device and is verified");

            ReportHelper.LogTest(Status.Info, "Checking app dialog is displayed correctly with 'Cancel' and 'Confirm' button...");
            ReportHelper.LogTest(Status.Info, "Tapping disconnect...");
            hearingSystemsPage.DisconnectDevices();
            ReportHelper.LogTest(Status.Info, "Tapped disconnect");
            ReportHelper.LogTest(Status.Info, "Checking app dialog...");
            Assert.IsTrue(DialogHelper.GetIsDialogDisplayed(TimeSpan.FromSeconds(5)), "App dialog is not visible");
            ReportHelper.LogTest(Status.Info, "App dialog is visible");
            ReportHelper.LogTest(Status.Info, "Checking app dialog confirm button text...");
            Assert.IsNotEmpty(new AppDialog().GetConfirmButtonText(), "App dialog confirm button text is empty");
            ReportHelper.LogTest(Status.Info, "App dialog confirm button text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking app dialog deny button text...");
            Assert.IsNotEmpty(new AppDialog().GetDenyButtonText(), "App dialog deny button text is empty");
            ReportHelper.LogTest(Status.Info, "App dialog deny button text is not empty");
            ReportHelper.LogTest(Status.Pass, "App dialog is displayed correctly with 'Cancel' and 'Confirm' button");

            ReportHelper.LogTest(Status.Info, "Clicking cancel...");
            new AppDialog().Deny();
            Thread.Sleep(500);
            ReportHelper.LogTest(Status.Info, "Clicked cancel");

            ReportHelper.LogTest(Status.Info, "Checking info of the left device again...");
            ReportHelper.LogTest(Status.Info, "Tapping left tab...");
            hearingSystemsPage.LeftDeviceTabClick();
            ReportHelper.LogTest(Status.Info, "Tapped left tab");
            ReportHelper.LogTest(Status.Info, "Checking if left tab is selected...");
            Assert.IsTrue(hearingSystemsPage.GetIsLeftTabSelected(), "Left tab is not selected");
            ReportHelper.LogTest(Status.Info, "Left tab is selected");
            ReportHelper.LogTest(Status.Info, "Checking left tab text...");
            Assert.IsNotEmpty(hearingSystemsPage.GetLeftTabText(), "Left tab text is empty");
            ReportHelper.LogTest(Status.Info, "Left tab text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left device name title...");
            Assert.AreEqual(leftNameTitle, hearingSystemsPage.GetLeftDeviceNameTitle(), "Left device name title is not matching");
            ReportHelper.LogTest(Status.Info, "Left device name title is matching");
            ReportHelper.LogTest(Status.Info, "Checking left device name...");
            Assert.AreEqual(leftName, hearingSystemsPage.GetLeftDeviceName(), "Left device name is not matching");
            ReportHelper.LogTest(Status.Info, "Left device name is matching");
            ReportHelper.LogTest(Status.Info, "Checking left device serial title...");
            Assert.AreEqual(leftSerialTitle, hearingSystemsPage.GetLeftDeviceSerialTitle(), "Left device serial title is not matching");
            ReportHelper.LogTest(Status.Info, "Left device serial title is matching");
            ReportHelper.LogTest(Status.Info, "Checking left device serial...");
            Assert.AreEqual(leftSerial, hearingSystemsPage.GetLeftDeviceSerial(), "Left device serial is not matching");
            ReportHelper.LogTest(Status.Info, "Left device serial is matching");
            ReportHelper.LogTest(Status.Info, "Checking left device state title...");
            Assert.AreEqual(leftStateTitle, hearingSystemsPage.GetLeftDeviceStateTitle(), "Left device state title is not matching");
            ReportHelper.LogTest(Status.Info, "Left device state title is matching");
            ReportHelper.LogTest(Status.Info, "Checking left device state...");
            Assert.AreEqual(leftState, hearingSystemsPage.GetLeftDeviceState(), "Left device state is not matching");
            ReportHelper.LogTest(Status.Info, "Left device state is matching");
            ReportHelper.LogTest(Status.Info, "Checking left device type title...");
            Assert.AreEqual(leftTypeTitle, hearingSystemsPage.GetLeftDeviceTypeTitle(), "Left device type title is not matching");
            ReportHelper.LogTest(Status.Info, "Left device type title is matching");
            ReportHelper.LogTest(Status.Info, "Checking left device type...");
            Assert.AreEqual(leftType, hearingSystemsPage.GetLeftDeviceType(), "Left device type is not matching");
            ReportHelper.LogTest(Status.Info, "Left device type is matching");
            ReportHelper.LogTest(Status.Info, "Checking left device UDI title...");
            Assert.AreEqual(leftUdiTitle, hearingSystemsPage.GetLeftDeviceUdiTitle(), "Left device UDI title is not matching");
            ReportHelper.LogTest(Status.Info, "Left device UDI title is matching");
            ReportHelper.LogTest(Status.Info, "Checking left device UDI...");
            Assert.AreEqual(leftUdi, hearingSystemsPage.GetLeftDeviceUdi(), "Left device UDI is not matching");
            ReportHelper.LogTest(Status.Info, "Left device UDI is matching");
            ReportHelper.LogTest(Status.Info, "Info of the left device is verified again");

            ReportHelper.LogTest(Status.Info, "Checking info of the right device again...");
            ReportHelper.LogTest(Status.Info, "Tapping right tab...");
            hearingSystemsPage.RightDeviceTabClick();
            ReportHelper.LogTest(Status.Info, "Tapped right tab");
            ReportHelper.LogTest(Status.Info, "Checking if right tab is selected...");
            Assert.IsTrue(hearingSystemsPage.GetIsRightTabSelected(), "Right tab is not selected");
            ReportHelper.LogTest(Status.Info, "Right tab is selected");
            ReportHelper.LogTest(Status.Info, "Checking right tab text...");
            Assert.IsNotEmpty(hearingSystemsPage.GetRightTabText(), "Right tab text is empty");
            ReportHelper.LogTest(Status.Info, "Right tab text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right device name title...");
            Assert.AreEqual(rightNameTitle, hearingSystemsPage.GetRightDeviceNameTitle(), "Right device name title is not matching");
            ReportHelper.LogTest(Status.Info, "Right device name title is matching");
            ReportHelper.LogTest(Status.Info, "Checking right device name...");
            Assert.AreEqual(rightName, hearingSystemsPage.GetRightDeviceName(), "Right device name is not matching");
            ReportHelper.LogTest(Status.Info, "Right device name is matching");
            ReportHelper.LogTest(Status.Info, "Checking right device serial title...");
            Assert.AreEqual(rightSerialTitle, hearingSystemsPage.GetRightDeviceSerialTitle(), "Right device serial title is not matching");
            ReportHelper.LogTest(Status.Info, "Right device serial title is matching");
            ReportHelper.LogTest(Status.Info, "Checking right device serial...");
            Assert.AreEqual(rightSerial, hearingSystemsPage.GetRightDeviceSerial(), "Right device serial is not matching");
            ReportHelper.LogTest(Status.Info, "Right device serial is matching");
            ReportHelper.LogTest(Status.Info, "Checking right device state title...");
            Assert.AreEqual(rightStateTitle, hearingSystemsPage.GetRightDeviceStateTitle(), "Right device state title is not matching");
            ReportHelper.LogTest(Status.Info, "Right device state title is matching");
            ReportHelper.LogTest(Status.Info, "Checking right device state...");
            Assert.AreEqual(rightState, hearingSystemsPage.GetRightDeviceState(), "Right device state is not matching");
            ReportHelper.LogTest(Status.Info, "Right device state is matching");
            ReportHelper.LogTest(Status.Info, "Checking right device type title...");
            Assert.AreEqual(rightTypeTitle, hearingSystemsPage.GetRightDeviceTypeTitle(), "Right device type title is not matching");
            ReportHelper.LogTest(Status.Info, "Right device type title is matching");
            ReportHelper.LogTest(Status.Info, "Checking right device type...");
            Assert.AreEqual(rightType, hearingSystemsPage.GetRightDeviceType(), "Right device type is not matching");
            ReportHelper.LogTest(Status.Info, "Right device type is matching");
            ReportHelper.LogTest(Status.Info, "Checking right device UDI title...");
            Assert.AreEqual(rightUdiTitle, hearingSystemsPage.GetRightDeviceUdiTitle(), "Right device UDI title is not matching");
            ReportHelper.LogTest(Status.Info, "Right device UDI title is matching");
            ReportHelper.LogTest(Status.Info, "Checking right device UDI...");
            Assert.AreEqual(rightUdi, hearingSystemsPage.GetRightDeviceUdi(), "Right device UDI is not matching");
            ReportHelper.LogTest(Status.Info, "Right device UDI is matching");
            ReportHelper.LogTest(Status.Info, "Info of the right device is verified again");

            ReportHelper.LogTest(Status.Pass, "All info is displayed correctly for connected device and is verified again");

            ReportHelper.LogTest(Status.Info, "Tapping disconnect again...");
            hearingSystemsPage.DisconnectDevices();
            ReportHelper.LogTest(Status.Info, "Tapped disconnect again");
            ReportHelper.LogTest(Status.Info, "Clicking confirm...");
            DialogHelper.ConfirmIfDisplayed();
            ReportHelper.LogTest(Status.Info, "Clicked confirm");

            // Disconnecting Message very quickly disappears in IOS and Appium is not able to get the element within that time.
            if (OnAndroid)
            {
                ReportHelper.LogTest(Status.Info, "Checking disconnecting Hearing System text...");
                Assert.IsTrue(hearingSystemsPage.VerifyDisconnectingText(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "manageHA_DisconnectingHearingSystems"), TimeSpan.FromSeconds(5)));
                ReportHelper.LogTest(Status.Pass, "Disconnecting Hearing System text verified in the " + Language_Audifon.English + " language");
            }

            ReportHelper.LogTest(Status.Info, "Waiting till initialize hardware page is loaded...");
            Assert.IsTrue(new InitializeHardwarePage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)), "Initialize hardware page is not loaded");
            var initializeHardwarePage = new InitializeHardwarePage();
            ReportHelper.LogTest(Status.Info, "Initialize hardware page is loaded");
            ReportHelper.LogTest(Status.Info, "Checking demo mode text...");
            Assert.IsNotEmpty(initializeHardwarePage.GetDemoModeText(), "Demo mode text is empty");
            ReportHelper.LogTest(Status.Info, "Demo mode text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking start scan text...");
            Assert.IsNotEmpty(initializeHardwarePage.GetScanText(), "Start scan text is empty");
            ReportHelper.LogTest(Status.Info, "Start scan text is not empty");
            ReportHelper.LogTest(Status.Pass, "'Start Search' page is displayed correctly having 'Start Search' and 'Demo Mode' option. Now tap 'Start Search'");
            ReportHelper.LogTest(Status.Info, "Clicking start scan...");
            initializeHardwarePage.StartScan();
            ReportHelper.LogTest(Status.Info, "Clicked start scan");
            var selectHearingAidsPage = new SelectHearingAidsPage();
            ReportHelper.LogTest(Status.Info, "Checking if start scan started...");
            Assert.IsTrue(selectHearingAidsPage.GetIsScanning(), "Start scan not started");
            ReportHelper.LogTest(Status.Info, "Start scan started");
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
            selectHearingAidsPage.GetIsDeviceSelected(firstHearingAid);
            if (secondHearingAid != null)
                selectHearingAidsPage.GetIsDeviceSelected(secondHearingAid);
            ReportHelper.LogTest(Status.Pass, "Found Hearing aids " + firstHearingAid + " and " + secondHearingAid);
            ReportHelper.LogTest(Status.Info, "Checking connect text...");
            Assert.IsNotEmpty(selectHearingAidsPage.GetConnectButtonText(), "Connect text is empty");
            ReportHelper.LogTest(Status.Info, "Connect text is not empty");
            ReportHelper.LogTest(Status.Info, "Clicking connect...");
            selectHearingAidsPage.Connect();
            ReportHelper.LogTest(Status.Info, "Clicked connect");

            ReportHelper.LogTest(Status.Info, "Waiting till dashboard page is loaded...");
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)), "Dashboard page is not loaded");
            dashboardPage = new DashboardPage();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "Dashboard page is loaded");
            ReportHelper.LogTest(Status.Pass, "Start view is displayed correctly containing L and R device icons.");

            AppManager.DeviceSettings.DisableWifi();
        }

        #endregion Sprint 13_1.5.0

        #region Sprint 14_1.5.0

        [Test]
        [Category("SystemTestsDeviceSpecificConfig")]
        [Description("TC-24417_Table-0")]
        public void ST24417_VerifyUnsupportedDeviceConnectionErrorPage()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            // The hearing aid needs to configured with fitting software other than the app which is being tested
            // For example if audifon app is tested then the hearing aid need to be configured in kindfit or any other, other than audifit
            var firstHearingAid = SelectHearingAid.GetLeftHearingAid();
            var secondHearingAid = SelectHearingAid.GetRightHearingAid();

            LaunchHelper.VerifyIntroPages();

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

            ReportHelper.LogTest(Status.Info, "Granting lowest needed permission to use app's location functions which is 'While in use' permission...");
            DialogHelper.ConfirmIfDisplayed();
            PermissionHelper.AllowPermissionIfRequested();
            ReportHelper.LogTest(Status.Info, "Lowest needed permission to use app's location functions which is 'While in use' permission granted");

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

            selectHearingAidsPage.WaitUntilDeviceFound(firstHearingAid);
            if (!selectHearingAidsPage.GetIsDeviceFound(firstHearingAid))
                selectHearingAidsPage.WaitUntilDeviceFound(firstHearingAid);

            if (selectHearingAidsPage != null)
                selectHearingAidsPage.WaitUntilDeviceFound(secondHearingAid);
            if (!selectHearingAidsPage.GetIsDeviceFound(secondHearingAid))
                selectHearingAidsPage.WaitUntilDeviceFound(secondHearingAid);

            selectHearingAidsPage.WaitUntilDeviceListNotChanging();
            selectHearingAidsPage.SelectDevicesExclusively(firstHearingAid, secondHearingAid);
            selectHearingAidsPage.GetIsDeviceSelected(firstHearingAid);
            if (selectHearingAidsPage != null)
                selectHearingAidsPage.GetIsDeviceSelected(secondHearingAid);

            ReportHelper.LogTest(Status.Info, "Checking connect button text...");
            Assert.IsNotEmpty(selectHearingAidsPage.GetConnectButtonText(), "Connect button text is empty");
            ReportHelper.LogTest(Status.Info, "Connect button text is not empty");

            ReportHelper.LogTest(Status.Info, "Clicking connect button...");
            selectHearingAidsPage.Connect();
            ReportHelper.LogTest(Status.Info, "Clicked connect button");

            ReportHelper.LogTest(Status.Info, "Waiting till unsupported hearing aid connection error page is loaded...");
            Assert.IsTrue(new HearingAidConnectionErrorPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(80)), "Unsupported hearing aid connection error page is not loaded");
            var hearingAidConnectionErrorPage = new HearingAidConnectionErrorPage();
            ReportHelper.LogTest(Status.Info, "Unsupported hearing aid connection error page is loaded");

            ReportHelper.LogTest(Status.Info, "Connecting two hearing aid with unsupported OEM ID and checking error page...");

            ReportHelper.LogTest(Status.Info, "Checking two hearing aid unsupported device page header text...");
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_BothInstrumentUnsupportedErrorTitle"), hearingAidConnectionErrorPage.GetPageTitle(), "Two hearing aid unsupported device page header text is not matching");
            ReportHelper.LogTest(Status.Pass, "Two hearing aid unsupported device page header text is matching");
            ReportHelper.LogTest(Status.Info, "Checking two hearing aid unsupported device page message text...");
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_BothInstrumentUnsupportedErrorMessage"), hearingAidConnectionErrorPage.GetPageMessage(), "Two hearing aid unsupported device page message text is not matching");
            ReportHelper.LogTest(Status.Pass, "Two hearing aid unsupported device page message text is matching");

            ReportHelper.LogTest(Status.Info, "Checking two hearing aid unsupported device page back to hearing aid title text...");
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionBackToSelectionTitle"), hearingAidConnectionErrorPage.GetTitleOfItem(0), "Two hearing aid unsupported device page back to hearing aid title text is not matching");
            ReportHelper.LogTest(Status.Pass, "Two hearing aid unsupported device page back to hearing aid title text is matching");
            ReportHelper.LogTest(Status.Info, "Checking two hearing aid unsupported device page back to hearing aid message text...");
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionBackToSelectionMessage").Replace("\r", string.Empty).Replace("\n", string.Empty).Replace(" ", string.Empty), hearingAidConnectionErrorPage.GetMessageOfItem(0).Replace("\r", string.Empty).Replace("\n", string.Empty).Replace(" ", string.Empty), "Two hearing aid unsupported device page back to hearing aid message text is not matching");
            ReportHelper.LogTest(Status.Pass, "Two hearing aid unsupported device page back to hearing aid message text is matching");

            ReportHelper.LogTest(Status.Pass, "Two hearing aid with unsupported OEM ID connected and error page is verified");

            ReportHelper.LogTest(Status.Info, "Clicking back to hearing aid selection link...");
            hearingAidConnectionErrorPage.BackToHearingAidSelectionUnsupportedDevicePage();
            ReportHelper.LogTest(Status.Info, "Clicked back to hearing aid selection link");

            ReportHelper.LogTest(Status.Info, "Checking if initialize hardware page is loaded...");
            Assert.IsTrue(new InitializeHardwarePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Initialize hardware page not loaded");
            initializeHardwarePage = new InitializeHardwarePage();
            ReportHelper.LogTest(Status.Info, "Initialize hardware page is loaded");
            ReportHelper.LogTest(Status.Info, "Clicking start scan...");
            initializeHardwarePage.StartScan();
            ReportHelper.LogTest(Status.Info, "Clicked start scan");

            ReportHelper.LogTest(Status.Info, "Checking if select hearing aids page is loaded...");
            Assert.IsTrue(new SelectHearingAidsPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Select hearing aids page not loaded");
            selectHearingAidsPage = new SelectHearingAidsPage();
            ReportHelper.LogTest(Status.Info, "Select hearing aids page is loaded");

            selectHearingAidsPage.WaitUntilDeviceFound(firstHearingAid);
            selectHearingAidsPage.WaitUntilDeviceListNotChanging();
            selectHearingAidsPage.SelectDevicesExclusively(firstHearingAid);
            Assert.IsTrue(selectHearingAidsPage.GetIsDeviceSelected(firstHearingAid), "First hearing aid is not selected");

            ReportHelper.LogTest(Status.Info, "Clicking connect button...");
            selectHearingAidsPage.Connect();
            ReportHelper.LogTest(Status.Info, "Clicked connect button");

            ReportHelper.LogTest(Status.Info, "Waiting till unsupported hearing aid connection error page is loaded...");
            Assert.IsTrue(new HearingAidConnectionErrorPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(80)), "Unsupported hearing aid connection error page is not loaded");
            hearingAidConnectionErrorPage = new HearingAidConnectionErrorPage();
            ReportHelper.LogTest(Status.Info, "Unsupported hearing aid connection error page is loaded");

            ReportHelper.LogTest(Status.Info, "Connecting right hearing aid with unsupported OEM ID and checking error page...");

            ReportHelper.LogTest(Status.Info, "Checking left unsupported device header text...");
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_LeftInstrumentUnsupportedErrorTitle"), hearingAidConnectionErrorPage.GetPageTitle(), "Left unsupported device header text is not matching");
            ReportHelper.LogTest(Status.Pass, "Left unsupported device header text is matching");
            ReportHelper.LogTest(Status.Info, "Checking left unsupported device message text...");
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_LeftInstrumentUnsupportedErrorMessage"), hearingAidConnectionErrorPage.GetPageMessage(), "Left unsupported device message text is not matching");
            ReportHelper.LogTest(Status.Pass, "Left unsupported device message text is matching");

            ReportHelper.LogTest(Status.Pass, "Left hearing aid with unsupported OEM ID connected and error page is verified");

            ReportHelper.LogTest(Status.Info, "Clicking back to hearing aid selection link...");
            hearingAidConnectionErrorPage.BackToHearingAidSelectionUnsupportedDevicePage();
            ReportHelper.LogTest(Status.Info, "Clicked back to hearing aid selection link");

            ReportHelper.LogTest(Status.Info, "Checking if initialize hardware page is loaded...");
            Assert.IsTrue(new InitializeHardwarePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Initialize hardware page not loaded");
            initializeHardwarePage = new InitializeHardwarePage();
            ReportHelper.LogTest(Status.Info, "Initialize hardware page is loaded");
            ReportHelper.LogTest(Status.Info, "Clicking start scan...");
            initializeHardwarePage.StartScan();
            ReportHelper.LogTest(Status.Info, "Clicked start scan");

            ReportHelper.LogTest(Status.Info, "Checking if select hearing aids page is loaded...");
            Assert.IsTrue(new SelectHearingAidsPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Select hearing aids page not loaded");
            selectHearingAidsPage = new SelectHearingAidsPage();
            ReportHelper.LogTest(Status.Info, "Select hearing aids page is loaded");

            selectHearingAidsPage.WaitUntilDeviceFound(secondHearingAid);
            selectHearingAidsPage.WaitUntilDeviceListNotChanging();
            selectHearingAidsPage.SelectDevicesExclusively(secondHearingAid);
            Assert.IsTrue(selectHearingAidsPage.GetIsDeviceSelected(secondHearingAid), "Second hearing aid is not selected");

            ReportHelper.LogTest(Status.Info, "Clicking connect button...");
            selectHearingAidsPage.Connect();
            ReportHelper.LogTest(Status.Info, "Clicked connect button");

            ReportHelper.LogTest(Status.Info, "Waiting till unsupported hearing aid connection error page is loaded...");
            Assert.IsTrue(new HearingAidConnectionErrorPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(80)), "Unsupported hearing aid connection error page is not loaded");
            hearingAidConnectionErrorPage = new HearingAidConnectionErrorPage();
            ReportHelper.LogTest(Status.Info, "Unsupported hearing aid connection error page is loaded");

            ReportHelper.LogTest(Status.Info, "Connecting left hearing aid with unsupported OEM ID and checking error page...");

            ReportHelper.LogTest(Status.Info, "Checking right unsupported device header text...");
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_RightInstrumentUnsupportedErrorTitle"), hearingAidConnectionErrorPage.GetPageTitle(), "Right unsupported device header text is not matching");
            ReportHelper.LogTest(Status.Pass, "Right unsupported device header text is matching");
            ReportHelper.LogTest(Status.Info, "Checking right unsupported device message text...");
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_RightInstrumentUnsupportedErrorMessage"), hearingAidConnectionErrorPage.GetPageMessage(), "Right unsupported device message text is not matching");
            ReportHelper.LogTest(Status.Pass, "Right unsupported device message text is matching");

            ReportHelper.LogTest(Status.Pass, "Left hearing aid with unsupported OEM ID connected and error page is verified");

            ReportHelper.LogTest(Status.Info, "Clicking back to hearing aid selection link...");
            hearingAidConnectionErrorPage.BackToHearingAidSelectionUnsupportedDevicePage();
            ReportHelper.LogTest(Status.Info, "Clicked back to hearing aid selection link");

            ReportHelper.LogTest(Status.Info, "Checking if initialize hardware page is loaded...");
            Assert.IsTrue(new InitializeHardwarePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Initialize hardware page not loaded");
            ReportHelper.LogTest(Status.Info, "Initialize hardware page is loaded");

            ReportHelper.LogTest(Status.Info, "Restarting app without app data...");
            AppManager.RestartApp(true);
            ReportHelper.LogTest(Status.Info, "Restarted app without app data");

            ReportHelper.LogTest(Status.Info, "Skip intro pages and connecting to hearing aid...");
            LaunchHelper.StartAppWithHearingAidsAndConnectOnly(firstHearingAid, secondHearingAid);
            ReportHelper.LogTest(Status.Info, "Clicked connect button");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off Left Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Left Hearing Aid turned off");

            // In IOS it takes 120 secs to go to Connection Error Page when HA is disconnected in the start
            ReportHelper.LogTest(Status.Info, "Waiting till hearing aid not found connection error page is loaded...");
            Assert.IsTrue(new HearingAidConnectionErrorPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(130)), "Hearing aid not found connection error page is not loaded");
            hearingAidConnectionErrorPage = new HearingAidConnectionErrorPage();
            ReportHelper.LogTest(Status.Info, "Hearing aid not found connection error page is loaded");

            ReportHelper.LogTest(Status.Info, "Clicking Use only one hearing aid...");
            hearingAidConnectionErrorPage.UseOnlyOneHearingSystem(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionUseOneInstrumentOnlyTitle"));
            ReportHelper.LogTest(Status.Info, "Clicked Use only one hearing aid");

            ReportHelper.LogTest(Status.Info, "Waiting till unsupported hearing aid connection error page is loaded...");
            Assert.IsTrue(new HearingAidConnectionErrorPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(80)), "Unsupported hearing aid connection error page is not loaded");
            hearingAidConnectionErrorPage = new HearingAidConnectionErrorPage();
            ReportHelper.LogTest(Status.Info, "Unsupported hearing aid connection error page is loaded");

            ReportHelper.LogTest(Status.Info, "Checking unsupported device with manual disconnection of left hearing device and use one hearing aid...");

            ReportHelper.LogTest(Status.Info, "Checking right unsupported device header text...");
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_RightInstrumentUnsupportedErrorTitle"), hearingAidConnectionErrorPage.GetPageTitle(), "Right unsupported device header text is not matching");
            ReportHelper.LogTest(Status.Pass, "Right unsupported device header text is matching");
            ReportHelper.LogTest(Status.Info, "Checking right unsupported device message text...");
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_RightInstrumentUnsupportedErrorMessage"), hearingAidConnectionErrorPage.GetPageMessage(), "Right unsupported device message text is not matching");
            ReportHelper.LogTest(Status.Pass, "Right unsupported device message text is matching");

            ReportHelper.LogTest(Status.Pass, "Unsupported device with manual disconnection of left hearing device and use one hearing aid is verified");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn on Left Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Left Hearing Aid turned on");

            ReportHelper.LogTest(Status.Info, "Restarting app without app data...");
            AppManager.RestartApp(true);
            ReportHelper.LogTest(Status.Info, "Restarted app without app data");

            ReportHelper.LogTest(Status.Info, "Skip intro pages and connecting to hearing aid...");
            LaunchHelper.StartAppWithHearingAidsAndConnectOnly(firstHearingAid, secondHearingAid);
            ReportHelper.LogTest(Status.Info, "Clicked connect button");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Right Hearing Aid turned off");

            // In IOS it takes 120 secs to go to Connection Error Page when HA is disconnected in the start
            ReportHelper.LogTest(Status.Info, "Waiting till hearing aid not found connection error page is loaded...");
            Assert.IsTrue(new HearingAidConnectionErrorPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(130)), "Hearing aid not found connection error page is loaded");
            hearingAidConnectionErrorPage = new HearingAidConnectionErrorPage();
            ReportHelper.LogTest(Status.Info, "Hearing aid not found connection error page is loaded");

            ReportHelper.LogTest(Status.Info, "Clicking Use only one hearing aid...");
            hearingAidConnectionErrorPage.UseOnlyOneHearingSystem(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_ResolutionUseOneInstrumentOnlyTitle"));
            ReportHelper.LogTest(Status.Info, "Clicked Use only one hearing aid");

            ReportHelper.LogTest(Status.Info, "Waiting till unsupported hearing aid connection error page is loaded...");
            Assert.IsTrue(new HearingAidConnectionErrorPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(80)), "Unsupported hearing aid connection error page is not loaded");
            hearingAidConnectionErrorPage = new HearingAidConnectionErrorPage();
            ReportHelper.LogTest(Status.Info, "Unsupported hearing aid connection error page is loaded");

            ReportHelper.LogTest(Status.Info, "Checking unsupported device with manual disconnection of right hearing device and use one hearing aid...");

            ReportHelper.LogTest(Status.Info, "Checking left unsupported device header text...");
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_LeftInstrumentUnsupportedErrorTitle"), hearingAidConnectionErrorPage.GetPageTitle(), "Left unsupported device header text is not matching");
            ReportHelper.LogTest(Status.Pass, "Left unsupported device header text is matching");
            ReportHelper.LogTest(Status.Info, "Checking left unsupported device message text...");
            Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "error_Connection_LeftInstrumentUnsupportedErrorMessage"), hearingAidConnectionErrorPage.GetPageMessage(), "Left unsupported device message text is not matching");
            ReportHelper.LogTest(Status.Pass, "Left unsupported device message text is matching");

            ReportHelper.LogTest(Status.Pass, "Unsupported device with manual disconnection of right hearing device and use one hearing aid is verified");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn on Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Right Hearing Aid turned on");
        }

        #endregion Sprint 14_1.5.0

        #region Sprint 16_1.5.0

        [Test]
        [Category("SystemTestsDeviceSpecificConfig")]
        [Description("TC-24552_Table-0")]
        public void ST24552_AddStreamingTileToAllPrograms()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            // This pair of hearing aid needs to configured as binural in Audifit without any specific configuration
            var leftHearingAidNormal = SelectHearingAid.GetLeftHearingAid();
            var rightHearingAidNormal = SelectHearingAid.GetRightHearingAid();

            CheckStreamingTile(leftHearingAidNormal, rightHearingAidNormal);

            ReportHelper.LogTest(Status.Info, "Restarting app after clearing app data...");
            AppManager.RestartApp(true);
            ReportHelper.LogTest(Status.Info, "App restarted after clearing app data");

            // This pair of hearing aid needs to configured as binural in Audifit with second program as Wind and third program as Phone
            var leftHearingAidConfigured = SelectHearingAid.GetLeftHearingAid(LeftHearingAid.Microgenisis_Lewi_R_Left_068842);
            var rightHearingAidConfigured = SelectHearingAid.GetRightHearingAid(RightHearingAid.Microgenisis_Lewi_R_Right_068846);

            CheckStreamingTile(leftHearingAidConfigured, rightHearingAidConfigured);

            void CheckStreamingTile(string leftHearingAid, string rightHearingAid)
            {
                var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(leftHearingAid, rightHearingAid).Page;
                ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + leftHearingAid + "' and Right '" + rightHearingAid + "'");
                dashboardPage.CheckStartView(dashboardPage);
                ReportHelper.LogTest(Status.Info, "App started in device mode after skipping intro pages and dashboard is loaded");
                ReportHelper.LogTest(Status.Info, "Checking number of programs...");
                Assert.AreEqual(4, dashboardPage.GetNumberOfPrograms(), "Number of programs is not 4");
                ReportHelper.LogTest(Status.Pass, "Number of programs is 4");

                ReportHelper.LogTest(Status.Info, "Opening second program...");
                dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
                var CurrentProgramName = new ProgramDetailPage().GetCurrentProgramName();
                ReportHelper.LogTest(Status.Info, "Opened '" + CurrentProgramName + "' program");

                ReportHelper.LogTest(Status.Info, "Checking if streaming tile is visible in '" + CurrentProgramName + "' program...");
                Assert.IsTrue(new ProgramDetailPage().GetIsStreamingDisplayVisible(), "Streaming tile is not visible in '" + CurrentProgramName + "' program");
                ReportHelper.LogTest(Status.Pass, "Streaming tile is visible in '" + CurrentProgramName + "' program");

                // favoriteProgramName: "Favourite 01" is the favorite name
                // favoriteProgramIconIndex: 1 is the index of the icon which will be set to the favorite
                // streamingValue: 0 is the streaming value to be set
                CreateFavoriteAndSetStreamValue(favoriteProgramName: "Favourite 01", favoriteProgramIconIndex: 1, streamingValue: 0);

                dashboardPage = new DashboardPage();
                ReportHelper.LogTest(Status.Info, "Checking number of programs...");
                Assert.AreEqual(5, dashboardPage.GetNumberOfPrograms(), "Number of programs is not 5");
                ReportHelper.LogTest(Status.Pass, "Number of programs is 5");

                ReportHelper.LogTest(Status.Info, "Opening third program...");
                dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 2);
                CurrentProgramName = new ProgramDetailPage().GetCurrentProgramName();
                ReportHelper.LogTest(Status.Info, "Opened '" + CurrentProgramName + "' program");

                ReportHelper.LogTest(Status.Info, "Checking if streaming tile is visible in '" + CurrentProgramName + "' program...");
                Assert.IsTrue(new ProgramDetailPage().GetIsStreamingDisplayVisible(), "Streaming tile is not visible in '" + CurrentProgramName + "' program");
                ReportHelper.LogTest(Status.Pass, "Streaming tile is visible in '" + CurrentProgramName + "' program");

                // favoriteProgramName: "Favourite 02" is the favorite name
                // favoriteProgramIconIndex: 2 is the index of the icon which will be set to the favorite
                // streamingValue: 1 is the streaming value to be set
                CreateFavoriteAndSetStreamValue(favoriteProgramName: "Favourite 02", favoriteProgramIconIndex: 2, streamingValue: 1);

                dashboardPage = new DashboardPage();
                ReportHelper.LogTest(Status.Info, "Checking number of programs...");
                Assert.AreEqual(6, dashboardPage.GetNumberOfPrograms(), "Number of programs is not 6");
                ReportHelper.LogTest(Status.Pass, "Number of programs is 6");

                // favoriteProgramIndex: 0 is the index passed since we want to open the first favorite
                //streamingValue: 0 is the streaming value to be verified
                CheckStreamingValueInFavorite(favoriteProgramIndex: 0, streamingValue: 0);

                // favoriteProgramIndex: 1 is the index passed since we want to open the second favorite
                //streamingValue: 1 is the streaming value to be verified
                CheckStreamingValueInFavorite(favoriteProgramIndex: 1, streamingValue: 1);
            }

            // favoriteProgramName is the name of the favorite which needs to be created
            // favoriteProgramIconIndex is the index of the icon which is going to be set for the favorite
            // streamingValue in the value which is has to be set
            void CreateFavoriteAndSetStreamValue(string favoriteProgramName, int favoriteProgramIconIndex, double streamingValue)
            {
                ReportHelper.LogTest(Status.Info, "Creating favorite '" + favoriteProgramName + "'...");
                FavoriteHelper.CreateFavoriteHearingProgram(favoriteProgramName, favoriteProgramIconIndex).Proceed();
                ReportHelper.LogTest(Status.Info, "Created favorite '" + favoriteProgramName + "'");

                var CurrentProgramName = new ProgramDetailPage().GetCurrentProgramName();
                ReportHelper.LogTest(Status.Info, "Checking if streaming tile is visible in '" + CurrentProgramName + "' favorite program...");
                Assert.IsTrue(new ProgramDetailPage().GetIsStreamingDisplayVisible(), "Streaming tile is not visible in '" + CurrentProgramName + "' favorite program");
                ReportHelper.LogTest(Status.Pass, "Streaming tile is visible in '" + CurrentProgramName + "' favorite program");

                ReportHelper.LogTest(Status.Info, "Opening streaming settings...");
                new ProgramDetailPage().StreamingDisplay.OpenSettings();
                var streamingPage = new ProgramDetailParamEditStreamingPage();
                ReportHelper.LogTest(Status.Info, "Opened streaming settings");

                ReportHelper.LogTest(Status.Info, "Checking streaming page...");
                ReportHelper.LogTest(Status.Info, "Checking streaming title...");
                Assert.AreEqual(streamingPage.GetTitle(), LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "prog_ParamStreamingTitle"), "Streaming title is not matching");
                ReportHelper.LogTest(Status.Info, "Streaming title is matching");
                ReportHelper.LogTest(Status.Info, "Checking streaming discription...");
                Assert.AreEqual(streamingPage.GetDescription(), LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "prog_ParamStreamingDescription"), "Streaming discription is not matching");
                ReportHelper.LogTest(Status.Info, "Streaming discription is matching");
                ReportHelper.LogTest(Status.Info, "Checking streaming environment text...");
                Assert.AreEqual(streamingPage.GetEnvironmentTitle(), LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "prog_ParamStreamingEnvironment"), "Streaming environment text is not matching");
                ReportHelper.LogTest(Status.Info, "Streaming environment text is matching");
                ReportHelper.LogTest(Status.Info, "Checking streaming source text...");
                Assert.AreEqual(streamingPage.GetSourceTitle(), LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "prog_ParamStreamingSource"), "Streaming source text is not matching");
                ReportHelper.LogTest(Status.Info, "Streaming source text is matching");
                ReportHelper.LogTest(Status.Pass, "Streaming page is verified");

                ReportHelper.LogTest(Status.Info, "Setting slider value to '" + streamingValue + "'...");
                streamingPage.SetStreamingSliderValue(streamingValue);
                ReportHelper.LogTest(Status.Info, "Slider value set to '" + streamingValue + "'");
                ReportHelper.LogTest(Status.Info, "Checking if slider value is set to '" + streamingValue + "'...");
                Assert.AreEqual(streamingValue, streamingPage.GetStreamingSliderValue(), delta: 0.1, "Slider value is not set to '" + streamingValue + "'");
                ReportHelper.LogTest(Status.Info, "Slider value is set to '" + streamingValue + "'");
                streamingPage.Close();

                ReportHelper.LogTest(Status.Info, "Tapping back to dashboard...");
                new ProgramDetailPage().TapBack();
                ReportHelper.LogTest(Status.Info, "Tapped back to dashboard");
            }

            // favoriteProgramIndex is the index of favorite in the Programs menu which starts from 0
            // streamingValue in the value which is has to be verified
            void CheckStreamingValueInFavorite(int favoriteProgramIndex, double streamingValue)
            {
                ReportHelper.LogTest(Status.Info, "Opening favorite program number '" + (favoriteProgramIndex + 1) + "'...");
                new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, favoriteProgramIndex);
                ReportHelper.LogTest(Status.Info, "Opened favorite program '" + new ProgramDetailPage().GetCurrentProgramName() + "'");

                ReportHelper.LogTest(Status.Info, "Verify slider value is set to '" + streamingValue + "'...");
                Assert.AreEqual(streamingValue, new ProgramDetailPage().StreamingDisplay.GetSliderValue(), delta: 0.1, "Slider value is not set to '" + streamingValue + "'");
                ReportHelper.LogTest(Status.Pass, "Slider value is set to '" + streamingValue + "' and is verified");

                ReportHelper.LogTest(Status.Info, "Tapping back to dashboard...");
                new ProgramDetailPage().TapBack();
                ReportHelper.LogTest(Status.Info, "Tapped back to dashboard");
            }
        }

        #endregion Sprint 16_1.5.0

        #endregion Test Cases
    }
}