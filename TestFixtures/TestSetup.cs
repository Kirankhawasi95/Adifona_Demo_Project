//#define TEST_FROM_IDE

using System;
using System.Diagnostics;
using System.IO;
using HorusUITest.Configuration;
using HorusUITest.Data;
using HorusUITest.Enums;
using HorusUITest.Helper;
using NUnit.Framework;

namespace HorusUITest.TestFixtures
{
    [SetUpFixture]
    public class TestSetup
    {
        private Stopwatch watch = new Stopwatch();

        private string SelectParameterIfProvided(string originalValue, string newValueFromParameter)
        {
            if (newValueFromParameter != null)
                return newValueFromParameter;
            else
                return originalValue;

        }

        private bool SelectParameterIfProvided(bool originalValue, string newValueFromParameter)
        {
            if (bool.TryParse(TestContext.Parameters[nameof(AppConfig.DefaultInstallApp)], out bool result))
                return result;
            else
                return originalValue;
        }
        protected bool OniOS => AppManager.Platform == Platform.iOS;

        private void FakeParametersForIdeUsage()
        {
            string iosUdid = TestContext.Parameters["IosUdid"];
            if (Environment.GetEnvironmentVariable("ANDROID_HOME") == null)
            {
                Environment.SetEnvironmentVariable("ANDROID_HOME", EnvironmentUtils.LocatePathToAndroidHome());      //Workaround for error "Could not find 'adb' in PATH ..." when running tests from within Visual Studio (for Mac).
                Output.Immediately($"ANDROID_HOME not set explicitly. Auto-detected to: {Environment.GetEnvironmentVariable("ANDROID_HOME")}");
            }
            else
            {
                Output.Immediately($"ANDROID_HOME: {Environment.GetEnvironmentVariable("ANDROID_HOME")}");
            }
            if (Environment.GetEnvironmentVariable("JAVA_HOME") == null)
            {
                Environment.SetEnvironmentVariable("JAVA_HOME", EnvironmentUtils.LocatePathToJavaHome());      //Workaround for error "JAVA_HOME is not set currently. Please set JAVA_HOME." when running from within VS Mac.
                Output.Immediately($"JAVA_HOME not set explicitly. Auto-detected to: {Environment.GetEnvironmentVariable("JAVA_HOME")}");
            }
            else
            {
                Output.Immediately($"JAVA_HOME: {Environment.GetEnvironmentVariable("JAVA_HOME")}");
            }
            AndroidManager.PathToAdbExecutable = Environment.GetEnvironmentVariable("ANDROID_HOME") + "/platform-tools/adb";
            AppConfig.DefaultIosPhone = SelectPhone.Ios(IosPhoneName.Microgenisis_iPhone_13);
            //if (AppConfig.DefaultIosPhone != null)
            //{
            //    iosUdid = SelectPhone.GetFirstiOSDevice();
            //    AppConfig.DefaultIosPhone = SelectPhone.GetPhoneByUdid(iosUdid);
            //}

#if !TEST_FROM_IDE
            AppConfig.DefaultAndroidPhone = SelectPhone.Android(AndroidPhoneName.Audifon_Nokia_7_plus);
#else            
            if (AppConfig.DefaultAndroidPhone == null)      //If AndroidPhone is not provided explicitly, any connected Android device is used.
            {
                string deviceSerial = AndroidManager.GetFirstDevice();
                if (deviceSerial != null)
                {
                    AndroidPhone androidPhone = new AndroidPhone(deviceSerial);
                    AppConfig.DefaultAndroidPhone = androidPhone;
                    Output.Immediately($"Android device not set explicitly. Auto-detected to: {AppConfig.DefaultAndroidPhone}");
                }
                else
                {
                    Output.Immediately($"Android device not set explicitly. Auto-detection using ADB failed as well. There is no Android device!");
                }
            }
            else
            {
                Output.Immediately($"Android device: {AppConfig.DefaultAndroidPhone}");
            }

            //möglicherweise muss auto iPhone detect hier implementiert werden

            //if (AppConfig.DefaultIosPhone == null)      //If iPhone is not provided explicitly, any connected iOS device is used.
            //{
            //    iosUdid = SelectPhone.GetFirstiOSDevice();
            //    if (iosUdid != null)
            //    {
            //        IosPhone iOSPhone = new IosPhone(iosUdid);
            //        AppConfig.DefaultIosPhone = iOSPhone;
            //        Output.Immediately($"iOS device not set explicitly. Auto-detected to: {AppConfig.DefaultIosPhone}");
            //    }
            //    else
            //    {
            //        Output.Immediately($"iOS device not set explicitly. Auto-detection using 'idevice_id' failed as well. There is no iOS device!");
            //    }
            //}
            //else
            //{
            //    Output.Immediately($"iOS device: {AppConfig.DefaultIosPhone}");
            //}
#endif

            // Getting Brand Name from Jenkins. If run from Visual studio it will run in Audifo App and can be changed if required in the else part
            string AppName = ReportHelper.GetJenkinsParameterValueByKey("AppName", string.Empty);
            if(AppName != string.Empty)
            {
                switch(AppName)
                {
                    case "Audifon":
                        {
                            //Audifon
                            AppConfig.DefaultBrand = Brand.Audifon;
                            AppConfig.DefaultAndroidPackageName = "com.audifon.horus";
                            AppConfig.DefaultIosBundleId = "com.audifon.horus";

                            break;
                        }
                    case "PureTone":
                        {
                            //Puretone
                            AppConfig.DefaultBrand = Brand.Puretone;
                            AppConfig.DefaultAndroidPackageName = "com.audifon.horus.puretone";
                            AppConfig.DefaultIosBundleId = "com.audifon.horus.puretone";

                            break;
                        }
                    case "PersonnaMedical":
                        {
                            //Persona
                            AppConfig.DefaultBrand = Brand.PersonaMedical;
                            AppConfig.DefaultAndroidPackageName = "com.personamedical.sombra";
                            AppConfig.DefaultIosBundleId = "com.personamedical.sombra";

                            break;
                        }
                    case "Kind":
                        {
                            //Kind
                            AppConfig.DefaultBrand = Brand.Kind;
                            AppConfig.DefaultAndroidPackageName = "com.audifon.horus.kind";
                            AppConfig.DefaultIosBundleId = "com.audifon.horus.kind";

                            break;
                        }
                    case "Hormann":
                        {
                            //Hormann
                            AppConfig.DefaultBrand = Brand.Hormann;
                            AppConfig.DefaultAndroidPackageName = "com.audifon.horus.hormann";
                            AppConfig.DefaultIosBundleId = "com.audifon.horus.hormann";

                            break;
                        }
                    case "RxEarsPro":
                        {
                            //RxEarsPro
                            AppConfig.DefaultBrand = Brand.RxEarsPro;
                            AppConfig.DefaultAndroidPackageName = "com.personamedical.rxearspro";
                            AppConfig.DefaultIosBundleId = "com.personamedical.rxearspro";

                            break;
                        }
                }
            }
            else
            {
                // Comment or Uncomment any app you want to run

                //Audifon
                AppConfig.DefaultBrand = Brand.Audifon;
                AppConfig.DefaultAndroidPackageName = "com.audifon.horus";
                AppConfig.DefaultIosBundleId = "com.audifon.horus";

                ////Puretone
                //AppConfig.DefaultBrand = Brand.Puretone;
                //AppConfig.DefaultAndroidPackageName = "com.audifon.horus.puretone";
                //AppConfig.DefaultIosBundleId = "com.audifon.horus.puretone";

                ////Persona
                //AppConfig.DefaultBrand = Brand.PersonaMedical;
                //AppConfig.DefaultAndroidPackageName = "com.personamedical.sombra";
                //AppConfig.DefaultIosBundleId = "com.personamedical.sombra";

                ////Kind
                //AppConfig.DefaultBrand = Brand.Kind;
                //AppConfig.DefaultAndroidPackageName = "com.audifon.horus.kind";
                //AppConfig.DefaultIosBundleId = "com.audifon.horus.kind";

                ////Hormann
                //AppConfig.DefaultBrand = Brand.Hormann;
                //AppConfig.DefaultAndroidPackageName = "com.audifon.horus.hormann";
                //AppConfig.DefaultIosBundleId = "com.audifon.horus.hormann";

                ////RxEarsPro
                //AppConfig.DefaultBrand = Brand.RxEarsPro;
                //AppConfig.DefaultAndroidPackageName = "com.personamedical.rxearspro";
                //AppConfig.DefaultIosBundleId = "com.personamedical.rxearspro";
            }

            //AppConfig.DefaultPathToApk = new FileInfo("C:/Users/zimmermannpe/Desktop/WingsAppapk/Kind/com.audifon.horus.kind-Signed.apk").FullName;                                       //Local debug build in solution (Mac + Windows)
            AppConfig.DefaultPathToApk = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Audifon\\APK\\com.audifon.horus-1.4.0.21215.505.apk").FullName;
        }

        private void ProcessParameters()
        {
            //Environment variables
            if (Environment.GetEnvironmentVariable("ANDROID_HOME") == null)
            {
                Environment.SetEnvironmentVariable("ANDROID_HOME", EnvironmentUtils.LocatePathToAndroidHome());
                Output.Immediately($"ANDROID_HOME not set explicitly. Auto-detected to: {Environment.GetEnvironmentVariable("ANDROID_HOME")}");
            }
            else
            {
                Output.Immediately($"ANDROID_HOME: {Environment.GetEnvironmentVariable("ANDROID_HOME")}");
            }
            if (Environment.GetEnvironmentVariable("JAVA_HOME") == null)
            {
                Environment.SetEnvironmentVariable("JAVA_HOME", EnvironmentUtils.LocatePathToJavaHome());
                Output.Immediately($"JAVA_HOME not set explicitly. Auto-detected to: {Environment.GetEnvironmentVariable("JAVA_HOME")}");
            }
            else
            {
                Output.Immediately($"JAVA_HOME: {Environment.GetEnvironmentVariable("JAVA_HOME")}");
            }

            //Path to ADB
            AndroidManager.PathToAdbExecutable = TestContext.Parameters[nameof(AndroidManager.PathToAdbExecutable)] ?? EnvironmentUtils.LocatePathToAdb();

            //App parameters
            string brand = TestContext.Parameters["Brand"];
            string installApp = TestContext.Parameters["InstallApp"];
            string deleteAppData = TestContext.Parameters["DeleteAppData"];
            string uninstallAppAfterTest = TestContext.Parameters["UninstallAppAfterTest"];
            string createOwnAppiumServer = TestContext.Parameters["CreateOwnAppiumServer"];
            string uninstallUtilityAppsAfterTest = TestContext.Parameters["UninstallUtilityAppsAfterTest"];
            string disableBluetoothAfterTest = TestContext.Parameters["DisableBluetoothAfterTest"];
            string androidUdid = TestContext.Parameters["AndroidUdid"];
            string androidPackageName = TestContext.Parameters["AndroidPackageName"];
            string pathToApk = TestContext.Parameters["PathToApk"];
            string iosUdid = TestContext.Parameters["IosUdid"];
            string iosBundleId = TestContext.Parameters["IosBundleId"];
            string pathToIpa = TestContext.Parameters["PathToIpa"];
            string reportInconclusiveAsFailed = TestContext.Parameters["ReportInconclusiveAsFailed"];

            AppConfig.DefaultInstallApp = SelectParameterIfProvided(AppConfig.DefaultInstallApp, installApp);
            AppConfig.DefaultDeleteAppData = SelectParameterIfProvided(AppConfig.DefaultDeleteAppData, deleteAppData);
            AppConfig.DefaultUninstallAppAfterTest = SelectParameterIfProvided(AppConfig.DefaultUninstallAppAfterTest, uninstallAppAfterTest);
            AppConfig.DefaultCreateOwnAppiumServer = SelectParameterIfProvided(AppConfig.DefaultCreateOwnAppiumServer, createOwnAppiumServer);
            AppConfig.DefaultUninstallUtilityAppsAfterTest = SelectParameterIfProvided(AppConfig.DefaultUninstallUtilityAppsAfterTest, uninstallUtilityAppsAfterTest);
            AppConfig.DefaultDisableBluetoothAfterTest = SelectParameterIfProvided(AppConfig.DefaultDisableBluetoothAfterTest, disableBluetoothAfterTest);
            AppConfig.DefaultAndroidPackageName = SelectParameterIfProvided(AppConfig.DefaultAndroidPackageName, androidPackageName);
            AppConfig.DefaultPathToApk = SelectParameterIfProvided(AppConfig.DefaultAndroidPackageName, pathToApk);
            AppConfig.DefaultIosBundleId = SelectParameterIfProvided(AppConfig.DefaultIosBundleId, iosBundleId);
            AppConfig.DefaultPathToIpa = SelectParameterIfProvided(AppConfig.DefaultPathToIpa, pathToIpa);
            Env.ReportInconclusiveAsFailed = SelectParameterIfProvided(Env.ReportInconclusiveAsFailed, reportInconclusiveAsFailed);
            if (brand != null)
            {
                brand = brand.ToLower();
                switch (brand)
                {
                    case "audifon": AppConfig.DefaultBrand = Brand.Audifon; break;
                    case "kind": AppConfig.DefaultBrand = Brand.Kind; break;
                    case "huier": AppConfig.DefaultBrand = Brand.HuiEr; break;
                    case "audiopss": AppConfig.DefaultBrand = Brand.AudioPSS; break;
                    case "personamedical": AppConfig.DefaultBrand = Brand.PersonaMedical; break;
                    case "puretone": AppConfig.DefaultBrand = Brand.Puretone; break;
                    case "hormann": AppConfig.DefaultBrand = Brand.Hormann; break;
                    case "rxearspro": AppConfig.DefaultBrand = Brand.RxEarsPro; break;
                }
            }
            if (androidUdid == null)
                androidUdid = AndroidManager.GetFirstDevice();
            if (androidUdid == null)
            {
                Output.Immediately($"Android device not set explicitly. Auto-detection using ADB failed as well. There is no Android device!");
            }
            else
            {
                AppConfig.DefaultAndroidPhone = new AndroidPhone(androidUdid);
                Output.Immediately($"Android device not set explicitly. Auto-detected to: {AppConfig.DefaultAndroidPhone}");
            }
            if (iosUdid == null)
                iosUdid = SelectPhone.GetFirstiOSDevice();
            if (iosUdid == null)
            {
                Output.Immediately($"No iPhone for test was set explicitly. Auto-detection using 'idevice_id' failed as well. There is no iPhone device!");
            }
            else
            {
                AppConfig.DefaultIosPhone = SelectPhone.GetPhoneByUdid(iosUdid);
                Output.Immediately($"iOS device not set explicitly. Auto-detected to: {AppConfig.DefaultIosPhone}");
            }

            //if (iosudid == null)
            //{
            //    iosudid = selectphone.getfirstiosdevice();
            //    appconfig.defaultiosphone = selectphone.getphonebyudid(iosudid);
            //}
            //if (iosudid == null)
            //{
            //    output.immediately($"no iphone for test was set explicitly. auto-detection using 'idevice_id' failed as well. there is no iphone device!");
            //}

            //if (iosudid == null)
            //{
            //    iosudid = selectphone.getfirstiosdevice();
            //    appconfig.defaultiosphone = selectphone.getphonebyudid(iosudid);
            //}
            //if (iosudid == null)
            //{
            //    output.immediately($"no iphone for test was set explicitly. auto-detection using 'idevice_id' failed as well. there is no iphone device!");
            //}

            if (iosUdid != null)
                AppConfig.DefaultIosPhone = new IosPhone(iosUdid);
        
            /****  else
                AppConfig.DefaultIosPhone = SelectPhone.Ios(IosPhoneName.Audifon_iPhone_7);    //HACK: Defaulting to audifon's iPhone 5s way for now. This is, of course, subject to change.
        ***/
            }

        [OneTimeSetUp]
        public void RunBeforeAllTestFixtures()
        {
            Output.Immediately("Starting OneTimeSetup");
            watch.Restart();
#if TEST_FROM_IDE
            FakeParametersForIdeUsage();
#else
            ProcessParameters();
#endif
            AppConfig.OutputDefaultConfig();       //Printing default config to console.

            ReportHelper.InitializeReport();

            Output.Immediately("Finished OneTimeSetup");
        }

        [OneTimeTearDown]
        public void RunAfterAllTestFixtures()
        {
            Output.Immediately("Starting OneTimeTearDown");
            AppManager.DisposeAppium();         //Just in case a test case / test fixture doesn't take care of that.

            ReportHelper.CloseReport();

            Output.Immediately("Finished OneTimeTearDown");
            Output.Immediately($"Finished all tests after {(int)watch.Elapsed.TotalSeconds} seconds");
        }
    }
}