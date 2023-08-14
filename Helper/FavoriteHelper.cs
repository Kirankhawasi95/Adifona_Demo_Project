namespace HorusUITest.Helper
{
    using AventStack.ExtentReports;
    using HorusUITest.PageObjects.Controls;
    using HorusUITest.PageObjects.Favorites.Automation;
    using HorusUITest.PageObjects.Favorites;
    using HorusUITest.PageObjects;
    using NUnit.Framework;
    using System;
    using System.Threading;
    using HorusUITest.Enums;

    public static class FavoriteHelper
    {
        private static bool OnAndroid => AppManager.Platform == Platform.Android;
        private static bool OniOS => AppManager.Platform == Platform.iOS;

        public static ProgramAutomationPage CreateFavoriteHearingProgram(string favoriteName, int programIconIndex)
        {
            ReportHelper.LogTest(Status.Info, "Opening program settings...");
            new ProgramDetailPage().OpenProgramSettings();
            var programDetailSettingsControlPage = new ProgramDetailSettingsControlPage();
            ReportHelper.LogTest(Status.Info, "Opened program settings");
            ReportHelper.LogTest(Status.Info, "Checking Program Details Settings UI elements...");
            ReportHelper.LogTest(Status.Info, "Checking if Name option is visible...");
            Assert.IsTrue(programDetailSettingsControlPage.GetIsCustomizeNameVisible(), "Customize Name option is not visible");
            ReportHelper.LogTest(Status.Info, "Customize Name option is visible");
            ReportHelper.LogTest(Status.Info, "Checking if Icon option is visible...");
            Assert.IsTrue(programDetailSettingsControlPage.GetIsCustomizeIconVisible(), "Customize Icon option is not visible");
            ReportHelper.LogTest(Status.Info, "Customize Icon option is visible");
            ReportHelper.LogTest(Status.Info, "Checking if Hearing Program option is visible...");
            Assert.IsTrue(programDetailSettingsControlPage.GetIsCreateFavoriteVisible(), "Create Hearing Program option is not visible");
            ReportHelper.LogTest(Status.Info, "Create Hearing Program option is visible");
            ReportHelper.LogTest(Status.Info, "Program Details Settings UI elements is verified");

            ReportHelper.LogTest(Status.Info, "Creating a favorite with name '" + favoriteName + "' and with icon index '" + programIconIndex + "'...");
            programDetailSettingsControlPage = new ProgramDetailSettingsControlPage();
            programDetailSettingsControlPage.CreateFavorite();
            ReportHelper.LogTest(Status.Info, "Checking if Program Name page is loaded after clicking Create Favorite...");
            var programNamePage = new ProgramNamePage();
            Assert.IsTrue(programNamePage.IsCurrentlyShown(), "Program Name page is not loaded");
            ReportHelper.LogTest(Status.Info, "Program Name page is loaded");
            programNamePage.EnterName(favoriteName);
            ReportHelper.LogTest(Status.Info, "Checking if Program Name is set...");
            Assert.IsNotEmpty(programNamePage.GetName(), "Program Name is not set");
            ReportHelper.LogTest(Status.Info, "Program Name is set");
            ReportHelper.LogTest(Status.Info, "Checking Proceed button text in Program Name page...");
            Assert.IsNotEmpty(programNamePage.GetProceedButtonText(), "Proceed button text is empty in Program Name page");
            ReportHelper.LogTest(Status.Info, "Proceed button text is not empty in Program Name page");
            ReportHelper.LogTest(Status.Info, "Clicking on proceed button...");
            programNamePage.Proceed();
            ReportHelper.LogTest(Status.Info, "Proceed button clicked");
            ReportHelper.LogTest(Status.Info, "Checking if Program Icon page is loaded after clicking Proceed...");
            var programIconPage = new ProgramIconPage();
            Assert.IsTrue(programIconPage.IsCurrentlyShown(), "Program Icon page is not loaded");
            ReportHelper.LogTest(Status.Info, "Proceed button clicked and icon page is loaded");
            ReportHelper.LogTest(Status.Info, "Checking cancel button text in Program Icon page...");
            Assert.IsNotEmpty(programIconPage.GetCancelButtonText(), "Cancel button text in Program Icon page is empty");
            ReportHelper.LogTest(Status.Info, "Cancel button text in Program Icon page is not empty");
            ReportHelper.LogTest(Status.Info, "Checking proceed button text in Program Icon page...");
            Assert.IsNotEmpty(programIconPage.GetProceedButtonText(), "Proceed button text in Program Icon page is empty");
            ReportHelper.LogTest(Status.Info, "Proceed button text in Program Icon page is not empty");
            ReportHelper.LogTest(Status.Info, "Checking selected icon text in Program Icon page...");
            Assert.IsNotEmpty(programIconPage.GetSelectedIconText(), "Selected icon text in Program Icon page is empty");
            ReportHelper.LogTest(Status.Info, "Selected icon text in Program Icon page is not empty");
            ReportHelper.LogTest(Status.Info, "Selecting program icon...");
            programIconPage.SelectIcon(programIconIndex);
            ReportHelper.LogTest(Status.Info, "Program icon selected");
            ReportHelper.LogTest(Status.Info, "Clicking proceed button...");
            programIconPage.Proceed();
            ReportHelper.LogTest(Status.Info, "Clicked proceed button");
            ReportHelper.LogTest(Status.Info, "Checking if Program Automation page is loaded after clicking Proceed...");
            var programAutomationPage = new ProgramAutomationPage();
            ReportHelper.LogTest(Status.Info, "Proceed button clicked and Program Automation page is loaded");
            ReportHelper.LogTest(Status.Info, "Checking if Progam automation is switched off by default...");
            Assert.IsFalse(programAutomationPage.GetIsAutomationSwitchChecked(), "Program automation is not switched off by default");
            ReportHelper.LogTest(Status.Info, "Program automation is switched off by default");
            ReportHelper.LogTest(Status.Info, "Turning on program automation switch...");
            programAutomationPage.TurnOnAutomation();
            if (DialogHelper.ConfirmIfDisplayed())
                Wait.UntilTrue(programAutomationPage.IsCurrentlyShown, TimeSpan.FromMilliseconds(2500));
            Assert.IsTrue(programAutomationPage.GetIsAutomationSwitchChecked(), "Program automation switch is not turned on");
            ReportHelper.LogTest(Status.Info, "Program automation switch is turned on");
            ReportHelper.LogTest(Status.Info, "Checking geofence automation is visible...");
            Assert.IsTrue(programAutomationPage.GetIsGeofenceAutomationVisible(), "Geofence automation is not visible");
            ReportHelper.LogTest(Status.Info, "Geofence automation is visible");
            ReportHelper.LogTest(Status.Info, "Checking wifi automation is visible...");
            Assert.IsTrue(programAutomationPage.GetIsWifiAutomationVisible(), "Wifi automation is not visible");
            ReportHelper.LogTest(Status.Info, "Wifi automation is visible");
            return programAutomationPage;
        }

        public static void SelectLocationAndCreateFavorite(string FavoriteName, double percentX, double percentY)
        {
            if (OnAndroid)
            {
                ReportHelper.LogTest(Status.Info, "Granting 'Always Allow' location access...");
                AppManager.DeviceSettings.GrantGPSBackgroundPermission();
                Thread.Sleep(5000);
                ReportHelper.LogTest(Status.Info, "'Always Allow' location access granted");
            }
            ReportHelper.LogTest(Status.Info, "Tapping 'Connect to a location' to set as auto start...");
            new ProgramAutomationPage().TapConnectToLocation();
            if (PermissionHelper.AllowPermissionIfRequested<AutomationGeofenceBindingPage>())
                ReportHelper.LogTest(Status.Info, "Granted permission for GPS");
            Thread.Sleep(3000);
            ReportHelper.LogTest(Status.Info, "Tapped 'Connect to a location' and set as auto start");
            ReportHelper.LogTest(Status.Info, "Selecting map location...");
            new AutomationGeofenceBindingPage().WaitUntilNoLoadingIndicator().SelectPosition(percentX, percentY);
            Thread.Sleep(1000);
            ReportHelper.LogTest(Status.Info, "Selected map location");
            ReportHelper.LogTest(Status.Info, "Clicking OK...");
            new AutomationGeofenceBindingPage().Ok();
            ReportHelper.LogTest(Status.Info, "Clicked OK");
            ReportHelper.LogTest(Status.Pass, "Map location is selected successfully app does not crash");
            ReportHelper.LogTest(Status.Info, "Waiting till program automation page is loaded...");
            Assert.IsTrue(new ProgramAutomationPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Program automation page is not loaded");
            ReportHelper.LogTest(Status.Info, "Program automation page is loaded");
            ReportHelper.LogTest(Status.Info, "Checking if location value is set...");
            Assert.IsTrue(new ProgramAutomationPage().GeofenceAutomation.GetIsValueSet(), "Location value is not set");
            ReportHelper.LogTest(Status.Info, "Location value is set");
            ReportHelper.LogTest(Status.Info, "Checking location value...");
            Assert.IsNotEmpty(new ProgramAutomationPage().GeofenceAutomation.GetValue(), "Location value is empty");
            ReportHelper.LogTest(Status.Info, "Location value is not empty");
            ReportHelper.LogTest(Status.Info, "Checking if delete icon is visible...");
            Assert.IsTrue(new ProgramAutomationPage().GeofenceAutomation.GetIsDeleteIconVisible(), "Delete icon is not visible");
            ReportHelper.LogTest(Status.Info, "Delete icon is visible");
            ReportHelper.LogTest(Status.Info, "Checking if settings icon is visible...");
            Assert.IsTrue(new ProgramAutomationPage().GeofenceAutomation.GetIsSettingsIconVisible(), "Settings icon is not visible");
            ReportHelper.LogTest(Status.Info, "Settings icon is visible");
            ReportHelper.LogTest(Status.Info, "Clicking proceed button...");
            new ProgramAutomationPage().Proceed();
            ReportHelper.LogTest(Status.Info, "Clicked proceed button");
            ReportHelper.LogTest(Status.Info, "Waiting till program details page is loaded...");
            Assert.IsTrue(new ProgramDetailPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(10)), "Program details page is not loaded");
            ReportHelper.LogTest(Status.Info, "Program details page is loaded");
            ReportHelper.LogTest(Status.Info, "Checking favorite program is selected...");
            Assert.AreEqual(FavoriteName, new ProgramDetailPage().GetCurrentProgramName(), "Favorite program is not selected");
            ReportHelper.LogTest(Status.Info, "Favorite program is selected");
        }

        public static ProgramAutomationPage CreateFavoriteHearingProgramWithoutCheck(string favoriteName, int programIconIndex)
        {
            ReportHelper.LogTest(Status.Info, "Opening program settings...");
            new ProgramDetailPage().OpenProgramSettings();
            var programDetailSettingsControlPage = new ProgramDetailSettingsControlPage();
            ReportHelper.LogTest(Status.Info, "Opened program settings");

            ReportHelper.LogTest(Status.Info, "Creating a favorite with name '" + favoriteName + "' and with icon index '" + programIconIndex + "'...");
            ReportHelper.LogTest(Status.Info, "Checking if Program Name page is loaded after clicking Create Favorite...");
            programDetailSettingsControlPage.CreateFavorite();
            var programNamePage = new ProgramNamePage();
            ReportHelper.LogTest(Status.Info, "Program Name page is loaded");
            ReportHelper.LogTest(Status.Info, "Setting program name...");
            programNamePage.EnterName(favoriteName);
            ReportHelper.LogTest(Status.Info, "Program name set");
            ReportHelper.LogTest(Status.Info, "Checking program name...");
            Assert.IsNotEmpty(programNamePage.GetName(), "Program name is empty");
            ReportHelper.LogTest(Status.Info, "Program name is not empty");
            ReportHelper.LogTest(Status.Info, "Clicking on proceed button...");
            programNamePage.Proceed();
            ReportHelper.LogTest(Status.Info, "Proceed button clicked");
            ReportHelper.LogTest(Status.Info, "Checking if Program Icon page is loaded after clicking Proceed...");
            var programIconPage = new ProgramIconPage();
            ReportHelper.LogTest(Status.Info, "Proceed button clicked and icon page is loaded");
            ReportHelper.LogTest(Status.Info, "Selecting program icon...");
            programIconPage.SelectIcon(programIconIndex);
            ReportHelper.LogTest(Status.Info, "Program icon selected");
            ReportHelper.LogTest(Status.Info, "Clicking proceed button...");
            programIconPage.Proceed();
            ReportHelper.LogTest(Status.Info, "Clicked proceed button");
            ReportHelper.LogTest(Status.Info, "Checking if Program Automation page is loaded after clicking Proceed...");
            var programAutomationPage = new ProgramAutomationPage();
            ReportHelper.LogTest(Status.Info, "Proceed button clicked and Program Automation page is loaded");

            return programAutomationPage;
        }
    }
}