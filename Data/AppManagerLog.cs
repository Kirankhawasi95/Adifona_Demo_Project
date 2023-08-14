using System;
using System.Collections.Generic;

namespace HorusUITest.Data
{
    public class AppManagerLog
    {
        private Dictionary<BasePhone, List<string>> appsToUninstall = new Dictionary<BasePhone, List<string>>();

        public List<AndroidPhone> UsedAndroidPhones { get; } = new List<AndroidPhone>();
        public List<IosPhone> UsedIosPhones { get; } = new List<IosPhone>();

        private void RegisterAppToUninstall<T>(T phone, string packageName) where T : BasePhone
        {
            if (!appsToUninstall.TryGetValue(phone, out var packageList))
            {
                packageList = new List<string>();
                appsToUninstall.Add(phone, packageList);
            }
            if (!packageList.Contains(packageName))
            {
                packageList.Add(packageName);
            }
        }

        private void RegisterUtilityAppsToUninstall<T>(T phone) where T : BasePhone
        {
            switch (phone)
            {
                case AndroidPhone _:
                    RegisterAppToUninstall(phone, "io.appium.settings");
                    RegisterAppToUninstall(phone, "io.appium.uiautomator2.server");
                    RegisterAppToUninstall(phone, "io.appium.uiautomator2.server.test");
                    RegisterAppToUninstall(phone, "io.appium.android.apis");
                    RegisterAppToUninstall(phone, "io.appium.unlock");
                    break;
                case IosPhone _:
                    //Do nothing for now
                    break;
                default:
                    throw new NotImplementedException("Unknown phone class.");
            }
        }

        public void RegisterUsedPhone<T>(T phone) where T : BasePhone
        {
            switch (phone)
            {
                case AndroidPhone androidPhone:
                    if (!UsedAndroidPhones.Contains(androidPhone))
                        UsedAndroidPhones.Add(androidPhone);
                    break;
                case IosPhone iosPhone:
                    if (!UsedIosPhones.Contains(iosPhone))
                        UsedIosPhones.Add(iosPhone);
                    break;
                default:
                    throw new NotImplementedException("Unknown phone class.");
            }
        }

        public void RegisterUsedPhone<T>(T phone, bool uninstallUtilityAppsAfterTest) where T : BasePhone
        {
            RegisterUsedPhone(phone);
            if (uninstallUtilityAppsAfterTest)
                RegisterUtilityAppsToUninstall(phone);
        }

        public void RegisterUsedPhone<T>(T phone, bool uninstallUtilityAppsAfterTest, string packageToUninstallAfterTest) where T : BasePhone
        {
            RegisterUsedPhone(phone, uninstallUtilityAppsAfterTest);
            RegisterAppToUninstall(phone, packageToUninstallAfterTest);
        }

        public List<string> GetAppsToUninstall(BasePhone phone)
        {
            if (appsToUninstall.TryGetValue(phone, out var result))
                return result;
            else
                return new List<string>();
        }
    }
}
