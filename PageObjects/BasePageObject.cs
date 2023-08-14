using System;
using HorusUITest.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using Platform = HorusUITest.Enums.Platform;

namespace HorusUITest.PageObjects
{
    //TODO: Add XML docs to every public member of every page object.

    public abstract class BasePageObject
    {
        protected BaseApp App => AppManager.App;
        protected bool OnAndroid => AppManager.Platform == Platform.Android;
        protected bool OniOS => AppManager.Platform == Platform.iOS;

        protected virtual void ClearCache()
        {
        }

        protected T InvokeQueryDontThrow<T>(Func<T> elementQuery) where T : class, IMobileElement<AppiumWebElement>
        {
            try
            {
                return elementQuery.Invoke();
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        /// <summary>
        /// Tries to find the element given by the query. Does not throw if no element can be found.
        /// </summary>
        /// <param name="elementQuery">The query to invoke.</param>
        /// <param name="element">Outputs the element if one is found, null otherwise.</param>
        /// <returns>True if an element could be found, false otherwise.</returns>
        protected bool TryInvokeQuery<T>(Func<T> elementQuery, out T element) where T : class, IMobileElement<AppiumWebElement>
        {
            element = InvokeQueryDontThrow(elementQuery);
            return element.Exists();
        }

        protected bool ElementExists(Func<IMobileElement<AppiumWebElement>> elementQuery)
        {
            return elementQuery.Exists();
        }

        protected bool DoUntilElementExists(Action action, Func<AppiumWebElement> elementQuery, out AppiumWebElement element, int maxNumberOfTries = int.MaxValue)
        {
            int i = 0;
            TryInvokeQuery(elementQuery, out element);
            while (i < maxNumberOfTries && !element.Exists())
            {
                i++;
                action();
                TryInvokeQuery(elementQuery, out element);
            }
            return element.Exists();
        }

        protected bool DoUntilElementExists(Action action, Func<AppiumWebElement> elementQuery, int maxNumberOfTries = int.MaxValue)
        {
            return DoUntilElementExists(action, elementQuery, out _, maxNumberOfTries);
        }
    }
}
