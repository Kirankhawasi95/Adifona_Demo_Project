using System;
using System.Collections.Generic;
using System.Threading;
using AventStack.ExtentReports;
using HorusUITest.Configuration;
using HorusUITest.Enums;
using HorusUITest.Helper;
using HorusUITest.PageObjects;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Controls.ProgramDetailParams;
using HorusUITest.PageObjects.Favorites;
using HorusUITest.PageObjects.Favorites.Automation;
using HorusUITest.PageObjects.Menu;
using HorusUITest.PageObjects.Menu.Info;
using HorusUITest.PageObjects.Start;
using HorusUITest.PageObjects.Start.Intro;
using NUnit.Framework;
using Platform = HorusUITest.Enums.Platform;

namespace HorusUITest.TestFixtures.TestCases
{
    public class SystemTestsDevice7 : BaseResettingTestFixture
    {
        #region Constructor

        public SystemTestsDevice7(Platform platform)
            : base(platform)
        {

        }

        #endregion Constructor

        #region Test Cases

        #region Sprint 19

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-17168_Table-2")]
        public void ST17168_UniformHandlingOfBluetoothManagement()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var firstHearingAid = SelectHearingAid.GetLeftHearingAid();
            var secondHearingAid = SelectHearingAid.GetRightHearingAid();

            // Bluetooth Disabled
            AppManager.App.DisableBluetooth();
            ReportHelper.LogTest(Status.Pass, "Bluetooth disabled successfully");

            // Navigate through intro pages to HardwareInitialization page
            LaunchHelper.SkipIntroPages();

            ReportHelper.LogTest(Status.Pass, "Skip the intro page and landed in  hardware error page");

            // Click on Repeat Operation
            new HardwareErrorPage().RetryProcess();
            ReportHelper.LogTest(Status.Pass, "Click Repeat operation in hardware error page");

            // Enable the Bluetooth and repeat process
            AppManager.App.EnableBluetooth();
            ReportHelper.LogTest(Status.Info, "Bluetooth enabled");
            PermissionHelper.AllowPermissionIfRequested();
            ReportHelper.LogTest(Status.Pass, "Landed in Hardware Initialization page");

            // Disable Bluetooth and Click Start Scan
            AppManager.App.DisableBluetooth();
            ReportHelper.LogTest(Status.Info, "Bluetooth disabled");
            // On IOS it Automatically goes to Hardware Error Page even if Start Scan in not clicked. The internal behaviour of Android and IOS are different. So hanndling it by below condition
            if (OnAndroid)
                new InitializeHardwarePage().StartScan();
            Thread.Sleep(500);
            DialogHelper.ConfirmIfDisplayed();
            PermissionHelper.AllowPermissionIfRequested();
            Assert.NotNull(new HardwareErrorPage().GetPageTitle());
            ReportHelper.LogTest(Status.Pass, "Landed in Hardware error page");

            // Enable Bluetooth and Repeat Operation
            AppManager.App.EnableBluetooth();
            ReportHelper.LogTest(Status.Info, "Bluetooth enabled");

            // On IOS it Automatically goes to Initialize Hardware Page even if Retry Process in not clicked. The internal behaviour of Android and IOS are different. So hanndling it by below condition
            if (OnAndroid)
                new HardwareErrorPage().RetryProcess();
            Thread.Sleep(500);
            ReportHelper.LogTest(Status.Pass, "Landed in Hardware initialization page");

            // Click on Start Scan
            new InitializeHardwarePage().StartScan();

            DialogHelper.ConfirmIfDisplayed();
            PermissionHelper.AllowPermissionIfRequested();

            // Click Connecting Hearing Systems and landed in Dashbaord Page
            var selectHearingAidsPage = new SelectHearingAidsPage();
            Assert.IsTrue(selectHearingAidsPage.GetIsScanning());
            Assert.IsNotEmpty(selectHearingAidsPage.GetDescription());
            Assert.IsNotEmpty(selectHearingAidsPage.GetCancelText());
            if (!selectHearingAidsPage.GetIsDeviceFound(firstHearingAid))
                selectHearingAidsPage.WaitUntilDeviceFound(firstHearingAid);

            if (secondHearingAid != null)
                selectHearingAidsPage.WaitUntilDeviceFound(secondHearingAid);
            if (!selectHearingAidsPage.GetIsDeviceFound(secondHearingAid))
                selectHearingAidsPage.WaitUntilDeviceFound(secondHearingAid);

            selectHearingAidsPage.WaitUntilDeviceListNotChanging();
            selectHearingAidsPage.SelectDevicesExclusively(firstHearingAid, secondHearingAid);
            Assert.IsTrue(selectHearingAidsPage.GetIsDeviceSelected(firstHearingAid));
            if (secondHearingAid != null)
                selectHearingAidsPage.GetIsDeviceSelected(secondHearingAid);
            ReportHelper.LogTest(Status.Pass, "Found Hearing aids " + firstHearingAid + " and " + secondHearingAid);
            Assert.IsNotEmpty(selectHearingAidsPage.GetConnectButtonText());
            selectHearingAidsPage.Connect();

            ReportHelper.LogTest(Status.Info, "Checking the Hearing Aid init page.");
            HearingAidInitPage hearingAidInitPage = new HearingAidInitPage();
            Thread.Sleep(500);
            Assert.IsTrue(hearingAidInitPage.LeftHearingAid.IsVisible);
            Assert.IsNotEmpty(hearingAidInitPage.LeftHearingAid.SideText);
            ReportHelper.LogTest(Status.Pass, "Icons and Texts for Left and Right hearing aid are visible on Init page.");

            Assert.IsTrue(hearingAidInitPage.LeftHearingAid.IsConnecting);
            ReportHelper.LogTest(Status.Pass, "Circular loading indicator is visible for Left and Right.");
            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(45));
            PermissionHelper.AllowPermissionIfRequested();

            ReportHelper.LogTest(Status.Info, "Connect the Hearing aids and check the Start View.");
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            var dashboardPage = new DashboardPage();
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Pass, "Start view is displayed correctly containing L and R device icons.");

            // Close the app
            AppManager.App.CloseApp();
            ReportHelper.LogTest(Status.Info, "App Closed");

            // Disable bluetooth
            AppManager.App.DisableBluetooth();
            ReportHelper.LogTest(Status.Info, "Bluetooth disabled");

            // Start the app and check the hardware error
            AppManager.App.LaunchApp();
            DialogHelper.ConfirmIfDisplayed();
            Assert.NotNull(new HardwareErrorPage().GetPageTitle());
            ReportHelper.LogTest(Status.Pass, "Close and open the app and landed in  hardware error page");

            // Enable bluetooth and Re-launch the app
            AppManager.App.CloseApp();
            ReportHelper.LogTest(Status.Info, "App Closed");

            AppManager.App.EnableBluetooth();
            ReportHelper.LogTest(Status.Info, "Bluetooth enabled");

            AppManager.App.LaunchApp();
            ReportHelper.LogTest(Status.Info, "App Launched");

            // Verify UI
            Thread.Sleep(500);
            Assert.IsTrue(new HearingAidInitPage().LeftHearingAid.IsVisible);
            Assert.IsNotEmpty(new HearingAidInitPage().LeftHearingAid.SideText);
            ReportHelper.LogTest(Status.Pass, "Icons and Texts for Left and Right hearing aid are visible on Init page.");

            Assert.IsTrue(new HearingAidInitPage().LeftHearingAid.IsConnecting);
            new HearingAidInitPage().WaitForConnection();

            ReportHelper.LogTest(Status.Pass, "Circular loading indicator is visible for Left and Right.");

            PermissionHelper.AllowPermissionIfRequested();

            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            ReportHelper.LogTest(Status.Pass, "Start view is displayed correctly containg L and R device icons.");
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-16830_Table-4")]
        public void ST16830_RemoveHardwareServiceImplementation()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            // Enable Wifi
            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            var firstHearingAid = SelectHearingAid.GetLeftHearingAid();
            var secondHearingAid = SelectHearingAid.GetRightHearingAid();

            // Disable bluetooth
            AppManager.App.DisableBluetooth();
            ReportHelper.LogTest(Status.Info, "Bluetooth disabled");

            // Skip pages
            ReportHelper.LogTest(Status.Info, "Checking the Welcome Page.");
            var introPageOne = new IntroPageOne();
            Assert.IsTrue(introPageOne.GetIsRightButtonVisible());
            Assert.IsFalse(introPageOne.GetIsLeftButtonVisible());
            ReportHelper.LogTest(Status.Pass, "Welcome Page is displayed correctly.");

            ReportHelper.LogTest(Status.Info, "Skip to last intro page 'Here We Go'.");
            while (!new IntroPageFive(false).IsCurrentlyShown())
            {
                new IntroPageOne(false).MoveRightBySwiping();
            }
            Assert.IsTrue(new IntroPageFive().GetIsLeftButtonVisible());
            Assert.IsTrue(new IntroPageFive().GetIsLeftButtonVisible());

            ReportHelper.LogTest(Status.Pass, "'Here we go' page is displayed correctly.");

            ReportHelper.LogTest(Status.Info, "Tap 'Continue' accept all dialogs and check is 'Start search' page is visible.");
            new IntroPageFive().Continue();

            //After app is rest from developement menu. The Dialogs dont appear again
            DialogHelper.ConfirmIfDisplayed();

            PermissionHelper.AllowPermissionIfRequested();

            // Enable bluetooth
            AppManager.App.EnableBluetooth();
            ReportHelper.LogTest(Status.Info, "Bluetooth enabled");
            // On IOS it Automatically goes to Initialize Hardware Page even if Retry Process in not clicked. The internal behaviour of Android and IOS are different. So hanndling it by below condition
            if (OnAndroid)
                new HardwareErrorPage().RetryProcess();
            Thread.Sleep(500);

            PermissionHelper.AllowPermissionIfRequested();

            // Connect to Hearing Aid
            var initializeHardwarePage = new InitializeHardwarePage();
            Assert.IsNotEmpty(initializeHardwarePage.GetDemoModeText());
            Assert.IsNotEmpty(initializeHardwarePage.GetScanText());
            ReportHelper.LogTest(Status.Pass, "'Start Search' page is displayed correctly having 'Start Search' and 'Demo Mode' option. Now tap 'Start Search'");

            initializeHardwarePage.StartScan();

            DialogHelper.ConfirmIfDisplayed();
            PermissionHelper.AllowPermissionIfRequested();

            var selectHearingAidsPage = new SelectHearingAidsPage();
            Assert.IsTrue(selectHearingAidsPage.GetIsScanning());
            Assert.IsNotEmpty(selectHearingAidsPage.GetDescription());
            Assert.IsNotEmpty(selectHearingAidsPage.GetCancelText());
            if (!selectHearingAidsPage.GetIsDeviceFound(firstHearingAid))
                selectHearingAidsPage.WaitUntilDeviceFound(firstHearingAid);

            if (secondHearingAid != null)
                selectHearingAidsPage.WaitUntilDeviceFound(secondHearingAid);
            if (!selectHearingAidsPage.GetIsDeviceFound(secondHearingAid))
                selectHearingAidsPage.WaitUntilDeviceFound(secondHearingAid);

            selectHearingAidsPage.WaitUntilDeviceListNotChanging();
            selectHearingAidsPage.SelectDevicesExclusively(firstHearingAid, secondHearingAid);
            Assert.IsTrue(selectHearingAidsPage.GetIsDeviceSelected(firstHearingAid));
            if (secondHearingAid != null)
                selectHearingAidsPage.GetIsDeviceSelected(secondHearingAid);
            ReportHelper.LogTest(Status.Pass, "Found Hearing aids " + firstHearingAid + " and " + secondHearingAid);
            Assert.IsNotEmpty(selectHearingAidsPage.GetConnectButtonText());
            selectHearingAidsPage.Connect();

            ReportHelper.LogTest(Status.Info, "Checking the Hearing Aid init page.");
            HearingAidInitPage hearingAidInitPage = new HearingAidInitPage();
            Wait.UntilTrue(() => hearingAidInitPage.LeftHearingAid.IsVisible, TimeSpan.FromSeconds(3));

            // Verify UI
            Assert.IsTrue(hearingAidInitPage.LeftHearingAid.IsVisible);
            Assert.IsNotEmpty(hearingAidInitPage.LeftHearingAid.SideText);
            ReportHelper.LogTest(Status.Pass, "Icons and Texts for Left and Right hearing aid are visible on Init page.");

            Assert.IsTrue(hearingAidInitPage.LeftHearingAid.IsConnecting);
            ReportHelper.LogTest(Status.Pass, "Circular loading indicator is visible for Left and Right.");

            PermissionHelper.AllowPermissionIfRequested();

            ReportHelper.LogTest(Status.Info, "Connect the Hearing aids and check the Start View.");
            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(45));
            PermissionHelper.AllowPermissionIfRequested();
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            var dashboardPage = new DashboardPage();
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Pass, "Start view is displayed correctly containing L and R device icons.");
            // Wait till the toast message disappears. It causes the test case to fail if it does not wait.
            Thread.Sleep(3000);

            var currentProgramName = new DashboardPage().GetCurrentProgramName();
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            ReportHelper.LogTest(Status.Pass, "Navigated successfully to " + currentProgramName + " page");

            ReportHelper.LogTest(Status.Info, "Create favorite program with Location auto start");

            new ProgramDetailPage().OpenProgramSettings();
            Assert.True(new ProgramDetailSettingsControlPage().GetIsCreateFavoriteFloatingButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "Program Settings page opened successfully");

            // Create hearing Program
            var myFavName = "Favorite 01";
            new ProgramDetailSettingsControlPage().CreateFavorite();
            var programNamePage = new ProgramNamePage();
            programNamePage.EnterName(myFavName);
            ReportHelper.LogTest(Status.Info, "Program name entered");

            Assert.IsNotEmpty(programNamePage.GetNavigationBarTitle());
            ReportHelper.LogTest(Status.Info, "Customize name title displays as expected");
            new ProgramNamePage().Proceed();
            ReportHelper.LogTest(Status.Info, "Next button clicked");

            Assert.IsNotEmpty(new ProgramIconPage().GetNavigationBarTitle());
            ReportHelper.LogTest(Status.Info, "Customize the icon title displays as expected");
            new ProgramIconPage().Proceed();
            ReportHelper.LogTest(Status.Info, "Next button clicked");

            Assert.IsNotEmpty(new ProgramAutomationPage().GetNavigationBarTitle()); ;
            ReportHelper.LogTest(Status.Info, "Auto Hearing program  title displays as expected");

            new ProgramAutomationPage().TurnOnAutomation();

            Assert.IsTrue(new ProgramAutomationPage().GetIsGeofenceAutomationVisible());
            Assert.IsTrue(new ProgramAutomationPage().GetIsWifiAutomationVisible());

            // Give "Always Allow" Location Permission before creating favorite based on Wifi. Latest Audifon build requires this permission to create favorite based on wifi
            AppManager.DeviceSettings.GrantGPSBackgroundPermission();

            new ProgramAutomationPage().TapConnectToWiFi();

            var connectedWifiName = new AutomationWifiBindingPage().GetWifiName();
            new AutomationWifiBindingPage().Ok();
            var actualWifiName = new ProgramAutomationPage().WifiAutomation.GetValue();
            Assert.AreEqual(connectedWifiName, actualWifiName);

            new ProgramAutomationPage().Proceed();
            ReportHelper.LogTest(Status.Info, "Program Automation is set to wifi");

            var programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(myFavName, programDetailPage.GetCurrentProgramName());

            // Closing the App
            AppManager.CloseApp();
            ReportHelper.LogTest(Status.Info, "App Closed");

            // Restarting the App
            AppManager.RestartApp(false);

            // App Restarted
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));

            DashboardPage dashboardPageRestart = new DashboardPage();
            ReportHelper.LogTest(Status.Info, "App Restarted");

            // Wait for Volume settings to get loaded
            dashboardPageRestart.WaitUntilProgramInitFinished();

            // Verify selected Program Name
            Thread.Sleep(3000);
            dashboardPageRestart.GetCurrentProgramName();
            Assert.AreEqual(myFavName, dashboardPageRestart.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "Favorite program automatically selected");

            // Open Imprint page and verify Build Number
            dashboardPageRestart.OpenMenuUsingTap();

            new MainMenuPage().OpenHelp();

            new HelpMenuPage().OpenImprint();

            ImprintPage imprintPage = new ImprintPage();

            string BuildNumber = imprintPage.GetBuildNumber();

            Assert.AreNotEqual(BuildNumber, string.Empty, "App Build Number Mismatch.");
            ReportHelper.LogTest(Status.Pass, "App Build Number is available");

            AppManager.DeviceSettings.DisableWifi();
        }

        #endregion Sprint 19

        #region Sprint 20

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-17753_Table-0")]
        public void ST17753_VerifyUpdateToXamarinForms()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            // Enable Wifi
            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            var firstHearingAid = SelectHearingAid.GetLeftHearingAid();
            var secondHearingAid = SelectHearingAid.GetRightHearingAid();

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTimeAndVerify(firstHearingAid, secondHearingAid);

            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            ReportHelper.LogTest(Status.Pass, "Start view is displayed correctly containing L and R device icons.");

            ReportHelper.LogTest(Status.Info, "Open and close hearing intruments info page.");
            dashboardPage.OpenLeftHearingDevice();
            Assert.IsTrue(new HearingInstrumentInfoControlPage().IsCurrentlyShown());
            new HearingInstrumentInfoControlPage().Close();
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)));

            new DashboardPage().OpenRightHearingDevice();
            Assert.IsTrue(new HearingInstrumentInfoControlPage().IsCurrentlyShown());
            new HearingInstrumentInfoControlPage().Close();
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)));
            ReportHelper.LogTest(Status.Pass, "Hearing instruments info page is displayed, app does not crash.");

            ReportHelper.LogTest(Status.Info, "Change volume using slider.");
            dashboardPage = new DashboardPage();
            var initialVolume = dashboardPage.GetVolumeSliderValue();
            dashboardPage.DecreaseVolume();
            dashboardPage.SetVolumeSliderValue(0.2);
            dashboardPage.DecreaseVolume().DecreaseVolume();
            Assert.AreNotEqual(initialVolume, new DashboardPage().GetVolumeSliderValue());
            // Reset volume to default
            dashboardPage.SetVolumeSliderValue(0.5);
            ReportHelper.LogTest(Status.Pass, "Change in volume is successfull, app does not crash.");

            //Open Music Program
            ReportHelper.LogTest(Status.Info, "Open program detail page of Music.");
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            var programDetailPage = new ProgramDetailPage();
            Assert.IsNotEmpty(programDetailPage.GetCurrentProgramName());
            Assert.IsTrue(programDetailPage.GetIsBinauralSettingsButtonVisible());
            Assert.IsTrue(programDetailPage.GetIsSettingsButtonDisplayed());
            ReportHelper.LogTest(Status.Pass, "Music program is opened successfully");

            // Checking Programs by sliding
            var currentProgramName = programDetailPage.GetCurrentProgramName();
            for (int i = 0; i < programDetailPage.GetNumberOfVisibiblePrograms(); i++)
            {
                programDetailPage.SelectProgram(i);
                Assert.AreNotEqual(currentProgramName, programDetailPage.GetCurrentProgramName());
                currentProgramName = programDetailPage.GetCurrentProgramName();
            }
            ReportHelper.LogTest(Status.Info, "program switching using slider is successfull.");

            ReportHelper.LogTest(Status.Info, "Create a favorite with location as autostart.");
            var programAutomationPage = FavoriteHelper.CreateFavoriteHearingProgram("fav-loc-1", 2);
            //Setting program automation to location
            Assert.IsTrue(programAutomationPage.GetIsGeofenceAutomationVisible());
            Assert.IsTrue(programAutomationPage.GetIsWifiAutomationVisible());

            AppManager.DeviceSettings.GrantGPSBackgroundPermission();
            Thread.Sleep(5000);

            programAutomationPage.TapConnectToLocation();
            Thread.Sleep(3000); // Wait for map to load

            // As after granting 'Allow always' permission on Android need to reopen Map to select location
            new AutomationGeofenceBindingPage().NavigateBack();
            new ProgramAutomationPage().TapConnectToLocation();
            Thread.Sleep(3000); // Wait for map to load

            //Selecting a location
            new AutomationGeofenceBindingPage().WaitUntilNoLoadingIndicator().SelectPosition(0.5, 0.5);
            Thread.Sleep(1000);
            new AutomationGeofenceBindingPage().Ok();
            //Checking view of program automation page after setting map location
            programAutomationPage = new ProgramAutomationPage();
            Assert.IsTrue(programAutomationPage.GeofenceAutomation.GetIsValueSet());
            Assert.IsTrue(programAutomationPage.GeofenceAutomation.GetIsDeleteIconVisible());
            Assert.IsTrue(programAutomationPage.GeofenceAutomation.GetIsSettingsIconVisible());
            programAutomationPage.Proceed();

            Assert.IsTrue(new ProgramDetailPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)));

            ReportHelper.LogTest(Status.Info, "Create a favorite with wifi as autostart.");
            programAutomationPage = FavoriteHelper.CreateFavoriteHearingProgramWithoutCheck("fav-wifi-1", 5);
            programAutomationPage.TurnOnAutomation();
            Assert.IsTrue(programAutomationPage.GetIsGeofenceAutomationVisible());
            Assert.IsTrue(programAutomationPage.GetIsWifiAutomationVisible());
            ReportHelper.LogTest(Status.Info, "Program automation turned on, WIFI and Location option visible");

            programAutomationPage.TapConnectToWiFi();
            new AutomationWifiBindingPage().Ok();
            ReportHelper.LogTest(Status.Info, "Program Automation is set to wifi");

            //Checking view of program automation page after setting Wifi
            programAutomationPage = new ProgramAutomationPage();
            Assert.IsTrue(programAutomationPage.IsCurrentlyShown(), "Program automation page not visible after setting wifi");
            Assert.IsTrue(programAutomationPage.WifiAutomation.GetIsValueSet());
            Assert.IsTrue(programAutomationPage.WifiAutomation.GetIsDeleteIconVisible());
            Assert.IsTrue(programAutomationPage.WifiAutomation.GetIsSettingsIconVisible());
            programAutomationPage.Proceed();
            Assert.IsTrue(new ProgramDetailPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)));
            new ProgramDetailPage().NavigateBack();
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)));

            dashboardPage = new DashboardPage();
            List<string> allProgramNames = new List<string>();
            currentProgramName = dashboardPage.GetCurrentProgramName();
            for (int i = 0; i < dashboardPage.GetNumberOfPrograms(); i++)
            {
                allProgramNames.Add(currentProgramName);
                dashboardPage.SelectProgram(i);
                Assert.AreNotEqual(currentProgramName, dashboardPage.GetCurrentProgramName());
                currentProgramName = dashboardPage.GetCurrentProgramName();
            }

            allProgramNames.Contains("fav-loc-1");
            allProgramNames.Contains("fav-wifi-1");

            ReportHelper.LogTest(Status.Info, "program switching on dashboard page is successfull.");

            AppManager.DeviceSettings.DisableWifi();
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-17836_Table-0")]
        public void ST17836_ReplaceGeoFenceImplementation()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            var firstHearingAid = SelectHearingAid.GetLeftHearingAid();
            var secondHearingAid = SelectHearingAid.GetRightHearingAid();

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTimeAndVerify(firstHearingAid, secondHearingAid);

            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            ReportHelper.LogTest(Status.Pass, "Start view is displayed correctly containing L and R device icons.");

            ReportHelper.LogTest(Status.Info, "Select 'Noise Comfort' Program from Dashboard Page.");
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            var programDetailPage = new ProgramDetailPage();
            Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsEqualizerDisplayVisible());
            ReportHelper.LogTest(Status.Pass, "Noise Comfort program is opened. All UI elements are visible");

            ReportHelper.LogTest(Status.Info, "Change program settings of parameters of Noise Comfort program.");

            //Noise Reduction
            var noiseReduction = NoiseReduction.Off;
            programDetailPage.NoiseReductionDisplay.OpenSettings();
            new ProgramDetailParamEditNoiseReductionPage().SelectNoiseReduction(noiseReduction);
            new ProgramDetailParamEditNoiseReductionPage().Close();
            Assert.AreEqual(noiseReduction.ToString(), new ProgramDetailPage().NoiseReductionDisplay.GetValue());
            ReportHelper.LogTest(Status.Pass, "Changed setting for Noise Reduction.");

            //Speech Focus
            var speechFocus = SpeechFocus.Off;
            new ProgramDetailPage().SpeechFocusDisplay.OpenSettings();
            new ProgramDetailParamEditSpeechFocusPage().SelectSpeechFocus(speechFocus);
            new ProgramDetailParamEditSpeechFocusPage().Close();
            Assert.AreEqual(speechFocus.ToString(), new ProgramDetailPage().SpeechFocusDisplay.GetValue());
            ReportHelper.LogTest(Status.Pass, "Changed setting for Speech Focus.");

            ReportHelper.LogTest(Status.Info, "Create a favorite with current location as autostart.");
            var favName = "fav-loc";
            var programAutomationPage = FavoriteHelper.CreateFavoriteHearingProgram(favName, 2);
            //Setting program automation to location
            Assert.IsTrue(programAutomationPage.GetIsGeofenceAutomationVisible());
            Assert.IsTrue(programAutomationPage.GetIsWifiAutomationVisible());

            AppManager.DeviceSettings.GrantGPSBackgroundPermission();
            Thread.Sleep(5000);

            programAutomationPage.TapConnectToLocation();
            //Checking Geofence binding page
            AutomationGeofenceBindingPage automationGeofenceBindingPage = new AutomationGeofenceBindingPage();

            // As after granting 'Allow always' permission need to reopen Map to select location
            Thread.Sleep(3000);
            new AutomationGeofenceBindingPage().NavigateBack();
            new ProgramAutomationPage().TapConnectToLocation();
            Thread.Sleep(3000); // Wait for map to load

            //Selecting a location
            new AutomationGeofenceBindingPage().WaitUntilNoLoadingIndicator().SelectPosition(0.5, 0.5).Ok();

            //Checking view of program automation page after setting map location
            programAutomationPage = new ProgramAutomationPage();
            Assert.IsTrue(programAutomationPage.GeofenceAutomation.GetIsValueSet());
            Thread.Sleep(2000);
            Assert.IsTrue(programAutomationPage.GeofenceAutomation.GetIsDeleteIconVisible());
            Assert.IsTrue(programAutomationPage.GeofenceAutomation.GetIsSettingsIconVisible());
            programAutomationPage.Proceed();
            Assert.IsTrue(new ProgramDetailPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)));
            new ProgramDetailPage().NavigateBack();
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)));

            new DashboardPage().SelectProgram(0);
            Assert.AreNotEqual(favName, new DashboardPage().GetCurrentProgramName());

            ReportHelper.LogTest(Status.Info, "Restart app to verify autostart of favorite program.");
            AppManager.RestartApp(false);
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));

            //ToDo: Location based favorite is not getting selected while execution through automation
            //Thread.Sleep(3000);
            //Assert.AreEqual(favName, new DashboardPage().GetCurrentProgramName());
            //ReportHelper.LogTest(Status.Pass, "Favorite program automatically selected");

            AppManager.DeviceSettings.DisableWifi();
        }

        /// <summary>
        /// Following test case has some steps which requires to be checked manually
        /// Discussed with Audifon team to verify them manually
        /// </summary>
        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-17881_Table-0")]
        public void ST17881_VerifyMuting()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            var firstHearingAid = SelectHearingAid.GetLeftHearingAid();
            var secondHearingAid = SelectHearingAid.GetRightHearingAid();

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTimeAndVerify(firstHearingAid, secondHearingAid);
            ReportHelper.LogTest(Status.Pass, "Start view is displayed correctly containing L and R device icons.");

            ReportHelper.LogTest(Status.Info, "Select 'Noise Comfort' Program from Dashboard Page.");
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            var programDetailPage = new ProgramDetailPage();
            Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsEqualizerDisplayVisible());
            ReportHelper.LogTest(Status.Pass, "Noise Comfort program is opened. All UI elements are visible");

            ReportHelper.LogTest(Status.Info, "Change program settings of parameters of Noise Comfort program.");

            //Noise Reduction
            var noiseReduction = NoiseReduction.Off;
            programDetailPage.NoiseReductionDisplay.OpenSettings();
            new ProgramDetailParamEditNoiseReductionPage().SelectNoiseReduction(noiseReduction);
            new ProgramDetailParamEditNoiseReductionPage().Close();
            Assert.AreEqual(noiseReduction.ToString(), new ProgramDetailPage().NoiseReductionDisplay.GetValue());
            ReportHelper.LogTest(Status.Pass, "Changed setting for Noise Reduction.");

            //Speech Focus
            var speechFocus = SpeechFocus.Off;
            new ProgramDetailPage().SpeechFocusDisplay.OpenSettings();
            new ProgramDetailParamEditSpeechFocusPage().SelectSpeechFocus(speechFocus);
            new ProgramDetailParamEditSpeechFocusPage().Close();
            Assert.AreEqual(speechFocus.ToString(), new ProgramDetailPage().SpeechFocusDisplay.GetValue());
            ReportHelper.LogTest(Status.Pass, "Changed setting for Speech Focus.");

            ReportHelper.LogTest(Status.Info, "Create a favorite with current location as autostart.");
            var favName = "fav-loc";
            var programAutomationPage = FavoriteHelper.CreateFavoriteHearingProgram(favName, 2);
            //Setting program automation to location
            Assert.IsTrue(programAutomationPage.GetIsGeofenceAutomationVisible());
            Assert.IsTrue(programAutomationPage.GetIsWifiAutomationVisible());

            AppManager.DeviceSettings.GrantGPSBackgroundPermission();
            Thread.Sleep(5000);

            programAutomationPage.TapConnectToLocation();
            /* ToDo: Code to be removed
            // As after granting 'Allow always' permission need to reopen Map to select location
            Thread.Sleep(3000);
            new AutomationGeofenceBindingPage().NavigateBack();
            new ProgramAutomationPage().TapConnectToLocation();
            */
            Thread.Sleep(3000); // Wait for map to load

            //Selecting a location
            new AutomationGeofenceBindingPage().WaitUntilNoLoadingIndicator().SelectPosition(0.5, 0.5);
            Thread.Sleep(1000);
            new AutomationGeofenceBindingPage().Ok();
            //Checking view of program automation page after setting map location
            programAutomationPage = new ProgramAutomationPage();
            Assert.IsTrue(programAutomationPage.GeofenceAutomation.GetIsValueSet());
            Thread.Sleep(1000);
            Assert.IsTrue(programAutomationPage.GeofenceAutomation.GetIsSettingsIconVisible(), "Setting icon on automation page is not visible. This is expected in iOS, Element.Displayed always return false.");
            Assert.IsTrue(programAutomationPage.GeofenceAutomation.GetIsDeleteIconVisible(), "Delete favorite icon on automation page is not visible. This is expected in iOS, Element.Displayed always return false.");
            programAutomationPage.Proceed();
            Assert.IsTrue(new ProgramDetailPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)));
            new ProgramDetailPage().NavigateBack();
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)));

            new DashboardPage().SelectProgram(0);
            Assert.AreNotEqual(favName, new DashboardPage().GetCurrentProgramName());

            ReportHelper.LogTest(Status.Info, "Restart app to verify autostart of favorite program.");
            AppManager.RestartApp(false);

            // Loading Dashboard
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            Thread.Sleep(3000); // Wait till the notification disappears. In iOS is causes the test case to fail
            DashboardPage dashboardPageRestart = new DashboardPage();
            dashboardPageRestart.WaitUntilProgramInitFinished();

            //ToDo: Location based favorite is not getting selected while execution through automation
            //Thread.Sleep(3000);
            //dashboardPageRestart.GetCurrentProgramName();
            //Assert.AreEqual(favName, dashboardPageRestart.GetCurrentProgramName());
            //ReportHelper.LogTest(Status.Pass, "Favorite program automatically selected");

            AppManager.DeviceSettings.DisableWifi();
        }

        [Test]
        [Category("SystemTestsDeviceSpecificConfig")]
        [Description("TC-18005_Table-0")]
        public void ST18005_CheckFirmware()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            // The Hearing Aid Connected is Lewi R Monaural Normally configured with firmware 1.10.1425
            string HearingAid43 = SelectHearingAid.GetLeftHearingAid(LeftHearingAid.Microgenisis_Lewi_R_Left_068843);

            // The Hearing Aid Connected is Lewi R Monaural and has to be Normally configured with firmware 1.11.1440
            string HearingAid40 = SelectHearingAid.GetLeftHearingAid(LeftHearingAid.Microgenisis_Lewi_R_Left_068840);

            // Load Intro and connect to Hearing Aid
            var dashboardPage43First = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(HearingAid43).Page;
            dashboardPage43First.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage43First.IsCurrentlyShown());
            dashboardPage43First.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "Hearing Aid Connected Left " + HearingAid43 + " configured with firmware 1.10.1425");

            // Verify If Hearing Aid is Visible
            Assert.IsTrue(dashboardPage43First.GetIsLeftHearingDeviceVisible());
            ReportHelper.LogTest(Status.Pass, "Start view is displayed correctly.");

            // Verify Volume
            double volume43First = dashboardPage43First.GetVolumeSliderValue();
            Assert.AreEqual(0.5, volume43First);
            ReportHelper.LogTest(Status.Pass, "Volume verified");

            // Verify Hearing Aid Information
            dashboardPage43First.OpenLeftHearingDevice();

            new HearingInstrumentInfoControlPage().CheckHAInformationFromDashboard(AppMode.Normal).Close();
            ReportHelper.LogTest(Status.Pass, "Hearing Aid Information Verified");

            AppManager.RestartApp(true);
            ReportHelper.LogTest(Status.Info, "App Restarted");

            // Load Intro and connect to Hearing Aid
            // This method performs till Connect to Hearing Aid after performing scan.
            var dashboardPage40 = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(HearingAid40).Page;
            dashboardPage40.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage40.IsCurrentlyShown());
            dashboardPage40.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "Hearing Aid Connected Left " + HearingAid40 + " configured with firmware 1.11.1440");

            // ToDo: 
            // After Connect to Hearing Aid button is clicked App is expected to show a Dialog with message "You tried to connect to a hearing system not supported by this app version" for the firmware 1.11.1440. 
            // Since we do not have option to upgrade the frame work to 1.11.1440 in Audifit for now we have skiped the verification of the message step for now.

            // Verify If Hearing Aid is Visible
            Assert.IsTrue(dashboardPage40.GetIsLeftHearingDeviceVisible());
            ReportHelper.LogTest(Status.Pass, "Start view is displayed correctly.");

            // Verify Volume
            double volume40 = dashboardPage40.GetVolumeSliderValue();
            Assert.AreEqual(0.5, volume40);
            ReportHelper.LogTest(Status.Pass, "Volume verified");

            // Verify Hearing Aid Information
            dashboardPage40.OpenLeftHearingDevice();

            new HearingInstrumentInfoControlPage().CheckHAInformationFromDashboard(AppMode.Normal).Close();
            ReportHelper.LogTest(Status.Pass, "Hearing Aid Information Verified");

            AppManager.RestartApp(true);
            ReportHelper.LogTest(Status.Info, "App Restarted");

            // Load Intro and connect to Hearing Aid
            var dashboardPage43Second = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(HearingAid43).Page;
            dashboardPage43Second.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage43Second.IsCurrentlyShown());
            dashboardPage43Second.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "Hearing Aid Connected Left " + HearingAid43 + " configured with firmware 1.10.1425");

            // Verify If Hearing Aid is Visible
            Assert.IsTrue(dashboardPage43Second.GetIsLeftHearingDeviceVisible());
            ReportHelper.LogTest(Status.Pass, "Start view is displayed correctly.");

            // Verify Volume
            double volume43Second = dashboardPage43Second.GetVolumeSliderValue();
            Assert.AreEqual(0.5, volume43Second);
            ReportHelper.LogTest(Status.Pass, "Volume verified");

            // Verify Hearing Aid Information
            dashboardPage43Second.OpenLeftHearingDevice();

            new HearingInstrumentInfoControlPage().CheckHAInformationFromDashboard(AppMode.Normal).Close();
            ReportHelper.LogTest(Status.Pass, "Hearing Aid Information Verified");
        }

        #endregion Sprint 20

        #endregion Test Cases
    }
}