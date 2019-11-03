namespace Zek.Windows.Forms.DevEx
{
    partial class ListForm
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
            this.biAdd = new DevExpress.XtraBars.BarButtonItem();
            this.biEdit = new DevExpress.XtraBars.BarButtonItem();
            this.biDelete = new DevExpress.XtraBars.BarButtonItem();
            this.biChoose = new DevExpress.XtraBars.BarButtonItem();
            this.biApprove = new DevExpress.XtraBars.BarButtonItem();
            this.biSum = new DevExpress.XtraBars.BarButtonItem();
            this.biPrint = new DevExpress.XtraBars.BarButtonItem();
            this.biExport = new DevExpress.XtraBars.BarButtonItem();
            this.biRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.biTopRecords = new DevExpress.XtraBars.BarCheckItem();
            this.biFilterPanel = new DevExpress.XtraBars.BarCheckItem();
            this.biFilter = new DevExpress.XtraBars.BarCheckItem();
            this.biFilterApprove = new DevExpress.XtraBars.BarCheckItem();
            this.biBestFit = new DevExpress.XtraBars.BarButtonItem();
            this.biSystemColumns = new DevExpress.XtraBars.BarCheckItem();
            this.biAutoRefresh = new DevExpress.XtraBars.BarCheckItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.miBlock = new DevExpress.XtraBars.BarButtonItem();
            this.miUnblock = new DevExpress.XtraBars.BarButtonItem();
            this.miDisapprove = new DevExpress.XtraBars.BarButtonItem();
            this.pnlFilter = new DevExpress.XtraEditors.PanelControl();
            this.btnFilter = new DevExpress.XtraEditors.SimpleButton();
            this.pnlGrid = new DevExpress.XtraEditors.PanelControl();
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFilter)).BeginInit();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
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
            this.biAdd,
            this.biEdit,
            this.biDelete,
            this.biChoose,
            this.biApprove,
            this.biSum,
            this.biPrint,
            this.biExport,
            this.biRefresh,
            this.biTopRecords,
            this.biFilterPanel,
            this.biFilter,
            this.biFilterApprove,
            this.biSystemColumns,
            this.miBlock,
            this.miUnblock,
            this.miDisapprove,
            this.biAutoRefresh,
            this.biBestFit});
            this.barManager.MaxItemId = 20;
            this.barManager.ShowFullMenus = true;
            // 
            // barTools
            // 
            this.barTools.BarName = "Tools";
            this.barTools.DockCol = 0;
            this.barTools.DockRow = 0;
            this.barTools.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barTools.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.biAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.biEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.biDelete),
            new DevExpress.XtraBars.LinkPersistInfo(this.biChoose, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.biApprove, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.biSum, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.biPrint),
            new DevExpress.XtraBars.LinkPersistInfo(this.biExport),
            new DevExpress.XtraBars.LinkPersistInfo(this.biRefresh, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.biTopRecords, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.biFilterPanel),
            new DevExpress.XtraBars.LinkPersistInfo(this.biFilter),
            new DevExpress.XtraBars.LinkPersistInfo(this.biFilterApprove),
            new DevExpress.XtraBars.LinkPersistInfo(this.biBestFit, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.biSystemColumns),
            new DevExpress.XtraBars.LinkPersistInfo(this.biAutoRefresh)});
            this.barTools.OptionsBar.DisableClose = true;
            this.barTools.OptionsBar.DrawDragBorder = false;
            this.barTools.OptionsBar.UseWholeRow = true;
            this.barTools.Text = "Tools";
            // 
            // biAdd
            // 
            this.biAdd.Caption = "Add";
            this.biAdd.Glyph = global::Zek.Properties.Images.add_24x24;
            this.biAdd.Id = 0;
            this.biAdd.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Insert);
            this.biAdd.Name = "biAdd";
            this.biAdd.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biAdd_ItemClick);
            // 
            // biEdit
            // 
            this.biEdit.Caption = "Edit";
            this.biEdit.Glyph = global::Zek.Properties.Images.tool_pencil_24x24;
            this.biEdit.Id = 1;
            this.biEdit.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2);
            this.biEdit.Name = "biEdit";
            this.biEdit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biEdit_ItemClick);
            // 
            // biDelete
            // 
            this.biDelete.Caption = "Delete";
            this.biDelete.Glyph = global::Zek.Properties.Images.delete_24x24;
            this.biDelete.Id = 2;
            this.biDelete.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8);
            this.biDelete.Name = "biDelete";
            this.biDelete.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biDelete_ItemClick);
            // 
            // biChoose
            // 
            this.biChoose.Caption = "Choose";
            this.biChoose.Enabled = false;
            this.biChoose.Glyph = global::Zek.Properties.Images.select_24x24;
            this.biChoose.Id = 3;
            this.biChoose.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Enter));
            this.biChoose.Name = "biChoose";
            this.biChoose.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biChoose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biChoose_ItemClick);
            // 
            // biApprove
            // 
            this.biApprove.Caption = "Approve";
            this.biApprove.Enabled = false;
            this.biApprove.Glyph = global::Zek.Properties.Images.contract_24x24;
            this.biApprove.Id = 4;
            this.biApprove.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F6));
            this.biApprove.Name = "biApprove";
            this.biApprove.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biApprove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biApprove_ItemClick);
            // 
            // biSum
            // 
            this.biSum.Caption = "Sum";
            this.biSum.Glyph = global::Zek.Properties.Images.sum_24x24;
            this.biSum.Id = 5;
            this.biSum.Name = "biSum";
            this.biSum.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biSum_ItemClick);
            // 
            // biPrint
            // 
            this.biPrint.Caption = "Print";
            this.biPrint.Glyph = global::Zek.Properties.Images.print_24x24;
            this.biPrint.Id = 6;
            this.biPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P));
            this.biPrint.Name = "biPrint";
            this.biPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.biPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biPrint_ItemClick);
            // 
            // biExport
            // 
            this.biExport.Caption = "Export";
            this.biExport.Glyph = global::Zek.Properties.Images.table_export_24x24;
            this.biExport.Id = 7;
            this.biExport.Name = "biExport";
            this.biExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biExport_ItemClick);
            // 
            // biRefresh
            // 
            this.biRefresh.Caption = "Refresh";
            this.biRefresh.Glyph = global::Zek.Properties.Images.refresh_update_24x24;
            this.biRefresh.Id = 8;
            this.biRefresh.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5);
            this.biRefresh.Name = "biRefresh";
            this.biRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biRefresh_ItemClick);
            // 
            // biTopRecords
            // 
            this.biTopRecords.Caption = "Top records";
            this.biTopRecords.Checked = true;
            this.biTopRecords.Glyph = global::Zek.Properties.Images.table_24x24;
            this.biTopRecords.Id = 9;
            this.biTopRecords.Name = "biTopRecords";
            this.biTopRecords.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.biTopRecords_CheckedChanged);
            // 
            // biFilterPanel
            // 
            this.biFilterPanel.Caption = "Filter Panel";
            this.biFilterPanel.Glyph = global::Zek.Properties.Images.filter_24x24;
            this.biFilterPanel.Id = 10;
            this.biFilterPanel.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F7));
            this.biFilterPanel.Name = "biFilterPanel";
            this.biFilterPanel.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.biFilterPanel_CheckedChanged);
            // 
            // biFilter
            // 
            this.biFilter.Caption = "Enable/Disable Filter";
            this.biFilter.Glyph = global::Zek.Properties.Images.filter_ok_24x24;
            this.biFilter.Id = 11;
            this.biFilter.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3);
            this.biFilter.Name = "biFilter";
            this.biFilter.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.biFilter_CheckedChanged);
            // 
            // biFilterApprove
            // 
            this.biFilterApprove.Caption = "Approved/Not Approved";
            this.biFilterApprove.Checked = true;
            this.biFilterApprove.Glyph = global::Zek.Properties.Images.contract_ok_24x24;
            this.biFilterApprove.Id = 12;
            this.biFilterApprove.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6);
            this.biFilterApprove.Name = "biFilterApprove";
            this.biFilterApprove.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.biFilterApprove_CheckedChanged);
            // 
            // biBestFit
            // 
            this.biBestFit.Caption = "Best Fit";
            this.biBestFit.Glyph = global::Zek.Properties.Images.full_screen_24x24;
            this.biBestFit.Id = 13;
            this.biBestFit.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9);
            this.biBestFit.Name = "biBestFit";
            this.biBestFit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biBestFit_ItemClick);
            // 
            // biSystemColumns
            // 
            this.biSystemColumns.Caption = "System Columns";
            this.biSystemColumns.Glyph = global::Zek.Properties.Images.table_field_ok_24x24;
            this.biSystemColumns.Id = 14;
            this.biSystemColumns.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F9));
            this.biSystemColumns.Name = "biSystemColumns";
            this.biSystemColumns.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.biSystemColumns_CheckedChanged);
            // 
            // biAutoRefresh
            // 
            this.biAutoRefresh.Caption = "Auto Refresh";
            this.biAutoRefresh.Checked = true;
            this.biAutoRefresh.Glyph = global::Zek.Properties.Images.reload_rotate_24x24;
            this.biAutoRefresh.Id = 15;
            this.biAutoRefresh.Name = "biAutoRefresh";
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
            // miBlock
            // 
            this.miBlock.Caption = "Block";
            this.miBlock.Id = 16;
            this.miBlock.Name = "miBlock";
            this.miBlock.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biBlock_ItemClick);
            // 
            // miUnblock
            // 
            this.miUnblock.Caption = "Unblock";
            this.miUnblock.Id = 17;
            this.miUnblock.Name = "miUnblock";
            this.miUnblock.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biUnblock_ItemClick);
            // 
            // miDisapprove
            // 
            this.miDisapprove.Caption = "Disapprove";
            this.miDisapprove.Glyph = global::Zek.Properties.Images.contract_delete_16x16;
            this.miDisapprove.Id = 18;
            this.miDisapprove.Name = "miDisapprove";
            this.miDisapprove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biDisapprove_ItemClick);
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.btnFilter);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 39);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(784, 100);
            this.pnlFilter.TabIndex = 0;
            this.pnlFilter.Visible = false;
            // 
            // btnFilter
            // 
            this.btnFilter.Image = global::Zek.Properties.Images.filter_new_16x16;
            this.btnFilter.Location = new System.Drawing.Point(697, 6);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 23);
            this.btnFilter.TabIndex = 99;
            this.btnFilter.Text = "Filter";
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // pnlGrid
            // 
            this.pnlGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 139);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(784, 423);
            this.pnlGrid.TabIndex = 1;
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.miBlock),
            new DevExpress.XtraBars.LinkPersistInfo(this.miUnblock),
            new DevExpress.XtraBars.LinkPersistInfo(this.miDisapprove, true)});
            this.popupMenu.Manager = this.barManager;
            this.popupMenu.Name = "popupMenu";
            // 
            // ListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "ListForm";
            this.Load += new System.EventHandler(this.ListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFilter)).EndInit();
            this.pnlFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraBars.BarDockControl barDockControlTop;
        protected DevExpress.XtraBars.BarDockControl barDockControlBottom;
        protected DevExpress.XtraBars.BarDockControl barDockControlLeft;
        protected DevExpress.XtraBars.BarDockControl barDockControlRight;
        protected DevExpress.XtraBars.BarButtonItem biAdd;
        protected DevExpress.XtraBars.BarButtonItem biEdit;
        protected DevExpress.XtraBars.BarButtonItem biDelete;
        protected DevExpress.XtraBars.BarButtonItem biChoose;
        protected DevExpress.XtraBars.BarButtonItem biApprove;
        protected DevExpress.XtraBars.BarButtonItem biSum;
        protected DevExpress.XtraBars.BarButtonItem biPrint;
        protected DevExpress.XtraBars.BarButtonItem biExport;
        protected DevExpress.XtraBars.BarButtonItem biRefresh;
        protected DevExpress.XtraBars.BarCheckItem biTopRecords;
        protected DevExpress.XtraBars.BarCheckItem biFilterPanel;
        protected DevExpress.XtraBars.BarCheckItem biFilter;
        protected DevExpress.XtraBars.BarCheckItem biFilterApprove;
        protected DevExpress.XtraBars.BarCheckItem biSystemColumns;
        protected DevExpress.XtraBars.BarButtonItem miDisapprove;
        protected DevExpress.XtraBars.BarButtonItem miBlock;
        protected DevExpress.XtraBars.BarButtonItem miUnblock;
        protected DevExpress.XtraBars.BarManager barManager;
        protected DevExpress.XtraBars.Bar barTools;
        protected DevExpress.XtraEditors.PanelControl pnlFilter;
        protected DevExpress.XtraEditors.PanelControl pnlGrid;
        protected DevExpress.XtraEditors.SimpleButton btnFilter;
        protected DevExpress.XtraBars.PopupMenu popupMenu;
        protected DevExpress.XtraBars.BarCheckItem biAutoRefresh;
        protected DevExpress.XtraBars.BarButtonItem biBestFit;
    }
}