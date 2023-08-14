using System;
using System.Collections.Generic;
using System.Threading;
using HorusUITest.Extensions;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Interfaces;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Favorites.Automation
{
    public class AutomationGeofenceBindingPage : BaseProgramConfigPage, IHasLoadingIndicator<AutomationGeofenceBindingPage>
    {
        //TODO: Flesh out the map if more functionalities are required.

        protected override Func<IMobileElement<AppiumWebElement>> NavigationBarQuery => () => NavigationBar;
        protected override Func<IMobileElement<AppiumWebElement>> CancelButtonQuery => () => null;        //no cancel button on this page

        private const string NavigationBarAID = "Horus.Views.Favorites.Automation.AutomationGeofenceBindingPage.NavigationBar";
        private const string MapContainerAID = "Horus.Views.Favorites.Automation.AutomationGeofenceBindingPage.MapContainer";
        private const string LoadingIndicatorAID = "Horus.Views.Favorites.Automation.AutomationGeofenceBindingPage.CircularActivityIndicator";
        private const string AddressTitleAID = "Horus.Views.Favorites.Automation.AutomationGeofenceBindingPage.AddressTitle";
        private const string AddressDescriptionAID = "Horus.Views.Favorites.Automation.AutomationGeofenceBindingPage.AddressDescription";
        private const string OkButtonAID = "Horus.Views.Favorites.Automation.AutomationGeofenceBindingPage.OkButton";
        private const string ActualOkBlindButtonAID = "Horus.Views.Favorites.Automation.AutomationGeofenceBindingPage.ActualOkBlindButton";

        [FindsByAndroidUIAutomator(Accessibility = NavigationBarAID), FindsByIOSUIAutomation(Accessibility = NavigationBarAID)]
        private IMobileElement<AppiumWebElement> NavigationBar { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = MapContainerAID), FindsByIOSUIAutomation(Accessibility = MapContainerAID)]
        private IMobileElement<AppiumWebElement> MapContainer { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = LoadingIndicatorAID), FindsByIOSUIAutomation(Accessibility = LoadingIndicatorAID)]
        private IMobileElement<AppiumWebElement> LoadingIndicator { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = AddressTitleAID), FindsByIOSUIAutomation(Accessibility = AddressTitleAID)]
        private IMobileElement<AppiumWebElement> AddressTitle { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = AddressDescriptionAID), FindsByIOSUIAutomation(Accessibility = AddressDescriptionAID)]
        private IMobileElement<AppiumWebElement> AddressDescription { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = ActualOkBlindButtonAID), FindsByIOSUIAutomation(Accessibility = ActualOkBlindButtonAID)]
        private IMobileElement<AppiumWebElement> ActualOkBlindButton { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = OkButtonAID), FindsByIOSUIAutomation(Accessibility = OkButtonAID)]
        private IMobileElement<AppiumWebElement> OkButton { get; set; }

        [FindsByAndroidUIAutomator(ClassName = "android.widget.ImageView")]
        private IList<AppiumWebElement> AndroidMapButtons { get; set; }

        private AppiumWebElement AndroidMyLocationButton => AndroidMapButtons[0];
        private AppiumWebElement AndroidZoomInButton => AndroidMapButtons[1];
        private AppiumWebElement AndroidZoomOutButton => AndroidMapButtons[2];

        private CircularActivityIndicator<AutomationGeofenceBindingPage> loadingIndicator;

        public AutomationGeofenceBindingPage(bool assertOnPage = true) : base(assertOnPage)
        {
            loadingIndicator = new CircularActivityIndicator<AutomationGeofenceBindingPage>(this);
        }

        public AutomationGeofenceBindingPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
            loadingIndicator = new CircularActivityIndicator<AutomationGeofenceBindingPage>(this);
        }

        /// <summary>
        /// Taps the button 'Ok' and navigates to <see cref="ProgramAutomationPage"/>.
        /// </summary>
        public void Ok()
        {
            App.Tap(OkButton);
        }

        /// <summary>
        /// Returns the text of the Ok button.
        /// </summary>
        /// <returns></returns>
        public string GetOkButtonText()
        {
            return OkButton.GetTextOrNull();
        }

        public bool GetIsOkButtonEnabled()
        {
            return ActualOkBlindButton.Enabled;
        }

        public string GetAddressTitle()
        {
            return AddressTitle.GetTextOrNull();
        }

        public string GetAddressDescription()
        {
            return AddressDescription.GetTextOrNull();
        }

        /// <summary>
        /// Selectes the position given by percental coordinates relative to the map's size.
        /// This is only a MVP-method until more sophisticated means of navigation are implemented.
        /// </summary>
        /// <param name="percentX">Horizontal percentage: 0 = Left, 1 = Right.</param>
        /// <param name="percentY">Vertical percentage: 0 = Top, 1 = Bottom.</param>
        /// <returns></returns>
        public AutomationGeofenceBindingPage SelectPosition(double percentX, double percentY)
        {
            App.Tap(MapContainer, percentX, percentY);
            Thread.Sleep(1000);
            return this;
        }

        public bool GetIsLoadingIndicatorVisible()
        {
            return loadingIndicator.GetIsLoadingIndicatorVisible();
        }

        public AutomationGeofenceBindingPage WaitUntilNoLoadingIndicator(TimeSpan? timeout = null)
        {
            return loadingIndicator.WaitUntilNoLoadingIndicator(timeout);
        }

        public AutomationGeofenceBindingPage ANDROID_ONLY_GoToMyLocation()
        {
            App.Tap(App.WaitForElement(() => AndroidMyLocationButton));
            Thread.Sleep(3000); //Animation
            return this;
        }
    }
}
