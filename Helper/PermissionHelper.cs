using System;
using System.Diagnostics;
using AventStack.ExtentReports;
using HorusUITest.Configuration;
using HorusUITest.Enums;
using HorusUITest.PageObjects;
using HorusUITest.PageObjects.Dialogs;

namespace HorusUITest.Helper
{
    public static class PermissionHelper
    {
        public static void AllowPermission()
        {
            if (AppManager.Platform == Platform.Android)
            {
                new AppDialog().Confirm();
            }
            new PermissionDialog().Allow();
        }

        public static void DenyPermission()
        {
            if (AppManager.Platform == Platform.Android)
            {
                new AppDialog().Confirm();
            }
            new PermissionDialog().Deny();
        }

        /// <summary>
        /// Allows a permission if requested within the given timeout.
        /// </summary>
        /// <param name="alsoConfirmedDialog">Outputs <see cref="true" if an <see cref="AppDialog"/> had been confirmed as well (Android), <see cref="false"/> otherwise.</param>
        /// <param name="timeout">Maximum duration the method waits for a permission dialog.</param>
        /// <returns><see cref="true"/> if a persmission was actually requested, <see cref="false"/> otherwise.</returns>
        public static bool AllowPermissionIfRequested(out bool alsoConfirmedDialog, TimeSpan? timeout = null)
        {
            alsoConfirmedDialog = false;
            timeout = timeout ?? Env.OPTIONAL_PERMISSION_TIMEOUT;
            if (AppManager.Platform == Platform.Android)
            {
                if (DialogHelper.ConfirmIfDisplayed(timeout))
                    alsoConfirmedDialog = true;
            }
            var dialog = new PermissionDialog(false);
            if (dialog.IsShowingBeforeTimeout(timeout.Value))
            {
                dialog.Allow();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Allows a permission if requested within the given timeout.
        /// </summary>
        /// <param name="timeout">Maximum duration the method waits for a permission dialog.</param>
        /// <returns><see cref="true"/> if a persmission was actually requested, <see cref="false"/> otherwise.</returns>
        public static bool AllowPermissionIfRequested(TimeSpan? timeout = null)
        {
            return AllowPermissionIfRequested(out _, timeout);
        }

        private static bool AnswerPermissionIfRequested<T>(T expectedPage, out bool alsoConfirmedDialog, bool allowPermission, TimeSpan? timeout = null) where T : BasePage
        {
            alsoConfirmedDialog = false;
            timeout = timeout ?? Env.OPTIONAL_DIALOG_TIMEOUT;
            Stopwatch timer = new Stopwatch();

            if (AppManager.Platform == Platform.Android)
            {
                BasePage[] pages = { new AppDialog(false), new PermissionDialog(false), expectedPage };
                timer.Start();
                do
                {
                    BasePage page = PageHelper.WaitForAnyPageDontThrow(timeout.Value, pages);
                    switch (page)
                    {
                        case AppDialog appDialog:
                            if (!alsoConfirmedDialog)       //Confirm dialog only once
                            {
                                appDialog.Confirm();
                                alsoConfirmedDialog = true;
                            }
                            break;
                        case PermissionDialog permissionDialog:
                            if (allowPermission)
                                permissionDialog.Allow();
                            else
                                permissionDialog.Deny();
                            return true;
                        case T _:
                            return PermissionHelper.AllowPermissionIfRequested(TimeSpan.Zero);
                    }
                } while (timer.Elapsed <= timeout.Value);
            }
            else
            {
                BasePage[] pages = { new PermissionDialog(false), PageHelper.CreateInstance<T>(false) };
                timer.Start();
                do
                {
                    BasePage page = PageHelper.WaitForAnyPageDontThrow(timeout.Value, pages);
                    switch (page)
                    {
                        case PermissionDialog permissionDialog:
                            if (allowPermission)
                                permissionDialog.Allow();
                            else
                                permissionDialog.Deny();
                            return true;
                        case T _:
                            return PermissionHelper.AllowPermissionIfRequested(TimeSpan.Zero);
                    }
                } while (timer.Elapsed <= timeout.Value);
            }
            return false;
        }

        /// <summary>
        /// Allows a permission if requested within the given timeout. Returns either after granting a permission or if <paramref name="expectedPage"/> is displayed.
        /// </summary>
        /// <typeparam name="T">The class of the expected page to to be displayed after granting the permission.</typeparam>
        /// <param name="expectedPage">The page expected to be displayed after granting the permission.</param>
        /// <param name="alsoConfirmedDialog">Outputs <see cref="true" if an <see cref="AppDialog"/> had been confirmed as well (Android), <see cref="false"/> otherwise.</param>
        /// <param name="timeout">Maximum duration the method waits for a permission dialog.</param>
        /// <returns><see cref="true"/> if a persmission was actually requested, <see cref="false"/> otherwise.</returns>
        public static bool AllowPermissionIfRequested<T>(T expectedPage, out bool alsoConfirmedDialog, TimeSpan? timeout = null) where T : BasePage
        {
            return AnswerPermissionIfRequested(expectedPage, out alsoConfirmedDialog, true, timeout);
        }

        /// <summary>
        /// Allows a permission if requested within the given timeout. Returns either after granting a permission or if <paramref name="expectedPage"/> is displayed.
        /// </summary>
        /// <typeparam name="T">The class of the expected page to to be displayed after granting the permission.</typeparam>
        /// <param name="expectedPage">The page expected to be displayed after granting the permission.</param>
        /// <param name="timeout">Maximum duration the method waits for a permission dialog.</param>
        /// <returns><see cref="true"/> if a persmission was actually requested, <see cref="false"/> otherwise.</returns>
        public static bool AllowPermissionIfRequested<T>(T expectedPage, TimeSpan? timeout = null) where T : BasePage
        {
            return AllowPermissionIfRequested(expectedPage, out _, timeout);
        }

        /// <summary>
        /// Allows a permission if requested within the given timeout. Returns either after granting a permission or if the expected page given by <typeparamref name="T"/> is displayed.
        /// </summary>
        /// <typeparam name="T">The class of the expected page to to be displayed after granting the permission.</typeparam>
        /// <param name="alsoConfirmedDialog">Outputs <see cref="true" if an <see cref="AppDialog"/> had been confirmed as well (Android), <see cref="false"/> otherwise.</param>
        /// <param name="timeout">Maximum duration the method waits for a permission dialog.</param>
        /// <returns><see cref="true"/> if a persmission was actually requested, <see cref="false"/> otherwise.</returns>
        public static bool AllowPermissionIfRequested<T>(out bool alsoConfirmedDialog, TimeSpan? timeout = null) where T : BasePage
        {
            T expectedPage = PageHelper.CreateInstance<T>(false);
            return AllowPermissionIfRequested(expectedPage, out alsoConfirmedDialog, timeout);
        }

        /// <summary>
        /// Allows a permission if requested within the given timeout. Returns either after granting a permission or if the expected page given by <typeparamref name="T"/> is displayed.
        /// </summary>
        /// <typeparam name="T">The class of the expected page to to be displayed after granting the permission.</typeparam>
        /// <param name="timeout">Maximum duration the method waits for a permission dialog.</param>
        /// <returns><see cref="true"/> if a persmission was actually requested, <see cref="false"/> otherwise.</returns>
        public static bool AllowPermissionIfRequested<T>(TimeSpan? timeout = null) where T : BasePage
        {
            return AllowPermissionIfRequested<T>(out _, timeout);
        }

        /// <summary>
        /// Denies a permission if requested within the given timeout. Returns either after denying a permission or if <paramref name="expectedPage"/> is displayed.
        /// </summary>
        /// <typeparam name="T">The class of the expected page to to be displayed after denying the permission.</typeparam>
        /// <param name="expectedPage">The page expected to be displayed after denying the permission.</param>
        /// <param name="alsoConfirmedDialog">Outputs <see cref="true" if an <see cref="AppDialog"/> had been confirmed as well (Android), <see cref="false"/> otherwise.</param>
        /// <param name="timeout">Maximum duration the method waits for a permission dialog.</param>
        /// <returns><see cref="true"/> if a persmission was actually requested, <see cref="false"/> otherwise.</returns>
        public static bool DenyPermissionIfRequested<T>(T expectedPage, out bool alsoConfirmedDialog, TimeSpan? timeout = null) where T : BasePage
        {
            return AnswerPermissionIfRequested(expectedPage, out alsoConfirmedDialog, false, timeout);
        }

        /// <summary>
        /// Denies a permission if requested within the given timeout. Returns either after denying a permission or if <paramref name="expectedPage"/> is displayed.
        /// </summary>
        /// <typeparam name="T">The class of the expected page to to be displayed after denying the permission.</typeparam>
        /// <param name="expectedPage">The page expected to be displayed after denying the permission.</param>
        /// <param name="timeout">Maximum duration the method waits for a permission dialog.</param>
        /// <returns><see cref="true"/> if a persmission was actually requested, <see cref="false"/> otherwise.</returns>
        public static bool DenyPermissionIfRequested<T>(T expectedPage, TimeSpan? timeout = null) where T : BasePage
        {
            return DenyPermissionIfRequested(expectedPage, out _, timeout);
        }

        /// <summary>
        /// Denies a permission if requested within the given timeout. Returns either after denying a permission or if the expected page given by <typeparamref name="T"/> is displayed.
        /// </summary>
        /// <typeparam name="T">The class of the expected page to to be displayed after denying the permission.</typeparam>
        /// <param name="alsoConfirmedDialog">Outputs <see cref="true" if an <see cref="AppDialog"/> had been confirmed as well (Android), <see cref="false"/> otherwise.</param>
        /// <param name="timeout">Maximum duration the method waits for a permission dialog.</param>
        /// <returns><see cref="true"/> if a persmission was actually requested, <see cref="false"/> otherwise.</returns>
        public static bool DenyPermissionIfRequested<T>(out bool alsoConfirmedDialog, TimeSpan? timeout = null) where T : BasePage
        {
            T expectedPage = PageHelper.CreateInstance<T>(false);
            return DenyPermissionIfRequested(expectedPage, out alsoConfirmedDialog, timeout);
        }

        /// <summary>
        /// Denies a permission if requested within the given timeout. Returns either after denying a permission or if the expected page given by <typeparamref name="T"/> is displayed.
        /// </summary>
        /// <typeparam name="T">The class of the expected page to to be displayed after denying the permission.</typeparam>
        /// <param name="timeout">Maximum duration the method waits for a permission dialog.</param>
        /// <returns><see cref="true"/> if a persmission was actually requested, <see cref="false"/> otherwise.</returns>
        public static bool DenyPermissionIfRequested<T>(TimeSpan? timeout = null) where T : BasePage
        {
            return DenyPermissionIfRequested<T>(out _, timeout);
        }

        /// <summary>
        /// Denies a permission if requested within the given timeout.
        /// </summary>
        /// <param name="timeout">Maximum duration the method waits for a permission dialog.</param>
        /// <returns><see cref="true"/> if a persmission was actually requested, <see cref="false"/> otherwise.</returns>
        public static bool DenyPermissionIfRequested(TimeSpan? timeout = null)
        {
            timeout = timeout ?? Env.OPTIONAL_PERMISSION_TIMEOUT;
            if (AppManager.Platform == Platform.Android)
            {
                DialogHelper.ConfirmIfDisplayed(timeout);
            }
            var dialog = new PermissionDialog(false);
            if (dialog.IsShowingBeforeTimeout(timeout.Value))
            {
                dialog.Deny();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Dismisses all <see cref="AppDialog"/>s and <see cref="PermissionDialog"/>s for a maximum duration of <paramref name="timeout"/>.
        /// Returns either after dismissing all dialogs or if <paramref name="timeout"/> is reached.
        /// Same logic as <see cref="DialogHelper.DismissAllDialogsAndPermissionRequests(TimeSpan?)"/>
        /// </summary>
        /// <param name="timeout">The maximum duration to wait for any dialogs or permissions. Defaults to <see cref="TimeSpan.Zero"/>.</param>
        /// <returns><see cref="true"/> if any dialog or permission was dismissed, <see cref="false"/> otherwise.</returns>
        public static bool DismissAllDialogsAndPermissionRequests(TimeSpan? timeout = null)
        {
            return DialogHelper.DismissAllDialogsAndPermissionRequests(timeout);
        }

        /// <summary>
        /// This method handles <see cref="AppDialog"/>s and "Allow all the time" permission required to set location auto-start for a program 
        /// For Android-11 "Allow all the time" needs to be enabled from Settings.
        /// </summary>
        public static void HandlePermissionForGeoFenceAutomation()
        {
            if (DialogHelper.GetIsDialogDisplayed(TimeSpan.FromSeconds(10)))
            {
                ReportHelper.LogTest(Status.Info, "Dialog displayed for Permission");
                if (new AppDialog().GetIsDenyButtonVisible())
                {
                    //Android 11 Scenario
                    ReportHelper.LogTest(Status.Info, "Allow access for Android 11 Mobiles");
                    AppManager.DeviceSettings.GrantGPSBackgroundPermission();
                    ReportHelper.LogTest(Status.Info, "GPS permission is set to 'Always allow'.");
                    new AppDialog().Deny();
                    //Permission dialog dosent appears at this point in Android 11
                }
                else
                {
                    //Android 10 Scenario and also if there is no internet
                    ReportHelper.LogTest(Status.Info, "Allow access for Android 10 Mobiles");
                    DialogHelper.ConfirmIfDisplayed();
                    PermissionHelper.AllowPermissionIfRequested();
                }
            }
            else
            {
                //For lower Android version no dialog appears at this point
                ReportHelper.LogTest(Status.Info, "No dialog displayed for Permission");
            }
        }
    }
}