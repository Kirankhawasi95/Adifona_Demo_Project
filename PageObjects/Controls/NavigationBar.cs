using HorusUITest.PageObjects.Controls.Interfaces;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Controls
{
    public class NavigationBar : BasePageObject, INavigationBar
    {
        protected AppiumWebElement Title => App.FindElementByAutomationId("Horus.Views.Controls.NavigationBar.Title");
        protected AppiumWebElement BackButtonIcon => App.FindElementByAutomationId("Horus.Views.Controls.NavigationBar.BackButtonIcon");
        //Right icon is deliberately not a part of the INavigationBar interface, because only few view pages support it.
        protected AppiumWebElement RightButtonIcon => App.FindElementByAutomationId("Horus.Views.Controls.NavigationBar.RightButtonIcon");

        public void TapBack()
        {
            App.Tap(BackButtonIcon);
        }

        public void SwipeBack()
        {
            App.SwipeLeftToRight();
        }

        public string GetNavigationBarTitle()
        {
            return Title.Text;
        }

        public void TapRightIcon()
        {
            App.Tap(RightButtonIcon);
        }

        public void NavigateBack()
        {
            TapBack();
        }

        public bool GetIsBackButtonDisplayed()
        {
            return BackButtonIcon.Displayed;
        }
    }
}