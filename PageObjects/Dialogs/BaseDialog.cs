using System;
using System.Collections.Generic;
using System.Linq;
using HorusUITest.Configuration;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Dialogs
{
    public abstract class BaseDialog : BasePage
    {
        protected abstract AppiumWebElement AndroidTitle { get; }
        private IReadOnlyCollection<AppiumWebElement> AndroidButtons => App.FindElementsByClassName("android.widget.Button");
        private IReadOnlyCollection<AppiumWebElement> AndroidTexts => App.FindElementsByClassName("android.widget.TextView");

        protected AppiumWebElement IosAlert => App.FindElementByClassName("XCUIElementTypeAlert");
        private IReadOnlyCollection<AppiumWebElement> IosTexts => App.FindElementsByClassName("XCUIElementTypeStaticText", IosAlert);
        private IReadOnlyCollection<AppiumWebElement> IosButtons => App.FindElementsByClassName("XCUIElementTypeButton", IosAlert);

        public BaseDialog(bool assertOnPage) : base(assertOnPage)
        {
        }

        protected BaseDialog(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = () => AndroidTitle,
            iOS = () => IosAlert
        };

        protected override void ClearCache()
        {
            base.ClearCache();
            buttonsCache = null;
            textsCache = null;
        }

        private IReadOnlyCollection<AppiumWebElement> Buttons
        {
            get
            {
                if (OnAndroid)
                    return AndroidButtons;
                if (OniOS)
                    return IosButtons;
                throw new PlatformNotSupportedException("Invalid platform.");
            }
        }

        private IReadOnlyCollection<AppiumWebElement> Texts
        {
            get
            {
                if (OnAndroid)
                    return AndroidTexts;
                if (OniOS)
                    return IosTexts;
                throw new PlatformNotSupportedException("Invalid platform.");
            }
        }

        private IReadOnlyCollection<AppiumWebElement> buttonsCache;
        protected IReadOnlyCollection<AppiumWebElement> ButtonsCache
        {
            get
            {
                buttonsCache = buttonsCache ?? Buttons;
                return buttonsCache;
            }
        }

        private IReadOnlyCollection<AppiumWebElement> textsCache;
        protected IReadOnlyCollection<AppiumWebElement> TextsCache
        {
            get
            {
                textsCache = textsCache ?? Texts;
                return textsCache;
            }
        }

        public int GetNumberOfTexts()
        {
            return TextsCache.Count;
        }

        public string GetText(int index)
        {
            return TextsCache.ElementAt(index).Text;
        }

        public int GetNumberOfButtons()
        {
            return ButtonsCache.Count;
        }

        public string GetButtonText(int index)
        {
            return ButtonsCache.ElementAt(index).Text;
        }

        public void TapButton(int index)
        {
            App.Tap(ButtonsCache.ElementAt(index));
            this.IsGoneBeforeTimeout(Env.DIALOG_GONE_MAXDURATION);
            ClearCache();
        }
    }
}
