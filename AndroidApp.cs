using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using HorusUITest.Helper;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Interactions;
using HorusUITest.Data;
using HorusUITest.Enums;

namespace HorusUITest
{
    public class AndroidApp : BaseApp
    {
        public readonly string deviceSerial;
        private readonly AndroidDriver<AppiumWebElement> driver;
        public override AppiumDriver<AppiumWebElement> Driver => driver;

        //private bool isAppInBackground = false;

        public AndroidApp(AndroidDriver<AppiumWebElement> driver, string deviceSerial)
        {
            this.driver = driver;
            this.deviceSerial = deviceSerial;
            Initialize();
        }

        public override void Tap(int x, int y, int numberOfTaps = 1)
        {
            PointerInputDevice finger = new PointerInputDevice(PointerKind.Touch);
            ActionSequence sequence = new ActionSequence(finger);
            sequence.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, x, y, TimeSpan.Zero));
            for (int i = 0; i < numberOfTaps; i++)
            {
                sequence.AddAction(finger.CreatePointerDown(MouseButton.Left));
                sequence.AddAction(finger.CreatePause(TimeSpan.FromMilliseconds(50)));
                sequence.AddAction(finger.CreatePointerUp(MouseButton.Left));
                if (i < numberOfTaps - 1)
                    sequence.AddAction(finger.CreatePause(TimeSpan.FromMilliseconds(50)));
            }
            Driver.PerformActions(new List<ActionSequence>() { sequence });
        }

        public override void Swipe(int startX, int startY, int endX, int endY, bool useInertia = true, TimeSpan? timeToSwipe = null)
        {
            //Forcing the coordinates to be within the screen width and height.
            //startX.Clamp(1, ScreenSize.Width - 1);        //Swiping to the exact border of the screen somehow gives an error ("outside of rect"), therefore 1 pixel buffer zone.
            //startY.Clamp(1, ScreenSize.Height - 1);
            //endX.Clamp(1, ScreenSize.Width - 1);
            //endY.Clamp(1, ScreenSize.Height - 1);

            timeToSwipe = timeToSwipe ?? TimeSpan.FromMilliseconds(50);
            PointerInputDevice finger = new PointerInputDevice(PointerKind.Touch);
            ActionSequence sequence = new ActionSequence(finger);

            sequence.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, startX, startY, TimeSpan.Zero));
            sequence.AddAction(finger.CreatePointerDown(MouseButton.Left));
            sequence.AddAction(finger.CreatePause(TimeSpan.FromMilliseconds(100)));
            sequence.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, endX, endY, timeToSwipe.Value));
            sequence.AddAction(finger.CreatePointerUp(MouseButton.Left));
            if (!useInertia) sequence.AddAction(finger.CreatePause(TimeSpan.FromMilliseconds(100)));
            if (!useInertia) sequence.AddAction(finger.CreatePointerCancel());

            Driver.PerformActions(new List<ActionSequence>() { sequence });
        }

        public override void LockDevice(int seconds)
        {
            //driver.Lock();
            PressPowerButton();
            Thread.Sleep(seconds * 1000);
            //driver.Unlock(); //this dosent work hence changed the implementation
            PressPowerButton();
            PressMenuButton();
        }

        public override void PressMenuButton()
        {
            driver.PressKeyCode(AndroidKeyCode.Keycode_MENU);
        }

        public override void PressHomeButton()
        {
            driver.PressKeyCode(AndroidKeyCode.Keycode_HOME);
        }

        public override void PressBackButton()
        {
            driver.PressKeyCode(AndroidKeyCode.Keycode_BACK);
        }

        public void PressPowerButton()
        {
            driver.PressKeyCode(AndroidKeyCode.Keycode_POWER);
        }

        public override void PressEnter()
        {
            driver.PressKeyCode(AndroidKeyCode.Enter);
        }

        public override void PressSpace()
        {
            driver.PressKeyCode(AndroidKeyCode.Space);
        }

        public override void PressBackspace()
        {
            driver.PressKeyCode(AndroidKeyCode.BackSpace);
        }

        public override bool GetToggleSwitchState(IMobileElement<AppiumWebElement> toggleSwitch)
        {
            if (bool.TryParse(toggleSwitch.GetAttribute("Checked"), out var result))
                return result;
            throw new InvalidOperationException("Unable to get attribute 'Checked' of given element.");
        }

        public override AppiumWebElement FindElementByXPath(string xPath, AppiumWebElement root = null, bool verifyVisibility = false)
        {
            string visibilityCheck = verifyVisibility ? " Verifying visibility." : "";
            Debug.WriteLine($"Searching for '{xPath}' with root '{root}'.{visibilityCheck}");
            if (root != null)
                return root.FindElementByXPath(xPath);
            else
                return Driver.FindElementByXPath(xPath);
        }

        public override AppiumWebElement FindElementByAutomationId(string id, AppiumWebElement root = null, bool verifyVisibility = false)
        {
            string visibilityCheck = verifyVisibility ? " Verifying visibility." : "";
            Debug.WriteLine($"Searching for '{id}' with root '{root}'.{visibilityCheck}");
            if (root != null)
                return root.FindElementByAccessibilityId(id);
            else
                return Driver.FindElementByAccessibilityId(id);
        }

        public override IReadOnlyCollection<AppiumWebElement> FindElementsByAutomationId(string id, AppiumWebElement root = null, bool verifyVisibility = false)
        {
            string visibilityCheck = verifyVisibility ? " Verifying visibility." : "";
            Debug.WriteLine($"Searching for ALL '{id}' with root '{root}'.{visibilityCheck}");
            if (root != null)
                return root.FindElementsByAccessibilityId(id);
            else
                return Driver.FindElementsByAccessibilityId(id);
        }

        public override void OpenQuickSettings()
        {
            SwipeRelativeToScreenSize(.5, .01, .5, .5);
            SwipeRelativeToScreenSize(.5, .01, .5, .5);
        }

        public override void CloseQuickSettings()
        {
            SwipeRelativeToScreenSize(.5, .9, .01, .5);
        }

        public override void OpenSystemSettings()
        {
            // Navigation from the app to the system settings is done by
            // clicking the settings icon in the quick settings menu.
            OpenQuickSettings();
            var el1 = FindElementByAutomationId("Einstellungen öffnen.");
            el1.Click();
        }

        public override void HideKeyboard()
        {
            //Driver.HideKeyboard();
            TapRelativeToScreenSize(0.75, 0.05);
        }

        public override void LaunchApp()
        {
            Driver.LaunchApp();
        }

        public override void CloseApp()
        {
            Driver.CloseApp();
        }

        public override void ResetApp()
        {
            // ToDo: For Android 13 Driver.ResetApp(); throws error since the permission for notification window appears. But still reset of app data happens.
            // The reason for the failiure is permission dialog appears before the app is opened and even though the app is open in the background after few seconds,
            //      it considers that app is not open and throws error. This needs to be handled.
            // Currently when the Driver.ResetApp(); throws error we Allow the notification and the automation keeps working and even though it fails it still resets the app data.
            // This will work as usual for other Android versions.
            try { Driver.ResetApp(); }
            catch { PermissionHelper.AllowPermissionIfRequested(); }
        }

        public override void EnableBluetooth()
        {
            AndroidManager.EnableBluetooth(deviceSerial);
        }

        public override void DisableBluetooth()
        {
            AndroidManager.DisableBluetooth(deviceSerial);
        }

        public override void EnableLocation()
        {
            // TODO: Test, wich works best:
            // There are two possibilities to toggleLocationService on Android
            // First: http://appium.io/docs/en/commands/device/network/toggle-location-services/#toggle-location-services
            // TODO: Check for isEnabled
            //((AndroidDriver<AppiumWebElement>)Driver).ToggleLocationServices();
            // Second: via adb commands
            AndroidManager.EnableLocation(deviceSerial);
        }

        public override void DisableLocation()
        {
            // TODO: Test, wich works best:
            // There are two possibilities to toggleLocationService on Android
            // First: http://appium.io/docs/en/commands/device/network/toggle-location-services/#toggle-location-services
            // TODO: Check for isEnabled
            //((AndroidDriver<AppiumWebElement>)Driver).ToggleLocationServices();
            // Second: via adb commands
            AndroidManager.DisableLocation(deviceSerial);
        }

        public override void EnableWifi()
        {
            AndroidManager.EnableWifi(deviceSerial);
        }

        public override void DisableWifi()
        {
            AndroidManager.DisableWifi(deviceSerial);
        }

        public override bool PutAppToBackground(int appBackgroundTime)
        {
            try
            {
                driver.BackgroundApp(appBackgroundTime);
                return true;
            }
            catch
            {
                Output.Immediately("Failed to set App in Background");
                return false;
            }

        }

        public override bool PutAppToBackground(double appBackgroundTime)
        {
            try
            {
                driver.BackgroundApp(TimeSpan.FromSeconds(appBackgroundTime));
                return true;
            }
            catch
            {
                Output.Immediately("Failed to set App in Background");
                return false;
            }

        }

        public override bool PutAppToBackground()
        {
            try
            {
                driver.BackgroundApp();
                return true;
            }
            catch
            {
                Output.Immediately("Failed to set App in Background");
                return false;
            }

        }

        public override void GetAppInForeground()
        {
            bool isAppInBackground = PutAppToBackground();
            if (isAppInBackground)
                LaunchApp();
            //driver.LaunchApp();
        }

        public override void GrantGPSPermission()
        {

            AndroidManager.GrantGPSPermission(AppConfig.DefaultAndroidPackageName);
        }

        public override void RevokeGPSPermission()
        {

            AndroidManager.RevokeGPSPermission(AppConfig.DefaultAndroidPackageName);
        }
        public override void GrantGPSBackgroundPermission()
        {
            AndroidManager.GrantGPSBackgroundPermission(AppConfig.DefaultAndroidPackageName);
        }
        public override void RevokeGPSBackgroundPermission()
        {
            AndroidManager.RevokeGPSBackgroundPermission(AppConfig.DefaultAndroidPackageName);
        }
        public override void EnableMobileData()
        {
            AndroidManager.EnableMobileData(deviceSerial);
        }

        public override void DisableMobileData()
        {
            AndroidManager.DisableMobileData(deviceSerial);
        }

        public override void ChangeDeviceLanguage(Language_Device language)
        {
            switch (language)
            {
                case Language_Device.German_Germany:
                    AndroidManager.ChangeDeviceLanguage(deviceSerial, "de", "DE");
                    break;
                case Language_Device.German_Switzerland:
                    AndroidManager.ChangeDeviceLanguage(deviceSerial, "de", "CH");
                    break;
                case Language_Device.English_UK:
                    AndroidManager.ChangeDeviceLanguage(deviceSerial, "en", "GB");
                    break;
                case Language_Device.English_US:
                    AndroidManager.ChangeDeviceLanguage(deviceSerial, "en", "US");
                    break;
                case Language_Device.Italian_Italy:
                    AndroidManager.ChangeDeviceLanguage(deviceSerial, "it", "IT");
                    break;
                case Language_Device.French_France:
                    AndroidManager.ChangeDeviceLanguage(deviceSerial, "fr", "FR");
                    break;
                case Language_Device.Polish_Poland:
                    AndroidManager.ChangeDeviceLanguage(deviceSerial, "pl", "PL");
                    break;
                case Language_Device.Dutch_Netherlands:
                    AndroidManager.ChangeDeviceLanguage(deviceSerial, "nl", "NL");
                    break;
                case Language_Device.Spanish_Spain:
                    AndroidManager.ChangeDeviceLanguage(deviceSerial, "es", "ES");
                    break;
                case Language_Device.Spanish_Mexico:
                    AndroidManager.ChangeDeviceLanguage(deviceSerial, "es", "MX");
                    break;
                case Language_Device.Portuguese_Portugal:
                    AndroidManager.ChangeDeviceLanguage(deviceSerial, "pt", "PT");
                    break;
                case Language_Device.Czech_Czech:
                    AndroidManager.ChangeDeviceLanguage(deviceSerial, "cs", "CZ");
                    break;
                case Language_Device.Turkish_Turkish:
                    AndroidManager.ChangeDeviceLanguage(deviceSerial, "tr", "TR");
                    break;
                case Language_Device.Russian_Russia:
                    AndroidManager.ChangeDeviceLanguage(deviceSerial, "ru", "RU");
                    break;
                case Language_Device.Hindi_India:
                    AndroidManager.ChangeDeviceLanguage(deviceSerial, "hi", "IN");
                    break;
                default: throw new NotImplementedException($"Unknown language '{language}'.");
            }
        }

        public override void ChangeDeviceDate(DateTime dateValue)
        {
            AndroidManager.ChangeDeviceDate(deviceSerial, dateValue);
        }

        public override string GetDeviceOSVersion()
        {
            return AndroidManager.GetAndroidVersion();
        }
    }
}
