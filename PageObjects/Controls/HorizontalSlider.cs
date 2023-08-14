using System;
using System.Drawing;
using HorusUITest.Extensions;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Controls
{
    public class HorizontalSlider<T> : BaseFactoryControl
    {
        private const string ContainingGridAID = "Horus.Views.Controls.HorizontalSlider.ContainingGrid";
        private const string SelectedBarAID = "Horus.Views.Controls.HorizontalSlider.ctrlBarSelected";
        private const string UnselectedBarAID = "Horus.Views.Controls.HorizontalSlider.ctrlBarUnselected";
        private const string ThumbAID = "Horus.Views.Controls.HorizontalSlider.ctrlThumb";

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = ContainingGridAID), FindsByIOSUIAutomation(Accessibility = ContainingGridAID)]
        private IMobileElement<AppiumWebElement> ContainingGrid { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = SelectedBarAID), FindsByIOSUIAutomation(Accessibility = SelectedBarAID)]
        private IMobileElement<AppiumWebElement> SelectedBar { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = UnselectedBarAID), FindsByIOSUIAutomation(Accessibility = UnselectedBarAID)]
        private IMobileElement<AppiumWebElement> UnselectedBar { get; set; }

        [FindsByAndroidUIAutomator(Accessibility = ThumbAID), FindsByIOSUIAutomation(Accessibility = ThumbAID)]
        private IMobileElement<AppiumWebElement> Thumb { get; set; }

        private T page;

        public HorizontalSlider(T page, IMobileElement<AppiumWebElement> sliderElement) : base(sliderElement)
        {
            this.page = page;
        }

        private int? gridX;
        private int GridX
        {
            get
            {
                gridX = gridX ?? ContainingGrid.Location.X;
                return gridX.Value;
            }
        }

        private int? gridWidth;
        private int GridWidth
        {
            get
            {
                gridWidth = gridWidth ?? ContainingGrid.Size.Width;
                return gridWidth.Value;
            }
        }

        private Size? thumbSize;
        private Size ThumbSize
        {
            get
            {
                thumbSize = thumbSize ?? Thumb.Size;
                return thumbSize.Value;
            }
        }

        public double GetRelativeValue()
        {
            var result = (Thumb.Location.X - GridX) / (double)(GridWidth - ThumbSize.Width);
            return Math.Truncate(result * 100) / 100;
        }

        private void SetRelativeValueOnAndroid(double value)
        {
            Point thumbLocation = Thumb.Location;
            double targetX = value * (GridWidth - ThumbSize.Width) + GridX + ThumbSize.Width * 0.5;

            double deltaX = targetX - (thumbLocation.X + ThumbSize.Width * 0.5);
            int adjustedDeltaX = (int)(deltaX * GridWidth / (GridWidth - ThumbSize.Width));
            int startX = thumbLocation.X + ThumbSize.Width / 2;
            int startY = thumbLocation.Y + ThumbSize.Height / 2;
            int msToPerform = (int)(Math.Abs(adjustedDeltaX) * 200f / gridWidth);  //normalizing swipe speed to 200ms per GridWidth
            msToPerform.Clamp(50, 200);
            App.SwipeToOffset(startX, startY, adjustedDeltaX, 0, useInertia: false, TimeSpan.FromMilliseconds(msToPerform));
        }

        private void SetRelativeValueOnIos(double value)
        {
            Point thumbLocation = Thumb.Location;
            double targetX = value * (GridWidth - ThumbSize.Width) + GridX + ThumbSize.Width * 0.5;

            double deltaX = targetX - (thumbLocation.X + ThumbSize.Width * 0.5);
            int adjustedDeltaX = (int)(deltaX * GridWidth / (GridWidth - ThumbSize.Width));

            int iosOffset = (int)Math.Round((14f / 414f) * App.ScreenSize.Width);     //"Magic Numbers": 14 -> necessary offset measured. 414 -> Screen width of the device the measurement was taken on.
            int threshold = (int)Math.Ceiling(GridWidth / 41f);                       //                 41 -> one less than double the step count of almost every horizontal slider (21)
            if (adjustedDeltaX > threshold)
                adjustedDeltaX += iosOffset;
            else if (adjustedDeltaX < -threshold)
                adjustedDeltaX -= iosOffset;

            int msToPerform = (int)(Math.Abs(adjustedDeltaX) * 600 / gridWidth);  //normalizing swipe speed to 600ms per GridWidth
            int startX = thumbLocation.X + ThumbSize.Width / 2;
            int startY = thumbLocation.Y + ThumbSize.Height / 2;
            msToPerform.Clamp(150, 600);
            App.SwipeToOffset(startX, startY, adjustedDeltaX, 0, useInertia: false, TimeSpan.FromMilliseconds(msToPerform));
        }

        public T SetRelativeValue(double value)
        {
            if (value < 0f || value > 1f)
                throw new ArgumentOutOfRangeException($"Target slider value must be within 0 and 1, but was {value}");

            if (OniOS)
                SetRelativeValueOnIos(value);
            else
                SetRelativeValueOnAndroid(value);
            return page;
        }

        public ITouchAction PressHorizontalVolumeSlider()
        {
            ITouchAction touchAction = new TouchAction(App.Driver);
            return touchAction.LongPress(Thumb);
        }
    }
}
