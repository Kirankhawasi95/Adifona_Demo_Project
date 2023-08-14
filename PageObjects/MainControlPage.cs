using System;
using HorusUITest.Extensions;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Menu;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects
{
    public abstract class MainControlPage<T> : BaseFactoryPage where T : MainControlPage<T>
    {
        //Capture transient message when changing volume.

        private const string MainMenuButtonAID = "Horus.Views.MainControlPage.ctrlMenuButton";
        private const string LeftDeviceAID = "Horus.Views.MainControlPage.LeftDevice";
        private const string RightDeviceAID = "Horus.Views.MainControlPage.RightDevice";
        private const string DemoModeLabelAID = "Horus.Views.MainControlPage.DemoModeLabel";
        private const string VolumeControlAID = "Horus.Views.MainControlPage.BottomVolumeControl";
        private const string DeviceTextAID = "Horus.Views.Controls.HearingInstrumentStateControl.Text";

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = MainMenuButtonAID), FindsByIOSUIAutomation(Accessibility = MainMenuButtonAID)]
        private IMobileElement<AppiumWebElement> MainMenuButton { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = LeftDeviceAID), FindsByIOSUIAutomation(Accessibility = LeftDeviceAID)]
        private IMobileElement<AppiumWebElement> LeftDevice { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = RightDeviceAID), FindsByIOSUIAutomation(Accessibility = RightDeviceAID)]
        private IMobileElement<AppiumWebElement> RightDevice { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = DemoModeLabelAID), FindsByIOSUIAutomation(Accessibility = DemoModeLabelAID)]
        private IMobileElement<AppiumWebElement> DemoModeLabel { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = VolumeControlAID), FindsByIOSUIAutomation(Accessibility = VolumeControlAID)]
        private IMobileElement<AppiumWebElement> VolumeControl { get; set; }

        private Func<IMobileElement<AppiumWebElement>, IMobileElement<AppiumWebElement>> DeviceText => (e) => App.FindElementByAutomationId(DeviceTextAID, root: e);

        protected VolumeControl<MainControlPage<T>> volumeControl;

        private AppiumWebElement DemoModeLabelElement => App.FindElementByAutomationId(DemoModeLabelAID);
        private AppiumWebElement LeftDeviceElement => App.FindElementByAutomationId(LeftDeviceAID);
        private AppiumWebElement RightDeviceElement => App.FindElementByAutomationId(RightDeviceAID);

        public MainControlPage(bool assertOnPage) : base(assertOnPage)
        {
            volumeControl = new VolumeControl<MainControlPage<T>>(this, VolumeControl);
        }

        public MainControlPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
            volumeControl = new VolumeControl<MainControlPage<T>>(this, VolumeControl);
        }

        public abstract string GetCurrentProgramName();

        /// <summary>
        /// This Ui element with this locator is not present on iOS
        /// </summary>
        /// <returns></returns>
        public bool GetIsMenuHamburgerButtonDisplayed()
        {
            if (OnAndroid)
                return MainMenuButton.Exists();
            else
                return true;
        }

        /// <summary>
        /// Navigates to <see cref="MainMenuPage"/>
        /// </summary>
        public void OpenMenuUsingTap()
        {
            App.Tap(MainMenuButton);
        }

        /// <summary>
        /// Navigates to <see cref="MainMenuPage"/>
        /// </summary>
        public void OpenMenuUsingSwipe()
        {
            App.SwipeLeftToRight();
            ClearCache();
        }

        public bool GetIsLeftHearingDeviceVisible()
        {
            return TryInvokeQuery(() => LeftDevice, out _);
        }

        public bool GetIsRightHearingDeviceVisible()
        {
            return TryInvokeQuery(() => RightDevice, out _);
        }

        /// <summary>
        /// Navigates to <see cref="HearingInstrumentInfoControlPage"/>.
        /// </summary>
        public void OpenLeftHearingDevice()
        {
            App.Tap(LeftDevice);
        }

        /// <summary>
        /// Navigates to <see cref="HearingInstrumentInfoControlPage"/>.
        /// </summary>
        public void OpenRightHearingDevice()
        {
            App.Tap(RightDevice);
        }

        public string GetLeftDeviceText()
        {
            //return DeviceTexts[0].Text;
            //throw new NoSuchElementException
            return LeftDevice.FindElement(DeviceText).Text;
        }

        public string GetRightDeviceText()
        {
            //int index = DeviceTexts.Count == 2 ? 1 : 0;
            //return DeviceTexts[index].Text;
            return RightDevice.FindElement(DeviceText).Text;
        }

        public T DecreaseVolume()
        {
            return (T)volumeControl.DecreaseVolume();
        }

        public T IncreaseVolume()
        {
            return (T)volumeControl.IncreaseVolume();
        }

        public bool GetIsBinauralSettingsButtonVisible()
        {
            return volumeControl.GetIsBinauralButtonVisible();
        }

        public double GetVolumeSliderValue()
        {
            return volumeControl.GetVolumeSliderValue();
        }

        public T SetVolumeSliderValue(double value)
        {
            return (T)volumeControl.SetVolumeSliderValue(value);
        }

        /// <summary>
        /// Get Top Bar
        /// </summary>
        /// <returns></returns>
        public string GetTopBarColor()
        {
            return App.GetColorFromImageByPixel(DemoModeLabelElement, 10, 10);
        }

        /// <summary>
        /// Get Left HA Color
        /// </summary>
        /// <returns></returns>
        public string GetLeftDeviceColor()
        {
            return App.GetColorFromImageByPixel(LeftDeviceElement, 73, 66);
        }

        /// <summary>
        /// Get Right HA Color
        /// </summary>
        /// <returns></returns>
        public string GetRightDeviceColor()
        {
            return App.GetColorFromImageByPixel(RightDeviceElement, 88, 65);
        }
    }
}
