using System;
using System.Collections.Generic;
using System.Text;
using Utility;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using CreatorSupport;

namespace Primitives
{
    /// <summary>An IndexedFace is analagous to a polygon.</summary>
    public class IndexedFace
    {
        /// <summary>Gets the IndexedFaceSet that contains this IndexedFace</summary>
        public IndexedFaceSet ParentIndexedFaceSet { get; private set; }
        /// <summary>Gets the list of Edges that make up this IndexedFace</summary>
        public List<Edge> Edges { get; private set; }
        /// <summary>Gets the list of Vertices that define this IndexedFace in the order of the way they were defined in the data file to preserve frontface/backface</summary>
        public List<Vertex> Vertices { get; private set; }
        
        private Coord mNormalVector;
        private Coord mNormalVector_ModelingCoordinates;
        private bool mIsFrontFacing;
        private bool mIsTransparent;
        private Rectangle mBoundingBox;

        private static Pen sIntersectionTestPen = new Pen(Color.Black, 2f);

        /// <summary>
        /// Creates a new IndexedFace belonging to the specified IndexedFaceSet
        /// </summary>
        /// <param name="parentIndexedFaceSet">The IndexedFaceSet of which this IndexedFace is a part</param>
        internal IndexedFace(IndexedFaceSet parentIndexedFaceSet)
        {
            ParentIndexedFaceSet = parentIndexedFaceSet;
            Edges = new List<Edge>();
            Vertices = new List<Vertex>();
            mIsFrontFacing = true;
            mIsTransparent = false;
        }
        /// <summary>Gets the vector normal to this IndexedFace expressed in modeling coordinates</summary>
        public Coord NormalVector_ModelingCoordinates
        {
            get
            {
                return mNormalVector_ModelingCoordinates;
            }
        }
        /// <summary>Gets the vector normal to this IndexedFace expressed in view coordinates</summary>
        public Coord NormalVector
        {
            get
            {
                return mNormalVector;
            }
        }
        /// <summary>Gets a value indicating whether or not this IndexedFace is front-facing when expressed in view coordinates, meaning when looking at the Face, its Edges are in counter-clockwise order</summary>
        public bool IsFrontFacing
        {
            get
            {
                return mIsFrontFacing;
            }
        }
        /// <summary>Gets a value indicating whether or not this IndexedFace should be drawn</summary>
        public bool IsTransparent
        {
            get
            {
                return mIsTransparent;
            }
            internal set
            {
                mIsTransparent = value;
            }
        }
        /// <summary>Gets the box that bounds this IndexedFace when drawn to the screen.</summary>
        public Rectangle BoundingBox
        {
            get
            {
                return mBoundingBox;
            }
        }
        /// <summary>Gets the GraphicsPath that defines the edges of this IndexedFace in View Coordinates</summary>
        public GraphicsPath GraphicsPath
        {
            get
            {
                PointF[] fs = new PointF[Vertices.Count];
                byte[] ts = new byte[Vertices.Count];
                for (int i = 0; i < Vertices.Count; i++)
                {
                    fs[i] = Vertices[i].ViewCoord.ToPointF();
                    ts[i] = (byte)PathPointType.Line;
                }
                ts[0] = (byte)PathPointType.Start;
                GraphicsPath g = new GraphicsPath(fs, ts);
                g.CloseAllFigures();
                return g;
            }
        }

        /// <summary>Gets the GraphicsPath that defines the edges of this IndexedFace in Modeling Coordinates</summary>
        public GraphicsPath GraphicsPath_ModelingCoordinates
        {
            get
            {
                Coord axisUnitVector = NormalVector;
                Coord perpendicularUnitVector = axisUnitVector.CrossProduct((Vertices[1].ModelingCoord - Vertices[0].ModelingCoord).UnitVector); //parallel to plane

                PointF[] fs = new PointF[Vertices.Count];
                byte[] ts = new byte[Vertices.Count];
                for (int i = 0; i < Vertices.Count; i++)
                {
                    fs[i] = Transformer.ViewFromAxis(Vertices[i].ModelingCoord, axisUnitVector, perpendicularUnitVector);
                    ts[i] = (byte)PathPointType.Line;
                }
                ts[0] = (byte)PathPointType.Start;
                GraphicsPath g = new GraphicsPath(fs, ts);
                g.CloseAllFigures();
                return g;
            }
        }

        /// <summary>Gets the direction vector (in view coordinates) of the passed-in edge when viewed from this IndexedFace.</summary>
        public Coord GetDirectionVector(Edge e)
        {
            if (this == e.CreatorFace)
                return (e.EndVertex.ViewCoord - e.StartVertex.ViewCoord);
            else if (this == e.OtherFace)
                return (e.StartVertex.ViewCoord - e.EndVertex.ViewCoord);
            else
                throw new Exception("Failed to get direction vector. This Face is not one of supplied Edge's Faces.");
        }


        

        /// <summary>Returns true if the supplied Coord is contained within the on-screen projection of this IndexedFace. All Z values are ignored.</summary>
        public bool ContainsPoint2D(Coord c)
        {
            using (GraphicsPath g = GraphicsPath)
            {
                return g.IsVisible(c.ToPointF()) || g.IsOutlineVisible(c.ToPointF(), sIntersectionTestPen);
            }
        }

        /// <summary>Returns true if the supplied Modeling Coord is contained within the IndexedFace. After rotating, all Z values are ignored.</summary>
        public bool ContainsPoint2D_ModelingCoordinates(Coord c)
        {
            Coord axisUnitVector = NormalVector;
            Coord perpendicularUnitVector = axisUnitVector.CrossProduct((Vertices[1].ModelingCoord - Vertices[0].ModelingCoord).UnitVector); //parallel to plane

            GraphicsPath g = GraphicsPath_ModelingCoordinates;
            PointF cTransformed = Transformer.ViewFromAxis(c, axisUnitVector, perpendicularUnitVector);
            return (g.IsVisible(cTransformed));
        }
        

        /// <summary>Sets NormalVector_ModelingCoordinates. Only needs to be called once after adding all Vertices.</summary>
        internal void UpdateNormalVector_ModelingCoordinates()
        {
            //update the vector. don't just use the first three vertices - the polygon might have a convex edge there and the result will be wrong.
            mNormalVector_ModelingCoordinates = new Coord();
            for (int i = 0; i < Vertices.Count - 2; i++)
            {
                mNormalVector_ModelingCoordinates += (Vertices[i].ModelingCoord - Vertices[i + 1].ModelingCoord).CrossProduct(Vertices[i + 2].ModelingCoord - Vertices[i + 1].ModelingCoord);
            }
            int c = Vertices.Count;
            mNormalVector_ModelingCoordinates += (Vertices[c - 1].ModelingCoord - Vertices[c - 2].ModelingCoord).CrossProduct(Vertices[0].ModelingCoord - Vertices[c - 2].ModelingCoord);
            mNormalVector_ModelingCoordinates += (Vertices[c - 1].ModelingCoord - Vertices[0].ModelingCoord).CrossProduct(Vertices[1].ModelingCoord - Vertices[0].ModelingCoord);
            mNormalVector_ModelingCoordinates /= mNormalVector_ModelingCoordinates.Length;
        }
        /// <summary>Sets the NormalVector to reflect the current view of the IndexedFace on the screen. Automatically called from Refresh().</summary>
        internal void UpdateNormalVector()
        {
            //update the vector. don't just use the first three vertices - the polygon might have a convex edge there and the result will be wrong.
            mNormalVector = new Coord();
            for (int i = 0; i < Vertices.Count - 2; i++)
            {
                mNormalVector += (Vertices[i].ViewCoord - Vertices[i + 1].ViewCoord).CrossProduct(Vertices[i + 2].ViewCoord - Vertices[i + 1].ViewCoord);
            }
            int c = Vertices.Count;
            mNormalVector += (Vertices[c - 1].ViewCoord - Vertices[c - 2].ViewCoord).CrossProduct(Vertices[0].ViewCoord - Vertices[c - 2].ViewCoord);
            mNormalVector += (Vertices[c - 1].ViewCoord - Vertices[0].ViewCoord).CrossProduct(Vertices[1].ViewCoord - Vertices[0].ViewCoord);
            mNormalVector /= mNormalVector.Length;

            if (!mNormalVector.IsValid())
            {
                StringBuilder b = new StringBuilder();
                b.Append("Invalid Normal Vector: ").AppendLine(mNormalVector.ToString());
                b.AppendLine("Vertices: ");
                foreach (Vertex v in Vertices)
                    b.AppendLine(v.ViewCoord.ToString());
                System.Diagnostics.Debug.WriteLine(b.ToString());
                //throw new Exception(b.ToString());
            }
        }

        /// <summary>Updates the normal vector, sets IsFrontFacing and refreshes the bounding box</summary>
        internal void Refresh()
        {
            UpdateNormalVector();

            if (mIsTransparent || Edges.Any<Edge>(e => e.OtherFace == null))
            {
                mIsFrontFacing = true;
            }
            else
            {
                //the face may no longer be front-facing (or no longer back-facing)
                double dotProduct = NormalVector.DotProduct(new Coord(0, 0, 1));
                int dotSign = Math.Sign(dotProduct);
                mIsFrontFacing = (dotSign == 1);
            }
            //update the bounding box
            mBoundingBox = Helper.GetRectangleWithGivenCorners(Vertices[0].ViewCoord.ToPointD(), Vertices[1].ViewCoord.ToPointD());
            for (int i = 1; i < Vertices.Count; i++)
            {
                mBoundingBox = Rectangle.Union(mBoundingBox, Helper.GetRectangleWithGivenCorners(Vertices[i - 1].ViewCoord.ToPointD(), Vertices[i].ViewCoord.ToPointD()));
            }
        }

        /// <summary>
        /// Returns true if this IndexedFace is between the camera and the given point in view coordinates
        /// </summary>
        /// <param name="point_ViewCoordinates">The point to compare this IndexedFace to</param>
        /// <returns>True if this IndexedFace is between the camera and the given point in view coordinates</returns>
        public bool IsBetweenCameraAndPoint3D(Coord point_ViewCoordinates)
        {
            Coord cameraPoint = point_ViewCoordinates;
            cameraPoint.Z = 0; //comare to a point at the same location but on the screen.
            return IntersectsWith_ViewCoordinates(point_ViewCoordinates, cameraPoint);
        }
        /// <summary>
        /// Returns true of a line segment between the specified coordinates intersects with this IndexedFace in view coordinates
        /// </summary>
        /// <param name="point1">One endpoint of the line to test</param>
        /// <param name="point2">The other endpoint of the line to test</param>
        /// <returns>True of a line segment between the specified coordinates intersects with this IndexedFace in view coordinates</returns>
        private bool IntersectsWith_ViewCoordinates(Coord point1, Coord point2)
        {
            Coord c;
            return IntersectsWith(NormalVector, Vertices[0].ViewCoord, point1, point2, out c);
        }

        /// <summary>
        /// Returns true of a line segment between the specified coordinates intersects with this IndexedFace in modeling coordinates
        /// </summary>
        /// <param name="point1">One endpoint of the line to test</param>
        /// <param name="point2">The other endpoint of the line to test</param>
        /// <returns>True of a line segment between the specified coordinates intersects with this IndexedFace in modeling coordinates</returns>
        public bool IntersectsWith_ModelingCoordinates(Edge toIntersect, out Coord intersectionPoint_ModelingCoordinates)
        {
            return IntersectsWith(NormalVector_ModelingCoordinates, Vertices[0].ModelingCoord, toIntersect.StartVertex.ModelingCoord, toIntersect.EndVertex.ModelingCoord, out intersectionPoint_ModelingCoordinates);
        }

        /// <summary>
        /// Determines if and where a line segment intersects with a plane. Algorithm from http://local.wasp.uwa.edu.au/~pbourke/geometry/planeline/.
        /// </summary>
        /// <param name="normalVector">The plane's normal vector</param>
        /// <param name="pointOnFace">Any point on the plane</param>
        /// <param name="point1">One endpoint of the line to test</param>
        /// <param name="point2">The other endpoint of the line to test</param>
        /// <param name="intersectionPoint">If there is an intersection, this will be set to the point at which the intersection occurs</param>
        /// <returns>True if an intersection occurs.</returns>
        private static bool IntersectsWith(Coord normalVector, Coord pointOnFace, Coord point1, Coord point2, out Coord intersectionPoint)
        {
            intersectionPoint = new Coord();
            double uDenom = normalVector.DotProduct(point2 - point1);
            if (uDenom == 0) //the line from p2 to p1 is perpendicular to the plane's normal (i.e. parallel to plane)
                return false;

            double uNum = normalVector.DotProduct(pointOnFace - point1);
            double u = uNum / uDenom;
            if (Helper.IsBetween0And1(u))
            {
                //P = P1 + u (P2 - P1)
                intersectionPoint = point1 + u * (point2 - point1);
                return true;
            }
            else
                return false;
        }
    }
}
