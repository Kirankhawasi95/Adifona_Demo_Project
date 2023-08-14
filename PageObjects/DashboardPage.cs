using System;
using System.Collections.Generic;
using System.Threading;
using AventStack.ExtentReports;
using HorusUITest.Enums;
using HorusUITest.Helper;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Interfaces;
using HorusUITest.PageObjects.Menu;
using HorusUITest.PageObjects.Menu.Programs;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;

namespace HorusUITest.PageObjects
{
    public class DashboardPage : MainControlPage<DashboardPage>, IHasLoadingIndicator<DashboardPage>
    {
        protected override Func<IMobileElement<AppiumWebElement>> TraitQuery => () => CurrentProgramName;

        private const string HeaderAID = "Horus.Views.MainControlPage.ctrlExtendedHeader";
        private const string CurrentProgramNameAID = "Horus.Views.MainControlPage.ctrlProgramTitleHome";
        private const string ProgramSelectionAID = "Horus.Views.MainControlPage.ctrlProgramSelection";
        private const string HasProgramParamsLoadedAID = "Horus.Views.MainControlPage.HasProgramParamsLoaded";
        private const string DemoModeLabelAID = "Horus.Views.MainControlPage.DemoModeLabel";

        //[FindsByAndroidUIAutomator(Accessibility = HeaderAID), FindsByIOSUIAutomation(Accessibility = HeaderAID)]
        //protected IMobileElement<AppiumWebElement> Header { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = CurrentProgramNameAID), FindsByIOSUIAutomation(Accessibility = CurrentProgramNameAID)]
        protected IMobileElement<AppiumWebElement> CurrentProgramName { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = ProgramSelectionAID), FindsByIOSUIAutomation(Accessibility = ProgramSelectionAID)]
        private IMobileElement<AppiumWebElement> ProgramSelection { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = HasProgramParamsLoadedAID), FindsByIOSUIAutomation(Accessibility = HasProgramParamsLoadedAID)]
        private IMobileElement<AppiumWebElement> HasProgramParamsLoaded { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = DemoModeLabelAID), FindsByIOSUIAutomation(Accessibility = DemoModeLabelAID)]
        private IMobileElement<AppiumWebElement> DemoModeLabel { get; set; }
        private IMobileElement<AppiumWebElement> SnackBar => App.FindElementById("com.audifon.horus:id/snackbar_text");

        private CircularActivityIndicator<DashboardPage> loadingIndicator;

        private ProgramSelectionDisplay<DashboardPage> programSelector;
        private ProgramSelectionDisplay<DashboardPage> ProgramSelector
        {
            get
            {
                programSelector = programSelector ?? new ProgramSelectionDisplay<DashboardPage>(this, ProgramSelection);
                return programSelector;
            }
        }

        protected override void ClearCache()
        {
            base.ClearCache();
            programSelector = null;
        }

        public DashboardPage(bool assertOnPage = true) : base(assertOnPage)
        {
            loadingIndicator = new CircularActivityIndicator<DashboardPage>(this);
        }

        public DashboardPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
            loadingIndicator = new CircularActivityIndicator<DashboardPage>(this);
        }

        public bool GetIsProgramInitFinished()
        {
            var text = HasProgramParamsLoaded.Text;
            switch (text)
            {
                case "True":
                    return true;
                case "False":
                    return false;
                default:
                    throw new Exception($"Invalid status of 'HasProgramParamsLoaded': {text}");
            }
        }

        /// <summary>
        /// Waits until the program parameters are loaded. This method should be called after restarting the app with real devices connected,
        /// as the parameters must be retrieved from the devices before changing or opening hearing programs is allowed.
        /// Timeout defaults to 60 seconds.
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public DashboardPage WaitUntilProgramInitFinished(TimeSpan? timeout = null)
        {
            timeout = timeout ?? TimeSpan.FromSeconds(60);
            Wait.UntilTrue(GetIsProgramInitFinished, timeout.Value);
            if (OniOS)
                Thread.Sleep(2500); //Popup message blocks any interaction for 2 seconds on iOS. 0.5 seconds added to account for fade-out.
                                    //  PermissionHelper.AllowPermissionIfRequested<DashboardPage>();
            return this;
        }

        public override string GetCurrentProgramName()
        {
            return CurrentProgramName.Text;
        }

        public string GetDemoModeLabelText()
        {
            return DemoModeLabel.Text;
        }

        public bool GetIsLoadingIndicatorVisible()
        {
            return loadingIndicator.GetIsLoadingIndicatorVisible();
        }

        public DashboardPage WaitUntilNoLoadingIndicator(TimeSpan? timeout = null)
        {
            return loadingIndicator.WaitUntilNoLoadingIndicator(timeout);
        }

        public int GetNumberOfPrograms()
        {
            return ProgramSelector.GetNumberOfPrograms();
        }

        public string GetProgramIcon(int index)
        {
            return ProgramSelector.GetProgramIcon(index);
        }

        public List<string> GetAllProgramIcons()
        {
            return ProgramSelector.GetAllProgramIcons();
        }

        public int GetIndexOfProgramIcon(string icon)
        {
            return ProgramSelector.GetIndexOfProgramIcon(icon);
        }

        public bool GetIsProgramExisting(int index)
        {
            return ProgramSelector.GetIsProgramExisting(index);
        }

        public bool GetIsProgramExisting(string icon)
        {
            return ProgramSelector.GetIsProgramExisting(icon);
        }

        /// <summary>
        /// Checks if the program with the given index is the currently selected.
        /// WARNING: Changing the hearing program alters their order!
        /// </summary>
        /// <param name="index"></param>
        public bool GetIsProgramSelected(int index)
        {
            return ProgramSelector.GetIsProgramSelected(index);
        }

        /// <summary>
        /// Returns whether or not the program with the given icon text is currently active. Does nothing if it's already selected.
        /// This method is slower than calling by index, but is safer in regards to changing program order.
        /// </summary>
        /// <param name="icon">The string representation of the program's icon. Can be acquired by calling <see cref="GetProgramIcon(int)"/></param>.
        public bool GetIsProgramSelected(string icon)
        {
            return ProgramSelector.GetIsProgramSelected(icon);
        }

        /// <summary>
        /// Selects the indexed hearing program. Does nothing if it's already selected.
        /// WARNING: Changing the hearing program alters their order!
        /// </summary>
        /// <param name="index"></param>
        public DashboardPage SelectProgram(int index)
        {
            return ProgramSelector.SelectProgram(index);
        }

        /// <summary>
        /// Selects the hearing program that corresponds to the given icon text. Does nothing if it's already selected.
        /// This method is slower than selecting by index, but is safer in regards to changing program order.
        /// </summary>
        /// <param name="icon">The string representation of the program's icon. Can be acquired by calling <see cref="GetProgramIcon(int)"/></param>.
        public DashboardPage SelectProgram(string icon)
        {
            return ProgramSelector.SelectProgram(icon);
        }

        /// <summary>
        /// Opens the selected hearing program and navigates to <see cref="ProgramDetailPage"/>.
        /// </summary>
        public void OpenCurrentProgram()
        {
            ProgramSelector.OpenCurrentProgram();
            ClearCache();
        }

        /// <summary>
        /// Opens the indexed hearing program and navigates to <see cref="ProgramDetailPage"/>.
        /// </summary>
        public void OpenProgram(int index)
        {
            ProgramSelector.OpenProgram(index);
            ClearCache();
        }

        /// <summary>
        /// Opens the hearing program given by the icon text and navigates to <see cref="ProgramDetailPage"/>.
        /// This method is slower than opening by index, but is safer in regards to changing program order.
        /// </summary>
        /// <param name="icon">The string representation of the program's icon. Can be acquired by calling <see cref="GetProgramIcon(int)"/></param>.
        public void OpenProgram(string icon)
        {
            ProgramSelector.OpenProgram(icon);
            ClearCache();
        }

        /// <summary>
        /// Select Program By Menu Type and Index
        /// </summary>
        /// <param name="mainMenuType"></param>
        /// <param name="index"></param>
        public void OpenProgramFromProgramsMenu(MainMenuTypes mainMenuType, int index)
        {
            OpenMenuUsingTap();
            new MainMenuPage().OpenPrograms();

            switch (mainMenuType)
            {
                case MainMenuTypes.Preset:
                    new ProgramsMenuPage().SelectMainProgram(index);
                    break;
                case MainMenuTypes.Streaming:
                    new ProgramsMenuPage().SelectStreamingProgram(index);
                    break;
                case MainMenuTypes.Favorites:
                    new ProgramsMenuPage().SelectFavoriteProgram(index);
                    break;
            }

            Thread.Sleep(500);
            OpenCurrentProgram();
        }

        public void WaitForToastToDisappear()
        {
            App.WaitForNoElement(() => SnackBar, TimeSpan.FromSeconds(30));
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
        /// Get Selected Program Color
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetSelectedProgramColor(int index)
        {
            return App.GetColorFromImageByPixel(ProgramSelector.GetProgramElement(index), 5, 5);
        }

        /// <summary>
        /// Method checks the dashboard UI. 
        /// Slider value set to default value. If specific value needs to be verified then it needs to be passed
        /// </summary>
        /// <param name="dashboardPage"></param>
        /// <param name="sliderValue"></param>
        public void CheckStartView(DashboardPage dashboardPage, double sliderValue = 0.5)
        {
            ReportHelper.LogTest(Status.Info, "Checking the Start view in the dashboard...");
            ReportHelper.LogTest(Status.Info, "Checking if programs are initialized...");
            Assert.IsTrue(dashboardPage.GetIsProgramInitFinished(), "Programs not initialized");
            ReportHelper.LogTest(Status.Info, "Programs initialized");
            ReportHelper.LogTest(Status.Info, "Checking if menu Hamburger Button Displayed is displayed...");
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed(), "Menu Hamburger Button Displayed is not displayed");
            ReportHelper.LogTest(Status.Info, "Menu Hamburger Button Displayed is displayed");
            ReportHelper.LogTest(Status.Info, "Checking if left hearing device is visible...");
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible(), "Left hearing device is not visible");
            ReportHelper.LogTest(Status.Info, "Left hearing device is visible");
            ReportHelper.LogTest(Status.Info, "Checking if right hearing device is visible...");
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible(), "Right hearing device is not visible");
            ReportHelper.LogTest(Status.Info, "Right hearing device is visible");
            ReportHelper.LogTest(Status.Info, "Checking if programs count is zero...");
            Assert.NotZero(dashboardPage.GetNumberOfPrograms(), "Programs count is zero");
            ReportHelper.LogTest(Status.Info, "Programs count is not zero");
            ReportHelper.LogTest(Status.Pass, "Start view in the dashboard is verified");
            ReportHelper.LogTest(Status.Info, "Checking if the volume is set to '" + sliderValue + "'...");
            Assert.AreEqual(sliderValue, dashboardPage.GetVolumeSliderValue(), delta: 0, "Volume is not set to '" + sliderValue + "' value");
            ReportHelper.LogTest(Status.Info, "Volume is set to '" + sliderValue + "' value");
        }
    }
}