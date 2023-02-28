using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utility;
using System.Drawing.Imaging;

namespace Viewer
{
    /// <summary>
    /// Provides a method of viewing ArcFiles
    /// </summary>
    public partial class ViewerCanvas : Utility.ArcView
    {
        private ArcFile mArcFile = new ArcFile();

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewerCanvas"/> class.
        /// </summary>
        public ViewerCanvas()
        {
            InitializeComponent();
            ShowLines = false;
        }

        #region Properties

        /// <summary>
        /// Gets the pixels per centimeter (defined as the height in pixels of this ViewerCanvas divided by the intended height of the ArcFile's Canvas)
        /// </summary>
        /// <value>The pixels per centimeter.</value>
        private double PixelsPerCentimeter
        {
            get { return Height / (double)mArcFile.CanvasSize.Height; }
        }

        /// <summary>
        /// Gets or sets the arc file to display.
        /// </summary>
        /// <value>The arc file.</value>
        public ArcFile ArcFile
        {
            get { return mArcFile; }
            set
            {
                if (mArcFile == value)
                    return;
                mArcFile = value;

                //Fire a ViewModeChanged event to make sure the canvas is sized correctly if in Stereoscopic mode
                FireViewModeChangedEvent();
                Dirty = true;
            }
        }


        #endregion

        /// <summary>
        /// Causes this screen to redraw itself when the ArcFile is changed.
        /// </summary>
        private void mArcFile_ArcFileChanged()
        {
            Dirty = true;
        }

        #region ArcView Implementation

        public override IEnumerable<DrawableObjects.Point> GetPointsToDraw(double viewAngle)
        {
            double baseViewAngle = -viewAngle + 90; //For the following math, angle 0 must be in the direction of the positive x axis instead of straight up
            double viewAngleToUse = baseViewAngle; //The view angle we actually use will vary depending whether or not the arc is upsidedown

            foreach (ArcSection arcSection in mArcFile.ArcSections)
            {
                if (arcSection.IsUpsideDown)
                    viewAngleToUse = baseViewAngle + 180;
                else
                    viewAngleToUse = baseViewAngle;

                double startAngle = Math.Min(arcSection.StartAngle, arcSection.EndAngle);
                double endAngle = Math.Max(arcSection.StartAngle, arcSection.EndAngle);
                if (viewAngleToUse >= startAngle && viewAngleToUse <= endAngle)
                {
                    double angle = viewAngleToUse * Math.PI / 180.0; //for trig math, angle must be in radians
                    double cosViewAngle = Math.Cos(angle);
                    double sinViewAngle = Math.Sin(angle);

                    float x = (float)(Width / 2.0 + (arcSection.Center.X + cosViewAngle * arcSection.Radius) * PixelsPerCentimeter);
                    float y = (float)(Height / 2.0 - (arcSection.Center.Y + sinViewAngle * arcSection.Radius) * PixelsPerCentimeter);

                    DrawableObjects.Point arcPoint = new DrawableObjects.Point(new PointF(x, y));
                    yield return arcPoint;
                }
            }
        }

        public override IEnumerable<DrawableObjects.Arc> GetArcsToDraw()
        {
            foreach (ArcSection arcSection in mArcFile.ArcSections)
            {
                float radius = (float)(arcSection.Radius * PixelsPerCentimeter);
                float centerX = (float)(Width / 2.0 + (arcSection.Center.X * PixelsPerCentimeter));
                float centerY = (float)(Height / 2.0 - (arcSection.Center.Y * PixelsPerCentimeter));
                PointF arcRectLocation = new PointF(centerX - radius, centerY - radius);
                RectangleF arcSquare = new RectangleF(arcRectLocation, new SizeF(radius * 2, radius * 2));
                float startAngle = -(float)Math.Min(arcSection.StartAngle, arcSection.EndAngle);
                float sweep = -(float)Math.Abs(arcSection.EndAngle - arcSection.StartAngle);

                DrawableObjects.Arc arc = new DrawableObjects.Arc(arcSquare, startAngle, sweep);
                yield return arc;
            }
        }

        public override IEnumerable<DrawableObjects.Line> GetLinesToDraw(double viewAngle)
        {
            throw new Exception("ViewerCanvas does not support Line (vector) mode");
        }


        public override double CanvasAspectRatio
        {
            get { return mArcFile.CanvasSize.Width / mArcFile.CanvasSize.Height; }
        }


        #endregion

    }
}
