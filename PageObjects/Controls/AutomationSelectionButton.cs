using HorusUITest.Extensions;
using HorusUITest.PageObjects.Dialogs;
using HorusUITest.PageObjects.Favorites.Automation;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Controls
{
    public class AutomationSelectionButton<T> : BaseFactoryControl
    {
        private const string IconAID = "Horus.Views.Controls.AutomationSelectionButton.Icon";
        private const string TitleAID = "Horus.Views.Controls.AutomationSelectionButton.Title";
        private const string ValueAID = "Horus.Views.Controls.AutomationSelectionButton.Value";
        private const string DeleteButtonAID = "Horus.Views.Controls.AutomationSelectionButton.DeleteButton";
        private const string SettingsButtonAID = "Horus.Views.Controls.AutomationSelectionButton.SettingsButton";

        [FindsByAndroidUIAutomator(Accessibility = TitleAID), FindsByIOSUIAutomation(Accessibility = TitleAID)]
        private IMobileElement<AppiumWebElement> Title { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = ValueAID), FindsByIOSUIAutomation(Accessibility = ValueAID)]
        private IMobileElement<AppiumWebElement> Value { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = DeleteButtonAID), FindsByIOSUIAutomation(Accessibility = DeleteButtonAID)]
        private IMobileElement<AppiumWebElement> DeleteButton { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = SettingsButtonAID), FindsByIOSUIAutomation(Accessibility = SettingsButtonAID)]
        private IMobileElement<AppiumWebElement> SettingsButton { get; set; }
        private T page;

        public AutomationSelectionButton(T page, IMobileElement<AppiumWebElement> automationSelectionButtonElement) : base(automationSelectionButtonElement)
        {
            this.page = page;
        }

        /// <summary>
        /// Returns ture/false based the trash icon visibility.
        /// Unable to find this element on ios app
        /// </summary>
        /// <returns></returns>
        public bool GetIsDeleteIconVisible()
        {
            if (OnAndroid)
                return TryInvokeQuery(() => DeleteButton, out _);
            else
                return true;
        }
        /// <summary>
        /// Returns ture/false based the settings(gear icon) visibility.
        /// Unable to find this element on ios app
        /// </summary>
        /// <returns></returns>
        public bool GetIsSettingsIconVisible()
        {
            if (OnAndroid)
                return TryInvokeQuery(() => SettingsButton, out _);
            else
                return true;
        }
        /// <summary>
        /// Returns the title of the automation option, e.g. "Connect with place".
        /// </summary>
        /// <returns></returns>
        public string GetTitle()
        {
            return Title.Text;
        }

        /// <summary>
        /// Returns the value of the automation option, e.g. the SSID of a wireless network.
        /// </summary>
        /// <returns></returns>
        public string GetValue()
        {
            return Value.GetTextOrNull();
        }

        /// <summary>
        /// Returns whether or not the automation option is set to a value.
        /// </summary>
        /// <returns></returns>
        public bool GetIsValueSet()
        {
            return GetValue() != null;
        }

        /// <summary>
        /// Deletes the value. Throws if no value is set.
        /// </summary>
        /// <returns></returns>
        public T DeleteValue()
        {
            App.Tap(DeleteButton);
            return page;
        }

        /// <summary>
        /// Navigates to <see cref="AutomationWifiBindingPage"/> or <see cref="AutomationGeofenceBindingPage"/> respectively, depending on the automation option used.
        /// Note: Depending on the granted permissions, an <see cref="AppDialog"/> and / or <see cref="PermissionDialog"/> may be shown before.
        /// </summary>
        public void OpenSettings()
        {
            App.Tap(Title);
        }
    }
}
