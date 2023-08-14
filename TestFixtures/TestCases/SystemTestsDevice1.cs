using HorusUITest.Helper;
using NUnit.Framework;
using Platform = HorusUITest.Enums.Platform;
using HorusUITest.PageObjects.Menu;
using HorusUITest.PageObjects.Menu.Help;
using HorusUITest.PageObjects;
using HorusUITest.PageObjects.Controls.ProgramDetailParams;
using HorusUITest.Enums;
using System.Threading;
using HorusUITest.PageObjects.Start;
using System;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Favorites;
using HorusUITest.Configuration;
using AventStack.ExtentReports;

namespace HorusUITest.TestFixtures.TestCases
{
    public class SystemTestsDevice1 : BaseResettingTestFixture
    {
        #region Constructor

        public SystemTestsDevice1(Platform platform)
            : base(platform)
        {

        }

        #endregion Constructor

        #region Test Cases

        #region Sprint 4

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-5970_Table-120")]
        public void ST5970_FindHearingAids()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

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

            // Open Help Menu
            var mainMenuPage = new MainMenuPage();
            mainMenuPage.OpenHelp();
            ReportHelper.LogTest(Status.Info, "Open Help Menu");

            // Open Find Hearing Aid Devices
            var helpMenuPage = new HelpMenuPage();
            helpMenuPage.OpenFindHearingDevices();
            Thread.Sleep(2000);
            // ToDo: This is done because first time when the App is installed the Right HA and Left HA is not shown in the map. We need to tap back and again open it to see it.
            new FindDevicesPage().TapBack();
            new HelpMenuPage().OpenFindHearingDevices();
            ReportHelper.LogTest(Status.Info, "Open Find Hearing Aid Devices");
            var findDevicesPage = new FindDevicesPage();
            Assert.IsTrue(findDevicesPage.GetIsMapViewSelected());
            Assert.IsTrue(findDevicesPage.GetIsLeftDeviceSelected());
            Assert.IsTrue(findDevicesPage.GetIsHearingAidVisibleOnMap(LeftHearingAidName, Side.Left.ToString()));

            Assert.IsNotEmpty(findDevicesPage.GetToggleViewButtonText());

            findDevicesPage.SelectRightDevice();
            Assert.IsTrue(findDevicesPage.GetIsRightDeviceSelected());
            Assert.IsTrue(findDevicesPage.GetIsHearingAidVisibleOnMap(RightHearingAidName, Side.Right.ToString()));

            ReportHelper.LogTest(Status.Pass, "Left and Right device are visible correctly on map view.");

            AppManager.DeviceSettings.DisableWifi();
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-6077_Table-117")]
        public void ST6077_CreatePairingWizard()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            // Load the App in Demo Mode
            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

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

            // Closing the App
            AppManager.CloseApp();
            ReportHelper.LogTest(Status.Info, "App Closed");

            // Restarting the App
            AppManager.RestartApp(true);
            ReportHelper.LogTest(Status.Info, "App Restarted");

            // Getting the Hearing Aid Name
            string LeftHearingAidName = SelectHearingAid.GetLeftHearingAid();
            string RightHearingAidName = SelectHearingAid.GetRightHearingAid();

            // Load Intro and connect to Hearing Aid
            var dashboardPageSecondLoad = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(LeftHearingAidName, RightHearingAidName).Page;
            dashboardPageSecondLoad.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPageSecondLoad.IsCurrentlyShown());
            dashboardPageSecondLoad.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + LeftHearingAidName + "' and Right '" + RightHearingAidName + "'");

            ReportHelper.LogTest(Status.Info, "Dashboard Loaded");
        }

        #endregion Sprint 4

        #region Sprint 5

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-6322_Table-114")]
        public void ST6322_ChangeVolumeControlMonaural()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            string LeftHearingAidName = SelectHearingAid.GetLeftHearingAid();

            // Load Intro and connect to Hearing Aid
            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(LeftHearingAidName).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aid Left '" + LeftHearingAidName + "'");

            // Select and open First Program
            dashboardPage.SelectProgram(0);
            dashboardPage.OpenCurrentProgram();
            ReportHelper.LogTest(Status.Info, "First program selected and Opened");

            var programDetailPage = new ProgramDetailPage();

            // Check the Program volume control
            CheckProgramVolumeControl();
            ReportHelper.LogTest(Status.Info, "Checked program volume control");

            ReportHelper.LogTest(Status.Info, "Minimum volume set");

            // Mute the Hearing Aid
            programDetailPage.DecreaseVolume();
            ReportHelper.LogTest(Status.Info, "Muted the volume");

            // Un Mute the Hearing Aid
            programDetailPage.IncreaseVolume();
            ReportHelper.LogTest(Status.Info, "Unmuted the volume");

            // Setting the volume to max
            for (int i = 0; i < 6; i++)
            {
                programDetailPage.IncreaseVolume();
            }
            ReportHelper.LogTest(Status.Info, "Volume set to max");

            // Setting the volume back to Zero
            for (int i = 0; i < 6; i++)
            {
                programDetailPage.DecreaseVolume();
            }
            ReportHelper.LogTest(Status.Info, "Volume set to zero");

            void CheckProgramVolumeControl()
            {
                // First the Program volume is set to middle
                programDetailPage.SetVolumeSliderValue(0.5);
                var singleVolume50Program = programDetailPage.GetVolumeSliderValue();
                Assert.AreEqual(0.5, singleVolume50Program);

                // The Program volume is set to miminum
                programDetailPage.SetVolumeSliderValue(0.0);
                var singleVolume0Program = programDetailPage.GetVolumeSliderValue();
                Assert.AreEqual(0, singleVolume0Program);
                ReportHelper.LogTest(Status.Info, "Program volume set to minimum");

                // Program volume is muted
                programDetailPage.DecreaseVolume();
                ReportHelper.LogTest(Status.Info, "Program volume Muted");

                // Program volume is Unmuted
                programDetailPage.IncreaseVolume();

                // The Program volume is set to miminum
                programDetailPage.SetVolumeSliderValue(1);
                var singleVolume1Program = programDetailPage.GetVolumeSliderValue();
                Assert.AreEqual(1, singleVolume1Program);
                ReportHelper.LogTest(Status.Info, "Program volume set to Maximum");

                // Monaural or Binaural Program Volume is reset to middle based on channel type
                // Doing Reset in loop since when directly set to 0.5 to goes to 0.4. Hence doing it step by step
                for (double i = 0.9; i >= 0.5; i -= 0.1)
                {
                    programDetailPage.SetVolumeSliderValue(i);
                    Thread.Sleep(500);
                }
                singleVolume50Program = programDetailPage.GetVolumeSliderValue();
                Assert.AreEqual(0.5, singleVolume50Program);
            }
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-6358_Table-112")]
        public void ST6358_ChangeVolumeControlBinaural()
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

            // Select and open First Program
            dashboardPage.SelectProgram(0);
            dashboardPage.OpenCurrentProgram();
            ReportHelper.LogTest(Status.Info, "First program selected and Opened");

            var programDetailPage = new ProgramDetailPage();

            // Check the Program volume control
            CheckProgramVolumeControl();
            ReportHelper.LogTest(Status.Info, "Checked program volume control");

            // Open Binaural Settings
            programDetailPage.OpenBinauralSettings();

            var paramEditBinauralPage = new ProgramDetailParamEditBinauralPage();

            // Checking for Monaural
            CheckVolumeControl(VolumeChannel.Single);
            ReportHelper.LogTest(Status.Info, "Checked volume for Monaural");

            // Turn On Binaural
            paramEditBinauralPage.TurnOnBinauralSeparation();
            Assert.IsTrue(paramEditBinauralPage.GetIsBinauralSwitchChecked(), "Turn on Binaural seperation failed");
            ReportHelper.LogTest(Status.Info, "Binaural settings activated");

            // Checking for Binaural Left
            CheckVolumeControl(VolumeChannel.Left);
            ReportHelper.LogTest(Status.Info, "Checked volume for Binaural Left");

            // Checking for Binaural Right
            CheckVolumeControl(VolumeChannel.Right);
            ReportHelper.LogTest(Status.Info, "Checked volume for Binaural Right");

            // Turn off Binaural
            paramEditBinauralPage.TurnOffBinauralSeparation();
            ReportHelper.LogTest(Status.Info, "Binaural settings deactivated");

            // Checking for Monaural again
            CheckVolumeControl(VolumeChannel.Single);
            ReportHelper.LogTest(Status.Info, "Checked volume for Monaural again");

            void CheckProgramVolumeControl()
            {
                // First the Program volume is set to middle
                programDetailPage.SetVolumeSliderValue(0.5);
                var singleVolume50Program = programDetailPage.GetVolumeSliderValue();
                Assert.AreEqual(0.5, Math.Round(singleVolume50Program, 1));
                ReportHelper.LogTest(Status.Info, "Volume set to 0");

                // The Program volume is set to miminum
                programDetailPage.SetVolumeSliderValue(0.0);
                var singleVolume0Program = programDetailPage.GetVolumeSliderValue();
                Assert.AreEqual(0, Math.Round(singleVolume0Program, 0));
                ReportHelper.LogTest(Status.Info, "Volume set to minimum in ProgramDetailPage");

                // Program volume is muted
                programDetailPage.DecreaseVolume();
                ReportHelper.LogTest(Status.Info, "Muted in ProgramDetailPage");

                // Program volume is Unmuted
                programDetailPage.IncreaseVolume();
                ReportHelper.LogTest(Status.Info, "Unmuted in ProgramDetailPage");

                // The Program volume is set to miminum
                programDetailPage.SetVolumeSliderValue(1);
                var singleVolume1Program = programDetailPage.GetVolumeSliderValue();
                Assert.AreEqual(1, Math.Round(singleVolume1Program, 0));
                ReportHelper.LogTest(Status.Info, "Volume set to maximum in ProgramDetailPage");

                // Reset Program volume is set to middle
                // Doing Reset in loop since when directly set to 0.5 to goes to 0.4. Hence doing it step by step
                for (double i = 0.9; i >= 0.5; i -= 0.1)
                {
                    programDetailPage.SetVolumeSliderValue(i);
                    Thread.Sleep(500);
                }
                singleVolume50Program = programDetailPage.GetVolumeSliderValue();
                Assert.AreEqual(0.5, Math.Round(singleVolume50Program, 1));
                ReportHelper.LogTest(Status.Info, "Volume set back to 0 in ProgramDetailPage");
            }

            void CheckVolumeControl(VolumeChannel channel)
            {
                const int sliderStepCount = 21;
                double tolerance = 1f / sliderStepCount;
                //HACK: iOS swiping is less precise
                //if (OniOS) tolerance *= 2;

                // Monaural or Binaural Program Volume is set to middle based on channel type
                paramEditBinauralPage.SetVolumeSliderValue(channel, 0.5);
                var singleVolume50 = paramEditBinauralPage.GetVolumeSliderValue(channel);
                Assert.AreEqual(Math.Round(0.5, 1), Math.Round(singleVolume50, 1), tolerance);
                ReportHelper.LogTest(Status.Info, "Volume set to 0 in BinauralPage");

                // Monaural or Binaural Program Volume is set to minimum based on channel type
                paramEditBinauralPage.SetVolumeSliderValue(channel, 0.0);
                var singleVolume0 = paramEditBinauralPage.GetVolumeSliderValue(channel);
                Assert.AreEqual(0, Math.Round(singleVolume0, 0), tolerance);
                ReportHelper.LogTest(Status.Info, "Volume set to minimum in BinauralPage");

                // Monaural or Binaural Program Volume is set to maximum based on channel type
                paramEditBinauralPage.SetVolumeSliderValue(channel, 1);
                var singleVolume1 = paramEditBinauralPage.GetVolumeSliderValue(channel);
                Assert.AreEqual(1, Math.Round(singleVolume1, 0), tolerance);
                ReportHelper.LogTest(Status.Info, "Volume set to maximum in BinauralPage");

                // Monaural or Binaural Program Volume is reset to middle based on channel type
                // Doing Reset in loop since when directly set to 0.5 to goes to 0.4. Hence doing it step by step
                for (double i = 0.9; i >= 0.5; i -= 0.1)
                {
                    paramEditBinauralPage.SetVolumeSliderValue(channel, i);
                    Thread.Sleep(500);
                }
                singleVolume50 = paramEditBinauralPage.GetVolumeSliderValue(channel);
                Assert.AreEqual(Math.Round(0.5, 1), Math.Round(singleVolume50, 1), tolerance);
                ReportHelper.LogTest(Status.Info, "Volume set back to 0 in BinauralPage");
            }
        }

        #endregion Sprint 5

        #region Sprint 7

        [Test]
        [Category("SystemTestsDeviceSpecificConfig")]
        [Description("TC-10673_Table-71")]
        public void ST10673_SpeechNoiseTinnitusStreaming()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            // Need to configure Risa R Hearing Aid and Enable Noise Only from Audifit 5
            string LeftHearingAidName = SelectHearingAid.GetLeftHearingAid(LeftHearingAid.Microgenisis_Risa_R_Left_068821);
            string RightHearingAidName = SelectHearingAid.GetRightHearingAid(RightHearingAid.Microgenisis_Risa_R_Right_068818);

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

            // Speech Focus opened and closed
            Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible(), "Speech Focus not visible");
            programDetailPage.SpeechFocusDisplay.OpenSettings();
            var programDetailParamEditSpeechFocusPage = new ProgramDetailParamEditSpeechFocusPage();
            var speechFocus = programDetailParamEditSpeechFocusPage.GetSelectedSpeechFocus();
            ReportHelper.LogTest(Status.Info, "Speech Focus Type Noted: " + speechFocus.ToString());
            programDetailParamEditSpeechFocusPage.Close();
            ReportHelper.LogTest(Status.Info, "Speech Focus opened and closed");

            // Noise Reduction opened and closed
            programDetailPage.NoiseReductionDisplay.OpenSettings();
            var ProgramDetailParamEditNoiseReductionPage = new ProgramDetailParamEditNoiseReductionPage();
            var noiseReduction = ProgramDetailParamEditNoiseReductionPage.GetSelectedNoiseReduction();
            ReportHelper.LogTest(Status.Info, "Noise Reduction Type Noted: " + noiseReduction.ToString());
            ProgramDetailParamEditNoiseReductionPage.Close();
            ReportHelper.LogTest(Status.Info, "Noise Reduction opened and closed");

            // Open Tinnitus Only Program
            new ProgramDetailPage().TapBack();
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            var programDetailPageTinnitus = new ProgramDetailPage();
            ReportHelper.LogTest(Status.Info, "Tinnitus opened");

            programDetailPageTinnitus.TinnitusOnlyDisplay.OpenSettings();
            var programDetailParamEditTinnitusPage = new ProgramDetailParamEditTinnitusPage();
            bool isNoiseOn = programDetailParamEditTinnitusPage.GetIsTinnitusSwitchChecked();
            ReportHelper.LogTest(Status.Info, "Tinnitus Noise On Noted: " + isNoiseOn.ToString().ToLower());
            double tinnitusVolume = programDetailParamEditTinnitusPage.GetVolumeSliderValue();
            ReportHelper.LogTest(Status.Info, "Tinnitus Volume Noted: " + tinnitusVolume.ToString());

            double lowEqValue = programDetailParamEditTinnitusPage.GetEqualizerSliderValue(EqBand.Low);
            ReportHelper.LogTest(Status.Info, "Tinnitus EQ Low Noted: " + lowEqValue.ToString());
            double midEqValue = programDetailParamEditTinnitusPage.GetEqualizerSliderValue(EqBand.Mid);
            ReportHelper.LogTest(Status.Info, "Tinnitus EQ Medium Noted: " + midEqValue.ToString());
            double highEqValue = programDetailParamEditTinnitusPage.GetEqualizerSliderValue(EqBand.High);
            ReportHelper.LogTest(Status.Info, "Tinnitus EQ High Noted: " + highEqValue.ToString());

            // EQ Values Changed
            programDetailParamEditTinnitusPage.SetEqualizerSliderValue(EqBand.Low, 0.0);
            programDetailParamEditTinnitusPage.SetEqualizerSliderValue(EqBand.Low, 1.0);
            programDetailParamEditTinnitusPage.SetEqualizerSliderValue(EqBand.Low, 0.5);
            programDetailParamEditTinnitusPage.SetEqualizerSliderValue(EqBand.Mid, 0.0);
            programDetailParamEditTinnitusPage.SetEqualizerSliderValue(EqBand.Mid, 1.0);
            programDetailParamEditTinnitusPage.SetEqualizerSliderValue(EqBand.Mid, 0.5);
            programDetailParamEditTinnitusPage.SetEqualizerSliderValue(EqBand.High, 0.0);
            programDetailParamEditTinnitusPage.SetEqualizerSliderValue(EqBand.High, 1.0);
            programDetailParamEditTinnitusPage.SetEqualizerSliderValue(EqBand.High, 0.5);
            ReportHelper.LogTest(Status.Info, "Adjusting EQ values");
            programDetailParamEditTinnitusPage.Close();

            // Open Noise Only Program
            string ProgramNameTinnitus = new ProgramDetailPage().GetCurrentProgramName();
            ReportHelper.LogTest(Status.Info, "Noise Only Program Opened");
            ReportHelper.LogTest(Status.Info, "Program Name Noted: " + ProgramNameTinnitus);

            // Rename Noice Only Program
            programDetailPageTinnitus.OpenProgramSettings();
            string changedProgramNameTinnitus = "Tinnitus";
            new ProgramDetailSettingsControlPage().CustomizeName();
            new ProgramNamePage().EnterName(changedProgramNameTinnitus).Proceed();
            new ProgramDetailPage().OpenProgramSettings();
            new ProgramDetailSettingsControlPage().CustomizeIcon();
            new ProgramIconPage().SelectIcon(25).Proceed();
            ReportHelper.LogTest(Status.Info, "Changed Program Name and Icon Noted: " + changedProgramNameTinnitus);
            new ProgramDetailPage().TapBack();

            // Open Music Program
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 2);
            ReportHelper.LogTest(Status.Info, "Music Program Opened");

            // Name set to Music Program
            new ProgramDetailPage().OpenProgramSettings();
            string changedProgramNameMusic1 = "Music 1";
            new ProgramDetailSettingsControlPage().CustomizeName();
            new ProgramNamePage().EnterName(changedProgramNameMusic1).Proceed();
            new ProgramDetailPage().OpenProgramSettings();
            new ProgramDetailSettingsControlPage().CustomizeIcon();
            new ProgramIconPage().SelectIcon(26).Proceed();
            ReportHelper.LogTest(Status.Info, "Changed Program Name and Icon Noted: " + changedProgramNameMusic1);
            new ProgramDetailPage().TapBack();

            // Open Audio Stream Program
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Streaming, 0);
            ReportHelper.LogTest(Status.Info, "Audo Streaming Program Opened");

            // Open Settings and change Stram value
            new ProgramDetailPage().StreamingDisplay.OpenSettings();
            var streamingPage = new ProgramDetailParamEditStreamingPage();
            streamingPage.SetStreamingSliderValue(0.5);
            streamingPage.SetStreamingSliderValue(1.0);
            streamingPage.SetStreamingSliderValue(0.0);
            streamingPage.SetStreamingSliderValue(0.5);
            double audoiStreamValue = streamingPage.GetStreamingSliderValue();
            streamingPage.Close();
            ReportHelper.LogTest(Status.Info, "Changed Audio Stream Value Noted: " + audoiStreamValue);

            // Adding Favorite to Audio Streaming Program
            string stramFavoriteName = "My Favorite 1";
            new ProgramDetailPage().OpenProgramSettings();
            new ProgramDetailSettingsControlPage().CreateFavorite();
            new ProgramNamePage().EnterName(stramFavoriteName).Proceed();
            new ProgramIconPage().SelectIcon(24).Proceed();
            new ProgramAutomationPage().Proceed();
            ReportHelper.LogTest(Status.Info, "Favorite Program Name and Icon Noted: " + stramFavoriteName);
            new ProgramDetailPage().TapBack();

            // Open Music Program
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 2);
            ReportHelper.LogTest(Status.Info, "Music Program Opened");

            // Name set to Music Program
            new ProgramDetailPage().OpenProgramSettings();
            string changedProgramNameMusic2 = "Music 2";
            new ProgramDetailSettingsControlPage().CustomizeName();
            new ProgramNamePage().EnterName(changedProgramNameMusic2).Proceed();
            new ProgramDetailPage().OpenProgramSettings();
            new ProgramDetailSettingsControlPage().CustomizeIcon();
            new ProgramIconPage().SelectIcon(27).Proceed();
            ReportHelper.LogTest(Status.Info, "Changed Program Name and Icon Noted: " + changedProgramNameMusic2);
            new ProgramDetailPage().TapBack();

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

            // Checking Noted Changes
            // Check Basic Program
            dashboardPageRestart.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 0);
            ReportHelper.LogTest(Status.Info, "Verify Basic Program");

            // Check Speech Focus
            Assert.IsTrue(new ProgramDetailPage().GetIsSpeechFocusDisplayVisible(), "Speech Focus not visible");
            new ProgramDetailPage().SpeechFocusDisplay.OpenSettings();
            var programDetailParamEditSpeechFocusPageRestart = new ProgramDetailParamEditSpeechFocusPage();
            var speechFocusCurrent = programDetailParamEditSpeechFocusPageRestart.GetSelectedSpeechFocus();
            Assert.AreEqual(speechFocus, speechFocusCurrent, "Speech focus does not match");
            programDetailParamEditSpeechFocusPageRestart.Close();
            ReportHelper.LogTest(Status.Pass, "Speech Focus Verified");

            // Check Noise Reduction
            new ProgramDetailPage().NoiseReductionDisplay.OpenSettings();
            var programDetailParamEditNoiseReductionPageRestart = new ProgramDetailParamEditNoiseReductionPage();
            var noiseReductionCurrent = programDetailParamEditNoiseReductionPageRestart.GetSelectedNoiseReduction();
            Assert.AreEqual(noiseReduction, noiseReductionCurrent, "Noise Reduction does not match");
            programDetailParamEditNoiseReductionPageRestart.Close();
            ReportHelper.LogTest(Status.Pass, "Noise Reduction Verified");
            new ProgramDetailPage().TapBack();

            // Check Tinnitus
            dashboardPageRestart.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            ReportHelper.LogTest(Status.Info, "Verify Noise Only Program");

            // Checking Program Name
            string changedProgramNameTinnitusCurrent = new ProgramDetailPage().GetCurrentProgramName();
            Assert.AreEqual(changedProgramNameTinnitus, changedProgramNameTinnitusCurrent, "Program Name does not match");
            ReportHelper.LogTest(Status.Pass, "Program Name Verified");

            // Check Is Noise
            new ProgramDetailPage().TinnitusOnlyDisplay.OpenSettings();
            var programDetailParamEditTinnitusPageCurrent = new ProgramDetailParamEditTinnitusPage();
            bool isNoiseOnCurrent = programDetailParamEditTinnitusPageCurrent.GetIsTinnitusSwitchChecked();
            Assert.AreEqual(isNoiseOn, isNoiseOnCurrent, "Is Noise value does not match");
            ReportHelper.LogTest(Status.Pass, "Is Noise value Verified");

            // Check Volume
            double tinnitusVolumeCurrent = programDetailParamEditTinnitusPageCurrent.GetVolumeSliderValue();
            Assert.AreEqual(tinnitusVolume, tinnitusVolumeCurrent, "Volume value does not match");
            ReportHelper.LogTest(Status.Pass, "Volume value Verified");

            // Check Low EQ Value
            double lowEqValueCurrent = programDetailParamEditTinnitusPageCurrent.GetEqualizerSliderValue(EqBand.Low);
            Assert.AreEqual(lowEqValue, lowEqValueCurrent, "Low EQ value does not match");
            ReportHelper.LogTest(Status.Pass, "Low EQ value Verified");

            // Check Medium EQ Value
            double midEqValueCurrent = programDetailParamEditTinnitusPageCurrent.GetEqualizerSliderValue(EqBand.Mid);
            Assert.AreEqual(midEqValue, midEqValueCurrent, "Medium EQ value does not match");
            ReportHelper.LogTest(Status.Pass, "Medium EQ value Verified");

            // Check High EQ Value
            double highEqValueCurrent = programDetailParamEditTinnitusPageCurrent.GetEqualizerSliderValue(EqBand.High);
            Assert.AreEqual(highEqValue, highEqValueCurrent, "High EQ value does not match");
            ReportHelper.LogTest(Status.Pass, "High EQ value Verified");

            programDetailParamEditTinnitusPageCurrent.Close();
            new ProgramDetailPage().TapBack();

            // Open Audio Stream Program
            dashboardPageRestart.OpenProgramFromProgramsMenu(MainMenuTypes.Streaming, 0);
            ReportHelper.LogTest(Status.Info, "Verify Audo Streaming Program");

            // Check Stream Value
            new ProgramDetailPage().StreamingDisplay.OpenSettings();
            double audoiStreamValueCurrent = new ProgramDetailParamEditStreamingPage().GetStreamingSliderValue();
            Assert.AreEqual(Math.Round(audoiStreamValue, 1), Math.Round(audoiStreamValueCurrent, 1), "Stream value does not match");
            ReportHelper.LogTest(Status.Pass, "Stream value Verified");
            new ProgramDetailParamEditStreamingPage().Close();
            new ProgramDetailPage().TapBack();

            // Checking Favorite Program
            dashboardPageRestart.OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 0);
            ReportHelper.LogTest(Status.Info, "Verify Favorite Program");

            // Checking Favorite Name
            string stramFavoriteNameCurrent = new ProgramDetailPage().GetCurrentProgramName();
            Assert.AreEqual(stramFavoriteName, stramFavoriteNameCurrent, "Favorite Name value does not match");
            ReportHelper.LogTest(Status.Pass, "Favorite Name value Verified");
            new ProgramDetailPage().TapBack();

            // Checking Music Program
            dashboardPageRestart.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 2);
            ReportHelper.LogTest(Status.Info, "Verify Music Program");

            // Checking Music Program Name
            string changedProgramNameMusic2Current = new ProgramDetailPage().GetCurrentProgramName();
            Assert.AreEqual(changedProgramNameMusic2, changedProgramNameMusic2Current, "Music Program Name value does not match");
            ReportHelper.LogTest(Status.Pass, "Music Program Name value Verified");
            new ProgramDetailPage().TapBack();
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-6561_Table-97")]
        public void ST6561_CheckBluetoothActivation()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            //Disable bluetooth in order to check the error page
            AppManager.DeviceSettings.DisableBluetooth();
            LaunchHelper.SkipIntroPages();
            ReportHelper.LogTest(Status.Info, "Click on Here we go button");

            Assert.IsTrue(new HardwareErrorPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), "Assert on page failed as bluetooth error page does not appear. Please disable bluetooth before starting test");
            var hardwareErrorPage = new HardwareErrorPage();

            // Bluetooth Error Page
            Assert.IsNotEmpty(hardwareErrorPage.GetPageTitle());
            Assert.IsNotEmpty(hardwareErrorPage.GetPageMessage());
            ReportHelper.LogTest(Status.Info, "Bluetooth error page appears, user need to enable bluetooth from settings to procees further");

            // Putting App in background
            ReportHelper.LogTest(Status.Info, "Putting app to background and forground again to check behavior of app.");
            AppManager.DeviceSettings.PutAppToBackground();
            Thread.Sleep(500);

            // Putting App in background
            AppManager.DeviceSettings.GetAppInForeground();
            ReportHelper.LogTest(Status.Info, "Getting back app in forground to check the view.");
            ReportHelper.LogTest(Status.Info, "Check the view of bluetooth error page");
            Thread.Sleep(500);

            // Verify Data
            hardwareErrorPage = new HardwareErrorPage();
            Assert.IsNotEmpty(hardwareErrorPage.GetPageTitle());
            Assert.IsNotEmpty(hardwareErrorPage.GetPageMessage());
            ReportHelper.LogTest(Status.Info, "The bluetooth error page is still visible, user need to enable bluetooth from settings to procees further");

            // Enable Bluetooth
            AppManager.DeviceSettings.EnableBluetooth();
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-7903_Table-87")]
        public void ST7903_CancelConnectionAttempt()
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
        }

        #endregion Sprint 7

        #endregion Test Cases
    }
}