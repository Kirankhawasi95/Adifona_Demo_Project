using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Controls.ProgramDetailParams
{
    public class ProgramDetailParamEditTinnitusPage : BaseEqualizerParamEdit<ProgramDetailParamEditTinnitusPage>
    {
        protected override string ContainerAID => "Horus.Views.MainControlPage.TinnitusContainer";

        private const string TitleAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditTinnitus.Title";

        //Noise
        private const string TinnitusSwitchTitleAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditTinnitus.TinnitusSwitchTitle";
        private const string TinnitusSwitchAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditTinnitus.TinnitusSwitch";

        //Volume
        private const string VolumeTitleAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditTinnitus.AmplificationTitle";
        private const string VolumeSliderAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditTinnitus.AmplificationSlider";
        private const string VolumeLowTitleAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditTinnitus.AmplificationLowTitle";
        private const string VolumeHighTitleAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditTinnitus.AmplificationHighTitle";

        //Equalizer
        private const string EqualizerTitleAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditTinnitus.EqualizerTitle";
        private const string LowSliderAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditTinnitus.LowSlider";
        private const string MidSliderAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditTinnitus.MidSlider";
        private const string HighSliderAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditTinnitus.HighSlider";
        private const string LowTitleAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditTinnitus.LowSliderTitle";
        private const string MidTitleAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditTinnitus.MidSliderTitle";
        private const string HighTitleAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditTinnitus.HighSliderTitle";

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = TitleAID), FindsByIOSUIAutomation(Accessibility = TitleAID)]
        private IMobileElement<AppiumWebElement> Title { get; set; }

        //Noise
        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = TinnitusSwitchTitleAID), FindsByIOSUIAutomation(Accessibility = TinnitusSwitchTitleAID)]
        private IMobileElement<AppiumWebElement> TinnitusSwitchTitle { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = TinnitusSwitchAID), FindsByIOSUIAutomation(Accessibility = TinnitusSwitchAID)]
        private IMobileElement<AppiumWebElement> TinnitusSwitch { get; set; }

        //Volume
        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = VolumeTitleAID), FindsByIOSUIAutomation(Accessibility = VolumeTitleAID)]
        private IMobileElement<AppiumWebElement> VolumeTitle { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = VolumeSliderAID), FindsByIOSUIAutomation(Accessibility = VolumeSliderAID)]
        private IMobileElement<AppiumWebElement> AmplificationSliderElement { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = VolumeLowTitleAID), FindsByIOSUIAutomation(Accessibility = VolumeLowTitleAID)]
        private IMobileElement<AppiumWebElement> VolumeLowTitle { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = VolumeHighTitleAID), FindsByIOSUIAutomation(Accessibility = VolumeHighTitleAID)]
        private IMobileElement<AppiumWebElement> VolumeHighTitle { get; set; }

        //Equalizer
        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = EqualizerTitleAID), FindsByIOSUIAutomation(Accessibility = EqualizerTitleAID)]
        private IMobileElement<AppiumWebElement> EqualizerTitle { get; set; }

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

        protected override VerticalSlider<ProgramDetailParamEditTinnitusPage> LowSlider => lowSlider;
        protected override VerticalSlider<ProgramDetailParamEditTinnitusPage> MidSlider => midSlider;
        protected override VerticalSlider<ProgramDetailParamEditTinnitusPage> HighSlider => highSlider;
        protected override Func<IMobileElement<AppiumWebElement>> LowTitleQuery => () => LowTitle;
        protected override Func<IMobileElement<AppiumWebElement>> MidTitleQuery => () => MidTitle;
        protected override Func<IMobileElement<AppiumWebElement>> HighTitleQuery => () => HighTitle;

        private HorizontalSlider<ProgramDetailParamEditTinnitusPage> volumeSlider;
        private VerticalSlider<ProgramDetailParamEditTinnitusPage> lowSlider;
        private VerticalSlider<ProgramDetailParamEditTinnitusPage> midSlider;
        private VerticalSlider<ProgramDetailParamEditTinnitusPage> highSlider;

        private void Initialize()
        {
            volumeSlider = new HorizontalSlider<ProgramDetailParamEditTinnitusPage>(this, AmplificationSliderElement);
            lowSlider = new VerticalSlider<ProgramDetailParamEditTinnitusPage>(this, LowSliderElement);
            midSlider = new VerticalSlider<ProgramDetailParamEditTinnitusPage>(this, MidSliderElement);
            highSlider = new VerticalSlider<ProgramDetailParamEditTinnitusPage>(this, HighSliderElement);
        }

        public ProgramDetailParamEditTinnitusPage(bool assertOnPage = true) : base(assertOnPage)
        {
            Initialize();
        }

        public ProgramDetailParamEditTinnitusPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
            Initialize();
        }

        public string GetTitle()
        {
            return Title.Text;
        }

        public string GetTinnitusSwitchTitle()
        {
            return TinnitusSwitchTitle.Text;
        }

        public bool GetIsTinnitusSwitchChecked()
        {
            return App.GetToggleSwitchState(TinnitusSwitch);
        }

        public ProgramDetailParamEditTinnitusPage ToggleTinnitusSwitch()
        {
            App.Tap(TinnitusSwitch);
            return this;
        }

        public ProgramDetailParamEditTinnitusPage TurnOnTinnitus()
        {
            if (!GetIsTinnitusSwitchChecked())
            {
                ToggleTinnitusSwitch();
            }
            return this;
        }

        public ProgramDetailParamEditTinnitusPage TurnOffTinnitus()
        {
            if (GetIsTinnitusSwitchChecked())
            {
                ToggleTinnitusSwitch();
            }
            return this;
        }

        public string GetEqualizerTitle()
        {
            return EqualizerTitle.Text;
        }

        public string GetVolumeTitle()
        {
            return VolumeTitle.Text;
        }

        public string GetVolumeLowTitle()
        {
            return VolumeLowTitle.Text;
        }

        public string GetVolumeHighTitle()
        {
            return VolumeHighTitle.Text;
        }

        public double GetVolumeSliderValue()
        {
            return volumeSlider.GetRelativeValue();
        }

        public ProgramDetailParamEditTinnitusPage SetVolumeSliderValue(double value)
        {
            return volumeSlider.SetRelativeValue(value);
        }
    }
}
