using System;
using HorusUITest.Extensions;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Favorites.Automation
{
    public class AutomationWifiBindingPage : BaseProgramConfigPage
    {
        protected override Func<IMobileElement<AppiumWebElement>> NavigationBarQuery => () => NavigationBar;
        protected override Func<IMobileElement<AppiumWebElement>> CancelButtonQuery => () => null;        //no cancel button on this page

        private const string NavigationBarAID = "Horus.Views.Favorites.Automation.AutomationWifiBindingPage.NavigationBar";
        private const string DescriptionAID = "Horus.Views.Favorites.Automation.AutomationWifiBindingPage.Description";

        private const string WifiStatusAID = "Horus.Views.Favorites.Automation.AutomationWifiBindingPage.WifiStatus";
        private const string WifiNameAID = "Horus.Views.Favorites.Automation.AutomationWifiBindingPage.WifiName";

        private const string NoWifiPlatformSpecificDescriptionAID = "Horus.Views.Favorites.Automation.AutomationWifiBindingPage.NoWifiPlatformSpecificDescription";
        private const string ScanButtonAID = "Horus.Views.Favorites.Automation.AutomationWifiBindingPage.ScanButton";

        private const string OkButtonAID = "Horus.Views.Favorites.Automation.AutomationWifiBindingPage.OkButton";

        public AutomationWifiBindingPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public AutomationWifiBindingPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        [FindsByAndroidUIAutomator(Accessibility = NavigationBarAID), FindsByIOSUIAutomation(Accessibility = NavigationBarAID)]
        private IMobileElement<AppiumWebElement> NavigationBar { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = DescriptionAID), FindsByIOSUIAutomation(Accessibility = DescriptionAID)]
        private IMobileElement<AppiumWebElement> Description { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = WifiStatusAID), FindsByIOSUIAutomation(Accessibility = WifiStatusAID)]
        private IMobileElement<AppiumWebElement> WifiStatus { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = WifiNameAID), FindsByIOSUIAutomation(Accessibility = WifiNameAID)]
        private IMobileElement<AppiumWebElement> WifiName { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = NoWifiPlatformSpecificDescriptionAID), FindsByIOSUIAutomation(Accessibility = NoWifiPlatformSpecificDescriptionAID)]
        private IMobileElement<AppiumWebElement> NoWifiPlatformSpecificDescription { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = ScanButtonAID), FindsByIOSUIAutomation(Accessibility = ScanButtonAID)]
        private IMobileElement<AppiumWebElement> ScanButton { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = OkButtonAID), FindsByIOSUIAutomation(Accessibility = OkButtonAID)]
        private IMobileElement<AppiumWebElement> OkButton { get; set; }

        public string GetDescription()
        {
            return Description.Text;
        }

        public bool GetIsWifiFound()
        {
            return TryInvokeQuery(() => WifiName, out _);
        }

        /// <summary>
        /// Returns the Wi-Fi status, e.g. "Connected to Wi-Fi".
        /// </summary>
        /// <returns></returns>
        public string GetWifiStatus()
        {
            return WifiStatus.Text;
        }

        /// <summary>
        /// Returns the Wi-Fi SSID of the detected network.
        /// </summary>
        /// <returns>The WLAN SSID. Return <see cref="null"/> if no network has been found.</returns>
        public string GetWifiName()
        {
            return WifiName.GetTextOrNull();
        }

        /// <summary>
        /// Returns the platform specific hint that is shown when no WLAN was detected.
        /// </summary>
        /// <returns>Returns the hint. Returns <see cref="null"/> if no network has been found.</returns>
        public string GetNoWifiPlatformSpecificDescription()
        {
            return NoWifiPlatformSpecificDescription.GetTextOrNull();
        }

        /// <summary>
        /// Returns the text of the scan button.
        /// </summary>
        /// <returns>Returns the text. Returns <see cref="null"/> if no network has been found.</returns>
        public string GetScanButtonText()
        {
            return ScanButton.GetTextOrNull();
        }

        /// <summary>
        /// Presses the scan button. Throws if a network has already been found (scan button not visible).
        /// </summary>
        /// <returns></returns>
        public AutomationWifiBindingPage Scan()
        {
            App.Tap(ScanButton);
            return this;
        }

        /// <summary>
        /// Taps the button 'Ok' and navigates to <see cref="ProgramAutomationPage"/>.
        /// </summary>
        public void Ok()
        {
            App.Tap(OkButton);
        }

        /// <summary>
        /// Returns the text of the Ok button.
        /// </summary>
        /// <returns></returns>
        public string GetOkButtonText()
        {
            return OkButton.Text;
        }
    }
}
