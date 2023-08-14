using HorusUITest.PageObjects.Interfaces;

namespace HorusUITest.PageObjects.Controls.Interfaces
{
    public interface INavigationBar : IHasBackNavigation
    {
        void TapBack();
        void SwipeBack();
        bool GetIsBackButtonDisplayed();
        string GetNavigationBarTitle();
    }
}
