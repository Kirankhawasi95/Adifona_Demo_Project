using System;
using System.Threading;
using HorusUITest.Helper;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Dialogs;
using HorusUITest.PageObjects.Favorites.Automation;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Favorites
{
    public class ProgramAutomationPage : BaseProgramConfigPage
    {
        protected override Func<IMobileElement<AppiumWebElement>> NavigationBarQuery => () => NavigationBar;
        protected override Func<IMobileElement<AppiumWebElement>> CancelButtonQuery => () => CancelButton;

        private const string NavigationBarAID = "Horus.Views.Favorites.ProgramAutomationPage.NavigationBar";
        private const string DescriptionAID = "Horus.Views.Favorites.ProgramAutomationPage.Description";
        private const string AutomationSwitchTitleAID = "Horus.Views.Favorites.ProgramAutomationPage.AutomationSwitchTitle";
        private const string AutomationSwitchAID = "Horus.Views.Favorites.ProgramAutomationPage.AutomationSwitch";
        private const string WifiButtonAID = "Horus.Views.Favorites.ProgramAutomationPage.WifiButton";
        private const string GeofenceButtonAID = "Horus.Views.Favorites.ProgramAutomationPage.GeofenceButton";
        private const string CancelButtonAID = "Horus.Views.Favorites.ProgramAutomationPage.CancelButton";
        private const string ProceedButtonAID = "Horus.Views.Favorites.ProgramAutomationPage.ProceedButton";

        [FindsByAndroidUIAutomator(Accessibility = NavigationBarAID), FindsByIOSUIAutomation(Accessibility = NavigationBarAID)]
        private IMobileElement<AppiumWebElement> NavigationBar { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = DescriptionAID), FindsByIOSUIAutomation(Accessibility = DescriptionAID)]
        private IMobileElement<AppiumWebElement> Description { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = AutomationSwitchTitleAID), FindsByIOSUIAutomation(Accessibility = AutomationSwitchTitleAID)]
        private IMobileElement<AppiumWebElement> AutomationSwitchTitle { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = AutomationSwitchAID), FindsByIOSUIAutomation(Accessibility = AutomationSwitchAID)]
        private IMobileElement<AppiumWebElement> AutomationSwitch { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = WifiButtonAID), FindsByIOSUIAutomation(Accessibility = WifiButtonAID)]
        private IMobileElement<AppiumWebElement> WifiButton { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = GeofenceButtonAID), FindsByIOSUIAutomation(Accessibility = GeofenceButtonAID)]
        private IMobileElement<AppiumWebElement> GeofenceButton { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = CancelButtonAID), FindsByIOSUIAutomation(Accessibility = CancelButtonAID)]
        private IMobileElement<AppiumWebElement> CancelButton { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = ProceedButtonAID), FindsByIOSUIAutomation(Accessibility = ProceedButtonAID)]
        private IMobileElement<AppiumWebElement> ProceedButton { get; set; }

        public ProgramAutomationPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public ProgramAutomationPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        public string GetDescription()
        {
            return Description.Text;
        }

        /// <summary>
        /// Taps the button 'Save Favorite' or 'Accept' respectively, depending on the current workflow.
        /// 'Save Favorite' navigates to <see cref="ProgramDetailPage"/>. 'Accept' navigates to <see cref="ProgramDetailSettingsControlPage"/>.
        /// </summary>
        public void Proceed()
        {
            App.Tap(ProceedButton);
        }

        public string GetProceedButtonText()
        {
            App.HideKeyboard();
            return ProceedButton.Text;
        }

        public string GetAutomationSwitchTitle()
        {
            return AutomationSwitchTitle.Text;
        }

        public bool GetIsAutomationSwitchChecked()
        {
            return App.GetToggleSwitchState(AutomationSwitch);
        }

        public ProgramAutomationPage ToggleBinauralSwitch()
        {
            App.Tap(AutomationSwitch);
            return this;
        }

        public void TapConnectToWiFi()
        {
            App.Tap(WifiButton);
        }

        public void TapConnectToLocation()
        {
            App.Tap(GeofenceButton);
            DialogHelper.ConfirmIfDisplayed();
            PermissionHelper.HandlePermissionForGeoFenceAutomation();
            //Select Map location after this
        }

        public ProgramAutomationPage TurnOnAutomation()
        {
            if (!GetIsAutomationSwitchChecked())
            {
                ToggleBinauralSwitch();
            }
            DialogHelper.ConfirmIfDisplayed();
            return this;
        }

        public ProgramAutomationPage TurnOffAutomation()
        {
            if (GetIsAutomationSwitchChecked())
            {
                ToggleBinauralSwitch();
            }
            return this;
        }

        public bool GetIsWifiAutomationVisible()
        {
            return TryInvokeQuery(() => WifiButton, out _);
        }

        public bool GetIsGeofenceAutomationVisible()
        {
            return TryInvokeQuery(() => GeofenceButton, out _);
        }

        private AutomationSelectionButton<ProgramAutomationPage> wifiAutomation;
        public AutomationSelectionButton<ProgramAutomationPage> WifiAutomation
        {
            get
            {
                wifiAutomation = wifiAutomation ?? new AutomationSelectionButton<ProgramAutomationPage>(this, WifiButton);
                return wifiAutomation;
            }
        }

        private AutomationSelectionButton<ProgramAutomationPage> geofenceAutomation;
        public AutomationSelectionButton<ProgramAutomationPage> GeofenceAutomation
        {
            get
            {
                geofenceAutomation = geofenceAutomation ?? new AutomationSelectionButton<ProgramAutomationPage>(this, GeofenceButton);
                return geofenceAutomation;
            }
        }

        /// <summary>
        /// Navigates to <see cref="AutomationHelpPage"/>.
        /// </summary>
        public void OpenHelpPage()
        {
            TapRightNavigationBarButton();
        }
    }
}
