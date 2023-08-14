using System;
using System.Collections.Generic;
using System.Linq;
using HorusUITest.Enums;
using HorusUITest.Extensions;
using HorusUITest.Helper;
using HorusUITest.PageObjects.Controls.Interfaces;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Controls
{
    public class MenuItems : BasePageObject, IMenuItems
    {
        private IReadOnlyCollection<AppiumWebElement> Items
        {
            get
            {
                if (scrollView != null)     //if menu items are within a ScrollView, visibility should be verified, as items may be outside of the screen
                    return App.FindElementsByAutomationId("Horus.Views.Controls.Menu.PageMenuButton.MenuItemText", verifyVisibility: false);
                else
                    return App.FindElementsByAutomationId("Horus.Views.Controls.Menu.PageMenuButton.MenuItemText", verifyVisibility: false);
            }
        }

        private IScrollViewService scrollView;

        public MenuItems(IScrollViewService scrollView = null)
        {
            this.scrollView = scrollView;
        }

        protected override void ClearCache()
        {
            base.ClearCache();
            itemsCache = null;
            visibleTextsCache = null;
            allTextsCache = null;
        }

        public void InvalidateCache()
        {
            ClearCache();
        }

        private IReadOnlyCollection<AppiumWebElement> itemsCache;
        private IReadOnlyCollection<AppiumWebElement> ItemsCache
        {
            get
            {
                itemsCache = itemsCache ?? Items;
                return itemsCache;
            }
        }

        private List<string> allTextsCache;
        private List<string> AllTextsCache
        {
            get
            {
                allTextsCache = allTextsCache ?? GetAllTexts();
                return allTextsCache;
            }
            set
            {
                allTextsCache = value;
            }
        }

        private List<string> GetAllTexts()
        {
            if (scrollView == null)
                return GetAllVisible();

            scrollView.ScrollToTop();
            var result = GetAllVisible();
            do
            {
                scrollView.ScrollDown(1);
                var newItems = GetAllVisible();
                foreach (var s in newItems)
                {
                    if (!result.Contains(s))
                        result.Add(s);
                }
            } while (!scrollView.GetIsScrolledToBottom());
            return result;
        }

        private List<string> visibleTextsCache;
        private string GetFromCache(int index, IndexType indexType)
        {
            if (indexType == IndexType.Relative)
            {
                visibleTextsCache = visibleTextsCache ?? CollectionHelper.CreateListOfNullElements<string>(ItemsCache.Count);
                visibleTextsCache[index] = visibleTextsCache[index] ?? ItemsCache.ElementAt(index).Text;
                return visibleTextsCache[index];
            }
            else
            {
                return AllTextsCache[index];
            }
        }

        public string Get(int index, IndexType indexType)
        {
            return GetFromCache(index, indexType);
        }

        public List<string> GetAllVisible()
        {
            var result = new List<string>();
            for (int i = 0; i < ItemsCache.Count; i++)
            {
                if (OnAndroid || (OniOS && ItemsCache.ElementAt(i).IsOnScreen()))
                {
                    result.Add(Get(i, IndexType.Relative));
                }
            }
            return result;
        }

        public List<string> GetAll()
        {
            return AllTextsCache;
        }

        public int CountAllVisible()
        {
            return ItemsCache.Count;
        }

        public int CountAll()
        {
            return AllTextsCache.Count;
        }

        public void Open(int index, IndexType indexType)
        {
            if (indexType == IndexType.Relative)
                App.Tap(ItemsCache.ElementAt(index));
            else
                throw new NotImplementedException(); //TODO: implement
            ClearCache();
        }
    }
}
