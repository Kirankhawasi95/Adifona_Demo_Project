using System;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Start
{
    public abstract class WelcomePage : BasePage
    {
        protected WelcomePage(bool assertOnPage) : base(assertOnPage)
        {
        }

        protected WelcomePage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        private AppiumWebElement LeftButton => App.FindElementByAutomationId("Horus.Views.Start.WelcomePage.LeftButton");
        private AppiumWebElement RightButton => App.FindElementByAutomationId("Horus.Views.Start.WelcomePage.RightButton");
        private AppiumWebElement LoadingIndicator => App.FindElementByAutomationId("Horus.Views.Start.WelcomePage.CircularActivityIndicator");

        public void MoveRightBySwiping()
        {
            App.SwipeRightToLeft();
        }

        public void MoveLeftBySwiping()
        {
            App.SwipeLeftToRight();
        }

        public bool GetIsRightButtonVisible()
        {
            return TryInvokeQuery(() => RightButton, out _);
        }

        public bool GetIsLeftButtonVisible()
        {
            return TryInvokeQuery(() => LeftButton, out _);
        }

        public void MoveRightByTapping()
        {
            App.Tap(RightButton);
        }

        public void MoveLeftByTapping()
        {
            App.Tap(LeftButton);
        }
    }
}
