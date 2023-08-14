using System;
using NUnit.Framework;
using HorusUITest.PageObjects.Favorites.Automation;


namespace HorusUITest.Helper
{
    public static class AndroidManager
    {
        private static string GetDeviceString(string deviceSerial)
        {
            return deviceSerial == null ? throw new ArgumentException("DeviceSerial must not be null.") : "-s " + deviceSerial + " ";
        }
        public static string uId;
        private static string pathToAdbExecutable;
        public static string PathToAdbExecutable
        {
            get
            {
                if (pathToAdbExecutable == null)
                    throw new NullReferenceException("'" + nameof(AndroidManager) + "." + nameof(PathToAdbExecutable) + "' not set.");
                return pathToAdbExecutable;
            }
            set
            {
                pathToAdbExecutable = value;
            }
        }

        public static void UnlockDevice(string deviceSerial)
        {
            Output.Immediately($"Unlocking device {deviceSerial}");
            string deviceString = GetDeviceString(deviceSerial);
            //get dumpsys for power stats which includes screen on/off info
            string power = CommandUtils.ExecuteShell(PathToAdbExecutable, deviceString + "shell dumpsys power");

            //checks if screen is on/off. Two versions for different android versions.
            if (power.Contains("mScreenOn=false") || power.Contains("Display Power: state=OFF"))
            {
                //Sends keycode for power on
                var on_res = CommandUtils.ExecuteShell(PathToAdbExecutable, deviceString + "shell input keyevent 26");
                //Sends keycode for menu button. This will unlock stock android lockscreen. 
                //Does nothing if lockscreen is disabled
                var menu_res = CommandUtils.ExecuteShell(PathToAdbExecutable, deviceString + "shell input keyevent 82");
                Assert.IsTrue(on_res == string.Empty || menu_res == string.Empty,
                "There was a problem turning on the screen, msg: {0}, {1}", on_res, menu_res);
            }
        }

        public static void LockDevice(string deviceSerial)
        {
            Output.Immediately($"Locking device {deviceSerial}");
            string deviceString = GetDeviceString(deviceSerial);
            //get dumpsys for power stats which includes screen on/off info
            string power = CommandUtils.ExecuteShell(PathToAdbExecutable, deviceString + "shell dumpsys power");

            //checks if screen is on/off. Two versions for different android versions.
            if (power.Contains("mScreenOn=true") || power.Contains("Display Power: state=ON"))
            {
                //Sends keycode for power on
                var on_res = CommandUtils.ExecuteShell(PathToAdbExecutable, deviceString + "shell input keyevent 26");
                //Sends keycode for menu button. This will unlock stock android lockscreen. 
                //Does nothing if lockscreen is disabled
                var menu_res = CommandUtils.ExecuteShell(PathToAdbExecutable, deviceString + "shell input keyevent 82");
                Assert.IsTrue(on_res == string.Empty || menu_res == string.Empty,
                "There was a problem turning on the screen, msg: {0}, {1}", on_res, menu_res);
            }
        }
        public static string GetFirstDevice()
        {
            string response = CommandUtils.ExecuteShell(PathToAdbExecutable, "devices");
            foreach (var s in response.Replace("\r", "").Split('\n'))
            {
                if (s.EndsWith("device"))
                {
                    uId = s.Split('\t')[0];
                    return uId;
                }
                else if (s.Contains("\t"))
                {
                    uId = s.Split('\t')[0];
                    return uId;
                }
            }
            return null;
        }

        public static bool GetIsApkInstalled(string deviceSerial, string packageName)
        {
            string deviceString = GetDeviceString(deviceSerial);
            string response = CommandUtils.ExecuteShell(PathToAdbExecutable, deviceString + "shell pm list packages " + packageName);
            foreach (var s in response.Replace("\r", "").Split('\n'))
            {
                if (s.EndsWith(packageName)) return true;
            }
            return false;
        }

        public static void UninstallApk(string deviceSerial, string packageName)
        {
            if (!GetIsApkInstalled(deviceSerial, packageName))
                return;
            Output.Immediately($"Uninstalling APK '{packageName}'");
            string deviceString = GetDeviceString(deviceSerial);
            CommandUtils.ExecuteShell(PathToAdbExecutable, deviceString + "shell pm uninstall " + packageName);
        }

        public static void DeleteAppData(string deviceSerial, string packageName)
        {
            Output.Immediately($"Deleting app data of package '{packageName}'");
            string deviceString = GetDeviceString(deviceSerial);
            CommandUtils.ExecuteShell(PathToAdbExecutable, deviceString + "shell pm clear " + packageName);
        }

        /// <summary>
        /// Enables Bluetooth on the given device. The package 'io.appium.settings' must already be installed.
        /// </summary>
        /// <param name="deviceSerial"></param>
        public static void EnableBluetooth(string deviceSerial)
        {
            string deviceString = GetDeviceString(deviceSerial);
            CommandUtils.ExecuteShell(PathToAdbExecutable, deviceString + "shell am broadcast -a io.appium.settings.bluetooth --es setstatus enable");
        }

        /// <summary>
        /// Disables Bluetooth on the given device. The package 'io.appium.settings' must already be installed.
        /// </summary>
        /// <param name="deviceSerial"></param>
        public static void DisableBluetooth(string deviceSerial)
        {
            string deviceString = GetDeviceString(deviceSerial);
            CommandUtils.ExecuteShell(PathToAdbExecutable, deviceString + "shell am broadcast -a io.appium.settings.bluetooth --es setstatus disable");
        }

        /// <summary>
        /// Enables Wifi on the given device. The package 'io.appium.settings' must already be installed.
        /// </summary>
        /// <param name="deviceSerial"></param>
        public static void EnableWifi(string deviceSerial)
        {
            string deviceString = GetDeviceString(deviceSerial);
            CommandUtils.ExecuteShell(PathToAdbExecutable, deviceString + "shell am broadcast -a io.appium.settings.wifi --es setstatus enable");
        }

        /// <summary>
        /// Disables Wifi on the given device. The package 'io.appium.settings' must already be installed.
        /// </summary>
        /// <param name="deviceSerial"></param>
        public static void DisableWifi(string deviceSerial)
        {
            string deviceString = GetDeviceString(deviceSerial);
            CommandUtils.ExecuteShell(PathToAdbExecutable, deviceString + "shell am broadcast -a io.appium.settings.wifi --es setstatus disable");
        }

        /// <summary>
        /// Disables Location on the given device. The package 'io.appium.settings' must already be installed.
        /// </summary>
        /// <param name="deviceSerial"></param>
        public static void DisableLocation(string deviceSerial)
        {
            string deviceString = GetDeviceString(deviceSerial);
            //CommandUtils.ExecuteShell(PathToAdbExecutable, deviceString + "shell settings put secure location_providers_allowed -gps");
            CommandUtils.ExecuteShell(PathToAdbExecutable, deviceString + "shell settings put secure location_mode 0");
        }

        /// <summary>
        /// Enables Location on the given device. The package 'io.appium.settings' must already be installed.
        /// </summary>
        /// <param name="deviceSerial"></param>
        public static void EnableLocation(string deviceSerial)
        {
            string deviceString = GetDeviceString(deviceSerial);
            //CommandUtils.ExecuteShell(PathToAdbExecutable, deviceString + "shell settings put secure location_providers_allowed +gps");
            CommandUtils.ExecuteShell(PathToAdbExecutable, deviceString + "shell settings put secure location_mode 3");
        }

        /// <summary>
        /// Enables "Allow all the time" for GPS permission
        /// Required when selecting map location on <see cref="AutomationGeofenceBindingPage"/>
        /// </summary>
        /// <param name="appPackage"></param>
        public static void GrantGPSBackgroundPermission(string appPackage)
        {
            CommandUtils.ExecuteShell(PathToAdbExecutable, "shell pm grant " + appPackage + " android.permission.ACCESS_BACKGROUND_LOCATION");
        }

        /// <summary>
        /// Revoking permission at runtime causes app to close unexpectedly
        /// </summary>
        /// <param name="appPackage"></param>
        public static void RevokeGPSBackgroundPermission(string appPackage)
        {
            CommandUtils.ExecuteShell(PathToAdbExecutable, "shell pm revoke " + appPackage + " android.permission.ACCESS_BACKGROUND_LOCATION");
        }

        /// <summary>
        /// App requires "Allow all the time" on <see cref="AutomationGeofenceBindingPage"/> to select map location
        /// But permission dialog dosent appears in Android-11, and only option is to enable from settings. 
        /// Use <see cref="GrantGPSBackgroundPermission"/> before selecting map location
        /// </summary>
        /// <param name="appPackage"></param>
        public static void GrantGPSPermission(string appPackage)
        {
            CommandUtils.ExecuteShell(PathToAdbExecutable, "shell pm grant " + appPackage + " android.permission.ACCESS_FINE_LOCATION");
        }

        /// <summary>
        /// Revoking permission at runtime causes app to close unexpectedly
        /// </summary>
        /// <param name="appPackage"></param>
        public static void RevokeGPSPermission(string appPackage)
        {
            CommandUtils.ExecuteShell(PathToAdbExecutable, "shell pm revoke " +appPackage+ " android.permission.ACCESS_FINE_LOCATION");
        }
        public static void EnableMobileData(string deviceSerial)
        {
            string deviceString = GetDeviceString(deviceSerial);
            CommandUtils.ExecuteShell(PathToAdbExecutable, deviceString + "shell svc data enable");
        }
        public static void DisableMobileData(string deviceSerial)
        {
            string deviceString = GetDeviceString(deviceSerial);
            CommandUtils.ExecuteShell(PathToAdbExecutable, deviceString + "shell svc data disable");
        }
        public static void ChangeDeviceLanguage(string deviceSerial, string langaugeCode, string countryCode)
        {
            string deviceString = GetDeviceString(deviceSerial);
            CommandUtils.ExecuteShell(PathToAdbExecutable, deviceString + "shell am broadcast -a io.appium.settings.locale --es lang "+langaugeCode+" --es country "+countryCode);
        }

        public static void ChangeDeviceDate(string deviceSerial, DateTime dateValue)
        {
            string Day = dateValue.Day.ToString();
            if (Day.Length == 1)
                Day = "0" + Day;
            string Month = dateValue.Month.ToString();
            if (Month.Length == 1)
                Month = "0" + Month;
            string Hour = dateValue.Hour.ToString();
            if (Hour.Length == 1)
                Hour = "0" + Hour;
            string Minutes = dateValue.Minute.ToString();
            if (Minutes.Length == 1)
                Minutes = "0" + Minutes;

            string changeDateText = Month + Day + Hour + Minutes + ".00";

            string deviceString = GetDeviceString(deviceSerial);
            CommandUtils.ExecuteShell(PathToAdbExecutable, deviceString + "shell date " + changeDateText);
        }

        public static string GetAndroidVersion()
        {
            string AndroidVersion = CommandUtils.ExecuteShell(PathToAdbExecutable, "shell getprop ro.build.version.release");
            // Getting only Major version
            if (AndroidVersion.IndexOf('.') != -1)
                return AndroidVersion.Substring(0, AndroidVersion.IndexOf('.'));
            else
                return AndroidVersion;
        }
    }
}
