using System;
using System.Diagnostics;
using HorusUITest.Configuration;
using HorusUITest.PageObjects;
using HorusUITest.PageObjects.Dialogs;

namespace HorusUITest.Helper
{
    public static class DialogHelper
    {
        /// <summary>
        /// Confirms the dialog. Throws if there's no dialog.
        /// </summary>
        public static void Confirm()
        {
            new AppDialog().Confirm();
        }

        /// <summary>
        /// Denies the dialog. Throws if there's no dialog or if the dialog doesn't have an option to deny it.
        /// </summary>
        public static void Deny()
        {
            new AppDialog().Deny();
        }

        /// <summary>
        /// Confirms an <see cref="AppDialog>"/> if displayed. Returns <see cref="true"/> if a dialog was actually displayed, <see cref="false"/> otherwise.
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns>Returns <see cref="true"/> if a dialog was actually displayed, <see cref="false"/> otherwise.</returns>
        public static bool ConfirmIfDisplayed(TimeSpan? timeout = null)
        {
            timeout = timeout ?? Env.OPTIONAL_DIALOG_TIMEOUT;
            var dialog = new AppDialog(false);
            if (dialog.IsShowingBeforeTimeout(timeout.Value))
            {
                dialog.Confirm();
                return true;
            }
            return false;
        }

        private static bool AnswerIfDisplayed<T>(T expectedPage, bool confirmDialog, TimeSpan? timeout = null) where T : BasePage
        {
            timeout = timeout ?? Env.OPTIONAL_DIALOG_TIMEOUT;
            while (true)
            {
                BasePage page = PageHelper.WaitForAnyPageDontThrow(timeout.Value, new AppDialog(false), expectedPage);
                switch (page)
                {
                    case AppDialog appDialog:
                        if (confirmDialog)
                            appDialog.Confirm();
                        else
                            appDialog.Deny();
                        return true;
                    case T _:
                        return DialogHelper.ConfirmIfDisplayed(TimeSpan.Zero);
                }
            }
        }

        /// <summary>
        /// Confirms an <see cref="AppDialog>"/> if displayed within the given timeout. Returns either after confirming a dialog or if <paramref name="expectedPage"/> is displayed.
        /// </summary>
        /// <typeparam name="T">Type of the expected page.</typeparam>
        /// <param name="expectedPage">The page expected to be displayed after confirming the dialog.</param>
        /// <param name="timeout">Maximum duration the method waits for a dialog.</param>
        /// <returns>Returns <see cref="true"/> if a dialog was actually displayed, <see cref="false"/> otherwise.</returns>
        public static bool ConfirmIfDisplayed<T>(T expectedPage, TimeSpan? timeout = null) where T : BasePage
        {
            return AnswerIfDisplayed(expectedPage, true, timeout);
        }

        /// <summary>
        /// Confirms an <see cref="AppDialog>"/> if displayed within the given timeout. Returns either after confirming a dialog or if the expected page given by <typeparamref name="T"/> is displayed.
        /// </summary>
        /// <typeparam name="T">The class of the expected page to to be displayed after confirming the dialog.</typeparam>
        /// <param name="timeout">Maximum duration the method waits for a dialog.</param>
        /// <returns>Returns <see cref="true"/> if a dialog was actually displayed, <see cref="false"/> otherwise.</returns>
        public static bool ConfirmIfDisplayed<T>(TimeSpan? timeout = null) where T : BasePage
        {
            T expectedPage = PageHelper.CreateInstance<T>(false);
            return ConfirmIfDisplayed(expectedPage, timeout);
        }

        /// <summary>
        /// Denies an <see cref="AppDialog>"/> if displayed within the given timeout. Returns either after denying a dialog or if <paramref name="expectedPage"/> is displayed.
        /// </summary>
        /// <typeparam name="T">The class of the expected page to to be displayed after denying the dialog.</typeparam>
        /// <param name="expectedPage">The page expected to be displayed after confirming the dialog.</param>
        /// <param name="timeout">Maximum duration the method waits for a dialog.</param>
        /// <returns>Returns <see cref="true"/> if a dialog was actually displayed, <see cref="false"/> otherwise.</returns>
        public static bool DenyIfDisplayed<T>(T expectedPage, TimeSpan? timeout = null) where T : BasePage
        {
            return AnswerIfDisplayed(expectedPage, false, timeout);
        }

        /// <summary>
        /// Denies an <see cref="AppDialog>"/> if displayed within the given timeout. Returns either after denying a dialog or if the expected page given by <typeparamref name="T"/> is displayed.
        /// </summary>
        /// <typeparam name="T">The class of the expected page to to be displayed after denying the dialog.</typeparam>
        /// <param name="timeout">Maximum duration the method waits for a dialog.</param>
        /// <returns>Returns <see cref="true"/> if a dialog was actually displayed, <see cref="false"/> otherwise.</returns>
        public static bool DenyIfDisplayed<T>(TimeSpan? timeout = null) where T : BasePage
        {
            T expectedPage = PageHelper.CreateInstance<T>(false);
            return DenyIfDisplayed(expectedPage, timeout);
        }

        /// <summary>
        /// Denies an <see cref="AppDialog>"/> if displayed. Returns <see cref="true"/> if a dialog was actually displayed, <see cref="false"/> otherwise.
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns>Returns <see cref="true"/> if a dialog was actually displayed, <see cref="false"/> otherwise.</returns>
        public static bool DenyIfDisplayed(TimeSpan? timeout = null)
        {
            timeout = timeout ?? Env.OPTIONAL_DIALOG_TIMEOUT;
            var dialog = new AppDialog(false);
            if (dialog.IsShowingBeforeTimeout(timeout.Value))
            {
                dialog.Deny();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns whether or not an <see cref="AppDialog"/> is displayed within the given amount of time.
        /// </summary>
        /// <param name="timeout">How long to wait for a dialog.</param>
        /// <returns>Return <see cref="true"/> if a dialog is displayed before timeout, <see cref="false"/> otherwise</returns>.
        public static bool GetIsDialogDisplayed(TimeSpan timeout)
        {
            var dialog = new AppDialog(false);
            return dialog.IsShowingBeforeTimeout(timeout);
        }

        /// <summary>
        /// Returns whether or not an <see cref="AppDialog"/> is currently displayed on the screen.
        /// </summary>
        /// <returns>Return <see cref="true"/> if a dialog is displayed, <see cref="false"/> otherwise</returns>.
        public static bool GetIsDialogDisplayed()
        {
            return GetIsDialogDisplayed(TimeSpan.Zero);
        }

        /// <summary>
        /// Dismisses all <see cref="AppDialog"/>s and <see cref="PermissionDialog"/>s for a maximum duration of <paramref name="timeout"/>.
        /// Returns either after dismissing all dialogs or if <paramref name="timeout"/> is reached.
        /// Same logic as <see cref="PermissionHelper.DismissAllDialogsAndPermissionRequests(TimeSpan?)"/>
        /// </summary>
        /// <param name="timeout">The maximum duration to wait for any dialogs or permissions. Defaults to <see cref="TimeSpan.Zero"/>.</param>
        /// <returns><see cref="true"/> if any dialog or permission was dismissed, <see cref="false"/> otherwise.</returns>
        public static bool DismissAllDialogsAndPermissionRequests(TimeSpan? timeout = null)
        {
            timeout = timeout ?? TimeSpan.Zero;
            BasePage[] pages = new BasePage[2];
            pages[0] = new AppDialog(false);
            pages[1] = new PermissionDialog(false);
            Stopwatch timer = new Stopwatch();
            timer.Start();
            bool didSomething = false;
            bool finished = false;
            while (!finished)
            {
                var page = PageHelper.WaitForAnyPageDontThrow(TimeSpan.Zero, pages);
                switch (page)
                {
                    case AppDialog appDialog:
                        appDialog.Confirm();
                        didSomething = true;
                        break;
                    case PermissionDialog permissionDialog:
                        permissionDialog.Allow();
                        didSomething = true;
                        break;
                    case null:
                        if (didSomething || timer.Elapsed > timeout.Value)
                            finished = true;
                        break;
                    default:
                        throw new Exception("Unexpected page found.");
                }
            }
            return didSomething;
        }
    }
}
