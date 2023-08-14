using System;
using HorusUITest.Configuration;
using HorusUITest.Helper;
using HorusUITest.PageObjects.Interfaces;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Controls.ProgramDetailParams
{
    public abstract class BaseParamEdit<T> : BaseRootedFactoryPage, IHasLoadingIndicator<T>, IHasBackNavigation where T : BaseParamEdit<T>
    {
        protected override sealed Func<IMobileElement<AppiumWebElement>> RootQuery => () => App.FindElementByAutomationId(ContainerAID, verifyVisibility: true);
        protected abstract string ContainerAID { get; }

        private const string ContentAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamsContainer.Content";
        private const string LoadingIndicatorAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamsContainer.CircularActivityIndicator";
        private const string CloseButtonAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamsContainer.CloseButton";
        private const string ActualCloseBlindButtonAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamsContainer.ActualCloseBlindButton";

        [FindsByAndroidUIAutomator(Accessibility = LoadingIndicatorAID), FindsByIOSUIAutomation(Accessibility = LoadingIndicatorAID)]
        private IMobileElement<AppiumWebElement> LoadingIndicator { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = CloseButtonAID), FindsByIOSUIAutomation(Accessibility = CloseButtonAID)]
        private IMobileElement<AppiumWebElement> CloseButton { get; set; }

        //This PageFactory property tends to cause false negatives when trying to find it after it has been disabled / re-enabled.
        //[FindsByAndroidUIAutomator(Accessibility = ActualCloseBlindButtonAID), FindsByIOSUIAutomation(Accessibility = ActualCloseBlindButtonAID)]
        //private IMobileElement<AppiumWebElement> ActualCloseBlindButton { get; set; }
        //For now, a simple FindElementByAutomationId is used instead (potentially slower, but works reliably).
        private AppiumWebElement ActualCloseBlindButton => App.FindElementByAutomationId(ActualCloseBlindButtonAID, verifyVisibility: true);

        public BaseParamEdit(bool assertOnPage) : base(assertOnPage)
        {
        }

        public BaseParamEdit(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        public bool GetIsLoadingIndicatorVisible()
        {
            return TryInvokeQuery(() => LoadingIndicator, out _);
        }

        public T WaitUntilNoLoadingIndicator(TimeSpan? timeout = null)
        {
            timeout = timeout ?? Env.DEFAULT_LOADING_INDICATOR_TIMEOUT;
            App.WaitForNoElement(() => LoadingIndicator, timeout);
            return (T)this;
        }

        public T WaitUntilCloseButtonEnabled(TimeSpan? timeout = null)
        {
            timeout = timeout ?? Env.DEFAULT_EXPLICIT_TIMEOUT;
            Wait.UntilTrue(GetIsCloseButtonEnabled, timeout.Value);
            return (T)this;
        }

        public bool GetIsCloseButtonEnabled()
        {
            return ElementExists(() => ActualCloseBlindButton);
        }

        public string GetCloseButtonText()
        {
            return CloseButton.Text;
        }

        /// <summary>
        /// Closes the window and navigates back to <see cref="ProgramDetailPage"/>.
        /// </summary>
        public void Close(bool buttonMustBeEnabled = true)
        {
            if (buttonMustBeEnabled)
                WaitUntilCloseButtonEnabled();
            App.Tap(CloseButton);
            ClearCache();
        }

        public void NavigateBack()
        {
            Close();
        }
        public ITouchAction TapCloseButton()
        {
            ITouchAction touchAction = new TouchAction(App.Driver);
            return touchAction.Tap(ActualCloseBlindButton);
        }
        public ITouchAction LongPressCloseButton()
        {
            ITouchAction touchAction = new TouchAction(App.Driver);
            return touchAction.LongPress(ActualCloseBlindButton);
        }
    }
}
