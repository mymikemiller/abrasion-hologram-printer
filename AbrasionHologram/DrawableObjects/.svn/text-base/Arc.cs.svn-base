using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DrawableObjects
{
    /// <summary>
    /// Defines a single Arc on the screen, including StartAngle and Sweep. Able to draw itself onto a specified Graphics object.
    /// </summary>
    public struct Arc
    {
        /// <summary>
        /// Gets or sets the square bounding the circle defining this Arc
        /// </summary>
        /// <value>The arc square.</value>
        public RectangleF ArcSquare { get; private set; }
        /// <summary>
        /// Gets or sets the start angle for the arc. Specified in degrees with 0 being along the positive X axis and increasing counterclockwise.
        /// </summary>
        /// <value>The start angle.</value>
        public float StartAngle { get; private set; }
        /// <summary>
        /// Gets or sets the amount of clockwise sweep to use when drawing the Arc.
        /// </summary>
        /// <value>The sweep.</value>
        public float Sweep { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Arc"/> struct.
        /// </summary>
        /// <param name="arcSquare">The arc square.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweep">The sweep.</param>
        public Arc(RectangleF arcSquare, float startAngle, float sweep)
            : this()
        {
            ArcSquare = arcSquare;
            StartAngle = startAngle;
            Sweep = sweep;
        }

        /// <summary>
        /// Draws the Arc to the specified graphics object with the specified horizontal offset.
        /// </summary>
        /// <param name="g">The graphics object.</param>
        /// <param name="horizontalOffset">The horizontal offset.</param>
        public void Draw(Graphics g, float horizontalOffset)
        {
            g.DrawArc(Pens.Gray, ArcSquare.X + horizontalOffset, ArcSquare.Y, ArcSquare.Width, ArcSquare.Height, StartAngle, Sweep);
        }
    }
}
