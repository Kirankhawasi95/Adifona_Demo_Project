using System;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu.Help.InstructionsForUse
{
    public class InstructionsForUsePage : BaseNavigationPage
    {
        public InstructionsForUsePage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public InstructionsForUsePage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Menu.Help.InstructionsForUse.InstructionsForUsePage.NavigationBar");
    }

    //TODO: Flesh out this page, once it's implemented.
}
