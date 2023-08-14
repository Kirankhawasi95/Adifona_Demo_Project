using HorusUITest.Extensions;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Controls
{
    public class VolumeControl<T> : BaseFactoryControl
    {
        protected const string VolumeSliderAID = "Horus.Views.Controls.VolumeControl.VolumeSlider";
        protected const string DecreaseVolumeButtonAID = "Horus.Views.Controls.VolumeControl.DecreaseVolumeButton";
        protected const string IncreaseVolumeButtonAID = "Horus.Views.Controls.VolumeControl.IncreaseVolumeButton";
        protected const string BinauralButtonAID = "Horus.Views.Controls.VolumeControl.BinauralButton";
        protected const string TransientVolumeDisplayAID = "Horus.Views.Controls.VolumeControl.TransientVolumeDisplay";
        protected const string TransientVolumeValueAID = "Horus.Views.Controls.VolumeControl.TransientVolumeValue";
        protected const string TransientMuteAID = "Horus.Views.Controls.VolumeControl.TransientMute";

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = VolumeSliderAID), FindsByIOSUIAutomation(Accessibility = VolumeSliderAID)]
        private IMobileElement<AppiumWebElement> VolumeSlider { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = DecreaseVolumeButtonAID), FindsByIOSUIAutomation(Accessibility = DecreaseVolumeButtonAID)]
        private IMobileElement<AppiumWebElement> DecreaseVolumeButton { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = IncreaseVolumeButtonAID), FindsByIOSUIAutomation(Accessibility = IncreaseVolumeButtonAID)]
        private IMobileElement<AppiumWebElement> IncreaseVolumeButton { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = BinauralButtonAID), FindsByIOSUIAutomation(Accessibility = BinauralButtonAID)]
        private IMobileElement<AppiumWebElement> BinauralButton { get; set; }

        //[FindsByAndroidUIAutomator(Accessibility = TransientVolumeDisplayAID), FindsByIOSUIAutomation(Accessibility = TransientVolumeDisplayAID)]
        //private IMobileElement<AppiumWebElement> TransientVolumeDisplay { get; set; }

        //[FindsByAndroidUIAutomator(Accessibility = TransientVolumeValueAID), FindsByIOSUIAutomation(Accessibility = TransientVolumeValueAID)]
        //private IMobileElement<AppiumWebElement> TransientVolumeValue { get; set; }

        //[FindsByAndroidUIAutomator(Accessibility = TransientMuteAID), FindsByIOSUIAutomation(Accessibility = TransientMuteAID)]
        //private IMobileElement<AppiumWebElement> TransientMute { get; set; }

        private readonly T page;
        private HorizontalSlider<T> slider;

        public VolumeControl(T page, IMobileElement<AppiumWebElement> parent) : base(parent)
        {
            this.page = page;
            slider = new HorizontalSlider<T>(page, VolumeSlider);
        }

        public T DecreaseVolume()
        {
            App.Tap(DecreaseVolumeButton);
            return page;
        }

        public T IncreaseVolume()
        {
            App.Tap(IncreaseVolumeButton);
            return page;
        }

        public double GetVolumeSliderValue()
        {
            return slider.GetRelativeValue();
        }

        public T SetVolumeSliderValue(double value)
        {
            return slider.SetRelativeValue(value);
        }

        public bool GetIsBinauralButtonVisible()
        {
            return BinauralButton.Exists();
        }

        public void OpenBinauralSettings()
        {
            App.Tap(BinauralButton);
        }
        public ITouchAction PressVolumeSlider()
        {
            return slider.PressHorizontalVolumeSlider().Wait(3000);
        }
    }
}
