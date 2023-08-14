using System;
using System.Collections.Generic;
using System.Threading;
using AventStack.ExtentReports;
using HorusUITest.Enums;
using HorusUITest.Helper;
using HorusUITest.PageObjects;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Controls.ProgramDetailParams;
using HorusUITest.PageObjects.Menu;
using HorusUITest.PageObjects.Menu.Help;
using HorusUITest.PageObjects.Menu.Info;
using HorusUITest.PageObjects.Menu.Programs;
using NUnit.Framework;

namespace HorusUITest.TestFixtures.TestCases
{
    /// <summary>
    /// This fixture tests the testing project itself. Each test navigates to a target view and verifies
    /// that every public functionality of the corresponding page objects work as intended.
    /// </summary>
    public class SystemTestsDemo1 : BaseResettingTestFixture
    {
        #region Constructor

        public SystemTestsDemo1(Platform platform)
            : base(platform)
        {

        }

        #endregion Constructor

        #region Test Cases

        #region Sprint 1

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-5866_Table-125")]
        public void ST5866_SelectProgram()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode after skipping intro pages and dashboard is loaded");

            dashboardPage.CheckStartView(dashboardPage);

            ReportHelper.LogTest(Status.Info, "Opening main menu using tap...");
            dashboardPage.OpenMenuUsingTap();
            ReportHelper.LogTest(Status.Info, "Opened main menu using tap");
            ReportHelper.LogTest(Status.Info, "Checking if 'Programs' text is empty...");
            Assert.IsNotEmpty(new MainMenuPage().GetProgramsText(), "'Programs' text is empty");
            ReportHelper.LogTest(Status.Pass, "'Programs' text is not empty");

            ReportHelper.LogTest(Status.Info, "Opening 'Programs' from main menu...");
            new MainMenuPage().OpenPrograms();
            ReportHelper.LogTest(Status.Info, "Opened 'Programs' from main menu");
            ReportHelper.LogTest(Status.Info, "Getting all programs names from the 'Programs' menu...");
            var programNames = GetMenuItemTexts(new ProgramsMenuPage());
            CollectionAssert.IsNotEmpty(programNames, "No programs available in 'Programs' menu");
            ReportHelper.LogTest(Status.Pass, "Program names (" + string.Join(",", programNames) + ") collected, Navigate back to dashboard page");
            ReportHelper.LogTest(Status.Info, "Navigating back to Main Menu...");
            new ProgramsMenuPage().NavigateBack();
            ReportHelper.LogTest(Status.Info, "Navigated back to Main Menu");
            ReportHelper.LogTest(Status.Info, "Closing Main Menu using tap...");
            new MainMenuPage().CloseMenuUsingTap();
            ReportHelper.LogTest(Status.Info, "Closed Main Menu using tap");
            ReportHelper.LogTest(Status.Info, "Waiting for dashboard page to be loaded...");
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Dashboard page not loaded");
            dashboardPage = new DashboardPage();
            ReportHelper.LogTest(Status.Info, "Dashboard page loaded");
            ReportHelper.LogTest(Status.Info, "Checking if programs from Programs menu and Dashbboard Page are equal...");
            Assert.AreEqual(programNames.Count, dashboardPage.GetNumberOfPrograms(), "Programs from Programs menu and Dashbboard Page are not equal");
            ReportHelper.LogTest(Status.Info, "Programs from Programs menu and Dashbboard Page are equal");

            ReportHelper.LogTest(Status.Info, "Checking navigation of programs on Dashbboard Page...");
            for (int i = 0; i < programNames.Count; i++)
            {
                dashboardPage.SelectProgram(i);
                ReportHelper.LogTest(Status.Info, "Checking program name '" + dashboardPage.GetCurrentProgramName() + "'...");
                CollectionAssert.Contains(programNames, dashboardPage.GetCurrentProgramName(), "Program name '" + dashboardPage.GetCurrentProgramName() + "' is not available");
                ReportHelper.LogTest(Status.Info, "Program name '" + dashboardPage.GetCurrentProgramName() + "' is available");
            }
            ReportHelper.LogTest(Status.Pass, "Programs can be successfully changed on Dashboard page by program icons");

            ReportHelper.LogTest(Status.Info, "Check detail view of programs and make verify changes in parameters.");

            //Open Music Program
            ReportHelper.LogTest(Status.Info, "Navigating to Music program from Main Menu using tap...");
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            ReportHelper.LogTest(Status.Info, "Navigated to Music program from Main Menu using tap");
            var programDetailPage = new ProgramDetailPage();
            programDetailPage.ProgramDetailPageUiCheck();
            // Tile names and its expected visibility is passed as parameter along with prgram name
            var programTiles = new Dictionary<string, bool>
            {
                { "SpeechFocus", true },
                { "NoiseReduction", true },
                { "Tinnitus", true },
                { "Equalizer", true },
                // Streaming tile should not be visible for Music program in demo mode
                { "Streaming", false }
            };
            programDetailPage.ProgramDetailsTileCheck(programDetailPage.GetCurrentProgramName(), programTiles);

            ReportHelper.LogTest(Status.Info, "Opening Noise Reduction settings...");
            programDetailPage.NoiseReductionDisplay.OpenSettings();
            ReportHelper.LogTest(Status.Info, "Opened Noise Reduction settings");
            var noiseComfortNoiseReduction = NoiseReduction.Off;
            ReportHelper.LogTest(Status.Info, "Changing Noise Reduction in Music program to " + noiseComfortNoiseReduction.ToString() + "...");
            new ProgramDetailParamEditNoiseReductionPage().SelectNoiseReduction(noiseComfortNoiseReduction);
            ReportHelper.LogTest(Status.Info, "Changed Noise Reduction in Music program to " + noiseComfortNoiseReduction.ToString());
            ReportHelper.LogTest(Status.Info, "Closing Noise Reduction settings...");
            new ProgramDetailParamEditNoiseReductionPage().Close();
            ReportHelper.LogTest(Status.Info, "Closed Noise Reduction settings");
            ReportHelper.LogTest(Status.Info, "Checking if Noise Reduction is " + new ProgramDetailPage().NoiseReductionDisplay.GetValue() + " ...");
            Assert.AreEqual(noiseComfortNoiseReduction.ToString(), new ProgramDetailPage().NoiseReductionDisplay.GetValue());
            ReportHelper.LogTest(Status.Pass, "Noise Reduction is " + new ProgramDetailPage().NoiseReductionDisplay.GetValue() + " and it is verified");

            //Open Tinnitus Program
            ReportHelper.LogTest(Status.Info, "Navigating to Tinnitus program...");
            programDetailPage.SelectProgram(2);
            ReportHelper.LogTest(Status.Info, "Navigated to Tinnitus program");

            programDetailPage = new ProgramDetailPage();
            ReportHelper.LogTest(Status.Info, "Waiting until Tinnitus Only Display is Visible...");
            Wait.UntilTrue(() => programDetailPage.GetIsTinnitusOnlyDisplayVisible(), TimeSpan.FromSeconds(3));
            ReportHelper.LogTest(Status.Info, "Tinnitus Only Display is Visible");

            ReportHelper.LogTest(Status.Info, "Opening Tinnitus Only Display settings...");
            programDetailPage.TinnitusOnlyDisplay.OpenSettings();
            ReportHelper.LogTest(Status.Info, "Opened Tinnitus Only Display settings");

            var programDetailParamEditTinnitusPage = new ProgramDetailParamEditTinnitusPage();
            ReportHelper.LogTest(Status.Info, "Checking Equalizer Title is not empty...");
            Assert.IsNotEmpty(programDetailParamEditTinnitusPage.GetEqualizerTitle(), "Equalizer Title is empty");
            ReportHelper.LogTest(Status.Info, "Equalizer Title is not empty");
            ReportHelper.LogTest(Status.Info, "Checking Equalizer Slider Title for Low is not empty...");
            Assert.IsNotEmpty(programDetailParamEditTinnitusPage.GetEqualizerSliderTitle(EqBand.Low), "Equalizer Slider Title for Low is empty");
            ReportHelper.LogTest(Status.Info, "Equalizer Slider Title for Low is not empty");
            ReportHelper.LogTest(Status.Info, "Checking Equalizer Slider Title for Mid is not empty...");
            Assert.IsNotEmpty(programDetailParamEditTinnitusPage.GetEqualizerSliderTitle(EqBand.Mid), "Equalizer Slider Title for Mid is empty");
            ReportHelper.LogTest(Status.Info, "Equalizer Slider Title for Mid is not empty");
            ReportHelper.LogTest(Status.Info, "Checking Equalizer Slider Title for High is not empty...");
            Assert.IsNotEmpty(programDetailParamEditTinnitusPage.GetEqualizerSliderTitle(EqBand.High), "Equalizer Slider Title for High is empty");
            ReportHelper.LogTest(Status.Info, "Equalizer Slider Title for High is not empty");
            ReportHelper.LogTest(Status.Info, "Checking Close button text is not empty...");
            Assert.IsNotEmpty(programDetailParamEditTinnitusPage.GetCloseButtonText(), "Close button text is empty");
            ReportHelper.LogTest(Status.Info, "Close button text is not empty");

            double volumeSliderValue = 0;
            ReportHelper.LogTest(Status.Info, "Changing Volume in Tinnitus program to " + volumeSliderValue + "...");
            programDetailParamEditTinnitusPage.SetVolumeSliderValue(volumeSliderValue);
            ReportHelper.LogTest(Status.Info, "Changed Volume in Tinnitus program to " + volumeSliderValue);
            ReportHelper.LogTest(Status.Info, "Closing settings...");
            programDetailParamEditTinnitusPage.Close();
            ReportHelper.LogTest(Status.Info, "Settings closed");
            ReportHelper.LogTest(Status.Info, "Waiting till Program Details page is loaded...");
            Assert.IsTrue(new ProgramDetailPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Program Details page is not loaded");
            ReportHelper.LogTest(Status.Info, "Program Details page is loaded");
            ReportHelper.LogTest(Status.Info, "Checking if Volume changed to " + new ProgramDetailPage().TinnitusOnlyDisplay.GetVolumeSliderValue() + "...");
            Assert.AreEqual(volumeSliderValue, new ProgramDetailPage().TinnitusOnlyDisplay.GetVolumeSliderValue(), "Volume changed not to " + new ProgramDetailPage().TinnitusOnlyDisplay.GetVolumeSliderValue());
            ReportHelper.LogTest(Status.Pass, "Volume changed to " + new ProgramDetailPage().TinnitusOnlyDisplay.GetVolumeSliderValue() + " and is verified");

            //Open Streaming program
            ReportHelper.LogTest(Status.Info, "Navigating to Streaming program...");
            new ProgramDetailPage().SelectProgram(3);
            ReportHelper.LogTest(Status.Info, "Navigated to Streaming program");

            programDetailPage = new ProgramDetailPage();
            // Tile names and its expected visibility is passed as parameter along with prgram name
            programTiles = new Dictionary<string, bool>
            {
                { "SpeechFocus", true },
                { "NoiseReduction", true },
                // Tinnitus should not be visible in Audio Straming program
                { "Tinnitus", false },
                { "Equalizer", true },
                { "Streaming", true }
            };
            programDetailPage.ProgramDetailsTileCheck(programDetailPage.GetCurrentProgramName(), programTiles);

            ReportHelper.LogTest(Status.Info, "Opening Streaming Display settings...");
            programDetailPage.StreamingDisplay.OpenSettings();
            ReportHelper.LogTest(Status.Info, "Opened Streaming Display settings");
            var streamingValue = 1;
            ReportHelper.LogTest(Status.Info, "Changing Streaming slder value to " + streamingValue + "...");
            new ProgramDetailParamEditStreamingPage().SetStreamingSliderValue(streamingValue);
            ReportHelper.LogTest(Status.Info, "Changed Streaming slder value to " + streamingValue);
            ReportHelper.LogTest(Status.Info, "Closing settings...");
            new ProgramDetailParamEditStreamingPage().Close();
            ReportHelper.LogTest(Status.Info, "Settings closed");
            ReportHelper.LogTest(Status.Info, "Checking if Streaming slder value changed to " + new ProgramDetailPage().StreamingDisplay.GetSliderValue() + "...");
            Assert.AreEqual(streamingValue, new ProgramDetailPage().StreamingDisplay.GetSliderValue(), "Streaming slder value not changed to " + new ProgramDetailPage().StreamingDisplay.GetSliderValue());
            ReportHelper.LogTest(Status.Pass, "Streaming slder value changed to " + new ProgramDetailPage().StreamingDisplay.GetSliderValue() + " and is verified");

            ReportHelper.LogTest(Status.Info, "Navigate back to programs and checking the changes made...");
            //Music
            ReportHelper.LogTest(Status.Info, "Opening Music program...");
            programDetailPage.SelectProgram(1);
            ReportHelper.LogTest(Status.Info, "Opened Music program");
            ReportHelper.LogTest(Status.Info, "Checking if Comfort Noise Reduction value changed to " + programDetailPage.NoiseReductionDisplay.GetValue() + "...");
            Assert.AreEqual(noiseComfortNoiseReduction.ToString(), programDetailPage.NoiseReductionDisplay.GetValue(), "Comfort Noise Reduction value not changed to " + programDetailPage.NoiseReductionDisplay.GetValue());
            ReportHelper.LogTest(Status.Info, "Comfort Noise Reduction value changed to " + programDetailPage.NoiseReductionDisplay.GetValue());
            //Tinnitus
            ReportHelper.LogTest(Status.Info, "Opening Tinnitus program...");
            programDetailPage.SelectProgram(2);
            ReportHelper.LogTest(Status.Info, "Opened Tinnitus program");
            ReportHelper.LogTest(Status.Info, "Checking if Tinnitus Only Display value changed to " + new ProgramDetailPage().TinnitusOnlyDisplay.GetVolumeSliderValue() + "...");
            Assert.AreEqual(volumeSliderValue, new ProgramDetailPage().TinnitusOnlyDisplay.GetVolumeSliderValue(), "Tinnitus Only Display value not changed to " + new ProgramDetailPage().TinnitusOnlyDisplay.GetVolumeSliderValue());
            ReportHelper.LogTest(Status.Info, "Tinnitus Only Display value changed to " + new ProgramDetailPage().TinnitusOnlyDisplay.GetVolumeSliderValue());
            //Streaming
            ReportHelper.LogTest(Status.Info, "Opening Streaming program...");
            new ProgramDetailPage().SelectProgram(3);
            ReportHelper.LogTest(Status.Info, "Opened Streaming program");
            ReportHelper.LogTest(Status.Info, "Checking if Streaming Display value changed to " + new ProgramDetailPage().StreamingDisplay.GetSliderValue() + "...");
            Assert.AreEqual(streamingValue, new ProgramDetailPage().StreamingDisplay.GetSliderValue(), "Streaming Display value not changed to " + new ProgramDetailPage().StreamingDisplay.GetSliderValue());
            ReportHelper.LogTest(Status.Info, "Streaming Display value changed to " + new ProgramDetailPage().StreamingDisplay.GetSliderValue());
            ReportHelper.LogTest(Status.Pass, "Check successful changes made in program parameters is saved");

            ReportHelper.LogTest(Status.Info, "Navigating back and check the Start View in dashbard page...");
            new ProgramDetailPage().NavigateBack();
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Dashboard page not loaded");
            ReportHelper.LogTest(Status.Info, "Navigated to dashbard page...");

            new DashboardPage().CheckStartView(new DashboardPage());

            List<string> GetMenuItemTexts(BaseSubMenuPage menuPage)
            {
                var menuItemsList = menuPage.MenuItems.GetAllVisible();
                return menuItemsList;
            }
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-5903_Table-123")]
        public void ST5903_ShowMenuInfo()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode after skipping intro pages and dashboard is loaded");

            dashboardPage.CheckStartView(dashboardPage);

            ReportHelper.LogTest(Status.Info, "Opening the Main Menu from Dashboard page using tap...");
            dashboardPage.OpenMenuUsingTap();
            ReportHelper.LogTest(Status.Info, "Opened the Main Menu from Dashboard page using tap");
            ReportHelper.LogTest(Status.Info, "Checking the Help menu text from Main Menu page...");
            Assert.IsNotEmpty(new MainMenuPage().GetHelpText(), "Help menu text from Main Menu page is empty");
            ReportHelper.LogTest(Status.Pass, "Help menu text from Main Menu page is not empty");

            ReportHelper.LogTest(Status.Info, "Opening the Help menu from Main Menu...");
            new MainMenuPage().OpenHelp();
            ReportHelper.LogTest(Status.Info, "Opened the Help menu from Main Menu");
            ReportHelper.LogTest(Status.Info, "Checking the Imprint text from Help Menu page...");
            Assert.IsNotEmpty(new HelpMenuPage().GetImprintText(), "Imprint text from Help Menu page is empty");
            ReportHelper.LogTest(Status.Pass, "Imprint text from Help Menu page is not empty");

            ReportHelper.LogTest(Status.Info, "Opening the Imprint page from Help Menu...");
            new HelpMenuPage().OpenImprint();
            Assert.IsTrue(new ImprintPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Imprint page not loaded");
            ReportHelper.LogTest(Status.Info, "Opened the Imprint page from Help Menu");

            var imprintPage = new ImprintPage();
            ReportHelper.LogTest(Status.Info, "Current App Version: " + imprintPage.GetVersion() + " " + imprintPage.GetBuildNumber());
            ReportHelper.LogTest(Status.Info, "Checking all the UI elements of Imprint page...");
            ReportHelper.LogTest(Status.Info, "Checking navigation bar title...");
            Assert.IsNotEmpty(imprintPage.GetNavigationBarTitle());
            ReportHelper.LogTest(Status.Info, "Checking app version...");
            Assert.IsNotEmpty(imprintPage.GetVersion(), "App version text is empty");
            ReportHelper.LogTest(Status.Info, "App version text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking address header...");
            Assert.IsNotEmpty(imprintPage.GetAddressHeader(), "Address header text is empty");
            ReportHelper.LogTest(Status.Info, "Address header text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking company name...");
            Assert.IsNotEmpty(imprintPage.GetAppCompanyName(), "Company name text is empty");
            ReportHelper.LogTest(Status.Info, "Company name text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking company street...");
            Assert.IsNotEmpty(imprintPage.GetAppCompanyStreet(), "Company street text is empty");
            ReportHelper.LogTest(Status.Info, "Company street text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking postel code...");
            Assert.IsNotEmpty(imprintPage.GetAppCompanyPostalCodeCity(), "Postel code text is empty");
            ReportHelper.LogTest(Status.Info, "Postel code text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking company state...");
            Assert.IsNotEmpty(imprintPage.GetAppCommpanyState(), "Company state text is empty");
            ReportHelper.LogTest(Status.Info, "Company state text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking support website...");
            Assert.IsNotEmpty(imprintPage.GetSupportWebsite(), "Support website text is empty");
            ReportHelper.LogTest(Status.Info, "Support website text is not empty");
            // For RxEarsPro, their website has restricted the access. Hence we get 403 Forbidden error and not able to check it using automation. Manually the wesite gets loaded
            if (AppManager.Brand != Brand.RxEarsPro)
            {
                ReportHelper.LogTest(Status.Info, "Checking " + AppManager.Brand + " support website 'https://" + imprintPage.GetSupportWebsite() + "' is valid...");
                Assert.IsTrue(imprintPage.GetIsSupportWebsiteValid());
                ReportHelper.LogTest(Status.Info, AppManager.Brand + " support website is valid and can be opened successfuly.");
            }
            // Manufacturer details not available for rxearspro and personna medical
            if (AppManager.Brand != Brand.RxEarsPro && AppManager.Brand != Brand.PersonaMedical)
            {
                ReportHelper.LogTest(Status.Info, "Checking manufacturer header...");
                Assert.IsNotEmpty(imprintPage.GetManufacturerStaticLabel(), "Manufacturer header is empty");
                ReportHelper.LogTest(Status.Info, "Manufacturer header is not empty");
                ReportHelper.LogTest(Status.Info, "Checking manufacturer company name...");
                Assert.IsNotEmpty(imprintPage.GetManufacturerCompanyName(), "Manufacturer company name is empty");
                ReportHelper.LogTest(Status.Info, "Manufacturer company name is not empty");
                ReportHelper.LogTest(Status.Info, "Checking manufacturer postal code...");
                Assert.IsNotEmpty(imprintPage.GetManufacturerPostalCodeCity(), "Manufacturer postal code is empty");
                ReportHelper.LogTest(Status.Info, "Manufacturer postal code is not empty");
                ReportHelper.LogTest(Status.Info, "Checking manufacturer company city...");
                Assert.IsNotEmpty(imprintPage.GetManufacturerCompanyStreet(), "Manufacturer company city is empty");
                ReportHelper.LogTest(Status.Info, "Manufacturer company city is not empty");
            }
            ReportHelper.LogTest(Status.Pass, "All the UI elements of Imprint page is verified");

            ReportHelper.LogTest(Status.Info, "Checking the back navigation from Imprint page...");
            imprintPage.NavigateBack();
            Assert.IsTrue(new HelpMenuPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Help menu page not loaded");
            ReportHelper.LogTest(Status.Pass, "Back navigation to Help menu is successful");

            ReportHelper.LogTest(Status.Info, "Checking the back navigation from Help Menu page...");
            new HelpMenuPage().NavigateBack();
            Assert.IsTrue(new MainMenuPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Main menu page not loaded");
            ReportHelper.LogTest(Status.Pass, "Back navigation to Main menu is successful");

            ReportHelper.LogTest(Status.Info, "Closing main menu using tap...");
            new MainMenuPage().CloseMenuUsingTap();
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Dashboard page not loaded");
            ReportHelper.LogTest(Status.Pass, "Closed main menu using tap and Dashboard page start view is displayed");

            AppManager.DeviceSettings.DisableWifi();
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-5958_Table-122")]
        public void ST5958_DisplayHearingAidInformation()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode after skipping intro pages and dashboard is loaded");

            dashboardPage.CheckStartView(dashboardPage);

            ReportHelper.LogTest(Status.Info, "Opening the Main Menu from Dashboard page using tap...");
            dashboardPage.OpenMenuUsingTap();
            ReportHelper.LogTest(Status.Info, "Opened the Main Menu from Dashboard page using tap");

            // Open Settings Menu
            ReportHelper.LogTest(Status.Info, "Opening the Settings Menu from Main Menu page...");
            new MainMenuPage().OpenSettings();
            Assert.IsTrue(new SettingsMenuPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), "Settings menu page not loaded");
            ReportHelper.LogTest(Status.Info, "Opened the Settings Menu from Main Menu page");

            ReportHelper.LogTest(Status.Info, "Getting all the menu text from Settings Menu page...");
            var settingsItems = GetMenuItemTexts(new SettingsMenuPage());
            Assert.IsNotEmpty(settingsItems, "No text available in Settings Menu page");
            foreach (var settingsItem in settingsItems)
            {
                Assert.IsNotEmpty(settingsItem, "Menu Item '" + settingsItem + "' is empty");
                ReportHelper.LogTest(Status.Info, "Menu Item '" + settingsItem + "' is not empty");
            }
            ReportHelper.LogTest(Status.Info, "All the menu text from Settings Menu page obtained and verified");

            // Verify Settings Index and Names
            ReportHelper.LogTest(Status.Info, "Checking if the names obtained form menu matches with its index...");
            var settingsMenuPage = new SettingsMenuPage();
            string name0 = settingsMenuPage.MenuItems.Get(0, IndexType.Relative);
            string name1 = settingsMenuPage.MenuItems.Get(1, IndexType.Relative);
            string name2 = settingsMenuPage.MenuItems.Get(2, IndexType.Relative);
            string name3 = settingsMenuPage.MenuItems.Get(3, IndexType.Relative);
            Assert.AreEqual(name0, settingsItems[0]);
            Assert.AreEqual(name1, settingsItems[1]);
            Assert.AreEqual(name2, settingsItems[2]);
            Assert.AreEqual(name3, settingsItems[3]);
            Assert.AreEqual(name0, settingsMenuPage.MenuItems.Get(0, IndexType.Relative));
            Assert.AreEqual(name1, settingsMenuPage.MenuItems.Get(1, IndexType.Relative));
            Assert.AreEqual(name2, settingsMenuPage.MenuItems.Get(2, IndexType.Relative));
            Assert.AreEqual(name3, settingsMenuPage.MenuItems.Get(3, IndexType.Relative));
            ReportHelper.LogTest(Status.Pass, "The names obtained form menu matches with its index and is verified");

            ReportHelper.LogTest(Status.Info, "Checking Menu Item names...");
            ReportHelper.LogTest(Status.Info, "Checking demo mode text...");
            Assert.IsNotEmpty(settingsMenuPage.GetDemoModeText(), "Demo mode text is empty");
            ReportHelper.LogTest(Status.Info, "Demo mode text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking language text...");
            Assert.IsNotEmpty(settingsMenuPage.GetLanguageText(), "Language text is empty");
            ReportHelper.LogTest(Status.Info, "Language text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking hearing aids text...");
            Assert.IsNotEmpty(settingsMenuPage.GetMyHearingAidsText(), "Hearing aids text is empty");
            ReportHelper.LogTest(Status.Info, "Hearing aids text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking permissions text...");
            Assert.IsNotEmpty(settingsMenuPage.GetPermissionsText(), "Permissions text is empty");
            ReportHelper.LogTest(Status.Info, "Permissions text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking app reset text...");
            Assert.IsNotEmpty(settingsMenuPage.GetAppResetText(), "App reset text is empty");
            ReportHelper.LogTest(Status.Info, "App reset text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking logs text...");
            Assert.IsNotEmpty(settingsMenuPage.GetLogsText(), "Logs text is empty");
            ReportHelper.LogTest(Status.Info, "Logs text is not empty");
            ReportHelper.LogTest(Status.Pass, "Menu Item Names Verified");

            // Open My Hearing Aids Page
            ReportHelper.LogTest(Status.Info, "Opening My Hearing Aid page...");
            settingsMenuPage.OpenMyHearingAids();
            ReportHelper.LogTest(Status.Info, "Opened My Hearing Aid page");

            HearingSystemManagementPage hearingSystemManagementPage = new HearingSystemManagementPage();

            hearingSystemManagementPage.CheckHAInformationFromSettings(AppMode.Demo, Side.Left);
            hearingSystemManagementPage.CheckHAInformationFromSettings(AppMode.Demo, Side.Right);

            hearingSystemManagementPage.LeftDeviceTabClick();
            ReportHelper.LogTest(Status.Info, "Left tab selected again");
            ReportHelper.LogTest(Status.Info, "Tapping back to settings menu...");
            hearingSystemManagementPage.TapBack();
            ReportHelper.LogTest(Status.Info, "Settings menu loaded");
            ReportHelper.LogTest(Status.Info, "Tapping back to main menu...");
            new SettingsMenuPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Main menu loaded");
            ReportHelper.LogTest(Status.Info, "Closing main menu using tap...");
            new MainMenuPage().CloseMenuUsingTap();
            ReportHelper.LogTest(Status.Info, "Closed main menu using tap");

            ReportHelper.LogTest(Status.Info, "Opening Left device info page from Dashboard...");
            new DashboardPage().OpenLeftHearingDevice();
            ReportHelper.LogTest(Status.Info, "Opened Left device info page from Dashboard");
            new HearingInstrumentInfoControlPage().CheckHAInformationFromDashboard(AppMode.Demo).Close();
            ReportHelper.LogTest(Status.Pass, "All details are visible on Left Hearing Instrument Info page.");

            ReportHelper.LogTest(Status.Info, "Opening Right device info page from Dashboard...");
            new DashboardPage().OpenRightHearingDevice();
            ReportHelper.LogTest(Status.Info, "Opened Right device info page from Dashboard");
            new HearingInstrumentInfoControlPage().CheckHAInformationFromDashboard(AppMode.Demo).Close();
            ReportHelper.LogTest(Status.Pass, "All details are visible on Right Hearing Instrument Info page.");

            List<string> GetMenuItemTexts(BaseSubMenuPage menuPage)
            {
                var menuItemsList = menuPage.MenuItems.GetAllVisible();
                return menuItemsList;
            }
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-5969_Table-121")]
        public void ST5969_FindHearingAids()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode after skipping intro pages and dashboard is loaded");

            ReportHelper.LogTest(Status.Info, "Opening left hearing device information...");
            dashboardPage.OpenLeftHearingDevice();
            var hearingInstrumentInfoPage = new HearingInstrumentInfoControlPage();
            ReportHelper.LogTest(Status.Info, "Opened left hearing device information");
            var leftDeviceName = hearingInstrumentInfoPage.GetDeviceName();
            Assert.IsNotEmpty(leftDeviceName);
            ReportHelper.LogTest(Status.Info, "Closing left hearing device information...");
            hearingInstrumentInfoPage.Close();
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Dashboard page not loaded");
            ReportHelper.LogTest(Status.Info, "Closed left hearing device information and dashboard page is loaded");
            ReportHelper.LogTest(Status.Info, "Opening right hearing device information...");
            new DashboardPage().OpenRightHearingDevice();
            hearingInstrumentInfoPage = new HearingInstrumentInfoControlPage();
            ReportHelper.LogTest(Status.Info, "Opened right hearing device information");
            var rightDeviceName = hearingInstrumentInfoPage.GetDeviceName();
            Assert.IsNotEmpty(rightDeviceName);
            ReportHelper.LogTest(Status.Info, "Closing right hearing device information...");
            hearingInstrumentInfoPage.Close();
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Dashboard page not loaded");
            ReportHelper.LogTest(Status.Info, "Closed right hearing device information and dashboard page is loaded");

            //Open Menu 
            ReportHelper.LogTest(Status.Info, "Opening main menu using tap...");
            new DashboardPage().OpenMenuUsingTap();
            var mainMenuPage = new MainMenuPage();
            ReportHelper.LogTest(Status.Info, "Opened main menu using tap");
            ReportHelper.LogTest(Status.Info, "Checking Programs text...");
            Assert.IsNotEmpty(mainMenuPage.GetProgramsText(), "Programs text is empty");
            ReportHelper.LogTest(Status.Info, "Programs text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking Settings text...");
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText(), "Settings text is empty");
            ReportHelper.LogTest(Status.Info, "Settings text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking Help text...");
            Assert.IsNotEmpty(mainMenuPage.GetHelpText(), "Help text is empty");
            ReportHelper.LogTest(Status.Info, "Help text is not empty");

            //Check help submenu
            ReportHelper.LogTest(Status.Info, "Opening help menu...");
            mainMenuPage.OpenHelp();
            var helpMenuPage = new HelpMenuPage();
            ReportHelper.LogTest(Status.Info, "Opened help menu");
            ReportHelper.LogTest(Status.Info, "Checking Find Hearing Devices text...");
            Assert.IsNotEmpty(helpMenuPage.GetFindHearingDevicesText(), "Find Hearing Devices text is empty");
            ReportHelper.LogTest(Status.Info, "Find Hearing Devices text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking Help Topics text...");
            Assert.IsNotEmpty(helpMenuPage.GetHelpTopicsText(), "Help Topics text is empty");
            ReportHelper.LogTest(Status.Info, "Help Topics text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking Instructions For Use text...");
            Assert.IsNotEmpty(helpMenuPage.GetInstructionsForUseText(), "Instructions For Use text is empty");
            ReportHelper.LogTest(Status.Info, "Instructions For Use text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking Information Menu text...");
            Assert.IsNotEmpty(helpMenuPage.GetInformationMenuText(), "Information Menu text is empty");
            ReportHelper.LogTest(Status.Info, "Information Menu text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking Imprint text...");
            Assert.IsNotEmpty(helpMenuPage.GetImprintText(), "Imprint text is empty");
            ReportHelper.LogTest(Status.Info, "Imprint text is not empty");
            ReportHelper.LogTest(Status.Pass, "Main menu and Help menu texts are verified");

            //Check find hearing devices page map view
            ReportHelper.LogTest(Status.Info, "Opening find hearing device page...");
            helpMenuPage.OpenFindHearingDevices();
            Thread.Sleep(2000);
            var findDevicesPage = new FindDevicesPage();
            ReportHelper.LogTest(Status.Info, "Opened find hearing device page");
            ReportHelper.LogTest(Status.Info, "Checking if left device is selected...");
            Assert.IsTrue(findDevicesPage.GetIsLeftDeviceSelected());
            ReportHelper.LogTest(Status.Info, "Left device is selected");
            ReportHelper.LogTest(Status.Info, "Checking if map view is selected...");
            Assert.IsTrue(findDevicesPage.GetIsMapViewSelected());
            ReportHelper.LogTest(Status.Info, "Map view is selected");
            ReportHelper.LogTest(Status.Info, "Checking if left hearing aid is visible on map...");
            Assert.IsTrue(findDevicesPage.GetIsHearingAidVisibleOnMap(leftDeviceName, Side.Left.ToString()));
            ReportHelper.LogTest(Status.Info, "Left hearing aid is visible on map");
            ReportHelper.LogTest(Status.Info, "Checking toggle view button text...");
            Assert.IsNotEmpty(findDevicesPage.GetToggleViewButtonText(), "Toggle view button text is empty");
            ReportHelper.LogTest(Status.Info, "Toggle view button text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right device text...");
            Assert.IsNotEmpty(findDevicesPage.GetRightDeviceText(), "Right device text is empty");
            ReportHelper.LogTest(Status.Info, "Right device text is not empty");
            ReportHelper.LogTest(Status.Info, "Selecting right device...");
            findDevicesPage.SelectRightDevice();
            Assert.IsTrue(findDevicesPage.GetIsRightDeviceSelected(), "Right device is not selected");
            ReportHelper.LogTest(Status.Info, "Right device is selected");
            ReportHelper.LogTest(Status.Info, "Checking if right hearing aid is visible on map...");
            Assert.IsTrue(findDevicesPage.GetIsHearingAidVisibleOnMap(rightDeviceName, Side.Right.ToString()));
            ReportHelper.LogTest(Status.Info, "Right hearing aid is visible on map");
            ReportHelper.LogTest(Status.Pass, "Left and Right device are visible correctly on map view");

            AppManager.DeviceSettings.DisableWifi();
        }

        #endregion Sprint 1

        #region Sprint 2

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-5902_Table-124")]
        public void ST5902_SelectProgramDetailView()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            // Loading Dashboard Page
            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode after skipping intro pages and dashboard is loaded");

            //Changing the hearing program
            string lastName = null;
            for (int i = 0; i < dashboardPage.GetNumberOfPrograms(); i++)
            {
                dashboardPage.SelectProgram(i);
                Assert.IsTrue(dashboardPage.GetIsProgramSelected(i), $"Failed to select a hearing program {i}");
                string newName = dashboardPage.GetCurrentProgramName();
                Assert.AreNotEqual(lastName, newName, "Duplicate hearing program names found");
                lastName = newName;
            }
            ReportHelper.LogTest(Status.Info, "Changing the hearing program and verifying that each program has a unique name");

            // Open current program
            ReportHelper.LogTest(Status.Info, "Opening Music program details from menu using tap...");
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            ReportHelper.LogTest(Status.Info, "Opened Music program details from menu using tap");
            var programDetailPage = new ProgramDetailPage();
            programDetailPage.ProgramDetailPageUiCheck();
            // Tile names and its expected visibility is passed as parameter along with prgram name
            var programTiles = new Dictionary<string, bool>
            {
                { "SpeechFocus", true },
                { "NoiseReduction", true },
                { "Tinnitus", true },
                { "Equalizer", true },
                // Streaming tile should not be visible for Music program in demo mode
                { "Streaming", false }
            };
            programDetailPage.ProgramDetailsTileCheck(programDetailPage.GetCurrentProgramName(), programTiles);

            for (int j = 0; j < programDetailPage.GetNumberOfVisibiblePrograms(); j++)
            {
                programDetailPage.SelectProgram(j);
            }
            ReportHelper.LogTest(Status.Info, "Program switched via program bar on top of program detail page successfully");

            ReportHelper.LogTest(Status.Info, "Locking device for 10 seconds...");
            AppManager.DeviceSettings.LockDevice(10);
            ReportHelper.LogTest(Status.Info, "Device unlocked");
            ReportHelper.LogTest(Status.Info, "Put app to background for 10 seconds");
            AppManager.DeviceSettings.PutAppToBackground(10);
            programDetailPage = new ProgramDetailPage();
            programDetailPage.WaitForPage(TimeSpan.FromSeconds(5));
            ReportHelper.LogTest(Status.Info, "App in foreground and program details page still visible");

            ReportHelper.LogTest(Status.Info, "Selecting 1st program...");
            programDetailPage.SelectProgram(1);
            ReportHelper.LogTest(Status.Info, "Program '" + programDetailPage.GetCurrentProgramName() + "' selected");
            ReportHelper.LogTest(Status.Info, "Checking program name in program details page...");
            Assert.IsNotEmpty(programDetailPage.GetCurrentProgramName(), "Program name text in program details page is empty");
            ReportHelper.LogTest(Status.Info, "Program name text in program details page is not empty");
            ReportHelper.LogTest(Status.Info, "Checking if binaural settings button is visible in program details page...");
            Assert.IsTrue(programDetailPage.GetIsBinauralSettingsButtonVisible(), "Binaural settings button is not visible in program details page");
            ReportHelper.LogTest(Status.Info, "Binaural settings button is visible in program details page");
            ReportHelper.LogTest(Status.Info, "Checking if settings button is visible in program details page...");
            Assert.IsTrue(programDetailPage.GetIsSettingsButtonDisplayed(), "Settings button is not visible in program details page");
            ReportHelper.LogTest(Status.Info, "Settings button is visible in program details page");
            ReportHelper.LogTest(Status.Info, "Checking if speech focus display is visible in program details page...");
            Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible(), "Speech focus display is not visible in program details page");
            ReportHelper.LogTest(Status.Info, "Speech focus display is visible in program details page");
            ReportHelper.LogTest(Status.Info, "Checking if noise reduction display is visible in program details page...");
            Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible(), "Noise reduction display is not visible in program details page");
            ReportHelper.LogTest(Status.Info, "Noise reduction display is visible in program details page");
            ReportHelper.LogTest(Status.Info, "Checking if tinnitus display is visible in program details page...");
            Assert.IsTrue(programDetailPage.GetIsTinnitusDisplayVisible(), "Tinnitus display is not visible in program details page");
            ReportHelper.LogTest(Status.Info, "Tinnitus display is visible in program details page");
            ReportHelper.LogTest(Status.Pass, "Music program view is restored and all elements are visible");

            // Navigate to Main Menu
            ReportHelper.LogTest(Status.Info, "Tapping back to dashboard page...");
            new ProgramDetailPage().TapBack();
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Dashboard page not loaded");
            ReportHelper.LogTest(Status.Info, "Dashboard page loaded");

            //Check the switching of programs again on dashboard page
            lastName = null;
            for (int i = 0; i < dashboardPage.GetNumberOfPrograms(); i++)
            {
                dashboardPage.SelectProgram(i);
                Assert.IsTrue(dashboardPage.GetIsProgramSelected(i), $"Failed to select a hearing program {i}.");
                string newName = dashboardPage.GetCurrentProgramName();
                Assert.AreNotEqual(lastName, newName, "Duplicate hearing program names found.");
                lastName = newName;
            }
            ReportHelper.LogTest(Status.Info, "Switching of programs on Start view is verified.");
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-5998_Table-119")]
        public void ST5998_PerformanceMessurementLongPeriod()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            // Load Dashboard Page
            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode after skipping intro pages and dashboard is loaded");

            for (int i = 0; i < dashboardPage.GetNumberOfPrograms(); i++)
            {
                dashboardPage.SelectProgram(i);
            }
            ReportHelper.LogTest(Status.Info, "Program switched via dashboard page sucessfully");

            ReportHelper.LogTest(Status.Info, "Put device in stand by for 10 seconds");
            AppManager.DeviceSettings.LockDevice(10);
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Dashboard page not loaded");
            dashboardPage = new DashboardPage();
            ReportHelper.LogTest(Status.Info, "App is in foreground again and dashboard page is visible");
            for (int i = 0; i < dashboardPage.GetNumberOfPrograms(); i++)
            {
                dashboardPage.SelectProgram(i);
            }

            ReportHelper.LogTest(Status.Info, "Navigation to different programs is possible after stand by");
        }

        #endregion Sprint 2

        #endregion Test Cases
    }
}