using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Controls.ProgramDetailParams
{
    public class ProgramDetailParamDisplayStreaming : ParamDisplay<ProgramDetailPage>
    {
        private const string SliderAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamDisplayStreaming.Slider";

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = SliderAID), FindsByIOSUIAutomation(Accessibility = SliderAID)]
        private IMobileElement<AppiumWebElement> SliderElement { get; set; }

        private HorizontalSlider<ProgramDetailPage> slider;

        public ProgramDetailParamDisplayStreaming(ProgramDetailPage page, IMobileElement<AppiumWebElement> parent) : base(page, parent)
        {
            slider = new HorizontalSlider<ProgramDetailPage>(page, SliderElement);
        }

        public double GetSliderValue()
        {
            return slider.GetRelativeValue();
        }

        public ProgramDetailPage SetSliderValue(double value)
        {
            return slider.SetRelativeValue(value);
        }

        /// <summary>
        /// Navigates to <see cref="ProgramDetailParamEditStreamingPage"/>.
        /// </summary>
        public override void OpenSettings()
        {
            base.OpenSettings();
        }
    }
}
