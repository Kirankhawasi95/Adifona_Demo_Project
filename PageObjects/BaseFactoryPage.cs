using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects
{
    public abstract class BaseFactoryPage : BasePage
    {
        //INFO: PageFactory will NOT initialize properties marked with 'override'. Therefore, instead of an abstract TraitElement, an abstract TraitQuery is used to be implemented by derived pages.

        protected abstract Func<IMobileElement<AppiumWebElement>> TraitQuery { get; }

        protected override sealed PlatformQuery Trait => new PlatformQuery
        {
            Android = TraitQuery,
            iOS = TraitQuery
        };

        public BaseFactoryPage(bool assertOnPage) : base(false)
        {
            PageFactory.InitElements(App.Driver, this, new AppiumPageObjectMemberDecorator(new TimeOutDuration(TimeSpan.Zero)));
            if (assertOnPage)
                AssertOnPage();
        }

        public BaseFactoryPage(TimeSpan assertOnPageTimeout) : base(false)
        {
            PageFactory.InitElements(App.Driver, this, new AppiumPageObjectMemberDecorator(new TimeOutDuration(TimeSpan.Zero)));
            AssertOnPage(assertOnPageTimeout);
        }
    }
}
