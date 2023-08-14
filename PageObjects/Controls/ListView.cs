using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using HorusUITest.Enums;
using HorusUITest.Extensions;
using NUnit.Framework;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Controls
{
    public class ListView : BasePageObject
    {
        const int maxSwipeCount = 20;

        private readonly Func<AppiumWebElement> listViewQuery;
        private readonly Func<IReadOnlyCollection<AppiumWebElement>> itemsQuery;
        private readonly Func<AppiumWebElement, AppiumWebElement>[] identifierQueries;
        private readonly Func<AppiumWebElement, AppiumWebElement> topMarkerQuery;
        private readonly Func<AppiumWebElement, AppiumWebElement> bottomMarkerQuery;

        private IReadOnlyCollection<AppiumWebElement> Items => itemsQuery.Invoke();
        private AppiumWebElement ListViewElement => listViewQuery.Invoke();

        private readonly int idCount;

        public ListView(Func<AppiumWebElement> listViewQuery, Func<IReadOnlyCollection<AppiumWebElement>> itemsQuery, Func<AppiumWebElement, AppiumWebElement> topMarkerQuery, Func<AppiumWebElement, AppiumWebElement> bottomMarkerQuery, params Func<AppiumWebElement, AppiumWebElement>[] identifierQueries)
        {
            if (identifierQueries == null) throw new ArgumentNullException("At least one query to an identifying element must be given.");
            idCount = identifierQueries.Count();
            if (idCount < 1) throw new ArgumentException("At least one query to an identifying element must be given.");

            this.listViewQuery = listViewQuery;
            this.itemsQuery = itemsQuery;
            this.identifierQueries = identifierQueries;
            this.topMarkerQuery = topMarkerQuery;
            this.bottomMarkerQuery = bottomMarkerQuery;
        }

        //HACK WARNING: This static member is likely to cause issues when testing on multiple devices during the same test run, as slop is a device-specific parameter.
        //Symptoms may include: Over- or undershooting swipes that were meant to be precise. As a result of that: accessing wrong elements.
        //To resolve this, slop may become a non-static member (at the cost of increased test execution runtime).
        //Further information regarding slop: https://developer.android.com/reference/android/view/ViewConfiguration.html#getScaledTouchSlop%28%29
        private static int? slop;
        private int Slop
        {
            get
            {
                slop = slop ?? MeasureSlop();
                return slop.Value;
            }
        }

        private AppiumWebElement GetIdElement(int index)
        {
            return ListViewElement.FindElementDontThrow(identifierQueries[index]);
        }

        private string[] GetIds()
        {
            string[] result = new string[idCount];
            for (int i = 0; i < idCount; i++)
            {
                result[i] = GetIdElement(i).GetTextOrNull();
            }
            return result;
        }

        private bool IdsMatch(string[] one, string[] two)
        {
            if (one == null || two == null) throw new ArgumentException("Uninitialized array.");
            if (one.Count() != idCount) throw new ArgumentException("Unexpected array size.");
            if (two.Count() != idCount) throw new ArgumentException("Unexpected array size.");

            for (int i = 0; i < idCount; i++)
            {
                if (one[i] == null) return false;
                if (one[i] != two[i]) return false;
            }

            return true;
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

        private int? itemHeight;
        private int ItemHeight
        {
            get
            {
                itemHeight = itemHeight ?? DetectItemHeight();
                return itemHeight.Value;
            }
            set
            {
                itemHeight = value;
            }
        }

        private int? itemDistance;
        private int ItemDistance
        {
            get
            {
                itemDistance = itemDistance ?? DetectItemDistance();
                return itemDistance.Value;
            }
            set
            {
                itemDistance = value;
            }
        }

        private bool cacheEnabled = true;
        public bool CacheEnabled
        {
            get => cacheEnabled;
            set
            {
                ClearCache();
                cacheEnabled = value;
            }
        }

        private IReadOnlyCollection<AppiumWebElement> partialItemsCache;
        private IReadOnlyCollection<AppiumWebElement> PartialItemsCache
        {
            get
            {
                if (!CacheEnabled)
                    return Items;
                partialItemsCache = partialItemsCache ?? Items;
                return partialItemsCache;
            }
        }

        private IReadOnlyCollection<AppiumWebElement> completeItemsCache;
        private IReadOnlyCollection<AppiumWebElement> CompleteItemsCache
        {
            get
            {
                if (!CacheEnabled)
                    return GetCompleteItems();
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
                    if (topMarkerQuery == null)
                        result.Add(PartialItemsCache.ElementAt(i));
                    else if (PartialItemsCache.ElementAt(i).TryFindElement(topMarkerQuery, out _))
                        result.Add(PartialItemsCache.ElementAt(i));
                }
                else if (i == PartialItemsCache.Count - 1)
                {
                    if (bottomMarkerQuery == null)
                        result.Add(PartialItemsCache.ElementAt(i));
                    else if (PartialItemsCache.ElementAt(i).TryFindElement(bottomMarkerQuery, out _))
                        result.Add(PartialItemsCache.ElementAt(i));
                }
                else
                {
                    result.Add(PartialItemsCache.ElementAt(i));
                }
            }
            return result.AsReadOnly();
        }

        private int? DetectItemHeight()
        {
            if (PartialItemsCache.Count < 1) return null;
            int result = 0;
            foreach (var m in PartialItemsCache)
            {
                result = Math.Max(result, m.Size.Height);
            }
            return result;
        }

        private int? DetectItemDistance()
        {
            if (PartialItemsCache.Count < 2) return null;
            Rectangle upperRect = PartialItemsCache.ElementAt(0).Rect;
            Rectangle lowerRect = PartialItemsCache.ElementAt(1).Rect;
            int heightOfLargerRect = Math.Max(upperRect.Height, lowerRect.Height);
            Assert.AreEqual(ItemHeight, heightOfLargerRect, "Assertion failed in DetectItemDistance. Size of the larger item frame didn't match the expected item height.");
            int distanceTopToTop = lowerRect.Top - upperRect.Top;
            int distanceBottomToBottom = lowerRect.Bottom - upperRect.Bottom;
            int result = Math.Max(distanceTopToTop, distanceBottomToBottom);
            return result;
        }

        /// <summary>
        /// Measures the device-specific slop by swiping a fixed distance and comparing a visual element's expected position with the actual position.
        /// Slop must be accounted for when swiping with pixel-precision is required.
        /// </summary>
        /// <returns>The slop distance in number of pixels.</returns>
        private int MeasureSlop()
        {
            var rectBeforeSwipe = PartialItemsCache.ElementAt(0).Rect;
            App.SwipeRelativeToElementSizePixelPerfect(ListViewElement, .5, .5, 0, -100);
            var rectAfterSwipe = PartialItemsCache.ElementAt(0).Rect;
            int actualDistance = rectBeforeSwipe.Bottom - rectAfterSwipe.Bottom;
            int slop = 100 - actualDistance;
            App.SwipeRelativeToElementSizePixelPerfect(ListViewElement, .5, .5, 0, 100);
            return slop;
        }

        public void WaitForItems(TimeSpan timeout)
        {
            App.WaitForElements(() => PartialItemsCache, timeout);
        }

        public AppiumWebElement GetItem(int index, IndexType indexType)
        {
            if (indexType == IndexType.Relative)
                return CompleteItemsCache.ElementAt(index);
            else throw new NotImplementedException("Item access using absolute indices is not yet implemented.");
        }

        public int GetNumberOfPartialItemsOnScreen()
        {
            return PartialItemsCache.Count;
        }

        public int GetNumberOfItemsOnScreen()
        {
            return CompleteItemsCache.Count;
        }

        public IReadOnlyCollection<AppiumWebElement> GetAllItemsOnScreen()
        {
            return CompleteItemsCache;
        }

        public IReadOnlyCollection<AppiumWebElement> GetAllPartialItemsOnScreen()
        {
            return PartialItemsCache;
        }

        public void ScrollUp(int numberOfItems)
        {
            //TODO: Scroll multiple items at once (if possible).
            if (PartialItemsCache.Count < 2) return;
            for (int i = 0; i < numberOfItems; i++)
            {
                App.SwipeRelativeToElementSizePixelPerfect(ListViewElement, .5, 0, deltaX: 0, deltaY: (ItemDistance + Slop), safetyGap: 1);
                ClearCache();
            }
        }

        public void ScrollDown(int numberOfItems)
        {
            //TODO: Scroll multiple items at once (if possible).
            if (PartialItemsCache.Count < 2) return;
            for (int i = 0; i < numberOfItems; i++)
            {
                App.SwipeRelativeToElementSizePixelPerfect(ListViewElement, .5, 1, deltaX: 0, deltaY: -(ItemDistance + Slop), safetyGap: 1);
                ClearCache();
            }
        }

        public void ScrollToTop()
        {
            if (PartialItemsCache.Count == 0)
                return;     //No items -> no need to scroll.

            var lastIds = GetIds();
            var lastPos = GetIdElement(0).Location.X;
            int swipeCounter = 0;
            while (swipeCounter <= maxSwipeCount)
            {
                App.SwipeRelativeToElementSize(ListViewElement, .5, 0, .5, 1, safetyGap: 1);
                ClearCache();
                swipeCounter++;

                var newIds = GetIds();
                var newPos = GetIdElement(0).Location.X;

                if (IdsMatch(lastIds, newIds) && lastPos == newPos)
                    break;

                lastIds = newIds;
                lastPos = newPos;
            }
            Assert.LessOrEqual(swipeCounter, maxSwipeCount, "The number of swipes exceeded the permitted maximum while scrolling to the top.");
        }
        
        public void ScrollToBottom()
        {
            if (PartialItemsCache.Count == 0)
                return;     //No items -> no need to scroll.

            var lastIds = GetIds();
            var lastPos = GetIdElement(0).Location.X;
            int swipeCounter = 0;
            while (swipeCounter <= maxSwipeCount)
            {
                App.SwipeRelativeToElementSize(ListViewElement, .5, 1, .5, 0, safetyGap: 1);
                ClearCache();
                swipeCounter++;

                var newIds = GetIds();
                var newPos = GetIdElement(0).Location.X;

                if (IdsMatch(lastIds, newIds) && lastPos == newPos)
                    break;

                lastIds = newIds;
                lastPos = newPos;
            }
            Assert.LessOrEqual(swipeCounter, maxSwipeCount, "The number of swipes exceeded the permitted maximum while scrolling to the bottom.");
        }
    }
}
