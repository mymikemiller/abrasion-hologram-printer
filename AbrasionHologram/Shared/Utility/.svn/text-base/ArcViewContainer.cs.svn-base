using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utility
{
    /// <summary>Holds an ArcView (eg. ViewerCanvas or CreatorCanvas) within a ProportionalControl and provides a slider at the top to control the ViewAngle</summary>
    public partial class ArcViewContainer : UserControl
    {
        private double mAutoAngleIncrement = .5;
        private int mAutoAngleDirection = 1;
        /// <summary>If true, auto angle movement sweeps the full bounds of the ViewAngleTrackBar. If false, it only sweeps the middle third of the TrackBar.</summary>
        private bool mAutoAngleFullSweep = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArcViewContainer"/> class.
        /// </summary>
        public ArcViewContainer()
        {
            InitializeComponent();
        }

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this instance contains an ArcView control as its ProportionalControl's InnerControl
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has an ArcView control; otherwise, <c>false</c>.
        /// </value>
        public bool HasArcView { get { return mProportionalControl.InnerControl != null; } }

        /// <summary>
        /// Gets or sets the ArcView control contained by this ArcViewContainer
        /// </summary>
        /// <value>The ArcView.</value>
        public ArcView ArcView
        {
            get
            {
                if (!HasArcView)
                    return null;
                else
                    return (ArcView)mProportionalControl.InnerControl; 
            }
            set
            {
                if (value != null && (!HasArcView || value != ArcView))
                {
                    //Remove the previous ArcView's listeners and then set the new one
                    if (HasArcView)
                    {
                        ArcView.PreviewKeyDown -= ArcViewContainer_PreviewKeyDown;
                        ArcView.ViewModeChanged -= ArcViewContainer_ViewModeChanged;
                    }

                    value.PreviewKeyDown += ArcViewContainer_PreviewKeyDown;
                    value.ViewModeChanged += ArcViewContainer_ViewModeChanged;
                    mProportionalControl.InnerControl = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the arc view aspect ratio.
        /// </summary>
        /// <value>The arc view aspect ratio.</value>
        public double ArcViewAspectRatio
        {
            get { return mProportionalControl.AspectRatio; }
            set { mProportionalControl.AspectRatio = value; }
        }

        #endregion

        /// <summary>
        /// Handles the ValueChanged event of the mViewAngleTrackBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mViewAngleTrackBar_ValueChanged(object sender, EventArgs e)
        {
            if(HasArcView)
                ArcView.ViewAngle = mViewAngleTrackBar.Value / 10.0;
        }
        /// <summary>
        /// Handles the PreviewKeyDown event of the ArcViewContainer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PreviewKeyDownEventArgs"/> instance containing the event data.</param>
        private void ArcViewContainer_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.C)
                mViewAngleTrackBar.Value = 0;
            else if (e.KeyCode == Keys.A)
            {
                mAutoAngleFullSweep = true;
                if (mAutoAngleTimer.Enabled)
                    mAutoAngleTimer.Stop();
                else
                    mAutoAngleTimer.Start();
            }
            else if (e.KeyCode == Keys.Q)
            {
                mAutoAngleFullSweep = false;
                if (mAutoAngleTimer.Enabled)
                    mAutoAngleTimer.Stop();
                else
                    mAutoAngleTimer.Start();
            }
        }

        /// <summary>
        /// Handles the ViewModeChanged event of the ArcViewContainer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ArcViewContainer_ViewModeChanged(object sender, EventArgs e)
        {
            if (HasArcView)
            {
                //Reset the view angle
                mViewAngleTrackBar.Value = 0;

                //properly size the canvas
                if (ArcView.ViewMode == ViewMode.Stereoscopic)
                    ArcViewAspectRatio = ArcView.CanvasAspectRatio * 2;
                else
                    ArcViewAspectRatio = ArcView.CanvasAspectRatio;

                //set the bounds of the ViewAngle trackbar
                if (ArcView.ViewMode == ViewMode.Stereoscopic || ArcView.ViewMode == ViewMode.RedBlue)
                    mViewAngleTrackBar.Maximum = (int)((45 - ArcView.StereoscopicDisparityAngle) * 10);
                else
                    mViewAngleTrackBar.Maximum = 45 * 10;

                mViewAngleTrackBar.Minimum = -mViewAngleTrackBar.Maximum;
                mViewAngleTrackBar.TickFrequency = mViewAngleTrackBar.Maximum;
            }
        }

        /// <summary>
        /// Handles the Tick event of the mAutoAngleTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void mAutoAngleTimer_Tick(object sender, EventArgs e)
        {
            int min = mAutoAngleFullSweep ? mViewAngleTrackBar.Minimum : (mViewAngleTrackBar.Maximum - mViewAngleTrackBar.Minimum) / 3 + mViewAngleTrackBar.Minimum;
            int max = mAutoAngleFullSweep ? mViewAngleTrackBar.Maximum : mViewAngleTrackBar.Maximum - (mViewAngleTrackBar.Maximum - mViewAngleTrackBar.Minimum) / 3;

            if (mViewAngleTrackBar.Value >= max)
                mAutoAngleDirection = -1;
            else if (mViewAngleTrackBar.Value <= min)
                mAutoAngleDirection = 1;

            int newValue = (int)(mViewAngleTrackBar.Value + 30 * mAutoAngleIncrement * mAutoAngleDirection);
            newValue = Math.Max(newValue, mViewAngleTrackBar.Minimum);
            newValue = Math.Min(newValue, mViewAngleTrackBar.Maximum);
            mViewAngleTrackBar.Value = newValue;
        }

    }
}
