using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Primitives;
using Utility;

namespace Primitives
{
    /// <summary>
    /// Represents a Coord that exists along an Edge. Retains information as to how far along the Edge this cord is.
    /// </summary>
    public class EdgeCoord
    {
        /// <summary>Gets the Edge that this EdgeCoord belongs to</summary>
        public Edge Edge { get; private set; }
        /// <summary>Gets the Position of this Coord, expressed as a fraction (between 0 and 1) along the Edge</summary>
        public double Position { get; private set; }

        /// <summary>
        /// Creates a new EdgeCoord specifying how far along the parent Edge's this EdgeCoord exists.
        /// </summary>
        /// <param name="position">Specifying how far along the parent Edge's this EdgeCoord exists. Expressed as a value between 0 and 1.</param>
        public EdgeCoord(Edge edge, double position)
        {
            Edge = edge;
            Position = position;
        }

        /// <summary>Gets the View Coord to be drawn</summary>
        public Coord Coord
        {
            get
            {
                return Edge.StartVertex.ViewCoord + (Edge.EndVertex.ViewCoord - Edge.StartVertex.ViewCoord) * Position;
            }
        }
        /// <summary>Gets the View Coord that would be drawn if being viewed at Zero Angle</summary>
        public Coord Coord_ZeroAngle
        {
            get
            {
                return Edge.StartVertex.ViewCoord_ZeroAngle + (Edge.EndVertex.ViewCoord_ZeroAngle - Edge.StartVertex.ViewCoord_ZeroAngle) * Position;
            }
        }

        /// <summary>Returns a value indicating whether or not this EdgeCoord is visible at the current ViewAngle</summary>
        /// <returns>True if this EdgeCoord is visible at the current ViewAngle</returns>
        public bool IsVisible()
        {
            foreach (EdgeSection es in Edge.VisibleEdgeSections)
            {
                if (Position >= es.StartPosition && Position <= es.EndPosition)
                    return true;
            }
            return false;
        }

        /// <summary>Compares this EdgeCoord to every IndexedFace in the supplied ShapeList to determine its visibility.</summary>
        public bool IsVisible_Slow(ShapeList shapeList)
        {
            return !IsBehindIndexedFace(shapeList);
        }

        /// <summary>
        /// Gets a value indicating if this EdgeCorod is behind any IndexedFace in the supplied ShapeList
        /// </summary>
        /// <param name="shapeList">The ShapeList which contains the IndexedFaces to compare this EdgeCoord to</param>
        /// <returns>True if this EdgeCorod is behind any IndexedFace in the supplied ShapeList</returns>
        private bool IsBehindIndexedFace(ShapeList shapeList)
        {
            foreach (IndexedFace ifc in shapeList.IndexedFaces)
            {
                if (!Edge.ContainsFace(ifc)) //only check against faces that this EdgeCoord's Edge is not a part of
                {
                    bool isVertexOfCurrentFace = false;
                    foreach (Edge e in ifc.Edges)
                    {
                        if (e.StartVertex.ViewCoord == Coord || e.EndVertex.ViewCoord == Coord)
                        {
                            isVertexOfCurrentFace = true;
                            break;
                        }
                    }
                    if (!isVertexOfCurrentFace)
                    {
                        if (ifc.BoundingBox.Contains(Coord.ToPoint())) //if the bounding box doesn't contain the point, it must be outside of the polygon.
                        {
                            if (ifc.ContainsPoint2D(Coord))
                            {
                                if (ifc.IsBetweenCameraAndPoint3D(Coord))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}
