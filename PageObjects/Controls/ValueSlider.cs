using System.Collections.Generic;
using HorusUITest.Extensions;
using HorusUITest.Helper;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Controls
{
    public class ValueSlider<T> : BaseFactoryControl
    {
        private const string SliderImageAID = "Horus.Views.Controls.ValueSlider.SliderImage";
        private const string ItemGridAID = "Horus.Views.Controls.ValueSlider.ItemGrid";
        private const string SelectedIndexAID = "Horus.Views.Controls.ValueSlider.SelectedIndex";
        private const string ItemAID = "Horus.Views.Controls.ValueSlider.Item";

        [FindsByAndroidUIAutomator(Accessibility = SelectedIndexAID), FindsByIOSUIAutomation(Accessibility = SelectedIndexAID)]
        private IMobileElement<AppiumWebElement> SelectedIndex { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = ItemAID), FindsByIOSUIAutomation(Accessibility = ItemAID)]
        private IList<AppiumWebElement> Items { get; set; }

        private T page;
        private int numberOfValues;

        public ValueSlider(T page, IMobileElement<AppiumWebElement> valueSliderElement, int numberOfValues) : base(valueSliderElement)
        {
            this.page = page;
            this.numberOfValues = numberOfValues;
        }

        private List<string> itemsCache;
        public string GetItem(int index)
        {
            itemsCache = itemsCache ?? CollectionHelper.CreateListOfNullElements<string>(numberOfValues);
            itemsCache[index] = itemsCache[index] ?? Items[index].Text;
            return itemsCache[index];
        }

        public int GetNumberOfItems()
        {
            return Items.Count;
        }

        public int GetSelectedIndex()
        {
            var newindex = SelectedIndex.Text.ToInt();
            return newindex;
        }

        public string GetSelectedItem()
        {
            return GetItem(GetSelectedIndex());
        }

        public T SelectItem(int index)
        {
            App.Tap(Items[index]);
            return page;
        }
        public ITouchAction SelectItemByTouchAction(int index)
        {
            ITouchAction touchAction = new TouchAction(App.Driver);
            return touchAction.Tap(Items[index]);
            
        }

    }
}
