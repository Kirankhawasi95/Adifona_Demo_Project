using HorusUITest.Enums;
using System;

namespace HorusUITest.Helper
{
    public class DeviceSettings
    {
        BaseApp _app;

        public DeviceSettings(BaseApp app)
        {
            _app = app;
        }

        public void EnableBluetooth()
        {
            _app.EnableBluetooth();
        }

        public void DisableBluetooth()
        {
            _app.DisableBluetooth();
        }

        public void EnableLocation()
        {
            _app.EnableLocation();
        }

        public void DisableLocation()
        {
            _app.DisableLocation();
        }

        public void EnableWifi()
        {
            _app.EnableWifi();
        }

        public void DisableWifi()
        {
            _app.DisableWifi();
        }
        public void GrantGPSPermission()
        {
            _app.GrantGPSPermission();
        }

        public void RevokeGPSPermission()
        {
            _app.RevokeGPSPermission();
        }
        public void GrantGPSBackgroundPermission()
        {
            _app.GrantGPSBackgroundPermission();
        }
        public void RevokeGPSBackgroundPermission()
        {
            _app.RevokeGPSBackgroundPermission();
        }

        public void LockDevice(int seconds)
        {
            _app.LockDevice(seconds);
        }
        
        public void HideKeyboard()
        {
            _app.HideKeyboard();
        }

        public void PutAppToBackground(int appBackgroundTime)
        {
            _app.PutAppToBackground(appBackgroundTime);
        }

        /// <summary>
        /// Can use this method if we need to put the app in background for less than 1 sec
        /// </summary>
        /// <param name="appBackgroundTime"></param>
        public void PutAppToBackground(double appBackgroundTime)
        {
            _app.PutAppToBackground(appBackgroundTime);
        }

        public void PutAppToBackground()
        {
            _app.PutAppToBackground();
        }

        public void GetAppInForeground()
        {
            _app.GetAppInForeground();
        }
        public void EnableMobileData()
        {
            _app.EnableMobileData();
        }

        public void DisableMobileData()
        {
            _app.DisableMobileData();
        }
        public void ChangeDeviceLanguage(Language_Device language)
        {
            _app.ChangeDeviceLanguage(language);
        }

        public void ChangeDeviceDate(DateTime dateValue)
        {
            _app.ChangeDeviceDate(dateValue);
        }

        public string GetDeviceOSVersion()
        {
            return _app.GetDeviceOSVersion();
        }
    }
}
