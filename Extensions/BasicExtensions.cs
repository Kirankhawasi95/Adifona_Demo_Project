using System;
using System.Drawing;
using System.Text.RegularExpressions;
using HorusUITest.Enums;

namespace HorusUITest.Extensions
{
    public static class BasicExtensions
    {
        /// <summary>
        /// Guarantees that the value doesn't exceed the lower and upper bound.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static void Clamp(this ref int value, int min, int max)
        {
            if (value < min)
                value = min;
            else if (value > max)
                value = max;
        }

        public static string GetNumbers(this string text)
        {
            return Regex.Replace(text, @"[^\d]", "");
        }

        public static int ToInt(this string text)
        {
            if (int.TryParse(text, out int result))
                return result;
            else
                throw new ArgumentException("Unable to convert string '" + text + "' to int.");
        }

        public static int ExtractInt(this string text)
        {
            return text.GetNumbers().ToInt();
        }

        public static Point GetPercentalPosition(this Rectangle rect, double percentX, double percentY)
        {
            int x = (int)(rect.X + rect.Width * percentX);
            int y = (int)(rect.Y + rect.Height * percentY);
            return new Point(x, y);
        }

        public static Point GetCenter(this Rectangle rect)
        {
            return GetPercentalPosition(rect, 0.5, 0.5);
        }

        public static double GetDistanceTo(this Point here, Point there)
        {
            return Math.Sqrt(Math.Pow(here.X - there.X, 2) + Math.Pow(here.Y - there.Y, 2));
        }
    }
}
