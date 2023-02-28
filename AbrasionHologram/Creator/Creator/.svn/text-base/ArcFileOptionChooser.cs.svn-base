using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Creator
{
    /// <summary>A form allowing the user to choose options specifying how ArcFiles are to be created</summary>
    public partial class ArcFileOptionChooser : Form
    {
        /// <summary>Creates a new ArcFileOptionChoose form</summary>
        public ArcFileOptionChooser()
        {
            InitializeComponent();
            txtWidth.Text = ArcFileSettings.CanvasSize.Width.ToString();
            txtHeight.Text = ArcFileSettings.CanvasSize.Height.ToString();
            txtAngularResolution.Text = ArcFileSettings.AngularResolution.ToString();
            chkQuickMode.Checked = ArcFileSettings.QuickMode;
        }

        /// <summary>
        /// Handles the Click event of the btnOK control. Sets the options based on the information selected on the screen.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            bool error = false;
            double width = 0;
            double height = 0;
            double angularResolution = 0;
            if(!double.TryParse(txtWidth.Text, out width))
                error = true;
            if(!double.TryParse(txtHeight.Text, out height))
                error = true;
            if(!double.TryParse(txtAngularResolution.Text, out angularResolution))
                error = true;
            if (width <= 0)
                error = true;
            if (height <= 0)
                error = true;
            if (angularResolution <= 0)
                error = true;

            if(error)
                MessageBox.Show("Please enter valid, positive decimal values.", "Invalid values specified", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                ArcFileSettings.CanvasSize = new SizeF((float)width, (float)height);
                ArcFileSettings.AngularResolution = angularResolution;
                ArcFileSettings.QuickMode = chkQuickMode.Checked;
            }

            if (!error)
                Close();
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control. Closes the form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
