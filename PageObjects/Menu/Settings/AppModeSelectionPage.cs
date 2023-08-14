using System;
using HorusUITest.Enums;
using HorusUITest.Helper;
using HorusUITest.PageObjects.Dialogs;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu.Settings
{
    public class AppModeSelectionPage : BaseNavigationPage
    {
        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Menu.Settings.AppModeSelectionPage.NavigationBar");
        private AppiumWebElement Title => App.FindElementByAutomationId("Horus.Views.Menu.Settings.AppModeSelectionPage.Title");
        private AppiumWebElement AcceptButton => App.FindElementByAutomationId("Horus.Views.Menu.Settings.AppModeSelectionPage.Accept");

        private AppiumWebElement DemoMode => App.FindElementByAutomationId("Horus.ViewModels.Menu.Settings.DemoMode");
        private AppiumWebElement NormalMode => App.FindElementByAutomationId("Horus.ViewModels.Menu.Settings.NormalMode");

        //These selectors are unreliable on iOS. A workaround using invisible labels that hold the selected / current language has been implemented instead.
        //private AppiumWebElement DemoModeIsSelected => App.FindElementByAutomationId("Horus.ViewModels.Menu.Settings.DemoModeIsSelected", verifyVisibility: true);
        //private AppiumWebElement NormalModeIsSelected => App.FindElementByAutomationId("Horus.ViewModels.Menu.Settings.NormalModeIsSelected", verifyVisibility: true);

        private AppiumWebElement SelectedAppModeName => App.FindElementByAutomationId("Horus.Views.Menu.Settings.AppModeSelectionPage.SelectedItemName");

        public AppModeSelectionPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public AppModeSelectionPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        public string GetTitle()
        {
            return Title.Text;
        }

        private AppMode GetAppMode(string appModeName)
        {
            switch (appModeName)
            {
                case "DemoMode": return AppMode.Demo;
                case "NormalMode": return AppMode.Normal;
                default: throw new NotImplementedException($"Unknown AppMode name '{appModeName}'.");
            }
        }

        private AppiumWebElement GetAppModeElement(AppMode appMode)
        {
            switch (appMode)
            {
                case AppMode.Demo: return DemoMode;
                case AppMode.Normal: return NormalMode;
                default: throw new NotImplementedException($"Unknown AppMode '{appMode}'.");
            }
        }

        private string GetSelectedAppModeName()
        {
            string result = null;
            bool condition()
            {
                result = SelectedAppModeName.Text;
                return result != "null";
            }
            Wait.UntilTrue(condition, TimeSpan.FromSeconds(2));
            return result;
        }

        public string GetAppModeText(AppMode appMode)
        {
            return GetAppModeElement(appMode).Text;
        }

        public AppMode GetSelectedAppMode()
        {
            return GetAppMode(GetSelectedAppModeName());
        }

        /// <summary>
        /// Taps the 'Accept' button, potentially causing an <see cref="AppDialog"/> to be displayed.
        /// </summary>
        public void Accept()
        {
            App.Tap(AcceptButton);
        }

        /// <summary>
        /// Selects an <see cref="AppMode"/>.
        /// </summary>
        /// <param name="appMode"></param>
        /// <returns></returns>
        public AppModeSelectionPage SelectAppMode(AppMode appMode)
        {
            App.Tap(GetAppModeElement(appMode));
            return this;
        }

        /// <summary>
        /// Changes the <see cref="AppMode"/> by selecting, tapping 'Accept' and and confirming the <see cref="AppDialog"/> if necessary.
        /// In this method the mode will be selected and Accept will be tapped and confirmation will also be displayed. After calling this method, when the Accept method is called again, it will redirect to Welcome screen
        /// </summary>
        /// <param name="appMode"></param>
        public void ChangeAppMode(AppMode appMode)
        {
            SelectAppMode(appMode);
            Accept();
            DialogHelper.ConfirmIfDisplayed();
        }

        /// <summary>
        /// In this method only the mode will be changed and neither the accept nor the confirmation will be displayed
        /// </summary>
        /// <param name="appMode"></param>
        public void ChangeAppModeOnly(AppMode appMode)
        {
            SelectAppMode(appMode);
        }
    }
}
