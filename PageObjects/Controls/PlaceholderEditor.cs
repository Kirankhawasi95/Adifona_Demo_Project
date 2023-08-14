using System;
using HorusUITest.PageObjects.Controls.Interfaces;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Controls
{
    public class PlaceholderEditor<T> : BasePageObject, IPlaceholderEditor<T>
    { 
        private readonly T parentPage;
        private readonly Func<AppiumWebElement> editorQuery;

        private AppiumWebElement editorElement;
        private AppiumWebElement EditorElement
        {
            get
            {
                editorElement = editorElement ?? editorQuery.Invoke();
                return editorElement;
            }
        }

        public PlaceholderEditor(T parentPage, Func<AppiumWebElement> editorQuery)
        {
            this.parentPage = parentPage;
            this.editorQuery = editorQuery;
        }

        //TODO: Doesn't work reliably on iOS 13.4 -> some part of the text remains.
        public T ClearEditorText()
        {
            EditorElement.Clear();
            App.HideKeyboard();
            return parentPage;
        }

        public string GetEditorText()
        {
            return EditorElement.Text;
        }

        public T EnterTextIntoEditor(string text)
        {
            EditorElement.SendKeys(text);
            App.HideKeyboard();
            return parentPage;
        }
    }
}
