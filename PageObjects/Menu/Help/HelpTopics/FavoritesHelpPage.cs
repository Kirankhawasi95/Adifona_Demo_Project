using System;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu.Help.HelpTopics
{
    public class FavoritesHelpPage : BaseScrollableHelpPage
    {
        public FavoritesHelpPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public FavoritesHelpPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Menu.Help.HelpTopics.FavoritesHelpPage.NavigationBar");
    }
}
