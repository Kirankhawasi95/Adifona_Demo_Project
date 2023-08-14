using System;
using HorusUITest.Enums;
using HorusUITest.Helper;

namespace HorusUITest.Data
{
    public class AppConfig
    {
        //Static default
        public static Brand DefaultBrand { get; set; }
        public static bool DefaultCreateOwnAppiumServer { get; set; } = true;
        public static bool DefaultInstallApp { get; set; } = false; //As we no longer have access to apk file for Android
        public static bool DefaultDeleteAppData { get; set; } = true;
        public static bool DefaultUninstallAppAfterTest { get; set; } = false;
        public static bool DefaultUninstallUtilityAppsAfterTest { get; set; } = true;
        public static bool DefaultDisableBluetoothAfterTest { get; set; } = true;

        public static AndroidPhone DefaultAndroidPhone { get; set; }
        public static string DefaultAndroidPackageName { get; set; }
        public static string DefaultPathToApk { get; set; }

        public static IosPhone DefaultIosPhone { get; set; }
        public static string DefaultIosBundleId { get; set; }
        public static string DefaultPathToIpa { get; set; }

        public static string DefaultXcodeSigningId { get; set; } = "iPhone Developer";
        public static string DefaultXcodeOrgId { get; set; } = "4M6ZTR278W";

        /// <summary>
        /// The below variable needs to be set as true to get html report in email.
        /// </summary>
        public static bool DefaultEnableReporting { get; set; } = false;
        /// <summary>
        /// Default email id to which the email will be sent when DefaultEnableReporting is set as true.
        /// </summary>
        public static string DefaultReportEmailRecipient { get; set; } = "ranjith.babu@mgtechsoft.com";
        /// <summary>
        /// By Default RunIOSTestCaseFromWindows will be false so that Audifon users can execute IOS test cases directly from MAC PC.
        /// RunIOSTestCaseFromWindows has to be set as true only when we have to run IOS test case from windows PC connected to MAC PC in same network.
        /// </summary>
        public static bool DefaultRunIOSTestCaseFromWindows { get; set; } = false;
        /// <summary>
        /// DefaultRunIOSTestCaseFromWindowsMacIP will contain the IP of the mac pc. 
        /// When RunIOSTestCaseFromWindows is set as true, test case will be executed in mac system from windows system connecting to this IP.
        /// Both windows and mac should be connected to same IP and the network IP of mac should be specified below.
        /// </summary>
        public static string DefaultRunIOSTestCaseFromWindowsMacIP { get; set; } = "192.168.157.23";
        /// <summary>
        /// This property will decide if the app has to be installed from store.
        /// This needs to be set true to enable this feature.
        /// </summary>
        public static bool DefaultInstallFromStore { get; set; } = false;

        /// <summary>
        /// This variable is to set which organization hearing aids needs to be tested. Either "Microgenisis" or "audifon"
        /// </summary>
        public static string DefaultOrganizationName { get; set; } = "Microgenisis";

        //Instance
        public Platform Platform { get; set; }
        public Brand Brand { get; set; }
        public bool CreateOwnAppiumServer { get; set; }
        public bool InstallApp { get; set; }
        public bool DeleteAppData { get; set; }
        public bool UninstallAppAfterTest { get; set; }
        public bool UninstallUtilityAppsAfterTest { get; set; }

        public BasePhone Phone { get; set; }
        public string AppPackageName { get; set; }
        public string PathToAppPackage { get; set; }

        public string XcodeSigningId { get; set; }
        public string XcodeOrgId { get; set; }
        public bool DisableBluetoothAfterTest { get; set; }

        public bool RunIOSTestCaseFromWindows { get; set; }

        public string RunIOSTestCaseFromWindowsMacIP { get; set; }

        private static void ApplyDefaults(AppConfig config, Platform platform)
        {
            config.Platform = platform;
            config.Brand = DefaultBrand;
            config.InstallApp = DefaultInstallApp;
            config.DeleteAppData = DefaultDeleteAppData;
            config.UninstallAppAfterTest = DefaultUninstallAppAfterTest;
            config.CreateOwnAppiumServer = DefaultCreateOwnAppiumServer;
            config.UninstallUtilityAppsAfterTest = DefaultUninstallUtilityAppsAfterTest;
            config.DisableBluetoothAfterTest = DefaultDisableBluetoothAfterTest;
            config.RunIOSTestCaseFromWindows = DefaultRunIOSTestCaseFromWindows;
            config.RunIOSTestCaseFromWindowsMacIP = DefaultRunIOSTestCaseFromWindowsMacIP;

            switch (platform)
            {
                case Platform.Android:
                    config.Phone = DefaultAndroidPhone;
                    config.AppPackageName = DefaultAndroidPackageName;
                    config.PathToAppPackage = DefaultPathToApk;
                    break;
                case Platform.iOS:
                    config.Phone = DefaultIosPhone;
                    if (DefaultIosBundleId != null)
                        config.AppPackageName = DefaultIosBundleId;
                    else
                        config.AppPackageName = DefaultAndroidPackageName.ToLower();    //HACK: As of June 2020 BundleId is the lower case version of AndroidPackageName (com.audifon.Horus.KIND -> com.audifon.horus.kind)
                    config.PathToAppPackage = DefaultPathToIpa;
                    break;
                default:
                    throw new NotImplementedException("Unknown Platform.");
            }
            config.XcodeSigningId = DefaultXcodeSigningId;
            config.XcodeOrgId = DefaultXcodeOrgId;
        }

        private void SetConfig(Platform platform, BasePhone phone, Brand brand, bool createOwnAppiumServer, bool installApp, bool deleteAppData,
            bool uninstallAppAfterTest, bool uninstallUtilityAppsAfterTest, bool disableBluetoothAfterTest, string appPackageName, string pathToAppPackage, string xcodeSigningId, string xcodeOrgId)
        {
            Platform = platform;
            Phone = phone;
            Brand = brand;
            CreateOwnAppiumServer = createOwnAppiumServer;
            InstallApp = installApp;
            DeleteAppData = deleteAppData;
            UninstallAppAfterTest = uninstallAppAfterTest;
            UninstallUtilityAppsAfterTest = uninstallUtilityAppsAfterTest;
            DisableBluetoothAfterTest = disableBluetoothAfterTest;
            AppPackageName = appPackageName;
            PathToAppPackage = pathToAppPackage;
            XcodeSigningId = xcodeSigningId;
            XcodeOrgId = xcodeOrgId;
        }

        private AppConfig()
        {
        }

        /// <summary>
        /// Default configuration, depending on <paramref name="currentPlatform"/>.
        /// </summary>
        /// <param name="currentPlatform"></param>
        public AppConfig(Platform currentPlatform)
        {
            ApplyDefaults(this, currentPlatform);
        }

        /// <summary>
        /// Default configuration with selected <paramref name="androidPhone"/>.
        /// </summary>
        /// <param name="androidPhone"></param>
        public AppConfig(AndroidPhone androidPhone) : this(Platform.Android)
        {
            Phone = androidPhone;
        }

        /// <summary>
        /// Default configuration with selected <paramref name="iosPhone"/>.
        /// </summary>
        /// <param name="iosPhone"></param>
        public AppConfig(IosPhone iosPhone) : this(Platform.iOS)
        {
            Phone = iosPhone;
        }

        /// <summary>
        /// Custom configuration for tests on Android.
        /// </summary>
        public AppConfig(AndroidPhone androidPhone, Brand brand, bool createOwnAppiumServer, bool installApp, bool deleteAppData,
            bool uninstallAppAfterTest, bool uninstallUtilityAppsAfterTest, bool disableBluetoothAfterTest, string appPackageName, string pathToAppPackage)
        {
            SetConfig(Platform.Android, androidPhone, brand, createOwnAppiumServer, installApp, deleteAppData,
                uninstallAppAfterTest, uninstallUtilityAppsAfterTest, disableBluetoothAfterTest, appPackageName, pathToAppPackage, null, null);
        }

        /// <summary>
        /// Custom configuration for tests on iOS.
        /// </summary>
        public AppConfig(IosPhone iosPhone, Brand brand, bool createOwnAppiumServer, bool installApp, bool deleteAppData,
            bool uninstallAppAfterTest, bool uninstallUtilityAppsAfterTest, bool disableBluetoothAfterTest, string appPackageName, string pathToAppPackage, string xcodeSigningId, string xcodeOrgId) : this(iosPhone)
        {
            SetConfig(Platform.iOS, iosPhone, brand, createOwnAppiumServer, installApp, deleteAppData,
                uninstallAppAfterTest, uninstallUtilityAppsAfterTest, disableBluetoothAfterTest, appPackageName, pathToAppPackage, xcodeSigningId, xcodeOrgId);
        }

        /// <summary>
        /// Default configuration with selected <paramref name="androidPhone"/> and <paramref name="iosPhone"/>, depending on <paramref name="currentPlatform"/>.
        /// Arguments that are not provided, default to the static configuration.
        /// </summary>
        /// <param name="currentPlatform">The platform to run the tests on. Determines whether <paramref name="androidPhone"/> or <paramref name="iosPhone"/> is used.</param>
        /// <param name="androidPhone">Phone to use when running tests on Android.</param>
        /// <param name="iosPhone">Phone to use when running tests on iOS.</param>
        public AppConfig(Platform currentPlatform, AndroidPhone androidPhone, IosPhone iosPhone) : this(currentPlatform)
        {
            switch (currentPlatform)
            {
                case Platform.Android:
                    Phone = androidPhone;
                    break;
                case Platform.iOS:
                    Phone = iosPhone;
                    break;
                default:
                    throw new NotImplementedException("Unknown Platform.");
            }
        }

        public static void OutputDefaultConfig()
        {
            Output.Immediately($"{nameof(AppConfig.DefaultBrand)}: {AppConfig.DefaultBrand}");
            Output.Immediately($"{nameof(AppConfig.DefaultInstallApp)}: {AppConfig.DefaultInstallApp}");
            Output.Immediately($"{nameof(AppConfig.DefaultDeleteAppData)}: {AppConfig.DefaultDeleteAppData}");
            Output.Immediately($"{nameof(AppConfig.DefaultUninstallAppAfterTest)}: {AppConfig.DefaultUninstallAppAfterTest}");
            Output.Immediately($"{nameof(AppConfig.DefaultCreateOwnAppiumServer)}: {AppConfig.DefaultCreateOwnAppiumServer}");
            Output.Immediately($"{nameof(AppConfig.DefaultUninstallUtilityAppsAfterTest)}: {AppConfig.DefaultUninstallUtilityAppsAfterTest}");
            Output.Immediately($"{nameof(AppConfig.DefaultDisableBluetoothAfterTest)}: {AppConfig.DefaultDisableBluetoothAfterTest}");
            Output.Immediately($"{nameof(AppConfig.DefaultAndroidPhone)}: {AppConfig.DefaultAndroidPhone}");
            Output.Immediately($"{nameof(AppConfig.DefaultAndroidPackageName)}: {AppConfig.DefaultAndroidPackageName}");
            Output.Immediately($"{nameof(AppConfig.DefaultPathToApk)}: {AppConfig.DefaultPathToApk}");
            Output.Immediately($"{nameof(AppConfig.DefaultIosPhone)}: {AppConfig.DefaultIosPhone}");
            Output.Immediately($"{nameof(AppConfig.DefaultIosBundleId)}: {AppConfig.DefaultIosBundleId}");
            Output.Immediately($"{nameof(AppConfig.DefaultPathToIpa)}: {AppConfig.DefaultPathToIpa}");
            Output.Immediately($"{nameof(AppConfig.DefaultXcodeSigningId)}: {AppConfig.DefaultXcodeSigningId}");
            Output.Immediately($"{nameof(AppConfig.DefaultXcodeOrgId)}: {AppConfig.DefaultXcodeOrgId}");
        }

        public void OutputConfig()
        {
            Output.Immediately($"{nameof(Platform)}: {Platform}");
            Output.Immediately($"{nameof(Brand)}: {DefaultBrand}");
            Output.Immediately($"{nameof(InstallApp)}: {InstallApp}");
            Output.Immediately($"{nameof(DeleteAppData)}: {DeleteAppData}");
            Output.Immediately($"{nameof(UninstallAppAfterTest)}: {UninstallAppAfterTest}");
            Output.Immediately($"{nameof(CreateOwnAppiumServer)}: {CreateOwnAppiumServer}");
            Output.Immediately($"{nameof(UninstallUtilityAppsAfterTest)}: {UninstallUtilityAppsAfterTest}");
            Output.Immediately($"{nameof(DisableBluetoothAfterTest)}: {DisableBluetoothAfterTest}");
            Output.Immediately($"{nameof(Phone)}: {Phone}");
            Output.Immediately($"{nameof(AppPackageName)}: {AppPackageName}");
            Output.Immediately($"{nameof(PathToAppPackage)}: {PathToAppPackage}");
            Output.Immediately($"{nameof(XcodeSigningId)}: {XcodeSigningId}");
            Output.Immediately($"{nameof(XcodeOrgId)}: {XcodeOrgId}");
        }
    }
}