using System;
using HorusUITest.Enums;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;

namespace HorusUITest.PageObjects.Controls.ProgramDetailParams
{
    public abstract class BaseEqualizerParamEdit<T> : BaseParamEdit<BaseEqualizerParamEdit<T>>
    {
        protected abstract VerticalSlider<T> LowSlider { get; }
        protected abstract VerticalSlider<T> MidSlider { get; }
        protected abstract VerticalSlider<T> HighSlider { get; }
        protected abstract Func<IMobileElement<AppiumWebElement>> LowTitleQuery { get; }
        protected abstract Func<IMobileElement<AppiumWebElement>> MidTitleQuery { get; }
        protected abstract Func<IMobileElement<AppiumWebElement>> HighTitleQuery { get; }

        public BaseEqualizerParamEdit(bool assertOnPage) : base(assertOnPage)
        {
        }

        public BaseEqualizerParamEdit(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        private VerticalSlider<T> GetSlider(EqBand band)
        {
            switch (band)
            {
                case EqBand.Low: return LowSlider;
                case EqBand.Mid: return MidSlider;
                case EqBand.High: return HighSlider;
                default: throw new NotImplementedException("Unknown Frequency.");
            }
        }

        private IMobileElement<AppiumWebElement> GetSliderTitle(EqBand band)
        {
            switch (band)
            {
                case EqBand.Low: return LowTitleQuery.Invoke();
                case EqBand.Mid: return MidTitleQuery.Invoke();
                case EqBand.High: return HighTitleQuery.Invoke();
                default: throw new NotImplementedException("Unknown Frequency.");
            }
        }

        /// <summary>
        /// Determines the relative value of the EQ slider by comparing the position of the thumb with the length of the bar.
        /// </summary>
        /// <param name="band">Specifies the slider.</param>
        /// <returns>A value between 0 (bottom) and 1 (top).</returns>
        public double GetEqualizerSliderValue(EqBand band)
        {
            return GetSlider(band).GetRelativeValue();
        }

        /// <summary>
        /// Sets the relative value of the EQ slider.
        /// </summary>
        /// <param name="band">Specifies the slider.</param>
        /// <param name="value">The relative value to be set. Must be within 0 (bottom) and 1 (top).</param>
        /// <returns>The instance of of the page.</returns>
        public T SetEqualizerSliderValue(EqBand band, double value)
        {
            return GetSlider(band).SetRelativeValue(value);
        }

        /// <summary>
        /// Returns the descriptive text of a slider.
        /// </summary>
        /// <param name="band">Specifies the slider.</param>
        /// <returns>The slider's title.</returns>
        public string GetEqualizerSliderTitle(EqBand band)
        {
            return GetSliderTitle(band).Text;
        }
    }
}
