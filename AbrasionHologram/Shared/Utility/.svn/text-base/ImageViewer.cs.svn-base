using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utility
{
    public partial class ImageViewer : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageViewer"/> class. This is a helper class which pops up a screen containing the specified image.
        /// </summary>
        public ImageViewer()
        {
            InitializeComponent();

        }
        /// <summary>
        /// Shows the image.
        /// </summary>
        /// <param name="image">The image.</param>
        public void ShowImage(Bitmap image)
        {
            this.Width = image.Width;
            this.Height = image.Height;
            pictureBox.Image = image;
            this.Show();
        }

    }
}
