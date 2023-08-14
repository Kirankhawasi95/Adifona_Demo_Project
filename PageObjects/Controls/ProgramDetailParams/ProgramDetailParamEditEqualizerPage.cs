using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Controls.ProgramDetailParams
{
    public class ProgramDetailParamEditEqualizerPage : BaseEqualizerParamEdit<ProgramDetailParamEditEqualizerPage>
    {
        protected override string ContainerAID => "Horus.Views.MainControlPage.EqualizerContainer";

        private const string TitleAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditEqualizer.Title";
        private const string DescriptionAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditEqualizer.Description";
        private const string LowSliderAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditEqualizer.LowSlider";
        private const string MidSliderAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditEqualizer.MidSlider";
        private const string HighSliderAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditEqualizer.HighSlider";
        private const string LowTitleAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditEqualizer.LowTitle";
        private const string MidTitleAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditEqualizer.MidTitle";
        private const string HighTitleAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditEqualizer.HighTitle";

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = TitleAID), FindsByIOSUIAutomation(Accessibility = TitleAID)]
        private IMobileElement<AppiumWebElement> Title { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = DescriptionAID), FindsByIOSUIAutomation(Accessibility = DescriptionAID)]
        private IMobileElement<AppiumWebElement> Description { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = LowSliderAID), FindsByIOSUIAutomation(Accessibility = LowSliderAID)]
        private IMobileElement<AppiumWebElement> LowSliderElement { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = MidSliderAID), FindsByIOSUIAutomation(Accessibility = MidSliderAID)]
        private IMobileElement<AppiumWebElement> MidSliderElement { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = HighSliderAID), FindsByIOSUIAutomation(Accessibility = HighSliderAID)]
        private IMobileElement<AppiumWebElement> HighSliderElement { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = LowTitleAID), FindsByIOSUIAutomation(Accessibility = LowTitleAID)]
        private IMobileElement<AppiumWebElement> LowTitle { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = MidTitleAID), FindsByIOSUIAutomation(Accessibility = MidTitleAID)]
        private IMobileElement<AppiumWebElement> MidTitle { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = HighTitleAID), FindsByIOSUIAutomation(Accessibility = HighTitleAID)]
        private IMobileElement<AppiumWebElement> HighTitle { get; set; }

        protected override Func<IMobileElement<AppiumWebElement>> LowTitleQuery => () => LowTitle;
        protected override Func<IMobileElement<AppiumWebElement>> MidTitleQuery => () => MidTitle;
        protected override Func<IMobileElement<AppiumWebElement>> HighTitleQuery => () => HighTitle;
        protected override VerticalSlider<ProgramDetailParamEditEqualizerPage> LowSlider => lowSlider;
        protected override VerticalSlider<ProgramDetailParamEditEqualizerPage> MidSlider => midSlider;
        protected override VerticalSlider<ProgramDetailParamEditEqualizerPage> HighSlider => highSlider;

        private VerticalSlider<ProgramDetailParamEditEqualizerPage> lowSlider;
        private VerticalSlider<ProgramDetailParamEditEqualizerPage> midSlider;
        private VerticalSlider<ProgramDetailParamEditEqualizerPage> highSlider;

        private void Initialize()
        {
            lowSlider = new VerticalSlider<ProgramDetailParamEditEqualizerPage>(this, LowSliderElement);
            midSlider = new VerticalSlider<ProgramDetailParamEditEqualizerPage>(this, MidSliderElement);
            highSlider = new VerticalSlider<ProgramDetailParamEditEqualizerPage>(this, HighSliderElement);
        }

        public ProgramDetailParamEditEqualizerPage(bool assertOnPage = true) : base(assertOnPage)
        {
            Initialize();
        }

        public ProgramDetailParamEditEqualizerPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
            Initialize();
        }

        public string GetTitle()
        {
            return Title.Text;
        }

        public string GetDescription()
        {
            return Description.Text;
        }
    }
}
