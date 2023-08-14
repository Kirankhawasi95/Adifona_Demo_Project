using System;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Controls.Interfaces;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu
{
    public abstract class BaseScrollViewNavigationPage<T> : BaseNavigationPage, IScrollView<T> where T : BaseScrollViewNavigationPage<T>
    {
        protected abstract AppiumWebElement MainScrollView { get; }
        protected abstract AppiumWebElement TopOfScrollView { get; }
        protected abstract AppiumWebElement BottomOfScrollView { get; }

        private ScrollView<BaseScrollViewNavigationPage<T>> scrollView;
        protected virtual ScrollView<BaseScrollViewNavigationPage<T>> ScrollView => scrollView;

        protected AppiumWebElement LocateElement(Func<AppiumWebElement> elementQuery)
        {
            return ScrollView.LocateElement(elementQuery);
        }

        public BaseScrollViewNavigationPage(bool assertOnPage) : base(assertOnPage)
        {
            scrollView = new ScrollView<BaseScrollViewNavigationPage<T>>(this, () => MainScrollView, () => TopOfScrollView, () => BottomOfScrollView, ClearCache);
        }

        public BaseScrollViewNavigationPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
            scrollView = new ScrollView<BaseScrollViewNavigationPage<T>>(this, () => MainScrollView, () => TopOfScrollView, () => BottomOfScrollView, ClearCache);
        }

        public bool GetIsScrolledToBottom()
        {
            return ScrollView.GetIsScrolledToBottom();
        }

        public bool GetIsScrolledToTop()
        {
            return ScrollView.GetIsScrolledToTop();
        }

        public T ScrollDown(double verticalPercentage)
        {
            return (T)ScrollView.ScrollDown(verticalPercentage);
        }

        public T ScrollToBottom(int maxNumberOfSwipes = 10)
        {
            return (T)ScrollView.ScrollToBottom(maxNumberOfSwipes);
        }

        public T ScrollToTop(int maxNumberOfSwipes = 10)
        {
            return (T)ScrollView.ScrollToTop(maxNumberOfSwipes);
        }

        public T ScrollUp(double verticalPercentage)
        {
            return (T)ScrollView.ScrollUp(verticalPercentage);
        }
    }
}
