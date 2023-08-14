using System;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Controls.Interfaces;
using HorusUITest.PageObjects.Dialogs;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;

namespace HorusUITest.PageObjects.Favorites
{
    public abstract class BaseProgramConfigPage : BaseFactoryPage, INavigationBar
    {
        protected override sealed Func<IMobileElement<AppiumWebElement>> TraitQuery => NavigationBarQuery;
        protected abstract Func<IMobileElement<AppiumWebElement>> NavigationBarQuery { get; }
        protected abstract Func<IMobileElement<AppiumWebElement>> CancelButtonQuery { get; }

        private IMobileElement<AppiumWebElement> CancelButton => CancelButtonQuery.Invoke();

        private FactoryNavigationBar navigationBar;

        public BaseProgramConfigPage(bool assertOnPage) : base(assertOnPage)
        {
            navigationBar = new FactoryNavigationBar(NavigationBarQuery.Invoke());
        }

        public BaseProgramConfigPage (TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
            navigationBar = new FactoryNavigationBar(NavigationBarQuery.Invoke());
        }

        public void TapBack()
        {
            navigationBar.TapBack();
        }

        public void SwipeBack()
        {
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

        public void TapRightNavigationBarButton()
        {
            navigationBar.TapRightIcon();
        }

        public bool GetIsCancelButtonVisible()
        {
            return TryInvokeQuery(CancelButtonQuery, out _);
        }

        /// <summary>
        /// Taps the cancel button, but doesn't confirm the dialog.
        /// </summary>
        public virtual void Cancel()
        {
            App.HideKeyboard();
            App.Tap(CancelButton);
        }

        /// <summary>
        /// Taps the cancel button and confirms the dialog. Navigates back to <see cref="ProgramDetailSettingsControlPage"/>.
        /// </summary>
        public virtual void CancelAndConfirm()
        {
            Cancel();
            new AppDialog().Confirm();
        }

        public virtual string GetCancelButtonText()
        {
            App.HideKeyboard();
            return CancelButton.Text;
        }

        public bool GetIsBackButtonDisplayed()
        {
            return navigationBar.GetIsBackButtonDisplayed();
        }
    }
}
