using System;
using HorusUITest.PageObjects.Controls.Menu;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu.Settings
{
    public class SettingPermissionsPage : BaseNavigationPage
    {
        public SettingPermissionsPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public SettingPermissionsPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Menu.Settings.SettingPermissionsPage.NavigationBar");

        private AppiumWebElement LocationGroupHeader => App.FindElementByAutomationId("Horus.Views.Menu.Settings.SettingPermissionsPage.LocationGroupHeader");
        private AppiumWebElement LocationLabel => App.FindElementByAutomationId("Horus.Views.Menu.Settings.SettingPermissionsPage.LocationLabel");
        private AppiumWebElement LocationSwitch => App.FindElementByAutomationId("Horus.Views.Menu.Settings.SettingPermissionsPage.LocationSwitch");

        public string GetLocationGroupHeader()
        {
            return MenuGroupHeader.GetHeaderOf(LocationGroupHeader).Text;
        }

        public string GetLocationPermissionText()
        {
            return LocationLabel.Text;
        }

        public bool GetIsLocationPermissionSwitchChecked()
        {
            return App.GetToggleSwitchState(LocationSwitch);
        }

        /// <summary>
        /// Toggles the location permission switch without confirming the dialog.
        /// </summary>
        /// <returns></returns>
        public SettingPermissionsPage ToggleLocationSwitch()
        {
            App.Tap(LocationSwitch);
            return this;
        }

        /// <summary>
        /// Turns on the location permission and confirms the dialog. Does nothing if already turned on.
        /// </summary>
        /// <returns></returns>
        public SettingPermissionsPage TurnOnLocationPermission()
        {
            if (!GetIsLocationPermissionSwitchChecked())
                ToggleLocationSwitch();
            return this;
        }

        /// <summary>
        /// Turns off the location permission and confirms the dialog. Does nothing if already turned off.
        /// </summary>
        /// <returns></returns>
        public SettingPermissionsPage TurnOffLocationPermission()
        {
            if (GetIsLocationPermissionSwitchChecked())
                ToggleLocationSwitch();
            return this;
        }
    }
}
