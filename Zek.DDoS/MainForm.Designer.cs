namespace Zek.DDoS
{
    partial class MainForm
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
            this.lblIp = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblIp
            // 
            this.lblIp.AutoSize = true;
            this.lblIp.Location = new System.Drawing.Point(12, 15);
            this.lblIp.Name = "lblIp";
            this.lblIp.Size = new System.Drawing.Size(20, 13);
            this.lblIp.TabIndex = 0;
            this.lblIp.Text = "IP:";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(96, 12);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(534, 20);
            this.txtIp.TabIndex = 1;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(636, 12);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 20);
            this.txtPort.TabIndex = 2;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(661, 60);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(96, 84);
            this.txtLog.MaxLength = 0;
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(640, 318);
            this.txtLog.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIp);
            this.Controls.Add(this.lblIp);
            this.Name = "MainForm";
            this.Text = "DDoS";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIp;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtLog;
    }
}

