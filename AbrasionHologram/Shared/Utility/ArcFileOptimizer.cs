using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Utility
{
    /// <summary>
    /// Provides methods to optimize the order of ArcSections within an ArcFile.
    /// </summary>
    public static class ArcFileOptimizer
    {
        private static double mXSpeedSquared = 1;
        private static double mYSpeedSquared = 1;
        private static double mRSpeedSquared = 1;

        /// <summary>Gets and sets the relative speed of the X axis</summary>
        public static double XSpeed { get { return Math.Sqrt(mXSpeedSquared); } set { mXSpeedSquared = value * value; } }
        /// <summary>Gets and sets the relative speed of the Y axis</summary>
        public static double YSpeed { get { return Math.Sqrt(mYSpeedSquared); } set { mYSpeedSquared = value * value; } }
        /// <summary>Gets and sets the relative speed of the Radial axis</summary>
        public static double RSpeed { get { return Math.Sqrt(mRSpeedSquared); } set { mRSpeedSquared = value * value; } }

        /// <summary>
        /// Optimizes the order of ArcSections within the given ArcFile
        /// </summary>
        /// <param name="source">The ArcFile to optimize</param>
        /// <returns>The ordered list of ArcSections</returns>
        public static List<ArcSection> Optimize(ArcFile source)
        {
            List<ArcSection> optimizedList = new List<ArcSection>(source.ArcSections.Count);

            LinkedList<ArcSection> unAdded = new LinkedList<ArcSection>(source.ArcSections);


            //Start by finding the ArcSection with x and y closest to the center of the canvas
            ArcSection center = new ArcSection(new PointD(source.CanvasSize.Width / 2, source.CanvasSize.Height / 2), 1, 45, -45);
            
            LinkedListNode<ArcSection> currentNearestNode = unAdded.First;
            double currentNearestWeightedDistance = double.MaxValue;

            LinkedListNode<ArcSection> node = unAdded.First.Next;
            while (node != null)
            {
                center.Radius = node.Value.Radius; //we're only comparing x and y, so make sure the radius is the same
                double currentWeightedDistance = WeightedDistance(center, node.Value);
                if (currentWeightedDistance < currentNearestWeightedDistance)
                {
                    currentNearestNode = node;
                    currentNearestWeightedDistance = currentWeightedDistance;
                }
                node = node.Next;
            }

            optimizedList.Add(currentNearestNode.Value);
            unAdded.Remove(currentNearestNode);


            while (unAdded.Count > 0)
            {
                currentNearestWeightedDistance = double.MaxValue;

                //Now use a simple "greedy" algorithm to put the sections in order by continually choosing the nearest one
                node = unAdded.First;
                currentNearestNode = node;
                while (node != null)
                {
                    double currentWeightedDistance = WeightedDistance(currentNearestNode.Value, node.Value);
                    if (currentWeightedDistance < currentNearestWeightedDistance)
                    {
                        currentNearestNode = node;
                        currentNearestWeightedDistance = currentWeightedDistance;
                    }
                    node = node.Next;
                }

                optimizedList.Add(currentNearestNode.Value);
                unAdded.Remove(currentNearestNode);
            }

            return optimizedList;
        }

        /// <summary>
        /// Gets the weighted "distance" between two arcs
        /// </summary>
        /// <param name="arc1">The first arc</param>
        /// <param name="arc2">The second arc</param>
        /// <returns>The weighted distance between the two arcs. This distance takes into account the specified static Speed values.</returns>
        private static double WeightedDistance(ArcSection arc1, ArcSection arc2)
        {
            if (arc1 == arc2)
                return 0;

            double dx = arc2.Center.X - arc1.Center.X;
            double dy = arc2.Center.Y - arc1.Center.Y;
            double dr = arc2.Radius - arc1.Radius;
            return Math.Sqrt(dx * dx * mXSpeedSquared + dy * dy * mYSpeedSquared + dr * dr * mRSpeedSquared);
        }
    }
}
