using System;
using HorusUITest.PageObjects.Controls.Menu;
using HorusUITest.PageObjects.Menu.Help.HelpTopics;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu.Help
{
    public class HelpTopicsPage : BaseSubMenuPage
    {
        public HelpTopicsPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public HelpTopicsPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Menu.Help.HelpTopicsPage.NavigationBar");

        private AppiumWebElement MenuItemConnectHearingAids => App.FindElementByAutomationId("Horus.Views.Menu.Help.HelpTopicsPage.ConnectHearingAids");
        private AppiumWebElement MenuItemDisconnectHearingAids => App.FindElementByAutomationId("Horus.Views.Menu.Help.HelpTopicsPage.DisconnectHearingAids");
        private AppiumWebElement MenuItemMainScreen => App.FindElementByAutomationId("Horus.Views.Menu.Help.HelpTopicsPage.MainScreen");
        private AppiumWebElement MenuItemProgramSelection => App.FindElementByAutomationId("Horus.Views.Menu.Help.HelpTopicsPage.ProgramSelection");
        private AppiumWebElement MenuItemBinauralSeparation => App.FindElementByAutomationId("Horus.Views.Menu.Help.HelpTopicsPage.BinauralSeparation");
        private AppiumWebElement MenuItemAutomaticProgram => App.FindElementByAutomationId("Horus.Views.Menu.Help.HelpTopicsPage.AutomaticProgram");
        private AppiumWebElement MenuItemStreamingProgram => App.FindElementByAutomationId("Horus.Views.Menu.Help.HelpTopicsPage.StreamingProgram");
        private AppiumWebElement MenuItemFavorites => App.FindElementByAutomationId("Horus.Views.Menu.Help.HelpTopicsPage.Favorites");
        private AppiumWebElement MenuItemMainMenu => App.FindElementByAutomationId("Horus.Views.Menu.Help.HelpTopicsPage.MainMenu");

        public string GetConnectHearingAidsText()
        {
            return PageMenuButton.GetLabelOf(MenuItemConnectHearingAids).Text;
        }

        public string GetDisconnectHearingAidsText()
        {
            return PageMenuButton.GetLabelOf(MenuItemDisconnectHearingAids).Text;
        }

        public string GetMainScreenText()
        {
            return PageMenuButton.GetLabelOf(MenuItemMainScreen).Text;
        }

        public string GetProgramSelectionText()
        {
            return PageMenuButton.GetLabelOf(MenuItemProgramSelection).Text;
        }

        public string GetBinauralSeparationText()
        {
            return PageMenuButton.GetLabelOf(MenuItemBinauralSeparation).Text;
        }

        public string GetAutomaticProgramText()
        {
            return PageMenuButton.GetLabelOf(MenuItemAutomaticProgram).Text;
        }

        public string GetStreamingProgramText()
        {
            return PageMenuButton.GetLabelOf(MenuItemStreamingProgram).Text;
        }

        public string GetFavoritesText()
        {
            return PageMenuButton.GetLabelOf(MenuItemFavorites).Text;
        }

        public string GetMainMenuText()
        {
            return PageMenuButton.GetLabelOf(MenuItemMainMenu).Text;
        }

        /// <summary>
        /// Navigates to <see cref="ConnectHearingAidsHelpPage"/>.
        /// </summary>
        public void OpenConnectHearingAids()
        {
            App.Tap(MenuItemConnectHearingAids);
        }

        /// <summary>
        /// Navigates to <see cref="DisconnectHearingAidsHelpPage"/>.
        /// </summary>
        public void OpenDisconnectHearingAids()
        {
            App.Tap(MenuItemDisconnectHearingAids);
        }

        /// <summary>
        /// Navigates to <see cref="MainPageHelpPage"/>.
        /// </summary>
        public void OpenHomePage()
        {
            App.Tap(MenuItemMainScreen);
        }

        /// <summary>
        /// Navigates to <see cref="ProgramSelectionHelpPage"/>.
        /// </summary>
        public void OpenProgramSelection()
        {
            App.Tap(MenuItemProgramSelection);
        }

        /// <summary>
        /// Navigates to <see cref="DisconnectVolumeControlHelpPage"/>.
        /// </summary>
        public void OpenBinauralSeparation()
        {
            App.Tap(MenuItemBinauralSeparation);
        }

        /// <summary>
        /// Navigates to <see cref="AutomaticProgramHelpPage"/>.
        /// </summary>
        public void OpenAutomaticProgram()
        {
            App.Tap(MenuItemAutomaticProgram);
        }

        /// <summary>
        /// Navigates to <see cref="StreamingProgramHelpPage"/>.
        /// </summary>
        public void OpenStreamingProgram()
        {
            App.Tap(MenuItemStreamingProgram);
        }

        /// <summary>
        /// Navigates to <see cref="FavoritesHelpPage"/>.
        /// </summary>
        public void OpenFavorites()
        {
            App.Tap(MenuItemFavorites);
        }

        /// <summary>
        /// Navigates to <see cref="MainMenuHelpPage"/>.
        /// </summary>
        public void OpenMainMenu()
        {
            App.Tap(MenuItemMainMenu);
        }
    }
}
