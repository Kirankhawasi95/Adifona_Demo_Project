namespace HorusUITest.PageObjects.Controls.Interfaces
{
    public interface IPlaceholderEditor<T>
    {
        string GetEditorText();
        T ClearEditorText();
        T EnterTextIntoEditor(string text);
    }
}
