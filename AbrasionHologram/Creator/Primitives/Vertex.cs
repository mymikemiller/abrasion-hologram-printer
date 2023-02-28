using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using CreatorSupport;

namespace Primitives
{
    /// <summary>Represents a vertex adjoining (generally) three or more IndexedFaces by way of being the end-point for the Edges connecting those IndexedFaces.</summary>
    public class Vertex
    {
        /// <summary>Specifies the index into the parent IndexFaceSet's AvailableVertexLocations and AvailableViewVertexLocations lists that this Vertex's location is stored.</summary>
        public int VertexIndex { get; private set; }

        /// <summary>The list of Edges that this Vertex is a part of. This list is initially blank. Edgest must be added to it after initialization.</summary>
        public List<Edge> Edges { get; private set; }

        /// <summary>The list of IndexedFaces that this Vertex is a part of.</summary>
        public List<IndexedFace> IndexedFaces { get; private set; }

        /// <summary>The IndexedFaceSet that this Vertex is a part of.</summary>
        public IndexedFaceSet ParentIndexedFaceSet { get; private set; }

        /// <summary>
        /// Creates a new Vertex.
        /// </summary>
        /// <param name="parentIndexedFaceSet">The IndexedFaceSet containing this Vertex</param>
        /// <param name="vertexIndex">The index into the parent IndexedFaceSet's AvailableVertexLocations and AvailableViewVertexLocations lists that this Vertex's location is stored</param>
        public Vertex(IndexedFaceSet parentIndexedFaceSet, int vertexIndex)
        {
            ParentIndexedFaceSet = parentIndexedFaceSet;
            VertexIndex = vertexIndex;
            Edges = new List<Edge>();
            IndexedFaces = new List<IndexedFace>();
        }

        /// <summary>Gets the modeling coordinate for this Vertex</summary>
        public Coord ModelingCoord
        {
            get
            {
                return ParentIndexedFaceSet.AvailableVertexLocations[VertexIndex];
            }
        }
        /// <summary>Gets the view coordinate for this Vertex</summary>
        public Coord ViewCoord
        {
            get
            {
                return Transformer.GetArcCoord(ParentIndexedFaceSet.AvailableViewVertexLocations_ZeroAngle[VertexIndex]);
            }
        }
        /// <summary>Gets the view coordinate for this Vector when viewed at zero angle</summary>
        public Coord ViewCoord_ZeroAngle
        {
            get
            {
                return ParentIndexedFaceSet.AvailableViewVertexLocations_ZeroAngle[VertexIndex];
            }
        }
    }
}
