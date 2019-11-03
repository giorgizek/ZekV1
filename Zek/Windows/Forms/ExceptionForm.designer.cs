namespace Zek.Windows.Forms
{
    partial class ExceptionForm
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
            this.tabControlGeneral = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.lblInformation = new System.Windows.Forms.Label();
            this.lblFramework = new System.Windows.Forms.Label();
            this.txtInformation = new System.Windows.Forms.TextBox();
            this.txtFramework = new System.Windows.Forms.TextBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.lblApplication = new System.Windows.Forms.Label();
            this.txtApplication = new System.Windows.Forms.TextBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.picGeneral = new System.Windows.Forms.PictureBox();
            this.tabException = new System.Windows.Forms.TabPage();
            this.lblStackTrace = new System.Windows.Forms.Label();
            this.lblSource = new System.Windows.Forms.Label();
            this.lblMessage2 = new System.Windows.Forms.Label();
            this.txtStackTrace = new System.Windows.Forms.TextBox();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtMessage2 = new System.Windows.Forms.TextBox();
            this.tabInnerExceptions = new System.Windows.Forms.TabPage();
            this.dgvInnerExceptions = new System.Windows.Forms.DataGridView();
            this.tabAssemblies = new System.Windows.Forms.TabPage();
            this.listViewAssemblies = new System.Windows.Forms.ListView();
            this.btnSaveToFile = new System.Windows.Forms.Button();
            this.btnClipboard = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControlGeneral.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGeneral)).BeginInit();
            this.tabException.SuspendLayout();
            this.tabInnerExceptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInnerExceptions)).BeginInit();
            this.tabAssemblies.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlGeneral
            // 
            this.tabControlGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlGeneral.Controls.Add(this.tabGeneral);
            this.tabControlGeneral.Controls.Add(this.tabException);
            this.tabControlGeneral.Controls.Add(this.tabInnerExceptions);
            this.tabControlGeneral.Controls.Add(this.tabAssemblies);
            this.tabControlGeneral.Location = new System.Drawing.Point(4, 4);
            this.tabControlGeneral.Name = "tabControlGeneral";
            this.tabControlGeneral.SelectedIndex = 0;
            this.tabControlGeneral.Size = new System.Drawing.Size(577, 361);
            this.tabControlGeneral.TabIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.lblInformation);
            this.tabGeneral.Controls.Add(this.lblFramework);
            this.tabGeneral.Controls.Add(this.txtInformation);
            this.tabGeneral.Controls.Add(this.txtFramework);
            this.tabGeneral.Controls.Add(this.lblVersion);
            this.tabGeneral.Controls.Add(this.txtVersion);
            this.tabGeneral.Controls.Add(this.lblApplication);
            this.tabGeneral.Controls.Add(this.txtApplication);
            this.tabGeneral.Controls.Add(this.txtMessage);
            this.tabGeneral.Controls.Add(this.picGeneral);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(569, 335);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // lblInformation
            // 
            this.lblInformation.Location = new System.Drawing.Point(6, 164);
            this.lblInformation.Name = "lblInformation";
            this.lblInformation.Size = new System.Drawing.Size(565, 20);
            this.lblInformation.TabIndex = 7;
            this.lblInformation.Text = "Please enter detailed information about events which cause this exception. ";
            this.lblInformation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFramework
            // 
            this.lblFramework.Location = new System.Drawing.Point(6, 132);
            this.lblFramework.Name = "lblFramework";
            this.lblFramework.Size = new System.Drawing.Size(70, 20);
            this.lblFramework.TabIndex = 5;
            this.lblFramework.Text = "Framework:";
            this.lblFramework.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtInformation
            // 
            this.txtInformation.AcceptsReturn = true;
            this.txtInformation.AcceptsTab = true;
            this.txtInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInformation.BackColor = System.Drawing.SystemColors.Window;
            this.txtInformation.Location = new System.Drawing.Point(6, 187);
            this.txtInformation.MaxLength = 0;
            this.txtInformation.Multiline = true;
            this.txtInformation.Name = "txtInformation";
            this.txtInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInformation.Size = new System.Drawing.Size(557, 142);
            this.txtInformation.TabIndex = 8;
            this.txtInformation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextboxKeyDown);
            // 
            // txtFramework
            // 
            this.txtFramework.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFramework.BackColor = System.Drawing.SystemColors.Window;
            this.txtFramework.Location = new System.Drawing.Point(76, 132);
            this.txtFramework.Name = "txtFramework";
            this.txtFramework.ReadOnly = true;
            this.txtFramework.Size = new System.Drawing.Size(487, 20);
            this.txtFramework.TabIndex = 6;
            this.txtFramework.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextboxKeyDown);
            // 
            // lblVersion
            // 
            this.lblVersion.Location = new System.Drawing.Point(6, 103);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(64, 20);
            this.lblVersion.TabIndex = 3;
            this.lblVersion.Text = "Version:";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtVersion
            // 
            this.txtVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVersion.BackColor = System.Drawing.SystemColors.Window;
            this.txtVersion.Location = new System.Drawing.Point(76, 103);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            this.txtVersion.Size = new System.Drawing.Size(487, 20);
            this.txtVersion.TabIndex = 4;
            this.txtVersion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextboxKeyDown);
            // 
            // lblApplication
            // 
            this.lblApplication.Location = new System.Drawing.Point(6, 77);
            this.lblApplication.Name = "lblApplication";
            this.lblApplication.Size = new System.Drawing.Size(64, 20);
            this.lblApplication.TabIndex = 1;
            this.lblApplication.Text = "Application:";
            this.lblApplication.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtApplication
            // 
            this.txtApplication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtApplication.BackColor = System.Drawing.SystemColors.Window;
            this.txtApplication.Location = new System.Drawing.Point(76, 77);
            this.txtApplication.Name = "txtApplication";
            this.txtApplication.ReadOnly = true;
            this.txtApplication.Size = new System.Drawing.Size(487, 20);
            this.txtApplication.TabIndex = 2;
            this.txtApplication.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextboxKeyDown);
            // 
            // txtMessage
            // 
            this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessage.BackColor = System.Drawing.SystemColors.Window;
            this.txtMessage.Location = new System.Drawing.Point(76, 6);
            this.txtMessage.MaxLength = 0;
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(487, 64);
            this.txtMessage.TabIndex = 0;
            this.txtMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextboxKeyDown);
            // 
            // picGeneral
            // 
            this.picGeneral.Image = global::Zek.Properties.Images.warning_64x64;
            this.picGeneral.Location = new System.Drawing.Point(6, 6);
            this.picGeneral.Name = "picGeneral";
            this.picGeneral.Size = new System.Drawing.Size(64, 64);
            this.picGeneral.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picGeneral.TabIndex = 27;
            this.picGeneral.TabStop = false;
            // 
            // tabException
            // 
            this.tabException.Controls.Add(this.lblStackTrace);
            this.tabException.Controls.Add(this.lblSource);
            this.tabException.Controls.Add(this.lblMessage2);
            this.tabException.Controls.Add(this.txtStackTrace);
            this.tabException.Controls.Add(this.txtSource);
            this.tabException.Controls.Add(this.txtMessage2);
            this.tabException.Location = new System.Drawing.Point(4, 22);
            this.tabException.Name = "tabException";
            this.tabException.Padding = new System.Windows.Forms.Padding(3);
            this.tabException.Size = new System.Drawing.Size(569, 335);
            this.tabException.TabIndex = 1;
            this.tabException.Text = "Exception";
            this.tabException.UseVisualStyleBackColor = true;
            // 
            // lblStackTrace
            // 
            this.lblStackTrace.Location = new System.Drawing.Point(6, 145);
            this.lblStackTrace.Name = "lblStackTrace";
            this.lblStackTrace.Size = new System.Drawing.Size(559, 20);
            this.lblStackTrace.TabIndex = 4;
            this.lblStackTrace.Text = "Stack Trace:";
            this.lblStackTrace.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSource
            // 
            this.lblSource.Location = new System.Drawing.Point(6, 96);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(559, 20);
            this.lblSource.TabIndex = 2;
            this.lblSource.Text = "Source:";
            this.lblSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMessage2
            // 
            this.lblMessage2.Location = new System.Drawing.Point(6, 6);
            this.lblMessage2.Name = "lblMessage2";
            this.lblMessage2.Size = new System.Drawing.Size(559, 20);
            this.lblMessage2.TabIndex = 0;
            this.lblMessage2.Text = "Message:";
            this.lblMessage2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtStackTrace
            // 
            this.txtStackTrace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStackTrace.BackColor = System.Drawing.SystemColors.Window;
            this.txtStackTrace.Location = new System.Drawing.Point(6, 168);
            this.txtStackTrace.MaxLength = 0;
            this.txtStackTrace.Multiline = true;
            this.txtStackTrace.Name = "txtStackTrace";
            this.txtStackTrace.ReadOnly = true;
            this.txtStackTrace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStackTrace.Size = new System.Drawing.Size(557, 164);
            this.txtStackTrace.TabIndex = 5;
            this.txtStackTrace.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextboxKeyDown);
            // 
            // txtSource
            // 
            this.txtSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSource.BackColor = System.Drawing.SystemColors.Window;
            this.txtSource.Location = new System.Drawing.Point(6, 119);
            this.txtSource.MaxLength = 0;
            this.txtSource.Multiline = true;
            this.txtSource.Name = "txtSource";
            this.txtSource.ReadOnly = true;
            this.txtSource.Size = new System.Drawing.Size(557, 23);
            this.txtSource.TabIndex = 3;
            this.txtSource.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextboxKeyDown);
            // 
            // txtMessage2
            // 
            this.txtMessage2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessage2.BackColor = System.Drawing.SystemColors.Window;
            this.txtMessage2.Location = new System.Drawing.Point(6, 29);
            this.txtMessage2.MaxLength = 0;
            this.txtMessage2.Multiline = true;
            this.txtMessage2.Name = "txtMessage2";
            this.txtMessage2.ReadOnly = true;
            this.txtMessage2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage2.Size = new System.Drawing.Size(557, 64);
            this.txtMessage2.TabIndex = 1;
            this.txtMessage2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTextboxKeyDown);
            // 
            // tabInnerExceptions
            // 
            this.tabInnerExceptions.Controls.Add(this.dgvInnerExceptions);
            this.tabInnerExceptions.Location = new System.Drawing.Point(4, 22);
            this.tabInnerExceptions.Name = "tabInnerExceptions";
            this.tabInnerExceptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabInnerExceptions.Size = new System.Drawing.Size(569, 335);
            this.tabInnerExceptions.TabIndex = 3;
            this.tabInnerExceptions.Text = "Inner Exceptions";
            this.tabInnerExceptions.UseVisualStyleBackColor = true;
            // 
            // dgvInnerExceptions
            // 
            this.dgvInnerExceptions.AllowUserToAddRows = false;
            this.dgvInnerExceptions.AllowUserToDeleteRows = false;
            this.dgvInnerExceptions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvInnerExceptions.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvInnerExceptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInnerExceptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInnerExceptions.Location = new System.Drawing.Point(3, 3);
            this.dgvInnerExceptions.Name = "dgvInnerExceptions";
            this.dgvInnerExceptions.ReadOnly = true;
            this.dgvInnerExceptions.Size = new System.Drawing.Size(563, 329);
            this.dgvInnerExceptions.TabIndex = 0;
            // 
            // tabAssemblies
            // 
            this.tabAssemblies.Controls.Add(this.listViewAssemblies);
            this.tabAssemblies.Location = new System.Drawing.Point(4, 22);
            this.tabAssemblies.Name = "tabAssemblies";
            this.tabAssemblies.Padding = new System.Windows.Forms.Padding(3);
            this.tabAssemblies.Size = new System.Drawing.Size(569, 335);
            this.tabAssemblies.TabIndex = 2;
            this.tabAssemblies.Text = "Assemblies";
            this.tabAssemblies.UseVisualStyleBackColor = true;
            // 
            // listViewAssemblies
            // 
            this.listViewAssemblies.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewAssemblies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewAssemblies.FullRowSelect = true;
            this.listViewAssemblies.HotTracking = true;
            this.listViewAssemblies.HoverSelection = true;
            this.listViewAssemblies.Location = new System.Drawing.Point(3, 3);
            this.listViewAssemblies.Name = "listViewAssemblies";
            this.listViewAssemblies.Size = new System.Drawing.Size(563, 329);
            this.listViewAssemblies.TabIndex = 0;
            this.listViewAssemblies.UseCompatibleStateImageBehavior = false;
            this.listViewAssemblies.View = System.Windows.Forms.View.Details;
            // 
            // btnSaveToFile
            // 
            this.btnSaveToFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveToFile.Location = new System.Drawing.Point(261, 371);
            this.btnSaveToFile.Name = "btnSaveToFile";
            this.btnSaveToFile.Size = new System.Drawing.Size(108, 23);
            this.btnSaveToFile.TabIndex = 2;
            this.btnSaveToFile.Text = "Save to File";
            this.btnSaveToFile.Click += new System.EventHandler(this.btnSaveToFile_Click);
            // 
            // btnClipboard
            // 
            this.btnClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClipboard.Location = new System.Drawing.Point(375, 371);
            this.btnClipboard.Name = "btnClipboard";
            this.btnClipboard.Size = new System.Drawing.Size(108, 23);
            this.btnClipboard.TabIndex = 3;
            this.btnClipboard.Text = "Copy to Clipboard";
            this.btnClipboard.Click += new System.EventHandler(this.btnClipboard_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(489, 371);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Close";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ExceptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(584, 402);
            this.Controls.Add(this.btnSaveToFile);
            this.Controls.Add(this.btnClipboard);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tabControlGeneral);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(480, 352);
            this.Name = "ExceptionForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.tabControlGeneral.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGeneral)).EndInit();
            this.tabException.ResumeLayout(false);
            this.tabException.PerformLayout();
            this.tabInnerExceptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInnerExceptions)).EndInit();
            this.tabAssemblies.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.TabControl tabControlGeneral;
        protected System.Windows.Forms.TabPage tabGeneral;
        protected System.Windows.Forms.TabPage tabException;
        protected System.Windows.Forms.TabPage tabAssemblies;
        protected System.Windows.Forms.PictureBox picGeneral;
        protected System.Windows.Forms.TextBox txtMessage;
        protected System.Windows.Forms.Label lblInformation;
        protected System.Windows.Forms.Label lblFramework;
        protected System.Windows.Forms.TextBox txtInformation;
        protected System.Windows.Forms.TextBox txtFramework;
        protected System.Windows.Forms.Label lblVersion;
        protected System.Windows.Forms.TextBox txtVersion;
        protected System.Windows.Forms.Label lblApplication;
        protected System.Windows.Forms.TextBox txtApplication;
        protected System.Windows.Forms.Button btnSaveToFile;
        protected System.Windows.Forms.Button btnClipboard;
        protected System.Windows.Forms.Button btnCancel;
        protected System.Windows.Forms.Label lblStackTrace;
        protected System.Windows.Forms.Label lblSource;
        protected System.Windows.Forms.Label lblMessage2;
        protected System.Windows.Forms.TextBox txtStackTrace;
        protected System.Windows.Forms.TextBox txtSource;
        protected System.Windows.Forms.TextBox txtMessage2;
        protected System.Windows.Forms.ListView listViewAssemblies;
        private System.Windows.Forms.TabPage tabInnerExceptions;
        private System.Windows.Forms.DataGridView dgvInnerExceptions;
    }
}