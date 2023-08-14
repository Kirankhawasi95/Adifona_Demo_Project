using System;
using HorusUITest.Enums;
using HorusUITest.Helper;
using HorusUITest.PageObjects;
using HorusUITest.PageObjects.Dialogs;
using HorusUITest.PageObjects.Favorites;
using HorusUITest.PageObjects.Menu;
using HorusUITest.PageObjects.Menu.Settings;
using NUnit.Framework;
using System.Threading;
using HorusUITest.PageObjects.Controls.ProgramDetailParams;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Start.Intro;
using AventStack.ExtentReports;
using HorusUITest.PageObjects.Start;

namespace HorusUITest.TestFixtures.TestCases
{
    public class SystemTestsDemo7 : BaseResettingTestFixture
    {
        #region Constructor

        public SystemTestsDemo7(Platform platform) : base(platform)
        {

        }

        #endregion Constructor

        #region Test Cases

        #region Sprint 10

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-11978_Table-55")]
        public void ST11978_VerifyProgramSubmenus()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            //Open Music Program
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            var programDetailPage = new ProgramDetailPage();
            Assert.IsNotEmpty(programDetailPage.GetCurrentProgramName());
            var currentProgramName = programDetailPage.GetCurrentProgramName();
            Assert.IsTrue(programDetailPage.GetIsBinauralSettingsButtonVisible());
            Assert.IsTrue(programDetailPage.GetIsSettingsButtonDisplayed());
            Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible());
            Assert.IsTrue(programDetailPage.GetIsTinnitusDisplayVisible());
            ReportHelper.LogTest(Status.Info, "Music program is opened successfully with music settings menu");

            //Open SpeechFocus submenu
            programDetailPage.SpeechFocusDisplay.OpenSettings();
            Assert.IsNotEmpty(new ProgramDetailParamEditSpeechFocusPage().GetDescription());
            ReportHelper.LogTest(Status.Info, "SpeechFocus submenu is opened");

            ReportHelper.LogTest(Status.Info, "Put app to standby mode for three seconds");
            AppManager.DeviceSettings.LockDevice(3);

            Assert.IsNotEmpty(new ProgramDetailParamEditSpeechFocusPage().GetDescription());
            ReportHelper.LogTest(Status.Pass, "SpeechFocus submenu is still open after lock and unlock");
            new ProgramDetailParamEditSpeechFocusPage().Close();

            Assert.AreEqual(currentProgramName, new ProgramDetailPage().GetCurrentProgramName());
            ReportHelper.LogTest(Status.Info, currentProgramName + " Program is open");

            ReportHelper.LogTest(Status.Info, "Put app to background for three seconds again");
            AppManager.DeviceSettings.PutAppToBackground(3);

            Assert.AreEqual(currentProgramName, new ProgramDetailPage().GetCurrentProgramName());
            ReportHelper.LogTest(Status.Info, currentProgramName + " Program is open when put in background and then put to background");
        }

        // After Deactivation of Multitouch this is not possible to perform close button long press and slider change parellelly since multitouch has been disabled (As a part of bug fix of Bug: 11982). Cannot be tested through Automation and needs to be tested manually.
        [Test]
        [Category("SystemTestsManual")]
        [Description("TC-11980_Table-54")]
        public void ST11980_VerifyInactiveButtonInteractions()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            //Open Music Program
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            var programDetailPage = new ProgramDetailPage();
            Assert.IsTrue(programDetailPage.GetIsSettingsButtonDisplayed());
            Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible());
            programDetailPage.SpeechFocusDisplay.OpenSettings();
            var speechFocusPage = new ProgramDetailParamEditSpeechFocusPage();
            Assert.IsNotEmpty(speechFocusPage.GetSelectedSpeechFocusName());
            var initialSpeechFocus = speechFocusPage.GetSelectedSpeechFocusName();
            Assert.IsNotEmpty(speechFocusPage.GetCloseButtonText());

            // ToDo: Need to fix Multi touch Feature for IOS App
            speechFocusPage.ChangeSettingAndClose(SpeechFocus.Auto);

            var changedSpeechFocus = speechFocusPage.GetSelectedSpeechFocusName();
            Assert.AreNotEqual(initialSpeechFocus, changedSpeechFocus);
            Assert.IsNotEmpty(speechFocusPage.GetCloseButtonText());
            Assert.IsNotEmpty(speechFocusPage.GetDescription());

            // ToDo: Need to fix Multi touch Feature for IOS App
            speechFocusPage.LongPressCloseButtonAndChangeSetting(SpeechFocus.Off);

            Assert.AreNotEqual(changedSpeechFocus, speechFocusPage.GetSelectedSpeechFocusName());
            Assert.IsNotEmpty(speechFocusPage.GetCloseButtonText());
            Assert.IsNotEmpty(speechFocusPage.GetDescription());

            speechFocusPage.Close();
            Assert.IsTrue(new ProgramDetailPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
        }

        #endregion Sprint 10

        #region Sprint 11

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-12070_Table-48")]
        public void ST12070_VerifySpeechFocusSubmenu()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            //Open Music Program
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            var programDetailPage = new ProgramDetailPage();
            Assert.IsNotEmpty(programDetailPage.GetCurrentProgramName());
            Assert.IsTrue(programDetailPage.GetIsBinauralSettingsButtonVisible());
            Assert.IsTrue(programDetailPage.GetIsSettingsButtonDisplayed());
            ReportHelper.LogTest(Status.Pass, "Music program is opened successfully");

            var previousSetting = programDetailPage.SpeechFocusDisplay.GetValue();

            foreach (SpeechFocus setting in Enum.GetValues(typeof(SpeechFocus)))
            {
                new ProgramDetailPage().SpeechFocusDisplay.OpenSettings();
                var speechFocusPage = new ProgramDetailParamEditSpeechFocusPage();
                Assert.IsNotEmpty(speechFocusPage.GetDescription());
                Assert.IsNotEmpty(speechFocusPage.GetTitle());
                Assert.IsNotEmpty(speechFocusPage.GetCloseButtonText());
                Assert.AreEqual(previousSetting, speechFocusPage.GetSelectedSpeechFocus().ToString());
                ReportHelper.LogTest(Status.Info, "Speech Focus menu is opened, existing setting is : " + previousSetting);
                speechFocusPage.SelectSpeechFocus(setting);
                Assert.AreEqual(setting, speechFocusPage.GetSelectedSpeechFocus());
                ReportHelper.LogTest(Status.Info, "Changed Speech Focus setting : " + setting);
                speechFocusPage.Close(); //Navigates back to program detail page
                Assert.IsTrue(new ProgramDetailPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
                Assert.AreEqual(setting.ToString(), new ProgramDetailPage().SpeechFocusDisplay.GetValue());
                ReportHelper.LogTest(Status.Info, "Speech Focus is closed");
                ReportHelper.LogTest(Status.Pass, "Program Detail page is visible and changed setting of Speech Focus is also displayed");
                previousSetting = setting.ToString();
            }

            ReportHelper.LogTest(Status.Info, "Open and close Speech Focus menu 10 times.");

            for (int i = 0; i <= 10; i++)
            {
                new ProgramDetailPage().SpeechFocusDisplay.OpenSettings();
                var speechFocusPage = new ProgramDetailParamEditSpeechFocusPage();
                Assert.AreEqual(previousSetting, speechFocusPage.GetSelectedSpeechFocus().ToString());
                speechFocusPage.Close();
                Assert.AreEqual(previousSetting, new ProgramDetailPage().SpeechFocusDisplay.GetValue());
                ReportHelper.LogTest(Status.Pass, "Iteration :" + i + " is done.");
            }
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-11998_Table-50")]
        public void ST11998_LanguageSelectionMenu()
        {
            ReportHelper.LogTest(Status.Info, AppManager.Brand + " App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");
            // Wait till the toast message disappears. It causes the test case to fail if it does not wait.
            Thread.Sleep(3000);

            Assert.IsTrue(dashboardPage.GetIsProgramInitFinished());
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            Assert.IsTrue(dashboardPage.GetIsLeftHearingDeviceVisible());
            Assert.IsTrue(dashboardPage.GetIsRightHearingDeviceVisible());
            Assert.NotZero(dashboardPage.GetNumberOfPrograms());
            ReportHelper.LogTest(Status.Info, "App launched successfully in demo mode");

            ReportHelper.LogTest(Status.Info, "Open Settings Menu");
            var settingsMenuPage = NavigationHelper.NavigateToSettingsMenu(dashboardPage);
            Assert.IsFalse(settingsMenuPage.GetIsDevelopmentStuffVisible());
            Assert.IsNotEmpty(settingsMenuPage.GetMyHearingAidsText());
            Assert.IsNotEmpty(settingsMenuPage.GetPermissionsText());
            Assert.IsNotEmpty(settingsMenuPage.GetLanguageText());
            Assert.IsNotEmpty(settingsMenuPage.GetDemoModeText());
            ReportHelper.LogTest(Status.Pass, "Setting menu is verifed.");

            ReportHelper.LogTest(Status.Info, "Open Language selection menu");
            settingsMenuPage.OpenLanguage();
            var settingLanguagePage = new SettingLanguagePage();

            var preSelectedLanguage = settingLanguagePage.GetSelectedLangaugeGeneric(AppManager.Brand); // preset language before changing
            Enum changedLanguage;
            ReportHelper.LogTest(Status.Info, "App is preset to " + preSelectedLanguage + " language");

            if (preSelectedLanguage.ToString().Equals("English"))
            {
                switch (AppManager.Brand)
                {
                    case Brand.Audifon:
                        settingLanguagePage.SelectLanguageAudifon(Language_Audifon.German);
                        changedLanguage = Language_Audifon.German;
                        break;
                    case Brand.Kind:
                        settingLanguagePage.SelectLanguage(Language.Dutch);
                        changedLanguage = Language.Dutch;
                        break;
                    case Brand.Puretone:
                        settingLanguagePage.SelectLanguagePuretone(Language_Puretone.Spanish);
                        changedLanguage = Language_Puretone.Spanish;
                        break;
                    case Brand.PersonaMedical:
                        settingLanguagePage.SelectLanguagePersona(Language_Persona.Spanish);
                        changedLanguage = Language_Persona.Spanish;
                        break;
                    case Brand.Hormann:
                        settingLanguagePage.SelectLanguageHormann(Language_Hormann.Spanish);
                        changedLanguage = Language_Hormann.Spanish;
                        break;
                    case Brand.RxEarsPro:
                        settingLanguagePage.SelectLanguageRxEarsPro(Language_RxEarsPro.English);
                        changedLanguage = Language_RxEarsPro.English;
                        break;
                    default: throw new NotImplementedException("Band not inplemented.");
                }

                ReportHelper.LogTest(Status.Info, changedLanguage.ToString() + " language is selected");
            }
            else
            {
                //If preset language is not English then setting to english
                //As English is common in all OEMs, no switch case based on brand
                settingLanguagePage.SelectLanguageAudifon(Language_Audifon.English);
                changedLanguage = Language_Audifon.English;
                ReportHelper.LogTest(Status.Info, "English language is selected");
            }

            ReportHelper.LogTest(Status.Info, "Put device in Standby for 10 seconds");
            AppManager.DeviceSettings.LockDevice(10);
            Thread.Sleep(2000);
            settingLanguagePage = new SettingLanguagePage();
            Assert.AreNotEqual(preSelectedLanguage, settingLanguagePage.GetSelectedLangaugeGeneric(AppManager.Brand));
            Assert.AreEqual(changedLanguage, settingLanguagePage.GetSelectedLangaugeGeneric(AppManager.Brand));
            ReportHelper.LogTest(Status.Pass, "Device is on again," + changedLanguage + " is selected.");

            ReportHelper.LogTest(Status.Info, "Put device in Background for 10 seconds");
            //few times the app dosent goes to background if sleep is not added.There has to some gap between standy and background operation
            Thread.Sleep(1000);
            AppManager.DeviceSettings.PutAppToBackground(10);
            Thread.Sleep(2000);
            settingLanguagePage = new SettingLanguagePage();
            Assert.AreNotEqual(preSelectedLanguage, settingLanguagePage.GetSelectedLangaugeGeneric(AppManager.Brand));
            Assert.AreEqual(changedLanguage, settingLanguagePage.GetSelectedLangaugeGeneric(AppManager.Brand));
            ReportHelper.LogTest(Status.Pass, "App is in foreground again," + changedLanguage + " is selected.");
            Thread.Sleep(1000); // only for demo purpose
            settingLanguagePage.Accept();
            Thread.Sleep(1000);
            Assert.IsTrue(DialogHelper.GetIsDialogDisplayed());
            Assert.IsNotEmpty(new AppDialog().GetTitle());
            Assert.IsNotEmpty(new AppDialog().GetMessage());
            Assert.IsNotEmpty(new AppDialog().GetConfirmButtonText());
            Assert.IsNotEmpty(new AppDialog().GetDenyButtonText());
            ReportHelper.LogTest(Status.Pass, "Dialog is displayed, to confirm the language change.");

            ReportHelper.LogTest(Status.Info, "Put device in Standby for 10 seconds");
            AppManager.DeviceSettings.LockDevice(10);
            Thread.Sleep(2000);
            Assert.IsTrue(DialogHelper.GetIsDialogDisplayed());
            ReportHelper.LogTest(Status.Pass, "Device is on again and dialog is still displayed.");

            ReportHelper.LogTest(Status.Info, "Put device in Background for 10 seconds");
            Thread.Sleep(1000); // failure here check again
            AppManager.DeviceSettings.PutAppToBackground(10);
            Thread.Sleep(2000);
            Assert.IsTrue(DialogHelper.GetIsDialogDisplayed());
            ReportHelper.LogTest(Status.Pass, "App is in foreground again, and dialog is still displayed.");

            DialogHelper.Confirm();
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(60)));
            ReportHelper.LogTest(Status.Pass, "Dashboard page is displayed and app language is changed to " + changedLanguage);
        }

        // Press of volume slider and close button cannot be performed parelley since multitouch has been disabled (As a part of bug fix of Bug: 11982). Cannot be tested through Automation and needs to be tested manually.
        [Test]
        [Category("SystemTestsManual")]
        [Description("TC-11983_Table-52")]
        public void ST11983_VerifyVolumeMenuSlider()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            ReportHelper.LogTest(Status.Info, "Open Music program.");
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            var programDetailPage = new ProgramDetailPage();
            Assert.IsNotEmpty(programDetailPage.GetCurrentProgramName());
            Assert.IsTrue(programDetailPage.GetIsBinauralSettingsButtonVisible());
            Assert.IsTrue(programDetailPage.GetIsSettingsButtonDisplayed());
            ReportHelper.LogTest(Status.Pass, "Music program is opened successfully.");

            programDetailPage.OpenBinauralSettings();
            var programDetailParamEditBinauralPage = new ProgramDetailParamEditBinauralPage();
            if (programDetailParamEditBinauralPage.GetIsBinauralSwitchChecked())
            {
                programDetailParamEditBinauralPage.TurnOffBinauralSeparation();
            }
            var sliderValue = programDetailParamEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Single);

            // ToDo: Need to fix Multi touch Feature for IOS App
            programDetailParamEditBinauralPage.PressVolumeSliderAndCloseMenu();

            Assert.IsTrue(new ProgramDetailPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            ReportHelper.LogTest(Status.Pass, "Volume menu is closed and Music program is visible.");
            //Test step to move the now invisible slider on programDetailPage cannot be automated.
            //As slider element which was pressed and hold, will be no longer present on current page.
            //Informed Audifon team and got confirmation for the same.

            Assert.AreEqual(sliderValue, new ProgramDetailPage().GetVolumeSliderValue(), "Volume is changed.");
            ReportHelper.LogTest(Status.Pass, "Volume remains same after perform simultanous actions.");
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-12996_Table-36")]
        public void ST12996_CreateDeleteFavorites()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            //Open Music Program
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            var programDetailPage = new ProgramDetailPage();
            Assert.IsNotEmpty(programDetailPage.GetCurrentProgramName());
            Assert.IsTrue(programDetailPage.GetIsBinauralSettingsButtonVisible());
            Assert.IsTrue(programDetailPage.GetIsSettingsButtonDisplayed());
            ReportHelper.LogTest(Status.Info, "Music program is opened successfully");

            // Open ProgramSettings (gear icon)
            programDetailPage.OpenProgramSettings();
            var programSettingsControlPage = new ProgramDetailSettingsControlPage();
            Assert.IsTrue(programSettingsControlPage.GetIsCreateFavoriteVisible());
            Assert.IsTrue(programSettingsControlPage.GetIsCustomizeIconVisible());
            Assert.IsTrue(programSettingsControlPage.GetIsCustomizeNameVisible());
            Assert.IsTrue(programSettingsControlPage.GetIsCreateFavoriteFloatingButtonDisplayed());
            ReportHelper.LogTest(Status.Pass, "Customize name, Customize icon, Create favorites and '+' are visible");

            // Create Favorite program ("1")
            programSettingsControlPage.CreateFavorite();
            var programNamePage = new ProgramNamePage();
            Assert.IsNotEmpty(programNamePage.GetDescription());
            ReportHelper.LogTest(Status.Pass, "Customize name view opened");
            programNamePage.EnterName("1");
            var favouriteProgramName_1 = programNamePage.GetName();
            programNamePage.Proceed();
            ReportHelper.LogTest(Status.Pass, "Name for the new hearing program is created");
            Assert.IsNotEmpty(new ProgramIconPage().GetDescription());
            ReportHelper.LogTest(Status.Pass, "Customize symbol view opened");
            var programIconPage = new ProgramIconPage();
            programIconPage.SelectIcon(1).Proceed();
            var programAutomationPage = new ProgramAutomationPage();
            Assert.IsNotEmpty(programAutomationPage.GetDescription());
            ReportHelper.LogTest(Status.Pass, "Auto hearing program start view opened");
            programAutomationPage = new ProgramAutomationPage();
            Assert.IsNotEmpty(programAutomationPage.GetProceedButtonText());
            ReportHelper.LogTest(Status.Pass, "The view contained Save favorite button");
            programAutomationPage.Proceed();
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(favouriteProgramName_1, programDetailPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "The saved favorite program '1' is displayed in program view");

            //Create Favorite program ("2")
            new ProgramDetailPage().OpenProgramSettings();
            new ProgramDetailSettingsControlPage().CreateFavorite();
            programNamePage = new ProgramNamePage();
            programNamePage.EnterName("2");
            var favouriteProgramName_2 = programNamePage.GetName();
            programNamePage.Proceed();
            programIconPage = new ProgramIconPage();
            programIconPage.SelectIcon(2).Proceed();
            programAutomationPage = new ProgramAutomationPage();
            Assert.IsNotEmpty(programAutomationPage.GetDescription());
            ReportHelper.LogTest(Status.Pass, "Auto hearing program start view opened");
            programAutomationPage = new ProgramAutomationPage();
            Assert.IsNotEmpty(programAutomationPage.GetProceedButtonText());
            ReportHelper.LogTest(Status.Pass, "The view contained Save favorite button");
            programAutomationPage.Proceed();
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(favouriteProgramName_2, programDetailPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "The saved favorite program '2' is displayed in program view");

            //Create Favorite program ("3")
            new ProgramDetailPage().OpenProgramSettings();
            new ProgramDetailSettingsControlPage().CreateFavorite();
            programNamePage = new ProgramNamePage();
            programNamePage.EnterName("3");
            var favouriteProgramName_3 = programNamePage.GetName();
            programNamePage.Proceed();
            programIconPage = new ProgramIconPage();
            programIconPage.SelectIcon(3).Proceed();
            programAutomationPage = new ProgramAutomationPage();
            Assert.IsNotEmpty(programAutomationPage.GetDescription());
            ReportHelper.LogTest(Status.Pass, "Auto hearing program start view opened");
            programAutomationPage = new ProgramAutomationPage();
            Assert.IsNotEmpty(programAutomationPage.GetProceedButtonText());
            ReportHelper.LogTest(Status.Pass, "The view contained Save favorite button");
            programAutomationPage.Proceed();
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(favouriteProgramName_3, programDetailPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "The saved favorite program '3' is displayed in program view");

            //Create Favorite program ("4")
            new ProgramDetailPage().OpenProgramSettings();
            new ProgramDetailSettingsControlPage().CreateFavorite();
            programNamePage = new ProgramNamePage();
            programNamePage.EnterName("4");
            var favouriteProgramName_4 = programNamePage.GetName();
            programNamePage.Proceed();
            programIconPage = new ProgramIconPage();
            programIconPage.SelectIcon(4).Proceed();
            programAutomationPage = new ProgramAutomationPage();
            Assert.IsNotEmpty(programAutomationPage.GetDescription());
            ReportHelper.LogTest(Status.Pass, "Auto hearing program start view opened");
            programAutomationPage = new ProgramAutomationPage();
            Assert.IsNotEmpty(programAutomationPage.GetProceedButtonText());
            ReportHelper.LogTest(Status.Pass, "The view contained Save favorite button");
            programAutomationPage.Proceed();
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(favouriteProgramName_4, programDetailPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "The saved favorite program '4' is displayed in program view");

            //Create Favorite program ("5") and verify message
            new ProgramDetailPage().OpenProgramSettings();
            new ProgramDetailSettingsControlPage().CreateFavorite();
            // need to verify toast message "Unfortunately, you cannot create any more favourites"
            ReportHelper.LogTest(Status.Info, "While creating fifth favourite program, App displayed message");
            new ProgramDetailSettingsControlPage().NavigateBack();
            new ProgramDetailPage().NavigateBack();

            //Open favourite "1" and delete
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 0);
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(favouriteProgramName_1, programDetailPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "The favorite program '1' is displayed in program view");
            programDetailPage.OpenProgramSettings();
            new ProgramDetailSettingsControlPage().DeleteFavoriteAndConfirm();
            new ProgramDetailPage().NavigateBack();
            Assert.AreEqual(7, new DashboardPage().GetNumberOfPrograms());
            ReportHelper.LogTest(Status.Pass, "The favorite program '1' is deleted");

            //Select favourite "2" 
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 0);
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(favouriteProgramName_2, programDetailPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "The favorite program '2' is selected");
            programDetailPage.NavigateBack();

            //Select favourite "3" 
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 1);
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(favouriteProgramName_3, programDetailPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "The favorite program '3' is selected");
            programDetailPage.NavigateBack();

            //Select favourite "4" and delete
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 2);
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(favouriteProgramName_4, programDetailPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "The favorite program '4' is selected");
            programDetailPage.OpenProgramSettings();
            new ProgramDetailSettingsControlPage().DeleteFavoriteAndConfirm();
            new ProgramDetailPage().NavigateBack();
            Assert.AreEqual(6, new DashboardPage().GetNumberOfPrograms());
            ReportHelper.LogTest(Status.Pass, "The favorite program '4' is deleted");

            //Select  favourite "2" and delete 
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 0);
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(favouriteProgramName_2, programDetailPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "The favorite program '2' is selected");
            programDetailPage.OpenProgramSettings();
            new ProgramDetailSettingsControlPage().DeleteFavoriteAndConfirm();
            new ProgramDetailPage().NavigateBack();
            Assert.AreEqual(5, new DashboardPage().GetNumberOfPrograms());
            ReportHelper.LogTest(Status.Pass, "The favorite program '2' is deleted");

            //Select  favourite "3" and delete 
            new DashboardPage().OpenProgramFromProgramsMenu(MainMenuTypes.Favorites, 0);
            programDetailPage = new ProgramDetailPage();
            programDetailPage = new ProgramDetailPage();
            Assert.AreEqual(favouriteProgramName_3, programDetailPage.GetCurrentProgramName());
            ReportHelper.LogTest(Status.Pass, "The favorite program '2' is selected");
            programDetailPage.OpenProgramSettings();
            new ProgramDetailSettingsControlPage().DeleteFavoriteAndConfirm();
            new ProgramDetailPage().NavigateBack();
            Assert.AreEqual(4, new DashboardPage().GetNumberOfPrograms());
            ReportHelper.LogTest(Status.Pass, "The favorite program '3' is deleted");
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-12513_Table-40")]
        public void ST12513_BinauralSettingExpandsQuietly()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            //Open MainMenu
            dashboardPage.OpenMenuUsingTap();
            var mainMenuPage = new MainMenuPage();
            ReportHelper.LogTest(Status.Pass, "App does not crash and Settings, Programs, Help are visible on Main Menu Page.");

            //Close Mainmenu
            mainMenuPage.CloseMenuUsingTap();
            Assert.IsTrue(new DashboardPage().IsCurrentlyShown());
            ReportHelper.LogTest(Status.Pass, "Dashboard page is visible");

            //Open Music Program
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            var programDetailPage = new ProgramDetailPage();
            ReportHelper.LogTest(Status.Info, "Music Program Opened");

            //Open Binaural button
            programDetailPage.OpenBinauralSettings();

            var paramEditBinauralPage = new ProgramDetailParamEditBinauralPage();
            paramEditBinauralPage.ToggleBinauralSwitch();
            ReportHelper.LogTest(Status.Info, "Binaural Switch Toggled");

            Assert.IsTrue(paramEditBinauralPage.GetIsVolumeControlVisible(VolumeChannel.Left));
            Assert.IsTrue(paramEditBinauralPage.GetIsVolumeControlVisible(VolumeChannel.Right));
            ReportHelper.LogTest(Status.Info, "Binaural Switch Toggled Verified");

            paramEditBinauralPage.ToggleBinauralSwitch();
            ReportHelper.LogTest(Status.Info, "Binaural Switch Toggled Back");
            Assert.IsTrue(paramEditBinauralPage.GetIsVolumeControlVisible(VolumeChannel.Single), "Volume Control is not visible on Binaural Settings Page");//Delebritely Failed TC here
            Assert.IsFalse(paramEditBinauralPage.GetIsVolumeControlVisible(VolumeChannel.Left));
            Assert.IsFalse(paramEditBinauralPage.GetIsVolumeControlVisible(VolumeChannel.Right));
            ReportHelper.LogTest(Status.Info, "Binaural Switch Toggled Back Verified");

            paramEditBinauralPage.NavigateBack();
        }

        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-12426_Table-43")]
        public void ST12426_LabelingForBinauralSeparation()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            dashboardPage.CheckStartView(dashboardPage);

            //Open MainMenu
            dashboardPage.OpenMenuUsingTap();
            var mainMenuPage = new MainMenuPage();
            ReportHelper.LogTest(Status.Info, "Main Menu Opened");
            ReportHelper.LogTest(Status.Pass, "App does not crash and Settings, Programs, Help are visible on Main Menu Page.");

            //Close Mainmenu
            mainMenuPage.CloseMenuUsingTap();
            Assert.IsTrue(new DashboardPage().IsCurrentlyShown());
            ReportHelper.LogTest(Status.Info, "Main Menu Closed");

            //Open Music Program
            dashboardPage.OpenProgramFromProgramsMenu(MainMenuTypes.Preset, 1);
            var programDetailPage = new ProgramDetailPage();
            ReportHelper.LogTest(Status.Info, "Music Program Opened");

            //Open Binaural button
            programDetailPage.OpenBinauralSettings();
            ReportHelper.LogTest(Status.Info, "Binaural Settings Opened");

            // Set parameters in Edit Binaural Page
            var paramEditBinauralPage = new ProgramDetailParamEditBinauralPage();

            // Turn on Binaural Switch
            Assert.IsFalse(paramEditBinauralPage.GetIsBinauralSwitchChecked());
            paramEditBinauralPage.TurnOnBinauralSeparation();
            Assert.IsTrue(paramEditBinauralPage.GetIsBinauralSwitchChecked());
            ReportHelper.LogTest(Status.Info, "Binaural Switch turned on");

            // Checking values
            bool singleVisibleInitial = paramEditBinauralPage.GetIsVolumeControlVisible(VolumeChannel.Single);
            bool leftVisibleInitial = paramEditBinauralPage.GetIsVolumeControlVisible(VolumeChannel.Left);
            bool rightVisibleInitial = paramEditBinauralPage.GetIsVolumeControlVisible(VolumeChannel.Right);
            Assert.IsFalse(singleVisibleInitial);
            Assert.IsTrue(leftVisibleInitial);
            Assert.IsTrue(rightVisibleInitial);
            ReportHelper.LogTest(Status.Info, "Value check successfull");
            const int sliderStepCount = 21;
            double tolerance = 1f / sliderStepCount;
            //if (OniOS) tolerance *= 2;      //HACK: iOS swiping is less precise
            tolerance *= 2;

            //VolumeControl to 0db
            double SetVolumeControltoZeroDB(VolumeChannel channel)
            {
                paramEditBinauralPage.SetVolumeSliderValue(channel, 0.5);
                var singleVolume50 = paramEditBinauralPage.GetVolumeSliderValue(channel);
                Assert.AreEqual(0.5, singleVolume50, tolerance);

                return singleVolume50;
            }

            //Left volume control to 0db
            var Leftto0db = SetVolumeControltoZeroDB(VolumeChannel.Left);
            ReportHelper.LogTest(Status.Info, "Value check for Left successfull");

            //Right volume control to 0db
            var Rightto0db = SetVolumeControltoZeroDB(VolumeChannel.Right);
            ReportHelper.LogTest(Status.Info, "Value check for Right successfull");

            //Increase Left channelVolume by 1db
            var leftfirstIncrement = paramEditBinauralPage.IncreaseVolume(VolumeChannel.Left);
            Assert.AreEqual(0.6, paramEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Left), tolerance);
            ReportHelper.LogTest(Status.Info, "Value check for first Left increment successfull");
            ReportHelper.LogTest(Status.Pass, "App displayed message:" + paramEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Left));

            //Increase Right channelVolume by 1db
            //the value is incremented by 0.05(actual value is 0.55,but it is rouding of 0.6,so we are validating it with 0.6)
            var rightfirstIncrement = paramEditBinauralPage.IncreaseVolume(VolumeChannel.Right);
            Assert.AreEqual(0.6, paramEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Right), tolerance);
            ReportHelper.LogTest(Status.Info, "Value check for first Right increment successfull");
            ReportHelper.LogTest(Status.Pass, "App displayed message:" + paramEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Right));

            //Increase Right channelVolume by 1db again
            var right2ndIncrement = paramEditBinauralPage.IncreaseVolume(VolumeChannel.Right);
            Assert.AreEqual(0.6, paramEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Right), tolerance);
            ReportHelper.LogTest(Status.Info, "Value check for second Right increment successfull");
            ReportHelper.LogTest(Status.Pass, "App displayed message:" + paramEditBinauralPage.GetVolumeSliderValue(VolumeChannel.Right));
        }

        #endregion Sprint 11

        #region Sprint 12
        [Test]
        [Category("SystemTestsDemo")]
        [Description("TC-12249_Table-44")]
        public void ST12249_VerifyHearingAidInformation()
        {
            ReportHelper.LogTest(Status.Info, "App initialized successfully");

            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.WaitUntilProgramInitFinished();
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            dashboardPage.WaitForToastToDisappear();
            ReportHelper.LogTest(Status.Info, "App started in demo mode");

            //Open Mainmenu
            Assert.IsTrue(dashboardPage.GetIsMenuHamburgerButtonDisplayed());
            dashboardPage.OpenMenuUsingTap();
            var mainMenuPage = new MainMenuPage();
            Assert.IsNotEmpty(mainMenuPage.GetSettingsText());
            ReportHelper.LogTest(Status.Pass, "The main menu is opened.");

            //Open Settings
            mainMenuPage.OpenSettings();
            var settingsMenuPage = new SettingsMenuPage();
            Assert.IsNotEmpty(settingsMenuPage.GetMyHearingAidsText());
            Assert.IsNotEmpty(settingsMenuPage.GetPermissionsText());
            Assert.IsNotEmpty(settingsMenuPage.GetLanguageText());
            Assert.IsNotEmpty(settingsMenuPage.GetDemoModeText());
            ReportHelper.LogTest(Status.Pass, "The Settings menu is opened all items are visible.");

            settingsMenuPage.OpenMyHearingAids();

            var hearingSystemsPage = new HearingSystemManagementPage().CheckHAInformationFromSettings(AppMode.Demo);

            hearingSystemsPage.NavigateBack();
            Assert.IsTrue(new SettingsMenuPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            new SettingsMenuPage().NavigateBack();
            Assert.IsTrue(new MainMenuPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            new MainMenuPage().CloseMenuUsingTap();
            Assert.IsTrue(new DashboardPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));

            ReportHelper.LogTest(Status.Info, "Opening Left device info page from Dashboard.");
            new DashboardPage().OpenLeftHearingDevice();
            var hearingInstrumentInfoPage = new HearingInstrumentInfoControlPage().CheckHAInformationFromDashboard(AppMode.Demo);
            hearingInstrumentInfoPage.Close();
            ReportHelper.LogTest(Status.Pass, "All details are visible on Left Hearing Instrument Info page.");

            ReportHelper.LogTest(Status.Info, "Opening Right device info page from Dashboard.");
            new DashboardPage().OpenRightHearingDevice();
            hearingInstrumentInfoPage = new HearingInstrumentInfoControlPage().CheckHAInformationFromDashboard(AppMode.Demo);
            hearingInstrumentInfoPage.Close();
            ReportHelper.LogTest(Status.Pass, "All details are visible on Right Hearing Instrument Info page.");

            ReportHelper.LogTest(Status.Info, "Exiting Demo mode.");
            NavigationHelper.NavigateToSettingsMenu(new DashboardPage()).OpenDemoMode();
            var appModeSelectionPage = new AppModeSelectionPage();
            Assert.IsNotEmpty(appModeSelectionPage.GetAppModeText(AppMode.Demo));
            Assert.IsNotEmpty(appModeSelectionPage.GetAppModeText(AppMode.Normal));
            appModeSelectionPage.ChangeAppMode(AppMode.Normal);

            // After setting to normal mode the app lands in InitializeHardwarePage only for Kind
            if (AppManager.Brand == Brand.Kind)
            {
                Assert.IsTrue(new InitializeHardwarePage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
                ReportHelper.LogTest(Status.Pass, "Exited Demo Mode, InitializeHardwarePage screen is visible.");
            }
            else
            {
                Assert.IsTrue(new IntroPageOne().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
                ReportHelper.LogTest(Status.Pass, "Exited Demo Mode, Welcome screen is visible.");
            }
        }

        #endregion Sprint 12

        #endregion Test Cases
    }
}