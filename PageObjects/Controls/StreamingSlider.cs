using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Controls
{
    public class StreamingSlider<T> : HorizontalSlider<T>
    {
        private const string HorizontalSliderAID = "Horus.Views.Controls.StreamingSlider.HorizontalSlider";
        private const string EnvironmentTitleAID = "Horus.Views.Controls.StreamingSlider.EnvironmentTitle";
        private const string SourceTitleAID = "Horus.Views.Controls.StreamingSlider.SourceTitle";

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = EnvironmentTitleAID), FindsByIOSUIAutomation(Accessibility = EnvironmentTitleAID)]
        private IMobileElement<AppiumWebElement> EnvironmentTitle { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = SourceTitleAID), FindsByIOSUIAutomation(Accessibility = SourceTitleAID)]
        private IMobileElement<AppiumWebElement> SourceTitle { get; set; }

        public StreamingSlider(T page, IMobileElement<AppiumWebElement> sliderElement) : base(page, sliderElement) { }

        public string GetEnvironmentTitle()
        {
            return EnvironmentTitle.Text;
        }

        public string GetSourceTitle()
        {
            return SourceTitle.Text;
        }
    }
}
