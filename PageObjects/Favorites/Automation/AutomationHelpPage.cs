using System;
using HorusUITest.PageObjects.Menu;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Favorites.Automation
{
    public class AutomationHelpPage : BaseScrollViewNavigationPage<AutomationHelpPage>
    {
        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Favorites.Automation.AutomationHelpPage.NavigationBar");
        protected override AppiumWebElement MainScrollView => App.FindElementByAutomationId("Horus.Views.Favorites.Automation.AutomationHelpPage.MainScrollView");
        protected override AppiumWebElement TopOfScrollView => Description;
        protected override AppiumWebElement BottomOfScrollView => WifiDescription;

        private AppiumWebElement Description => App.FindElementByAutomationId("Horus.Views.Favorites.Automation.AutomationHelpPage.Description");
        private AppiumWebElement GeoHeader => App.FindElementByAutomationId("Horus.Views.Favorites.Automation.AutomationHelpPage.GeoHeader");
        private AppiumWebElement GeoDescription => App.FindElementByAutomationId("Horus.Views.Favorites.Automation.AutomationHelpPage.GeoDescription");
        private AppiumWebElement WifiHeader => App.FindElementByAutomationId("Horus.Views.Favorites.Automation.AutomationHelpPage.WifiHeader");
        private AppiumWebElement WifiDescription => App.FindElementByAutomationId("Horus.Views.Favorites.Automation.AutomationHelpPage.WifiDescription");

        public AutomationHelpPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public AutomationHelpPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        public string GetDescription() { return LocateElement(() => Description).Text; }
        public string GetGeoHeader() { return LocateElement(() => GeoHeader).Text; }
        public string GetGeoDescription() { return LocateElement(() => GeoDescription).Text; }
        public string GetWifiHeader() { return LocateElement(() => WifiHeader).Text; }
        public string GetWifiDescription() { return LocateElement(() => WifiDescription).Text; }
    }
}
