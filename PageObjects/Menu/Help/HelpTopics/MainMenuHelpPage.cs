using System;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu.Help.HelpTopics
{
    public class MainMenuHelpPage : BaseNavigationPage
    {
        public MainMenuHelpPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public MainMenuHelpPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Menu.Help.HelpTopics.MainMenuHelpPage.NavigationBar");
    }
}
