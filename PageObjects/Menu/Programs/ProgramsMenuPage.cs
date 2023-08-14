using System;
using System.Collections.Generic;
using System.Linq;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Controls.Menu;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu.Programs
{
    public class ProgramsMenuPage : BaseSubMenuPage
    {
        public ProgramsMenuPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public ProgramsMenuPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Menu.Programms.ProgrammsMenuPage.NavigationBar");

        private IReadOnlyCollection<AppiumWebElement> MainProgramsAndroid => App.FindElementsByAutomationId("Horus.Views.Menu.Programms.ProgrammsMenuPage.MainProgramButton");
        private IReadOnlyCollection<AppiumWebElement> StreamingProgramsAndroid => App.FindElementsByAutomationId("Horus.Views.Menu.Programms.ProgrammsMenuPage.StreamingProgramButton");
        private IReadOnlyCollection<AppiumWebElement> FavoriteProgramsAndroid => App.FindElementsByAutomationId("Horus.Views.Menu.Programms.ProgrammsMenuPage.FavoriteProgramButton");

        private IReadOnlyCollection<AppiumWebElement> MainProgramsIos => App.FindElementsByAutomationId("Horus.Views.Menu.Programms.ProgrammsMenuPage.MainProgramViewCell");
        private IReadOnlyCollection<AppiumWebElement> StreamingProgramsIos => App.FindElementsByAutomationId("Horus.Views.Menu.Programms.ProgrammsMenuPage.StreamingProgramViewCell");
        private IReadOnlyCollection<AppiumWebElement> FavoriteProgramsIos => App.FindElementsByAutomationId("Horus.Views.Menu.Programms.ProgrammsMenuPage.FavoriteProgramViewCell");

        private IReadOnlyCollection<AppiumWebElement> MainPrograms => OnAndroid ? MainProgramsAndroid : MainProgramsIos;
        private IReadOnlyCollection<AppiumWebElement> StreamingPrograms => OnAndroid ? StreamingProgramsAndroid : StreamingProgramsIos;
        private IReadOnlyCollection<AppiumWebElement> FavoritePrograms => OnAndroid ? FavoriteProgramsAndroid : FavoriteProgramsIos;

        MenuItems programs = new MenuItems();

        public int GetNumberOfPrograms()
        {
            return programs.CountAll();
        }

        public int GetNumberOfMainPrograms()
        {
            return MainPrograms.Count();
        }

        public int GetNumberOfStreamingPrograms()
        {
            return StreamingPrograms.Count();
        }

        public int GetNumberOfFavoritePrograms()
        {
            return FavoritePrograms.Count();
        }

        public void SelectProgram(int index)
        {
            programs.Open(index, Enums.IndexType.Relative);
        }

        public void SelectMainProgram(int index)
        {
            App.Tap(MainPrograms.ElementAt(index));
        }

        public void SelectStreamingProgram(int index)
        {
            App.Tap(StreamingPrograms.ElementAt(index));
        }

        public void SelectFavoriteProgram(int index)
        {
            App.Tap(FavoritePrograms.ElementAt(index));
        }

        public string GetProgramName(int index)
        {
            return programs.Get(index, Enums.IndexType.Relative);
        }

        public string GetMainProgramName(int index)
        {
            return PageMenuButton.GetLabelOf(MainPrograms.ElementAt(index)).Text;
        }

        public string GetStreamingProgramName(int index)
        {
            return PageMenuButton.GetLabelOf(StreamingPrograms.ElementAt(index)).Text;
        }

        public string GetFavoriteProgramName(int index)
        {
            return PageMenuButton.GetLabelOf(FavoritePrograms.ElementAt(index)).Text;
        }

        public List<string> GetAllProgramNames()
        {
            return programs.GetAll();
        }
    }
}
