using System;
using System.Diagnostics;
using System.Threading;
using HorusUITest.Helper;
using HorusUITest.PageObjects;
using HorusUITest.PageObjects.Controls.ProgramDetailParams;
using HorusUITest.PageObjects.Menu;
using HorusUITest.PageObjects.Menu.Help;
using HorusUITest.PageObjects.Menu.Programs;
using HorusUITest.PageObjects.Start;
using HorusUITest.PageObjects.Start.Intro;
using NUnit.Framework;
using OpenQA.Selenium;
using Platform = HorusUITest.Enums.Platform;

namespace HorusUITest.TestFixtures
{
    public class Experiments : BaseResettingTestFixture
    {
        public Experiments(Platform platform)
            : base(platform)
        {
        }

        [Test]
        [Category("Experiment")]
        public void CreateInstance()
        {
            IntroPageTwo intro2 = PageHelper.CreateInstance<IntroPageTwo>(false);
            IntroPageOne intro1 = PageHelper.CreateInstance<IntroPageOne>();
            intro1.MoveRightBySwiping();
            intro2.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3));
            intro2.MoveRightBySwiping();
        }

        [Test]
        [Category("Experiment")]
        public void PageHelperTests()
        {
            IntroPageTwo intro2 = PageHelper.CreateInstance<IntroPageTwo>(false);
            IntroPageOne intro1 = PageHelper.CreateInstance<IntroPageOne>();
            intro1.MoveRightBySwiping();
            intro2.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3));
            intro2.MoveRightBySwiping();

            var page = PageHelper.WaitForAnyPage(typeof(DashboardPage), typeof(IntroPageOne), typeof(HelpMenuPage), typeof(IntroPageFour));
            Debug.WriteLine(page.GetType());
            Debug.WriteLine(page.GetType().ToString());
            switch (page)
            {
                case DashboardPage _:
                    Debug.WriteLine("Dashboard");
                    break;
                case IntroPageOne _:
                    Debug.WriteLine("IntroPageOne");
                    break;
                case HelpMenuPage _:
                    Debug.WriteLine("HelpMenuPage");
                    break;
                case IntroPageThree _:
                    Debug.WriteLine("IntroPageThree");
                    break;
            }
        }

        [Test]
        [Category("Experiment")]
        public void FindDevicesPage()
        {
            LaunchHelper.StartAppInDemoModeByAnyMeans().Page.OpenMenuUsingSwipe();
            new MainMenuPage().OpenHelp();
            var page = new HelpMenuPage();
            page.OpenFindHearingDevices();
            PermissionHelper.AllowPermissionIfRequested();
            new FindDevicesPage().TapBack();
            Assert.IsTrue(page.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));
        }

        [Test]
        [Category("Experiment")]
        public void VolumeControlSwipe()
        {
            var page = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;

            var initialVolume = page.GetVolumeSliderValue();

            page.SetVolumeSliderValue(.075);
            page.SetVolumeSliderValue(.075);
            page.SetVolumeSliderValue(.075);
            page.SetVolumeSliderValue(.075);

            page.SetVolumeSliderValue(0);
            page.SetVolumeSliderValue(.5);
            page.SetVolumeSliderValue(.1);
            page.SetVolumeSliderValue(.9);
            page.SetVolumeSliderValue(.4);
            page.SetVolumeSliderValue(.7);
            page.SetVolumeSliderValue(.5);
            page.SetVolumeSliderValue(.0);
            page.SetVolumeSliderValue(1);

            page.SetVolumeSliderValue(0);
            page.SetVolumeSliderValue(.5);
            page.SetVolumeSliderValue(.1);
            page.SetVolumeSliderValue(.9);
            page.SetVolumeSliderValue(.4);
            page.SetVolumeSliderValue(.7);
            page.SetVolumeSliderValue(.5);
            page.SetVolumeSliderValue(.0);
            page.SetVolumeSliderValue(1);
        }

        [Test]
        [Category("Experiment")]
        public void ScanForDevices()
        {
            SelectHearingAidsPage page = null;
            var firstPage = PageHelper.WaitForAnyPage(typeof(IntroPageOne), typeof(InitializeHardwarePage));
            switch (firstPage)
            {
                case IntroPageOne _:
                    page = LaunchHelper.SkipToSelectHearingAidsPage().Page;
                    break;
                case InitializeHardwarePage x:
                    x.StartScan();
                    page = new SelectHearingAidsPage();
                    break;
            }

            bool isScanning = page.GetIsScanning();
            Assert.IsTrue(isScanning);

            page.WaitUntilDeviceFound();
            var devices1 = page.GetAllDeviceNamesAndStatuses();
            page.WaitUntilDevicesFound(1);
            page.WaitUntilDeviceFound("Devel HA#888888").WaitUntilDeviceFound("Devel HA#999999");
            page.WaitUntilDevicesFound(2);

            Wait.UntilTrue(() => page.GetSelectedDeviceNames().Count == 2, TimeSpan.FromSeconds(3));

            page.DeselectDevice(1);
            var selected = page.GetSelectedDeviceNames();
            Assert.AreEqual(1, selected.Count);
            page.DeselectDevice(selected[0], false);

            page.SelectDevice(0);

            var devices = page.GetAllDeviceNamesAndStatuses();
            Assert.IsTrue(devices[0].Selected);
            Assert.IsTrue(devices[1].Selected);

            page.WaitUntilScanFinished();

            foreach (var item in page.GetSelectedDeviceNames())
            {
                page.DeselectDevice(item, true);
            }

            Assert.IsTrue(page.GetIsScanning());
        }

        [Test]
        [Category("Experiment")]
        public void HaInitPage()
        {
            SelectHearingAidsPage selectPage = null;
            var firstPage = PageHelper.WaitForAnyPage(typeof(IntroPageOne), typeof(InitializeHardwarePage));
            switch (firstPage)
            {
                case IntroPageOne _:
                    selectPage = LaunchHelper.SkipToSelectHearingAidsPage().Page;
                    break;
                case InitializeHardwarePage x:
                    x.StartScan();
                    selectPage = new SelectHearingAidsPage();
                    break;
            }

            selectPage.WaitUntilDevicesFound(2);
            Wait.UntilTrue(() => selectPage.GetSelectedDeviceNames().Count == 2, TimeSpan.FromSeconds(15));
            selectPage.Connect();

            var page = new HearingAidInitPage();

            page.WaitForConnection();

            new DashboardPage();
        }

        [Test]
        [Category("Experiment")]
        public void HaInitErrorPage()
        {
            var selectPage = LaunchHelper.SkipToSelectHearingAidsPage().Page;
            selectPage.WaitUntilDevicesFound(2);
            Wait.UntilTrue(() => selectPage.GetSelectedDeviceNames().Count == 2, TimeSpan.FromSeconds(15));
            selectPage.Connect();

            var initPage = new HearingAidInitPage();
            Wait.UntilTrue(() =>
            {
                return initPage.LeftHearingAid.IsConnected || initPage.RightHearingAid.IsConnected;
            }, TimeSpan.FromSeconds(45));
            initPage.CancelAndConfirm();

            var page = new HearingAidConnectionErrorPage();
            page.StartDemoMode();
        }

        [Test]
        [Category("Experiment")]
        public void DevStuff()
        {
            LaunchHelper.StartAppInDemoModeByAnyMeans().Page.OpenMenuUsingSwipe();
            new MainMenuPage().OpenSettings();
            var page = new SettingsMenuPage();
            Assert.IsFalse(page.GetIsDevelopmentStuffVisible());
            page.ShowDevelopmentStuff();
            Assert.IsTrue(page.GetIsDevelopmentStuffVisible());
            page.HideDevelopmentStuff();
            Assert.IsFalse(page.GetIsDevelopmentStuffVisible());

            string logsText = page.GetLogsText();
            string resetText = page.GetAppResetText();
            //string errorPagetext = page.GetErrorPageText();

            Assert.IsNotEmpty(logsText);
            Assert.IsNotEmpty(resetText);
            //Assert.IsNotEmpty(errorPagetext);
        }

        [Test]
        [Category("Experiment")]
        public void JustGetMeToTheDashboard()
        {
            var page = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
        }

        [Test]
        [Category("Experiment")]
        public void FreezeOnProgramDetailPage()
        {
            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;

            //Test opening and closing main menu using swipe
            Output.TestStep("Opening and closing main menu using swipe");
            dashboardPage.OpenMenuUsingSwipe();
            new MainMenuPage().CloseMenuUsingSwipe();
            Assert.IsTrue(dashboardPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), $"Unsuccessful back navigation using swipe on {nameof(MainMenuPage)}.");

            //Test opening and closing main menu using tap
            Output.TestStep("Opening and closing main menu using tap");
            dashboardPage.OpenMenuUsingTap();
            new MainMenuPage().CloseMenuUsingTap();
            Assert.IsTrue(dashboardPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), $"Unsuccessful back navigation using tap on {nameof(MainMenuPage)}.");

            string initialProgramName = dashboardPage.GetCurrentProgramName();

            //Navigating to programs menu
            dashboardPage.OpenMenuUsingTap();
            new MainMenuPage().OpenPrograms();
            var programPage = new ProgramsMenuPage();

            //Testing programs menu
            Output.TestStep("Testing 'Programs'");
            Assert.AreEqual(programPage.GetNumberOfMainPrograms(), 3, "The number of programs in the 'Preset' category is expected to be 3 in demo mode.");
            Assert.AreEqual(programPage.GetNumberOfStreamingPrograms(), 1, "The number of programs in the 'Streaming' category is expected to be 1 in demo mode.");
            StringAssert.AreEqualIgnoringCase(initialProgramName, programPage.GetMainProgramName(0), "Mismatch between initial program name and first program name in the 'Preset' category.");
            var secondProgramName = programPage.GetMainProgramName(1);
            programPage.SelectMainProgram(1);
            Assert.IsTrue(dashboardPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)), "Selecting a hearing program from the menu didn't navigate to the DashboardPage.");
            StringAssert.AreEqualIgnoringCase(secondProgramName, dashboardPage.GetCurrentProgramName(), "Mismatch between previously selected program name and current program name.");

            //Testing Find Hearing Devices
            Output.TestStep("Testing help menu items:");
            dashboardPage.OpenMenuUsingSwipe();
            new MainMenuPage().OpenHelp();
            var helpPage = new HelpMenuPage();
            Output.TestStep("Testing 'Hearing Devices Find'", 2);
            helpPage.OpenFindHearingDevices();
            PermissionHelper.AllowPermissionIfRequested();
            var findPage = new FindDevicesPage();
            Assert.IsTrue(findPage.GetIsMapViewSelected(), "Map view was not pre-selected.");
            Assert.IsTrue(findPage.GetIsLeftDeviceSelected(), "Left devices was not pre-selected.");
            findPage.SelectRightDevice();
            Assert.IsTrue(findPage.GetIsRightDeviceSelected(), "Couldn't switch to right device.");
            findPage.SelectNearFieldView();
            Assert.IsTrue(findPage.GetIsNearFieldViewSelected(), "Couldn't switch to near-field view.");
            findPage.TapBack();
            new HelpMenuPage().SwipeBack();
            new MainMenuPage().CloseMenuUsingSwipe();
            dashboardPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3));

            //Changing the volume
            Output.TestStep("Changing the volume using the slider", 2);
            dashboardPage.SetVolumeSliderValue(.25);
            Assert.AreEqual(.25, dashboardPage.GetVolumeSliderValue(), delta: .10, message: $"Unable to decrease the volume using the volume slider on {nameof(DashboardPage)}.");
            dashboardPage.SetVolumeSliderValue(.75);
            Assert.AreEqual(.75, dashboardPage.GetVolumeSliderValue(), delta: .10, message: $"Unable to increase the volume using the volume slider on {nameof(DashboardPage)}.");
            Output.TestStep("Changing the volume using the buttons", 2);
            double volume = dashboardPage.GetVolumeSliderValue();
            dashboardPage.DecreaseVolume();
            Assert.Less(dashboardPage.GetVolumeSliderValue(), volume, $"Unable to decrease the volume using the buttons on {nameof(DashboardPage)}.");
            dashboardPage.IncreaseVolume();
            Assert.AreEqual(volume, dashboardPage.GetVolumeSliderValue(), $"Unable to increase the volume using the buttons on {nameof(DashboardPage)}.");

            //Checking the number of hearing programs
            Output.TestStep("Checking number of hearing programs", 2);
            Assert.AreEqual(4, dashboardPage.GetNumberOfPrograms(), "Unexpected number of hearing programs in demo mode.");

            //Changing the hearing program
            Output.TestStep("Changing the hearing program and verifying that each program has a unique name", 2);
            string lastName = null;
            for (int i = 0; i < 4; i++)
            {
                dashboardPage.SelectProgram(i);
                Assert.IsTrue(dashboardPage.GetIsProgramSelected(i), $"Failed to select a hearing program {i}.");
                string newName = dashboardPage.GetCurrentProgramName();
                Assert.AreNotEqual(lastName, newName, "Duplicate hearing program names found.");
                lastName = newName;
            }

            //Testing program detail page
            Output.TestStep("Testing the streaming program details:");
            dashboardPage.OpenCurrentProgram();
            var detailPage = new ProgramDetailPage();

            //Streaming slider
            detailPage.SelectProgram(3);
            Output.TestStep("Editing the streaming settings", 2);
            Assert.IsTrue(detailPage.GetIsStreamingDisplayVisible(), "Expected the streaming hearing program to be active, but no streaming settings were found.");
            detailPage.StreamingDisplay.OpenSettings();
            var streamingPage = new ProgramDetailParamEditStreamingPage();
            streamingPage.SetStreamingSliderValue(0.75);
            Assert.AreEqual(.75, streamingPage.GetStreamingSliderValue(), delta: .10, message: "Failed to swipe the streaming slider to the target value.");
            streamingPage.Close();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), $"Unsuccessful navigation using close button on {nameof(ProgramDetailParamEditStreamingPage)}.");
            Assert.AreEqual(.75, detailPage.StreamingDisplay.GetSliderValue(), delta: .10, message: "Streaming display doesn't seem to reflect the change that's been made.");
        }

        [Test]
        [Category("Experiment")]
        public void OneDeviceExperiment()
        {
            string deviceName = "Devel HA#888888";
            LaunchHelper.SkipToSelectHearingAidsPage();
            var selectionPage = new SelectHearingAidsPage().WaitUntilDeviceFound(deviceName);
            Thread.Sleep(5000);
            selectionPage.SelectDevice(deviceName);
            foreach (var name in selectionPage.GetAllDeviceNames())
            {
                if (name != deviceName)
                {
                    selectionPage.DeselectDevice(name, false);
                }
            }
            selectionPage.Connect();
            new HearingAidInitPage().WaitForConnection();
            var page = new DashboardPage();
            Assert.IsFalse(page.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(page.GetIsRightHearingDeviceVisible());
            Assert.Throws<NoSuchElementException>(() => page.GetLeftDeviceText());
            Assert.AreEqual("R", page.GetRightDeviceText());
        }

        [Test]
        [Category("Experiment")]
        public void ParamsLoaded()
        {
            string deviceName = "Devel HA#888888";
            LaunchHelper.SkipToSelectHearingAidsPage();
            var selectionPage = new SelectHearingAidsPage().WaitUntilDeviceFound(deviceName);
            Thread.Sleep(5000);
            selectionPage.SelectDevice(deviceName);
            foreach (var name in selectionPage.GetAllDeviceNames())
            {
                if (name != deviceName)
                {
                    selectionPage.DeselectDevice(name, false);
                }
            }
            selectionPage.Connect();
            new HearingAidInitPage().WaitForConnection();
            var page = new DashboardPage();
            bool areProgramsLoadedDirectlyAfterFirstStart = page.GetIsProgramInitFinished();
            Assert.IsTrue(areProgramsLoadedDirectlyAfterFirstStart);
            AppManager.RestartApp(false);
            page = new DashboardPage();
            bool areProgramsLoadedDirectlyAfterRestart = page.GetIsProgramInitFinished();
            Assert.IsFalse(areProgramsLoadedDirectlyAfterRestart);
            page.WaitUntilProgramInitFinished();
            page.OpenCurrentProgram();
            new ProgramDetailPage();
        }

        [Test]
        [Category("Experiment")]
        public void HearingAidLaunchHelperOneDevice()
        {
            string rightDeviceName = "Devel HA#888888";
            var launchResult = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(rightDeviceName, numberOfConnectionAttempts: 2);
            var dashboardPage = launchResult.Page;
            Assert.IsFalse(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
        }

        [Test]
        [Category("Experiment")]
        public void HearingAidLaunchHelperTwoDevices()
        {
            string rightDeviceName = "Devel HA#888888";
            string leftDeviceName = "Devel HA#999999";
            var launchResult = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(leftDeviceName, rightDeviceName, 2);
            var dashboardPage = launchResult.Page;
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
        }

        [Test]
        [Category("Experiment")]
        public void ConnectionErrorPage()
        {
            var page = new HearingAidConnectionErrorPage();
            page.RetryConnection();
        }

        [Test]
        [Category("Experiment")]
        public void BluetoothAndWifi()
        {
            //var page = new IntroPageOne();
            AppManager.DeviceSettings.DisableBluetooth();
            AppManager.DeviceSettings.EnableBluetooth();
            AppManager.DeviceSettings.DisableWifi();
            AppManager.DeviceSettings.EnableWifi();
        }
    }
}
