using System;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu.Settings
{
    //This is a dev-only page. Respecting that, no effort went into finishing this page.
    //This page can be navigated to (including verification), but should generally be considered non-functional.

    [Obsolete("Non-functional development / debug page.")]
    public class LogPage : BaseNavigationPage
    {
        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Menu.Settings.LogPage.NavigationBar");

        private AppiumWebElement Editor => App.FindElementByAutomationId("Horus.Views.Menu.Settings.LogPage.Editor");
        private AppiumWebElement ClearLogButton => App.FindElementByAutomationId("Horus.Views.Menu.Settings.LogPage.ClearLogButton");
        private AppiumWebElement SendByMailButton => App.FindElementByAutomationId("Horus.Views.Menu.Settings.LogPage.SendByMailButton");

        public LogPage(bool assertOnPage = true) : base(assertOnPage)
        {

        }

        public LogPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {

        }

        /// <summary>
        /// Method for getting Log Text
        /// </summary>
        /// <returns></returns>
        public string GetLogText()
        {
            return Editor.Text;
        }

        /// <summary>
        /// Clear Log Text
        /// </summary>
        public void ClearLog()
        {
            ClearLogButton.Click();
        }
        public bool GetIsClearLogButtonVisible()
        {
            return ClearLogButton.Displayed;
        }
        /// <summary>
        /// Send Log Text in Email
        /// </summary>
        public void SendLogByEmail()
        {
            SendByMailButton.Click();
        }
        public bool GetIsSendLogByEmailButtonVisible()
        {
            return SendByMailButton.Displayed;
        }
    }
}