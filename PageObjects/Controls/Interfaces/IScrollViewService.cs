using HorusUITest.Configuration;

namespace HorusUITest.PageObjects.Controls.Interfaces
{
    public interface IScrollViewService
    {
        void ScrollToTop(int maxNumberOfSwipes = Env.DEFAULT_MAX_SWIPES_TO_SCROLL);
        void ScrollToBottom(int maxNumberOfSwipes = Env.DEFAULT_MAX_SWIPES_TO_SCROLL);
        void ScrollUp(double verticalPercentage);
        void ScrollDown(double verticalPercentage);
        bool GetIsScrolledToTop();
        bool GetIsScrolledToBottom();
    }
}
