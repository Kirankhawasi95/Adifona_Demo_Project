using System;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu.Help.HelpTopics
{
    public class DisconnectVolumeControlHelpPage : BaseScrollableHelpPage
    {
        public DisconnectVolumeControlHelpPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public DisconnectVolumeControlHelpPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Menu.Help.HelpTopics.DisconnectVolumeControlHelpPage.NavigationBar");
    }
}
