using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DrawableObjects
{
    /// <summary>
    /// Represents a Line which can be drawn to the screen.
    /// </summary>
    public struct Line
    {
        /// <summary>
        /// Gets or sets the start point.
        /// </summary>
        /// <value>The start point.</value>
        public PointF StartPoint { get; private set; }
        /// <summary>
        /// Gets or sets the end point.
        /// </summary>
        /// <value>The end point.</value>
        public PointF EndPoint { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> struct.
        /// </summary>
        /// <param name="startPoint">The start point.</param>
        /// <param name="endPoint">The end point.</param>
        public Line(PointF startPoint, PointF endPoint)
            : this()
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        /// <summary>
        /// Draws the specified graphics object
        /// </summary>
        /// <param name="g">The graphics object.</param>
        /// <param name="p">The pen to use when drawing the Line.</param>
        /// <param name="horizontalOffset">The horizontal offset.</param>
        public void Draw(Graphics g, Pen p, float horizontalOffset)
        {
            g.DrawLine(p, StartPoint.X + horizontalOffset, StartPoint.Y, EndPoint.X + horizontalOffset, EndPoint.Y);
        }
    }
}
