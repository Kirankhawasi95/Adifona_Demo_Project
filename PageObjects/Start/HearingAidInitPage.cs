using System;
using System.Threading;
using HorusUITest.Helper;
using HorusUITest.PageObjects.Controls.Init;
using HorusUITest.PageObjects.Dialogs;
using OpenQA.Selenium.Appium;
using System.Threading.Tasks;
using HorusUITest.Enums;

namespace HorusUITest.PageObjects.Start
{
    public class HearingAidInitPage : BasePage
    {
        private AppiumWebElement Header => App.FindElementByAutomationId("Horus.Views.Start.HearingAidInitPage.InitializationPageHeader");
        private AppiumWebElement Description => App.FindElementByAutomationId("Horus.Views.Start.HearingAidInitPage.Description");
        private AppiumWebElement LeftHearingAidInitDisplay => App.FindElementByAutomationId("Horus.Views.Start.HearingAidInitPage.LeftHearingAidInitDisplay", verifyVisibility: true);
        private AppiumWebElement RightHearingAidInitDisplay => App.FindElementByAutomationId("Horus.Views.Start.HearingAidInitPage.RightHearingAidInitDisplay", verifyVisibility: true);
        private AppiumWebElement CancelButton => App.FindElementByAutomationId("Horus.Views.Start.HearingAidInitPage.CancelButton");

        public HearingAidInitPage(bool assertOnPage = true) : base(assertOnPage)
        {
            leftHearingAid = new HearingAidInitDisplay(() => LeftHearingAidInitDisplay);
            rightHearingAid = new HearingAidInitDisplay(() => RightHearingAidInitDisplay);
        }

        public HearingAidInitPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
            leftHearingAid = new HearingAidInitDisplay(() => LeftHearingAidInitDisplay);
            rightHearingAid = new HearingAidInitDisplay(() => RightHearingAidInitDisplay);
        }

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = () => Header,
            iOS = () => Header
        };

        HearingAidInitDisplay leftHearingAid;
        public HearingAidInitDisplay LeftHearingAid => leftHearingAid;

        HearingAidInitDisplay rightHearingAid;
        public HearingAidInitDisplay RightHearingAid => rightHearingAid;

        /// <summary>
        /// Continuously checks for Bluetooth connection requests and app dialogs to confirm. This method returns the next page after leaving (either <see cref="DashboardPage"/> when the connection was successful <see cref="HearingAidConnectionErrorPage"/> if the app's connection timeout occured).
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns>An instance of <see cref="DashboardPage"/> or <see cref="HearingAidConnectionErrorPage"/>, depending on the outcome of the connection attempt.</returns>
        public BasePage WaitForConnection(TimeSpan? timeout = null)
        {
            if (timeout == null)
                timeout = TimeSpan.FromSeconds(80);

            BasePage page = null;
            BasePage[] pages = PageHelper.CreateInstances(typeof(PermissionDialog), typeof(AppDialog), typeof(HearingAidInitPage), typeof(HearingAidConnectionErrorPage), typeof(FirstTimeProcessingPage), typeof(DashboardPage));
            Wait.UntilTrue(() =>
            {
                page = PageHelper.WaitForAnyPageDontThrow(TimeSpan.FromSeconds(25), pages);
                switch (page)
                {
                    case PermissionDialog permissionDialog:
                        permissionDialog.Allow();
                        break;
                    case AppDialog appDialog:
                        if (OnAndroid) //if on Android app Dialog can be confirmed 
                        {
                            appDialog.Confirm();
                        }
                        else
                        {
                            if (appDialog.GetNumberOfButtons() == 1) //app dialog and permission dialog can't be diffentiatedon iOS, so thee is a need to check if appDialog orpermission diolg is shown 
                                appDialog.Confirm();
                            else
                                new PermissionDialog().Allow();
                        }
                        break;
                    case HearingAidInitPage _:
                         PermissionHelper.AllowPermissionIfRequested<HearingAidInitPage>();
                         break;
                    case HearingAidConnectionErrorPage _:
                         return true;
                    case FirstTimeProcessingPage _:     //will automatically navigate to DashboardPage, just wait
                         PermissionHelper.AllowPermissionIfRequested<FirstTimeProcessingPage>();
                         //Task.Delay(2000);
                         break;
                    case DashboardPage _:
                         PermissionHelper.AllowPermissionIfRequested<DashboardPage>();
                         //Thread.Sleep(2000);
                         return true;
                    default:
                        Output.Immediately("unexpected page or unhandled notification on app start found");
                        break;
                }
                return false;
            }, timeout.Value, $"{nameof(HearingAidInitPage)} was stuck for {timeout.Value.TotalSeconds} seconds.");
            return page;
        }

        /// <summary>
        /// Taps the cancel button, which brings up an <see cref="AppDialog"/> for confirmation. If confirmed, the app will navigate to <see cref="HearingAidConnectionErrorPage"/>.
        /// </summary>
        public void Cancel()
        {
            App.Tap(CancelButton);
        }

        /// <summary>
        /// Taps the cancel button and confirms the <see cref="AppDialog"/>. Navigates to <see cref="HearingAidConnectionErrorPage"/>.
        /// </summary>
        public void CancelAndConfirm()
        {
            Cancel();
            new AppDialog().Confirm();
        }

        /// <summary>
        /// Check Main Logo Image 
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public bool CheckBrandImage(Brand brand)
        {
            string BrandName = Enum.GetName(typeof(Brand), brand);
            AppiumWebElement element = App.FindElementByImage("OEM\\" + BrandName + "\\main_logo.png");
            return element.Displayed;
        }

        /// <summary>
        /// Check Right HA Image before connecting (Light Grey)
        /// </summary>
        /// <returns></returns>
        public bool CheckRightHAImage()
        {
            AppiumWebElement element = App.FindElementByImage("HearingInstruments\\product_image_wings_hdo_right_light.png");
            return element.Displayed;
        }

        /// <summary>
        /// Check Left HA Image before connecting (Light Grey)
        /// </summary>
        /// <returns></returns>
        public bool CheckLeftHAImage()
        {
            AppiumWebElement element = App.FindElementByImage("HearingInstruments\\product_image_wings_hdo_left_light.png");
            return element.Displayed;
        }

        public string GetDescriptionText()
        {
            return Description.Text;
        }

        public bool IsReadingFromCache(string Message, TimeSpan? timeout = null)
        {
            timeout = timeout ?? TimeSpan.FromSeconds(60);
            return Wait.For(() => ContainsChachedText(Message), timeout.Value);
        }

        private bool ContainsChachedText(string Message)
        {
            return Description.Text.Contains(Message);
        }

        public bool WaitForMessage(string Message, TimeSpan? timeout = null)
        {
            timeout = timeout ?? TimeSpan.FromSeconds(60);
            return Wait.For(() => ContainsMessage(Message), timeout.Value);
        }

        private bool ContainsMessage(string Message)
        {
            return Description.Text.Contains(Message);
        }
    }
}
