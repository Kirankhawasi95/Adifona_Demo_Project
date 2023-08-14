using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;

namespace HorusUITest.PageObjects.Controls.ProgramDetailParams
{
    public class ProgramDetailParamDisplayNoiseReduction : ProgramDetailParamDisplayGeneral
    {
        public ProgramDetailParamDisplayNoiseReduction(ProgramDetailPage page, IMobileElement<AppiumWebElement> parent) : base(page, parent) { }

        /// <summary>
        /// Navigates to <see cref="ProgramDetailParamEditNoiseReductionPage"/>.
        /// </summary>
        public override void OpenSettings()
        {
            base.OpenSettings();
        }
    }
}
