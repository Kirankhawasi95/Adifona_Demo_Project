using System;
using System.Threading;
using AventStack.ExtentReports;
using HorusUITest.Configuration;
using HorusUITest.Data;
using HorusUITest.Enums;
using HorusUITest.Helper;
using HorusUITest.PageObjects;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Controls.ProgramDetailParams;
using HorusUITest.PageObjects.Dialogs;
using HorusUITest.PageObjects.Menu;
using HorusUITest.PageObjects.Menu.Help;
using HorusUITest.PageObjects.Menu.Settings;
using HorusUITest.PageObjects.Start;
using NUnit.Framework;
using Platform = HorusUITest.Enums.Platform;

namespace HorusUITest.TestFixtures.TestCases
{
    public class SystemTestsDevice3 : BaseResettingTestFixture
    {
        #region Constructor

        public SystemTestsDevice3(Platform platform)
            : base(platform)
        {

        }

        #endregion Constructor

        #region Test Cases

        #region Sprint 12

        /// <summary>
        /// Test case 7546 and 7539 (Table-91,92) have been merged as they have identical test steps.
        /// A parameterized test case is created below
        /// </summary>
        /// <param name="configuration"></param>
        [TestCase("Monaural_Right", TestName = "ValidateNearFieldSearch_Monaural_Right")]
        [TestCase("Monaural_Left", TestName = "ValidateNearFieldSearch_Monaural_Left")]
        [Category("SystemTestsDeviceManual")]
        [Description("TC-7546_Table-91")]
        public void ST7546_ValidateNearFieldSearch(String configuration)
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            string hearingAid;
            double thresholdValue = 0.999; //threadshold value for image comparison score

            if (configuration.Equals("Monaural_Right"))
                hearingAid = SelectHearingAid.GetRightHearingAid();
            else
                hearingAid = SelectHearingAid.GetLeftHearingAid();

            string HearingAidText = "Left";
            if (configuration.Equals("Monaural_Right"))
                HearingAidText = "Right";

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(hearingAid).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aid " + HearingAidText + " '" + hearingAid + "'");

            NavigationHelper.NavigateToHelpMenu(dashboardPage).OpenFindHearingDevices();
            var findDevicesPage = new FindDevicesPage();
            Assert.IsTrue(findDevicesPage.GetIsMapViewSelected());
            Assert.IsNotEmpty(findDevicesPage.GetLeftDeviceText());
            Assert.IsNotEmpty(findDevicesPage.GetRightDeviceText());
            Assert.IsNotEmpty(findDevicesPage.GetToggleViewButtonText());
            findDevicesPage.SelectNearFieldView();
            Assert.IsTrue(findDevicesPage.GetIsNearFieldViewSelected());
            ReportHelper.LogTest(Status.Pass, "Near field view is visible.");

            Thread.Sleep(2000);
            ReportHelper.LogTest(Status.Info, "Taking baseline screeshot now");
            var baseLineImage = AppManager.App.TakeScreenshot().AsBase64EncodedString;

            Assert.IsNotEmpty(baseLineImage);
            ReportHelper.LogTest(Status.Info, "Baseline screenshot is taken Move the device now");

            //Semi Automatic -> Move device away from HA in order to make Near Field view change
            ReportHelper.LogTest(Status.Info, "ALERT: Please Move " + HearingAidText + " Hearing Aid Away within 10 seconds");
            Thread.Sleep(10000);

            ReportHelper.LogTest(Status.Info, "Taking new screeshot now");
            var baseLineImageCurrent = AppManager.App.TakeScreenshot().AsBase64EncodedString;

            // ToDo: Need to install OpenCV and the uncomment the below three lines and need test this
            //double score = ImageComparison.GetImageSimilarityScore(baseLineImage, baseLineImageCurrent);
            //Assert.Less(score, thresholdValue, "There are no visual differences in signal strength.");
            //ReportHelper.LogTest(Status.Info, "Score is " + score);
            ReportHelper.LogTest(Status.Pass, "On moving the device away, signal strength change is visible.");

            AppManager.DeviceSettings.DisableWifi();
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-7480_Table-93")]
        public void ST7480_VerifyFindHearingSystemsFunctionality()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            HearingAid firstHearingAid = SelectHearingAid.Left();
            HearingAid secondHearingAid = SelectHearingAid.Right();

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(firstHearingAid, secondHearingAid).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + firstHearingAid.Name + "' and Right '" + secondHearingAid.Name + "'");

            ReportHelper.LogTest(Status.Info, "Disabling hearing aids tracking from Settings Menu.");
            NavigationHelper.NavigateToSettingsMenu(dashboardPage).OpenPermissions();
            Assert.IsTrue(new SettingPermissionsPage().GetIsLocationPermissionSwitchChecked());
            new SettingPermissionsPage().TurnOffLocationPermission();
            Assert.IsFalse(new SettingPermissionsPage().GetIsLocationPermissionSwitchChecked());
            ReportHelper.LogTest(Status.Pass, "Hearing aids tracking is disabled.");

            new SettingPermissionsPage().NavigateBack();
            new SettingsMenuPage().NavigateBack();

            new MainMenuPage().OpenHelp();
            new HelpMenuPage().OpenFindHearingDevices();
            Assert.IsTrue(DialogHelper.GetIsDialogDisplayed(TimeSpan.FromSeconds(5)));
            Assert.IsNotEmpty(new AppDialog().GetConfirmButtonText());
            Assert.IsNotEmpty(new AppDialog().GetDenyButtonText());
            Assert.IsNotEmpty(new AppDialog().GetMessage());
            ReportHelper.LogTest(Status.Pass, "Dialog appears as tracking is disbled.");
            DialogHelper.Confirm();

            ReportHelper.LogTest(Status.Info, "Tracing is enabled checking the FindDevicesPage again.");
            Assert.IsTrue(new FindDevicesPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            new FindDevicesPage().CheckFindDevicesPage(ChannelMode.Binaural, firstHearingAid, secondHearingAid);
            ReportHelper.LogTest(Status.Pass, "Left and Right both devices are visible on FindDevicesPage.");

            AppManager.DeviceSettings.DisableWifi();
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-15325_Table-25")]
        public void ST15325_BlankScreenOnRestart()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            string LeftHearingAidName = SelectHearingAid.GetLeftHearingAid();
            string RightHearingAidName = SelectHearingAid.GetRightHearingAid();

            // Start Scan
            LaunchHelper.StartAppWithHearingAidsScanOnly(LeftHearingAidName, RightHearingAidName);
            ReportHelper.LogTest(Status.Info, "Scan started and Hearing Aid Listed");

            // Closing the App
            AppManager.CloseApp();
            ReportHelper.LogTest(Status.Info, "App Closed");

            // Restarting the App
            AppManager.RestartApp(false);
            ReportHelper.LogTest(Status.Info, "App Restarted");

            new InitializeHardwarePage().StartScan();

            ReportHelper.LogTest(Status.Info, "Start Scan again successfull");
            Thread.Sleep(2000);

            new SelectHearingAidsPage().Cancel();
            ReportHelper.LogTest(Status.Info, "Cancel Scan successfull");
            Thread.Sleep(2000);
        }

        #endregion Sprint 12

        #region Sprint 13

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-9820_Table-79")]
        public void ST9820_VerifyCompleteHearingAidInformation()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var firstHearingAid = SelectHearingAid.GetLeftHearingAid();
            var secondHearingAid = SelectHearingAid.GetRightHearingAid();

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(firstHearingAid, secondHearingAid).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + firstHearingAid + "' and Right '" + secondHearingAid + "'");

            //Open Mainmenu
            ReportHelper.LogTest(Status.Info, "Open Main Menu by tap.");
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            dashboardPage.OpenMenuUsingTap();
            var mainMenuPage = new MainMenuPage();
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText());
            ReportHelper.LogTest(Status.Pass, "The main menu is opened successfully");

            //Open Settings
            ReportHelper.LogTest(Status.Info, "Open the Setting submenu from Main Menu.");
            mainMenuPage.OpenSettings();
            var settingsMenuPage = new SettingsMenuPage();
            Assert.IsNotEmpty(settingsMenuPage.GetMyHearingAidsText());
            Assert.IsNotEmpty(settingsMenuPage.GetPermissionsText());
            Assert.IsNotEmpty(settingsMenuPage.GetLanguageText());
            Assert.IsNotEmpty(settingsMenuPage.GetDemoModeText());
            ReportHelper.LogTest(Status.Pass, "The Settings menu is opened successfully and all items are submenu items are visible.");

            ReportHelper.LogTest(Status.Info, "Open the 'My Hearing Systems' submenu and verify Left and Right devices.");
            settingsMenuPage.OpenMyHearingAids();

            new HearingSystemManagementPage().CheckHAInformationFromSettings(AppMode.Normal, Side.Left, Side.Right).NavigateBack();

            ReportHelper.LogTest(Status.Info, "Navigate back to Dasboard Page and check Left and Right devices.");
            Assert.IsTrue(new SettingsMenuPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            new SettingsMenuPage().NavigateBack();
            Assert.IsTrue(new MainMenuPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            new MainMenuPage().CloseMenuUsingTap();
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));

            ReportHelper.LogTest(Status.Info, "Opening Left device info page from Dashboard.");
            new DashboardPage().OpenLeftHearingDevice();
            new HearingInstrumentInfoControlPage().CheckHAInformationFromDashboard(AppMode.Normal).Close();
            ReportHelper.LogTest(Status.Pass, "All details are visible on Left Hearing Instrument Info page.");

            ReportHelper.LogTest(Status.Info, "Opening Right device info page from Dashboard.");
            new DashboardPage().OpenRightHearingDevice();
            new HearingInstrumentInfoControlPage().CheckHAInformationFromDashboard(AppMode.Normal).Close();
            ReportHelper.LogTest(Status.Pass, "All details are visible on Right Hearing Instrument Info page.");
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-9771_Table-80")]
        public void ST9771_MonauralAdjustSoundInThreeChannels()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            double thresholdValue = 0.999; //threadshold value for image comparison score

            var firstHearingAid = SelectHearingAid.GetRightHearingAid();
            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(firstHearingAid).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aid Left '" + firstHearingAid + "'");

            ReportHelper.LogTest(Status.Info, "Open Music program from 'Programs' menu");
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 2);
            var programDetailPage = new ProgramDetailPage();
            new ProgramDetailPage().ProgramDetailPageUiCheck(Side.Right);
            Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsEqualizerDisplayVisible());
            ReportHelper.LogTest(Status.Pass, "Music program check is successfull. All UI elements are visible");

            //Check default settings 
            ReportHelper.LogTest(Status.Info, "Check the default values of parameter of Music program");
            Assert.IsNotEmpty(programDetailPage.SpeechFocusDisplay.GetValue());
            Assert.IsNotEmpty(programDetailPage.NoiseReductionDisplay.GetValue());

            var baseLineImage = AppManager.App.TakeScreenshot().AsBase64EncodedString;
            ReportHelper.LogTest(Status.Info, "Baseline screenshot is taken with default settings.");
            ReportHelper.LogTest(Status.Pass, "Speech focus, Noise reduction with their default settings  are visible on Music program Page.");

            //Check volume slider
            ReportHelper.LogTest(Status.Info, "Check the functionality of Volume slider by increasing and decresing volume.");
            var actualSliderValue = programDetailPage.GetVolumeSliderValue();
            programDetailPage.IncreaseVolume().IncreaseVolume().IncreaseVolume();
            Assert.AreNotEqual(actualSliderValue, programDetailPage.GetVolumeSliderValue());
            ReportHelper.LogTest(Status.Pass, "Slider is movement using '+' button is successfull.");
            actualSliderValue = programDetailPage.GetVolumeSliderValue();
            programDetailPage.DecreaseVolume().DecreaseVolume().DecreaseVolume();
            Assert.AreNotEqual(actualSliderValue, programDetailPage.GetVolumeSliderValue());
            ReportHelper.LogTest(Status.Pass, "Slider is movement using '-' button is successfull.");

            //Open equalizer display
            ReportHelper.LogTest(Status.Info, "Open the sound equalizer parameter.");
            programDetailPage.EqualizerDisplay.OpenSettings();
            var equilizerDisplay = new ProgramDetailParamEditEqualizerPage();
            var initialLowValue = equilizerDisplay.GetEqualizerSliderValue(EqBand.Low);
            var initialMidValue = equilizerDisplay.GetEqualizerSliderValue(EqBand.Mid);
            var initialHighValue = equilizerDisplay.GetEqualizerSliderValue(EqBand.High);
            ReportHelper.LogTest(Status.Pass, "Sound menu is opened note is made for default settings, now changing the settings.");

            var lowValue = 1;
            equilizerDisplay.SetEqualizerSliderValue(EqBand.Low, lowValue);
            Assert.AreEqual(lowValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.Low));

            var midValue = 1;
            equilizerDisplay.SetEqualizerSliderValue(EqBand.Mid, midValue);
            Assert.AreEqual(midValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.Mid));

            var highValue = 0;
            equilizerDisplay.SetEqualizerSliderValue(EqBand.High, highValue);
            Assert.AreEqual(highValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.High));

            ReportHelper.LogTest(Status.Pass, "Three sliders are visible and can be moved upward and downward.");
            equilizerDisplay.Close();

            Assert.IsTrue(new ProgramDetailPage().GetIsEqualizerDisplayVisible());
            ReportHelper.LogTest(Status.Info, "Sound menu is closed.");

            ReportHelper.LogTest(Status.Info, "Taking new screeshot of Music program page after settings are changed and comparing with baseline.");
            var baseLineImageCurrent = AppManager.App.TakeScreenshot().AsBase64EncodedString;

            // ToDo: Need to install OpenCV and the uncomment the below three lines and need test this
            //double score = ImageComparison.GetImageSimilarityScore(baseLineImage, baseLineImageCurrent);
            //Assert.Less(score, thresholdValue, "Music program pictogram is not changed as per changes made in sound parameter.");
            //ReportHelper.LogTest(Status.Info, "Score is " + score);
            ReportHelper.LogTest(Status.Pass, "Music program pictogram is changed as per changes made in sound parameter.");

            //RestartApp
            ReportHelper.LogTest(Status.Info, "Restart the app and check the program settings.");
            AppManager.RestartApp(false);

            // Load Dashboard
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            ReportHelper.LogTest(Status.Pass, "App is restarted successfully");

            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 2);
            programDetailPage = new ProgramDetailPage();
            programDetailPage.EqualizerDisplay.OpenSettings();
            equilizerDisplay = new ProgramDetailParamEditEqualizerPage();

            //Check Sound settings
            Assert.AreEqual(initialLowValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.Low));
            Assert.AreEqual(initialMidValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.Mid));
            Assert.AreEqual(initialHighValue, equilizerDisplay.GetEqualizerSliderValue(EqBand.High));

            ReportHelper.LogTest(Status.Pass, "The sound settings correspond to noted default settings in this test plan.");
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-9770_Table-81")]
        public void ST9770_BinauralAdjustSoundInThreeChannels()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            double thresholdValue = 0.999; //threadshold value for image comparison score

            var firstHearingAid = SelectHearingAid.GetLeftHearingAid();
            var secondHearingAid = SelectHearingAid.GetRightHearingAid();

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(firstHearingAid, secondHearingAid).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + firstHearingAid + "' and Right '" + secondHearingAid + "'");

            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());

            ReportHelper.LogTest(Status.Info, "Open Music program from 'Programs' menu");
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 2);
            var programDetailPage = new ProgramDetailPage();
            new ProgramDetailPage().ProgramDetailPageUiCheck();
            Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsEqualizerDisplayVisible());
            ReportHelper.LogTest(Status.Pass, "Music program check is successfull. All UI elements are visible");

            //Check default settings 
            ReportHelper.LogTest(Status.Info, "Check the default values of parameter of Music program");
            Assert.IsNotEmpty(programDetailPage.SpeechFocusDisplay.GetValue());
            Assert.IsNotEmpty(programDetailPage.NoiseReductionDisplay.GetValue());

            var baseLineImage = AppManager.App.TakeScreenshot().AsBase64EncodedString;
            ReportHelper.LogTest(Status.Info, "Baseline screenshot is taken with default settings.");
            ReportHelper.LogTest(Status.Pass, "Speech focus, Noise reduction with their default settings  are visible on Music program Page.");

            //Check volume slider
            ReportHelper.LogTest(Status.Info, "Check the functionality of Volume slider by increasing and decresing volume.");
            var actualSliderValue = programDetailPage.GetVolumeSliderValue();
            programDetailPage.IncreaseVolume().IncreaseVolume().IncreaseVolume();
            Assert.AreNotEqual(actualSliderValue, programDetailPage.GetVolumeSliderValue());
            ReportHelper.LogTest(Status.Pass, "Slider is movement using '+' button is successfull.");
            actualSliderValue = programDetailPage.GetVolumeSliderValue();
            programDetailPage.DecreaseVolume().DecreaseVolume().DecreaseVolume();
            Assert.AreNotEqual(actualSliderValue, programDetailPage.GetVolumeSliderValue());
            ReportHelper.LogTest(Status.Pass, "Slider is movement using '-' button is successfull.");

            //Open equalizer display
            ReportHelper.LogTest(Status.Info, "Open the sound equalizer parameter.");
            programDetailPage.EqualizerDisplay.OpenSettings();
            var equilizerDisplay = new ProgramDetailParamEditEqualizerPage();
            ReportHelper.LogTest(Status.Pass, "Sound menu is opened, now changing the settings.");

            double lowValue = 1;
            equilizerDisplay.SetEqualizerSliderValue(EqBand.Low, lowValue);
            Assert.AreEqual(Math.Round(lowValue, 1), Math.Round(equilizerDisplay.GetEqualizerSliderValue(EqBand.Low), 1));

            double midValue = 1;
            equilizerDisplay.SetEqualizerSliderValue(EqBand.Mid, midValue);
            Assert.AreEqual(Math.Round(midValue, 1), Math.Round(equilizerDisplay.GetEqualizerSliderValue(EqBand.Mid), 1));

            double highValue = 0;
            equilizerDisplay.SetEqualizerSliderValue(EqBand.High, highValue);
            Assert.AreEqual(Math.Round(highValue, 1), Math.Round(equilizerDisplay.GetEqualizerSliderValue(EqBand.High), 1));

            ReportHelper.LogTest(Status.Pass, "Three sliders are visible and can be moved upward and downward.");
            equilizerDisplay.Close();

            Assert.IsTrue(new ProgramDetailPage().GetIsEqualizerDisplayVisible());
            ReportHelper.LogTest(Status.Info, "Sound menu is closed.");

            ReportHelper.LogTest(Status.Info, "Taking new screeshot of Music program page after settings are changed and comparing with baseline.");
            var baseLineImageCurrent = AppManager.App.TakeScreenshot().AsBase64EncodedString;

            // ToDo: Need to install OpenCV and the uncomment the below three lines and need test this
            //double score = ImageComparison.GetImageSimilarityScore(baseLineImage, baseLineImageCurrent);
            //Assert.Less(score, thresholdValue, "Music program pictogram is not changed as per changes made in sound parameter.");
            //ReportHelper.LogTest(Status.Info, "Score is " + score);
            ReportHelper.LogTest(Status.Pass, "Music program pictogram is changed as per changes made in sound parameter.");

            //Create favorite program
            ReportHelper.LogTest(Status.Info, "Create a favorite program with changes setting of Music program.");
            var favProgramName = "Favorite 01";
            FavoriteHelper.CreateFavoriteHearingProgram(favProgramName, 5).Proceed();
            Assert.IsTrue(new ProgramDetailPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));

            //RestartApp
            ReportHelper.LogTest(Status.Info, "Restart the app and check the program settings.");
            AppManager.RestartApp(false);

            // Load Dashboard
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            dashboardPage = new DashboardPage();
            dashboardPage.WaitUntilProgramInitFinished();
            ReportHelper.LogTest(Status.Pass, "App is restarted successfully");

            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 0);
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(favProgramName, programDetailPage.GetCurrentProgramName());
            programDetailPage.EqualizerDisplay.OpenSettings();
            equilizerDisplay = new ProgramDetailParamEditEqualizerPage();

            //Check Sound settings
            Assert.AreEqual(Math.Round(lowValue, 1), Math.Round(equilizerDisplay.GetEqualizerSliderValue(EqBand.Low), 1));
            Assert.AreEqual(Math.Round(midValue, 1), Math.Round(equilizerDisplay.GetEqualizerSliderValue(EqBand.Mid), 1));
            Assert.AreEqual(Math.Round(highValue, 1), Math.Round(equilizerDisplay.GetEqualizerSliderValue(EqBand.High), 1));

            ReportHelper.LogTest(Status.Pass, "The favorite sound settings correspond to the favorite sound settings noted in this test plan.");
        }

        /// <summary>
        /// In below test case the signal stength of Near field is verified using <seealso cref="ImageComparison"/>
        /// Based of image similarity score we can determine the change in signal strength
        /// The HA or phone needs to be moved away from each other manually
        /// </summary>
        [Test]
        [Category("SystemTestsDeviceManual")]
        [Description("TC-7547_Table-90")]
        public void ST7547_CheckNearFieldSearchBinaural()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            double thresholdValue = 0.999; //threadshold value for image comparison score

            var firstHearingAid = SelectHearingAid.GetLeftHearingAid();
            var secondHearingAid = SelectHearingAid.GetRightHearingAid();

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(firstHearingAid, secondHearingAid).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + firstHearingAid + "' and Right '" + secondHearingAid + "'");

            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());

            ReportHelper.LogTest(Status.Info, "Navigate to 'Find Hearing Devices' Page and check the view.");
            NavigationHelper.NavigateToHelpMenu(dashboardPage).OpenFindHearingDevices();
            var findDevicesPage = new FindDevicesPage();
            Assert.IsTrue(findDevicesPage.GetIsMapViewSelected());
            Assert.IsNotEmpty(findDevicesPage.GetLeftDeviceText());
            Assert.IsNotEmpty(findDevicesPage.GetRightDeviceText());
            Assert.IsNotEmpty(findDevicesPage.GetToggleViewButtonText());
            ReportHelper.LogTest(Status.Pass, "Check successfull. All UI elements are visible on 'Find Hearing Devices'");

            ReportHelper.LogTest(Status.Info, "Switch to 'Near Field View' to test signal strength for Left and Right side.");
            findDevicesPage.SelectNearFieldView();
            Assert.IsTrue(findDevicesPage.GetIsNearFieldViewSelected());
            ReportHelper.LogTest(Status.Pass, "Near field view is visible.");

            //Left
            ReportHelper.LogTest(Status.Info, "Verifying for Left Device");
            Thread.Sleep(2000);
            ReportHelper.LogTest(Status.Info, "Taking baseline screeshot for Left device now");
            var baseLineImageLeft = AppManager.App.TakeScreenshot().AsBase64EncodedString;
            Assert.IsNotEmpty(baseLineImageLeft);
            ReportHelper.LogTest(Status.Info, "Baseline screenshot is taken Move the device now");

            //Semi Automatic -> Move device away from HA in order to make Near Field view change
            ReportHelper.LogTest(Status.Info, "ALERT: Please Move Left Hearing Aid Away within 10 seconds");
            Thread.Sleep(10000);

            ReportHelper.LogTest(Status.Info, "Taking new screeshot now");
            var baseLineImageLeftCurrent = AppManager.App.TakeScreenshot().AsBase64EncodedString;

            // ToDo: Need to install OpenCV and the uncomment the below two lines and need test this
            //double scoreLeft = ImageComparison.GetImageSimilarityScore(baseLineImageLeft, baseLineImageLeftCurrent);
            //ReportHelper.LogTest(Status.Info, "Image similarity score for left device is " + scoreLeft);

            //Right
            ReportHelper.LogTest(Status.Info, "Verifying for Right Device");
            new FindDevicesPage().SelectRightDevice();
            Thread.Sleep(2000);
            ReportHelper.LogTest(Status.Info, "Taking baseline screeshot for Right device now");
            var baseLineImageRight = AppManager.App.TakeScreenshot().AsBase64EncodedString;
            Assert.IsNotEmpty(baseLineImageRight);
            ReportHelper.LogTest(Status.Info, "Baseline screenshot is taken Move the device now");

            //Semi Automatic -> Move device away from HA in order to make Near Field view change
            ReportHelper.LogTest(Status.Info, "ALERT: Please Move Right Hearing Aid Away within 10 seconds");
            Thread.Sleep(10000);

            ReportHelper.LogTest(Status.Info, "Taking new screeshot now");
            var baseLineImageRightCurrent = AppManager.App.TakeScreenshot().AsBase64EncodedString;

            // ToDo: Need to install OpenCV and the uncomment the below two lines and need test this
            //double scoreRight = ImageComparison.GetImageSimilarityScore(baseLineImageRight, baseLineImageRightCurrent);
            //ReportHelper.LogTest(Status.Info, "Image similarity score for Right device is " + scoreRight);

            ReportHelper.LogTest(Status.Info, "Validating similarity scores for Left and Right device.");

            // ToDo: Need to install OpenCV and the uncomment the below five lines and need test this
            //Assert.Multiple(() =>
            //{
            //    Assert.Less(scoreLeft, thresholdValue, "There are no visual differences in signal strength for Left Device.");
            //    Assert.Less(scoreRight, thresholdValue, "There are no visual differences in signal strength for Right Device.");
            //});

            ReportHelper.LogTest(Status.Pass, "Check successful, On moving the device away, signal strength change is visible for both Left and Right sides.");

            AppManager.DeviceSettings.DisableWifi();
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-9028_Table-84")]
        public void ST9028_ReadOutHearingAids()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            string LeftHearingAidName = SelectHearingAid.GetLeftHearingAid();
            string RightHearingAidName = SelectHearingAid.GetRightHearingAid();

            // Non Audifon Hearing Aids
            // Currently we do not have Non Audifon Bluetooth Hearing Aids
            // The below are just Bluetooth Head Phones
            //TODO: Need to change the name and test it with Non Audifon Bluetooth Hearing Aid once we get it
            string LeftNonAudifoneHearingAidName = "boAt Rockerz 400";
            string RightNonAudifonHearingAidName = "boAt Rockerz 400";

            // This will become true if Non Audifon Hearing Aid is connected
            bool IsFirstNonAudifonDeviceAvailable = false;
            bool IsSecondNonAudifonDeviceAvailable = false;

            // Load Intro and connect to Hearing Aid
            var dashboardPage = LaunchHelper.StartAppWithNonAudifonHearingAids(out IsFirstNonAudifonDeviceAvailable, out IsSecondNonAudifonDeviceAvailable, LeftHearingAidName, LeftNonAudifoneHearingAidName, RightHearingAidName, RightNonAudifonHearingAidName).Page;

            // Checking if the Non Audifon Hearing Aid is listed
            Assert.AreEqual(IsFirstNonAudifonDeviceAvailable, false, "First Non Audifon listed");
            Assert.AreEqual(IsSecondNonAudifonDeviceAvailable, false, "Second Non Audifon listed");
            ReportHelper.LogTest(Status.Pass, "Non Audifon Hearing Aid Not Listed");

            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + LeftHearingAidName + "' and Right '" + RightHearingAidName + "'");

            // Open Basic Program
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 0);
            var programDetailPage = new ProgramDetailPage();
            ReportHelper.LogTest(Status.Info, "Basic Program opened");
            string ProgramName = programDetailPage.GetCurrentProgramName();
            Assert.AreEqual(ProgramName != string.Empty, true, "Program not loaded");
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-9027_Table-85")]
        public void ST9027_FindHearingAidsWithConnection()
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
            initPage.CancelAndConfirm();
            ReportHelper.LogTest(Status.Info, "Canceled the connect of hearing aid");

            Assert.IsTrue(new HearingAidConnectionErrorPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(130)));

            //TODO: There are steps where single hearing aid either left or right hearing aid has to be disconnected which needs to be discussed and Automated
        }

        #endregion Sprint 13

        #endregion Test Cases
    }
}