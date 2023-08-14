using System;
using System.Linq;
using HorusUITest.Configuration;
using HorusUITest.Extensions;
using OpenQA.Selenium.Appium;
using HorusUITest.Helper;
using NUnit.Framework;


namespace HorusUITest.PageObjects.Dialogs
{
    public class PermissionDialog : BaseDialog
    {
       public PermissionDialog(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public PermissionDialog(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override AppiumWebElement AndroidTitle
        {
            get
            {
                AppiumWebElement element = App.FindElementByIdDontThrow("com.android.permissioncontroller:id/permission_message");      //Android 10
                return element.Exists() ? element : App.FindElementById("com.android.packageinstaller:id/permission_message");          //Android 7 bis Android 9
            }
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
        
        protected AppiumWebElement ButtonAllow
        {
            get
            {
                if (OnAndroid)
                {
                    AppiumWebElement element = App.FindElementByIdDontThrow("com.android.packageinstaller:id/permission_allow_button");                     //Android 7 bis Android 9
                    if (!element.Exists()) element = App.FindElementByIdDontThrow("com.android.permissioncontroller:id/permission_allow_always_button");    //Android 10 always
                    if (!element.Exists()) element = App.FindElementByIdDontThrow("com.android.permissioncontroller:id/permission_allow_button");           //Android 10
                    return element.Exists() ? element : App.FindElementById("com.android.permissioncontroller:id/permission_allow_foreground_only_button"); //Android 10 foreground only
                }
                else if (OniOS)
                {
                    string checkButtonText = "";
                    switch (ButtonsCache.Count)
                    {
                        case 3:
                            for (int i = 0; i < ButtonsCache.Count; i++)
                            {
                                var actualElement = ButtonsCache.ElementAt(i);
                                checkButtonText= actualElement.GetTextOrEmptyString();
                                if(checkButtonText == "Allow While Using App") 
                                    return ButtonsCache.ElementAt(i);
                            }
                            break;
                        case 2:
                            var firstElement = ButtonsCache.ElementAt(0);
                            var lastElement = ButtonsCache.ElementAt(ButtonsCache.Count - 1);
                            var firstRect = firstElement.Rect;
                            var lastRect = lastElement.Rect;
                            if (lastRect.X - firstRect.X < lastRect.Y - firstRect.Y)
                            return lastElement;
                            else
                            {
                                checkButtonText = lastElement.GetTextOrEmptyString();
                            if (checkButtonText == "OK")
                                return lastElement;
                            else
                            return firstElement;
                        }
                        default:
                            PermissionHelper.DismissAllDialogsAndPermissionRequests(TimeSpan.FromSeconds(3));
                            break;
                    }
                }throw new PlatformNotSupportedException("Invalid platform.");
            }
        }

        protected AppiumWebElement ButtonDeny
        {
            get
            {
                if (OnAndroid)
                {
                    AppiumWebElement element = App.FindElementByIdDontThrow("com.android.permissioncontroller:id/permission_deny_button");          //Android 10
                    return element.Exists() ? element : App.FindElementById("com.android.packageinstaller:id/permission_deny_button");              //Android 7 to Android 9
                }
                else if (OniOS)
                {
                    if (ButtonsCache.Count == 3)
                        return ButtonsCache.ElementAt(2);   //Vertical arrangement, e.g. location: "Allow While Using app" -> "Allow Once" -> "Don't Allow"
                    else
                        return ButtonsCache.ElementAt(0);   //Horizontal arrangement, e.g. Bluetooth: "Don't Allow" -> "OK"
                }
                throw new PlatformNotSupportedException("Invalid platform.");
            }
        }

        public string GetTitle()
        {
            return Title.Text;
        }

        public string GetAllowText()
        {
            return ButtonAllow.Text;
        }

        public string GetDenyText()
        {
            return ButtonDeny.Text;
        }

        public void Allow()
        {
            if (ButtonsCache.Count > 0)
            {
                App.Tap(ButtonAllow);
                this.IsGoneBeforeTimeout(Env.DIALOG_GONE_MAXDURATION);
            }
            else return;
        }

        public void Deny()
        {
            App.Tap(ButtonDeny);
            this.IsGoneBeforeTimeout(Env.DIALOG_GONE_MAXDURATION);
        }
    }
}
