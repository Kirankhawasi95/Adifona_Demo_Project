using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using HorusUITest.Configuration;
using HorusUITest.Data;
using HorusUITest.Enums;
using HorusUITest.Helper;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Service;
using Platform = HorusUITest.Enums.Platform;

namespace HorusUITest
{
    public static class AppManager
    {
        private static AppiumLocalService appiumLocalService;
        private static AppiumLocalService AppiumLocalService
        {
            get
            {
                appiumLocalService = appiumLocalService ?? CreateAppiumLocalService();
                return appiumLocalService;
            }
        }

        private static BaseApp app;
        public static BaseApp App
        {
            get
            {
                if (app == null)
                    throw new NullReferenceException("'" + nameof(AppManager) + "." + nameof(App) + "' not set. Call '" + nameof(AppManager) + "." + nameof(AppManager.InitializeApp) + "' before trying to access it.");
                return app;
            }
        }

        private static AppConfig AppConfig { get; set; }

        public static DeviceSettings DeviceSettings;

        public static Platform Platform => AppConfig.Platform;
        public static Brand Brand => AppConfig.Brand;
        public static bool InstallApp => AppConfig.InstallApp;
        public static bool DeleteAppData => AppConfig.DeleteAppData;
        public static bool CreateOwnAppiumServer => AppConfig.CreateOwnAppiumServer;
        public static bool UninstallAppAfterTest => AppConfig.UninstallAppAfterTest;
        public static bool UninstallUtilityAppsAfterTest => AppConfig.UninstallUtilityAppsAfterTest;
        public static bool DisableBluetoothAfterTest => AppConfig.DisableBluetoothAfterTest;
        public static AndroidDriver<AppiumWebElement> androidDriver;
        public static BasePhone Phone => AppConfig.Phone;
        public static string AppPackageName => AppConfig.AppPackageName;
        public static string PathToAppPackage => AppConfig.PathToAppPackage;

        public static string XcodeSigningId => AppConfig.XcodeSigningId;
        public static string XcodeOrgId => AppConfig.XcodeOrgId;

        public static bool RunIOSTestCaseFromWindows => AppConfig.RunIOSTestCaseFromWindows;

        public static string RunIOSTestCaseFromWindowsMacIP => AppConfig.RunIOSTestCaseFromWindowsMacIP;

        public static AppManagerLog Log { get; } = new AppManagerLog();

        public static bool IsAppInitialized => app != null;

        public static void DisposeAppium()
        {
            Output.Immediately("Disposing Appium server");
            app?.QuitDriver();
            app = null;
            appiumLocalService?.Dispose();
            appiumLocalService = null;
        }

        private static AppiumLocalService CreateAppiumLocalService()
        {
            //Programmatischer Start eines lokalen Appium-Servers.
            //Hierfür ist es notwendig, node.js und npm sowie einen Appium-Server mit "npm install -g appium" installiert zu haben.
            //Desweiteren muss node.js über PATH erreichbar sein (sollte für gewöhnlich bei der Installation erfolgen).
            //Falls trotz angelegter Environment Variable "ANDROID_HOME" diese nicht gefunden wird, muss ANDROID_HOME programmatisch gesetzt werden:
            //Und zwar hiermit --> Environment.SetEnvironmentVariable("ANDROID_HOME", "/Users/oliverkeidel/Library/Android/sdk");

            Output.Immediately("Creating new Appium server");
            AppiumLocalService result = AppiumLocalService.BuildDefaultService();
            result.Start();
            return result;
        }

        private static AndroidDriver<AppiumWebElement> InitializeAndroidDriver(AppiumOptions options, bool createOwnAppiumServer)
        {
            AndroidDriver<AppiumWebElement> androidDriver;
            if (createOwnAppiumServer)
            {
                AppiumLocalService localAppiumServer = AppiumLocalService;
                Output.Immediately("Connecting to own Appium server");
                androidDriver = new AndroidDriver<AppiumWebElement>(localAppiumServer, options, Env.INIT_TIMEOUT);
            }
            else
            {
                Output.Immediately("Connecting to pre-existing Appium server at http://localhost:4723/wd/hub");
                androidDriver = new AndroidDriver<AppiumWebElement>(new Uri("http://localhost:4723/wd/hub"), options, Env.INIT_TIMEOUT);
            }
            return androidDriver;
        }

        private static IOSDriver<AppiumWebElement> InitializeIosDriver(AppiumOptions options, bool createOwnAppiumServer)
        {

            IOSDriver<AppiumWebElement> iosDriver;

            // By Default RunIOSTestCaseFromWindows will be false so that Audifon users can execute IOS test cases directly from MAC PC
            // RunIOSTestCaseFromWindows has to be set as true only when we have to run IOS test case from windows PC connected to MAC PC in same network.
            // RunIOSTestCaseFromWindowsMacIP variable should contain the IP of the network to which both windows and mac PC are connected.
            if (RunIOSTestCaseFromWindows)
            {
                // Appium from different MAC Laptop with IP obtained from RunIOSTestCaseFromWindowsMacIP variable is connected
                string MACAppiumPort = "4723";

                Output.Immediately("Connecting to pre-existing Appium server at http://" + RunIOSTestCaseFromWindowsMacIP + ":" + MACAppiumPort + "/wd/hub");

                iosDriver = new IOSDriver<AppiumWebElement>(new Uri("http://" + RunIOSTestCaseFromWindowsMacIP + ":" + MACAppiumPort + "/wd/hub"), options, Env.INIT_TIMEOUT);
            }
            else
            {
                if (createOwnAppiumServer)
                {
                    AppiumLocalService localAppiumServer = AppiumLocalService;
                    Output.Immediately("Connecting to own Appium server");
                    iosDriver = new IOSDriver<AppiumWebElement>(localAppiumServer, options, Env.INIT_TIMEOUT);
                }
                else
                {
                    Output.Immediately("Connecting to pre-existing Appium server at http://localhost:4723/wd/hub");
                    iosDriver = new IOSDriver<AppiumWebElement>(new Uri("http://localhost:4723/wd/hub"), options, Env.INIT_TIMEOUT);
                }
            }
            return iosDriver;
        }

        public static void InstallAppFromStore(AppConfig config)
        {
            AppConfig = config;

            string AppName = "audifon";

            switch (Platform)
            {
                case Platform.Android:
                    {
                        Output.Immediately($"Initializing Install from Play Store");

                        // Settng Desired capabilites
                        AppiumOptions options = new AppiumOptions();
                        options.AddAdditionalCapability(MobileCapabilityType.NewCommandTimeout, 3600);
                        options.AddAdditionalCapability(MobileCapabilityType.PlatformName, Platform.ToString());
                        options.AddAdditionalCapability(MobileCapabilityType.DeviceName, Phone.DeviceName);
                        options.AddAdditionalCapability(MobileCapabilityType.Udid, Phone.Udid);
                        options.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, Phone.PlatformVersion);
                        options.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "com.android.vending");
                        options.AddAdditionalCapability(MobileCapabilityType.AutomationName, "UIAutomator2");
                        options.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, "com.google.android.finsky.activities.MainActivity");
                        options.AddAdditionalCapability("autoLaunch", false);

                        // Uninstall APK if already installed
                        AndroidManager.UninstallApk(Phone.Udid, AppPackageName);
                        Output.Immediately(AppName + " app uninstalled");

                        // Unlock the phone
                        AndroidManager.UnlockDevice(Phone.Udid);
                        Output.Immediately($"Phone Unlocked");

                        // Initalize driver
                        AndroidDriver<AppiumWebElement> androidDriverPlayStore = InitializeAndroidDriver(options, CreateOwnAppiumServer);

                        // Giving ImplicitWait as 5 secs
                        androidDriverPlayStore.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                        // Enable Wifi
                        AndroidManager.EnableWifi(Phone.Udid);
                        Thread.Sleep(5000);

                        // Change language of the mobile to english since the object which we get are from english texts
                        AndroidManager.ChangeDeviceLanguage(Phone.Udid, "en", "GB");
                        Thread.Sleep(5000);

                        // Launch App
                        androidDriverPlayStore.LaunchApp();
                        Thread.Sleep(5000);
                        Output.Immediately($"Opened Play Store");

                        // Click in Search text box to type the app name
                        androidDriverPlayStore.FindElement(MobileBy.XPath("//android.widget.TextView[@text='Search for apps & games']")).Click();

                        // Type App Name and click Search
                        androidDriverPlayStore.FindElementByClassName("android.widget.EditText").SendKeys(AppName);
                        androidDriverPlayStore.PressKeyCode(66);

                        // Clicking the Audifon app from search list
                        androidDriverPlayStore.FindElement(MobileBy.XPath("//android.view.View[contains(@content-desc, '" + AppName + "')]")).Click();

                        // Clicking Install Button
                        androidDriverPlayStore.FindElement(MobileBy.XPath("//android.widget.Button[@text='Install']")).Click();

                        // Waiting till the Open button is enabled after installation
                        Wait.UntilTrue(() => androidDriverPlayStore.FindElement(MobileBy.XPath("//android.widget.Button[@text='Open']")).Enabled, TimeSpan.FromMinutes(5));

                        // Close Play Store and Release Driver
                        androidDriverPlayStore.CloseApp();
                        androidDriverPlayStore.Quit();

                        Output.Immediately($"Closed Play Store");

                        // Disable Wifi
                        AndroidManager.DisableWifi(Phone.Udid);
                        Thread.Sleep(5000);

                        Output.Immediately(AppName + " successfully installed from Play Store");

                        break;
                    }
                case Platform.iOS:
                    {
                        Output.Immediately($"Initializing Install from Test Flight");

                        // Settng Desired capabilites
                        AppiumOptions options = new AppiumOptions();
                        options.AddAdditionalCapability(MobileCapabilityType.NewCommandTimeout, 3600);
                        options.AddAdditionalCapability(MobileCapabilityType.PlatformName, Platform.ToString());
                        options.AddAdditionalCapability(MobileCapabilityType.DeviceName, Phone.DeviceName);
                        options.AddAdditionalCapability(MobileCapabilityType.Udid, Phone.Udid);
                        options.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, Phone.PlatformVersion);
                        options.AddAdditionalCapability(IOSMobileCapabilityType.BundleId, "com.apple.TestFlight");
                        options.AddAdditionalCapability(MobileCapabilityType.AutomationName, "XCUITest");
                        options.AddAdditionalCapability("simpleIsVisibleCheck", true);
                        //options.AddAdditionalCapability("autoLaunch", false);
                        options.AddAdditionalCapability("xcodeSigningId", XcodeSigningId);
                        options.AddAdditionalCapability("xcodeOrgId", XcodeOrgId);
                        options.AddAdditionalCapability(IOSMobileCapabilityType.ShowIOSLog, false);
                        options.AddAdditionalCapability("showXcodeLog", false);
                        options.AddAdditionalCapability("waitForQuiescence", false);
                        options.AddAdditionalCapability("useJSONSource", true);

                        // Initalize driver
                        IOSDriver<AppiumWebElement> iOSDriverAppleStore = InitializeIosDriver(options, CreateOwnAppiumServer);

                        // Giving ImplicitWait as 5 secs
                        iOSDriverAppleStore.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                        iOSDriverAppleStore.FindElement(MobileBy.XPath("//XCUIElementTypeOther[contains(@name, '" + AppName + "')]")).Click();
                        Thread.Sleep(2000);

                        // Installing if the App is not Already Installed
                        bool IsAppUpdatedOrInstalled = false;
                        if (iOSDriverAppleStore.FindElements(MobileBy.XPath("//XCUIElementTypeButton[@name='INSTALL']")).Count > 0)
                        {
                            iOSDriverAppleStore.FindElement(MobileBy.XPath("//XCUIElementTypeButton[@name='INSTALL']")).Click();
                            IsAppUpdatedOrInstalled = true;
                        }

                        // Updating if the App is not Already Updated
                        if (iOSDriverAppleStore.FindElements(MobileBy.XPath("//XCUIElementTypeButton[@name='UPDATE']")).Count > 0)
                        {
                            iOSDriverAppleStore.FindElement(MobileBy.XPath("//XCUIElementTypeButton[@name='UPDATE']")).Click();
                            IsAppUpdatedOrInstalled = true;
                        }

                        // Waiting for the App to be installed or updated if available
                        if (IsAppUpdatedOrInstalled)
                        {
                            Wait.UntilTrue(() => iOSDriverAppleStore.FindElements(MobileBy.XPath("//XCUIElementTypeButton[@name='OPEN']")).Count > 0, TimeSpan.FromMinutes(5));

                            // When app in installed or updated additional screen appears. Work arround to close it. In BeforeAll Audifon app will be closed and opened
                            iOSDriverAppleStore.FindElement(MobileBy.XPath("//XCUIElementTypeButton[@name='OPEN']")).Click();
                        }

                        // Close Play Store and Release Driver
                        iOSDriverAppleStore.CloseApp();
                        iOSDriverAppleStore.Quit();

                        Output.Immediately($"Closed Test Flight");

                        Output.Immediately(AppName + " successfully installed from Test Flight");

                        break;
                    }
            }
        }

        public static void UpdateAppFromStore()
        {
            string AppName = "audifon";

            Output.Immediately($"Initializing Install from Play Store");

            // Settng Desired capabilites
            AppiumOptions options = new AppiumOptions();
            options.AddAdditionalCapability(MobileCapabilityType.NewCommandTimeout, 3600);
            options.AddAdditionalCapability(MobileCapabilityType.PlatformName, Platform.ToString());
            options.AddAdditionalCapability(MobileCapabilityType.DeviceName, Phone.DeviceName);
            options.AddAdditionalCapability(MobileCapabilityType.Udid, Phone.Udid);
            options.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, Phone.PlatformVersion);
            options.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "com.android.vending");
            options.AddAdditionalCapability(MobileCapabilityType.AutomationName, "UIAutomator2");
            options.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, "com.google.android.finsky.activities.MainActivity");
            options.AddAdditionalCapability("autoLaunch", false);

            // Initalize driver
            AndroidDriver<AppiumWebElement> androidDriverPlayStore = InitializeAndroidDriver(options, CreateOwnAppiumServer);

            // Giving ImplicitWait as 5 secs
            androidDriverPlayStore.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // Launch App
            androidDriverPlayStore.LaunchApp();
            Thread.Sleep(5000);
            Output.Immediately($"Opened Play Store");

            // Click in Search text box to type the app name
            androidDriverPlayStore.FindElement(MobileBy.XPath("//android.widget.TextView[@text='Search for apps & games']")).Click();

            // Type App Name and click Search
            androidDriverPlayStore.FindElementByClassName("android.widget.EditText").SendKeys(AppName);
            androidDriverPlayStore.PressKeyCode(66);

            // Clicking the Audifon app from search list
            androidDriverPlayStore.FindElement(MobileBy.XPath("//android.view.View[contains(@content-desc, '" + AppName + "')]")).Click();

            // Clicking Install Button
            androidDriverPlayStore.FindElement(MobileBy.XPath("//android.widget.Button[@text='Update']")).Click();

            // Waiting till the Open button is enabled after installation
            Wait.UntilTrue(() => androidDriverPlayStore.FindElement(MobileBy.XPath("//android.widget.Button[@text='Open']")).Enabled, TimeSpan.FromMinutes(5));

            // Close Play Store and Release Driver
            androidDriverPlayStore.CloseApp();
            androidDriverPlayStore.Quit();

            Output.Immediately($"Closed Play Store");

            Output.Immediately(AppName + " successfully installed from Play Store");
        }

        public static void InitializeApp(AppConfig config)
        {
            Output.Immediately($"Initializing app with following configuration: ");
            config.OutputConfig();
            AppConfig = config;

            if (Env.ReportInconclusiveAsFailed)
            {
                Assert.That(Phone, Is.Not.Null, $"{nameof(AppConfig)}.{nameof(AppConfig.Phone)} was not initialized.");
                Assert.That(AppPackageName, Is.Not.Null.And.Not.Empty, $"{nameof(AppConfig)}.{nameof(AppConfig.AppPackageName)} was not provided.");
            }
            else
            {
                Assume.That(Phone, Is.Not.Null, $"{nameof(AppConfig)}.{nameof(AppConfig.Phone)} was not initialized.");
                Assume.That(AppPackageName, Is.Not.Null.And.Not.Empty, $"{nameof(AppConfig)}.{nameof(AppConfig.AppPackageName)} was not provided.");
            }

            AppiumOptions options = new AppiumOptions();
            options.AddAdditionalCapability(MobileCapabilityType.NewCommandTimeout, 3600);
            options.AddAdditionalCapability(MobileCapabilityType.PlatformName, Platform.ToString());
            options.AddAdditionalCapability(MobileCapabilityType.BrowserName, "");
            options.AddAdditionalCapability(MobileCapabilityType.DeviceName, Phone.DeviceName);
            options.AddAdditionalCapability(MobileCapabilityType.Udid, Phone.Udid);
            options.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, Phone.PlatformVersion);
            options.AddAdditionalCapability("autoLaunch", false);       //if autolaunch is set to false, driver based commands like Driver.LaunchApp() do not work on iOS and as a workaround we must use Driver.ExecuteScript(...). See IosApp.cs
            options.AddAdditionalCapability(MobileCapabilityType.NoReset, true);
            options.AddAdditionalCapability(MobileCapabilityType.FullReset, false);

            switch (Platform)
            {
                case Platform.Android:
                    options.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, AppPackageName);
                    options.AddAdditionalCapability(MobileCapabilityType.AutomationName, "UIAutomator2");
                    options.AddAdditionalCapability(AndroidMobileCapabilityType.AppWaitActivity, "horus.MainActivity");
                    options.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, "horus.MainActivity");
                    //options.AddAdditionalCapability("autoDismissAlerts", "true");

                    AndroidManager.UnlockDevice(Phone.Udid);
                    androidDriver = InitializeAndroidDriver(options, CreateOwnAppiumServer);
                    
                    //Note: Installation of .apk is handled here. MobileCapabilityType.App must NOT be used as it may cause issues with Google cloud backup!
                    if (InstallApp)
                    {
                        if (Env.ReportInconclusiveAsFailed)
                            Assert.That(PathToAppPackage, Is.Not.Null.And.Not.Empty, $"{nameof(AppConfig.InstallApp)} was set to true, but {nameof(AppConfig)}.{nameof(AppConfig.PathToAppPackage)} was not provided.");
                        else
                            Assume.That(PathToAppPackage, Is.Not.Null.And.Not.Empty, $"{nameof(AppConfig.InstallApp)} was set to true, but {nameof(AppConfig)}.{nameof(AppConfig.PathToAppPackage)} was not provided.");

                        AndroidManager.UninstallApk(Phone.Udid, AppPackageName);
                        try
                        {
                            Output.Immediately($"Installing APK {PathToAppPackage}");
                            androidDriver.InstallApp(PathToAppPackage);        //Installing the app, thus provoking potential app data restore, so that it can be removed before any tests are run.
                        }
                        catch (Exception e)
                        {
                            Output.Immediately($"Error while installing APK. Original error: {e.Message}");
                            throw new Exception("Error while installing APK.", e);
                        }
                    }
                    if (DeleteAppData)
                        AndroidManager.DeleteAppData(Phone.Udid, AppPackageName);    //Removing the app data using adb. App data might have been introduced via Google backup upon installation
                    app = new AndroidApp(androidDriver, Phone.Udid);

                    try
                    {
                        Output.Immediately($"Enabling Bluetooth on device {Phone}");
                        if (!ReferenceEquals(DeviceSettings, null))
                            DeviceSettings.EnableBluetooth();
                    }
                    catch (Exception ex)
                    {
                        Assert.Warn($"Enabling Bluetooth failed. Message was: {ex.Message}");
                    }

                    break;

                case Platform.iOS:
                    options.AddAdditionalCapability(IOSMobileCapabilityType.BundleId, AppPackageName);
                    options.AddAdditionalCapability(MobileCapabilityType.AutomationName, "XCUITest");
                    options.AddAdditionalCapability("simpleIsVisibleCheck", true);

                    //WebDriverAgent signing
                    options.AddAdditionalCapability("xcodeSigningId", XcodeSigningId);
                    options.AddAdditionalCapability("xcodeOrgId", XcodeOrgId);

                    //Logging output
                    options.AddAdditionalCapability(IOSMobileCapabilityType.ShowIOSLog, false);
                    options.AddAdditionalCapability("showXcodeLog", false);

                    //DEBUG & EXPERIMENTS
                    options.AddAdditionalCapability("waitForQuiescence", false);
                    options.AddAdditionalCapability("useJSONSource", true);

                    IOSDriver<AppiumWebElement> iOSDriver = InitializeIosDriver(options, CreateOwnAppiumServer);
                    app = new IosApp(iOSDriver);
                    break;
                default: throw new Exception("Unknown platform");
            }

            //App is installed and initialized at this point -> Registering used phone to AppManagerLog.
            if (UninstallAppAfterTest)
                Log.RegisterUsedPhone(Phone, UninstallUtilityAppsAfterTest, AppPackageName);
            else
                Log.RegisterUsedPhone(Phone, UninstallUtilityAppsAfterTest);
            DeviceSettings = new DeviceSettings(App);
        }

        public static void InitializeAppAfterUpdate(AppConfig config)
        {
            Output.Immediately($"Initializing app with following configuration: ");
            config.OutputConfig();
            AppConfig = config;

            AppiumOptions options = new AppiumOptions();
            options.AddAdditionalCapability(MobileCapabilityType.NewCommandTimeout, 3600);
            options.AddAdditionalCapability(MobileCapabilityType.PlatformName, Platform.ToString());
            options.AddAdditionalCapability(MobileCapabilityType.BrowserName, "");
            options.AddAdditionalCapability(MobileCapabilityType.DeviceName, Phone.DeviceName);
            options.AddAdditionalCapability(MobileCapabilityType.Udid, Phone.Udid);
            options.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, Phone.PlatformVersion);
            options.AddAdditionalCapability("autoLaunch", false);       //if autolaunch is set to false, driver based commands like Driver.LaunchApp() do not work on iOS and as a workaround we must use Driver.ExecuteScript(...). See IosApp.cs
            options.AddAdditionalCapability(MobileCapabilityType.NoReset, true);
            options.AddAdditionalCapability(MobileCapabilityType.FullReset, false);

            options.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, AppPackageName);
            options.AddAdditionalCapability(MobileCapabilityType.AutomationName, "UIAutomator2");
            options.AddAdditionalCapability(AndroidMobileCapabilityType.AppWaitActivity, "horus.MainActivity");
            //SplashActivity has been removed from Horus solution since new implementation of splash screen mechanism coming with Android 12
            //Changes described in PBI 20728 --> need to use 'MainActivity' as Activity name for the Android activity you want to launch from your package
            options.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, "horus.MainActivity");

            androidDriver = InitializeAndroidDriver(options, CreateOwnAppiumServer);

            app = new AndroidApp(androidDriver, Phone.Udid);

            DeviceSettings = new DeviceSettings(App);
        }

        public static void StartApp(bool deleteAppData)
        {
            if (deleteAppData)
            {
                Debug.WriteLine("   ### Starting app, deleting app data.");
                app?.ResetApp();
            }
            else
            {
                Debug.WriteLine("   ### Starting app, keeping app data.");
                app?.LaunchApp();
            }
        }

        public static void CloseApp()
        {
            if (app != null)
            {
                Debug.WriteLine("   ### Closing app (if its running).");
                app.CloseApp();
            }
        }

        public static void RestartApp(bool deleteAppData)
        {
            if (deleteAppData)
            {
                Debug.WriteLine("   ### Restarting app, deleting app data.");
                app?.ResetApp();
            }
            else
            {
                Debug.WriteLine("   ### Restarting app, keeping app data.");
                app?.CloseApp();
                app?.LaunchApp();
            }
        }

        public static void FinishAndCleanUp()
        {
            if (DisableBluetoothAfterTest)
            {
                try
                {
                    Output.Immediately($"Disabling Bluetooth on device {Phone}");
                    DialogHelper.DismissAllDialogsAndPermissionRequests();
                    DeviceSettings.DisableBluetooth();
                }
                catch (Exception ex)
                {
                    Assert.Warn($"Disabling Bluetooth failed. Message was: {ex.Message}");
                }
            }
            try
            {
                List<string> packagesToUninstall = Log.GetAppsToUninstall(Phone);
                if (packagesToUninstall.Count > 0)
                {
                    Output.Immediately($"Removing packages from device {Phone}:");
                    foreach (string package in packagesToUninstall)
                    {
                        AndroidManager.UninstallApk(Phone.Udid, package);
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Warn($"Removing packages failed. Message was: {ex.Message}");
            }
        }

        public static void UninstallApk()
        {
            AndroidManager.UninstallApk(Phone.Udid, AppPackageName);
        }

        public static void InstallApk(string APKFilePath)
        {
            androidDriver.InstallApp(APKFilePath);
        }
    }
}