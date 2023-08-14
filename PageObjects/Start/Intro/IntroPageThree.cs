using System;
using HorusUITest.Enums;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Start.Intro
{
    public class IntroPageThree : WelcomePage
    {
        public IntroPageThree(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public IntroPageThree(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        private AppiumWebElement Title => App.FindElementByAutomationId("Horus.Views.Start.Intro.IntroPageThree.introPage3_Title");

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = () => Title,
            iOS = () => Title
        };

        public bool CheckInto3Image(Brand brand)
        {
            string BrandName = Enum.GetName(typeof(Brand), brand);
            AppiumWebElement element = App.FindElementByImage("OEM\\" + BrandName + "\\intro3.png");
            return element.Displayed;
        }
    }
}
