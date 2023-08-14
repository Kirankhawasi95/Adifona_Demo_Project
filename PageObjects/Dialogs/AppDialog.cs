using System;
using System.Linq;
using HorusUITest.Configuration;

using HorusUITest.Helper;
using HorusUITest.Data;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Dialogs
{
    public class AppDialog : BaseDialog
    {
        protected override AppiumWebElement AndroidTitle => App.FindElementById(AppManager.AppPackageName + ":id/alertTitle");
        private AppiumWebElement AndroidMessage => App.FindElementById("android:id/message");
        private AppiumWebElement AndroidButton1 => App.FindElementById("android:id/button1");
        private AppiumWebElement AndroidButton2 => App.FindElementById("android:id/button2");

    

        public AppDialog(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public AppDialog(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected AppiumWebElement Title
        {
            get
            {
                if (OnAndroid)
                    return AndroidTitle;
                if (OniOS)
                    return TextsCache.ElementAt(0);
                throw new PlatformNotSupportedException("Invalid platform.");
            }
        }

      
        protected AppiumWebElement Message
        {
            get
            {
                if (OnAndroid)
                    return AndroidMessage;
                if (OniOS)
                    return TextsCache.ElementAt(1);
                throw new PlatformNotSupportedException("Invalid platform.");
            }
        }

        protected AppiumWebElement ConfirmButton
        {
            get
            {
               if (OnAndroid)
                    return AndroidButton1;
                if (OniOS)
                {
                    switch (ButtonsCache.Count)
                    {
                        case 1:
                            return ButtonsCache.ElementAt(0);
                        case 2:
                            return ButtonsCache.ElementAt(1);
                        case 3:
                            return ButtonsCache.ElementAt(0);
                        case 4:
                            return ButtonsCache.ElementAt(1);
                        default:
                            break;
                    }
                }
                throw new PlatformNotSupportedException("Invalid platform.");
            }
        }

        protected AppiumWebElement DenyButton
        {
            get
            {
                if (OnAndroid)
                    return AndroidButton2;
                if (OniOS)
                    return ButtonsCache.ElementAt(0);
                throw new PlatformNotSupportedException("Invalid platform.");
            }
        }

        public void Confirm()
        {
            App.Tap(ConfirmButton);
            this.IsGoneBeforeTimeout(Env.DIALOG_GONE_MAXDURATION);
            ClearCache();
        }

        public void Deny()
        {
            App.Tap(DenyButton);
            this.IsGoneBeforeTimeout(Env.DIALOG_GONE_MAXDURATION);
            ClearCache();
        }

        public string GetTitle()
        {
            return Title.Text;
        }

        public string GetMessage()
        {
            return Message.Text;
        }

        public string GetConfirmButtonText()
        {
            return ConfirmButton.Text;
        }

        public string GetDenyButtonText()
        {
            return DenyButton.Text;
        }
        public bool GetIsDenyButtonVisible()
        {
            try
            {
                return DenyButton.Displayed;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
