using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;

namespace HorusUITest.PageObjects.Controls.ProgramDetailParams
{
    public class ProgramDetailParamDisplayTinnitus : ProgramDetailParamDisplayGeneral
    {
        public ProgramDetailParamDisplayTinnitus(ProgramDetailPage page, IMobileElement<AppiumWebElement> parent) : base(page, parent) { }

        /// <summary>
        /// Navigates to <see cref="ProgramDetailParamEditTinnitusPage"/>.
        /// </summary>
        public override void OpenSettings()
        {
            base.OpenSettings();
        }
    }
}
