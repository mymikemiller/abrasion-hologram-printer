using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Primitives;
using System.Drawing;
using Utility;
using System.Collections;
using CreatorSupport;

namespace Creator
{
    /// <summary>A static class used to traverse Edges of IndexedFaceSets and extract visible EdgeSections when in HiddenLine mode.</summary>
    internal static class EdgeTraverser
    {
        private static ShapeList mShapeList;
        public static bool Dirty { get; set; }

        /// <summary>
        /// Initializes the static variables of the <see cref="EdgeTraverser"/> class.
        /// </summary>
        static EdgeTraverser()
        {
            Dirty = true;
        }

        /// <summary>
        /// Gets or sets the shape list to be traversed by this EdgeTraverser.
        /// </summary>
        /// <value>The shape list.</value>
        public static ShapeList ShapeList
        {
            get { return mShapeList; }
            set
            {
                if (mShapeList == value)
                    return;

                mShapeList = value;
                Dirty = true;
            }
        }

        /// <summary>
        /// Gets the visible edge sections.
        /// </summary>
        /// <value>The visible edge sections.</value>
        internal static IEnumerable<EdgeSection> VisibleEdgeSections
        {
            get
            {
                if (mShapeList == null)
                    throw new Exception("Please set EdgeTraverser's ShapeList before attempting to access VisibleEdgeSections");

                if (Dirty)
                    TraverseEdges();

                foreach (Edge e in mShapeList.Edges)
                    foreach (EdgeSection es in e.VisibleEdgeSections)
                        yield return es;
            }
        }

        /// <summary>
        /// Traverses the edges, adding elements to the VisibleEdgeSections list.
        /// </summary>
        private static void TraverseEdges()
        {
            foreach (Edge e in mShapeList.Edges)
            {
                ProcessEdge(e);
            }
            Dirty = false;
        }

        /// <summary>Splits the edge up into sections with constant visibility by determining if and where it intersects any of the ShapeList's Silhouette Edges.</summary>
        private static void ProcessEdge(Edge e)
        {
            e.EdgeSections.Clear();

            if (e.Type == EdgeType.FrontFacing || e.Type == EdgeType.Silhouette)
            {
                //initialize the intersections list with the face intersections.
                List<Intersection> intersections = new List<Intersection>(e.FaceIntersections);

                //loop through every Silhouette Edge in the ShapeList
                foreach (Edge edgeToCheck in mShapeList.Edges.Where(silhouetteEdge => silhouetteEdge.Type == EdgeType.Silhouette))
                {
                    if (e != edgeToCheck) //don't compare against itself
                    {
                        //Check for intersection
                        double intersectionPosition; //will be between 0 and 1

                        if (e.IntersectsBehind(edgeToCheck, out intersectionPosition))
                            intersections.Add(new Intersection(e, intersectionPosition));
                    }
                }

                //we now know the location of every intersection with a Silhouette Edge that is in front of the Edge being processed.
                //the list needs to be ordered in the direction of travel so we can appropriately add the EdgeSections to the Edge's list.

                //Separate the Edge into the appropriate EdgeSections ordered by increasing distance from the e.StartVertex.
                IOrderedEnumerable<Intersection> orderedIntersections = intersections.OrderBy<Intersection, double>(i => i.Position);

                double lastPosition = 0;
                EdgeSection es;
                foreach (Intersection si in orderedIntersections)
                {
                    es = new EdgeSection(e, lastPosition, si.Position);
                    ComputeVisibility(es);
                    e.EdgeSections.Add(es);
                    lastPosition = si.Position;
                }
                es = new EdgeSection(e, lastPosition, 1);
                ComputeVisibility(es);
                e.EdgeSections.Add(es);
            }
        }

        /// <summary>Appropriately sets the Visible property of the specified EdgeSection.</summary>
        private static void ComputeVisibility(EdgeSection es)
        {
            Coord testPoint = es.MidCoord; //test visibility of the midpoint of the EdgeSection

            foreach (IndexedFace face in mShapeList.IndexedFaces.Where(iface => !iface.IsTransparent && iface.IsFrontFacing)) //only check against front-facing faces
            {
                if (!es.Edge.ContainsFace(face)) //only check against faces that this EdgeSection's Edge is not a part of
                {
                    if (face.BoundingBox.Contains(testPoint.ToPoint())) //if the bounding box doesn't contain the point, it must be outside of the polygon.
                    {
                        if (face.ContainsPoint2D(testPoint))
                        {
                            if (face.IsBetweenCameraAndPoint3D(testPoint))
                            {
                                es.Visible = false;
                                return;
                            }
                        }
                    }
                }
            }
            es.Visible = true;
        }

        /// <summary>
        /// Forces the traversal, even if the EdgeTraverser is not dirty.
        /// </summary>
        internal static void ForceTraversal()
        {
            TraverseEdges();
        }
    }
}
