namespace Zek.Windows.Forms.DevEx
{
    partial class EditForm
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
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barTools = new DevExpress.XtraBars.Bar();
            this.biSave = new DevExpress.XtraBars.BarButtonItem();
            this.biSaveAndClose = new DevExpress.XtraBars.BarButtonItem();
            this.biCancel = new DevExpress.XtraBars.BarButtonItem();
            this.biPrint = new DevExpress.XtraBars.BarButtonItem();
            this.biValidate = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.AllowCustomization = false;
            this.barManager.AllowMoveBarOnToolbar = false;
            this.barManager.AllowQuickCustomization = false;
            this.barManager.AllowShowToolbarsPopup = false;
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barTools});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.biSave,
            this.biSaveAndClose,
            this.biCancel,
            this.biPrint,
            this.biValidate});
            this.barManager.MaxItemId = 5;
            this.barManager.ShowFullMenus = true;
            // 
            // barTools
            // 
            this.barTools.BarName = "Tools";
            this.barTools.DockCol = 0;
            this.barTools.DockRow = 0;
            this.barTools.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barTools.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.biSave),
            new DevExpress.XtraBars.LinkPersistInfo(this.biSaveAndClose),
            new DevExpress.XtraBars.LinkPersistInfo(this.biCancel, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.biPrint, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.biValidate, true)});
            this.barTools.OptionsBar.DisableClose = true;
            this.barTools.OptionsBar.DrawDragBorder = false;
            this.barTools.OptionsBar.UseWholeRow = true;
            this.barTools.Text = "Tools";
            // 
            // biSave
            // 
            this.biSave.Caption = "Save";
            this.biSave.Glyph = global::Zek.Properties.Images.save_24x24;
            this.biSave.Hint = "Save";
            this.biSave.Id = 0;
            this.biSave.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S));
            this.biSave.Name = "biSave";
            this.biSave.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biSave_ItemClick);
            // 
            // biSaveAndClose
            // 
            this.biSaveAndClose.Caption = "Save && Close";
            this.biSaveAndClose.Glyph = global::Zek.Properties.Images.save_import_24x24;
            this.biSaveAndClose.Hint = "Save && Close";
            this.biSaveAndClose.Id = 1;
            this.biSaveAndClose.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Enter));
            this.biSaveAndClose.Name = "biSaveAndClose";
            this.biSaveAndClose.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biSaveAndClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biSaveAndClose_ItemClick);
            // 
            // biCancel
            // 
            this.biCancel.Caption = "Cancel";
            this.biCancel.Glyph = global::Zek.Properties.Images.cancel_24x24;
            this.biCancel.Hint = "Cancel";
            this.biCancel.Id = 2;
            this.biCancel.Name = "biCancel";
            this.biCancel.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biCancel_ItemClick);
            // 
            // biPrint
            // 
            this.biPrint.Caption = "Print";
            this.biPrint.Glyph = global::Zek.Properties.Images.print_24x24;
            this.biPrint.Hint = "Print";
            this.biPrint.Id = 3;
            this.biPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P));
            this.biPrint.Name = "biPrint";
            this.biPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.biPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biPrint_ItemClick);
            // 
            // biValidate
            // 
            this.biValidate.Caption = "Validate";
            this.biValidate.Glyph = global::Zek.Properties.Images.ok_24x24;
            this.biValidate.Hint = "Validate";
            this.biValidate.Id = 4;
            this.biValidate.Name = "biValidate";
            this.biValidate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.biValidate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biValidate_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(784, 39);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 562);
            this.barDockControlBottom.Size = new System.Drawing.Size(784, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 39);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 523);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(784, 39);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 523);
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "EditForm";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraBars.BarDockControl barDockControlLeft;
        protected DevExpress.XtraBars.BarDockControl barDockControlRight;
        protected DevExpress.XtraBars.BarDockControl barDockControlBottom;
        protected DevExpress.XtraBars.BarDockControl barDockControlTop;
        protected DevExpress.XtraBars.Bar barTools;
        protected DevExpress.XtraBars.BarButtonItem biSave;
        protected DevExpress.XtraBars.BarButtonItem biSaveAndClose;
        protected DevExpress.XtraBars.BarButtonItem biCancel;
        protected DevExpress.XtraBars.BarButtonItem biPrint;
        protected DevExpress.XtraBars.BarButtonItem biValidate;
        protected DevExpress.XtraBars.BarManager barManager;
    }
}