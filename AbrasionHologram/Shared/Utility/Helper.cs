using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Utility
{
    public static class Helper
    {
        public static bool DesignMode { get; set; }

        static Helper()
        {
            DesignMode = true;
        }

        /// <summary>Returns true if the specified double is between 0 and 1.</summary>
        public static bool IsBetween0And1(double normalizedValue)
        {
            return (normalizedValue >= 0 && normalizedValue <= 1);
        }

        public static Rectangle GetRectangleWithGivenCorners(PointD p1, PointD p2)
        {
            return GetRectangleWithGivenCorners(p1.ToPoint(), p2.ToPoint());
        }
        public static Rectangle GetRectangleWithGivenCorners(Point p1, Point p2)
        {
            int left = Math.Min(p1.X, p2.X);
            int right = Math.Max(p1.X, p2.X);
            int top = Math.Min(p1.Y, p2.Y);
            int bottom = Math.Max(p1.Y, p2.Y);
            return System.Drawing.Rectangle.FromLTRB(left, top, right, bottom);
        }

    }
}
