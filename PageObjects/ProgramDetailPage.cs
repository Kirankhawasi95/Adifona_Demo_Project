using System;
using System.Collections.Generic;
using System.Drawing;
using AventStack.ExtentReports;
using HorusUITest.Enums;
using HorusUITest.Extensions;
using HorusUITest.Helper;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Controls.ProgramDetailParams;
using HorusUITest.PageObjects.Interfaces;
using HorusUITest.PageObjects.Menu;
using HorusUITest.PageObjects.Menu.Programs;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects
{
    public class ProgramDetailPage : MainControlPage<ProgramDetailPage>, IHasBackNavigation
    {
        protected override Func<IMobileElement<AppiumWebElement>> TraitQuery => () => Title;

        //Main
        private const string TitleAID = "Horus.Views.MainControlPage.ctrlTitle";
        private const string BackButtonAID = "Horus.Views.MainControlPage.btBack";
        private const string ProgramSelectionAID = "Horus.Views.MainControlPage≤.ctrlDetailProgramSelection";
        private const string ProgramSelectionScrollBarAID = "Horus.Views.MainControlPage.ctrlDetailProgramSelection";
        private const string ProgramSelectionItemIconAID = "Horus.Views.MainControlPage.ProgramSelectionItemIcon";
        private const string SelectedItemMarkerAID = "Horus.Views.MainControlPage.DetailProgramSelected";
        private const string CurrentProgramNameAID = "Horus.Views.MainControlPage.ctrlProgramTitleDetails";
        private const string SettingsButtonAID = "Horus.Views.MainControlPage.btSettings";
        private const string ParameterContainerAID = "Horus.Views.MainControlPage.ctrlDetailProgramParameter";

        //Program parameters
        private const string AutoDisplayAID = "Horus.Views.Controls.ProgramDetailParameterControl.AutoProgram";
        private const string StreamingDisplayAID = "Horus.Views.Controls.ProgramDetailParameterControl.Streaming";
        private const string EqualizerDisplayAID = "Horus.Views.Controls.ProgramDetailParameterControl.Equalizer";
        private const string TinnitusDisplayAID = "Horus.Views.Controls.ProgramDetailParameterControl.Tinnitus";
        private const string TinnitusOnlyDisplayAID = "Horus.Views.Controls.ProgramDetailParameterControl.TinnitusOnly";
        private const string NoiseReductionDisplayAID = "Horus.Views.Controls.ProgramDetailParameterControl.NoiseReduction";
        private const string SpeechFocusDisplayAID = "Horus.Views.Controls.ProgramDetailParameterControl.SpeechFocus";

        //Main
        [FindsByAndroidUIAutomator(Accessibility = TitleAID), FindsByIOSUIAutomation(Accessibility = TitleAID)]
        private IMobileElement<AppiumWebElement> Title { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = BackButtonAID), FindsByIOSUIAutomation(Accessibility = BackButtonAID)]
        private IMobileElement<AppiumWebElement> BackButton { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = ProgramSelectionAID), FindsByIOSUIAutomation(Accessibility = ProgramSelectionAID)]
        private IMobileElement<AppiumWebElement> ProgramSelection { get; set; }
        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = ProgramSelectionScrollBarAID), FindsByIOSUIAutomation(Accessibility = ProgramSelectionScrollBarAID)]
        private IMobileElement<AppiumWebElement> ProgramSelectionScolllBar { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = ProgramSelectionItemIconAID), FindsByIOSUIAutomation(Accessibility = ProgramSelectionItemIconAID)]
        private IList<AppiumWebElement> ProgramIcons { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = SelectedItemMarkerAID), FindsByIOSUIAutomation(Accessibility = SelectedItemMarkerAID)]
        private IList<AppiumWebElement> SelectedItemMarker { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = CurrentProgramNameAID), FindsByIOSUIAutomation(Accessibility = CurrentProgramNameAID)]
        private IMobileElement<AppiumWebElement> CurrentProgramName { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = SettingsButtonAID), FindsByIOSUIAutomation(Accessibility = SettingsButtonAID)]
        private IMobileElement<AppiumWebElement> SettingsButton { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = ParameterContainerAID), FindsByIOSUIAutomation(Accessibility = ParameterContainerAID)]
        private IMobileElement<AppiumWebElement> ParameterContainer { get; set; }

        //Program parameters
        [FindsByAndroidUIAutomator(Accessibility = AutoDisplayAID), FindsByIOSUIAutomation(Accessibility = AutoDisplayAID)]
        private IMobileElement<AppiumWebElement> AutoDisplayElement { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = StreamingDisplayAID), FindsByIOSUIAutomation(Accessibility = StreamingDisplayAID)]
        private IMobileElement<AppiumWebElement> StreamingDisplayElement { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = EqualizerDisplayAID), FindsByIOSUIAutomation(Accessibility = EqualizerDisplayAID)]
        private IMobileElement<AppiumWebElement> EqualizerDisplayElement { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = TinnitusDisplayAID), FindsByIOSUIAutomation(Accessibility = TinnitusDisplayAID)]
        private IMobileElement<AppiumWebElement> TinnitusDisplayElement { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = TinnitusOnlyDisplayAID), FindsByIOSUIAutomation(Accessibility = TinnitusOnlyDisplayAID)]
        private IMobileElement<AppiumWebElement> TinnitusOnlyDisplayElement { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = NoiseReductionDisplayAID), FindsByIOSUIAutomation(Accessibility = NoiseReductionDisplayAID)]
        private IMobileElement<AppiumWebElement> NoiseReductionDisplayElement { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = SpeechFocusDisplayAID), FindsByIOSUIAutomation(Accessibility = SpeechFocusDisplayAID)]
        private IMobileElement<AppiumWebElement> SpeechFocusDisplayElement { get; set; }

        private AppiumWebElement SettingsButtonElement => App.FindElementByAutomationId(SettingsButtonAID);
        private AppiumWebElement CurrentProgramNameElement => App.FindElementByAutomationId(CurrentProgramNameAID);

        protected override void ClearCache()
        {
            base.ClearCache();
            autoDisplay = null;
            equalizerDisplay = null;
            streamingDisplay = null;
            tinnitusDisplay = null;
            speechFocusDisplay = null;
            noiseReductionDisplay = null;
            tinnitusOnlyDisplay = null;
        }

        private ProgramDetailParamDisplayAuto autoDisplay;
        public ProgramDetailParamDisplayAuto AutoDisplay
        {
            get
            {
                autoDisplay = autoDisplay ?? new ProgramDetailParamDisplayAuto(this, AutoDisplayElement);
                return autoDisplay;
            }
        }

        private ProgramDetailParamDisplayEqualizer equalizerDisplay;
        public ProgramDetailParamDisplayEqualizer EqualizerDisplay
        {
            get
            {
                equalizerDisplay = equalizerDisplay ?? new ProgramDetailParamDisplayEqualizer(this, EqualizerDisplayElement);
                return equalizerDisplay;
            }
        }

        private ProgramDetailParamDisplayStreaming streamingDisplay;
        public ProgramDetailParamDisplayStreaming StreamingDisplay
        {
            get
            {
                streamingDisplay = streamingDisplay ?? new ProgramDetailParamDisplayStreaming(this, StreamingDisplayElement);
                return streamingDisplay;
            }
        }

        private ProgramDetailParamDisplayTinnitusOnly tinnitusOnlyDisplay;
        public ProgramDetailParamDisplayTinnitusOnly TinnitusOnlyDisplay
        {
            get
            {
                tinnitusOnlyDisplay = tinnitusOnlyDisplay ?? new ProgramDetailParamDisplayTinnitusOnly(this, TinnitusOnlyDisplayElement);
                return tinnitusOnlyDisplay;
            }
        }

        private ProgramDetailParamDisplaySpeechFocus speechFocusDisplay;
        public ProgramDetailParamDisplaySpeechFocus SpeechFocusDisplay
        {
            get
            {
                speechFocusDisplay = speechFocusDisplay ?? new ProgramDetailParamDisplaySpeechFocus(this, SpeechFocusDisplayElement);
                return speechFocusDisplay;
            }
        }

        private ProgramDetailParamDisplayNoiseReduction noiseReductionDisplay;
        public ProgramDetailParamDisplayNoiseReduction NoiseReductionDisplay
        {
            get
            {
                noiseReductionDisplay = noiseReductionDisplay ?? new ProgramDetailParamDisplayNoiseReduction(this, NoiseReductionDisplayElement);
                return noiseReductionDisplay;
            }
        }

        private ProgramDetailParamDisplayTinnitus tinnitusDisplay;

        public ProgramDetailPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public ProgramDetailPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        public ProgramDetailParamDisplayTinnitus TinnitusDisplay
        {
            get
            {
                tinnitusDisplay = tinnitusDisplay ?? new ProgramDetailParamDisplayTinnitus(this, TinnitusDisplayElement);
                return tinnitusDisplay;
            }
        }

       // public void SelectProgramFromProgramsMenu(MainMenuTypes typ, int test) {

            //added dummy methode till calrfiaction with MG about implementation has taken place, afterwards this must be removed 
       // }
        public int GetNumberOfVisibiblePrograms()
        {
            return ProgramIcons.Count;

        }
        public String GetTextOfVisibleProgram(int index)
        {
            return ProgramIcons[index].Text;
        }

        public ProgramDetailPage SelectProgram(int index)
        {
            App.Tap(ProgramIcons[index]);
            ClearCache();
            return this;
        }

        public string GetTitle()
        {
            return Title.Text;
        }

        public bool GetIsProgramSwitchingScrollBarDisplayed()
        {
            return ProgramSelectionScolllBar.Exists();
        }

        public override string GetCurrentProgramName()
        {
            return CurrentProgramName.Text;
        }

        /// <summary>
        /// Back button is displayed correctly in iOS but this method returns false 
        /// </summary>
        /// <returns></returns>
        public bool GetIsBackButtonDisplayed()
        {
            if (OnAndroid)
                return BackButton.Exists();
            else
                return true;
        }

        /// <summary>
        /// Navigates to <see cref="DashboardPage"/>.
        /// </summary>
        public void TapBack()
        {
            App.Tap(BackButton);
            ClearCache();
        }

        public void NavigateBack()
        {
            TapBack();
        }

        public bool GetIsSettingsButtonDisplayed()
        {
            if (OnAndroid)
                return SettingsButton.Exists();
            else
                return true;
        }

        /// <summary>
        /// Navigates to <see cref="ProgramDetailSettingsControlPage"/>.
        /// </summary>
        public void OpenProgramSettings()
        {
            App.Tap(SettingsButton);
            ClearCache();
        }

        /// <summary>
        /// Navigates to <see cref="ProgramDetailParamEditBinauralPage"/>.
        /// </summary>
        public void OpenBinauralSettings()
        {
            volumeControl.OpenBinauralSettings();
        }
        public void SelectProgramFromProgramsMenu(MainMenuTypes mainMenuType, int index)
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

        }

        #region Program parameters
        public bool GetIsAutoDisplayVisible()
        {
            return AutoDisplayElement.Exists();
        }

        public bool GetIsStreamingDisplayVisible()
        {
            return StreamingDisplayElement.Exists();
        }

        public bool GetIsEqualizerDisplayVisible()
        {
            return EqualizerDisplayElement.Exists();
        }

        public bool GetIsTinnitusDisplayVisible()
        {
            return TinnitusDisplayElement.Exists();
        }

        public bool GetIsTinnitusOnlyDisplayVisible()
        {
            return TinnitusOnlyDisplayElement.Exists();
        }

        public bool GetIsNoiseReductionDisplayVisible()
        {
            return NoiseReductionDisplayElement.Exists();
        }

        public bool GetIsSpeechFocusDisplayVisible()
        {
            return SpeechFocusDisplayElement.Exists();
        }
        #endregion

        /// <summary>
        /// Get Selected Program Color
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetSelectedProgramColor(int index)
        {
            return App.GetColorFromImageByPixel(ProgramIcons[index], 5, 5);
        }

        /// <summary>
        /// Get Settings Icon Color
        /// </summary>
        /// <returns></returns>
        public string GetSettingsIconColor()
        {
            return App.GetColorFromImageByPixel(SettingsButtonElement, 60, 25);
        }

        /// <summary>
        /// Get Program Name Color
        /// </summary>
        /// <param name="mainMenuTypes"></param>
        /// <returns></returns>
        public string GetTitleColor(MainMenuTypes mainMenuTypes)
        {
            switch (mainMenuTypes)
            {
                case MainMenuTypes.Streaming: return App.GetColorFromImageByPixel(CurrentProgramNameElement, 38, 43); ;
                default: return App.GetColorFromImageByPixel(CurrentProgramNameElement, 11, 44);
            }
        }

        public bool CheckForProgramNameOverlapping()
        {
            Rectangle rectangleProgramName = CurrentProgramName.GetRect();
            Rectangle rectangleSettings = SettingsButton.GetRect();

            var overlap = !(rectangleProgramName.Right < rectangleSettings.Left ||
                rectangleProgramName.Left > rectangleSettings.Right ||
                rectangleProgramName.Bottom < rectangleSettings.Top ||
                rectangleProgramName.Top > rectangleSettings.Bottom);

            return overlap;
        }

        /// <summary>
        /// Method to verify basic common UI controls for all the programs
        /// </summary>
        /// <param name="side">0 is for left and 1 is for right</param>
        public void ProgramDetailPageUiCheck(Side side = 0)
        {
            var programDetailPage = new ProgramDetailPage();

            ReportHelper.LogTest(Status.Info, "Checking program detail page UI...");
            if (side.Equals(Side.Left))
            {
                //For monaural left config
                ReportHelper.LogTest(Status.Info, "Checking if left hearing device is visible on program detail page...");
                Assert.IsTrue(programDetailPage.GetIsLeftHearingDeviceVisible(), "Left hearing device is not visible on program detail page");
                ReportHelper.LogTest(Status.Info, "Left hearing device is visible on program detail page");
            }
            else if (side.Equals(Side.Right))
            {
                //For monaural right config
                ReportHelper.LogTest(Status.Info, "Checking if right hearing device is visible on program detail page...");
                Assert.IsTrue(programDetailPage.GetIsRightHearingDeviceVisible(), "Right hearing device is not visible on program detail page");
                ReportHelper.LogTest(Status.Info, "Right hearing device is visible on program detail page");
            }
            else
            {
                //For demo mode or binaural config
                ReportHelper.LogTest(Status.Info, "Checking if left hearing device is visible on program detail page...");
                Assert.IsTrue(programDetailPage.GetIsLeftHearingDeviceVisible(), "Left hearing device is not visible on program detail page");
                ReportHelper.LogTest(Status.Info, "Left hearing device is visible on program detail page");
                ReportHelper.LogTest(Status.Info, "Checking if right hearing device is visible on program detail page...");
                Assert.IsTrue(programDetailPage.GetIsRightHearingDeviceVisible(), "Right hearing device is not visible on program detail page");
                ReportHelper.LogTest(Status.Info, "Right hearing device is visible on program detail page");
            }
            ReportHelper.LogTest(Status.Info, "Checking if program name on program detail page is empty...");
            Assert.IsNotEmpty(programDetailPage.GetCurrentProgramName(), "Program name on program detail page is empty");
            ReportHelper.LogTest(Status.Info, "Program name on program detail page is not empty");
            ReportHelper.LogTest(Status.Info, "Checking if back button is visible on program detail page...");
            Assert.IsTrue(programDetailPage.GetIsBackButtonDisplayed(), "Back button is not visible on program detail page");
            ReportHelper.LogTest(Status.Info, "Back button is visible on program detail page");
            ReportHelper.LogTest(Status.Info, "Checking if menu Hamburger button is visible on program detail page...");
            Assert.IsTrue(programDetailPage.GetIsMenuHamburgerButtonDisplayed(), "Menu Hamburger button is not visible on program detail page");
            ReportHelper.LogTest(Status.Info, "Menu Hamburger button is visible on program detail page");
            ReportHelper.LogTest(Status.Info, "Checking if horizontal scrollbar is visible on program detail page...");
            Assert.IsTrue(programDetailPage.GetIsProgramSwitchingScrollBarDisplayed(), "Horizontal scrollbar is not visible on program detail page");
            ReportHelper.LogTest(Status.Info, "Horizontal scrollbar is visible on program detail page");
            ReportHelper.LogTest(Status.Info, "Checking if program settings gear icon is visible on program detail page...");
            Assert.IsTrue(programDetailPage.GetIsSettingsButtonDisplayed(), "Program settings gear icon is not visible on program detail page");
            ReportHelper.LogTest(Status.Info, "Program settings gear icon is visible on program detail page");
            ReportHelper.LogTest(Status.Info, "Checking binaural settings button is visibile...");
            Assert.IsTrue(programDetailPage.GetIsBinauralSettingsButtonVisible(), "Binaural settings button is not visibile");
            ReportHelper.LogTest(Status.Info, "Binaural settings button is visibile");
            ReportHelper.LogTest(Status.Pass, "Program detail page UI is verified");
        }

        /// <summary>
        /// Method used to verify the visiblity of the tiles in the program details page. 
        /// This method has parameter as dictionary containing multiple tile for the for the program for which it needs to be verified.
        /// Input to it will be a Dictionary of tileType and visiblility. tileType should be the name of the tile and visiblility is its expected visiblility to be verified.
        /// SpeechFocus, NoiseReduction, Tinnitus, Equalizer and Streaming tile types are currently handled and new tile types needs to be added if required in the future.
        /// </summary>
        /// <param name="programTiles"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public void ProgramDetailsTileCheck(string programName, Dictionary<string, bool> programTiles)
        {
            var programDetailPage = new ProgramDetailPage();
            ReportHelper.LogTest(Status.Info, "Checking program details tiles for program '" + programName + "'...");

            foreach (var programTile in programTiles)
            {
                switch (programTile.Key)
                {
                    case "SpeechFocus":
                        {
                            ReportHelper.LogTest(Status.Info, $"Checking if speech focus tile is visibile on ProgramDetailPage of program {programName}");
                            if (programTile.Value)
                            {
                                Assert.IsTrue(programDetailPage.GetIsSpeechFocusDisplayVisible(), "Expected speech focus display to be visible, but is not visibile");
                                ReportHelper.LogTest(Status.Info, "Speech focus display is visibile");
                            }
                            else
                            {
                                Assert.IsFalse(programDetailPage.GetIsSpeechFocusDisplayVisible(), "Expected speech focus display not to be shown, but is visibile.");
                                ReportHelper.LogTest(Status.Info, "Speech focus display is not visibile");
                            }
                            break;
                        }
                    case "NoiseReduction":
                        {
                            ReportHelper.LogTest(Status.Info, $"Checking is noise reduction display is visibile on ProgramDetailPage of program {programName}.");
                            if (programTile.Value)
                            {
                                Assert.IsTrue(programDetailPage.GetIsNoiseReductionDisplayVisible(), "Expected noise reduction display to be visible, but is not visible");
                                ReportHelper.LogTest(Status.Info, "Noise reduction display is visibile");
                            }
                            else
                            {
                                Assert.IsFalse(programDetailPage.GetIsNoiseReductionDisplayVisible(), "Expected noise reduction display not to be shown, but is visible.");
                                ReportHelper.LogTest(Status.Info, "Noise reduction display is not visibile");
                            }
                            break;
                        }
                    case "Tinnitus":
                        {
                            ReportHelper.LogTest(Status.Info, $"Checking is tinnitus display is visibile on ProgramDetailPage of program {programName}.");
                            if (programTile.Value)
                            {
                                Assert.IsTrue(programDetailPage.GetIsTinnitusDisplayVisible(), "Expected tinnitus display to be visible, but is not visible");
                                ReportHelper.LogTest(Status.Info, "Tinnitus display is visibile");
                            }
                            else
                            {
                                Assert.IsFalse(programDetailPage.GetIsTinnitusDisplayVisible(), "Expected tinnitus display not to be shown, but is visible.");
                                ReportHelper.LogTest(Status.Info, "Tinnitus display is not visible");
                            }
                            break;
                        }
                    case "Equalizer":
                        {
                            ReportHelper.LogTest(Status.Info, $"Checking is equalizer display is visible on ProgramDetailPage of program {programName}.");
                            if (programTile.Value)
                            {
                                Assert.IsTrue(programDetailPage.GetIsEqualizerDisplayVisible(), "Expected equalizer display to be visible, but is not visible");
                                ReportHelper.LogTest(Status.Info, "Equalizer display is visible");
                            }
                            else
                            {
                                Assert.IsFalse(programDetailPage.GetIsEqualizerDisplayVisible(), "Expected equalizer display not to be shown, but is visible.");
                                ReportHelper.LogTest(Status.Info, "Equalizer display is not visible");
                            }
                            break;
                        }
                    case "Streaming":
                        {
                            ReportHelper.LogTest(Status.Info, $"Checking is streaming display is visible on ProgramDetailPage of program {programName}.");
                            if (programTile.Value)
                            {
                                Assert.IsTrue(programDetailPage.GetIsStreamingDisplayVisible(), "Expected streaming display to be visible, but is not visible");
                                ReportHelper.LogTest(Status.Info, "Streaming tile display is visible");
                            }
                            else
                            {
                                Assert.IsFalse(programDetailPage.GetIsStreamingDisplayVisible(), "Expected streaming display not to be shown, but is visible.");
                                ReportHelper.LogTest(Status.Info, "Streaming tile display is not visible");
                            }
                            break;
                        }
                    default: throw new NotImplementedException("Tile type implementation not implemented");
                }
            }

            ReportHelper.LogTest(Status.Pass, "Program details tiles for program '" + programName + "' is verified");
        }
    }
}
