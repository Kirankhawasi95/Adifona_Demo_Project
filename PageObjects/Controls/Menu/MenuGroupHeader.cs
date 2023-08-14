using System;
using System.Collections.Generic;
using HorusUITest.Extensions;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Controls.Menu
{
    public class MenuGroupHeader : BasePageObject
    {
        private Func<AppiumWebElement, AppiumWebElement> Header => (e) => App.FindElementByAutomationId("Horus.Views.Controls.Menu.MenuGroupHeader.advancedLabelHeader", e);
        private Func<AppiumWebElement, IReadOnlyCollection<AppiumWebElement>> Headers => (e) => App.FindElementsByAutomationId("Horus.Views.Controls.Menu.MenuGroupHeader.advancedLabelHeader", e);

        private MenuGroupHeader() { }
        private static MenuGroupHeader instance;
        private static MenuGroupHeader GetInstance()
        {
            instance = instance ?? new MenuGroupHeader();
            return instance;
        }

        private AppiumWebElement HeaderOf(AppiumWebElement root) { return root.FindElement(Header); }
        private IReadOnlyCollection<AppiumWebElement> AllHeadersOf(AppiumWebElement root) { return root.FindElements(Headers); }

        public static AppiumWebElement GetHeaderOf(AppiumWebElement root) { return GetInstance().HeaderOf(root); }
        public static IReadOnlyCollection<AppiumWebElement> GetAllHeadersOf(AppiumWebElement root) { return GetInstance().AllHeadersOf(root); }
    }
}
