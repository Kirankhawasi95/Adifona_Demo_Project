using System;
using System.Collections.Generic;
using System.Linq;
using HorusUITest.Enums;
using HorusUITest.Extensions;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Controls
{
    public class TemplateScrollView<T> : ScrollView<T>
    {
        private readonly Func<IReadOnlyCollection<AppiumWebElement>> itemsQuery;
        private readonly Func<AppiumWebElement, AppiumWebElement> topOfItemQuerey;
        private readonly Func<AppiumWebElement, AppiumWebElement> bottomOfItemQuery;

        public TemplateScrollView(T page, Func<AppiumWebElement> scrollViewQuery, Func<AppiumWebElement> topOfScrollViewQuery, Func<AppiumWebElement> bottomOfScrollViewQuery, Action clearParentCache, Func<IReadOnlyCollection<AppiumWebElement>> itemsQuery, Func<AppiumWebElement, AppiumWebElement> topOfItemQuerey = null, Func<AppiumWebElement, AppiumWebElement> bottomOfItemQuery = null)
            : base(page, scrollViewQuery, topOfScrollViewQuery, bottomOfScrollViewQuery, clearParentCache)
        {
            this.itemsQuery = itemsQuery;
            this.topOfItemQuerey = topOfItemQuerey;
            this.bottomOfItemQuery = bottomOfItemQuery;
        }

        protected override void ClearCache()
        {
            base.ClearCache();
            partialItemsCache = null;
            completeItemsCache = null;
        }

        public void InvalidateCache()
        {
            ClearCache();
        }

        public override T ScrollToTop(int maxNumberOfSwipes = int.MaxValue)
        {
            T page = base.ScrollToTop(maxNumberOfSwipes);
            ClearCache();
            return page;
        }

        public override T ScrollToBottom(int maxNumberOfSwipes = int.MaxValue)
        {
            T page = base.ScrollToBottom(maxNumberOfSwipes);
            ClearCache();
            return page;
        }

        public override T ScrollUp(double verticalPercentage)
        {
            T page = base.ScrollUp(verticalPercentage);
            ClearCache();
            return page;
        }

        public override T ScrollDown(double verticalPercentage)
        {
            T page = base.ScrollDown(verticalPercentage);
            ClearCache();
            return page;
        }

        private IReadOnlyCollection<AppiumWebElement> partialItemsCache;
        private IReadOnlyCollection<AppiumWebElement> PartialItemsCache
        {
            get
            {
                partialItemsCache = partialItemsCache ?? itemsQuery.Invoke();
                return partialItemsCache;
            }
        }

        private IReadOnlyCollection<AppiumWebElement> completeItemsCache;
        private IReadOnlyCollection<AppiumWebElement> CompleteItemsCache
        {
            get
            {
                completeItemsCache = completeItemsCache ?? GetCompleteItems();
                return completeItemsCache;
            }
        }

        private IReadOnlyCollection<AppiumWebElement> GetCompleteItems()
        {
            var result = new List<AppiumWebElement>();
            for (int i = 0; i < PartialItemsCache.Count; i++)
            {
                if (i == 0)
                {
                    if (topOfItemQuerey == null || PartialItemsCache.ElementAt(i).TryFindElement(topOfItemQuerey, out _))
                        result.Add(PartialItemsCache.ElementAt(i));
                }
                else if (i == PartialItemsCache.Count - 1)
                {
                    if (bottomOfItemQuery == null || PartialItemsCache.ElementAt(i).TryFindElement(bottomOfItemQuery, out _))
                        result.Add(PartialItemsCache.ElementAt(i));
                }
                else
                {
                    result.Add(PartialItemsCache.ElementAt(i));
                }
            }
            return result.AsReadOnly();
        }

        public IReadOnlyCollection<AppiumWebElement> GetCompleteItemsOnScreen()
        {
            return CompleteItemsCache;
        }

        public AppiumWebElement GetItem(int index, IndexType indexType)
        {
            if (indexType == IndexType.Relative)
                return CompleteItemsCache.ElementAt(index);
            else
                throw new NotImplementedException();
            //TODO: Low priority: Implement element access using an absolute index.
        }
    }
}
