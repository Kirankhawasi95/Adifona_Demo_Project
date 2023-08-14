using System;
using System.Collections.Generic;
using System.Threading;
using AventStack.ExtentReports;
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
using HorusUITest.PageObjects.Menu.Programs;
using HorusUITest.PageObjects.Menu.Settings;
using HorusUITest.PageObjects.Start.Intro;
using NUnit.Framework;

namespace HorusUITest.TestFixtures.TestCases
{
    public class SystemTestsDemo2 : BaseResettingTestFixture
    {
        #region Constructor

        public SystemTestsDemo2(Platform platform)
            : base(platform)
        {

        }

        #endregion Constructor

        #region Test Cases

        #region Sprint 2

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-6094_Table-116")]
        public void ST6094_UIGraphicsForAppLaunch()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            ReportHelper.LogTest(Status.Info, "Checking if Intro 1 page loaded...");
            Assert.IsTrue(new IntroPageOne().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Intro 1 page is not loaded");
            ReportHelper.LogTest(Status.Pass, "Intro 1 page is loaded");

            ReportHelper.LogTest(Status.Info, "Checking the welcome page title text...");
            Assert.IsNotEmpty(new IntroPageOne().GetTitle(), "Welcome page displayed and title is empty");
            ReportHelper.LogTest(Status.Pass, "Welcome page title is not empty");
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-6442_Table-106")]
        public void ST6442_SwitchNearFieldAndMapView()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode after skipping intro pages and dashboard is loaded");

            ReportHelper.LogTest(Status.Info, "Opening main menu using tap...");
            dashboardPage.OpenMenuUsingTap();
            ReportHelper.LogTest(Status.Info, "Opened main menu using tap");
            ReportHelper.LogTest(Status.Info, "Opening help menu from main menu...");
            new MainMenuPage().OpenHelp();
            ReportHelper.LogTest(Status.Info, "Opened help menu from main menu");
            ReportHelper.LogTest(Status.Info, "Opening find hearng device page from help menu...");
            new HelpMenuPage().OpenFindHearingDevices();
            PermissionHelper.AllowPermissionIfRequested();
            ReportHelper.LogTest(Status.Info, "The find hearing aid process started successfully.");
            var finddevicespage = new FindDevicesPage();
            ReportHelper.LogTest(Status.Info, "Opened find hearng device page from help menu");

            ReportHelper.LogTest(Status.Info, "Switching between NEAR FIELD SEARCH and MAP VIEW for 4 times...");
            var isMapViewSelected = finddevicespage.GetIsMapViewSelected();
            for (int i = 0; i < 4; i++)
            {
                finddevicespage = finddevicespage.ToggleView();
                if (!isMapViewSelected)
                    finddevicespage.GetIsNearFieldViewSelected();
                else
                    finddevicespage.GetIsMapViewSelected();
            }
            ReportHelper.LogTest(Status.Pass, "Switched between NEAR FIELD SEARCH and MAP VIEW for 4 times and app did not crash");

            ReportHelper.LogTest(Status.Info, "Switching to near field view...");
            finddevicespage.SelectNearFieldView();
            if (!finddevicespage.GetIsNearFieldViewSelected())
                ReportHelper.LogTest(Status.Fail, "Switching between NEAR FIELD SEARCH and MAP VIEW Failed");
            Assert.IsTrue(finddevicespage.GetIsNearFieldViewSelected(), "Not switched to near field view");
            ReportHelper.LogTest(Status.Info, "Switched to near field view");
            ReportHelper.LogTest(Status.Info, "Switching to map view...");
            finddevicespage.SelectMapView();
            if (!finddevicespage.GetIsMapViewSelected())
                ReportHelper.LogTest(Status.Fail, "Switching between NEAR FIELD SEARCH and MAP VIEW Failed");
            Assert.IsTrue(finddevicespage.GetIsMapViewSelected(), "Not switched to near map view");
            ReportHelper.LogTest(Status.Info, "Switched to map view");
            ReportHelper.LogTest(Status.Info, "Switching to near field view...");
            finddevicespage.SelectNearFieldView();
            if (!finddevicespage.GetIsNearFieldViewSelected())
                ReportHelper.LogTest(Status.Fail, "Switching between NEAR FIELD SEARCH and MAP VIEW Failed");
            Assert.IsTrue(finddevicespage.GetIsNearFieldViewSelected(), "Not switched to near field view");
            ReportHelper.LogTest(Status.Info, "Switched to near field view");
            ReportHelper.LogTest(Status.Pass, "Toggled between NEAR FIELD SEARCH and MAP VIEW and app did not crash");

            AppManager.DeviceSettings.DisableWifi();
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-6364_Table-109")]
        public void ST6364_ChangeVolumeControl()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            const int sliderStepCount = 21;
            double tolerance = 1f / sliderStepCount;
            //if (OniOS) tolerance *= 2;      //HACK: iOS swiping is less precise
            tolerance *= 2;

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode after skipping intro pages and dashboard is loaded");

            ReportHelper.LogTest(Status.Info, "Changing dashboard page volume settings randomly...");
            SetDashboardSliderValue(0.7);
            SetDashboardSliderValue(0);
            SetDashboardSliderValue(0.5);
            SetDashboardSliderValue(0.1);
            SetDashboardSliderValue(0.9);
            SetDashboardSliderValue(0.4);
            SetDashboardSliderValue(0.7);
            SetDashboardSliderValue(0.5);
            SetDashboardSliderValue(0);
            SetDashboardSliderValue(1);
            SetDashboardSliderValue(0);
            SetDashboardSliderValue(0.5);
            SetDashboardSliderValue(0.1);
            SetDashboardSliderValue(0.9);
            SetDashboardSliderValue(0.4);
            SetDashboardSliderValue(0.7);
            SetDashboardSliderValue(0.5);
            SetDashboardSliderValue(0);
            SetDashboardSliderValue(1);
            SetDashboardSliderValue(0.5);
            ReportHelper.LogTest(Status.Pass, "Changed dashboard page volume settings randomly and verified");

            // Open current program
            ReportHelper.LogTest(Status.Info, "Opening current program...");
            dashboardPage.OpenCurrentProgram();
            ReportHelper.LogTest(Status.Info, "Opened current program");

            // Open Binaural Settings
            ReportHelper.LogTest(Status.Info, "Opening program binaural settings...");
            new ProgramDetailPage().OpenBinauralSettings();
            var paramEditBinauralPage = new ProgramDetailParamEditBinauralPage();
            ReportHelper.LogTest(Status.Info, "Opened program binaural settings");

            // Checking UI
            ReportHelper.LogTest(Status.Info, "Checking program binaural settings title...");
            Assert.IsNotEmpty(paramEditBinauralPage.GetTitle(), "Program binaural settings title is empty");
            ReportHelper.LogTest(Status.Info, "Program binaural settings title is not empty");
            ReportHelper.LogTest(Status.Info, "Checking program binaural settings discription...");
            Assert.IsNotEmpty(paramEditBinauralPage.GetDescription(), "Program binaural settings discription is empty");
            ReportHelper.LogTest(Status.Info, "Program binaural settings discription is not empty");
            ReportHelper.LogTest(Status.Info, "Checking program binaural settings switch title...");
            Assert.IsNotEmpty(paramEditBinauralPage.GetBinauralSwitchTitle(), "Program binaural settings switch title is empty");
            ReportHelper.LogTest(Status.Info, "Program binaural settings switch title is not empty");

            // Turn on Binaural Switch
            ReportHelper.LogTest(Status.Info, "Checking if binaural switch is not checked by default...");
            Assert.IsFalse(paramEditBinauralPage.GetIsBinauralSwitchChecked(), "Binaural switch is checked by default");
            ReportHelper.LogTest(Status.Info, "Binaural switch is not checked by default");

            ReportHelper.LogTest(Status.Info, "Turning on binaural switch...");
            paramEditBinauralPage.TurnOnBinauralSeparation();
            Assert.IsTrue(paramEditBinauralPage.GetIsBinauralSwitchChecked(), "Binaural switch not turned on");
            ReportHelper.LogTest(Status.Info, "Binaural switch turned on");

            // Checking values
            ReportHelper.LogTest(Status.Info, "Checking if single volume control is visible...");
            Assert.IsFalse(paramEditBinauralPage.GetIsVolumeControlVisible(VolumeChannel.Single), "Single volume control is visible");
            ReportHelper.LogTest(Status.Info, "Single volume control is not visible");
            ReportHelper.LogTest(Status.Info, "Checking if left volume control is visible...");
            Assert.IsTrue(paramEditBinauralPage.GetIsVolumeControlVisible(VolumeChannel.Left), "Left volume control is not visible");
            ReportHelper.LogTest(Status.Info, "Left volume control is visible");
            ReportHelper.LogTest(Status.Info, "Checking if right volume control is visible...");
            Assert.IsTrue(paramEditBinauralPage.GetIsVolumeControlVisible(VolumeChannel.Right), "Right volume control is not visible");
            ReportHelper.LogTest(Status.Info, "Right volume control is visible");

            //Left volume control
            ReportHelper.LogTest(Status.Info, "Checking value for left...");
            var finalLeft = CheckVolumeControl(VolumeChannel.Left);
            ReportHelper.LogTest(Status.Info, "Value check for left successfull");

            //Right volume control
            ReportHelper.LogTest(Status.Info, "Checking value for right...");
            var finalRight = CheckVolumeControl(VolumeChannel.Right);
            ReportHelper.LogTest(Status.Info, "Value check for right successfull");

            // Checking if value of right and left are same
            ReportHelper.LogTest(Status.Info, "Checking if final right and final left values match...");
            Assert.AreEqual(finalLeft, finalRight, tolerance, "Final right and final left values do not match");
            ReportHelper.LogTest(Status.Info, "Final right and final left values match");

            // Single volume double check
            paramEditBinauralPage.Close();
            ReportHelper.LogTest(Status.Info, "Changing program detail page volume settings randomly...");
            var programdetailpage = new ProgramDetailPage();
            SetProgramDetailSliderValue(0.7);
            SetProgramDetailSliderValue(0);
            ReportHelper.LogTest(Status.Info, "Decreasing volume once...");
            programdetailpage.DecreaseVolume();
            Assert.AreEqual(0, new ProgramDetailPage().GetVolumeSliderValue(), tolerance, "Volume not decreased");
            ReportHelper.LogTest(Status.Info, "Volume decreased once");
            SetProgramDetailSliderValue(0.5);
            SetProgramDetailSliderValue(0.1);
            SetProgramDetailSliderValue(0.9);
            SetProgramDetailSliderValue(0);
            ReportHelper.LogTest(Status.Info, "Increasing volume once...");
            programdetailpage.IncreaseVolume();
            Assert.AreEqual(0, new ProgramDetailPage().GetVolumeSliderValue(), tolerance, "Volume not increased");
            ReportHelper.LogTest(Status.Info, "Volume increased once");
            ReportHelper.LogTest(Status.Pass, "Changed program detal page volume settings randomly and verified");

            void SetDashboardSliderValue(double value)
            {
                ReportHelper.LogTest(Status.Info, "Setting dashboard page slider value to '" + value.ToString() + "'...");
                new DashboardPage().SetVolumeSliderValue(value);
                Assert.AreEqual(value, new DashboardPage().GetVolumeSliderValue(), tolerance, "Dashboard page slider value not set to '" + value.ToString() + "'");
                ReportHelper.LogTest(Status.Info, "Dashboard page slider value set to '" + value.ToString() + "' and is verified");
            }

            void SetProgramDetailSliderValue(double value)
            {
                ReportHelper.LogTest(Status.Info, "Setting program detail page slider value to '" + value.ToString() + "'...");
                new ProgramDetailPage().SetVolumeSliderValue(value);
                Assert.AreEqual(value, new ProgramDetailPage().GetVolumeSliderValue(), tolerance, "Program detail page slider value not set to '" + value.ToString() + "'");
                ReportHelper.LogTest(Status.Info, "Program detail page slider value set to '" + value.ToString() + "' and is verified");
            }

            //Volume control check
            double CheckVolumeControl(VolumeChannel channel)
            {
                ReportHelper.LogTest(Status.Info, "Changing slider value set to '0.5'...");
                paramEditBinauralPage.SetVolumeSliderValue(channel, 0.5);
                var singleVolume50 = paramEditBinauralPage.GetVolumeSliderValue(channel);
                Assert.AreEqual(0.5, Math.Round(singleVolume50, 1), tolerance);
                ReportHelper.LogTest(Status.Info, "Slider value is '0.5' and is verified");

                ReportHelper.LogTest(Status.Info, "Changing slider value set to '0'...");
                paramEditBinauralPage.SetVolumeSliderValue(channel, 0.0);
                var singleVolume0 = paramEditBinauralPage.GetVolumeSliderValue(channel);
                Assert.AreEqual(0, Math.Round(singleVolume0, 0), tolerance);
                ReportHelper.LogTest(Status.Info, "Slider value is '0' and is verified");

                ReportHelper.LogTest(Status.Info, "Changing slider value set to '1'...");
                paramEditBinauralPage.SetVolumeSliderValue(channel, 1);
                var singleVolume1 = paramEditBinauralPage.GetVolumeSliderValue(channel);
                Assert.AreEqual(1, Math.Round(singleVolume1, 0), tolerance);
                ReportHelper.LogTest(Status.Info, "Slider value is '1' and is verified");

                ReportHelper.LogTest(Status.Info, "Changing slider value set to '0.75'...");
                paramEditBinauralPage.SetVolumeSliderValue(channel, 0.75);
                var singleVolume75 = paramEditBinauralPage.GetVolumeSliderValue(channel);
                Assert.AreEqual(0.75, Math.Round(singleVolume75, 2), tolerance);
                ReportHelper.LogTest(Status.Info, "Slider value is '0.75' and is verified");

                ReportHelper.LogTest(Status.Info, "Changing slider value set to '0.25'...");
                paramEditBinauralPage.SetVolumeSliderValue(channel, 0.25);
                var singleVolume25 = paramEditBinauralPage.GetVolumeSliderValue(channel);
                Assert.AreEqual(0.25, Math.Round(singleVolume25, 2), tolerance);
                ReportHelper.LogTest(Status.Info, "Slider value is '0.25' and is verified");

                ReportHelper.LogTest(Status.Info, "Changing slider value set to '0.1'...");
                paramEditBinauralPage.SetVolumeSliderValue(channel, 0.1);
                var singleVolume10 = paramEditBinauralPage.GetVolumeSliderValue(channel);
                Assert.AreEqual(0.1, Math.Round(singleVolume10, 1), tolerance);
                ReportHelper.LogTest(Status.Info, "Slider value is '0.1' and is verified");

                ReportHelper.LogTest(Status.Info, "Changing slider value set to '0.9'...");
                paramEditBinauralPage.SetVolumeSliderValue(channel, 0.90);
                var singleVolume90 = paramEditBinauralPage.GetVolumeSliderValue(channel);
                Assert.AreEqual(0.90, Math.Round(singleVolume90, 2), tolerance);
                ReportHelper.LogTest(Status.Info, "Slider value is '0.9' and is verified");

                ReportHelper.LogTest(Status.Info, "Decrease and increase volume and checking if it is still 0.9...");
                paramEditBinauralPage.DecreaseVolume(channel);
                Assert.Less(paramEditBinauralPage.GetVolumeSliderValue(channel), singleVolume90);
                paramEditBinauralPage.IncreaseVolume(channel);
                Assert.AreEqual(Math.Round(singleVolume90, 2), Math.Round(paramEditBinauralPage.GetVolumeSliderValue(channel), 2));
                ReportHelper.LogTest(Status.Info, "Slider value is '0.9' after decrease and increase of volume and is verified");

                return singleVolume90;
            }
        }

        #endregion Sprint 2

        #region Sprint 3

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-6257_Table-115")]
        public void ST6257_CreateAutoProgramStartConnectLocation()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode after skipping intro pages and dashboard is loaded");

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

            // Grant Full Access
            ReportHelper.LogTest(Status.Info, "Granting 'Always Allow' location access...");
            AppManager.DeviceSettings.GrantGPSBackgroundPermission();
            Thread.Sleep(5000);
            ReportHelper.LogTest(Status.Info, "'Always Allow' location access granted");

            ReportHelper.LogTest(Status.Info, "Create a favorite program for Music program...");
            string FavoriteName = "Favourite 01";
            var programAutomationPage = FavoriteHelper.CreateFavoriteHearingProgram(FavoriteName, 1);
            ReportHelper.LogTest(Status.Info, "Tapping 'Connect to a location' to set as auto start...");
            programAutomationPage.TapConnectToLocation();
            ReportHelper.LogTest(Status.Info, "Tapped 'Connect to a location' and set as auto start");
            ReportHelper.LogTest(Status.Info, "Disabling location...");
            AppManager.DeviceSettings.DisableLocation();
            DialogHelper.DenyIfDisplayed(TimeSpan.FromSeconds(3));
            ReportHelper.LogTest(Status.Info, "Location disabled");
            ReportHelper.LogTest(Status.Info, "Enabling location...");
            AppManager.DeviceSettings.EnableLocation();
            ReportHelper.LogTest(Status.Info, "Location enabled");

            /* ToDo: Need to remove the code if it is working in regression testing
            // As after granting 'Allow always' permission need to reopen Map to select location
            Thread.Sleep(3000);
            new AutomationGeofenceBindingPage().NavigateBack();
            new ProgramAutomationPage().TapConnectToLocation();
            */

            //select location
            Thread.Sleep(3000); // Wait till map gets loaded
            ReportHelper.LogTest(Status.Info, "Selecting map location...");
            new AutomationGeofenceBindingPage().WaitUntilNoLoadingIndicator().SelectPosition(0.5, 0.7);
            Thread.Sleep(1000);
            ReportHelper.LogTest(Status.Info, "Selected map location");
            ReportHelper.LogTest(Status.Info, "Clicking OK...");
            new AutomationGeofenceBindingPage().Ok();
            ReportHelper.LogTest(Status.Info, "Clicked OK");
            ReportHelper.LogTest(Status.Pass, "Map location is selected successfully app does not crash");

            programAutomationPage = new ProgramAutomationPage();
            var firstlocation = programAutomationPage.GeofenceAutomation.GetValue();
            ReportHelper.LogTest(Status.Info, "Checking if selected location is not empty...");
            Assert.IsNotNull(firstlocation, "Selected location is empty");
            ReportHelper.LogTest(Status.Info, "Selected location is not empty");
            ReportHelper.LogTest(Status.Info, "Checking if automation switch is checked...");
            Assert.IsTrue(programAutomationPage.GetIsAutomationSwitchChecked(), "Automation switch is not checked");
            ReportHelper.LogTest(Status.Info, "Automation switch is checked");

            ReportHelper.LogTest(Status.Info, "Checking the back navigation in create favorite workflow...");
            ReportHelper.LogTest(Status.Info, "Tapping back automation page to check if it goes to program icon page...");
            programAutomationPage.TapBack();
            Assert.IsTrue(new ProgramIconPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)), "Program icon page not loaded");
            ReportHelper.LogTest(Status.Info, "Program icon page loaded after tapping back");
            ReportHelper.LogTest(Status.Info, "Tapping back program icon page to check if it goes to program name page...");
            new ProgramIconPage().TapBack();
            Assert.IsTrue(new ProgramNamePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)), "Program name page not loaded");
            ReportHelper.LogTest(Status.Info, "Program name page loaded after tapping back");
            ReportHelper.LogTest(Status.Info, "Proceeding from program name page after setting the name...");
            new ProgramNamePage().EnterName(FavoriteName).Proceed();
            ReportHelper.LogTest(Status.Info, "Proceeded from program name page");
            ReportHelper.LogTest(Status.Info, "Proceeding from program icon page...");
            new ProgramIconPage().Proceed();
            ReportHelper.LogTest(Status.Info, "Proceeded from program icon page");
            ReportHelper.LogTest(Status.Info, "Proceeding from program automation page...");
            new ProgramAutomationPage().Proceed();
            ReportHelper.LogTest(Status.Info, "Proceeded from program automation page");
            ReportHelper.LogTest(Status.Pass, "Checking if back navigation works fine and favorite is saved successfully...");
            Assert.IsTrue(new ProgramDetailPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)), "Program detail page not loaded");
            ReportHelper.LogTest(Status.Pass, "Back navigation works fine and favorite is saved successfully");

            new ProgramDetailPage().ProgramDetailPageUiCheck();

            ReportHelper.LogTest(Status.Info, "Opening program settings...");
            new ProgramDetailPage().OpenProgramSettings();
            var programDetailSettingsControlPage = new ProgramDetailSettingsControlPage();
            ReportHelper.LogTest(Status.Info, "Program settings opened");
            ReportHelper.LogTest(Status.Pass, "Checking if automation is 'enabled' in the settings of favorite...");
            Assert.IsTrue(programDetailSettingsControlPage.GetIsAutoStartEnabled(), "Automation is not 'enabled' in the settings of favorite");
            ReportHelper.LogTest(Status.Pass, "Automation is 'enabled' in the settings of favorite");
            ReportHelper.LogTest(Status.Info, "Opening auto hearing program start...");
            programDetailSettingsControlPage.OpenAutoHearingProgramStart();
            ReportHelper.LogTest(Status.Info, "Opened auto hearing program start");

            ReportHelper.LogTest(Status.Info, "Checking if program automation page is visible after setting map location...");
            programAutomationPage = new ProgramAutomationPage();
            Assert.IsTrue(programAutomationPage.IsCurrentlyShown(), "Program automation page not visible after setting map location");
            ReportHelper.LogTest(Status.Info, "Program automation page is visible after setting map location");
            ReportHelper.LogTest(Status.Info, "Checking if connected to location and address are visible on page...");
            Assert.IsTrue(programAutomationPage.GeofenceAutomation.GetIsValueSet(), "Connected to location and address are not visible on page");
            ReportHelper.LogTest(Status.Pass, "Connected to location and address are visible on page");

            ReportHelper.LogTest(Status.Info, "Checking the automation page UI again...");
            ReportHelper.LogTest(Status.Info, "Checking if automation switch is checked...");
            Assert.IsTrue(programAutomationPage.GetIsAutomationSwitchChecked(), "Automation switch is not checked");
            ReportHelper.LogTest(Status.Info, "Automation switch is checked");
            ReportHelper.LogTest(Status.Info, "Checking if geofence automation is visible...");
            Assert.IsTrue(programAutomationPage.GetIsGeofenceAutomationVisible(), "Geofence automation is not visible");
            ReportHelper.LogTest(Status.Info, "Geofence automation is visible");
            ReportHelper.LogTest(Status.Info, "Checking if wifi automation is not visible...");
            Assert.IsFalse(programAutomationPage.GetIsWifiAutomationVisible(), "Wifi automation is visible");
            ReportHelper.LogTest(Status.Info, "Wifi automation is not visible");
            ReportHelper.LogTest(Status.Info, "Checking if geofence automation value is empty...");
            Assert.IsNotNull(programAutomationPage.GeofenceAutomation.GetValue(), "Geofence automation value is empty");
            ReportHelper.LogTest(Status.Info, "Geofence automation value is not empty");
            ReportHelper.LogTest(Status.Info, "Checking if delete icon is visible...");
            Assert.IsTrue(programAutomationPage.GeofenceAutomation.GetIsDeleteIconVisible(), "Delete icon is not visible");
            ReportHelper.LogTest(Status.Info, "Delete icon is visible");
            ReportHelper.LogTest(Status.Info, "Checking if settings icon is visible...");
            Assert.IsTrue(programAutomationPage.GeofenceAutomation.GetIsSettingsIconVisible(), "Settings icon is not visible");
            ReportHelper.LogTest(Status.Info, "Settings icon is visible");
            ReportHelper.LogTest(Status.Pass, "Automation page UI is verified");

            ReportHelper.LogTest(Status.Info, "Tapping Proceed after verification...");
            programAutomationPage.Proceed();
            ReportHelper.LogTest(Status.Info, "Tapped Proceed");

            ReportHelper.LogTest(Status.Info, "Opening main menu using tap...");
            new ProgramDetailPage().OpenMenuUsingTap();
            ReportHelper.LogTest(Status.Info, "Opened main menu using tap");
            ReportHelper.LogTest(Status.Info, "Opening settings menu...");
            new MainMenuPage().OpenSettings();
            ReportHelper.LogTest(Status.Info, "Opened settings menu");
            ReportHelper.LogTest(Status.Info, "Opening Permission page and disabling tracking...");
            new SettingsMenuPage().OpenPermissions();
            var settingPermissionsPage = new SettingPermissionsPage();
            settingPermissionsPage.TurnOffLocationPermission();
            ReportHelper.LogTest(Status.Info, "Opened Permission page and tracking disabled");
            ReportHelper.LogTest(Status.Info, "Tapping back to settings menu...");
            settingPermissionsPage.TapBack();
            ReportHelper.LogTest(Status.Info, "Tapped back to settings menu");
            ReportHelper.LogTest(Status.Info, "Tapping back to main menu...");
            new SettingsMenuPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Tapped back to main menu");
            ReportHelper.LogTest(Status.Info, "Closing main menu using tap...");
            new MainMenuPage().CloseMenuUsingTap();
            ReportHelper.LogTest(Status.Info, "Closed main menu using tap");

            ReportHelper.LogTest(Status.Info, "Opening program settings...");
            new ProgramDetailPage().OpenProgramSettings();
            programDetailSettingsControlPage = new ProgramDetailSettingsControlPage();
            ReportHelper.LogTest(Status.Info, "Opened program settings");
            ReportHelper.LogTest(Status.Info, "Checking if auto start is still enabled...");
            Assert.IsTrue(programDetailSettingsControlPage.GetIsAutoStartEnabled(), "Auto start is not enabled");
            ReportHelper.LogTest(Status.Info, "Auto start is still enabled");
            ReportHelper.LogTest(Status.Info, "Opening auto hearing program start...");
            programDetailSettingsControlPage.OpenAutoHearingProgramStart();
            ReportHelper.LogTest(Status.Info, "Opened auto hearing program start");

            ReportHelper.LogTest(Status.Info, "Checking the automation page UI again...");
            programAutomationPage = new ProgramAutomationPage();
            ReportHelper.LogTest(Status.Info, "Checking if automation switch is checked...");
            Assert.IsTrue(programAutomationPage.GetIsAutomationSwitchChecked(), "Automation switch is not checked");
            ReportHelper.LogTest(Status.Info, "Automation switch is checked");
            ReportHelper.LogTest(Status.Info, "Checking if geofence automation is visible...");
            Assert.IsTrue(programAutomationPage.GetIsGeofenceAutomationVisible(), "Geofence automation is not visible");
            ReportHelper.LogTest(Status.Info, "Geofence automation is visible");
            ReportHelper.LogTest(Status.Info, "Checking if wifi automation is not visible...");
            Assert.IsFalse(programAutomationPage.GetIsWifiAutomationVisible(), "Wifi automation is visible");
            ReportHelper.LogTest(Status.Info, "Wifi automation is not visible");
            ReportHelper.LogTest(Status.Info, "Checking if geofence automation value is empty...");
            Assert.IsNotNull(programAutomationPage.GeofenceAutomation.GetValue(), "Geofence automation value is empty");
            ReportHelper.LogTest(Status.Info, "Geofence automation value is not empty");
            ReportHelper.LogTest(Status.Info, "Checking if delete icon is visible...");
            Assert.IsTrue(programAutomationPage.GeofenceAutomation.GetIsDeleteIconVisible(), "Delete icon is not visible");
            ReportHelper.LogTest(Status.Info, "Delete icon is visible");
            ReportHelper.LogTest(Status.Info, "Checking if settings icon is visible...");
            Assert.IsTrue(programAutomationPage.GeofenceAutomation.GetIsSettingsIconVisible(), "Settings icon is not visible");
            ReportHelper.LogTest(Status.Info, "Settings icon is visible");
            ReportHelper.LogTest(Status.Pass, "Automation page UI is verified");

            ReportHelper.LogTest(Status.Info, "Changing the location previously set for autstart...");
            programAutomationPage = new ProgramAutomationPage();
            programAutomationPage.TapConnectToLocation();

            Thread.Sleep(3000); // Wait till map gets loaded
            ReportHelper.LogTest(Status.Info, "Changing map location...");
            new AutomationGeofenceBindingPage().WaitUntilNoLoadingIndicator().SelectPosition(0.7, 0.5);
            Thread.Sleep(1000);
            new AutomationGeofenceBindingPage().Ok();
            ReportHelper.LogTest(Status.Info, "Changed the location previously set for autstart");

            programAutomationPage = new ProgramAutomationPage();
            var secondlocation = programAutomationPage.GeofenceAutomation.GetValue();
            ReportHelper.LogTest(Status.Info, "Checking if selected location is not empty...");
            Assert.IsNotNull(secondlocation, "Selected location is empty");
            ReportHelper.LogTest(Status.Info, "Selected location is not empty");

            ReportHelper.LogTest(Status.Info, "Checking if first location and second location are not same...");
            Assert.AreNotEqual(secondlocation, firstlocation, "First location and second location are same");
            ReportHelper.LogTest(Status.Info, "First location and second location are not same");

            ReportHelper.LogTest(Status.Info, "Checking if automation switch is checked...");
            Assert.IsTrue(programAutomationPage.GetIsAutomationSwitchChecked(), "Automation switch is not checked");
            ReportHelper.LogTest(Status.Info, "Automation switch is checked");
            ReportHelper.LogTest(Status.Info, "Checking if geofence automation is visible...");
            Assert.IsTrue(programAutomationPage.GetIsGeofenceAutomationVisible(), "Geofence automation is not visible");
            ReportHelper.LogTest(Status.Info, "Geofence automation is visible");
            ReportHelper.LogTest(Status.Info, "Checking if wifi automation is not visible...");
            Assert.IsFalse(programAutomationPage.GetIsWifiAutomationVisible(), "Wifi automation is visible");
            ReportHelper.LogTest(Status.Info, "Wifi automation is not visible");

            ReportHelper.LogTest(Status.Info, "Deleting a connected location...");
            programAutomationPage.GeofenceAutomation.DeleteValue();
            ReportHelper.LogTest(Status.Info, "Connected location deleted");

            ReportHelper.LogTest(Status.Info, "Checking if automation switch is not checked...");
            Assert.IsFalse(programAutomationPage.GetIsAutomationSwitchChecked(), "Automation switch is checked");
            ReportHelper.LogTest(Status.Info, "Automation switch is not checked");
            ReportHelper.LogTest(Status.Info, "Checking if geofence automation is not visible...");
            Assert.IsFalse(programAutomationPage.GetIsGeofenceAutomationVisible(), "Geofence automation is visible");
            ReportHelper.LogTest(Status.Info, "Geofence automation is not visible");
            ReportHelper.LogTest(Status.Info, "Checking if wifi automation is not visible...");
            Assert.IsFalse(programAutomationPage.GetIsWifiAutomationVisible(), "Wifi automation is visible");
            ReportHelper.LogTest(Status.Info, "Wifi automation is not visible");

            ReportHelper.LogTest(Status.Info, "Turning on automation...");
            programAutomationPage.TurnOnAutomation();
            ReportHelper.LogTest(Status.Info, "Turned on automation");

            ReportHelper.LogTest(Status.Info, "Checking if automation switch is checked...");
            Assert.IsTrue(programAutomationPage.GetIsAutomationSwitchChecked(), "Automation switch is not checked");
            ReportHelper.LogTest(Status.Info, "Automation switch is checked");
            ReportHelper.LogTest(Status.Info, "Checking if geofence automation is visible...");
            Assert.IsTrue(programAutomationPage.GetIsGeofenceAutomationVisible(), "Geofence automation is not visible");
            ReportHelper.LogTest(Status.Info, "Geofence automation is visible");
            ReportHelper.LogTest(Status.Info, "Checking if wifi automation is visible...");
            Assert.IsTrue(programAutomationPage.GetIsWifiAutomationVisible(), "Wifi automation is not visible");
            ReportHelper.LogTest(Status.Info, "Wifi automation is visible");

            ReportHelper.LogTest(Status.Pass, "Loaction is deleted and program automation switch is deactivated now");

            AppManager.DeviceSettings.DisableWifi();
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-6001_Table-118")]
        public void ST6001_DetailSettingsForPrograms()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            DashboardPage dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode after skipping intro pages and dashboard is loaded");

            ReportHelper.LogTest(Status.Info, "Opening Music program details from menu using tap...");
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            ReportHelper.LogTest(Status.Info, "Opened Music program details from menu using tap");

            setMaxMin(new ProgramDetailPage(), true);

            // Create Favourite
            string FavoriteName = "Favourite 01";
            FavoriteHelper.CreateFavoriteHearingProgram(FavoriteName, 1).Proceed();
            var prgmDetailPage = new ProgramDetailPage();
            Assert.AreEqual(FavoriteName, prgmDetailPage.GetCurrentProgramName(), $"Expected to be on ProgramDetailPage of '" + FavoriteName + "' but was {prgmDetailPage.GetCurrentProgramName()}");
            ReportHelper.LogTest(Status.Info, "Favourite program '" + FavoriteName + "' is created successfully");

            setMaxMin(prgmDetailPage, false);

            ReportHelper.LogTest(Status.Info, "Closing the app...");
            AppManager.CloseApp();
            ReportHelper.LogTest(Status.Info, "App closed");
            ReportHelper.LogTest(Status.Info, "Starting the app with app data...");
            AppManager.StartApp(false);
            ReportHelper.LogTest(Status.Info, "App started with app data");
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)), "Dashboard page not loaded");
            dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            ReportHelper.LogTest(Status.Info, "App started and dashboard is loaded without going to the intro pages");

            ReportHelper.LogTest(Status.Info, "Opening Music program details from menu using tap...");
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            ReportHelper.LogTest(Status.Info, "Opened Music program details from menu using tap");

            verifyMax();

            ReportHelper.LogTest(Status.Info, "Opening Favorite program details from menu using tap...");
            new ProgramDetailPage().SelectProgramFromProgramsMenu(MainMenuTypes.Favorites, 0);
            ReportHelper.LogTest(Status.Info, "Opened Favorite program details from menu using tap");

            //check minimum values for the favourite program
            // The favorite program was created with max values.
            // After app restart it will ignore later modifed values
            // And will always retain max values
            verifyMax();

            void setMaxMin(ProgramDetailPage progmdetailpage, bool max)
            {
                double value = 0;
                if (max)
                {
                    ReportHelper.LogTest(Status.Info, "Setting all the parameters to maximum for program '" + progmdetailpage.GetCurrentProgramName() + "'...");
                    value = 1;
                }
                else
                    ReportHelper.LogTest(Status.Info, "Setting all the parameters to minimium for program '" + progmdetailpage.GetCurrentProgramName() + "'...");

                ReportHelper.LogTest(Status.Info, "Opening speech focus display...");
                progmdetailpage.SpeechFocusDisplay.OpenSettings();
                var programDetailParamEditSpeechFocusPage = new ProgramDetailParamEditSpeechFocusPage();
                ReportHelper.LogTest(Status.Info, "Opened speech focus display");
                if (max)
                {
                    ReportHelper.LogTest(Status.Info, "Setting speech focus to '" + SpeechFocus.Auto.ToString() + "' which is maximum...");
                    programDetailParamEditSpeechFocusPage.SelectSpeechFocus(SpeechFocus.Auto);
                    Assert.AreEqual(SpeechFocus.Auto, programDetailParamEditSpeechFocusPage.GetSelectedSpeechFocus(), "Speech focus not set to '" + SpeechFocus.Auto.ToString() + "'");
                    ReportHelper.LogTest(Status.Info, "Speech focus set to '" + SpeechFocus.Auto.ToString() + "' which is maximum and is verified");
                }
                else
                {
                    ReportHelper.LogTest(Status.Info, "Setting speech focus to '" + SpeechFocus.Off.ToString() + "' which is minimum...");
                    programDetailParamEditSpeechFocusPage.SelectSpeechFocus(SpeechFocus.Off);
                    Assert.AreEqual(SpeechFocus.Off, programDetailParamEditSpeechFocusPage.GetSelectedSpeechFocus(), "Speech focus not set to '" + SpeechFocus.Off.ToString() + "'");
                    ReportHelper.LogTest(Status.Info, "Speech focus set to '" + SpeechFocus.Off.ToString() + "' which is minimum and is verified");
                }
                ReportHelper.LogTest(Status.Info, "Closing speech focus display...");
                programDetailParamEditSpeechFocusPage.Close();
                ReportHelper.LogTest(Status.Info, "Closed speech focus display");

                ReportHelper.LogTest(Status.Info, "Opening noise reduction display...");
                new ProgramDetailPage().NoiseReductionDisplay.OpenSettings();
                var noiseReductionPage = new ProgramDetailParamEditNoiseReductionPage();
                ReportHelper.LogTest(Status.Info, "Opened noise reduction display");
                if (max)
                {
                    ReportHelper.LogTest(Status.Info, "Setting noise reduction to '" + NoiseReduction.Strong.ToString() + "' which is maximum...");
                    noiseReductionPage.SelectNoiseReduction(NoiseReduction.Strong);
                    Assert.AreEqual(NoiseReduction.Strong, noiseReductionPage.GetSelectedNoiseReduction(), "Noise reduction not set to '" + NoiseReduction.Strong.ToString() + "'");
                    ReportHelper.LogTest(Status.Info, "Noise reduction set to '" + NoiseReduction.Strong.ToString() + "' which is maximum and is verified");
                }
                else
                {
                    ReportHelper.LogTest(Status.Info, "Setting noise reduction to '" + NoiseReduction.Off.ToString() + "' which is minimum...");
                    noiseReductionPage.SelectNoiseReduction(NoiseReduction.Off);
                    Assert.AreEqual(NoiseReduction.Off, noiseReductionPage.GetSelectedNoiseReduction(), "Noise reduction not set to '" + NoiseReduction.Off.ToString() + "'");
                    ReportHelper.LogTest(Status.Info, "Noise reduction set to '" + NoiseReduction.Off.ToString() + "' which is minimum and is verified");
                }
                ReportHelper.LogTest(Status.Info, "Closing noise reduction display...");
                noiseReductionPage.Close();
                ReportHelper.LogTest(Status.Info, "Closed noise reduction display");

                ReportHelper.LogTest(Status.Info, "Opening tinnitus display...");
                new ProgramDetailPage().TinnitusDisplay.OpenSettings();
                var programDetailParamEditTinnitusPage = new ProgramDetailParamEditTinnitusPage();
                ReportHelper.LogTest(Status.Info, "Opened tinnitus display");
                ReportHelper.LogTest(Status.Info, "Setting tinnitus volume to '" + value.ToString() + "'...");
                programDetailParamEditTinnitusPage.SetVolumeSliderValue(value);
                Assert.AreEqual(Math.Round(value, 0), Math.Round(programDetailParamEditTinnitusPage.GetVolumeSliderValue(), 0), "Tinnitus volume not set to '" + value.ToString() + "'");
                ReportHelper.LogTest(Status.Info, "Tinnitus volume set to '" + value.ToString() + "' and is verified");
                ReportHelper.LogTest(Status.Info, "Setting tinnitus equalizer slider value of '" + EqBand.High.ToString() + "' to '" + value.ToString() + "'...");
                programDetailParamEditTinnitusPage.SetEqualizerSliderValue(EqBand.High, value);
                Assert.AreEqual(Math.Round(value, 0), Math.Round(programDetailParamEditTinnitusPage.GetEqualizerSliderValue(EqBand.High), 0), "Tinnitus equalizer slider value of '" + EqBand.High.ToString() + "' not set to '" + value.ToString() + "'");
                ReportHelper.LogTest(Status.Info, "Tinnitus equalizer slider value of '" + EqBand.High.ToString() + "' set to '" + value.ToString() + "' and is verified");
                ReportHelper.LogTest(Status.Info, "Setting tinnitus equalizer slider value of '" + EqBand.Mid.ToString() + "' to '" + value.ToString() + "'...");
                programDetailParamEditTinnitusPage.SetEqualizerSliderValue(EqBand.Mid, value);
                Assert.AreEqual(Math.Round(value, 0), Math.Round(programDetailParamEditTinnitusPage.GetEqualizerSliderValue(EqBand.Mid), 0), "Tinnitus equalizer slider value of '" + EqBand.Mid.ToString() + "' not set to '" + value.ToString() + "'");
                ReportHelper.LogTest(Status.Info, "Tinnitus equalizer slider value of '" + EqBand.Mid.ToString() + "' set to '" + value.ToString() + "' and is verified");
                ReportHelper.LogTest(Status.Info, "Setting tinnitus equalizer slider value of '" + EqBand.Low.ToString() + "' to '" + value.ToString() + "'...");
                programDetailParamEditTinnitusPage.SetEqualizerSliderValue(EqBand.Low, value);
                Assert.AreEqual(Math.Round(value, 0), Math.Round(programDetailParamEditTinnitusPage.GetEqualizerSliderValue(EqBand.Low), 0), "Tinnitus equalizer slider value of '" + EqBand.Low.ToString() + "' not set to '" + value.ToString() + "'");
                ReportHelper.LogTest(Status.Info, "Tinnitus equalizer slider value of '" + EqBand.Low.ToString() + "' set to '" + value.ToString() + "' and is verified");
                ReportHelper.LogTest(Status.Info, "Closing tinnitus display...");
                programDetailParamEditTinnitusPage.Close();
                ReportHelper.LogTest(Status.Info, "Closed tinnitus display");

                ReportHelper.LogTest(Status.Info, "Opening equalizer display...");
                new ProgramDetailPage().EqualizerDisplay.OpenSettings();
                var equalizerPage = new ProgramDetailParamEditEqualizerPage();
                ReportHelper.LogTest(Status.Info, "Opened equalizer display");
                ReportHelper.LogTest(Status.Info, "Setting equalizer slider value of '" + EqBand.High.ToString() + "' to '" + value.ToString() + "'...");
                equalizerPage.SetEqualizerSliderValue(EqBand.High, value);
                Assert.AreEqual(Math.Round(value, 0), Math.Round(equalizerPage.GetEqualizerSliderValue(EqBand.High), 0), "Equalizer slider value of '" + EqBand.High.ToString() + "' not set to '" + value.ToString() + "'");
                ReportHelper.LogTest(Status.Info, "Equalizer slider value of '" + EqBand.High.ToString() + "' set to '" + value.ToString() + "' and is verified");
                ReportHelper.LogTest(Status.Info, "Setting equalizer slider value of '" + EqBand.Mid.ToString() + "' to '" + value.ToString() + "'...");
                equalizerPage.SetEqualizerSliderValue(EqBand.Mid, value);
                Assert.AreEqual(Math.Round(value, 0), Math.Round(equalizerPage.GetEqualizerSliderValue(EqBand.Mid), 0), "Equalizer slider value of '" + EqBand.Mid.ToString() + "' not set to '" + value.ToString() + "'");
                ReportHelper.LogTest(Status.Info, "Equalizer slider value of '" + EqBand.Mid.ToString() + "' set to '" + value.ToString() + "' and is verified");
                ReportHelper.LogTest(Status.Info, "Setting equalizer slider value of '" + EqBand.Low.ToString() + "' to '" + value.ToString() + "'...");
                equalizerPage.SetEqualizerSliderValue(EqBand.Low, value);
                Assert.AreEqual(Math.Round(value, 0), Math.Round(equalizerPage.GetEqualizerSliderValue(EqBand.Low), 0), "Equalizer slider value of '" + EqBand.Low.ToString() + "' not set to '" + value.ToString() + "'");
                ReportHelper.LogTest(Status.Info, "Equalizer slider value of '" + EqBand.Low.ToString() + "' set to '" + value.ToString() + "' and is verified");
                ReportHelper.LogTest(Status.Info, "Closing equalizer display...");
                equalizerPage.Close();
                ReportHelper.LogTest(Status.Info, "Closed equalizer display");

                if (max)
                    ReportHelper.LogTest(Status.Pass, "Set all the parameters to maximum for program '" + new ProgramDetailPage().GetCurrentProgramName() + "' and is verified");
                else
                    ReportHelper.LogTest(Status.Pass, "Set all the parameters to minimum for program '" + new ProgramDetailPage().GetCurrentProgramName() + "' and is verified");
            }

            void verifyMax()
            {
                double value = 1;

                ReportHelper.LogTest(Status.Info, "Verify if all parameters set to maximum for program '" + new ProgramDetailPage().GetCurrentProgramName() + "'...");

                ReportHelper.LogTest(Status.Info, "Opening speech focus display...");
                new ProgramDetailPage().SpeechFocusDisplay.OpenSettings();
                var speechFocusPage = new ProgramDetailParamEditSpeechFocusPage();
                ReportHelper.LogTest(Status.Info, "Opened speech focus display");
                ReportHelper.LogTest(Status.Info, "Checking if speech focus display value set to '" + SpeechFocus.Auto.ToString() + "'...");
                Assert.AreEqual(SpeechFocus.Auto, speechFocusPage.GetSelectedSpeechFocus(), "Speech focus display value not set to '" + SpeechFocus.Auto.ToString() + "'");
                ReportHelper.LogTest(Status.Info, "Speech focus display value set to '" + SpeechFocus.Auto.ToString() + "' and is verified");
                ReportHelper.LogTest(Status.Info, "Closing speech focus display...");
                speechFocusPage.Close();
                ReportHelper.LogTest(Status.Info, "Closed speech focus display");

                ReportHelper.LogTest(Status.Info, "Opening noise reduction display...");
                new ProgramDetailPage().NoiseReductionDisplay.OpenSettings();
                var noiseReductionPageInFirstPrgm = new ProgramDetailParamEditNoiseReductionPage();
                ReportHelper.LogTest(Status.Info, "Opened noise reduction display");
                ReportHelper.LogTest(Status.Info, "Checking if noise reduction is set to '" + NoiseReduction.Strong.ToString() + "' which is maximum...");
                Assert.AreEqual(NoiseReduction.Strong, noiseReductionPageInFirstPrgm.GetSelectedNoiseReduction(), "Noise reduction not set to '" + NoiseReduction.Strong.ToString() + "'");
                ReportHelper.LogTest(Status.Info, "Noise reduction set to '" + NoiseReduction.Strong.ToString() + "' which is maximum and is verified");
                ReportHelper.LogTest(Status.Info, "Closing noise reduction display...");
                noiseReductionPageInFirstPrgm.Close();
                ReportHelper.LogTest(Status.Info, "Closed noise reduction display");

                ReportHelper.LogTest(Status.Info, "Opening tinnitus display...");
                new ProgramDetailPage().TinnitusDisplay.OpenSettings();
                var tinnitusPageInFirstPrgm = new ProgramDetailParamEditTinnitusPage();
                ReportHelper.LogTest(Status.Info, "Opened tinnitus display");
                ReportHelper.LogTest(Status.Info, "Checking if tinnitus volume is set to '" + value.ToString() + "'...");
                Assert.AreEqual(value, tinnitusPageInFirstPrgm.GetVolumeSliderValue(), "Tinnitus volume not set to '" + value.ToString() + "'");
                ReportHelper.LogTest(Status.Info, "Tinnitus volume set to '" + value.ToString() + "' and is verified");
                ReportHelper.LogTest(Status.Info, "Checking if tinnitus equalizer slider value of '" + EqBand.Low.ToString() + "' is set to '" + value.ToString() + "'...");
                Assert.AreEqual(value, tinnitusPageInFirstPrgm.GetEqualizerSliderValue(EqBand.Low), "Tinnitus equalizer slider value of '" + EqBand.Low.ToString() + "' not set to '" + value.ToString() + "'");
                ReportHelper.LogTest(Status.Info, "Tinnitus equalizer slider value of '" + EqBand.Low.ToString() + "' set to '" + value.ToString() + "' and is verified");
                ReportHelper.LogTest(Status.Info, "Checking if tinnitus equalizer slider value of '" + EqBand.Mid.ToString() + "' is set to '" + value.ToString() + "'...");
                Assert.AreEqual(value, tinnitusPageInFirstPrgm.GetEqualizerSliderValue(EqBand.Mid), "Tinnitus equalizer slider value of '" + EqBand.Mid.ToString() + "' not set to '" + value.ToString() + "'");
                ReportHelper.LogTest(Status.Info, "Tinnitus equalizer slider value of '" + EqBand.Mid.ToString() + "' set to '" + value.ToString() + "' and is verified");
                ReportHelper.LogTest(Status.Info, "Checking if tinnitus equalizer slider value of '" + EqBand.High.ToString() + "' is set to '" + value.ToString() + "'...");
                Assert.AreEqual(value, tinnitusPageInFirstPrgm.GetEqualizerSliderValue(EqBand.High), "Tinnitus equalizer slider value of '" + EqBand.High.ToString() + "' not set to '" + value.ToString() + "'");
                ReportHelper.LogTest(Status.Info, "Tinnitus equalizer slider value of '" + EqBand.High.ToString() + "' set to '" + value.ToString() + "' and is verified");
                ReportHelper.LogTest(Status.Info, "Closing tinnitus display...");
                tinnitusPageInFirstPrgm.Close();
                ReportHelper.LogTest(Status.Info, "Closed tinnitus display");

                ReportHelper.LogTest(Status.Info, "Opening equalizer display...");
                new ProgramDetailPage().EqualizerDisplay.OpenSettings();
                var equalizerPageInFirstPrgm = new ProgramDetailParamEditEqualizerPage();
                ReportHelper.LogTest(Status.Info, "Opened equalizer display");
                ReportHelper.LogTest(Status.Info, "Checking if equalizer slider value of '" + EqBand.Low.ToString() + "' is set to '" + value.ToString() + "'...");
                Assert.AreEqual(value, equalizerPageInFirstPrgm.GetEqualizerSliderValue(EqBand.Low), "Equalizer slider value of '" + EqBand.Low.ToString() + "' not set to '" + value.ToString() + "'");
                ReportHelper.LogTest(Status.Info, "Equalizer slider value of '" + EqBand.Low.ToString() + "' set to '" + value.ToString() + "' and is verified");
                ReportHelper.LogTest(Status.Info, "Checking if equalizer slider value of '" + EqBand.Mid.ToString() + "' is set to '" + value.ToString() + "'...");
                Assert.AreEqual(value, equalizerPageInFirstPrgm.GetEqualizerSliderValue(EqBand.Mid), "Equalizer slider value of '" + EqBand.Mid.ToString() + "' not set to '" + value.ToString() + "'");
                ReportHelper.LogTest(Status.Info, "Equalizer slider value of '" + EqBand.Mid.ToString() + "' set to '" + value.ToString() + "' and is verified");
                ReportHelper.LogTest(Status.Info, "Checking if equalizer slider value of '" + EqBand.High.ToString() + "' is set to '" + value.ToString() + "'...");
                Assert.AreEqual(value, equalizerPageInFirstPrgm.GetEqualizerSliderValue(EqBand.High), "Equalizer slider value of '" + EqBand.High.ToString() + "' not set to '" + value.ToString() + "'");
                ReportHelper.LogTest(Status.Info, "Equalizer slider value of '" + EqBand.High.ToString() + "' set to '" + value.ToString() + "' and is verified");
                ReportHelper.LogTest(Status.Info, "Closing equalizer display...");
                equalizerPageInFirstPrgm.Close();
                ReportHelper.LogTest(Status.Info, "Closed equalizer display");

                ReportHelper.LogTest(Status.Pass, "All the parameters set to maximum for program '" + new ProgramDetailPage().GetCurrentProgramName() + "' and is verified");
            }
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-6333_Table-113")]
        public void ST6333_CreateFavoriteWorkflow()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode after skipping intro pages and dashboard is loaded");

            ReportHelper.LogTest(Status.Info, "Selecting second program from dashboard...");
            dashboardPage.SelectProgram(2);
            ReportHelper.LogTest(Status.Info, "Selected second program from dashboard");
            var currentProgramName = dashboardPage.GetCurrentProgramName();
            ReportHelper.LogTest(Status.Info, "Opening selected program from dashboard...");
            dashboardPage.OpenCurrentProgram();
            var programDetailPage = new ProgramDetailPage();
            ReportHelper.LogTest(Status.Info, "Opened selected program from dashboard which is '" + currentProgramName + "'");

            new ProgramDetailPage().ProgramDetailPageUiCheck();

            ReportHelper.LogTest(Status.Info, "Opening program settings...");
            programDetailPage.OpenProgramSettings();
            var programDetailSettingsControlPage = new ProgramDetailSettingsControlPage();
            ReportHelper.LogTest(Status.Info, "Opened program settings");

            var myFavName = "Favorite 01";
            var myFavChangedName = "Favorite 01 Edited";

            ReportHelper.LogTest(Status.Info, "Creating favorite with name " + myFavName + "...");
            ReportHelper.LogTest(Status.Info, "Creating favorite without name...");
            programDetailSettingsControlPage.CreateFavorite();
            var programNamePage = new ProgramNamePage();
            ReportHelper.LogTest(Status.Info, "Clicking on proceed button in program name page...");
            programNamePage.Proceed();
            ReportHelper.LogTest(Status.Info, "Clicked on proceed button in program name page");
            ReportHelper.LogTest(Status.Info, "Checking if confirmation dialog appears in program name page for empty program name...");
            Assert.IsTrue(new AppDialog().IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), "Confirmation dialog not appeared in program name page for empty program name");
            var createFavoriteDialog = new AppDialog();
            ReportHelper.LogTest(Status.Info, "Confirmation dialog appeared in program name page for empty program name");
            ReportHelper.LogTest(Status.Info, "Checking dialog message in program name page for empty program name...");
            Assert.IsNotEmpty(createFavoriteDialog.GetMessage(), "Dialog message is empty in program name page for empty program name");
            ReportHelper.LogTest(Status.Info, "Dialog message is not empty in program name page for empty program name");
            ReportHelper.LogTest(Status.Info, "Clicking on confirm button in program name page...");
            createFavoriteDialog.Confirm();
            ReportHelper.LogTest(Status.Info, "Clicked on confirm button in program name page");
            programNamePage = new ProgramNamePage();
            ReportHelper.LogTest(Status.Info, "Entering program name in program name page...");
            programNamePage.EnterName(myFavName);
            ReportHelper.LogTest(Status.Info, "Program name entered in program name page");
            ReportHelper.LogTest(Status.Info, "Clicking cancel button in program name page...");
            programNamePage.Cancel();
            ReportHelper.LogTest(Status.Info, "Clicked cancel button in program name page");
            ReportHelper.LogTest(Status.Info, "Checking if confirmation dialog appears in program name page after clicking cancel...");
            Assert.IsTrue(new AppDialog().IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), "Confirmation dialog not appeared in program name page after clicking cancel");
            createFavoriteDialog = new AppDialog();
            ReportHelper.LogTest(Status.Info, "Confirmation dialog appeared in program name page after clicking cancel");
            ReportHelper.LogTest(Status.Info, "Checking dialog message in program name page after clicking cancel...");
            Assert.IsNotEmpty(createFavoriteDialog.GetMessage(), "Dialog message is empty in program name page after clicking cancel");
            ReportHelper.LogTest(Status.Info, "Dialog message is not empty in program name page after clicking cancel");
            ReportHelper.LogTest(Status.Info, "Clicking cancel button in program name page app dialog...");
            createFavoriteDialog.Deny();
            ReportHelper.LogTest(Status.Info, "Clicked cancel button in program name page app dialog");
            ReportHelper.LogTest(Status.Info, "Clicking on proceed button in program name page...");
            new ProgramNamePage().Proceed();
            ReportHelper.LogTest(Status.Info, "Clicked on proceed button in program name page");

            ReportHelper.LogTest(Status.Info, "Clicking cancel button in program icon page...");
            new ProgramIconPage().Cancel();
            ReportHelper.LogTest(Status.Info, "Clicked cancel button in program icon page");
            ReportHelper.LogTest(Status.Info, "Checking if confirmation dialog appears in program icon page after clicking cancel...");
            Assert.IsTrue(new AppDialog().IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), "Confirmation dialog not appeared in program icon page after clicking cancel");
            createFavoriteDialog = new AppDialog();
            ReportHelper.LogTest(Status.Info, "Confirmation dialog appeared in program icon page after clicking cancel");
            ReportHelper.LogTest(Status.Info, "Checking dialog message in program icon page after clicking cancel...");
            Assert.IsNotEmpty(createFavoriteDialog.GetMessage(), "Dialog message is empty in program icon page after clicking cancel");
            ReportHelper.LogTest(Status.Info, "Dialog message is not empty in program icon page after clicking cancel");
            ReportHelper.LogTest(Status.Info, "Clicking cancel button in program icon page app dialog...");
            createFavoriteDialog.Deny();
            ReportHelper.LogTest(Status.Info, "Clicked cancel button in program icon page app dialog");
            ReportHelper.LogTest(Status.Info, "Selecting 4th icon in program icon page and clicking proceed...");
            var programIconPage = new ProgramIconPage();
            programIconPage.SelectIcon(4);
            programIconPage.Proceed();
            ReportHelper.LogTest(Status.Info, "Selected 4th icon in program icon page and clicked proceed");

            var programAutomationPage = new ProgramAutomationPage();
            ReportHelper.LogTest(Status.Info, "Turning on automation switch...");
            programAutomationPage.TurnOnAutomation();
            ReportHelper.LogTest(Status.Info, "Automation switch turned on");
            ReportHelper.LogTest(Status.Info, "Checking if geofence automation is visible...");
            Assert.IsTrue(programAutomationPage.GetIsGeofenceAutomationVisible(), "Geofence automation is not visible");
            ReportHelper.LogTest(Status.Info, "Geofence automation is visible");
            ReportHelper.LogTest(Status.Info, "Checking if wifi automation is visible...");
            Assert.IsTrue(programAutomationPage.GetIsWifiAutomationVisible(), "Wifi automation is not visible");
            ReportHelper.LogTest(Status.Info, "Wifi automation is visible");

            ReportHelper.LogTest(Status.Info, "Enabling wifi...");
            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Wifi enabled");

            ReportHelper.LogTest(Status.Info, "Granting 'Always Allow' permission...");
            AppManager.DeviceSettings.GrantGPSBackgroundPermission();
            Thread.Sleep(5000);
            ReportHelper.LogTest(Status.Info, "'Always Allow' permission granted");

            ReportHelper.LogTest(Status.Info, "Tapping connect to wifi...");
            programAutomationPage.TapConnectToWiFi();
            ReportHelper.LogTest(Status.Info, "Tapped connect to wifi");
            var wifiName1 = new AutomationWifiBindingPage().GetWifiName();
            ReportHelper.LogTest(Status.Info, "Clicking OK...");
            new AutomationWifiBindingPage().Ok();
            ReportHelper.LogTest(Status.Info, "Clicked OK");

            ReportHelper.LogTest(Status.Pass, "Checking if program settings are not lost after navigating back and forward...");
            ReportHelper.LogTest(Status.Info, "Clicking tap back on automation page...");
            new ProgramAutomationPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Clicked tap back on automation page");
            ReportHelper.LogTest(Status.Info, "Clicking tap back on program icon page...");
            new ProgramIconPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Clicked tap back on program icon page");
            programNamePage = new ProgramNamePage();
            ReportHelper.LogTest(Status.Info, "Hiding keyboard on program name page...");
            AppManager.DeviceSettings.HideKeyboard();
            ReportHelper.LogTest(Status.Info, "Hidded keyboard on program name page");
            ReportHelper.LogTest(Status.Info, "Checking if program name has changed in program name page...");
            Assert.AreEqual(myFavName, programNamePage.GetName(), "Program name has changed in program name page");
            ReportHelper.LogTest(Status.Info, "Program name has not changed in program name page");
            ReportHelper.LogTest(Status.Info, "Clicking proceed on program name page...");
            programNamePage.Proceed();
            ReportHelper.LogTest(Status.Info, "Clicked proceed on program name page");
            ReportHelper.LogTest(Status.Info, "Clicking proceed on program icon page...");
            new ProgramIconPage().Proceed();
            ReportHelper.LogTest(Status.Info, "Clicked proceed on program icon page");
            ReportHelper.LogTest(Status.Info, "Checking if program automation page is loaded...");
            Assert.IsTrue(programAutomationPage.IsCurrentlyShown(), "Program automation page is not loaded");
            programAutomationPage = new ProgramAutomationPage();
            ReportHelper.LogTest(Status.Info, "Program automation page is loaded");
            var wifiName2 = programAutomationPage.WifiAutomation.GetValue();
            ReportHelper.LogTest(Status.Info, "Checking if the wifi name is same...");
            Assert.AreEqual(wifiName1, wifiName2, "Wifi name is not same");
            ReportHelper.LogTest(Status.Info, "Wifi name is same");
            ReportHelper.LogTest(Status.Pass, "Program settings are not lost after navigating back and forward");

            if (OnAndroid)
            {
                ReportHelper.LogTest(Status.Info, "Verifying back navigation through favourite workflow, using Android System back button...");
                ReportHelper.LogTest(Status.Info, "Tapping Android System back button on program automation page...");
                AppManager.App.PressBackButton();
                ReportHelper.LogTest(Status.Info, "Tapped Android System back button on program automation page");
                ReportHelper.LogTest(Status.Info, "Checking if page is in program icon page after Android System back...");
                Assert.IsTrue(programIconPage.IsCurrentlyShown(), "Page is not in program icon page after Android System back");
                ReportHelper.LogTest(Status.Info, "Page is in program icon page after Android System back");
                ReportHelper.LogTest(Status.Info, "Tapping Android System back button on program icon page...");
                AppManager.App.PressBackButton();
                ReportHelper.LogTest(Status.Info, "Tapped Android System back button on program icon page");
                ReportHelper.LogTest(Status.Info, "Checking if page is in program name page after Android System back...");
                Assert.IsTrue(programNamePage.IsCurrentlyShown(), "Page is not in program name page after Android System back");
                ReportHelper.LogTest(Status.Info, "Page is in program name page after Android System back");
                ReportHelper.LogTest(Status.Info, "Tapping Android System back button on program name page...");
                AppManager.App.PressBackButton();
                ReportHelper.LogTest(Status.Info, "Tapped Android System back button on program name page");
                ReportHelper.LogTest(Status.Info, "Checking 'Discard favourite' dialog appearing and click cancel...");
                bool isDiscardDisplayed = DialogHelper.DenyIfDisplayed<ProgramNamePage>();
                if (!isDiscardDisplayed)
                    ReportHelper.LogTest(Status.Info, "No 'Discard favourite' dialog has been dsiplayed when navigating backwards using Android system button");
                ReportHelper.LogTest(Status.Info, "Checking if page is in program name page after cancel...");
                Assert.IsTrue(programNamePage.IsCurrentlyShown());
                ReportHelper.LogTest(Status.Info, "Page is in program name page after cancel");
                ReportHelper.LogTest(Status.Info, "Clicking proceed in program name page...");
                programNamePage.Proceed();
                ReportHelper.LogTest(Status.Info, "Clicked proceed in program name page");
                ReportHelper.LogTest(Status.Info, "Checking if program icon page is loaded...");
                Assert.IsTrue(new ProgramIconPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)), "Program icon page is not loaded");
                Assert.IsTrue(new ProgramIconPage().IsCurrentlyShown(), "Program icon page is not loaded");
                ReportHelper.LogTest(Status.Info, "Program icon page is loaded");
                ReportHelper.LogTest(Status.Info, "Clicking proceed in program icon page...");
                new ProgramIconPage().Proceed();
                ReportHelper.LogTest(Status.Info, "Clicked proceed in program icon page");
                ReportHelper.LogTest(Status.Info, "Checking if program automation page is loaded...");
                Assert.IsTrue(new ProgramAutomationPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)), "Program automation page is not loaded");
                Assert.IsTrue(new ProgramAutomationPage().IsCurrentlyShown(), "Program automation page is not loaded");
                ReportHelper.LogTest(Status.Info, "Program automation page is loaded");
                ReportHelper.LogTest(Status.Pass, "Back navigation through favourite workflow, using Android System back button is verified");
            }
            ReportHelper.LogTest(Status.Info, "Clicking proceed in program automation page...");
            new ProgramAutomationPage().Proceed();
            ReportHelper.LogTest(Status.Info, "Clicked proceed in program automation page");
            ReportHelper.LogTest(Status.Pass, "Favorite with name " + myFavName + " created successfully");

            ReportHelper.LogTest(Status.Info, "Selecting programs in top program menu bar and verify each program...");
            programDetailPage = new ProgramDetailPage();
            for (int i = 0; i < programDetailPage.GetNumberOfVisibiblePrograms(); i++)
            {
                programDetailPage.SelectProgram(i);
                Thread.Sleep(2000);
                ReportHelper.LogTest(Status.Info, "Checking program name of program '" + programDetailPage.GetCurrentProgramName() + "'...");
                Assert.IsNotEmpty(programDetailPage.GetCurrentProgramName(), "Program name of program '" + programDetailPage.GetCurrentProgramName() + "' is empty");
                ReportHelper.LogTest(Status.Info, "Program name of program '" + programDetailPage.GetCurrentProgramName() + "' is not empty and is verified");
            }
            programDetailPage.TapBack();
            ReportHelper.LogTest(Status.Pass, "Selected programs in top program menu bar and each program is verified");

            ReportHelper.LogTest(Status.Info, "Opening main menu using tap...");
            new DashboardPage().OpenMenuUsingTap();
            ReportHelper.LogTest(Status.Info, "Opened main menu using tap");
            ReportHelper.LogTest(Status.Info, "Opening programs menu...");
            new MainMenuPage().OpenPrograms();
            ReportHelper.LogTest(Status.Info, "Opened programs menu");
            ReportHelper.LogTest(Status.Info, "Selecting first favorite program...");
            new ProgramsMenuPage().SelectFavoriteProgram(0);
            ReportHelper.LogTest(Status.Info, "Selected first favorite program");
            ReportHelper.LogTest(Status.Info, "Opening first favorite program...");
            new DashboardPage().OpenCurrentProgram();
            ReportHelper.LogTest(Status.Info, "Opened first favorite program");
            ReportHelper.LogTest(Status.Info, "Opening first favorite program settings...");
            new ProgramDetailPage().OpenProgramSettings();
            ReportHelper.LogTest(Status.Info, "Opened first favorite program settings");
            ReportHelper.LogTest(Status.Info, "Changing name of program to " + myFavChangedName + "...");
            new ProgramDetailSettingsControlPage().CustomizeName();
            programNamePage = new ProgramNamePage();
            programNamePage.EnterName(myFavChangedName);
            ReportHelper.LogTest(Status.Info, "Clicking on proceed in program name page...");
            programNamePage.Proceed();
            ReportHelper.LogTest(Status.Info, "Clicked on proceed in program name page");
            ReportHelper.LogTest(Status.Info, "Clicking on proceed in program name page...");
            Assert.AreEqual(myFavChangedName, new ProgramDetailPage().GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "Name of program changed to " + myFavChangedName + " successfully");
            ReportHelper.LogTest(Status.Info, "Opening first favorite program settings...");
            new ProgramDetailPage().OpenProgramSettings();
            ReportHelper.LogTest(Status.Info, "Opened first favorite program settings");
            ReportHelper.LogTest(Status.Info, "Changing icon of program...");
            new ProgramDetailSettingsControlPage().CustomizeIcon();
            programIconPage = new ProgramIconPage();
            programIconPage.SelectIcon(6);
            var changedIconText = programIconPage.GetIconText(6);
            ReportHelper.LogTest(Status.Info, "Clicking on proceed in program icon page...");
            programIconPage.Proceed();
            ReportHelper.LogTest(Status.Info, "Clicked on proceed in program icon page");
            ReportHelper.LogTest(Status.Info, "Collecting icons in list...");
            programDetailPage = new ProgramDetailPage();
            List<String> iconTextList = new List<String>();
            for (int i = 0; i < programDetailPage.GetNumberOfVisibiblePrograms(); i++)
            {
                iconTextList.Add(programDetailPage.GetTextOfVisibleProgram(i));
            }
            ReportHelper.LogTest(Status.Info, "Collected icons in list");
            ReportHelper.LogTest(Status.Info, "Checking if icon has been changed...");
            Assert.Contains(changedIconText, iconTextList, "Icon of program not changed");
            ReportHelper.LogTest(Status.Pass, "Icon of program changed and is verified");

            ReportHelper.LogTest(Status.Info, "Opening first favorite program settings...");
            new ProgramDetailPage().OpenProgramSettings();
            programDetailSettingsControlPage = new ProgramDetailSettingsControlPage();
            ReportHelper.LogTest(Status.Info, "Opened first favorite program settings");

            ReportHelper.LogTest(Status.Info, "Checking if auto start is enabled...");
            Assert.IsTrue(programDetailSettingsControlPage.GetIsAutoStartEnabled(), "Auto start is not enabled");
            ReportHelper.LogTest(Status.Info, "Auto start is enabled");

            ReportHelper.LogTest(Status.Info, "Opening auto hearing program start...");
            programDetailSettingsControlPage.OpenAutoHearingProgramStart();
            programAutomationPage = new ProgramAutomationPage();
            ReportHelper.LogTest(Status.Info, "Opened auto hearing program start");
            ReportHelper.LogTest(Status.Info, "Checking if geofence automation is visible...");
            Assert.IsFalse(programAutomationPage.GetIsGeofenceAutomationVisible(), "Geofence automation is visible");
            ReportHelper.LogTest(Status.Info, "Geofence automation is not visible");
            ReportHelper.LogTest(Status.Info, "Turning off automation...");
            programAutomationPage.TurnOffAutomation();
            ReportHelper.LogTest(Status.Info, "Automation turned off");

            ReportHelper.LogTest(Status.Info, "Disabling wifi...");
            AppManager.DeviceSettings.DisableWifi();
            ReportHelper.LogTest(Status.Info, "Wifi disabled");

            ReportHelper.LogTest(Status.Info, "Clicking on proceed in program automation page...");
            programAutomationPage.Proceed();
            ReportHelper.LogTest(Status.Info, "Clicked on proceed in program automation page");

            ReportHelper.LogTest(Status.Info, "Opening first favorite program settings...");
            new ProgramDetailPage().OpenProgramSettings();
            ReportHelper.LogTest(Status.Info, "Opened first favorite program settings");
            ReportHelper.LogTest(Status.Info, "Checking if auto start is enabled...");
            Assert.IsFalse(new ProgramDetailSettingsControlPage().GetIsAutoStartEnabled(), "Auto start is enabled");
            ReportHelper.LogTest(Status.Info, "Auto start is not enabled");

            ReportHelper.LogTest(Status.Info, "Deleting favorite program...");
            new ProgramDetailSettingsControlPage().DeleteFavoriteAndConfirm();
            ReportHelper.LogTest(Status.Info, "Checking if program is deleted...");
            Assert.AreNotEqual(myFavChangedName, new ProgramDetailPage().GetCurrentProgramName(), "Program is not deleted");
            ReportHelper.LogTest(Status.Info, "Program is deleted");
            ReportHelper.LogTest(Status.Pass, "Favorite program deleted successfully");
        }

        #endregion Sprint 3

        #endregion Test Cases
    }
}