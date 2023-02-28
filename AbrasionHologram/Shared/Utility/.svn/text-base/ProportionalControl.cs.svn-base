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
    /// <summary>
    /// Provides a control that hosts an inner control and automatically centers and sizes it to maintain a set aspect ratio while touching at least two edges of this outer control.
    /// </summary>
    public partial class ProportionalControl : UserControl
    {
        private Control mInnerControl;
        private double mAspectRatio = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProportionalControl"/> class.
        /// </summary>
        public ProportionalControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the inner control.
        /// </summary>
        /// <value>The inner control.</value>
        public Control InnerControl
        {
            get { return mInnerControl; }
            set
            {
                if (mInnerControl == value)
                    return;

                if (mInnerControl != null)
                    components.Remove(mInnerControl);

                mInnerControl = value;

                if(mInnerControl != null)
                    Controls.Add(mInnerControl);

                SizeInnerControl();
            }
        }

        /// <summary>
        /// Gets or sets the aspect ratio to maintain.
        /// </summary>
        /// <value>The aspect ratio.</value>
        public double AspectRatio
        {
            get { return mAspectRatio; }
            set
            {
                if (mAspectRatio == value)
                    return;

                if(value <= 0)
                    throw new Exception("ProportionalControl's AspectRatio must be positive. Value specified: " + value);

                mAspectRatio = value;
                SizeInnerControl();
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SizeInnerControl();
        }

        private void SizeInnerControl()
        {
            if (mInnerControl != null)
            {
                double outerAspectRatio = Width / (double)Height;

                if (outerAspectRatio > mAspectRatio)
                {
                    //This control's width is too large. Bring in the left and right sides of the inner control.
                    mInnerControl.Top = 0;
                    mInnerControl.Height = Height;
                    mInnerControl.Width = (int)(Height * mAspectRatio);
                    mInnerControl.Left = (int)((Width - mInnerControl.Width) / 2.0);
                }
                else
                {
                    //This control's height is too large. Bring in the top and bottom sides of the inner control.
                    mInnerControl.Left = 0;
                    mInnerControl.Width = Width;
                    mInnerControl.Height = (int)(Width / mAspectRatio);
                    mInnerControl.Top = (int)((Height - mInnerControl.Height) / 2.0);
                }
            }
        }
    }
}
