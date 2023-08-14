using System;
using System.Collections.Generic;
using System.Diagnostics;
using HorusUITest.Enums;
using HorusUITest.Helper;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Menu.Info;
using HorusUITest.PageObjects.Start.Intro;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Interactions;

namespace HorusUITest
{
    public class IosApp : BaseApp
    {
        private readonly IOSDriver<AppiumWebElement> driver;
        public override AppiumDriver<AppiumWebElement> Driver => driver;

        public IosApp(IOSDriver<AppiumWebElement> driver)
        {
            this.driver = driver;
            Initialize();
        }

        public override void LockDevice(int seconds)
        {
            driver.Lock(seconds);
        }

        public override void Tap(int x, int y, int numberOfTaps = 1)
        {
            var action = new TouchAction(driver);
            if (numberOfTaps == 6)
                TapMultipleTimes(x, y);
            else
            {
                for (int i = 0; i < numberOfTaps; i++)
                {
                    action.Press(x, y);
                    action.Wait(50);
                    action.Release();
                    if (i < numberOfTaps - 1)
                        action.Wait(50);
                }
                action.Perform();
            }
        }

        /// <summary>
        /// This method is only used to tap six times to show hidden menu
        /// Some abnormal behaviour is seen when we tap six times
        /// Works fine with three hence temporarily using this implementation
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void TapMultipleTimes(int x, int y)
        {
            var action = new TouchAction(driver);
            Output.Immediately("Tap Multiple times called");
            for (int i = 0; i < 3; i++)
            {
                action.Tap(x, y).Wait(50).Perform();
                Output.Immediately(i.ToString());

            }
        }

        public override void PressMenuButton()
        {
            throw new NotImplementedException("Pressing menu button is not available on iOS.");
        }

        public override void PressHomeButton()
        {
            throw new NotImplementedException("Pressing home button is not implemented yet on iOS.");
        }

        public override void PressBackButton()
        {
            
            new NavigationBar().NavigateBack();
            //AppManager.App.Driver.Navigate().Back(); // This is not working
            //throw new NotImplementedException("Pressing back button is not available on iOS.");
        }

        public override void PressEnter()
        {
            //TODO: Implement iOS PressEnter.
            throw new NotImplementedException("iOS key press not yet implemented.");
        }

        public override void PressSpace()
        {
            //TODO: Implement iOS PressSpace.
            throw new NotImplementedException();
        }

        public override void PressBackspace()
        {
            //TODO: Implement iOS PressBackspace.
            throw new NotImplementedException();
        }

        public override void OpenQuickSettings()
        {
            //SwipeRelativeToScreenSize(.5, 1, .5, .5);
            SwipeRelativeToScreenSize(.8, 0, .8, .5);
        }

        public override void CloseQuickSettings()
        {
            //SwipeRelativeToScreenSize(.5, 0, .5, .5);
            SwipeRelativeToScreenSize(.8, 1, .8, .5);
        }

        public override void OpenSystemSettings()
        {
            Driver.ActivateApp("com.apple.Preferences");
        }

        public override void HideKeyboard()
        {
            TapRelativeToScreenSize(0.5, 0.5);
        }

        public override bool GetToggleSwitchState(IMobileElement<AppiumWebElement> toggleSwitch)
        {
            string s = toggleSwitch.GetAttribute("value");
            if (s == "0") return false;
            if (s == "1") return true;
            throw new InvalidOperationException("Unable to get attribute 'Checked' of given element.");
        }

        public override AppiumWebElement FindElementByAutomationId(string id, AppiumWebElement root = null, bool verifyVisibility = false)
        {
            string visibilityCheck = verifyVisibility ? " Verifying visibility." : "";
            Debug.WriteLine($"Searching for '{id}' with root '{root}'.{visibilityCheck}");
            if (root != null)
            {
                if (verifyVisibility)
                    return root.FindElement(MobileBy.IosNSPredicate("name == '" + id + "' AND visible == 1"));
                else
                    return root.FindElementByAccessibilityId(id);
            }
            else
            {
                if (verifyVisibility)
                    return driver.FindElementByIosNsPredicate("name == '" + id + "' AND visible == 1");
                else
                    return driver.FindElementByAccessibilityId(id);
            }
        }

        public override IReadOnlyCollection<AppiumWebElement> FindElementsByAutomationId(string id, AppiumWebElement root = null, bool verifyVisibility = false)
        {
            string visibilityCheck = verifyVisibility ? " Verifying visibility." : "";
            Debug.WriteLine($"Searching for ALL '{id}' with root '{root}'.{visibilityCheck}");
            if (root != null)
            {
                if (verifyVisibility)
                    return root.FindElements(MobileBy.IosNSPredicate("name == '" + id + "' AND visible == 1"));
                else
                    return root.FindElementsByAccessibilityId(id);
            }
            else
            {
                if (verifyVisibility)
                    return driver.FindElementsByIosNsPredicate("name == '" + id + "' AND visible == 1");
                else
                    return driver.FindElementsByAccessibilityId(id);
            }
        }

        public override AppiumWebElement FindElementByXPath(string xPath, AppiumWebElement root = null, bool verifyVisibility = false)
        {
            string visibilityCheck = verifyVisibility ? " Verifying visibility." : "";
            Debug.WriteLine($"Searching for '{xPath}' with root '{root}'.{visibilityCheck}");
            if (root != null)
            {
                if (verifyVisibility)
                    return root.FindElement(MobileBy.IosNSPredicate("name == '" + xPath + "' AND visible == 1"));
                else
                    return root.FindElementByXPath(xPath);
            }
            else
            {
                if (verifyVisibility)
                    return driver.FindElementByIosNsPredicate("name == '" + xPath + "' AND visible == 1");
                else
                    return driver.FindElementByXPath(xPath);
            }
        }

        public override void Swipe(int startX, int startY, int endX, int endY, bool useInertia = true, TimeSpan? timeToSwipe = null)
        {
            int stepCount = 10;

            if (timeToSwipe == null)
            {
                timeToSwipe = TimeSpan.FromMilliseconds(50);
                stepCount = 1;
            }

            PointerInputDevice finger = new PointerInputDevice(PointerKind.Touch);
            ActionSequence sequence = new ActionSequence(finger);

            sequence.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, startX, startY, TimeSpan.Zero));
            sequence.AddAction(finger.CreatePointerDown(MouseButton.Left));
            sequence.AddAction(finger.CreatePause(TimeSpan.FromMilliseconds(100)));
            TimeSpan stepTime = TimeSpan.FromTicks(timeToSwipe.Value.Ticks / stepCount);
            for (int i = 1; i <= stepCount; i++)
            {
                int x = (int)(startX + (endX - startX) * (i / (double)stepCount));
                int y = (int)(startY + (endY - startY) * (i / (double)stepCount));
                sequence.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, x, y, stepTime));
            }
            sequence.AddAction(finger.CreatePointerUp(MouseButton.Left));
            if (!useInertia) sequence.AddAction(finger.CreatePause(TimeSpan.FromMilliseconds(100)));
            if (!useInertia) sequence.AddAction(finger.CreatePointerCancel());

            Driver.PerformActions(new List<ActionSequence>() { sequence });
        }

        public override void LaunchApp()
        {
            var parameters = new Dictionary<string, string>();
            string bundleId = (string)driver.Capabilities["bundleId"];
            parameters.Add("bundleId", bundleId);
            driver.ExecuteScript("mobile: launchApp", parameters);
        }

        public override void CloseApp()
        {
            var parameters = new Dictionary<string, string>();
            string bundleId = (string)driver.Capabilities["bundleId"];
            parameters.Add("bundleId", bundleId);
            driver.ExecuteScript("mobile: terminateApp", parameters);
        }

        public override void ResetApp()
        {
            //TODO: Resetting app data does not work on iOS. The app has to be uninstalled and installed again.
            //HACK: To get this "kind of" working, we just restart the app for now.
            CloseApp();
            //Driver.ResetApp();
            LaunchApp();

            // Currently doing Reset from Menu inside the App
            LaunchHelper.ResetIosAppFromSetting();
        }

#pragma warning disable CS0618 // Type or member is obsolete
        public override void EnableBluetooth()
        {
            OpenQuickSettings();
            new QuickSettingsPage().TurnOnBluetooth();
            CloseQuickSettings();
        }

        public override void DisableBluetooth()
        {
            OpenQuickSettings();
            new QuickSettingsPage().TurnOffBluetooth();
            CloseQuickSettings();
        }

        public override void EnableLocation()
        {
            //TODO: OpenSystemSettings();
            throw new NotImplementedException("iOS EnableLocation not yet implemented, because location is not accessible on iOS' Quick Menu.");
        }

        public override void DisableLocation()
        {
            throw new NotImplementedException("iOS DisableLocation not yet implemented, because location is not accessible on iOS' Quick Menu.");
        }

        public override void EnableWifi()
        {
            OpenQuickSettings();
            new QuickSettingsPage().TurnOnWifi();
            CloseQuickSettings();
        }

        public override void DisableWifi()
        {
            OpenQuickSettings();
            new QuickSettingsPage().TurnOffWifi();
            CloseQuickSettings();
        }

        public override bool PutAppToBackground(int appBackgroundTime)
        {
            try
            {
                driver.BackgroundApp(appBackgroundTime);
                return true;
            }
            catch(Exception e)
            {
                Output.Immediately($"Failed to set App in Background due to: {e.Message}");
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
            catch (Exception e)
            {
                Output.Immediately($"Failed to set App in Background due to: {e.Message}");
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
            catch(Exception e)
            {
                Output.Immediately($"Failed to set App in Background due to: {e.Message}");
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
            //throw new NotImplementedException("iOS GrantGPSPermission not yet implemented");
        }

        public override void RevokeGPSPermission()
        {
            throw new NotImplementedException("iOS RevokeGPSPermission not yet implemented");
        }

        public override void EnableMobileData()
        {
            throw new NotImplementedException("iOS EnableMobileData not yet implemented");
        }

        public override void DisableMobileData()
        {
            throw new NotImplementedException("iOS DisableMobileData not yet implemented");
        }

        public override void RevokeGPSBackgroundPermission()
        {
            throw new NotImplementedException("iOS RevokeGPSBackgroundPermission not yet implemented");
        }

        public override void GrantGPSBackgroundPermission()
        {
            //throw new NotImplementedException("iOS GrantGPSBackgroundPermission not yet implemented");
        }

        public override void ChangeDeviceLanguage(Language_Device language)
        {
            throw new NotImplementedException("It is not possible to automate change in OS language in iOS.");
        }

        public override void ChangeDeviceDate(DateTime dateValue)
        {
            throw new NotImplementedException("It is not possible to automate change in OS Date in iOS.");
        }

        public override string GetDeviceOSVersion()
        {
            throw new NotImplementedException("Get mobile version not implemented in iOS");
        }
#pragma warning restore CS0618 // Type or member is obsolete
    }
}
