using System;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Start.Intro
{
    public class IntroPageFive : WelcomePage
    {
        public IntroPageFive(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public IntroPageFive(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        private AppiumWebElement ContinueButton => App.FindElementByAutomationId("Horus.Views.Start.Intro.IntroPageFive.ContinueButton");

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = () => ContinueButton,
            iOS = () => ContinueButton
        };

        /// <summary>
        /// Navigates to <see cref="InitializeHardwarePage"/>, potentially requesting a permission on iOS.
        /// </summary>
        public void Continue()
        {
            App.Tap(ContinueButton);
        }
    }
}
