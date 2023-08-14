using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using HorusUITest.Enums;
using HorusUITest.Helper;
using HorusUITest.PageObjects;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Controls.ProgramDetailParams;
using HorusUITest.PageObjects.Dialogs;
using HorusUITest.PageObjects.Favorites;
using HorusUITest.PageObjects.Favorites.Automation;
using HorusUITest.PageObjects.Menu;
using HorusUITest.PageObjects.Menu.Help;
using HorusUITest.PageObjects.Menu.Help.HelpTopics;
using HorusUITest.PageObjects.Menu.Info;
using HorusUITest.PageObjects.Menu.Programs;
using HorusUITest.PageObjects.Menu.Settings;
using HorusUITest.PageObjects.Start;
using HorusUITest.PageObjects.Start.Intro;
using NUnit.Framework;
using Platform = HorusUITest.Enums.Platform;

namespace HorusUITest.TestFixtures
{
    /// <summary>
    /// This fixture tests the testing project itself. Each test navigates to a target view and verifies
    /// that every public functionality of the corresponding page objects work as intended.
    /// </summary>
    public class SelfTests : BaseResettingTestFixture
    {
        public SelfTests(Platform platform)
            : base(platform)
        {
        }

        [Test]
        [Category("SelfTest")]
        public void SettingsMenu()
        {
            LaunchHelper.StartAppInDemoModeByAnyMeans().Page
                .OpenMenuUsingSwipe();
            new MainMenuPage().OpenSettings();
            var settings = new SettingsMenuPage();
            Assert.IsNotEmpty(settings.GetNavigationBarTitle());

            string name2 = settings.MenuItems.Get(2, IndexType.Relative);
            string name0 = settings.MenuItems.Get(0, IndexType.Relative);
            string name1 = settings.MenuItems.Get(1, IndexType.Relative);
            string name3 = settings.MenuItems.Get(3, IndexType.Relative);
            var settingsItems = settings.MenuItems.GetAllVisible();
            Assert.IsNotEmpty(settingsItems);
            foreach (var s in settingsItems)
            {
                Assert.IsNotEmpty(s);
            }
            settings.TapBack();
            new MainMenuPage().OpenSettings();
            Assert.IsTrue(settings.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));
            Assert.AreEqual(name0, settingsItems[0]);
            Assert.AreEqual(name1, settingsItems[1]);
            Assert.AreEqual(name2, settingsItems[2]);
            Assert.AreEqual(name3, settingsItems[3]);
            Assert.AreEqual(name0, settings.MenuItems.Get(0, IndexType.Relative));
            Assert.AreEqual(name1, settings.MenuItems.Get(1, IndexType.Relative));
            Assert.AreEqual(name2, settings.MenuItems.Get(2, IndexType.Relative));
            Assert.AreEqual(name3, settings.MenuItems.Get(3, IndexType.Relative));

            Assert.IsNotEmpty(settings.GetDemoModeText());
            Assert.IsNotEmpty(settings.GetLanguageText());
            Assert.IsNotEmpty(settings.GetMyHearingAidsText());
            Assert.IsNotEmpty(settings.GetPermissionsText());
            Assert.IsNotEmpty(settings.GetAppResetText());
            Assert.IsNotEmpty(settings.GetLogsText());

            //My hearing aids
            settings.OpenMyHearingAids();
            new HearingSystemManagementPage().TapBack();
            Assert.IsTrue(settings.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));

            //Permissions
            settings.OpenPermissions();
            new SettingPermissionsPage().TapBack();
            Assert.IsTrue(settings.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));

            //Language
            settings.OpenLanguage();
            new SettingLanguagePage().TapBack();
            Assert.IsTrue(settings.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));

            //Demo Mode
            settings.OpenDemoMode();
            new AppModeSelectionPage().TapBack();
            Assert.IsTrue(settings.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));

            //Logs
            settings.OpenLogs();
#pragma warning disable CS0618 // Type or member is obsolete
            new LogPage().TapBack();
#pragma warning restore CS0618 // Type or member is obsolete
            Assert.IsTrue(settings.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));

            //App reset
            settings.OpenAppReset();
            PermissionHelper.AllowPermissionIfRequested();
            new InitializeHardwarePage();
        }

        [Test]
        [Category("SelfTest")]
        public void HearingAidsPage()
        {
            LaunchHelper.StartAppInDemoModeByAnyMeans().Page.OpenMenuUsingSwipe();
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenMyHearingAids();

            var hearing = new HearingSystemManagementPage();
            Assert.IsNotEmpty(hearing.GetNavigationBarTitle());

            Assert.IsTrue(hearing.GetIsLeftDeviceVisible());
            Assert.IsTrue(hearing.GetIsRightDeviceVisible());

            string leftName = hearing.GetLeftDeviceName();
            string leftIcon = hearing.GetLeftDeviceIconText();
            string leftSerial = hearing.GetLeftDeviceSerial();
            string leftState = hearing.GetLeftDeviceState();
            string leftType = hearing.GetLeftDeviceType();
            string leftNameTitle = hearing.GetLeftDeviceNameTitle();
            string leftTypeTitle = hearing.GetLeftDeviceTypeTitle();
            string leftSerialTitle = hearing.GetLeftDeviceSerialTitle();
            string leftStateTitle = hearing.GetLeftDeviceStateTitle();

            string rightName = hearing.GetRightDeviceName();
            string rightIcon = hearing.GetRightDeviceIconText();
            string rightSerial = hearing.GetRightDeviceSerial();
            string rightState = hearing.GetRightDeviceState();
            string rightType = hearing.GetRightDeviceType();
            string rightNameTitle = hearing.GetRightDeviceNameTitle();
            string rightTypeTitle = hearing.GetRightDeviceTypeTitle();
            string rightSerialTitle = hearing.GetRightDeviceSerialTitle();
            string rightStateTitle = hearing.GetRightDeviceStateTitle();

            Assert.IsNotEmpty(leftName);
            Assert.IsNotEmpty(leftIcon);
            Assert.IsNotEmpty(leftSerial);
            Assert.IsNotEmpty(leftState);
            Assert.IsNotEmpty(leftType);
            Assert.IsNotEmpty(leftNameTitle);
            Assert.IsNotEmpty(leftTypeTitle);
            Assert.IsNotEmpty(leftSerialTitle);
            Assert.IsNotEmpty(leftStateTitle);

            Assert.IsNotEmpty(rightName);
            Assert.IsNotEmpty(rightIcon);
            Assert.IsNotEmpty(rightSerial);
            Assert.IsNotEmpty(rightState);
            Assert.IsNotEmpty(rightType);
            Assert.IsNotEmpty(rightNameTitle);
            Assert.IsNotEmpty(rightTypeTitle);
            Assert.IsNotEmpty(rightSerialTitle);
            Assert.IsNotEmpty(rightStateTitle);

            //hearing.OpenManageHearingSystems();
            //pageXYZ.TapBack()

            Assert.IsTrue(hearing.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));
            hearing.TapBack();
            new SettingsMenuPage();
        }

        [Test]
        [Category("SelfTest")]
        public void AppDialog()
        {
            LaunchHelper.StartAppInDemoModeByAnyMeans().Page.OpenMenuUsingSwipe();

            //"Yes-No" dialog in language settings
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenLanguage();
            var page = new SettingLanguagePage();
            var initialLanguage = page.GetSelectedLanguage();
            HashSet<Language> languages = new HashSet<Language>() { Language.English, Language.German };
            languages.Remove(initialLanguage);
            var otherLanguage = languages.First();

            page.SelectLanguage(otherLanguage)
                .Accept();

            var dialog = new AppDialog();
            string title1 = dialog.GetTitle();
            string message1 = dialog.GetMessage();
            string no = dialog.GetDenyButtonText();
            string yes = dialog.GetConfirmButtonText();
            int buttonCount1 = dialog.GetNumberOfButtons();
            int textCount1 = dialog.GetNumberOfTexts();

            Assert.IsNotEmpty(title1);
            Assert.IsNotEmpty(message1);
            Assert.IsNotEmpty(yes);
            Assert.IsNotEmpty(no);
            Assert.AreEqual(2, buttonCount1);
            Assert.AreEqual(2, textCount1);
            Assert.AreEqual(title1, dialog.GetText(0));
            Assert.AreEqual(message1, dialog.GetText(1));
            Assert.AreEqual(no, dialog.GetButtonText(0));
            Assert.AreEqual(yes, dialog.GetButtonText(1));

            dialog.TapButton(0);
            Thread.Sleep(5000);
            Assert.IsTrue(page.IsCurrentlyShown());
            page.SelectLanguage(otherLanguage)
                .Accept();
            new AppDialog().Deny();
            Thread.Sleep(5000);
            Assert.IsTrue(page.IsCurrentlyShown());

            page.SelectLanguage(otherLanguage)
                .Accept();
            new AppDialog().TapButton(1);
            new DashboardPage().OpenMenuUsingSwipe();
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenLanguage();
            new SettingLanguagePage().SelectLanguage(initialLanguage)
                .Accept();
            new AppDialog().Confirm();

            //"Okay" dialog in theme settings
            new DashboardPage().OpenMenuUsingSwipe();
            new MainMenuPage().OpenSettings();

            //TODO: Darstellungs-Menü wurde entfernt -> anderen Dialog mit nur einer Option, wie z.B. "Okay" finden.
        }

        [Test]
        [Category("SelfTest")]
        public void SettingPermissionsPage()
        {
            LaunchHelper.StartAppInDemoModeByAnyMeans().Page.OpenMenuUsingSwipe();
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenPermissions();
            var page = new SettingPermissionsPage();

            string navBarTitle = page.GetNavigationBarTitle();
            string header = page.GetLocationGroupHeader();
            string label = page.GetLocationPermissionText();
            Assert.IsNotEmpty(navBarTitle);
            Assert.IsNotEmpty(header);
            Assert.IsNotEmpty(label);

            bool checkedInitial = page.GetIsLocationPermissionSwitchChecked();
            page.ToggleLocationSwitch();
            bool checkedAfterToggle = page.GetIsLocationPermissionSwitchChecked();
            Assert.AreNotEqual(checkedInitial, checkedAfterToggle);

            page.TurnOffLocationPermission();
            bool checkedAfterTurningOff1 = page.GetIsLocationPermissionSwitchChecked();
            page.TurnOffLocationPermission();
            bool checkedAfterTurningOff2 = page.GetIsLocationPermissionSwitchChecked();
            Assert.IsFalse(checkedAfterTurningOff1);
            Assert.IsFalse(checkedAfterTurningOff2);

            page.TurnOnLocationPermission();
            bool checkedAfterTurningOn1 = page.GetIsLocationPermissionSwitchChecked();
            page.TurnOnLocationPermission();
            bool checkedAfterTurningOn2 = page.GetIsLocationPermissionSwitchChecked();
            Assert.IsTrue(checkedAfterTurningOn1);
            Assert.IsTrue(checkedAfterTurningOn2);

            page.TapBack();
            new SettingsMenuPage();
        }

        [Test]
        [Category("SelfTest")]
        public void SettingLanguagePage()
        {
            LaunchHelper.StartAppInDemoModeByAnyMeans().Page.OpenMenuUsingSwipe();
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenLanguage();
            var page = new SettingLanguagePage();

            string navBarTitle = page.GetNavigationBarTitle();
            string title = page.GetTitle();
            string german = page.GetLanguageText(Language.German);
            string english = page.GetLanguageText(Language.English);
            string italian = page.GetLanguageText(Language.Italian);
            string selected = page.GetSelectedLanguageText();
            string current = page.GetCurrentLanguageText();

            Assert.IsNotEmpty(navBarTitle);
            Assert.IsNotEmpty(title);
            Assert.IsNotEmpty(german);
            Assert.IsNotEmpty(english);
            Assert.IsNotEmpty(italian);
            Assert.IsNotEmpty(selected);
            Assert.IsNotEmpty(current);

            var initialLanguage = page.GetSelectedLanguage();
            Assert.AreEqual(initialLanguage, page.GetCurrentLanguage());
            HashSet<Language> allLanguages = new HashSet<Language>((Language[])Enum.GetValues(typeof(Language)));
            HashSet<Language> languages = new HashSet<Language>(allLanguages);
            languages.Remove(initialLanguage);

            foreach (var currentLanguage in allLanguages)
            {
                page.SelectLanguage(currentLanguage);
                foreach (var l in allLanguages)
                {
                    if (l == currentLanguage)
                        Assert.AreEqual(l, page.GetSelectedLanguage());
                    else
                        Assert.AreNotEqual(l, page.GetSelectedLanguage());
                }
            }

            var testLanguage = languages.First();
            languages.Remove(testLanguage);
            page.SelectLanguage(testLanguage);
            Assert.AreEqual(testLanguage, page.GetSelectedLanguage());
            page.Accept();
            new AppDialog().Deny();
            Assert.AreEqual(testLanguage, page.GetSelectedLanguage());
            page.SelectLanguage(testLanguage).Accept();
            new AppDialog().Confirm();

            new DashboardPage().OpenMenuUsingSwipe();
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenLanguage();
            Assert.IsTrue(page.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            Assert.AreEqual(testLanguage, page.GetSelectedLanguage());

            foreach (var l in languages)
            {
                page.ChangeLanguage(l);
                new DashboardPage().OpenMenuUsingSwipe();
                new MainMenuPage().OpenSettings();
                new SettingsMenuPage().OpenLanguage();
                Assert.IsTrue(page.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
                Assert.AreEqual(l, page.GetSelectedLanguage());
                Assert.AreEqual(l, page.GetCurrentLanguage());
            }

            page.ChangeLanguage(initialLanguage);
            new DashboardPage().OpenMenuUsingSwipe();
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenLanguage();
            Assert.IsTrue(page.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)));
            Assert.AreEqual(initialLanguage, page.GetSelectedLanguage());

            page.TapBack();
            new SettingsMenuPage();
        }

        [Test]
        [Category("SelfTest")]
        public void HelpMenu()
        {
            LaunchHelper.StartAppInDemoModeByAnyMeans().Page.OpenMenuUsingSwipe();
            new MainMenuPage().OpenHelp();
            var page = new HelpMenuPage();

            string navBarTitle = page.GetNavigationBarTitle();
            Assert.IsNotEmpty(navBarTitle);

            Assert.IsTrue(page.GetIsScrolledToTop());
            var itemsTop = page.MenuItems.GetAllVisible();
            var allItems1 = page.MenuItems.GetAll();
            int allCount1 = page.MenuItems.CountAll();

            Assert.IsTrue(page.GetIsScrolledToBottom());
            var itemsBot = page.MenuItems.GetAllVisible();

            int allCount2 = page.MenuItems.CountAll();
            var allItems2 = page.MenuItems.GetAll();

            Assert.AreEqual(allCount1, allCount2);
            for (int i = 0; i < allCount1; i++)
            {
                Assert.IsNotEmpty(allItems1[i]);
                Assert.AreEqual(allItems1[i], allItems2[i]);
            }

            page.ScrollToTop();

            for (int i = 0; i < itemsTop.Count; i++)
            {
                Assert.IsNotEmpty(itemsTop[i]);
                Assert.AreEqual(itemsTop[i], page.MenuItems.Get(i, IndexType.Relative));
            }

            page.ScrollToBottom();

            for (int i = 0; i < itemsBot.Count; i++)
            {
                Assert.IsNotEmpty(itemsBot[i]);
                Assert.AreEqual(itemsBot[i], page.MenuItems.Get(i, IndexType.Relative));
            }

            Assert.AreEqual(itemsTop[0], allItems1[0]);
            Assert.AreEqual(itemsBot[itemsBot.Count - 1], allItems1[allCount1 - 1]);

            Assert.IsNotEmpty(page.GetFindHearingDevicesText());
            Assert.IsNotEmpty(page.GetHelpTopicsText());
            Assert.IsNotEmpty(page.GetImprintText());
            Assert.IsNotEmpty(page.GetInstructionsForUseText());
            Assert.IsNotEmpty(page.GetInformationMenuText());

            //Find hearing devices
            page.OpenFindHearingDevices();
            PermissionHelper.AllowPermissionIfRequested();
            new FindDevicesPage().TapBack();
            Assert.IsTrue(page.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));

            //TODO: Technical support
            //Not yet implemented in app.

            //Help topics
            page.OpenHelpTopics();
            new HelpTopicsPage().TapBack();
            Assert.IsTrue(page.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));

            //TODO: Instruction manual
            //Not yet implemented in app.

            //Imprint
            page.OpenImprint();
            new ImprintPage().TapBack();
            Assert.IsTrue(page.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));

            //TODO: Data protection
            //Not yet implemented in app.
        }

        [Test]
        [Category("SelfTest")]
        public void HelpTopics()
        {
            LaunchHelper.StartAppInDemoModeByAnyMeans().Page.OpenMenuUsingSwipe();
            new MainMenuPage().OpenHelp();
            new HelpMenuPage().OpenHelpTopics();

            var page = new HelpTopicsPage();
            var menuItems = page.MenuItems.GetAll();
            Assert.Greater(page.MenuItems.CountAll(), 0);
            foreach (var s in menuItems)
            {
                Assert.IsNotEmpty(s);
            }

            Assert.IsNotEmpty(page.GetNavigationBarTitle());
            Assert.IsNotEmpty(page.GetAutomaticProgramText());
            Assert.IsNotEmpty(page.GetBinauralSeparationText());
            Assert.IsNotEmpty(page.GetConnectHearingAidsText());
            Assert.IsNotEmpty(page.GetDisconnectHearingAidsText());
            Assert.IsNotEmpty(page.GetFavoritesText());
            Assert.IsNotEmpty(page.GetMainMenuText());
            Assert.IsNotEmpty(page.GetMainScreenText());
            Assert.IsNotEmpty(page.GetProgramSelectionText());
            Assert.IsNotEmpty(page.GetStreamingProgramText());

            page.OpenConnectHearingAids();
            var connectAidsPage = new ConnectHearingAidsHelpPage();
            Assert.IsNotEmpty(connectAidsPage.GetNavigationBarTitle());
            connectAidsPage
                .ScrollDown(.25)
                .ScrollToTop()
                .ScrollToBottom()
                .TapBack();
            Assert.IsTrue(page.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));

            page.OpenDisconnectHearingAids();
            var disconnectAidsPage = new DisconnectHearingAidsHelpPage();
            Assert.IsNotEmpty(disconnectAidsPage.GetNavigationBarTitle());
            disconnectAidsPage
                .ScrollDown(.25)
                .ScrollToTop()
                .ScrollToBottom()
                .TapBack();
            Assert.IsTrue(page.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));

            page.OpenHomePage();
            var mainScreenPage = new MainPageHelpPage();
            Assert.IsNotEmpty(mainScreenPage.GetNavigationBarTitle());
            mainScreenPage.TapBack();
            Assert.IsTrue(page.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));

            page.OpenProgramSelection();
            var programmPage = new ProgramSelectionHelpPage();
            Assert.IsNotEmpty(programmPage.GetNavigationBarTitle());
            programmPage
                .ScrollDown(.25)
                .ScrollToTop()
                .ScrollToBottom()
                .TapBack();
            Assert.IsTrue(page.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));

            page.OpenBinauralSeparation();
            var binauralPage = new DisconnectVolumeControlHelpPage();
            Assert.IsNotEmpty(binauralPage.GetNavigationBarTitle());
            binauralPage
                .ScrollDown(.25)
                .ScrollToTop()
                .ScrollToBottom()
                .TapBack();
            Assert.IsTrue(page.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));

            page.OpenAutomaticProgram();
            var automaticPage = new AutomaticProgramHelpPage();
            Assert.IsNotEmpty(automaticPage.GetNavigationBarTitle());
            automaticPage.TapBack();
            Assert.IsTrue(page.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));

            page.OpenStreamingProgram();
            var streamingPage = new StreamingProgramHelpPage();
            Assert.IsNotEmpty(streamingPage.GetNavigationBarTitle());
            streamingPage
                .ScrollDown(.25)
                .ScrollToTop()
                .ScrollToBottom()
                .TapBack();
            Assert.IsTrue(page.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));

            page.OpenFavorites();
            var favoritesPage = new FavoritesHelpPage();
            Assert.IsNotEmpty(favoritesPage.GetNavigationBarTitle());
            favoritesPage
                .ScrollDown(.25)
                .ScrollToTop()
                .ScrollToBottom()
                .TapBack();
            Assert.IsTrue(page.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));

            page.OpenMainMenu();
            var mainMenuPage = new MainMenuHelpPage();
            Assert.IsNotEmpty(mainMenuPage.GetNavigationBarTitle());
            mainMenuPage.TapBack();
            Assert.IsTrue(page.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));

            page.TapBack();
            new HelpMenuPage();
        }

        [Test]
        [Category("SelfTest")]
        public void AppModeSelectionPage()
        {
            LaunchHelper.StartAppInDemoModeByAnyMeans().Page.OpenMenuUsingSwipe();
            new MainMenuPage().OpenSettings();
            new SettingsMenuPage().OpenDemoMode();

            var page = new AppModeSelectionPage();

            Assert.IsNotEmpty(page.GetNavigationBarTitle());
            Assert.IsNotEmpty(page.GetTitle());
            Assert.IsNotEmpty(page.GetAppModeText(AppMode.Demo));
            Assert.IsNotEmpty(page.GetAppModeText(AppMode.Normal));
            Assert.AreEqual(AppMode.Demo, page.GetSelectedAppMode());

            page.SelectAppMode(AppMode.Normal);
            Assert.AreEqual(AppMode.Normal, page.GetSelectedAppMode());

            page.SelectAppMode(AppMode.Demo);
            Assert.AreEqual(AppMode.Demo, page.GetSelectedAppMode());

            page.ChangeAppMode(AppMode.Normal);
            Assert.IsTrue(page.IsGoneBeforeTimeout(TimeSpan.FromSeconds(10)));
        }

        [Test]
        [Category("SelfTest")]
        public void MainControlPage_DashboardPage()
        {
            var page = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;

            string programNameInitial = page.GetCurrentProgramName();

            Assert.IsNotEmpty(programNameInitial);

            //Devices
            string leftText = page.GetLeftDeviceText();
            string rightText = page.GetRightDeviceText();
            Assert.AreEqual("L", leftText);
            Assert.AreEqual("R", rightText);

            page.OpenLeftHearingDevice();
            new HearingInstrumentInfoControlPage().Close();
            Assert.IsTrue(page.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));

            page.OpenRightHearingDevice();
            new HearingInstrumentInfoControlPage().Close();
            Assert.IsTrue(page.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));

            //Program selection by index
            string initialName = page.GetCurrentProgramName();
            Assert.IsNotEmpty(initialName);
            int count = page.GetNumberOfPrograms();
            Assert.GreaterOrEqual(count, 2);

            page.SelectProgram(0);
            string name1 = page.GetCurrentProgramName();
            bool itemSelected1 = page.GetIsProgramSelected(0);
            bool otherSelected1 = page.GetIsProgramSelected(1);
            Assert.IsNotEmpty(name1);
            Assert.IsTrue(itemSelected1);
            Assert.IsFalse(otherSelected1);
            page.SelectProgram(0);
            Assert.IsFalse(page.IsGoneBeforeTimeout(TimeSpan.FromSeconds(1)));
            Assert.AreEqual(name1, page.GetCurrentProgramName());

            page.SelectProgram(1);
            string name2 = page.GetCurrentProgramName();
            bool itemSelected2 = page.GetIsProgramSelected(1);
            bool otherSelected2 = page.GetIsProgramSelected(0);
            Assert.IsNotEmpty(name2);
            Assert.AreNotEqual(name1, name2);
            Assert.IsTrue(itemSelected2);
            Assert.IsFalse(otherSelected2);

            List<string> names = new List<string>();
            for (int i = 0; i < count; i++)
            {
                page.SelectProgram(i);
                string newName = page.GetCurrentProgramName();
                Assert.IsNotEmpty(newName);
                Assert.IsFalse(names.Contains(newName));
                names.Add(newName);
            }

            //Program selection by icon
            var icons = page.GetAllProgramIcons();
            var previousProgramNames = new List<string>();
            foreach (var icon in icons)
            {
                page.SelectProgram(icon);
                Assert.IsTrue(page.GetIsProgramSelected(icon));
                string newProgramName = page.GetCurrentProgramName();
                Assert.IsFalse(previousProgramNames.Contains(newProgramName));
                previousProgramNames.Add(newProgramName);
            }

            //Program navigation
            page.SelectProgram(2);
            page.OpenCurrentProgram();
            new ProgramDetailPage().TapBack();
            Assert.IsTrue(page.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));
            page.OpenProgram(0);
            new ProgramDetailPage().TapBack();
            Assert.IsTrue(page.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));

            //Volume
            bool isBinauralButtonVisible = page.GetIsBinauralSettingsButtonVisible();
            Assert.IsFalse(isBinauralButtonVisible);

            var volumeInitial = page.GetVolumeSliderValue();

            const int sliderStepCount = 21;
            double tolerance = 1f / sliderStepCount;
            if (OniOS) tolerance *= 2;      //HACK: iOS swiping is less precise

            page.SetVolumeSliderValue(0.5);
            var volume50 = page.GetVolumeSliderValue();
            Assert.AreEqual(0.5, volume50, tolerance);

            page.SetVolumeSliderValue(0.75);
            var volume75 = page.GetVolumeSliderValue();
            Assert.AreEqual(0.75, volume75, tolerance);

            page.SetVolumeSliderValue(0);
            var volume0 = page.GetVolumeSliderValue();
            Assert.AreEqual(0, volume0, tolerance);

            page.DecreaseVolume();
            Assert.AreEqual(0, page.GetVolumeSliderValue(), tolerance);

            page.SetVolumeSliderValue(0.25);
            var volume25 = page.GetVolumeSliderValue();
            Assert.AreEqual(0.25, volume25, tolerance);

            page.SetVolumeSliderValue(volumeInitial);
            Assert.AreEqual(volumeInitial, page.GetVolumeSliderValue(), tolerance);

            page.SetVolumeSliderValue(1);
            var volume1 = page.GetVolumeSliderValue();
            Assert.AreEqual(1, volume1, tolerance);

            page.IncreaseVolume();
            Assert.AreEqual(1, page.GetVolumeSliderValue(), tolerance);

            double lastVolume = 1;
            for (int i = 0; i < 2; i++)
            {
                page.DecreaseVolume();
                double currentVolume = page.GetVolumeSliderValue();
                Assert.Less(currentVolume, lastVolume);
                lastVolume = currentVolume;
            }

            for (int i = 0; i < 2; i++)
            {
                page.IncreaseVolume();
                double currentVolume = page.GetVolumeSliderValue();
                Assert.Greater(currentVolume, lastVolume);
                lastVolume = currentVolume;
            }
        }

        [Test]
        [Category("SelfTest")]
        public void MainControlPage_ProgramDetailPage()
        {
            LaunchHelper.StartAppInDemoModeByAnyMeans().Page.OpenCurrentProgram();
            var page = new ProgramDetailPage();

            string title = page.GetTitle();

            //Devices
            string leftText = page.GetLeftDeviceText();
            string rightText = page.GetRightDeviceText();
            Assert.AreEqual("L", leftText);
            Assert.AreEqual("R", rightText);

            page.OpenLeftHearingDevice();
            new HearingInstrumentInfoControlPage().Close();
            Assert.IsTrue(page.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));

            page.OpenRightHearingDevice();
            new HearingInstrumentInfoControlPage().Close();
            Assert.IsTrue(page.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));

            //Programs
            string programNameInitial = page.GetCurrentProgramName();
            int count = page.GetNumberOfVisibiblePrograms();
            Assert.IsNotEmpty(title);
            Assert.IsNotEmpty(programNameInitial);

            List<string> names = new List<string>();
            for (int i = 0; i < count; i++)
            {
                page.SelectProgram(i);
                string newName = page.GetCurrentProgramName();
                Assert.IsNotEmpty(newName);
                Assert.IsFalse(names.Contains(newName));
                names.Add(newName);
            }

            var volumeInitial = page.GetVolumeSliderValue();

            const int sliderStepCount = 21;
            double tolerance = 1f / sliderStepCount;
            if (OniOS) tolerance *= 2;      //HACK: iOS swiping is less precise

            page.SetVolumeSliderValue(0.5);
            var volume50 = page.GetVolumeSliderValue();
            Assert.AreEqual(0.5, volume50, tolerance);

            page.SetVolumeSliderValue(0.75);
            var volume75 = page.GetVolumeSliderValue();
            Assert.AreEqual(0.75, volume75, tolerance);

            page.SetVolumeSliderValue(0);
            var volume0 = page.GetVolumeSliderValue();
            Assert.AreEqual(0, volume0, tolerance);

            page.DecreaseVolume();
            Assert.AreEqual(0, page.GetVolumeSliderValue(), tolerance);

            page.SetVolumeSliderValue(0.25);
            var volume25 = page.GetVolumeSliderValue();
            Assert.AreEqual(0.25, volume25, tolerance);

            page.SetVolumeSliderValue(volumeInitial);
            Assert.AreEqual(volumeInitial, page.GetVolumeSliderValue(), tolerance);

            page.SetVolumeSliderValue(1);
            var volume1 = page.GetVolumeSliderValue();
            Assert.AreEqual(1, volume1, tolerance);

            page.IncreaseVolume();
            Assert.AreEqual(1, page.GetVolumeSliderValue(), tolerance);

            double lastVolume = 1;
            for (int i = 0; i < 2; i++)
            {
                page.DecreaseVolume();
                double currentVolume = page.GetVolumeSliderValue();
                Assert.Less(currentVolume, lastVolume);
                lastVolume = currentVolume;
            }

            for (int i = 0; i < 2; i++)
            {
                page.IncreaseVolume();
                double currentVolume = page.GetVolumeSliderValue();
                Assert.Greater(currentVolume, lastVolume);
                lastVolume = currentVolume;
            }
        }

        [Test]
        [Category("SelfTest")]
        public void HearingInstrumentInfoControlPage()
        {
            LaunchHelper.StartAppInDemoModeByAnyMeans().Page.OpenLeftHearingDevice();
            var page = new HearingInstrumentInfoControlPage();

            string title = page.GetTitle();
            string type = page.GetDeviceType();
            string name = page.GetDeviceName();
            string serial = page.GetDeviceSerial();
            string typeTitle = page.GetDeviceTypeTitle();
            string nameTitle = page.GetDeviceNameTitle();
            string serialTitle = page.GetDeviceSerialTitle();
            string udiTitle = page.GetDeviceUdiTitle();
            string buttonText = page.GetCloseButtonText();
            string instrumentText = page.GetInstrumentControlText();
            string instrumentPercentage = page.GetInstrumentControlPercentage();

            Assert.IsNotEmpty(title);
            Assert.IsNotEmpty(type);
            Assert.IsNotEmpty(name);
            Assert.IsNotEmpty(serial);
            Assert.IsFalse(page.GetIsDeviceUdiVisible());   //UDI is not available in Demo-Mode
            Assert.IsNotEmpty(typeTitle);
            Assert.IsNotEmpty(nameTitle);
            Assert.IsNotEmpty(serialTitle);
            Assert.IsNotEmpty(udiTitle);
            Assert.IsNotEmpty(udiTitle);
            Assert.IsNotEmpty(buttonText);
            Assert.IsNotEmpty(instrumentText);
            Assert.IsNotEmpty(instrumentPercentage);
        }

        [Test]
        [Category("SelfTest")]
        public void ProgramDetailParamEditBinaural()
        {
            LaunchHelper.StartAppInDemoModeByAnyMeans().Page.OpenCurrentProgram();
            new ProgramDetailPage().OpenBinauralSettings();
            var page = new ProgramDetailParamEditBinauralPage();


            //Strings
            string title = page.GetTitle();
            string description = page.GetDescription();
            string switchTitle = page.GetBinauralSwitchTitle();
            Assert.IsNotEmpty(title);
            Assert.IsNotEmpty(description);
            Assert.IsNotEmpty(switchTitle);


            //Toggle switch
            Assert.IsFalse(page.GetIsBinauralSwitchChecked());
            page.TurnOnBinauralSeparation();
            Assert.IsTrue(page.GetIsBinauralSwitchChecked());
            page.TurnOnBinauralSeparation();
            Assert.IsTrue(page.GetIsBinauralSwitchChecked());
            page.TurnOffBinauralSeparation();
            Assert.IsFalse(page.GetIsBinauralSwitchChecked());
            page.TurnOffBinauralSeparation();
            Assert.IsFalse(page.GetIsBinauralSwitchChecked());

            bool singleVisibleInitial = page.GetIsVolumeControlVisible(VolumeChannel.Single);
            bool leftVisibleInitial = page.GetIsVolumeControlVisible(VolumeChannel.Left);
            bool rightVisibleInitial = page.GetIsVolumeControlVisible(VolumeChannel.Right);
            Assert.IsTrue(singleVisibleInitial);
            Assert.IsFalse(leftVisibleInitial);
            Assert.IsFalse(rightVisibleInitial);

            page.ToggleBinauralSwitch();
            bool singleVisibleAfterToggle = page.GetIsVolumeControlVisible(VolumeChannel.Single);
            bool leftVisibleAfterToggle = page.GetIsVolumeControlVisible(VolumeChannel.Left);
            bool rightVisibleAfterToggle = page.GetIsVolumeControlVisible(VolumeChannel.Right);
            Assert.IsFalse(singleVisibleAfterToggle);
            Assert.IsTrue(leftVisibleAfterToggle);
            Assert.IsTrue(rightVisibleAfterToggle);
            page.ToggleBinauralSwitch();

            page.ToggleBinauralSwitch();
            Assert.IsFalse(page.GetIsVolumeControlVisible(VolumeChannel.Single));
            page.ToggleBinauralSwitch();
            Assert.IsTrue(page.GetIsVolumeControlVisible(VolumeChannel.Single));

            page.ToggleBinauralSwitch();
            Assert.IsTrue(page.GetIsVolumeControlVisible(VolumeChannel.Left));
            page.ToggleBinauralSwitch();
            Assert.IsFalse(page.GetIsVolumeControlVisible(VolumeChannel.Left));

            page.ToggleBinauralSwitch();
            Assert.IsTrue(page.GetIsVolumeControlVisible(VolumeChannel.Right));
            page.ToggleBinauralSwitch();
            Assert.IsFalse(page.GetIsVolumeControlVisible(VolumeChannel.Right));

            //Volume control check
            double CheckVolumeControl(VolumeChannel channel)
            {
                const int sliderStepCount = 21;
                double tolerance = 1f / sliderStepCount;
                if (OniOS) tolerance *= 2;      //HACK: iOS swiping is less precise

                page.SetVolumeSliderValue(channel, 0.5);
                var singleVolume50 = page.GetVolumeSliderValue(channel);
                Assert.AreEqual(0.5, singleVolume50, tolerance);

                page.SetVolumeSliderValue(channel, 0.0);
                var singleVolume0 = page.GetVolumeSliderValue(channel);
                Assert.AreEqual(0, singleVolume0, tolerance);

                page.SetVolumeSliderValue(channel, 1);
                var singleVolume1 = page.GetVolumeSliderValue(channel);
                Assert.AreEqual(1, singleVolume1, tolerance);

                page.SetVolumeSliderValue(channel, 0.75);
                var singleVolume75 = page.GetVolumeSliderValue(channel);
                Assert.AreEqual(0.75, singleVolume75, tolerance);

                page.SetVolumeSliderValue(channel, 0.25);
                var singleVolume25 = page.GetVolumeSliderValue(channel);
                Assert.AreEqual(0.25, singleVolume25, tolerance);

                page.SetVolumeSliderValue(channel, 0.1);
                var singleVolume10 = page.GetVolumeSliderValue(channel);
                Assert.AreEqual(0.1, singleVolume10, tolerance);

                page.SetVolumeSliderValue(channel, 0.90);
                var singleVolume90 = page.GetVolumeSliderValue(channel);
                Assert.AreEqual(0.90, singleVolume90, tolerance);

                page.DecreaseVolume(channel);
                Assert.Less(page.GetVolumeSliderValue(channel), singleVolume90);

                page.IncreaseVolume(channel);
                Assert.AreEqual(singleVolume90, page.GetVolumeSliderValue(channel));

                return singleVolume90;
            }

            //Single volume control
            var singleValueInitial = page.GetVolumeSliderValue(VolumeChannel.Single);
            var finalSingle = CheckVolumeControl(VolumeChannel.Single);

            //Left volume control
            page.TurnOnBinauralSeparation();
            Assert.IsTrue(page.GetIsBinauralSwitchChecked());
            var finalLeft = CheckVolumeControl(VolumeChannel.Left);

            //Right volume control
            var finalRight = CheckVolumeControl(VolumeChannel.Right);

            Assert.AreEqual(finalLeft, finalRight);

            //Single volume double check
            page.Close();
            new ProgramDetailPage().OpenBinauralSettings();
            Assert.IsTrue(page.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));
            page.TurnOffBinauralSeparation();
            CheckVolumeControl(VolumeChannel.Single);
        }

        [Test]
        [Category("SelfTest")]
        public void ProgramDetailParameterDisplays()
        {
            LaunchHelper.StartAppInDemoModeByAnyMeans().Page.OpenCurrentProgram();
            var page = new ProgramDetailPage();

            //Auto program
            Assert.IsTrue(page.GetIsAutoDisplayVisible());
            Assert.IsFalse(page.GetIsEqualizerDisplayVisible());
            Assert.IsFalse(page.GetIsNoiseReductionDisplayVisible());
            Assert.IsFalse(page.GetIsSpeechFocusDisplayVisible());
            Assert.IsFalse(page.GetIsStreamingDisplayVisible());
            Assert.IsFalse(page.GetIsTinnitusDisplayVisible());
            Assert.IsFalse(page.GetIsTinnitusOnlyDisplayVisible());
            //Auto
            var autoDescription = page.AutoDisplay.GetDescription();
            Assert.IsNotEmpty(autoDescription);

            //Music program
            page.SelectProgram(1);
            Assert.IsFalse(page.GetIsAutoDisplayVisible());
            Assert.IsTrue(page.GetIsEqualizerDisplayVisible());
            Assert.IsTrue(page.GetIsNoiseReductionDisplayVisible());
            Assert.IsTrue(page.GetIsSpeechFocusDisplayVisible());
            Assert.IsFalse(page.GetIsStreamingDisplayVisible());
            Assert.IsTrue(page.GetIsTinnitusDisplayVisible());
            Assert.IsFalse(page.GetIsTinnitusOnlyDisplayVisible());
            //Speech focus
            string speechFocusTitle = page.SpeechFocusDisplay.GetTitle();
            string speechFocusValue = page.SpeechFocusDisplay.GetValue();
            Assert.IsNotEmpty(speechFocusTitle);
            Assert.IsNotEmpty(speechFocusValue);
            //Noise reduction
            string noiseReductionTitle = page.NoiseReductionDisplay.GetTitle();
            string noiseReductionValue = page.NoiseReductionDisplay.GetValue();
            Assert.IsNotEmpty(noiseReductionTitle);
            Assert.IsNotEmpty(noiseReductionValue);
            //Tinnitus
            string tinnitusTitle = page.TinnitusDisplay.GetTitle();
            string tinnitusValue = page.TinnitusDisplay.GetValue();
            Assert.IsNotEmpty(tinnitusTitle);
            Assert.IsNotEmpty(tinnitusValue);
            //Equalizer
            string eqTitle = page.EqualizerDisplay.GetTitle();
            Assert.IsNotEmpty(eqTitle);
            double eqLow = page.EqualizerDisplay.GetEqualizerSliderValue(EqBand.Low);
            double eqMid = page.EqualizerDisplay.GetEqualizerSliderValue(EqBand.Mid);
            double eqHigh = page.EqualizerDisplay.GetEqualizerSliderValue(EqBand.High);
            Assert.AreEqual(.5, eqLow, .05);
            Assert.AreEqual(.5, eqMid, .05);
            Assert.AreEqual(.5, eqHigh, .05);

            //Tinnitus program
            page.SelectProgram(2);
            Assert.IsFalse(page.GetIsAutoDisplayVisible());
            Assert.IsFalse(page.GetIsEqualizerDisplayVisible());
            Assert.IsFalse(page.GetIsNoiseReductionDisplayVisible());
            Assert.IsFalse(page.GetIsSpeechFocusDisplayVisible());
            Assert.IsFalse(page.GetIsStreamingDisplayVisible());
            Assert.IsFalse(page.GetIsTinnitusDisplayVisible());
            Assert.IsTrue(page.GetIsTinnitusOnlyDisplayVisible());
            //Tinnitus only
            string tinOnlyTitle = page.TinnitusOnlyDisplay.GetTitle();
            string tinOnlyAmpTitle = page.TinnitusOnlyDisplay.GetAmplificationTitle();
            string tinOnlyEqTitle = page.TinnitusOnlyDisplay.GetEqualizerTitle();
            Assert.IsNotEmpty(tinOnlyTitle);
            Assert.IsNotEmpty(tinOnlyAmpTitle);
            Assert.IsNotEmpty(tinOnlyEqTitle);
            double tinnitusVolume = page.TinnitusOnlyDisplay.GetVolumeSliderValue();
            double tinnitusLow = page.TinnitusOnlyDisplay.GetEqualizerSliderValue(EqBand.Low);
            double tinnitusMid = page.TinnitusOnlyDisplay.GetEqualizerSliderValue(EqBand.Mid);
            double tinnitusHight = page.TinnitusOnlyDisplay.GetEqualizerSliderValue(EqBand.High);
            Assert.AreEqual(.5, tinnitusVolume, .05);
            Assert.AreEqual(.5, tinnitusLow, .05);
            Assert.AreEqual(.5, tinnitusMid, .05);
            Assert.AreEqual(.5, tinnitusHight, .05);

            //Streaming program
            page.SelectProgram(3);
            Assert.IsFalse(page.GetIsAutoDisplayVisible());
            Assert.IsTrue(page.GetIsEqualizerDisplayVisible());
            Assert.IsTrue(page.GetIsNoiseReductionDisplayVisible());
            Assert.IsTrue(page.GetIsSpeechFocusDisplayVisible());
            Assert.IsTrue(page.GetIsStreamingDisplayVisible());
            Assert.IsFalse(page.GetIsTinnitusDisplayVisible());
            Assert.IsFalse(page.GetIsTinnitusOnlyDisplayVisible());
            //Streaming
            string streamingTitle = page.StreamingDisplay.GetTitle();
            Assert.IsNotEmpty(streamingTitle);
            double streamingValue = page.StreamingDisplay.GetSliderValue();
            Assert.AreEqual(0, streamingValue, .05);
            //Speech focus
            Assert.AreEqual(speechFocusTitle, page.SpeechFocusDisplay.GetTitle());
            Assert.AreEqual(speechFocusValue, page.SpeechFocusDisplay.GetValue());
            //Noise reduction
            Assert.AreEqual(noiseReductionTitle, page.NoiseReductionDisplay.GetTitle());
            Assert.AreEqual(noiseReductionValue, page.NoiseReductionDisplay.GetValue());
            //Equalizer
            Assert.AreEqual(eqTitle, page.EqualizerDisplay.GetTitle());
            double streamingLow = page.EqualizerDisplay.GetEqualizerSliderValue(EqBand.Low);
            double streamingMid = page.EqualizerDisplay.GetEqualizerSliderValue(EqBand.Mid);
            double streamingHigh = page.EqualizerDisplay.GetEqualizerSliderValue(EqBand.High);
            Assert.AreEqual(.5, streamingLow, .05);
            Assert.AreEqual(.5, streamingMid, .05);
            Assert.AreEqual(.5, streamingHigh, .05);
        }

        [Test]
        [Category("SelfTest")]
        public void ProgramDetailParamEditEqualizer()
        {
            LaunchHelper.StartAppInDemoModeByAnyMeans().Page.OpenCurrentProgram();
            var programDetailPage = new ProgramDetailPage();

            programDetailPage.SelectProgram(1);
            Assert.IsTrue(programDetailPage.GetIsEqualizerDisplayVisible());
            programDetailPage.EqualizerDisplay.OpenSettings();

            var page = new ProgramDetailParamEditEqualizerPage();
            string title = page.GetTitle();
            string description = page.GetDescription();
            string closeTitle = page.GetCloseButtonText();
            Assert.IsNotEmpty(title);
            Assert.IsNotEmpty(description);
            Assert.IsNotEmpty(closeTitle);
            Assert.IsTrue(page.GetIsCloseButtonEnabled());

            double checkSlider(EqBand band)
            {
                const int sliderStepCount = 13;
                double tolerance = 1f / sliderStepCount;
                if (OniOS) tolerance *= 2;      //HACK: iOS swiping is less precise

                string sliderTitle = page.GetEqualizerSliderTitle(band);
                Assert.IsNotEmpty(sliderTitle);

                page.SetEqualizerSliderValue(band, 0.5);
                var singleVolume50 = page.GetEqualizerSliderValue(band);
                Assert.AreEqual(0.5, singleVolume50, tolerance);
                page.WaitUntilNoLoadingIndicator();

                page.SetEqualizerSliderValue(band, 0.0);
                var singleVolume0 = page.GetEqualizerSliderValue(band);
                Assert.AreEqual(0, singleVolume0, tolerance);
                page.WaitUntilNoLoadingIndicator();

                page.SetEqualizerSliderValue(band, 1);
                var singleVolume1 = page.GetEqualizerSliderValue(band);
                Assert.AreEqual(1, singleVolume1, tolerance);
                page.WaitUntilNoLoadingIndicator();

                page.SetEqualizerSliderValue(band, 0.75);
                var singleVolume75 = page.GetEqualizerSliderValue(band);
                Assert.AreEqual(0.75, singleVolume75, tolerance);
                page.WaitUntilNoLoadingIndicator();

                page.SetEqualizerSliderValue(band, 0.25);
                var singleVolume25 = page.GetEqualizerSliderValue(band);
                Assert.AreEqual(0.25, singleVolume25, tolerance);
                page.WaitUntilNoLoadingIndicator();

                page.SetEqualizerSliderValue(band, 0.1);
                var singleVolume10 = page.GetEqualizerSliderValue(band);
                Assert.AreEqual(0.1, singleVolume10, tolerance);
                page.WaitUntilNoLoadingIndicator();

                page.SetEqualizerSliderValue(band, 0.90);
                var singleVolume90 = page.GetEqualizerSliderValue(band);
                Assert.AreEqual(0.90, singleVolume90, tolerance);
                page.WaitUntilNoLoadingIndicator();

                return singleVolume90;
            }

            foreach (EqBand band in Enum.GetValues(typeof(EqBand)))
            {
                checkSlider(band);
            }

            //Close button
            page.Close();
            new ProgramDetailPage();
        }

        [Test]
        [Category("SelfTest")]
        public void ProgramDetailParamEditTinnitus()
        {
            LaunchHelper.StartAppInDemoModeByAnyMeans().Page.OpenCurrentProgram();
            var programDetailPage = new ProgramDetailPage();

            programDetailPage.SelectProgram(1);
            Assert.IsTrue(programDetailPage.GetIsTinnitusDisplayVisible());
            programDetailPage.TinnitusDisplay.OpenSettings();

            // ** SettingsPage **
            var page = new ProgramDetailParamEditTinnitusPage();
            //General strings
            string settingsTitle = page.GetTitle();
            string settingsCloseButtonText = page.GetCloseButtonText();
            string switchTitle = page.GetTinnitusSwitchTitle();
            string volumeTitle = page.GetVolumeTitle();
            string volumeLowTitle = page.GetVolumeLowTitle();
            string volumeHighTitle = page.GetVolumeHighTitle();
            string equalizerTitle = page.GetEqualizerTitle();
            Assert.IsNotEmpty(settingsTitle);
            Assert.IsNotEmpty(settingsCloseButtonText);
            Assert.IsNotEmpty(switchTitle);
            Assert.IsNotEmpty(volumeTitle);
            Assert.IsNotEmpty(volumeLowTitle);
            Assert.IsNotEmpty(volumeHighTitle);
            Assert.IsNotEmpty(equalizerTitle);
            Assert.IsTrue(page.GetIsCloseButtonEnabled());

            //Switch
            Assert.IsTrue(page.GetIsTinnitusSwitchChecked());
            page.TurnOffTinnitus();
            Assert.IsFalse(page.GetIsTinnitusSwitchChecked());
            page.TurnOnTinnitus();
            Assert.IsTrue(page.GetIsTinnitusSwitchChecked());

            //Amplification
            {
                const int sliderStepCount = 21;
                double tolerance = 1f / sliderStepCount;
                if (OniOS) tolerance *= 2;      //HACK: iOS swiping is less precise

                page.SetVolumeSliderValue(0.5);
                var singleVolume50 = page.GetVolumeSliderValue();
                Assert.AreEqual(0.5, singleVolume50, tolerance);
                page.WaitUntilNoLoadingIndicator();

                page.SetVolumeSliderValue(0.0);
                var singleVolume0 = page.GetVolumeSliderValue();
                Assert.AreEqual(0, singleVolume0, tolerance);
                page.WaitUntilNoLoadingIndicator();

                page.SetVolumeSliderValue(1);
                var singleVolume1 = page.GetVolumeSliderValue();
                Assert.AreEqual(1, singleVolume1, tolerance);
                page.WaitUntilNoLoadingIndicator();

                page.SetVolumeSliderValue(0.75);
                var singleVolume75 = page.GetVolumeSliderValue();
                Assert.AreEqual(0.75, singleVolume75, tolerance);
                page.WaitUntilNoLoadingIndicator();

                page.SetVolumeSliderValue(0.25);
                var singleVolume25 = page.GetVolumeSliderValue();
                Assert.AreEqual(0.25, singleVolume25, tolerance);
                page.WaitUntilNoLoadingIndicator();

                page.SetVolumeSliderValue(0.1);
                var singleVolume10 = page.GetVolumeSliderValue();
                Assert.AreEqual(0.1, singleVolume10, tolerance);
                page.WaitUntilNoLoadingIndicator();

                page.SetVolumeSliderValue(0.90);
                var singleVolume90 = page.GetVolumeSliderValue();
                Assert.AreEqual(0.90, singleVolume90, tolerance);
                page.WaitUntilNoLoadingIndicator();
            }

            //Equalizer
            double checkSlider(EqBand band)
            {
                const int sliderStepCount = 13;
                double tolerance = 1f / sliderStepCount;
                if (OniOS) tolerance *= 2;      //HACK: iOS swiping is less precise

                string sliderTitle = page.GetEqualizerSliderTitle(band);
                Assert.IsNotEmpty(sliderTitle);

                page.SetEqualizerSliderValue(band, 0.5);
                var singleVolume50 = page.GetEqualizerSliderValue(band);
                Assert.AreEqual(0.5, singleVolume50, tolerance);
                page.WaitUntilNoLoadingIndicator();

                page.SetEqualizerSliderValue(band, 0.0);
                var singleVolume0 = page.GetEqualizerSliderValue(band);
                Assert.AreEqual(0, singleVolume0, tolerance);
                page.WaitUntilNoLoadingIndicator();

                page.SetEqualizerSliderValue(band, 1);
                var singleVolume1 = page.GetEqualizerSliderValue(band);
                Assert.AreEqual(1, singleVolume1, tolerance);
                page.WaitUntilNoLoadingIndicator();

                page.SetEqualizerSliderValue(band, 0.75);
                var singleVolume75 = page.GetEqualizerSliderValue(band);
                Assert.AreEqual(0.75, singleVolume75, tolerance);
                page.WaitUntilNoLoadingIndicator();

                page.SetEqualizerSliderValue(band, 0.25);
                var singleVolume25 = page.GetEqualizerSliderValue(band);
                Assert.AreEqual(0.25, singleVolume25, tolerance);
                page.WaitUntilNoLoadingIndicator();

                page.SetEqualizerSliderValue(band, 0.1);
                var singleVolume10 = page.GetEqualizerSliderValue(band);
                Assert.AreEqual(0.1, singleVolume10, tolerance);
                page.WaitUntilNoLoadingIndicator();

                page.SetEqualizerSliderValue(band, 0.90);
                var singleVolume90 = page.GetEqualizerSliderValue(band);
                Assert.AreEqual(0.90, singleVolume90, tolerance);
                page.WaitUntilNoLoadingIndicator();

                return singleVolume90;
            }

            foreach (EqBand band in Enum.GetValues(typeof(EqBand)))
            {
                checkSlider(band);
            }

            //Close button
            page.Close();
            new ProgramDetailPage();
        }

        [Test]
        [Category("SelfTest")]
        public void ProgramDetailParamEditSpeechFocus()
        {
            LaunchHelper.StartAppInDemoModeByAnyMeans().Page.OpenCurrentProgram();
            var programDetailPage = new ProgramDetailPage();

            programDetailPage.SelectProgram(1);
            Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible());
            string focusDisplay = programDetailPage.SpeechFocusDisplay.GetValue();
            programDetailPage.SpeechFocusDisplay.OpenSettings();

            var page = new ProgramDetailParamEditSpeechFocusPage();
            string title = page.GetTitle();
            string description = page.GetDescription();
            string closeButtonText = page.GetCloseButtonText();
            Assert.IsNotEmpty(title);
            Assert.IsNotEmpty(description);
            Assert.IsNotEmpty(closeButtonText);
            var focusInitial = page.GetSelectedSpeechFocus();
            Assert.AreEqual(focusDisplay, page.GetSpeechFocusName(focusInitial));
            Assert.IsTrue(page.GetIsCloseButtonEnabled());

            var names = new List<string>();
            foreach (SpeechFocus f in Enum.GetValues(typeof(SpeechFocus)))
            {
                page.SelectSpeechFocus(f);
                Assert.AreEqual(f, page.GetSelectedSpeechFocus());
                string newName = page.GetSpeechFocusName(f);
                Assert.IsFalse(names.Contains(newName));
                names.Add(newName);
            }

            page.SelectSpeechFocus(SpeechFocus.Front);
            string frontNameEdit = page.GetSpeechFocusName(SpeechFocus.Front);
            Assert.IsNotEmpty(frontNameEdit);
            bool buttonEnabled = page.GetIsCloseButtonEnabled();
            page.Close();

            Assert.IsTrue(programDetailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));
            string frontNameDisplay = programDetailPage.SpeechFocusDisplay.GetValue();
            Assert.AreEqual(frontNameEdit, frontNameDisplay);
        }

        [Test]
        [Category("SelfTest")]
        public void ProgramDetailParamEditNoiseReduction()
        {
            LaunchHelper.StartAppInDemoModeByAnyMeans().Page.OpenCurrentProgram();
            var programDetailPage = new ProgramDetailPage();

            programDetailPage.SelectProgram(1);
            Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible());
            string noiseReductionDisplay = programDetailPage.NoiseReductionDisplay.GetValue();
            programDetailPage.NoiseReductionDisplay.OpenSettings();

            var page = new ProgramDetailParamEditNoiseReductionPage();
            string title = page.GetTitle();
            string description = page.GetDescription();
            string closeButtonText = page.GetCloseButtonText();
            Assert.IsNotEmpty(title);
            Assert.IsNotEmpty(description);
            Assert.IsNotEmpty(closeButtonText);
            var noiseReductionInitial = page.GetSelectedNoiseReduction();
            Assert.AreEqual(noiseReductionDisplay, page.GetNoiseReductionName(noiseReductionInitial));
            Assert.IsTrue(page.GetIsCloseButtonEnabled());

            var names = new List<string>();
            foreach (NoiseReduction n in Enum.GetValues(typeof(NoiseReduction)))
            {
                page.SelectNoiseReduction(n);
                Assert.AreEqual(n, page.GetSelectedNoiseReduction());
                string newName = page.GetNoiseReductionName(n);
                Assert.IsFalse(names.Contains(newName));
                names.Add(newName);
            }

            page.SelectNoiseReduction(NoiseReduction.Light);
            string lightNameEdit = page.GetNoiseReductionName(NoiseReduction.Light);
            Assert.IsNotEmpty(lightNameEdit);
            bool buttonEnabled = page.GetIsCloseButtonEnabled();
            page.Close();

            Assert.IsTrue(programDetailPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));
            string lightNameDisplay = programDetailPage.NoiseReductionDisplay.GetValue();
            Assert.AreEqual(lightNameEdit, lightNameDisplay);
        }

        [Test]
        [Category("SelfTest")]
        public void ProgramDetailParamEditStreaming()
        {
            LaunchHelper.StartAppInDemoModeByAnyMeans().Page.OpenCurrentProgram();
            var programDetailPage = new ProgramDetailPage();

            //General strings
            programDetailPage.SelectProgram(3);
            Assert.IsTrue(programDetailPage.GetIsStreamingDisplayVisible());
            programDetailPage.StreamingDisplay.OpenSettings();

            var page = new ProgramDetailParamEditStreamingPage();
            string title = page.GetTitle();
            string description = page.GetDescription();
            string closeButtonText = page.GetCloseButtonText();
            string envTitle = page.GetEnvironmentTitle();
            string srcTitle = page.GetSourceTitle();
            Assert.IsNotEmpty(title);
            Assert.IsNotEmpty(description);
            Assert.IsNotEmpty(closeButtonText);
            Assert.IsNotEmpty(envTitle);
            Assert.IsNotEmpty(srcTitle);
            Assert.IsTrue(page.GetIsCloseButtonEnabled());

            //Streaming slider
            {
                double tolerance = 0.05;
                if (OniOS) tolerance *= 2;      //HACK: iOS swiping is less precise

                page.SetStreamingSliderValue(0.5);
                var singleVolume50 = page.GetStreamingSliderValue();
                Assert.AreEqual(0.5, singleVolume50, tolerance);
                page.WaitUntilNoLoadingIndicator();

                page.SetStreamingSliderValue(0.0);
                var singleVolume0 = page.GetStreamingSliderValue();
                Assert.AreEqual(0, singleVolume0, tolerance);
                page.WaitUntilNoLoadingIndicator();

                page.SetStreamingSliderValue(1);
                var singleVolume1 = page.GetStreamingSliderValue();
                Assert.AreEqual(1, singleVolume1, tolerance);
                page.WaitUntilNoLoadingIndicator();

                page.SetStreamingSliderValue(0.75);
                var singleVolume75 = page.GetStreamingSliderValue();
                Assert.AreEqual(0.75, singleVolume75, tolerance);
                page.WaitUntilNoLoadingIndicator();

                page.SetStreamingSliderValue(0.25);
                var singleVolume25 = page.GetStreamingSliderValue();
                Assert.AreEqual(0.25, singleVolume25, tolerance);
                page.WaitUntilNoLoadingIndicator();

                page.SetStreamingSliderValue(0.1);
                var singleVolume10 = page.GetStreamingSliderValue();
                Assert.AreEqual(0.1, singleVolume10, tolerance);
                page.WaitUntilNoLoadingIndicator();

                page.SetStreamingSliderValue(0.90);
                var singleVolume90 = page.GetStreamingSliderValue();
                Assert.AreEqual(0.90, singleVolume90, tolerance);
                page.WaitUntilNoLoadingIndicator();
            }

            //Close button
            page.Close();
            new ProgramDetailPage();
        }

        [Test]
        [Category("SelfTest")]
        public void ProgramDetailSettingsPages()
        {
            LaunchHelper.StartAppInDemoModeByAnyMeans().Page.OpenCurrentProgram();
            var programDetailPage = new ProgramDetailPage();

            programDetailPage.SelectProgram(1);
            programDetailPage.OpenProgramSettings();

            var settingsPage = new ProgramDetailSettingsControlPage();
            string settingsTitle = settingsPage.GetNavigationBarTitle();
            string settingsDesc = settingsPage.GetDescription();
            Assert.IsNotEmpty(settingsTitle);
            Assert.IsNotEmpty(settingsDesc);
            Assert.IsTrue(settingsPage.GetIsCustomizeNameVisible());
            Assert.IsTrue(settingsPage.GetIsCustomizeIconVisible());
            Assert.IsTrue(settingsPage.GetIsCreateFavoriteVisible());
            Assert.IsFalse(settingsPage.GetIsAutoStartVisible());
            Assert.IsFalse(settingsPage.GetIsAutoStartEnabled());
            Assert.IsFalse(settingsPage.GetIsDeleteFavoriteVisible());
            Assert.IsFalse(settingsPage.GetIsResetProgramVisible());

            string customizeNameText = settingsPage.GetCustomizeNameText();
            string customizeIconText = settingsPage.GetCustomizeIconText();
            string createFavoriteText = settingsPage.GetCreateFavoriteText();
            Assert.IsNotEmpty(customizeNameText);
            Assert.IsNotEmpty(customizeIconText);
            Assert.IsNotEmpty(createFavoriteText);

            settingsPage.CreateFavorite();

            //Program name
            new ProgramNamePage().TapBack();
            new ProgramDetailSettingsControlPage().CreateFavorite();

            var namePage = new ProgramNamePage();
            string nameNavBarTitle = namePage.GetNavigationBarTitle();
            string nameDesc = namePage.GetDescription();
            string nameTitle = namePage.GetNameTitle();
            string nameCancel = namePage.GetCancelButtonText();
            string nameProceed = namePage.GetProceedButtonText();
            Assert.IsNotEmpty(nameNavBarTitle);
            Assert.IsNotEmpty(nameDesc);
            Assert.IsNotEmpty(nameTitle);
            Assert.IsNotEmpty(nameCancel);
            Assert.IsNotEmpty(nameProceed);

            string nameInitial = namePage.GetName();
            Assert.NotNull(nameInitial);
            namePage.EnterName("DeleteThis");
            string nameDeleteThis = namePage.GetName();
            Assert.AreEqual("DeleteThis", nameDeleteThis);
            namePage.ClearName();
            Assert.AreEqual("", namePage.GetName());
            namePage.EnterName("ChangeThis");
            string nameChangeThis = namePage.GetName();
            Assert.AreEqual("ChangeThis", nameChangeThis);

            namePage.Cancel();
            new AppDialog().Deny();
            Assert.IsTrue(namePage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));

            namePage.EnterName("MyFavorite");
            string nameMyFavorite = namePage.GetName();
            Assert.AreEqual("MyFavorite", nameMyFavorite);

            namePage.Proceed();

            //Icon selection
            new ProgramIconPage().TapBack();
            new ProgramNamePage().Proceed();

            var iconPage = new ProgramIconPage();
            string iconNavBarTitle = iconPage.GetNavigationBarTitle();
            string iconDesc = iconPage.GetDescription();
            string iconTitle = iconPage.GetIconTitle();
            string iconCancel = iconPage.GetCancelButtonText();
            string iconProceed = iconPage.GetProceedButtonText();
            Assert.IsNotEmpty(iconNavBarTitle);
            Assert.IsNotEmpty(iconDesc);
            Assert.IsNotEmpty(iconTitle);
            Assert.IsNotEmpty(iconCancel);
            Assert.IsNotEmpty(iconProceed);

            Debug.WriteLine("Self test: Counting all icons.");
            var iconCount = iconPage.GetNumberOfIcons();
            Debug.WriteLine($"Self test: Found {iconCount} icons.");
            Assert.GreaterOrEqual(iconCount, 36);
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Debug.WriteLine("Self test: Checking all icons.");
            var icons = new List<string>();
            for (int i = 0; i < iconCount; i++)
            {
                string newIcon = iconPage.GetIconText(i);
                Assert.IsNotEmpty(newIcon);
                Assert.IsFalse(icons.Contains(newIcon));
                icons.Add(newIcon);
            }
            Debug.WriteLine($"Self test: Checked all icons in {watch.Elapsed.TotalSeconds} seconds.");
            Assert.AreEqual(icons[0], iconPage.GetIconText(0, 0));
            Assert.AreEqual(icons[5], iconPage.GetIconText(5, 0));
            Assert.AreEqual(icons[6], iconPage.GetIconText(0, 1));
            Assert.AreEqual(icons[30], iconPage.GetIconText(0, 5));
            Assert.AreEqual(icons[35], iconPage.GetIconText(5, 5));

            iconPage.SelectIcon(3, 0);
            iconPage.GetSelectedIconCoords(out int column, out int row);
            Assert.AreEqual(3, column);
            Assert.AreEqual(0, row);
            Assert.AreEqual(3, iconPage.GetSelectedIconIndex());

            iconPage.SelectIcon(1, 4);
            iconPage.GetSelectedIconCoords(out column, out row);
            Assert.AreEqual(1, column);
            Assert.AreEqual(4, row);
            Assert.AreEqual(25, iconPage.GetSelectedIconIndex());

            iconPage.SelectIcon(icons[17]);
            Assert.AreEqual(17, iconPage.GetSelectedIconIndex());

            iconPage.Proceed();

            //Automation
            new ProgramAutomationPage().TapBack();
            new ProgramIconPage().Proceed();

            var autoPage = new ProgramAutomationPage();
            string autoNavBarTitle = autoPage.GetNavigationBarTitle();
            string autoDesc = autoPage.GetDescription();
            string autoCancel = autoPage.GetCancelButtonText();
            string autoProceed = autoPage.GetProceedButtonText();
            string autoSwitchTitle = autoPage.GetAutomationSwitchTitle();
            Assert.IsNotEmpty(autoNavBarTitle);
            Assert.IsNotEmpty(autoDesc);
            Assert.IsNotEmpty(autoCancel);
            Assert.IsNotEmpty(autoProceed);
            Assert.IsNotEmpty(autoSwitchTitle);

            Assert.IsFalse(autoPage.GetIsAutomationSwitchChecked());
            autoPage.ToggleBinauralSwitch();
            Assert.IsTrue(autoPage.GetIsAutomationSwitchChecked());
            autoPage.ToggleBinauralSwitch();
            Assert.IsFalse(autoPage.GetIsAutomationSwitchChecked());

            autoPage.TurnOnAutomation();
            Assert.IsTrue(autoPage.GetIsAutomationSwitchChecked());
            autoPage.TurnOnAutomation();
            Assert.IsTrue(autoPage.GetIsAutomationSwitchChecked());

            autoPage.TurnOffAutomation();
            Assert.IsFalse(autoPage.GetIsAutomationSwitchChecked());
            autoPage.TurnOffAutomation();
            Assert.IsFalse(autoPage.GetIsAutomationSwitchChecked());

            //Wifi selection button
            Assert.IsFalse(autoPage.GetIsWifiAutomationVisible());
            autoPage.TurnOnAutomation();
            Assert.IsTrue(autoPage.GetIsWifiAutomationVisible());
            Assert.IsFalse(autoPage.WifiAutomation.GetIsValueSet());
            string wifiButtonTitle = autoPage.WifiAutomation.GetTitle();
            Assert.IsNotEmpty(wifiButtonTitle);
            autoPage.WifiAutomation.OpenSettings();

            //Wifi binding page
            //WARNING: This section requires an active Wi-Fi connection or else assertions will fail.
            new AutomationWifiBindingPage().TapBack();
            new ProgramAutomationPage().WifiAutomation.OpenSettings();
            var wifiPage = new AutomationWifiBindingPage();
            string wifiTitle = wifiPage.GetNavigationBarTitle();
            string wifiDescription = wifiPage.GetDescription();
            string wifiStatus = wifiPage.GetWifiStatus();
            string wifiOkText = wifiPage.GetOkButtonText();
            Assert.IsNotEmpty(wifiTitle);
            Assert.IsNotEmpty(wifiDescription);
            Assert.IsNotEmpty(wifiStatus);
            Assert.IsNotEmpty(wifiOkText);
            Assert.IsFalse(wifiPage.GetIsCancelButtonVisible());
            Assert.IsTrue(wifiPage.GetIsWifiFound(), "Unable to verify the app is connected to a WLAN. Is the network adapter enabled?");
            string wifiName = wifiPage.GetWifiName();
            Assert.IsNotEmpty(wifiName);
            wifiPage.Ok();

            //Wifi selection button (again)
            Assert.IsTrue(autoPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));
            Assert.IsTrue(autoPage.GetIsWifiAutomationVisible());
            Assert.IsTrue(autoPage.WifiAutomation.GetIsValueSet());
            Assert.AreEqual(wifiName, autoPage.WifiAutomation.GetValue());
            autoPage.WifiAutomation.DeleteValue();
            autoPage.TurnOnAutomation();
            Assert.IsTrue(autoPage.GetIsWifiAutomationVisible());
            Assert.IsFalse(autoPage.WifiAutomation.GetIsValueSet());

            autoPage.TurnOffAutomation();

            //Geofence selection button
            Assert.IsFalse(autoPage.GetIsGeofenceAutomationVisible());
            autoPage.TurnOnAutomation();
            Assert.IsTrue(autoPage.GetIsGeofenceAutomationVisible());
            Assert.IsFalse(autoPage.GeofenceAutomation.GetIsValueSet());
            string geoButtonTitle = autoPage.GeofenceAutomation.GetTitle();
            Assert.IsNotEmpty(geoButtonTitle);
            autoPage.GeofenceAutomation.OpenSettings();

            //Geofence binding page
            new AutomationGeofenceBindingPage().WaitUntilNoLoadingIndicator().TapBack();
            new ProgramAutomationPage().GeofenceAutomation.OpenSettings();
            var geoPage = new AutomationGeofenceBindingPage().WaitUntilNoLoadingIndicator();
            string geoTitle = geoPage.GetNavigationBarTitle();
            string geoOkText = geoPage.GetOkButtonText();
            Assert.IsNotEmpty(geoTitle);
            Assert.IsNotEmpty(geoOkText);
            Assert.IsFalse(geoPage.GetIsCancelButtonVisible());
            Assert.IsFalse(geoPage.GetIsOkButtonEnabled());
            if (OnAndroid)
            {
                geoPage.ANDROID_ONLY_GoToMyLocation();
            }
            geoPage.SelectPosition(0.25, 0.25);
            string geoAddressTitle = geoPage.GetAddressTitle();
            string geoAddressDesc = geoPage.GetAddressDescription();
            Assert.IsNotEmpty(geoAddressTitle);
            Assert.IsNotEmpty(geoAddressDesc);
            Assert.IsTrue(geoPage.GetIsOkButtonEnabled());
            geoPage.Ok();

            //Geofence selection button (again)
            Assert.IsTrue(autoPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));
            Assert.IsTrue(autoPage.GetIsGeofenceAutomationVisible());
            Assert.IsTrue(autoPage.GeofenceAutomation.GetIsValueSet());
            Assert.AreEqual(geoAddressDesc, autoPage.GeofenceAutomation.GetValue());
            autoPage.GeofenceAutomation.DeleteValue();
            autoPage.TurnOnAutomation();
            Assert.IsTrue(autoPage.GetIsGeofenceAutomationVisible());
            Assert.IsFalse(autoPage.GeofenceAutomation.GetIsValueSet());

            //Help page
            autoPage.OpenHelpPage();
            var helpPage = new AutomationHelpPage();
            string helpTitle = helpPage.GetNavigationBarTitle();
            string helpDesc = helpPage.GetDescription();
            string helpHeaderGeo = helpPage.GetGeoHeader();
            string helpDescGeo = helpPage.GetGeoDescription();
            string helpWifiHeader = helpPage.GetWifiHeader();
            string helpWifiDesc = helpPage.GetWifiDescription();
            Assert.IsNotEmpty(helpTitle);
            Assert.IsNotEmpty(helpDesc);
            Assert.IsNotEmpty(helpHeaderGeo);
            Assert.IsNotEmpty(helpDescGeo);
            Assert.IsNotEmpty(helpWifiHeader);
            Assert.IsNotEmpty(helpWifiDesc);
            helpPage.TapBack();

            new ProgramAutomationPage().Proceed();

            new ProgramDetailPage().OpenProgramSettings();

            Assert.IsTrue(settingsPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(3)));
            settingsPage.DeleteFavoriteAndConfirm();
            new ProgramDetailPage();
        }

        [Test]
        [Category("SelfTest")]
        public void SkipToPages()
        {
            var result1 = LaunchHelper.SkipToInitializeHardwarePage();
            var initPage = result1.Page;
            var launchLog = result1.Log;
            Assert.IsTrue(initPage.IsCurrentlyShown());
            new InitializeHardwarePage();
            Assert.IsTrue(launchLog.WasOnInitializeHardwarePage);
            Assert.AreEqual(typeof(IntroPageOne), launchLog.InitialPageType);

            AppManager.RestartApp(true);
            var result2 = LaunchHelper.StartAppInDemoModeForTheFirstTime();
            var dashboardPage = result2.Page;
            launchLog = result2.Log;
            Assert.IsTrue(dashboardPage.IsCurrentlyShown());
            new DashboardPage();
            Assert.IsTrue(launchLog.WasOnInitializeHardwarePage);
            Assert.AreEqual(typeof(IntroPageOne), launchLog.InitialPageType);
        }

        [Test]
        [Category("SelfTest")]
        public void ProgramsMenu()
        {
            var dashboardPage = LaunchHelper.StartAppInDemoModeByAnyMeans().Page;
            dashboardPage.OpenMenuUsingSwipe();
            new MainMenuPage().OpenPrograms();
            var page = new ProgramsMenuPage();
            int programCounts = page.GetNumberOfPrograms();
            int mainCount = page.GetNumberOfMainPrograms();
            int streamingCount = page.GetNumberOfStreamingPrograms();
            int favoriteCount = page.GetNumberOfFavoritePrograms();
            Assert.AreEqual(4, programCounts);
            Assert.AreEqual(4, mainCount + streamingCount + favoriteCount);

            string main0 = page.GetMainProgramName(0);
            string main1 = page.GetMainProgramName(1);
            string main2 = page.GetMainProgramName(2);
            string streaming0 = page.GetStreamingProgramName(0);

            var allNames = page.GetAllProgramNames();

            Assert.AreEqual(main0, allNames[0]);
            Assert.AreEqual(main1, allNames[1]);
            Assert.AreEqual(main2, allNames[2]);
            Assert.AreEqual(streaming0, allNames[3]);

            page.SelectProgram(3);

            dashboardPage.IsShowingBeforeTimeout(TimeSpan.FromSeconds(5));
            Assert.AreEqual(allNames[3].ToLower(), dashboardPage.GetCurrentProgramName().ToLower());
        }
    }
}
