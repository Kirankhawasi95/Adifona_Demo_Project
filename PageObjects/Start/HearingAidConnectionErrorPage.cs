using System;
using System.Collections.Generic;
using HorusUITest.Enums;
using HorusUITest.PageObjects.Controls;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Start
{
    //TODO: Finish if necessary (currently only retrying to connect and starting the demo mode is supported)
    // -> automate whole ListView with all available options
    public class HearingAidConnectionErrorPage : BasePage
    {
        private AppiumWebElement Header => App.FindElementByAutomationId("Horus.Views.Start.ConnectionErrorPage.InitializationPageHeader");
        private AppiumWebElement Title => App.FindElementByAutomationId("Horus.Views.Start.ConnectionErrorPage.Title");
        private AppiumWebElement Message => App.FindElementByAutomationId("Horus.Views.Start.ConnectionErrorPage.Message");

        private AppiumWebElement ListViewElement => App.FindElementByAutomationId("Horus.Views.Start.ConnectionErrorPage.ListView");
        private IReadOnlyCollection<AppiumWebElement> ListViewCells => App.FindElementsByAutomationId("Horus.Views.Start.ConnectionErrorPage.ListViewCell");
        private IReadOnlyCollection<AppiumWebElement> ListViewGrids => App.FindElementsByAutomationId("Horus.Views.Start.ConnectionErrorPage.ListViewGrid");
        private IReadOnlyCollection<AppiumWebElement> ListViewItems => OnAndroid ? ListViewGrids : ListViewCells;
        private Func<AppiumWebElement, AppiumWebElement> ItemTitle => (e) => App.FindElementByAutomationId("Horus.Views.Start.ConnectionErrorPage.ItemTitle", root: e);
        private Func<AppiumWebElement, AppiumWebElement> ItemMessage => (e) => App.FindElementByAutomationId("Horus.Views.Start.ConnectionErrorPage.ItemMessage", root: e);

        ListView listView;

        public HearingAidConnectionErrorPage(bool assertOnPage = true) : base(assertOnPage)
        {
            listView = new ListView(() => ListViewElement, () => ListViewItems, ItemTitle, ItemMessage, ItemTitle);
        }

        public HearingAidConnectionErrorPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
            listView = new ListView(() => ListViewElement, () => ListViewItems, ItemTitle, ItemMessage, ItemTitle);
        }

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = () => Header,
            iOS = () => Header
        };

        public void RetryConnection()
        {
            listView.ScrollToTop();
            App.Tap(listView.GetItem(0, IndexType.Relative));
        }

        public void BackToHearingAidSelectionUnsupportedDevicePage()
        {
            listView.ScrollToTop();
            App.Tap(listView.GetItem(0, IndexType.Relative));
        }

        public void BackToHearingAidSelectionPage()
        {
            listView.ScrollToTop();
            if (OniOS)
                App.Tap(listView.GetItem(1, IndexType.Relative));
            else
                App.Tap(listView.GetItem(2, IndexType.Relative));
        }

        /// <summary>
        /// Method overload added to Tap on 'Back to hearing system' by text since by index has issues after scroll in list view.
        /// </summary>
        /// <param name="BackToHearingSystemText">'Back to hearing system' Text is passed as parameter since text will differ for different languages</param>
        public void BackToHearingAidSelectionPage(string BackToHearingSystemText)
        {
            App.Tap(GetAppiumWebElementByText(BackToHearingSystemText));
        }

        public void StartDemoMode()
        {
            listView.ScrollToBottom();
            App.Tap(listView.GetItem(listView.GetNumberOfItemsOnScreen() - 1, IndexType.Relative));
        }

        public void UseOnlyOneHearingSystem()
        {
            listView.ScrollToBottom();
            App.Tap(listView.GetItem(1, IndexType.Relative));
        }

        /// <summary>
        /// Method overload added to Tap on 'Use one hearing system' by text since by index has issues after scroll in list view.
        /// </summary>
        /// <param name="UseOneHearingSystemText">'Use one hearing system' Text is passed as parameter since text will differ for different languages</param>
        public void UseOnlyOneHearingSystem(string UseOneHearingSystemText)
        {
            App.Tap(GetAppiumWebElementByText(UseOneHearingSystemText));
        }

        public void FindHearingAid()
        {
            listView.ScrollToBottom();
            App.Tap(listView.GetItem(2, IndexType.Relative));
        }

        /// <summary>
        /// Method overload added to Tap on 'Find hearing system' by text since by index has issues after scroll in list view.
        /// </summary>
        /// <param name="FindHearingSystemText">'Find hearing system' Text is passed as parameter since text will differ for different languages</param>
        public void FindHearingAid(string FindHearingSystemText)
        {
            App.Tap(GetAppiumWebElementByText(FindHearingSystemText));
        }

        public int GetNumberOfItems()
        {
            return listView.GetNumberOfItemsOnScreen();
        }

        public string GetTitleOfItem(int index, bool IsScrollToBottom = false)
        {
            if (IsScrollToBottom)
                listView.ScrollToBottom();
            else
                listView.ScrollToTop();

            return listView.GetItem(index, IndexType.Relative).FindElementByAccessibilityId("Horus.Views.Start.ConnectionErrorPage.ItemTitle").Text;
        }

        /// <summary>
        /// Method overloaded to check if the element is available by title text and get the title back if available since index varies after scroll.
        /// </summary>
        /// <param name="ItemHeaderText">Title text of the element whose text needs to be verified and title text to be obtained</param>
        /// <returns>Returns the title and throws error if the element by title is not available</returns>
        public string GetTitleOfItem(string ItemHeaderText)
        {
            return GetAppiumWebElementByText(ItemHeaderText).FindElementByAccessibilityId("Horus.Views.Start.ConnectionErrorPage.ItemTitle").Text;
        }

        public string GetMessageOfItem(int index, bool IsScrollToBottom = false)
        {
            if (IsScrollToBottom)
                listView.ScrollToBottom();
            else
                listView.ScrollToTop();

            return listView.GetItem(index, IndexType.Relative).FindElementByAccessibilityId("Horus.Views.Start.ConnectionErrorPage.ItemMessage").Text;
        }

        /// <summary>
        /// Method overloaded to check if the element is available by title text and get the item text back if available since index varies after scroll.
        /// </summary>
        /// <param name="ItemHeaderText">Title text of the element whose text needs to be verified and item text text to be obtained</param>
        /// <returns>Returns the item text and throws error if the element by title is not available</returns>
        public string GetMessageOfItem(string ItemHeaderText)
        {
            return GetAppiumWebElementByText(ItemHeaderText).FindElementByAccessibilityId("Horus.Views.Start.ConnectionErrorPage.ItemMessage").Text;
        }

        public string GetPageTitle()
        {
            return Title.Text;
        }

        public string GetPageMessage()
        {
            return Message.Text;
        }

        /// <summary>
        /// This method has been implemented since the index changes when scroll to bottom is done and wrong index is used to get text and tap. 
        /// This will work for IOS even though we dont have 'Pair my hearing system' option there since get text or tap is not done based on index.
        /// </summary>
        /// <param name="ItemHeaderText">ItemHeaderText is the text based on which the element has to be located</param>
        /// <returns>Returns the title and throws error if the element by title is not available</returns>
        private AppiumWebElement GetAppiumWebElementByText(string ItemHeaderText)
        {
            listView.ScrollToTop();

            IReadOnlyCollection<AppiumWebElement> appiumWebElements = listView.GetAllItemsOnScreen();

            bool IsElementFound = false;

            foreach (AppiumWebElement appiumWebElement in appiumWebElements)
            {
                if (appiumWebElement.FindElementByAccessibilityId("Horus.Views.Start.ConnectionErrorPage.ItemTitle").Text == ItemHeaderText)
                {
                    IsElementFound = true;
                    return appiumWebElement;
                }
            }

            if (!IsElementFound)
            {
                listView.ScrollToBottom();
                appiumWebElements = listView.GetAllItemsOnScreen();
                foreach (AppiumWebElement appiumWebElement in appiumWebElements)
                {
                    if (appiumWebElement.FindElementByAccessibilityId("Horus.Views.Start.ConnectionErrorPage.ItemTitle").Text == ItemHeaderText)
                    {
                        IsElementFound = true;
                        return appiumWebElement;
                    }
                }
            }

            throw new NotImplementedException("Element search perfomed with the text '" + ItemHeaderText + "' was not found.");
        }
    }
}