using System;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu.Help.HelpTopics
{
    public class DisconnectHearingAidsHelpPage : BaseScrollableHelpPage
    {
        public DisconnectHearingAidsHelpPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public DisconnectHearingAidsHelpPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Menu.Help.HelpTopics.DisconnectHearingAidsHelpPage.NavigationBar");
    }
}
