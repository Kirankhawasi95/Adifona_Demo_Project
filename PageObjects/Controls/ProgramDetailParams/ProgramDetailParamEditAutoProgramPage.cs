using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Controls.ProgramDetailParams
{
    [Obsolete("Usage within the app is unclear.")]
    public class ProgramDetailParamEditAutoProgramPage : BaseParamEdit<ProgramDetailParamEditAutoProgramPage>
    {
        protected override string ContainerAID => "Horus.Views.MainControlPage.AutoProgramContainer";

        private const string TitleAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditAutoProgram.Title";
        private const string DescriptionAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditAutoProgram.Description";
        private const string AutoProgramSwitchTitleAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditAutoProgram.AutoProgramSwitchTitle";
        private const string AutoProgramSwitchAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditAutoProgram.AutoProgramSwitch";

        public ProgramDetailParamEditAutoProgramPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public ProgramDetailParamEditAutoProgramPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = TitleAID), FindsByIOSUIAutomation(Accessibility = TitleAID)]
        private IMobileElement<AppiumWebElement> Title { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = DescriptionAID), FindsByIOSUIAutomation(Accessibility = DescriptionAID)]
        private IMobileElement<AppiumWebElement> Description { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = AutoProgramSwitchTitleAID), FindsByIOSUIAutomation(Accessibility = AutoProgramSwitchTitleAID)]
        private IMobileElement<AppiumWebElement> AutoProgramSwitchTitle { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = AutoProgramSwitchAID), FindsByIOSUIAutomation(Accessibility = AutoProgramSwitchAID)]
        private IMobileElement<AppiumWebElement> AutoProgramSwitch { get; set; }

        public string GetTitle()
        {
            return Title.Text;
        }

        public string GetDescription()
        {
            return Description.Text;
        }

        public string GetAutoProgramSwitchTitle()
        {
            return AutoProgramSwitchTitle.Text;
        }

        public bool GetIsAutoProgramSwitchChecked()
        {
            return App.GetToggleSwitchState(AutoProgramSwitch);
        }

        public ProgramDetailParamEditAutoProgramPage ToggleAutoProgramSwitch()
        {
            App.Tap(AutoProgramSwitch);
            return this;
        }

        public ProgramDetailParamEditAutoProgramPage TurnOnAutoProgram()
        {
            if (!GetIsAutoProgramSwitchChecked())
            {
                ToggleAutoProgramSwitch();
            }
            return this;
        }

        public ProgramDetailParamEditAutoProgramPage TurnOffAutoProgram()
        {
            if (GetIsAutoProgramSwitchChecked())
            {
                ToggleAutoProgramSwitch();
            }
            return this;
        }
    }
}
