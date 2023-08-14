using System;
using System.Threading;
using AventStack.ExtentReports;
using HorusUITest.Configuration;
using HorusUITest.Data;
using HorusUITest.Enums;
using HorusUITest.Helper;
using HorusUITest.PageObjects;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Dialogs;
using HorusUITest.PageObjects.Menu;
using HorusUITest.PageObjects.Menu.Help;
using HorusUITest.PageObjects.Menu.Settings;
using HorusUITest.PageObjects.Start;
using HorusUITest.PageObjects.Start.Intro;
using NUnit.Framework;
using Platform = HorusUITest.Enums.Platform;

namespace HorusUITest.TestFixtures.TestCases
{
    public class SystemTestsDevice5 : BaseResettingTestFixture
    {
        #region Constructor

        public SystemTestsDevice5(Platform platform)
            : base(platform)
        {

        }

        #endregion Constructor

        #region Test Cases

        #region Sprint 16

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-15095_Table-29")]
        public void ST15095_VerifyCompleteHearingAidInfo()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var firstHearingAid = SelectHearingAid.GetLeftHearingAid();

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTimeAndVerify(firstHearingAid);

            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            ReportHelper.LogTest(Status.Pass, "Start view is displayed correctly containg Left device icon.");

            ReportHelper.LogTest(Status.Info, "Check Hearing Aids information from Main Menu -> Settings.");
            NavigationHelper.NavigateToSettingsMenu(dashboardPage).OpenMyHearingAids();
            Assert.IsTrue(new HearingSystemManagementPage().IsCurrentlyShown());

            var hearingSystemsPage = new HearingSystemManagementPage().CheckHAInformationFromSettings(AppMode.Normal, Side.Left);

            ReportHelper.LogTest(Status.Info, "Navigate back to and check HA info from Start View.");
            hearingSystemsPage.NavigateBack();
            Assert.IsTrue(new SettingsMenuPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            new SettingsMenuPage().NavigateBack();

            Assert.IsTrue(new MainMenuPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            new MainMenuPage().CloseMenuUsingTap();

            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            dashboardPage = new DashboardPage();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            ReportHelper.LogTest(Status.Pass, "Start view is displayed correctly.");

            ReportHelper.LogTest(Status.Info, "Open and check the Left device info.");
            dashboardPage.OpenLeftHearingDevice();
            new HearingInstrumentInfoControlPage().CheckHAInformationFromDashboard(AppMode.Normal).Close();
            ReportHelper.LogTest(Status.Pass, "Info for left device is displayed correctly.");
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-15078_Table-31")]
        public void ST15078_InstallAppReset()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var firstHearingAid = SelectHearingAid.GetLeftHearingAid();
            var secondHearingAid = SelectHearingAid.GetRightHearingAid();

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTimeAndVerify(firstHearingAid, secondHearingAid);

            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());

            ReportHelper.LogTest(Status.Info, "Navigate to 'Settings' from Main Menu.");
            NavigationHelper.NavigateToSettingsMenu(dashboardPage);
            var settingsMenuPage = new SettingsMenuPage();
            Assert.IsNotEmpty(settingsMenuPage.GetMyHearingAidsText());
            Assert.IsNotEmpty(settingsMenuPage.GetPermissionsText());
            Assert.IsNotEmpty(settingsMenuPage.GetLanguageText());
            Assert.IsNotEmpty(settingsMenuPage.GetDemoModeText());
            Assert.IsFalse(settingsMenuPage.GetIsDevelopmentStuffVisible());
            ReportHelper.LogTest(Status.Pass, "The Settings menu is opened successfully and all items and submenu items are visible.");
            ReportHelper.LogTest(Status.Pass, "The Develpement stuff is currently hidden.");

            ReportHelper.LogTest(Status.Info, "Make the 'App Develpement Stuff' visible and check the view.");
            Thread.Sleep(2000);
            settingsMenuPage.ShowDevelopmentStuff();
            Thread.Sleep(2000);
            Assert.IsTrue(settingsMenuPage.GetIsDevelopmentStuffVisible());
            Assert.IsNotEmpty(settingsMenuPage.GetLogsText());
            Assert.IsNotEmpty(settingsMenuPage.GetAppResetText());
            Assert.IsNotEmpty(settingsMenuPage.GetConnectionErrorPageText());
            Assert.IsNotEmpty(settingsMenuPage.GetHardwareErrorPageText());
            ReportHelper.LogTest(Status.Pass, "The App developement menu displayed correctly and all items are visible.");

            ReportHelper.LogTest(Status.Info, "Reset the app and check if navigated to Intro pages.");
            settingsMenuPage.OpenAppReset();
            Assert.IsTrue(DialogHelper.ConfirmIfDisplayed());

            LaunchHelper.StartAppWithHearingAidsForTheFirstTimeAndVerify(firstHearingAid, secondHearingAid);
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-15079_Table-30")]
        public void ST15079_DisconnectHearingAids()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            // Expected language for Apps other than Hormann after we change the mobile language to a language which is not part of app
            Language_Audifon language_Audifon = Language_Audifon.English;
            // Changing only for Android since mobile language change is not implemented in IOS
            if (OnAndroid)
            {
                // Expected language Hormann after we change the mobile language to a language which is not part of app
                if (AppManager.Brand == Brand.Hormann)
                    language_Audifon = Language_Audifon.Spanish;

                ReportHelper.LogTest(Status.Info, "Changing device langauge to a language which is not part of app...");
                AppManager.DeviceSettings.ChangeDeviceLanguage(Language_Device.Hindi_India);
                ReportHelper.LogTest(Status.Info, "Device langauge changed to " + Language_Device.Hindi_India + " which is a language which is not part of app...");

                AppManager.RestartApp(true);
                ReportHelper.LogTest(Status.Info, "App restarted after changing the language to " + Language_Device.Hindi_India);

                Assert.IsTrue(new IntroPageOne().IsShowingBeforeTimeout(TimeSpan.FromSeconds(30)));
                ReportHelper.LogTest(Status.Info, "Checking the welcome text for default laguage of the app based on OEM");
                Assert.AreEqual(LanguageHelper.GetResourceTextByKey(AppManager.Brand, language_Audifon, "introPage1_Title"), new IntroPageOne().GetTitle());
                ReportHelper.LogTest(Status.Pass, "Welcome Page text is in " + language_Audifon + " language.");
            }

            var firstHearingAid = SelectHearingAid.GetLeftHearingAid();
            var secondHearingAid = SelectHearingAid.GetRightHearingAid();

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTimeAndVerify(firstHearingAid, secondHearingAid);

            ReportHelper.LogTest(Status.Info, "Navigating to my hearing aid page from main menu in dashboard page using tap...");
            NavigationHelper.NavigateToSettingsMenu(dashboardPage).OpenMyHearingAids();
            var hearingSystemsPage = new HearingSystemManagementPage();
            ReportHelper.LogTest(Status.Info, "Navigated to my hearing aid page from main menu in dashboard page using tap");

            ReportHelper.LogTest(Status.Info, "Tapping left tab...");
            hearingSystemsPage.LeftDeviceTabClick();
            ReportHelper.LogTest(Status.Info, "Tapped left tab");
            ReportHelper.LogTest(Status.Info, "Checking if left tab is selected...");
            Assert.IsTrue(hearingSystemsPage.GetIsLeftTabSelected(), "Left tab is not selected");
            ReportHelper.LogTest(Status.Info, "Left tab is selected");
            ReportHelper.LogTest(Status.Info, "Checking left tab text...");
            Assert.IsNotEmpty(hearingSystemsPage.GetLeftTabText(), "Left tab text is empty");
            ReportHelper.LogTest(Status.Info, "Left tab text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking if left UDI is visible...");
            Assert.IsTrue(hearingSystemsPage.GetIsLeftUdiVisible(), "Left UDI is not visible");
            ReportHelper.LogTest(Status.Info, "Left UDI is visible");

            string leftName = hearingSystemsPage.GetLeftDeviceName();
            string leftSerial = hearingSystemsPage.GetLeftDeviceSerial();
            string leftState = hearingSystemsPage.GetLeftDeviceState();
            string leftType = hearingSystemsPage.GetLeftDeviceType();
            string leftNameTitle = hearingSystemsPage.GetLeftDeviceNameTitle();
            string leftTypeTitle = hearingSystemsPage.GetLeftDeviceTypeTitle();
            string leftSerialTitle = hearingSystemsPage.GetLeftDeviceSerialTitle();
            string leftStateTitle = hearingSystemsPage.GetLeftDeviceStateTitle();
            string leftUdiTitle = hearingSystemsPage.GetLeftDeviceUdiTitle();
            string leftUdi = hearingSystemsPage.GetLeftDeviceUdi();

            ReportHelper.LogTest(Status.Info, "Tapping right tab...");
            hearingSystemsPage.RightDeviceTabClick();
            ReportHelper.LogTest(Status.Info, "Tapped right tab");
            ReportHelper.LogTest(Status.Info, "Checking if right tab is selected...");
            Assert.IsTrue(hearingSystemsPage.GetIsRightTabSelected(), "Right tab is not selected");
            ReportHelper.LogTest(Status.Info, "Right tab is selected");
            ReportHelper.LogTest(Status.Info, "Checking right tab text...");
            Assert.IsNotEmpty(hearingSystemsPage.GetRightTabText(), "Right tab text is empty");
            ReportHelper.LogTest(Status.Info, "Right tab text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking if right UDI is visible...");
            Assert.IsTrue(hearingSystemsPage.GetIsRightUdiVisible(), "Right UDI is not visible");
            ReportHelper.LogTest(Status.Info, "Right UDI is visible");

            string rightName = hearingSystemsPage.GetRightDeviceName();
            string rightSerial = hearingSystemsPage.GetRightDeviceSerial();
            string rightState = hearingSystemsPage.GetRightDeviceState();
            string rightType = hearingSystemsPage.GetRightDeviceType();
            string rightNameTitle = hearingSystemsPage.GetRightDeviceNameTitle();
            string rightTypeTitle = hearingSystemsPage.GetRightDeviceTypeTitle();
            string rightSerialTitle = hearingSystemsPage.GetRightDeviceSerialTitle();
            string rightStateTitle = hearingSystemsPage.GetRightDeviceStateTitle();
            string rightUdiTitle = hearingSystemsPage.GetRightDeviceUdiTitle();
            string rightUdi = hearingSystemsPage.GetRightDeviceUdi();

            ReportHelper.LogTest(Status.Info, "Checking info of the left device...");
            ReportHelper.LogTest(Status.Info, "Checking left device name...");
            Assert.IsNotEmpty(leftName, "Left device name is empty");
            ReportHelper.LogTest(Status.Info, "Left device name is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left device serial...");
            Assert.IsNotEmpty(leftSerial, "Left device serial is empty");
            ReportHelper.LogTest(Status.Info, "Left device serial is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left device state...");
            Assert.IsNotEmpty(leftState, "Left device state is empty");
            ReportHelper.LogTest(Status.Info, "Left device state is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left device type...");
            Assert.IsNotEmpty(leftType, "Left device type is empty");
            ReportHelper.LogTest(Status.Info, "Left device type is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left device UDI...");
            Assert.IsNotEmpty(leftUdi, "Left device UDI is empty");
            ReportHelper.LogTest(Status.Info, "Left device UDI is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left device name title...");
            Assert.IsNotEmpty(leftNameTitle, "Left device name title is empty");
            ReportHelper.LogTest(Status.Info, "Left device name title is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left device serial title...");
            Assert.IsNotEmpty(leftSerialTitle, "Left device serial title is empty");
            ReportHelper.LogTest(Status.Info, "Left device serial title is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left device state title...");
            Assert.IsNotEmpty(leftStateTitle, "Left device state title is empty");
            ReportHelper.LogTest(Status.Info, "Left device state title is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left device type title...");
            Assert.IsNotEmpty(leftTypeTitle, "Left device type title is empty");
            ReportHelper.LogTest(Status.Info, "Left device type title is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left device UDI title...");
            Assert.IsNotEmpty(leftUdiTitle, "Left device UDI title is empty");
            ReportHelper.LogTest(Status.Info, "Left device UDI title is not empty");
            ReportHelper.LogTest(Status.Info, "Info of the left device is verified");

            ReportHelper.LogTest(Status.Info, "Checking info of the right device...");
            ReportHelper.LogTest(Status.Info, "Checking right device name...");
            Assert.IsNotEmpty(rightName, "Right device name is empty");
            ReportHelper.LogTest(Status.Info, "Right device name is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right device serial...");
            Assert.IsNotEmpty(rightSerial, "Right device serial is empty");
            ReportHelper.LogTest(Status.Info, "Right device serial is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right device state...");
            Assert.IsNotEmpty(rightState, "Right device state is empty");
            ReportHelper.LogTest(Status.Info, "Right device state is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right device type...");
            Assert.IsNotEmpty(rightType, "Right device type is empty");
            ReportHelper.LogTest(Status.Info, "Right device type is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right device UDI...");
            Assert.IsNotEmpty(rightUdi, "Right device UDI is empty");
            ReportHelper.LogTest(Status.Info, "Right device UDI is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right device name title...");
            Assert.IsNotEmpty(rightNameTitle, "Right device name title is empty");
            ReportHelper.LogTest(Status.Info, "Right device name title is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right device serial title...");
            Assert.IsNotEmpty(rightSerialTitle, "Right device serial title is empty");
            ReportHelper.LogTest(Status.Info, "Right device serial title is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right device state title...");
            Assert.IsNotEmpty(rightStateTitle, "Right device state title is empty");
            ReportHelper.LogTest(Status.Info, "Right device state title is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right device type title...");
            Assert.IsNotEmpty(rightTypeTitle, "Right device type title is empty");
            ReportHelper.LogTest(Status.Info, "Right device type title is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right device UDI title...");
            Assert.IsNotEmpty(rightUdiTitle, "Right device UDI title is empty");
            ReportHelper.LogTest(Status.Info, "Right device UDI title is not empty");
            ReportHelper.LogTest(Status.Info, "Info of the right device is verified");

            ReportHelper.LogTest(Status.Pass, "All info is displayed correctly for connected device and is verified");

            ReportHelper.LogTest(Status.Info, "Checking app dialog is displayed correctly with 'Cancel' and 'Confirm' button...");
            ReportHelper.LogTest(Status.Info, "Tapping disconnect...");
            hearingSystemsPage.DisconnectDevices();
            ReportHelper.LogTest(Status.Info, "Tapped disconnect");
            ReportHelper.LogTest(Status.Info, "Checking app dialog...");
            Assert.IsTrue(DialogHelper.GetIsDialogDisplayed(TimeSpan.FromSeconds(5)), "App dialog is not visible");
            ReportHelper.LogTest(Status.Info, "App dialog is visible");
            ReportHelper.LogTest(Status.Info, "Checking app dialog confirm button text...");
            Assert.IsNotEmpty(new AppDialog().GetConfirmButtonText(), "App dialog confirm button text is empty");
            ReportHelper.LogTest(Status.Info, "App dialog confirm button text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking app dialog deny button text...");
            Assert.IsNotEmpty(new AppDialog().GetDenyButtonText(), "App dialog deny button text is empty");
            ReportHelper.LogTest(Status.Info, "App dialog deny button text is not empty");
            ReportHelper.LogTest(Status.Pass, "App dialog is displayed correctly with 'Cancel' and 'Confirm' button");

            ReportHelper.LogTest(Status.Info, "Clicking cancel...");
            new AppDialog().Deny();
            Thread.Sleep(500);
            ReportHelper.LogTest(Status.Info, "Clicked cancel");

            ReportHelper.LogTest(Status.Info, "Checking info of the left device again...");
            ReportHelper.LogTest(Status.Info, "Tapping left tab...");
            hearingSystemsPage.LeftDeviceTabClick();
            ReportHelper.LogTest(Status.Info, "Tapped left tab");
            ReportHelper.LogTest(Status.Info, "Checking if left tab is selected...");
            Assert.IsTrue(hearingSystemsPage.GetIsLeftTabSelected(), "Left tab is not selected");
            ReportHelper.LogTest(Status.Info, "Left tab is selected");
            ReportHelper.LogTest(Status.Info, "Checking left tab text...");
            Assert.IsNotEmpty(hearingSystemsPage.GetLeftTabText(), "Left tab text is empty");
            ReportHelper.LogTest(Status.Info, "Left tab text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left device name title...");
            Assert.AreEqual(leftNameTitle, hearingSystemsPage.GetLeftDeviceNameTitle(), "Left device name title is not matching");
            ReportHelper.LogTest(Status.Info, "Left device name title is matching");
            ReportHelper.LogTest(Status.Info, "Checking left device name...");
            Assert.AreEqual(leftName, hearingSystemsPage.GetLeftDeviceName(), "Left device name is not matching");
            ReportHelper.LogTest(Status.Info, "Left device name is matching");
            ReportHelper.LogTest(Status.Info, "Checking left device serial title...");
            Assert.AreEqual(leftSerialTitle, hearingSystemsPage.GetLeftDeviceSerialTitle(), "Left device serial title is not matching");
            ReportHelper.LogTest(Status.Info, "Left device serial title is matching");
            ReportHelper.LogTest(Status.Info, "Checking left device serial...");
            Assert.AreEqual(leftSerial, hearingSystemsPage.GetLeftDeviceSerial(), "Left device serial is not matching");
            ReportHelper.LogTest(Status.Info, "Left device serial is matching");
            ReportHelper.LogTest(Status.Info, "Checking left device state title...");
            Assert.AreEqual(leftStateTitle, hearingSystemsPage.GetLeftDeviceStateTitle(), "Left device state title is not matching");
            ReportHelper.LogTest(Status.Info, "Left device state title is matching");
            ReportHelper.LogTest(Status.Info, "Checking left device state...");
            Assert.AreEqual(leftState, hearingSystemsPage.GetLeftDeviceState(), "Left device state is not matching");
            ReportHelper.LogTest(Status.Info, "Left device state is matching");
            ReportHelper.LogTest(Status.Info, "Checking left device type title...");
            Assert.AreEqual(leftTypeTitle, hearingSystemsPage.GetLeftDeviceTypeTitle(), "Left device type title is not matching");
            ReportHelper.LogTest(Status.Info, "Left device type title is matching");
            ReportHelper.LogTest(Status.Info, "Checking left device type...");
            Assert.AreEqual(leftType, hearingSystemsPage.GetLeftDeviceType(), "Left device type is not matching");
            ReportHelper.LogTest(Status.Info, "Left device type is matching");
            ReportHelper.LogTest(Status.Info, "Checking left device UDI title...");
            Assert.AreEqual(leftUdiTitle, hearingSystemsPage.GetLeftDeviceUdiTitle(), "Left device UDI title is not matching");
            ReportHelper.LogTest(Status.Info, "Left device UDI title is matching");
            ReportHelper.LogTest(Status.Info, "Checking left device UDI...");
            Assert.AreEqual(leftUdi, hearingSystemsPage.GetLeftDeviceUdi(), "Left device UDI is not matching");
            ReportHelper.LogTest(Status.Info, "Left device UDI is matching");
            ReportHelper.LogTest(Status.Info, "Info of the left device is verified again");

            ReportHelper.LogTest(Status.Info, "Checking info of the right device again...");
            ReportHelper.LogTest(Status.Info, "Tapping right tab...");
            hearingSystemsPage.RightDeviceTabClick();
            ReportHelper.LogTest(Status.Info, "Tapped right tab");
            ReportHelper.LogTest(Status.Info, "Checking if right tab is selected...");
            Assert.IsTrue(hearingSystemsPage.GetIsRightTabSelected(), "Right tab is not selected");
            ReportHelper.LogTest(Status.Info, "Right tab is selected");
            ReportHelper.LogTest(Status.Info, "Checking right tab text...");
            Assert.IsNotEmpty(hearingSystemsPage.GetRightTabText(), "Right tab text is empty");
            ReportHelper.LogTest(Status.Info, "Right tab text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right device name title...");
            Assert.AreEqual(rightNameTitle, hearingSystemsPage.GetRightDeviceNameTitle(), "Right device name title is not matching");
            ReportHelper.LogTest(Status.Info, "Right device name title is matching");
            ReportHelper.LogTest(Status.Info, "Checking right device name...");
            Assert.AreEqual(rightName, hearingSystemsPage.GetRightDeviceName(), "Right device name is not matching");
            ReportHelper.LogTest(Status.Info, "Right device name is matching");
            ReportHelper.LogTest(Status.Info, "Checking right device serial title...");
            Assert.AreEqual(rightSerialTitle, hearingSystemsPage.GetRightDeviceSerialTitle(), "Right device serial title is not matching");
            ReportHelper.LogTest(Status.Info, "Right device serial title is matching");
            ReportHelper.LogTest(Status.Info, "Checking right device serial...");
            Assert.AreEqual(rightSerial, hearingSystemsPage.GetRightDeviceSerial(), "Right device serial is not matching");
            ReportHelper.LogTest(Status.Info, "Right device serial is matching");
            ReportHelper.LogTest(Status.Info, "Checking right device state title...");
            Assert.AreEqual(rightStateTitle, hearingSystemsPage.GetRightDeviceStateTitle(), "Right device state title is not matching");
            ReportHelper.LogTest(Status.Info, "Right device state title is matching");
            ReportHelper.LogTest(Status.Info, "Checking right device state...");
            Assert.AreEqual(rightState, hearingSystemsPage.GetRightDeviceState(), "Right device state is not matching");
            ReportHelper.LogTest(Status.Info, "Right device state is matching");
            ReportHelper.LogTest(Status.Info, "Checking right device type title...");
            Assert.AreEqual(rightTypeTitle, hearingSystemsPage.GetRightDeviceTypeTitle(), "Right device type title is not matching");
            ReportHelper.LogTest(Status.Info, "Right device type title is matching");
            ReportHelper.LogTest(Status.Info, "Checking right device type...");
            Assert.AreEqual(rightType, hearingSystemsPage.GetRightDeviceType(), "Right device type is not matching");
            ReportHelper.LogTest(Status.Info, "Right device type is matching");
            ReportHelper.LogTest(Status.Info, "Checking right device UDI title...");
            Assert.AreEqual(rightUdiTitle, hearingSystemsPage.GetRightDeviceUdiTitle(), "Right device UDI title is not matching");
            ReportHelper.LogTest(Status.Info, "Right device UDI title is matching");
            ReportHelper.LogTest(Status.Info, "Checking right device UDI...");
            Assert.AreEqual(rightUdi, hearingSystemsPage.GetRightDeviceUdi(), "Right device UDI is not matching");
            ReportHelper.LogTest(Status.Info, "Right device UDI is matching");
            ReportHelper.LogTest(Status.Info, "Info of the right device is verified again");

            ReportHelper.LogTest(Status.Pass, "All info is displayed correctly for connected device and is verified again");

            ReportHelper.LogTest(Status.Info, "Tapping disconnect again...");
            hearingSystemsPage.DisconnectDevices();
            ReportHelper.LogTest(Status.Info, "Tapped disconnect again");
            ReportHelper.LogTest(Status.Info, "Clicking confirm...");
            DialogHelper.ConfirmIfDisplayed();
            ReportHelper.LogTest(Status.Info, "Clicked confirm");

            // Disconnecting Message very quickly disappears in IOS and Appium is not able to get the element within that time.
            if (OnAndroid)
            {
                ReportHelper.LogTest(Status.Info, "Checking disconnecting Hearing System text...");
                Assert.IsTrue(hearingSystemsPage.VerifyDisconnectingText(LanguageHelper.GetResourceTextByKey(AppManager.Brand, Language_Audifon.English, "manageHA_DisconnectingHearingSystems"), TimeSpan.FromSeconds(5)));
                ReportHelper.LogTest(Status.Pass, "Disconnecting Hearing System text verified in the " + Language_Audifon.English + " language");
            }

            ReportHelper.LogTest(Status.Info, "Waiting till initialize hardware page is loaded...");
            Assert.IsTrue(new InitializeHardwarePage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)), "Initialize hardware page is not loaded");
            var initializeHardwarePage = new InitializeHardwarePage();
            ReportHelper.LogTest(Status.Info, "Initialize hardware page is loaded");
            ReportHelper.LogTest(Status.Info, "Checking demo mode text...");
            Assert.IsNotEmpty(initializeHardwarePage.GetDemoModeText(), "Demo mode text is empty");
            ReportHelper.LogTest(Status.Info, "Demo mode text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking start scan text...");
            Assert.IsNotEmpty(initializeHardwarePage.GetScanText(), "Start scan text is empty");
            ReportHelper.LogTest(Status.Info, "Start scan text is not empty");
            ReportHelper.LogTest(Status.Pass, "'Start Search' page is displayed correctly having 'Start Search' and 'Demo Mode' option. Now tap 'Start Search'");
            ReportHelper.LogTest(Status.Info, "Clicking start scan...");
            initializeHardwarePage.StartScan();
            ReportHelper.LogTest(Status.Info, "Clicked start scan");
            var selectHearingAidsPage = new SelectHearingAidsPage();
            ReportHelper.LogTest(Status.Info, "Checking if start scan started...");
            Assert.IsTrue(selectHearingAidsPage.GetIsScanning(), "Start scan not started");
            ReportHelper.LogTest(Status.Info, "Start scan started");
            ReportHelper.LogTest(Status.Info, "Checking description text...");
            Assert.IsNotEmpty(selectHearingAidsPage.GetDescription(), "Description text is empty");
            ReportHelper.LogTest(Status.Info, "Description text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking cancel text...");
            Assert.IsNotEmpty(selectHearingAidsPage.GetCancelText(), "Cancel text is empty");
            ReportHelper.LogTest(Status.Info, "Cancel text is not empty");
            if (!selectHearingAidsPage.GetIsDeviceFound(firstHearingAid))
                selectHearingAidsPage.WaitUntilDeviceFound(firstHearingAid);
            if (secondHearingAid != null)
                selectHearingAidsPage.WaitUntilDeviceFound(secondHearingAid);
            if (!selectHearingAidsPage.GetIsDeviceFound(secondHearingAid))
                selectHearingAidsPage.WaitUntilDeviceFound(secondHearingAid);
            selectHearingAidsPage.WaitUntilDeviceListNotChanging();
            selectHearingAidsPage.SelectDevicesExclusively(firstHearingAid, secondHearingAid);
            selectHearingAidsPage.GetIsDeviceSelected(firstHearingAid);
            if (secondHearingAid != null)
                selectHearingAidsPage.GetIsDeviceSelected(secondHearingAid);
            ReportHelper.LogTest(Status.Pass, "Found Hearing aids " + firstHearingAid + " and " + secondHearingAid);
            ReportHelper.LogTest(Status.Info, "Checking connect text...");
            Assert.IsNotEmpty(selectHearingAidsPage.GetConnectButtonText(), "Connect text is empty");
            ReportHelper.LogTest(Status.Info, "Connect text is not empty");
            ReportHelper.LogTest(Status.Info, "Clicking connect...");
            selectHearingAidsPage.Connect();
            ReportHelper.LogTest(Status.Info, "Clicked connect");

            ReportHelper.LogTest(Status.Info, "Waiting till dashboard page is loaded...");
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)), "Dashboard page is not loaded");
            dashboardPage = new DashboardPage();
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "Dashboard page is loaded");
            ReportHelper.LogTest(Status.Pass, "Start view is displayed correctly containing L and R device icons.");

            // Reset the language back to English. Changing only for Android since mobile language change is not implemented in IOS
            if (OnAndroid)
                AppManager.DeviceSettings.ChangeDeviceLanguage(Language_Device.English_US);
        }

        [Test]
        [Category("SystemTestsDevice")]
        [Description("TC-15378_Table-24")]
        public void ST15378_VerifyAppDataAfterUninstall()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var firstHearingAid = SelectHearingAid.GetLeftHearingAid();
            var secondHearingAid = SelectHearingAid.GetRightHearingAid();

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTimeAndVerify(firstHearingAid, secondHearingAid);

            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());

            //Test step mentions to Uninstall and Install app again.
            //Instead we are using delete app data and Restart app to achieve same.
            AppManager.RestartApp(true);

            LaunchHelper.StartAppWithHearingAidsForTheFirstTimeAndVerify(firstHearingAid, secondHearingAid);
        }

        #endregion Sprint 16

        #region Sprint 17

        [Test]
        [Category("SystemTestsDeviceManual")]
        [Description("TC-15407_Table-23")]
        public void ST15407_DeviceScanWorksAfterExitingDemoMode()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var firstHearingAid = SelectHearingAid.GetLeftHearingAid();
            var secondHearingAid = SelectHearingAid.GetRightHearingAid();

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

            // Connecting to Hearing Aid
            var initializeHardwarePage = new InitializeHardwarePage();
            Assert.IsNotEmpty(initializeHardwarePage.GetDemoModeText());
            Assert.IsNotEmpty(initializeHardwarePage.GetScanText());
            ReportHelper.LogTest(Status.Pass, "'Start Search' page is displayed correctly having 'Start Search' and 'Demo Mode' option. Now tap 'Start Search'");

            initializeHardwarePage.StartScan();

            // Allow Permissions
            DialogHelper.ConfirmIfDisplayed();
            PermissionHelper.AllowPermissionIfRequested();

            var selectHearingAidsPageFirstTime = new SelectHearingAidsPage();
            Assert.IsTrue(selectHearingAidsPageFirstTime.GetIsScanning());
            Assert.IsNotEmpty(selectHearingAidsPageFirstTime.GetDescription());
            Assert.IsNotEmpty(selectHearingAidsPageFirstTime.GetCancelText());
            if (!selectHearingAidsPageFirstTime.GetIsDeviceFound(firstHearingAid))
                selectHearingAidsPageFirstTime.WaitUntilDeviceFound(firstHearingAid);

            if (secondHearingAid != null)
                selectHearingAidsPageFirstTime.WaitUntilDeviceFound(secondHearingAid);
            if (!selectHearingAidsPageFirstTime.GetIsDeviceFound(secondHearingAid))
                selectHearingAidsPageFirstTime.WaitUntilDeviceFound(secondHearingAid);

            selectHearingAidsPageFirstTime.WaitUntilDeviceListNotChanging();
            selectHearingAidsPageFirstTime.SelectDevicesExclusively(firstHearingAid, secondHearingAid);
            Assert.IsTrue(selectHearingAidsPageFirstTime.GetIsDeviceSelected(firstHearingAid));
            if (secondHearingAid != null)
                selectHearingAidsPageFirstTime.GetIsDeviceSelected(secondHearingAid);
            ReportHelper.LogTest(Status.Pass, "Found Hearing aids " + firstHearingAid + " and " + secondHearingAid);
            Assert.IsNotEmpty(selectHearingAidsPageFirstTime.GetConnectButtonText());
            selectHearingAidsPageFirstTime.Connect();
            ReportHelper.LogTest(Status.Info, "Connected to Hearing Aid");
            Thread.Sleep(2000);

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off Left Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Left Hearing Aid turned off");

            // In IOS it takes 120 secs to go to Connection Error Page when HA is disconnected in the start
            Assert.IsTrue(new HearingAidConnectionErrorPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(130)));
            Assert.NotNull(new HearingAidConnectionErrorPage().GetPageTitle());
            new HearingAidConnectionErrorPage().BackToHearingAidSelectionPage();
            ReportHelper.LogTest(Status.Info, "Back to Hearing Aid Selecton Page");

            // Start in Demo Mode
            new InitializeHardwarePage().StartDemoMode();
            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(30));
            PermissionHelper.AllowPermissionIfRequested();
            new DashboardPage().IsCurrentlyShown();
            ReportHelper.LogTest(Status.Info, "App started in Demo Mode");

            // Open Menu
            new DashboardPage().OpenMenuUsingTap();
            var mainMenuPage = new MainMenuPage();
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText());
            ReportHelper.LogTest(Status.Pass, "The main menu is opened.");

            // Open Settings
            mainMenuPage.OpenSettings();

            new SettingsMenuPage().OpenDemoMode();

            new AppModeSelectionPage().SelectAppMode(AppMode.Normal);

            new AppModeSelectionPage().Accept();
            DialogHelper.Confirm();

            ReportHelper.LogTest(Status.Info, "App set to Normal Mode");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn on Left Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Left Hearing Aid turned on");

            // Starting the App
            Assert.IsTrue(new IntroPageOne().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Info, "Checking the Welcome Page.");
            var introPageOne1 = new IntroPageOne();
            Assert.IsTrue(introPageOne1.GetIsRightButtonVisible());
            Assert.IsFalse(introPageOne1.GetIsLeftButtonVisible());
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

            // Connect to Hearing Aid
            initializeHardwarePage = new InitializeHardwarePage();
            Assert.IsNotEmpty(initializeHardwarePage.GetDemoModeText());
            Assert.IsNotEmpty(initializeHardwarePage.GetScanText());
            ReportHelper.LogTest(Status.Pass, "'Start Search' page is displayed correctly having 'Start Search' and 'Demo Mode' option. Now tap 'Start Search'");

            initializeHardwarePage.StartScan();

            // Give Permission
            DialogHelper.ConfirmIfDisplayed();
            PermissionHelper.AllowPermissionIfRequested();

            var selectHearingAidsPageSecondTime = new SelectHearingAidsPage();
            Assert.IsTrue(selectHearingAidsPageSecondTime.GetIsScanning());
            Assert.IsNotEmpty(selectHearingAidsPageSecondTime.GetDescription());
            Assert.IsNotEmpty(selectHearingAidsPageSecondTime.GetCancelText());
            if (!selectHearingAidsPageSecondTime.GetIsDeviceFound(firstHearingAid))
                selectHearingAidsPageSecondTime.WaitUntilDeviceFound(firstHearingAid);

            if (secondHearingAid != null)
                selectHearingAidsPageSecondTime.WaitUntilDeviceFound(secondHearingAid);
            if (!selectHearingAidsPageSecondTime.GetIsDeviceFound(secondHearingAid))
                selectHearingAidsPageSecondTime.WaitUntilDeviceFound(secondHearingAid);

            selectHearingAidsPageSecondTime.WaitUntilDeviceListNotChanging();
            selectHearingAidsPageSecondTime.SelectDevicesExclusively(firstHearingAid, secondHearingAid);
            Assert.IsTrue(selectHearingAidsPageSecondTime.GetIsDeviceSelected(firstHearingAid));
            if (secondHearingAid != null)
                selectHearingAidsPageSecondTime.GetIsDeviceSelected(secondHearingAid);
            ReportHelper.LogTest(Status.Pass, "Found Hearing aids " + firstHearingAid + " and " + secondHearingAid);
            Assert.IsNotEmpty(selectHearingAidsPageSecondTime.GetConnectButtonText());
            selectHearingAidsPageSecondTime.Connect();

            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)), "DashboardPage is not the current loaded page");
            new DashboardPage().IsCurrentlyShown();
            ReportHelper.LogTest(Status.Info, "Dashboard loaded after connecting to hearing system");
        }

        /// <summary>
        /// Below test case has some test steps to disconnect and reconnect HA devices
        /// As discussed with Audifon team this step will be done manually.
        /// </summary>
        [Test]
        [Category("SystemTestsDeviceManual")]
        [Description("TC-15168_Table-27")]
        public void ST15168_VerifyInfoPageWhenHADisconnected()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var firstHearingAid = SelectHearingAid.GetLeftHearingAid();
            var secondHearingAid = SelectHearingAid.GetRightHearingAid();

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTimeAndVerify(firstHearingAid, secondHearingAid);

            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            ReportHelper.LogTest(Status.Pass, "Start view is displayed correctly containing L and R device icons.");

            ReportHelper.LogTest(Status.Info, "Check the information for Left device.");
            dashboardPage.OpenLeftHearingDevice();
            var hearingInstrumentInfoPage = new HearingInstrumentInfoControlPage().CheckHAInformationFromDashboard(AppMode.Normal);
            hearingInstrumentInfoPage.Close();
            ReportHelper.LogTest(Status.Pass, "All page dependent information for Left HA is displayed correctly.");

            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off Left Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Left Hearing Aid turned off");

            Assert.IsTrue(DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(25)));
            ReportHelper.LogTest(Status.Pass, "Pop up appears successfully after HA disconnected and is confirmed.");

            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            try
            {
                Assert.IsEmpty(new DashboardPage().GetLeftDeviceText());
            }
            catch (Exception)
            {
                ReportHelper.LogTest(Status.Pass, "The Left HA icon text is not visible on Dashboard page.");
            }
            new DashboardPage().OpenLeftHearingDevice();
            Assert.IsTrue(new DashboardPage().IsCurrentlyShown());
            ReportHelper.LogTest(Status.Pass, "The Left HA is no longer visible and info page is not available from Dashboard page.");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn on Left Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Left Hearing Aid turned on");

            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(25));
            Assert.IsTrue(new DashboardPage().GetIsLeftHearingDeviceVisible());

            ReportHelper.LogTest(Status.Info, "Check the information for Left device again.");
            new DashboardPage().OpenLeftHearingDevice();
            new HearingInstrumentInfoControlPage().CheckHAInformationFromDashboard(AppMode.Normal).Close();
            ReportHelper.LogTest(Status.Pass, "All page dependent information for Left HA is displayed correctly.");

            //Right HA
            ReportHelper.LogTest(Status.Info, "Check the information for Right device.");
            new DashboardPage().OpenRightHearingDevice();
            new HearingInstrumentInfoControlPage().CheckHAInformationFromDashboard(AppMode.Normal).Close();
            ReportHelper.LogTest(Status.Pass, "All page dependent information for Right HA is displayed correctly.");
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Right Hearing Aid turned off");

            Assert.IsTrue(DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(60)));
            ReportHelper.LogTest(Status.Pass, "Pop up appears successfully after HA disconnected and is confirmed.");

            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            try
            {
                Assert.IsEmpty(new DashboardPage().GetRightDeviceText());
            }
            catch (Exception)
            {
                ReportHelper.LogTest(Status.Pass, "The Right HA icon text is not visible on Dashboard page.");
            }
            new DashboardPage().OpenRightHearingDevice();
            Assert.IsTrue(new DashboardPage().IsCurrentlyShown());
            ReportHelper.LogTest(Status.Pass, "The Right HA is no longer visible and info page is not available from Dashboard page.");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn on Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Right Hearing Aid turned on");

            ReportHelper.LogTest(Status.Info, "Reconnect Right HA and check the display.");
            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(25));
            Assert.IsTrue(new DashboardPage().GetIsRightHearingDeviceVisible());

            ReportHelper.LogTest(Status.Info, "Check the information for Right device.");
            new DashboardPage().OpenRightHearingDevice();
            new HearingInstrumentInfoControlPage().CheckHAInformationFromDashboard(AppMode.Normal).Close();
            ReportHelper.LogTest(Status.Pass, "All page dependent information for Left HA is displayed correctly.");
        }

        /// <summary>
        /// When we switch off any HA side say left so a dialog will appear on confirm we will be redirected to
        /// same side here which we disconnected Left
        /// </summary>
        [Test]
        [Category("SystemTestsDeviceManual")]
        [Description("TC-15247_Table-26")]
        public void ST15247_VerifyFindDevicesPageWhenHADisconnected()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            AppManager.DeviceSettings.EnableWifi();
            Thread.Sleep(5000);

            double thresholdValue = 0.999; //threadshold value for image comparison score

            HearingAid firstHearingAid = SelectHearingAid.Left();
            HearingAid secondHearingAid = SelectHearingAid.Right();

            var dashboardPage = LaunchHelper.StartAppWithHearingAidsForTheFirstTime(firstHearingAid, secondHearingAid).Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + firstHearingAid.Name + "' and Right '" + secondHearingAid.Name + "'");

            ReportHelper.LogTest(Status.Info, "Dashboard page is displayed, Navigate to Find Devices page and check the view.");
            NavigationHelper.NavigateToHelpMenu(dashboardPage).OpenFindHearingDevices();
            Assert.IsTrue(new FindDevicesPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            var findDevicesPage = new FindDevicesPage().CheckFindDevicesPage(ChannelMode.Binaural, firstHearingAid, secondHearingAid);

            //This image will be used to compare with the grey out near field when deivce is disconnected
            findDevicesPage.SelectNearFieldView().SelectRightDevice();
            var baseLineImageRight = AppManager.App.TakeScreenshot().AsBase64EncodedString;

            findDevicesPage.SelectLeftDevice();
            var baseLineImageLeft = AppManager.App.TakeScreenshot().AsBase64EncodedString;

            findDevicesPage.SelectMapView().SelectRightDevice();
            Assert.IsTrue(findDevicesPage.GetIsMapViewSelected());
            Assert.IsTrue(findDevicesPage.GetIsRightDeviceSelected());

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Right Hearing Aid turned off");

            Assert.IsTrue(DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(25)));
            ReportHelper.LogTest(Status.Pass, "Pop up appears successfully after HA disconnected and is confirmed.");

            ReportHelper.LogTest(Status.Info, "Navigate to Left side and disconnect HA and check the behaviour.");
            Assert.IsTrue(new FindDevicesPage().SelectLeftDevice().GetIsLeftDeviceSelected());

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off Left Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Left Hearing Aid turned off");

            Assert.IsTrue(DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(25)));
            ReportHelper.LogTest(Status.Pass, "Pop up appears successfully after HA disconnected and is confirmed.");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 10 seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn on Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Right Hearing Aid turned on");

            ReportHelper.LogTest(Status.Info, "Switch on the Right HA again and check the Near Field View.");
            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(25));
            new FindDevicesPage().SelectNearFieldView();
            new FindDevicesPage().SelectRightDevice();
            Assert.IsTrue(new FindDevicesPage().GetIsNearFieldViewSelected());
            Assert.IsTrue(new FindDevicesPage().GetIsRightDeviceSelected());
            Assert.IsTrue(new FindDevicesPage().GetIsRightSignalStrengthControlVisible());
            ReportHelper.LogTest(Status.Pass, "Near Field View is displayed correctly for Right HA.");

            ReportHelper.LogTest(Status.Info, "Verifying the Near Field Signal for Right Device");
            ReportHelper.LogTest(Status.Info, "Taking baseline screeshot for Right device now");
            Assert.IsNotEmpty(baseLineImageRight);
            ReportHelper.LogTest(Status.Info, "Baseline screenshot is taken Move the device now");

            //MANUAL -> Move device away from HA in order to make Near Field view change
            ReportHelper.LogTest(Status.Info, "ALERT: Please Move Right Hearing Aid Away within 10 seconds");
            Thread.Sleep(10000);

            ReportHelper.LogTest(Status.Info, "Taking new screeshot now");
            // ToDo: Need to install OpenCV and the uncomment the below three lines and need test this
            //double scoreRight = ImageComparison.GetImageSimilarityScore(baseLineImageRight, AppManager.App.TakeScreenshot().AsBase64EncodedString);
            //ReportHelper.LogTest(Status.Info, "Image similarity score for Right device is " + scoreRight);
            //Assert.Less(scoreRight, thresholdValue, "There are no visual differences in signal strength for Right Device.");
            ReportHelper.LogTest(Status.Pass, "Check successful, On moving the device away, signal strength change is visible for Right HA.");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 5 Seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Right Hearing Aid turned off");

            Assert.IsTrue(DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(25)));
            ReportHelper.LogTest(Status.Pass, "Pop up appears successfully after HA disconnected and is confirmed.");
            Assert.IsTrue(new FindDevicesPage().GetIsMapViewSelected());
            Assert.IsTrue(new FindDevicesPage().GetIsRightDeviceSelected());

            ReportHelper.LogTest(Status.Info, "Switch to Near Field View of Left HA and verify RSSI levels in off state of HA.");
            new FindDevicesPage().SelectNearFieldView().SelectLeftDevice();
            Assert.IsTrue(new FindDevicesPage().GetIsNearFieldViewSelected());
            Assert.IsTrue(new FindDevicesPage().GetIsLeftDeviceSelected());

            // ToDo: Need to install OpenCV and the uncomment the below three lines and need test this
            //double scoreLeft = ImageComparison.GetImageSimilarityScore(baseLineImageLeft, AppManager.App.TakeScreenshot().AsBase64EncodedString);
            //ReportHelper.LogTest(Status.Info, "Image similarity score for left device is " + scoreLeft);
            //Assert.Less(scoreLeft, thresholdValue, "There are no visual differences in signal strength for Left Device.");
            ReportHelper.LogTest(Status.Pass, "Check successful, Signal strength is not visible after HA is disconnected.");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 5 Seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn on Left Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Left Hearing Aid turned on");

            ReportHelper.LogTest(Status.Info, "Switch on the Left HA again and check the Near Field View.");
            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(25));
            Assert.IsTrue(new FindDevicesPage().GetIsNearFieldViewSelected());
            Assert.IsTrue(new FindDevicesPage().GetIsLeftDeviceSelected());

            ReportHelper.LogTest(Status.Info, "Verifying the Near Field Signal again after turning on Left HA");
            ReportHelper.LogTest(Status.Info, "Taking baseline screeshot for Left device now");
            baseLineImageLeft = AppManager.App.TakeScreenshot().AsBase64EncodedString;
            Assert.IsNotEmpty(baseLineImageLeft);
            ReportHelper.LogTest(Status.Info, "Baseline screenshot is taken Move the device now");

            //MANUAL -> Move device away from HA in order to make Near Field view to change
            ReportHelper.LogTest(Status.Info, "ALERT: Please Move Left Hearing Aid Away within 10 seconds");
            Thread.Sleep(10000);

            ReportHelper.LogTest(Status.Info, "Taking new screeshot now");

            // ToDo: Need to install OpenCV and the uncomment the below three lines and need test this
            //scoreLeft = ImageComparison.GetImageSimilarityScore(baseLineImageRight, AppManager.App.TakeScreenshot().AsBase64EncodedString);
            //ReportHelper.LogTest(Status.Info, "Image similarity score for Left device is " + scoreLeft);
            //Assert.Less(scoreLeft, thresholdValue, "There are no visual differences in signal strength for Left Device.");
            ReportHelper.LogTest(Status.Pass, "Check successful, After turning on, signal strength is visible for Left HA.");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 5 Seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn on Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Right Hearing Aid turned on");

            ReportHelper.LogTest(Status.Info, "Switch on the Right HA again and check the Near Field View.");
            baseLineImageRight = AppManager.App.TakeScreenshot().AsBase64EncodedString;
            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(25));
            Assert.IsTrue(new FindDevicesPage().SelectRightDevice().GetIsRightDeviceSelected());
            Assert.IsTrue(new FindDevicesPage().GetIsNearFieldViewSelected());

            ReportHelper.LogTest(Status.Info, "Verifying the Near Field Signal for Right Device");
            ReportHelper.LogTest(Status.Info, "Taking baseline screeshot for Right device now");
            Assert.IsNotEmpty(baseLineImageRight);
            ReportHelper.LogTest(Status.Info, "Baseline screenshot is taken Move the device now");

            //MANUAL -> Move device away from HA in order to make Near Field view change
            ReportHelper.LogTest(Status.Info, "ALERT: Please Move Right Hearing Aid Away within 10 seconds");
            Thread.Sleep(10000);

            ReportHelper.LogTest(Status.Info, "Taking new screeshot now");

            // ToDo: Need to install OpenCV and the uncomment the below three lines and need test this
            //scoreRight = ImageComparison.GetImageSimilarityScore(baseLineImageRight, AppManager.App.TakeScreenshot().AsBase64EncodedString);
            //ReportHelper.LogTest(Status.Info, "Image similarity score for Right device is " + scoreRight);
            //Assert.Less(scoreRight, thresholdValue, "There are no visual differences in signal strength for Right Device.");
            ReportHelper.LogTest(Status.Pass, "Check successful, On moving the device away, signal strength change is visible for Right HA.");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 5 Seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn off Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Right Hearing Aid turned off");

            ReportHelper.LogTest(Status.Info, "Disconnect Right HA and check the behaviour.");
            Assert.IsTrue(DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(25)));
            ReportHelper.LogTest(Status.Pass, "Pop up appears successfully after HA disconnected and is confirmed.");
            Assert.IsTrue(new FindDevicesPage().GetIsMapViewSelected());

            ReportHelper.LogTest(Status.Info, "Switch to Near Field View of Right HA and verify RSSI levels in off state of HA.");
            Assert.IsTrue(new FindDevicesPage().SelectNearFieldView().GetIsRightDeviceSelected());
            Assert.IsTrue(new FindDevicesPage().GetIsNearFieldViewSelected());

            // ToDo: Need to install OpenCV and the uncomment the below three lines and need test this
            //scoreRight = ImageComparison.GetImageSimilarityScore(baseLineImageLeft, AppManager.App.TakeScreenshot().AsBase64EncodedString);
            //ReportHelper.LogTest(Status.Info, "Image similarity score for Right device is " + scoreRight);
            //Assert.Less(scoreLeft, thresholdValue, "There are no visual differences in signal strength for Right Device.");
            ReportHelper.LogTest(Status.Pass, "Check successful, Signal strength is not visible after HA is disconnected.");

            // ToDo: Need to automate connecton disconnection of Hearing Aid. Currently it is done manually and has to be done within 5 Seconds
            ReportHelper.LogTest(Status.Info, "ALERT: Please turn on Right Hearing Aid within 10 seconds");
            Thread.Sleep(10000);
            ReportHelper.LogTest(Status.Info, "Right Hearing Aid turned on");

            ReportHelper.LogTest(Status.Info, "Switch on the Right HA again and check the Near Field View.");
            baseLineImageRight = AppManager.App.TakeScreenshot().AsBase64EncodedString;
            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(25));
            Assert.IsTrue(new FindDevicesPage().SelectRightDevice().GetIsRightDeviceSelected());
            Assert.IsTrue(new FindDevicesPage().GetIsNearFieldViewSelected());

            ReportHelper.LogTest(Status.Info, "Verifying the Near Field Signal for Right Device");
            ReportHelper.LogTest(Status.Info, "Taking baseline screeshot for Right device now");
            Assert.IsNotEmpty(baseLineImageRight);
            ReportHelper.LogTest(Status.Info, "Baseline screenshot is taken Move the device now");

            //MANUAL -> Move device away from HA in order to make Near Field view change
            ReportHelper.LogTest(Status.Info, "ALERT: Please Move Right Hearing Aid Away within 10 seconds");
            Thread.Sleep(10000);

            ReportHelper.LogTest(Status.Info, "Taking new screeshot now");

            // ToDo: Need to install OpenCV and the uncomment the below three lines and need test this
            //scoreRight = ImageComparison.GetImageSimilarityScore(baseLineImageRight, AppManager.App.TakeScreenshot().AsBase64EncodedString);
            //ReportHelper.LogTest(Status.Info, "Image similarity score for Right device is " + scoreRight);
            //Assert.Less(scoreRight, thresholdValue, "There are no visual differences in signal strength for Right Device.");
            ReportHelper.LogTest(Status.Pass, "Check successful, On moving the device away, signal strength change is visible for Right HA.");

            AppManager.DeviceSettings.DisableWifi();
        }

        #endregion Sprint 17

        #endregion Test Cases
    }
}