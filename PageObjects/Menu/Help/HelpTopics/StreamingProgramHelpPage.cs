using System;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu.Help.HelpTopics
{
    public class StreamingProgramHelpPage : BaseScrollableHelpPage
    {
        public StreamingProgramHelpPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public StreamingProgramHelpPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Menu.Help.HelpTopics.StreamingProgramHelpPage.NavigationBar");
    }
}
