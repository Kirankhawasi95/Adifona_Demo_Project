using System;
using HorusUITest.Extensions;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Controls.Menu
{
    public class PageMenuButton : BasePageObject
    {
        private Func<AppiumWebElement, AppiumWebElement> TextLabel => (e) => App.FindElementByAutomationId("Horus.Views.Controls.Menu.PageMenuButton.MenuItemText", root: e, verifyVisibility: false);

        private PageMenuButton() { }
        private static PageMenuButton instance;
        private static PageMenuButton GetInstance()
        {
            instance = instance ?? new PageMenuButton();
            return instance;
        }

        private AppiumWebElement GetLabel(AppiumWebElement root) { return root.FindElement(TextLabel); }

        public static AppiumWebElement GetLabelOf(AppiumWebElement root) { return GetInstance().GetLabel(root); }
    }
}
