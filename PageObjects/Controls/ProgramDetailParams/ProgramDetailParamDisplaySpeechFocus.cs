using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;

namespace HorusUITest.PageObjects.Controls.ProgramDetailParams
{
    public class ProgramDetailParamDisplaySpeechFocus : ProgramDetailParamDisplayGeneral
    {
        public ProgramDetailParamDisplaySpeechFocus(ProgramDetailPage page, IMobileElement<AppiumWebElement> parent) : base(page, parent) { }

        /// <summary>
        /// Navigates to <see cref="ProgramDetailParamEditSpeechFocusPage"/>.
        /// </summary>
        public override void OpenSettings()
        {
            base.OpenSettings();
        }
    }
}
