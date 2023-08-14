using System;
using HorusUITest.Enums;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Start.Intro
{
    public class IntroPageTwo : WelcomePage
    {
        private AppiumWebElement Title => App.FindElementByAutomationId("Horus.Views.Start.Intro.IntroPageTwo.introPage2_Title");

        public IntroPageTwo(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public IntroPageTwo(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = () => Title,
            iOS = () => Title
        };

        public bool CheckInto2Image(Brand brand)
        {
            string BrandName = Enum.GetName(typeof(Brand), brand);
            AppiumWebElement element = App.FindElementByImage("OEM\\" + BrandName + "\\intro2.png");
            return element.Displayed;
        }
    }
}
