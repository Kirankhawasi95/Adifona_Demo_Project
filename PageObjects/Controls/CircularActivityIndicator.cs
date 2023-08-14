using System;
using HorusUITest.Configuration;
using HorusUITest.PageObjects.Interfaces;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Controls
{
    public class CircularActivityIndicator<T> : BasePageObject, IHasLoadingIndicator<T>
    {
        private AppiumWebElement LoadingIndicator => App.FindElementByAutomationId("Horus.Views.Controls.CircularActivityIndicator.ctrlImage", verifyVisibility: true);

        private T page;

        public CircularActivityIndicator(T page)
        {
            this.page = page;
        }

        public bool GetIsLoadingIndicatorVisible()
        {
            return TryInvokeQuery(() => LoadingIndicator, out _);
        }

        public T WaitUntilNoLoadingIndicator(TimeSpan? timeout = null)
        {
            timeout = timeout ?? Env.DEFAULT_LOADING_INDICATOR_TIMEOUT;
            App.WaitForNoElement(() => LoadingIndicator, timeout);
            return page;
        }
    }
}
