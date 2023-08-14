using System;
using HorusUITest.Enums;
using HorusUITest.Extensions;
using HorusUITest.Helper;
using HorusUITest.PageObjects.Dialogs;
using System.Threading;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu.Settings
{
    public class SettingLanguagePage : BaseNavigationPage
    {
        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Menu.Settings.SettingLanguagePage.NavigationBar");
        private AppiumWebElement Title => App.FindElementByAutomationId("Horus.Views.Menu.Settings.SettingLanguagePage.Title");
        private AppiumWebElement AcceptButton => App.FindElementByAutomationId("Horus.Views.Menu.Settings.SettingLanguagePage.Accept");

        private AppiumWebElement German => App.FindElementByAutomationId("Horus.ViewModels.Menu.Settings.German (Germany)");
        private AppiumWebElement English => App.FindElementByAutomationId("Horus.ViewModels.Menu.Settings.English (United States)");
        private AppiumWebElement EnglishUK => App.FindElementByAutomationId("Horus.ViewModels.Menu.Settings.English (United Kingdom)");
        private AppiumWebElement Spanish => App.FindElementByAutomationId("Horus.ViewModels.Menu.Settings.Spanish (Spain)");
        private AppiumWebElement Italian => App.FindElementByAutomationId("Horus.ViewModels.Menu.Settings.Italian (Italy)");
        private AppiumWebElement French => App.FindElementByAutomationId("Horus.ViewModels.Menu.Settings.French (France)");
        private AppiumWebElement Polish => App.FindElementByAutomationId("Horus.ViewModels.Menu.Settings.Polish (Poland)");
        private AppiumWebElement Dutch => App.FindElementByAutomationId("Horus.ViewModels.Menu.Settings.Dutch (Netherlands)");
        private AppiumWebElement Portuguese => App.FindElementByAutomationId("Horus.ViewModels.Menu.Settings.Portuguese (Brazil)");
        private AppiumWebElement Czech => App.FindElementByAutomationId("Horus.ViewModels.Menu.Settings.Czech (Czech Republic)");
        private AppiumWebElement Turkish => App.FindElementByAutomationId("Horus.ViewModels.Menu.Settings.Turkish (Turkey)");
        private AppiumWebElement Russian => App.FindElementByAutomationId("Horus.ViewModels.Menu.Settings.Russian (Russia)");
        private AppiumWebElement Greek => App.FindElementByAutomationId("Horus.ViewModels.Menu.Settings.Greek (Greece)");

        private AppiumWebElement SelectedLanguageName => App.FindElementByAutomationId("Horus.Views.Menu.Settings.SettingLanguagePage.SelectedLanguageName");
        private AppiumWebElement CurrentLanguageName => App.FindElementByAutomationId("Horus.Views.Menu.Settings.SettingLanguagePage.CurrentLanguageName");

        public SettingLanguagePage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public SettingLanguagePage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        public string GetTitle()
        {
            return Title.Text;
        }

        private Language GetLanguage(string languageName)
        {
            switch (languageName)
            {
                case "German (Germany)": return Language.German;
                case "English (United States)": return Language.English;
                case "Italian (Italy)": return Language.Italian;
                case "French (France)": return Language.French;
                case "Polish (Poland)": return Language.Polish;
                case "Dutch (Netherlands)": return Language.Dutch;
                case "Turkish (Turkey)": return Language.Turkish;
                default: throw new NotImplementedException($"Unknown language name '{languageName}'.");
            }
        }

        private Language_Audifon GetLanguageAudifon(string languageName)
        {
            switch (languageName)
            {
                case "German (Germany)": return Language_Audifon.German;
                case "English (United States)": return Language_Audifon.English;
                case "English (United Kingdom)": return Language_Audifon.English;
                case "Italian (Italy)": return Language_Audifon.Italian;
                case "French (France)": return Language_Audifon.French;
                case "Polish (Poland)": return Language_Audifon.Polish;
                case "Dutch (Netherlands)": return Language_Audifon.Dutch;
                case "Spanish (Spain)": return Language_Audifon.Spanish;
                case "Portuguese (Brazil)": return Language_Audifon.Portuguese;
                case "Czech (Czech Republic)": return Language_Audifon.Czech;
                case "Turkish (Turkey)": return Language_Audifon.Turkish;
                case "Russian (Russia)": return Language_Audifon.Russian;

                default: throw new NotImplementedException($"Unknown language name '{languageName}'.");
            }
        }

        private Language_Persona GetLanguagePersona(string languageName)
        {
            switch (languageName)
            {
                case "English (United States)": return Language_Persona.English;
                case "English (United Kingdom)": return Language_Persona.English;
                case "Spanish (Spain)": return Language_Persona.Spanish;
                case "Dutch (Netherlands)": return Language_Persona.Dutch;
                case "Portuguese (Brazil)": return Language_Persona.Portuguese;

                default: throw new NotImplementedException($"Unknown language name '{languageName}'.");
            }
        }

        private Language_Puretone GetLanguagePuretone(string languageName)
        {
            switch (languageName)
            {
                case "English (United Kingdom)": return Language_Puretone.English;
                case "Spanish (Spain)": return Language_Puretone.Spanish;
                //case "Greek (Greece)": return Language_Puretone.Greek;


                default: throw new NotImplementedException($"Unknown language name '{languageName}'.");
            }
        }

        private Language_Hormann GetLanguageHormann(string languageName)
        {
            switch (languageName)
            {
                case "English (United States)": return Language_Hormann.English;
                case "Spanish (Spain)": return Language_Hormann.Spanish;
                case "Portuguese (Brazil)": return Language_Hormann.Portuguese;
                default: throw new NotImplementedException($"Unknown language name '{languageName}'.");
            }
        }

        private Language_RxEarsPro GetLanguageRxEarsPro(string languageName)
        {
            switch (languageName)
            {
                case "English (United States)": return Language_RxEarsPro.English;
                default: throw new NotImplementedException($"Unknown language name '{languageName}'.");
            }
        }

        private AppiumWebElement GetLanguageElement(Language language)
        {
            switch (language)
            {
                case Language.German: return German;
                case Language.English: return English;
                case Language.Italian: return Italian;
                case Language.French: return French;
                case Language.Polish: return Polish;
                case Language.Dutch: return Dutch;
                case Language.Turkish: return Turkish;
                default: throw new NotImplementedException($"Unknown language '{language}'.");
            }
        }

        private AppiumWebElement GetLanguageElement(Language_Audifon language)
        {
            switch (language)
            {
                case Language_Audifon.German: return German;
                case Language_Audifon.English: return English;
                case Language_Audifon.Italian: return Italian;
                case Language_Audifon.French: return French;
                case Language_Audifon.Polish: return Polish;
                case Language_Audifon.Dutch: return Dutch;
                case Language_Audifon.Spanish: return Spanish;
                case Language_Audifon.Portuguese: return Portuguese;
                case Language_Audifon.Czech: return Czech;
                case Language_Audifon.Turkish: return Turkish;
                case Language_Audifon.Russian: return Russian;
                default: throw new NotImplementedException($"Unknown language '{language}'.");
            }
        }

        private AppiumWebElement GetLanguageElement(Language_Persona language)
        {
            switch (language)
            {
                case Language_Persona.English: return English;
                case Language_Persona.Spanish: return Spanish;
                case Language_Persona.Dutch: return Dutch;
                case Language_Persona.Portuguese: return Portuguese;
                default: throw new NotImplementedException($"Unknown language '{language}'.");
            }
        }

        private AppiumWebElement GetLanguageElement(Language_Puretone language)
        {
            switch (language)
            {
                case Language_Puretone.English: return EnglishUK;
                case Language_Puretone.Spanish: return Spanish;
                //case Language_Puretone.Greek: return Greek;
                default: throw new NotImplementedException($"Unknown language '{language}'.");
            }
        }

        private AppiumWebElement GetLanguageElement(Language_Hormann language)
        {
            switch (language)
            {
                case Language_Hormann.English: return English;
                case Language_Hormann.Spanish: return Spanish;
                case Language_Hormann.Portuguese: return Portuguese;
                default: throw new NotImplementedException($"Unknown language '{language}'.");
            }
        }

        private AppiumWebElement GetLanguageElement(Language_RxEarsPro language)
        {
            switch (language)
            {
                case Language_RxEarsPro.English: return English;
                default: throw new NotImplementedException($"Unknown language '{language}'.");
            }
        }

        private string GetSelectedLanguageName()
        {
            string result = null;
            bool condition()
            {
                result = SelectedLanguageName.Text;
                return result != "null";
            }
            Wait.UntilTrue(condition, TimeSpan.FromSeconds(2));
            return result;
        }

        private string GetCurrentLanguageName()
        {
            string result = null;
            bool condition()
            {
                result = CurrentLanguageName.Text;
                return result != "null";
            }
            Wait.UntilTrue(condition, TimeSpan.FromSeconds(2));
            return result;
        }

        /// <summary>
        /// Returns the text of the given <see cref="Language"/>.
        /// </summary>
        /// <param name="language"></param>
        /// <returns>The text that is shown on screen.</returns>
        public string GetLanguageText(Language language)
        {
            return GetLanguageElement(language).Text;
        }
		
        public string GetLanguageAudifonText(Language_Audifon languageAudifon)
        {
            return GetLanguageElement(languageAudifon).Text;
        }

        public string GetLanguagePersonaText(Language_Persona languagePersona)
        {
            return GetLanguageElement(languagePersona).Text;
        }

        public string GetLanguagePuretoneText(Language_Puretone languagePuretone)
        {
            return GetLanguageElement(languagePuretone).Text;
        }

        public string GetLanguageHormannText(Language_Hormann languageHormann)
        {
            return GetLanguageElement(languageHormann).Text;
        }

        public string GetLanguageRxEarsProText(Language_RxEarsPro languageRxEarsPro)
        {
            return GetLanguageElement(languageRxEarsPro).Text;
        }

        /// <summary>
        /// Returns the <see cref="Language"/> that is selected on screen.
        /// </summary>
        public Language GetSelectedLanguage()
        {
            return GetLanguage(GetSelectedLanguageName());
        }

        /// <summary>
        /// Returns the <see cref="Language_Audifon"/> that is selected on screen.
        /// </summary>
        public Language_Audifon GetSelectedLanguageAudifon()
        {
            return GetLanguageAudifon(GetSelectedLanguageName());
        }

        public Language_Persona GetSelectedLanguagePersona()
        {
            return GetLanguagePersona(GetSelectedLanguageName());
        }

        public Language_Puretone GetSelectedLanguagePuretone()
        {
            return GetLanguagePuretone(GetSelectedLanguageName());
        }

        /// <summary>
        /// Returns the <see cref="Language_Hormann"/> that is selected on screen.
        /// </summary>
        public Language_Hormann GetSelectedLanguageHormann()
        {
            return GetLanguageHormann(GetSelectedLanguageName());
        }

        public Language_RxEarsPro GetSelectedLanguageRxEarsPro()
        {
            return GetLanguageRxEarsPro(GetSelectedLanguageName());
        }

        /// <summary>
        /// Returns the <see cref="Language"/> the app is currently using.
        /// </summary>
        public Language GetCurrentLanguage()
        {
            return GetLanguage(GetCurrentLanguageName());
        }

        /// <summary>
        /// Returns the <see cref="Language_Audifon"/> the app is currently using.
        /// </summary>
        public Language_Audifon GetCurrentLanguageAudifon()
        {
            return GetLanguageAudifon(GetCurrentLanguageName());
        }

        public Language_Persona GetCurrentLanguagePersona()
        {
            return GetLanguagePersona(GetCurrentLanguageName());
        }

        public Language_Puretone GetCurrentLanguagePuretone()
        {
            return GetLanguagePuretone(GetCurrentLanguageName());
        }

        public Language_Hormann GetCurrentLanguageHormann()
        {
            return GetLanguageHormann(GetCurrentLanguageName());
        }

        public Language_RxEarsPro GetCurrentLanguageRxEarsPro()
        {
            return GetLanguageRxEarsPro(GetCurrentLanguageName());
        }

        /// <summary>
        /// Returns the text of the selected language.
        /// </summary>
        /// <returns></returns>
        public string GetSelectedLanguageText()
        {
            return GetLanguageText(GetSelectedLanguage());
        }

        /// <summary>
        /// Returns the text of the currently used language.
        /// </summary>
        /// <returns></returns>
        public string GetCurrentLanguageText()
        {
            return GetLanguageText(GetCurrentLanguage());
        }

        public Enum GetCurrentLangaugeGeneric(Brand brand)
        {
            switch (brand)
            {
                case Brand.Audifon:
                    return GetCurrentLanguageAudifon();
                case Brand.Kind:
                    return GetCurrentLanguage();
                case Brand.PersonaMedical:
                    return GetCurrentLanguagePersona();
                case Brand.Puretone:
                    return GetCurrentLanguagePuretone();
                case Brand.Hormann:
                    return GetCurrentLanguageHormann();
                case Brand.RxEarsPro:
                    return GetCurrentLanguageRxEarsPro();
                default:
                    throw new NotImplementedException("Band not inplemented.");
            }
        }

        public Enum GetSelectedLangaugeGeneric(Brand brand)
        {
            switch (brand)
            {
                case Brand.Audifon:
                    return GetSelectedLanguageAudifon();
                case Brand.Kind:
                    return GetSelectedLanguage();
                case Brand.PersonaMedical:
                    return GetSelectedLanguagePersona();
                case Brand.Puretone:
                    return GetSelectedLanguagePuretone();
                case Brand.Hormann:
                    return GetSelectedLanguageHormann();
                case Brand.RxEarsPro:
                    return GetSelectedLanguageRxEarsPro();
                default:
                    throw new NotImplementedException("Band not inplemented.");
            }
        }

        public bool GetIsAcceptButtonVisible()
        {
            return AcceptButton.Exists();
        }

        /// <summary>
        /// Taps the accept button. Shows an <see cref="AppDialog"/> before changing the language and leaving the page.
        /// </summary>
        public void Accept()
        {
            App.Tap(AcceptButton);
        }

        //public void SelectLanguageByBrand(Brand brand, Language language)
        //{

        //    switch (brand)
        //    {
        //        case Brand.Audifon:
        //            SettingLanguagePage.SelectLanguageAudifon(language);
        //            break;
        //        case Brand.Kind:
        //            break;
        //        case Brand.PersonaMedical:
        //            break;
        //        default:
        //            break;
        //    }
        //    //var languages = new Language_Audifon();
        //    if (brand == "Audifon")

        //    if (brand == "KIND")
        //        SettingLanguagePage.SelectLanguage(Language language);

        //}
        
        private bool swipeToBottom = false;
        
        /// <summary>
        /// Selects a <see cref="Language_Audifon"/>, but doesn't confirm the dialog.
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>

        public SettingLanguagePage SelectLanguageAudifon(Language_Audifon language)
        {
            var element = GetLanguageElement(language);
          
            if (!AppiumWebElementExtensions.Exists(element) && !swipeToBottom)
            {
                App.SwipeRelativeToScreenSize(0.5, 0.5, 0.5, 0.25);
                swipeToBottom = true;
            }

            if (!AppiumWebElementExtensions.Exists(element) && swipeToBottom)
            {
                App.SwipeRelativeToScreenSize(0.5, 0.5, 0.5, 0.75);
                swipeToBottom = false;
                Thread.Sleep(2000); //delay to ensure the swipe to top is executed already before checking for element
            }
            App.Tap(element);
            return this;
        }

        public SettingLanguagePage SelectLanguagePersona(Language_Persona language)
        {
            var element = GetLanguageElement(language);

            if (!AppiumWebElementExtensions.Exists(element) && !swipeToBottom)
            {
                App.SwipeRelativeToScreenSize(0.5, 0.5, 0.5, 0.25);
                swipeToBottom = true;
            }

            if (!AppiumWebElementExtensions.Exists(element) && swipeToBottom)
            {
                App.SwipeRelativeToScreenSize(0.5, 0.5, 0.5, 0.75);
                swipeToBottom = false;
                Thread.Sleep(2000); ////delay to ensure the swipe to top is executed already before checking for element
            }
            App.Tap(element);
            return this;
        }

        public SettingLanguagePage SelectLanguagePuretone(Language_Puretone language)
        {
            var element = GetLanguageElement(language);

            if (!AppiumWebElementExtensions.Exists(element) && !swipeToBottom)
            {
                App.SwipeRelativeToScreenSize(0.5, 0.5, 0.5, 0.25);
                swipeToBottom = true;
            }

            if (!AppiumWebElementExtensions.Exists(element) && swipeToBottom)
            {
                App.SwipeRelativeToScreenSize(0.5, 0.5, 0.5, 0.75);
                swipeToBottom = false;
                Thread.Sleep(2000); ////delay to ensure the swipe to top is executed already before checking for element
            }
            App.Tap(element);
            return this;
        }

        /// <summary>
        /// Selects a <see cref="Language"/>, but doesn't confirm the dialog.
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public SettingLanguagePage SelectLanguage(Language language)
        {
            var element = GetLanguageElement(language);

            if (!AppiumWebElementExtensions.Exists(element) && !swipeToBottom)
            {
                App.SwipeRelativeToScreenSize(0.5, 0.5, 0.5, 0.25);
                swipeToBottom = true;
            }

            if (!AppiumWebElementExtensions.Exists(element) && swipeToBottom)
            {
                App.SwipeRelativeToScreenSize(0.5, 0.5, 0.5, 0.75);
                swipeToBottom = false;
                Thread.Sleep(2000);//delay to ensure the swipe to top is executed already before checking for element
            }
            App.Tap(element);
            return this;
        }

        public SettingLanguagePage SelectLanguageHormann(Language_Hormann language)
        {
            var element = GetLanguageElement(language);

            if (!AppiumWebElementExtensions.Exists(element) && !swipeToBottom)
            {
                App.SwipeRelativeToScreenSize(0.5, 0.5, 0.5, 0.25);
                swipeToBottom = true;
            }

            if (!AppiumWebElementExtensions.Exists(element) && swipeToBottom)
            {
                App.SwipeRelativeToScreenSize(0.5, 0.5, 0.5, 0.75);
                swipeToBottom = false;
                Thread.Sleep(2000); //delay to ensure the swipe to top is executed already before checking for element
            }
            App.Tap(element);
            return this;
        }

        public SettingLanguagePage SelectLanguageRxEarsPro(Language_RxEarsPro language)
        {
            var element = GetLanguageElement(language);

            if (!AppiumWebElementExtensions.Exists(element) && !swipeToBottom)
            {
                App.SwipeRelativeToScreenSize(0.5, 0.5, 0.5, 0.25);
                swipeToBottom = true;
            }

            if (!AppiumWebElementExtensions.Exists(element) && swipeToBottom)
            {
                App.SwipeRelativeToScreenSize(0.5, 0.5, 0.5, 0.75);
                swipeToBottom = false;
                Thread.Sleep(2000); //delay to ensure the swipe to top is executed already before checking for element
            }
            App.Tap(element);
            return this;
        }

        /// <summary>
        /// Changes the app <see cref="Language"/> by selecting it and confirming the dialog.
        /// </summary>
        /// <param name="language"></param>
        public void ChangeLanguage(Language language)
        {
            SelectLanguage(language);
            Accept();
            DialogHelper.ConfirmIfDisplayed();
        }
    }
}
