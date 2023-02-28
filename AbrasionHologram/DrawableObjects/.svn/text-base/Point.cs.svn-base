using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DrawableObjects
{
    /// <summary>
    /// Represents a Point that can be drawn to the screen
    /// </summary>
    public struct Point
    {
        /// <summary>
        /// Gets or sets the underlying point.
        /// </summary>
        /// <value>The underlying point.</value>
        public PointF UnderlyingPoint { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> struct.
        /// </summary>
        /// <param name="underlyingPoint">The underlying point.</param>
        public Point(PointF underlyingPoint)
            : this()
        {
            UnderlyingPoint = underlyingPoint;
        }

        /// <summary>
        /// Draws this Point to the specified graphics object.
        /// </summary>
        /// <param name="g">The graphics object to draw on.</param>
        /// <param name="radius">The radius of the Point (Points are drawn as circles).</param>
        /// <param name="b">The brush to use when drawing the Point.</param>
        /// <param name="horizontalOffset">The horizontal offset.</param>
        public void Draw(Graphics g, float radius, Brush b, float horizontalOffset)
        {
            g.FillEllipse(b, UnderlyingPoint.X - (radius / 2) + horizontalOffset, UnderlyingPoint.Y - (radius / 2), radius, radius);
        }
    }
}
