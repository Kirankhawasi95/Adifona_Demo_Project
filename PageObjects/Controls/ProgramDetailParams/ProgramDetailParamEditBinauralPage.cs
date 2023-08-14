using System;
using System.Threading;
using HorusUITest.Enums;
using HorusUITest.Extensions;
using HorusUITest.PageObjects.Interfaces;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Controls.ProgramDetailParams
{
    public class ProgramDetailParamEditBinauralPage : BaseRootedFactoryPage, IHasBackNavigation
    {
        //TODO: Check this page using real hearing aids:
        //  - BinauralSwitch with only 1 Device?
        //  - VolumeControl/s with 1 Device?

        protected override Func<IMobileElement<AppiumWebElement>> RootQuery => () => App.FindElementByAutomationId(ContainerAID, verifyVisibility: true);

        private const string ContainerAID = "Horus.Views.MainControlPage.BinauralSettingsContainer";
        private const string TitleAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditBinaural.Title";
        private const string DescriptionAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditBinaural.Description";
        private const string BinauralSwitchAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditBinaural.ToggleBinauralSwitch";
        private const string BinauralSwitchTitleAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditBinaural.BinauralSwitchTitle";
        private const string LeftTitleAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditBinaural.ctrlDoubleAdvancedLabelLeft";
        private const string RightTitleAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditBinaural.ctrlDoubleVolumeSelectorRight";
        private const string SingleVolumeControlAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditBinaural.ctrlSingleVolumeSelector";
        private const string LeftVolumeControlAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditBinaural.ctrlDoubleVolumeSelectorLeft";
        private const string RightVolumeControlAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditBinaural.ctrlDoubleVolumeSelectorRight";
        private const string CloseButtonAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailBinauralSettingContainer.Close";

        [FindsByAndroidUIAutomator(Accessibility = TitleAID), FindsByIOSUIAutomation(Accessibility = TitleAID)]
        private IMobileElement<AppiumWebElement> Title { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = DescriptionAID), FindsByIOSUIAutomation(Accessibility = DescriptionAID)]
        private IMobileElement<AppiumWebElement> Description { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = BinauralSwitchAID), FindsByIOSUIAutomation(Accessibility = BinauralSwitchAID)]
        private IMobileElement<AppiumWebElement> BinauralSwitch { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = BinauralSwitchTitleAID), FindsByIOSUIAutomation(Accessibility = BinauralSwitchTitleAID)]
        private IMobileElement<AppiumWebElement> BinauralSwitchTitle { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = LeftTitleAID), FindsByIOSUIAutomation(Accessibility = LeftTitleAID)]
        private IMobileElement<AppiumWebElement> LeftTitle { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = RightTitleAID), FindsByIOSUIAutomation(Accessibility = RightTitleAID)]
        private IMobileElement<AppiumWebElement> RightTitle { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = SingleVolumeControlAID), FindsByIOSUIAutomation(Accessibility = SingleVolumeControlAID)]
        private IMobileElement<AppiumWebElement> SingleVolumeControlElement { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = LeftVolumeControlAID), FindsByIOSUIAutomation(Accessibility = LeftVolumeControlAID)]
        private IMobileElement<AppiumWebElement> LeftVolumeControlElement { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = RightVolumeControlAID), FindsByIOSUIAutomation(Accessibility = RightVolumeControlAID)]
        private IMobileElement<AppiumWebElement> RightVolumeControlElement { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = CloseButtonAID), FindsByIOSUIAutomation(Accessibility = CloseButtonAID)]
        private IMobileElement<AppiumWebElement> CloseButton { get; set; }

        private VolumeControl<ProgramDetailParamEditBinauralPage> singleVolumeControl;
        private VolumeControl<ProgramDetailParamEditBinauralPage> SingleVolumeControl
        {
            get
            {
                singleVolumeControl = singleVolumeControl ?? new VolumeControl<ProgramDetailParamEditBinauralPage>(this, SingleVolumeControlElement);
                return singleVolumeControl;
            }
        }

        private VolumeControl<ProgramDetailParamEditBinauralPage> leftVolumeControl;
        private VolumeControl<ProgramDetailParamEditBinauralPage> LeftVolumeControl
        {
            get
            {
                leftVolumeControl = leftVolumeControl ?? new VolumeControl<ProgramDetailParamEditBinauralPage>(this, LeftVolumeControlElement);
                return leftVolumeControl;
            }
        }

        private VolumeControl<ProgramDetailParamEditBinauralPage> rightVolumeControl;

        public ProgramDetailParamEditBinauralPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public ProgramDetailParamEditBinauralPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        private VolumeControl<ProgramDetailParamEditBinauralPage> RightVolumeControl
        {
            get
            {
                rightVolumeControl = rightVolumeControl ?? new VolumeControl<ProgramDetailParamEditBinauralPage>(this, RightVolumeControlElement);
                return rightVolumeControl;
            }
        }

        public string GetTitle()
        {
            return Title.Text;
        }

        public string GetDescription()
        {
            return Description.Text;
        }

        public string GetBinauralSwitchTitle()
        {
            return BinauralSwitchTitle.Text;
        }

        public bool GetIsBinauralSwitchChecked()
        {
            return App.GetToggleSwitchState(BinauralSwitch);
        }

        public ProgramDetailParamEditBinauralPage ToggleBinauralSwitch(bool waitForAnimation = true)
        {
            App.Tap(BinauralSwitch);
            if (waitForAnimation)
                Thread.Sleep(600);
            return this;
        }

        public ProgramDetailParamEditBinauralPage TurnOnBinauralSeparation(bool waitForAnimation = true)
        {
            if (!GetIsBinauralSwitchChecked())
            {
                ToggleBinauralSwitch(waitForAnimation);
            }
            return this;
        }

        public ProgramDetailParamEditBinauralPage TurnOffBinauralSeparation(bool waitForAnimation = true)
        {
            if (GetIsBinauralSwitchChecked())
            {
                ToggleBinauralSwitch(waitForAnimation);
            }
            return this;
        }

        private IMobileElement<AppiumWebElement> GetVolumeControlElement(VolumeChannel channel)
        {
            switch (channel)
            {
                case VolumeChannel.Single: return SingleVolumeControlElement;
                case VolumeChannel.Left: return LeftVolumeControlElement;
                case VolumeChannel.Right: return RightVolumeControlElement;
                default: throw new NotImplementedException("Unknown VolumeChannel.");
            }
        }

        private VolumeControl<ProgramDetailParamEditBinauralPage> GetVolumeControl(VolumeChannel channel)
        {
            switch (channel)
            {
                case VolumeChannel.Single: return SingleVolumeControl;
                case VolumeChannel.Left: return LeftVolumeControl;
                case VolumeChannel.Right: return RightVolumeControl;
                default: throw new NotImplementedException("Unknown VolumeChannel.");
            }
        }

        public bool GetIsVolumeControlVisible(VolumeChannel channel)
        {
            return GetVolumeControlElement(channel).Exists();
        }

        public double GetVolumeSliderValue(VolumeChannel channel)
        {
            return GetVolumeControl(channel).GetVolumeSliderValue();
        }

        public ProgramDetailParamEditBinauralPage SetVolumeSliderValue(VolumeChannel channel, double value)
        {
            return GetVolumeControl(channel).SetVolumeSliderValue(value);
        }

        public ProgramDetailParamEditBinauralPage IncreaseVolume(VolumeChannel channel)
        {
            return GetVolumeControl(channel).IncreaseVolume();
        }

        public ProgramDetailParamEditBinauralPage DecreaseVolume(VolumeChannel channel)
        {
            return GetVolumeControl(channel).DecreaseVolume();
        }
        public void PressVolumeSliderAndCloseMenu()
        {
            IMultiAction multiAction = new MultiAction(App.Driver);
            ITouchAction pressVolumeSliderAction = singleVolumeControl.PressVolumeSlider();
            ITouchAction closeMenuAction = new TouchAction(App.Driver).Tap(CloseButton);
            multiAction.Add(pressVolumeSliderAction).Add(closeMenuAction).Perform();
        }

        public void Close()
        {
            App.Tap(CloseButton);
        }

        public void NavigateBack()
        {
            Close();
        }
    }
}
