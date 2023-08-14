using System;
using HorusUITest.Enums;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Start.Intro
{
    public class IntroPageFour : WelcomePage
    {
        public IntroPageFour(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public IntroPageFour(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        private AppiumWebElement Title => App.FindElementByAutomationId("Horus.Views.Start.Intro.IntroPageFour.introPage4_Title");

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = () => Title,
            iOS = () => Title
        };

        public bool CheckInto4Image(Brand brand)
        {
            string BrandName = Enum.GetName(typeof(Brand), brand);
            AppiumWebElement element = App.FindElementByImage("OEM\\" + BrandName + "\\intro4.png");
            return element.Displayed;
        }
    }
}
