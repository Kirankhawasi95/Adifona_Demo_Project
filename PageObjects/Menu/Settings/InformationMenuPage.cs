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
    public class InformationMenuPage : BaseSubMenuPage, IScrollView<InformationMenuPage>
    {
        //TODO: Clean up TechnicalSupport and Tutorials once they are definitely removed from the app.

        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Menu.InformationMenuPage.NavigationBar");

        private AppiumWebElement MainScrollView => App.FindElementByAutomationId("Horus.Views.Menu.HelpMenu.MainScrollView");
        private AppiumWebElement BottomOfScrollView => App.FindElementByAutomationId("Horus.Views.Menu.HelpMenu.Info.EndOfScrollView", verifyVisibility: true);

        private AppiumWebElement GroupHeaderLegalInfo => App.FindElementByAutomationId("Horus.Views.Menu.HelpMenu.Info.GroupHeaderLegalInfo", verifyVisibility: true);
        //private AppiumWebElement MenuItemFindHearingDevices => App.FindElementByAutomationId("Horus.Views.Menu.HelpMenu.FindDevices", verifyVisibility: true);

        //private AppiumWebElement GroupHeaderTutorials => App.FindElementByAutomationId("Horus.Views.Menu.HelpMenu.GroupHeaderTutorials");
        //private AppiumWebElement MenuItemHelpTopics => App.FindElementByAutomationId("Horus.Views.Menu.HelpMenu.HelpTopics", verifyVisibility: true);
        //private AppiumWebElement MenuItemInstructionsForUse => App.FindElementByAutomationId("Horus.Views.Menu.HelpMenu.InstructionManual", verifyVisibility: true);

        //private AppiumWebElement GroupHeaderLegalInfos => App.FindElementByAutomationId("Horus.Views.Menu.HelpMenu.GroupHeaderLegalInfos");
        //private AppiumWebElement MenuItemImprint => App.FindElementByAutomationId("Horus.Views.Menu.HelpMenu.Imprint", verifyVisibility: true);
        private AppiumWebElement MenuItemTermsOfUse => App.FindElementByAutomationId("Horus.Views.Menu.HelpMenu.Info.TermsOfUse", verifyVisibility: true);
        private AppiumWebElement MenuItemPrivacyPolicy => App.FindElementByAutomationId("Horus.Views.Menu.HelpMenu.Info.PrivacyPolicy", verifyVisibility: true);
        private AppiumWebElement MenuItemLicenses => App.FindElementByAutomationId("Horus.Views.Menu.HelpMenu.Info.Licenses", verifyVisibility: true);

        private readonly ScrollView<InformationMenuPage> scrollView;

        public InformationMenuPage(bool assertOnPage = true) : base(assertOnPage)
        {
            scrollView = new ScrollView<InformationMenuPage>(this, () => MainScrollView, () => GroupHeaderLegalInfo, () => BottomOfScrollView, ClearCache);
            menuItems = new MenuItems(scrollView);
        }

        public InformationMenuPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
            scrollView = new ScrollView<InformationMenuPage>(this, () => MainScrollView, () => GroupHeaderLegalInfo, () => BottomOfScrollView, ClearCache);
            menuItems = new MenuItems(scrollView);
        }

        private AppiumWebElement LocateElement(Func<AppiumWebElement> elementQuery)
        {
            return scrollView.LocateElement(elementQuery);
        }

        public InformationMenuPage ScrollToTop(int maxNumberOfSwipes = Env.DEFAULT_MAX_SWIPES_TO_SCROLL)
        {
            return scrollView.ScrollToTop(maxNumberOfSwipes);
        }

        public InformationMenuPage ScrollToBottom(int maxNumberOfSwipes = Env.DEFAULT_MAX_SWIPES_TO_SCROLL)
        {
            return scrollView.ScrollToBottom(maxNumberOfSwipes);
        }

        public InformationMenuPage ScrollUp(double verticalPercentage)
        {
            return scrollView.ScrollUp(verticalPercentage);
        }

        public InformationMenuPage ScrollDown(double verticalPercentage)
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

        public string GetTermsOfUseText()
        {
            return LocateElement(() => PageMenuButton.GetLabelOf(MenuItemTermsOfUse)).Text;
        }

        public string GetPrivacyPolicyText()
        {
            return LocateElement(() => PageMenuButton.GetLabelOf(MenuItemPrivacyPolicy)).Text;
        }

        public string GetLicensesText()
        {
            return LocateElement(() => PageMenuButton.GetLabelOf(MenuItemLicenses)).Text;
        }        

        /// <summary>
        /// Navigates to <see cref="TermsOfUsePage"/>.
        /// </summary>
        public void OpenTermsofUse()
        {
            App.Tap(LocateElement(() => MenuItemTermsOfUse));
        }
        /// <summary>
        /// Navigates to <see cref="PrivacyPolicyPage"/>.
        /// </summary>
        public void OpenPrivacyPolicy()
        {
            App.Tap(LocateElement(() => MenuItemPrivacyPolicy));
        }

        /// <summary>
        /// Navigates to <see cref="LicencesPage"/>.
        /// </summary>
        public void OpenLicenses()
        {
            App.Tap(LocateElement(() => MenuItemLicenses));
        }
    }
}
