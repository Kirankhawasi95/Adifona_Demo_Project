using System;
using System.Collections.Generic;
using HorusUITest.Extensions;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Interfaces;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Favorites
{
    public class ProgramIconPage : BaseProgramConfigPage
    {
        private const int columnCount = 6;

        protected override Func<IMobileElement<AppiumWebElement>> NavigationBarQuery => () => NavigationBar;
        protected override Func<IMobileElement<AppiumWebElement>> CancelButtonQuery => () => CancelButton;

        private const string NavigationBarAID = "Horus.Views.Favorites.ProgramIconPage.NavigationBar";
        private const string DescriptionAID = "Horus.Views.Favorites.ProgramIconPage.Description";
        private const string IconTitleAID = "Horus.Views.Favorites.ProgramIconPage.IconTitle";
        private const string IconAID = "Horus.Views.Controls.IconSelectionButton.Icon";
        private const string SelectedIndexAID = "Horus.Views.Favorites.ProgramIconPage.SelectedIndex";
        private const string CancelButtonAID = "Horus.Views.Favorites.ProgramIconPage.CancelButton";
        private const string ProceedButtonAID = "Horus.Views.Favorites.ProgramIconPage.ProceedButton";

        [FindsByAndroidUIAutomator(Accessibility = NavigationBarAID), FindsByIOSUIAutomation(Accessibility = NavigationBarAID)]
        private IMobileElement<AppiumWebElement> NavigationBar { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = DescriptionAID), FindsByIOSUIAutomation(Accessibility = DescriptionAID)]
        private IMobileElement<AppiumWebElement> Description { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = IconTitleAID), FindsByIOSUIAutomation(Accessibility = IconTitleAID)]
        private IMobileElement<AppiumWebElement> IconTitle { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = IconAID), FindsByIOSUIAutomation(Accessibility = IconAID)]
        private IList<AppiumWebElement> Icons { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = SelectedIndexAID), FindsByIOSUIAutomation(Accessibility = SelectedIndexAID)]
        private IMobileElement<AppiumWebElement> SelectedIndex { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = CancelButtonAID), FindsByIOSUIAutomation(Accessibility = CancelButtonAID)]
        private IMobileElement<AppiumWebElement> CancelButton { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = ProceedButtonAID), FindsByIOSUIAutomation(Accessibility = ProceedButtonAID)]
        private IMobileElement<AppiumWebElement> ProceedButton { get; set; }

        public ProgramIconPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public ProgramIconPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        public string GetDescription()
        {
            return Description.Text;
        }

        public string GetIconTitle()
        {
            return IconTitle.Text;
        }

        /// <summary>
        /// Taps the button 'Next' or 'Accept' respectively, depending on the current workflow.
        /// 'Next' navigates to <see cref="ProgramAutomationPage"/>. 'Accept' navigates to <see cref="ProgramDetailSettingsControlPage"/>.
        /// </summary>
        public void Proceed()
        {
            App.Tap(ProceedButton);
        }

        public string GetProceedButtonText()
        {
            return ProceedButton.Text;
        }

        private int CoordsToIndex(int column, int row)
        {
            return row * columnCount + column;
        }

        private void IndexToCoords(int index, out int column, out int row)
        {
            row = index / columnCount;
            column = index % columnCount;
        }

        public int GetNumberOfIcons()
        {
            return Icons.Count;
        }

        public int GetSelectedIconIndex()
        {
            return SelectedIndex.Text.ToInt();
        }

        public ProgramIconPage GetSelectedIconCoords(out int column, out int row)
        {
            IndexToCoords(GetSelectedIconIndex(), out column, out row);
            return this;
        }

        public string GetSelectedIconText()
        {
            return GetIconText(GetSelectedIconIndex());
        }

        public ProgramIconPage SelectIcon(int index)
        {
            App.Tap(Icons[index]);
            return this;
        }

        public ProgramIconPage SelectIcon(int column, int row)
        {
            return SelectIcon(CoordsToIndex(column, row));
        }

        public ProgramIconPage SelectIcon(string iconText)
        {
            int numberOfItems = GetNumberOfIcons();
            for (int i = 0; i < numberOfItems; i++)
            {
                if(GetIconText(i) == iconText)
                {
                    SelectIcon(i);
                    return this;
                }
            }
            throw new ArgumentOutOfRangeException($"{nameof(iconText)}: {iconText}");
        }

        public string GetIconText(int index)
        {
            return Icons[index].Text;
        }

        public string GetIconText(int column, int row)
        {
            return GetIconText(CoordsToIndex(column, row));
        }
    }
}
