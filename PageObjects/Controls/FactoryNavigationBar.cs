using HorusUITest.PageObjects.Controls.Interfaces;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Controls
{
    public class FactoryNavigationBar : BaseFactoryControl, INavigationBar
    {
        private const string TitleAID = "Horus.Views.Controls.NavigationBar.Title";
        private const string BackButtonIconAID = "Horus.Views.Controls.NavigationBar.BackButtonIcon";
        private const string RightButtonIconAID = "Horus.Views.Controls.NavigationBar.RightButtonIcon";

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = TitleAID), FindsByIOSUIAutomation(Accessibility = TitleAID)]
        private IMobileElement<AppiumWebElement> Title { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = BackButtonIconAID), FindsByIOSUIAutomation(Accessibility = BackButtonIconAID)]
        private IMobileElement<AppiumWebElement> BackButtonIcon { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = RightButtonIconAID), FindsByIOSUIAutomation(Accessibility = RightButtonIconAID)]
        private IMobileElement<AppiumWebElement> RightButtonIcon { get; set; }

        public FactoryNavigationBar(IMobileElement<AppiumWebElement> parent) : base(parent) { }

        public void TapBack()
        {
            App.Tap(BackButtonIcon);
        }

        public void SwipeBack()
        {
            App.SwipeLeftToRight();
        }

        public void NavigateBack()
        {
            TapBack();
        }

        public string GetNavigationBarTitle()
        {
            return Title.Text;
        }

        public void TapRightIcon()
        {
            App.Tap(RightButtonIcon);
        }

        public bool GetIsBackButtonDisplayed()
        {
            return BackButtonIcon.Displayed;
        }
    }
}
