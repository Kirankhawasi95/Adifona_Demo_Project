using System;

namespace HorusUITest.PageObjects.Interfaces
{
    public interface IHasLoadingIndicator<T>
    {
        /// <summary>
        /// Returns whether or not the loading indicator is visible.
        /// </summary>
        /// <returns></returns>
        bool GetIsLoadingIndicatorVisible();
        /// <summary>
        /// Waits for the circular activity indicator to disappear.
        /// </summary>
        /// <param name="timeout">The maximum time span for the loading indicator to disappear before a timeout exception is thrown.</param>
        /// <returns>The instance of <see cref="T"/>.</returns>
        T WaitUntilNoLoadingIndicator(TimeSpan? timeout = null);
    }
}
