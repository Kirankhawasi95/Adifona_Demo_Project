using System;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu.Help.HelpTopics
{
    public class AutomaticProgramHelpPage : BaseNavigationPage
    {
        public AutomaticProgramHelpPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public AutomaticProgramHelpPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Menu.Help.HelpTopics.AutomaticProgramHelpPage.NavigationBar");
    }
}
