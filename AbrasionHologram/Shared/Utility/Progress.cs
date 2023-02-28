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
    /// <summary>
    /// Occurs when the operation is cancelled.
    /// </summary>
    public delegate void OperationCancelledHandler();

    /// <summary>
    /// Provides a simple form with a progress bar and cancel button. Progress bar automatically updates as the background process reports its progress.
    /// </summary>
    public partial class Progress : Form
    {
        public delegate void BackgroundOperation(BackgroundWorker worker);

        /// <summary>
        /// Gets or sets the operation to execute.
        /// </summary>
        /// <value>The operation.</value>
        public BackgroundOperation Operation { get; set; }

        private DialogResult result = DialogResult.OK;

        /// <summary>
        /// Initializes a new instance of the <see cref="Progress"/> class.
        /// </summary>
        public Progress()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the Progress control. Operation property must be set before showing the Progress form
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Progress_Load(object sender, EventArgs e)
        {
            if (Operation == null)
                throw new Exception("Operation property must be set before showing the Progress form");

            progressBar.Value = 0;

            backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            backgroundWorker.CancelAsync();
            result = DialogResult.Cancel;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Operation(backgroundWorker);
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.DialogResult = result;
            this.Close();
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = Math.Min(e.ProgressPercentage, 100);
        }



    }
}
