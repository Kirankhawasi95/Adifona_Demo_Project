using System;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu.Help.HelpTopics
{
    public abstract class BaseScrollableHelpPage : BaseScrollViewNavigationPage<BaseScrollableHelpPage>
    {
        public BaseScrollableHelpPage(bool assertOnPage) : base(assertOnPage)
        {
        }

        public BaseScrollableHelpPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override AppiumWebElement MainScrollView => App.FindElementByAutomationId("GenericHelpTopic.MainScrollView");
        protected override AppiumWebElement TopOfScrollView => App.FindElementByAutomationId("GenericHelpTopic.BeginOfScrollView", verifyVisibility: true);
        protected override AppiumWebElement BottomOfScrollView => App.FindElementByAutomationId("GenericHelpTopic.EndOfScrollView", verifyVisibility: true);
    }
}
