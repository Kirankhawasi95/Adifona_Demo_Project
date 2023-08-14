using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using HorusUITest.Configuration;
using HorusUITest.Enums;
using HorusUITest.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using HorusUITest.Helper;
using System.IO;

namespace HorusUITest
{
    public abstract class BaseApp
    {
        private Size? screenSize = null;
        public Size ScreenSize
        {
            get
            {
                screenSize = screenSize ?? Driver.Manage().Window.Size;
                return screenSize.Value;
            }
        }

        public abstract AppiumDriver<AppiumWebElement> Driver { get; }

        protected void Initialize()
        {
            Driver.Manage().Timeouts().ImplicitWait = Env.IMPLICIT_TIMEOUT;
        }

        /// <summary>
        /// Taps on a position on the screen.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public abstract void Tap(int x, int y, int numberOfTaps = 1);

        /// <summary>
        /// Taps on an relative position inside of an element (center by default).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Tap(IMobileElement<AppiumWebElement> element, double percentX = 0.5, double percentY = 0.5, int numberOfTaps = 1)
        {
            var position = element.GetRect().GetPercentalPosition(percentX, percentY);
            Tap(position.X, position.Y, numberOfTaps);
        }

        /// <summary>
        /// Taps on a position on the screen relative to the screen size.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="relativeY"></param>
        public void TapRelativeToScreenSize(double relativeX, double relativeY, int numberOfTaps = 1)
        {
            int absX = (int)(ScreenSize.Width * relativeX);
            int absY = (int)(ScreenSize.Height * relativeY);
            Tap(absX, absY, numberOfTaps);
        }

        public abstract void Swipe(int startX, int startY, int endX, int endY, bool useInertia = true, TimeSpan? timeToSwipe = null);

        public void SwipeToOffset(int startX, int startY, int deltaX, int deltaY, bool useInertia = true, TimeSpan? timeToSwipe = null)
        {
            var endX = startX + deltaX;
            var endY = startY + deltaY;

            Swipe(startX, startY, endX, endY, useInertia, timeToSwipe);
        }

        public void SwipeElementToElement(AppiumWebElement startElement, AppiumWebElement endElement, bool useInertia = true)
        {
            SwipeElementToElement(startElement, .5, .5, endElement, .5, .5, useInertia);
        }

        public string GetPageSource()
        {
            return Driver.PageSource;
        }

        public void SwipeElementToOffset<T>(T element, int deltaX, int deltaY, bool useInertia = true) where T : IMobileElement<AppiumWebElement>
        {
            var rect = element.GetRect();

            var startX = rect.X + rect.Width / 2;
            var startY = rect.Y + rect.Height / 2;
            var endX = startX + deltaX;
            var endY = startY + deltaY;

            Swipe(startX, startY, endX, endY, useInertia);
        }

        public void SwipeElementToPosition<T>(T element, int? x = null, int? y = null, bool useInertia = true) where T : IMobileElement<AppiumWebElement>
        {
            var rect = element.GetRect();

            var startX = rect.X + rect.Width / 2;
            var startY = rect.Y + rect.Height / 2;
            x = x ?? startX;
            y = y ?? startY;

            Swipe(startX, startY, x.Value, y.Value, useInertia);
        }

        private Rectangle GetSafeRect(Rectangle rect, int safetyGap)
        {
            rect.X += safetyGap;
            rect.Y += safetyGap;
            rect.Width -= safetyGap * 2;
            rect.Height -= safetyGap * 2;
            return rect;
        }

        public void SwipeRelativeToElementSize(AppiumWebElement element, double startPercentX, double startPercentY, double endPercentX, double endPercentY, bool useInertia = true, int safetyGap = 0)
        {
            var rect = GetSafeRect(element.Rect, safetyGap);

            var absoluteStartX = (int)(rect.Left + rect.Width * startPercentX);
            var absoluteStartY = (int)(rect.Top + rect.Height * startPercentY);
            var absoluteEndX = (int)(rect.Left + rect.Width * endPercentX);
            var absoluteEndY = (int)(rect.Top + rect.Height * endPercentY);

            Swipe(absoluteStartX, absoluteStartY, absoluteEndX, absoluteEndY, useInertia);
        }

        private void SwipePixelPerfect(int startX, int startY, int endX, int endY)
        {
            //Forcing the coordinates to be within the screen width and height.
            startX.Clamp(1, ScreenSize.Width - 1);        //Swiping to the exact border of the screen somehow gives an error ("outside of rect"), therefore 1 pixel buffer zone.
            startY.Clamp(1, ScreenSize.Height - 1);
            endX.Clamp(1, ScreenSize.Width - 1);
            endY.Clamp(1, ScreenSize.Height - 1);

            var action = new TouchAction(Driver);
            action.Press(startX, startY);
            action.Wait(100);
            action.MoveTo(endX, endY);
            action.Wait(100);
            action.Release();
            action.Perform();
        }

        public void SwipeRelativeToElementSizePixelPerfect(AppiumWebElement element, double startPercentX, double startPercentY, int deltaX, int deltaY, int safetyGap = 0)
        {
            var rect = GetSafeRect(element.Rect, safetyGap);

            var absoluteStartX = (int)(rect.Left + rect.Width * startPercentX);
            var absoluteStartY = (int)(rect.Top + rect.Height * startPercentY);
            var absoluteEndX = absoluteStartX + deltaX;
            var absoluteEndY = absoluteStartY + deltaY;

            SwipePixelPerfect(absoluteStartX, absoluteStartY, absoluteEndX, absoluteEndY);
        }

        public void SwipeRelativeToElementSize(AppiumWebElement element, double startPercentX, double startPercentY, int deltaX, int deltaY, bool useInertia = true, int safetyGap = 0)
        {
            var rect = GetSafeRect(element.Rect, safetyGap);

            var absoluteStartX = (int)(rect.Left + rect.Width * startPercentX);
            var absoluteStartY = (int)(rect.Top + rect.Height * startPercentY);
            var absoluteEndX = absoluteStartX + deltaX;
            var absoluteEndY = absoluteStartY + deltaY;

            Swipe(absoluteStartX, absoluteStartY, absoluteEndX, absoluteEndY, useInertia);
        }

        public void SwipeElementToElement(AppiumWebElement startElement, double startPercentX, double startPercentY, AppiumWebElement endElement, double endPercentX, double endPercentY, bool useInertia = true)
        {
            var startRect = startElement.Rect;
            var endRect = endElement.Rect;

            var absoluteStartX = (int)(startRect.Left + startRect.Width * startPercentX);
            var absoluteStartY = (int)(startRect.Top + startRect.Height * startPercentY);
            var absoluteEndX = (int)(endRect.Left + endRect.Width * endPercentX);
            var absoluteEndY = (int)(endRect.Top + endRect.Height * endPercentY);

            Swipe(absoluteStartX, absoluteStartY, absoluteEndX, absoluteEndY, useInertia);
        }

        /// <summary>
        /// Swipe by relative screen position. Arguments must be within 0..1.
        /// </summary>
        public void SwipeRelativeToScreenSize(double startPercentX, double startPercentY, double endPercentX, double endPercentY, bool useInertia = true)
        {
            var absoluteStartX = (int)(ScreenSize.Width * startPercentX);
            var absoluteStartY = (int)(ScreenSize.Height * startPercentY);
            var absoluteEndX = (int)(ScreenSize.Width * endPercentX);
            var absoluteEndY = (int)(ScreenSize.Height * endPercentY);

            Swipe(absoluteStartX, absoluteStartY, absoluteEndX, absoluteEndY, useInertia);
        }

        /// <summary>
        /// Swipe by relative screen position. Arguments must be within 0..1.
        /// </summary>
        public void SwipeElementRelativeToScreenSize(AppiumWebElement element, double percentDeltaX, double percentDeltaY, bool useInertia = true)
        {
            var rect = element.Rect;
            var absoluteDeltaX = (int)(ScreenSize.Width * percentDeltaX);
            var absoluteDeltaY = (int)(ScreenSize.Height * percentDeltaY);

            SwipeToOffset(rect.GetCenter().X, rect.GetCenter().Y, absoluteDeltaX, absoluteDeltaY, useInertia);
        }

        public void SwipeLeftToRight()
        {
            SwipeRelativeToScreenSize(.25, .1, .75, .1);
        }
        public void SwipeRightToLeft()
        {
            SwipeRelativeToScreenSize(.75, .1, .25, .1);
        }
        public void SwipeTopLeftToBottomRight()
        {
            SwipeRelativeToScreenSize(.1, .1, .9, .9);
        }
        public void SwipeBottomRightToTopLeft()
        {
            SwipeRelativeToScreenSize(.9, .9, .1, .1);
        }
        public AppiumWebElement FindElementByAutomationId(string id, IMobileElement<AppiumWebElement> root)
        {
            Debug.WriteLine($"Searching for '{id}' with root '{root}'.");
            if (root != null)
                return root.FindElementByAccessibilityId(id);
            else
                return Driver.FindElementByAccessibilityId(id);
        }

        public abstract AppiumWebElement FindElementByAutomationId(string id, AppiumWebElement root = null, bool verifyVisibility = false);
        public abstract IReadOnlyCollection<AppiumWebElement> FindElementsByAutomationId(string id, AppiumWebElement root = null, bool verifyVisibility = false);
        public abstract AppiumWebElement FindElementByXPath(string xPath, AppiumWebElement root = null, bool verifyVisibility = false);
        public abstract void PressEnter();
        public abstract void PressSpace();
        public abstract void PressBackspace();
        public abstract void PressHomeButton();
        public abstract void PressMenuButton();
        public abstract void PressBackButton();

        public AppiumWebElement FindElementById(string id, AppiumWebElement root = null)
        {
            Debug.WriteLine($"Searching for '{id}' with root '{root}'.");
            if (root != null)
                return root.FindElementById(id);
            else
                return Driver.FindElementById(id);
        }

        public AppiumWebElement FindElementByClassName(string className, AppiumWebElement root = null)
        {
            Debug.WriteLine($"Searching for '{className}' with root '{root}'.");
            if (root != null)
                return root.FindElementByClassName(className);
            else
                return Driver.FindElementByClassName(className);
        }

        public IReadOnlyCollection<AppiumWebElement> FindElementsById(string id, AppiumWebElement root = null)
        {
            Debug.WriteLine($"Searching for ALL '{id}' with root '{root}'.");
            if (root != null)
                return root.FindElementsById(id);
            else
                return Driver.FindElementsById(id);
        }


        public IReadOnlyCollection<AppiumWebElement> FindElementsByClassName(string className, AppiumWebElement root = null)
        {
            Debug.WriteLine($"Searching for ALL '{className}' with root '{root}'.");
            if (root != null)
                return root.FindElementsByClassName(className);
            else
                return Driver.FindElementsByClassName(className);
        }

        public AppiumWebElement FindElementByIdDontThrow(string id)
        {
            Debug.WriteLine($"Check-Searching for '{id}', not throwing.");
            try
            {
                return Driver.FindElementById(id);
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        public AppiumWebElement FindElementByAutomationIdDontThrow(string id)
        {
            Debug.Write($"Check-");
            try
            {
                return FindElementByAutomationId(id);
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        public AppiumWebElement FindElementInsideWebViewById(string id)
        {
            Debug.WriteLine($"Searching for '{id}' inside WebView.");
            return Driver.FindElementByXPath("//*[@resource-id='" + id + "']");
        }

        public T WaitForElement<T>(Func<T> elementQuery, TimeSpan? timeout = null) where T : class, IMobileElement<AppiumWebElement>
        {
            timeout = timeout ?? Env.DEFAULT_EXPLICIT_TIMEOUT;
            T element = null;
            WebDriverWait wait = new WebDriverWait(Driver, timeout.Value);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));
            wait.Until(condition =>
            {
                element = elementQuery.Invoke();
                return element.Exists();
            });
            return element;
        }

        public IReadOnlyCollection<AppiumWebElement> WaitForElements(Func<IReadOnlyCollection<AppiumWebElement>> elementQuery, TimeSpan? timeout = null)
        {
            timeout = timeout ?? Env.DEFAULT_EXPLICIT_TIMEOUT;
            IReadOnlyCollection<AppiumWebElement> elements = null;
            WebDriverWait wait = new WebDriverWait(Driver, timeout.Value);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException));
            wait.Until(condition =>
            {
                elements = elementQuery.Invoke();
                return elements.Count > 0;
            });
            return elements;
        }

        public void WaitForNoElement(Func<IMobileElement<AppiumWebElement>> elementQuery, TimeSpan? timeout = null)
        {
            timeout = timeout ?? Env.DEFAULT_EXPLICIT_TIMEOUT;
            WebDriverWait wait = new WebDriverWait(Driver, timeout.Value);
            wait.Until(condition =>
            {
                return !elementQuery.Exists();
            });
        }

        public abstract bool GetToggleSwitchState(IMobileElement<AppiumWebElement> toggleSwitch);

        public bool GetIsEnabled(AppiumWebElement element)
        {
            if (bool.TryParse(element.GetAttribute("enabled"), out var result))
                return result;
            throw new InvalidOperationException("Unable to get attribute 'enabled' of given element.");
        }

        public abstract void OpenQuickSettings();
        public abstract void CloseQuickSettings();
        public abstract void OpenSystemSettings();
        public abstract void HideKeyboard();
        public abstract void LockDevice(int seconds);

        public abstract void EnableBluetooth();
        public abstract void DisableBluetooth();
        public abstract void EnableWifi();
        public abstract void DisableWifi();
        public abstract void EnableMobileData();
        public abstract void DisableMobileData();
        public abstract void EnableLocation();
        public abstract void DisableLocation();
        public abstract void RevokeGPSBackgroundPermission();
        public abstract void GrantGPSBackgroundPermission();
        public abstract void GrantGPSPermission();
        public abstract void RevokeGPSPermission();

        public abstract bool PutAppToBackground(int appBackgroundTime);
        public abstract bool PutAppToBackground(double appBackgroundTime);
        public abstract bool PutAppToBackground();

        public abstract void GetAppInForeground();
        public abstract void LaunchApp();
        public abstract void CloseApp();
        public abstract void ResetApp();
        public abstract void ChangeDeviceLanguage(Language_Device language);

        public Screenshot TakeScreenshot()
        {
            return Driver.GetScreenshot();
        }

        public void QuitDriver()
        {
            Driver.Quit();
        }

        public void ZoomIn()
        {
            PointerInputDevice finger = new PointerInputDevice(PointerKind.Touch);
            PointerInputDevice finger2 = new PointerInputDevice(PointerKind.Touch);

            ActionSequence pinchAndZoom1 = new ActionSequence(finger, 0);
            pinchAndZoom1.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, (int)(ScreenSize.Width * 0.5), (int)(ScreenSize.Height * 0.5), TimeSpan.FromMilliseconds(0)));
            pinchAndZoom1.AddAction(finger.CreatePointerDown(MouseButton.Left));
            pinchAndZoom1.AddAction(finger.CreatePause(TimeSpan.FromMilliseconds(100)));
            pinchAndZoom1.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, (int)(ScreenSize.Width * 0.25), (int)(ScreenSize.Height * 0.25), TimeSpan.FromMilliseconds(600)));
            pinchAndZoom1.AddAction(finger.CreatePointerUp(MouseButton.Left));

            ActionSequence pinchAndZoom2 = new ActionSequence(finger2, 0);
            pinchAndZoom2.AddAction(finger2.CreatePointerMove(CoordinateOrigin.Viewport, (int)(ScreenSize.Width * 0.5), (int)(ScreenSize.Height * 0.5), TimeSpan.FromMilliseconds(0)));
            pinchAndZoom2.AddAction(finger2.CreatePointerDown(MouseButton.Left));
            pinchAndZoom2.AddAction(finger2.CreatePause(TimeSpan.FromMilliseconds(100)));
            pinchAndZoom2.AddAction(finger2.CreatePointerMove(CoordinateOrigin.Viewport, (int)(ScreenSize.Width * 0.75), (int)(ScreenSize.Height * 0.75), TimeSpan.FromMilliseconds(600)));
            pinchAndZoom2.AddAction(finger2.CreatePointerUp(MouseButton.Left));

            Driver.PerformActions(new List<ActionSequence>() { pinchAndZoom1, pinchAndZoom2 });
        }

        public void ZoomOut()
        {
            PointerInputDevice finger = new PointerInputDevice(PointerKind.Touch);
            PointerInputDevice finger2 = new PointerInputDevice(PointerKind.Touch);

            ActionSequence pinchAndZoom1 = new ActionSequence(finger, 0);
            pinchAndZoom1.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, (int)(ScreenSize.Width * 0.25), (int)(ScreenSize.Height * 0.25), TimeSpan.FromMilliseconds(0)));
            pinchAndZoom1.AddAction(finger.CreatePointerDown(MouseButton.Right));
            pinchAndZoom1.AddAction(finger.CreatePause(TimeSpan.FromMilliseconds(100)));
            pinchAndZoom1.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, (int)(ScreenSize.Width * 0.5), (int)(ScreenSize.Height * 0.5), TimeSpan.FromMilliseconds(600)));
            pinchAndZoom1.AddAction(finger.CreatePointerUp(MouseButton.Right));

            ActionSequence pinchAndZoom2 = new ActionSequence(finger2, 0);
            pinchAndZoom2.AddAction(finger2.CreatePointerMove(CoordinateOrigin.Viewport, (int)(ScreenSize.Width * 0.75), (int)(ScreenSize.Height * 0.75), TimeSpan.FromMilliseconds(0)));
            pinchAndZoom2.AddAction(finger2.CreatePointerDown(MouseButton.Right));
            pinchAndZoom2.AddAction(finger2.CreatePause(TimeSpan.FromMilliseconds(100)));
            pinchAndZoom2.AddAction(finger2.CreatePointerMove(CoordinateOrigin.Viewport, (int)(ScreenSize.Width * 0.5), (int)(ScreenSize.Height * 0.5), TimeSpan.FromMilliseconds(600)));
            pinchAndZoom2.AddAction(finger2.CreatePointerUp(MouseButton.Right));

            Driver.PerformActions(new List<ActionSequence>() { pinchAndZoom1, pinchAndZoom2 });
        }

        public void SwipeDown()
        {
            SwipeRelativeToScreenSize(0.5, 0.5, 0.5, 0.25);
        }

        public void SwipeUp()
        {
            SwipeRelativeToScreenSize(0.5, 0.5, 0.5, 0.75);
        }

        /// <summary>
        /// Finding Element by Image requires installation of opencv4nodejs else it will throw exception
        /// </summary>
        /// <param name="ImageNameWithExtension"></param>
        /// <returns></returns>
        public AppiumWebElement FindElementByImage(string ImageNameWithExtension)
        {
            string currentDir = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.ToLower().IndexOf("bin"));
            string imagePath = currentDir + "\\BaseImages\\" + ImageNameWithExtension;
            string imgBase64String = ImageComparison.GetBase64StringForImage(imagePath);
            return Driver.FindElementByImage(imgBase64String);
        }

        /// <summary>
        /// Method to get the color of pixel cordinate of a image
        /// </summary>
        /// <param name="appiumWebElement"></param>
        /// <param name="pixelX">Pixel X coordinate of the image for which the color has to be verified</param>
        /// <param name="pixelY">Pixel Y coordinate of the image for which the color has to be verified</param>
        /// <returns>Returns hexadecimal color code value starting with #</returns>
        public string GetColorFromImageByPixel(AppiumWebElement appiumWebElement, int pixelX, int pixelY)
        {
            string ColorCode = string.Empty;

            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Audifon\\ScreenShots";
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            string ImagePath = Path.Combine(folderPath, "ElementScreenShot.png");

            appiumWebElement.GetScreenshot().SaveAsFile(ImagePath, ScreenshotImageFormat.Png);

            using (var bitmap = new Bitmap(ImagePath))
            {
                bool found = false;

                for (int i = 0; i < bitmap.Width; i++)
                {
                    for (int j = 0; j < bitmap.Height; j++)
                    {
                        if (pixelX == i && pixelY == j)
                        {
                            Color pixel = bitmap.GetPixel(i, j);
                            ColorCode = "#" + pixel.R.ToString("X2") + pixel.G.ToString("X2") + pixel.B.ToString("X2");
                            found = true;
                            break;
                        }
                    }
                    if (found)
                        break;
                }
            }

            return ColorCode;
        }

        public abstract void ChangeDeviceDate(DateTime dateValue);

        public void ZoomWithTwoPoints()
        {
            PointerInputDevice finger = new PointerInputDevice(PointerKind.Touch);
            PointerInputDevice finger2 = new PointerInputDevice(PointerKind.Touch);
            
            ActionSequence pinchAndZoom1 = new ActionSequence(finger, 0);
            pinchAndZoom1.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, (int)(ScreenSize.Width * 0.5), (int)(ScreenSize.Height * 0.3), TimeSpan.FromMilliseconds(0)));
            pinchAndZoom1.AddAction(finger.CreatePointerDown(MouseButton.Left));
            pinchAndZoom1.AddAction(finger.CreatePause(TimeSpan.FromMilliseconds(100)));
            pinchAndZoom1.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, (int)(ScreenSize.Width * 0.5), (int)(ScreenSize.Height * 0.1), TimeSpan.FromMilliseconds(600)));
            pinchAndZoom1.AddAction(finger.CreatePointerUp(MouseButton.Left));

            ActionSequence pinchAndZoom2 = new ActionSequence(finger2, 0);
            pinchAndZoom2.AddAction(finger2.CreatePointerMove(CoordinateOrigin.Viewport, (int)(ScreenSize.Width * 0.5), (int)(ScreenSize.Height * 0.3), TimeSpan.FromMilliseconds(0)));
            pinchAndZoom2.AddAction(finger2.CreatePointerDown(MouseButton.Left));
            pinchAndZoom2.AddAction(finger2.CreatePause(TimeSpan.FromMilliseconds(100)));
            pinchAndZoom2.AddAction(finger2.CreatePointerMove(CoordinateOrigin.Viewport, (int)(ScreenSize.Width * 0.5), (int)(ScreenSize.Height * 0.4), TimeSpan.FromMilliseconds(600)));
            pinchAndZoom2.AddAction(finger2.CreatePointerUp(MouseButton.Left));

            Driver.PerformActions(new List<ActionSequence>() { pinchAndZoom1, pinchAndZoom2 });
        }

        public void ZoomWithFourPoints()
        {
            PointerInputDevice finger = new PointerInputDevice(PointerKind.Touch);
            PointerInputDevice finger2 = new PointerInputDevice(PointerKind.Touch);
            PointerInputDevice finger3 = new PointerInputDevice(PointerKind.Touch);
            PointerInputDevice finger4 = new PointerInputDevice(PointerKind.Touch);

            ActionSequence pinchAndZoom1 = new ActionSequence(finger, 0);
            pinchAndZoom1.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, (int)(ScreenSize.Width * 0.5), (int)(ScreenSize.Height * 0.2), TimeSpan.FromMilliseconds(0)));
            pinchAndZoom1.AddAction(finger.CreatePointerDown(MouseButton.Left));
            pinchAndZoom1.AddAction(finger.CreatePause(TimeSpan.FromMilliseconds(100)));
            pinchAndZoom1.AddAction(finger.CreatePointerMove(CoordinateOrigin.Viewport, (int)(ScreenSize.Width * 0.1), (int)(ScreenSize.Height * 0.1), TimeSpan.FromMilliseconds(600)));
            pinchAndZoom1.AddAction(finger.CreatePointerUp(MouseButton.Left));

            ActionSequence pinchAndZoom2 = new ActionSequence(finger2, 0);
            pinchAndZoom2.AddAction(finger2.CreatePointerMove(CoordinateOrigin.Viewport, (int)(ScreenSize.Width * 0.5), (int)(ScreenSize.Height * 0.2), TimeSpan.FromMilliseconds(0)));
            pinchAndZoom2.AddAction(finger2.CreatePointerDown(MouseButton.Left));
            pinchAndZoom2.AddAction(finger2.CreatePause(TimeSpan.FromMilliseconds(100)));
            pinchAndZoom2.AddAction(finger2.CreatePointerMove(CoordinateOrigin.Viewport, (int)(ScreenSize.Width * 0.1), (int)(ScreenSize.Height * 0.3), TimeSpan.FromMilliseconds(600)));
            pinchAndZoom2.AddAction(finger2.CreatePointerUp(MouseButton.Left));

            ActionSequence pinchAndZoom3 = new ActionSequence(finger3, 0);
            pinchAndZoom3.AddAction(finger3.CreatePointerMove(CoordinateOrigin.Viewport, (int)(ScreenSize.Width * 0.5), (int)(ScreenSize.Height * 0.2), TimeSpan.FromMilliseconds(0)));
            pinchAndZoom3.AddAction(finger3.CreatePointerDown(MouseButton.Left));
            pinchAndZoom3.AddAction(finger3.CreatePause(TimeSpan.FromMilliseconds(100)));
            pinchAndZoom3.AddAction(finger3.CreatePointerMove(CoordinateOrigin.Viewport, (int)(ScreenSize.Width * 0.7), (int)(ScreenSize.Height * 0.1), TimeSpan.FromMilliseconds(600)));
            pinchAndZoom3.AddAction(finger3.CreatePointerUp(MouseButton.Left));

            ActionSequence pinchAndZoom4 = new ActionSequence(finger4, 0);
            pinchAndZoom4.AddAction(finger4.CreatePointerMove(CoordinateOrigin.Viewport, (int)(ScreenSize.Width * 0.5), (int)(ScreenSize.Height * 0.2), TimeSpan.FromMilliseconds(0)));
            pinchAndZoom4.AddAction(finger4.CreatePointerDown(MouseButton.Left));
            pinchAndZoom4.AddAction(finger4.CreatePause(TimeSpan.FromMilliseconds(100)));
            pinchAndZoom4.AddAction(finger4.CreatePointerMove(CoordinateOrigin.Viewport, (int)(ScreenSize.Width * 0.7), (int)(ScreenSize.Height * 0.3), TimeSpan.FromMilliseconds(600)));
            pinchAndZoom4.AddAction(finger4.CreatePointerUp(MouseButton.Left));

            Driver.PerformActions(new List<ActionSequence>() { pinchAndZoom3, pinchAndZoom4, pinchAndZoom1, pinchAndZoom2 });
        }

        public abstract string GetDeviceOSVersion();
    }
}
