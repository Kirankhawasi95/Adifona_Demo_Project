using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;

namespace HorusUITest.Extensions
{
    public static class AppiumWebElementExtensions
    {
        public static bool Exists(this IMobileElement<AppiumWebElement> element)
        {
            if (element == null) return false;
            try
            {
                return element.Displayed;
            }
            catch (NoSuchElementException) { return false; }
            catch (StaleElementReferenceException) { return false; }
            catch (TargetInvocationException) { return false; }
        }

        public static bool Exists(this Func<IMobileElement<AppiumWebElement>> query)
        {
            if (query == null) return false;
            try
            {
                var element = query.Invoke();
                return element.Exists();
            }
            catch (NoSuchElementException) { return false; }
            catch (StaleElementReferenceException) { return false; }
            catch (TargetInvocationException) { return false; }
        }

        public static bool Exists(this IReadOnlyCollection<AppiumWebElement> elements)
        {
            if (elements == null) return false;
            return elements.Count > 0;
        }

        public static bool IsOnScreen(this IMobileElement<AppiumWebElement> element)
        {
            if (element == null) return false;
            try
            {
                var location = element.Location;
                var screenSize = AppManager.App.ScreenSize;
                if (location.X < 0) return false;
                if (location.Y < 0) return false;
                if (location.X > screenSize.Width) return false;
                if (location.Y > screenSize.Height) return false;
                return true;
            }
            catch (NoSuchElementException) { return false; }
            catch (StaleElementReferenceException) { return false; }
            catch (TargetInvocationException) { return false; }
        }

        public static bool TryFindElement(this AppiumWebElement parentElement, Func<AppiumWebElement, AppiumWebElement> query, out AppiumWebElement element)
        {
            try
            {
                element = parentElement.FindElement(query);
                return element != null;
            }
            catch (NoSuchElementException)
            {
                element = null;
                return false;
            }
        }

        public static bool TryFindElements(this AppiumWebElement parentElement, Func<AppiumWebElement, IReadOnlyCollection<AppiumWebElement>> query, out IReadOnlyCollection<AppiumWebElement> elements)
        {
            elements = query.Invoke(parentElement);
            return elements.Count > 0;
        }

        public static T FindElement<T>(this T parentElement, Func<T, T> query) where T : IMobileElement<AppiumWebElement>
        {
            return query.Invoke(parentElement);
        }

        public static AppiumWebElement FindElementDontThrow(this AppiumWebElement parentElement, Func<AppiumWebElement, AppiumWebElement> query)
        {
            try
            {
                return FindElement(parentElement, query);
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        public static IReadOnlyCollection<AppiumWebElement> FindElements(this AppiumWebElement parentElement, Func<AppiumWebElement, IReadOnlyCollection<AppiumWebElement>> query)
        {
            TryFindElements(parentElement, query, out var elements);    //If no elements were found, an empty collection is returned.
            return elements;
        }

        public static string GetTextOrNull(this IMobileElement<AppiumWebElement> element)
        {
            if (element == null) return null;
            try
            {
                return element.Text;
            }
            catch (NoSuchElementException) { return null; }
            catch (StaleElementReferenceException) { return null; }
            catch (TargetInvocationException) { return null; }
        }

        public static string GetTextOrEmptyString(this IMobileElement<AppiumWebElement> element)
        {
            var result = GetTextOrNull(element);
            return result ?? "";
        }

        public static Rectangle GetRect(this IMobileElement<AppiumWebElement> element)
        {
            Point location = element.Location;
            Size size = element.Size;
            return new Rectangle(location, size);
        }
    }
}
