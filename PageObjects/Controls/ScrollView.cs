using System;
using HorusUITest.Extensions;
using HorusUITest.PageObjects.Controls.Interfaces;
using NUnit.Framework;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Controls
{
    public class ScrollView<T> : BasePageObject, IScrollView<T>, IScrollViewService
    {
        private readonly T page;
        private readonly Func<AppiumWebElement> scrollViewQuery;
        private readonly Func<AppiumWebElement> topOfScrollViewQuery;
        private readonly Func<AppiumWebElement> bottomOfScrollViewQuery;

        private AppiumWebElement scrollViewElement;
        private AppiumWebElement ScrollViewElement
        {
            get
            {
                scrollViewElement = scrollViewElement ?? scrollViewQuery.Invoke();
                return scrollViewElement;
            }
        }

        private readonly Action clearParentCache;

        public ScrollView(T page, Func<AppiumWebElement> scrollViewQuery, Func<AppiumWebElement> topOfScrollViewQuery, Func<AppiumWebElement> bottomOfScrollViewQuery, Action clearParentCache)
        {
            this.page = page;
            this.scrollViewQuery = scrollViewQuery;
            this.topOfScrollViewQuery = topOfScrollViewQuery;
            this.bottomOfScrollViewQuery = bottomOfScrollViewQuery;
            this.clearParentCache = clearParentCache;
        }

        public virtual T ScrollToTop(int maxNumberOfSwipes = int.MaxValue)
        {
            bool success = DoUntilElementExists(() => ScrollUp(1, true), topOfScrollViewQuery, maxNumberOfSwipes);
            Assert.IsTrue(success, "Unable to verify at top of the view after {0} swipes.", maxNumberOfSwipes);
            clearParentCache();
            return page;
        }

        public virtual T ScrollToBottom(int maxNumberOfSwipes = int.MaxValue)
        {
            bool success = DoUntilElementExists(() => ScrollDown(1, true), bottomOfScrollViewQuery, maxNumberOfSwipes);
            Assert.IsTrue(success, "Unable to verify at bottom of the view after {0} swipes.", maxNumberOfSwipes);
            clearParentCache();
            return page;
        }

        private void ConstrainScroll(ref double y)
        {
            if (y < 0) y = 0;
            if (y > 1) y = 1;
        }

        private void ScrollUp(double verticalPercentage, bool useInertia)
        {
            ConstrainScroll(ref verticalPercentage);
            App.SwipeRelativeToElementSize(ScrollViewElement, .5, 0, .5, verticalPercentage, useInertia: useInertia, safetyGap: 10);
        }

        private void ScrollDown(double verticalPercentage, bool useInertia)
        {
            ConstrainScroll(ref verticalPercentage);
            App.SwipeRelativeToElementSize(ScrollViewElement, .5, 1, .5, 1 - verticalPercentage, useInertia: useInertia, safetyGap: 10);
        }

        public virtual T ScrollUp(double verticalPercentage)
        {
            ScrollUp(verticalPercentage, false);
            clearParentCache();
            return page;
        }

        public virtual T ScrollDown(double verticalPercentage)
        {
            ScrollDown(verticalPercentage, false);
            clearParentCache();
            return page;
        }

        /// <summary>
        /// PageMenuButtons are always reported as non-visible. This method attempts to work around that.
        /// </summary>
        private bool TryInvokeQuery_IosMenuButtonCompatible(Func<AppiumWebElement> elementQuery, out AppiumWebElement element)
        {
            if (OniOS)
            {
                element = InvokeQueryDontThrow(elementQuery);
                return element.IsOnScreen();
            }
            else
            {
                return TryInvokeQuery(elementQuery, out element);
            }
        }

        public AppiumWebElement LocateElement(Func<AppiumWebElement> elementQuery)
        {
            AppiumWebElement element;
            if (TryInvokeQuery_IosMenuButtonCompatible(elementQuery, out element)) return element;
            ScrollToTop();
            if (TryInvokeQuery_IosMenuButtonCompatible(elementQuery, out element)) return element;
            do
            {
                ScrollDown(1);
                if (TryInvokeQuery_IosMenuButtonCompatible(elementQuery, out element)) return element;
            } while (!bottomOfScrollViewQuery.Exists());

            return element;
        }

        //Wraps the methods and returns void to allow service clients (e.g. MenuItems) to be non-generic.
        void IScrollViewService.ScrollToTop(int maxNumberOfSwipes)
        {
            ScrollToTop(maxNumberOfSwipes);
        }

        void IScrollViewService.ScrollToBottom(int maxNumberOfSwipes)
        {
            ScrollToBottom(maxNumberOfSwipes);
        }

        void IScrollViewService.ScrollUp(double verticalPercentage)
        {
            ScrollUp(verticalPercentage);
        }

        void IScrollViewService.ScrollDown(double verticalPercentage)
        {
            ScrollDown(verticalPercentage);
        }

        public bool GetIsScrolledToTop()
        {
            return TryInvokeQuery(topOfScrollViewQuery, out _);
        }

        public bool GetIsScrolledToBottom()
        {
            return TryInvokeQuery(bottomOfScrollViewQuery, out _);
        }
    }
}
