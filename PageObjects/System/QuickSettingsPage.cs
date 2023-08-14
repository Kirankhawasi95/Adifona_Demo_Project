using System;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu.Info
{
    [Obsolete("This page is not intended to be used by test cases.")]
    public class QuickSettingsPage : BasePage
    {
        private AppiumWebElement AndroidBluetoothButton => throw new NotImplementedException();
        private AppiumWebElement AndroidWifiButton => throw new NotImplementedException();

        private AppiumWebElement IosBluetoothButton => App.FindElementByAutomationId("bluetooth-button");
        private AppiumWebElement IosWifiButton => App.FindElementByAutomationId("wifi-button");
        private AppiumWebElement IosMobileDataButton => App.FindElementByAutomationId("cellular-data-button");
        private AppiumWebElement BluetoothButton => OnAndroid ? AndroidBluetoothButton : IosBluetoothButton;
        private AppiumWebElement WifiButton => OnAndroid ? AndroidWifiButton : IosWifiButton;

        public QuickSettingsPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public QuickSettingsPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = () => BluetoothButton,
            iOS = () => BluetoothButton
        };

        public bool GetIsBluetoothEnabled()
        {
            return App.GetToggleSwitchState(BluetoothButton);
        }

        public QuickSettingsPage ToggleBluetooth()
        {
            App.Tap(BluetoothButton);
            return this;
        }

        public QuickSettingsPage TurnOnBluetooth()
        {
            if (!GetIsBluetoothEnabled())
            {
                ToggleBluetooth();
            }
            return this;
        }

        public QuickSettingsPage TurnOffBluetooth()
        {
            if (GetIsBluetoothEnabled())
            {
                ToggleBluetooth();
            }
            return this;
        }

        public bool GetIsWifiEnabled()
        {
            return App.GetToggleSwitchState(WifiButton);
        }

        public QuickSettingsPage ToggleWifi()
        {
            App.Tap(WifiButton);
            return this;
        }

        public QuickSettingsPage TurnOnWifi()
        {
            if (!GetIsWifiEnabled())
            {
                ToggleWifi();
            }
            return this;
        }

        public QuickSettingsPage TurnOffWifi()
        {
            if (GetIsWifiEnabled())
            {
                ToggleWifi();
            }
            return this;
        }
    }
}
