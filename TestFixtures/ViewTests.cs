using System;
using HorusUITest.Data;
using HorusUITest.Enums;
using HorusUITest.Helper;
using HorusUITest.PageObjects;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Controls.ProgramDetailParams;
using HorusUITest.PageObjects.Dialogs;
using HorusUITest.PageObjects.Favorites;
using HorusUITest.PageObjects.Favorites.Automation;
using HorusUITest.PageObjects.Interfaces;
using HorusUITest.PageObjects.Menu;
using HorusUITest.PageObjects.Menu.Help;
using HorusUITest.PageObjects.Menu.Help.HelpTopics;
using HorusUITest.PageObjects.Menu.Help.InstructionsForUse;
using HorusUITest.PageObjects.Menu.Info;
using HorusUITest.PageObjects.Menu.Programs;
using HorusUITest.PageObjects.Menu.Settings;
using HorusUITest.PageObjects.Start;
using HorusUITest.PageObjects.Start.Intro;
using NUnit.Framework;

namespace HorusUITest.TestFixtures
{
    public class ViewTests : BaseTestFixture
    {
        private bool impossibleToGetToDashboardPage = false;
        private bool impossibleToGetToIntroPageOne = false;

        private DashboardPage _dashboardPage;
        private MainMenuPage _mainMenuPage;
        private SettingsMenuPage _settingsMenuPage;
        private HelpMenuPage _helpMenuPage;
        private HelpTopicsPage _helpTopicsPage;
        private ProgramDetailPage _programDetailPage;
        private ProgramDetailSettingsControlPage _programSettingsPage;
        private ProgramAutomationPage _programAutomationPage;

        public ViewTests(Platform platform) : base(platform)
        {
        }

        private void EnsurePageIsDisplayed<T>(ref T page) where T : BasePage
        {
            T copyOfPage = page;    //Workaround in order to pass page by reference in lambda expression
            Assume.That<Action>(() => NavigationHelper.EnsurePageIsDisplayed(ref copyOfPage), Throws.Nothing, $"Precondition not met: Test case didn't start on {typeof(T).Name}.");
            page = copyOfPage;
        }

        protected T InstantiatePage<T>(int depth = 1, TimeSpan? assertOnPageTimeout = null) where T : BasePage
        {
            assertOnPageTimeout = assertOnPageTimeout ?? TimeSpan.FromSeconds(5);
            Output.TestStep($"Verifying that the page can be displayed: {typeof(T).Name}", depth);
            T page = null;
            Assert.DoesNotThrow(() => page = PageHelper.CreateInstance<T>(assertOnPageTimeout.Value), $"Failed to verify that expected {typeof(T).Name} is displayed.");
            return page;
        }

        protected BasePage InstantiatePage(Type pageType, int depth, TimeSpan? assertOnPageTimeout = null)
        {
            assertOnPageTimeout = assertOnPageTimeout ?? TimeSpan.FromSeconds(5);
            Output.TestStep($"Verifying that the page can be displayed: {pageType.Name}", depth);
            BasePage page = null;
            Assert.DoesNotThrow(() => page = PageHelper.CreateInstance(pageType, assertOnPageTimeout.Value), $"Failed to verify that expected {pageType.Name} is displayed.");
            return page;
        }

        protected T RevisitPage<T>(T pageToRevisit, Type typeOfPageToLeave = null) where T : BasePage
        {
            Assume.That(pageToRevisit, Is.Not.Null, $"Internal automation error: {pageToRevisit} was not initialized.");
            string previousPageInfo = string.Empty;
            if (typeOfPageToLeave != null)
                previousPageInfo = $" This might have been caused by a failed navigation attempt from {typeOfPageToLeave.Name} to {pageToRevisit.GetType().Name}.";
            Assert.DoesNotThrow(() => pageToRevisit.WaitForPage(), $"Failed to verify that expected {pageToRevisit.GetType().Name} is displayed.");
            return pageToRevisit;
        }

        protected T RevisitOrInstantiate<T>(ref T pageToRevisit, Type typeOfPageToLeave = null) where T : BasePage
        {
            if (pageToRevisit != null)
                pageToRevisit = RevisitPage(pageToRevisit, typeOfPageToLeave);
            else
                pageToRevisit = InstantiatePage<T>();
            return pageToRevisit;
        }

        protected void TestPage(Type pageTypeToTest, Action navigateTo, BasePage startingPage, Action additionalTestOnPage, ref BasePage pageToTest)
        {
            Assume.That(navigateTo, Is.Not.Null, $"Internal test automation error: {nameof(navigateTo)} was {null}.");
            Assume.That(startingPage, Is.Not.Null, $"Internal test automation error: {nameof(startingPage)} was {null}.");
            Assume.That(navigateTo, Throws.Nothing, $"Calling {navigateTo.Method.Name} on {startingPage.GetType().Name} didn't work.");
            pageToTest = InstantiatePage(pageTypeToTest, 1);
            Assert.DoesNotThrow(() => additionalTestOnPage(), $"Additional test on {pageTypeToTest.Name} failed.");
            Assert.DoesNotThrow(((IHasBackNavigation)pageToTest).NavigateBack, $"Failed to navigate back from {pageTypeToTest.Name} to {startingPage.GetType().Name}.");
            try
            {
                RevisitPage(startingPage, pageTypeToTest);
            }
            catch (Exception e)
            {
                //Back navigation is optional (simnplifies test execution) and not in the focus of the view test. So a warning is fine.
                Assert.Warn($"Back navigation failed. Error was: {e.Message}");
            }
        }

        protected void TestPage(Type pageTypeToTest, Action navigateTo, BasePage startingPage)
        {
            BasePage pageToTest = null;
            Action additionalTestOnPage = () => { };
            TestPage(pageTypeToTest, navigateTo, startingPage, additionalTestOnPage, ref pageToTest);
        }

        public override void BeforeAll()
        {
            base.BeforeAll();
            AppManager.InitializeApp(new AppConfig(CurrentPlatform));
            AppManager.CloseApp();              //Closing just in case the app is already running
            AppManager.StartApp(true);
        }

        public override void BeforeEachTest()
        {
            base.BeforeEachTest();
            Assume.That(impossibleToGetToDashboardPage, Is.False, "Test case could not be run. It was not possible to launch the app to the dashboard.");
        }

        public override void AfterAll()
        {
            base.AfterAll();
            AppManager.CloseApp();
            AppManager.FinishAndCleanUp();
        }

        private T AssumeOnStartupPage<T>(Action actionIfNotOnPage) where T : BasePage
        {
            T page = null;
            Assume.That<Action>(() =>
            {
                try
                {
                    Assert.That(() => PageHelper.GetIsPageShown<T>(out page), $"Test case didn't start on {typeof(T).Name}.");
                }
                catch
                {
                    try
                    {
                        DialogHelper.DismissAllDialogsAndPermissionRequests();
                        Assert.That(() => PageHelper.GetIsPageShown<T>(out page), $"Test case didn't start on {typeof(T).Name}.");
                    }
                    catch
                    {
                        Assert.IsFalse(impossibleToGetToIntroPageOne, $"Test case could not be run. The app didn't start on {nameof(IntroPageOne)} (or didn't start at all).");
                        actionIfNotOnPage();
                        page = PageHelper.CreateInstance<T>(false);
                        Assert.That(page.IsCurrentlyShown());
                    }
                }
            }, Throws.Nothing, $"Test case didn't start on {typeof(T).Name}. The attempt to navigate to the page failed as well.");
            return page;
        }

        [Test, Order(1)]
        [Category("ViewTest")]
        public void StartUp_IntroPage1CanBeDisplayed()
        {
            Assert.That<Action>(() =>
            {
                try
                {
                    InstantiatePage<IntroPageOne>();
                }
                catch
                {
                    Output.TestStep($"App didn't start on {nameof(IntroPageOne)}. Restarting the app in an attempt to fix that.");
                    try
                    {
                        AppManager.RestartApp(false);
                        DialogHelper.DismissAllDialogsAndPermissionRequests();
                        InstantiatePage<IntroPageOne>(assertOnPageTimeout: TimeSpan.FromSeconds(15));
                    }
                    catch
                    {
                        Output.TestStep($"App didn't start on {nameof(IntroPageOne)} even after restarting. Deleting app data and restarting again.");
                        try
                        {
                            AppManager.RestartApp(true);
                            InstantiatePage<IntroPageOne>(assertOnPageTimeout: TimeSpan.FromSeconds(15));
                        }
                        catch
                        {
                            impossibleToGetToIntroPageOne = true;
                            throw;
                        }
                    }
                }
            }, Throws.Nothing, $"App didn't start on {nameof(IntroPageOne)}. Deleting app data and restarting the app didn't help.");
        }

        [Test, Order(2)]
        [Category("ViewTest")]
        public void StartUp_IntroPage2CanBeDisplayed()
        {
            AssumeOnStartupPage<IntroPageOne>(StartUp_IntroPage1CanBeDisplayed).MoveRightBySwiping();
            InstantiatePage<IntroPageTwo>();
        }

        [Test, Order(3)]
        [Category("ViewTest")]
        public void StartUp_IntroPage3CanBeDisplayed()
        {
            AssumeOnStartupPage<IntroPageTwo>(StartUp_IntroPage2CanBeDisplayed).MoveRightBySwiping();
            InstantiatePage<IntroPageThree>();
        }

        [Test, Order(4)]
        [Category("ViewTest")]
        public void StartUp_IntroPage4CanBeDisplayed()
        {
            AssumeOnStartupPage<IntroPageThree>(StartUp_IntroPage3CanBeDisplayed).MoveRightBySwiping();
            InstantiatePage<IntroPageFour>();
        }

        [Test, Order(5)]
        [Category("ViewTest")]
        public void StartUp_IntroPage5CanBeDisplayed()
        {
            AssumeOnStartupPage<IntroPageFour>(StartUp_IntroPage4CanBeDisplayed).MoveRightBySwiping();
            InstantiatePage<IntroPageFive>();
        }

        [Test, Order(10)]
        [Category("ViewTest")]
        public void StartUp_HardwareInitializationCanBeDisplayed()
        {
            AssumeOnStartupPage<IntroPageFive>(StartUp_IntroPage5CanBeDisplayed).Continue();
            PermissionHelper.AllowPermissionIfRequested(new InitializeHardwarePage(false));
            while (true)
            {
                BasePage page = PageHelper.WaitForAnyPage(typeof(InitializeHardwarePage), typeof(PermissionDialog), typeof(HardwareErrorPage));
                switch (page)
                {
                    case InitializeHardwarePage _:
                        InstantiatePage<InitializeHardwarePage>();
                        return;
                    case PermissionDialog dialog:
                        dialog.Allow();
                        break;
                    case HardwareErrorPage _:
                        AppManager.DeviceSettings.EnableBluetooth();
                        InstantiatePage<InitializeHardwarePage>(assertOnPageTimeout: TimeSpan.FromSeconds(10));
                        return;
                    default:
                        throw new NotImplementedException("Unknown page detected.");
                }
            }
        }

        [Test, Order(11)]
        [Category("ViewTest")]
        public void StartUp_HearingAidSelectionCanBeDisplayed()
        {
            var initPage = AssumeOnStartupPage<InitializeHardwarePage>(StartUp_HardwareInitializationCanBeDisplayed);
            Action navigateTo = initPage.StartScan;
            TestPage(typeof(SelectHearingAidsPage), navigateTo, initPage);
        }

        [Test, Order(12)]
        [Category("ViewTest")]
        public void StartUp_HearingAidInitCanBeDisplayed()
        {
            AssumeOnStartupPage<InitializeHardwarePage>(StartUp_HardwareInitializationCanBeDisplayed).StartDemoMode();
            InstantiatePage<HearingAidInitPage>();
        }

        [Test, Order(20)]
        [Category("ViewTest")]
        public void StartUp_DashboardCanBeDisplayed()
        {
            try
            {
                LaunchHelper.StartAppInDemoModeByAnyMeans();
                _dashboardPage = InstantiatePage<DashboardPage>();
            }
            catch
            {
                try
                {
                    AppManager.RestartApp(false);
                    _dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
                }
                catch
                {
                    try
                    {
                        AppManager.RestartApp(true);
                        _dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
                    }
                    catch
                    {
                        impossibleToGetToDashboardPage = true;
                        throw;
                    }
                }
            }
        }

        [Test, Order(100)]
        [Category("ViewTest")]
        public void WhenOnDashboard_MainMenuCanBeDisplayed()
        {
            EnsurePageIsDisplayed(ref _dashboardPage);
            _dashboardPage.OpenMenuUsingSwipe();
            _mainMenuPage = InstantiatePage<MainMenuPage>();
        }

        [Test, Order(200)]
        [Category("ViewTest")]
        public void WhenOnMainMenu_ProgramsMenuCanBeDisplayed()
        {
            EnsurePageIsDisplayed(ref _mainMenuPage);
            TestPage(typeof(ProgramsMenuPage), _mainMenuPage.OpenPrograms, _mainMenuPage);
        }

        [Test, Order(300)]
        [Category("ViewTest")]
        public void WhenOnMainMenu_SettingsMenuCanBeDisplayed()
        {
            EnsurePageIsDisplayed(ref _mainMenuPage);
            _mainMenuPage.OpenSettings();
            _settingsMenuPage = InstantiatePage<SettingsMenuPage>();
        }

        [TestCase(typeof(HearingSystemManagementPage), TestName = "WhenOnSettingsMenu_MyHearingAidsCanBeDisplayed")]
        [TestCase(typeof(SettingPermissionsPage), TestName = "WhenOnSettingsMenu_PermissionsCanBeDisplayed")]
        [TestCase(typeof(SettingLanguagePage), TestName = "WhenOnSettingsMenu_LanguageCanBeDisplayed")]
        [TestCase(typeof(AppModeSelectionPage), TestName = "WhenOnSettingsMenu_DemoModeCanBeDisplayed")]
        [Order(400)]
        [Category("ViewTest")]
        public void WhenOnSettingsMenu_SettingsPagesCanBeDisplayed(Type pageType)
        {
            EnsurePageIsDisplayed(ref _settingsMenuPage);
            Action navigateTo = null;
            if (pageType == typeof(HearingSystemManagementPage))
                navigateTo = _settingsMenuPage.OpenMyHearingAids;
            else if (pageType == typeof(SettingPermissionsPage))
                navigateTo = _settingsMenuPage.OpenPermissions;
            else if (pageType == typeof(SettingLanguagePage))
                navigateTo = _settingsMenuPage.OpenLanguage;
            else if (pageType == typeof(AppModeSelectionPage))
                navigateTo = _settingsMenuPage.OpenDemoMode;
            TestPage(pageType, navigateTo, _settingsMenuPage);
        }

        [Test, Order(499)]
        [Category("ViewTest")]
        public void Utility_WhenOnSettingsMenu_CanNavigateToMainMenu()
        {
            EnsurePageIsDisplayed(ref _settingsMenuPage);
            _settingsMenuPage.TapBack();
            RevisitOrInstantiate(ref _mainMenuPage, typeof(SettingsMenuPage));
        }

        [Test, Order(500)]
        [Category("ViewTest")]
        public void WhenOnMainMenu_HelpMenuCanBeDisplayed()
        {
            EnsurePageIsDisplayed(ref _mainMenuPage);
            _mainMenuPage.OpenHelp();
            _helpMenuPage = InstantiatePage<HelpMenuPage>();
        }

        [TestCase(typeof(FindDevicesPage), TestName = "WhenOnHelpMenu_FindDevicesCanBeDisplayed")]
        [TestCase(typeof(HelpTopicsPage), TestName = "WhenOnHelpMenu_HelpTopicsCanBeDisplayed")]
        [TestCase(typeof(InstructionsForUsePage), TestName = "WhenOnHelpMenu_InstructionsCanBeDisplayed")]
        [TestCase(typeof(ImprintPage), TestName = "WhenOnHelpMenu_ImprintCanBeDisplayed")]
        [TestCase(typeof(PrivacyPolicyPage), TestName = "WhenHelpMenu_PrivacyPolicyCanBeDisplayed")]
        [TestCase(typeof(LicencesPage), TestName = "WhenOnHelpMenu_LicensesCanBeDisplayed")]
        [Order(600)]
        [Category("ViewTest")]
        public void WhenOnHelpMenu_HelpPagesCanBeDisplayed(Type pageType)
        {
            EnsurePageIsDisplayed(ref _helpMenuPage);
            Action navigateTo = null;
            Action additionalTest = null;
            BasePage pageToTest = null;
            if (pageType == typeof(FindDevicesPage))
            {
                navigateTo = () =>
                {
                    _helpMenuPage.OpenFindHearingDevices();
                    PermissionHelper.AllowPermissionIfRequested(new FindDevicesPage(false));
                };
                additionalTest = () =>
                {
                    var findPage = (FindDevicesPage)pageToTest;
                    findPage.SelectRightDevice();
                    findPage.SelectNearFieldView();
                    findPage.SelectLeftDevice();
                    findPage.SelectMapView();
                };
            }
            else if (pageType == typeof(HelpTopicsPage))
            {
                navigateTo = _helpMenuPage.OpenHelpTopics;
            }
            else if (pageType == typeof(InstructionsForUsePage))
            {
                navigateTo = _helpMenuPage.OpenInstructionsForUse;
            }
            else if (pageType == typeof(ImprintPage))
            {
                navigateTo = _helpMenuPage.OpenImprint;
            }
            else if (pageType == typeof(PrivacyPolicyPage))
            {
               // navigateTo = _helpMenuPage.OpenPrivacyPolicy;
            }
            else if (pageType == typeof(LicencesPage))
            {
                //navigateTo = _helpMenuPage.OpenLicenses;
            }
            if (additionalTest != null)
                TestPage(pageType, navigateTo, _helpMenuPage, additionalTest, ref pageToTest);
            else
                TestPage(pageType, navigateTo, _helpMenuPage);
        }

        [Test, Order(699)]
        [Category("ViewTest")]
        public void Utility_WhenOnHelpMenu_CanNavigateToHelpTopics()
        {
            EnsurePageIsDisplayed(ref _helpMenuPage);
            _helpMenuPage.OpenHelpTopics();
            _helpTopicsPage = InstantiatePage<HelpTopicsPage>();
        }

        [TestCase(typeof(ConnectHearingAidsHelpPage), TestName = "WhenOnHelpTopics_ConnectHearingAidsHelpCanBeDisplayed")]
        [TestCase(typeof(DisconnectHearingAidsHelpPage), TestName = "WhenOnHelpTopics_DisconnectHearingAidsHelpCanBeDisplayed")]
        [TestCase(typeof(MainPageHelpPage), TestName = "WhenOnHelpTopics_HomePageHelpCanBeDisplayed")]
        [TestCase(typeof(ProgramSelectionHelpPage), TestName = "WhenOnHelpTopics_ProgramSelectionHelpCanBeDisplayed")]
        [TestCase(typeof(DisconnectVolumeControlHelpPage), TestName = "WhenOnHelpTopics_DisconnectVolumeControlHelpCanBeDisplayed")]
        [TestCase(typeof(AutomaticProgramHelpPage), TestName = "WhenOnHelpTopics_AutomaticProgramHelpCanBeDisplayed")]
        [TestCase(typeof(StreamingProgramHelpPage), TestName = "WhenOnHelpTopics_StreamingProgramHelpCanBeDisplayed")]
        [TestCase(typeof(FavoritesHelpPage), TestName = "WhenOnHelpTopics_FavoritesHelpCanBeDisplayed")]
        [TestCase(typeof(MainMenuHelpPage), TestName = "WhenOnHelpTopics_MainMenuHelpCanBeDisplayed")]
        [Order(700)]
        [Category("ViewTest")]
        public void WhenOnHelpTopics_HelpTopicsPagesCanBeDisplayed(Type pageType)
        {
            EnsurePageIsDisplayed(ref _helpTopicsPage);
            Action navigateTo = null;
            if (pageType == typeof(ConnectHearingAidsHelpPage))
                navigateTo = _helpTopicsPage.OpenConnectHearingAids;
            else if (pageType == typeof(DisconnectHearingAidsHelpPage))
                navigateTo = _helpTopicsPage.OpenDisconnectHearingAids;
            else if (pageType == typeof(MainPageHelpPage))
                navigateTo = _helpTopicsPage.OpenHomePage;
            else if (pageType == typeof(ProgramSelectionHelpPage))
                navigateTo = _helpTopicsPage.OpenProgramSelection;
            else if (pageType == typeof(DisconnectVolumeControlHelpPage))
                navigateTo = _helpTopicsPage.OpenBinauralSeparation;
            else if (pageType == typeof(AutomaticProgramHelpPage))
                navigateTo = _helpTopicsPage.OpenAutomaticProgram;
            else if (pageType == typeof(StreamingProgramHelpPage))
                navigateTo = _helpTopicsPage.OpenStreamingProgram;
            else if (pageType == typeof(FavoritesHelpPage))
                navigateTo = _helpTopicsPage.OpenFavorites;
            else if (pageType == typeof(MainMenuHelpPage))
                navigateTo = _helpTopicsPage.OpenMainMenu;
            TestPage(pageType, navigateTo, _helpTopicsPage);
        }

        [Test, Order(999)]
        [Category("ViewTest")]
        public void Utility_WhenOnHelpTopics_CanNavigateToDashboard()
        {
            EnsurePageIsDisplayed(ref _helpTopicsPage);
            _helpTopicsPage.TapBack();
            RevisitOrInstantiate(ref _helpMenuPage, typeof(HelpTopicsPage)).TapBack();
            RevisitOrInstantiate(ref _mainMenuPage, typeof(HelpMenuPage)).CloseMenuUsingTap();
            RevisitOrInstantiate(ref _dashboardPage, typeof(MainMenuPage));
        }

        [TestCase(VolumeChannel.Left, TestName = "WhenOnDashboard_LeftHearingAidInformationCanBeDisplayed")]
        [TestCase(VolumeChannel.Right, TestName = "WhenOnDashboard_RightHearingAidInformationCanBeDisplayed")]
        [Order(1000)]
        [Category("ViewTest")]
        public void WhenOnDashboard_HearingAidInformationCanBeDisplayed(VolumeChannel channel)
        {
            EnsurePageIsDisplayed(ref _dashboardPage);
            Action navigateTo = null;
            if (channel == VolumeChannel.Left)
                navigateTo = _dashboardPage.OpenLeftHearingDevice;
            else if (channel == VolumeChannel.Right)
                navigateTo = _dashboardPage.OpenRightHearingDevice;
            TestPage(typeof(HearingInstrumentInfoControlPage), navigateTo, _dashboardPage);
        }

        [Test, Order(1099)]
        [Category("ViewTest")]
        public void Utility_WhenOnDashboard_CanNavigateToProgramDetails()
        {
            EnsurePageIsDisplayed(ref _dashboardPage);
            _dashboardPage.OpenCurrentProgram();
            RevisitOrInstantiate(ref _programDetailPage, typeof(DashboardPage));
        }

        [TestCase(0, TestName = "WhenOnProgramDetails_AutomaticProgramCanBeDisplayed")]
        [TestCase(1, TestName = "WhenOnProgramDetails_MusicProgramCanBeDisplayed")]
        [TestCase(2, TestName = "WhenOnProgramDetails_TinnitusProgramCanBeDisplayed")]
        [TestCase(3, TestName = "WhenOnProgramDetails_StreamingProgramCanBeDisplayed")]
        [Order(1100)]
        [Category("ViewTest")]
        public void WhenOnProgramDetails_ParamDisplaysCanBeDisplayed(int programIndex)
        {
            EnsurePageIsDisplayed(ref _programDetailPage);
            Assert.Less(programIndex, _programDetailPage.GetNumberOfVisibiblePrograms(), $"Program index was greater than (or equal to) total number of hearing programs.");
            _programDetailPage.SelectProgram(programIndex);

            switch (programIndex)
            {
                case 0:     //Automatic
                    Assert.IsTrue(_programDetailPage.GetIsAutoDisplayVisible(), $"Automatic display not found.");
                    break;
                case 1:     //Music
                    Assert.IsTrue(_programDetailPage.GetIsSpeechFocusDisplayVisible(), "Speech focus display not found.");
                    Assert.IsTrue(_programDetailPage.GetIsNoiseReductionDisplayVisible(), "Noise reduction display not found.");
                    Assert.IsTrue(_programDetailPage.GetIsTinnitusDisplayVisible(), "Tinnitus display not found.");
                    Assert.IsTrue(_programDetailPage.GetIsEqualizerDisplayVisible(), "Equalizer display not found.");
                    break;
                case 2:     //Tinnitus
                    Assert.IsTrue(_programDetailPage.GetIsTinnitusOnlyDisplayVisible(), "Tinnitus-Only display not found.");
                    break;
                case 3:     //Streaming
                    Assert.IsTrue(_programDetailPage.GetIsStreamingDisplayVisible(), "Streaming display not found.");
                    Assert.IsTrue(_programDetailPage.GetIsSpeechFocusDisplayVisible(), "Speech focus display not found.");
                    Assert.IsTrue(_programDetailPage.GetIsNoiseReductionDisplayVisible(), "Noise reduction display not found.");
                    Assert.IsTrue(_programDetailPage.GetIsEqualizerDisplayVisible(), "Equalizer display not found.");
                    break;
                default:
                    throw new NotImplementedException("Program index not covered by test case.");
            }

        }

        [TestCase(typeof(ProgramDetailParamEditSpeechFocusPage), TestName = "WhenOnProgramDetails_SpeechEditFocusCanBeDisplayed")]
        [TestCase(typeof(ProgramDetailParamEditNoiseReductionPage), TestName = "WhenOnProgramDetails_NoiseReductionEditCanBeDisplayed")]
        [TestCase(typeof(ProgramDetailParamEditTinnitusPage), TestName = "WhenOnProgramDetails_TinnitusEditCanBeDisplayed")]
        [TestCase(typeof(ProgramDetailParamEditEqualizerPage), TestName = "WhenOnProgramDetails_EqualizerEditCanBeDisplayed")]
        [TestCase(typeof(ProgramDetailParamEditStreamingPage), TestName = "WhenOnProgramDetails_StreamingEditCanBeDisplayed")]
        [TestCase(typeof(ProgramDetailParamEditBinauralPage), TestName = "WhenOnProgramDetails_BinauralEditCanBeDisplayed")]
        [Order(1200)]
        [Category("ViewTest")]
        public void WhenOnProgramDetails_ParamEditControlsCanBeDisplayed(Type pageType)
        {
            EnsurePageIsDisplayed(ref _programDetailPage);
            Action navigateTo = null;
            Action additionalTest = null;
            BasePage pageToTest = null;
            if (pageType == typeof(ProgramDetailParamEditSpeechFocusPage))
            {
                navigateTo = () =>
                {
                    _programDetailPage.SelectProgram(1);
                    _programDetailPage.SpeechFocusDisplay.OpenSettings();
                };
            }
            else if (pageType == typeof(ProgramDetailParamEditNoiseReductionPage))
            {
                navigateTo = () =>
                {
                    _programDetailPage.SelectProgram(1);
                    _programDetailPage.NoiseReductionDisplay.OpenSettings();
                };
            }
            else if (pageType == typeof(ProgramDetailParamEditTinnitusPage))
            {
                navigateTo = () =>
                {
                    _programDetailPage.SelectProgram(1);
                    _programDetailPage.TinnitusDisplay.OpenSettings();
                };
            }
            else if (pageType == typeof(ProgramDetailParamEditEqualizerPage))
            {
                navigateTo = () =>
                {
                    _programDetailPage.SelectProgram(1);
                    _programDetailPage.EqualizerDisplay.OpenSettings();
                };

            }
            else if (pageType == typeof(ProgramDetailParamEditStreamingPage))
            {
                navigateTo = () =>
                {
                    _programDetailPage.SelectProgram(3);
                    _programDetailPage.StreamingDisplay.OpenSettings();
                };

            }
            else if (pageType == typeof(ProgramDetailParamEditBinauralPage))
            {
                Assume.That(_programDetailPage.GetIsBinauralSettingsButtonVisible, "Binaural button not found.");
                navigateTo = () =>
                {
                    _programDetailPage.OpenBinauralSettings();
                };
                additionalTest = () =>
                {
                    var binauralPage = (ProgramDetailParamEditBinauralPage)pageToTest;
                    binauralPage.ToggleBinauralSwitch();
                };
            }
            if (additionalTest != null)
                TestPage(pageType, navigateTo, _programDetailPage, additionalTest, ref pageToTest);
            else
                TestPage(pageType, navigateTo, _programDetailPage);
        }

        [Test, Order(1300)]
        [Category("ViewTest")]
        public void WhenOnProgramDetails_ProgramSettingsCanBeDisplayed()
        {
            EnsurePageIsDisplayed(ref _programDetailPage);
            _programDetailPage.SelectProgram(1);
            _programDetailPage.OpenProgramSettings();
            _programSettingsPage = InstantiatePage<ProgramDetailSettingsControlPage>();
        }

        private void EnsureThatWeAreOnProgramSettingsPage()
        {
            if (_programSettingsPage == null || !_programSettingsPage.IsCurrentlyShown())
            {
                AppManager.RestartApp(false);
                _programDetailPage = NavigationHelper.NavigateToProgramDetailPage(LaunchHelper.StartAppInDemoModeByAnyMeans().Page);
                _programDetailPage.SelectProgram(1);
                _programDetailPage.OpenProgramSettings();
                _programSettingsPage = new ProgramDetailSettingsControlPage();
            }
        }

        private void EnsureThateWeAreOnProgramAutomationPage()
        {
            if (_programAutomationPage == null || !_programAutomationPage.IsCurrentlyShown())
            {
                EnsureThatWeAreOnProgramSettingsPage();
                _programSettingsPage.CreateFavorite();
                new ProgramNamePage().EnterName("MyFavorite").Proceed();
                new ProgramIconPage().SelectIcon(33).Proceed();
                _programAutomationPage = new ProgramAutomationPage();
            }
        }

        [TestCase(typeof(ProgramNamePage), TestName = "WhenOnProgramSettings_ProgramNamePageCanBeDisplayed")]
        [TestCase(typeof(ProgramIconPage), TestName = "WhenOnProgramSettings_ProgramIconPageCanBeDisplayed")]
        [Order(1400)]
        [Category("ViewTest")]
        public void WhenOnProgramSettings_ProgramSettingsPagesCanBeDisplayed(Type pageType)
        {
            Assume.That<Action>(EnsureThatWeAreOnProgramSettingsPage, Throws.Nothing, $"Failed to start the test case on {typeof(ProgramDetailSettingsControlPage).Name}.");
            Action navigateTo = null;
            if (pageType == typeof(ProgramNamePage))
                navigateTo = _programSettingsPage.CustomizeName;
            else if (pageType == typeof(ProgramIconPage))
                navigateTo = _programSettingsPage.CustomizeIcon;
            TestPage(pageType, navigateTo, _programSettingsPage);
        }

        [Test, Order(1500)]
        [Category("ViewTest")]
        public void WhenOnProgramSettings_ProgramAutomationCanBeDisplayed()
        {
            Assume.That<Action>(EnsureThatWeAreOnProgramSettingsPage, Throws.Nothing, $"Failed to start the test case on {typeof(ProgramDetailSettingsControlPage).Name}.");
            _programSettingsPage.CreateFavorite();
            new ProgramNamePage().EnterName("MyFavorite").Proceed();
            new ProgramIconPage().SelectIcon(33).Proceed();
            _programAutomationPage = InstantiatePage<ProgramAutomationPage>();
        }

        [TestCase(typeof(AutomationHelpPage), TestName = "WhenOnProgramAutomation_AutomationHelpCanBeDisplayed")]
        [TestCase(typeof(AutomationWifiBindingPage), TestName = "WhenOnProgramAutomation_WifiAutomationCanBeDisplayed")]
        [TestCase(typeof(AutomationGeofenceBindingPage), TestName = "WhenOnProgramAutomation_GeofenceAutomationCanBeDisplayed")]
        [Order(1600)]
        [Category("ViewTest")]
        public void WhenOnProgramAutomation_ProgramAutomationPagesCanBeDisplayed(Type pageType)
        {
            Assume.That<Action>(EnsureThateWeAreOnProgramAutomationPage, Throws.Nothing, $"Failed to start the test case on {typeof(ProgramAutomationPage).Name}.");
            Action navigateTo = null;
            if (pageType == typeof(AutomationHelpPage))
            {
                navigateTo = _programAutomationPage.OpenHelpPage;
            }
            else if (pageType == typeof(AutomationWifiBindingPage))
            {
                navigateTo = () =>
                {
                    _programAutomationPage.TurnOnAutomation();
                    _programAutomationPage.WifiAutomation.OpenSettings();
                };
            }
            else if (pageType == typeof(AutomationGeofenceBindingPage))
            {
                navigateTo = () =>
                {
                    _programAutomationPage.TurnOnAutomation();
                    _programAutomationPage.GeofenceAutomation.OpenSettings();
                };
            }
            TestPage(pageType, navigateTo, _programAutomationPage);
        }

        [Test, Order(1700)]
        [Category("ViewTest")]
        public void WhenOnProgramSettings_TapCancel_AppDialogIsDisplayed()
        {
            Assume.That<Action>(EnsureThateWeAreOnProgramAutomationPage, Throws.Nothing, $"Failed to start the test case on {typeof(ProgramAutomationPage).Name}.");
            _programAutomationPage.Cancel();
            var dialog = InstantiatePage<AppDialog>();
            try
            {
                dialog.Deny();
                _programAutomationPage.Proceed();
                _programDetailPage.WaitForPage(TimeSpan.FromSeconds(5));
            }
            catch
            {
                //Do nothing. Try block only serves utility purpose, so an exception is no reason to fail the test case.
            }
        }

        [Test, Order(1800)]
        [Category("ViewTest")]
        public void Utility_WhenOnProgramDetails_DeleteFavorite_ProgramDetailsAreDisplayed()
        {
            EnsurePageIsDisplayed(ref _programDetailPage);
            Assume.That(_programDetailPage.GetNumberOfVisibiblePrograms(), Is.GreaterThan(3), "App didn't seem to have the default hearing programs.");
            _programDetailPage.SelectProgram(3);
            Assume.That(_programDetailPage.GetNumberOfVisibiblePrograms(), Is.GreaterThan(4), "There was no favorite to delete.");
            _programDetailPage.SelectProgram(4);
            _programDetailPage.OpenProgramSettings();
            RevisitOrInstantiate(ref _programSettingsPage).DeleteFavoriteAndConfirm();
            RevisitOrInstantiate(ref _programDetailPage);
        }

        [Test, Order(3099)]
        [Category("ViewTest")]
        public void Utility_WhenOnProgramDetails_CanNavigateToSettingsMenu()
        {
            EnsurePageIsDisplayed(ref _programDetailPage);
            _programDetailPage.OpenMenuUsingTap();
            RevisitOrInstantiate(ref _mainMenuPage).OpenSettings();
            RevisitOrInstantiate(ref _settingsMenuPage);
        }

#pragma warning disable CS0618 // Type or member is obsolete
        [Obsolete("Tests hidden development pages")]
        [TestCase(typeof(LogPage), TestName = "WhenOnSettingsMenu_LogCanBeDisplayed_DEV")]
        [TestCase(typeof(HardwareErrorPage), TestName = "WhenOnSettingsMenu_HardwareErrorPageCanBeDisplayed_DEV")]
        [TestCase(typeof(HearingAidConnectionErrorPage), TestName = "WhenOnSettingsMenu_HearingAidConnectionErrorPageCanBeDisplayed_DEV")]
        [Order(3100)]
        [Category("ViewTest")]
        public void WhenOnSettingsMenu_DevelopmentPagesCanBeDisplayed_DEV(Type pageType)
        {
            EnsurePageIsDisplayed(ref _settingsMenuPage);
            TestDelegate navigateTo = null;
            TestDelegate navigateBack = null;
            if (pageType == typeof(LogPage))
            {
                navigateTo = _settingsMenuPage.OpenLogs;
                navigateBack = () => new LogPage(false).TapBack();
            }
            else if (pageType == typeof(HardwareErrorPage))
            {
                navigateTo = _settingsMenuPage.OpenHardwareErrorPage;
                navigateBack = () => new HardwareErrorPage(false).RetryProcess();
            }
            else if (pageType == typeof(HearingAidConnectionErrorPage))
            {
                navigateTo = _settingsMenuPage.OpenConnectionErrorPage;
                navigateBack = () => new HearingAidConnectionErrorPage(false).RetryConnection();
            }
            Assume.That(navigateTo, Throws.Nothing, $"Calling {navigateTo.Method.Name} on {_settingsMenuPage.GetType().Name} didn't work.");
            var pageToTest = InstantiatePage(pageType, 1);
            try
            {
                navigateBack();
                _settingsMenuPage.WaitForPage(TimeSpan.FromSeconds(5));
            }
            catch
            {
                //Do nothing. Try-block only serves utility purpose, so an exception is no reason to fail the test case.
            }
        }
#pragma warning restore CS0618 // Type or member is obsolete

    }
}