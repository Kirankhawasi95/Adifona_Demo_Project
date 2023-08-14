using System;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu.Info
{
    public class PrivacyPolicyPage : BaseNavigationPage
    {
        public PrivacyPolicyPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public PrivacyPolicyPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Menu.Info.PrivacyPolicyPage.NavigationBar");
    }

    //TODO: Flesh out this page, once it's implemented.
}
