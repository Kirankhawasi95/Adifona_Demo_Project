using System;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu.Settings
{
    public class SettingConnectedDevicesPage : BaseNavigationPage
    {
        public SettingConnectedDevicesPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public SettingConnectedDevicesPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Menu.Settings.SettingConnectedDevicesPage.NavigationBar");

        //private AppiumWebElement  => App.FindElementByAutomationId("");
        //private AppiumWebElement  => App.FindElementByAutomationId("");
        //private AppiumWebElement  => App.FindElementByAutomationId("");
        //private AppiumWebElement  => App.FindElementByAutomationId("");
        //private AppiumWebElement  => App.FindElementByAutomationId("");
        //private AppiumWebElement  => App.FindElementByAutomationId("");
        //private AppiumWebElement  => App.FindElementByAutomationId("");
        //private AppiumWebElement  => App.FindElementByAutomationId("");

        //TODO: Find usage within app and give this page some functionalities.
    }
}
    