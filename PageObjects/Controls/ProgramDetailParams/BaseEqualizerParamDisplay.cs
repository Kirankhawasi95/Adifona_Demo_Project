using System;
using HorusUITest.Enums;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;

namespace HorusUITest.PageObjects.Controls.ProgramDetailParams
{
    public abstract class BaseEqualizerParamDisplay : ParamDisplay<ProgramDetailPage>
    {
        protected abstract VerticalSlider<ProgramDetailPage> LowSlider { get; }
        protected abstract VerticalSlider<ProgramDetailPage> MidSlider { get; }
        protected abstract VerticalSlider<ProgramDetailPage> HighSlider { get; }

        public BaseEqualizerParamDisplay(ProgramDetailPage page, IMobileElement<AppiumWebElement> parent) : base(page, parent) { }

        private VerticalSlider<ProgramDetailPage> GetSlider(EqBand band)
        {
            switch (band)
            {
                case EqBand.Low: return LowSlider;
                case EqBand.Mid: return MidSlider;
                case EqBand.High: return HighSlider;
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
        /// Sets the relative value of the EQ slider. Warning: As of yet, the app does not support changing parameters directly from the overview panel.
        /// </summary>
        /// <param name="band">Specifies the slider.</param>
        /// <param name="value">The relative value to be set. Must be within 0 (bottom) and 1 (top).</param>
        /// <returns>The instance of <see cref="ProgramDetailPage"/>.</returns>
        public ProgramDetailPage SetEqualizerSliderValue(EqBand band, double value)
        {
            return GetSlider(band).SetRelativeValue(value);
        }
    }
}
