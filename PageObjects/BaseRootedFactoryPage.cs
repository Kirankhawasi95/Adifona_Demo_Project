using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects
{
    public abstract class BaseRootedFactoryPage : BasePage
    {
        protected abstract Func<IMobileElement<AppiumWebElement>> RootQuery { get; }

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = RootQuery,
            iOS = RootQuery
        };

        public BaseRootedFactoryPage(bool assertOnPage) : base(false)
        {
            IMobileElement<AppiumWebElement> pageRoot;

            if (assertOnPage)
                pageRoot = AssertOnPage();
            else
                pageRoot = App.WaitForElement(RootQuery);

            PageFactory.InitElements(pageRoot, this, new AppiumPageObjectMemberDecorator(new TimeOutDuration(TimeSpan.Zero)));
        }

        public BaseRootedFactoryPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
            IMobileElement<AppiumWebElement> pageRoot;
            pageRoot = AssertOnPage(assertOnPageTimeout);
            PageFactory.InitElements(pageRoot, this, new AppiumPageObjectMemberDecorator(new TimeOutDuration(TimeSpan.Zero)));
        }
    }
}
