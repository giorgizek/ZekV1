namespace Zek.Windows.Forms.DevEx
{
    partial class EditDialogForm
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
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Image = global::Zek.Properties.Images.ok_24x24;
            this.btnOk.Location = new System.Drawing.Point(44, 227);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(150, 33);
            this.btnOk.TabIndex = 98;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::Zek.Properties.Images.cancel_24x24;
            this.btnCancel.Location = new System.Drawing.Point(200, 227);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 33);
            this.btnCancel.TabIndex = 99;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EditDialogForm
            // 
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(394, 272);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditDialogForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraEditors.SimpleButton btnOk;
        protected DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}