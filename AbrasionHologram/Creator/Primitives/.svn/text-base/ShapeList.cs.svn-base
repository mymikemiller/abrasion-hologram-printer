using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using Primitives;

namespace Primitives
{
    /// <summary>
    /// Represents a collection of IndexedFaceSets. All IndexedFaces can be enumerated through the IndexedFaces property, and all Edges in each of those IndexedFaces can be enumerated through the Edges attribute.
    /// </summary>
    public class ShapeList
    {
        /// <summary>Gets and sets the list of IndexedFaceSets that make up this ShapeList</summary>
        public List<IndexedFaceSet> IndexedFaceSets { get; set; }

        public bool Dirty { get; set; }

        /// <summary>Creates an empty ShapeList</summary>
        public ShapeList()
        {
            IndexedFaceSets = new List<IndexedFaceSet>();
            Dirty = true;
        }

        public int Count { get { return IndexedFaceSets.Count; } }

        /// <summary>Calls Refresh() on each of the IndexedFaceSets in this ShapeList (to update the AvailableViewVertexLocations for each to match the latest transform matrix).</summary>
        public void Refresh()
        {
            foreach (IndexedFaceSet ifs in IndexedFaceSets)
            {
                ifs.Refresh();
            }
            //BoundingBoxes must be refreshed after refreshing all the IndexedFaceSets. Otherwise, the BoundingBox for Auxiliary Edges won't be accurate.
            foreach (IndexedFaceSet ifs in IndexedFaceSets)
            {
                foreach (Edge e in ifs.Edges)
                {
                    e.RefreshBoundingBox();
                }
            }

            Dirty = false;
        }

        /// <summary>Enumerates through every IndexedFace in this ShapeList</summary>
        public IEnumerable<IndexedFace> IndexedFaces
        {
            get
            {
                if (Dirty)
                    Refresh();

                foreach (IndexedFaceSet ifs in IndexedFaceSets)
                    foreach (IndexedFace ifc in ifs.IndexedFaces)
                        yield return ifc;
            }
        }

        /// <summary>Enumerates through every Edge in this ShapeList</summary>
        public IEnumerable<Edge> Edges
        {
            get
            {
                if (Dirty)
                    Refresh();

                foreach (IndexedFaceSet ifs in IndexedFaceSets)
                    foreach (IndexedFace ifc in ifs.IndexedFaces)
                        foreach (Edge e in ifc.Edges)
                            yield return e;
            }
        }

        /// <summary>Gets the number of Edges in this ShapeList</summary>
        public int EdgeCount
        {
            get
            {
                int total = 0;
                foreach (IndexedFaceSet ifs in IndexedFaceSets)
                    foreach (IndexedFace ifc in ifs.IndexedFaces)
                        total += ifc.Edges.Count;
                return total;
            }
        }
    }
}
