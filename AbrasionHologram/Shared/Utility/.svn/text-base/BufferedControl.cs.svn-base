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
    //http://blog.paranoidferret.com/index.php/2008/04/25/winforms-tutorial-manage-your-own-double-buffering/
    public abstract partial class BufferedControl : UserControl
    {

        private bool mDirty;
        private bool mPainting = false;
        private BufferedGraphicsContext mBufferContext;
        private BufferedGraphics mBuffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="BufferedControl"/> class.
        /// </summary>
        public BufferedControl()
        {
            InitializeComponent();
            Helper.DesignMode = DesignMode;

            mBufferContext = new BufferedGraphicsContext();
            SizeGraphicsBuffer();

            //We're managing double buffering ourselves, so we don't need the control to do it for us
            SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
            SetStyle(ControlStyles.DoubleBuffer, false);

        }

        /// <summary>
        /// Sizes the graphics buffer to match the DisplayRectangle.
        /// </summary>
        private void SizeGraphicsBuffer()
        {
            if (mBuffer != null)
            {
                mBuffer.Dispose();
                mBuffer = null;
            }

            if (mBufferContext == null)
                return;

            if (DisplayRectangle.Width <= 0)
                return;

            if (DisplayRectangle.Height <= 0)
                return;

            using (Graphics graphics = CreateGraphics())
                mBuffer = mBufferContext.Allocate(graphics,
                  DisplayRectangle);

            Dirty = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BufferedControl"/> is dirty and requires redrawing.
        /// </summary>
        /// <value><c>true</c> if dirty; otherwise, <c>false</c>.</value>
        public bool Dirty
        {
            get { return mDirty; }
            set
            {
                if (!value)
                    return;

                mDirty = true;

                if(!mPainting)
                    Invalidate();
            }
        }

        /// <summary>
        /// Paints the background of the control.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data.</param>
        protected override void OnPaintBackground(PaintEventArgs e)
        { /* Do Nothing */ }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            SizeGraphicsBuffer();
            base.OnSizeChanged(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            mPainting = true;

            if (mBuffer == null)
            {
                Draw(e.Graphics);
                return;
            }

            if (mDirty)
            {
                mDirty = false;
                Draw(mBuffer.Graphics);
            }

            mBuffer.Render(e.Graphics);

            mPainting = false;
        }

        public abstract void Draw(Graphics graphics);


    }
}
