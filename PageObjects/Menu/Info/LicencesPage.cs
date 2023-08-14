using System;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu.Info
{
    public class LicencesPage : BaseNavigationPage
    {
        public LicencesPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public LicencesPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Menu.Info.LicensesPage.NavigationBar");
    }

    //TODO: Flesh out this page, once it's implemented.
}
