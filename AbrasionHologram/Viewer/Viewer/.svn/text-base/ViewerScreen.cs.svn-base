using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utility;
using Microsoft.Win32;
using System.IO;

namespace Viewer
{
    /// <summary>
    /// Provides options for viewing and printing ArcFiles.
    /// </summary>
    public partial class ViewerScreen : Form
    {
        private ViewerCanvas mView;

        protected MruStripMenuInline mruMenu;
        static string mruRegKey = @"SOFTWARE\HologramPrinter\ViewerMRU";

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewerScreen"/> class.
        /// </summary>
        public ViewerScreen()
        {
            InitializeComponent();
            InitializeView();
            mArcViewContainer.ArcView = mView;

            InitializeMruStrip();

            if (mruMenu.NumEntries > 0)
            {
                string lastFile = mruMenu.GetFileAt(0);
                if (!string.IsNullOrEmpty(lastFile))
                {
                    OnMruFileClick(0, lastFile);
                }
            }
            ApplyOptions();            
        }



        /// <summary>
        /// Initializes the view.
        /// </summary>
        public void InitializeView()
        {
            mView = new ViewerCanvas();
            mView.BackColor = System.Drawing.SystemColors.Control;
            mView.BorderStyle = BorderStyle.FixedSingle;
            mView.Dirty = false;
            mView.Location = new System.Drawing.Point(0, 0);
            mView.Name = "mView";
            mView.Size = new System.Drawing.Size(20, 15);
            mView.ViewAngle = 0;
        }


        #region Recent Files List Initialization
        /// <summary>
        /// Initializes the Most Recently Used strip.
        /// </summary>
        private void InitializeMruStrip()
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(mruRegKey);
            mruMenu = new MruStripMenuInline(mnuFile, mnuRecentFiles, new MruStripMenu.ClickedHandler(OnMruFileClick), mruRegKey, 8);
            mruMenu.LoadFromRegistry();
        }
        /// <summary>
        /// Called when an MRU item is clicked.
        /// </summary>
        /// <param name="number">The number of the item clicked.</param>
        /// <param name="filename">The filename.</param>
        private void OnMruFileClick(int number, String filename)
        {
            FileInfo file = new FileInfo(filename);
            if (file.Exists)
            {
                if (file.Extension.ToLower().Equals(".arc"))
                {
                    OpenFile(filename);
                    mruMenu.SetFirstFile(number);
                }
                else
                {
                    MessageBox.Show("The file '" + filename + "' is not an arc file and will be removed from the Recent Files(s) list."
                    , "Error opening file"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
                    mruMenu.RemoveFile(number);
                }
            }
            else
            {
                MessageBox.Show("The file '" + filename + "' cannot be opened and will be removed from the Recent Files(s) list."
                    , "Error opening file"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
                mruMenu.RemoveFile(number);
            }
            mruMenu.SaveToRegistry();
        }
        #endregion


        /// <summary>
        /// Gets or sets the arc file to display.
        /// </summary>
        /// <value>The arc file.</value>
        public ArcFile ArcFile
        {
            get { return mView.ArcFile; }
            set 
            {
                if (mView.ArcFile == value)
                    return;

                mView.ArcFile = value;
            }
        }

        /// <summary>
        /// Handles the Click event of the mnuSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Arc File (.arc)|*.arc";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (sfd.FileName != "")
                    {
                        ArcFile.SaveAs(sfd.FileName);
                        mruMenu.AddFile(sfd.FileName);
                        mruMenu.SaveToRegistry();
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the mnuOpen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Arc File (.arc)|*.arc";
                ofd.Multiselect = false;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (ofd.FileName != "")
                    {
                        OpenFile(ofd.FileName);
                        mruMenu.AddFile(ofd.FileName);
                        mruMenu.SaveToRegistry();
                    }
                }
            }
        }

        /// <summary>
        /// Opens the specified file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        private void OpenFile(string fileName)
        {
            ArcFile = new ArcFile(fileName);
        }

        /// <summary>
        /// Handles an OptionChanged event by applying the options.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void optionChanged(object sender, EventArgs e)
        {
            ApplyOptions();
        }
        private void ApplyOptions()
        {
            mView.ShowArcs = mnuArcs.Checked;
            mView.ShowPoints = mnuPoints.Checked;
            mView.AntiAlias = mnuAntiAlias.Checked;

            if (mnuRedBlue.Checked)
                mView.ViewMode = ViewMode.RedBlue;
            else if (mnuStereoscopic.Checked)
                mView.ViewMode = ViewMode.Stereoscopic;
            else
                mView.ViewMode = ViewMode.Normal;
        }


        /// <summary>
        /// Handles the Click event of the mnuViewMode control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuViewMode_Click(object sender, EventArgs e)
        {
            mnuNormal.Checked = false;
            mnuRedBlue.Checked = false;
            mnuStereoscopic.Checked = false;

            ((ToolStripMenuItem)sender).Checked = true;

            ApplyOptions();
        }

        /// <summary>
        /// Handles the Click event of the mnuExit control. Closes the screen.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuExit_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Handles the Click event of the mnuOptimizeArcOrder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuOptimizeArcOrder_Click(object sender, EventArgs e)
        {
            List<ArcSection> after = ArcFileOptimizer.Optimize(ArcFile);
            MessageBox.Show("Optimization complete.", "Arc Order Optimization", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
