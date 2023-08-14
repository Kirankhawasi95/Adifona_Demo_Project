using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using System;
using Platform = HorusUITest.Enums.Platform;

namespace HorusUITest
{
    public class PlatformQuery
    {
        public Func<IMobileElement<AppiumWebElement>> Android
        {
            set
            {
                if (AppManager.Platform == Platform.Android)
                    current = value;
            }
        }

        public Func<IMobileElement<AppiumWebElement>> iOS
        {
            set
            {
                if (AppManager.Platform == Platform.iOS)
                    current = value;
            }
        }

        Func<IMobileElement<AppiumWebElement>> current;
        public Func<IMobileElement<AppiumWebElement>> Current
        {
            get
            {
                if (current == null)
                    throw new NullReferenceException("Trait not set for current platform");

                return current;
            }
        }
    }
}