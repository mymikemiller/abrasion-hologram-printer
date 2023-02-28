using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using System.Drawing;

namespace Primitives
{ 
    /// <summary>Specifies the classification of the ViewEdge.</summary>
    public enum EdgeType
    {
        /// <summary>Specifies that the Edge connects two FrontFacing IndexedFaces</summary>
        FrontFacing,
        /// <summary>Specifies that the Edge connects two BackFacing IndexedFaces</summary>
        BackFacing,
        /// <summary>Specifies that the Edge connects a FrontFacing and a BackFacing IndexedFace</summary>
        Silhouette
    }

    /// <summary>Specifies whether or not this Edge connects two IndexedFaces, and if so, whether the Edge connection is Internal or External.</summary>
    public enum ConnectionType
    {
        /// <summary>Specifies that this Edge does not connect two IndexedFaces.</summary>
        None,
        /// <summary>Specifies that this Edge is an Internal Edge, meaning the angle between the IndexedFaces' NormalVectors is less than 180. Example: a room's corner viewed from the inside.</summary>
        Internal,
        /// <summary>Specifies that this Edge is an External Edge, meaning the angle between the IndexedFaces' NormalVectors is 180 or greater. Example: a house's corner viewed from the outside.</summary>
        External
    }

    /// <summary>Represents the edge shared by two IndexedFaces.</summary>
    public class Edge
    {
        /// <summary>Gets the IndexedFace that created this Edge. Null for Auxiliary Faces. Knowing which Face created the Edge helps to make sense of StartVertex and EndVertex: From CreatorFace's point of view, StartVertex and EndVertex are in counter-clockwise order.</summary>
        public IndexedFace CreatorFace { get; private set; }
        /// <summary>Gets the Face that did not create this Edge. From OtherFace's point of view, StartVertex and EndVertex are in clockwise order instead of counter-clockwise as would be expected when looking from the point of view of the CreatorFace.</summary>
        public IndexedFace OtherFace { get; private set; }
        /// <summary>Gets the first Vertex supplied in the creation of this Edge. From CreatorFace's point of view, StartVertex comes before EndVertex in counter-clockwise order.</summary>
        public Vertex StartVertex { get; private set; }
        /// <summary>Gets the second Vertex supplied in the creation of this Edge. From CreatorFace's point of view, EndVertex comes after StartVertex in counter-clockwise order.</summary>
        public Vertex EndVertex { get; private set; }

        /// <summary>Specifies the classification of the ViewEdge.</summary>
        public EdgeType Type { get; private set; }
        /// <summary>Specifies whether or not this Edge connects two IndexedFaces, and if so, whether the Edge connection is Internal or External.</summary>
        public ConnectionType ConnectionType { get; private set; }

        /// <summary>Gets and sets the List of EdgeSections that make up this Edge. The StartVertex of the first EdgeSection is always either the Edge's StartVertex or EndVertex, depending on the direction of travel through the Edge. The EndVertex of the last EdgeSection is always this Edge's Vertex that is not the first EdgeSection's StartVertex.</summary>
        public List<EdgeSection> EdgeSections { get; set; }
        /// <summary>Gets the list EdgeSections making up this edge which are Visible. May be empty.</summary>
        public IEnumerable<EdgeSection> VisibleEdgeSections { get { return EdgeSections.Where(edgeSection => edgeSection.Visible); } }

        /// <summary>A List of all the coordinates along this Edge where it intersects with any IndexedFace. When splitting this Edge into EdgeSections, this list of Intersections is always used in addition to any Silhouette Edge intersections.</summary>
        public List<Intersection> FaceIntersections { get; set; }

        /// <summary>Gets the Rectangle that bounds this Edge when drawn on the sceen (the Vertices' ViewCoords are used).</summary>
        public Rectangle BoundingBox { get; private set; }

        /// <summary>Gets and Sets the ID for this Edge, used to uniquely identify each Edge.</summary>
        public int EdgeID { get; set; }

        /// <summary>Creates a new Edge.</summary>
        /// <param name="startVertex">The Vertex at one side of the Edge.</param>
        /// <param name="endVertex">The Vertex at the other side of the Edge.</param>
        /// <param name="creatorFace">The Face for which this Edge was created.</param>
        public Edge(Vertex startVertex, Vertex endVertex, IndexedFace creatorFace)
        {
            StartVertex = startVertex;
            EndVertex = endVertex;
            CreatorFace = creatorFace;
            OtherFace = null;
            Type = EdgeType.FrontFacing; //Temporarily set to FrontFacing - its Type will be updated on Refresh().            
            EdgeSections = new List<EdgeSection>();
            FaceIntersections = new List<Intersection>();
            ConnectionType = ConnectionType.None;
        }

        /// <summary>Sets OtherFace to be the passed-in IndexedFace</summary>
        public void AddFace(IndexedFace face)
        {
            if (OtherFace != null)
                throw new Exception("Cannot set OtherFace on an Edge which already has an OtherFace");
            OtherFace = face;
        }
        /// <summary>Updates the ConnectionType of this Edge to accurately reflect the two IndexedFaces that it connects. No update is made if this Edge does not have an OtherFace. The ConnectionType never changes once set, so it is only necessary to call this function once. Make sure both IndexedFaces have their NormalVectors updated before calling this function.</summary>
        public void UpdateConnectionType()
        {
            if (OtherFace != null)
            {
                double val = (EndVertex.ModelingCoord - StartVertex.ModelingCoord).DotProduct(CreatorFace.NormalVector.CrossProduct(OtherFace.NormalVector));

                ConnectionType = Math.Sign(val) == 1 ? ConnectionType.External : ConnectionType.Internal;
            }
        }
        /// <summary>Refreshes the bounding box for this Edge</summary>
        public void RefreshBoundingBox()
        {
            BoundingBox = Helper.GetRectangleWithGivenCorners(StartVertex.ViewCoord.ToPointD(), EndVertex.ViewCoord.ToPointD());
        }

        /// <summary>Returns true if this Edge has Vertices with the same VertexIndex values as the passed in Edge. The order of the Vertices is ignored.</summary>
        public bool ContainsSameVerticesAs(Edge rhs)
        {
            return ((rhs.StartVertex.VertexIndex == this.StartVertex.VertexIndex && rhs.EndVertex.VertexIndex == this.EndVertex.VertexIndex) || (rhs.EndVertex.VertexIndex == this.StartVertex.VertexIndex && rhs.StartVertex.VertexIndex == this.EndVertex.VertexIndex));
        }

        /// <summary>Returns true if the specified Vertex is either the StartVertex or EndVertex of this Edge.</summary>
        public bool ContainsVertex(Vertex v)
        {
            return (v == StartVertex || v == EndVertex);
        }
        /// <summary>Returns true if the specified IndexedFace is either the CreatorFace or OtherFace of this Edge.</summary>
        public bool ContainsFace(IndexedFace ifc)
        {
            return (CreatorFace == ifc || OtherFace == ifc);
        }

        /// <summary>Returns the length of this Edge in modeling coordinates</summary>
        public double Length_ModelingCoordinates
        {
            get
            {
                return (EndVertex.ModelingCoord - StartVertex.ModelingCoord).Length;
            }
        }
        /// <summary>Returns the length of this Edge in view coordinates</summary>
        public double Length_ViewCoordinates
        {
            get
            {
                return (EndVertex.ViewCoord - StartVertex.ViewCoord).Length;
            }
        }

        /// <summary>Returns true if this Edge intersects the supplied Edge when drawn on the screen. ViewCoords are used. If there is a 2D projected intersection, the Z values at the intersection are compared. Returns True only if the Z value for the the SilhouetteEdge indicates that it is in front of this Edge.</summary>
        /// <param name="silhouetteEdge">The Edge to check against.</param>
        ///<param name="intersectionPosition">If there is an intersection, this value will be set to a number between 0 and 1 corresponding to the position along this Edge that the intersection occurs.</param>
        /// <returns>True if there was an intersection. Does not count intersections at the endpoints of the Edge.</returns>
        public bool IntersectsBehind(Edge silhouetteEdge, out double intersectionPosition)
        {
            intersectionPosition = 0;

            if (!silhouetteEdge.BoundingBox.IntersectsWith(BoundingBox)) //if the BoundingBoxes don't intersect, then the Edges don't intersect
                return false;

            double a = this.StartVertex.ViewCoord.X;
            double f = this.StartVertex.ViewCoord.Y;
            double b = this.EndVertex.ViewCoord.X;
            double g = this.EndVertex.ViewCoord.Y;

            double c = silhouetteEdge.StartVertex.ViewCoord.X;
            double k = silhouetteEdge.StartVertex.ViewCoord.Y;
            double d = silhouetteEdge.EndVertex.ViewCoord.X;
            double l = silhouetteEdge.EndVertex.ViewCoord.Y;

            //Normalization technique will be used to determine if the lines intersect.
            //The following equations will be solved for t and u. If t and u are both between 0 and 1, the lines intersect
            // x1+(x2-x1)t = x3+(x4-x3)u
            // y1+(y2-y1)t = y3+(y4-y3)u
            double den = (a * (k - l) - b * (k - l) - (c - d) * (f - g));
            intersectionPosition = (a * (k - l) - c * (f - l) + d * (f - k)) / den;
            double intersectionPosition_silhouetteEdge = -(a * (g - k) - b * (f - k) + c * (f - g)) / den;

            if (Helper.IsBetween0And1(intersectionPosition) && Helper.IsBetween0And1(intersectionPosition_silhouetteEdge))
            {
                //They intersect, but we only care about the intersection if this Edge is behind the silhouetteEdge
                double zi = this.StartVertex.ViewCoord.Z + (this.EndVertex.ViewCoord.Z - this.StartVertex.ViewCoord.Z) * intersectionPosition;
                double ziSilhouette = silhouetteEdge.StartVertex.ViewCoord.Z + (silhouetteEdge.EndVertex.ViewCoord.Z - silhouetteEdge.StartVertex.ViewCoord.Z) * intersectionPosition_silhouetteEdge;
                if (zi > ziSilhouette) //if zi > ziSilhouette, the Silhouette Edge is behind this Edge, and thus the intersection can be ignored.
                    return false;
                else
                    return true;
            }
            else 
                return false;
        }

        /// <summary>Enumerates through the Coords that make up this Edge.</summary>
        /// <param name="coordsPerUnitLength">The number of coords per unit modeling-coordinate length</param>
        /// <returns>The Coords that make up this Edge</returns>
        public IEnumerable<EdgeCoord> GetCoords(double coordsPerUnitLength)
        {
            //Split the Edge up into coords
            int numPoints = GetNumCoords(coordsPerUnitLength);

            for (int i = 0; i < numPoints; i++)
            {
                double fraction = i / (double)(numPoints - 1);
                EdgeCoord ec = new EdgeCoord(this, fraction);
                yield return ec;
            }
        }

        /// <summary>Gets the number of Coords that make up this Edge</summary>
        /// <param name="coordsPerUnitLength">The number of coords per unit modeling-coordinate length</param>
        /// <returns>The number of Coords that make up this Edge</returns>
        public int GetNumCoords(double coordsPerUnitLength)
        {
            return Math.Max(2, (int)(coordsPerUnitLength * Length_ModelingCoordinates));
        }



        /// <summary>Refreshes this Edge so that its Type accurately reflects the Types of its Faces.</summary>
        internal void Refresh()
        {
            //todo: check validity of what happens when there's only one edge
            if (OtherFace == null)
                Type = EdgeType.Silhouette;
            else if (CreatorFace.IsFrontFacing && OtherFace.IsFrontFacing)
                Type = EdgeType.FrontFacing;
            else if (!CreatorFace.IsFrontFacing && !OtherFace.IsFrontFacing)
                Type = EdgeType.BackFacing;
            else
                Type = EdgeType.Silhouette;
        }

        /// <summary>Returns the Vertex that is not the supplied Vertex. An Exception is thrown if the supplied Vertex is not either the StartVertex or EndVertex of this Edge.</summary>
        public Vertex GetOtherVertex(Vertex unwantedVertex)
        {
            if (unwantedVertex == StartVertex)
                return EndVertex;
            else if (unwantedVertex == EndVertex)
                return StartVertex;
            else
                throw new Exception("Can not Get Other Vertex because the supplied unwantedVertex is not part of this Edge.");
        }
    }
}
