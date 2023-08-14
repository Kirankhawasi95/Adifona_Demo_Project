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
using HorusUITest.PageObjects.Menu.Settings;
using HorusUITest.PageObjects.Start;
using HorusUITest.PageObjects.Start.Intro;
using NUnit.Framework;
using Platform = HorusUITest.Enums.Platform;

namespace HorusUITest.TestFixtures.FunctionalTests
{
    public class SwitchAppModeTest : BaseResettingTestFixture
    {
        public SwitchAppModeTest(Platform platform)
            : base(platform)
        {
        }

        [Test]
        [Category("FunctionalTest")]
        // [Platform("Android")]
        public void SwitchModeTest()
        {
#if !TEST_FROM_IDE
            AndroidPhone androidPhone = SelectPhone.Android(AndroidPhoneName.Audifon_Nokia_7_plus);
            IosPhone iosPhone = SelectPhone.Ios(IosPhoneName.Audifon_iPhone_8);

            AppManager.InitializeApp(new AppConfig(CurrentPlatform, androidPhone, iosPhone));
            AppManager.StartApp(true);
#endif

            HearingAid leftHearingAid = SelectHearingAid.Left(LeftHearingAid.Audifon_Functiona_HG_082280);
            HearingAid rightHearingAid = SelectHearingAid.Right(RightHearingAid.Audifon_Functiona_HG_082279);

            //Startup and connection
            Output.Immediately("The app has started.");
            Output.TestStep("Connecting hearing aids and proceeding to dashboard");

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(rightHearingAid, leftHearingAid).Page;
            
            //Check Number of Programs
            Output.TestStep("Checking number of programs on dashboard page");
            Assert.AreEqual(4, dashboardPage.GetNumberOfPrograms(), "Number of programs on Dashboard Page does not meet the precondition for this test");
            var initialProgram = dashboardPage.GetCurrentProgramName();

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
            
            //Open Settings Menu to change to Demo-Mode
            Output.TestStep("Open main menu, navigating to AppModeSelectionPage using app menu structure");
            dashboardPage.OpenMenuUsingTap();
            var mainMenuPage = new MainMenuPage();
            mainMenuPage.OpenSettings();
            var settingsMenuPage = new SettingsMenuPage();
            Output.TestStep("Opening AppModeSelectionPage", 2);
            settingsMenuPage.OpenDemoMode();
            var demoModePage = new AppModeSelectionPage();
            
            //Get actual app mode and switch to "Demo-Mode"
            Output.TestStep("Read actual app mode and check if it is 'Normal' mode",2);
            var actualAppMode = demoModePage.GetSelectedAppMode().ToString();
            Assert.AreEqual(actualAppMode,demoModePage.GetAppModeText(AppMode.Normal),"Expected App Mode to be 'normal' mode but is different");
            Output.TestStep("Select and confirm 'Demo-Mode'",2);
            var selectAppMode= demoModePage.SelectAppMode(AppMode.Demo);
            Assert.AreEqual("Demo", selectAppMode.ToString(), "App Mode 'Demo' could not be selected");
            demoModePage.Accept();
            
            //Wait for App re-start in demo-mode, expected to be on demo-mode dashboard Page
            Output.TestStep("Wait for App Re-Connect in Demo-Mode expecting to be on Dashboard Page");
            dashboardPage = new DashboardPage();
            Assert.IsTrue(dashboardPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)),"Expected to be on Dashboard Page but was another");
            
            //Open settings menu to select normal mode
            Output.TestStep("Open main menu, navigating to AppModeSelectionPage using app menu structure again");
            dashboardPage.OpenMenuUsingTap();
            mainMenuPage = new MainMenuPage();
            mainMenuPage.OpenSettings();
            settingsMenuPage = new SettingsMenuPage();
            Output.TestStep("Opening AppModeSelectionPage", 2);
            settingsMenuPage.OpenDemoMode();
            demoModePage = new AppModeSelectionPage();
            
            //Get actual app mode and switch to "Normal" Mode
            Output.TestStep("Read actual app mode and check if it is 'Demo' mode", 2);
            actualAppMode = demoModePage.GetSelectedAppMode().ToString();
            Assert.AreEqual(actualAppMode, demoModePage.GetAppModeText(AppMode.Demo), "Expected App Mode to be in 'Demo' mode but is different");
            Output.TestStep("Select and confirm 'Normal' mode", 2);
            selectAppMode = demoModePage.SelectAppMode(AppMode.Normal);
            Assert.AreEqual("Normal", selectAppMode.ToString(), "App Mode 'Normal' could not be selected");
            demoModePage.Accept();
            
            //Checking first app page after selecting 'Normal' mode from menu structure 
            Output.TestStep("Checking first app page, specific to app brand, after selecting 'Normal' mode from menu structure");
            var audifonPage = new IntroPageOne();
            var kindPage = new InitializeHardwarePage();
            var rightDevice = rightHearingAid.ToString();
            var leftDevice = leftHearingAid.ToString();
            //Checking expected Page depending on app brand specific requirement
            if ((TestContext.Parameters["Brand"] == "Audifon")|| (TestContext.Parameters["Brand"] == "HuiEr"))
            {
                Output.TestStep("Checking to be on 'IntroPageOne' as it is expected for detected 'Audifon' app version",2);
                Assert.IsTrue(audifonPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)),"Expected  to be on 'IntroPageOne' after selecting 'Normal' mode");
                Output.TestStep("Select and Connect hearing aids again and navigate to Dashboard page");
                LaunchHelper.SkipToSelectHearingAidsPage();
                var selectHearingAids = new SelectHearingAidsPage();
                selectHearingAids.WaitUntilScanFinished();
                selectHearingAids.GetIsDeviceFound(rightDevice);
                selectHearingAids.GetIsDeviceFound(leftDevice);
                //selectHearingAids.GetDeviceIndex(rightDevice);
                //selectHearingAids.GetDeviceIndex(leftDevice);
                selectHearingAids.SelectDevice(rightDevice);
                selectHearingAids.SelectDevice(leftDevice);
                selectHearingAids.Connect();
            }
            else
            { 
               Output.TestStep("Checking to be on 'InitializeHardwarePage' as it is expected for detected 'KIND' app version", 2);
               Assert.IsTrue(kindPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)), "Expected to be on 'InitializeHardwarePage' after selecting 'Normal' mode");
               Output.TestStep("Select and Connect hearing aids again and navigate to Dashboard page");
               kindPage.StartScan();
               var selectHearingAids = new SelectHearingAidsPage();
               //hier ggf noch zeitlicher Verbesserung bzgl WaitUntilScanFinished Funktion?
               selectHearingAids.WaitUntilScanFinished();
               selectHearingAids.GetIsDeviceFound(rightDevice);
               selectHearingAids.GetIsDeviceFound(leftDevice);
               //selectHearingAids.GetDeviceIndex(rightDevice);
               //selectHearingAids.GetDeviceIndex(leftDevice);
               selectHearingAids.SelectDevice(rightDevice);
               selectHearingAids.SelectDevice(leftDevice);
               selectHearingAids.Connect();
            }
            Output.TestStep("Checking if back on Dashboard page after staring app in 'Normal' mode");
            Assert.IsTrue(dashboardPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)),"Expected to be back on dashboard Page");
        }
    }
}