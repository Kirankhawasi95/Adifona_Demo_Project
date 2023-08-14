using System;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu.Help.HelpTopics
{
    public class MainPageHelpPage : BaseNavigationPage
    {
        public MainPageHelpPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public MainPageHelpPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Menu.Help.HelpTopics.MainPageHelpPage.NavigationBar");
    }
}
