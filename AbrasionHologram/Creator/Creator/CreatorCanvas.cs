using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CreatorSupport;
using Utility;
using Primitives;
using System.Drawing.Drawing2D;

namespace Creator
{
    /// <summary>An ArcView UserControl that provides methods to zoom, fly, pan, etc. through the scene. Note: this UserControl will not show up in the designer because ArcView is abstract.</summary>
    public partial class CreatorCanvas : Utility.ArcView
    {
        /// <summary>Gets a value understood to be a null Point. It is actually a point at int.MaxValue, int.MaxValue.</summary>
        public static Point NullPoint { get { return new Point(int.MaxValue, int.MaxValue); } }

        ShapeList mShapeList = new ShapeList();

        private Point mLastMousePosition = NullPoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatorCanvas"/> class.
        /// </summary>
        public CreatorCanvas()
        {
            Helper.DesignMode = DesignMode;
            InitializeComponent();

            this.BeforeDraw += new EventHandler(CreatorCanvas_BeforeDraw);
            this.BeforeSecondDraw += new EventHandler(CreatorCanvas_BeforeSecondDraw);
            ViewContext.ViewChanged += new ViewChangedHandler(ViewContext_ViewChanged);
            DrawOptions.DrawOptionChanged += new DrawOptionChangedHandler(DrawOptions_DrawOptionChanged);
            this.ViewAngleChanged += new EventHandler(CreatorCanvas_ViewAngleChanged);

            this.Resize += new EventHandler(CreatorCanvas_Resize);

            if(!DesignMode)
                ViewContext.CanvasSize = this.Size;
        }

        /// <summary>
        /// Handles the ViewAngleChanged event of the CreatorCanvas control. Causes the control to redraw itself at the proper ViewAngle.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void CreatorCanvas_ViewAngleChanged(object sender, EventArgs e)
        {
            ViewContext.ViewAngle = ViewAngle;
            mShapeList.Dirty = true;
        }

        /// <summary>
        /// Handles the draw option changed event. Causes the control to redraw itself.
        /// </summary>
        /// <param name="e">The <see cref="CreatorSupport.RedrawRequiredEventArgs"/> instance containing the event data.</param>
        void DrawOptions_DrawOptionChanged(RedrawRequiredEventArgs e)
        {
            Dirty = true;
        }

        /// <summary>
        /// Handles the Resize event of the CreatorCanvas control. Ensures that the size of the Canvas is kept up to date.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void CreatorCanvas_Resize(object sender, EventArgs e)
        {
            ViewContext.CanvasSize = this.Size;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the shape list which contains all the IndexedFaceSets to be drawn by this control.
        /// </summary>
        /// <value>The shape list.</value>
        public ShapeList ShapeList
        {
            get { return mShapeList; }
            set
            {
                if (mShapeList == value)
                    return;

                mShapeList = value;
                mShapeList.Dirty = true;
                EdgeTraverser.ShapeList = mShapeList;
                Dirty = true;
            }
        }

        #endregion

        #region Navigation

        /// <summary>
        /// Handles the MouseDown event of the View control. Stores the clicked location.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void View_MouseDown(object sender, MouseEventArgs e)
        {
            if (!DesignMode)
            {
                //prepare for the user to drag by storing the current mouse location.
                mLastMousePosition = e.Location;
            }
        }

        /// <summary>
        /// Handles the MouseUp event of the View control. Clears the stored mouse location.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void View_MouseUp(object sender, MouseEventArgs e)
        {
            if (!DesignMode)
            {
                //the user is done dragging.
                mLastMousePosition = NullPoint;
            }
        }

        /// <summary>
        /// Handles the <see cref="E:System.Windows.Forms.Control.MouseWheel"/> event. Handles zooming (left mouse button pressed), flying (control key pressed) and scaling (no button pressed).
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data.</param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (!DesignMode)
            {
                base.OnMouseWheel(e);
                if (ModifierKeys == Keys.Control || ModifierKeys == (Keys.Control | Keys.Shift))
                {
                    double flyAmount = (e.Delta < 0) ? -1 : 1;
                    if (ViewContext.SlowNavigation)
                        flyAmount /= 10;
                    ViewContext.Fly(flyAmount);
                }
                else
                {
                    if (MouseButtons == MouseButtons.Left)
                    {
                        double zoomAmount = (e.Delta < 0) ? -1 : 1;
                        if (ViewContext.SlowNavigation)
                            zoomAmount /= 10;
                        ViewContext.Zoom(zoomAmount);
                    }
                    else if (MouseButtons == MouseButtons.None)
                    {
                        double scaleAmount = (e.Delta < 0) ? .9 : -1.1;
                        if (ViewContext.SlowNavigation)
                            scaleAmount += .09;
                        ViewContext.Scale(Math.Abs(scaleAmount));
                    }
                }
            }
        }

        /// <summary>
        /// Handles the MouseMove event of the View control. Orbits (if left mouse button is down), pans (if middle mouse button is down [or both left and right]), and looks around (if right mouse button is down)
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void View_MouseMove(object sender, MouseEventArgs e)
        {
            if (!DesignMode)
            {
                if (e.Button != MouseButtons.None)
                {
                    if (mLastMousePosition != NullPoint)
                    {
                        PointD deltaMousePosition = GetDelta(e.Location, mLastMousePosition);

                        if (e.Button == MouseButtons.Left)
                            Orbit(deltaMousePosition);
                        else if (e.Button == (MouseButtons.Left | MouseButtons.Right) || e.Button == MouseButtons.Middle)
                            Pan(mLastMousePosition, e.Location);
                        else if (e.Button == MouseButtons.Right)
                            LookAround(deltaMousePosition);
                    }

                    mLastMousePosition = e.Location;
                }
            }
        }

        /// <summary>
        /// Gets the difference between two points in screen coordinates. If ViewContext.SlowNavigation, returns one-tenth of this value.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        private PointD GetDelta(Point start, Point end)
        {
            if (ViewContext.SlowNavigation)
                return new PointD((end.X - start.X) / 10.0, (end.Y - start.Y) / 10.0);
            return new PointD(end.X - start.X, end.Y - start.Y);
        }

        /// <summary>
        /// Pans the view according to the specified mouse clicks.
        /// </summary>
        /// <param name="lastMouseClick">The last mouse click.</param>
        /// <param name="currentMouseClick">The current mouse click.</param>
        private void Pan(Point lastMouseClick, Point currentMouseClick)
        {
            Coord lastMouse = new Coord(lastMouseClick.X, lastMouseClick.Y, 0);
            Coord currentMouse = new Coord(currentMouseClick.X, currentMouseClick.Y, 0);
            ViewContext.Pan(lastMouse, currentMouse);
        }
        /// <summary>
        /// Orbits the view according to the specified change in mouse position.
        /// </summary>
        /// <param name="deltaMousePosition">The delta mouse position.</param>
        private void Orbit(PointD deltaMousePosition)
        {
            Coord newPoLocation_ViewCoordinates = new Coord((deltaMousePosition.X * ViewContext.N.Length / 2) + (Width / 2), (deltaMousePosition.Y * ViewContext.N.Length / 2) + (Height / 2), 0);
            ViewContext.Orbit(newPoLocation_ViewCoordinates);
        }
        /// <summary>
        /// Looks around according to the specified change in mouse position.
        /// </summary>
        /// <param name="deltaMousePosition">The delta mouse position.</param>
        private void LookAround(PointD deltaMousePosition)
        {
            Coord newPrLocation_ViewCoordinates = new Coord((deltaMousePosition.X * ViewContext.N.Length / 2) + (Width / 2), (deltaMousePosition.Y * ViewContext.N.Length / 2) + (Height / 2), 0);
            ViewContext.LookAround(newPrLocation_ViewCoordinates);
        }

        #endregion

        /// <summary>
        /// Causes the canvas to repaint itself.
        /// </summary>
        /// <param name="e">The <see cref="CreatorSupport.RedrawRequiredEventArgs"/> instance containing the event data.</param>
        void ViewContext_ViewChanged(RedrawRequiredEventArgs e)
        {
            mShapeList.Dirty = true;
            Dirty = true;
        }

        /// <summary>
        /// Handles the KeyPress event of the View control. Resets the camera to its default position when the user presses the R key.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void View_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!DesignMode)
            {
                if (e.KeyChar == 'r' || e.KeyChar == 'R')
                    ViewContext.ResetCamera();
            }
        }

        /// <summary>
        /// Handles the KeyUp event of the View control. Disables ViewContext.SlowNavigation when the user releases Shift.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void View_KeyUp(object sender, KeyEventArgs e)
        {
            if (!DesignMode)
            {
                if (e.KeyCode == Keys.ShiftKey)
                    ViewContext.SlowNavigation = false;
            }
        }

        /// <summary>
        /// Handles the PreviewKeyDown event of the View control. Enables ViewContext.SlowNavigation when the user holds Shift.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PreviewKeyDownEventArgs"/> instance containing the event data.</param>
        private void View_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (!DesignMode)
            {
                if (e.KeyCode == Keys.ShiftKey)
                    ViewContext.SlowNavigation = true;
            }
        }

        /// <summary>
        /// Handles the BeforeDraw event of the CreatorCanvas control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CreatorCanvas_BeforeDraw(object sender, EventArgs e)
        {
            PreDraw();
        }
        /// <summary>
        /// Handles the BeforeSecondDraw event of the CreatorCanvas control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CreatorCanvas_BeforeSecondDraw(object sender, EventArgs e)
        {
            PreDraw();
        }
        /// <summary>
        /// Does any work that must be done before the control draws itelf.
        /// </summary>
        private void PreDraw()
        {
            EdgeTraverser.Dirty = true;
        }

        #region ArcView Implementation

        /// <summary>
        /// Gets the points to draw.
        /// </summary>
        /// <param name="viewAngle">The view angle.</param>
        /// <returns></returns>
        public override IEnumerable<DrawableObjects.Point> GetPointsToDraw(double viewAngle)
        {
            double buViewAngle = ViewAngle;
            ViewContext.ViewAngle = viewAngle;
            if (DrawOptions.VisibilityMode == VisibilityMode.HiddenLine)
                foreach (EdgeSection es in EdgeTraverser.VisibleEdgeSections)
                    foreach (EdgeCoord c in es.Edge.GetCoords(DrawOptions.CoordsPerUnitLength))
                    {
                        if (DrawOptions.VisibilityMode == VisibilityMode.Transparent || c.IsVisible())
                            yield return new DrawableObjects.Point(c.Coord.ToPointF());
                    }
            else
                foreach (Edge edge in mShapeList.Edges)
                    foreach (EdgeCoord c in edge.GetCoords(DrawOptions.CoordsPerUnitLength))
                        yield return new DrawableObjects.Point(c.Coord.ToPointF());

            ViewContext.ViewAngle = buViewAngle;
        }


        /// <summary>
        /// Gets the arcs to draw.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<DrawableObjects.Arc> GetArcsToDraw()
        {
            foreach (Edge e in mShapeList.Edges)
            {
                foreach (EdgeCoord c in e.GetCoords(DrawOptions.CoordsPerUnitLength))
                {
                    RectangleF arcRect = Transformer.GetArcSquare(c.Coord_ZeroAngle);
                    float startAngle = c.Coord_ZeroAngle.Z - ViewContext.N_ViewCoordinates > 0 ? 45 : 180 + 45;
                    DrawableObjects.Arc arc = new DrawableObjects.Arc(arcRect, startAngle, 90);
                    yield return arc;
                }
            }
        }

        /// <summary>
        /// Gets the lines to draw.
        /// </summary>
        /// <param name="viewAngle">The view angle.</param>
        /// <returns></returns>
        public override IEnumerable<DrawableObjects.Line> GetLinesToDraw(double viewAngle)
        {
            double buViewAngle = ViewAngle;
            ViewContext.ViewAngle = viewAngle;
            if (DrawOptions.VisibilityMode == VisibilityMode.HiddenLine)
                foreach (EdgeSection es in EdgeTraverser.VisibleEdgeSections)
                    yield return new DrawableObjects.Line(es.StartCoord.ToPointF(), es.EndCoord.ToPointF());
            else
                foreach (Edge e in mShapeList.Edges)
                    yield return new DrawableObjects.Line(e.StartVertex.ViewCoord.ToPointF(), e.EndVertex.ViewCoord.ToPointF());
            ViewContext.ViewAngle = buViewAngle;
        }


        /// <summary>
        /// The aspect ratio of the canvas that this ArcView represents. This aspect ratio does not necessarily reflect the aspect ratio of this control; when in Stereoscopic mode, the actual aspect ratio will be twice this value.
        /// </summary>
        public override double CanvasAspectRatio
        {
            get { return ArcFileSettings.CanvasSize.Width / ArcFileSettings.CanvasSize.Height; }
        }

        #endregion
    }
}
