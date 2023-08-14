
using System;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu.Info
{
    public class TermsOfUsePage : BaseNavigationPage
    {
        public TermsOfUsePage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public TermsOfUsePage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Menu.Info.TermsOfUsePage.NavigationBar");
    }

    //TODO: Flesh out this page, once it's implemented.
}
