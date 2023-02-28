using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Utility
{
    public enum ViewMode { Normal, RedBlue, Stereoscopic }

    /// <summary>
    /// Provides a BufferedControl that displays arcs in Normal, Red/Blue and Stereoscopic mode.
    /// </summary>
    public abstract partial class ArcView : BufferedControl
    {
        private double mViewAngle = 0;
        private double mPointSizeProportion = .008;
        private bool mShowArcs = true;
        private bool mShowPoints = true;
        private bool mShowLines= true;
        private ViewMode mViewMode = ViewMode.Normal;
        private double mStereoscopicDisparityAngle = 8;
        private bool mAntiAlias = false;

        public event EventHandler ViewModeChanged;
        public event EventHandler ViewAngleChanged;
        /// <summary>Occurs right before drawing to the screen</summary>
        public event EventHandler BeforeDraw;
        /// <summary>Occurs right before drawing the second image to the screen when in Red/Blue or Stereoscopic mode</summary>
        public event EventHandler BeforeSecondDraw;


        /// <summary>
        /// Initializes a new instance of the <see cref="ArcView"/> class.
        /// </summary>
        public ArcView()
        {
            InitializeComponent();
        }

        #region Properties
        /// <summary>The angle at which light source is illuminating the arcs. This controls where the points will be along the arcs. ViewAngle 0 is straight up with negative ViewAngles to the left and positive to the right.</summary>
        public double ViewAngle
        {
            get { return mViewAngle; }
            set
            {
                if (mViewAngle == value)
                    return;
                mViewAngle = value;

                if (ViewAngleChanged != null)
                    ViewAngleChanged(this, new EventArgs());

                Dirty = true;
            }
        }

        /// <summary>The size of the points to draw, specified as a fraction of the width or height (smallest dimension) of this ArcView</summary>
        public double PointSizeProportion
        {
            get { return mPointSizeProportion; }
            set
            {
                if (mPointSizeProportion == value)
                    return;
                mPointSizeProportion = value;
                Dirty = true;
            }
        }

        /// <summary>Specifies whether or not to draw arcs on the screen.</summary>
        public bool ShowArcs
        {
            get { return mShowArcs; }
            set
            {
                if (mShowArcs == value)
                    return;
                mShowArcs = value;
                Dirty = true;
            }
        }

        /// <summary>Specifies whether or not to draw Points on the screen.</summary>
        public bool ShowPoints
        {
            get { return mShowPoints; }
            set
            {
                if (mShowPoints == value)
                    return;
                mShowPoints = value;
                Dirty = true;
            }
        }

        /// <summary>Specifies whether or not to draw Lines along the edges of the shapes.</summary>
        public bool ShowLines
        {
            get { return mShowLines; }
            set
            {
                if (mShowLines == value)
                    return;
                mShowLines = value;
                Dirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the view mode.
        /// </summary>
        /// <value>The view mode.</value>
        public ViewMode ViewMode
        {
            get { return mViewMode; }
            set
            {
                if (mViewMode == value)
                    return;

                mViewMode = value;
                FireViewModeChangedEvent();

                Dirty = true;
            }
        }

        /// <summary>
        /// Fires the view mode changed event.
        /// </summary>
        public void FireViewModeChangedEvent()
        {
            if (ViewModeChanged != null)
                ViewModeChanged(this, new EventArgs());
        }

        /// <summary>
        /// Gets or sets the stereoscopic disparity angle used in Red/Blue and Stereoscopic ViewModes.
        /// </summary>
        /// <value>The stereoscopic disparity angle.</value>
        public double StereoscopicDisparityAngle
        {
            get
            {
                return mStereoscopicDisparityAngle;
            }
            set
            {
                if (mStereoscopicDisparityAngle == value)
                    return;
                mStereoscopicDisparityAngle = value;
                Dirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to anti alias the drawing.
        /// </summary>
        /// <value><c>true</c> if anti aliasing is enabled; otherwise, <c>false</c>.</value>
        public bool AntiAlias
        {
            get { return mAntiAlias; }
            set
            {
                if (mAntiAlias == value)
                    return;

                mAntiAlias = value;
                Dirty = true;
            }
        }

        #endregion

        #region Drawing

        /// <summary>
        /// Draws appropriately to the specified graphics object
        /// </summary>
        /// <param name="g">The graphics object.</param>
        public override void Draw(Graphics g)
        {
            g.SmoothingMode = mAntiAlias ? System.Drawing.Drawing2D.SmoothingMode.HighQuality : System.Drawing.Drawing2D.SmoothingMode.None;

            //Raise an event to notify that we are about to draw
            if (BeforeDraw != null)
                BeforeDraw(this, new EventArgs());

            switch (ViewMode)
            {
                case ViewMode.Normal:
                    DrawNormal(g);
                    break;
                case ViewMode.RedBlue:
                    DrawRedBlue(g);
                    break;
                case ViewMode.Stereoscopic:
                    DrawStereoscopic(g);
                    break;
            }
        }

        private void DrawNormal(Graphics g)
        {
            g.Clear(Color.White);

            if (ShowArcs)
                DrawArcs(g, 0);
            if (ShowLines)
                DrawLines(g, ViewAngle, Pens.Blue, 0);
            if (ShowPoints)
                DrawPoints(g, ViewAngle, Brushes.Blue);
        }
        private void DrawRedBlue(Graphics g)
        {
            float transparency = .5f;
            //float transparency = .6f;
            float[][] mtrx = new float[5][] {
            new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
            new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
            new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
            new float[] {0.0f, 0.0f, 0.0f, transparency, 0.0f},
            new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}};

            ColorMatrix colorMatrix = new ColorMatrix(mtrx);
            using (ImageAttributes ia = new ImageAttributes())
            {
                ia.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                Color leftColor = Color.Cyan; //DrawOptions.SwitchLeftRight ? Color.Red : Color.Cyan;
                Color rightColor = Color.Red; //DrawOptions.SwitchLeftRight ? Color.Cyan : Color.Red;
                //Color leftColor = Color.FromArgb(32, 32, 255);

                //The rightBitmap will be blitted onto g after the left image is drawn onto g
                Bitmap rightBitmap = new Bitmap(Width, Height);
                Graphics rightGraphics = Graphics.FromImage(rightBitmap);

                rightGraphics.Clear(rightColor);
                g.Clear(leftColor);

                if (ShowLines)
                    DrawLines(g, ViewAngle + StereoscopicDisparityAngle, Pens.Black);
                if (ShowPoints)
                    DrawPoints(g, ViewAngle + StereoscopicDisparityAngle, Brushes.Black);

                if (BeforeSecondDraw != null)
                    BeforeSecondDraw(this, new EventArgs());

                if (ShowLines)
                    DrawLines(rightGraphics, ViewAngle - StereoscopicDisparityAngle, Pens.Black);
                if (ShowPoints)
                    DrawPoints(rightGraphics, ViewAngle - StereoscopicDisparityAngle, Brushes.Black);

                g.DrawImage(rightBitmap, new Rectangle(0, 0, rightBitmap.Width, rightBitmap.Height), 0, 0, rightBitmap.Width, rightBitmap.Height, GraphicsUnit.Pixel, ia);
            }
        }
        private void DrawStereoscopic(Graphics g)
        {
            g.Clear(Color.White);

            g.Clip = new Region(new Rectangle(0, 0, Width / 2, Height));
            if (ShowArcs)
                DrawArcs(g, -Width / 4);
            if (ShowLines)
                DrawLines(g, ViewAngle + StereoscopicDisparityAngle, Pens.Blue, -Width / 4f);
            if(ShowPoints)
                DrawPoints(g, ViewAngle + StereoscopicDisparityAngle, Brushes.Blue, - Width / 4f);

            if (BeforeSecondDraw != null)
                BeforeSecondDraw(this, new EventArgs());

            g.Clip = new Region(new Rectangle(Width / 2, 0, Width / 2, Height));
            if (ShowArcs)
                DrawArcs(g, Width / 4);
            if (ShowLines)
                DrawLines(g, ViewAngle - StereoscopicDisparityAngle, Pens.Blue, Width / 4f);
            if (ShowPoints)
                DrawPoints(g, ViewAngle - StereoscopicDisparityAngle, Brushes.Blue, Width / 4f);

            g.ResetClip();
        }



        private void DrawArcs(Graphics g, float horizontalOffset)
        {
            foreach (DrawableObjects.Arc arc in GetArcsToDraw())
            {
                if (!arc.ArcSquare.IsEmpty)
                {
                    arc.Draw(g, horizontalOffset);
                }
            }
        }

        private void DrawPoints(Graphics g, double viewAngle, Brush b)
        {
            DrawPoints(g, viewAngle, b, 0);
        }

        private void DrawPoints(Graphics g, double viewAngle, Brush b, float horizontalOffset)
        {
            float radius = (float)Math.Max(4, Height < Width ? Height * mPointSizeProportion : Width * mPointSizeProportion);

            foreach (DrawableObjects.Point p in GetPointsToDraw(viewAngle))
            {
                p.Draw(g, radius, b, horizontalOffset);
            }
        }

        private void DrawLines(Graphics g, double viewAngle, Pen p)
        {
            DrawLines(g, viewAngle, p, 0);
        }

        private void DrawLines(Graphics g, double viewAngle, Pen p, float horizontalOffset)
        {
            foreach (DrawableObjects.Line l in GetLinesToDraw(viewAngle))
                l.Draw(g, p, horizontalOffset);
        }


        #endregion Drawing



        #region Abstract Functions & Properties

        /// <summary>
        /// Gets the points to draw.
        /// </summary>
        /// <param name="viewAngle">The view angle.</param>
        /// <returns></returns>
        public abstract IEnumerable<DrawableObjects.Point> GetPointsToDraw(double viewAngle);
        /// <summary>
        /// Gets the arcs to draw.
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<DrawableObjects.Arc> GetArcsToDraw();
        /// <summary>
        /// Gets the lines to draw.
        /// </summary>
        /// <param name="viewAngle">The view angle.</param>
        /// <returns></returns>
        public abstract IEnumerable<DrawableObjects.Line> GetLinesToDraw(double viewAngle);

        /// <summary>The aspect ratio of the canvas that this ArcView represents. This aspect ratio does not necissarily reflect the aspect ratio of this control; when in Stereoscopic mode, the actual aspect ratio will be twice this value.</summary>
        public abstract double CanvasAspectRatio { get; }

        #endregion



    }
}
