using System;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Start
{
    /// <summary>
    /// Allows scanning for hearing aids or starting the demo mode.
    /// </summary>
    public class InitializeHardwarePage : BasePage
    {
        private AppiumWebElement demoModeButton => App.FindElementByAutomationId("Horus.Views.Start.InitializeHardwarePage.DemoModeButton", verifyVisibility: true);
        private AppiumWebElement scanButton => App.FindElementByAutomationId("Horus.Views.Start.InitializeHardwarePage.ScanButton");

        public InitializeHardwarePage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public InitializeHardwarePage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = () => demoModeButton,
            iOS = () => demoModeButton
        };

        //TODO: Automate scanning for hearing aids.
        /// <summary>
        /// Navigates to <see cref="SelectHearingAidsPage"/>.
        /// </summary>
        public void StartScan()
        {
            App.Tap(scanButton);
        }

        /// <summary>
        /// Get Scan Text
        /// </summary>
        /// <returns></returns>
        public string GetScanText()
        {
            return scanButton.Text;
        }

        /// <summary>
        /// Get Demo Mode Text
        /// </summary>
        /// <returns></returns>
        public string GetDemoModeText()
        {
            return demoModeButton.Text;
        }

        /// <summary>
        /// Navigates to <see cref="DashboardPage"/>.
        /// </summary>
        public void StartDemoMode()
        {
            App.Tap(demoModeButton);
        }
    }
}
