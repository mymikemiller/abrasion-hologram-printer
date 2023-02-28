namespace Utility
{
    partial class ArcViewContainer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mViewAngleTrackBar = new System.Windows.Forms.TrackBar();
            this.mAutoAngleTimer = new System.Windows.Forms.Timer(this.components);
            this.mProportionalControl = new Utility.ProportionalControl();
            ((System.ComponentModel.ISupportInitialize)(this.mViewAngleTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // mViewAngleTrackBar
            // 
            this.mViewAngleTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mViewAngleTrackBar.Location = new System.Drawing.Point(0, 3);
            this.mViewAngleTrackBar.Maximum = 450;
            this.mViewAngleTrackBar.Minimum = -450;
            this.mViewAngleTrackBar.Name = "mViewAngleTrackBar";
            this.mViewAngleTrackBar.Size = new System.Drawing.Size(477, 45);
            this.mViewAngleTrackBar.TabIndex = 3;
            this.mViewAngleTrackBar.TickFrequency = 450;
            this.mViewAngleTrackBar.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ArcViewContainer_PreviewKeyDown);
            this.mViewAngleTrackBar.ValueChanged += new System.EventHandler(this.mViewAngleTrackBar_ValueChanged);
            // 
            // mAutoAngleTimer
            // 
            this.mAutoAngleTimer.Interval = 50;
            this.mAutoAngleTimer.Tick += new System.EventHandler(this.mAutoAngleTimer_Tick);
            // 
            // mProportionalControl
            // 
            this.mProportionalControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mProportionalControl.AspectRatio = 1;
            this.mProportionalControl.InnerControl = null;
            this.mProportionalControl.Location = new System.Drawing.Point(14, 54);
            this.mProportionalControl.Name = "mProportionalControl";
            this.mProportionalControl.Size = new System.Drawing.Size(452, 305);
            this.mProportionalControl.TabIndex = 4;
            // 
            // ArcViewContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mProportionalControl);
            this.Controls.Add(this.mViewAngleTrackBar);
            this.Name = "ArcViewContainer";
            this.Size = new System.Drawing.Size(477, 372);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ArcViewContainer_PreviewKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.mViewAngleTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar mViewAngleTrackBar;
        private ProportionalControl mProportionalControl;
        private System.Windows.Forms.Timer mAutoAngleTimer;
    }
}
