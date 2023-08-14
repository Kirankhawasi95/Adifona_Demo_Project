using System;
using System.Threading;
using HorusUITest.Data;
using HorusUITest.Enums;
using HorusUITest.PageObjects;
using HorusUITest.PageObjects.Dialogs;
using HorusUITest.PageObjects.Start;
using HorusUITest.PageObjects.Start.Intro;
using HorusUITest.PageObjects.Menu;
using NUnit.Framework;
using AventStack.ExtentReports;

namespace HorusUITest.Helper
{
    public static class LaunchHelper
    {
        #region Additional Methods

        private static bool OnAndroid => AppManager.Platform == Platform.Android;
        private static bool OniOS => AppManager.Platform == Platform.iOS;

        /// <summary>
        /// Skips the intro pages and continues to the <see cref="InitializeHardwarePage"/>, potentially beeing preceeded by an <see cref="AppDialog"/> and / or <see cref="PermissionDialog"/>.
        /// Precondition: No existing app data.
        public static LaunchLog SkipIntroPages()
        {
            LaunchLog launchLog = LaunchLog.Default;
            launchLog.InitialPageType = typeof(IntroPageOne);
            PermissionHelper.AllowPermissionIfRequested<IntroPageOne>();
            new IntroPageOne().MoveRightBySwiping();
            new IntroPageTwo().MoveRightBySwiping();
            new IntroPageThree().MoveRightBySwiping();
            new IntroPageFour().MoveRightBySwiping();
            new IntroPageFive().Continue();
            return launchLog;
        }

        public static LaunchLog VerifyIntroPages()
        {
            LaunchLog launchLog = LaunchLog.Default;
            launchLog.InitialPageType = typeof(IntroPageOne);

            ReportHelper.LogTest(Status.Info, "Checking if Intro pages are displayed correctly...");
            ReportHelper.LogTest(Status.Info, "Checking if welcome page is loaded...");
            Assert.IsTrue(new IntroPageOne().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Welcome page is not loaded");
            var introPageOne = new IntroPageOne();
            ReportHelper.LogTest(Status.Info, "Welcome page is loaded");
            ReportHelper.LogTest(Status.Info, "Check welcome page title...");
            Assert.IsNotEmpty(new IntroPageOne().GetTitle(), "Welcome page title is empty");
            ReportHelper.LogTest(Status.Info, "Welcome page title is not empty");
            ReportHelper.LogTest(Status.Info, "Check if right button is visible...");
            Assert.IsTrue(introPageOne.GetIsRightButtonVisible(), "Right button is not visible");
            ReportHelper.LogTest(Status.Info, "Right button is visible");
            ReportHelper.LogTest(Status.Info, "Check if left button is not visible...");
            Assert.IsFalse(introPageOne.GetIsLeftButtonVisible(), "Left button is visible");
            ReportHelper.LogTest(Status.Info, "Left button is not visible");
            ReportHelper.LogTest(Status.Info, "Checking if Intro 1 page is loaded...");
            Assert.IsTrue(new IntroPageOne().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Intro 1 page not loaded");
            ReportHelper.LogTest(Status.Info, "Intro 1 page is loaded");
            ReportHelper.LogTest(Status.Info, "Swiping Intro 1 Page by right...");
            new IntroPageOne().MoveRightBySwiping();
            ReportHelper.LogTest(Status.Info, "Swiped Intro 1 Page by right");
            ReportHelper.LogTest(Status.Info, "Checking if Intro 2 page is loaded...");
            Assert.IsTrue(new IntroPageTwo().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Intro 2 page not loaded");
            ReportHelper.LogTest(Status.Info, "Intro 2 page is loaded");
            ReportHelper.LogTest(Status.Info, "Swiping Intro 2 Page by right...");
            new IntroPageTwo().MoveRightBySwiping();
            ReportHelper.LogTest(Status.Info, "Swiped Intro 2 Page by right");
            ReportHelper.LogTest(Status.Info, "Checking if Intro 3 page is loaded...");
            Assert.IsTrue(new IntroPageThree().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Intro 3 page not loaded");
            ReportHelper.LogTest(Status.Info, "Intro 3 page is loaded");
            ReportHelper.LogTest(Status.Info, "Swiping Intro 3 Page by right...");
            new IntroPageThree().MoveRightBySwiping();
            ReportHelper.LogTest(Status.Info, "Swiped Intro 3 Page by right");
            ReportHelper.LogTest(Status.Info, "Checking if Intro 4 page is loaded...");
            Assert.IsTrue(new IntroPageFour().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Intro 4 page not loaded");
            ReportHelper.LogTest(Status.Info, "Intro 4 page is loaded");
            ReportHelper.LogTest(Status.Info, "Swiping Intro 4 Page by right...");
            new IntroPageFour().MoveRightBySwiping();
            ReportHelper.LogTest(Status.Info, "Swiped Intro 4 Page by right");
            ReportHelper.LogTest(Status.Info, "Checking if Intro 5 page is loaded...");
            Assert.IsTrue(new IntroPageFive().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Intro 5 page not loaded");
            ReportHelper.LogTest(Status.Info, "Intro 5 page is loaded");
            ReportHelper.LogTest(Status.Info, "Check if right button is visible...");
            Assert.IsTrue(new IntroPageFive().GetIsLeftButtonVisible(), "Right button is not visible");
            ReportHelper.LogTest(Status.Info, "Right button is visible");
            ReportHelper.LogTest(Status.Info, "Check if left button is visible...");
            Assert.IsTrue(new IntroPageFive().GetIsLeftButtonVisible(), "Left button is not visible");
            ReportHelper.LogTest(Status.Info, "Left button is visible");
            ReportHelper.LogTest(Status.Info, "Tapping 'Continue'...");
            new IntroPageFive().Continue();
            ReportHelper.LogTest(Status.Info, "'Continue' tapped");
            ReportHelper.LogTest(Status.Pass, "Intro pages are displayed correctly");

            return launchLog;
        }

        /// <summary>
        /// Skips to the <see cref="InitializeHardwarePage"/>, auto-accepting any dialogs.
        /// Precondition: No existing app data.
        /// </summary>
        public static LaunchResult<InitializeHardwarePage> SkipToInitializeHardwarePage()
        {
            LaunchLog launchLog = SkipIntroPages();
            launchLog.WasOnInitializeHardwarePage = true;
            BasePage page = GetCurrentPageWhileSkippingPermissionDialogs(out int newAppDialogs, out int newPermissions, typeof(InitializeHardwarePage), typeof(HardwareErrorPage));
            launchLog.Add(newAppDialogs, newPermissions);
            switch (page)
            {
                case InitializeHardwarePage _:
                    break;
                case HardwareErrorPage _:
                    AppManager.DeviceSettings.EnableBluetooth();
                    Thread.Sleep(2000);
                    page = new InitializeHardwarePage();
                    break;
                default:
                    throw new Exception("Unexpected page found.");
            }
            return new LaunchResult<InitializeHardwarePage>((InitializeHardwarePage)page, launchLog);
        }

        /// <summary>
        /// Skips to the <see cref="SelectHearingAidsPage"/>, auto-accepting any dialogs.
        /// Precondition: No existing app data.
        /// </summary>
        public static LaunchResult<SelectHearingAidsPage> SkipToSelectHearingAidsPage()
        {
            var launchResult = SkipToInitializeHardwarePage();
            launchResult.Page.StartScan();
            PermissionHelper.AllowPermissionIfRequested<SelectHearingAidsPage>();
            Thread.Sleep(1500);
            return new LaunchResult<SelectHearingAidsPage>(new SelectHearingAidsPage(), launchResult.Log);
        }

        public static LaunchResult<HearingAidInitPage>
            SkipToHearingAidInitPage()
        {
            var launchResult = SkipToInitializeHardwarePage();
            launchResult.Page.StartScan();
            PermissionHelper.AllowPermissionIfRequested<InitializeHardwarePage>();
            //Thread.Sleep(1500);
            return new LaunchResult<HearingAidInitPage>(new HearingAidInitPage(), launchResult.Log);
        }

        #endregion Additional Methods

        #region Private Helpers

        private static BasePage GetCurrentPageWhileSkippingDialogs(out int appDialogs, out int permissions, params Type[] pageTypes)
        {
            Type[] allTypes = new Type[pageTypes.Length + 2];
            allTypes[0] = typeof(AppDialog);
            allTypes[1] = typeof(PermissionDialog);
            pageTypes.CopyTo(allTypes, 2);
            appDialogs = 0;
            permissions = 0;

            while (true)
            {
                var page = PageHelper.WaitForAnyPage(allTypes);
                switch (page)
                {
                    case AppDialog _:
                        ((AppDialog)page).Confirm();
                        appDialogs++;
                        break;
                    case PermissionDialog _:
                        ((PermissionDialog)page).Allow();
                        permissions++;
                        break;
                    default:
                        if (OniOS)
                        {
                            SkipAllDialogs(out int newAppDialogs, out int newPermissions);
                            appDialogs += newAppDialogs;
                            permissions += newPermissions;
                        }
                        return page;
                }
            }
        }

        private static BasePage GetCurrentPageWhileSkippingPermissionDialogs(out int appDialogs, out int permissions, params Type[] pageTypes)
        {
            Type[] allTypes;
            if (OnAndroid)
            {
                allTypes = new Type[pageTypes.Length + 2];
                allTypes[0] = typeof(AppDialog);
                allTypes[1] = typeof(PermissionDialog);
                pageTypes.CopyTo(allTypes, 2);
            }
            else
            {
                allTypes = new Type[pageTypes.Length + 1];
                allTypes[0] = typeof(PermissionDialog);
                pageTypes.CopyTo(allTypes, 1);
            }
            appDialogs = 0;
            permissions = 0;

            while (true)
            {
                var page = PageHelper.WaitForAnyPage(allTypes);
                switch (page)
                {
                    case AppDialog _:
                        ((AppDialog)page).Confirm();
                        appDialogs++;
                        break;
                    case PermissionDialog _:
                        ((PermissionDialog)page).Allow();
                        permissions++;
                        break;
                    default:
                        if (OniOS)
                        {
                            SkipAllDialogs(out int newAppDialogs, out int newPermissions);
                            appDialogs += newAppDialogs;
                            permissions += newPermissions;
                        }
                        return page;
                }
            }
        }

        private static void SkipAllDialogs(out int appDialogs, out int permissions)
        {
            appDialogs = 0;
            permissions = 0;
            bool appDialogSkipped;
            bool permissionSkipped;
            int seconds = 0;
            do
            {
                appDialogSkipped = DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(seconds));
                if (appDialogSkipped)
                {
                    appDialogs++;
                    seconds = 1;
                }
                permissionSkipped = PermissionHelper.AllowPermissionIfRequested(TimeSpan.FromSeconds(seconds));
                if (permissionSkipped)
                {
                    permissions++;
                    seconds = 1;
                }
            }
            while (appDialogSkipped == true || permissionSkipped == true);
        }

        #endregion Private Helpers

        #region Start App in Demo Mode

        /// <summary>
        /// Skips to the <see cref="DashboardPage"/> in demo mode, auto-accepting any dialog.
        /// Precondition: No existing app data.
        /// </summary>
        public static LaunchResult<DashboardPage> StartAppInDemoModeForTheFirstTime()
        {
            var launchResult = SkipToInitializeHardwarePage();
            LaunchLog launchLog = launchResult.Log;
            launchResult.Page.StartDemoMode();
            var page = GetCurrentPageWhileSkippingDialogs(out int newAppDialogs, out int newPermissions, typeof(DashboardPage));
            launchLog.Add(newAppDialogs, newPermissions);
            switch (page)
            {
                case DashboardPage _:
                    return new LaunchResult<DashboardPage>((DashboardPage)page, launchLog);

                default:
                    throw new Exception("Unexpected page found.");
            }
        }

        /// <summary>
        /// Skips to the <see cref="DashboardPage"/> in demo mode, auto-accepting any dialog.
        /// This method is expected to navigate the app to the <see cref="DashboardPage"/>, regardless of existing app data.
        /// </summary>
        /// <param name="launchLog">Holds a set of data acquired during the process.</param>
        public static LaunchResult<DashboardPage> StartAppInDemoModeByAnyMeans()
        {
            LaunchLog launchLog = LaunchLog.Default;
            int newAppDialogs;
            int newPermissions;
            BasePage page;
            while (true)
            {
                page = GetCurrentPageWhileSkippingDialogs(out newAppDialogs, out newPermissions, typeof(IntroPageOne), typeof(HardwareErrorPage), typeof(InitializeHardwarePage), typeof(DashboardPage));
                launchLog.Add(newAppDialogs, newPermissions);
                launchLog.InitialPageType = page.GetType();
                switch (page)
                {
                    case IntroPageOne _:
                        goto OnIntroPageOne;
                    case HardwareErrorPage errorPage:
                        AppManager.DeviceSettings.EnableBluetooth();
                        errorPage.RetryProcess();
                        errorPage.IsGoneBeforeTimeout(TimeSpan.FromSeconds(5));
                        break;
                    case InitializeHardwarePage _:
                        goto OnInitializeHardwarePage;
                    case DashboardPage _:
                        if (OniOS)
                        {
                            ((DashboardPage)page).WaitForToastToDisappear();
                            goto OnIntroPageOne;
                        }
                        goto OnDashboardPage;
                    default:
                        throw new Exception("Unexpected page found.");
                }
            }

        OnIntroPageOne:
            SkipIntroPages();
            page = GetCurrentPageWhileSkippingDialogs(out newAppDialogs, out newPermissions, typeof(InitializeHardwarePage), typeof(DashboardPage));
            launchLog.Add(newAppDialogs, newPermissions);
            switch (page)
            {
                case InitializeHardwarePage _:
                    goto OnInitializeHardwarePage;
                case DashboardPage _:
                    goto OnDashboardPage;
                default:
                    throw new Exception("Unexpected page found.");
            }

        OnInitializeHardwarePage:
            ((InitializeHardwarePage)page).StartDemoMode();
            page = GetCurrentPageWhileSkippingDialogs(out newAppDialogs, out newPermissions, typeof(DashboardPage));
            launchLog.Add(newAppDialogs, newPermissions);
            switch (page)
            {
                case DashboardPage _:
                    goto OnDashboardPage;
                default:
                    throw new Exception("Unexpected page found.");
            }

        OnDashboardPage:
            var dashboardPage = new LaunchResult<DashboardPage>((DashboardPage)page, launchLog);
            dashboardPage.Page.WaitForToastToDisappear();
            return dashboardPage;
        }

        #endregion Start App in Demo Mode

        #region Starting App with Hearing Aids

        public static LaunchResult<DashboardPage> StartAppWithHearingAidsForTheFirstTime(string firstDeviceName, string secondDeviceName = null, int numberOfConnectionAttempts = 1)
        {
            var launchResult = SkipToSelectHearingAidsPage();
            LaunchLog launchLog = launchResult.Log;

            if (OnAndroid || OniOS) //temporarily fix for issue assigned to WorkItem 17400 - failed connection when devices are not coupled under MFi before using them in the app
            {
                SelectHearingAidsPage selectionPage = launchResult.Page;
                selectionPage.WaitUntilDeviceFound(firstDeviceName);
                if (!selectionPage.GetIsDeviceFound(firstDeviceName))
                    selectionPage.WaitUntilDeviceFound(firstDeviceName);

                if (secondDeviceName != null)
                    selectionPage.WaitUntilDeviceFound(secondDeviceName);
                if (!selectionPage.GetIsDeviceFound(secondDeviceName))
                    selectionPage.WaitUntilDeviceFound(secondDeviceName);

                selectionPage.WaitUntilDeviceListNotChanging();
                selectionPage.SelectDevicesExclusively(firstDeviceName, secondDeviceName);
                selectionPage.GetIsDeviceSelected(firstDeviceName);
                if (secondDeviceName != null)
                    selectionPage.GetIsDeviceSelected(secondDeviceName);
                selectionPage.Connect();

                for (int i = 0; i < numberOfConnectionAttempts; i++)
                {
                    var initPage = new HearingAidInitPage();
                    BasePage page = initPage.WaitForConnection();
                    switch (page)
                    {
                        case DashboardPage dashboardPage:
                            launchLog.HearingAidConnectionAttempts = i + 1;
                            return new LaunchResult<DashboardPage>(dashboardPage, launchLog);
                        case HearingAidConnectionErrorPage errorPage:
                            errorPage.RetryConnection();
                            break;
                        default:
                            throw new Exception("Unexpected page found.");
                    }
                }
            }

            //HearingAidInitPage initPage;
            for (int i = 0; i < numberOfConnectionAttempts; i++)
            {
                var initPage = new HearingAidInitPage();
                BasePage page = initPage.WaitForConnection();
                switch (page)
                {
                    case DashboardPage dashboardPage:
                        launchLog.HearingAidConnectionAttempts = i + 1;
                        dashboardPage.WaitForToastToDisappear();
                        return new LaunchResult<DashboardPage>(dashboardPage, launchLog);
                    case HearingAidConnectionErrorPage errorPage:
                        errorPage.RetryConnection();
                        break;
                    default:
                        throw new Exception("Unexpected page found.");
                }
            }
            throw new Exception($"Failed to connect to hearing device after {numberOfConnectionAttempts} {(numberOfConnectionAttempts == 1 ? "attempt" : "attempts")}.");
        }

        /// <summary>
        /// Starts the app with the given hearing aid(s) and navigates to the <see cref="DashboardPage"/>. Takes care of turning on the hearing aids and establishing a connection.
        /// </summary>
        /// <param name="firstHearingAid"></param>
        /// <param name="secondHearingAid"></param>
        /// <param name="numberOfConnectionAttempts"></param>
        /// <returns></returns>
        public static LaunchResult<DashboardPage> StartAppWithHearingAidsForTheFirstTime(HearingAid firstHearingAid, HearingAid secondHearingAid = null, int numberOfConnectionAttempts = 1)
        {
            if (firstHearingAid == null)
                throw new NullReferenceException($"{nameof(StartAppWithHearingAidsForTheFirstTime)}: {nameof(firstHearingAid)} was not initalized.");
            // firstHearingAid.Enable();
            // secondHearingAid?.Enable();
            return StartAppWithHearingAidsForTheFirstTime(firstHearingAid.Name, secondHearingAid?.Name, numberOfConnectionAttempts);
        }

        /// <summary>
        /// This method is used in test cases where we have disconnect devices and connect same or new device(s) again without restarting app <see cref="HearingSystemManagementPage.DisconnectDevices()"/>
        /// The app must be on <see cref="InitializeHardwarePage"/> for this method to work
        /// Navigates to the <see cref="DashboardPage"/>. Takes care of turning on the hearing aids and establishing a connection. 
        /// </summary>
        /// <param name="firstHearingAid"></param>
        /// <param name="secondHearingAid"></param>
        /// <param name="numberOfConnectionAttempts"></param>
        /// <returns></returns>
        public static DashboardPage ReconnectHearingAidsFromStartScan(HearingAid firstHearingAid, HearingAid secondHearingAid = null, int numberOfConnectionAttempts = 1)
        {
            if (firstHearingAid == null)
                throw new NullReferenceException($"{nameof(StartAppWithHearingAidsForTheFirstTime)}: {nameof(firstHearingAid)} was not initalized.");

            return ReconnectHearingAidsFromStartScan(firstHearingAid.Name, secondHearingAid?.Name, numberOfConnectionAttempts);
        }

        public static DashboardPage ReconnectHearingAidsFromStartScan(string firstDeviceName, string secondDeviceName = null, int numberOfConnectionAttempts = 1)
        {
            new InitializeHardwarePage().StartScan();
            Thread.Sleep(1500);

            DialogHelper.ConfirmIfDisplayed();
            PermissionHelper.AllowPermissionIfRequested();

            if (OnAndroid || OniOS)
            {
                SelectHearingAidsPage selectionPage = new SelectHearingAidsPage();
                selectionPage.WaitUntilDeviceFound(firstDeviceName);
                if (!selectionPage.GetIsDeviceFound(firstDeviceName))
                    selectionPage.WaitUntilDeviceFound(firstDeviceName);

                if (secondDeviceName != null)
                    selectionPage.WaitUntilDeviceFound(secondDeviceName);
                if (!selectionPage.GetIsDeviceFound(secondDeviceName))
                    selectionPage.WaitUntilDeviceFound(secondDeviceName);

                selectionPage.WaitUntilDeviceListNotChanging();
                selectionPage.SelectDevicesExclusively(firstDeviceName, secondDeviceName);
                selectionPage.GetIsDeviceSelected(firstDeviceName);
                if (secondDeviceName != null)
                    selectionPage.GetIsDeviceSelected(secondDeviceName);
                selectionPage.Connect();
            }

            HearingAidInitPage initPage;
            for (int i = 0; i < numberOfConnectionAttempts; i++)
            {
                initPage = new HearingAidInitPage();
                BasePage page = initPage.WaitForConnection();
                switch (page)
                {
                    case DashboardPage dashboardPage:
                        dashboardPage.WaitForToastToDisappear();
                        return dashboardPage;
                    case HearingAidConnectionErrorPage errorPage:
                        errorPage.RetryConnection();
                        break;
                    default:
                        throw new Exception("Unexpected page found.");
                }
            }
            throw new Exception($"Failed to connect to hearing device after {numberOfConnectionAttempts} {(numberOfConnectionAttempts == 1 ? "attempt" : "attempts")}.");
        }

        public static LaunchResult<DashboardPage> StartAppUsingExistingConfigData()
        {
            var log = LaunchLog.Default;
            log.InitialPageType = typeof(DashboardPage);
            return new LaunchResult<DashboardPage>(new DashboardPage(), log);
        }

        /// <summary>
        /// This method performs till Connect to Hearing Aid after performing scan.
        /// </summary>
        /// <param name="firstDeviceName"></param>
        /// <param name="secondDeviceName"></param>
        /// <param name="numberOfConnectionAttempts"></param>
        public static void StartAppWithHearingAidsAndConnectOnly(string firstDeviceName, string secondDeviceName = null, int numberOfConnectionAttempts = 1)
        {
            var launchResult = SkipToSelectHearingAidsPage();
            LaunchLog launchLog = launchResult.Log;

            if (OnAndroid || OniOS) //temporarily fix for issue assigned to WorkItem 17400 - failed connection when devices are not coupled under MFi before using them in the app
            {
                SelectHearingAidsPage selectionPage = launchResult.Page;
                selectionPage.WaitUntilDeviceFound(firstDeviceName);
                if (!selectionPage.GetIsDeviceFound(firstDeviceName))
                    selectionPage.WaitUntilDeviceFound(firstDeviceName);

                if (secondDeviceName != null)
                    selectionPage.WaitUntilDeviceFound(secondDeviceName);
                if (!selectionPage.GetIsDeviceFound(secondDeviceName))
                    selectionPage.WaitUntilDeviceFound(secondDeviceName);

                selectionPage.WaitUntilDeviceListNotChanging();
                selectionPage.SelectDevicesExclusively(firstDeviceName, secondDeviceName);
                selectionPage.GetIsDeviceSelected(firstDeviceName);
                if (secondDeviceName != null)
                    selectionPage.GetIsDeviceSelected(secondDeviceName);
                selectionPage.Connect();
            }
        }

        public static LaunchResult<DashboardPage> StartAppWithNonAudifonHearingAids(out bool IsFirstNonAudifonDeviceAvailable, out bool IsSecondNonAudifonDeviceAvailable, string firstDeviceName, string firstNonAudifonDeviceName, string secondDeviceName = null, string secondNonAudifonDeviceName = null, int numberOfConnectionAttempts = 1)
        {
            var launchResult = SkipToSelectHearingAidsPage();
            LaunchLog launchLog = launchResult.Log;

            // Will be Set true if non Audifon device is listed
            IsFirstNonAudifonDeviceAvailable = false;
            IsSecondNonAudifonDeviceAvailable = false;

            if (OnAndroid || OniOS) //temporarily fix for issue assigned to WorkItem 17400 - failed connection when devices are not coupled under MFi before using them in the app
            {
                SelectHearingAidsPage selectionPage = launchResult.Page;

                // Hearing Aid which belongs to Audifon will also be listed and will connected
                selectionPage.WaitUntilDeviceFound(firstDeviceName);
                if (!selectionPage.GetIsDeviceFound(firstDeviceName))
                    selectionPage.WaitUntilDeviceFound(firstDeviceName);

                if (secondDeviceName != null)
                    selectionPage.WaitUntilDeviceFound(secondDeviceName);
                if (!selectionPage.GetIsDeviceFound(secondDeviceName))
                    selectionPage.WaitUntilDeviceFound(secondDeviceName);

                selectionPage.WaitUntilDeviceListNotChanging();
                selectionPage.SelectDevicesExclusively(firstDeviceName, secondDeviceName);
                selectionPage.GetIsDeviceSelected(firstDeviceName);
                if (secondDeviceName != null)
                    selectionPage.GetIsDeviceSelected(secondDeviceName);

                // Checking if Non Audifon Device is Listed
                IsFirstNonAudifonDeviceAvailable = selectionPage.GetIsDeviceSelected(firstNonAudifonDeviceName, false);
                if (secondNonAudifonDeviceName != null)
                    IsSecondNonAudifonDeviceAvailable = selectionPage.GetIsDeviceSelected(secondNonAudifonDeviceName, false);

                selectionPage.Connect();
            }

            // Hearing Aid which belongs to Audifon will also be listed and will connected
            HearingAidInitPage initPage;
            for (int i = 0; i < numberOfConnectionAttempts; i++)
            {
                initPage = new HearingAidInitPage();
                BasePage page = initPage.WaitForConnection();
                switch (page)
                {
                    case DashboardPage dashboardPage:
                        launchLog.HearingAidConnectionAttempts = i + 1;
                        return new LaunchResult<DashboardPage>(dashboardPage, launchLog);
                    case HearingAidConnectionErrorPage errorPage:
                        errorPage.RetryConnection();
                        break;
                    default:
                        throw new Exception("Unexpected page found.");
                }
            }
            throw new Exception($"Failed to connect to hearing device after {numberOfConnectionAttempts} {(numberOfConnectionAttempts == 1 ? "attempt" : "attempts")}.");
        }

        public static LaunchResult<SelectHearingAidsPage> StartAppWithHearingAidsScanOnly(string firstDeviceName, string secondDeviceName = null)
        {
            var launchResult = SkipToSelectHearingAidsPage();
            LaunchLog launchLog = launchResult.Log;
            SelectHearingAidsPage selectionPage = launchResult.Page;

            if (OnAndroid || OniOS) //temporarily fix for issue assigned to WorkItem 17400 - failed connection when devices are not coupled under MFi before using them in the app
            {
                selectionPage.WaitUntilDeviceFound(firstDeviceName);
                if (!selectionPage.GetIsDeviceFound(firstDeviceName))
                    selectionPage.WaitUntilDeviceFound(firstDeviceName);

                if (secondDeviceName != null)
                    selectionPage.WaitUntilDeviceFound(secondDeviceName);
                if (!selectionPage.GetIsDeviceFound(secondDeviceName))
                    selectionPage.WaitUntilDeviceFound(secondDeviceName);

                selectionPage.WaitUntilDeviceListNotChanging();
                selectionPage.SelectDevicesExclusively(firstDeviceName, secondDeviceName);
                selectionPage.GetIsDeviceSelected(firstDeviceName);
                if (secondDeviceName != null)
                    selectionPage.GetIsDeviceSelected(secondDeviceName);
            }
            return new LaunchResult<SelectHearingAidsPage>(selectionPage, launchLog);
        }

        /// <summary>
        /// This method skips to Dashboard page
        /// Also verifies all Intro Pages, <see cref="InitializeHardwarePage", <see cref="SelectHearingAidsPage"/>
        /// <see cref="HearingAidInitPage", Verifies and accepts all <see cref="AppDialog"/> and <see cref="PermissionDialog"
        /// </summary>
        /// <param name="firstHearingAid"></param>
        /// <param name="secondHearingAid"></param>
        /// <returns></returns>
        public static DashboardPage StartAppWithHearingAidsForTheFirstTimeAndVerify(string firstHearingAid, string secondHearingAid = null, int numberOfConnectionAttempts = 1)
        {
            LaunchLog launchLog = VerifyIntroPages();

            ReportHelper.LogTest(Status.Info, "Checking if initialize hardware page is loaded...");
            Assert.IsTrue(new InitializeHardwarePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Initialize hardware page not loaded");
            var initializeHardwarePage = new InitializeHardwarePage();
            ReportHelper.LogTest(Status.Info, "Initialize hardware page is loaded");
            ReportHelper.LogTest(Status.Info, "Checking demo mode text...");
            Assert.IsNotEmpty(initializeHardwarePage.GetDemoModeText(), "Demo mode text is empty");
            ReportHelper.LogTest(Status.Info, "Demo mode text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking start scan text...");
            Assert.IsNotEmpty(initializeHardwarePage.GetScanText(), "Start scan text is empty");
            ReportHelper.LogTest(Status.Info, "Start scan text is not empty");
            ReportHelper.LogTest(Status.Info, "Clicking start scan...");
            initializeHardwarePage.StartScan();
            ReportHelper.LogTest(Status.Info, "Clicked start scan");

            PermissionHelper.AllowPermissionIfRequested<SelectHearingAidsPage>();
            Thread.Sleep(1500);

            ReportHelper.LogTest(Status.Info, "Checking if select hearing aids page is loaded...");
            Assert.IsTrue(new SelectHearingAidsPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Select hearing aids page not loaded");
            var selectHearingAidsPage = new SelectHearingAidsPage();
            ReportHelper.LogTest(Status.Info, "Select hearing aids page is loaded");
            ReportHelper.LogTest(Status.Info, "Checking description text...");
            Assert.IsNotEmpty(selectHearingAidsPage.GetDescription(), "Description text is empty");
            ReportHelper.LogTest(Status.Info, "Description text is not empty");
            ReportHelper.LogTest(Status.Info, "Checking cancel text...");
            Assert.IsNotEmpty(selectHearingAidsPage.GetCancelText(), "Cancel text is empty");
            ReportHelper.LogTest(Status.Info, "Cancel text is not empty");

            if (OnAndroid || OniOS) //temporarily fix for issue assigned to WorkItem 17400 - failed connection when devices are not coupled under MFi before using them in the app
            {
                if (!selectHearingAidsPage.GetIsDeviceFound(firstHearingAid))
                    selectHearingAidsPage.WaitUntilDeviceFound(firstHearingAid);

                if (secondHearingAid != null)
                    selectHearingAidsPage.WaitUntilDeviceFound(secondHearingAid);
                if (!selectHearingAidsPage.GetIsDeviceFound(secondHearingAid))
                    selectHearingAidsPage.WaitUntilDeviceFound(secondHearingAid);

                selectHearingAidsPage.WaitUntilDeviceListNotChanging();
                selectHearingAidsPage.SelectDevicesExclusively(firstHearingAid, secondHearingAid);
                Assert.IsTrue(selectHearingAidsPage.GetIsDeviceSelected(firstHearingAid));
                if (secondHearingAid != null)
                    selectHearingAidsPage.GetIsDeviceSelected(secondHearingAid);

                ReportHelper.LogTest(Status.Info, "Checking connect button text...");
                Assert.IsNotEmpty(selectHearingAidsPage.GetConnectButtonText(), "Connect button text is empty");
                ReportHelper.LogTest(Status.Info, "Connect button text is not empty");

                ReportHelper.LogTest(Status.Info, "Clicking connect button...");
                selectHearingAidsPage.Connect();
                ReportHelper.LogTest(Status.Info, "Clicked connect button");

                for (int i = 0; i < numberOfConnectionAttempts; i++)
                {
                    var initPage = new HearingAidInitPage();
                    BasePage page = initPage.WaitForConnection();
                    switch (page)
                    {
                        case DashboardPage dashboardPage:
                            launchLog.HearingAidConnectionAttempts = i + 1;
                            return new LaunchResult<DashboardPage>(dashboardPage, launchLog).Page;
                        case HearingAidConnectionErrorPage errorPage:
                            errorPage.RetryConnection();
                            break;
                        default:
                            throw new Exception("Unexpected page found.");
                    }
                }
            }

            //HearingAidInitPage initPage;
            for (int i = 0; i < numberOfConnectionAttempts; i++)
            {
                var initPage = new HearingAidInitPage();
                BasePage page = initPage.WaitForConnection();
                switch (page)
                {
                    case DashboardPage dashboardPage:
                        launchLog.HearingAidConnectionAttempts = i + 1;
                        dashboardPage.WaitUntilProgramInitFinished();
                        Assert.IsTrue(dashboardPage.IsCurrentlyShown());
                        dashboardPage.WaitForToastToDisappear();
                        Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
                        if (!ReferenceEquals(secondHearingAid, null))
                            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
                        Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
                        ReportHelper.LogTest(Status.Info, "Dashboard page is loaded in device mode after skipping intro pages");

                        if (!ReferenceEquals(secondHearingAid, null))
                            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aids Left '" + firstHearingAid + "' and Right '" + secondHearingAid + "'");
                        else
                            ReportHelper.LogTest(Status.Info, "App started and connected to Hearing Aid Left '" + firstHearingAid + "'");

                        return new LaunchResult<DashboardPage>(dashboardPage, launchLog).Page;
                    case HearingAidConnectionErrorPage errorPage:
                        errorPage.RetryConnection();
                        break;
                    default:
                        throw new Exception("Unexpected page found.");
                }
            }
            throw new Exception($"Failed to connect to hearing device after {numberOfConnectionAttempts} {(numberOfConnectionAttempts == 1 ? "attempt" : "attempts")}.");
        }

        #endregion Starting App with Hearing Aids

        #region IOS Reset Method

        /// <summary>
        /// This is a temporary solution to reset app before each test on iOS platform. This method does not Reset Permissions
        /// </summary>
        /// <returns></returns>
        public static IntroPageOne ResetIosAppFromSetting()
        {
            Output.Immediately("Resetting iOS app");

            bool IsAppLoaded = false;

            // if App starts from Intro Page
            if (new IntroPageOne(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)))
            {
                SkipIntroPages();
                Output.Immediately("App is in IntroPageOne");

                DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(5));

                if (new InitializeHardwarePage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)))
                {
                    // If the App is newly installed
                    new InitializeHardwarePage().StartDemoMode();
                    Output.Immediately("Loaded InitializeHardwarePage");
                }
                else
                {
                    // If the App already installed and updated
                    Output.Immediately("Skipped InitializeHardwarePage");
                }
                IsAppLoaded = true;
            }

            // If App starts from InitializeHardwarePage where we have Start Scan and Start in Demo Mode options
            if (new InitializeHardwarePage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)))
            {
                new InitializeHardwarePage().StartDemoMode();
                Output.Immediately("App is in InitializeHardwarePage");
                IsAppLoaded = true;
            }

            // If the App starts from SelectHearingAidsPage where Previously connected HA is Automatically connected and loaded
            if (new SelectHearingAidsPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)))
            {
                new SelectHearingAidsPage().Cancel();
                Assert.IsTrue(new InitializeHardwarePage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)));
                new InitializeHardwarePage().StartDemoMode();
                Output.Immediately("App is in SelectHearingAidsPage");
                IsAppLoaded = true;
            }

            // If the App goes to HearingAidConnectionErrorPage where we have two options Back To HearingAid Selection option and Find HA Option
            if (new HearingAidConnectionErrorPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)))
            {
                new HearingAidConnectionErrorPage().BackToHearingAidSelectionPage();
                Assert.IsTrue(new InitializeHardwarePage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)));
                new InitializeHardwarePage().StartDemoMode();
                Output.Immediately("App is in HearingAidConnectionErrorPage");
                IsAppLoaded = true;
            }

            // If the App is in HearingAidInitPage page and trying to connect available or unavailable HA. This will be executed only if none of the above cases matches 
            if (!IsAppLoaded && new HearingAidInitPage(false).IsGoneBeforeTimeout(TimeSpan.FromSeconds(25)) == false)
            {
                new HearingAidInitPage().CancelAndConfirm();
                Assert.IsTrue(new HearingAidConnectionErrorPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
                new HearingAidConnectionErrorPage().BackToHearingAidSelectionPage();
                Assert.IsTrue(new InitializeHardwarePage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(2)));
                new InitializeHardwarePage().StartDemoMode();
                Output.Immediately("App is in HearingAidInitPage");
                IsAppLoaded = true;
            }

            // Allow permission if requested
            DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(10));
            PermissionHelper.AllowPermissionIfRequested();

            // Wait till dashboard page is shown
            Assert.IsTrue(new DashboardPage(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));

            // Reset the App from Settings Menu
            var settingsMenuPage = NavigationHelper.NavigateToSettingsMenu(new DashboardPage());
            settingsMenuPage.ShowDevelopmentStuff();

            try
            {
                settingsMenuPage.ResetApp();
            }
            catch (Exception ex)
            {
                Output.Immediately("Reset is unsuccessfull with Error: " + ex.Message);
                throw new Exception("Reset is unsuccessfull.", ex);
            }

            Assert.IsTrue(new IntroPageOne(false).IsShowingBeforeTimeout(TimeSpan.FromSeconds(20)));
            Output.Immediately("iOS app is reset successfully. Navigated to IntroPageOne");
            return new IntroPageOne();
        }

        #endregion IOS Reset Method
    }
}