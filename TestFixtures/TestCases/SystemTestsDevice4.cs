using System;
using System.Collections.Generic;
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
using HorusUITest.PageObjects.Favorites;
using HorusUITest.PageObjects.Menu;
using HorusUITest.PageObjects.Menu.Programs;
using HorusUITest.PageObjects.Menu.Settings;
using HorusUITest.PageObjects.Start;
using HorusUITest.PageObjects.Start.Intro;
using NUnit.Framework;
using Platform = HorusUITest.Enums.Platform;

namespace HorusUITest.TestFixtures.TestCases
{
    public class SystemTestsDevice4 : BaseResettingTestFixture
    {
        #region Constructor

        public SystemTestsDevice4(Platform platform)
            : base(platform)
        {

        }

        #endregion Constructor

        #region Test Cases

        #region Sprint 14

        /// <summary>
        /// This test case 10560 table-77 is splitted in two parts.
        /// 1. Set Automatic settings in Basic Program in Audifit.
        /// 2. Do not set Automatic settings for Basic Program in Audifit.
        /// </summary>
        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-10560_Table-77")]
        public void ST10560_CheckAutomaticSettingsInBasicProgram()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            HearingAid firstHearingAid = SelectHearingAid.Left();
            HearingAid secondHearingAid = SelectHearingAid.Right();

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(firstHearingAid, secondHearingAid).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + firstHearingAid.Name + "' and Right '" + secondHearingAid.Name + "'");

            ReportHelper.LogTest(Status.Info, "Getting all programs names from the 'Programs' menu.");
            dashboardPage.OpenMenuUsingTap();
            Assert.IsNotEmpty(new MainMenuPage().GetProgramsText());
            new MainMenuPage().OpenPrograms();
            ReportHelper.LogTest(Status.Info, "Getting the Programs from Programs Menu");
            var programNames = GetMenuItemTexts(new ProgramsMenuPage());
            CollectionAssert.IsNotEmpty(programNames);
            string Names = string.Empty;
            foreach (var item in programNames)
            {
                Names += item + ", ";
            }
            ReportHelper.LogTest(Status.Info, "Program Name(s): " + Names.Trim().Trim(','));
            ReportHelper.LogTest(Status.Pass, "Program names collected, Navigate back to dashboard page.");

            new ProgramsMenuPage().NavigateBack();
            new MainMenuPage().CloseMenuUsingTap();
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            dashboardPage = new DashboardPage();

            ReportHelper.LogTest(Status.Info, "Check navigation of programs on Dashbboard Page.");
            Assert.AreEqual(programNames.Count, new DashboardPage().GetNumberOfPrograms());

            for (int i = 0; i < programNames.Count; i++)
            {
                dashboardPage.SelectProgram(i);
                //Assert.AreEqual(programNames[i], dashboardPage.GetCurrentProgramName());
                CollectionAssert.Contains(programNames, dashboardPage.GetCurrentProgramName());
            }
            ReportHelper.LogTest(Status.Pass, "Programs can be successfully changed on Dashboard page by program icons.");

            //Basic Program
            ReportHelper.LogTest(Status.Info, "Select 'Basic Program(Automatic)' from Dashboard Page.");
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 0);
            var programDetailPage = new ProgramDetailPage();
            new ProgramDetailPage().ProgramDetailPageUiCheck();
            Assert.IsNotEmpty(programDetailPage.AutoDisplay.GetDescription());
            ReportHelper.LogTest(Status.Pass, "Basic Program(Automatic) is selected successfully.");
            programDetailPage.OpenProgramSettings();
            Assert.IsFalse(new ProgramDetailSettingsControlPage().GetIsCreateFavoriteVisible(), "Create Favorite option should not be available but is present, Please check Audifit settings or select correct program. ");
            new ProgramDetailSettingsControlPage().NavigateBack();
            ReportHelper.LogTest(Status.Pass, "Check successful 'Create Favorite' option is not available for Basic Program(Automatic).");

            //Noise Comfort Program
            ReportHelper.LogTest(Status.Info, "Navigate to Noise Comfort program.");
            programDetailPage.SelectProgram(1);
            Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsEqualizerDisplayVisible());
            ReportHelper.LogTest(Status.Pass, "Noise Comfort program is opened. All UI elements are visible");
            ReportHelper.LogTest(Status.Info, "Change program settings of parameters of Noise Comfort program.");

            //Noise Reduction
            var noiseComfortNoiseReduction = NoiseReduction.Off;
            programDetailPage.NoiseReductionDisplay.OpenSettings();
            new ProgramDetailParamEditNoiseReductionPage().SelectNoiseReduction(noiseComfortNoiseReduction);
            new ProgramDetailParamEditNoiseReductionPage().Close();
            Assert.AreEqual(noiseComfortNoiseReduction.ToString(), new ProgramDetailPage().NoiseReductionDisplay.GetValue());
            ReportHelper.LogTest(Status.Pass, "Changed setting for Noise Reduction.");

            //Speech Focus
            var noiseComfortSpeechFocus = SpeechFocus.Off;
            new ProgramDetailPage().SpeechFocusDisplay.OpenSettings();
            new ProgramDetailParamEditSpeechFocusPage().SelectSpeechFocus(noiseComfortSpeechFocus);
            new ProgramDetailParamEditSpeechFocusPage().Close();
            Assert.AreEqual(noiseComfortSpeechFocus.ToString(), new ProgramDetailPage().SpeechFocusDisplay.GetValue());
            ReportHelper.LogTest(Status.Pass, "Changed setting for Speech Focus.");

            //Sound
            var noiseComfortequalizerBand = EqBand.Mid;
            var noiseComfortValue = 1;
            new ProgramDetailPage().EqualizerDisplay.OpenSettings();
            new ProgramDetailParamEditEqualizerPage().SetEqualizerSliderValue(noiseComfortequalizerBand, noiseComfortValue);
            new ProgramDetailParamEditEqualizerPage().Close();
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(noiseComfortValue, Math.Round(programDetailPage.EqualizerDisplay.GetEqualizerSliderValue(noiseComfortequalizerBand), 0));
            ReportHelper.LogTest(Status.Pass, "Changed setting for Equalizer.");

            programDetailPage.OpenProgramSettings();
            Assert.IsTrue(new ProgramDetailSettingsControlPage().GetIsCreateFavoriteVisible(), "Create Favorite option should be available but is not present");
            new ProgramDetailSettingsControlPage().NavigateBack();
            ReportHelper.LogTest(Status.Pass, "Check successful 'Create Favorite' option is available for Noise Reduction");

            //Music Program
            ReportHelper.LogTest(Status.Info, "Navigate to Music Program.");
            programDetailPage.SelectProgram(2);
            programDetailPage = new ProgramDetailPage();
            Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsEqualizerDisplayVisible());
            ReportHelper.LogTest(Status.Pass, "Music program is opened. All UI elements are visible");
            ReportHelper.LogTest(Status.Info, "Change program settings of parameters of Music program.");
            //Speech Focus
            var musicSpeechFocus = SpeechFocus.Front;
            new ProgramDetailPage().SpeechFocusDisplay.OpenSettings();
            new ProgramDetailParamEditSpeechFocusPage().SelectSpeechFocus(musicSpeechFocus);
            new ProgramDetailParamEditSpeechFocusPage().Close();
            Assert.AreEqual(musicSpeechFocus.ToString(), new ProgramDetailPage().SpeechFocusDisplay.GetValue());
            ReportHelper.LogTest(Status.Pass, "Changed setting for Speech Focus.");

            new ProgramDetailPage().OpenProgramSettings();
            Assert.IsTrue(new ProgramDetailSettingsControlPage().GetIsCreateFavoriteVisible(), "Create Favorite option should be available but is not present");
            new ProgramDetailSettingsControlPage().NavigateBack();
            ReportHelper.LogTest(Status.Pass, "Check successful 'Create Favorite' option is available for Music");

            //Streaming Program
            ReportHelper.LogTest(Status.Info, "Navigate to Streaming Program.");
            new ProgramDetailPage().SelectProgram(3);
            programDetailPage = new ProgramDetailPage();
            Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsEqualizerDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsStreamingDisplayVisible());
            ReportHelper.LogTest(Status.Pass, "Streaming program is opened. All UI elements are visible");

            ReportHelper.LogTest(Status.Info, "Change program settings of parameters of Streaming program.");
            var streamingValue = 1;
            programDetailPage.StreamingDisplay.OpenSettings();
            new ProgramDetailParamEditStreamingPage().SetStreamingSliderValue(streamingValue);
            new ProgramDetailParamEditStreamingPage().Close();
            Assert.AreEqual(streamingValue, new ProgramDetailPage().StreamingDisplay.GetSliderValue());
            ReportHelper.LogTest(Status.Pass, "Changed setting for Streaming.");

            new ProgramDetailPage().OpenProgramSettings();
            Assert.IsTrue(new ProgramDetailSettingsControlPage().GetIsCreateFavoriteVisible(), "Create Favorite option should be available but is not present");
            new ProgramDetailSettingsControlPage().NavigateBack();
            ReportHelper.LogTest(Status.Pass, "Check successful 'Create Favorite' option is available for Streaming");

            ReportHelper.LogTest(Status.Info, "Navigate to all Programs to verify changes again.");
            programDetailPage = new ProgramDetailPage();

            //Noise Comfort
            programDetailPage.SelectProgram(1);
            Assert.AreEqual(noiseComfortNoiseReduction.ToString(), programDetailPage.NoiseReductionDisplay.GetValue());
            Assert.AreEqual(noiseComfortSpeechFocus.ToString(), new ProgramDetailPage().SpeechFocusDisplay.GetValue());
            Assert.AreEqual(noiseComfortValue, Math.Round(programDetailPage.EqualizerDisplay.GetEqualizerSliderValue(noiseComfortequalizerBand), 0));

            //Music
            programDetailPage.SelectProgram(2);
            Assert.AreEqual(musicSpeechFocus.ToString(), new ProgramDetailPage().SpeechFocusDisplay.GetValue());

            //Streaming
            new ProgramDetailPage().SelectProgram(3);
            Assert.AreEqual(streamingValue, Math.Round(new ProgramDetailPage().StreamingDisplay.GetSliderValue(), 0));
            ReportHelper.LogTest(Status.Pass, "Check successful change in program parameters is saved.");

            List<string> GetMenuItemTexts(BaseSubMenuPage menuPage)
            {
                var menuItemsList = menuPage.MenuItems.GetAllVisible();
                return menuItemsList;
            }
        }

        /// <summary>
        /// This test case may require configution from Audit as a prerequisite.
        /// In which basic program will not have Auto settings but parameters like speech fcous, noise red. etc
        /// This test should be excluded from regression suite as it may fail with 
        /// default configuration in Audifit.
        /// </summary>
        [Test]
        [Category("SystemTestsDeviceSpecificConfig")]
        [Description("TC-10560_Table-77_1")]
        public void ST10560_CheckSettingsForBaseProgram()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            // Need to configure Risa R Hearing Aid with Input Function in Basic Program as Microphone and Enable Speech in Noise from Audifit 5
            var firstHearingAid = SelectHearingAid.GetLeftHearingAid(LeftHearingAid.Microgenisis_Risa_R_Left_068826);
            var secondHearingAid = SelectHearingAid.GetRightHearingAid(RightHearingAid.Microgenisis_Risa_R_Right_068822);

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(firstHearingAid, secondHearingAid).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + firstHearingAid + "' and Right '" + secondHearingAid + "'");

            ReportHelper.LogTest(Status.Info, "Getting all programs names from the 'Programs' menu.");
            dashboardPage.OpenMenuUsingTap();
            Assert.IsNotEmpty(new MainMenuPage().GetProgramsText());
            new MainMenuPage().OpenPrograms();
            ReportHelper.LogTest(Status.Info, "Getting the Programs from Programs Menu");
            var programNames = GetMenuItemTexts(new ProgramsMenuPage());
            CollectionAssert.IsNotEmpty(programNames);
            string Names = string.Empty;
            foreach (var item in programNames)
            {
                Names += item + ", ";
            }
            ReportHelper.LogTest(Status.Info, "Program Name(s): " + Names.Trim().Trim(','));
            ReportHelper.LogTest(Status.Pass, "Program names collected, Navigate back to dashboard page.");

            new ProgramsMenuPage().NavigateBack();
            new MainMenuPage().CloseMenuUsingTap();
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            dashboardPage = new DashboardPage();

            ReportHelper.LogTest(Status.Info, "Check navigation of programs on Dashbboard Page.");
            Assert.AreEqual(programNames.Count, new DashboardPage().GetNumberOfPrograms());

            for (int i = 0; i < programNames.Count; i++)
            {
                dashboardPage.SelectProgram(i);
                //Assert.AreEqual(programNames[i], dashboardPage.GetCurrentProgramName());
                CollectionAssert.Contains(programNames, dashboardPage.GetCurrentProgramName());
            }
            ReportHelper.LogTest(Status.Pass, "Programs can be successfully changed on Dashboard page by program icons.");

            //Basic Program
            ReportHelper.LogTest(Status.Info, "Select 'Basic Program' from Dashboard Page.");
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 0);
            var programDetailPage = new ProgramDetailPage();
            new ProgramDetailPage().ProgramDetailPageUiCheck();
            Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible(), "Speech focus not visible");
            Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsEqualizerDisplayVisible());
            ReportHelper.LogTest(Status.Pass, "Base program is opened. All UI elements are visible");
            programDetailPage.OpenProgramSettings();
            Assert.IsTrue(new ProgramDetailSettingsControlPage().GetIsCreateFavoriteVisible(), "Create Favorite option should be available but is not present, Please check Audifit settings or select correct program. ");
            new ProgramDetailSettingsControlPage().NavigateBack();
            ReportHelper.LogTest(Status.Pass, "Check successful 'Create Favorite' option is available for Basic Program.");

            //Noise Comfort Program
            ReportHelper.LogTest(Status.Info, "Navigate to Noise Comfort program.");
            programDetailPage.SelectProgram(1);
            Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsEqualizerDisplayVisible());
            ReportHelper.LogTest(Status.Pass, "Noise Comfort program is opened. All UI elements are visible");
            ReportHelper.LogTest(Status.Info, "Change program settings of parameters of Noise Comfort program.");

            //Noise Reduction
            var noiseComfortNoiseReduction = NoiseReduction.Off;
            programDetailPage.NoiseReductionDisplay.OpenSettings();
            new ProgramDetailParamEditNoiseReductionPage().SelectNoiseReduction(noiseComfortNoiseReduction);
            new ProgramDetailParamEditNoiseReductionPage().Close();
            Assert.AreEqual(noiseComfortNoiseReduction.ToString(), new ProgramDetailPage().NoiseReductionDisplay.GetValue());
            ReportHelper.LogTest(Status.Pass, "Changed setting for Noise Reduction.");

            //Speech Focus
            var noiseComfortSpeechFocus = SpeechFocus.Off;
            new ProgramDetailPage().SpeechFocusDisplay.OpenSettings();
            new ProgramDetailParamEditSpeechFocusPage().SelectSpeechFocus(noiseComfortSpeechFocus);
            new ProgramDetailParamEditSpeechFocusPage().Close();
            Assert.AreEqual(noiseComfortSpeechFocus.ToString(), new ProgramDetailPage().SpeechFocusDisplay.GetValue());
            ReportHelper.LogTest(Status.Pass, "Changed setting for Speech Focus.");

            //Sound
            var noiseComfortequalizerBand = EqBand.Mid;
            var noiseComfortValue = 1;
            new ProgramDetailPage().EqualizerDisplay.OpenSettings();
            new ProgramDetailParamEditEqualizerPage().SetEqualizerSliderValue(noiseComfortequalizerBand, noiseComfortValue);
            new ProgramDetailParamEditEqualizerPage().Close();
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(noiseComfortValue, programDetailPage.EqualizerDisplay.GetEqualizerSliderValue(noiseComfortequalizerBand));
            ReportHelper.LogTest(Status.Pass, "Changed setting for Equalizer.");

            programDetailPage.OpenProgramSettings();
            Assert.IsTrue(new ProgramDetailSettingsControlPage().GetIsCreateFavoriteVisible(), "Create Favorite option should be available but is not present");
            new ProgramDetailSettingsControlPage().NavigateBack();
            ReportHelper.LogTest(Status.Pass, "Check successful 'Create Favorite' option is available for Noise Reduction");

            //Music Program
            ReportHelper.LogTest(Status.Info, "Navigate to Music Program.");
            programDetailPage.SelectProgram(2);
            programDetailPage = new ProgramDetailPage();
            Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsEqualizerDisplayVisible());
            ReportHelper.LogTest(Status.Pass, "Music program is opened. All UI elements are visible");
            ReportHelper.LogTest(Status.Info, "Change program settings of parameters of Music program.");

            //Speech Focus
            var musicSpeechFocus = SpeechFocus.Front;
            new ProgramDetailPage().SpeechFocusDisplay.OpenSettings();
            new ProgramDetailParamEditSpeechFocusPage().SelectSpeechFocus(musicSpeechFocus);
            new ProgramDetailParamEditSpeechFocusPage().Close();
            Assert.AreEqual(musicSpeechFocus.ToString(), new ProgramDetailPage().SpeechFocusDisplay.GetValue());
            ReportHelper.LogTest(Status.Pass, "Changed setting for Speech Focus.");

            new ProgramDetailPage().OpenProgramSettings();
            Assert.IsTrue(new ProgramDetailSettingsControlPage().GetIsCreateFavoriteVisible(), "Create Favorite option should be available but is not present");
            new ProgramDetailSettingsControlPage().NavigateBack();
            ReportHelper.LogTest(Status.Pass, "Check successful 'Create Favorite' option is available for Music");

            //Streaming Program
            ReportHelper.LogTest(Status.Info, "Navigate to Streaming Program.");
            new ProgramDetailPage().SelectProgram(3);
            programDetailPage = new ProgramDetailPage();
            Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsEqualizerDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsStreamingDisplayVisible());
            ReportHelper.LogTest(Status.Pass, "Streaming program is opened. All UI elements are visible");

            ReportHelper.LogTest(Status.Info, "Change program settings of parameters of Streaming program.");
            var streamingValue = 1;
            programDetailPage.StreamingDisplay.OpenSettings();
            new ProgramDetailParamEditStreamingPage().SetStreamingSliderValue(streamingValue);
            new ProgramDetailParamEditStreamingPage().Close();
            Assert.AreEqual(streamingValue, new ProgramDetailPage().StreamingDisplay.GetSliderValue());
            ReportHelper.LogTest(Status.Pass, "Changed setting for Streaming.");

            new ProgramDetailPage().OpenProgramSettings();
            Assert.IsTrue(new ProgramDetailSettingsControlPage().GetIsCreateFavoriteVisible(), "Create Favorite option should be available but is not present");
            new ProgramDetailSettingsControlPage().NavigateBack();
            ReportHelper.LogTest(Status.Pass, "Check successful 'Create Favorite' option is available for Streaming");

            ReportHelper.LogTest(Status.Info, "Navigate to all Programs to verify changes again.");
            programDetailPage = new ProgramDetailPage();

            //Noise Comfort
            programDetailPage.SelectProgram(1);
            Assert.AreEqual(noiseComfortNoiseReduction.ToString(), programDetailPage.NoiseReductionDisplay.GetValue());
            Assert.AreEqual(noiseComfortSpeechFocus.ToString(), new ProgramDetailPage().SpeechFocusDisplay.GetValue());
            Assert.AreEqual(noiseComfortValue, programDetailPage.EqualizerDisplay.GetEqualizerSliderValue(noiseComfortequalizerBand));

            //Music
            programDetailPage.SelectProgram(2);
            Assert.AreEqual(musicSpeechFocus.ToString(), new ProgramDetailPage().SpeechFocusDisplay.GetValue());

            //Streaming
            new ProgramDetailPage().SelectProgram(3);
            Assert.AreEqual(streamingValue, new ProgramDetailPage().StreamingDisplay.GetSliderValue());
            ReportHelper.LogTest(Status.Pass, "Check successful change in program parameters is saved.");

            List<string> GetMenuItemTexts(BaseSubMenuPage menuPage)
            {
                var menuItemsList = menuPage.MenuItems.GetAllVisible();
                return menuItemsList;
            }
        }

        /// <summary>
        /// To automate turning on and off HA some additional hardware is required.
        /// Currently test steps only check the Init page animations.
        /// </summary>
        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-10721_Table-69")]
        public void ST10721_VerifyHearingAidInitPage()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            string firstHearingAid = SelectHearingAid.GetLeftHearingAid();
            string secondHearingAid = SelectHearingAid.GetRightHearingAid();

            SelectHearingAidsPage selectHearingAidsPage = LaunchHelper.SkipToSelectHearingAidsPage().Page;
            selectHearingAidsPage.WaitUntilDeviceFound(firstHearingAid);
            selectHearingAidsPage.WaitUntilDeviceFound(secondHearingAid);
            selectHearingAidsPage.WaitUntilDeviceListNotChanging();
            Assert.IsTrue(selectHearingAidsPage.GetIsDeviceSelected(firstHearingAid));
            Assert.IsTrue(selectHearingAidsPage.GetIsDeviceSelected(secondHearingAid));
            selectHearingAidsPage.Connect();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + firstHearingAid + "' and Right '" + secondHearingAid + "'");

            //The test step to turn off then on  the left HA is not automated and needs to done in future

            ReportHelper.LogTest(Status.Info, "Checking the Hearing Aid init page.");
            HearingAidInitPage hearingAidInitPage = new HearingAidInitPage();

            //Assert.IsTrue(hearingAidInitPage.RightHearingAid.IsVisible);
            //Assert.IsNotEmpty(hearingAidInitPage.RightHearingAid.SideText);
            //Assert.IsTrue(hearingAidInitPage.LeftHearingAid.IsVisible);
            //Assert.IsNotEmpty(hearingAidInitPage.LeftHearingAid.SideText);
            //ReportHelper.LogTest(Status.Pass, "Icons and Texts for Left and Right hearing aid are visible on Init page.");

            //Assert.IsTrue(hearingAidInitPage.LeftHearingAid.IsConnecting);
            //Assert.IsTrue(hearingAidInitPage.RightHearingAid.IsConnecting);
            //ReportHelper.LogTest(Status.Pass, "Circular loading indicator is visible for Left and Right.");

            Wait.UntilTrue(() =>
            {
                return hearingAidInitPage.RightHearingAid.IsConnected || hearingAidInitPage.LeftHearingAid.IsConnected;
            }, TimeSpan.FromSeconds(20));
            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(45));
            PermissionHelper.AllowPermissionIfRequested();
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)));
            ReportHelper.LogTest(Status.Pass, "Dashbaord page is visible.");
        }

        [Test]
        [Category("SystemTestsDeviceSpecificConfig")]
        [Description("TC-10765_Table-68")]
        public void ST10765_ImplementNoiseOnly()
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

            // Open Noise Only Program
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            var programDetailPage = new ProgramDetailPage();
            ReportHelper.LogTest(Status.Info, "Noise Only Program opened");

            programDetailPage.TinnitusOnlyDisplay.OpenSettings();

            var programDetailParamEditTinnitusPage = new ProgramDetailParamEditTinnitusPage();

            bool isNoiseOn = programDetailParamEditTinnitusPage.GetIsTinnitusSwitchChecked();

            Assert.IsTrue(isNoiseOn, "Noise only is checked");
            ReportHelper.LogTest(Status.Info, "Noise only is checked");

            const int sliderStepCount = 21;
            double tolerance = 1f / sliderStepCount;
            if (OniOS) tolerance *= 2;

            programDetailParamEditTinnitusPage.SetVolumeSliderValue(0.4);
            var singleVolume0 = programDetailParamEditTinnitusPage.GetVolumeSliderValue();
            Assert.AreEqual(0.4, singleVolume0, tolerance);
            programDetailParamEditTinnitusPage.WaitUntilNoLoadingIndicator();

            programDetailParamEditTinnitusPage.SetVolumeSliderValue(0.6);
            var singleVolume1 = programDetailParamEditTinnitusPage.GetVolumeSliderValue();
            Assert.AreEqual(0.6, singleVolume1, tolerance);
            programDetailParamEditTinnitusPage.WaitUntilNoLoadingIndicator();

            programDetailParamEditTinnitusPage.SetVolumeSliderValue(0.5);
            var singleVolumePoint5 = programDetailParamEditTinnitusPage.GetVolumeSliderValue();
            Assert.AreEqual(0.5, Math.Round(singleVolumePoint5, 1), tolerance);
            programDetailParamEditTinnitusPage.WaitUntilNoLoadingIndicator();

            programDetailParamEditTinnitusPage.Close();

            new ProgramDetailPage().OpenProgramSettings();

            string FavoriteName = "Favorite 01";
            new ProgramDetailSettingsControlPage().CreateFavorite();
            new ProgramNamePage().EnterName(FavoriteName).Proceed();
            new ProgramIconPage().SelectIcon(24).Proceed();
            new ProgramAutomationPage().Proceed();
            ReportHelper.LogTest(Status.Info, "Favorite Program Name and Icon Noted: " + FavoriteName);
            new ProgramDetailPage().TapBack();

            // Closing the App
            AppManager.CloseApp();
            ReportHelper.LogTest(Status.Info, "App Closed");

            // Restarting the App
            AppManager.RestartApp(false);
            ReportHelper.LogTest(Status.Info, "App Restarted");

            // Load Dashboard
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            DashboardPage dashboardPageRestart = new DashboardPage();
            dashboardPageRestart.WaitUntilProgramInitFinished();

            // Open Noise Only Program
            dashboardPageRestart.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            var programDetailPageRestart = new ProgramDetailPage();
            ReportHelper.LogTest(Status.Info, "Noise Only Program opened");

            programDetailPageRestart.TinnitusOnlyDisplay.OpenSettings();

            var programDetailParamEditTinnitusPageResrart = new ProgramDetailParamEditTinnitusPage();

            bool isNoiseOnRestart = programDetailParamEditTinnitusPageResrart.GetIsTinnitusSwitchChecked();

            Assert.IsTrue(isNoiseOn == isNoiseOnRestart, "Tinitus Switch noted value and current value is matching");
            ReportHelper.LogTest(Status.Pass, "Tinitus Switch noted value and current value is matching");

            var singleVolumeRestart = programDetailParamEditTinnitusPageResrart.GetVolumeSliderValue();

            Assert.IsTrue(singleVolumePoint5 == singleVolumeRestart, "Tinitus volume noted value and current value is matching");
            ReportHelper.LogTest(Status.Pass, "Tinitus volume noted value and current value is matching");
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-10632_Table-72")]
        public void ST10632_SearchingWirelessDevices()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            string LeftHearingAidName = SelectHearingAid.GetLeftHearingAid();
            string RightHearingAidName = SelectHearingAid.GetRightHearingAid();

            // Load Intro and connect to Hearing Aid
            var selectPage = LaunchHelper.SkipToSelectHearingAidsPage().Page;
            selectPage.WaitUntilDevicesFound(2);
            Wait.UntilTrue(() => selectPage.GetSelectedDeviceNames().Count == 2, TimeSpan.FromSeconds(15));
            selectPage.Cancel();
            ReportHelper.LogTest(Status.Info, "Hearing Aid Connect attempt Left " + LeftHearingAidName + " and Right " + RightHearingAidName + " cancelled");

            new InitializeHardwarePage().StartScan();

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
            selectPage.Connect();

            new HearingAidInitPage().WaitForConnection();

            new DashboardPage().OpenLeftHearingDevice();

            var HearingInstrumentPageLeft = new HearingInstrumentInfoControlPage();

            string titleLeft = HearingInstrumentPageLeft.GetTitle();
            string typeLeft = HearingInstrumentPageLeft.GetDeviceType();
            string nameLeft = HearingInstrumentPageLeft.GetDeviceName();
            string serialLeft = HearingInstrumentPageLeft.GetDeviceSerial();

            Assert.IsNotEmpty(titleLeft);
            Assert.IsNotEmpty(typeLeft);
            Assert.IsNotEmpty(nameLeft);
            Assert.IsNotEmpty(serialLeft);

            HearingInstrumentPageLeft.Close();

            ReportHelper.LogTest(Status.Pass, "Left Hearing Aid Details Verified");
            new DashboardPage().OpenRightHearingDevice();

            // Verify Right Hearing Aid Devices
            var hearingInstrumentPageRight = new HearingInstrumentInfoControlPage();
            string titleRight = hearingInstrumentPageRight.GetTitle();
            string typeRight = hearingInstrumentPageRight.GetDeviceType();
            string nameRight = hearingInstrumentPageRight.GetDeviceName();
            string serialRight = hearingInstrumentPageRight.GetDeviceSerial();

            Assert.IsNotEmpty(titleRight);
            Assert.IsNotEmpty(typeRight);
            Assert.IsNotEmpty(nameRight);
            Assert.IsNotEmpty(serialRight);

            hearingInstrumentPageRight.Close();

            ReportHelper.LogTest(Status.Pass, "Right Hearing Aid Details Verified");
        }

        #endregion Sprint 14

        #region Sprint 15

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-15020_Table-33")]
        public void ST15020_VerifyHearingAidConnection()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var firstHearingAid = SelectHearingAid.GetLeftHearingAid();

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTimeAndVerify(firstHearingAid);

            ReportHelper.LogTest(Status.Info, "Navigate to 'My Hearing Instruments' page from settings.");
            NavigationHelper.NavigateToSettingsMenu(dashboardPage).OpenMyHearingAids();
            var hearingSystemPage = new HearingSystemManagementPage();

            Assert.IsNotEmpty(hearingSystemPage.GetLeftTabText());
            Assert.IsTrue(hearingSystemPage.GetIsLeftTabSelected());
            string leftName = hearingSystemPage.GetLeftDeviceName();
            string leftSerial = hearingSystemPage.GetLeftDeviceSerial();
            string leftState = hearingSystemPage.GetLeftDeviceState();
            string leftType = hearingSystemPage.GetLeftDeviceType();
            string leftNameTitle = hearingSystemPage.GetLeftDeviceNameTitle();
            string leftTypeTitle = hearingSystemPage.GetLeftDeviceTypeTitle();
            string leftSerialTitle = hearingSystemPage.GetLeftDeviceSerialTitle();
            string leftStateTitle = hearingSystemPage.GetLeftDeviceStateTitle();
            Assert.IsTrue(hearingSystemPage.GetIsLeftUdiVisible());
            string leftUdiTitle = hearingSystemPage.GetLeftDeviceUdiTitle();
            string leftUdi = hearingSystemPage.GetLeftDeviceUdi();

            ReportHelper.LogTest(Status.Info, "Checking info of the Left device.");
            Assert.IsNotEmpty(leftName);
            Assert.IsNotEmpty(leftSerial);
            Assert.IsNotEmpty(leftState);
            Assert.IsNotEmpty(leftType);
            Assert.IsNotEmpty(leftNameTitle);
            Assert.IsNotEmpty(leftTypeTitle);
            Assert.IsNotEmpty(leftSerialTitle);
            Assert.IsNotEmpty(leftStateTitle);
            Assert.IsNotEmpty(leftUdiTitle);
            Assert.IsNotEmpty(leftUdi);
            ReportHelper.LogTest(Status.Pass, "All info is diaplayed correctly for connected device.");

            ReportHelper.LogTest(Status.Info, "Tap 'Diconnect' and verify the App dialog.");
            hearingSystemPage.DisconnectDevices();
            Assert.IsTrue(DialogHelper.GetIsDialogDisplayed(TimeSpan.FromSeconds(5)));
            Assert.IsNotEmpty(new AppDialog().GetConfirmButtonText());
            Assert.IsNotEmpty(new AppDialog().GetDenyButtonText());
            ReportHelper.LogTest(Status.Pass, "App dialog is displayed correctly with 'Cancel' and 'Confirm' button.");
            new AppDialog().Deny();
            Thread.Sleep(500);

            ReportHelper.LogTest(Status.Info, "Cancel the dialog and check the hearing aid information again.");
            Assert.AreEqual(leftName, hearingSystemPage.GetLeftDeviceName());
            Assert.AreEqual(leftNameTitle, hearingSystemPage.GetLeftDeviceNameTitle());
            Assert.AreEqual(leftSerialTitle, hearingSystemPage.GetLeftDeviceSerialTitle());
            Assert.AreEqual(leftSerial, hearingSystemPage.GetLeftDeviceSerial());
            Assert.AreEqual(leftStateTitle, hearingSystemPage.GetLeftDeviceStateTitle());
            Assert.AreEqual(leftState, hearingSystemPage.GetLeftDeviceState()); //Check this ?
            Assert.AreEqual(leftTypeTitle, hearingSystemPage.GetLeftDeviceTypeTitle());
            Assert.AreEqual(leftType, hearingSystemPage.GetLeftDeviceType());
            Assert.AreEqual(leftUdiTitle, hearingSystemPage.GetLeftDeviceUdiTitle());
            Assert.AreEqual(leftUdi, hearingSystemPage.GetLeftDeviceUdi());
            ReportHelper.LogTest(Status.Pass, "The 'My hearing instruments' view is displayed with unchanged information.");

            ReportHelper.LogTest(Status.Info, "Tap 'Diconnect' and verify the App dialog.");
            hearingSystemPage.DisconnectDevices();

            DialogHelper.ConfirmIfDisplayed();

            Assert.IsTrue(new InitializeHardwarePage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)));
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

            selectHearingAidsPage.WaitUntilDeviceListNotChanging();
            selectHearingAidsPage.SelectDevicesExclusively(firstHearingAid);
            Assert.IsTrue(selectHearingAidsPage.GetIsDeviceSelected(firstHearingAid));

            ReportHelper.LogTest(Status.Pass, "Found Hearing aids " + firstHearingAid);
            Assert.IsNotEmpty(selectHearingAidsPage.GetConnectButtonText());
            selectHearingAidsPage.Connect();

            ReportHelper.LogTest(Status.Info, "Connect the Hearing aids and check the Start View.");
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            dashboardPage = new DashboardPage();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Pass, "Start view is displayed correctly containing L device icon.");
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-12749_Table-38")]
        public void ST12749_CheckNavigationOnIntroPages()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var firstHearingAid = SelectHearingAid.GetLeftHearingAid();
            var secondHearingAid = SelectHearingAid.GetRightHearingAid();

            ReportHelper.LogTest(Status.Info, "Checking the Welcome Page.");
            var introPageOne = new IntroPageOne();
            Assert.IsTrue(introPageOne.GetIsRightButtonVisible());
            Assert.IsFalse(introPageOne.GetIsLeftButtonVisible());
            ReportHelper.LogTest(Status.Pass, "Welcome Page is displayed correctly.");

            int pageIndex = 4;
            for (int i = 1; i <= pageIndex; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    //Move forward to next page from Welcome
                    Assert.IsTrue(new IntroPageOne(false).GetIsRightButtonVisible());
                    new IntroPageOne(false).MoveRightBySwiping();
                    Thread.Sleep(1000);
                    Assert.IsTrue(new IntroPageOne(false).GetIsLeftButtonVisible());//to ascertain that we are not still on Welcome Page
                }
                for (int j = 1; j <= i; j++)
                {
                    //Move backward all they way back to Welcome page
                    // ToDo: IPhone does not have back button and the current implementation is new NavigationBar().NavigateBack(); which does not work for Intro Page
                    if (OniOS)
                        new IntroPageOne(false).MoveLeftByTapping();
                    else
                        AppManager.App.PressBackButton();
                    Thread.Sleep(1000);
                }
                Assert.IsTrue(new IntroPageOne(false).GetIsRightButtonVisible());
                Assert.IsFalse(new IntroPageOne(false).GetIsLeftButtonVisible());//to ascertain that we are back on Welcome Page
            }

            ReportHelper.LogTest(Status.Pass, "Welcome Page is displayed correctly after back navigation.");

            ReportHelper.LogTest(Status.Info, "Press Back again and check the behaviour.");

            // ToDo: IPhone does not have back button and the current implementation is new NavigationBar().NavigateBack(); which does not close the App. So closing it by AppManager.CloseApp(); method
            if (OniOS)
                AppManager.CloseApp();
            else
                AppManager.App.PressBackButton();

            Assert.IsTrue(new IntroPageOne(false).IsGoneBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Pass, "App is closed successfully after pressing back from Welcome Page.");
            AppManager.StartApp(false);
            Assert.IsTrue(new IntroPageOne(false).GetIsRightButtonVisible());
            Assert.IsFalse(new IntroPageOne(false).GetIsLeftButtonVisible());//to ascertain that we are back on Welcome Page
            ReportHelper.LogTest(Status.Pass, "App is opened successfully and Welcome Page is visible.");

            ReportHelper.LogTest(Status.Info, "Navigate to page 'Here We Go'.");
            while (!new IntroPageFive(false).IsCurrentlyShown())
            {
                new IntroPageOne(false).MoveRightBySwiping();
            }
            Assert.IsTrue(new IntroPageFive().GetIsLeftButtonVisible());
            ReportHelper.LogTest(Status.Pass, "'Here we go' page is displayed correctly.");

            ReportHelper.LogTest(Status.Info, "Tap 'Continue' accept all dialogs and check is 'Start search' page is visible.");
            new IntroPageFive().Continue();
            DialogHelper.ConfirmIfDisplayed();
            ReportHelper.LogTest(Status.Pass, "App dialog is displayed correctly.");
            DialogHelper.ConfirmIfDisplayed();
            PermissionHelper.AllowPermissionIfRequested();
            var initializeHardwarePage = new InitializeHardwarePage();
            Assert.IsNotEmpty(initializeHardwarePage.GetDemoModeText());
            Assert.IsNotEmpty(initializeHardwarePage.GetScanText());
            ReportHelper.LogTest(Status.Pass, "'Start Search' page is displayed correctly. Now tap 'Start Scan'");

            initializeHardwarePage.StartScan();
            DialogHelper.ConfirmIfDisplayed();
            PermissionHelper.AllowPermissionIfRequested();
            var selectHearingAidsPage = new SelectHearingAidsPage();
            Assert.IsTrue(selectHearingAidsPage.GetIsScanning());
            Assert.IsNotEmpty(selectHearingAidsPage.GetDescription());
            Assert.IsNotEmpty(selectHearingAidsPage.GetCancelText());
            selectHearingAidsPage.WaitUntilDeviceFound(firstHearingAid);
            Assert.IsTrue(selectHearingAidsPage.GetIsDeviceFound(firstHearingAid));
            if (!selectHearingAidsPage.GetIsDeviceFound(secondHearingAid))
                selectHearingAidsPage.WaitUntilDeviceFound(secondHearingAid);
            Assert.IsTrue(selectHearingAidsPage.GetIsDeviceFound(secondHearingAid));
            ReportHelper.LogTest(Status.Pass, "Found Hearing aids " + firstHearingAid + " and " + secondHearingAid);

            //Cancel Search
            ReportHelper.LogTest(Status.Info, "Cancel Search on Select Hearing Aids page.");
            selectHearingAidsPage.Cancel();
            Assert.IsTrue(new InitializeHardwarePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            Assert.IsNotEmpty(initializeHardwarePage.GetDemoModeText());
            Assert.IsNotEmpty(initializeHardwarePage.GetScanText());
            ReportHelper.LogTest(Status.Pass, "'Start Search' page is displayed after cancel search. Now again 'Start Scan'");
            initializeHardwarePage.StartScan();
            new SelectHearingAidsPage().WaitUntilDevicesFound(2);

            List<string> deviceNames = new SelectHearingAidsPage().GetAllDeviceNames();
            CollectionAssert.Contains(deviceNames, firstHearingAid);
            CollectionAssert.Contains(deviceNames, secondHearingAid);
            ReportHelper.LogTest(Status.Pass, "The existing intruments are still presnt in the scan view.");

            ReportHelper.LogTest(Status.Info, "Use back button to navigate back.");

            // ToDo: IPhone does not have back button and the current implementation is new NavigationBar().NavigateBack(); which does not close the App. So going back by new SelectHearingAidsPage().Cancel(); method
            if (OniOS)
                new SelectHearingAidsPage().Cancel();
            else
                AppManager.App.PressBackButton();

            Assert.IsTrue(new InitializeHardwarePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            Assert.IsNotEmpty(initializeHardwarePage.GetDemoModeText());
            Assert.IsNotEmpty(initializeHardwarePage.GetScanText());
            ReportHelper.LogTest(Status.Pass, "'Start Search' page is displayed after pressing back button.");

            ReportHelper.LogTest(Status.Info, "Press back again to close the app.");

            // ToDo: IPhone does not have back button and the current implementation is new NavigationBar().NavigateBack(); which does not close the App. So closing it by AppManager.CloseApp(); method
            if (OniOS)
                AppManager.CloseApp();
            else
                AppManager.App.PressBackButton();

            AppManager.StartApp(false);
            Assert.IsTrue(new InitializeHardwarePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            Assert.IsNotEmpty(initializeHardwarePage.GetScanText());
            ReportHelper.LogTest(Status.Pass, "App is started again 'Start Search' page is displayed successfully.");

            initializeHardwarePage.StartScan();
            new SelectHearingAidsPage().WaitUntilScanFinished();
            new SelectHearingAidsPage().WaitUntilDevicesFound(2);
            deviceNames = new SelectHearingAidsPage().GetAllDeviceNames();
            CollectionAssert.Contains(deviceNames, firstHearingAid);
            CollectionAssert.Contains(deviceNames, secondHearingAid);
            ReportHelper.LogTest(Status.Pass, "The existing intruments are still presnt in the scan view.");

            ReportHelper.LogTest(Status.Info, "Use back button again to navigate back.");

            // ToDo: IPhone does not have back button and the current implementation is new NavigationBar().NavigateBack(); which does not close the App. So going back by new SelectHearingAidsPage().Cancel(); method
            if (OniOS)
                new SelectHearingAidsPage().Cancel();
            else
                AppManager.App.PressBackButton();

            Assert.IsTrue(new InitializeHardwarePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            Assert.IsNotEmpty(initializeHardwarePage.GetDemoModeText());
            Assert.IsNotEmpty(initializeHardwarePage.GetScanText());
            ReportHelper.LogTest(Status.Pass, "'Start Search' page is displayed after pressing back button.");

            ReportHelper.LogTest(Status.Info, "Press back again to close the app.");

            // ToDo: IPhone does not have back button and the current implementation is new NavigationBar().NavigateBack(); which does not close the App. So closing it by AppManager.CloseApp(); method
            if (OniOS)
                AppManager.CloseApp();
            else
                AppManager.App.PressBackButton();

            AppManager.StartApp(false);
            Assert.IsTrue(new InitializeHardwarePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            Assert.IsNotEmpty(initializeHardwarePage.GetDemoModeText());
            ReportHelper.LogTest(Status.Pass, "App is started again 'Start Search' page is displayed successfully.");
            initializeHardwarePage.StartDemoMode();
            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(20));
            PermissionHelper.AllowPermissionIfRequested();
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(30)));
            new DashboardPage().CheckStartView(new DashboardPage());

            // ToDo: IPhone does not have back button and the current implementation is new NavigationBar().NavigateBack(); which does not close the App. So closing it by AppManager.CloseApp(); method
            if (OniOS)
                AppManager.CloseApp();
            else
                AppManager.App.PressBackButton();

            ReportHelper.LogTest(Status.Info, "Press back again to close the app.");
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-13165_Table-35")]
        public void ST13165_ScreenToActive()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            string LeftHearingAidName = SelectHearingAid.GetLeftHearingAid();
            string RightHearingAidName = SelectHearingAid.GetRightHearingAid();

            // Start App in Demo Mode
            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
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
            ReportHelper.LogTest(Status.Info, "The settings is opened in Demo Mode");

            // Change the Mode to Normal
            new SettingsMenuPage().OpenDemoMode();
            var appModeSelectionPage = new AppModeSelectionPage();
            appModeSelectionPage.SelectAppMode(AppMode.Normal);
            ReportHelper.LogTest(Status.Info, "App mode set to normal");
            appModeSelectionPage.ChangeAppMode(AppMode.Normal);

            // After setting to normal mode the app lands in InitializeHardwarePage only for Kind
            if (AppManager.Brand == Brand.Kind)
            {
                Assert.IsTrue(new InitializeHardwarePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
                ReportHelper.LogTest(Status.Pass, "Exited Demo Mode, InitializeHardwarePage screen is visible.");

                dashboardPage = LaunchHelper.ReconnectHearingAidsFromStartScan(LeftHearingAidName, RightHearingAidName);
            }
            else
            {
                Assert.IsTrue(new IntroPageOne().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
                ReportHelper.LogTest(Status.Pass, "Exited Demo Mode, Welcome screen is visible.");

                dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(LeftHearingAidName, RightHearingAidName).Page;
            }

            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + LeftHearingAidName + "' and Right '" + RightHearingAidName + "'");

            dashboardPage.OpenMenuUsingTap();
            new MainMenuPage().OpenSettings();
            ReportHelper.LogTest(Status.Info, "The settings is opened in Device Mode");

            // Change the Mode to Demo
            new SettingsMenuPage().OpenDemoMode();
            var appModeSelectionPageDemo = new AppModeSelectionPage();
            appModeSelectionPageDemo.SelectAppMode(AppMode.Demo);
            appModeSelectionPageDemo.ChangeAppMode(AppMode.Demo);
            ReportHelper.LogTest(Status.Info, "App mode set to demo");

            ReportHelper.LogTest(Status.Pass, "Exited Demo Mode, Welcome screen is visible.");
        }

        [Test]
        [Category("SystemTestsDeviceSpecificConfig")]
        [Description("TC-12207_Table-45")]
        public void ST12207_IdentifyDevicePairs()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            // The below is Monaural Hearing Aid Configured in Audifit 5
            string LeftHearingAidNameSingle = SelectHearingAid.GetLeftHearingAid(LeftHearingAid.Microgenisis_Lewi_R_Left_068843);

            // The below are Binaural Hearing Aids Configured in Audifit 5
            string LeftHearingAidName = SelectHearingAid.GetLeftHearingAid(LeftHearingAid.Microgenisis_Risa_R_Left_068826);
            string RightHearingAidName = SelectHearingAid.GetRightHearingAid(RightHearingAid.Microgenisis_Risa_R_Right_068822);

            ReportHelper.LogTest(Status.Info, "App started");

            // Load Intro and connect to Hearing Aid
            var selectPageSingle = LaunchHelper.SkipToSelectHearingAidsPage().Page;
            ReportHelper.LogTest(Status.Info, "Scan started for Mononural Attempt 1");
            selectPageSingle.WaitUntilDevicesFound();

            // Cancel Scan
            selectPageSingle.Cancel();
            ReportHelper.LogTest(Status.Info, "Scan canceled Mononural Attempt 1");

            // Do Scan
            new InitializeHardwarePage().StartScan();
            ReportHelper.LogTest(Status.Info, "Scan started for Mononural Attempt 2");

            selectPageSingle.WaitUntilDeviceFound(LeftHearingAidNameSingle);
            if (!selectPageSingle.GetIsDeviceFound(LeftHearingAidNameSingle))
                selectPageSingle.WaitUntilDeviceFound(LeftHearingAidNameSingle);

            selectPageSingle.WaitUntilDeviceListNotChanging();
            selectPageSingle.SelectDevicesExclusively(LeftHearingAidNameSingle);
            selectPageSingle.GetIsDeviceSelected(LeftHearingAidNameSingle);

            // Cancel Scan
            selectPageSingle.Cancel();
            ReportHelper.LogTest(Status.Info, "Scan canceled Mononural Attempt 1");

            ReportHelper.LogTest(Status.Pass, "Mononural Hearing Aid Connect attempt Left" + LeftHearingAidNameSingle + " cancelled for attempts 2");

            // Do Scan
            new InitializeHardwarePage().StartScan();
            ReportHelper.LogTest(Status.Info, "Scan started for Binaural Attempt 1");

            // Try to connect to Binaural Hearing Aid and Cancel
            SelectHearingAidsPage selectPage = new SelectHearingAidsPage();
            selectPage.WaitUntilDevicesFound(1, TimeSpan.FromSeconds(3));

            // Cancel Scan
            selectPage.Cancel();
            ReportHelper.LogTest(Status.Info, "Scan cancelled for Binaural Attempt 1");

            // Do Scan
            new InitializeHardwarePage().StartScan();
            ReportHelper.LogTest(Status.Info, "Scan started Binaural Attempt 2");

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

            // Cancel Scan
            selectPage.Cancel();
            ReportHelper.LogTest(Status.Info, "Scan canceled Binaural Attempt 2");

            ReportHelper.LogTest(Status.Pass, "Binaural Hearing Aid Connect attempt Left" + LeftHearingAidName + " and Right " + RightHearingAidName + " cancelled for attempts 2");
        }

        #endregion Sprint 15

        #endregion Test Cases
    }
}