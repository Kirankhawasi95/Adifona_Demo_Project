using HorusUITest.Helper;
using NUnit.Framework;
using Platform = HorusUITest.Enums.Platform;
using HorusUITest.PageObjects.Menu;
using HorusUITest.PageObjects.Menu.Settings;
using HorusUITest.PageObjects.Menu.Help;
using System;
using HorusUITest.Configuration;
using HorusUITest.Enums;
using HorusUITest.Data;
using HorusUITest.PageObjects;
using HorusUITest.PageObjects.Controls.ProgramDetailParams;
using System.Threading;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Favorites;
using AventStack.ExtentReports;

namespace HorusUITest.TestFixtures.TestCases
{
    public class SystemTestsDevice2 : BaseResettingTestFixture
    {
        #region Constructor

        public SystemTestsDevice2(Platform platform)
            : base(platform)
        {

        }

        #endregion Constructor

        #region Methods

        private void ResetAppFromMenu()
        {
            ReportHelper.LogTest(Status.Info, "Tapping back from find device page...");
            new FindDevicesPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Tapped back from find device page");
            ReportHelper.LogTest(Status.Info, "Tapping back from help menu page...");
            new HelpMenuPage().TapBack();
            ReportHelper.LogTest(Status.Info, "Tapped back from help menu page");

            ReportHelper.LogTest(Status.Info, "Opening settings menu...");
            new MainMenuPage().OpenSettings();
            ReportHelper.LogTest(Status.Info, "Opened settings menu");

            ReportHelper.LogTest(Status.Info, "Resetting app from settings menu...");
            new SettingsMenuPage().OpenAppReset();
            DialogHelper.ConfirmIfDisplayed();
            ReportHelper.LogTest(Status.Pass, "App reset done from settings menu");
        }

        /// <summary>
        /// Method to move Binural Slides
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="paramEditBinauralPage"></param>
        private void CheckVolumeControl(VolumeChannel channel, ref ProgramDetailParamEditBinauralPage paramEditBinauralPage)
        {
            const int sliderStepCount = 21;
            double tolerance = 1f / sliderStepCount;
            //HACK: iOS swiping is less precise
            //if (OniOS) tolerance *= 2;

            // Monaural or Binaural Program Volume is set to middle based on channel type
            ReportHelper.LogTest(Status.Info, "Setting slider value to 0.5...");
            paramEditBinauralPage.SetVolumeSliderValue(channel, 0.5);
            ReportHelper.LogTest(Status.Info, "Slider value set to 0.5...");
            var singleVolume50 = paramEditBinauralPage.GetVolumeSliderValue(channel);
            ReportHelper.LogTest(Status.Info, "Checking if slider value is 0.5...");
            Assert.AreEqual(0.5, Math.Round(singleVolume50, 1), delta: tolerance, "Slider value is not 0.5");
            ReportHelper.LogTest(Status.Info, "Slider value is 0.5");

            // Monaural or Binaural Program Volume is set to minimum based on channel type
            ReportHelper.LogTest(Status.Info, "Setting slider value to 0...");
            paramEditBinauralPage.SetVolumeSliderValue(channel, 0.0);
            ReportHelper.LogTest(Status.Info, "Slider value set to 0...");
            var singleVolume0 = paramEditBinauralPage.GetVolumeSliderValue(channel);
            ReportHelper.LogTest(Status.Info, "Checking if slider value is 0...");
            Assert.AreEqual(0, Math.Round(singleVolume0, 0), delta: tolerance, "Slider value is not 0");
            ReportHelper.LogTest(Status.Info, "Slider value is 0");

            // Monaural or Binaural Program Volume is set to maximum based on channel type
            ReportHelper.LogTest(Status.Info, "Setting slider value to 1...");
            paramEditBinauralPage.SetVolumeSliderValue(channel, 1);
            ReportHelper.LogTest(Status.Info, "Slider value set to 1...");
            var singleVolume1 = paramEditBinauralPage.GetVolumeSliderValue(channel);
            ReportHelper.LogTest(Status.Info, "Checking if slider value is 1...");
            Assert.AreEqual(1, Math.Round(singleVolume1, 0), delta: tolerance, "Slider value is not 1");
            ReportHelper.LogTest(Status.Info, "Slider value is 1");

            // Monaural or Binaural Program Volume is reset to middle based on channel type
            // Doing Reset in loop since when directly set to 0.5 to goes to 0.4. Hence doing it step by step
            ReportHelper.LogTest(Status.Info, "Setting slider value to 0.5...");
            for (double i = 0.9; i >= 0.5; i -= 0.1)
            {
                paramEditBinauralPage.SetVolumeSliderValue(channel, i);
            }
            ReportHelper.LogTest(Status.Info, "Slider value set to 0.5...");
            singleVolume50 = paramEditBinauralPage.GetVolumeSliderValue(channel);
            ReportHelper.LogTest(Status.Info, "Checking if slider value is 0.5...");
            Assert.AreEqual(0.5, Math.Round(singleVolume50, 1), delta: tolerance, "Slider value is not 0.5");
            ReportHelper.LogTest(Status.Info, "Slider value is 0.5");
        }

        #endregion Methods

        #region Test Cases

        #region Sprint 8

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-6432_Table-108")]
        public void ST6432_CheckLog()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            string LeftHearingAidName = SelectHearingAid.GetLeftHearingAid();
            string RightHearingAidName = SelectHearingAid.GetRightHearingAid();

            // Load Intro and connect to Hearing Aid
            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(LeftHearingAidName, RightHearingAidName).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + LeftHearingAidName + "' and Right '" + RightHearingAidName + "'");

            // Click on the menu button at the top right of the screen
            dashboardPage.OpenMenuUsingTap();
            ReportHelper.LogTest(Status.Info, "Click on the menu button at the top right of the screen");

            // Open Settings Menu
            new MainMenuPage().OpenSettings();
            ReportHelper.LogTest(Status.Info, "Open Settings Menu");

            // Open Logs
            new SettingsMenuPage().OpenLogs();
            ReportHelper.LogTest(Status.Info, "Open Logs");

            // Get Log Page Text First Time
            ReportHelper.LogTest(Status.Info, "First Log Verification");
            string LogText = new LogPage().GetLogText();
            Assert.AreNotEqual(LogText, string.Empty, "Log Text is empty");
            ReportHelper.LogTest(Status.Pass, "First check: Logs are available");
            new LogPage().TapBack();

            // Get Log Page Text Second Time
            ReportHelper.LogTest(Status.Info, "Second Log Verification");
            new SettingsMenuPage().OpenLogs();
            LogText = new LogPage().GetLogText();
            Assert.AreNotEqual(LogText, string.Empty, "Log Text is empty");
            ReportHelper.LogTest(Status.Pass, "Second check: Logs are available");
            new LogPage().TapBack();

            // Get Log Page Text Third Time
            ReportHelper.LogTest(Status.Info, "Third Log Verification");
            new SettingsMenuPage().OpenLogs();
            LogText = new LogPage().GetLogText();
            Assert.AreNotEqual(LogText, string.Empty, "Log Text is empty");
            ReportHelper.LogTest(Status.Pass, "Third check: Logs are available");
            new LogPage().TapBack();
        }

        [Test]
        [Category("SystemTestsDeviceLongStandBy")]
        [Description("TC-11729_Table-56")]
        public void ST11729_CheckBackgroundLocationActivities()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            // ToDo: There is timeout error for 15 mins. Currently putting it for 15 secs
            int appBackgroundDuration = 15;

            string leftHearingAidName = SelectHearingAid.GetLeftHearingAid();

            // Load Intro and connect to Hearing Aid
            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(leftHearingAidName).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aid Left '" + leftHearingAidName + "'");

            //Open Find hearing devices page
            NavigationHelper.NavigateToHelpMenu(dashboardPage).OpenFindHearingDevices();
            Assert.IsTrue(new FindDevicesPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Info, "Find hearing aid page is displayed");

            ReportHelper.LogTest(Status.Info, "Putting the app in background for 15 minutes.");
            AppManager.DeviceSettings.PutAppToBackground(appBackgroundDuration);
            Assert.IsTrue(new FindDevicesPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Info, "App is in foreground now and Find Devices Page view is displayed..");

            AppManager.DeviceSettings.DisableWifi();
        }

        #endregion Sprint 8

        #region Sprint 9

        /// <summary>
        /// This test step turn on/off HA and check the list on the app is skipped.
        /// As turning on/off HA is cannot be automated due to lack of hardware
        /// </summary>
        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-6505_Table-103")]
        public void ST6505_VerifyHearingAidSelectionList()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var selectHearingAidsPage = LaunchHelper.SkipToSelectHearingAidsPage().Page;
            Assert.IsTrue(selectHearingAidsPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Pass, "Select Hearing aids page is visible");

            selectHearingAidsPage.WaitUntilScanFinished();
            Assert.NotZero(selectHearingAidsPage.GetNumberOfDevices());
            var deviceList = selectHearingAidsPage.GetAllDeviceNames();
            CollectionAssert.IsNotEmpty(deviceList);
            CollectionAssert.AllItemsAreUnique(deviceList);

            ReportHelper.LogTest(Status.Info, "Following devices are shown in the list");

            foreach (var device in deviceList)
            {
                ReportHelper.LogTest(Status.Info, device);
            }

            ReportHelper.LogTest(Status.Pass, "All hearing aid appears only once in the list.");
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-6446_Table-104")]
        public void ST6446_PreSelectedViewInFindDevicesPage()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            HearingAid LeftHearingAid = SelectHearingAid.Left();
            HearingAid RightHearingAid = SelectHearingAid.Right();

            // Monaural Right
            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(RightHearingAid).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aid Right '" + RightHearingAid.Name + "'");

            ReportHelper.LogTest(Status.Info, "App started with Monaural Right configuration");
            NavigationHelper.NavigateToHelpMenu(dashboardPage).OpenFindHearingDevices();
            Thread.Sleep(2000);
            new FindDevicesPage().TapBack();
            new HelpMenuPage().OpenFindHearingDevices();
            new FindDevicesPage().CheckFindDevicesPage(ChannelMode.Monaural, RightHearingAid);
            ReportHelper.LogTest(Status.Pass, "Test finished for Monaural Right config successfully.");
            ResetAppFromMenu();

            // Monaural Left
            dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(LeftHearingAid).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aid Left '" + LeftHearingAid.Name + "'");

            ReportHelper.LogTest(Status.Info, "Test started for Monaural Left configuration");
            NavigationHelper.NavigateToHelpMenu(dashboardPage).OpenFindHearingDevices();
            new FindDevicesPage().CheckFindDevicesPage(ChannelMode.Monaural, LeftHearingAid);
            ReportHelper.LogTest(Status.Pass, "Test finished for Monaural Left config successfully.");
            ResetAppFromMenu();

            // Binaural
            dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(LeftHearingAid, RightHearingAid).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + LeftHearingAid.Name + "' and Right '" + RightHearingAid.Name + "'");

            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());

            ReportHelper.LogTest(Status.Info, "Test started for Binaural configuration");
            Assert.AreEqual(RightHearingAid.Channel, LeftHearingAid.Channel, "Channel has to be binaural for both hearing aids");
            NavigationHelper.NavigateToHelpMenu(dashboardPage).OpenFindHearingDevices();
            new FindDevicesPage().CheckFindDevicesPage(ChannelMode.Binaural, LeftHearingAid, RightHearingAid);
            ReportHelper.LogTest(Status.Pass, "Test finished for Binaural config successfully.");
            ResetAppFromMenu();

            AppManager.DeviceSettings.DisableWifi();
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-6443_Table-105")]
        public void ST6443_VerifyFindDevicesPageView()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            HearingAid LeftHearingAid = SelectHearingAid.Left();
            HearingAid RightHearingAid = SelectHearingAid.Right();

            //Testing with one device
            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(RightHearingAid).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aid Right '" + RightHearingAid.Name + "'");

            ReportHelper.LogTest(Status.Info, "App started with Name " + RightHearingAid.Name + " Side " + RightHearingAid.Side + " Channel " + RightHearingAid.Channel);

            NavigationHelper.NavigateToHelpMenu(dashboardPage).OpenFindHearingDevices();
            new FindDevicesPage().CheckFindDevicesPage(ChannelMode.Monaural, RightHearingAid);
            ReportHelper.LogTest(Status.Pass, "Check finished for monaural config successfully.");
            ResetAppFromMenu();

            //Binaural
            dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(LeftHearingAid, RightHearingAid).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + LeftHearingAid.Name + "' and Right '" + RightHearingAid.Name + "'");

            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());

            ReportHelper.LogTest(Status.Info, "First Hearing Aid Name " + LeftHearingAid.Name + " Side " + LeftHearingAid.Side + " Channel " + LeftHearingAid.Channel);
            ReportHelper.LogTest(Status.Info, "Second Hearing Aid Name " + RightHearingAid.Name + " Side " + RightHearingAid.Side + " Channel " + RightHearingAid.Channel);

            NavigationHelper.NavigateToHelpMenu(dashboardPage).OpenFindHearingDevices();
            new FindDevicesPage().CheckFindDevicesPage(ChannelMode.Binaural, LeftHearingAid, RightHearingAid);
            ReportHelper.LogTest(Status.Pass, "Test finished for Binaural config successfully.");
            ResetAppFromMenu();

            AppManager.DeviceSettings.DisableWifi();
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-6437_Table-107")]
        public void ST6437_ChangeVolumeControlBinauralParallel()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            string LeftHearingAidName = SelectHearingAid.GetLeftHearingAid();
            string RightHearingAidName = SelectHearingAid.GetRightHearingAid();

            // Load Intro and connect to Hearing Aid
            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(LeftHearingAidName, RightHearingAidName).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + LeftHearingAidName + "' and Right '" + RightHearingAidName + "'");

            // Open Basic Program
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 0);

            var programDetailPage = new ProgramDetailPage();

            // Open Binaural Settings
            programDetailPage.OpenBinauralSettings();

            var paramEditBinauralPage = new ProgramDetailParamEditBinauralPage();

            paramEditBinauralPage.TurnOnBinauralSeparation();
            Assert.IsTrue(paramEditBinauralPage.GetIsBinauralSwitchChecked(), "Turn on Binaural seperation failed");
            ReportHelper.LogTest(Status.Info, "Binaural settings activated");

            // Checking for Binaural Left
            // Create a thread and call a background method and start for Left
            Thread threadLeft = new Thread(() => { CheckVolumeControl(VolumeChannel.Left, ref paramEditBinauralPage); });
            threadLeft.Start();

            // Checking for Binaural Right
            CheckVolumeControl(VolumeChannel.Right, ref paramEditBinauralPage);

            ReportHelper.LogTest(Status.Info, "Checked volume for Binaural Left and Right togather");
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-6361_Table-111")]
        public void ST6361_ChangeVolumeControlRestartApp()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            string LeftHearingAidName = SelectHearingAid.GetLeftHearingAid();
            string RightHearingAidName = SelectHearingAid.GetRightHearingAid();

            // Load Intro and connect to Hearing Aid
            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(LeftHearingAidName, RightHearingAidName).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + LeftHearingAidName + "' and Right '" + RightHearingAidName + "'");

            // Open Basic Program
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 0);

            var programDetailPage = new ProgramDetailPage();

            // Open Binaural Settings
            programDetailPage.OpenBinauralSettings();

            var paramEditBinauralPage = new ProgramDetailParamEditBinauralPage();

            paramEditBinauralPage.TurnOnBinauralSeparation();
            Assert.IsTrue(paramEditBinauralPage.GetIsBinauralSwitchChecked(), "Turn on Binaural seperation failed");
            ReportHelper.LogTest(Status.Info, "Binaural settings activated");

            const int sliderStepCount = 21;
            double tolerance = 1f / sliderStepCount;
            //HACK: iOS swiping is less precise
            //if (OniOS) tolerance *= 2;

            // Set Left Volume to Minimum
            paramEditBinauralPage.SetVolumeSliderValue(VolumeChannel.Left, 0.0);
            var singleVolume0 = paramEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Left);
            Assert.AreEqual(0, singleVolume0, tolerance);
            ReportHelper.LogTest(Status.Info, "Left Volume to Minimum");

            paramEditBinauralPage.SetVolumeSliderValue(VolumeChannel.Right, 1.0);
            var singleVolume1 = paramEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Right);
            Assert.AreEqual(1.0, singleVolume1, tolerance);
            ReportHelper.LogTest(Status.Info, "Right Volume to Maximum");

            // Closing the App
            AppManager.CloseApp();
            ReportHelper.LogTest(Status.Info, "App Closed");

            // Restarting the App
            AppManager.RestartApp(false);
            ReportHelper.LogTest(Status.Info, "App Restarted");

            // Loading Dashboard
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            DashboardPage dashboardPageRestart = new DashboardPage();
            dashboardPageRestart.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPageRestart.IsCurrentlyShown());
            dashboardPageRestart.WaitForToastToDisappear();
            Assert.IsTrue(dashboardPageRestart.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPageRestart.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPageRestart.GetIsMenuHamburgerButtonDisplayed());

            // Open Basic Program
            dashboardPageRestart.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 0);

            var programDetailPageRestart = new ProgramDetailPage();

            // Open Binaural Settings
            programDetailPageRestart.OpenBinauralSettings();

            // Checking if the Binural Settings is enabled
            var paramEditBinauralPageRestart = new ProgramDetailParamEditBinauralPage();
            Assert.IsTrue(paramEditBinauralPageRestart.GetIsBinauralSwitchChecked(), "Turn on Binaural seperation failed");
            ReportHelper.LogTest(Status.Info, "Binaural settings activated after restart");

            // Checking Left Volume
            var singleVolume0Restart = paramEditBinauralPageRestart.GetVolumeSliderValue(VolumeChannel.Left);
            Assert.AreEqual(singleVolume0, singleVolume0Restart, "Left value after restart does not match");
            ReportHelper.LogTest(Status.Info, "Volume Left Settings Verified");

            // Reset to Default Volume
            paramEditBinauralPageRestart.SetVolumeSliderValue(VolumeChannel.Left, 0.5);

            // Checking Right Volume
            var singleVolume1Restart = paramEditBinauralPageRestart.GetVolumeSliderValue(VolumeChannel.Right);
            Assert.AreEqual(singleVolume1, singleVolume1Restart, "Right value after restart does not match");
            ReportHelper.LogTest(Status.Info, "Volume Right Settings Verified");

            // Reset to Default Volume
            paramEditBinauralPageRestart.SetVolumeSliderValue(VolumeChannel.Right, 0.5);

            paramEditBinauralPageRestart.Close();
        }

        #endregion Sprint 9

        #region Sprint 10

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-6543_Table-98")]
        public void ST6543_ApplyInitWorkflow()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            string LeftHearingAidName = SelectHearingAid.GetLeftHearingAid();
            string RightHearingAidName = SelectHearingAid.GetRightHearingAid();

            // Load Intro and connect to Hearing Aid
            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(LeftHearingAidName, RightHearingAidName).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + LeftHearingAidName + "' and Right '" + RightHearingAidName + "'");

            // Open Basic Program
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 0);
            var programDetailPage = new ProgramDetailPage();
            ReportHelper.LogTest(Status.Info, "Basic Program opened");

            // Verification of Components in screen
            ReportHelper.LogTest(Status.Pass, "Back button is visible on program detail page");
            Assert.IsTrue(programDetailPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Pass, "Menu Hamburger button is visible on program detail page");
            Assert.IsTrue(programDetailPage.GetIsProgramSwitchingScrollBarDisplayed());
            ReportHelper.LogTest(Status.Pass, "Horizontal scrollbar is visible on program detail page");
            Assert.IsTrue(programDetailPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(programDetailPage.GetIsRightHearingDeviceVisible());
            ReportHelper.LogTest(Status.Pass, "Left and Right hearing device are visible on program detail page");
            Assert.IsTrue(programDetailPage.GetIsSettingsButtonDisplayed());
            ReportHelper.LogTest(Status.Pass, "Program settings gear icon is visible");

            // Closing the App
            AppManager.CloseApp();
            ReportHelper.LogTest(Status.Info, "App Closed");

            // Restarting the App
            AppManager.RestartApp(false);
            ReportHelper.LogTest(Status.Info, "App Restarted");

            // Loading Dashboard
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            DashboardPage dashboardPageRestart = new DashboardPage();
            dashboardPageRestart.WaitUntilProgramInitFinished();
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-6531_Table-100")]
        public void ST6531_ChangeProgramNameAndIcon()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            string LeftHearingAidName = SelectHearingAid.GetLeftHearingAid();
            string RightHearingAidName = SelectHearingAid.GetRightHearingAid();

            // Load Intro and connect to Hearing Aid
            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(LeftHearingAidName, RightHearingAidName).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + LeftHearingAidName + "' and Right '" + RightHearingAidName + "'");

            // Open Noise Only Program
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            var programDetailPage = new ProgramDetailPage();
            ReportHelper.LogTest(Status.Info, "Noise Only Program opened");

            programDetailPage.OpenProgramSettings();

            string FavoriteName = "My Favorite 1";
            new ProgramDetailSettingsControlPage().CreateFavorite();
            new ProgramNamePage().EnterName(FavoriteName).Proceed();
            new ProgramIconPage().SelectIcon(24).Proceed();
            new ProgramAutomationPage().Proceed();
            ReportHelper.LogTest(Status.Info, "Favorite Program Name '" + FavoriteName + "' and Icon Noted");
            new ProgramDetailPage().TapBack();

            // Open Favorite Program
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 0);
            string CurrentFavoriteName = new ProgramDetailPage().GetCurrentProgramName();

            // Verify Created Favorite Details
            Assert.AreEqual(FavoriteName, CurrentFavoriteName, "Favorite Program Names does not match");
            ReportHelper.LogTest(Status.Info, "Favorite Program Names is created");
        }

        /// <summary>
        /// The tep step to verify the volume change on the HA is not automated
        /// In below test case we have verified on the UI
        /// </summary>
        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-11981_Table-53")]
        public void ST11981_VerifyVolumeButtons()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            //Testing with one hearing device
            var firstHearingAid = SelectHearingAid.GetRightHearingAid();
            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(firstHearingAid).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aid Right '" + firstHearingAid + "'");

            ReportHelper.LogTest(Status.Info, "Checking volume slider");
            var initialVolume = dashboardPage.GetVolumeSliderValue();
            ReportHelper.LogTest(Status.Info, "Initial volume is " + initialVolume);

            Assert.Greater(initialVolume, 0);
            Assert.Less(initialVolume, 1);
            ReportHelper.LogTest(Status.Pass, "Initial volume is niether minimum nor maximum.");

            //Increase volume from initial value
            dashboardPage.IncreaseVolume().IncreaseVolume();
            Assert.Greater(dashboardPage.GetVolumeSliderValue(), initialVolume);
            ReportHelper.LogTest(Status.Pass, "Volume is increased.");

            //Decreasing value from current volume
            dashboardPage.SetVolumeSliderValue(0);
            Assert.Less(dashboardPage.GetVolumeSliderValue(), initialVolume);
            ReportHelper.LogTest(Status.Pass, "Volume is decreased.");

            // Reset to Default Volume
            dashboardPage.SetVolumeSliderValue(0.5);
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-11438_Table-60")]
        public void ST11438_VerifyReverseSwipeFunctionality()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            //Testing with one hearing device
            var firstHearingAid = SelectHearingAid.GetLeftHearingAid();
            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(firstHearingAid).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aid Left '" + firstHearingAid + "'");

            ReportHelper.LogTest(Status.Info, "Swipe from Left to Right on Dashboard page");
            AppManager.App.SwipeLeftToRight();
            Assert.IsTrue(new MainMenuPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Pass, "Main menu is visible");

            ReportHelper.LogTest(Status.Info, "Again Swipe from Left to Right");
            AppManager.App.SwipeLeftToRight();

            Assert.IsTrue(new MainMenuPage().IsCurrentlyShown());
            ReportHelper.LogTest(Status.Pass, "App shows no reaction Main menu is still visible.");

            ReportHelper.LogTest(Status.Info, "Swipe from Right to Left on Main menu page");
            AppManager.App.SwipeRightToLeft();

            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));
            Assert.IsTrue(new DashboardPage().IsCurrentlyShown());
            ReportHelper.LogTest(Status.Pass, "Main menu is closed and Dashboard page is visible.");

            ReportHelper.LogTest(Status.Info, "Again Swipe from Right to Left");
            AppManager.App.SwipeRightToLeft();
            Assert.IsTrue(new DashboardPage().IsCurrentlyShown());
            ReportHelper.LogTest(Status.Pass, "App shows no reaction Dashboard page is still visible.");

            ReportHelper.LogTest(Status.Info, "Checking volume slider.");
            dashboardPage = new DashboardPage();

            //Reduce volume to min
            dashboardPage.DecreaseVolume();
            dashboardPage.SetVolumeSliderValue(0).DecreaseVolume();
            Assert.AreEqual(0, dashboardPage.GetVolumeSliderValue());
            Console.WriteLine(dashboardPage.GetVolumeSliderValue());
            Thread.Sleep(1000);

            //Move the slider in one motion to set volume to max
            dashboardPage.SetVolumeSliderValue(1);
            Assert.AreEqual(1, dashboardPage.GetVolumeSliderValue());
            ReportHelper.LogTest(Status.Pass, "Main Menu dosent open, Dashboard page is still visible.");

            // Reset to Default Volume
            dashboardPage.SetVolumeSliderValue(0.5);

            NavigationHelper.NavigateToHelpMenu(dashboardPage);
            Assert.IsTrue(new HelpMenuPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Pass, "Help menu is visible.");

            for (int i = 0; i <= 5; i++)
            {
                AppManager.App.SwipeTopLeftToBottomRight();
                Assert.IsTrue(new HelpMenuPage().IsCurrentlyShown());

                AppManager.App.SwipeBottomRightToTopLeft();
                Assert.IsTrue(new HelpMenuPage().IsCurrentlyShown());
            }
            ReportHelper.LogTest(Status.Pass, "Diagonal swipe is performed and app does not show any reaction");

            ReportHelper.LogTest(Status.Info, "Swipe from Left to Right on Help Menu page");
            AppManager.App.SwipeLeftToRight();
            Assert.IsTrue(new MainMenuPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Pass, "Main menu is visible.");

            var mainMenuPage = new MainMenuPage();
            var menuItemsList = mainMenuPage.MenuItems.GetAllVisible();
            for (int i = 0; i < menuItemsList.Count; i++)
            {
                mainMenuPage.MenuItems.Open(i, IndexType.Relative);
                Assert.IsTrue(mainMenuPage.IsGoneBeforeTimeout(TimeSpan.FromSeconds(5)));
                AppManager.App.SwipeLeftToRight();
                Assert.IsTrue(mainMenuPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            }
            ReportHelper.LogTest(Status.Pass, "Back navigation by swipe is verified");

            ReportHelper.LogTest(Status.Info, "Perform a short horizontal swipe on Language menu item.");
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().SwipeOnMenuItemLanguage();
            ReportHelper.LogTest(Status.Pass, "Back navigation by swipe is verified");
        }

        #endregion Sprint 10

        #endregion Test Cases
    }
}