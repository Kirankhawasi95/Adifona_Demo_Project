using System;
using OpenQA.Selenium.Appium;
using HorusUITest.Helper;
using HorusUITest.PageObjects.Dialogs;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium.Appium.Interfaces;
using HorusUITest.Enums;

namespace HorusUITest.PageObjects.Start.Intro
{
    public class IntroPageOne : WelcomePage
    {
        public IntroPageOne(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public IntroPageOne(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        private AppiumWebElement Title => App.FindElementByAutomationId("Horus.Views.Start.Intro.IntroPageOne.introPage1_Title");

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = () => Title,
            iOS = () => Title
        };

        public string GetTitle()
        {
            return Title.Text;
        }

        public bool CheckInto1Image(Brand brand)
        {
            string BrandName = Enum.GetName(typeof(Brand), brand);
            AppiumWebElement element = App.FindElementByImage("OEM\\" + BrandName + "\\intro1.png");
            return element.Displayed;
        }
    }
}