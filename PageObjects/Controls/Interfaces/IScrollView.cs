using HorusUITest.Configuration;

namespace HorusUITest.PageObjects.Controls.Interfaces
{
    public interface IScrollView<T>
    {
        T ScrollToTop(int maxNumberOfSwipes = Env.DEFAULT_MAX_SWIPES_TO_SCROLL);
        T ScrollToBottom(int maxNumberOfSwipes = Env.DEFAULT_MAX_SWIPES_TO_SCROLL);
        T ScrollUp(double verticalPercentage);
        T ScrollDown(double verticalPercentage);
        bool GetIsScrolledToTop();
        bool GetIsScrolledToBottom();
    }
}
