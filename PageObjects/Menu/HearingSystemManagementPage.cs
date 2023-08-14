using System;
using AventStack.ExtentReports;
using HorusUITest.Enums;
using HorusUITest.Helper;
using HorusUITest.PageObjects.Dialogs;
using HorusUITest.PageObjects.Start;
using NUnit.Framework;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu
{
    public class HearingSystemManagementPage : BaseNavigationPage
    {
        public HearingSystemManagementPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public HearingSystemManagementPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.NavigationBar");

        private AppiumWebElement LeftDeviceTab => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.LeftDeviceTab");
        private AppiumWebElement LeftDeviceTabSelected => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.LeftDeviceTabSelected");
        private AppiumWebElement LeftIcon => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.LeftIcon");
        private AppiumWebElement LeftName => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.LeftName");
        private AppiumWebElement LeftType => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.LeftType");
        private AppiumWebElement LeftSerial => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.LeftSerial");
        private AppiumWebElement LeftState => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.LeftState");
        private AppiumWebElement LeftFirmware => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.LeftFirmware");

        private AppiumWebElement LeftNameTitle => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.LeftNameTitle");
        private AppiumWebElement LeftTypeTitle => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.LeftTypeTitle");
        private AppiumWebElement LeftSerialTitle => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.LeftSerialTitle");
        private AppiumWebElement LeftStateTitle => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.LeftStateTitle");

        private AppiumWebElement LeftUdi => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.LeftUdi");
        private AppiumWebElement LeftUdiTitle => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.LeftUdiTitle");

        private AppiumWebElement RightDeviceTab => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.RightDeviceTab");
        private AppiumWebElement RightDeviceTabSelected => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.RightDeviceTabSelected");
        private AppiumWebElement RightIcon => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.RightIcon");
        private AppiumWebElement RightName => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.RightName");
        private AppiumWebElement RightType => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.RightType");
        private AppiumWebElement RightSerial => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.RightSerial");
        private AppiumWebElement RightState => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.RightState");
        private AppiumWebElement RightFirmware => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.RightFirmware");

        private AppiumWebElement RightNameTitle => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.RightNameTitle");
        private AppiumWebElement RightTypeTitle => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.RightTypeTitle");
        private AppiumWebElement RightSerialTitle => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.RightSerialTitle");
        private AppiumWebElement RightStateTitle => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.RightStateTitle");

        private AppiumWebElement RightUdi => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.RightUdi");
        private AppiumWebElement RightUdiTitle => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.RightUdiTitle");

        private AppiumWebElement DisconnectDevicesButton => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.DisconnectDevicesButton");

        private AppiumWebElement DisconnectingMessage => App.FindElementByAutomationId("Horus.Views.Menu.HearingSystemManagementPage.DisconnectCircularActivityMessage");

        public bool GetIsLeftDeviceVisible()
        {
            return TryInvokeQuery(() => LeftIcon, out _);
        }

        public bool GetIsLeftTabSelected()
        {
            return TryInvokeQuery(() => LeftDeviceTabSelected, out _);
        }

        public void LeftDeviceTabClick()
        {
            App.Tap(LeftDeviceTab);
        }

        public string GetLeftTabText()
        {
            return LeftDeviceTab.Text;
        }

        public string GetLeftDeviceIconText()
        {
            return LeftIcon.Text;
        }

        public string GetLeftDeviceName()
        {
            return LeftName.Text;
        }

        public string GetLeftDeviceType()
        {
            return LeftType.Text;
        }

        public string GetLeftDeviceSerial()
        {
            return LeftSerial.Text;
        }

        public string GetLeftDeviceState()
        {
            return LeftState.Text;
        }

        public string GetLeftDeviceFirmware()
        {
            return LeftFirmware.Text;
        }

        public string GetLeftDeviceNameTitle()
        {
            return LeftNameTitle.Text;
        }

        public string GetLeftDeviceTypeTitle()
        {
            return LeftTypeTitle.Text;
        }

        public string GetLeftDeviceSerialTitle()
        {
            return LeftSerialTitle.Text;
        }

        public string GetLeftDeviceStateTitle()
        {
            return LeftStateTitle.Text;
        }

        public bool GetIsLeftUdiVisible()
        {
            return TryInvokeQuery(() => LeftUdiTitle, out _);
        }

        public string GetLeftDeviceUdi()
        {
            return LeftUdi.Text;
        }

        public string GetLeftDeviceUdiTitle()
        {
            return LeftUdiTitle.Text;
        }

        public bool GetIsRightDeviceVisible()
        {
            return TryInvokeQuery(() => RightIcon, out _);
        }

        public bool GetIsRightTabSelected()
        {
            return TryInvokeQuery(() => RightDeviceTabSelected, out _);
        }

        public void RightDeviceTabClick()
        {
            App.Tap(RightDeviceTab);
        }

        public string GetRightTabText()
        {
            return RightDeviceTab.Text;
        }

        public string GetRightDeviceIconText()
        {
            return RightIcon.Text;
        }

        public string GetRightDeviceName()
        {
            return RightName.Text;
        }

        public string GetRightDeviceType()
        {
            return RightType.Text;
        }

        public string GetRightDeviceSerial()
        {
            return RightSerial.Text;
        }

        public string GetRightDeviceState()
        {
            return RightState.Text;
        }

        public string GetRightDeviceFirmware()
        {
            return RightFirmware.Text;
        }

        public string GetRightDeviceNameTitle()
        {
            return RightNameTitle.Text;
        }

        public string GetRightDeviceTypeTitle()
        {
            return RightTypeTitle.Text;
        }

        public string GetRightDeviceSerialTitle()
        {
            return RightSerialTitle.Text;
        }

        public string GetRightDeviceStateTitle()
        {
            return RightStateTitle.Text;
        }

        public bool GetIsRightUdiVisible()
        {
            return TryInvokeQuery(() => RightUdiTitle, out _);
        }

        public string GetRightDeviceUdi()
        {
            return RightUdi.Text;
        }

        public string GetRightDeviceUdiTitle()
        {
            return RightUdiTitle.Text;
        }

        /// <summary>
        /// Displays an <see cref="AppDialog"/> and, if confirmed, navigates to <see cref="InitializeHardwarePage"/>.
        /// </summary>
        public void DisconnectDevices()
        {
            App.Tap(DisconnectDevicesButton);
        }

        public string GetDisconnectDevicesTitle()
        {
            return DisconnectDevicesButton.Text;
        }

        public bool VerifyDisconnectingText(string DiconnectingMessage, TimeSpan? timeout = null)
        {
            timeout = timeout ?? TimeSpan.FromSeconds(35);
            return Wait.For((() => ContainsDisconnectingText(DiconnectingMessage)), timeout.Value);
        }

        private bool ContainsDisconnectingText(string DiconnectingMessage)
        {
            return DisconnectingMessage.Text.Contains(DiconnectingMessage);
        }

        public HearingSystemManagementPage CheckHAInformationFromSettings(AppMode mode, Side left = 0, Side right = 0)
        {
            var hearingSystemsPage = new HearingSystemManagementPage();

            switch (mode)
            {
                case AppMode.Demo:
                    CheckLeftDeviceInfo(hearingSystemsPage, mode);
                    CheckRightDeviceInfo(hearingSystemsPage, mode);
                    break;
                case AppMode.Normal:
                    if (left != 0)
                        CheckLeftDeviceInfo(hearingSystemsPage, mode);
                    if (right != 0)
                        CheckRightDeviceInfo(hearingSystemsPage, mode);
                    break;
                default:
                    break;
            }

            return hearingSystemsPage;
        }

        private void CheckLeftDeviceInfo(HearingSystemManagementPage hearingSystemsPage, AppMode mode)
        {
            ReportHelper.LogTest(Status.Info, "Tapping left device information tab...");
            hearingSystemsPage.LeftDeviceTabClick();
            ReportHelper.LogTest(Status.Info, "Tapped left device information tab");
            ReportHelper.LogTest(Status.Info, "Checking left device information...");
            ReportHelper.LogTest(Status.Info, "Checking if Left tab is selected...");
            Assert.IsTrue(hearingSystemsPage.GetIsLeftTabSelected(), "Left tab is not selected");
            ReportHelper.LogTest(Status.Info, "Left tab is selected");
            ReportHelper.LogTest(Status.Info, "Checking left tab text...");
            Assert.IsNotEmpty(hearingSystemsPage.GetLeftTabText(), "Left tab text is empty");
            ReportHelper.LogTest(Status.Info, "Left tab text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left tab device name title text...");
            Assert.IsNotEmpty(hearingSystemsPage.GetLeftDeviceNameTitle(), "Left tab device name title text is empty");
            ReportHelper.LogTest(Status.Info, "Left tab device name title text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left tab device name text...");
            Assert.IsNotEmpty(hearingSystemsPage.GetLeftDeviceName(), "Left tab device name text is empty");
            ReportHelper.LogTest(Status.Info, "Left tab device name text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left tab device serial title text...");
            Assert.IsNotEmpty(hearingSystemsPage.GetLeftDeviceSerialTitle(), "Left tab device serial title text is empty");
            ReportHelper.LogTest(Status.Info, "Left tab device serial title text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left tab device serial text...");
            Assert.IsNotEmpty(hearingSystemsPage.GetLeftDeviceSerial(), "Left tab device serial text is empty");
            ReportHelper.LogTest(Status.Info, "Left tab device serial text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left tab device state title text...");
            Assert.IsNotEmpty(hearingSystemsPage.GetLeftDeviceStateTitle(), "Left tab device state title text is empty");
            ReportHelper.LogTest(Status.Info, "Left tab device state title text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left tab device state text...");
            Assert.IsNotEmpty(hearingSystemsPage.GetLeftDeviceState(), "Left tab device state text is empty");
            ReportHelper.LogTest(Status.Info, "Left tab device state text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left tab device type title text...");
            Assert.IsNotEmpty(hearingSystemsPage.GetLeftDeviceTypeTitle(), "Left tab device type title text is empty");
            ReportHelper.LogTest(Status.Info, "Left tab device type title text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking left tab device type text...");
            Assert.IsNotEmpty(hearingSystemsPage.GetLeftDeviceType(), "Left tab device type text is empty");
            ReportHelper.LogTest(Status.Info, "Left tab device type text is not empty");
            if (mode.Equals(AppMode.Normal))
            {
                ReportHelper.LogTest(Status.Info, "Checking if left tab UDI is visible in normal mode...");
                Assert.True(hearingSystemsPage.GetIsLeftUdiVisible(), "Left tab UDI is not visible in normal mode");
                ReportHelper.LogTest(Status.Info, "Left tab UDI is visible in normal mode");
                Assert.IsNotEmpty(hearingSystemsPage.GetLeftDeviceUdi());
                Assert.IsNotEmpty(hearingSystemsPage.GetLeftDeviceUdiTitle());
            }
            else
            {
                ReportHelper.LogTest(Status.Info, "Checking if left tab UDI is not visible in demo mode...");
                Assert.IsFalse(hearingSystemsPage.GetIsLeftUdiVisible(), "Left tab UDI is visible in demo mode");
                ReportHelper.LogTest(Status.Info, "Left tab UDI is not visible in demo mode");
            }
            ReportHelper.LogTest(Status.Pass, "Left device information is verified");
        }

        private void CheckRightDeviceInfo(HearingSystemManagementPage hearingSystemsPage, AppMode mode)
        {
            ReportHelper.LogTest(Status.Info, "Tapping right device information tab...");
            hearingSystemsPage.RightDeviceTabClick();
            ReportHelper.LogTest(Status.Info, "Tapped right device information tab");
            ReportHelper.LogTest(Status.Info, "Checking right device information...");
            ReportHelper.LogTest(Status.Info, "Checking if Right tab is selected...");
            Assert.IsTrue(hearingSystemsPage.GetIsRightTabSelected(), "Right tab is not selected");
            ReportHelper.LogTest(Status.Info, "Right tab is selected");
            ReportHelper.LogTest(Status.Info, "Checking right tab text...");
            Assert.IsNotEmpty(hearingSystemsPage.GetRightTabText(), "Right tab text is empty");
            ReportHelper.LogTest(Status.Info, "Right tab text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right tab device name title text...");
            Assert.IsNotEmpty(hearingSystemsPage.GetRightDeviceNameTitle(), "Right tab device name title text is empty");
            ReportHelper.LogTest(Status.Info, "Right tab device name title text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right tab device name text...");
            Assert.IsNotEmpty(hearingSystemsPage.GetRightDeviceName(), "Right tab device name text is empty");
            ReportHelper.LogTest(Status.Info, "Right tab device name text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right tab device serial title text...");
            Assert.IsNotEmpty(hearingSystemsPage.GetRightDeviceSerialTitle(), "Right tab device serial title text is empty");
            ReportHelper.LogTest(Status.Info, "Right tab device serial title text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right tab device serial text...");
            Assert.IsNotEmpty(hearingSystemsPage.GetRightDeviceSerial(), "Right tab device serial text is empty");
            ReportHelper.LogTest(Status.Info, "Right tab device serial text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right tab device state title text...");
            Assert.IsNotEmpty(hearingSystemsPage.GetRightDeviceStateTitle(), "Right tab device state title text is empty");
            ReportHelper.LogTest(Status.Info, "Right tab device state title text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right tab device state text...");
            Assert.IsNotEmpty(hearingSystemsPage.GetRightDeviceState(), "Right tab device state text is empty");
            ReportHelper.LogTest(Status.Info, "Right tab device state text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right tab device type title text...");
            Assert.IsNotEmpty(hearingSystemsPage.GetRightDeviceTypeTitle(), "Right tab device type title text is empty");
            ReportHelper.LogTest(Status.Info, "Right tab device type title text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking right tab device type text...");
            Assert.IsNotEmpty(hearingSystemsPage.GetRightDeviceType(), "Right tab device type text is empty");
            ReportHelper.LogTest(Status.Info, "Right tab device type text is not empty");
            if (mode.Equals(AppMode.Normal))
            {
                ReportHelper.LogTest(Status.Info, "Checking if right tab UDI is visible in normal mode...");
                Assert.True(hearingSystemsPage.GetIsRightUdiVisible(), "Right tab UDI is not visible in normal mode");
                ReportHelper.LogTest(Status.Info, "Right tab UDI is visible in normal mode");
                Assert.IsNotEmpty(hearingSystemsPage.GetRightDeviceUdi());
                Assert.IsNotEmpty(hearingSystemsPage.GetRightDeviceUdiTitle());
            }
            else
            {
                ReportHelper.LogTest(Status.Info, "Checking if right tab UDI is not visible in demo mode...");
                Assert.IsFalse(hearingSystemsPage.GetIsRightUdiVisible(), "Right tab UDI is visible in demo mode");
                ReportHelper.LogTest(Status.Info, "Right tab UDI is not visible in demo mode");
            }
            ReportHelper.LogTest(Status.Pass, "Right device information is verified");
        }
    }
}
