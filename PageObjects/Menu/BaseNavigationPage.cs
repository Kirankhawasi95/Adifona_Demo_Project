using System;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Controls.Interfaces;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu
{
    public abstract class BaseNavigationPage : BasePage, INavigationBar
    {
        protected abstract AppiumWebElement TraitNavBar { get; }

        protected INavigationBar navigationBar = new NavigationBar();
        //private FactoryNavigationBar navigationBar;

        protected BaseNavigationPage(bool assertOnPage) : base(assertOnPage)
        {
        }

        protected BaseNavigationPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override sealed PlatformQuery Trait => new PlatformQuery
        {
            Android = () => TraitNavBar,
            iOS = () => TraitNavBar
        };

        public virtual void TapBack()
        {
            ClearCache();
            navigationBar.TapBack();
        }

        public virtual void SwipeBack()
        {
            ClearCache();
            navigationBar.SwipeBack();
        }

        public void NavigateBack()
        {
            TapBack();
        }

        public string GetNavigationBarTitle()
        {
            return navigationBar.GetNavigationBarTitle(); 
        }

        public bool GetIsBackButtonDisplayed()
        {
            return navigationBar.GetIsBackButtonDisplayed();
        }

        //public void TapRightIcon()
        //{
        //  navigationBar.TapRightIcon();
        //}
    }
}
