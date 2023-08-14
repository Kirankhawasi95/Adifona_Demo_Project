using System;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Controls.Interfaces;
using HorusUITest.PageObjects.Interfaces;

namespace HorusUITest.PageObjects.Menu
{
    public abstract class BaseSubMenuPage : BaseNavigationPage, IHasMenuItems
    {
        protected MenuItems menuItems = new MenuItems();

        protected BaseSubMenuPage(bool assertOnPage) : base(assertOnPage)
        {
        }

        protected BaseSubMenuPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        public IMenuItems MenuItems => menuItems;

        protected override void ClearCache()
        {
            base.ClearCache();
            menuItems.InvalidateCache();
        }
    }
}
