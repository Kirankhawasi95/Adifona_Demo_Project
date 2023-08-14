using HorusUITest.Extensions;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Controls.ProgramDetailParams
{
    public class ProgramDetailParamDisplayAuto : BaseFactoryControl
    {
        private const string DescriptionAID = "Horus.Views.Controls.ProgramDetailParams.ProgramDetailParamDisplayAuto.Description";

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = DescriptionAID), FindsByIOSUIAutomation(Accessibility = DescriptionAID)]
        private IMobileElement<AppiumWebElement> Description { get; set; }

        private ProgramDetailPage page;

        public ProgramDetailParamDisplayAuto(ProgramDetailPage page, IMobileElement<AppiumWebElement> parent) : base(parent)
        {
            this.page = page;
        }

        public string GetDescription()
        {
            return Description.GetTextOrNull();
        }
    }
}
