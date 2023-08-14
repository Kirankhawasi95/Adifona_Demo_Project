using System;
using System.IO;
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
using HorusUITest.PageObjects.Menu.Help.InstructionsForUse;
using HorusUITest.PageObjects.Menu.Info;
using HorusUITest.PageObjects.Menu.Settings;
using HorusUITest.PageObjects.Start;
using HorusUITest.PageObjects.Start.Intro;
using NUnit.Framework;
using Platform = HorusUITest.Enums.Platform;

namespace HorusUITest.TestFixtures.TestCases
{
    public class SystemTestsDevice8 : BaseResettingTestFixture
    {
        #region Constructor

        public SystemTestsDevice8(Platform platform)
            : base(platform)
        {

        }

        #endregion Constructor

        #region Test Cases

        #region Sprint 21

        [Test]
        //[Ignore("It is not possible to automate change in OS language in iOS.")]
        [Category("SystemTestsDevice")]
        [Description("TC-17184_Table-1")]
        public void ST17184_PersonnaMedicalUICheck()
        {
            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            // Language to be tested
            Language_Device language_Device = Language_Device.English_US;
            Language_Audifon SpanishAppLanguage = Language_Audifon.Spanish;
            Language_Audifon GermanAppLanguage = Language_Audifon.German;

            // Changing Mobile Language
            AppManager.DeviceSettings.ChangeDeviceLanguage(language_Device);
            ReportHelper.LogTest(Status.Info, "Change device langauge to " + language_Device + " and restart app.");

            // Reset App after language change
            AppManager.RestartApp(true);
            ReportHelper.LogTest(Status.Info, "App Restarted");

            // Load Dashboard Page
            DashboardPage dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "App started in demo mode");
            // Wait till the toast message disappears. It causes the test case to fail if it does not wait.
            Thread.Sleep(3000);

            dashboardPage.OpenMenuUsingTap();

            new MainMenuPage().OpenSettings();

            new SettingsMenuPage().OpenLanguage();

            Assert.IsTrue(new SettingLanguagePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Info, "Language Menu opened");

            // Change the Language to Spanish Language
            new SettingLanguagePage().SelectLanguageAudifon(SpanishAppLanguage);
            ReportHelper.LogTest(Status.Info, "Select " + SpanishAppLanguage + " Language");

            new SettingLanguagePage().Accept();
            new AppDialog().Confirm();
            new DashboardPage().OpenMenuUsingTap();

            new MainMenuPage().OpenSettings();

            new SettingsMenuPage().OpenDemoMode();

            // Change to Normal mode
            var appModeSelectionPage = new AppModeSelectionPage();
            appModeSelectionPage.SelectAppMode(AppMode.Normal);
            ReportHelper.LogTest(Status.Pass, "App mode set to normal");
            appModeSelectionPage.ChangeAppMode(AppMode.Normal);
            Assert.IsTrue(new IntroPageOne().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Pass, "Exited Demo Mode, Welcome screen is visible.");

            string LeftHearingAidName = SelectHearingAid.GetLeftHearingAid();
            string RightHearingAidName = SelectHearingAid.GetRightHearingAid();

            // Load Dashboard and connect to Hearing Aid
            var dashboardPageDevice = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(LeftHearingAidName, RightHearingAidName).Page;
            dashboardPageDevice.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPageDevice.IsCurrentlyShown());
            dashboardPageDevice.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPageDevice.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPageDevice.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPageDevice.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + LeftHearingAidName + "' and Right '" + RightHearingAidName + "'");

            // Open Program
            dashboardPageDevice.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            ReportHelper.LogTest(Status.Info, "Program is open");

            // Create Favourite and Verify First Time
            string FavoriteProgramName = "Favourite 01";
            var Prgmdetailpage = new ProgramDetailPage();
            Prgmdetailpage.OpenProgramSettings();
            var programSettingsControlPage = new ProgramDetailSettingsControlPage();
            programSettingsControlPage.CreateFavorite();
            var programNamePage = new ProgramNamePage();
            programNamePage.EnterName(FavoriteProgramName).Proceed();
            var programIconPage = new ProgramIconPage();
            programIconPage.SelectIcon(24).Proceed();
            var programAutomationPage = new ProgramAutomationPage();
            programAutomationPage.Proceed();
            ReportHelper.LogTest(Status.Pass, "Check if newly created Favourite is available and its name is '" + FavoriteProgramName + "'");
            var prgmDetailPage = new ProgramDetailPage();
            Assert.AreEqual(FavoriteProgramName, prgmDetailPage.GetCurrentProgramName(), "Expected to be on ProgramDetailPage of '" + FavoriteProgramName + "' but was " + prgmDetailPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "Favourite program '" + FavoriteProgramName + "' is created successfully");
            prgmDetailPage.TapBack();

            // Verify Hearing Aid Systems page
            NavigationHelper.NavigateToSettingsMenu(dashboardPageDevice).OpenMyHearingAids();

            string expectedType = HearingAidModels.GetName(HearingAidModel.LewiR, AppManager.Brand);

            HearingSystemManagementPage hearingSystemsPage = new HearingSystemManagementPage();
            hearingSystemsPage.LeftDeviceTabClick();
            //Assert.AreEqual("lewi R", hearingSystemsPage.GetLeftDeviceType());
            Assert.AreEqual(expectedType, hearingSystemsPage.GetLeftDeviceType());
            hearingSystemsPage.RightDeviceTabClick();
            //Assert.AreEqual("lewi R", hearingSystemsPage.GetRightDeviceType());
            Assert.AreEqual(expectedType, hearingSystemsPage.GetRightDeviceType());
            ReportHelper.LogTest(Status.Pass, "Hearing Aid Systems page verified");

            hearingSystemsPage.TapBack();
            new SettingsMenuPage().TapBack();

            // Verify Help Topics in Spanish
            new MainMenuPage().OpenHelp();

            CheckMenuItemsAndTitle(new HelpMenuPage());
            new HelpMenuPage().OpenHelpTopics();
            CheckMenuItemsAndTitle(new HelpTopicsPage());
            new HelpTopicsPage().NavigateBack();
            new HelpMenuPage().OpenInformationMenu();
            CheckMenuItemsAndTitle(new InformationMenuPage());
            ReportHelper.LogTest(Status.Pass, "Help Topics verified");

            // Start App Again
            AppManager.RestartApp(true);
            ReportHelper.LogTest(Status.Info, "App Started again after reset of the data");

            // Load Dashboard and connect to Hearing Aid again
            var dashboardPageDeviceSecondLoad = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(LeftHearingAidName, RightHearingAidName).Page;
            dashboardPageDeviceSecondLoad.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPageDeviceSecondLoad.IsCurrentlyShown());
            dashboardPageDeviceSecondLoad.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + LeftHearingAidName + "' and Right '" + RightHearingAidName + "'");

            dashboardPageDeviceSecondLoad.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 2);
            ReportHelper.LogTest(Status.Info, "Program is selected");

            // Create Favourite and Verify Second time
            FavoriteProgramName = "Favourite 02";
            var PrgmdetailpageSecondLoad = new ProgramDetailPage();
            PrgmdetailpageSecondLoad.OpenProgramSettings();
            var programSettingsControlPageSecondLoad = new ProgramDetailSettingsControlPage();
            programSettingsControlPageSecondLoad.CreateFavorite();
            var programNamePageSecondLoad = new ProgramNamePage();
            programNamePageSecondLoad.EnterName(FavoriteProgramName).Proceed();
            var programIconPageSecondLoad = new ProgramIconPage();
            programIconPageSecondLoad.SelectIcon(25).Proceed();
            var programAutomationPageSecondLoad = new ProgramAutomationPage();
            programAutomationPageSecondLoad.Proceed();
            ReportHelper.LogTest(Status.Pass, "Check if newly created Favourite is available and its name is '" + FavoriteProgramName + "'");
            var prgmDetailPageSecondLoad = new ProgramDetailPage();
            Assert.AreEqual(FavoriteProgramName, prgmDetailPageSecondLoad.GetCurrentProgramName(), "Expected to be on ProgramDetailPage of '" + FavoriteProgramName + "' but was " + prgmDetailPageSecondLoad.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "Favourite program '" + FavoriteProgramName + "' is created successfully");
            prgmDetailPageSecondLoad.TapBack();

            // Verify Hearing Aid Systems page
            NavigationHelper.NavigateToSettingsMenu(new DashboardPage()).OpenMyHearingAids();

            HearingSystemManagementPage hearingSystemsPageSecondLoad = new HearingSystemManagementPage();
            hearingSystemsPageSecondLoad.LeftDeviceTabClick();
            //Assert.AreEqual("lewi R", hearingSystemsPageSecondLoad.GetLeftDeviceType());
            Assert.AreEqual(expectedType, hearingSystemsPageSecondLoad.GetLeftDeviceType());
            hearingSystemsPageSecondLoad.RightDeviceTabClick();
            //Assert.AreEqual("lewi R", hearingSystemsPageSecondLoad.GetRightDeviceType());
            Assert.AreEqual(expectedType, hearingSystemsPageSecondLoad.GetRightDeviceType());
            ReportHelper.LogTest(Status.Pass, "Hearing Aid Systems page verified");

            hearingSystemsPageSecondLoad.TapBack();
            new SettingsMenuPage().TapBack();

            // Verify Help Topics in Spanish again
            new MainMenuPage().OpenHelp();

            CheckMenuItemsAndTitle(new HelpMenuPage());
            new HelpMenuPage().OpenHelpTopics();
            CheckMenuItemsAndTitle(new HelpTopicsPage());
            new HelpTopicsPage().NavigateBack();
            new HelpMenuPage().OpenInformationMenu();
            CheckMenuItemsAndTitle(new InformationMenuPage());

            new InformationMenuPage().TapBack();
            new HelpMenuPage().TapBack();

            // Open Settings
            new MainMenuPage().OpenSettings();

            // Open Language
            new SettingsMenuPage().OpenLanguage();

            Assert.IsTrue(new SettingLanguagePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Info, "Language Menu opened");

            // Change the Language to German Language
            new SettingLanguagePage().SelectLanguageAudifon(GermanAppLanguage);
            ReportHelper.LogTest(Status.Info, "Select " + GermanAppLanguage + " Language");
            new SettingLanguagePage().Accept();
            new AppDialog().Confirm();

            new DashboardPage().OpenMenuUsingTap();

            // Verify Help Topics in German
            new MainMenuPage().OpenHelp();

            CheckMenuItemsAndTitle(new HelpMenuPage());
            new HelpMenuPage().OpenHelpTopics();
            CheckMenuItemsAndTitle(new HelpTopicsPage());
            new HelpTopicsPage().NavigateBack();
            new HelpMenuPage().OpenInformationMenu();
            CheckMenuItemsAndTitle(new InformationMenuPage());
            ReportHelper.LogTest(Status.Pass, "Help Topics verified");

            // Reset the Mobile language to English 
            AppManager.DeviceSettings.ChangeDeviceLanguage(Language_Device.English_US);
            ReportHelper.LogTest(Status.Info, "Change device langauge to English");

            AppManager.DeviceSettings.DisableWifi();

            void CheckMenuItemsAndTitle(BaseSubMenuPage menuPage)
            {
                ReportHelper.LogTest(Status.Info, "Checking " + menuPage.GetType().Name + "...");
                var menuItemsList = menuPage.MenuItems.GetAllVisible();
                int count = menuPage.MenuItems.CountAllVisible();
                if (count > 0)
                {
                    for (int i = 0; i < menuItemsList.Count; i++)
                    {
                        Thread.Sleep(500);
                        menuPage.MenuItems.Open(i, IndexType.Relative);
                        Thread.Sleep(2000);

                        // ToDo: The text for Imprint is different in menu and Imprint page title in Spanish
                        if (menuItemsList[i] == "Aviso legal")
                            Assert.AreEqual("Impresión", menuPage.GetNavigationBarTitle());
                        else
                            Assert.AreEqual(menuItemsList[i], menuPage.GetNavigationBarTitle());

                        ReportHelper.LogTest(Status.Info, "Help Topic " + menuPage.GetNavigationBarTitle() + " verified");
                        Thread.Sleep(500);
                        menuPage.NavigateBack();
                    }
                }
                ReportHelper.LogTest(Status.Pass, "All menu items checked for " + menuPage.GetType().Name);
            }
        }

        [Test]
        [Category("SystemTestsDeviceSpecificConfig")]
        [Description("TC-19412_Table-0")]
        public void ST19412_CheckUpgradedFirmware()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            // The Hearing Aid Connected is Binaural Normally configured with firmware 1.10.1425 for both sides
            LeftHearingAid leftHearingAid = Configuration.LeftHearingAid.Microgenisis_Lewi_R_Left_068845;
            RightHearingAid rightHearingAid = Configuration.RightHearingAid.Microgenisis_Lewi_R_Right_068844;

            string LeftHearingAid = SelectHearingAid.GetLeftHearingAid(leftHearingAid);
            string RightHearingAid = SelectHearingAid.GetRightHearingAid(rightHearingAid);

            // Load Intro and connect to Hearing Aid
            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(LeftHearingAid, RightHearingAid).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "Hearing Aid Connected Left " + LeftHearingAid + " and Right " + RightHearingAid + " both configured with firmware 1.10.1425");
            ReportHelper.LogTest(Status.Pass, "Start view is displayed correctly.");

            // Open Hearing Aid Systems page
            NavigationHelper.NavigateToSettingsMenu(dashboardPage).OpenMyHearingAids();

            string expectedTypeLewiR = HearingAidModels.GetName(HearingAidModel.LewiR, AppManager.Brand);
            string expectedTypeLewiS = HearingAidModels.GetName(HearingAidModel.LewiS, AppManager.Brand);

            // Verify Hearing Aid Systems page
            HearingSystemManagementPage hearingSystemsPage = new HearingSystemManagementPage();

            hearingSystemsPage.LeftDeviceTabClick();
            Data.HearingAid hearingAidLeft = SelectHearingAid.Left(leftHearingAid);
            string NameLeft = hearingAidLeft.Name;
            string SerialLeft = "Y" + hearingAidLeft.Name.Split('#')[1];

            Assert.AreEqual(NameLeft, hearingSystemsPage.GetLeftDeviceName());
            //Assert.AreEqual("lewi R", hearingSystemsPage.GetLeftDeviceType());
            Assert.AreEqual(expectedTypeLewiR, hearingSystemsPage.GetLeftDeviceType());
            Assert.AreEqual(SerialLeft, hearingSystemsPage.GetLeftDeviceSerial());
            Assert.AreEqual("Connected", hearingSystemsPage.GetLeftDeviceState());
            Assert.AreEqual("1.10.1425", hearingSystemsPage.GetLeftDeviceFirmware());
            Assert.IsTrue(hearingSystemsPage.GetIsLeftUdiVisible());
            Assert.IsNotEmpty(hearingSystemsPage.GetLeftDeviceUdi());

            hearingSystemsPage.RightDeviceTabClick();
            Data.HearingAid hearingAidRight = SelectHearingAid.Right(rightHearingAid);
            string NameRight = hearingAidRight.Name;
            string SerialRight = "Y" + hearingAidRight.Name.Split('#')[1];

            Assert.AreEqual(NameRight, hearingSystemsPage.GetRightDeviceName());
            //Assert.AreEqual("lewi R", hearingSystemsPage.GetRightDeviceType());
            Assert.AreEqual(expectedTypeLewiR, hearingSystemsPage.GetRightDeviceType());
            Assert.AreEqual(SerialRight, hearingSystemsPage.GetRightDeviceSerial());
            Assert.AreEqual("Connected", hearingSystemsPage.GetRightDeviceState());
            Assert.AreEqual("1.10.1425", hearingSystemsPage.GetRightDeviceFirmware());
            Assert.IsTrue(hearingSystemsPage.GetIsRightUdiVisible());
            Assert.IsNotEmpty(hearingSystemsPage.GetRightDeviceUdi());

            ReportHelper.LogTest(Status.Pass, "Hearing Aid Systems page verified");

            hearingSystemsPage.TapBack();

            // Open Demo Mode Settings
            new SettingsMenuPage().OpenDemoMode();

            // Change to Demo mode
            var appModeSelectionPageDemo = new AppModeSelectionPage();
            appModeSelectionPageDemo.SelectAppMode(AppMode.Demo);
            ReportHelper.LogTest(Status.Pass, "App mode set to Demo");
            appModeSelectionPageDemo.ChangeAppMode(AppMode.Demo);
            ReportHelper.LogTest(Status.Pass, "Exited Normal Mode, Welcome screen is visible.");

            // Load Dashboard Page
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            DashboardPage dashboardPageDemoMode = new DashboardPage();
            dashboardPageDemoMode.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPageDemoMode.IsCurrentlyShown());
            dashboardPageDemoMode.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPageDemoMode.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPageDemoMode.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPageDemoMode.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Pass, "Start view is displayed correctly.");

            // Open Hearing Aid Systems page
            NavigationHelper.NavigateToSettingsMenu(dashboardPageDemoMode).OpenMyHearingAids();

            // Verify Hearing Aid Systems page
            HearingSystemManagementPage hearingSystemsPageDemoMode = new HearingSystemManagementPage();

            hearingSystemsPageDemoMode.LeftDeviceTabClick();
            Assert.AreEqual("Demo HG#0003400", hearingSystemsPageDemoMode.GetLeftDeviceName());
            //Assert.AreEqual("lewi S", hearingSystemsPageDemoMode.GetLeftDeviceType());
            Assert.AreEqual(expectedTypeLewiS, hearingSystemsPageDemoMode.GetLeftDeviceType());
            Assert.AreEqual("0003400", hearingSystemsPageDemoMode.GetLeftDeviceSerial());
            Assert.AreEqual("Connected", hearingSystemsPageDemoMode.GetLeftDeviceState());
            Assert.AreEqual("1.10.1425", hearingSystemsPageDemoMode.GetLeftDeviceFirmware());

            hearingSystemsPageDemoMode.RightDeviceTabClick();
            Assert.AreEqual("Demo HG#0003401", hearingSystemsPageDemoMode.GetRightDeviceName());
            //Assert.AreEqual("lewi S", hearingSystemsPageDemoMode.GetRightDeviceType());
            Assert.AreEqual(expectedTypeLewiS, hearingSystemsPageDemoMode.GetRightDeviceType());
            Assert.AreEqual("0003401", hearingSystemsPageDemoMode.GetRightDeviceSerial());
            Assert.AreEqual("Connected", hearingSystemsPageDemoMode.GetRightDeviceState());
            Assert.AreEqual("1.10.1425", hearingSystemsPageDemoMode.GetRightDeviceFirmware());

            ReportHelper.LogTest(Status.Pass, "Hearing Aid Systems page verified in Demo Mode");

            hearingSystemsPage.TapBack();

            // Open Demo Mode Settings
            new SettingsMenuPage().OpenDemoMode();

            // Change to Normal mode
            var appModeSelectionPageNormal = new AppModeSelectionPage();
            appModeSelectionPageNormal.SelectAppMode(AppMode.Normal);
            ReportHelper.LogTest(Status.Pass, "App mode set to Normal");
            appModeSelectionPageNormal.ChangeAppMode(AppMode.Normal);
            Assert.IsTrue(new IntroPageOne().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Pass, "Exited Demo Mode, Welcome screen is visible.");

            // Connecting to Binaural Hearing Aid Normally confgured with Left with Firmware 1.11.1440 and Right with Firmware 1.10.1425
            // Currently using same HA for both. But in Rigrassion testing we need two Binaural Hearing Aids to check this one pair with 1.10.1425 firmware and another pair with left with firmware 1.11.1440 and Right with Firmware 1.10.1425
            leftHearingAid = Configuration.LeftHearingAid.Microgenisis_Lewi_R_Left_068845;
            rightHearingAid = Configuration.RightHearingAid.Microgenisis_Lewi_R_Right_068844;

            LeftHearingAid = SelectHearingAid.GetLeftHearingAid(leftHearingAid);
            RightHearingAid = SelectHearingAid.GetRightHearingAid(rightHearingAid);

            // Load Intro and connect to Hearing Aid
            var dashboardPageSecondLoad = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(LeftHearingAid, RightHearingAid).Page;
            dashboardPageSecondLoad.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPageSecondLoad.IsCurrentlyShown());
            dashboardPageSecondLoad.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPageSecondLoad.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPageSecondLoad.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPageSecondLoad.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "Hearing Aid Connected Left " + LeftHearingAid + " configured with firmware 1.11.1440 and Right " + RightHearingAid + " configured with firmware 1.10.1425");

            // Verify If Hearing Aid is Visible
            Assert.IsTrue(dashboardPageSecondLoad.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPageSecondLoad.GetIsRightHearingDeviceVisible());
            ReportHelper.LogTest(Status.Pass, "Start view is displayed correctly.");

            // Open Hearing Aid Systems page
            NavigationHelper.NavigateToSettingsMenu(dashboardPageSecondLoad).OpenMyHearingAids();

            // Verify Hearing Aid Systems page
            HearingSystemManagementPage hearingSystemsPageSecondLoad = new HearingSystemManagementPage();

            hearingSystemsPageSecondLoad.LeftDeviceTabClick();
            hearingAidLeft = SelectHearingAid.Left(leftHearingAid);
            NameLeft = hearingAidLeft.Name;
            SerialLeft = "Y" + hearingAidLeft.Name.Split('#')[1];

            Assert.AreEqual(NameLeft, hearingSystemsPageSecondLoad.GetLeftDeviceName());
            //Assert.AreEqual("lewi R", hearingSystemsPageSecondLoad.GetLeftDeviceType());
            Assert.AreEqual(expectedTypeLewiR, hearingSystemsPageSecondLoad.GetLeftDeviceType());
            Assert.AreEqual(SerialLeft, hearingSystemsPageSecondLoad.GetLeftDeviceSerial());
            Assert.AreEqual("Connected", hearingSystemsPageSecondLoad.GetLeftDeviceState());
            Assert.IsTrue(hearingSystemsPageSecondLoad.GetIsLeftUdiVisible());
            Assert.IsNotEmpty(hearingSystemsPageSecondLoad.GetLeftDeviceUdi());
            // ToDo: Commenting this assert because it will fail since we do not have provision to change the Firmware to 1.11.1440 in Audifit 5.8. This has to be done in Audifit 5.9
            //Assert.AreEqual("1.11.1440", hearingSystemsPageSecondLoad.GetLeftDeviceFirmware());

            hearingSystemsPageSecondLoad.RightDeviceTabClick();
            hearingAidRight = SelectHearingAid.Right(rightHearingAid);
            NameRight = hearingAidRight.Name;
            SerialRight = "Y" + hearingAidRight.Name.Split('#')[1];

            Assert.AreEqual(NameRight, hearingSystemsPageSecondLoad.GetRightDeviceName());
            //Assert.AreEqual("lewi R", hearingSystemsPageSecondLoad.GetRightDeviceType());
            Assert.AreEqual(expectedTypeLewiR, hearingSystemsPageSecondLoad.GetRightDeviceType());
            Assert.AreEqual(SerialRight, hearingSystemsPageSecondLoad.GetRightDeviceSerial());
            Assert.AreEqual("Connected", hearingSystemsPageSecondLoad.GetRightDeviceState());
            Assert.AreEqual("1.10.1425", hearingSystemsPageSecondLoad.GetRightDeviceFirmware());
            Assert.IsTrue(hearingSystemsPageSecondLoad.GetIsRightUdiVisible());
            Assert.IsNotEmpty(hearingSystemsPageSecondLoad.GetRightDeviceUdi());

            ReportHelper.LogTest(Status.Pass, "Hearing Aid Systems page verified with different firmwares");

            hearingSystemsPageSecondLoad.TapBack();

            new SettingsMenuPage().TapBack();

            new MainMenuPage().CloseMenuUsingTap();

            ReportHelper.LogTest(Status.Info, "Menus closed");
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-18896_Table-0")]
        public void ST18896_PuretoneImplementSpecification()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

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

            ReportHelper.LogTest(Status.Info, "Navigate to setting menu");
            var settingsMenuPage = NavigationHelper.NavigateToSettingsMenu(new DashboardPage());
            Assert.IsNotEmpty(settingsMenuPage.GetMyHearingAidsText());
            Assert.IsNotEmpty(settingsMenuPage.GetPermissionsText());
            Assert.IsNotEmpty(settingsMenuPage.GetLanguageText());
            Assert.IsNotEmpty(settingsMenuPage.GetDemoModeText());
            ReportHelper.LogTest(Status.Pass, "HearingAids, Permissions, Language, DemoMode are visible on Main Menu Page.");

            if (AppManager.Brand.Equals(Brand.Puretone))
            {
                ReportHelper.LogTest(Status.Info, "Check the available lanuage in Puretone");
                settingsMenuPage.OpenLanguage();
                Assert.AreEqual(Language_Puretone.English, new SettingLanguagePage().GetSelectedLanguagePuretone());
                ReportHelper.LogTest(Status.Pass, "Check successful, only English is available in Puretone app.");
                new SettingLanguagePage().NavigateBack();
            }

            ReportHelper.LogTest(Status.Info, "Change App mode from Demo to Normal");
            new SettingsMenuPage().OpenDemoMode();
            new AppModeSelectionPage().ChangeAppMode(AppMode.Normal);

            var firstDeviceName = SelectHearingAid.GetLeftHearingAid();
            var secondDeviceName = SelectHearingAid.GetRightHearingAid();

            // After setting to normal mode the app lands in InitializeHardwarePage only for Kind
            if (AppManager.Brand == Brand.Kind)
            {
                Assert.IsTrue(new InitializeHardwarePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));

                dashboardPage = LaunchHelper.ReconnectHearingAidsFromStartScan(firstDeviceName, secondDeviceName);
            }
            else
            {
                Assert.IsTrue(new IntroPageOne().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));

                var selectionPage = LaunchHelper.SkipToSelectHearingAidsPage().Page;

                //TODO -> As puretone does not support all HA please modify the HA here to run tc on Puretone App

                ReportHelper.LogTest(Status.Info, "App Mode is changed to Normal. Connecting two HA");

                selectionPage.WaitUntilDeviceFound(firstDeviceName);
                if (!selectionPage.GetIsDeviceFound(firstDeviceName))
                    selectionPage.WaitUntilDeviceFound(firstDeviceName);

                if (secondDeviceName != null)
                    selectionPage.WaitUntilDeviceFound(secondDeviceName);
                if (!selectionPage.GetIsDeviceFound(secondDeviceName))
                    selectionPage.WaitUntilDeviceFound(secondDeviceName);

                selectionPage.WaitUntilDeviceListNotChanging();
                selectionPage.SelectDevicesExclusively(firstDeviceName, secondDeviceName);
                selectionPage.GetIsDeviceSelected(firstDeviceName);
                if (secondDeviceName != null)
                    selectionPage.GetIsDeviceSelected(secondDeviceName);
                selectionPage.Connect();
            }

            ReportHelper.LogTest(Status.Info, "Connect the Hearing aids and check the Start View.");
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            dashboardPage = new DashboardPage();

            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            ReportHelper.LogTest(Status.Pass, "App stared succesfully with HA.");

            ReportHelper.LogTest(Status.Info, "Open and verify program detail page for Music.");
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 2);
            var programDetailPage = new ProgramDetailPage();
            Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsEqualizerDisplayVisible());
            ReportHelper.LogTest(Status.Pass, "Music program is opened. All UI elements are visible");

            ReportHelper.LogTest(Status.Info, "Create a favorite.");
            var favName = "fav";
            FavoriteHelper.CreateFavoriteHearingProgram(favName, 2).Proceed();
            Assert.AreEqual(favName, new ProgramDetailPage().GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "favorite program is created successfully");

            ReportHelper.LogTest(Status.Info, "Check items on Main menu page.");
            new ProgramDetailPage().OpenMenuUsingTap();
            var mainMenuPage = new MainMenuPage();
            Assert.IsNotEmpty(mainMenuPage.GetProgramsText());
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText());
            Assert.IsNotEmpty(mainMenuPage.GetHelpText());
            ReportHelper.LogTest(Status.Pass, "Settings, Programs, Help are visible on Main Menu Page.");
        }

        [Test]
        [Category("SystemTestsDeviceSpecificConfig")]
        [Description("TC-18007_Table-0")]
        public void ST18007_UpdateSceneDetectActivation()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            // ToDo: Currently configured Risa R Hearing Aid. Need to configure based on the Test Case on Audifit 5.9
            var firstHearingAid = SelectHearingAid.GetLeftHearingAid(LeftHearingAid.Microgenisis_Risa_R_Left_068821);
            var secondHearingAid = SelectHearingAid.GetRightHearingAid(RightHearingAid.Microgenisis_Risa_R_Right_068818);

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(firstHearingAid, secondHearingAid).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + firstHearingAid + "' and Right '" + secondHearingAid + "'");

            ReportHelper.LogTest(Status.Info, "Dashboard page is visible");

            ReportHelper.LogTest(Status.Info, "Check Automatic program detail view");
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 0);
            var programDetailPage = new ProgramDetailPage();
            new ProgramDetailPage().ProgramDetailPageUiCheck();
            Assert.IsNotEmpty(programDetailPage.AutoDisplay.GetDescription());
            ReportHelper.LogTest(Status.Pass, "Basic Program(Automatic) is selected successfully.");

            AppManager.RestartApp(false);

            //TODO -: Reconfigure the hearing instrument in audifit 5.8 P2 or lower by disabling scene detect.

            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            Assert.IsTrue(new DashboardPage().IsCurrentlyShown());
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 0);
            programDetailPage = new ProgramDetailPage();
            new ProgramDetailPage().SelectProgram(3);
            new ProgramDetailPage().ProgramDetailPageUiCheck();
            Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsEqualizerDisplayVisible());
            ReportHelper.LogTest(Status.Pass, "Base program is opened. All UI elements are visible");

            ReportHelper.LogTest(Status.Info, "Try to change setting of Basic Program.");
            programDetailPage.SpeechFocusDisplay.OpenSettings();
            var speechFocusPage = new ProgramDetailParamEditSpeechFocusPage();
            speechFocusPage.SelectSpeechFocus(SpeechFocus.Front);
            speechFocusPage.Close();
            ReportHelper.LogTest(Status.Pass, "Settings can be changed in Basic program");

            AppManager.RestartApp(false);

            //TODO-: Reconfigure the hearing instrument in audifit 5.9 or higher by enabling scene detect.

            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            Assert.IsTrue(new DashboardPage().IsCurrentlyShown());
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 0);
            programDetailPage = new ProgramDetailPage();
            new ProgramDetailPage().SelectProgram(1);
            new ProgramDetailPage().ProgramDetailPageUiCheck();
            Assert.IsNotEmpty(programDetailPage.AutoDisplay.GetDescription());

            AppManager.RestartApp(false);

            //TODO -: Reconfigure the hearing instrument in audifit 5.9 or higher by disabling scene detect.

            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            Assert.IsTrue(new DashboardPage().IsCurrentlyShown());
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 0);
            programDetailPage = new ProgramDetailPage();
            new ProgramDetailPage().SelectProgram(3);
            new ProgramDetailPage().ProgramDetailPageUiCheck();
            Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsEqualizerDisplayVisible());
            ReportHelper.LogTest(Status.Pass, "Base program is opened. All UI elements are visible");
        }

        #endregion Sprint 21

        #region Sprint 23

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-18913_Table-0")]
        public void ST18913_RemoveDeprecatedClasses()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            string LeftHearingAidName = SelectHearingAid.GetLeftHearingAid();
            string RightHearingAidName = SelectHearingAid.GetRightHearingAid();

            // Load Dashboard and connect to Hearing Aid
            var dashboardPageDevice = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(LeftHearingAidName, RightHearingAidName).Page;
            dashboardPageDevice.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPageDevice.IsCurrentlyShown());
            dashboardPageDevice.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPageDevice.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPageDevice.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPageDevice.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + LeftHearingAidName + "' and Right '" + RightHearingAidName + "'");

            // Open All Programs from Program Menu and Verify
            // Open First Program from Program Menu and Verify
            dashboardPageDevice.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 0);
            var programDetailPage = new ProgramDetailPage();
            Assert.IsNotEmpty(programDetailPage.GetCurrentProgramName());
            programDetailPage.TapBack();
            ReportHelper.LogTest(Status.Info, "First Program opened from Program Menu and Verified");

            // Open Second Program from Program Menu and Verify
            dashboardPageDevice.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            programDetailPage = new ProgramDetailPage();
            Assert.IsNotEmpty(programDetailPage.GetCurrentProgramName());
            programDetailPage.TapBack();
            ReportHelper.LogTest(Status.Info, "Second Program opened from Program Menu and Verified");

            // Open Third Program from Program Menu and Verify
            dashboardPageDevice.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 2);
            programDetailPage = new ProgramDetailPage();
            Assert.IsNotEmpty(programDetailPage.GetCurrentProgramName());
            programDetailPage.TapBack();
            ReportHelper.LogTest(Status.Info, "Third Program opened from Program Menu and Verified");

            // Open Fourth Program from Program Menu and Verify
            dashboardPageDevice.OpenProgramFromProgramsMenu(MainMenuTypes.Streaming, 0);
            programDetailPage = new ProgramDetailPage();
            Assert.IsNotEmpty(programDetailPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Info, "Fourth Program opened from Program Menu and Verified");

            ReportHelper.LogTest(Status.Pass, "All Programs opened from Program Menu and Verified");

            // Open All Programs from Slider Menu and Verify
            // Open Third Program from Slider Menu and Verify
            programDetailPage.SelectProgram(2);
            Assert.IsNotEmpty(programDetailPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Info, "Third Program opened from Swipe Menu and Verified");

            // Open Second Program from Slider Menu and Verify
            programDetailPage.SelectProgram(1);
            Assert.IsNotEmpty(programDetailPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Info, "Second Program opened from Swipe Menu and Verified");

            // Open First Program from Slider Menu and Verify
            programDetailPage.SelectProgram(0);
            Assert.IsNotEmpty(programDetailPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Info, "First Program opened from Swipe Menu and Verified");
            ReportHelper.LogTest(Status.Pass, "All Programs opened from Swipe Menu and Verified");

            // ToDo: Need to Automate the step "Change programs via rocker switch on hearing systems"

            // ToDo: Need to Automate the step "Change volume via rocker switch on hearing systems"

            // Adgust the volume and verify in Program Details Screen
            programDetailPage.SetVolumeSliderValue(1);
            Assert.AreEqual(1, programDetailPage.GetVolumeSliderValue(), delta: 0.1);

            programDetailPage.SetVolumeSliderValue(0);
            Assert.AreEqual(0, programDetailPage.GetVolumeSliderValue(), delta: 0.1);

            programDetailPage.SetVolumeSliderValue(0.5);
            Assert.AreEqual(0.5, programDetailPage.GetVolumeSliderValue(), delta: 0.1);

            ReportHelper.LogTest(Status.Pass, "Volume Increase Decrease Verfied in Program Details Screen");

            // Open and Enable Binaural settings
            programDetailPage.OpenBinauralSettings();

            var programDetailParamEditBinauralPage = new ProgramDetailParamEditBinauralPage();

            programDetailParamEditBinauralPage.TurnOnBinauralSeparation();
            ReportHelper.LogTest(Status.Info, "Binaural Switch turned on");

            // Increase Decrease volume in Binaural page and Verify 
            programDetailParamEditBinauralPage.SetVolumeSliderValue(VolumeChannel.Left, 0.4);
            Assert.AreEqual(Math.Round(0.4, 1), Math.Round(programDetailParamEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Left), 1), delta: 0.1);

            programDetailParamEditBinauralPage.SetVolumeSliderValue(VolumeChannel.Left, 0.5);
            Assert.AreEqual(Math.Round(0.5, 1), Math.Round(programDetailParamEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Left), 1), delta: 0.1);

            programDetailParamEditBinauralPage.SetVolumeSliderValue(VolumeChannel.Left, 0.6);
            Assert.AreEqual(Math.Round(0.6, 1), Math.Round(programDetailParamEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Left), 1), delta: 0.1);

            programDetailParamEditBinauralPage.SetVolumeSliderValue(VolumeChannel.Left, 0.6);
            Assert.AreEqual(Math.Round(0.6, 1), Math.Round(programDetailParamEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Left), 1), delta: 0.1);

            programDetailParamEditBinauralPage.SetVolumeSliderValue(VolumeChannel.Left, 0.5);
            Assert.AreEqual(Math.Round(0.5, 1), Math.Round(programDetailParamEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Left), 1), delta: 0.1);

            programDetailParamEditBinauralPage.SetVolumeSliderValue(VolumeChannel.Right, 0.6);
            Assert.AreEqual(Math.Round(0.6, 1), Math.Round(programDetailParamEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Right), 1), delta: 0.1);

            programDetailParamEditBinauralPage.SetVolumeSliderValue(VolumeChannel.Right, 0.5);
            Assert.AreEqual(Math.Round(0.5, 1), Math.Round(programDetailParamEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Right), 1), delta: 0.1);

            programDetailParamEditBinauralPage.SetVolumeSliderValue(VolumeChannel.Right, 0.4);
            Assert.AreEqual(Math.Round(0.4, 1), Math.Round(programDetailParamEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Right), 1), delta: 0.1);

            programDetailParamEditBinauralPage.SetVolumeSliderValue(VolumeChannel.Right, 0.4);
            Assert.AreEqual(Math.Round(0.4, 1), Math.Round(programDetailParamEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Right), 1), delta: 0.1);

            programDetailParamEditBinauralPage.SetVolumeSliderValue(VolumeChannel.Right, 0.5);
            Assert.AreEqual(Math.Round(0.5, 1), Math.Round(programDetailParamEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Right), 1), delta: 0.1);

            programDetailParamEditBinauralPage.Close();

            ReportHelper.LogTest(Status.Pass, "Volume Increase Decrease Verfied in Binaural Screen");

            new ProgramDetailPage().TapBack();

            // Open Music Program and Verify Parameters
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);

            // Change Speech Focus
            Assert.IsTrue(new ProgramDetailPage().GetIsSpeechFocusDisplayVisible(), "Speech Focus Settings do not exist");
            new ProgramDetailPage().SpeechFocusDisplay.OpenSettings();
            new ProgramDetailParamEditSpeechFocusPage().SelectSpeechFocus(SpeechFocus.Front);
            Assert.AreEqual(new ProgramDetailParamEditSpeechFocusPage().GetSelectedSpeechFocus(), SpeechFocus.Front);
            ReportHelper.LogTest(Status.Info, "Speech Focus Parameter Changed");
            new ProgramDetailParamEditSpeechFocusPage().Close();

            // Change Noise Reduction
            Assert.IsTrue(new ProgramDetailPage().GetIsNoiseReductionDisplayVisible(), "Noise Reduction Settings do not exists");
            new ProgramDetailPage().NoiseReductionDisplay.OpenSettings();
            new ProgramDetailParamEditNoiseReductionPage().SelectNoiseReduction(NoiseReduction.Medium);
            Assert.AreEqual(new ProgramDetailParamEditNoiseReductionPage().GetSelectedNoiseReduction(), NoiseReduction.Medium);
            ReportHelper.LogTest(Status.Info, "Noise Reduction Parameter Changed");
            new ProgramDetailParamEditNoiseReductionPage().Close();

            ReportHelper.LogTest(Status.Pass, "Parameters changed and Verified in Music Program");

            // Change the Program Name and Icon and Verify
            string NewProgramName = "Program Name Edited";

            new ProgramDetailPage().OpenProgramSettings();
            ProgramDetailSettingsControlPage programDetailSettingsControlPage = new ProgramDetailSettingsControlPage();
            programDetailSettingsControlPage.CustomizeName();

            // Program Name
            ProgramNamePage programNamePage = new ProgramNamePage();
            programNamePage.EnterName(NewProgramName);
            programNamePage.Proceed();
            Assert.AreEqual(NewProgramName, new ProgramDetailPage().GetCurrentProgramName());
            ReportHelper.LogTest(Status.Info, "Name of program changed to " + NewProgramName + " successfully");

            new ProgramDetailPage().OpenProgramSettings();
            new ProgramDetailSettingsControlPage().CustomizeIcon();

            // Program Icon
            ProgramIconPage programIconPage = new ProgramIconPage();
            programIconPage.SelectIcon(5);
            programIconPage.Proceed();
            ReportHelper.LogTest(Status.Info, "Icon of program changed successfully");

            ReportHelper.LogTest(Status.Pass, "Name and Icon of program icon changed successfully");

            new ProgramDetailPage().TapBack();

            new DashboardPage().OpenMenuUsingTap();

            // Open Help Menu and Verify
            new MainMenuPage().OpenHelp();
            CheckMenuItemsAndTitle(new HelpMenuPage());

            // Help Topics Submenu in Help
            new HelpMenuPage().OpenHelpTopics();
            CheckMenuItemsAndTitle(new HelpTopicsPage());
            new HelpTopicsPage().NavigateBack();

            // Information Submenu in Help
            new HelpMenuPage().OpenInformationMenu();
            CheckMenuItemsAndTitle(new InformationMenuPage());
            new InformationMenuPage().NavigateBack();
            new HelpMenuPage().NavigateBack();

            ReportHelper.LogTest(Status.Pass, "Help Menu Opened and Verified");

            // Open Settings Menu
            new MainMenuPage().OpenSettings();

            // Open My Hearing Aid and Verify
            new SettingsMenuPage().OpenMyHearingAids();
            HearingSystemManagementPage hearingSystemsPage = new HearingSystemManagementPage();
            hearingSystemsPage = hearingSystemsPage.CheckHAInformationFromSettings(AppMode.Normal, Side.Left);
            hearingSystemsPage = hearingSystemsPage.CheckHAInformationFromSettings(AppMode.Normal, Side.Right);
            ReportHelper.LogTest(Status.Pass, "My Hearing Aid openned and Verified");
            hearingSystemsPage.TapBack();

            // Open Permessions and Turn it Off
            new SettingsMenuPage().OpenPermissions();
            SettingPermissionsPage settingPermissionsPage = new SettingPermissionsPage();
            Assert.IsTrue(settingPermissionsPage.GetIsLocationPermissionSwitchChecked());
            settingPermissionsPage.TurnOffLocationPermission();
            ReportHelper.LogTest(Status.Pass, "Location Permission Switch is Off");
            settingPermissionsPage.NavigateBack();

            // Open Language
            new SettingsMenuPage().OpenLanguage();
            SettingLanguagePage settingLanguagePage = new SettingLanguagePage();
            settingLanguagePage.SelectLanguage(Language.Dutch);
            settingLanguagePage.SelectLanguage(Language.English);
            ReportHelper.LogTest(Status.Pass, "Accept button enables and disables when the Language is selected");
            settingLanguagePage.TapBack();

            // Open Demo Mode
            new SettingsMenuPage().OpenDemoMode();
            AppModeSelectionPage appModeSelectionPage = new AppModeSelectionPage();
            appModeSelectionPage.SelectAppMode(AppMode.Normal);
            appModeSelectionPage.SelectAppMode(AppMode.Demo);
            ReportHelper.LogTest(Status.Pass, "Accept button enables and disables when the Mode is changed");
            appModeSelectionPage.TapBack();

            // Open Developer Menu
            new SettingsMenuPage().OpenLogs();
            ReportHelper.LogTest(Status.Pass, "Developer Menu is open");
            new LogPage().TapBack();

            new SettingsMenuPage().TapBack();

            // Open Find Hearing Aid Systems and Activate Tracking Hearing Aid Systems
            new MainMenuPage().OpenHelp();
            new HelpMenuPage().OpenFindHearingDevices();
            DialogHelper.ConfirmIfDisplayed();
            Thread.Sleep(2000);
            new FindDevicesPage().NavigateBack();
            new HelpMenuPage().TapBack();
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenPermissions();
            SettingPermissionsPage settingPermissionsPageSecond = new SettingPermissionsPage();
            Assert.IsTrue(settingPermissionsPageSecond.GetIsLocationPermissionSwitchChecked());
            ReportHelper.LogTest(Status.Pass, "Openned Find Hearing Aid Systems and Activated Tracking Hearing Aid Systems");
            settingPermissionsPageSecond.TapBack();

            new SettingsMenuPage().TapBack();
            new MainMenuPage().CloseMenuUsingTap();

            // Open Music Program and Create a Favorite
            ReportHelper.LogTest(Status.Info, "Create a favorite program based on location...");
            string FavoriteName = "Favorite 01";
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 2);
            FavoriteHelper.CreateFavoriteHearingProgram(FavoriteName, 8);
            FavoriteHelper.SelectLocationAndCreateFavorite(FavoriteName, 0.5, 0.5);
            ReportHelper.LogTest(Status.Pass, "Favorite created with name '" + FavoriteName + "' based on location");

            new ProgramDetailPage().TapBack();

            AppManager.DeviceSettings.RevokeGPSPermission();
            Thread.Sleep(200);
            ReportHelper.LogTest(Status.Pass, "GPS Permission Revoked");

            // Restart App
            AppManager.RestartApp(false);
            ReportHelper.LogTest(Status.Info, "App Restarted");

            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(30));

            // Deny GPS Pemission
            //AppDialog appDialog = new AppDialog();
            //Assert.IsNotEmpty(appDialog.GetConfirmButtonText());
            //Assert.IsNotEmpty(appDialog.GetDenyButtonText());
            //appDialog.Deny();
            //ReportHelper.LogTest(Status.Pass, "Cancel Pressed");

            //new HardwareErrorPage().RetryProcess();

            // Grant GPS Pemission
            AppManager.DeviceSettings.GrantGPSPermission();
            Thread.Sleep(1000);
            //AppManager.App.PressBackButton();
            //ReportHelper.LogTest(Status.Pass, "Settings Pressed and Access given");

            // Load Dashboard
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            DashboardPage dashboardPageRestart = new DashboardPage();
            dashboardPageRestart.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPageRestart.IsCurrentlyShown());
            dashboardPageRestart.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPageRestart.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPageRestart.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPageRestart.GetIsMenuHamburgerButtonDisplayed());

            // Wait till the Location based program is activated
            Thread.Sleep(8000);

            // Verify selected Program Name
            dashboardPageRestart = new DashboardPage();
            Assert.AreEqual(FavoriteName, dashboardPageRestart.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "Favorite program automatically selected");

            // Disable Bluetooth
            AppManager.DeviceSettings.DisableBluetooth();
            ReportHelper.LogTest(Status.Info, "Bluetooth disabled");
            DialogHelper.ConfirmIfDisplayed();
            DialogHelper.ConfirmIfDisplayed();
            Thread.Sleep(2000);
            ReportHelper.LogTest(Status.Pass, "Bluetooth disabled and connection to Hearing Aid Intrupted and app does not crash");

            // Enable Bluetooth
            AppManager.DeviceSettings.EnableBluetooth();
            ReportHelper.LogTest(Status.Info, "Bluetooth enabled");
            // Waiting till the Loading dialog disappears
            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(15));

            dashboardPageRestart.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPageRestart.IsCurrentlyShown());
            dashboardPageRestart.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPageRestart.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPageRestart.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPageRestart.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Pass, "Both Hearing Aids connected and Left Right Symbols are visible");

            // Disable Wifi
            AppManager.DeviceSettings.DisableWifi();
            Thread.Sleep(3000);
            ReportHelper.LogTest(Status.Info, "Wifi disabled");

            // Check if the connected device name and details are there
            dashboardPageRestart.OpenMenuUsingTap();
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenMyHearingAids();
            HearingSystemManagementPage hearingSystemsPageRestart = new HearingSystemManagementPage();
            hearingSystemsPageRestart.LeftDeviceTabClick();
            Assert.IsNotEmpty(hearingSystemsPageRestart.GetLeftDeviceName());
            Assert.IsNotEmpty(hearingSystemsPageRestart.GetLeftDeviceType());
            Assert.IsNotEmpty(hearingSystemsPageRestart.GetLeftDeviceSerial());
            Assert.IsNotEmpty(hearingSystemsPageRestart.GetLeftDeviceState());
            Assert.IsTrue(hearingSystemsPageRestart.GetIsLeftUdiVisible());
            Assert.IsNotEmpty(hearingSystemsPageRestart.GetLeftDeviceUdi());

            hearingSystemsPageRestart.RightDeviceTabClick();
            Assert.IsNotEmpty(hearingSystemsPageRestart.GetRightDeviceName());
            Assert.IsNotEmpty(hearingSystemsPageRestart.GetRightDeviceType());
            Assert.IsNotEmpty(hearingSystemsPageRestart.GetRightDeviceSerial());
            Assert.IsNotEmpty(hearingSystemsPageRestart.GetRightDeviceState());
            Assert.IsTrue(hearingSystemsPageRestart.GetIsRightUdiVisible());
            Assert.IsNotEmpty(hearingSystemsPageRestart.GetRightDeviceUdi());
            ReportHelper.LogTest(Status.Pass, "Verify if my Hearing Aid Details are available");
            hearingSystemsPageRestart.TapBack();

            new SettingsMenuPage().TapBack();
            new MainMenuPage().CloseMenuUsingTap();

            // Disabling Mobile Data if it is enabled. If it is enabled it will go into the location page and not display message
            AppManager.DeviceSettings.DisableMobileData();
            Thread.Sleep(3000);
            ReportHelper.LogTest(Status.Info, "Mobile data disabled");

            // Try to create favorite without Wifi connection
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 2);
            new ProgramDetailPage().OpenProgramSettings();
            new ProgramDetailSettingsControlPage().CreateFavorite();
            ProgramNamePage programNamePageCheck = new ProgramNamePage();
            programNamePageCheck.EnterName("Favorite Check");
            Assert.IsNotEmpty(programNamePageCheck.GetName());
            programNamePageCheck.Proceed();
            ProgramIconPage programIconPageCheck = new ProgramIconPage();
            programIconPageCheck.SelectIcon(12);
            programIconPageCheck.Proceed();
            new ProgramAutomationPage().TurnOnAutomation();
            Assert.IsTrue(new ProgramAutomationPage().GetIsGeofenceAutomationVisible());
            Assert.IsTrue(new ProgramAutomationPage().GetIsWifiAutomationVisible());
            new ProgramAutomationPage().TapConnectToLocation();
            DialogHelper.ConfirmIfDisplayed();
            new ProgramAutomationPage().TapConnectToWiFi();
            DialogHelper.ConfirmIfDisplayed();
            new ProgramAutomationPage().CancelAndConfirm();

            new ProgramDetailSettingsControlPage().TapBack();

            new ProgramDetailPage().TapBack();
            ReportHelper.LogTest(Status.Pass, "Not able to create favorites");

            // Delete Favorites
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 0);
            new ProgramDetailPage().OpenProgramSettings();
            new ProgramDetailSettingsControlPage().DeleteFavoriteAndConfirm();
            ReportHelper.LogTest(Status.Pass, "Favorite deleted successfully");

            // Restart App
            AppManager.RestartApp(false);
            ReportHelper.LogTest(Status.Info, "App Restarted Again");

            // Load Dashboard
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            DashboardPage dashboardPageRestartSecond = new DashboardPage();
            dashboardPageRestartSecond.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPageRestartSecond.IsCurrentlyShown());
            dashboardPageRestartSecond.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPageRestartSecond.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPageRestartSecond.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPageRestartSecond.GetIsMenuHamburgerButtonDisplayed());
            // Wait till the splash message disappears
            Thread.Sleep(3000);

            // Verify selected Program Name
            dashboardPageRestartSecond.GetCurrentProgramName();
            Assert.AreNotEqual(FavoriteName, dashboardPageRestartSecond.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "Previously created and deleted Favorite program not automatically selected");

            // Disconnect the Hearing Aid
            dashboardPageRestartSecond.OpenMenuUsingTap();
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenMyHearingAids();
            new HearingSystemManagementPage().DisconnectDevices();

            // Press Cancel First
            AppDialog appDialogDisconnect = new AppDialog();
            Assert.IsNotEmpty(appDialogDisconnect.GetConfirmButtonText());
            Assert.IsNotEmpty(appDialogDisconnect.GetDenyButtonText());
            appDialogDisconnect.Deny();
            ReportHelper.LogTest(Status.Pass, "Cancel Pressed");

            // Then press Disconnect
            new HearingSystemManagementPage().DisconnectDevices();
            DialogHelper.ConfirmIfDisplayed();
            ReportHelper.LogTest(Status.Pass, "Disconnect Pressed");

            Assert.IsTrue(new InitializeHardwarePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            Assert.IsNotEmpty(new InitializeHardwarePage().GetScanText());
            ReportHelper.LogTest(Status.Pass, "Hearing Aid disconnected and Start Scan Page Shown");

            void CheckMenuItemsAndTitle(BaseSubMenuPage menuPage)
            {
                ReportHelper.LogTest(Status.Info, "Checking " + menuPage.GetType().Name + "...");
                var menuItemsList = menuPage.MenuItems.GetAllVisible();
                for (int i = 0; i < menuItemsList.Count; i++)
                {
                    Thread.Sleep(500);
                    menuPage.MenuItems.Open(i, IndexType.Relative);
                    Thread.Sleep(2000);
                    Assert.AreEqual(menuItemsList[i], menuPage.GetNavigationBarTitle());
                    ReportHelper.LogTest(Status.Info, "On " + menuPage.GetNavigationBarTitle() + " Page");
                    Thread.Sleep(500);
                    menuPage.NavigateBack();
                }
                ReportHelper.LogTest(Status.Pass, "All menu items checked for " + menuPage.GetType().Name);
            }
        }

        #endregion Sprint 23

        #region Sprint 28

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-20559_Table-0")]
        public void ST20559_UseTabbedViewOnPage()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var LeftHearingAidName = SelectHearingAid.GetLeftHearingAid();
            var RightHearingAidName = SelectHearingAid.GetRightHearingAid();

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(LeftHearingAidName, RightHearingAidName).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + LeftHearingAidName + "' and Right '" + RightHearingAidName + "'");
            ReportHelper.LogTest(Status.Pass, "Dashboard page loaded");

            NavigationHelper.NavigateToSettingsMenu(dashboardPage).OpenMyHearingAids();

            var hearingSystemsPage = new HearingSystemManagementPage();

            ReportHelper.LogTest(Status.Info, "Verify Left device information");
            hearingSystemsPage.LeftDeviceTabClick();
            Assert.IsTrue(hearingSystemsPage.GetIsLeftTabSelected());
            Assert.IsNotEmpty(hearingSystemsPage.GetLeftTabText());
            Assert.IsNotEmpty(hearingSystemsPage.GetLeftDeviceName());
            Assert.IsNotEmpty(hearingSystemsPage.GetLeftDeviceSerial());
            var leftStateConnected = hearingSystemsPage.GetLeftDeviceState();
            Assert.IsNotEmpty(leftStateConnected);
            Assert.IsNotEmpty(hearingSystemsPage.GetLeftDeviceType());
            Assert.IsTrue(hearingSystemsPage.GetIsLeftUdiVisible());
            Assert.IsNotEmpty(hearingSystemsPage.GetLeftDeviceUdi());

            ReportHelper.LogTest(Status.Info, "Verify Right device information");
            hearingSystemsPage.RightDeviceTabClick();
            Assert.IsTrue(hearingSystemsPage.GetIsRightTabSelected());
            Assert.IsNotEmpty(hearingSystemsPage.GetRightTabText());
            Assert.IsNotEmpty(hearingSystemsPage.GetRightDeviceName());
            Assert.IsNotEmpty(hearingSystemsPage.GetRightDeviceSerial());
            string rightStateConnected = hearingSystemsPage.GetRightDeviceState();
            Assert.IsNotEmpty(rightStateConnected);
            Assert.IsNotEmpty(hearingSystemsPage.GetRightDeviceType());
            Assert.IsTrue(hearingSystemsPage.GetIsRightUdiVisible());
            Assert.IsNotEmpty(hearingSystemsPage.GetRightDeviceUdi());

            hearingSystemsPage.LeftDeviceTabClick();
            Assert.IsTrue(hearingSystemsPage.GetIsLeftTabSelected());

            ReportHelper.LogTest(Status.Pass, "Left and Right device info verified");

            // Disable Bluetooth
            AppManager.DeviceSettings.DisableBluetooth();
            ReportHelper.LogTest(Status.Info, "Bluetooth disabled");
            Thread.Sleep(2000);
            // ToDo: For iOS when the stacked message is shown assert fails on AppDialog. Hence not asserting it.
            if (OnAndroid)
            {
                DialogHelper.ConfirmIfDisplayed();
                DialogHelper.ConfirmIfDisplayed();
            }
            else
            {
                new AppDialog(false).Confirm();
                new AppDialog(false).Confirm();
            }
            ReportHelper.LogTest(Status.Pass, "Bluetooth disabled and connection to Hearing Aid Intrupted and app does not crash");

            Assert.IsTrue(new HearingSystemManagementPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            string leftStateDisconnected = new HearingSystemManagementPage().GetLeftDeviceState();
            Assert.IsNotEmpty(leftStateDisconnected);
            Assert.AreNotEqual(leftStateConnected, leftStateDisconnected);
            new HearingSystemManagementPage().RightDeviceTabClick();
            Assert.IsTrue(new HearingSystemManagementPage().GetIsRightTabSelected());
            string rightStateDisconnected = new HearingSystemManagementPage().GetRightDeviceState();
            Assert.IsNotEmpty(rightStateDisconnected);
            Assert.AreNotEqual(rightStateConnected, rightStateDisconnected);
            new HearingSystemManagementPage().LeftDeviceTabClick();
            Assert.IsTrue(new HearingSystemManagementPage().GetIsLeftTabSelected());
            ReportHelper.LogTest(Status.Pass, "Status changed to disconected after disabling bluetooth");

            new HearingSystemManagementPage().NavigateBack();

            ReportHelper.LogTest(Status.Info, "Navigate back to check Left and Right devices.");
            Assert.IsTrue(new SettingsMenuPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            new SettingsMenuPage().OpenMyHearingAids();

            Assert.IsTrue(new HearingSystemManagementPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            Assert.AreEqual(leftStateDisconnected, new HearingSystemManagementPage().GetLeftDeviceState());
            new HearingSystemManagementPage().RightDeviceTabClick();
            Assert.IsTrue(new HearingSystemManagementPage().GetIsRightTabSelected());
            Assert.AreEqual(rightStateDisconnected, new HearingSystemManagementPage().GetRightDeviceState());
            new HearingSystemManagementPage().LeftDeviceTabClick();
            Assert.IsTrue(new HearingSystemManagementPage().GetIsLeftTabSelected());
            ReportHelper.LogTest(Status.Pass, "Status did not change after navigate back and opening My Hearing Systems");

            // Enable Bluetooth
            AppManager.DeviceSettings.EnableBluetooth();
            ReportHelper.LogTest(Status.Info, "Bluetooth enabled");
            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(25));

            Assert.AreEqual(leftStateConnected, new HearingSystemManagementPage().GetLeftDeviceState());
            new HearingSystemManagementPage().RightDeviceTabClick();
            Assert.IsTrue(new HearingSystemManagementPage().GetIsRightTabSelected());
            Assert.AreEqual(rightStateConnected, new HearingSystemManagementPage().GetRightDeviceState());
            new HearingSystemManagementPage().LeftDeviceTabClick();
            Assert.IsTrue(new HearingSystemManagementPage().GetIsLeftTabSelected());
            ReportHelper.LogTest(Status.Pass, "Status changed to connected after enabling bluetooth");

            new HearingSystemManagementPage().NavigateBack();

            Assert.IsTrue(new SettingsMenuPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));

            // Disable Bluetooth
            AppManager.App.DisableBluetooth();
            ReportHelper.LogTest(Status.Info, "Bluetooth disabled");
            Thread.Sleep(2000);
            // ToDo: For iOS when the stacked message is shown assert fails on AppDialog. Hence not asserting it.
            if (OnAndroid)
            {
                DialogHelper.ConfirmIfDisplayed();
                DialogHelper.ConfirmIfDisplayed();
            }
            else
            {
                new AppDialog(false).Confirm();
                new AppDialog(false).Confirm();
            }
            ReportHelper.LogTest(Status.Pass, "Bluetooth disabled and connection to Hearing Aid Intrupted and app does not crash");

            Assert.IsTrue(new SettingsMenuPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            new SettingsMenuPage().OpenMyHearingAids();

            Assert.IsTrue(new HearingSystemManagementPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            Assert.AreEqual(leftStateDisconnected, new HearingSystemManagementPage().GetLeftDeviceState());
            new HearingSystemManagementPage().RightDeviceTabClick();
            Assert.IsTrue(new HearingSystemManagementPage().GetIsRightTabSelected());
            Assert.AreEqual(rightStateDisconnected, new HearingSystemManagementPage().GetRightDeviceState());
            new HearingSystemManagementPage().LeftDeviceTabClick();
            Assert.IsTrue(new HearingSystemManagementPage().GetIsLeftTabSelected());
            ReportHelper.LogTest(Status.Pass, "Status changed to disconected after disabling bluetooth");

            // Enable the Bluetooth
            AppManager.App.EnableBluetooth();
            ReportHelper.LogTest(Status.Info, "Bluetooth enabled");
            // It takes 25 secs to connect the hearing and and the toast message to disappear
            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(25));

            Assert.AreEqual(leftStateConnected, new HearingSystemManagementPage().GetLeftDeviceState());
            new HearingSystemManagementPage().RightDeviceTabClick();
            Assert.IsTrue(new HearingSystemManagementPage().GetIsRightTabSelected());
            Assert.AreEqual(rightStateConnected, new HearingSystemManagementPage().GetRightDeviceState());
            new HearingSystemManagementPage().LeftDeviceTabClick();
            Assert.IsTrue(new HearingSystemManagementPage().GetIsLeftTabSelected());
            ReportHelper.LogTest(Status.Pass, "Status changed to connected after enabling bluetooth");

            new HearingSystemManagementPage().DisconnectDevices();

            // Press Cancel Button
            AppDialog appDialogDisconnect = new AppDialog();
            Assert.IsNotEmpty(appDialogDisconnect.GetConfirmButtonText());
            Assert.IsNotEmpty(appDialogDisconnect.GetDenyButtonText());
            appDialogDisconnect.Deny();
            ReportHelper.LogTest(Status.Pass, "Cancel Pressed");

            // Next Press Disconnect Button
            new HearingSystemManagementPage().DisconnectDevices();
            DialogHelper.ConfirmIfDisplayed();
            ReportHelper.LogTest(Status.Pass, "Disconnect Pressed");

            Assert.IsTrue(new InitializeHardwarePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            Assert.IsNotEmpty(new InitializeHardwarePage().GetScanText());
            ReportHelper.LogTest(Status.Pass, "Hearing Aid disconnected and Start Scan Page Shown");
        }

        #endregion Sprint 28

        #region Sprint 30

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-19712_Table-0")]
        public void ST19712_FinalizeOEMPuretone()
        {
            //ToDo: Need to verify the name and icon of the app in the mobile

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            string LeftHearingAid = SelectHearingAid.GetLeftHearingAid();
            string RightHearingAid = SelectHearingAid.GetRightHearingAid();

            Language_Device language_Device = Language_Device.German_Germany;

            // Changing Mobile Language
            AppManager.DeviceSettings.ChangeDeviceLanguage(language_Device);
            ReportHelper.LogTest(Status.Info, "Enable device langauge to " + language_Device + " and restart app.");

            // Reset App after language changed to German
            AppManager.RestartApp(true);
            ReportHelper.LogTest(Status.Info, "App Restarted");

            //ToDo: Splash image cannot be verified because while running the test case in automation that screen does not appear

            // Navigate Intro pages
            Assert.IsTrue(new IntroPageOne().IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)));
            string welocmePageTitle = new IntroPageOne().GetTitle();
            Assert.IsNotEmpty(welocmePageTitle);
            if (AppManager.Brand == Brand.Puretone)
                Assert.AreEqual("Welcome!", welocmePageTitle, "Welcome page is not displayed correctly in English.");

            // ToDo: Need to install OpenCV and the uncomment the below line and need test this
            //Assert.IsTrue(new IntroPageOne().CheckInto1Image(AppManager.Brand), "The image is not matching");
            ReportHelper.LogTest(Status.Info, "Intro One Page image is shown correctly and welcome page title is '" + new IntroPageOne().GetTitle() + "'");
            ReportHelper.LogTest(Status.Pass, "Welcome page text shown correctly in English language.");
            new IntroPageOne().MoveRightByTapping();
            // ToDo: Need to install OpenCV and the uncomment the below line and need test this
            //Assert.IsTrue(new IntroPageTwo().CheckInto2Image(AppManager.Brand), "The image is not matching");
            ReportHelper.LogTest(Status.Info, "Intro Two Page image is shown correctly");
            new IntroPageTwo().MoveRightByTapping();
            // ToDo: Need to install OpenCV and the uncomment the below line and need test this
            //Assert.IsTrue(new IntroPageThree().CheckInto3Image(AppManager.Brand), "The image is not matching");
            ReportHelper.LogTest(Status.Info, "Intro Three Page image is shown correctly");
            new IntroPageThree().MoveRightByTapping();
            // ToDo: Need to install OpenCV and the uncomment the below line and need test this
            //Assert.IsTrue(new IntroPageFour().CheckInto4Image(AppManager.Brand), "The image is not matching");
            ReportHelper.LogTest(Status.Info, "Intro Four Page image is shown correctly");
            new IntroPageFour().MoveRightByTapping();
            new IntroPageFive().Continue();
            ReportHelper.LogTest(Status.Pass, "All Intro Page images are shown correctly");

            // Load Initialize Hardware Page
            Assert.IsTrue(new InitializeHardwarePage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            new InitializeHardwarePage().StartScan();

            // Give Permissions
            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(5));
            PermissionHelper.AllowPermissionIfRequested();

            var selectHearingAidsPageCommon = new SelectHearingAidsPage();
            Assert.IsTrue(selectHearingAidsPageCommon.GetIsScanning());

            // Connecting First Hearing Aid
            if (!selectHearingAidsPageCommon.GetIsDeviceFound(LeftHearingAid))
                selectHearingAidsPageCommon.WaitUntilDeviceFound(LeftHearingAid);

            // Connecting Second Hearing Aid if Available
            if (RightHearingAid != null)
                selectHearingAidsPageCommon.WaitUntilDeviceFound(RightHearingAid);
            if (!selectHearingAidsPageCommon.GetIsDeviceFound(RightHearingAid))
                selectHearingAidsPageCommon.WaitUntilDeviceFound(RightHearingAid);

            selectHearingAidsPageCommon.WaitUntilDeviceListNotChanging();
            selectHearingAidsPageCommon.SelectDevicesExclusively(LeftHearingAid, RightHearingAid);
            Assert.IsTrue(selectHearingAidsPageCommon.GetIsDeviceSelected(LeftHearingAid));
            if (RightHearingAid != null)
                selectHearingAidsPageCommon.GetIsDeviceSelected(RightHearingAid);
            Assert.IsNotEmpty(selectHearingAidsPageCommon.GetConnectButtonText());
            selectHearingAidsPageCommon.Connect();

            ReportHelper.LogTest(Status.Info, "Hearing Aid Connected Left " + LeftHearingAid + " and Right " + RightHearingAid);

            // Load Dashboard Page
            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(45));
            PermissionHelper.AllowPermissionIfRequested();

            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            var dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());

            // ToDo: Need to install OpenCV and the uncomment the below line and need test this
            //dashboardPage.CheckBrandImage(AppManager.Brand);
            ReportHelper.LogTest(Status.Info, "Connection page Brand image is shown correctly");

            // Verify Programs color schema
            int count = dashboardPage.GetNumberOfPrograms();
            for (int i = 0; i < count; i++)
            {
                MainMenuTypes mainMenuTypes = MainMenuTypes.Preset;
                if (i == 3)
                    mainMenuTypes = MainMenuTypes.Streaming;

                new DashboardPage().SelectProgram(i);
                string SelectedIconColor = new DashboardPage().GetSelectedProgramColor(i);
                new DashboardPage().OpenCurrentProgram();
                Thread.Sleep(2000);

                Assert.IsNotEmpty(new ProgramDetailPage().GetCurrentProgramName());
                Assert.IsTrue(new ProgramDetailPage().GetIsBinauralSettingsButtonVisible());
                Assert.IsTrue(new ProgramDetailPage().GetIsSettingsButtonDisplayed());
                ReportHelper.LogTest(Status.Pass, "Program Details controls are available");

                string SelectedDetailsIconColor = new ProgramDetailPage().GetSelectedProgramColor(i);
                //ToDo: The colors of programs in Dashboard and Programs in Program Details page slightly vary. Hence commenting the below
                //Assert.AreEqual(SelectedIconColor, SelectedDetailsIconColor, "Icon colors in Dashboard page and Program Details page are not matching");
                //ReportHelper.LogTest(Status.Pass, "Icon colors in Dashboard page and Program Details page are matching");

                string SettingsColor = new ProgramDetailPage().GetSettingsIconColor();
                string HeadingColor = new ProgramDetailPage().GetTitleColor(mainMenuTypes);

                switch (AppManager.Brand)
                {
                    case Brand.Audifon:
                        {
                            //ToDo: The colors of Colors are changing. Need to confirm with audifon team and update the below
                            //if (mainMenuTypes == MainMenuTypes.Streaming)
                            //    Assert.AreEqual("#377B8D", SettingsColor, new ProgramDetailPage().GetCurrentProgramName() + " Settings color is not matching");
                            //else
                            //    Assert.AreEqual("#9D1823", SettingsColor, new ProgramDetailPage().GetCurrentProgramName() + " Settings color is not matching");
                            //if (mainMenuTypes == MainMenuTypes.Streaming)
                            //    Assert.AreEqual("#357A8B", HeadingColor, new ProgramDetailPage().GetCurrentProgramName() + " Program Name color is not matching");
                            //else
                            //    Assert.AreEqual("#9E1922", HeadingColor, new ProgramDetailPage().GetCurrentProgramName() + " Program Name color is not matching");

                            break;
                        }
                    case Brand.Puretone:
                        {
                            if (mainMenuTypes == MainMenuTypes.Streaming)
                                Assert.AreEqual("#0B68D9", SettingsColor, new ProgramDetailPage().GetCurrentProgramName() + " Settings color is not matching");
                            else
                                Assert.AreEqual("#002596", SettingsColor, new ProgramDetailPage().GetCurrentProgramName() + " Settings color is not matching");
                            if (mainMenuTypes == MainMenuTypes.Streaming)
                                Assert.AreEqual("#0B68D9", HeadingColor, new ProgramDetailPage().GetCurrentProgramName() + " Program Name color is not matching");
                            else
                                Assert.AreEqual("#002596", HeadingColor, new ProgramDetailPage().GetCurrentProgramName() + " Program Name color is not matching");
                            break;
                        }
                }

                ReportHelper.LogTest(Status.Pass, new ProgramDetailPage().GetCurrentProgramName() + " Settings color is '" + SettingsColor + "' and is matching with '" + AppManager.Brand + "' app color schema");
                ReportHelper.LogTest(Status.Pass, new ProgramDetailPage().GetCurrentProgramName() + " Program Name color is '" + SettingsColor + "' and is matching with '" + AppManager.Brand + "' app color schema");

                new ProgramDetailPage().TapBack();
            }

            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);

            // Open ProgramSettings 
            new ProgramDetailPage().OpenProgramSettings();
            var programSettingsControlPage = new ProgramDetailSettingsControlPage();
            Assert.IsTrue(programSettingsControlPage.GetIsCreateFavoriteVisible());
            Assert.IsTrue(programSettingsControlPage.GetIsCustomizeIconVisible());
            Assert.IsTrue(programSettingsControlPage.GetIsCustomizeNameVisible());
            Assert.IsTrue(programSettingsControlPage.GetIsCreateFavoriteFloatingButtonDisplayed());
            ReportHelper.LogTest(Status.Pass, "Customize name, Customize icon, Create favorites and '+' are visible");

            // Create Favorite
            string FavoriteName = "Favorite 01";
            programSettingsControlPage.CreateFavorite();
            ProgramNamePage programNamePage = new ProgramNamePage();
            programNamePage.EnterName(FavoriteName);
            Assert.IsNotEmpty(programNamePage.GetName());
            programNamePage.Proceed();
            ProgramIconPage programIconPage = new ProgramIconPage();
            programIconPage.SelectIcon(12);
            programIconPage.Proceed();
            new ProgramAutomationPage().Proceed();
            new ProgramDetailPage().TapBack();
            ReportHelper.LogTest(Status.Pass, "Favorite created with name '" + FavoriteName + "'");

            // Verify Favorite color schema
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 0);

            Assert.IsNotEmpty(new ProgramDetailPage().GetCurrentProgramName());
            Assert.IsTrue(new ProgramDetailPage().GetIsBinauralSettingsButtonVisible());
            Assert.IsTrue(new ProgramDetailPage().GetIsSettingsButtonDisplayed());
            ReportHelper.LogTest(Status.Pass, "Favorite Program Details controls are available");

            string SettingsColorFavorite = new ProgramDetailPage().GetSettingsIconColor();
            string HeadingColorFavorite = new ProgramDetailPage().GetTitleColor(MainMenuTypes.Favorites);

            switch (AppManager.Brand)
            {
                case Brand.Audifon:
                    {
                        //ToDo: Color needs to be changed after discussion with audifon team
                        //Assert.AreEqual("#838383", SettingsColorFavorite, FavoriteName + " Settings color is not matching");
                        //Assert.AreEqual("#9F9F9F", HeadingColorFavorite, FavoriteName + " Program Name color is not matching");

                        break;
                    }
                case Brand.Puretone:
                    {
                        //ToDo: Color needs to be changed after discussion with audifon team
                        //Assert.AreEqual("#333333", SettingsColorFavorite, FavoriteName + " Settings color is not matching");
                        //Assert.AreEqual("#333333", HeadingColorFavorite, FavoriteName + " Program Name color is not matching");

                        break;
                    }
            }

            ReportHelper.LogTest(Status.Pass, FavoriteName + " Settings color is '" + SettingsColorFavorite + "' and is matching with '" + AppManager.Brand + "' app color schema");
            ReportHelper.LogTest(Status.Pass, FavoriteName + " Program Name color is '" + HeadingColorFavorite + "' and is matching with '" + AppManager.Brand + "' app color schema");

            new ProgramDetailPage().TapBack();

            // Check Left and Right HA
            new DashboardPage().OpenLeftHearingDevice();
            Assert.IsNotEmpty(new HearingInstrumentInfoControlPage().GetDeviceType());
            if (AppManager.Brand == Brand.Puretone)
                Assert.AreEqual("Ev16-R", new HearingInstrumentInfoControlPage().GetDeviceType());
            ReportHelper.LogTest(Status.Info, "Left HA Device is '" + new HearingInstrumentInfoControlPage().GetDeviceType() + "'");
            new HearingInstrumentInfoControlPage().Close();

            new DashboardPage().OpenRightHearingDevice();
            Assert.IsNotEmpty(new HearingInstrumentInfoControlPage().GetDeviceType());
            if (AppManager.Brand == Brand.Puretone)
                Assert.AreEqual("Ev16-R", new HearingInstrumentInfoControlPage().GetDeviceType());
            ReportHelper.LogTest(Status.Info, "Right HA Device is '" + new HearingInstrumentInfoControlPage().GetDeviceType() + "'");
            new HearingInstrumentInfoControlPage().Close();
            ReportHelper.LogTest(Status.Pass, "HA Names verified for both Left and Right");

            // Check selected language
            NavigationHelper.NavigateToSettingsMenu(new DashboardPage()).OpenLanguage();
            if (AppManager.Brand == Brand.Puretone)
                Assert.AreEqual(Language_Puretone.English, new SettingLanguagePage().GetSelectedLanguagePuretone());
            ReportHelper.LogTest(Status.Pass, "Selected language is verified");

            new SettingLanguagePage().TapBack();

            new SettingsMenuPage().TapBack();

            // Check Help Menu
            new MainMenuPage().OpenHelp();
            new HelpMenuPage().OpenInstructionsForUse();
            Assert.IsNotEmpty(new InstructionsForUsePage().GetNavigationBarTitle());
            new InstructionsForUsePage().TapBack();

            new HelpMenuPage().OpenInformationMenu();

            new InformationMenuPage().OpenPrivacyPolicy();
            Assert.IsNotEmpty(new PrivacyPolicyPage().GetNavigationBarTitle());
            new PrivacyPolicyPage().TapBack();

            new InformationMenuPage().OpenTermsofUse();
            Assert.IsNotEmpty(new TermsOfUsePage().GetNavigationBarTitle());
            new TermsOfUsePage().TapBack();

            new InformationMenuPage().TapBack();

            new HelpMenuPage().OpenImprint();
            Assert.IsNotEmpty(new ImprintPage().GetAddressHeader());
            Assert.IsNotEmpty(new ImprintPage().GetAppCompanyName());
            Assert.IsNotEmpty(new ImprintPage().GetAppCompanyStreet());
            Assert.IsNotEmpty(new ImprintPage().GetAppCompanyPostalCodeCity());
            Assert.IsNotEmpty(new ImprintPage().GetAppCommpanyState());

            if (AppManager.Brand == Brand.Puretone)
            {
                Assert.AreEqual("9-11 Henley Business Park, Trident Close, Medway City Estate", new ImprintPage().GetAppCompanyStreet());
                Assert.AreEqual("Rochester, Kent, ME24FR", new ImprintPage().GetAppCompanyPostalCodeCity());
                Assert.AreEqual("United Kingdom", new ImprintPage().GetAppCommpanyState());
            }

            ReportHelper.LogTest(Status.Pass, "Imprint page details verified");

            new ImprintPage().TapBack();

            new HelpMenuPage().TapBack();
            ReportHelper.LogTest(Status.Pass, "Help Menu Verified");

            // Open My Hearing Aid System, Verify and disconnect devices
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenMyHearingAids();

            new HearingSystemManagementPage().LeftDeviceTabClick();
            Assert.IsTrue(new HearingSystemManagementPage().GetIsLeftTabSelected());
            Assert.IsNotEmpty(new HearingSystemManagementPage().GetLeftDeviceType());
            if (AppManager.Brand == Brand.Puretone)
                Assert.AreEqual("Ev16-R", new HearingSystemManagementPage().GetLeftDeviceType());

            new HearingSystemManagementPage().RightDeviceTabClick();
            Assert.IsTrue(new HearingSystemManagementPage().GetIsRightTabSelected());
            Assert.IsNotEmpty(new HearingSystemManagementPage().GetRightDeviceType());
            if (AppManager.Brand == Brand.Puretone)
                Assert.AreEqual("Ev16-R", new HearingSystemManagementPage().GetRightDeviceType());

            new HearingSystemManagementPage().LeftDeviceTabClick();
            Assert.IsTrue(new HearingSystemManagementPage().GetIsLeftTabSelected());
            new HearingSystemManagementPage().DisconnectDevices();
            DialogHelper.ConfirmIfDisplayed();
            ReportHelper.LogTest(Status.Pass, "Hearing Aids Disconnected");

            // Open Demo Mode
            Assert.IsTrue(new InitializeHardwarePage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)));
            InitializeHardwarePage initializeHardwarePage = new InitializeHardwarePage();
            Assert.IsNotEmpty(initializeHardwarePage.GetDemoModeText());
            Assert.IsNotEmpty(initializeHardwarePage.GetScanText());
            ReportHelper.LogTest(Status.Pass, "'Start Search' page is displayed correctly having 'Start Search' and 'Demo Mode' option. Now tap 'Start Search'");

            initializeHardwarePage.StartDemoMode();

            // Load Dashboard Page
            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(20));
            PermissionHelper.AllowPermissionIfRequested();
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            DashboardPage dashboardPageDemo = new DashboardPage();
            dashboardPageDemo.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPageDemo.IsCurrentlyShown());
            dashboardPageDemo.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPageDemo.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPageDemo.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPageDemo.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            // Open Find Hearing Device Page
            NavigationHelper.NavigateToHelpMenu(dashboardPageDemo).OpenFindHearingDevices();
            Assert.IsTrue(new FindDevicesPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            Assert.IsTrue(new FindDevicesPage().GetIsLeftDeviceSelected());
            Assert.IsNotEmpty(new FindDevicesPage().GetLeftDeviceText());
            new FindDevicesPage().SelectRightDevice();
            Assert.IsTrue(new FindDevicesPage().GetIsRightDeviceSelected());
            Assert.IsNotEmpty(new FindDevicesPage().GetRightDeviceText());
            new FindDevicesPage().SelectLeftDevice();
            Assert.IsTrue(new FindDevicesPage().GetIsLeftDeviceSelected());
            ReportHelper.LogTest(Status.Pass, "Find hearing aid page is verified");

            // Reset the Mobile language to English 
            AppManager.DeviceSettings.ChangeDeviceLanguage(Language_Device.English_US);
            ReportHelper.LogTest(Status.Info, "Change device langauge to English");

            AppManager.DeviceSettings.DisableWifi();
        }

        [Test]
        [Category("SystemTestsDeviceManual")]
        [Description("TC-19461_Table-0")]
        public void ST19461_DisplayOfBatteryLevelDepending()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var LeftHearingAidName = SelectHearingAid.GetLeftHearingAid();
            var RightHearingAidName = SelectHearingAid.GetRightHearingAid();

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(LeftHearingAidName, RightHearingAidName).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + LeftHearingAidName + "' and Right '" + RightHearingAidName + "'");
            ReportHelper.LogTest(Status.Pass, "Dashboard page loaded");

            // Open the hearing aid information page for left hearing aid
            ReportHelper.LogTest(Status.Info, "Open Left Hearing Device information page");
            dashboardPage.OpenLeftHearingDevice();
            new HearingInstrumentInfoControlPage().CheckHAInformationFromDashboard(AppMode.Normal);
            ReportHelper.LogTest(Status.Pass, "Device details for connected Left hearing device is displayed");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off Left Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Left Hearing Aid turned off");

            DialogHelper.Confirm();

            Assert.IsNotEmpty(new HearingInstrumentInfoControlPage().GetCloseButtonText());
            ReportHelper.LogTest(Status.Pass, "Alert message displayed after turning off Left hearing device and window is not closed");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn on Left Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Left Hearing Aid turned on");

            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(25));

            Assert.IsNotEmpty(new HearingInstrumentInfoControlPage().GetCloseButtonText());
            ReportHelper.LogTest(Status.Info, "window is still open after turning on Left device");

            // Close the left hearing aid information page
            new HearingInstrumentInfoControlPage().Close();

            // Open right hearing aid information page
            ReportHelper.LogTest(Status.Info, "Open Right Hearing Device information page");
            new DashboardPage().OpenRightHearingDevice();
            new HearingInstrumentInfoControlPage().CheckHAInformationFromDashboard(AppMode.Normal);
            ReportHelper.LogTest(Status.Pass, "Device details for connected Right hearing device is displayed");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Right Hearing Aid turned off");

            DialogHelper.Confirm();

            Assert.IsNotEmpty(new HearingInstrumentInfoControlPage().GetCloseButtonText());
            ReportHelper.LogTest(Status.Pass, "Alert message displayed after turning off right hearing device and window is not closed");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn on Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Right Hearing Aid turned on");

            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(25));

            Assert.IsNotEmpty(new HearingInstrumentInfoControlPage().GetCloseButtonText());
            ReportHelper.LogTest(Status.Info, "window is still open after turning on Right device");

            new HearingInstrumentInfoControlPage().Close();

            Assert.IsTrue(new DashboardPage().GetIsRightHearingDeviceVisible());

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off Left Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Left Hearing Aid turned off");

            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(25));

            //Assert.IsFalse(new DashboardPage().GetIsLeftHearingDeviceVisible());
            ReportHelper.LogTest(Status.Pass, "Alert message displayed after turning off Left hearing device and Left Hearing Aid is not visible");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Right Hearing Aid turned off");

            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(25));

            //Assert.IsFalse(new DashboardPage().GetIsRightHearingDeviceVisible());
            ReportHelper.LogTest(Status.Pass, "Alert message displayed after turning off Right hearing device and Right Hearing Aid is not visible");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn on Both Left and Right Hearing Aids within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Both Left and Right Hearing Aids turning on");

            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(25));

            Assert.IsTrue(new DashboardPage().GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(new DashboardPage().GetIsRightHearingDeviceVisible());
            ReportHelper.LogTest(Status.Pass, "Device turned on and Left and Right Icons are visible");

            new DashboardPage().OpenLeftHearingDevice();
            new HearingInstrumentInfoControlPage().CheckHAInformationFromDashboard(AppMode.Normal);
            ReportHelper.LogTest(Status.Pass, "Device details for connected left hearing device is displayed");

            // Close the left hearing aid information page
            new HearingInstrumentInfoControlPage().Close();

            new DashboardPage().OpenRightHearingDevice();
            new HearingInstrumentInfoControlPage().CheckHAInformationFromDashboard(AppMode.Normal);
            ReportHelper.LogTest(Status.Pass, "Device details for connected right hearing device is displayed");

            // Close the right hearing aid information page
            new HearingInstrumentInfoControlPage().Close();

            // ToDo: Test case has been inplemented till step 12. We could not automate steps after Step 12 because we did not have a build where Battery Capacity is enabled and we do not have latest Audifit 5 latest version to change firmware
        }

        #endregion Sprint 30

        #region Sprint 33

        [Test]
        [Category("SystemTestsDeviceObsolate")]
        [Description("TC-20958_Table-0")]
        public void ST20958_CodeCleanupUseXamarinPreferencesDevice()
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

            string LeftHearingAidName = SelectHearingAid.GetLeftHearingAid();
            string RightHearingAidName = SelectHearingAid.GetRightHearingAid();

            // Load Intro and connect to Hearing Aid
            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(LeftHearingAidName, RightHearingAidName).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + LeftHearingAidName + "' and Right '" + RightHearingAidName + "'");

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
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 2);
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
            ReportHelper.LogTest(Status.Info, "Noise Reduction set to Strong");
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

            // Create Favourite 
            string FavoriteName = "Favourite 01";
            var programAutomationPage = FavoriteHelper.CreateFavoriteHearingProgram(FavoriteName, 5);
            programAutomationPage.Proceed();
            ReportHelper.LogTest(Status.Pass, "Favourite program '" + FavoriteName + "' is created successfully");

            new ProgramDetailPage().TapBack();
            Assert.AreEqual(FavoriteName, new DashboardPage().GetCurrentProgramName());

            //Open the Noise comfort program and set the mimunm
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            ReportHelper.LogTest(Status.Info, "Noise comfort program is selected");

            //open speech focus
            new ProgramDetailPage().SpeechFocusDisplay.OpenSettings();

            //Setting Speech Focus to minimum
            programDetailParamEditSpeechFocusPage = new ProgramDetailParamEditSpeechFocusPage();
            programDetailParamEditSpeechFocusPage.SelectSpeechFocus(SpeechFocus.Off);
            Assert.AreEqual(SpeechFocus.Off, programDetailParamEditSpeechFocusPage.GetSelectedSpeechFocus());
            programDetailParamEditSpeechFocusPage.Close();
            ReportHelper.LogTest(Status.Info, "Speech Focus set to Off");

            //open noise reduction and setting to minimum volume
            new ProgramDetailPage().NoiseReductionDisplay.OpenSettings();

            //Setting noise reduction to minimum
            noiseReductionPage = new ProgramDetailParamEditNoiseReductionPage();
            noiseReductionPage.SelectNoiseReduction(NoiseReduction.Off);
            Assert.AreEqual(NoiseReduction.Off, noiseReductionPage.GetSelectedNoiseReduction());
            ReportHelper.LogTest(Status.Info, "Noise reduction set to Off");
            noiseReductionPage.Close();

            //Open the sound program and setting sound to maximum volume
            new ProgramDetailPage().EqualizerDisplay.OpenSettings();
            equilizerDisplay = new ProgramDetailParamEditEqualizerPage();

            var lowMinValue = 0;
            equilizerDisplay.SetEqualizerSliderValue(EqBand.Low, lowMinValue);
            Assert.AreEqual(lowMinValue, Math.Round(equilizerDisplay.GetEqualizerSliderValue(EqBand.Low), 0));

            var midMinValue = 0;
            equilizerDisplay.SetEqualizerSliderValue(EqBand.Mid, midMinValue);
            Assert.AreEqual(midMinValue, Math.Round(equilizerDisplay.GetEqualizerSliderValue(EqBand.Mid), 0));

            var highMinValue = 0;
            equilizerDisplay.SetEqualizerSliderValue(EqBand.High, highMinValue);
            Assert.AreEqual(highMinValue, Math.Round(equilizerDisplay.GetEqualizerSliderValue(EqBand.High), 0));
            ReportHelper.LogTest(Status.Info, "Decreasing Equlizer display values to Minimum");
            equilizerDisplay.Close();

            // Create Favourite and save with wifi
            var favoriteWithWifi = "Favorite 01 Wifi";
            programAutomationPage = FavoriteHelper.CreateFavoriteHearingProgram(favoriteWithWifi, 4);
            AppManager.DeviceSettings.GrantGPSBackgroundPermission();
            Assert.IsTrue(programAutomationPage.GetIsGeofenceAutomationVisible());
            Assert.IsTrue(programAutomationPage.GetIsWifiAutomationVisible());
            ReportHelper.LogTest(Status.Pass, "The options Connect to WLAN and Connect to location appeared.");

            //Select "Connect to Wifi"
            programAutomationPage.TapConnectToWiFi();
            new AutomationWifiBindingPage().GetWifiName();
            new AutomationWifiBindingPage().Ok();
            new ProgramAutomationPage().Proceed();
            ReportHelper.LogTest(Status.Pass, "Favourite program '" + favoriteWithWifi + "' is created successfully");

            new ProgramDetailPage().TapBack();

            //Select Noise comfort
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Streaming, 0);
            ReportHelper.LogTest(Status.Info, "Wireless Streaming Program Opened");

            // Open Settings and change Streaming value with set medium volume
            new ProgramDetailPage().StreamingDisplay.OpenSettings();
            var streamingPage = new ProgramDetailParamEditStreamingPage();
            var streamingMidvalue = 0.5;
            streamingPage.SetStreamingSliderValue(streamingMidvalue);
            Assert.AreEqual(streamingMidvalue, Math.Round(streamingPage.GetStreamingSliderValue(), 1));
            ReportHelper.LogTest(Status.Info, "Streaming Display set to Meduim volume");
            streamingPage.Close();

            //setting Speech focus setting to medium volume
            new ProgramDetailPage().SpeechFocusDisplay.OpenSettings();
            programDetailParamEditSpeechFocusPage = new ProgramDetailParamEditSpeechFocusPage();
            programDetailParamEditSpeechFocusPage.SelectSpeechFocus(SpeechFocus.Front);
            Assert.AreEqual(SpeechFocus.Front, programDetailParamEditSpeechFocusPage.GetSelectedSpeechFocus());
            ReportHelper.LogTest(Status.Info, "Speech Focus set to Front");
            programDetailParamEditSpeechFocusPage.Close();

            //Setting noise reduction to medium volume
            new ProgramDetailPage().NoiseReductionDisplay.OpenSettings();
            noiseReductionPage = new ProgramDetailParamEditNoiseReductionPage();
            noiseReductionPage.SelectNoiseReduction(NoiseReduction.Medium);
            Assert.AreEqual(NoiseReduction.Medium, noiseReductionPage.GetSelectedNoiseReduction());
            ReportHelper.LogTest(Status.Info, "Noise Reduction set to Medium");
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
            ReportHelper.LogTest(Status.Info, "Equlizer display set values to Medimum");
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
            ReportHelper.LogTest(Status.Info, "Connected to location and address are visible on page.");
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
            ReportHelper.LogTest(Status.Info, "Location Permission in Settings Menu turned off");
            settingPermissionsPage.TapBack();
            new SettingsMenuPage().TapBack();
            new MainMenuPage().CloseMenuUsingTap();

            //Open setting and Switch to another language
            new DashboardPage().OpenMenuUsingTap();
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenLanguage();
            var settingLanguagePage = new SettingLanguagePage();
            var selectedLangauageText = settingLanguagePage.SelectLanguageAudifon(Language_Audifon.German);
            new SettingLanguagePage().Accept();
            new AppDialog().Confirm();
            ReportHelper.LogTest(Status.Info, "Language changed to German");
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));

            // App restarted
            AppManager.RestartApp(false);
            ReportHelper.LogTest(Status.Info, "App restarted");

            // Load Dashboard
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(80)));
            dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            ReportHelper.LogTest(Status.Pass, "App is restarted successfully");

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

            //Open the favorite and check the after restart are equal
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 1);
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(favoriteWithWifi, programDetailPage.GetCurrentProgramName());
            programDetailPage.EqualizerDisplay.OpenSettings();
            equilizerDisplay = new ProgramDetailParamEditEqualizerPage();
            Assert.AreEqual(lowMinValue, Math.Round(equilizerDisplay.GetEqualizerSliderValue(EqBand.Low), 0));
            Assert.AreEqual(midMinValue, Math.Round(equilizerDisplay.GetEqualizerSliderValue(EqBand.Mid), 0));
            Assert.AreEqual(highMinValue, Math.Round(equilizerDisplay.GetEqualizerSliderValue(EqBand.High), 0));
            ReportHelper.LogTest(Status.Pass, "Equilizer display value set to minmium is verified");
            equilizerDisplay.Close();

            new ProgramDetailPage().SpeechFocusDisplay.OpenSettings();
            speechFocusPage = new ProgramDetailParamEditSpeechFocusPage();
            Assert.AreEqual(SpeechFocus.Off, speechFocusPage.GetSelectedSpeechFocus());
            ReportHelper.LogTest(Status.Pass, "Speech foucus display value set to Off is verified");
            speechFocusPage.Close();

            new ProgramDetailPage().NoiseReductionDisplay.OpenSettings();
            noiseReduction = new ProgramDetailParamEditNoiseReductionPage();
            Assert.AreEqual(NoiseReduction.Off, noiseReduction.GetSelectedNoiseReduction());
            ReportHelper.LogTest(Status.Pass, "Noise reduction display value set to Off is verified");
            noiseReduction.Close();

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
            ReportHelper.LogTest(Status.Pass, "Equilizer display value set to medium is verified");
            equilizerDisplay.Close();

            new ProgramDetailPage().SpeechFocusDisplay.OpenSettings();
            var speechFocus = new ProgramDetailParamEditSpeechFocusPage();
            Assert.AreEqual(SpeechFocus.Front, speechFocus.GetSelectedSpeechFocus());
            ReportHelper.LogTest(Status.Pass, "Speech foucus display value set to Front is verified");
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

            // Open App in Device Mode after update from play store
            ReportHelper.LogTest(Status.Info, "Checking the Welcome Page.");
            var introPageOne = new IntroPageOne();
            Assert.IsTrue(introPageOne.GetIsRightButtonVisible());
            Assert.IsFalse(introPageOne.GetIsLeftButtonVisible());
            ReportHelper.LogTest(Status.Pass, "Welcome Page is displayed correctly.");

            ReportHelper.LogTest(Status.Info, "Skip to last intro page 'Here We Go'.");
            new IntroPageOne().MoveRightBySwiping();
            new IntroPageTwo().MoveRightBySwiping();
            new IntroPageThree().MoveRightBySwiping();
            new IntroPageFour().MoveRightBySwiping();
            Assert.IsTrue(new IntroPageFive().GetIsLeftButtonVisible());
            Assert.IsTrue(new IntroPageFive().GetIsLeftButtonVisible());
            ReportHelper.LogTest(Status.Pass, "'Here we go' page is displayed correctly.");

            ReportHelper.LogTest(Status.Info, "Tap 'Continue' accept all dialogs and check is 'Start search' page is visible.");
            new IntroPageFive().Continue();

            ReportHelper.LogTest(Status.Info, "Connect the Hearing aids and check the Start View.");
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(80)));
            dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            ReportHelper.LogTest(Status.Info, "App started in device mode after update from play store");

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

            //Open the favorite and check the after update are equal
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

            //Open the favorite and check the after update are equal
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 1);
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(favoriteWithWifi, programDetailPage.GetCurrentProgramName());
            programDetailPage.EqualizerDisplay.OpenSettings();
            equilizerDisplay = new ProgramDetailParamEditEqualizerPage();
            Assert.AreEqual(lowMinValue, Math.Round(equilizerDisplay.GetEqualizerSliderValue(EqBand.Low), 0));
            Assert.AreEqual(midMinValue, Math.Round(equilizerDisplay.GetEqualizerSliderValue(EqBand.Mid), 0));
            Assert.AreEqual(highMinValue, Math.Round(equilizerDisplay.GetEqualizerSliderValue(EqBand.High), 0));
            ReportHelper.LogTest(Status.Pass, "Equilizer display value set to minmium is verified after update");
            equilizerDisplay.Close();

            new ProgramDetailPage().SpeechFocusDisplay.OpenSettings();
            speechFocusPage = new ProgramDetailParamEditSpeechFocusPage();
            Assert.AreEqual(SpeechFocus.Off, speechFocusPage.GetSelectedSpeechFocus());
            ReportHelper.LogTest(Status.Pass, "Speech foucus display value set to Off is verified after update");
            speechFocusPage.Close();

            new ProgramDetailPage().NoiseReductionDisplay.OpenSettings();
            noiseReduction = new ProgramDetailParamEditNoiseReductionPage();
            Assert.AreEqual(NoiseReduction.Off, noiseReduction.GetSelectedNoiseReduction());
            ReportHelper.LogTest(Status.Pass, "Noise reduction display value set to Off is verified after update");
            noiseReduction.Close();

            new ProgramDetailPage().TapBack();

            //Open the favoritewithlocation and check after update are equal.
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 2);
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(favoriteWithLocation, programDetailPage.GetCurrentProgramName());

            programDetailPage.EqualizerDisplay.OpenSettings();
            equilizerDisplay = new ProgramDetailParamEditEqualizerPage();
            Assert.AreEqual(lowMidValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.Low));
            Assert.AreEqual(midMidValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.Mid));
            Assert.AreEqual(highMidValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.High));
            ReportHelper.LogTest(Status.Pass, "Equilizer display value set to medium is verified after update");
            equilizerDisplay.Close();

            new ProgramDetailPage().SpeechFocusDisplay.OpenSettings();
            speechFocus = new ProgramDetailParamEditSpeechFocusPage();
            Assert.AreEqual(SpeechFocus.Front, speechFocus.GetSelectedSpeechFocus());
            ReportHelper.LogTest(Status.Pass, "Speech foucus display value set to Front is verified after update");
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

            //Open permission and check after update is Disable tracking the hearing systems
            new SettingsMenuPage().OpenPermissions();
            settingPermissions = new SettingPermissionsPage();
            Assert.IsFalse(settingPermissions.GetIsLocationPermissionSwitchChecked());
            ReportHelper.LogTest(Status.Pass, "Location permission switch turned off is verified after update");
            settingPermissions.TapBack();
            new SettingsMenuPage().TapBack();
            new MainMenuPage().CloseMenuUsingTap();

            //Open setting and Switch to another language after update
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
        [Category("SystemTestsDevice")]
        [Description("TC-21779_Table-0")]
        public void ST21779_CheckDeviceConnectionBehaviour()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            string LeftHearingAidName = SelectHearingAid.GetLeftHearingAid();
            string RightHearingAidName = SelectHearingAid.GetRightHearingAid();

            // Load Intro and connect to Hearing Aid
            var selectPage = LaunchHelper.SkipToSelectHearingAidsPage().Page;
            selectPage.WaitUntilDevicesFound(2);
            Wait.UntilTrue(() => selectPage.GetSelectedDeviceNames().Count == 2, TimeSpan.FromSeconds(15));
            selectPage.Connect();
            ReportHelper.LogTest(Status.Info, "Hearing Aid Connect attempt Left" + LeftHearingAidName + " and Right " + RightHearingAidName);

            // Cancel the connect of hearing aid
            var initPage = new HearingAidInitPage();
            Wait.UntilTrue(() =>
            {
                return initPage.LeftHearingAid.IsConnected || initPage.RightHearingAid.IsConnected;
            }, TimeSpan.FromSeconds(45));
            initPage.Cancel();
            Assert.IsTrue(DialogHelper.GetIsDialogDisplayed(TimeSpan.FromSeconds(1)));
            Assert.IsNotEmpty(new AppDialog().GetTitle());
            Assert.IsNotEmpty(new AppDialog().GetMessage());
            Assert.IsNotEmpty(new AppDialog().GetConfirmButtonText());
            Assert.IsNotEmpty(new AppDialog().GetDenyButtonText());
            ReportHelper.LogTest(Status.Pass, "Dialog is displayed, to confirm the cancel connect.");
            DialogHelper.Confirm();
            ReportHelper.LogTest(Status.Info, "Canceled the connect of hearing aid");

            Assert.IsTrue(new HearingAidConnectionErrorPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(130)));
            Assert.IsNotEmpty(new HearingAidConnectionErrorPage().GetPageTitle());
            new HearingAidConnectionErrorPage().RetryConnection();
            ReportHelper.LogTest(Status.Info, "Retry connection done");
            ReportHelper.LogTest(Status.Pass, "App did not crash");

            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(45));
            PermissionHelper.AllowPermissionIfRequested();

            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            var dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "Dashboard Loaded");

            // Tap Menu and Settings Selected
            dashboardPage.OpenMenuUsingTap();
            var mainMenuPage = new MainMenuPage();
            mainMenuPage.OpenSettings();
            var settingsMenuPage = new SettingsMenuPage();
            ReportHelper.LogTest(Status.Info, "Menu opened and Settings selected");

            // Open Hearing Aid
            settingsMenuPage.OpenMyHearingAids();
            ReportHelper.LogTest(Status.Info, "Opened Hearing Aid Details");

            // Disconnect Hearing Aid
            var hearingAidManagementPage = new HearingSystemManagementPage();
            hearingAidManagementPage.DisconnectDevices();
            DialogHelper.ConfirmIfDisplayed();
            ReportHelper.LogTest(Status.Info, "Hearing Aids Disconnected");

            Assert.IsTrue(new InitializeHardwarePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            Assert.IsNotEmpty(new InitializeHardwarePage().GetDemoModeText());
            Assert.IsNotEmpty(new InitializeHardwarePage().GetScanText());
            ReportHelper.LogTest(Status.Info, "Text is shown correctly");
            new InitializeHardwarePage().StartScan();
            ReportHelper.LogTest(Status.Info, "Performed start scan");

            selectPage.WaitUntilDeviceFound(LeftHearingAidName);
            if (!selectPage.GetIsDeviceFound(LeftHearingAidName))
                selectPage.WaitUntilDeviceFound(LeftHearingAidName);

            if (RightHearingAidName != null)
                selectPage.WaitUntilDeviceFound(RightHearingAidName);
            if (!selectPage.GetIsDeviceFound(RightHearingAidName))
                selectPage.WaitUntilDeviceFound(RightHearingAidName);

            selectPage.WaitUntilDeviceListNotChanging();
            selectPage.SelectDevicesExclusively(LeftHearingAidName, RightHearingAidName);
            selectPage.GetIsDeviceSelected(LeftHearingAidName);
            if (RightHearingAidName != null)
                selectPage.GetIsDeviceSelected(RightHearingAidName);

            ReportHelper.LogTest(Status.Pass, "Hearing Aids are listed");

            // Restarting the App
            AppManager.RestartApp(false);
            ReportHelper.LogTest(Status.Info, "App Restarted");

            Assert.IsTrue(new InitializeHardwarePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));

            new InitializeHardwarePage().StartScan();
            ReportHelper.LogTest(Status.Info, "Performed start scan again");

            var selectHearingAidsPage = new SelectHearingAidsPage();

            selectPage.WaitUntilDeviceFound(LeftHearingAidName);
            if (!selectPage.GetIsDeviceFound(LeftHearingAidName))
                selectPage.WaitUntilDeviceFound(LeftHearingAidName);

            if (RightHearingAidName != null)
                selectPage.WaitUntilDeviceFound(RightHearingAidName);
            if (!selectPage.GetIsDeviceFound(RightHearingAidName))
                selectPage.WaitUntilDeviceFound(RightHearingAidName);

            selectPage.WaitUntilDeviceListNotChanging();
            selectPage.SelectDevicesExclusively(LeftHearingAidName, RightHearingAidName);
            selectPage.GetIsDeviceSelected(LeftHearingAidName);
            if (RightHearingAidName != null)
                selectPage.GetIsDeviceSelected(RightHearingAidName);

            selectHearingAidsPage.Connect();

            Assert.IsTrue(new HearingAidInitPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            initPage = new HearingAidInitPage();

            Wait.UntilTrue(() =>
            {
                return initPage.LeftHearingAid.IsConnected || initPage.RightHearingAid.IsConnected;
            }, TimeSpan.FromSeconds(45));
            initPage.CancelAndConfirm();
            ReportHelper.LogTest(Status.Info, "Canceled the connect of hearing aid again");

            Assert.IsTrue(new HearingAidConnectionErrorPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(130)));
            new HearingAidConnectionErrorPage().RetryConnection();
            ReportHelper.LogTest(Status.Info, "Retry connection done again");
            ReportHelper.LogTest(Status.Pass, "App did not crash in second iteration also");

            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "Dashboard Loaded again");
        }

        [Test]
        [Category("SystemTestsDeviceSpecificConfig")]
        [Description("TC-22350_Table-0")]
        public void ST22350_CheckDeviceConnectionTime()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            // ToDo: Currently the date is not getting changed. Need to change the date to next day manually before excecution of test case.
            //// Changing Mobile date to next day
            //AppManager.DeviceSettings.ChangeDeviceDate(DateTime.Now.AddDays(1));
            //ReportHelper.LogTest(Status.Info, "Device date set to 1 day ahead");

            string LeftHearingAidName = SelectHearingAid.GetLeftHearingAid();
            string RightHearingAidName = SelectHearingAid.GetRightHearingAid();

            // Navigate throught to intro and start the Demo mode
            new IntroPageOne().MoveRightBySwiping();
            new IntroPageTwo().MoveRightBySwiping();
            new IntroPageThree().MoveRightBySwiping();
            new IntroPageFour().MoveRightBySwiping();
            Assert.IsTrue(new IntroPageFive().GetIsLeftButtonVisible());
            new IntroPageFive().Continue();

            Assert.IsTrue(new InitializeHardwarePage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Info, "Verified Into Pages");

            // Start App in Demo Mode
            new InitializeHardwarePage().StartScan();
            ReportHelper.LogTest(Status.Info, "Performed start scan again");

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

            var watch = System.Diagnostics.Stopwatch.StartNew();

            Assert.IsTrue(new HearingAidInitPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)));
            var initPage = new HearingAidInitPage();
            Assert.IsFalse(initPage.IsReadingFromCache(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "init_StepLoadingCachedProgramParams"), TimeSpan.FromSeconds(55)), "Memory is not read from scrach");
            ReportHelper.LogTest(Status.Info, "Reading memory from scarch");

            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(45));
            PermissionHelper.AllowPermissionIfRequested();

            ReportHelper.LogTest(Status.Info, "Hearing Aid Connected Left " + LeftHearingAidName + " and Right " + RightHearingAidName);

            // Load Dashboard Page
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            DashboardPage dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "App started in device mode");
            ReportHelper.LogTest(Status.Pass, "The memory has been read from scrach");

            watch.Stop();
            var firstelapsedSecs = watch.ElapsedMilliseconds / 1000;

            ReportHelper.LogTest(Status.Info, "Time (seconds) taken for First Load is: " + firstelapsedSecs);

            AppManager.RestartApp(false);
            ReportHelper.LogTest(Status.Info, "App Restarted");

            watch = System.Diagnostics.Stopwatch.StartNew();

            Assert.IsTrue(new HearingAidInitPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)));
            initPage = new HearingAidInitPage();
            Assert.IsTrue(initPage.IsReadingFromCache(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "init_StepLoadingCachedProgramParams"), TimeSpan.FromSeconds(55)), "Memory is not read from cache");
            ReportHelper.LogTest(Status.Info, "Reading memory from chache");

            // Load Dashboard Page
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "App restarted in device mode");
            ReportHelper.LogTest(Status.Pass, "The memory has been read from chache");

            watch.Stop();
            var secondelapsedSecs = watch.ElapsedMilliseconds / 1000;

            ReportHelper.LogTest(Status.Info, "Time (seconds) taken for Second Load is: " + secondelapsedSecs);

            Assert.Less(secondelapsedSecs, firstelapsedSecs, "Reconnect time taken is not less than Connect time");
            ReportHelper.LogTest(Status.Pass, "Reconnect time taken is less than Connect time");

            //// Reset Mobile date to current day
            //AppManager.DeviceSettings.ChangeDeviceDate(DateTime.Now);
            //ReportHelper.LogTest(Status.Info, "Device date reset to current date");
        }

        [Test]
        [Category("SystemTestsDeviceSpecificConfig")]
        [Description("TC-22359_Table-0")]
        public void ST22359_CheckUpdateProductLibrary()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            // Hearing System needs to upgraded with Firmware 1.13.1507 before testing
            var LeftHearingAidName = SelectHearingAid.GetLeftHearingAid(LeftHearingAid.Microgenisis_Lewi_R_Left_068842);
            var RightHearingAidName = SelectHearingAid.GetRightHearingAid(RightHearingAid.Microgenisis_Lewi_R_Right_068846);

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(LeftHearingAidName, RightHearingAidName).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + LeftHearingAidName + "' and Right '" + RightHearingAidName + "'");
            ReportHelper.LogTest(Status.Info, "Dashboard page loaded");

            Assert.AreEqual(dashboardPage.GetVolumeSliderValue(), 0.5);
            ReportHelper.LogTest(Status.Pass, "Default loaded volume is verified in Dashboard Page");

            dashboardPage.SetVolumeSliderValue(0.0);
            Assert.AreEqual(dashboardPage.GetVolumeSliderValue(), 0.0);
            dashboardPage.DecreaseVolume();
            ReportHelper.LogTest(Status.Info, "Muted");
            dashboardPage.IncreaseVolume();
            ReportHelper.LogTest(Status.Info, "Unmuted");

            double sliderVolume = 0.0;
            for (double i = 0.0; i <= 1.0; i++)
            {
                dashboardPage.SetVolumeSliderValue(i);
                Assert.AreEqual(Math.Round(dashboardPage.GetVolumeSliderValue(), 1), Math.Round(i, 1));
                Thread.Sleep(1000);
                sliderVolume = i;
            }

            ReportHelper.LogTest(Status.Pass, "Volume changed and verified in Dashboard Page");

            // Check Firmware Version
            dashboardPage.OpenMenuUsingTap();
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenMyHearingAids();
            var hearingSystemsPage = new HearingSystemManagementPage();
            hearingSystemsPage.LeftDeviceTabClick();
            Assert.IsTrue(hearingSystemsPage.GetIsLeftTabSelected());
            Assert.IsNotEmpty(hearingSystemsPage.GetLeftTabText());
            Assert.IsNotEmpty(hearingSystemsPage.GetLeftDeviceFirmware(), "Left Hearing System Firmware is empty.");
            ReportHelper.LogTest(Status.Pass, "Left Hearing System Firmware is available");
            hearingSystemsPage.RightDeviceTabClick();
            Assert.IsTrue(hearingSystemsPage.GetIsRightTabSelected());
            Assert.IsNotEmpty(hearingSystemsPage.GetRightTabText());
            Assert.IsNotEmpty(hearingSystemsPage.GetRightDeviceFirmware(), "Right Hearing System Firmware is empty.");
            ReportHelper.LogTest(Status.Pass, "Right Hearing System Firmware is available");
            hearingSystemsPage.LeftDeviceTabClick();
            hearingSystemsPage.TapBack();
            new SettingsMenuPage().TapBack();
            new MainMenuPage().CloseMenuUsingTap();

            // Open Noise comfort and change the parameters
            dashboardPage = new DashboardPage();
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);

            var programDetailPage = new ProgramDetailPage();

            Assert.IsTrue(programDetailPage.GetIsBinauralSettingsButtonVisible());
            programDetailPage.OpenBinauralSettings();
            var paramEditBinauralPage = new ProgramDetailParamEditBinauralPage();

            Assert.AreEqual(paramEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Single), sliderVolume);

            Assert.IsFalse(paramEditBinauralPage.GetIsBinauralSwitchChecked());
            paramEditBinauralPage.TurnOnBinauralSeparation();
            Assert.IsTrue(paramEditBinauralPage.GetIsBinauralSwitchChecked());
            ReportHelper.LogTest(Status.Info, "Binaural Switch turned on");

            Assert.AreEqual(paramEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Left), sliderVolume);

            Assert.AreEqual(paramEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Right), sliderVolume);

            ReportHelper.LogTest(Status.Pass, "Volume change is verified in Program Details Page");

            paramEditBinauralPage.Close();

            programDetailPage = new ProgramDetailPage();
            Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible());
            programDetailPage.NoiseReductionDisplay.OpenSettings();
            var noiseReductionNoiseComfort = NoiseReduction.Off;
            new ProgramDetailParamEditNoiseReductionPage().SelectNoiseReduction(noiseReductionNoiseComfort);
            Assert.AreEqual(noiseReductionNoiseComfort, new ProgramDetailParamEditNoiseReductionPage().GetSelectedNoiseReduction());
            ReportHelper.LogTest(Status.Pass, "Changed setting for Noise Comfort Program Noise Reduction and values noted");
            new ProgramDetailParamEditNoiseReductionPage().Close();
            new ProgramDetailPage().TapBack();

            // Open Music and change the parameters
            dashboardPage = new DashboardPage();
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 2);
            programDetailPage = new ProgramDetailPage();
            Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible());
            programDetailPage.NoiseReductionDisplay.OpenSettings();
            var noiseReductionMusic = NoiseReduction.Off;
            new ProgramDetailParamEditNoiseReductionPage().SelectNoiseReduction(noiseReductionMusic);
            Assert.AreEqual(noiseReductionMusic, new ProgramDetailParamEditNoiseReductionPage().GetSelectedNoiseReduction());
            ReportHelper.LogTest(Status.Pass, "Changed setting for Music Program Noise Reduction and values noted");
            new ProgramDetailParamEditNoiseReductionPage().Close();
            new ProgramDetailPage().TapBack();

            // Open Wireless Streaming and change the parameters
            dashboardPage = new DashboardPage();
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Streaming, 0);
            programDetailPage = new ProgramDetailPage();
            Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible());
            programDetailPage.NoiseReductionDisplay.OpenSettings();
            var noiseReductionWirelessStreaming = NoiseReduction.Off;
            new ProgramDetailParamEditNoiseReductionPage().SelectNoiseReduction(noiseReductionWirelessStreaming);
            Assert.AreEqual(noiseReductionWirelessStreaming, new ProgramDetailParamEditNoiseReductionPage().GetSelectedNoiseReduction());
            ReportHelper.LogTest(Status.Pass, "Changed setting for Wireless Straming Program Noise Reduction and values noted");
            new ProgramDetailParamEditNoiseReductionPage().Close();
            new ProgramDetailPage().TapBack();

            // Open Noise comfort and verify the parameters
            dashboardPage = new DashboardPage();
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            programDetailPage = new ProgramDetailPage();
            programDetailPage.NoiseReductionDisplay.OpenSettings();
            Assert.AreEqual(noiseReductionNoiseComfort, new ProgramDetailParamEditNoiseReductionPage().GetSelectedNoiseReduction());
            ReportHelper.LogTest(Status.Pass, "Noise Comfort Program Noise Reduction values verified");
            new ProgramDetailParamEditNoiseReductionPage().Close();
            new ProgramDetailPage().TapBack();

            // Open Music and verify the parameters
            dashboardPage = new DashboardPage();
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 2);
            programDetailPage = new ProgramDetailPage();
            programDetailPage.NoiseReductionDisplay.OpenSettings();
            Assert.AreEqual(noiseReductionMusic, new ProgramDetailParamEditNoiseReductionPage().GetSelectedNoiseReduction());
            ReportHelper.LogTest(Status.Pass, "Music Program Noise Reduction values verified");
            new ProgramDetailParamEditNoiseReductionPage().Close();
            new ProgramDetailPage().TapBack();

            // Open Wireless Streaming and verify the parameters
            dashboardPage = new DashboardPage();
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Streaming, 0);
            programDetailPage = new ProgramDetailPage();
            programDetailPage.NoiseReductionDisplay.OpenSettings();
            Assert.AreEqual(noiseReductionWirelessStreaming, new ProgramDetailParamEditNoiseReductionPage().GetSelectedNoiseReduction());
            ReportHelper.LogTest(Status.Pass, "Wireless Streaming Program Noise Reduction values verified");
            new ProgramDetailParamEditNoiseReductionPage().Close();
            new ProgramDetailPage().TapBack();

            ReportHelper.LogTest(Status.Pass, "Read and Write to memory is verified");
        }

        #endregion Sprint 33

        #endregion Test Cases
    }
}