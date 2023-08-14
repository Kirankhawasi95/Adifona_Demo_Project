using System;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Controls.Interfaces;
using HorusUITest.PageObjects.Controls.Menu;
using HorusUITest.PageObjects.Interfaces;
using HorusUITest.PageObjects.Menu.Programs;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;

namespace HorusUITest.PageObjects.Menu
{
    public class MainMenuPage : BasePage, IHasMenuItems
    {
        private AppiumWebElement CloseMenuButton => App.FindElementByAutomationId("Horus.Views.Menu.MainMenu.CloseMenuButton");
        private AppiumWebElement MenuItemPrograms => App.FindElementByAutomationId("Horus.Views.Controls.Menu.MainMenuContent.mainMenu_BtnPrograms");
        private AppiumWebElement MenuItemSettings => App.FindElementByAutomationId("Horus.Views.Controls.Menu.MainMenuContent.mainMenu_BtnOptions");
        private AppiumWebElement MenuItemHelp => App.FindElementByAutomationId("Horus.Views.Controls.Menu.MainMenuContent.mainMenu_BtnHelp");

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = () => CloseMenuButton,
            iOS = () => CloseMenuButton
        };

        protected MenuItems menuItems = new MenuItems();

        public MainMenuPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public MainMenuPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        public IMenuItems MenuItems => menuItems;

        protected override void ClearCache()
        {
            base.ClearCache();
            menuItems.InvalidateCache();
        }

        public void CloseMenuUsingTap()
        {
            App.Tap(CloseMenuButton);
        }

        public void CloseMenuUsingSwipe()
        {
            App.SwipeRightToLeft();
        }

        public string GetProgramsText()
        {
            return PageMenuButton.GetLabelOf(MenuItemPrograms).Text;
        }

        public string GetSettingsText()
        {
            return PageMenuButton.GetLabelOf(MenuItemSettings).Text;
        }

        public string GetHelpText()
        {
            return PageMenuButton.GetLabelOf(MenuItemHelp).Text;
        }

        /// <summary>
        /// Navigates to <see cref="ProgramsMenuPage"/>.
        /// </summary>
        public void OpenPrograms()
        {
            App.Tap(MenuItemPrograms);
        }

        /// <summary>
        /// Navigates to <see cref="SettingsMenuPage"/>.
        /// </summary>
        public void OpenSettings()
        {
            App.Tap(MenuItemSettings);
        }

        /// <summary>
        /// Navigates to <see cref="HelpMenuPage"/>.
        /// </summary>
        public void OpenHelp()
        {
            App.Tap(MenuItemHelp);
        }

        public void TapSettingsAndHelpSimultanously()
        {
            IMultiAction multiAction = new MultiAction(App.Driver);
            ITouchAction tapSettingsMenu = new TouchAction(App.Driver).Tap(MenuItemSettings);
            ITouchAction tapHelpMenu = new TouchAction(App.Driver).Tap(MenuItemHelp);
            multiAction.Add(tapHelpMenu).Add(tapSettingsMenu).Perform();
            
            
        }

    }
}
