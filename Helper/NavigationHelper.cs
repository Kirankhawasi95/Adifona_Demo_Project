using System;
using HorusUITest.PageObjects;
using HorusUITest.PageObjects.Menu;
using HorusUITest.PageObjects.Menu.Help;
using HorusUITest.PageObjects.Start;
using HorusUITest.PageObjects.Start.Intro;

namespace HorusUITest.Helper
{
    public static class NavigationHelper
    {
        public static MainMenuPage NavigateToMainMenu(DashboardPage dashboardPage)
        {
            //dashboardPage.OpenMenuUsingSwipe();
            dashboardPage.OpenMenuUsingTap();
            return new MainMenuPage();
        }

        public static SettingsMenuPage NavigateToSettingsMenu(DashboardPage dashboardPage)
        {
            NavigateToMainMenu(dashboardPage).OpenSettings();
            return new SettingsMenuPage();
        }

        public static HelpMenuPage NavigateToHelpMenu (DashboardPage dashboardPage)
        {
            NavigateToMainMenu(dashboardPage).OpenHelp();
            return new HelpMenuPage();
        }

        public static HelpTopicsPage NavigateToHelpTopics(DashboardPage dashboardPage)
        {
            NavigateToHelpMenu(dashboardPage).OpenHelpTopics();
            return new HelpTopicsPage();
        }

        public static ProgramDetailPage NavigateToProgramDetailPage(DashboardPage dashboardPage)
        {
            dashboardPage.OpenCurrentProgram();
            return new ProgramDetailPage();
        }

        /// <summary>
        /// Compares the type <paramref name="currentPage"/> with the desired page type and verifies that it is displayed.
        /// TODO: WARNING: Currently this method only works for a select group of pages and is NOT SUITABLE FOR GENERAL USE!
        /// WARNING: This method only works in DEMO MODE.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public static void EnsurePageIsDisplayed<T>(ref T currentPage) where T : BasePage
        {
            //Early return if everything is ok
            if (currentPage != null && currentPage.IsCurrentlyShown())
                return;

            //Establishing a known starting point -> DashboardPage
            DashboardPage dashboardPage;
            BasePage page = PageHelper.WaitForAnyPageDontThrow(TimeSpan.Zero, typeof(IntroPageOne), typeof(InitializeHardwarePage), typeof(HearingAidInitPage), typeof(DashboardPage));
            switch (page)
            {
                case IntroPageOne _:
                    dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
                    break;
                case InitializeHardwarePage initPage:
                    initPage.StartDemoMode();
                    dashboardPage = new DashboardPage();
                    break;
                case HearingAidInitPage _:
                    dashboardPage = new DashboardPage();
                    break;
                case DashboardPage dbPage:
                    dashboardPage = dbPage;
                    break;
                case null:
                    AppManager.RestartApp(false);
                    dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
                    break;
                default: throw new NotImplementedException("Unknown page found.");
            }

            //Navigating to the target page
            if (typeof(T) == typeof(DashboardPage))
                currentPage = (T)(BasePage)dashboardPage;
            else if (typeof(T) == typeof(ProgramDetailPage))
                currentPage = (T)(BasePage)NavigateToProgramDetailPage(dashboardPage);
            else if (typeof(T) == typeof(MainMenuPage))
                currentPage = (T)(BasePage)NavigateToMainMenu(dashboardPage);
            else if (typeof(T) == typeof(SettingsMenuPage))
                currentPage = (T)(BasePage)NavigateToSettingsMenu(dashboardPage);
            else if (typeof(T) == typeof(HelpMenuPage))
                currentPage = (T)(BasePage)NavigateToHelpMenu(dashboardPage);
            else if (typeof(T) == typeof(HelpTopicsPage))
                currentPage = (T)(BasePage)NavigateToHelpTopics(dashboardPage);
            else throw new NotImplementedException("Navigation to the desired page is not implemented.");
        }
    }
}
