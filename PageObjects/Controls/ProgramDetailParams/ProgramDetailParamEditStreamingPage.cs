using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Controls.ProgramDetailParams
{
    public class ProgramDetailParamEditStreamingPage : BaseParamEdit<ProgramDetailParamEditStreamingPage>
    {
        protected override string ContainerAID => "Horus.Views.MainControlPage.StreamingContainer";

        private const string TitleAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditStreaming.Title";
        private const string DescriptionAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditStreaming.Description";
        private const string StreamingSliderAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditStreaming.StreamingSlider";

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = TitleAID), FindsByIOSUIAutomation(Accessibility = TitleAID)]
        private IMobileElement<AppiumWebElement> Title { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = DescriptionAID), FindsByIOSUIAutomation(Accessibility = DescriptionAID)]
        private IMobileElement<AppiumWebElement> Description { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = StreamingSliderAID), FindsByIOSUIAutomation(Accessibility = StreamingSliderAID)]
        private IMobileElement<AppiumWebElement> StreamingSliderElement { get; set; }

        StreamingSlider<ProgramDetailParamEditStreamingPage> streamingSlider;

        public ProgramDetailParamEditStreamingPage(bool assertOnPage = true) : base(assertOnPage)
        {
            streamingSlider = new StreamingSlider<ProgramDetailParamEditStreamingPage>(this, StreamingSliderElement);
        }

        public ProgramDetailParamEditStreamingPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
            streamingSlider = new StreamingSlider<ProgramDetailParamEditStreamingPage>(this, StreamingSliderElement);
        }

        public string GetTitle()
        {
            return Title.Text;
        }

        public string GetDescription()
        {
            return Description.Text;
        }

        public double GetStreamingSliderValue()
        {
            return streamingSlider.GetRelativeValue();
        }

        public ProgramDetailParamEditStreamingPage SetStreamingSliderValue(double value)
        {
            return streamingSlider.SetRelativeValue(value);
        }

        public string GetEnvironmentTitle()
        {
            return streamingSlider.GetEnvironmentTitle();
        }

        public string GetSourceTitle()
        {
            return streamingSlider.GetSourceTitle();
        }
    }
}
