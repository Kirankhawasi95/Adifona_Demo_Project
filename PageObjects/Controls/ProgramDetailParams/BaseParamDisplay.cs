using HorusUITest.Extensions;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Controls.ProgramDetailParams
{
    public class ParamDisplay<T> : BaseFactoryControl
    {
        private const string TitleAID = "Horus.Views.Controls.ProgramDetailParameterContainerControl.Title";

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = TitleAID), FindsByIOSUIAutomation(Accessibility = TitleAID)]
        private IMobileElement<AppiumWebElement> Title { get; set; }

        protected T page;

        public ParamDisplay(T page, IMobileElement<AppiumWebElement> parent) : base(parent)
        {
            this.page = page;
        }

        public string GetTitle()
        {
            return Title.GetTextOrNull();
        }

        /// <summary>
        /// Navigates to the respective parameter settings page.
        /// </summary>
        public virtual void OpenSettings()
        {
            App.Tap(Title);
        }
    }
}
