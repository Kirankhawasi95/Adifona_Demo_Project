using System;
using HorusUITest.Helper;
using HorusUITest.PageObjects.Controls.Menu;
using HorusUITest.PageObjects.Menu.Settings;
using HorusUITest.PageObjects.Start;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.MultiTouch;

namespace HorusUITest.PageObjects.Menu
{
    public class SettingsMenuPage : BaseSubMenuPage
    {
        public SettingsMenuPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public SettingsMenuPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Menu.SettingsMenu.NavigationBar");

        private AppiumWebElement GroupHeaderHearingAid => App.FindElementByAutomationId("Horus.Views.Menu.SettingsMenu.settingsPage_SubTitle_HearingSystem");
        private AppiumWebElement MenuItemMyHearingAids => App.FindElementByAutomationId("Horus.Views.Menu.SettingsMenu.settingsPage_ConnectedDevices");

        private AppiumWebElement GroupHeaderAppSettings => App.FindElementByAutomationId("Horus.Views.Menu.SettingsMenu.settingsPage_SubTitle_AppSettings");
        private AppiumWebElement MenuItemPermissions => App.FindElementByAutomationId("Horus.Views.Menu.SettingsMenu.settingsPage_Permissions");
        private AppiumWebElement MenuItemLanguage => App.FindElementByAutomationId("Horus.Views.Menu.SettingsMenu.settingsPage_Language");
        private AppiumWebElement MenuItemDemoMode => App.FindElementByAutomationId("Horus.Views.Menu.SettingsMenu.settingsPage_AppMode");

        private AppiumWebElement GroupHeaderDevelopmentStuff => App.FindElementByAutomationId("Horus.Views.Menu.SettingsMenu.GroupHeaderDevelopmentStuff", verifyVisibility: true);
        private AppiumWebElement MenuItemLogs => App.FindElementByAutomationId("Horus.Views.Menu.SettingsMenu.Logs", verifyVisibility: true);
        private AppiumWebElement MenuItemAppReset => App.FindElementByAutomationId("Horus.Views.Menu.SettingsMenu.AppReset", verifyVisibility: true);
        private AppiumWebElement MenuItemHardwareErrorPage => App.FindElementByAutomationId("Horus.Views.Menu.SettingsMenu.HardwareErrorPage", verifyVisibility: true);
        private AppiumWebElement MenuItemConnectionErrorPage => App.FindElementByAutomationId("Horus.Views.Menu.SettingsMenu.ConnectionErrorPage", verifyVisibility: true);

        public string GetMyHearingAidsText()
        {
            return PageMenuButton.GetLabelOf(MenuItemMyHearingAids).Text;
        }

        public string GetPermissionsText()
        {
            return PageMenuButton.GetLabelOf(MenuItemPermissions).Text;
        }

        public string GetLanguageText()
        {
            return PageMenuButton.GetLabelOf(MenuItemLanguage).Text;
        }

        public string GetDemoModeText()
        {
            return PageMenuButton.GetLabelOf(MenuItemDemoMode).Text;
        }

        /// <summary>
        /// Navigates to <see cref="HearingSystemManagementPage"/>.
        /// </summary>
        public void OpenMyHearingAids()
        {
            App.Tap(MenuItemMyHearingAids);
        }

        /// <summary>
        /// Navigates to <see cref="SettingPermissionsPage"/>.
        /// </summary>
        public void OpenPermissions()
        {
            App.Tap(MenuItemPermissions);
        }

        /// <summary>
        /// Navigates to <see cref="SettingLanguagePage"/>.
        /// </summary>
        public void OpenLanguage()
        {
            App.Tap(MenuItemLanguage);
        }

        public void SwipeOnMenuItemLanguage()
        {
            App.SwipeRelativeToElementSize(MenuItemLanguage, 0.1, 0.5, 0.9, 0.5);
        }

        /// <summary>
        /// Navigates to <see cref="AppModeSelectionPage"/>.
        /// </summary>
        public void OpenDemoMode()
        {
            App.Tap(MenuItemDemoMode);
        }


        #region Hidden Development Features
        public bool GetIsDevelopmentStuffVisible()
        {
            return TryInvokeQuery(() => GroupHeaderDevelopmentStuff, out _);
        }

        public SettingsMenuPage ToggleDevelopmentStuff()
        {
            App.Tap(MenuGroupHeader.GetHeaderOf(null), numberOfTaps: 6);
            return this;
        }

        public SettingsMenuPage ShowDevelopmentStuff()
        {
            if (!GetIsDevelopmentStuffVisible())
                ToggleDevelopmentStuff();
            return this;
        }

        public SettingsMenuPage HideDevelopmentStuff()
        {
            if (GetIsDevelopmentStuffVisible())
                ToggleDevelopmentStuff();
            return this;
        }

        /// <summary>
        /// Tries to return a (potentially) hidden DevelopmentStuff element. If the first attempt fails, dev options are toggled.
        /// </summary>
        /// <param name="elementQuery"></param>
        /// <returns></returns>
        private AppiumWebElement GetElementSafely(Func<AppiumWebElement> elementQuery)
        {
            if (TryInvokeQuery(elementQuery, out var result))
                return result;
            else
            {
                ToggleDevelopmentStuff();
                return elementQuery.Invoke();
            }
        }

        public string GetLogsText()
        {
            return PageMenuButton.GetLabelOf(GetElementSafely(() => MenuItemLogs)).Text;
        }

        public string GetAppResetText()
        {
            return PageMenuButton.GetLabelOf(GetElementSafely(() => MenuItemAppReset)).Text;
        }

        public string GetHardwareErrorPageText()
        {
            return PageMenuButton.GetLabelOf(GetElementSafely(() => MenuItemHardwareErrorPage)).Text;
        }

        public string GetConnectionErrorPageText()
        {
            return PageMenuButton.GetLabelOf(GetElementSafely(() => MenuItemConnectionErrorPage)).Text;
        }

        //public string GetErrorPageText()
        //{
        //    return PageMenuButton.GetLabelOf(GetElementSafely(() => MenuItemHardwareErrorPage)).Text;
        //}

        /// <summary>
        /// Navigates to <see cref="LogPage"/>.
        /// </summary>
        public void OpenLogs()
        {
            App.Tap(GetElementSafely(() => MenuItemLogs));
        }

        /// <summary>
        /// Navigates to <see cref="InitializeHardwarePage"/>, potentially requesting a permission.
        /// </summary>
        public void OpenAppReset()
        {
            App.Tap(GetElementSafely(() => MenuItemAppReset));
        }

        public void ResetApp()
        {
            OpenAppReset();
            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(10));
        }

        /// <summary>
        /// Navigates to <see cref="HardwareErrorPage"/>.
        /// </summary>
        public void OpenHardwareErrorPage()
        {
            App.Tap(GetElementSafely(() => MenuItemHardwareErrorPage));
        }

        /// <summary>
        /// Navigates to <see cref="HearingAidConnectionErrorPage"/>.
        /// </summary>
        public void OpenConnectionErrorPage()
        {
            App.Tap(GetElementSafely(() => MenuItemConnectionErrorPage));
        }
        #endregion
    }
}
