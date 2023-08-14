using HorusUITest.Data;
using HorusUITest.Helper;
using NUnit.Framework;
using Platform = HorusUITest.Enums.Platform;

namespace HorusUITest.TestFixtures
{
    public abstract class BaseResettingTestFixture : BaseTestFixture
    {
        public BaseResettingTestFixture(Platform platform) : base(platform)
        {
        }

        [OneTimeSetUp]
        public override void BeforeAll()
        {
            base.BeforeAll();
            // Condition to install from store only once. A configuration key which is set to false needs to be made true for this functionality
            if (AppConfig.DefaultInstallFromStore && ReportHelper.TestCaseNames.Count == 0)
                AppManager.InstallAppFromStore(new AppConfig(CurrentPlatform));
            AppManager.InitializeApp(new AppConfig(CurrentPlatform));
            AppManager.DeviceSettings.EnableBluetooth();
            AppManager.CloseApp();
        }

        [SetUp]
        public override void BeforeEachTest()
        {
            base.BeforeEachTest();
            AppManager.StartApp(true);
        }

        [TearDown]
        public override void AfterEachTest()
        {
            base.AfterEachTest();

            // Closing the App
            AppManager.CloseApp();
            ReportHelper.LogTest(AventStack.ExtentReports.Status.Info, "App Closed");
        }

        public override void AfterAll()
        {
            base.AfterAll();
            AppManager.FinishAndCleanUp();
            AppManager.DisposeAppium();
        }
    }
}