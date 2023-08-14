using System.Collections.Generic;
using System.Drawing;

namespace HorusUITest.Helper
{
    public static class CollectionHelper
    {
        public static List<T> CreateListOfNullElements<T>(int count) where T : class
        {
            var result = new List<T>();
            for (int i = 0; i < count; i++)
            {
                result.Add(null);
            }
            return result;
        }

        public static List<Rectangle?> CreateListOfNullRects(int count)
        {
            var result = new List<Rectangle?>();
            for (int i = 0; i < count; i++)
            {
                result.Add(null);
            }
            return result;
        }
    }
}
