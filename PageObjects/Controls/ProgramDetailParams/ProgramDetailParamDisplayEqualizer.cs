using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Controls.ProgramDetailParams
{
    public class ProgramDetailParamDisplayEqualizer : BaseEqualizerParamDisplay
    {
        private const string LowSliderAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamDisplayEqualizer.LowSlider";
        private const string MidSliderAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamDisplayEqualizer.MidSlider";
        private const string HighSliderAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamDisplayEqualizer.HighSlider";

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = LowSliderAID), FindsByIOSUIAutomation(Accessibility = LowSliderAID)]
        private IMobileElement<AppiumWebElement> LowSliderElement { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = MidSliderAID), FindsByIOSUIAutomation(Accessibility = MidSliderAID)]
        private IMobileElement<AppiumWebElement> MidSliderElement { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = HighSliderAID), FindsByIOSUIAutomation(Accessibility = HighSliderAID)]
        private IMobileElement<AppiumWebElement> HighSliderElement { get; set; }

        protected override VerticalSlider<ProgramDetailPage> LowSlider => lowSlider;
        protected override VerticalSlider<ProgramDetailPage> MidSlider => midSlider;
        protected override VerticalSlider<ProgramDetailPage> HighSlider => highSlider;

        private VerticalSlider<ProgramDetailPage> lowSlider;
        private VerticalSlider<ProgramDetailPage> midSlider;
        private VerticalSlider<ProgramDetailPage> highSlider;

        public ProgramDetailParamDisplayEqualizer(ProgramDetailPage page, IMobileElement<AppiumWebElement> parent) : base(page, parent)
        {
            lowSlider = new VerticalSlider<ProgramDetailPage>(page, LowSliderElement);
            midSlider = new VerticalSlider<ProgramDetailPage>(page, MidSliderElement);
            highSlider = new VerticalSlider<ProgramDetailPage>(page, HighSliderElement);
        }

        /// <summary>
        /// Navigates to <see cref="ProgramDetailParamEditEqualizerPage"/>.
        /// </summary>
        public override void OpenSettings()
        {
            base.OpenSettings();
        }
    }
}
