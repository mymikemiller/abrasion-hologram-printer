using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;
using CreatorSupport;
using Primitives;
using System.Drawing;
using System.ComponentModel;

namespace Creator
{
    /// <summary>A static class with methods allowing creation of an ArcFiles based on a supplied ShapeList. Generation runs in the specified background worker thread, which provides status information. The arcs in the ArcFile created will be HiddenLine if DrawOptions.VisibilityMode is HiddenLine, otherwise they will be transparent.</summary>
    static class ArcFileCreator
    {
        /// <summary>Gets the ArcFile that was last created by calling the CreateArcFile method.</summary>
        public static ArcFile ArcFile { get; private set; }
        /// <summary>Sets the ShapeList to be used when the CreateArcFile method is called.</summary>
        public static ShapeList ShapeList { get; set; }

        /// <summary>
        /// Generates an ArcFile. ArcFileCreator's static ShapeList property must be set before calling this method, and the resulting ArcFile will be set as the static ArcFile property of ArcFileCreator once creation is complete.
        /// </summary>
        /// <param name="worker">The BackgroundWorker whose thread will be used to create the ArcFile. Generates an event when creation is complete.</param>
        public static void CreateArcFile(BackgroundWorker worker)
        {
            if (ShapeList == null)
                throw new Exception("ShapeList must be set before calling CreateArcFile");

            ArcFile arcFile;
            if (DrawOptions.VisibilityMode == VisibilityMode.Transparent)
            {
                arcFile = CreateArcFile_Transparent(ShapeList, worker);
            }
            else
            {
                List<EdgeVisibilityInfo> visibilities = IterateThroughAngles(ShapeList, worker);
                arcFile = CreateArcFile_HiddenLine(visibilities);
            }
            arcFile.CanvasSize = ArcFileSettings.CanvasSize;
            ArcFile = arcFile;
        }

        /// <summary>
        /// Creates an ArcFile based on the specified list of EdgeVisibilityInfos. This method is lightweight because the visiblity has already been calculated for each Edge, and thus it does not need to send updates to the background worker.
        /// </summary>
        /// <param name="visibilities">A list of EdgeVisibilityInfos for each Edge to be added to the ArcFile.</param>
        /// <returns>The created ArcFile</returns>
        private static ArcFile CreateArcFile_HiddenLine(List<EdgeVisibilityInfo> visibilities)
        {
            ArcFile arcFile = new ArcFile();
            double increment = ArcFileSettings.AngularStepIncrement;

            foreach (EdgeVisibilityInfo edgeVisibility in visibilities)
            {
                List<EdgeCoord> coords = edgeVisibility.Edge.GetCoords(DrawOptions.CoordsPerUnitLength).ToList();

                foreach (CoordVisibilityInfo coordVisibility in edgeVisibility.CoordVisibilities)
                {
                    ArcSection arcSection = ConvertCoordToArcSection(coords[coordVisibility.CoordIndex].Coord);

                    foreach (VisibleRange range in coordVisibility.VisibleRanges)
                    {
                        ArcSection newArcSection = arcSection.Clone();
                        double angleOffset = newArcSection.IsUpsideDown ? 180 : 0;
                        newArcSection.StartAngle = -range.StartAngle + 90 + angleOffset;
                        newArcSection.EndAngle = -range.EndAngle + 90 + angleOffset;
                        arcFile.ArcSections.Add(newArcSection);
                    }
                }
            }

            return arcFile;
        }
        /// <summary>
        /// Creates an ArcFile without using a hidden line algorithm. All Edges are fully visible.
        /// </summary>
        /// <param name="shapeList">The ShapeList containing the Edges to add to the ArcFile</param>
        /// <param name="worker">The BackgroundWorker creating this ArcFile. Progress updates will be sent to this worker.</param>
        /// <returns>The created ArcFile</returns>
        private static ArcFile CreateArcFile_Transparent(ShapeList shapeList, BackgroundWorker worker)
        {
            worker.ReportProgress(0);
            int edgeCount = shapeList.EdgeCount;
            int currentEdge = 1;

            ArcFile arcFile = new ArcFile();
            foreach (Edge edge in shapeList.Edges)
            {
                if (worker.CancellationPending)
                    break;

                foreach (EdgeCoord c in edge.GetCoords(DrawOptions.CoordsPerUnitLength))
                {
                    arcFile.ArcSections.Add(ConvertCoordToArcSection(c.Coord));
                }
                worker.ReportProgress((currentEdge++ / edgeCount) * 100);
            }
            return arcFile;
        }

        /// <summary>
        /// Converts a given Coord to an ArcSection
        /// </summary>
        /// <param name="zeroAngleViewCoord">The Coord to convert. Must be a ViewCoord at zero-angle.</param>
        /// <returns>The ArcSection representing the specified Coord</returns>
        private static ArcSection ConvertCoordToArcSection(Coord zeroAngleViewCoord)
        {
            //there are two possible options for how to scale the arcs from the size of the image on the screen to the size of the canvas:
            //base the scale factor on the ratio of widths, or heights. Because we want to preserve all arcs that are visible on the screen,
            //choose the smaller scale factor. That way if the aspect ratios aren't the same, nothing will be cut off; instead, whitespace will be added.
            double scale = Math.Min(ArcFileSettings.CanvasSize.Width / ViewContext.CanvasSize.Width, ArcFileSettings.CanvasSize.Height / ViewContext.CanvasSize.Height);

            PointD centerSource = new PointD(ViewContext.CanvasSize.Width / 2.0, ViewContext.CanvasSize.Height / 2.0);
            PointD distanceFromCenterScreen = Transformer.GetArcCenter(zeroAngleViewCoord) - centerSource;
            //Account for the origin being at top-left instead of bottom-left
            distanceFromCenterScreen.Y = -distanceFromCenterScreen.Y;
            PointD distanceFromCenter = distanceFromCenterScreen * scale;

            double radius = Transformer.GetArcRadius(zeroAngleViewCoord) * scale;

            //positive radius implies in front of canvas (i.e. upside-down arc). Specify angles at the bottom half of the circle.
            double angleOffset = radius > 0 ? 180 : 0;

            return new ArcSection(distanceFromCenter, Math.Abs(radius), 90 + 45 + angleOffset, 45 + angleOffset);
        }

        /// <summary>
        /// Iterates through all the view angles from -45 to 45 with an interval specified in ArcFileSettings.AngularStepIncrement and generates a list of EdgeVisibilityInfos.
        /// </summary>
        /// <param name="shapeList">The ShapeList containing the Edges to use when creating the EdgeVisibilityInfo list</param>
        /// <param name="worker">The BackgroundWorker. Progress updates will be sent to this worker.</param>
        /// <returns>The generated list of EdgeVisibilityInfos</returns>
        private static List<EdgeVisibilityInfo> IterateThroughAngles(ShapeList shapeList, BackgroundWorker worker)
        {
            worker.ReportProgress(0);

            double backUpViewAngle = ViewContext.ViewAngle;
            ViewContext.HaltViewChangedEvent = true;

            ViewContext.ViewAngle = -45;
            EdgeTraverser.ShapeList = shapeList;
            if (ArcFileSettings.QuickMode)
                EdgeTraverser.ForceTraversal();
            shapeList.Dirty = true;

            List<EdgeVisibilityInfo> edgeVisibilities = new List<EdgeVisibilityInfo>();
            foreach (Edge edge in shapeList.Edges)
                edgeVisibilities.Add(new EdgeVisibilityInfo(edge));

            double increment = ArcFileSettings.AngularStepIncrement;
            for (double angle = -45; angle <= 45; angle += increment)
            {
                if (worker.CancellationPending)
                    break;

                ViewContext.ViewAngle = angle;
                if (ArcFileSettings.QuickMode)
                    EdgeTraverser.ForceTraversal();
                shapeList.Dirty = true;
                foreach (EdgeVisibilityInfo edgeVisibility in edgeVisibilities)
                {
                    if (worker.CancellationPending)
                        break;

                    if (edgeVisibility.Edge.VisibleEdgeSections.FirstOrDefault() != null) //if the edge has no visible edge sections, no Coords will be visible
                    {
                        List<EdgeCoord> coordsInEdge = edgeVisibility.Edge.GetCoords(DrawOptions.CoordsPerUnitLength).ToList();
                        foreach (CoordVisibilityInfo coordVisibility in edgeVisibility.CoordVisibilities)
                        {
                            if (worker.CancellationPending)
                                break;

                            EdgeCoord c = coordsInEdge[coordVisibility.CoordIndex];

                            //Only add this angle to list of visible angles if coord is visible at this angle
                            if (ArcFileSettings.QuickMode)
                            {
                                if (c.IsVisible())
                                    coordVisibility.AddVisibleAngle(angle);
                            }
                            else
                            {
                                if (c.IsVisible_Slow(shapeList))
                                    coordVisibility.AddVisibleAngle(angle);
                            }
                            
                        }
                    }
                }

                //we're going from -45 to 45; current progress is distance from -45 divided by 90, the total distance.
                //But, we multiply by 100 to get percent, so divide by .9 instead of 90.
                worker.ReportProgress((int)((angle + 45) / .9));
            }

            ViewContext.ViewAngle = backUpViewAngle;
            ViewContext.HaltViewChangedEvent = false;
            return edgeVisibilities;
        }
    }

    /// <summary>Stores this list of CoordVisibilities that make up the associated Edge</summary>
    internal class EdgeVisibilityInfo
    {
        /// <summary>The Edge whose visibility this EdgeVisibilityInfo defines</summary>
        internal Edge Edge { get; private set; }
        /// <summary>The list of CoordVisibilities defining the visiblity for the associated Edge</summary>
        internal List<CoordVisibilityInfo> CoordVisibilities { get; set; }

        /// <summary>Creates a new EdgeVisibilityInfo representing the specified Edge. The CoordVisibilties list will be initialized with CoordVisibilityInfos containing no VisibleRanges.</summary>
        /// <param name="edge">The Edge that this EdgeVisibilityInfo represents</param>
        internal EdgeVisibilityInfo(Edge edge)
        {
            Edge = edge;
            CoordVisibilities = new List<CoordVisibilityInfo>();
            int numPoints = edge.GetNumCoords(DrawOptions.CoordsPerUnitLength);
            for (int i = 0; i < numPoints; i++)
                CoordVisibilities.Add(new CoordVisibilityInfo(i));
        }
    }

    /// <summary>
    /// Stores the ranges of angles at which the associated Coord is visible.
    /// </summary>
    internal class CoordVisibilityInfo
    {
        /// <summary>Gets and sets the index into the Edge's Coords array (Edge.GetViewCoords()) that this CoordVisibilityInfo represents.</summary>
        internal int CoordIndex { get; set; }

        /// <summary>Gets the list of ranges of angles at which the associated Coord is visible</summary>
        internal List<VisibleRange> VisibleRanges { get; private set; }

        /// <summary>
        /// Creates a new CoordVisibilityInfo representing the specified Coord. Its VisibleRanges list is initially empty.
        /// </summary>
        /// <param name="coordIndex">The index into the Edge's Coords array (Edge.GetViewCoords()) that this CoordVisibilityInfo represents</param>
        internal CoordVisibilityInfo(int coordIndex)
        {
            CoordIndex = coordIndex;
            VisibleRanges = new List<VisibleRange>();
        }

        /// <summary>
        /// Specifies that the associated Coord is visible at the specified angle. This will modify the bounds of an existing VisibleRange (if the specified angle is one increment [ArcFileSettings.AngularStepIncrement] away from the last angle added) or add a new VisibleRange if necessary.
        /// </summary>
        /// <param name="angle">An angle at which the associated Coord is visible. Angles must be added in ascending order.</param>
        internal void AddVisibleAngle(double angle)
        {
            if (VisibleRanges.Count == 0)
                VisibleRanges.Add(new VisibleRange(angle));
            else
            {
                VisibleRange lastVal = VisibleRanges[VisibleRanges.Count - 1];
                //if the angle being added is one increment away from the last angle added, it part of the last VisibleRange.
                //Otherwise, the last VisibleRange is complete and the angle being added starts a new VisibleRange.
                if (lastVal.EndAngle == angle - ArcFileSettings.AngularStepIncrement)
                    lastVal.EndAngle = angle;
                else
                {
                    VisibleRanges.Add(new VisibleRange(angle));
                }
            }
        }
    }

    /// <summary>Represents a range of angles at which a Coord is visible</summary>
    internal class VisibleRange
    {
        /// <summary>The angle at which this VisibleRange begins</summary>
        internal double StartAngle { get; set; }
        /// <summary>The angle at which this VisibleRange ends</summary>
        internal double EndAngle { get; set; }

        /// <summary>Creates a new VisibleRange with the specified StartAngle (which is also the EndAngle)</summary>
        /// <param name="initialAngle">The angle which is the initial start and end angle for this VisibleRange</param>
        internal VisibleRange(double initialAngle)
        {
            StartAngle = initialAngle;
            EndAngle = initialAngle;
        }
        /// <summary>
        /// Returns a human-readable string representing this VisibleRange
        /// </summary>
        /// <returns>A human-readable string representing this VisibleRange</returns>
        public override string ToString()
        {
            return StartAngle + ", " + EndAngle;
        }
    }
}
