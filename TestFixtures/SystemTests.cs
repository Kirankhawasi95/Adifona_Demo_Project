using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using HorusUITest.Adapters;
using HorusUITest.Configuration;
using HorusUITest.Data;
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
using HorusUITest.PageObjects.Menu.Help.HelpTopics;
using HorusUITest.PageObjects.Menu.Help.InstructionsForUse;
using HorusUITest.PageObjects.Menu.Info;
using HorusUITest.PageObjects.Menu.Programs;
using HorusUITest.PageObjects.Menu.Settings;
using HorusUITest.PageObjects.Start;
using NUnit.Framework;

namespace HorusUITest.TestFixtures
{
    public class SystemTests : BaseTestFixture
    {
        public SystemTests(Platform platform)
            : base(platform)
        {
        }

        public override void AfterEachTest()
        {
            base.AfterEachTest();
            AppManager.CloseApp();
            AppManager.FinishAndCleanUp();
            //SwitchBox.DisableAll();
        }

        [Test]
        [Category("SystemTest0001")]
       // [TestCase(TestName = "ST0001")]

        public void ST0001_Geraet_erstmals_verbinden()
        {
            AndroidPhone androidPhone = SelectPhone.Android(AndroidPhoneName.Audifon_HTC_10);
            IosPhone iosPhone = SelectPhone.Ios(IosPhoneName.Audifon_iPhone_7_Plus);

            AppManager.InitializeApp(new AppConfig(CurrentPlatform, androidPhone, iosPhone));
            AppManager.StartApp(true);

            HearingAid hearingAid = SelectHearingAid.Left(LeftHearingAid.Audifon_ST0001_HG_026123);
            AppManager.DeviceSettings.EnableBluetooth();
            //Startup and connection
            Output.Immediately("The app has started.");
            Output.TestStep("Connecting hearing aids and proceeding to dashboard");
            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(hearingAid).Page;

            //On dashboard
            Output.TestStep("Checking number of hearing programs");
            Assert.AreEqual(2, dashboardPage.GetNumberOfPrograms(), "The total number of hearing programs doesn't meet the preconditions of this test.");
            string initialProgramName = dashboardPage.GetCurrentProgramName();

            //Create favorite for base program, including geo position
            Output.TestStep("Create favorite for base program, including geo position");
            Output.TestStep("Navigating to program detail of base program", 2);
            dashboardPage.OpenCurrentProgram();
            var testing = new ProgramDetailPage().SelectProgram(0);
            testing.OpenProgramSettings();
            Output.TestStep("Creating favorite with geofence on base program", 2);
            new ProgramDetailSettingsControlPage().CreateFavorite();
            var favoriteNameBase = "CopyBaseProgram";
            new ProgramNamePage().EnterName(favoriteNameBase).Proceed();
            new ProgramIconPage().SelectIcon(5).Proceed();
            Output.TestStep("Creating geofence based on current position", 2);
            var programAutomationPage = new ProgramAutomationPage().TurnOnAutomation();
            if (DialogHelper.ConfirmIfDisplayed()) 
                Wait.UntilTrue(programAutomationPage.IsCurrentlyShown, TimeSpan.FromMilliseconds(2500));
            programAutomationPage.GeofenceAutomation.OpenSettings();
            if (CurrentPlatform == Platform.Android)
                AppManager.DeviceSettings.GrantGPSBackgroundPermission();
            if (PermissionHelper.AllowPermissionIfRequested<AutomationGeofenceBindingPage>())
                Output.TestStep("Granted permission for GPS", 2);
            new AutomationGeofenceBindingPage().WaitUntilNoLoadingIndicator().SelectPosition(0.5, 0.5).Ok();
            Output.TestStep("Save favorite", 2);
            new ProgramAutomationPage().Proceed();
            Output.TestStep("Verifying that the number of programs is increased by one", 2);
            Assert.AreEqual(3, new ProgramDetailPage().GetNumberOfVisibiblePrograms(), "The total number of hearing programs doesn't include the favorite for the base program.");

            //Create favorite for second program
            Output.TestStep("Create favorite for second program");
            Output.TestStep("Navigating to program detail of second program", 2);
            new ProgramDetailPage().SelectProgram(1);
            var programDetailPage = new ProgramDetailPage().SetVolumeSliderValue(0);
            programDetailPage.DecreaseVolume();
            programDetailPage.OpenProgramSettings();
            Output.TestStep("Creating favorite on second program", 2);
            new ProgramDetailSettingsControlPage().CreateFavorite();
            var favoriteNameSecond = "CopySecondProgram";
            new ProgramNamePage().EnterName(favoriteNameSecond).Proceed();
            new ProgramIconPage().SelectIcon(3).Proceed();
            Output.TestStep("Looking up for Wifi and cancel", 2);
            new ProgramAutomationPage().TurnOnAutomation().WifiAutomation.OpenSettings();
            //new AutomationWifiBindingPage().Cancel();
            new AutomationWifiBindingPage().TapBack();//added "TapBack()" option instead of "Cancel()" to avoid TestCase breaking at this Step, because "Cancel()" is not available on Page AutomationWifiBindingPage and in the TestCase no Wifi Binding should be active avoiding breaking the Case
            Output.TestStep("Save favorite", 2);
            new ProgramAutomationPage().Proceed();
            Output.TestStep("Verifying that the number of programs is increased by one", 2);
            Assert.AreEqual(4, new ProgramDetailPage().GetNumberOfVisibiblePrograms(), "The total number of hearing programs doesn't include the favorite for the second program.");

            //Restarting the app to check if the settings persist and check if the geofence switch works
            Output.TestStep("Restarting the app and expecting to be back to the dashboard");
            AppManager.RestartApp(false);
            dashboardPage = new DashboardPage();
            Output.TestStep("Checking number of hearing programs that are shown on the dashboard (yet again)", 2);
            Assert.AreEqual(4, dashboardPage.GetNumberOfPrograms(), "Unexpected number of hearing programs after creating new favorites and restarting the app.");
            Output.TestStep("Wait 10 seconds for the geofence switch and check if it switches to favorite", 2);
            Thread.Sleep(15000);
            StringAssert.AreEqualIgnoringCase(favoriteNameBase, dashboardPage.GetCurrentProgramName(), "Program did not switch to favorite with geofence.");
        }

        [Test]
        [Category("SystemTest0002")]
       // [TestCase(TestName = "ST0002")]
        public void ST0002_Geraet_erneut_verbinden()
        {
            AndroidPhone androidPhone = SelectPhone.Android(AndroidPhoneName.Audifon_Galaxy_A7);
            IosPhone iosPhone = SelectPhone.Ios(IosPhoneName.Audifon_iPhone_SE);

            AppManager.InitializeApp(new AppConfig(CurrentPlatform, androidPhone, iosPhone));
            AppManager.StartApp(true);

            HearingAid hearingAid = SelectHearingAid.Right(RightHearingAid.Audifon_ST0002_HG_025956);
            AppManager.DeviceSettings.EnableBluetooth();

            //Startup and connection
            Output.Immediately("The app has started.");
            Output.TestStep("Connecting hearing aids and proceeding to dashboard");
            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(hearingAid).Page;

            //on Dashboardpage
            Output.TestStep("Checking number of hearing programs");
            Assert.AreEqual(2, dashboardPage.GetNumberOfPrograms(), "The total number of hearing programs doesn't meet the preconditions of this test.");
            string initialProgramName = dashboardPage.GetCurrentProgramName();

            //Verify that a favorite cannot be created from the first Programm
            Output.TestStep("open detailpage of the active program on dashboardPge");
            dashboardPage.OpenCurrentProgram();

            Output.TestStep("Open program settings menu for program index 0", 2);
            new ProgramDetailPage().SelectProgram(0).OpenProgramSettings();
            var programSettingsPage = new ProgramDetailSettingsControlPage();

            //Output.TestStep("Check if selected program is automatic program", 2);
            //Assert.AreEqual("Automatik", programSettingsPage.GetNavigationBarTitle(), "The current program is not the automatic program."); //TODO: Make this independent from the app language

            //Output.TestStep("Verify that a favorite cannot be created from the first program", 2);
            //Assert.IsFalse(programSettingsPage.GetIsCreateFavoriteVisible(), "'Create Favorite' option available for automatic program");

            Output.TestStep("Navigate back to program detail page ", 2);
            programSettingsPage.TapBack();
            var detailPage = new ProgramDetailPage();

            //check Program Sliderbar for existing Favoriteprogram
            Output.TestStep("Checking the number of hearing programs");
            Assert.AreEqual(2, detailPage.GetNumberOfVisibiblePrograms(), "The total number of hearing programs doesn't meet the preconditions of this test.");

            Output.TestStep("Open program detail page for second program");
            detailPage.SelectProgram(1);
            Output.TestStep("Check program details of second program");
            //StringAssert.AreEqualIgnoringCase("Musik", detailPage.GetCurrentProgramName(), "Second program was expected to be 'Musik' program");    //TODO: Make this independent from the app language
            Assert.IsTrue(detailPage.GetIsSpeechFocusDisplayVisible(), "SpeechFocusSettings do not exist");
            Assert.IsTrue(detailPage.GetIsNoiseReductionDisplayVisible(), "NoiseReductionSettings do not exist");
            Assert.IsTrue(detailPage.GetIsTinnitusDisplayVisible(), "TinnitusSettings do not exist");
            Assert.IsTrue(detailPage.GetIsEqualizerDisplayVisible(), "EqualizerSettings do not exist");
            Assert.IsFalse(detailPage.GetIsBinauralSettingsButtonVisible(), "BinauralSettings exist in monaural mode");
            Assert.IsFalse(detailPage.GetIsLeftHearingDeviceVisible(), "LeftHearingAidInformation exists, but should not have been connected");
            Assert.IsTrue(detailPage.GetIsRightHearingDeviceVisible(), "RightHearingAidInformation does not exist");
            /*Implementierung da aktuell keine prüfung ob Back Button und Menu sichtbar in ProgramDetailPage*/
            Assert.DoesNotThrow(() => detailPage.OpenMenuUsingTap(), "Unable to open the main menu using tap.");
            new MainMenuPage().CloseMenuUsingTap();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), "Unable to close MainMenu using tap");

            // Edit Speech Focus Detail Settings
            Output.TestStep("Changing Parametersettings for each Menu in ProgramDetailPage");
            Output.TestStep("Chaniging Speechfocus Setting to 'Auto'", 2);
            detailPage.SpeechFocusDisplay.OpenSettings();
            var speechFocusPage = new ProgramDetailParamEditSpeechFocusPage();
            var initialMusicProgramSpeechFocus = speechFocusPage.GetSelectedSpeechFocus();
            speechFocusPage.SelectSpeechFocus(SpeechFocus.Auto);
            string expectedSpeechFocusSetting = speechFocusPage.GetSpeechFocusName(SpeechFocus.Auto);
            speechFocusPage.Close();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), "Unable to close the speech focus settings view");
            StringAssert.AreEqualIgnoringCase(expectedSpeechFocusSetting, detailPage.SpeechFocusDisplay.GetValue(), "Mismatch between selected and actual SpeechFocus Settings");

            //Edit Noise Reduction Detail Settings
            Output.TestStep("Chaninging NoiseReduction to 'Stark'", 2);
            detailPage.NoiseReductionDisplay.OpenSettings();
            var noisePage = new ProgramDetailParamEditNoiseReductionPage();
            var initialMusicProgramNoiseReduction = noisePage.GetSelectedNoiseReduction();
            noisePage.SelectNoiseReduction(NoiseReduction.Strong);
            string expectedNoiseReductionSetting = noisePage.GetNoiseReductionName(NoiseReduction.Strong);
            noisePage.Close();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), "Unable to close the noise reduction settings view");
            StringAssert.AreEqualIgnoringCase(expectedNoiseReductionSetting, detailPage.NoiseReductionDisplay.GetValue(), "Mismatch between selected and actual NoiseReduction Settings");

            //Edit Tinnitus Detail Settings
            Output.TestStep("Changing all Values in Tinnitus Settings to maximum value", 2);
            detailPage.TinnitusDisplay.OpenSettings();
            var tinnitusPage = new ProgramDetailParamEditTinnitusPage();
            var initialMusicProgramTinnitusLowEq = tinnitusPage.GetEqualizerSliderValue(EqBand.Low);
            var initialMusicProgramTinnitusMidEq = tinnitusPage.GetEqualizerSliderValue(EqBand.Mid);
            var initialMusicProgramTinnitusHighEq = tinnitusPage.GetEqualizerSliderValue(EqBand.High);
            var initialMusicProgramTinnitusVolume = tinnitusPage.GetEqualizerSliderValue(EqBand.High);
            tinnitusPage.TurnOnTinnitus();
            tinnitusPage.SetEqualizerSliderValue(EqBand.Low, 1.0);
            tinnitusPage.SetEqualizerSliderValue(EqBand.Mid, 1.0);
            tinnitusPage.SetEqualizerSliderValue(EqBand.High, 1.0);
            tinnitusPage.SetVolumeSliderValue(1.0);
            Assert.AreEqual(1.0, tinnitusPage.GetEqualizerSliderValue(EqBand.Low), 0.1, "Mismatch between selected and actual Settings for Low Equalizer in Tinnitus Detail Settings");
            Assert.AreEqual(1.0, tinnitusPage.GetEqualizerSliderValue(EqBand.Mid), 0.1, "Mismatch between selected and actual Settings for Mid Equalizer in Tinnitus Detail Settings");
            Assert.AreEqual(1.0, tinnitusPage.GetEqualizerSliderValue(EqBand.High), 0.1, "Mismatch between selected and actual Settings for High Equalizer in Tinnitus Detail Settings");
            Assert.AreEqual(1.0, tinnitusPage.GetVolumeSliderValue(), 0.1, "Mismatch between selected and actual Settings for Volume Slider in Tinnitus Detail Settings");
            tinnitusPage.Close();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), "Unable to close the tinnitus settings view");

            //Edit Equalizer Detail Settings
            Output.TestStep("Changing all Values in Equalizer Settings Menu Detail View to maximum", 2);
            detailPage.EqualizerDisplay.OpenSettings();
            var equalizerPage = new ProgramDetailParamEditEqualizerPage();
            var initialMusicProgramEqualizerLowEq = equalizerPage.GetEqualizerSliderValue(EqBand.Low);
            var initialMusicProgramEqualizerMidEq = equalizerPage.GetEqualizerSliderValue(EqBand.Mid);
            var initialMusicProgramEqualizerHighEq = equalizerPage.GetEqualizerSliderValue(EqBand.High);
            equalizerPage.SetEqualizerSliderValue(EqBand.Low, 1.0);
            equalizerPage.SetEqualizerSliderValue(EqBand.Mid, 1.0);
            equalizerPage.SetEqualizerSliderValue(EqBand.High, 1.0);
            var lowEq = equalizerPage.GetEqualizerSliderValue(EqBand.Low);
            var midEq = equalizerPage.GetEqualizerSliderValue(EqBand.Mid);
            var highEq = equalizerPage.GetEqualizerSliderValue(EqBand.High);
            Assert.AreEqual(1.0, equalizerPage.GetEqualizerSliderValue(EqBand.Low), 0.1, "Unable to set the equalizer slider to the desired value (low band)");
            Assert.AreEqual(1.0, equalizerPage.GetEqualizerSliderValue(EqBand.Mid), 0.1, "Unable to set the equalizer slider to the desired value (mid band)");
            Assert.AreEqual(1.0, equalizerPage.GetEqualizerSliderValue(EqBand.High), 0.1, "Unable to set the equalizer slider to the desired value (high band)");
            equalizerPage.Close();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), "Unable to close the equalizer settings view");
            Assert.AreEqual(lowEq, detailPage.EqualizerDisplay.GetEqualizerSliderValue(EqBand.Low), "Mismatch between selected and actual Settings for Low Equalizer in Equalizer Detail Settings");
            Assert.AreEqual(midEq, detailPage.EqualizerDisplay.GetEqualizerSliderValue(EqBand.Mid), "Mismatch between selected and actual Settings for Mid Equalizer in Equalizer Detail Settings");
            Assert.AreEqual(highEq, detailPage.EqualizerDisplay.GetEqualizerSliderValue(EqBand.High), "Mismatch between selected and actual Settings for High Equalizer in Equalizer Detail Settings");

            //create Favorite with actual Settings
            Output.TestStep("Create favorite from second program");
            detailPage.OpenProgramSettings();
            programSettingsPage = new ProgramDetailSettingsControlPage();
            Assert.IsTrue(programSettingsPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), "Could not open ProgramDetailSettingsControlPage from ProgramDateilView.");
            programSettingsPage.CreateFavorite();
            Output.TestStep("Enter Favorite Name", 2);
            var namePage = new ProgramNamePage();
            const string myFavoriteName = "MyFavorite";
            namePage.EnterName(myFavoriteName).Proceed();
            Output.TestStep("Select Favorite Icon", 2);
            var iconPage = new ProgramIconPage();
            iconPage.SelectIcon(16).Proceed();
            Output.TestStep("Create Favorite with actual Settings", 2);
            new ProgramAutomationPage().Proceed();

            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), "Unable to finalize create favorit workflow");

            /*Ergänzung nach Anpassung der Teststeps, setzen aller Programmeinstellungen im erzeugten Favoriten auf Minimum(Step 9)*/
            //set favorit menu to minimum value in all cases
            // Edit Speech Focus Detail Settings
            Output.TestStep("Changing Parametersettings to minimum value for each Menu in Favrorit ProgramDetailPage");
            Output.TestStep("Chaniging Speechfocus Setting to 'OFF'", 2);
            detailPage.SpeechFocusDisplay.OpenSettings();
            speechFocusPage = new ProgramDetailParamEditSpeechFocusPage();
            speechFocusPage.SelectSpeechFocus(SpeechFocus.Off);
            expectedSpeechFocusSetting = speechFocusPage.GetSpeechFocusName(SpeechFocus.Off);
            speechFocusPage.Close();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), "Unable to close the speech focus settings view");
            StringAssert.AreEqualIgnoringCase(expectedSpeechFocusSetting, detailPage.SpeechFocusDisplay.GetValue(), "Mismatch between selected and actual SpeechFocus Settings");

            //Edit Noise Reduction Detail Settings
            Output.TestStep("Chaninging NoiseReduction to 'OFF'", 2);
            detailPage.NoiseReductionDisplay.OpenSettings();
            noisePage = new ProgramDetailParamEditNoiseReductionPage();
            noisePage.SelectNoiseReduction(NoiseReduction.Off);
            expectedNoiseReductionSetting = noisePage.GetNoiseReductionName(NoiseReduction.Off);
            noisePage.Close();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), "Unable to close the noise reduction settings view");
            StringAssert.AreEqualIgnoringCase(expectedNoiseReductionSetting, detailPage.NoiseReductionDisplay.GetValue(), "Mismatch between selected and actual NoiseReduction Settings");

            //Edit Tinnitus Detail Settings
            Output.TestStep("Changing all Values in Tinnitus Settings to minimum value", 2);
            detailPage.TinnitusDisplay.OpenSettings();
            tinnitusPage = new ProgramDetailParamEditTinnitusPage();
            tinnitusPage.TurnOnTinnitus();
            tinnitusPage.SetEqualizerSliderValue(EqBand.Low, 0.0);
            tinnitusPage.SetEqualizerSliderValue(EqBand.Mid, 0.0);
            tinnitusPage.SetEqualizerSliderValue(EqBand.High, 0.0);
            tinnitusPage.SetVolumeSliderValue(0);
            Assert.AreEqual(0.0, tinnitusPage.GetEqualizerSliderValue(EqBand.Low), 0.1, "Mismatch between selected and actual Settings for Low Equalizer in Tinnitus Detail Settings");
            Assert.AreEqual(0.0, tinnitusPage.GetEqualizerSliderValue(EqBand.Mid), 0.1, "Mismatch between selected and actual Settings for Mid Equalizer in Tinnitus Detail Settings");
            Assert.AreEqual(0.0, tinnitusPage.GetEqualizerSliderValue(EqBand.High), 0.1, "Mismatch between selected and actual Settings for High Equalizer in Tinnitus Detail Settings");
            Assert.AreEqual(0.0, tinnitusPage.GetVolumeSliderValue(), 0.1, "Mismatch between selected and actual Settings for Volume Slider in Tinnitus Detail Settings");
            tinnitusPage.Close();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), "Unable to close the tinnitus settings view");

            //Edit Equalizer Detail Settings
            Output.TestStep("Changing all Values in Equalizer Settings Menu Detail View to minimum", 2);
            detailPage.EqualizerDisplay.OpenSettings();
            equalizerPage = new ProgramDetailParamEditEqualizerPage();
            equalizerPage.SetEqualizerSliderValue(EqBand.Low, 0.0);
            equalizerPage.SetEqualizerSliderValue(EqBand.Mid, 0.0);
            equalizerPage.SetEqualizerSliderValue(EqBand.High, 0.0);
            lowEq = equalizerPage.GetEqualizerSliderValue(EqBand.Low);
            midEq = equalizerPage.GetEqualizerSliderValue(EqBand.Mid);
            highEq = equalizerPage.GetEqualizerSliderValue(EqBand.High);
            Assert.AreEqual(0.0, equalizerPage.GetEqualizerSliderValue(EqBand.Low), 0.1, "Unable to set the equalizer slider to the desired value (low band)");
            Assert.AreEqual(0.0, equalizerPage.GetEqualizerSliderValue(EqBand.Mid), 0.1, "Unable to set the equalizer slider to the desired value (mid band)");
            Assert.AreEqual(0.0, equalizerPage.GetEqualizerSliderValue(EqBand.High), 0.1, "Unable to set the equalizer slider to the desired value (high band)");
            equalizerPage.Close();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), "Unable to close the equalizer settings view");
            Assert.AreEqual(lowEq, detailPage.EqualizerDisplay.GetEqualizerSliderValue(EqBand.Low), 0.1, "Mismatch between selected and actual Settings for Low Equalizer in Equalizer Detail Settings");
            Assert.AreEqual(midEq, detailPage.EqualizerDisplay.GetEqualizerSliderValue(EqBand.Mid),0.1,  "Mismatch between selected and actual Settings for Mid Equalizer in Equalizer Detail Settings");
            Assert.AreEqual(highEq, detailPage.EqualizerDisplay.GetEqualizerSliderValue(EqBand.High), 0.1,  "Mismatch between selected and actual Settings for High Equalizer in Equalizer Detail Settings");


            //Restart App
            Output.TestStep("Restart App");
            AppManager.RestartApp(false);

            //reading Number of Programs after App Restart
            Output.TestStep("Getting Number of Programs after App Restart");
            dashboardPage = new DashboardPage();
            //var programsAvailAfterRestart = dashboardPage.GetNumberOfPrograms();
            Assert.AreEqual(3, dashboardPage.GetNumberOfPrograms(), "Unexpected Number of Programs");
            dashboardPage.WaitUntilProgramInitFinished();
            
            //checking settings of second program - "Musik"
            dashboardPage.OpenCurrentProgram();
            detailPage = new ProgramDetailPage();
            detailPage.SelectProgram(1);
            Assert.IsTrue(detailPage.GetIsSpeechFocusDisplayVisible(), "SpeechFocusSettings do not exist");
            Assert.IsTrue(detailPage.GetIsNoiseReductionDisplayVisible(), "NoiseReductionSettings do not exist");
            Assert.IsTrue(detailPage.GetIsTinnitusDisplayVisible(), "TinnitusSettings do not exist");
            Assert.IsTrue(detailPage.GetIsEqualizerDisplayVisible(), "EqualizerSettings do not exist");
            Assert.IsFalse(detailPage.GetIsBinauralSettingsButtonVisible(), "BinauralSettings exist in monaural mode");
            Assert.IsFalse(detailPage.GetIsLeftHearingDeviceVisible(), "LeftHearingAidInformation exists, but should not have been connected");
            Assert.IsTrue(detailPage.GetIsRightHearingDeviceVisible(), "RightHearingAidInformation does not exist");

            //check SpeechFocus Default Setting
            Output.TestStep("check SpeechFocus Setting in second Program", 2);
            detailPage.SpeechFocusDisplay.OpenSettings();
            speechFocusPage = new ProgramDetailParamEditSpeechFocusPage();
            Assert.AreEqual(initialMusicProgramSpeechFocus, speechFocusPage.GetSelectedSpeechFocus(), "Mismatch between expected and actual SpeechFocus Settings");
            speechFocusPage.Close();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), "Unable to close the speech focus settings view");

            //check NoiseReduction Default Setting
            Output.TestStep("check NoiseReduction Setting in second Program", 2);
            detailPage.NoiseReductionDisplay.OpenSettings();
            noisePage = new ProgramDetailParamEditNoiseReductionPage();
            Assert.AreEqual(initialMusicProgramNoiseReduction, noisePage.GetSelectedNoiseReduction(), "Mismatch between expected and actual NoiseReduction Settings");
            noisePage.Close();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), "Unable to close the noise reduction settings view");


            //Check Tinnitus Detail Settings in second(Musik) Programm
            Output.TestStep("Check Tinnitus Switch State in Detail Page of second Programm", 2);
            detailPage.TinnitusDisplay.OpenSettings();
            tinnitusPage = new ProgramDetailParamEditTinnitusPage();
            tinnitusPage.TurnOnTinnitus();
            Assert.AreEqual(initialMusicProgramTinnitusLowEq, tinnitusPage.GetEqualizerSliderValue(EqBand.Low), "Mismatch between selected and actual Settings for Low Equalizer in Tinnitus Detail Settings");
            Assert.AreEqual(initialMusicProgramTinnitusMidEq, tinnitusPage.GetEqualizerSliderValue(EqBand.Mid), "Mismatch between selected and actual Settings for Mid Equalizer in Tinnitus Detail Settings");
            Assert.AreEqual(initialMusicProgramTinnitusHighEq, tinnitusPage.GetEqualizerSliderValue(EqBand.High), "Mismatch between selected and actual Settings for High Equalizer in Tinnitus Detail Settings");
            Assert.AreEqual(initialMusicProgramTinnitusVolume, tinnitusPage.GetVolumeSliderValue(), "Mismatch between selected and actual Settings for Volume Slider in Tinnitus Detail Settings");
            tinnitusPage.Close();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), "Unable to close the tinnitus settings view");

            //Check Equalizer Default Setting
            Output.TestStep("check Equalizer Setting in second Program", 2);
            detailPage.EqualizerDisplay.OpenSettings();
            equalizerPage = new ProgramDetailParamEditEqualizerPage();
            Assert.AreEqual(initialMusicProgramEqualizerLowEq, equalizerPage.GetEqualizerSliderValue(EqBand.Low), "Mismatch between expecteded and actual Settings for Low Equalizer in Equalizer Detail Settings");
            Assert.AreEqual(initialMusicProgramEqualizerMidEq, equalizerPage.GetEqualizerSliderValue(EqBand.Mid), "Mismatch between expecteded and actual Settings for Mid Equalizer in Equalizer Detail Settings");
            Assert.AreEqual(initialMusicProgramEqualizerHighEq, equalizerPage.GetEqualizerSliderValue(EqBand.High), "Mismatch between expecteded and actual Settings for High Equalizer in Equalizer Detail Settings");
            //Navigating back to Dashboard
            equalizerPage.Close();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), "Unable to close the equalizer settings view");

            //
            //
            //
            //Check Settings in Favorite after App Restart
            Output.TestStep("Check all Favorit settings after App Restart");
            Output.TestStep("Check Favorite Program Settings after App Restart", 2);
            detailPage.SelectProgram(2);
            StringAssert.AreEqualIgnoringCase(myFavoriteName, detailPage.GetCurrentProgramName(), "Expected favorite to be selected, but the name of the current program doesn't match the favorite's name.");
            Assert.IsTrue(detailPage.GetIsSpeechFocusDisplayVisible(), "SpeechFocusSettings do not exist");
            Assert.IsTrue(detailPage.GetIsNoiseReductionDisplayVisible(), "NoiseReductionSettings do not exist");
            Assert.IsTrue(detailPage.GetIsTinnitusDisplayVisible(), "TinnitusSettings do not exist");
            Assert.IsTrue(detailPage.GetIsEqualizerDisplayVisible(), "EqualizerSettings do not exist");
            Assert.IsFalse(detailPage.GetIsBinauralSettingsButtonVisible(), "BinauralSettings exist in monaural mode");
            Assert.IsFalse(detailPage.GetIsLeftHearingDeviceVisible(), "LeftHearingAidInformation exists, but should not have been connected");
            Assert.IsTrue(detailPage.GetIsRightHearingDeviceVisible(), "RightHearingAidInformation does not exist");

            //check SpeechFocus Default Setting
            Output.TestStep("check SpeechFocus Setting in Favorite Program", 2);
            detailPage.SpeechFocusDisplay.OpenSettings();
            speechFocusPage = new ProgramDetailParamEditSpeechFocusPage();
            Assert.AreEqual(SpeechFocus.Auto, speechFocusPage.GetSelectedSpeechFocus(), "Mismatch between expected and actual SpeechFocus Settings");
            speechFocusPage.Close();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), "Unable to close the speech focus settings view");


            //check NoiseReduction Default Setting
            Output.TestStep("check NoiseReduction Setting in Favorite Program", 2);
            detailPage.NoiseReductionDisplay.OpenSettings();
            noisePage = new ProgramDetailParamEditNoiseReductionPage();
            Assert.AreEqual(NoiseReduction.Strong, noisePage.GetSelectedNoiseReduction(), "Mismatch between expected and actual NoiseReduction Settings");
            noisePage.Close();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), "Unable to close the noise reduction settings view");


            //Check Tinnitus Detail Settings in Favorit Programm
            Output.TestStep("Check Tinnitus Switch State in Detail Page in Favorite Program", 2);
            detailPage.TinnitusDisplay.OpenSettings();
            tinnitusPage = new ProgramDetailParamEditTinnitusPage();
            tinnitusPage.TurnOnTinnitus();
            Assert.AreEqual(1.0, tinnitusPage.GetEqualizerSliderValue(EqBand.Low), 0.1, "Mismatch between selected and actual Settings for Low Equalizer in Tinnitus Detail Settings");
            Assert.AreEqual(1.0, tinnitusPage.GetEqualizerSliderValue(EqBand.Mid), 0.1, "Mismatch between selected and actual Settings for Mid Equalizer in Tinnitus Detail Settings");
            Assert.AreEqual(1.0, tinnitusPage.GetEqualizerSliderValue(EqBand.High), 0.1, "Mismatch between selected and actual Settings for High Equalizer in Tinnitus Detail Settings");
            Assert.AreEqual(1.0, tinnitusPage.GetVolumeSliderValue(), 0.1, "Mismatch between selected and actual Settings for Volume Slider in Tinnitus Detail Settings");
            tinnitusPage.Close();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), "Unable to close the tinnitus settings view");

            //Check Equalizer Default Setting
            Output.TestStep("check Equalizer Setting in Favorite Program", 2);
            detailPage.EqualizerDisplay.OpenSettings();
            equalizerPage = new ProgramDetailParamEditEqualizerPage();
            Assert.AreEqual(1.0, equalizerPage.GetEqualizerSliderValue(EqBand.Low), 0.1, "Mismatch between expecteded and actual Settings for Low Equalizer in Equalizer Detail Settings");
            Assert.AreEqual(1.0, equalizerPage.GetEqualizerSliderValue(EqBand.Mid), 0.1, "Mismatch between expecteded and actual Settings for Mid Equalizer in Equalizer Detail Settings");
            Assert.AreEqual(1.0, equalizerPage.GetEqualizerSliderValue(EqBand.High), 0.1, "Mismatch between expecteded and actual Settings for High Equalizer in Equalizer Detail Settings");
            //Navigating back to Dashboard
            equalizerPage.Close();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), "Unable to close the equalizer settings view");
        }

        [Test]
        [Category("SystemTest0004")]
        //[TestCase(TestName = "ST0004")]
        public void ST0004_DemoModus_verwenden()
        {
            AndroidPhone androidPhone = SelectPhone.Android(AndroidPhoneName.Audifon_Nokia_7_plus);
        //   IosPhone iosPhone = SelectPhone.Ios(IosPhoneName.Audifon_iPhone_7);

            AppManager.InitializeApp(new AppConfig(CurrentPlatform));
            AppManager.StartApp(true);
            AppManager.DeviceSettings.EnableBluetooth();

            void AssertHearingDeviceType(string leftType = null, string rightType = null)
            {
                string expectedType = HearingAidModels.GetName(HearingAidModel.LewiS, AppManager.Brand);
                if (leftType != null)
                    Assert.AreEqual(expectedType, leftType, "Wrong device type in demo mode (left side).");
                if (rightType != null)
                    Assert.AreEqual(expectedType, rightType, "Wrong device type in demo mode (right side).");
            }

            Output.Immediately("The app has started.");
            Output.TestStep("Navigating to dashboard in demo mode");
            //(var dashboardPage, var launchLog) = LaunchHelper.StartAppInDemoModeByAnyMeans();
            (var dashboardPage, var launchLog) = LaunchHelper.StartAppInDemoModeForTheFirstTime();

            // Assert.GreaterOrEqual(launchLog.AllowedPermissions, 1, "Expected the app to start for the first time, but no permission was requested during launch to dashboard.");

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

            //Navigating to settings menu
            dashboardPage.OpenMenuUsingSwipe();
            Output.TestStep("Testing settings menu:");
            new MainMenuPage().OpenSettings();
            var settingsPage = new SettingsMenuPage();

            //Testing My Hearing Aids
            Output.TestStep("Testing 'My Hearing Aids'", 2);
            settingsPage.OpenMyHearingAids();
            var aidsPage = new HearingSystemManagementPage();
            aidsPage.LeftDeviceTabClick();
            Assert.IsTrue(aidsPage.GetIsLeftTabSelected(), "My Hearing Aids didn't show a left device in demo mode.");
            Assert.AreEqual("Demo HG#0003400", aidsPage.GetLeftDeviceName(), "Wrong device name in demo mode (left side).");
            AssertHearingDeviceType(aidsPage.GetLeftDeviceType());
            aidsPage.RightDeviceTabClick();
            Assert.IsTrue(aidsPage.GetIsRightTabSelected(), "My Hearing Aids didn't show a right device in demo mode.");
            Assert.AreEqual("Demo HG#0003401", aidsPage.GetRightDeviceName(), "Wrong device name in demo mode (right side).");
            AssertHearingDeviceType(aidsPage.GetRightDeviceType());
            aidsPage.SwipeBack();
            Assert.IsTrue(settingsPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), $"Unsuccessful back navigation using swipe on {nameof(HearingSystemManagementPage)}.");

            //Testing Permissions
            Output.TestStep("Testing 'Permissions'", 2);
            settingsPage.OpenPermissions();
            var permissionsPage = new SettingPermissionsPage();
            Assert.IsTrue(permissionsPage.GetIsLocationPermissionSwitchChecked(), "Location permission toggle switch was not pre-checked in demo mode.");
            permissionsPage.ToggleLocationSwitch();
            Assert.IsFalse(permissionsPage.GetIsLocationPermissionSwitchChecked(), "Unable to toggle location permission switch.");
            permissionsPage.ToggleLocationSwitch();
            Assert.IsTrue(permissionsPage.GetIsLocationPermissionSwitchChecked(), "Unable to toggle location permission switch.");
            permissionsPage.SwipeBack();
            Assert.IsTrue(settingsPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), $"Unsuccessful back navigation using swipe on {nameof(SettingPermissionsPage)}.");

            //Testing Language
            Output.TestStep("Testing 'Language'", 2);
            string brand = AppConfig.DefaultBrand.ToString();
            
                settingsPage.OpenLanguage();
            var languagePage = new SettingLanguagePage();
            //var initialLanguage = languagePage.GetCurrentLanguage(); //old: used before separation between Kind and Audifit Language testing
            //TODO: Check for the correct language. German?
            //Assert.AreEqual(Language.German, initialLanguage, "Wrong language pre-selected.");
            switch (brand)
            {
                case "Audifon":
                    var languagesAudifon = new HashSet<Language_Audifon>((Language_Audifon[])Enum.GetValues(typeof(Language_Audifon)));
                    var initialLanguageAudifon = languagePage.GetCurrentLanguageAudifon();
                    languagesAudifon.Remove(initialLanguageAudifon);
                    foreach (var l in languagesAudifon)
                    {
                        languagePage.SelectLanguageAudifon(l);
                        Assert.AreEqual(l, languagePage.GetSelectedLanguageAudifon(), $"Unable to select language {l}.");
                    }
                    languagePage.Accept();
                    Assert.IsTrue(DialogHelper.DenyIfDisplayed(), $"The accept button doesn't seem to work on {nameof(SettingLanguagePage)}.");
                    languagePage.SelectLanguageAudifon(initialLanguageAudifon);
                    Assert.AreEqual(initialLanguageAudifon, languagePage.GetSelectedLanguageAudifon(), $"Unable to select language {initialLanguageAudifon}.");
                    break;

                case "Kind":
                    var languagesKind = new HashSet<Language>((Language[])Enum.GetValues(typeof(Language)));
                    var initialLanguage = languagePage.GetCurrentLanguage();
                    languagesKind.Remove(initialLanguage);
                    foreach (var l in languagesKind)
                    {
                        languagePage.SelectLanguage(l);
                        Assert.AreEqual(l, languagePage.GetSelectedLanguage(), $"Unable to select language {l}.");
                    }
                    languagePage.Accept();
                    Assert.IsTrue(DialogHelper.DenyIfDisplayed(), $"The accept button doesn't seem to work on {nameof(SettingLanguagePage)}.");
                    languagePage.SelectLanguage(initialLanguage);
                    Assert.AreEqual(initialLanguage, languagePage.GetSelectedLanguage(), $"Unable to select language {initialLanguage}.");
                    break;

                case "PersonaMedical":
                    var languagesPersona = new HashSet<Language_Persona>((Language_Persona[])Enum.GetValues(typeof(Language_Persona)));
                    var initialLanguagePersona = languagePage.GetCurrentLanguagePersona();
                    languagesPersona.Remove(initialLanguagePersona);
                    foreach (var l in languagesPersona)
                    {
                        languagePage.SelectLanguagePersona(l);
                        Assert.AreEqual(l, languagePage.GetSelectedLanguagePersona(), $"Unable to select language {l}.");
                    }
                    languagePage.Accept();
                    Assert.IsTrue(DialogHelper.DenyIfDisplayed(), $"The accept button doesn't seem to work on {nameof(SettingLanguagePage)}.");
                    languagePage.SelectLanguagePersona(initialLanguagePersona);
                    Assert.AreEqual(initialLanguagePersona, languagePage.GetSelectedLanguagePersona(), $"Unable to select language {initialLanguagePersona}.");
                    break;

                case "Puretone":
                    var languagesPuretone = new HashSet<Language_Puretone>((Language_Puretone[])Enum.GetValues(typeof(Language_Puretone)));
                    var initialLanguagePuretone = languagePage.GetCurrentLanguagePuretone();
                    languagesPuretone.Remove(initialLanguagePuretone);
                    foreach (var l in languagesPuretone)
                    {
                        languagePage.SelectLanguagePuretone(l);
                        Assert.AreEqual(l, languagePage.GetSelectedLanguagePuretone(), $"Unable to select language {l}.");
                    }
                    languagePage.Accept();
                    Assert.IsTrue(DialogHelper.DenyIfDisplayed(), $"The accept button doesn't seem to work on {nameof(SettingLanguagePage)}.");
                    languagePage.SelectLanguagePuretone(initialLanguagePuretone);
                    Assert.AreEqual(initialLanguagePuretone, languagePage.GetSelectedLanguagePuretone(), $"Unable to select language {initialLanguagePuretone}.");
                    break;
                case "Hormann":
                    var languagesHormann = new HashSet<Language_Hormann>((Language_Hormann[])Enum.GetValues(typeof(Language_Hormann)));
                    var initialLanguageHormann = languagePage.GetCurrentLanguageHormann();
                    languagesHormann.Remove(initialLanguageHormann);
                    foreach (var l in languagesHormann)
                    {
                        languagePage.SelectLanguageHormann(l);
                        Assert.AreEqual(l, languagePage.GetSelectedLanguageHormann(), $"Unable to select language {l}.");
                    }
                    languagePage.Accept();
                    Assert.IsTrue(DialogHelper.DenyIfDisplayed(), $"The accept button doesn't seem to work on {nameof(SettingLanguagePage)}.");
                    languagePage.SelectLanguageHormann(initialLanguageHormann);
                    Assert.AreEqual(initialLanguageHormann, languagePage.GetSelectedLanguageHormann(), $"Unable to select language {initialLanguageHormann}.");
                    break;
                case "RxEarsPro":
                    var languagesRxEarsPro = new HashSet<Language_RxEarsPro>((Language_RxEarsPro[])Enum.GetValues(typeof(Language_RxEarsPro)));
                    var initialLanguageRxEarsPro = languagePage.GetCurrentLanguageRxEarsPro();
                    languagesRxEarsPro.Remove(initialLanguageRxEarsPro);
                    foreach (var l in languagesRxEarsPro)
                    {
                        languagePage.SelectLanguageRxEarsPro(l);
                        Assert.AreEqual(l, languagePage.GetSelectedLanguageRxEarsPro(), $"Unable to select language {l}.");
                    }
                    languagePage.Accept();
                    // Since this brand has only one language the Accept button will not be enabled
                    //Assert.IsTrue(DialogHelper.DenyIfDisplayed(), $"The accept button doesn't seem to work on {nameof(SettingLanguagePage)}.");
                    languagePage.SelectLanguageRxEarsPro(initialLanguageRxEarsPro);
                    Assert.AreEqual(initialLanguageRxEarsPro, languagePage.GetSelectedLanguageRxEarsPro(), $"Unable to select language {initialLanguageRxEarsPro}.");
                    break;
            }
            /*
             * Block: old implementation or if no separation between audifon and KIND Language selection is needed
            //languages.Remove(initialLanguage);
            //foreach (var l in languages)
            //{
            //    languagePage.SelectLanguage(l);
            //    Assert.AreEqual(l, languagePage.GetSelectedLanguage(), $"Unable to select language {l}.");
            //}
            //languagePage.Accept();
            //Assert.IsTrue(DialogHelper.DenyIfDisplayed(), $"The accept button doesn't seem to work on {nameof(SettingLanguagePage)}.");
            //languagePage.SelectLanguage(initialLanguage);
            //Assert.AreEqual(initialLanguage, languagePage.GetSelectedLanguage(), $"Unable to select language {initialLanguage}.");
            */
            languagePage.SwipeBack();
            Assert.IsTrue(settingsPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), $"Unsuccessful back navigation using swipe on {nameof(SettingLanguagePage)}.");

            //Testing Demo Mode
            Output.TestStep("Testing 'Demo Mode'", 2);
            settingsPage.OpenDemoMode();
            var demoPage = new AppModeSelectionPage();
            Assert.AreEqual(AppMode.Demo, demoPage.GetSelectedAppMode(), $"Pre-selected app mode was not {AppMode.Demo} in demo mode.");
            demoPage.SelectAppMode(AppMode.Normal);
            Assert.AreEqual(AppMode.Normal, demoPage.GetSelectedAppMode(), "Unable to select an app mode.");
            demoPage.Accept();
            Assert.IsTrue(DialogHelper.DenyIfDisplayed(), $"The accept button doesn't seem to work on {nameof(AppModeSelectionPage)}.");
            demoPage.SelectAppMode(AppMode.Demo);
            Assert.AreEqual(AppMode.Demo, demoPage.GetSelectedAppMode(), "Unable to select an app mode.");
            demoPage.SwipeBack();
            Assert.IsTrue(settingsPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), $"Unsuccessful back navigation using swipe on {nameof(AppModeSelectionPage)}.");

            //Navigating back to main menu
            settingsPage.SwipeBack();

            //Navigating to help menu
            Output.TestStep("Testing help menu items:");
            new MainMenuPage().OpenHelp();
            var helpPage = new HelpMenuPage();

            //Testing Find Hearing Devices
            Output.TestStep("Testing 'Hearing Devices Find'", 2);
            helpPage.OpenFindHearingDevices();
            PermissionHelper.AllowPermissionIfRequested<FindDevicesPage>();
            var findPage = new FindDevicesPage();
            Assert.IsTrue(findPage.GetIsMapViewSelected(), "Map view was not pre-selected.");
            Assert.IsTrue(findPage.GetIsLeftDeviceSelected(), "Left devices was not pre-selected.");
            findPage.SelectRightDevice();
            Assert.IsTrue(findPage.GetIsRightDeviceSelected(), "Couldn't switch to right device.");
            findPage.SelectNearFieldView();
            Assert.IsTrue(findPage.GetIsNearFieldViewSelected(), "Couldn't switch to near-field view.");

            //findPage.SwipeBack(); //SwipeBack does not work on this page.
            //Assert.IsTrue(helpPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), $"Unsuccessful back navigation using swipe on {nameof(FindDevicesPage)}.");
            findPage.TapBack(); //HACK: Using Tap instead
            Assert.IsTrue(helpPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), $"Unsuccessful back navigation using tap on {nameof(FindDevicesPage)}.");

            //Testing Help Topics
            Output.TestStep("Testing 'Help Topics'", 2);
            helpPage.OpenHelpTopics();
            var topicsPage = new HelpTopicsPage();
            void TestHelpTopicPage(BaseNavigationPage page)
            {
                page.SwipeBack();
                Assert.IsTrue(topicsPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), $"Unsuccessful back navigation using tap on {page.GetType().Name}.");
            }
            topicsPage.OpenConnectHearingAids();
            TestHelpTopicPage(new ConnectHearingAidsHelpPage());
            topicsPage.OpenDisconnectHearingAids();
            TestHelpTopicPage(new DisconnectHearingAidsHelpPage());
            topicsPage.OpenHomePage();
            TestHelpTopicPage(new MainPageHelpPage());
            topicsPage.OpenProgramSelection();
            TestHelpTopicPage(new ProgramSelectionHelpPage());
            topicsPage.OpenBinauralSeparation();
            TestHelpTopicPage(new DisconnectVolumeControlHelpPage());
            topicsPage.OpenAutomaticProgram();
            TestHelpTopicPage(new AutomaticProgramHelpPage());
            topicsPage.OpenStreamingProgram();
            TestHelpTopicPage(new StreamingProgramHelpPage());
            topicsPage.OpenFavorites();
            TestHelpTopicPage(new FavoritesHelpPage());
            topicsPage.OpenMainMenu();
            TestHelpTopicPage(new MainMenuHelpPage());
            topicsPage.SwipeBack();
            Assert.IsTrue(helpPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), $"Unsuccessful back navigation using swipe on {nameof(HelpTopicsPage)}.");

            //Testing Instruction Manual
            Output.TestStep("Testing 'Instruction Manual'");
            helpPage.OpenInstructionsForUse();
            var manualPage = new InstructionsForUsePage();
            //TODO: Add checks once the page exists.
            manualPage.SwipeBack();
            Assert.IsTrue(helpPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), $"Unsuccessful back navigation using swipe on {nameof(InstructionsForUsePage)}.");

            //Testing Information Menu
            Output.TestStep("Testing 'Menu Information");
            helpPage.OpenInformationMenu();
            var informationMenuPage = new InformationMenuPage();

            Output.TestStep("Testing 'Privacyploicy'", 2);
            informationMenuPage.OpenPrivacyPolicy();
            var privacyPage = new PrivacyPolicyPage();
            //TODO: Add checks once the page exists.
            privacyPage.SwipeBack();
            Assert.IsTrue(informationMenuPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), $"Unsuccessful back navigation using swipe on {nameof(PrivacyPolicyPage)}.");

            //Testing Terms of Use
            Output.TestStep("Testing 'Terms of Use'", 2);
            informationMenuPage.OpenTermsofUse();
            var termsOfUsePage = new TermsOfUsePage();
            //TODO: Add checks once the page exists.
            termsOfUsePage.SwipeBack();
            Assert.IsTrue(informationMenuPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), $"Unsuccessful back navigation using swipe on {nameof(PrivacyPolicyPage)}.");

            //Testing Licenses
            Output.TestStep("Testing 'Licenses'", 2);
            informationMenuPage.OpenLicenses();
            var licensesPage = new LicencesPage();
            //TODO: Add checks once the page exists.
            licensesPage.SwipeBack();
            Assert.IsTrue(informationMenuPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), $"Unsuccessful back navigation using swipe on {nameof(LicencesPage)}.");
            informationMenuPage.SwipeBack();
            Assert.IsTrue(helpPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), $"Unsuccessful back navigation using swipe on {nameof(InformationMenuPage)}.");

            //Testing Imprint
            Output.TestStep("Testing 'Imprint'", 2);
            helpPage.OpenImprint();
            var imprintPage = new ImprintPage();
            Assert.IsNotEmpty(imprintPage.GetAddressHeader(), $"{nameof(imprintPage.GetAddressHeader)} was empty.");
            Assert.IsNotEmpty(imprintPage.GetAppCommpanyState(), $"{nameof(imprintPage.GetAppCommpanyState)} was empty.");
            if (brand == "Audifon")
                Assert.AreEqual("Germany", imprintPage.GetAppCommpanyState(), "Wrong App Company state.");
            Assert.IsNotEmpty(imprintPage.GetAppCompanyName(), $"{nameof(imprintPage.GetAppCompanyName)} was empty.");
            Assert.IsNotEmpty(imprintPage.GetAppCompanyPostalCodeCity(), $"{nameof(imprintPage.GetAppCompanyPostalCodeCity)} was empty.");
            Assert.IsNotEmpty(imprintPage.GetAppCompanyStreet(), $"{nameof(imprintPage.GetAppCompanyStreet)} was empty.");
            Assert.IsNotEmpty(imprintPage.GetAppHeader(), $"{nameof(imprintPage.GetAppHeader)} was empty.");
            // Assert.IsNotEmpty(imprintPage.GetBuildNumber(), $"{nameof(imprintPage.GetBuildNumber)} was empty.");
            if (brand != "PersonaMedical" && brand != "RxEarsPro")
            {
                Assert.IsNotEmpty(imprintPage.GetManufacturerStaticLabel(), $"{nameof(imprintPage.GetManufacturerStaticLabel)} was empty.");
                Assert.AreEqual("audifon GmbH & Co. KG", imprintPage.GetManufacturerCompanyName(), "Wrong manufacturer company name.");
                Assert.AreEqual("Werner-von-Siemens-Str. 2", imprintPage.GetManufacturerCompanyStreet(), "Wrong manufacturer street.");
                Assert.AreEqual("D-99625 Kölleda", imprintPage.GetManufacturerPostalCodeCity(), "Wrong manufacturer postal code / city.");
            }
            Assert.IsNotEmpty(imprintPage.GetSupportDescription(), $"{nameof(imprintPage.GetSupportDescription)} was empty.");
            Assert.IsNotEmpty(imprintPage.GetSupportHeader(), $"{nameof(imprintPage.GetSupportHeader)} was empty.");
            Assert.IsNotEmpty(imprintPage.GetSupportWebsite(), $"{nameof(imprintPage.GetSupportWebsite)} was empty.");
            Assert.IsTrue(imprintPage.GetVersion().StartsWith("Version: 1"), "Version was expected to start with 'Version 1'.");//line used for evaluation of testcase work
            //TODO: Update version number to be verified, use line 635 for testing of release verion
            //Assert.IsTrue(imprintPage.GetVersion().StartsWith("Version 1"), "Version was expected to start with 'Version 1'.");
            imprintPage.SwipeBack();
            Assert.IsTrue(helpPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), $"Unsuccessful back navigation using swipe on {nameof(ImprintPage)}.");

            //Navigating back to the dashboard
            helpPage.SwipeBack();
            new MainMenuPage().CloseMenuUsingSwipe();
            Assert.IsTrue(dashboardPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), $"Unsuccessful back navigation using swipe on {nameof(MainMenuPage)}.");

            //Testing the dashboard
            Output.TestStep("Testing the dashboard:");

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

            //Testing the left hearing aid page
            Output.TestStep("Testing the left hearing aid info page from dashboard", 2);
            dashboardPage.OpenLeftHearingDevice();
            var devicePage = new HearingInstrumentInfoControlPage();
            Assert.AreEqual("Demo HG#0003400", devicePage.GetDeviceName(), "Wrong device name in demo mode (left side).");
            AssertHearingDeviceType(leftType: devicePage.GetDeviceType());
            devicePage.Close();
            Assert.IsTrue(dashboardPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), $"Unsuccessful navigation using close button on {nameof(HearingInstrumentInfoControlPage)}.");

            //Testing the right hearing aid page
            Output.TestStep("Testing the right hearing aid info page from dashboard", 2);
            dashboardPage.OpenRightHearingDevice();
            devicePage = new HearingInstrumentInfoControlPage();
            Assert.AreEqual("Demo HG#0003401", devicePage.GetDeviceName(), "Wrong device name in demo mode (right side).");
            AssertHearingDeviceType(rightType: devicePage.GetDeviceType());
            devicePage.Close();
            Assert.IsTrue(dashboardPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), $"Unsuccessful navigation using close button on {nameof(HearingInstrumentInfoControlPage)}.");

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
            Thread.Sleep(2000);
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

            //Speech focus
            Output.TestStep("Editing the speech focus settings", 2);
            Assert.IsTrue(detailPage.GetIsSpeechFocusDisplayVisible(), "Expected the streaming hearing program to be active, but no speech focus settings were found.");
            detailPage.SpeechFocusDisplay.OpenSettings();
            var speechFocusPage = new ProgramDetailParamEditSpeechFocusPage();
            speechFocusPage.SelectSpeechFocus(SpeechFocus.Front);
            Assert.AreEqual(SpeechFocus.Front, speechFocusPage.GetSelectedSpeechFocus(), "Failed to change the speech focus option.");
            string speechFocusName = speechFocusPage.GetSelectedSpeechFocusName();
            speechFocusPage.Close();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), $"Unsuccessful navigation using close button on {nameof(ProgramDetailParamEditSpeechFocusPage)}.");
            StringAssert.AreEqualIgnoringCase(speechFocusName, detailPage.SpeechFocusDisplay.GetValue(), "Speech focus display doesn't seem to reflect the change that's been made.");

            //Noise reduction
            Output.TestStep("Editing the noise reduction settings", 2);
            Assert.IsTrue(detailPage.GetIsNoiseReductionDisplayVisible(), "Expected the streaming hearing program to be active, but no noise reduction parameter was found.");
            detailPage.NoiseReductionDisplay.OpenSettings();
            var noiseReductionPage = new ProgramDetailParamEditNoiseReductionPage();
            var speechtestfocus = NoiseReduction.Speech;
            noiseReductionPage.SelectNoiseReduction(speechtestfocus);
            Assert.AreEqual(speechtestfocus, noiseReductionPage.GetSelectedNoiseReduction(), "Failed to change the noise reduction option.");
            string noiseReductionName = noiseReductionPage.GetSelectedNoiseReductionName();
            noiseReductionPage.Close();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), $"Unsuccessful navigation using close button on {nameof(ProgramDetailParamEditNoiseReductionPage)}.");
            StringAssert.AreEqualIgnoringCase(noiseReductionName, detailPage.NoiseReductionDisplay.GetValue(), "Noise reduction display doesn't seem to reflect the change that's been made.");

            //Equalizer
            Output.TestStep("Editing the equalizer settings", 2);
            Assert.IsTrue(detailPage.GetIsEqualizerDisplayVisible(), "Expected the streaming hearing program to be active, but no equalizer was found.");
            detailPage.EqualizerDisplay.OpenSettings();
            var equalizerPage = new ProgramDetailParamEditEqualizerPage();
            equalizerPage.SetEqualizerSliderValue(EqBand.Low, .6);
            equalizerPage.SetEqualizerSliderValue(EqBand.Mid, .3);
            equalizerPage.SetEqualizerSliderValue(EqBand.High, .9);
            Assert.AreEqual(0.6, equalizerPage.GetEqualizerSliderValue(EqBand.Low), delta: .1, message: "Failed to swipe the equalizer slider to the target value.");
            Assert.AreEqual(0.3, equalizerPage.GetEqualizerSliderValue(EqBand.Mid), delta: .1, message: "Failed to swipe the equalizer slider to the target value.");
            Assert.AreEqual(0.9, equalizerPage.GetEqualizerSliderValue(EqBand.High), delta: .1, message: "Failed to swipe the equalizer slider to the target value.");
            equalizerPage.Close();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), $"Unsuccessful navigation using close button on {nameof(ProgramDetailParamEditEqualizerPage)}.");
            Assert.AreEqual(0.6, detailPage.EqualizerDisplay.GetEqualizerSliderValue(EqBand.Low), delta: .1, message: "Equalizer display doesn't seem to reflect the changes that've been made.");
            Assert.AreEqual(0.3, detailPage.EqualizerDisplay.GetEqualizerSliderValue(EqBand.Mid), delta: .1, message: "Equalizer display doesn't seem to reflect the changes that've been made.");
            Assert.AreEqual(0.9, detailPage.EqualizerDisplay.GetEqualizerSliderValue(EqBand.High), delta: .1, message: "Equalizer display doesn't seem to reflect the changes that've been made.");

            //Creating a favorite hearing program
            Output.TestStep("Creating a new favorite hearing program");
            detailPage.OpenProgramSettings();
            var programSettingsPage = new ProgramDetailSettingsControlPage();
            programSettingsPage.CreateFavorite();
            new ProgramNamePage().EnterName("My Favorite").Proceed();
            new ProgramIconPage().SelectIcon(3).Proceed();
            new ProgramAutomationPage().Proceed();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), $"Unsuccessful navigation using save button on {nameof(ProgramAutomationPage)}.");

            //Checking the newly createed favorite
            Output.TestStep("Checking the newly createed favorite");
            StringAssert.AreEqualIgnoringCase("My Favorite", detailPage.GetCurrentProgramName(), "Name mismatch between current program and newly created favorite. The new favorite doesn't seem to have been selected.");
            Assert.AreEqual(.75, detailPage.StreamingDisplay.GetSliderValue(), delta: .10, message: "Streaming settings have not been carried over to the new favorite.");
            StringAssert.AreEqualIgnoringCase(speechFocusName, detailPage.SpeechFocusDisplay.GetValue(), "Speech focus settings have not been carried over to the new favorite.");
            StringAssert.AreEqualIgnoringCase(noiseReductionName, detailPage.NoiseReductionDisplay.GetValue(), "Noise reduction settings have not been carried over to the new favorite.");
            Assert.AreEqual(0.6, detailPage.EqualizerDisplay.GetEqualizerSliderValue(EqBand.Low), delta: .1, message: "Equalizer settings have not been carried over to the new favorite.");
            Assert.AreEqual(0.3, detailPage.EqualizerDisplay.GetEqualizerSliderValue(EqBand.Mid), delta: .1, message: "Equalizer settings have not been carried over to the new favorite.");
            Assert.AreEqual(0.9, detailPage.EqualizerDisplay.GetEqualizerSliderValue(EqBand.High), delta: .1, message: "Equalizer settings have not been carried over to the new favorite.");

            //Checking the number of hearing programs (again)
            Output.TestStep("Checking number of hearing programs that are shown on the dashboard");
            detailPage.TapBack();
            Assert.IsTrue(dashboardPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), $"Unsuccessful back navigationusing tap on {nameof(ProgramDetailPage)}.");
            Assert.AreEqual(5, dashboardPage.GetNumberOfPrograms(), "Unexpected number of hearing programs after creating a new favorite.");

            //Restarting the app to check if the settings persist
            Output.TestStep("Restarting the app and expecting to be back to the dashboard");
            AppManager.RestartApp(false);
            dashboardPage = new DashboardPage();
            Output.TestStep("Checking number of hearing programs that are shown on the dashboard (yet again)");
            Assert.AreEqual(5, dashboardPage.GetNumberOfPrograms(), "Unexpected number of hearing programs after creating a new favorite and restarting the app.");

            for (int i = 4; i >= 0; i--)
            {
                dashboardPage.SelectProgram(i);
                if (dashboardPage.GetCurrentProgramName().ToLower().Equals("my favorite"))
                    break;
            }
            dashboardPage.OpenCurrentProgram();
            detailPage = new ProgramDetailPage();
            StringAssert.AreEqualIgnoringCase("my favorite", detailPage.GetCurrentProgramName(), "Name mismatch between current program and program that should've been opened. This might be an issue within the UI test project itself.");

            //Checking the favorite hearing program after the restart
            Output.TestStep("Checking the favorite hearing program's parameters once again");
            Assert.AreEqual(.75, detailPage.StreamingDisplay.GetSliderValue(), delta: .10, message: "Streaming settings have been lost after restarting the app.");
            StringAssert.AreEqualIgnoringCase(speechFocusName, detailPage.SpeechFocusDisplay.GetValue(), "Speech focus settings have been lost after restarting the app.");
            StringAssert.AreEqualIgnoringCase(noiseReductionName, detailPage.NoiseReductionDisplay.GetValue(), "Noise reduction settings have been lost after restarting the app.");
            Assert.AreEqual(0.6, detailPage.EqualizerDisplay.GetEqualizerSliderValue(EqBand.Low), delta: .1, message: "Equalizer settings have been lost after restarting the app.");
            Assert.AreEqual(0.3, detailPage.EqualizerDisplay.GetEqualizerSliderValue(EqBand.Mid), delta: .1, message: "Equalizer settings have been lost after restarting the app.");
            Assert.AreEqual(0.9, detailPage.EqualizerDisplay.GetEqualizerSliderValue(EqBand.High), delta: .1, message: "Equalizer settings have been lost after restarting the app.");
        }

        [Test]
        [Category("SystemTest0005")]
        //[TestCase(TestName = "ST0005")]
        public void ST0005_Menue_verwenden()
        {
            AndroidPhone androidPhone = SelectPhone.Android(AndroidPhoneName.Audifon_Galaxy_S7);
            IosPhone iosPhone = SelectPhone.Ios(IosPhoneName.Audifon_iPhone_7_Plus);

            AppManager.InitializeApp(new AppConfig(CurrentPlatform, androidPhone, iosPhone));
            AppManager.StartApp(true);
			AppManager.DeviceSettings.EnableBluetooth();

            HearingAid leftAid = SelectHearingAid.Left(LeftHearingAid.Audifon_ST0005_HG_030644);
            HearingAid rightAid = SelectHearingAid.Right(RightHearingAid.Audifon_ST0005_HG_030600);

            //Startup and connection
            Output.Immediately("The app has started.");
            Output.TestStep("Connecting hearing aids and proceeding to dashboard");
            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(leftAid, rightAid).Page;

            //On dashboard
            Output.TestStep("Checking number of hearing programs");
            Assert.AreEqual(2, dashboardPage.GetNumberOfPrograms(), "The total number of hearing programs doesn't meet the preconditions of this test.");
            string initialProgramName = dashboardPage.GetCurrentProgramName();

            //Testing the hearing program menu
            Output.TestStep("Testing hearing program menu:");
            Output.TestStep("Navigating to the view", 2);
            dashboardPage.OpenMenuUsingSwipe();
            new MainMenuPage().OpenPrograms();
            var programPage = new ProgramsMenuPage();
            Output.TestStep("Checking the content of the view", 2);
            List<string> programNames = programPage.GetAllProgramNames();
            Assert.AreEqual(2, programNames.Count, "The total number of hearing programs doesn't meet the preconditions of this test.");
            Output.TestStep("Changing the hearing program", 2);
            string newProgramName = null;
            bool hasSelectedProgram = false;
            for (int i = 0; i < programNames.Count; i++)
            {
                if (programNames[i].ToLower() != initialProgramName.ToLower())
                {
                    newProgramName = programNames[i];
                    programPage.SelectProgram(i);
                    hasSelectedProgram = true;
                    break;
                }
            }
            Assert.IsTrue(hasSelectedProgram, "Failed to select a hearing program. This happens if all programs have the same name.");
            Output.TestStep("Verifying on the dashboard that the program has indeed changed", 2);
            Assert.IsTrue(dashboardPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)), "Changing the hearing program from the hearing program menu didn't navigate us to the dashboard within 10 seconds.");
            Thread.Sleep(3500);
            StringAssert.AreEqualIgnoringCase(newProgramName, dashboardPage.GetCurrentProgramName(), "The name of the current hearing program on the dashboard page doesn't match the name of the one that's been selected.");

            //Testing 'Hearing Devices Find'
            Output.TestStep("Testing 'Hearing Devices Find':");
            Output.TestStep("Navigating to the view", 2);
            dashboardPage.OpenMenuUsingSwipe();
            new MainMenuPage().OpenHelp();
            new HelpMenuPage().OpenFindHearingDevices();
            var findPage = new FindDevicesPage();
            Output.TestStep("Checking the content of the view", 2);
            Assert.IsTrue(findPage.GetIsMapViewSelected(), "Map view was not pre-selected.");
            Assert.IsTrue(findPage.GetIsLeftDeviceSelected(), "Left devices was not pre-selected.");
            findPage.SelectRightDevice();
            Assert.IsTrue(findPage.GetIsRightDeviceSelected(), "Couldn't switch to right device on map view.");
            findPage.SelectNearFieldView();
            Assert.IsTrue(findPage.GetIsNearFieldViewSelected(), "Couldn't switch to near-field view.");
            Assert.IsTrue(findPage.GetIsRightSignalStrengthControlVisible(), "Right signal strength indicator is not shown on near-field search view.");
            findPage.SelectLeftDevice();
            Assert.IsTrue(findPage.GetIsLeftDeviceSelected(), "Couldn't switch to left device on near-field search view.");
            Assert.IsTrue(findPage.GetIsLeftSignalStrengthControlVisible(), "Left signal strength indicator is not shown on near-field search view.");
            findPage.SelectMapView();
            Assert.IsTrue(findPage.GetIsMapViewSelected(), "Couldn't switch to back to map view.");
            Output.TestStep("Navigating back to the dashboard", 2);
            findPage.TapBack();
            new HelpMenuPage().SwipeBack();
            new MainMenuPage().CloseMenuUsingSwipe();
            Assert.IsTrue(dashboardPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), $"Unsuccessful back navigation using swipe on {nameof(MainMenuPage)}.");

            //Testing 'My Hearing Aids'
            Output.TestStep("Testing 'My Hearing Aids':");
            Output.TestStep("Navigating to the view", 2);
            dashboardPage.OpenMenuUsingSwipe();
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenMyHearingAids();
            var devicePage = new HearingSystemManagementPage();
            Output.TestStep("Checking the content of the view", 2);
            devicePage.LeftDeviceTabClick();
            Assert.IsTrue(devicePage.GetIsLeftTabSelected(), "The left device is not shown.");
            Assert.AreEqual(leftAid.Name, devicePage.GetLeftDeviceName(), "Wrong device name (left).");
            //Assert.AreEqual(leftAid.Model.ToBrandSpecific(AppManager.Brand), devicePage.GetLeftDeviceType(), "Wrong device type (left).");
            Assert.AreEqual(HearingAidModels.GetName(leftAid.Model.ToBrandSpecific(AppManager.Brand)), devicePage.GetLeftDeviceType(), "Wrong device type (left).");
            devicePage.RightDeviceTabClick();
            Assert.IsTrue(devicePage.GetIsRightTabSelected(), "The right device is not shown.");
            Assert.AreEqual(rightAid.Name, devicePage.GetRightDeviceName(), "Wrong device name (right).");
            //Assert.AreEqual(rightAid.Model.ToBrandSpecific(AppManager.Brand), devicePage.GetRightDeviceType(), "Wrong device type (right).");
            Assert.AreEqual(HearingAidModels.GetName(rightAid.Model.ToBrandSpecific(AppManager.Brand)), devicePage.GetRightDeviceType(), "Wrong device type (right).");

            //Assert.AreEqual("KINDwings 2200", devicePage.GetLeftDeviceType(), "Wrong device type (left).");
            //Assert.AreEqual("KINDwings 2200", devicePage.GetRightDeviceType(), "Wrong device type (right).");


            Output.TestStep("Disconnecting the devices", 2);
            devicePage.DisconnectDevices();
            Assert.IsTrue(DialogHelper.GetIsDialogDisplayed(TimeSpan.FromSeconds(3)), "Tried to disconnect the devices, but no confirmation dialog showed up.");
            DialogHelper.Confirm();
            Output.TestStep("Verifying that we're on the hardware initialization page", 2);
            new InitializeHardwarePage();
        }
    }

}

