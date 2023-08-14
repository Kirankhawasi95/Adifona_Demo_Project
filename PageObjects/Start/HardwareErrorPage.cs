using System;
using System.Collections.Generic;
using HorusUITest.Enums;
using HorusUITest.PageObjects.Controls;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Start
{
    //TODO: Finish if necessary
    // -> automate whole ListView with all available options
    public class HardwareErrorPage : BasePage
    {
        private AppiumWebElement Header => App.FindElementByAutomationId("Horus.Views.Start.HardwareErrorPage.InitializationPageHeader");
        private AppiumWebElement Title => App.FindElementByAutomationId("Horus.Views.Start.HardwareErrorPage.Title");
        private AppiumWebElement Message => App.FindElementByAutomationId("Horus.Views.Start.HardwareErrorPage.Message");

        private AppiumWebElement ListViewElement => App.FindElementByAutomationId("Horus.Views.Start.HardwareErrorPage.ListView");
        private IReadOnlyCollection<AppiumWebElement> ListViewCells => App.FindElementsByAutomationId("Horus.Views.Start.HardwareErrorPage.ListViewCell");
        private IReadOnlyCollection<AppiumWebElement> ListViewGrids => App.FindElementsByAutomationId("Horus.Views.Start.HardwareErrorPage.ListViewGrid");
        private IReadOnlyCollection<AppiumWebElement> ListViewItems => OnAndroid ? ListViewGrids : ListViewCells;
        private Func<AppiumWebElement, AppiumWebElement> ItemTitle => (e) => App.FindElementByAutomationId("Horus.Views.Start.HardwareErrorPage.ItemTitle", root: e);
        private Func<AppiumWebElement, AppiumWebElement> ItemMessage => (e) => App.FindElementByAutomationId("Horus.Views.Start.HardwareErrorPage.ItemMessage", root: e);

        ListView listView;

        public HardwareErrorPage(bool assertOnPage = true) : base(assertOnPage)
        {
            listView = new ListView(() => ListViewElement, () => ListViewItems, ItemTitle, ItemMessage, ItemTitle);
        }

        public HardwareErrorPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
            listView = new ListView(() => ListViewElement, () => ListViewItems, ItemTitle, ItemMessage, ItemTitle);
        }

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = () => Header,
            iOS = () => Header
        };

        public void RetryProcess()
        {
            listView.ScrollToTop();
            App.Tap(listView.GetItem(0, IndexType.Relative));
        }

        public void StartDemoMode()
        {
            listView.ScrollToTop();
            App.Tap(listView.GetItem(1, IndexType.Relative));
        }

        /// <summary>
        /// When location is rovoked continue button appears in index 1
        /// </summary>
        public void Continue()
        {
            listView.ScrollToTop();
            App.Tap(listView.GetItem(1, IndexType.Relative));
        }

        public int GetNumberOfItems()
        {
            return listView.GetNumberOfItemsOnScreen();
        }
        
        public string GetTitleOfItem(int index)
        {
            listView.ScrollToTop();
            return listView.GetItem(index, IndexType.Relative).FindElementByAccessibilityId("Horus.Views.Start.HardwareErrorPage.ItemTitle").Text;
        }
        
        public string GetMessageOfItem(int index)
        {
            listView.ScrollToTop();
            return listView.GetItem(index, IndexType.Relative).FindElementByAccessibilityId("Horus.Views.Start.HardwareErrorPage.ItemMessage").Text;
        }
        
        public string GetPageTitle()
        {
            return Title.Text;
        }
        
        public string GetPageMessage()
        {
            return Message.Text;
        }
    }
}
