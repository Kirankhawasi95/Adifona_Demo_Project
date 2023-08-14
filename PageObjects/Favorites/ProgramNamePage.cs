using System;
using System.Threading;
using HorusUITest.PageObjects.Controls;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Favorites
{
    public class ProgramNamePage : BaseProgramConfigPage
    {
        protected override Func<IMobileElement<AppiumWebElement>> NavigationBarQuery => () => NavigationBar;
        protected override Func<IMobileElement<AppiumWebElement>> CancelButtonQuery => () => CancelButton;

        private const string NavigationBarAID = "Horus.Views.Favorites.ProgramNamePage.NavigationBar";
        private const string DescriptionAID = "Horus.Views.Favorites.ProgramNamePage.Description";
        private const string NameTitleAID = "Horus.Views.Favorites.ProgramNamePage.NameTitle";
        private const string NameEntryAID = "Horus.Views.Favorites.ProgramNamePage.NameEntry";
        private const string CancelButtonAID = "Horus.Views.Favorites.ProgramNamePage.CancelButton";
        private const string ProceedButtonAID = "Horus.Views.Favorites.ProgramNamePage.ProceedButton";

        [FindsByAndroidUIAutomator(Accessibility = NavigationBarAID), FindsByIOSUIAutomation(Accessibility = NavigationBarAID)]
        private IMobileElement<AppiumWebElement> NavigationBar { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = DescriptionAID), FindsByIOSUIAutomation(Accessibility = DescriptionAID)]
        private IMobileElement<AppiumWebElement> Description { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = NameTitleAID), FindsByIOSUIAutomation(Accessibility = NameTitleAID)]
        private IMobileElement<AppiumWebElement> NameTitle { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = NameEntryAID), FindsByIOSUIAutomation(Accessibility = NameEntryAID)]
        private IMobileElement<AppiumWebElement> NameEntry { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = CancelButtonAID), FindsByIOSUIAutomation(Accessibility = CancelButtonAID)]
        private IMobileElement<AppiumWebElement> CancelButton { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = ProceedButtonAID), FindsByIOSUIAutomation(Accessibility = ProceedButtonAID)]
        private IMobileElement<AppiumWebElement> ProceedButton { get; set; }

        public ProgramNamePage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public ProgramNamePage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        public string GetDescription()
        {
            return Description.Text;
        }

        public string GetNameTitle()
        {
            return NameTitle.Text;
        }

        public string GetName()
        {
            return NameEntry.Text;
        }

        public ProgramNamePage EnterName(string name)
        {
            App.HideKeyboard();
            NameEntry.Clear();
            App.HideKeyboard();
            NameEntry.SendKeys(name);
            App.HideKeyboard();
            return this;
        }

        public ProgramNamePage ClearName()
        {
            NameEntry.Clear();
            return this;
        }

        public override string GetCancelButtonText()
        {
            App.HideKeyboard();
            return base.GetCancelButtonText();
        }

        /// <summary>
        /// Taps the button 'Next' or 'Accept' respectively, depending on the current workflow.
        /// 'Next' navigates to <see cref="ProgramIconPage"/>. 'Accept' navigates to <see cref="ProgramDetailSettingsControlPage"/>.
        /// </summary>
        public void Proceed()
        {
            App.HideKeyboard();
            App.Tap(ProceedButton);
        }

        public string GetProceedButtonText()
        {
            App.HideKeyboard();
            return ProceedButton.Text;
        }
    }
}
