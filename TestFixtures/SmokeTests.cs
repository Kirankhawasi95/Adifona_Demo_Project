using System;
using HorusUITest.Data;
using HorusUITest.Helper;
using HorusUITest.PageObjects;
using NUnit.Framework;
using Platform = HorusUITest.Enums.Platform;

namespace HorusUITest.TestFixtures
{
    public class SmokeTests : BaseTestFixture
    {
        public SmokeTests(Platform platform)
            : base(platform)
        {
        }

        public override void BeforeAll()
        {
            base.BeforeAll();
            AppManager.InitializeApp(new AppConfig(CurrentPlatform));
        }

        public override void AfterAll()
        {
            base.AfterAll();
            AppManager.CloseApp();
            AppManager.FinishAndCleanUp();
        }

        [Test, Order(0)]
        [Category("SmokeTest")]
        public void AppCanStartInDemoMode()
        {
            AppManager.CloseApp();
            AppManager.StartApp(true);
            Assert.DoesNotThrow(() => LaunchHelper.StartAppInDemoModeByAnyMeans(), "App could not be launched to the dashboard.");
        }

        [Test, Order(1)]
        [Category("SmokeTest")]
        public void AppCanRestartInDemoMode()
        {
            DashboardPage dashboardPage = null;
            Assume.That<Action>(() =>
            {
                try
                {
                    Assert.That(() => PageHelper.GetIsPageShown(out dashboardPage), $"Test case didn't start on {nameof(DashboardPage)}.");
                }
                catch
                {
                    AppCanStartInDemoMode();
                    dashboardPage = new DashboardPage(false);
                }
            }, Throws.Nothing, $"Test case didn't start on {nameof(DashboardPage)}. The attempt to navigate to the page failed as well.");
            Assume.That(dashboardPage, Is.Not.Null, $"Internal test automation error. {nameof(dashboardPage)} was not initialized after preconditions check.");
            AppManager.RestartApp(false);
            Assert.IsTrue(dashboardPage.IsGoneBeforeTimeout(TimeSpan.FromSeconds(5)), "Unable to restart the app,");
            Assert.DoesNotThrow(() => LaunchHelper.StartAppUsingExistingConfigData(), "App did not launch back to the dashboard after restarting.");
        }
    }
}
