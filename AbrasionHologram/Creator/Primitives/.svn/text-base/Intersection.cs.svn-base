using System;
using System.Collections.Generic;
using System.Text;
using Utility;

namespace Primitives
{
    /// <summary>Represents the point at which an Edge intersects behind another Edge, or at which an Edge intersects an IndexedFace</summary>
    public class Intersection
    {
        /// <summary>Gets and sets the Edge which is involved in this Intersection</summary>
        public Edge Edge { get; set; }
        /// <summary>The position (0.0 to 1.0) along the Edge at which this Intersection occurs</summary>
        public double Position { get; set; }

        /// <summary>Creates a new Intersection object with the specified Edge and distance</summary>
        /// <param name="edge">The Edge involved in this Intersection</param>
        /// <param name="distanceFromStart">he position (0.0 to 1.0) along the Edge at which this Intersection occurs</param>
        public Intersection(Edge edge, double distanceFromStart)
        {
            Edge = edge;
            Position = distanceFromStart;
        }
    }
}
