using System;
using AventStack.ExtentReports;
using HorusUITest.Enums;
using HorusUITest.Extensions;
using HorusUITest.Helper;
using HorusUITest.PageObjects.Interfaces;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Controls
{
    public class HearingInstrumentInfoControlPage : BaseFactoryPage, IHasBackNavigation
    {
        protected override Func<IMobileElement<AppiumWebElement>> TraitQuery => () => Title;

        private const string TitleAID = "Horus.Views.Controls.HearingInstrumentInfoControl.Title";
        private const string DeviceTypeAID = "Horus.Views.Controls.HearingInstrumentInfoControl.DeviceType";
        private const string DeviceNameAID = "Horus.Views.Controls.HearingInstrumentInfoControl.DeviceName";
        private const string DeviceSerialAID = "Horus.Views.Controls.HearingInstrumentInfoControl.DeviceSerial";
        private const string DeviceUdiAID = "Horus.Views.Controls.HearingInstrumentInfoControl.DeviceUdi";
        private const string DeviceTypeTitleAID = "Horus.Views.Controls.HearingInstrumentInfoControl.DeviceTypeTitle";
        private const string DeviceNameTitleAID = "Horus.Views.Controls.HearingInstrumentInfoControl.DeviceNameTitle";
        private const string DeviceSerialTitleAID = "Horus.Views.Controls.HearingInstrumentInfoControl.DeviceSerialTitle";
        private const string DeviceUdiTitleAID = "Horus.Views.Controls.HearingInstrumentInfoControl.DeviceUdiTitle";
        private const string BatteryLevelTitleAID = "Horus.Views.Controls.HearingInstrumentInfoControl.BatteryLevelTitle";
        private const string BatteryLevelValueAID = "Horus.Views.Controls.HearingInstrumentInfoControl.BatteryLevelValue";
        private const string CloseButtonAID = "Horus.Views.Controls.HearingInstrumentInfoControl.CloseButton";

        private const string InstrumentValidationDisplayControlAID = "Horus.Views.Controls.HearingInstrumentInfoControl.InstrumentValidationDisplayControl";
        private const string InstrumentControlTextAID = "Horus.Views.Controls.Init.InstrumentValidationDisplayControl.DisplayText";
        private const string InstrumentControlPercentageAID = "Horus.Views.Controls.Init.InstrumentValidationDisplayControl.Percentage";
        
        public HearingInstrumentInfoControlPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public HearingInstrumentInfoControlPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        [FindsByAndroidUIAutomator(Accessibility = TitleAID), FindsByIOSUIAutomation(Accessibility = TitleAID)]
        private IMobileElement<AppiumWebElement> Title { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = DeviceTypeAID), FindsByIOSUIAutomation(Accessibility = DeviceTypeAID)]
        private IMobileElement<AppiumWebElement> DeviceType { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = DeviceNameAID), FindsByIOSUIAutomation(Accessibility = DeviceNameAID)]
        private IMobileElement<AppiumWebElement> DeviceName { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = DeviceSerialAID), FindsByIOSUIAutomation(Accessibility = DeviceSerialAID)]
        private IMobileElement<AppiumWebElement> DeviceSerial { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = DeviceUdiAID), FindsByIOSUIAutomation(Accessibility = DeviceUdiAID)]
        private IMobileElement<AppiumWebElement> DeviceUdi { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = DeviceTypeTitleAID), FindsByIOSUIAutomation(Accessibility = DeviceTypeTitleAID)]
        private IMobileElement<AppiumWebElement> DeviceTypeTitle { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = DeviceNameTitleAID), FindsByIOSUIAutomation(Accessibility = DeviceNameTitleAID)]
        private IMobileElement<AppiumWebElement> DeviceNameTitle { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = DeviceSerialTitleAID), FindsByIOSUIAutomation(Accessibility = DeviceSerialTitleAID)]
        private IMobileElement<AppiumWebElement> DeviceSerialTitle { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = DeviceUdiTitleAID), FindsByIOSUIAutomation(Accessibility = DeviceUdiTitleAID)]
        private IMobileElement<AppiumWebElement> DeviceUdiTitle { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = BatteryLevelTitleAID), FindsByIOSUIAutomation(Accessibility = BatteryLevelTitleAID)]
        private IMobileElement<AppiumWebElement> BatteryLevelTitle { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = BatteryLevelValueAID), FindsByIOSUIAutomation(Accessibility = BatteryLevelValueAID)]
        private IMobileElement<AppiumWebElement> BatteryLevelValue { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = CloseButtonAID), FindsByIOSUIAutomation(Accessibility = CloseButtonAID)]
        private IMobileElement<AppiumWebElement> CloseButton { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = InstrumentControlTextAID), FindsByIOSUIAutomation(Accessibility = InstrumentControlTextAID)]
        private IMobileElement<AppiumWebElement> InstrumentControlText { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = InstrumentControlPercentageAID), FindsByIOSUIAutomation(Accessibility = InstrumentControlPercentageAID)]
        private IMobileElement<AppiumWebElement> InstrumentControlPercentage { get; set; }

        private AppiumWebElement InstrumentValidationDisplayControlElement => App.FindElementByAutomationId(InstrumentValidationDisplayControlAID);

        public bool GetIsDeviceUdiVisible()
        {
            return TryInvokeQuery(() => DeviceUdi, out _);
        }

        public string GetTitle()
        {
            return Title.Text;
        }

        public string GetDeviceType()
        {
            return DeviceType.Text;
        }

        public string GetDeviceName()
        {
            return DeviceName.Text;
        }

        public string GetDeviceSerial()
        {
            return DeviceSerial.Text;
        }

        /// <summary>
        /// Returns the UDI. Throws if there is none.
        /// </summary>
        /// <returns></returns>
        public string GetDeviceUdi()
        {
            return DeviceUdi.Text;
        }

        public string GetBatteryLevelValue()
        {
            return BatteryLevelValue.Text;
        }

        public string GetDeviceTypeTitle()
        {
            return DeviceTypeTitle.Text;
        }

        public string GetDeviceNameTitle()
        {
            return DeviceNameTitle.Text;
        }

        public string GetDeviceSerialTitle()
        {
            return DeviceSerialTitle.Text;
        }

        /// <summary>
        /// Returns the UDI title. Throws if there is none.
        /// </summary>
        /// <returns></returns>
        public string GetDeviceUdiTitle()
        {
            return DeviceUdiTitle.GetTextOrNull();
        }

        public string GetBatteryLevelTitle()
        {
            return BatteryLevelTitle.Text;
        }

        public string GetCloseButtonText()
        {
            return CloseButton.Text;
        }

        public string GetInstrumentControlText()
        {
            return InstrumentControlText.Text;
        }

        public string GetInstrumentControlPercentage()
        {
            return InstrumentControlPercentage.GetTextOrNull();
        }

        public void Close()
        {
            App.Tap(CloseButton);
            //TODO: Maybe add a WaitForPageToLeave() here, since iOS detects background elements and an AssertOnPage() on the following page might return too early.
        }

        public void NavigateBack()
        {
            App.Tap(CloseButton);
        }

        /// <summary>
        /// Get Device Indicator Color
        /// </summary>
        /// <returns></returns>
        public string GetDeviceIndicatorColor()
        {
            return App.GetColorFromImageByPixel(InstrumentValidationDisplayControlElement, 20, 128);
        }

        public HearingInstrumentInfoControlPage CheckHAInformationFromDashboard(AppMode mode)
        {
            ReportHelper.LogTest(Status.Info, "Waiting till Hearing Instrument Info Control Page is loaded...");
            Assert.IsTrue(new HearingInstrumentInfoControlPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Info, "Hearing Instrument Info Control Page is loaded");
            var hearingInstrumentInfoPage = new HearingInstrumentInfoControlPage();
            ReportHelper.LogTest(Status.Info, "Checking Hearing Instrument Info Control Page UI...");
            ReportHelper.LogTest(Status.Info, "Checking title...");
            Assert.IsNotEmpty(hearingInstrumentInfoPage.GetTitle(), "Title is empty");
            ReportHelper.LogTest(Status.Info, "Title is not empty");
            ReportHelper.LogTest(Status.Info, "Checking device type title...");
            Assert.IsNotEmpty(hearingInstrumentInfoPage.GetDeviceTypeTitle(), "Device type title is empty");
            ReportHelper.LogTest(Status.Info, "Device type title is not empty");
            ReportHelper.LogTest(Status.Info, "Checking device type...");
            Assert.IsNotEmpty(hearingInstrumentInfoPage.GetDeviceType(), "Device type is empty");
            ReportHelper.LogTest(Status.Info, "Device type is not empty");
            ReportHelper.LogTest(Status.Info, "Checking device name title...");
            Assert.IsNotEmpty(hearingInstrumentInfoPage.GetDeviceNameTitle(), "Device name title is empty");
            ReportHelper.LogTest(Status.Info, "Device name title is not empty");
            ReportHelper.LogTest(Status.Info, "Checking device name...");
            Assert.IsNotEmpty(hearingInstrumentInfoPage.GetDeviceName(), "Device name is empty");
            ReportHelper.LogTest(Status.Info, "Device name is not empty");
            ReportHelper.LogTest(Status.Info, "Checking device serial title...");
            Assert.IsNotEmpty(hearingInstrumentInfoPage.GetDeviceSerialTitle(), "Device serial title is empty");
            ReportHelper.LogTest(Status.Info, "Device serial title is not empty");
            ReportHelper.LogTest(Status.Info, "Checking device serial...");
            Assert.IsNotEmpty(hearingInstrumentInfoPage.GetDeviceSerial(), "Device serial is empty");
            ReportHelper.LogTest(Status.Info, "Device serial is not empty");
            ReportHelper.LogTest(Status.Info, "Checking instrument control text...");
            Assert.IsNotEmpty(hearingInstrumentInfoPage.GetInstrumentControlText(), "Instrument control text is empty");
            ReportHelper.LogTest(Status.Info, "Instrument control text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking close button text...");
            Assert.IsNotEmpty(hearingInstrumentInfoPage.GetCloseButtonText(), "Close button text is empty");
            ReportHelper.LogTest(Status.Info, "Close button text is not empty");
            ReportHelper.LogTest(Status.Info, "Hearing Instrument Info Control Page UI is verified");
            return hearingInstrumentInfoPage;
        }
    }
}
