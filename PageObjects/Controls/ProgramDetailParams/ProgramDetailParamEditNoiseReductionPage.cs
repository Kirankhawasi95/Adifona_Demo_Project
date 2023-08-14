using System;
using HorusUITest.Enums;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Controls.ProgramDetailParams
{
    public class ProgramDetailParamEditNoiseReductionPage : BaseParamEdit<ProgramDetailParamEditNoiseReductionPage>
    {
        protected override string ContainerAID => "Horus.Views.MainControlPage.NoiseReductionContainer";

        private const string TitleAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditNoiseReduction.Title";
        private const string DescriptionAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditNoiseReduction.Description";
        private const string ValueSliderAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditNoiseReduction.ValueSlider";

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = TitleAID), FindsByIOSUIAutomation(Accessibility = TitleAID)]
        private IMobileElement<AppiumWebElement> Title { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = DescriptionAID), FindsByIOSUIAutomation(Accessibility = DescriptionAID)]
        private IMobileElement<AppiumWebElement> Description { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = ValueSliderAID), FindsByIOSUIAutomation(Accessibility = ValueSliderAID)]
        private IMobileElement<AppiumWebElement> ValueSliderElement { get; set; }

        private ValueSlider<ProgramDetailParamEditNoiseReductionPage> valueSlider;

        public ProgramDetailParamEditNoiseReductionPage(bool assertOnPage = true) : base(assertOnPage)
        {
            valueSlider = new ValueSlider<ProgramDetailParamEditNoiseReductionPage>(this, ValueSliderElement, Enum.GetNames(typeof(NoiseReduction)).Length);
        }

        public ProgramDetailParamEditNoiseReductionPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
            valueSlider = new ValueSlider<ProgramDetailParamEditNoiseReductionPage>(this, ValueSliderElement, Enum.GetNames(typeof(NoiseReduction)).Length);
        }

        public string GetTitle()
        {
            return Title.Text;
        }

        public string GetDescription()
        {
            return Description.Text;
        }

        public NoiseReduction GetSelectedNoiseReduction()
        {
            return (NoiseReduction)(GetSelectedItemIndexBySelectedIndex(valueSlider.GetSelectedIndex()));
        }

        //ToDo: The index varies in Selected Index and Selected Item of slider. Giving a patch so that both matches. Needs to be changed
        private int GetSelectedItemIndexBySelectedIndex(int SelectedIndex)
        {
            switch (SelectedIndex)
            {
                case 1: return 3;
                case 2: return 1;
                case 3: return 4;
                case 4: return 2;
                default: return SelectedIndex;
            }
        }

        public ProgramDetailParamEditNoiseReductionPage SelectNoiseReduction(NoiseReduction noiseReduction)
        {
            return valueSlider.SelectItem((int)noiseReduction);
        }

        public string GetNoiseReductionName(NoiseReduction noiseReduction)
        {
            return valueSlider.GetItem((int)noiseReduction);
        }

        public string GetSelectedNoiseReductionName()
        {
            return GetNoiseReductionName(GetSelectedNoiseReduction());
        }
    }
}
