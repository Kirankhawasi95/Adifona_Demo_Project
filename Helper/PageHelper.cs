using System;
using System.Diagnostics;
using HorusUITest.Configuration;
using HorusUITest.PageObjects;

namespace HorusUITest.Helper
{
    public static class PageHelper
    {
        private static string GetNames(Type[] pageTypes)
        {
            string names = string.Empty;
            foreach (var t in pageTypes)
            {
                names = names + Environment.NewLine + "   " + t.ToString();
            }
            return names;
        }

        private static string GetNames(BasePage[] pages)
        {
            string names = string.Empty;
            foreach (var t in pages)
            {
                names = names + Environment.NewLine + "   " + t.GetType().ToString();
            }
            return names;
        }

        private static BasePage WaitForAnyPage(TimeSpan timeout, Stopwatch watch, params BasePage[] pages)
        {
            int index = 0;
            while (watch.Elapsed < timeout)
            {
                if (pages[index].IsCurrentlyShown())
                    return pages[index];
                index = (index + 1) % pages.Length;
            }

            throw new TimeoutException("Timeout while waiting for any of the following pages:" + GetNames(pages));
        }

        /// <summary>
        /// Returns the current page by cyclicly checking the presence of every potential page's respective trait and selecting the first match.
        /// Each page is guaranteed to be checked at least once, regardless of the timeout, unless the current page was found.
        /// Throws a <see cref="TimeoutException"/> if no page was found within the given amount of time.
        /// </summary>
        /// <param name="timeout">Determines how long the search is allowed to take.</param>
        /// <param name="pageTypes">The types of potential pages to be expected.</param>
        /// <returns>Returns an instance to the current page.</returns>
        public static BasePage WaitForAnyPage(TimeSpan timeout, params Type[] pageTypes)
        {
            if (pageTypes.Length < 1) throw new ArgumentException("At least one page type must be given.");
            Debug.WriteLine("Waiting for any of the following pages:" + GetNames(pageTypes));

            Stopwatch watch = new Stopwatch();          //Creating instances and checking every page once before potentially timing out
            watch.Start();
            BasePage[] pages = new BasePage[pageTypes.Length];
            for (int i = 0; i < pageTypes.Length; i++)
            {
                pages[i] = CreateInstance(pageTypes[i], false);
                if (pages[i].IsCurrentlyShown())
                    return pages[i];
            }

            return WaitForAnyPage(timeout, watch, pages);
        }

        /// <summary>
        /// Returns the current page by cyclicly checking the presence of every potential page's respective trait and selecting the first match.
        /// Each page is guaranteed to be checked at least once, regardless of the timeout, unless the current page was found.
        /// Throws a <see cref="TimeoutException"/> if no page was found within the given amount of time.
        /// </summary>
        /// <param name="timeout">Determines how long the search is allowed to take.</param>
        /// <param name="pages">Instances of potential pages to be expected.</param>
        /// <returns>Returns an instance to the current page.</returns>
        public static BasePage WaitForAnyPage(TimeSpan timeout, params BasePage[] pages)
        {
            if (pages.Length < 1) throw new ArgumentException("At least one page type must be given.");
            Debug.WriteLine("Waiting for any of the following pages:" + GetNames(pages));

            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < pages.Length; i++)      //Checking every page once before potentially timing out
            {
                if (pages[i].IsCurrentlyShown())
                    return pages[i];
            }

            return WaitForAnyPage(timeout, watch, pages);
        }

        /// <summary>
        /// Returns the current page by cyclicly checking the presence of every potential page's respective trait and selecting the first match.
        /// Each page is guaranteed to be checked at least once, regardless of the timeout, unless the current page was found.
        /// Throws a <see cref="TimeoutException"/> if no page was found within the given amount of time.
        /// The timeout is set to <see cref="Env.DEFAULT_EXPLICIT_TIMEOUT"/>.
        /// </summary>
        /// <param name="pageTypes">The types of potential pages to be expected.</param>
        /// <returns>Returns an instance to the current page.</returns>
        public static BasePage WaitForAnyPage(params Type[] pageTypes)
        {
            return WaitForAnyPage(Env.DEFAULT_EXPLICIT_TIMEOUT, pageTypes);
        }

        /// <summary>
        /// Returns the current page by cyclicly checking the presence of every potential page's respective trait and selecting the first match.
        /// Each page is guaranteed to be checked at least once, regardless of the timeout, unless the current page was found.
        /// Throws a <see cref="TimeoutException"/> if no page was found within the given amount of time.
        /// The timeout is set to <see cref="Env.DEFAULT_EXPLICIT_TIMEOUT"/>.
        /// </summary>
        /// <param name="pages">Instances of potential pages to be expected.</param>
        /// <returns>Returns an instance to the current page.</returns>
        public static BasePage WaitForAnyPage(params BasePage[] pages)
        {
            return WaitForAnyPage(Env.DEFAULT_EXPLICIT_TIMEOUT, pages);
        }

        /// <summary>
        /// Returns the current page by cyclicly checking the presence of every potential page's respective trait and selecting the first match.
        /// Each page is guaranteed to be checked at least once, regardless of the timeout, unless the current page was found.
        /// </summary>
        /// <param name="timeout">Determines how long the search is allowed to take.</param>
        /// <param name="pageTypes">The types of potential pages to be expected.</param>
        /// <returns>Returns an instance to the current page or <see cref="null"/> if none was found before the timout.</returns>
        public static BasePage WaitForAnyPageDontThrow(TimeSpan timeout, params Type[] pageTypes)
        {
            try
            {
                return WaitForAnyPage(timeout, pageTypes);
            }
            catch (TimeoutException)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the current page by cyclicly checking the presence of every potential page's respective trait and selecting the first match.
        /// Each page is guaranteed to be checked at least once, regardless of the timeout, unless the current page was found.
        /// </summary>
        /// <param name="timeout">Determines how long the search is allowed to take.</param>
        /// <param name="pages">Instances of potential pages to be expected.</param>
        /// <returns>Returns an instance to the current page or <see cref="null"/> if none was found before the timout.</returns>
        public static BasePage WaitForAnyPageDontThrow(TimeSpan timeout, params BasePage[] pages)
        {
            try
            {
                return WaitForAnyPage(timeout, pages);
            }
            catch (TimeoutException)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the current page by cyclicly checking the presence of every potential page's respective trait and selecting the first match.
        /// Each page is guaranteed to be checked at least once, regardless of the timeout, unless the current page was found.
        /// The timeout is set to <see cref="Env.DEFAULT_EXPLICIT_TIMEOUT"/>.
        /// </summary>
        /// <param name="pageTypes">The types of potential pages to be expected.</param>
        /// <returns>Returns an instance to the current page or <see cref="null"/> if none was found before the timout.</returns>
        public static BasePage WaitForAnyPageDontThrow(params Type[] pageTypes)
        {
            return WaitForAnyPageDontThrow(Env.DEFAULT_EXPLICIT_TIMEOUT, pageTypes);
        }

        /// <summary>
        /// Returns the current page by cyclicly checking the presence of every potential page's respective trait and selecting the first match.
        /// Each page is guaranteed to be checked at least once, regardless of the timeout, unless the current page was found.
        /// The timeout is set to <see cref="Env.DEFAULT_EXPLICIT_TIMEOUT"/>.
        /// </summary>
        /// <param name="pages">Instances of potential pages to be expected.</param>
        /// <returns>Returns an instance to the current page or <see cref="null"/> if none was found before the timout.</returns>
        public static BasePage WaitForAnyPageDontThrow(params BasePage[] pages)
        {
            return WaitForAnyPageDontThrow(Env.DEFAULT_EXPLICIT_TIMEOUT, pages);
        }

        public static BasePage CreateInstance(Type pageType, bool assertOnPage = true)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            try
            {
                return (BasePage)Activator.CreateInstance(pageType, new object[] { assertOnPage });
            }
            catch
            {
                //HACK: Exception has to be thrown manually in order to enable Nunit to report meaningful information when Activator.CreateInstance() fails.
                throw new Exception($"Failed to instantiate {pageType.Name} within {timer.Elapsed}.");
            }
        }

        public static T CreateInstance<T>(bool assertOnPage = true) where T : BasePage
        {
            return (T)CreateInstance(typeof(T), assertOnPage);
        }

        public static BasePage CreateInstance(Type pageType, TimeSpan assertOnPageTimeout)
        {
            try
            {
                return (BasePage)Activator.CreateInstance(pageType, new object[] { assertOnPageTimeout });
            }
            catch
            {
                //HACK: Exception has to be thrown manually in order to enable Nunit to report meaningful information when Activator.CreateInstance() fails.
                throw new Exception($"Failed to instantiate {pageType.Name} within {assertOnPageTimeout}.");
            }
        }

        public static T CreateInstance<T>(TimeSpan assertOnPageTimeout) where T : BasePage
        {
            return (T)CreateInstance(typeof(T), assertOnPageTimeout);
        }

        /// <summary>
        /// Creates instances of given <paramref name="pageTypes"/> without asserting that they are displayed.
        /// </summary>
        /// <param name="pageTypes"></param>
        /// <returns>An array of instantiated pages.</returns>
        public static BasePage[] CreateInstances(params Type[] pageTypes)
        {
            BasePage[] pages = new BasePage[pageTypes.Length];
            for (int i = 0; i < pageTypes.Length; i++)
            {
                pages[i] = CreateInstance(pageTypes[i], false);
            }
            return pages;
        }

        public static bool GetIsPageShown(Type pageType, out BasePage page)
        {
            page = CreateInstance(pageType, false);
            return page.IsCurrentlyShown();
        }

        public static bool GetIsPageShown(Type pageType)
        {
            return GetIsPageShown(pageType, out _);
        }

        public static bool GetIsPageShown<T>(out T page) where T : BasePage
        {
            bool result = GetIsPageShown(typeof(T), out BasePage outPage);
            page = (T)outPage;
            return result;
        }

        public static bool GetIsPageShown<T>() where T : BasePage
        {
            return GetIsPageShown<T>(out _);
        }
    }
}
