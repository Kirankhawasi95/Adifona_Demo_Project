using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using HorusUITest.Extensions;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace HorusUITest.PageObjects.Controls
{
    public class ProgramSelectionDisplay<T> : BaseFactoryControl
    {
        private const string ProgramButtonAID = "Horus.Views.Controls.ProgramSelectionDisplayItem.Program";
        private const string ProgramIconAID = "Horus.Views.Controls.ProgramSelectionDisplayItem.Icon";

        [CacheLookup, FindsByAndroidUIAutomator(Accessibility = ProgramIconAID), FindsByIOSUIAutomation(Accessibility = ProgramIconAID)]
        private IList<AppiumWebElement> Programs { get; set; }

        private T page;
        private IMobileElement<AppiumWebElement> parent;

        public ProgramSelectionDisplay(T page, IMobileElement<AppiumWebElement> parent) : base(parent)
        {
            this.page = page;
            this.parent = parent;
        }

        private Rectangle? parentRect;
        private Rectangle ParentRect
        {
            get
            {
                parentRect = parentRect ?? parent.GetRect();
                return parentRect.Value;
            }
        }

        public int GetNumberOfPrograms()
        {
            return Programs.Count;
        }

        public string GetProgramIcon(int index)
        {
            return Programs[index].Text;
        }

        public List<string> GetAllProgramIcons()
        {
            var result = new List<string>();
            int count = Programs.Count;
            for (int i = 0; i < count; i++)
            {
                result.Add(GetProgramIcon(i));
            }
            return result;
        }

        public int GetIndexOfProgramIcon(string icon)
        {
            int count = Programs.Count;
            for (int i = 0; i < count; i++)
            {
                if (GetProgramIcon(i) == icon)
                    return i;
            }
            throw new ArgumentException($"There is no hearing program with icon text '{icon}'.");
        }

        public bool GetIsProgramExisting(int index)
        {
            return index >= 0 && index < Programs.Count;
        }

        public bool GetIsProgramExisting(string icon)
        {
            try
            {
                return GetIsProgramExisting(GetIndexOfProgramIcon(icon));
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns whether or not the indexed program is currently active.
        /// </summary>
        /// <param name="index"></param>
        public bool GetIsProgramSelected(int index)
        {
            var rect = Programs[index].Rect;
            double tolerance = ParentRect.Width / 10f;
            double distance = rect.GetCenter().GetDistanceTo(ParentRect.GetCenter());
            if (distance > tolerance)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Returns whether or not the program with the given icon text is currently active. Does nothing if it's already selected.
        /// This method is slower than calling by index, but is safer in regards to changing program order.
        /// </summary>
        /// <param name="icon">The string representation of the program's icon. Can be acquired by calling <see cref="GetProgramIcon(int)"/></param>.
        public bool GetIsProgramSelected(string icon)
        {
            return GetIsProgramSelected(GetIndexOfProgramIcon(icon));
        }

        /// <summary>
        /// Selects the indexed hearing program. Does nothing if it's already selected.
        /// </summary>
        /// <param name="index"></param>
        public T SelectProgram(int index)
        {
            if (!GetIsProgramSelected(index))
            {
                App.Tap(Programs[index]);
                Thread.Sleep(500);      //Waiting for animations.
            }
            return page;
        }

        /// <summary>
        /// Selects the hearing program that corresponds to the given icon text. Does nothing if it's already selected.
        /// This method is slower than selecting by index, but is safer in regards to changing program order.
        /// </summary>
        /// <param name="icon">The string representation of the program's icon. Can be acquired by calling <see cref="GetProgramIcon(int)"/></param>.
        public T SelectProgram(string icon)
        {
            return SelectProgram(GetIndexOfProgramIcon(icon));
        }

        /// <summary>
        /// Opens the selected hearing program and navigates to <see cref="ProgramDetailPage"/>.
        /// </summary>
        public void OpenCurrentProgram()
        {
            App.Tap(parent);    //The current hearing program's icon is in the center of the parent element, so this shortcut is allowed.
        }

        /// <summary>
        /// Opens the indexed hearing program and navigates to <see cref="ProgramDetailPage"/>.
        /// </summary>
        public void OpenProgram(int index)
        {
            SelectProgram(index);
            OpenCurrentProgram();
        }

        /// <summary>
        /// Opens the hearing program given by the icon text and navigates to <see cref="ProgramDetailPage"/>.
        /// This method is slower than opening by index, but is safer in regards to changing program order.
        /// </summary>
        /// <param name="icon">The string representation of the program's icon. Can be acquired by calling <see cref="GetProgramIcon(int)"/></param>.
        public void OpenProgram(string icon)
        {
            OpenProgram(GetIndexOfProgramIcon(icon));
        }

        /// <summary>
        /// Get Program Element
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public AppiumWebElement GetProgramElement(int index)
        {
            return Programs[index];
        }
    }
}
