using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using HorusUITest.Configuration;
using HorusUITest.Extensions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;

namespace HorusUITest.PageObjects
{
    public abstract class BasePage : BasePageObject
    {
        private IReadOnlyCollection<AppiumWebElement> AndroidElements => App.FindElementsByClassName("android.widget.TextView");
        private IReadOnlyCollection<AppiumWebElement> IosElements => App.FindElementsByClassName("XCUIElementTypeStaticText");

        protected abstract PlatformQuery Trait { get; }

        protected BasePage(bool assertOnPage)
        {
            if (assertOnPage)
                AssertOnPage();
        }

        protected BasePage(TimeSpan assertOnPageTimeout)
        {
            AssertOnPage(assertOnPageTimeout);
        }

        private IReadOnlyCollection<AppiumWebElement> Elements
        {
            get
            {
                if (OnAndroid)
                    return AndroidElements;
                if (OniOS)
                    return IosElements;
                throw new PlatformNotSupportedException("Invalid platform.");
            }
        }

        /// <summary>
        /// Asserts that the trait is present (or will be present before timeout). Timeout defaults to <see cref="Env.DEFAULT_EXPLICIT_TIMEOUT"/>.
        /// </summary>
        /// <param name="timeout">Time to wait before the assertion fails</param>
        /// <param name="secondaryTraitQuery">A query to an additional element that must also be present</param>
        /// <returns>Reference to the trait's element.</returns>
        protected IMobileElement<AppiumWebElement> AssertOnPage(TimeSpan? timeout = null, Func<IMobileElement<AppiumWebElement>> secondaryTraitQuery = null, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = -1)
        {
            Debug.WriteLine($"   ### Asserting on {this.GetType().Name}");
            if (timeout == null)
                timeout = Env.DEFAULT_EXPLICIT_TIMEOUT;

            var message = "Unable to verify on page: " + this.GetType().Name + ". Assertion called by: " + memberName + " at line " + lineNumber + " of file " + filePath;

            IMobileElement<AppiumWebElement> result = null;

            if (timeout == TimeSpan.Zero)
            {
                Assert.IsTrue(TryInvokeQuery(Trait.Current, out result), message);
                if (secondaryTraitQuery != null)
                    Assert.IsTrue(TryInvokeQuery(secondaryTraitQuery, out _), message);
                return result;
            }
            else
            {                
                Assert.DoesNotThrow(() => result = App.WaitForElement(Trait.Current, timeout.Value), message);
                if (secondaryTraitQuery != null)
                    Assert.DoesNotThrow(() => App.WaitForElement(secondaryTraitQuery, timeout.Value), message);
                return result;
            }
        }

        /// <summary>
        /// Waits until the page is displayed. Timeout defaults to <see cref="Env.DEFAULT_EXPLICIT_TIMEOUT"/>.
        /// </summary>
        /// <param name="timeout"></param>
        public void WaitForPage(TimeSpan? timeout = null)
        {
            App.WaitForElement(Trait.Current, timeout);
        }

        /// <summary>
        /// Verifies that the trait is no longer present. Defaults to a 5 second wait.
        /// </summary>  
        /// <param name="timeout">Time to wait before the assertion fails</param>
        public void WaitForPageToLeave(TimeSpan? timeout = null)
        {
            Debug.WriteLine($"   ### Asserting NOT on {this.GetType().Name}");
            timeout = timeout ?? Env.DEFAULT_EXPLICIT_TIMEOUT;
            var message = "Unable to verify NOT on page: " + this.GetType().Name;

            Assert.DoesNotThrow(() => App.WaitForNoElement(Trait.Current, timeout.Value), message);
        }

        public bool IsCurrentlyShown()
        {
            Debug.WriteLine($"   ### Checking if currently on {this.GetType().Name}");
            bool result = Trait.Current.Exists();
            return result;
        }

        public bool IsShowingBeforeTimeout(TimeSpan timeout)
        {
            try
            {
                Debug.WriteLine($"   ### Checking if {this.GetType().Name} shows up");
                App.WaitForElement(Trait.Current, timeout);
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public bool IsGoneBeforeTimeout(TimeSpan timeout)
        {
            try
            {
                Debug.WriteLine($"   ### Checking if {this.GetType().Name} disappears");
                App.WaitForNoElement(Trait.Current, timeout);
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public List<string> GetAllTextOfPage()
        {
            List<string> elementTexts= new List<string>();
            foreach (var element in Elements)
            {
                elementTexts.Add(element.Text);
            }
            return elementTexts;
        }
    }
}