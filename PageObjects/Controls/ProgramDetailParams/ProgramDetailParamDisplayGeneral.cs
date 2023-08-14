using HorusUITest.Extensions;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Controls.ProgramDetailParams
{
    public class ProgramDetailParamDisplayGeneral : ParamDisplay<ProgramDetailPage>
    {
        private const string ValueAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamDisplayGeneral.Value";

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = ValueAID), FindsByIOSUIAutomation(Accessibility = ValueAID)]
        private IMobileElement<AppiumWebElement> Value { get; set; }

        public ProgramDetailParamDisplayGeneral(ProgramDetailPage page, IMobileElement<AppiumWebElement> parent) : base(page, parent) { }

        public string GetValue()
        {
            return Value.GetTextOrNull();
        }
    }
}
