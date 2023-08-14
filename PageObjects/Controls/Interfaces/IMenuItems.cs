using System.Collections.Generic;
using HorusUITest.Enums;

namespace HorusUITest.PageObjects.Controls.Interfaces
{
    public interface IMenuItems
    {
        int CountAllVisible();
        int CountAll();
        List<string> GetAllVisible();
        List<string> GetAll();
        string Get(int index, IndexType indexType);
        void Open(int index, IndexType indexType);
    }
}
