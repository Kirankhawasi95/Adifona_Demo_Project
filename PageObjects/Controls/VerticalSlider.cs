using System;
using System.Drawing;
using HorusUITest.Extensions;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Appium.MultiTouch;


namespace HorusUITest.PageObjects.Controls
{
    public class VerticalSlider<T> : BaseFactoryControl
    {
        //TODO: Consider making this class non-generic

        private const string ContainingGridAID = "Horus.Views.Controls.VerticalSlider.ContainingGrid";
        private const string BarAID = "Horus.Views.Controls.VerticalSlider.ctrlBar";
        private const string ThumbAID = "Horus.Views.Controls.VerticalSlider.ctrlThumb";

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = ContainingGridAID), FindsByIOSUIAutomation(Accessibility = ContainingGridAID)]
        private IMobileElement<AppiumWebElement> ContainingGrid { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = BarAID), FindsByIOSUIAutomation(Accessibility = BarAID)]
        private IMobileElement<AppiumWebElement> Bar { get; set; }

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = ThumbAID), FindsByIOSUIAutomation(Accessibility = ThumbAID)]
        private IMobileElement<AppiumWebElement> Thumb { get; set; }

        private T page;

        public VerticalSlider(T page, IMobileElement<AppiumWebElement> sliderElement) : base(sliderElement)
        {
            this.page = page;
        }

        private int? gridY;
        private int GridY
        {
            get
            {
                gridY = gridY ?? ContainingGrid.Location.Y;
                return gridY.Value;
            }
        }

        private int? gridHeight;
        private int GridHeight
        {
            get
            {
                gridHeight = gridHeight ?? ContainingGrid.Size.Height;
                return gridHeight.Value;
            }
        }

        private int? thumbWidth;
        private int ThumbWidth
        {
            get
            {
                thumbWidth = thumbWidth ?? Thumb.Size.Width;
                return thumbWidth.Value;
            }
            set
            {
                thumbWidth = value;
            }
        }

        private bool normalThumbSizeIsAccurate = false;
        private int? normalThumbHeight = null;
        private int NormalThumbHeight
        {
            get
            {
                if (normalThumbHeight == null)
                {
                    NormalThumbHeight = Thumb.Size.Height;
                }
                return normalThumbHeight.Value;
            }
            set
            {
                if (normalThumbSizeIsAccurate) return;
                if (normalThumbHeight == null)
                {
                    if ((double)value / ThumbWidth < 0.5)
                        value = (int)Math.Round(ThumbWidth * 0.523);
                    else
                        normalThumbSizeIsAccurate = true;
                    normalThumbHeight = value;
                }
                else if (value >= normalThumbHeight)
                {
                    normalThumbHeight = value;
                    if ((double)value / ThumbWidth >= 0.5)
                        normalThumbSizeIsAccurate = true;
                }
            }
        }

        private double GetThumbCenterY(Rectangle thumbRect)
        {
            double coarseLocation = (thumbRect.GetCenter().Y - GridY) / (double)GridHeight;
            if (coarseLocation > 0.5)
                return thumbRect.Top + 0.5 * NormalThumbHeight;
            else
                return thumbRect.Bottom - 0.5 * NormalThumbHeight;
        }

        public double GetRelativeValue()
        {
            var thumbRect = Thumb.GetRect();
            ThumbWidth = thumbRect.Size.Width;
            NormalThumbHeight = thumbRect.Size.Height;
            double result = (GetThumbCenterY(thumbRect) - GridY) / GridHeight;
            return 1 - Math.Truncate(result * 100) / 100;
        }

        private void SetRelativeValueOnAndroid(double value)
        {
            Point thumbLocation = Thumb.Location;
            Size thumbSize = Thumb.Size;
            double targetY = (1 - value) * GridHeight + GridY;
            int deltaY = ((int)(targetY - GetThumbCenterY(new Rectangle(thumbLocation, thumbSize))) - thumbSize.Height / 2);
            int startX = thumbLocation.X + thumbSize.Width / 2;
            int startY = thumbLocation.Y + thumbSize.Height / 2;
            int msToPerform = (int)(Math.Abs(deltaY) * 200f / GridHeight);  //normalizing swipe speed to 200ms per GridHeight
            msToPerform.Clamp(50, 200);
            App.SwipeToOffset(startX, startY, 0, deltaY, useInertia: false, TimeSpan.FromMilliseconds(msToPerform));
        }

        private void SetRelativeValueOnIos(double value)
        {
            Point thumbLocation = Thumb.Location;
            Size thumbSize = Thumb.Size;
            double targetY = (1 - value) * GridHeight + GridY;
            int deltaY = (int)(targetY - GetThumbCenterY(new Rectangle(thumbLocation, thumbSize)));

            int iosOffset = (int)Math.Round((14f / 414f) * App.ScreenSize.Width);     //"Magic Numbers":  14 -> necessary offset measured. 414 -> Screen width of the device the measurement was taken on.
            int threshold = (int)Math.Ceiling(GridHeight / 25f);                      //                  25 -> one less than double the step count of every vertical slider in the app (13)
            if (deltaY > threshold)
                deltaY += iosOffset;
            else if (deltaY < -threshold)
                deltaY -= iosOffset;

            int startX = thumbLocation.X + thumbSize.Width / 2;
            int startY = thumbLocation.Y + thumbSize.Height / 2;
            startY.Clamp(GridY + 1, GridY + GridHeight - 1);
            int msToPerform = (int)(Math.Abs(deltaY) * 600f / GridHeight);  //normalizing swipe speed to 200ms per GridHeight
            msToPerform.Clamp(150, 600);
            App.SwipeToOffset(startX, startY, 0, deltaY, useInertia: false, TimeSpan.FromMilliseconds(msToPerform));
        }

        public T SetRelativeValue(double value)
        {
            if (value < 0f || value > 1f)
                throw new ArgumentOutOfRangeException($"Target slider value must be within 0 and 1, but was {value}");

            if (OniOS)
                SetRelativeValueOnIos(value);
            else
                //SetRelativeValueOnAndroid(value);
                MoveVerticalSlider(value);
            return page;
        }
        /// <summary>
        /// By SetRelativeValueOnAndroid the slider is not moving properly
        /// hence using TouchActions instead
        /// Also the slider movement dosent seem very smooth when tried manually
        /// </summary>
        /// <param name="value"></param>
        public void MoveVerticalSlider(double value)
        {
            TouchAction touch = new TouchAction(AppManager.androidDriver);
            double startX = Thumb.Location.X + Thumb.Size.Width / 2;
            double startY = Thumb.Location.Y + Thumb.Size.Height / 2;
            double targetY = (1 - value) * GridHeight + GridY;
            touch.LongPress(startX, startY).MoveTo(startX, targetY).Release().Perform();
        }

    }
}
