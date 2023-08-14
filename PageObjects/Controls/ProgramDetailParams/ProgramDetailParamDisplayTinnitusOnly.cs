using HorusUITest.Extensions;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Controls.ProgramDetailParams
{
    public class ProgramDetailParamDisplayTinnitusOnly : BaseEqualizerParamDisplay
    {
        private const string AmplificationTitleAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamDisplayTinnitusOnly.AmplificationTitle";
        private const string EqualizerTitleAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamDisplayTinnitusOnly.EqualizerTitle";
        private const string VolumeSliderAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamDisplayTinnitusOnly.VolumeSlider";
        private const string LowSliderAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamDisplayTinnitusOnly.LowSlider";
        private const string MidSliderAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamDisplayTinnitusOnly.MidSlider";
        private const string HighSliderAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamDisplayTinnitusOnly.HighSlider";

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = AmplificationTitleAID), FindsByIOSUIAutomation(Accessibility = AmplificationTitleAID)]
        private IMobileElement<AppiumWebElement> AmplificationTitle { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = EqualizerTitleAID), FindsByIOSUIAutomation(Accessibility = EqualizerTitleAID)]
        private IMobileElement<AppiumWebElement> EqualizerTitle { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = VolumeSliderAID), FindsByIOSUIAutomation(Accessibility = VolumeSliderAID)]
        private IMobileElement<AppiumWebElement> VolumeSliderElement { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = LowSliderAID), FindsByIOSUIAutomation(Accessibility = LowSliderAID)]
        private IMobileElement<AppiumWebElement> LowSliderElement { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = MidSliderAID), FindsByIOSUIAutomation(Accessibility = MidSliderAID)]
        private IMobileElement<AppiumWebElement> MidSliderElement { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = HighSliderAID), FindsByIOSUIAutomation(Accessibility = HighSliderAID)]
        private IMobileElement<AppiumWebElement> HighSliderElement { get; set; }

        protected override VerticalSlider<ProgramDetailPage> LowSlider => lowSlider;
        protected override VerticalSlider<ProgramDetailPage> MidSlider => midSlider;
        protected override VerticalSlider<ProgramDetailPage> HighSlider => highSlider;

        private HorizontalSlider<ProgramDetailPage> volumeSlider;
        private VerticalSlider<ProgramDetailPage> lowSlider;
        private VerticalSlider<ProgramDetailPage> midSlider;
        private VerticalSlider<ProgramDetailPage> highSlider;

        public ProgramDetailParamDisplayTinnitusOnly(ProgramDetailPage page, IMobileElement<AppiumWebElement> parent) : base(page, parent)
        {
            volumeSlider = new HorizontalSlider<ProgramDetailPage>(page, VolumeSliderElement);
            lowSlider = new VerticalSlider<ProgramDetailPage>(page, LowSliderElement);
            midSlider = new VerticalSlider<ProgramDetailPage>(page, MidSliderElement);
            highSlider = new VerticalSlider<ProgramDetailPage>(page, HighSliderElement);
        }

        public string GetAmplificationTitle()
        {
            return AmplificationTitle.GetTextOrNull();
        }

        public string GetEqualizerTitle()
        {
            return EqualizerTitle.GetTextOrNull();
        }

        public double GetVolumeSliderValue()
        {
            return volumeSlider.GetRelativeValue();
        }

        public ProgramDetailPage SetVolumeSliderValue(double value)
        {
            return volumeSlider.SetRelativeValue(value);
        }

        /// <summary>
        /// Navigates to <see cref="ProgramDetailParamEditTinnitusPage"/>.
        /// </summary>
        public override void OpenSettings()
        {
            base.OpenSettings();
        }
    }
}
