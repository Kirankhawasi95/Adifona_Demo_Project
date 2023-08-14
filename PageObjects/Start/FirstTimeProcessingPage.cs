using System;
using HorusUITest.Helper;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Start
{
    /// <summary>
    /// Page that is shown before entering the <see cref="DashboardPage"/> when connecting with hearing aids for the first time.
    /// </summary>
    public class FirstTimeProcessingPage : BasePage
    {
        private AppiumWebElement Message => App.FindElementByAutomationId("Horus.Views.MainControlPage.FirstTimeProcessingMessage");

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = () => Message,
            iOS = () => Message
        };

        public FirstTimeProcessingPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public FirstTimeProcessingPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        public bool IsMessageAvailable(string ValidationMessage, TimeSpan? timeout = null)
        {
            timeout = timeout ?? TimeSpan.FromSeconds(60);
            return Wait.For(() => ContainsMessageText(ValidationMessage), timeout.Value);
        }

        private bool ContainsMessageText(string ValidationMessage)
        {
            return Message.Text.Contains(ValidationMessage);
        }
    }
}
