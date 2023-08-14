using System;
using System.Threading;
using HorusUITest.Adapters;
using HorusUITest.Configuration;
using HorusUITest.Data;
using HorusUITest.Enums;
using HorusUITest.Helper;
using HorusUITest.PageObjects;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Controls.ProgramDetailParams;
using HorusUITest.PageObjects.Favorites;
using HorusUITest.PageObjects.Favorites.Automation;
using HorusUITest.PageObjects.Menu;

using HorusUITest.PageObjects.Menu.Info;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Platform = HorusUITest.Enums.Platform;


namespace HorusUITest.TestFixtures.FunctionalTests
{
    public class FavouriteWorkflowTests : BaseResettingTestFixture
    {
        public FavouriteWorkflowTests(Platform platform)
            : base(platform)
        {
        }

        [Test]
        [Category("FunctionalTest")]
       // [Platform("Android")]
        public void CreateFavouriteTest()
        {
#if !TEST_FROM_IDE
            AndroidPhone androidPhone = SelectPhone.Android(AndroidPhoneName.Audifon_Nokia_7_plus);
            IosPhone iosPhone = SelectPhone.Ios(IosPhoneName.Audifon_iPhone_8);

            AppManager.InitializeApp(new AppConfig(CurrentPlatform, androidPhone, iosPhone));
            AppManager.StartApp(true);
#endif

            HearingAid leftHearingAid = SelectHearingAid.Left(LeftHearingAid.Audifon_Functiona_HG_082280);
            HearingAid rightHearingAid = SelectHearingAid.Right(RightHearingAid.Audifon_Functiona_HG_082279);

            void AssertHearingDeviceType(string leftType = null, string rightType = null)
            {
                string expectedType = HearingAidModels.GetName(HearingAidModel.LewiS, AppManager.Brand);
                if (leftType != null)
                    Assert.AreEqual(expectedType, leftType, "Wrong device type in demo mode (left side).");
                if (rightType != null)
                    Assert.AreEqual(expectedType, rightType, "Wrong device type in demo mode (right side).");
            }

            //Startup and connection
            Output.Immediately("The app has started.");
            Output.TestStep("Connecting hearing aids and proceeding to dashboard");
            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(rightHearingAid, leftHearingAid).Page;

            //Check Number of Programs
            Output.TestStep("Checking number of programs on dashboard page");
            Assert.AreEqual(4, dashboardPage.GetNumberOfPrograms(), "Number of programs on Dashboard Page does not meet the precondition for this test");
            var initialProgram = dashboardPage.GetCurrentProgramName();

            //Check information on imprint page
            Output.TestStep("Check informations on imprint page");
            Output.TestStep("Open main menu using tap", 2);
            dashboardPage.OpenMenuUsingTap();
            var mainMenuPage = new MainMenuPage();
            mainMenuPage.OpenHelp();
            Output.TestStep("Navigating to imprint page to check content", 2);
            var helpMenuPage = new HelpMenuPage();
            helpMenuPage.OpenImprint();
            var imprintPage = new ImprintPage();
            Assert.IsNotEmpty(imprintPage.GetAddressHeader(), $"{nameof(imprintPage.GetAddressHeader)} was empty.");
            Assert.IsNotEmpty(imprintPage.GetAppCommpanyState(), $"{nameof(imprintPage.GetAppCommpanyState)} was empty.");
            Assert.IsNotEmpty(imprintPage.GetAppCompanyName(), $"{nameof(imprintPage.GetAppCompanyName)} was empty.");
            Assert.IsNotEmpty(imprintPage.GetAppCompanyPostalCodeCity(), $"{nameof(imprintPage.GetAppCompanyPostalCodeCity)} was empty.");
            Assert.IsNotEmpty(imprintPage.GetAppCompanyStreet(), $"{nameof(imprintPage.GetAppCompanyStreet)} was empty.");
            Assert.IsNotEmpty(imprintPage.GetAppHeader(), $"{nameof(imprintPage.GetAppHeader)} was empty.");
            Assert.IsTrue(imprintPage.GetVersion().StartsWith("Version: 1.1"), "Version was expected to start with 'Version: 1.1'");
            Assert.IsNotEmpty(imprintPage.GetManufacturerStaticLabel(), $"{nameof(imprintPage.GetManufacturerStaticLabel)} was empty.");
            Assert.IsNotEmpty(imprintPage.GetSupportDescription(), $"{nameof(imprintPage.GetSupportDescription)} was empty.");
            Assert.IsNotEmpty(imprintPage.GetSupportHeader(), $"{nameof(imprintPage.GetSupportHeader)} was empty.");
            Assert.IsNotEmpty(imprintPage.GetSupportWebsite(), $"{nameof(imprintPage.GetSupportWebsite)} was empty.");
            Assert.AreEqual("audifon GmbH & Co. KG", imprintPage.GetManufacturerCompanyName(), "Wrong manufacturer company name.");
            Assert.AreEqual("Werner-von-Siemens-Str. 2", imprintPage.GetManufacturerCompanyStreet(), "Wrong manufacturer street.");
            Assert.AreEqual("D-99625 Kölleda", imprintPage.GetManufacturerPostalCodeCity(), "Wrong manufacturer postal code / city.");

            Output.TestStep("Navigate back to dashboard page", 2);
            imprintPage.TapBack();
            Assert.IsTrue(helpMenuPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), $"Could not navigate back to {nameof(HelpMenuPage)} using tap");
            helpMenuPage.TapBack();
            Assert.IsTrue(mainMenuPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), $"Could not navigate back to {nameof(MainMenuPage)} using tap");
            mainMenuPage.CloseMenuUsingTap();
            Assert.IsTrue(dashboardPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), $"Could not navigate back to {nameof(DashboardPage)} using tap");

            //Check hearing system information for connected devices
            Output.TestStep("Testing the right hearing aid info page from dashboard");
            dashboardPage.OpenRightHearingDevice();
            var devicePage = new HearingInstrumentInfoControlPage();
            AssertHearingDeviceType(rightType: devicePage.GetDeviceType());
            devicePage.Close();
            Assert.IsTrue(dashboardPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), $"Unsuccessful navigation using close button on {nameof(HearingInstrumentInfoControlPage)}.");

            //Testing the left hearing aid page
            Output.TestStep("Testing the left hearing aid info page from dashboard");
            dashboardPage.OpenLeftHearingDevice();
            devicePage = new HearingInstrumentInfoControlPage();
            AssertHearingDeviceType(leftType: devicePage.GetDeviceType());
            devicePage.Close();
            Assert.IsTrue(dashboardPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), $"Unsuccessful navigation using close button on {nameof(HearingInstrumentInfoControlPage)}.");

            //Changing the hearing program and check names
            Output.TestStep("Changing the hearing program and verifying that each program has a unique name");
            string lastName = null;
            for (int i = 0; i < 4; i++)
            {
                dashboardPage.SelectProgram(i);
                Assert.IsTrue(dashboardPage.GetIsProgramSelected(i), $"Failed to select a hearing program {i}.");
                string newName = dashboardPage.GetCurrentProgramName();
                Assert.AreNotEqual(lastName, newName, "Duplicate hearing program names found.");
                lastName = newName;
            }
            //re-select first program
            // ToDo: prüfen ob benötigt!
            Output.TestStep("Select first program as active program", 2);
            var firstProgram = dashboardPage.SelectProgram(0).GetCurrentProgramName();
            Assert.AreEqual(initialProgram, firstProgram, "Selected program is not expected initial program with index 0 ");

            //Change volume to 6dB on dashboard page slider
            Output.TestStep("Change volume using button '+' / '-' next to volume control");
            Output.TestStep("Change Volume to 6dB", 2);
            var actualSliderValue = dashboardPage.GetVolumeSliderValue();
            var increasedValue = 0.0; ;
            var decreasedValue = 0.0;
            for (var j = 0; j < 6; j++)
            {
                dashboardPage.IncreaseVolume();
                increasedValue = dashboardPage.GetVolumeSliderValue();
                Assert.AreNotEqual(actualSliderValue, increasedValue, "Volume could not be increased on volume control using '+' button");
                actualSliderValue = dashboardPage.GetVolumeSliderValue();
            }
            //Change volume to -2dB on dashboard page slider
            Output.TestStep("Change Volume to -2dB", 2);
            for (var j = 6; j > -2; j--)
            {
                dashboardPage.DecreaseVolume();
                decreasedValue = dashboardPage.GetVolumeSliderValue();
                Assert.AreNotEqual(actualSliderValue, decreasedValue, $"Volume could not be decreased on volume control using '-' button");
                actualSliderValue = dashboardPage.GetVolumeSliderValue();
            }
            //TODO: Find out if binaural slider could be identified if active
          
            Output.TestStep("Set volume control to initial value 0dB", 2);
            for (var j = -2; j < 0; j++)
            {
                dashboardPage.IncreaseVolume();
                increasedValue = dashboardPage.GetVolumeSliderValue();
                Assert.AreNotEqual(actualSliderValue, increasedValue, "Volume could not be increased on volume control using '+' button again");
                actualSliderValue = dashboardPage.GetVolumeSliderValue();
            }
            //dashboardPage.IncreaseVolume();
            //Assert.AreEqual(0.0, dashboardPage.GetVolumeSliderValue(), 0.05, "Volume could not be set to initial Value 0dB");

            //Change equalizer parameter settings for favorit to be created
            Output.TestStep("Change equalizer parameter settings for favorit to be created");
            dashboardPage.OpenCurrentProgram();
            var detailPage = new ProgramDetailPage();
            detailPage.SelectProgram(1);
            detailPage.EqualizerDisplay.OpenSettings();
            var equalizerSettingsPage = new ProgramDetailParamEditEqualizerPage();
            equalizerSettingsPage.SetEqualizerSliderValue(EqBand.Low, 0.5);
            equalizerSettingsPage.SetEqualizerSliderValue(EqBand.Mid, 0.0);
            equalizerSettingsPage.SetEqualizerSliderValue(EqBand.High, 0.8);
            var lowEq = equalizerSettingsPage.GetEqualizerSliderValue(EqBand.Low);
            var midEq = equalizerSettingsPage.GetEqualizerSliderValue(EqBand.Mid);
            var highEq = equalizerSettingsPage.GetEqualizerSliderValue(EqBand.High);
            Assert.AreEqual(0.5, equalizerSettingsPage.GetEqualizerSliderValue(EqBand.Low), 0.05, "Unable to set the equalizer slider to the desired value (low band)");
            Assert.AreEqual(0.0, equalizerSettingsPage.GetEqualizerSliderValue(EqBand.Mid), 0.05, "Unable to set the equalizer slider to the desired value (mid band)");
            Assert.AreEqual(0.8, equalizerSettingsPage.GetEqualizerSliderValue(EqBand.High), 0.05, "Unable to set the equalizer slider to the desired value (high band)");
            equalizerSettingsPage.Close();
            // ToDo: prüfen weitere Möglichkeit Timeout einzubinden, Page!?
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)), "Writing of program parameters took an unexpected amount of time");

            //Creat favorit for comfort program and select programautomation with location
            Output.TestStep("Create favorit for comfort program with auto program start via location");
            detailPage.OpenProgramSettings();
            var programSettingsPage = new ProgramDetailSettingsControlPage();
            programSettingsPage.CreateFavorite();
            new ProgramNamePage().EnterName("Maske").Proceed();
            new ProgramIconPage().SelectIcon(15).Proceed();
            new ProgramAutomationPage().TurnOnAutomation().GeofenceAutomation.OpenSettings();
            if (PermissionHelper.AllowPermissionIfRequested<AutomationGeofenceBindingPage>())
                Output.TestStep("Granted permission for GPS", 2);
            new AutomationGeofenceBindingPage().WaitUntilNoLoadingIndicator().SelectPosition(0.5, 0.5).Ok();
            Output.TestStep("Save favorite", 2);
            new ProgramAutomationPage().Proceed();

            Output.TestStep("Navigate back to Dashboard Page and check number of available programs");
            new ProgramDetailPage().TapBack();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown(), "Could not navigate from ProgramDetailPage to Dashboard Page");
            Output.TestStep("Verifying that the number of programs is increased by one", 2);
            Assert.AreEqual(5, dashboardPage.GetNumberOfPrograms(), "Number of programs on dashboard page does not match expected number after create favorit");

            //Check program switching and names on dashboard page after creating favorit
            Output.TestStep("Check program switching and names on dashboard is still possible");
            for (int i = 0; i < 5; i++)
            {
                dashboardPage.SelectProgram(i);
                Assert.IsTrue(dashboardPage.GetIsProgramSelected(i), $"Failed to select a hearing program {i}.");
                string newName = dashboardPage.GetCurrentProgramName();
                Assert.AreNotEqual(lastName, newName, "Duplicate hearing program names found.");
                lastName = newName;
            }

            Output.TestStep("Change parameter of Noise Reduction to 'Strong' in favorit program");
            //make sure that selects favorit program as program to be opened
            dashboardPage.SelectProgram(4).OpenCurrentProgram();
            Assert.AreEqual(detailPage.GetCurrentProgramName(), "Maske", "open program is not expected pre-created favorit");
            detailPage.NoiseReductionDisplay.OpenSettings();
            var noiseReductionSettingsPage = new ProgramDetailParamEditNoiseReductionPage();
            noiseReductionSettingsPage.SelectNoiseReduction(NoiseReduction.Strong);
            Thread.Sleep(10);
            noiseReductionSettingsPage.Close();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Writing of program parameters took an unexpected amount of time");
            var temporaryMaskeNoiseReduction = detailPage.NoiseReductionDisplay.GetValue();

            //Adjust Settings for Wireless Streaming program without program automation
            Output.TestStep("Switch to Wireless Streaming program");
            detailPage.SelectProgram(3);
            Output.TestStep("Adjust settings for Wireless Streaming program", 2);
            detailPage.StreamingDisplay.OpenSettings();
            var streamingSettingsPage = new ProgramDetailParamEditStreamingPage();
            streamingSettingsPage.SetStreamingSliderValue(0.0);
            var streamSettings = streamingSettingsPage.GetStreamingSliderValue();
            Assert.AreEqual(0.0, streamingSettingsPage.GetStreamingSliderValue(), 0.05, "Unable to set the streaming slider to the desired value 'Enviroment'");
            Thread.Sleep(10);
            streamingSettingsPage.Close();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Writing of program parameters took an unexpected amount of time");

            //Create favorit for Wireless Streaming program without program automation
            Output.TestStep("Create favorit for Wireless Streaming program without program automation enabled", 2);
            detailPage.OpenProgramSettings();
            programSettingsPage.CreateFavorite();
            new ProgramNamePage().EnterName("Live").Proceed();
            new ProgramIconPage().SelectIcon(8).Proceed();
            new ProgramAutomationPage().Proceed();
            Output.TestStep("Change equalizer Settings after on created favorit program 'Live'", 2);
            detailPage.EqualizerDisplay.OpenSettings();
            equalizerSettingsPage.SetEqualizerSliderValue(EqBand.Low, 0.5);
            equalizerSettingsPage.SetEqualizerSliderValue(EqBand.Mid, 0.2);
            equalizerSettingsPage.SetEqualizerSliderValue(EqBand.High, 0.3);
            lowEq = equalizerSettingsPage.GetEqualizerSliderValue(EqBand.Low);
            midEq = equalizerSettingsPage.GetEqualizerSliderValue(EqBand.Mid);
            highEq = equalizerSettingsPage.GetEqualizerSliderValue(EqBand.High);
            Assert.AreEqual(0.5, equalizerSettingsPage.GetEqualizerSliderValue(EqBand.Low), 0.05, "Unable to set the equalizer slider to the desired value (low band)");
            Assert.AreEqual(0.2, equalizerSettingsPage.GetEqualizerSliderValue(EqBand.Mid), 0.05, "Unable to set the equalizer slider to the desired value (mid band)");
            Assert.AreEqual(0.3, equalizerSettingsPage.GetEqualizerSliderValue(EqBand.High), 0.05, "Unable to set the equalizer slider to the desired value (high band)");
            Thread.Sleep(10);
            equalizerSettingsPage.Close();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Writing of program parameters took an unexpected amount of time");
            detailPage.TapBack();
            Assert.AreEqual(6, dashboardPage.GetNumberOfPrograms(), "Number of programs on dashboard page does not match expected number after second favorit");

            //Check program switching and names on dashboard page again after newly creating favorit
            Output.TestStep("Check program switching and names on dashboard is still possible");
            for (int i = 0; i < 6; i++)
            {
                dashboardPage.SelectProgram(i);
                Assert.IsTrue(dashboardPage.GetIsProgramSelected(i), $"Failed to select a hearing program {i}.");
                string newName = dashboardPage.GetCurrentProgramName();
                Assert.AreNotEqual(lastName, newName, "Duplicate hearing program names found.");
                lastName = newName;
            }

            //Resart App and check if favorits are still standing after reload
            Output.TestStep("Resart App");
            AppManager.RestartApp(false);

            dashboardPage = new DashboardPage();
            Output.TestStep("Wait 10 seconds for Initialization process is done after restarting the app");
            Thread.Sleep(10);
            // ToDo prüfen auf anzeigen popup?
            //reading Number of Programs after App Restart
            Output.TestStep("Check number of programs on dashboard page after restarting the app and initialzation timeout");
            Assert.AreEqual(6, dashboardPage.GetNumberOfPrograms(), "Number of programs does not match expected number after restarting the app");

            //Create third favorit programm again without program automation
            Output.TestStep("Create additional favorit of noise combi program");
            dashboardPage.OpenCurrentProgram();
            detailPage.SelectProgram(2);
            Output.TestStep("Change equalizer settings", 2);
            detailPage.EqualizerDisplay.OpenSettings();
            equalizerSettingsPage.SetEqualizerSliderValue(EqBand.Low, 1.0);
            equalizerSettingsPage.SetEqualizerSliderValue(EqBand.Mid, 0.9);
            equalizerSettingsPage.SetEqualizerSliderValue(EqBand.High, 0.7);
            lowEq = equalizerSettingsPage.GetEqualizerSliderValue(EqBand.Low);
            midEq = equalizerSettingsPage.GetEqualizerSliderValue(EqBand.Mid);
            highEq = equalizerSettingsPage.GetEqualizerSliderValue(EqBand.High);
            Assert.AreEqual(1.0, equalizerSettingsPage.GetEqualizerSliderValue(EqBand.Low), 0.05, "Unable to set the equalizer slider to the desired value (low band)");
            Assert.AreEqual(0.9, equalizerSettingsPage.GetEqualizerSliderValue(EqBand.Mid), 0.05, "Unable to set the equalizer slider to the desired value (mid band)");
            Assert.AreEqual(0.7, equalizerSettingsPage.GetEqualizerSliderValue(EqBand.High), 0.05, "Unable to set the equalizer slider to the desired value (high band)");
            Thread.Sleep(10);
            equalizerSettingsPage.Close();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Writing of program parameters took an unexpected amount of time");


            Output.TestStep("Create favorit with actual settings", 2);
            detailPage.OpenProgramSettings();
            programSettingsPage.CreateFavorite();
            new ProgramNamePage().EnterName("Ruhe").Proceed();
            new ProgramIconPage().SelectIcon(11).Proceed();
            new ProgramAutomationPage().Proceed();

            //after creating favorit change Parameter of third favorit for noise reduction to boost
            Output.TestStep("Change noise reduction in favorit 'Ruhe' to boost", 2);
            detailPage.NoiseReductionDisplay.OpenSettings();
            noiseReductionSettingsPage.SelectNoiseReduction(NoiseReduction.Strong);
            var temporaryRuheNoiseReduction = noiseReductionSettingsPage.GetSelectedNoiseReduction();
            Thread.Sleep(10);
            noiseReductionSettingsPage.Close();
            Assert.IsTrue(detailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "");
            detailPage.TapBack();
            Assert.AreEqual(7, dashboardPage.GetNumberOfPrograms(), "Number of programs on dashboard page does not match expected number after second favorit");

            for (int i = 6; i >= 0; i--)
            {
                dashboardPage.SelectProgram(i);
                Assert.IsTrue(dashboardPage.GetIsProgramSelected(i), $"Failed to select a hearing program {i}.");
                string newName = dashboardPage.GetCurrentProgramName();
                Assert.AreNotEqual(lastName, newName, "Duplicate hearing program names found.");
                lastName = newName;
            }

            //Change volume to 6dB on dashboard page slider
            Output.TestStep("Change volume using button '+' / '-' next to volume control");
            Output.TestStep(" Change Volume to 4dB", 2);
            actualSliderValue = 0.0;
            for (var j = 0; j < 4; j++)
            {
                dashboardPage.IncreaseVolume();
                increasedValue = dashboardPage.GetVolumeSliderValue();
                Assert.AreNotEqual(actualSliderValue, increasedValue, $"Volume could not be decreased on volume control using '+' button");
                actualSliderValue = dashboardPage.GetVolumeSliderValue();
            }
            //Change volume to -2dB on dashboard page slider
            Output.TestStep("Change Volume to 0dB", 2);
            for (var j = 4; j >= 0; j--)
            {
                dashboardPage.DecreaseVolume();
                decreasedValue = dashboardPage.GetVolumeSliderValue();
                Assert.AreNotEqual(actualSliderValue, decreasedValue, $"Volume could not be decreased on volume control using '-' button");
                actualSliderValue = dashboardPage.GetVolumeSliderValue();
            }
            //Restart App again to check if favorites still available after restart
            AppManager.RestartApp(false);

            dashboardPage = new DashboardPage();
            Output.TestStep("Wait 10 seconds for Initialization process is done after restarting the app");
            Thread.Sleep(10);
            //reading Number of Programs after App Restart
            Output.TestStep("Check number of programs on dashboard page after restarting the app and initialzation timeout");
            Assert.AreEqual(7, dashboardPage.GetNumberOfPrograms(), "Number of programs does not match expected number after restarting the app");
        }


        //TODO: diese steps in UI-TestSolution implementieren
        //lege app in hintergrund, hole app in vordergrund...wähle favorit Ruhe check parameter
        //lege app in hintergrund, schalte hg aus und wieder ein, hole app in vordergrund warte 10 sek auf reconnect der hg
        //restart app, warte auf dashboard page warte 10 sek checke programm, trenne erneut verbindung zu beiden und schalte wieder ein, prüfe reconnect, ggf chekce infos auf dashborad

        [Test]
        [Category("FunctionalTest")]
        // [Platform("Android")]
        public void DemoModus_CreateAndDeleteFavouriteTest()
        {
#if !TEST_FROM_IDE
            AndroidPhone androidPhone = SelectPhone.Android(AndroidPhoneName.Audifon_Nokia_7_plus);
            IosPhone iosPhone = SelectPhone.Ios(IosPhoneName.Audifon_iPhone_8);

            AppManager.InitializeApp(new AppConfig(CurrentPlatform, androidPhone, iosPhone));
            AppManager.StartApp(true);
#endif

            //Startup and connection and check hearing programs on dashboard
            Output.Immediately("The app has started.");
            Output.TestStep("Start app in Demo-Mode and proceeding to dashboard");
            LaunchHelper.StartAppInDemoModeForTheFirstTime();
            var dashboardPage = new DashboardPage();
            Output.TestStep("Check number of available hearing programs in 'Demo-Mode'");
            Assert.AreEqual(4,  dashboardPage.GetNumberOfPrograms(),$"Expected number of hearing programs is four but was {dashboardPage.GetNumberOfPrograms()}");

            ////TODO: may add adjusting params to program and app reset without favourite, check if params are set back to default after restart
            
            //Open second program 'Music' and create favourite program 
            Output.TestStep("Create Favourite for second program 'Music'");
            dashboardPage.OpenCurrentProgram();
            var programDetailPage = new ProgramDetailPage();
            programDetailPage.SelectProgram(1);
            var checkSecondProgramName = programDetailPage.GetCurrentProgramName();
            programDetailPage.OpenProgramSettings();
            var programSettingsControlPage = new ProgramDetailSettingsControlPage();
            programSettingsControlPage.CreateFavorite();
            var programNamePage = new ProgramNamePage();
            programNamePage.EnterName("MyFavourite1").Proceed();
            var programIconPage = new ProgramIconPage();
            programIconPage.SelectIcon(1).Proceed();
            var programAutomationPage = new ProgramAutomationPage();
            programAutomationPage.Proceed();
            Output.TestStep("Check if newly created Favourite is available and its name is 'MyFavourite1'", 2);
            Assert.AreEqual("MyFavourite1", programDetailPage.GetCurrentProgramName(), $"Expected to be on ProgramDetailPage of 'Favourite1' but was {programDetailPage.GetCurrentProgramName()}");


            //Create favourite for third program name it 'MyFavourite2'
            Output.TestStep("Create Favourite for third program");
            programDetailPage.SelectProgram(2);
            programDetailPage.OpenProgramSettings();
            programSettingsControlPage.CreateFavorite();
            programNamePage.EnterName("MyFavourite2").Proceed();
            programIconPage.SelectIcon(3).Proceed();
            programAutomationPage.Proceed();
            Output.TestStep("Check if newly created Favourite is available and its name is 'MyFavourite2'", 2);
            Assert.AreEqual("MyFavourite2", programDetailPage.GetCurrentProgramName(), $"Expected to be on ProgramDetailPage of 'Favourite2' but was {programDetailPage.GetCurrentProgramName()}");


            //Create favourite for fourth program
            Output.TestStep("Create Favourite for fourth program");
            programDetailPage.SelectProgram(3);
            programDetailPage.OpenProgramSettings();
            programSettingsControlPage.CreateFavorite();
            programNamePage.EnterName("MyFavourite3").Proceed();
            programIconPage.SelectIcon(4).Proceed();
            programAutomationPage.Proceed();
            Output.TestStep("Check if newly created Favourite is available and its name is 'MyFavourite3'", 2);
            Assert.AreEqual("MyFavourite3", programDetailPage.GetCurrentProgramName(), $"Expected to be on ProgramDetailPage of 'Favourite3' but was {programDetailPage.GetCurrentProgramName()}");


            //Create additional favourite for second program
            Output.TestStep("Create additional Favourite for second program");
            programDetailPage.SelectProgram(1);
            programDetailPage.OpenProgramSettings();
            programSettingsControlPage.CreateFavorite();
            programNamePage.EnterName("MyFavourite4").Proceed();
            programIconPage.SelectIcon(16).Proceed();
            programAutomationPage.Proceed();
            Output.TestStep("Check if newly created Favourite is available and its name is 'MyFavourite4'",2);
            Assert.AreEqual("MyFavourite4", programDetailPage.GetCurrentProgramName(), $"Expected to be on ProgramDetailPage of 'Favourite4' but was {programDetailPage.GetCurrentProgramName()}");
            programDetailPage.TapBack();
            Assert.AreEqual(8, dashboardPage.GetNumberOfPrograms(), $"Expected number of progams on dashboard page was 8 but is {dashboardPage.GetNumberOfPrograms()}");


            //Try to create one more (fifth) favourite program, expect that app will block this
            Output.TestStep("Check app restriction to create more than 4 user defined programs");
            dashboardPage.SelectProgram(1);
            dashboardPage.OpenCurrentProgram();
            programDetailPage.OpenProgramSettings();
            programSettingsControlPage.CreateFavorite();
            Assert.IsTrue(programSettingsControlPage.IsCurrentlyShown(), "Expected that creating an additional favourite is blocked by the app and 'ProgramSettingsControlPage' is shown ");
            
            //navigate back to programDetailPage for "Music"
            programSettingsControlPage.TapBack();
            Assert.AreEqual(checkSecondProgramName, programDetailPage.GetCurrentProgramName(), $"Expected to be on ProgramDetailPage of 'Music' but was {programDetailPage.GetCurrentProgramName()}");

            //delete the first favourite program "MyFavourite1"
            Output.TestStep("Delete first Favourite and check if it is available on dashboard");
            var automaticProgramDetailPage = programDetailPage.SelectProgram(0); //save programDetailPage information for Assertion after 'MyFavourite1' has been deleted
            programDetailPage.SelectProgram(4);
            programDetailPage.OpenProgramSettings();
            programSettingsControlPage.DeleteFavoriteAndConfirm();
            Assert.IsTrue(automaticProgramDetailPage.IsCurrentlyShown(),"Expected to be on ProgramDetailPage for 'Automatic' program after favourite program 'MyFavourite1' has been deleted");
            
            //navigate back to dashboard page and check and check existing program names, expect that "MyFavourite1" is not existing
            programDetailPage.TapBack();
            var numberOfDashboardPrograms = dashboardPage.GetNumberOfPrograms();
            Assert.AreEqual(7, numberOfDashboardPrograms, $"Expected number of programs on dashboard page to be seven but was {numberOfDashboardPrograms}");
            for (int j = 0; j < 7; j++)
            {
                dashboardPage.SelectProgram(j);
                Assert.AreNotEqual("MyFavourite1", dashboardPage.GetCurrentProgramName(),"Expected not to find program named 'MyFavourite1' but was found on dashboard");
            }

            //from dashboard page open 'Music' program 
            Output.TestStep("Check for remaining favourite programs");
            dashboardPage.SelectProgram(1);
            dashboardPage.OpenCurrentProgram();
            programDetailPage.SelectProgram(5);
            Assert.AreEqual("MyFavourite2", programDetailPage.GetCurrentProgramName(), $"Expected actual program title is 'MyFavourite2' but was {nameof(programDetailPage.GetCurrentProgramName)}");
            programDetailPage.SelectProgram(6);
            Assert.AreEqual("MyFavourite3", programDetailPage.GetCurrentProgramName(),$"Expected actual program title is 'MyFavourite3' but was {nameof(programDetailPage.GetCurrentProgramName)}");
            programDetailPage.SelectProgram(7);
            Assert.AreEqual("MyFavourite4", programDetailPage.GetCurrentProgramName(), $"Expected actual program title is 'MyFavourite4' but was {nameof(programDetailPage.GetCurrentProgramName)}");

            //delete 'MyFavourite4' and check its availablility after deleting
            Output.TestStep("Delete fourth Favourite and check if it is available on dashboard");
            programDetailPage.OpenProgramSettings();
            programSettingsControlPage.DeleteFavoriteAndConfirm();
            Assert.IsTrue(automaticProgramDetailPage.IsCurrentlyShown(), "Expected to be on ProgramDetailPage for 'Automatic' program");
            programDetailPage.TapBack();
            numberOfDashboardPrograms = dashboardPage.GetNumberOfPrograms();
            Assert.AreEqual(6, numberOfDashboardPrograms, $"Expected number of programs on dashboard page to be seven but was {numberOfDashboardPrograms}");
            for (int j = 0; j < 6; j++)
            {
                dashboardPage.SelectProgram(j);
                Assert.AreNotEqual("MyFavourite4", dashboardPage.GetCurrentProgramName(), "Expected not to find program named 'MyFavourite4' but was found on dashboard");
            }

            //check availability of 'MyFavourite2' and 'MyFavourite3'
            Output.TestStep("Check for remaining favourite programs");
            dashboardPage.OpenCurrentProgram();
            programDetailPage.SelectProgram(6);
            Assert.AreEqual("MyFavourite3", programDetailPage.GetCurrentProgramName(), $"Expected actual program title is 'MyFavourite3' but was {nameof(programDetailPage.GetCurrentProgramName)}");
            programDetailPage.SelectProgram(5);
            Assert.AreEqual("MyFavourite2", programDetailPage.GetCurrentProgramName(), $"Expected actual program title is 'MyFavourite2' but was {nameof(programDetailPage.GetCurrentProgramName)}");

            //delete 'MyFavourite2' and check its availablility after deleting
            Output.TestStep("Delete second Favourite and check if it is available on dashboard");
            programDetailPage.OpenProgramSettings();
            programSettingsControlPage.DeleteFavoriteAndConfirm();
            Assert.IsTrue(automaticProgramDetailPage.IsCurrentlyShown(), "Expected to be on ProgramDetailPage for 'Automatic' program");
            programDetailPage.TapBack();
            numberOfDashboardPrograms = dashboardPage.GetNumberOfPrograms();
            Assert.AreEqual(5, numberOfDashboardPrograms, $"Expected number of programs on dashboard page to be seven but was {numberOfDashboardPrograms}");
            for (int j = 0; j < 5; j++)
            {
                dashboardPage.SelectProgram(j);
                Assert.AreNotEqual("MyFavourite2", dashboardPage.GetCurrentProgramName(), "Expected not to find program named 'MyFavourite2' but was found on dashboard");
            }

            //delete 'MyFavourite3' and check its availablility after deleting
            Output.TestStep("Delete third Favourite and check if it is available on dashboard");
            dashboardPage.OpenCurrentProgram();
            programDetailPage.SelectProgram(6);
            Assert.AreEqual("MyFavourite3", programDetailPage.GetCurrentProgramName(), $"Expected actual program title is 'MyFavourite3' but was {nameof(programDetailPage.GetCurrentProgramName)}");
            programDetailPage.OpenProgramSettings();
            programSettingsControlPage.DeleteFavoriteAndConfirm();
            Assert.IsTrue(automaticProgramDetailPage.IsCurrentlyShown(), "Expected to be on ProgramDetailPage for 'Automatic' program");
            programDetailPage.TapBack();
            numberOfDashboardPrograms = dashboardPage.GetNumberOfPrograms();
            Assert.AreEqual(4, numberOfDashboardPrograms, $"Expected number of programs on dashboard page to be seven but was {numberOfDashboardPrograms}");
            for (int j = 0; j < 4; j++)
            {
                dashboardPage.SelectProgram(j);
                Assert.AreNotEqual("MyFavourite3", dashboardPage.GetCurrentProgramName(), "Expected not to find program named 'MyFavourite3' but was found on dashboard");
            }
            Output.TestStep("Close App - Test finished");
            AppManager.CloseApp();
        }
    }
}


