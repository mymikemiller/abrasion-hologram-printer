using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using System.Drawing;

namespace Primitives
{
    /// <summary>Represents a section of an Edge with a constant QI value. Some Edges are made entirely of one EdgeSection, some are split into multiple EdgeSections if they go behind silhouette edges, etc.</summary>
    public class EdgeSection
    {
        private Rectangle mBoundingBox = Rectangle.Empty;

        /// <summary>Specifies whether or not this EdgeSection is visible and thus should be drawn.</summary>
        public bool Visible { get; set; }

        /// <summary>The Edge that this EdgeSection is a part of.</summary>
        public Edge Edge { get; private set; }

        /// <summary>Gets the position along this EdgeSection's Edge that this EdgeSection starts. Expressed as a fraction between 0 and 1.</summary>
        public double StartPosition { get; private set; }
        /// <summary>Gets the position along this EdgeSection's Edge that this EdgeSection ends. Expressed as a fraction between 0 and 1.</summary>
        public double EndPosition { get; private set; }
        
        /// <summary>Gets the 3D Coord value that begins this EdgeSection</summary>
        public Coord StartCoord { get; private set; }
        /// <summary>Gets the 3D Coord value that ends this EdgeSection</summary>
        public Coord EndCoord { get; private set; }
        /// <summary>Gets the 3D Coord value in the middle of this EdgeSection</summary>
        public Coord MidCoord { get; private set; }

        /// <summary>
        /// Creates a new EdgeSection for the specified Edge.
        /// </summary>
        /// <param name="e">The Edge that contains this EdgeSection</param>
        /// <param name="startPosition">The position (expressed as a fraction between 0 and 1) along the supplied Edge that this EdgeSection starts</param>
        /// <param name="endPosition">The position (expressed as a fraction between 0 and 1) along the supplied Edge that this EdgeSection ends</param>
        public EdgeSection(Edge e, double startPosition, double endPosition)
        {
            Edge = e;
            StartPosition = startPosition;
            EndPosition = endPosition;

            Coord diff = Edge.EndVertex.ViewCoord - Edge.StartVertex.ViewCoord;
            StartCoord = Edge.StartVertex.ViewCoord + diff * StartPosition;
            EndCoord = Edge.StartVertex.ViewCoord + diff * EndPosition;
            MidCoord = (StartCoord + EndCoord) / 2;
        }


        
    }
}
