using System;
using HorusUITest.Enums;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Controls.ProgramDetailParams
{
    public class ProgramDetailParamEditSpeechFocusPage : BaseParamEdit<ProgramDetailParamEditSpeechFocusPage>
    {
        protected override string ContainerAID => "Horus.Views.MainControlPage.SpeechFocusContainer";

        private const string TitleAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditSpeechFocus.Title";
        private const string DescriptionAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditSpeechFocus.Description";
        private const string ValueSliderAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamEditSpeechFocus.ValueSlider";

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = TitleAID), FindsByIOSUIAutomation(Accessibility = TitleAID)]
        private IMobileElement<AppiumWebElement> Title { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = DescriptionAID), FindsByIOSUIAutomation(Accessibility = DescriptionAID)]
        private IMobileElement<AppiumWebElement> Description { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = ValueSliderAID), FindsByIOSUIAutomation(Accessibility = ValueSliderAID)]
        private IMobileElement<AppiumWebElement> ValueSliderElement{ get; set; }

        private ValueSlider<ProgramDetailParamEditSpeechFocusPage> valueSlider;

        public ProgramDetailParamEditSpeechFocusPage(bool assertOnPage = true) : base(assertOnPage)
        {
            valueSlider = new ValueSlider<ProgramDetailParamEditSpeechFocusPage>(this, ValueSliderElement, Enum.GetNames(typeof(SpeechFocus)).Length);
        }

        public ProgramDetailParamEditSpeechFocusPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
            valueSlider = new ValueSlider<ProgramDetailParamEditSpeechFocusPage>(this, ValueSliderElement, Enum.GetNames(typeof(SpeechFocus)).Length);
        }

        public string GetTitle()
        {
            return Title.Text;
        }

        public string GetDescription()
        {
            return Description.Text;
        }

        public SpeechFocus GetSelectedSpeechFocus()
        {
            return (SpeechFocus)valueSlider.GetSelectedIndex();
        }

        public ProgramDetailParamEditSpeechFocusPage SelectSpeechFocus(SpeechFocus speechFocus)
        {
            return valueSlider.SelectItem((int)speechFocus);
        }

        public string GetSpeechFocusName(SpeechFocus speechFocus)
        {
            return valueSlider.GetItem((int)speechFocus);
        }

        public string GetSelectedSpeechFocusName()
        {
            return GetSpeechFocusName(GetSelectedSpeechFocus());
        }
        public void ChangeSettingAndClose(SpeechFocus speechFocus)
        {
            ITouchAction actionOne = new TouchAction(App.Driver);
            actionOne = TapCloseButton();

            ITouchAction actionTwo = new TouchAction(App.Driver);
            actionTwo = valueSlider.SelectItemByTouchAction((int)speechFocus);

            IMultiAction multiAction = new MultiAction(App.Driver);
            multiAction.Add(actionTwo);
            multiAction.Add(actionOne);

            multiAction.Perform();
        }
        public void LongPressCloseButtonAndChangeSetting(SpeechFocus speechFocus)
        {
            ITouchAction actionOne = new TouchAction(App.Driver);
            actionOne = LongPressCloseButton();

            ITouchAction actionTwo = new TouchAction(App.Driver);
            actionTwo = valueSlider.SelectItemByTouchAction((int)speechFocus);

            IMultiAction multiAction = new MultiAction(App.Driver);
            multiAction.Add(actionOne);
            multiAction.Add(actionTwo);

            multiAction.Perform();
        }
    }
}
