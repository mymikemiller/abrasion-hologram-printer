namespace Creator
{
    partial class CreatorScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRecentFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowArcs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPoints = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLines = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHiddenLine = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuNormal = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRedBlue = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStereoscopic = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAntiAlias = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuArcFileOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGenerateArcFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileManipulation = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSwitchBackFront = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.mArcViewContainer = new Utility.ArcViewContainer();
            this.grpResolution = new System.Windows.Forms.GroupBox();
            this.txtMaxResolution = new System.Windows.Forms.TextBox();
            this.lblMax = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.mResolutionTrackBar = new System.Windows.Forms.TrackBar();
            this.txtResolution = new System.Windows.Forms.TextBox();
            this.menuStrip.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.grpResolution.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mResolutionTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuView,
            this.toolsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(952, 24);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpen,
            this.toolStripSeparator5,
            this.mnuRecentFiles,
            this.toolStripSeparator2,
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(35, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuOpen
            // 
            this.mnuOpen.Name = "mnuOpen";
            this.mnuOpen.Size = new System.Drawing.Size(143, 22);
            this.mnuOpen.Text = "&Open";
            this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(140, 6);
            // 
            // mnuRecentFiles
            // 
            this.mnuRecentFiles.Name = "mnuRecentFiles";
            this.mnuRecentFiles.Size = new System.Drawing.Size(143, 22);
            this.mnuRecentFiles.Text = "Recent Files";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(140, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(143, 22);
            this.mnuExit.Text = "E&xit";
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShowArcs,
            this.mnuPoints,
            this.mnuLines,
            this.toolStripSeparator1,
            this.mnuHiddenLine,
            this.toolStripSeparator3,
            this.mnuNormal,
            this.mnuRedBlue,
            this.mnuStereoscopic,
            this.toolStripSeparator4,
            this.mnuAntiAlias});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(41, 20);
            this.mnuView.Text = "&View";
            // 
            // mnuShowArcs
            // 
            this.mnuShowArcs.CheckOnClick = true;
            this.mnuShowArcs.Name = "mnuShowArcs";
            this.mnuShowArcs.Size = new System.Drawing.Size(146, 22);
            this.mnuShowArcs.Text = "&Arcs";
            this.mnuShowArcs.Click += new System.EventHandler(this.optionChanged);
            // 
            // mnuPoints
            // 
            this.mnuPoints.CheckOnClick = true;
            this.mnuPoints.Name = "mnuPoints";
            this.mnuPoints.Size = new System.Drawing.Size(146, 22);
            this.mnuPoints.Text = "&Points";
            this.mnuPoints.CheckedChanged += new System.EventHandler(this.optionChanged);
            // 
            // mnuLines
            // 
            this.mnuLines.Checked = true;
            this.mnuLines.CheckOnClick = true;
            this.mnuLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuLines.Name = "mnuLines";
            this.mnuLines.Size = new System.Drawing.Size(146, 22);
            this.mnuLines.Text = "&Lines";
            this.mnuLines.CheckedChanged += new System.EventHandler(this.optionChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // mnuHiddenLine
            // 
            this.mnuHiddenLine.CheckOnClick = true;
            this.mnuHiddenLine.Name = "mnuHiddenLine";
            this.mnuHiddenLine.Size = new System.Drawing.Size(146, 22);
            this.mnuHiddenLine.Text = "&Hidden Line";
            this.mnuHiddenLine.CheckedChanged += new System.EventHandler(this.optionChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(143, 6);
            // 
            // mnuNormal
            // 
            this.mnuNormal.Checked = true;
            this.mnuNormal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuNormal.Name = "mnuNormal";
            this.mnuNormal.Size = new System.Drawing.Size(146, 22);
            this.mnuNormal.Text = "&Normal";
            this.mnuNormal.Click += new System.EventHandler(this.mnuViewMode_Click);
            // 
            // mnuRedBlue
            // 
            this.mnuRedBlue.Name = "mnuRedBlue";
            this.mnuRedBlue.Size = new System.Drawing.Size(146, 22);
            this.mnuRedBlue.Text = "&Red / Blue";
            this.mnuRedBlue.Click += new System.EventHandler(this.mnuViewMode_Click);
            // 
            // mnuStereoscopic
            // 
            this.mnuStereoscopic.Name = "mnuStereoscopic";
            this.mnuStereoscopic.Size = new System.Drawing.Size(146, 22);
            this.mnuStereoscopic.Text = "&Stereoscopic";
            this.mnuStereoscopic.Click += new System.EventHandler(this.mnuViewMode_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(143, 6);
            // 
            // mnuAntiAlias
            // 
            this.mnuAntiAlias.CheckOnClick = true;
            this.mnuAntiAlias.Name = "mnuAntiAlias";
            this.mnuAntiAlias.Size = new System.Drawing.Size(146, 22);
            this.mnuAntiAlias.Text = "Anti-Alias";
            this.mnuAntiAlias.CheckedChanged += new System.EventHandler(this.optionChanged);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuArcFileOptions,
            this.mnuGenerateArcFile,
            this.toolStripSeparator6,
            this.mnuFileManipulation});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // mnuArcFileOptions
            // 
            this.mnuArcFileOptions.Name = "mnuArcFileOptions";
            this.mnuArcFileOptions.Size = new System.Drawing.Size(168, 22);
            this.mnuArcFileOptions.Text = "Arc File &Options";
            this.mnuArcFileOptions.Click += new System.EventHandler(this.mnuArcFileOptions_Click);
            // 
            // mnuGenerateArcFile
            // 
            this.mnuGenerateArcFile.Name = "mnuGenerateArcFile";
            this.mnuGenerateArcFile.Size = new System.Drawing.Size(168, 22);
            this.mnuGenerateArcFile.Text = "&Generate Arc File";
            this.mnuGenerateArcFile.Click += new System.EventHandler(this.mnuGenerateArcFile_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(165, 6);
            // 
            // mnuFileManipulation
            // 
            this.mnuFileManipulation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSwitchBackFront});
            this.mnuFileManipulation.Name = "mnuFileManipulation";
            this.mnuFileManipulation.Size = new System.Drawing.Size(168, 22);
            this.mnuFileManipulation.Text = "&File Manipulation";
            this.mnuFileManipulation.ToolTipText = "Provides tools to permanently alter the input file";
            // 
            // mnuSwitchBackFront
            // 
            this.mnuSwitchBackFront.Name = "mnuSwitchBackFront";
            this.mnuSwitchBackFront.Size = new System.Drawing.Size(177, 22);
            this.mnuSwitchBackFront.Tag = "";
            this.mnuSwitchBackFront.Text = "Switch Back / Front";
            this.mnuSwitchBackFront.ToolTipText = "Turns Front-Facing polygons into Back-Facing and vice-versa. Use this tool if the" +
                " object looks inside-out in Hidden Line mode.";
            this.mnuSwitchBackFront.Click += new System.EventHandler(this.mnuSwitchBackFront_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.mArcViewContainer);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpResolution);
            this.splitContainer.Size = new System.Drawing.Size(952, 662);
            this.splitContainer.SplitterDistance = 875;
            this.splitContainer.TabIndex = 3;
            // 
            // mArcViewContainer
            // 
            this.mArcViewContainer.ArcView = null;
            this.mArcViewContainer.ArcViewAspectRatio = 1;
            this.mArcViewContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mArcViewContainer.Location = new System.Drawing.Point(0, 0);
            this.mArcViewContainer.Name = "mArcViewContainer";
            this.mArcViewContainer.Size = new System.Drawing.Size(875, 662);
            this.mArcViewContainer.TabIndex = 0;
            // 
            // grpResolution
            // 
            this.grpResolution.Controls.Add(this.txtMaxResolution);
            this.grpResolution.Controls.Add(this.lblMax);
            this.grpResolution.Controls.Add(this.lblValue);
            this.grpResolution.Controls.Add(this.mResolutionTrackBar);
            this.grpResolution.Controls.Add(this.txtResolution);
            this.grpResolution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpResolution.Location = new System.Drawing.Point(0, 0);
            this.grpResolution.Name = "grpResolution";
            this.grpResolution.Size = new System.Drawing.Size(73, 662);
            this.grpResolution.TabIndex = 2;
            this.grpResolution.TabStop = false;
            this.grpResolution.Text = "Resolution";
            // 
            // txtMaxResolution
            // 
            this.txtMaxResolution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxResolution.Location = new System.Drawing.Point(6, 36);
            this.txtMaxResolution.Name = "txtMaxResolution";
            this.txtMaxResolution.Size = new System.Drawing.Size(60, 20);
            this.txtMaxResolution.TabIndex = 4;
            this.txtMaxResolution.Text = "5000";
            this.txtMaxResolution.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtResolution_PreviewKeyDown);
            this.txtMaxResolution.Leave += new System.EventHandler(this.txtResolution_Leave);
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Location = new System.Drawing.Point(7, 20);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(30, 13);
            this.lblMax.TabIndex = 3;
            this.lblMax.Text = "Max:";
            // 
            // lblValue
            // 
            this.lblValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(7, 59);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(37, 13);
            this.lblValue.TabIndex = 2;
            this.lblValue.Text = "Value:";
            // 
            // mResolutionTrackBar
            // 
            this.mResolutionTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.mResolutionTrackBar.Location = new System.Drawing.Point(14, 101);
            this.mResolutionTrackBar.Maximum = 5000;
            this.mResolutionTrackBar.Name = "mResolutionTrackBar";
            this.mResolutionTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.mResolutionTrackBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mResolutionTrackBar.Size = new System.Drawing.Size(45, 555);
            this.mResolutionTrackBar.TabIndex = 0;
            this.mResolutionTrackBar.TickFrequency = 5000;
            this.mResolutionTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.mResolutionTrackBar.Value = 800;
            this.mResolutionTrackBar.ValueChanged += new System.EventHandler(this.mResolutionTrackBar_ValueChanged);
            // 
            // txtResolution
            // 
            this.txtResolution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResolution.Location = new System.Drawing.Point(6, 75);
            this.txtResolution.Name = "txtResolution";
            this.txtResolution.Size = new System.Drawing.Size(60, 20);
            this.txtResolution.TabIndex = 1;
            this.txtResolution.Text = "800";
            this.txtResolution.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtResolution_PreviewKeyDown);
            this.txtResolution.Leave += new System.EventHandler(this.txtResolution_Leave);
            // 
            // CreatorScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 686);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuStrip);
            this.Location = new System.Drawing.Point(600, 300);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "CreatorScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Abrasion Hologram Creator";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.grpResolution.ResumeLayout(false);
            this.grpResolution.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mResolutionTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuView;
        private System.Windows.Forms.ToolStripMenuItem mnuShowArcs;
        private System.Windows.Forms.ToolStripMenuItem mnuHiddenLine;
        private System.Windows.Forms.ToolStripMenuItem mnuOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuRecentFiles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnuNormal;
        private System.Windows.Forms.ToolStripMenuItem mnuRedBlue;
        private System.Windows.Forms.ToolStripMenuItem mnuStereoscopic;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuFileManipulation;
        private System.Windows.Forms.ToolStripMenuItem mnuSwitchBackFront;
        private System.Windows.Forms.ToolStripMenuItem mnuPoints;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TrackBar mResolutionTrackBar;
        private System.Windows.Forms.GroupBox grpResolution;
        private System.Windows.Forms.TextBox txtResolution;
        private System.Windows.Forms.TextBox txtMaxResolution;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem mnuArcFileOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuGenerateArcFile;
        private Utility.ArcViewContainer mArcViewContainer;
        private System.Windows.Forms.ToolStripMenuItem mnuLines;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuAntiAlias;
    }
}