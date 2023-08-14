using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects
{
    public abstract class BaseFactoryControl : BasePageObject
    {
        public BaseFactoryControl(IMobileElement<AppiumWebElement> parent)
        {
            PageFactory.InitElements(parent, this, new AppiumPageObjectMemberDecorator(new TimeOutDuration(TimeSpan.Zero)));
        }
    }
}
