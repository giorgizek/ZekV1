namespace Zek.Updater.Server
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabMain = new System.Windows.Forms.TabPage();
            this.dgvFile = new System.Windows.Forms.DataGridView();
            this.pnlTop1 = new System.Windows.Forms.Panel();
            this.cmbAppExeName = new System.Windows.Forms.ComboBox();
            this.cmbCompress = new System.Windows.Forms.ComboBox();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.txtCompressFolderName = new System.Windows.Forms.TextBox();
            this.lblCompressFolderName = new System.Windows.Forms.Label();
            this.lblCompress = new System.Windows.Forms.Label();
            this.lblAppExeName = new System.Windows.Forms.Label();
            this.txtApplicationName = new System.Windows.Forms.TextBox();
            this.lblApplicationName = new System.Windows.Forms.Label();
            this.numDirectoryLevel = new System.Windows.Forms.NumericUpDown();
            this.btnDeploy = new System.Windows.Forms.Button();
            this.lblDirectoryLevel = new System.Windows.Forms.Label();
            this.lblDirectoryDestination = new System.Windows.Forms.Label();
            this.lblDirectoryUpdate = new System.Windows.Forms.Label();
            this.txtDirectoryDestination = new System.Windows.Forms.TextBox();
            this.txtDirectoryUpdate = new System.Windows.Forms.TextBox();
            this.btnDirectoryDestination = new System.Windows.Forms.Button();
            this.btnDirectoryUpdate = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.progressFiles = new System.Windows.Forms.ToolStripProgressBar();
            this.tslblProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.bsDeploy = new System.ComponentModel.BackgroundWorker();
            this.chkDeleteBin = new System.Windows.Forms.CheckBox();
            this.tabControl.SuspendLayout();
            this.tabMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFile)).BeginInit();
            this.pnlTop1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDirectoryLevel)).BeginInit();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabMain);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(784, 540);
            this.tabControl.TabIndex = 0;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.dgvFile);
            this.tabMain.Controls.Add(this.pnlTop1);
            this.tabMain.Location = new System.Drawing.Point(4, 22);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabMain.Size = new System.Drawing.Size(776, 514);
            this.tabMain.TabIndex = 0;
            this.tabMain.Text = "Main";
            this.tabMain.UseVisualStyleBackColor = true;
            // 
            // dgvFile
            // 
            this.dgvFile.AllowUserToAddRows = false;
            this.dgvFile.AllowUserToDeleteRows = false;
            this.dgvFile.AllowUserToOrderColumns = true;
            this.dgvFile.AllowUserToResizeRows = false;
            this.dgvFile.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFile.Location = new System.Drawing.Point(3, 243);
            this.dgvFile.Name = "dgvFile";
            this.dgvFile.Size = new System.Drawing.Size(770, 268);
            this.dgvFile.TabIndex = 1;
            // 
            // pnlTop1
            // 
            this.pnlTop1.Controls.Add(this.chkDeleteBin);
            this.pnlTop1.Controls.Add(this.cmbAppExeName);
            this.pnlTop1.Controls.Add(this.cmbCompress);
            this.pnlTop1.Controls.Add(this.txtVersion);
            this.pnlTop1.Controls.Add(this.lblVersion);
            this.pnlTop1.Controls.Add(this.txtCompressFolderName);
            this.pnlTop1.Controls.Add(this.lblCompressFolderName);
            this.pnlTop1.Controls.Add(this.lblCompress);
            this.pnlTop1.Controls.Add(this.lblAppExeName);
            this.pnlTop1.Controls.Add(this.txtApplicationName);
            this.pnlTop1.Controls.Add(this.lblApplicationName);
            this.pnlTop1.Controls.Add(this.numDirectoryLevel);
            this.pnlTop1.Controls.Add(this.btnDeploy);
            this.pnlTop1.Controls.Add(this.lblDirectoryLevel);
            this.pnlTop1.Controls.Add(this.lblDirectoryDestination);
            this.pnlTop1.Controls.Add(this.lblDirectoryUpdate);
            this.pnlTop1.Controls.Add(this.txtDirectoryDestination);
            this.pnlTop1.Controls.Add(this.txtDirectoryUpdate);
            this.pnlTop1.Controls.Add(this.btnDirectoryDestination);
            this.pnlTop1.Controls.Add(this.btnDirectoryUpdate);
            this.pnlTop1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop1.Location = new System.Drawing.Point(3, 3);
            this.pnlTop1.Name = "pnlTop1";
            this.pnlTop1.Size = new System.Drawing.Size(770, 240);
            this.pnlTop1.TabIndex = 0;
            // 
            // cmbAppExeName
            // 
            this.cmbAppExeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAppExeName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAppExeName.FormattingEnabled = true;
            this.cmbAppExeName.Location = new System.Drawing.Point(164, 107);
            this.cmbAppExeName.Name = "cmbAppExeName";
            this.cmbAppExeName.Size = new System.Drawing.Size(500, 21);
            this.cmbAppExeName.TabIndex = 13;
            this.cmbAppExeName.SelectedIndexChanged += new System.EventHandler(this.cmbAppExeName_SelectedIndexChanged);
            // 
            // cmbCompress
            // 
            this.cmbCompress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCompress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompress.FormattingEnabled = true;
            this.cmbCompress.Items.AddRange(new object[] {
            "",
            "GZip",
            "7Zip"});
            this.cmbCompress.Location = new System.Drawing.Point(164, 186);
            this.cmbCompress.Name = "cmbCompress";
            this.cmbCompress.Size = new System.Drawing.Size(500, 21);
            this.cmbCompress.TabIndex = 19;
            this.cmbCompress.SelectedIndexChanged += new System.EventHandler(this.cmbCompress_SelectedIndexChanged);
            // 
            // txtVersion
            // 
            this.txtVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVersion.Location = new System.Drawing.Point(164, 134);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            this.txtVersion.Size = new System.Drawing.Size(500, 20);
            this.txtVersion.TabIndex = 15;
            this.txtVersion.TextChanged += new System.EventHandler(this.txtVersion_TextChanged);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(3, 137);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(45, 13);
            this.lblVersion.TabIndex = 14;
            this.lblVersion.Text = "Version:";
            // 
            // txtCompressFolderName
            // 
            this.txtCompressFolderName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCompressFolderName.Location = new System.Drawing.Point(164, 160);
            this.txtCompressFolderName.Name = "txtCompressFolderName";
            this.txtCompressFolderName.Size = new System.Drawing.Size(500, 20);
            this.txtCompressFolderName.TabIndex = 17;
            this.txtCompressFolderName.TextChanged += new System.EventHandler(this.txtCompressFolderName_TextChanged);
            // 
            // lblCompressFolderName
            // 
            this.lblCompressFolderName.AutoSize = true;
            this.lblCompressFolderName.Location = new System.Drawing.Point(3, 163);
            this.lblCompressFolderName.Name = "lblCompressFolderName";
            this.lblCompressFolderName.Size = new System.Drawing.Size(119, 13);
            this.lblCompressFolderName.TabIndex = 16;
            this.lblCompressFolderName.Text = "Compress Folder Name:";
            // 
            // lblCompress
            // 
            this.lblCompress.AutoSize = true;
            this.lblCompress.Location = new System.Drawing.Point(3, 190);
            this.lblCompress.Name = "lblCompress";
            this.lblCompress.Size = new System.Drawing.Size(56, 13);
            this.lblCompress.TabIndex = 18;
            this.lblCompress.Text = "Compress:";
            // 
            // lblAppExeName
            // 
            this.lblAppExeName.AutoSize = true;
            this.lblAppExeName.Location = new System.Drawing.Point(3, 110);
            this.lblAppExeName.Name = "lblAppExeName";
            this.lblAppExeName.Size = new System.Drawing.Size(84, 13);
            this.lblAppExeName.TabIndex = 12;
            this.lblAppExeName.Text = "App EXE Name:";
            // 
            // txtApplicationName
            // 
            this.txtApplicationName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtApplicationName.Location = new System.Drawing.Point(164, 81);
            this.txtApplicationName.Name = "txtApplicationName";
            this.txtApplicationName.Size = new System.Drawing.Size(500, 20);
            this.txtApplicationName.TabIndex = 9;
            this.txtApplicationName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // lblApplicationName
            // 
            this.lblApplicationName.AutoSize = true;
            this.lblApplicationName.Location = new System.Drawing.Point(3, 84);
            this.lblApplicationName.Name = "lblApplicationName";
            this.lblApplicationName.Size = new System.Drawing.Size(93, 13);
            this.lblApplicationName.TabIndex = 8;
            this.lblApplicationName.Text = "Application Name:";
            // 
            // numDirectoryLevel
            // 
            this.numDirectoryLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numDirectoryLevel.Location = new System.Drawing.Point(164, 29);
            this.numDirectoryLevel.Name = "numDirectoryLevel";
            this.numDirectoryLevel.Size = new System.Drawing.Size(500, 20);
            this.numDirectoryLevel.TabIndex = 3;
            this.numDirectoryLevel.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numDirectoryLevel.ValueChanged += new System.EventHandler(this.numDirectoryLevel_ValueChanged);
            // 
            // btnDeploy
            // 
            this.btnDeploy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeploy.Image = global::Zek.Updater.Server.Properties.Resources.internet_upload_16x16;
            this.btnDeploy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeploy.Location = new System.Drawing.Point(687, 185);
            this.btnDeploy.Name = "btnDeploy";
            this.btnDeploy.Size = new System.Drawing.Size(80, 23);
            this.btnDeploy.TabIndex = 22;
            this.btnDeploy.Text = "Deploy";
            this.btnDeploy.UseVisualStyleBackColor = true;
            this.btnDeploy.Click += new System.EventHandler(this.btnDeploy_Click);
            // 
            // lblDirectoryLevel
            // 
            this.lblDirectoryLevel.AutoSize = true;
            this.lblDirectoryLevel.Location = new System.Drawing.Point(3, 33);
            this.lblDirectoryLevel.Name = "lblDirectoryLevel";
            this.lblDirectoryLevel.Size = new System.Drawing.Size(78, 13);
            this.lblDirectoryLevel.TabIndex = 2;
            this.lblDirectoryLevel.Text = "Directory Level";
            // 
            // lblDirectoryDestination
            // 
            this.lblDirectoryDestination.AutoSize = true;
            this.lblDirectoryDestination.Location = new System.Drawing.Point(3, 59);
            this.lblDirectoryDestination.Name = "lblDirectoryDestination";
            this.lblDirectoryDestination.Size = new System.Drawing.Size(108, 13);
            this.lblDirectoryDestination.TabIndex = 4;
            this.lblDirectoryDestination.Text = "Directory Destination:";
            // 
            // lblDirectoryUpdate
            // 
            this.lblDirectoryUpdate.AutoSize = true;
            this.lblDirectoryUpdate.Location = new System.Drawing.Point(3, 7);
            this.lblDirectoryUpdate.Name = "lblDirectoryUpdate";
            this.lblDirectoryUpdate.Size = new System.Drawing.Size(90, 13);
            this.lblDirectoryUpdate.TabIndex = 0;
            this.lblDirectoryUpdate.Text = "Directory Update:";
            // 
            // txtDirectoryDestination
            // 
            this.txtDirectoryDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDirectoryDestination.Location = new System.Drawing.Point(164, 55);
            this.txtDirectoryDestination.Name = "txtDirectoryDestination";
            this.txtDirectoryDestination.Size = new System.Drawing.Size(500, 20);
            this.txtDirectoryDestination.TabIndex = 5;
            this.txtDirectoryDestination.TextChanged += new System.EventHandler(this.txtDirectoryDestination_TextChanged);
            // 
            // txtDirectoryUpdate
            // 
            this.txtDirectoryUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDirectoryUpdate.Location = new System.Drawing.Point(164, 3);
            this.txtDirectoryUpdate.Name = "txtDirectoryUpdate";
            this.txtDirectoryUpdate.Size = new System.Drawing.Size(500, 20);
            this.txtDirectoryUpdate.TabIndex = 1;
            this.txtDirectoryUpdate.TextChanged += new System.EventHandler(this.txtDirectoryUpdate_TextChanged);
            // 
            // btnDirectoryDestination
            // 
            this.btnDirectoryDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDirectoryDestination.Image = global::Zek.Updater.Server.Properties.Resources.folder_open_16x16;
            this.btnDirectoryDestination.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDirectoryDestination.Location = new System.Drawing.Point(685, 54);
            this.btnDirectoryDestination.Name = "btnDirectoryDestination";
            this.btnDirectoryDestination.Size = new System.Drawing.Size(80, 23);
            this.btnDirectoryDestination.TabIndex = 21;
            this.btnDirectoryDestination.Text = "Choose";
            this.btnDirectoryDestination.UseVisualStyleBackColor = true;
            this.btnDirectoryDestination.Click += new System.EventHandler(this.btnDirectoryDestination_Click);
            // 
            // btnDirectoryUpdate
            // 
            this.btnDirectoryUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDirectoryUpdate.Image = global::Zek.Updater.Server.Properties.Resources.folder_open_16x16;
            this.btnDirectoryUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDirectoryUpdate.Location = new System.Drawing.Point(685, 2);
            this.btnDirectoryUpdate.Name = "btnDirectoryUpdate";
            this.btnDirectoryUpdate.Size = new System.Drawing.Size(80, 23);
            this.btnDirectoryUpdate.TabIndex = 20;
            this.btnDirectoryUpdate.Text = "Choose";
            this.btnDirectoryUpdate.UseVisualStyleBackColor = true;
            this.btnDirectoryUpdate.Click += new System.EventHandler(this.btnOpenDir_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Choose Directory";
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressFiles,
            this.tslblProgress});
            this.statusStrip.Location = new System.Drawing.Point(0, 540);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(784, 22);
            this.statusStrip.TabIndex = 1;
            // 
            // progressFiles
            // 
            this.progressFiles.AutoSize = false;
            this.progressFiles.Name = "progressFiles";
            this.progressFiles.Size = new System.Drawing.Size(200, 16);
            this.progressFiles.Step = 1;
            // 
            // tslblProgress
            // 
            this.tslblProgress.Name = "tslblProgress";
            this.tslblProgress.Size = new System.Drawing.Size(16, 17);
            this.tslblProgress.Text = "...";
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // bsDeploy
            // 
            this.bsDeploy.WorkerReportsProgress = true;
            this.bsDeploy.WorkerSupportsCancellation = true;
            this.bsDeploy.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bsDeploy_DoWork);
            this.bsDeploy.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bsDeploy_ProgressChanged);
            this.bsDeploy.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bsDeploy_RunWorkerCompleted);
            // 
            // chkDeleteBin
            // 
            this.chkDeleteBin.AutoSize = true;
            this.chkDeleteBin.Location = new System.Drawing.Point(164, 213);
            this.chkDeleteBin.Name = "chkDeleteBin";
            this.chkDeleteBin.Size = new System.Drawing.Size(75, 17);
            this.chkDeleteBin.TabIndex = 23;
            this.chkDeleteBin.Text = "Delete Bin";
            this.chkDeleteBin.UseVisualStyleBackColor = false;
            this.chkDeleteBin.CheckedChanged += new System.EventHandler(this.chkDeleteBin_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Updater Server";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.tabControl.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFile)).EndInit();
            this.pnlTop1.ResumeLayout(false);
            this.pnlTop1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDirectoryLevel)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabMain;
        private System.Windows.Forms.Button btnDirectoryUpdate;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TextBox txtDirectoryUpdate;
        private System.Windows.Forms.Label lblDirectoryUpdate;
        private System.Windows.Forms.DataGridView dgvFile;
        private System.Windows.Forms.Button btnDeploy;
        private System.Windows.Forms.Panel pnlTop1;
        private System.Windows.Forms.NumericUpDown numDirectoryLevel;
        private System.Windows.Forms.Label lblDirectoryLevel;
        private System.Windows.Forms.TextBox txtApplicationName;
        private System.Windows.Forms.Label lblApplicationName;
        private System.Windows.Forms.Label lblAppExeName;
        private System.Windows.Forms.Label lblDirectoryDestination;
        private System.Windows.Forms.TextBox txtDirectoryDestination;
        private System.Windows.Forms.Button btnDirectoryDestination;
        private System.Windows.Forms.ComboBox cmbCompress;
        private System.Windows.Forms.Label lblCompress;
        private System.Windows.Forms.ComboBox cmbAppExeName;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripProgressBar progressFiles;
        private System.Windows.Forms.ToolStripStatusLabel tslblProgress;
        private System.Windows.Forms.TextBox txtCompressFolderName;
        private System.Windows.Forms.Label lblCompressFolderName;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label lblVersion;
        private System.ComponentModel.BackgroundWorker bsDeploy;
        private System.Windows.Forms.CheckBox chkDeleteBin;
    }
}

