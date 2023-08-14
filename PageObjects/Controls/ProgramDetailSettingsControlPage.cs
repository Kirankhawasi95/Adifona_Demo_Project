using System;
using System.Drawing;
using HorusUITest.Extensions;
using HorusUITest.PageObjects.Controls.Interfaces;
using HorusUITest.PageObjects.Dialogs;
using HorusUITest.PageObjects.Favorites;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Controls
{
    public class ProgramDetailSettingsControlPage : BaseRootedFactoryPage, INavigationBar
    {
        protected override Func<IMobileElement<AppiumWebElement>> RootQuery => () => App.FindElementByAutomationId("Horus.Views.MainControlPage.ProgramDetailSettingsControl", verifyVisibility: true);

        private const string NavigationBarAID = "Horus.Views.Controls.ProgramDetailSettingsControl.NavigationBar";
        private const string DescriptionAID = "Horus.Views.Controls.ProgramDetailSettingsControl.Description";
        private const string ChangeNameAID = "Horus.Views.Controls.ProgramDetailSettingsControl.ChangeName";
        private const string ChangeIconAID = "Horus.Views.Controls.ProgramDetailSettingsControl.ChangeIcon";
        private const string AutoStartAID = "Horus.Views.Controls.ProgramDetailSettingsControl.AutoStart";
        private const string AutoStartEnabledAID = "Horus.Views.Controls.ProgramDetailSettingsControl.AutoStartEnabled";
        private const string ResetProgramAID = "Horus.Views.Controls.ProgramDetailSettingsControl.ResetProgram";
        private const string CreateFavoriteAID = "Horus.Views.Controls.ProgramDetailSettingsControl.CreateFavorite";
        private const string DeleteFavoriteAID = "Horus.Views.Controls.ProgramDetailSettingsControl.DeleteFavorite";
        private const string CreateFavoriteFloatingButtonAID = "Horus.Views.Controls.ProgramDetailSettingsControl.CreateFavoriteFloatingButton";
        private AppiumWebElement SnackBar => App.FindElementById("com.audifon.horus:id/snackbar_text");

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = NavigationBarAID), FindsByIOSUIAutomation(Accessibility = NavigationBarAID)]
        private IMobileElement<AppiumWebElement> NavigationBarElement { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = DescriptionAID), FindsByIOSUIAutomation(Accessibility = DescriptionAID)]
        private IMobileElement<AppiumWebElement> Description { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = ChangeNameAID), FindsByIOSUIAutomation(Accessibility = ChangeNameAID)]
        private IMobileElement<AppiumWebElement> ChangeNameElement { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = ChangeIconAID), FindsByIOSUIAutomation(Accessibility = ChangeIconAID)]
        private IMobileElement<AppiumWebElement> ChangeIconElement { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = AutoStartAID), FindsByIOSUIAutomation(Accessibility = AutoStartAID)]
        private IMobileElement<AppiumWebElement> AutoStartElement { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = AutoStartEnabledAID), FindsByIOSUIAutomation(Accessibility = AutoStartEnabledAID)]
        private IMobileElement<AppiumWebElement> AutoStartEnabledElement { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = ResetProgramAID), FindsByIOSUIAutomation(Accessibility = ResetProgramAID)]
        private IMobileElement<AppiumWebElement> ResetProgramElement { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = CreateFavoriteAID), FindsByIOSUIAutomation(Accessibility = CreateFavoriteAID)]
        private IMobileElement<AppiumWebElement> CreateFavoriteElement { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = DeleteFavoriteAID), FindsByIOSUIAutomation(Accessibility = DeleteFavoriteAID)]
        private IMobileElement<AppiumWebElement> DeleteFavoriteElement { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = CreateFavoriteFloatingButtonAID), FindsByIOSUIAutomation(Accessibility = CreateFavoriteFloatingButtonAID)]
        private IMobileElement<AppiumWebElement> CreateFavoriteFloatingButtonElement { get; set; }

        //INavigationBar navigationBar;
        private FactoryNavigationBar navigationBar;

        public ProgramDetailSettingsControlPage(bool assertOnPage = true) : base(assertOnPage)
        {
            navigationBar = new FactoryNavigationBar(NavigationBarElement);
        }

        public ProgramDetailSettingsControlPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
            navigationBar = new FactoryNavigationBar(NavigationBarElement);
        }

        public string GetDescription()
        {
            return Description.Text;
        }

        /// <summary>
        /// Create Favorite floating button is not present on ios app
        /// </summary>
        /// <returns></returns>
        public bool GetIsCreateFavoriteFloatingButtonDisplayed() 
        {
            if (OnAndroid)
                return CreateFavoriteFloatingButtonElement.Exists();
            else
                return true;
        }
        public void CreateFavoriteFloatingButton()
        {
            if (OnAndroid)
                App.Tap(CreateFavoriteFloatingButtonElement);
            else
                App.Tap(CreateFavoriteElement);
        }


        public bool GetIsCustomizeNameVisible() { return GetCustomizeNameText() != null; }
        public bool GetIsCustomizeIconVisible() { return GetCustomizeIconText() != null; }
        public bool GetIsAutoStartVisible() { return GetAutoStartText() != null; }
        public bool GetIsAutoStartEnabled() { return GetAutoStartEnabledText() != null; }
        public bool GetIsResetProgramVisible() { return GetResetProgramText() != null; }
        public bool GetIsCreateFavoriteVisible() { return GetCreateFavoriteText() != null; }
        public bool GetIsDeleteFavoriteVisible() { return GetDeleteFavoriteText() != null; }
        public bool GetIsSnackBarVisible() { return GetSnackBarText() != null; }
        public string GetCustomizeNameText() { return ChangeNameElement.Text; }
        public string GetCustomizeIconText() { return ChangeIconElement.Text; }
        public string GetAutoStartText() { return AutoStartElement.GetTextOrNull(); }
        public string GetAutoStartEnabledText() { return AutoStartEnabledElement.GetTextOrNull(); }
        public string GetResetProgramText() { return ResetProgramElement.GetTextOrNull(); }
        public string GetCreateFavoriteText() { return CreateFavoriteElement.GetTextOrNull(); }
        public string GetDeleteFavoriteText() { return DeleteFavoriteElement.GetTextOrNull(); }
        public string GetSnackBarText()
        {
            CreateFavorite();
            try
            {
                 return App.WaitForElement(() => SnackBar).Text;
                //return SnackBar.Text;
            }
            catch (NoSuchElementException e)
            {
                throw new NoSuchElementException("Snack Bar could not be located. Appears only appears if we try to create more than 4 favorites", e);
            };
        }

        /// <summary>
        /// Navigates to <see cref="ProgramNamePage"/>.
        /// </summary>
        public void CustomizeName() { App.Tap(ChangeNameElement); }

        /// <summary>
        /// Navigates to <see cref="ProgramIconPage"/>.
        /// </summary>
        public void CustomizeIcon() { App.Tap(ChangeIconElement); }

        /// <summary>
        /// Navigates to <see cref="ProgramAutomationPage"/>.
        /// </summary>
        public void SetAutoStart() { App.Tap(AutoStartElement); }

        /// <summary>
        /// Presses the "Reset Program" button, thereby prompting an <see cref="AppDialog"/> for confirmation.
        /// </summary>
        public void ResetProgram() { App.Tap(ResetProgramElement); }

        /// <summary>
        /// Resets the program and navigates to <see cref="ProgramDetailPage"/>.
        /// </summary>
        public void ResetProgramAndConfirm()
        {
            ResetProgram();
            new AppDialog().Confirm();
        }

        /// <summary>
        /// Navigates to <see cref="ProgramNamePage"/>.
        /// </summary>
        public void CreateFavorite() { App.Tap(CreateFavoriteElement); }

        /// <summary>
        /// Navigates to <see cref="ProgramAutomationPage"/>.
        /// </summary>
        public void OpenAutoHearingProgramStart() { App.Tap(AutoStartElement); }
        
        /// <summary>
        /// Presses the "Delete Favorite" button, thereby prompting an <see cref="AppDialog"/> for confirmation.
        /// </summary>
        public void DeleteFavorite() { App.Tap(DeleteFavoriteElement); }

        /// <summary>
        /// Deletes the favorite and navigates to <see cref="ProgramDetailPage"/>.
        /// </summary>
        public void DeleteFavoriteAndConfirm()
        {
            DeleteFavorite();
            new AppDialog().Confirm();
        }

        public void TapBack()
        {
            navigationBar.TapBack();
        }

        public void TapRightIcon()
        {
            navigationBar.TapRightIcon();
        }

        public void SwipeBack()
        {
            navigationBar.SwipeBack();
        }

        /// <summary>
        /// On this page the close icon for iOS is on right and for android is left
        /// </summary>
        public void NavigateBack()
        {
            // Earlier version of IOS Audifon App had close button on top right. So it was handled with condition. Not both Android and IOS App has been normalized 
            //if (OnAndroid)
            //    TapBack();
            //else
            //    CloseProgramDetailSettingsControlPage();

            TapBack();
        }

        public string GetNavigationBarTitle()
        {
            return navigationBar.GetNavigationBarTitle();
        }

        public void CloseProgramDetailSettingsControlPage()
        {
            navigationBar.TapRightIcon();
        }

        public bool GetIsBackButtonDisplayed()
        {
            return navigationBar.GetIsBackButtonDisplayed();
        }

        public bool CheckForAutoHearingProgramStartOverlapping()
        {
            Rectangle rectangleAutoStart = AutoStartElement.GetRect();
            Rectangle rectangleEnabled = AutoStartEnabledElement.GetRect();

            var overlap = !(rectangleAutoStart.Right < rectangleEnabled.Left ||
                rectangleAutoStart.Left > rectangleEnabled.Right ||
                rectangleAutoStart.Bottom < rectangleEnabled.Top ||
                rectangleAutoStart.Top > rectangleEnabled.Bottom);

            return overlap;
        }
    }
}
