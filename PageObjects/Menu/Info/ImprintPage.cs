using System;
using System.Net;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu.Info
{
    public class ImprintPage : BaseScrollViewNavigationPage<ImprintPage>
    {
        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Menu.Info.ImprintPage.NavigationBar");

        protected override AppiumWebElement MainScrollView => App.FindElementByAutomationId("Horus.Views.Menu.Info.ImprintPage.MainScrollView");
        protected override AppiumWebElement TopOfScrollView => AppHeader;
        protected override AppiumWebElement BottomOfScrollView => ManufacturerPostalCodeCity;

        private AppiumWebElement AppHeader => App.FindElementByAutomationId("Horus.Views.Menu.Info.ImprintPage.AppHeader", verifyVisibility: true);
        private AppiumWebElement Version => App.FindElementByAutomationId("Horus.Views.Menu.Info.ImprintPage.Version");
        private AppiumWebElement BuildNumber => App.FindElementByAutomationId("Horus.Views.Menu.Info.ImprintPage.Build");

        private AppiumWebElement AddressHeader => App.FindElementByAutomationId("Horus.Views.Menu.Info.ImprintPage.AddressHeader");
        private AppiumWebElement AppCompanyName => App.FindElementByAutomationId("Horus.Views.Menu.Info.ImprintPage.AppCompanyName");
        private AppiumWebElement AppCompanyStreet => App.FindElementByAutomationId("Horus.Views.Menu.Info.ImprintPage.AppCompanyStreet");
        private AppiumWebElement AppCompanyPostalCodeCity => App.FindElementByAutomationId("Horus.Views.Menu.Info.ImprintPage.AppCompanyPostalCodeCity");
        private AppiumWebElement AppCommpanyState => App.FindElementByAutomationId("Horus.Views.Menu.Info.ImprintPage.AppCommpanyState");

        private AppiumWebElement SupportHeader => App.FindElementByAutomationId("Horus.Views.Menu.Info.ImprintPage.SupportHeader");
        private AppiumWebElement SupportDescription => App.FindElementByAutomationId("Horus.Views.Menu.Info.ImprintPage.SupportDescription");
        private AppiumWebElement SupportWebsite => App.FindElementByAutomationId("Horus.Views.Menu.Info.ImprintPage.SupportWebsite");

        private AppiumWebElement ManufacturerStaticLabel => App.FindElementByAutomationId("Horus.Views.Menu.Info.ImprintPage.Manufacturer");
        private AppiumWebElement ManufacturerCompanyName => App.FindElementByAutomationId("Horus.Views.Menu.Info.ImprintPage.ManufacturerCompanyName");
        private AppiumWebElement ManufacturerCompanyStreet => App.FindElementByAutomationId("Horus.Views.Menu.Info.ImprintPage.ManufacturerCompanyStreet");
        private AppiumWebElement ManufacturerPostalCodeCity => App.FindElementByAutomationId("Horus.Views.Menu.Info.ImprintPage.ManufacturerPostalCodeCity", verifyVisibility: true);

        public ImprintPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public ImprintPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        public string GetAppHeader()
        {
            return LocateElement(() => AppHeader).Text;
        }

        public string GetVersion()
        {
            return LocateElement(() => Version).Text;
        }

        public string GetBuildNumber()
        {
            return LocateElement(() => BuildNumber).Text;
        }

        public string GetAddressHeader()
        {
            return LocateElement(() => AddressHeader).Text;
        }

        public string GetAppCompanyName()
        {
            return LocateElement(() => AppCompanyName).Text;
        }

        public string GetAppCompanyStreet()
        {
            return LocateElement(() => AppCompanyStreet).Text;
        }

        public string GetAppCompanyPostalCodeCity()
        {
            return LocateElement(() => AppCompanyPostalCodeCity).Text;
        }

        public string GetAppCommpanyState()
        {
            return LocateElement(() => AppCommpanyState).Text;
        }

        public string GetSupportHeader()
        {
            return LocateElement(() => SupportHeader).Text;
        }

        public string GetSupportDescription()
        {
            return LocateElement(() => SupportDescription).Text;
        }

        public string GetSupportWebsite()
        {
            return LocateElement(() => SupportWebsite).Text;
        }

        public string GetManufacturerStaticLabel()
        {
            return LocateElement(() => ManufacturerStaticLabel).Text;
        }

        public string GetManufacturerCompanyName()
        {
            return LocateElement(() => ManufacturerCompanyName).Text;
        }

        public string GetManufacturerCompanyStreet()
        {
            return LocateElement(() => ManufacturerCompanyStreet).Text;
        }

        public string GetManufacturerPostalCodeCity()
        {
            return LocateElement(() => ManufacturerPostalCodeCity).Text;
        }

        public void OpenSupportWebsite()
        {
            App.Tap(SupportWebsite);
        }

        public bool GetIsSupportWebsiteValid()
        {
            string url = "https://" + GetSupportWebsite();
            var request = HttpWebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return true;

                case HttpStatusCode.Accepted:
                    return true;

                default:
                    return false;
            }

        }
    }
}
