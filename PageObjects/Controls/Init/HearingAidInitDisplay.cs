using System;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Controls.Init
{
    public class HearingAidInitDisplay : BasePageObject
    {
        private AppiumWebElement Side => App.FindElementByAutomationId("Horus.Views.Controls.Init.HearingAidInitDisplay.Side", rootQuery.Invoke());
        private AppiumWebElement LoadingIndicator => App.FindElementByAutomationId("Horus.Views.Controls.Init.HearingAidInitDisplay.CircularActivityIndicator", rootQuery.Invoke(), true);
        private AppiumWebElement ConnectedIndicator => App.FindElementByAutomationId("Horus.Views.Controls.Init.HearingAidInitDisplay.ConnectedIndicator", rootQuery.Invoke(), true);

        private readonly Func<AppiumWebElement> rootQuery;

        public HearingAidInitDisplay(Func<AppiumWebElement> rootQuery)
        {
            this.rootQuery = rootQuery;
        }

        public bool IsVisible => TryInvokeQuery(rootQuery, out _);

        public bool IsConnecting => TryInvokeQuery(() => LoadingIndicator, out _);

        public bool IsConnected => TryInvokeQuery(() => ConnectedIndicator, out _);

        public string SideText => Side.Text;
    }
}
