using System;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu.Help.HelpTopics
{
    public class ConnectHearingAidsHelpPage : BaseScrollableHelpPage
    {
        public ConnectHearingAidsHelpPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public ConnectHearingAidsHelpPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Menu.Help.HelpTopics.ConnectHearingAidsHelpPage.NavigationBar");
    }
}
