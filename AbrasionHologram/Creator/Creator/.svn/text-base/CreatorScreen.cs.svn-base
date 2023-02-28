using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InputFileParser;
using Primitives;
using Utility;
using Microsoft.Win32;
using System.IO;

namespace Creator
{
    /// <summary>
    /// A Windows form allowing the user to open X3D files, manipulate the view, and create an ArcFile ready for printing.
    /// </summary>
    public partial class CreatorScreen : Form
    {
        private CreatorCanvas mView;

        /// <summary>The Most-RecentlyUsed menu keeping track of the X3D files the user has recently opened.</summary>
        protected MruStripMenuInline mruMenu;
        const string mruRegKey = @"SOFTWARE\HologramPrinter\CreatorMRU";

        private X3DFile mCurrentFile = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatorScreen"/> class.
        /// </summary>
        public CreatorScreen()
        {
            InitializeComponent();
            
            InitializeView();
            mArcViewContainer.ArcViewAspectRatio = ArcFileSettings.CanvasSize.Width / (double)ArcFileSettings.CanvasSize.Height;
            mArcViewContainer.ArcView = mView;
            
            InitializeMruStrip();

            ApplyOptions();

            //Load the last-opened file.
            if (mruMenu.NumEntries > 0)
            {
                string lastFile = mruMenu.GetFileAt(0);
                if (!string.IsNullOrEmpty(lastFile))
                {
                    OnMruFileClick(0, lastFile);
                }
            }
        }

        /// <summary>
        /// Initializes the view.
        /// </summary>
        public void InitializeView()
        {
            mView = new CreatorCanvas();

            mView.Anchor = System.Windows.Forms.AnchorStyles.None;
            mView.Name = "mView";
            mView.BorderStyle = BorderStyle.FixedSingle;
            mView.Size = new System.Drawing.Size(20, 15);
        }

        #region File Handling

        /// <summary>
        /// Opens the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        private void OpenFile(string fileName)
        {
            mCurrentFile = new X3DFile(fileName);
            mCurrentFile.Parse();
            mView.ShapeList = mCurrentFile.ShapeList;
        }

        /// <summary>
        /// Handles the Click event of the mnuOpen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "X3D Files (*.x3d)|*.x3d";
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                OpenFile(ofd.FileName);
                mruMenu.AddFile(ofd.FileName);
                mruMenu.SaveToRegistry();
            }
        }
        #endregion

        #region Recent Files List Initialization
        /// <summary>
        /// Initializes the MRU strip with values from the registry.
        /// </summary>
        private void InitializeMruStrip()
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(mruRegKey);
            mruMenu = new MruStripMenuInline(mnuFile, mnuRecentFiles, new MruStripMenu.ClickedHandler(OnMruFileClick), mruRegKey, 8);
            mruMenu.LoadFromRegistry();
        }
        /// <summary>
        /// Called when the user clicks an item in the MRU list. Opens the specified file.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="filename">The filename.</param>
        private void OnMruFileClick(int number, String filename)
        {
            FileInfo file = new FileInfo(filename);
            if (file.Exists)
            {
                if (file.Extension.ToLower().Equals(".x3d"))
                {
                    OpenFile(filename);
                    mruMenu.SetFirstFile(number);
                }
                else
                {
                    MessageBox.Show("The file '" + filename + "' is not an x3d file and will be removed from the Recent Files(s) list."
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

        #region Point Resolution

        /// <summary>
        /// Handles the ValueChanged event of the mResolutionTrackBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mResolutionTrackBar_ValueChanged(object sender, EventArgs e)
        {
            txtResolution.Text = mResolutionTrackBar.Value.ToString();
            ApplyOptions();
        }
        /// <summary>
        /// Applies the max resolution.
        /// </summary>
        private void ApplyMaxResolution()
        {
            int val;
            if (int.TryParse(txtMaxResolution.Text, out val))
            {
                int newVal = Math.Min(val, mResolutionTrackBar.Value);
                txtResolution.Text = newVal.ToString();
                mResolutionTrackBar.Value = newVal;
                mResolutionTrackBar.Maximum = val;
                mResolutionTrackBar.TickFrequency = val;
            }
            else
            {
                MessageBox.Show("Please enter an integer value.", "Invalid Max Resolution Specified", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Handles the PreviewKeyDown event of the txtResolution and txtMaxResolution controls.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PreviewKeyDownEventArgs"/> instance containing the event data.</param>
        private void txtResolution_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                ApplyResolution();
        }
        /// <summary>
        /// Handles the Leave event of the txtResolution and txtMaxResolution control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void txtResolution_Leave(object sender, EventArgs e)
        {
            ApplyResolution();
        }
        /// <summary>
        /// Applies the resolution specified by the mResolutionTrackBar.
        /// </summary>
        private void ApplyResolution()
        {
            int val;
            if (int.TryParse(txtResolution.Text, out val))
            {
                int newMax = Math.Max(mResolutionTrackBar.Maximum, val);
                txtMaxResolution.Text = newMax.ToString();
                mResolutionTrackBar.Maximum = newMax;
                mResolutionTrackBar.TickFrequency = newMax;
                mResolutionTrackBar.Value = val;
            }
            else
            {
                MessageBox.Show("Please enter an integer value.", "Invalid Resolution Specified", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Option Control

        /// <summary>
        /// Occurs when the user changes an option.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void optionChanged(object sender, EventArgs e)
        {
            ApplyOptions();
        }

        /// <summary>
        /// Applies the options selected on the screen.
        /// </summary>
        private void ApplyOptions()
        {
            mView.ShowArcs = mnuShowArcs.Checked;
            mView.ShowPoints = mnuPoints.Checked;
            mView.ShowLines = mnuLines.Checked;
            splitContainer.Panel2Collapsed = !mView.ShowPoints;
            DrawOptions.VisibilityMode = mnuHiddenLine.Checked ? VisibilityMode.HiddenLine : VisibilityMode.Transparent;
            mView.AntiAlias = mnuAntiAlias.Checked;


            if (mnuRedBlue.Checked)
                mView.ViewMode = ViewMode.RedBlue;
            else if (mnuStereoscopic.Checked)
                mView.ViewMode = ViewMode.Stereoscopic;
            else
                mView.ViewMode = ViewMode.Normal;

            DrawOptions.CoordsPerUnitLength = mResolutionTrackBar.Value / 100.0;
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
        /// Handles the Click event of the mnuArcFileOptions control. Opens the screen allowing the user to choose options for ArcFile creation.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuArcFileOptions_Click(object sender, EventArgs e)
        {
            using (ArcFileOptionChooser options = new ArcFileOptionChooser())
            {
                options.ShowDialog();
                mArcViewContainer.ArcViewAspectRatio = ArcFileSettings.CanvasSize.Width / (double)ArcFileSettings.CanvasSize.Height;
            }
        }

        #endregion

        #region Tools

        /// <summary>
        /// Handles the Click event of the mnuSwitchBackFront control. Switches back- and front-facing polygons in the currently opened file.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuSwitchBackFront_Click(object sender, EventArgs e)
        {
            if (mCurrentFile != null)
            {
                mCurrentFile.SwitchBackFront();
                OpenFile(mCurrentFile.FullPath);
            }
        }

        #endregion

        /// <summary>
        /// Handles the Click event of the mnuGenerateArcFile control. Opens a ViewerScreen allowing the ArcFile to be printed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void mnuGenerateArcFile_Click(object sender, EventArgs e)
        {
            ArcFileCreator.ShapeList = mView.ShapeList;
            using (Utility.Progress p = new Progress())
            {
                p.Operation = ArcFileCreator.CreateArcFile;
                if (p.ShowDialog() == DialogResult.OK)
                {
                    ArcFile arcFile = ArcFileCreator.ArcFile;
                    Viewer.ViewerScreen viewerScreen = new Viewer.ViewerScreen();

                    viewerScreen.ArcFile = arcFile;
                    viewerScreen.Show();
                }
            }
        }
    }
}
