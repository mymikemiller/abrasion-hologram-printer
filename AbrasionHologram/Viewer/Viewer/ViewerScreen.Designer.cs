namespace Viewer
{
    partial class ViewerScreen
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRecentFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuArcs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPoints = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuNormal = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRedBlue = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStereoscopic = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAntiAlias = new System.Windows.Forms.ToolStripMenuItem();
            this.mArcViewContainer = new Utility.ArcViewContainer();
            this.mnuOptimizeArcOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuView,
            this.mnuTools});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(952, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSave,
            this.mnuOpen,
            this.toolStripSeparator3,
            this.mnuRecentFiles,
            this.toolStripSeparator4,
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(35, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuSave
            // 
            this.mnuSave.Name = "mnuSave";
            this.mnuSave.Size = new System.Drawing.Size(152, 22);
            this.mnuSave.Text = "&Save";
            this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // mnuOpen
            // 
            this.mnuOpen.Name = "mnuOpen";
            this.mnuOpen.Size = new System.Drawing.Size(152, 22);
            this.mnuOpen.Text = "&Open";
            this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuRecentFiles
            // 
            this.mnuRecentFiles.Name = "mnuRecentFiles";
            this.mnuRecentFiles.Size = new System.Drawing.Size(152, 22);
            this.mnuRecentFiles.Text = "Recent Files";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(152, 22);
            this.mnuExit.Text = "E&xit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuArcs,
            this.mnuPoints,
            this.toolStripSeparator1,
            this.mnuNormal,
            this.mnuRedBlue,
            this.mnuStereoscopic,
            this.toolStripSeparator2,
            this.mnuAntiAlias});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(41, 20);
            this.mnuView.Text = "&View";
            // 
            // mnuArcs
            // 
            this.mnuArcs.Checked = true;
            this.mnuArcs.CheckOnClick = true;
            this.mnuArcs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuArcs.Name = "mnuArcs";
            this.mnuArcs.Size = new System.Drawing.Size(152, 22);
            this.mnuArcs.Text = "Arcs";
            this.mnuArcs.Click += new System.EventHandler(this.optionChanged);
            // 
            // mnuPoints
            // 
            this.mnuPoints.Checked = true;
            this.mnuPoints.CheckOnClick = true;
            this.mnuPoints.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuPoints.Name = "mnuPoints";
            this.mnuPoints.ShowShortcutKeys = false;
            this.mnuPoints.Size = new System.Drawing.Size(152, 22);
            this.mnuPoints.Text = "Points";
            this.mnuPoints.Click += new System.EventHandler(this.optionChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuNormal
            // 
            this.mnuNormal.Checked = true;
            this.mnuNormal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuNormal.Name = "mnuNormal";
            this.mnuNormal.Size = new System.Drawing.Size(152, 22);
            this.mnuNormal.Text = "Normal";
            this.mnuNormal.Click += new System.EventHandler(this.mnuViewMode_Click);
            // 
            // mnuRedBlue
            // 
            this.mnuRedBlue.Name = "mnuRedBlue";
            this.mnuRedBlue.Size = new System.Drawing.Size(152, 22);
            this.mnuRedBlue.Text = "Red/Blue";
            this.mnuRedBlue.Click += new System.EventHandler(this.mnuViewMode_Click);
            // 
            // mnuStereoscopic
            // 
            this.mnuStereoscopic.Name = "mnuStereoscopic";
            this.mnuStereoscopic.Size = new System.Drawing.Size(152, 22);
            this.mnuStereoscopic.Text = "Stereoscopic";
            this.mnuStereoscopic.Click += new System.EventHandler(this.mnuViewMode_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuAntiAlias
            // 
            this.mnuAntiAlias.CheckOnClick = true;
            this.mnuAntiAlias.Name = "mnuAntiAlias";
            this.mnuAntiAlias.Size = new System.Drawing.Size(152, 22);
            this.mnuAntiAlias.Text = "Anti-Alias";
            this.mnuAntiAlias.CheckedChanged += new System.EventHandler(this.optionChanged);
            // 
            // mArcViewContainer
            // 
            this.mArcViewContainer.ArcView = null;
            this.mArcViewContainer.ArcViewAspectRatio = 1;
            this.mArcViewContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mArcViewContainer.Location = new System.Drawing.Point(0, 24);
            this.mArcViewContainer.Name = "mArcViewContainer";
            this.mArcViewContainer.Size = new System.Drawing.Size(952, 662);
            this.mArcViewContainer.TabIndex = 2;
            // 
            // mnuOptimizeArcOrder
            // 
            this.mnuOptimizeArcOrder.Name = "mnuOptimizeArcOrder";
            this.mnuOptimizeArcOrder.Size = new System.Drawing.Size(176, 22);
            this.mnuOptimizeArcOrder.Text = "Optimize Arc Order";
            this.mnuOptimizeArcOrder.Click += new System.EventHandler(this.mnuOptimizeArcOrder_Click);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOptimizeArcOrder});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(44, 20);
            this.mnuTools.Text = "Tools";
            // 
            // ViewerScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 686);
            this.Controls.Add(this.mArcViewContainer);
            this.Controls.Add(this.menuStrip1);
            this.Location = new System.Drawing.Point(600, 300);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ViewerScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Abrasion Hologram Viewer";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuSave;
        private System.Windows.Forms.ToolStripMenuItem mnuOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuView;
        private System.Windows.Forms.ToolStripMenuItem mnuArcs;
        private Utility.ArcViewContainer mArcViewContainer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuNormal;
        private System.Windows.Forms.ToolStripMenuItem mnuRedBlue;
        private System.Windows.Forms.ToolStripMenuItem mnuStereoscopic;
        private System.Windows.Forms.ToolStripMenuItem mnuPoints;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuAntiAlias;
        private System.Windows.Forms.ToolStripMenuItem mnuRecentFiles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripMenuItem mnuOptimizeArcOrder;
    }
}

