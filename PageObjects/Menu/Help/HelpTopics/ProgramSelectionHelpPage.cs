using System;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu.Help.HelpTopics
{
    public class ProgramSelectionHelpPage : BaseScrollableHelpPage
    {
        public ProgramSelectionHelpPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public ProgramSelectionHelpPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Menu.Help.HelpTopics.ProgramSelectionHelpPage.NavigationBar");
    }
}
