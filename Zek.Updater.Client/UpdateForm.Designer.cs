namespace Zek.Updater.Client
{
    partial class UpdateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateForm));
            this.progressDownload = new System.Windows.Forms.ProgressBar();
            this.progressExtract = new System.Windows.Forms.ProgressBar();
            this.lblDownload = new System.Windows.Forms.Label();
            this.lblExtract = new System.Windows.Forms.Label();
            this.bwDownload = new System.ComponentModel.BackgroundWorker();
            this.bwExtract = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // progressDownload
            // 
            this.progressDownload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressDownload.Location = new System.Drawing.Point(12, 12);
            this.progressDownload.Name = "progressDownload";
            this.progressDownload.Size = new System.Drawing.Size(380, 23);
            this.progressDownload.Step = 1;
            this.progressDownload.TabIndex = 0;
            // 
            // progressExtract
            // 
            this.progressExtract.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressExtract.Location = new System.Drawing.Point(12, 93);
            this.progressExtract.Name = "progressExtract";
            this.progressExtract.Size = new System.Drawing.Size(377, 23);
            this.progressExtract.Step = 1;
            this.progressExtract.TabIndex = 1;
            // 
            // lblDownload
            // 
            this.lblDownload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDownload.Location = new System.Drawing.Point(12, 38);
            this.lblDownload.Name = "lblDownload";
            this.lblDownload.Size = new System.Drawing.Size(380, 40);
            this.lblDownload.TabIndex = 2;
            this.lblDownload.Text = "...";
            // 
            // lblExtract
            // 
            this.lblExtract.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExtract.Location = new System.Drawing.Point(12, 119);
            this.lblExtract.Name = "lblExtract";
            this.lblExtract.Size = new System.Drawing.Size(380, 40);
            this.lblExtract.TabIndex = 3;
            this.lblExtract.Text = "Waiting to download...";
            // 
            // bwDownload
            // 
            this.bwDownload.WorkerReportsProgress = true;
            this.bwDownload.WorkerSupportsCancellation = true;
            this.bwDownload.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwDownload_DoWork);
            this.bwDownload.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwDownload_ProgressChanged);
            this.bwDownload.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwDownload_RunWorkerCompleted);
            // 
            // bwExtract
            // 
            this.bwExtract.WorkerReportsProgress = true;
            this.bwExtract.WorkerSupportsCancellation = true;
            this.bwExtract.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwExtract_DoWork);
            this.bwExtract.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwExtract_ProgressChanged);
            this.bwExtract.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwExtract_RunWorkerCompleted);
            // 
            // UpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 172);
            this.Controls.Add(this.lblExtract);
            this.Controls.Add(this.lblDownload);
            this.Controls.Add(this.progressExtract);
            this.Controls.Add(this.progressDownload);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UpdateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Available";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UpdateForm_FormClosed);
            this.Shown += new System.EventHandler(this.UpdateForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressDownload;
        private System.Windows.Forms.ProgressBar progressExtract;
        private System.Windows.Forms.Label lblDownload;
        private System.Windows.Forms.Label lblExtract;
        private System.ComponentModel.BackgroundWorker bwDownload;
        private System.ComponentModel.BackgroundWorker bwExtract;
    }
}

