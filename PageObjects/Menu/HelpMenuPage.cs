using System;
using HorusUITest.Configuration;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Controls.Interfaces;
using HorusUITest.PageObjects.Controls.Menu;
using HorusUITest.PageObjects.Menu.Help;
using HorusUITest.PageObjects.Menu.Info;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu
{
    public class HelpMenuPage : BaseSubMenuPage, IScrollView<HelpMenuPage>
    {
        //TODO: Clean up TechnicalSupport and Tutorials once they are definitely removed from the app.

        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Menu.HelpMenu.NavigationBar");

        private AppiumWebElement MainScrollView => App.FindElementByAutomationId("Horus.Views.Menu.HelpMenu.MainScrollView");
        private AppiumWebElement BottomOfScrollView => App.FindElementByAutomationId("Horus.Views.Menu.HelpMenu.EndOfScrollView", verifyVisibility: true);

        private AppiumWebElement GroupHeaderDevices => App.FindElementByAutomationId("Horus.Views.Menu.HelpMenu.GroupHeaderDevices", verifyVisibility: true);
        private AppiumWebElement MenuItemFindHearingDevices => App.FindElementByAutomationId("Horus.Views.Menu.HelpMenu.FindDevices", verifyVisibility: true);

        private AppiumWebElement GroupHeaderTutorials => App.FindElementByAutomationId("Horus.Views.Menu.HelpMenu.GroupHeaderTutorials");
        private AppiumWebElement MenuItemHelpTopics => App.FindElementByAutomationId("Horus.Views.Menu.HelpMenu.HelpTopics", verifyVisibility: true);
        private AppiumWebElement MenuItemInstructionsForUse => App.FindElementByAutomationId("Horus.Views.Menu.HelpMenu.InstructionManual", verifyVisibility: true);

        private AppiumWebElement GroupHeaderLegalInfos => App.FindElementByAutomationId("Horus.Views.Menu.HelpMenu.GroupHeaderLegalInfos");
        private AppiumWebElement MenuItemImprint => App.FindElementByAutomationId("Horus.Views.Menu.HelpMenu.Imprint", verifyVisibility: true);
        private AppiumWebElement InformationMenu => App.FindElementByAutomationId("Horus.Views.Menu.HelpMenu.InformationMenu", verifyVisibility: true);

        private readonly ScrollView<HelpMenuPage> scrollView;

        public HelpMenuPage(bool assertOnPage = true) : base(assertOnPage)
        {
            scrollView = new ScrollView<HelpMenuPage>(this, () => MainScrollView, () => GroupHeaderDevices, () => BottomOfScrollView, ClearCache);
            menuItems = new MenuItems(scrollView);
        }

        public HelpMenuPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
            scrollView = new ScrollView<HelpMenuPage>(this, () => MainScrollView, () => GroupHeaderDevices, () => BottomOfScrollView, ClearCache);
            menuItems = new MenuItems(scrollView);
        }

        private AppiumWebElement LocateElement(Func<AppiumWebElement> elementQuery)
        {
            return scrollView.LocateElement(elementQuery);
        }

        public HelpMenuPage ScrollToTop(int maxNumberOfSwipes = Env.DEFAULT_MAX_SWIPES_TO_SCROLL)
        {
            return scrollView.ScrollToTop(maxNumberOfSwipes);
        }

        public HelpMenuPage ScrollToBottom(int maxNumberOfSwipes = Env.DEFAULT_MAX_SWIPES_TO_SCROLL)
        {
            return scrollView.ScrollToBottom(maxNumberOfSwipes);
        }

        public HelpMenuPage ScrollUp(double verticalPercentage)
        {
            return scrollView.ScrollUp(verticalPercentage);
        }

        public HelpMenuPage ScrollDown(double verticalPercentage)
        {
            return scrollView.ScrollDown(verticalPercentage);
        }

        public bool GetIsScrolledToTop()
        {
            return scrollView.GetIsScrolledToTop();
        }

        public bool GetIsScrolledToBottom()
        {
            return scrollView.GetIsScrolledToBottom();
        }

        public string GetFindHearingDevicesText()
        {
            return LocateElement(() => PageMenuButton.GetLabelOf(MenuItemFindHearingDevices)).Text;
        }

        public string GetHelpTopicsText()
        {
            return LocateElement(() => PageMenuButton.GetLabelOf(MenuItemHelpTopics)).Text;
        }

        public string GetInstructionsForUseText()
        {
            return LocateElement(() => PageMenuButton.GetLabelOf(MenuItemInstructionsForUse)).Text;
        }

        public string GetImprintText()
        {
            return LocateElement(() => PageMenuButton.GetLabelOf(MenuItemImprint)).Text;
        }

        public string GetInformationMenuText()
        {
            return LocateElement(() => PageMenuButton.GetLabelOf(InformationMenu)).Text;
        }

        

        /// <summary>
        /// Navigates to <see cref="FindDevicesPage"/>, potentially requesting a location permission before.
        /// </summary>
        public void OpenFindHearingDevices()
        {
            App.Tap(LocateElement(() => MenuItemFindHearingDevices));
        }

        /// <summary>
        /// Navigates to <see cref="HelpTopicsPage"/>.
        /// </summary>
        public void OpenHelpTopics()
        {
            App.Tap(LocateElement(() => MenuItemHelpTopics));
        }

        /// <summary>
        /// Navigates to <see cref="InstructionsForUsePage"/>.
        /// </summary>
        public void OpenInstructionsForUse()
        {
            App.Tap(LocateElement(() => MenuItemInstructionsForUse));
        }

        /// <summary>
        /// Navigates to <see cref="ImprintPage"/>.
        /// </summary>
        public void OpenImprint()
        {
            App.Tap(LocateElement(() => MenuItemImprint));
        }

        /// <summary>
        /// Navigates to <see cref="InformationMenuPage"/>.
        /// </summary>
        public void OpenInformationMenu()
        {
            App.Tap(LocateElement(() => InformationMenu));
        }
    }
}
