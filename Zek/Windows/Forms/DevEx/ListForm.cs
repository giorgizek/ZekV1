using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DevExpress.Data.Filtering;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Grid;
using Zek.Data;
using Zek.Extensions.DevEx;
using Zek.Properties;

namespace Zek.Windows.Forms.DevEx
{
    public partial class ListForm : BaseForm//, IBrowseForm
    {
        public ListForm()
        {
            //OptionsListForm.IsBlockable = false;
            //OptionsListForm.IsUnblockable = false;
            //OptionsListForm.IsDisapprovable = false;

            InitializeComponent();


            biAdd.Caption = Resources.Add;
            biAdd.Hint = Resources.AddHint;
            biEdit.Caption = Resources.Edit;
            biEdit.Hint = Resources.EditHint;
            biDelete.Caption = Resources.Delete;
            biDelete.Hint = Resources.DeleteHint;
            biChoose.Caption = Resources.Choose;
            biChoose.Hint = Resources.ChooseHint;
            biApprove.Caption = Resources.Approve;
            biApprove.Hint = Resources.ApproveHint;
            biSum.Caption = Resources.Sum;
            biSum.Hint = Resources.SumHint;
            biPrint.Caption = Resources.Print;
            biPrint.Hint = Resources.PrintHint;
            biExport.Caption = Resources.Export;
            biExport.Hint = Resources.ExportHint;
            biRefresh.Caption = Resources.Refresh;
            biRefresh.Hint = Resources.RefreshHint;
            biTopRecords.Caption = string.Format(Resources.TopRecordsCaptionFormat, 100);
            biTopRecords.Hint = string.Format(Resources.TopRecordsHintFormat, 100);
            biFilterPanel.Caption = Resources.FilterPanel;
            biFilterPanel.Hint = Resources.FilterPanelHint;
            biFilter.Caption = Resources.FilterEnableDisable;
            biFilter.Hint = Resources.FilterEnableDisableHint;
            biFilterApprove.Caption = Resources.ApprovedNotApproved;
            biFilterApprove.Hint = Resources.ApprovedNotApprovedHint;
            biBestFit.Caption = Resources.BestFit;
            biBestFit.Hint = Resources.BestFitHint;
            biSystemColumns.Caption = Resources.SystemColumns;
            biSystemColumns.Hint = Resources.SystemColumnsHint;
            biAutoRefresh.Caption = Resources.AutoRefresh;
            biAutoRefresh.Hint = Resources.AutoRefreshHint;

            miBlock.Caption = Resources.Block;
            miUnblock.Caption = Resources.Unblock;
            miDisapprove.Caption = Resources.Disapprove;

            _optionsListForm.PropertyChanged += OnOptionsPropertyChanged;
            _optionsGrid.PropertyChanged += OnOptionsPropertyChanged;


        }

        #region Options
        private readonly ListFormOptions _optionsListForm = new ListFormOptions();
        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Zek"), Description("List form configuration.")]
        public ListFormOptions OptionsListForm => _optionsListForm;

        private readonly XtraGridViewOptions _optionsGrid = new XtraGridViewOptions();
        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Zek"), Description("Grid configuration.")]
        public XtraGridViewOptions OptionsGrid => _optionsGrid;

        //BaseGridViewOptions IBrowseForm.OptionsGrid
        //{
        //    get { return _optionsGrid; }
        //}

        protected override void OnOptionsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "ListFormStyle": OnListFormStyleChanged(); break;
                case "IsAddable": OnIsAddableChanged(); break;
                case "IsApprovable": OnIsApprovableChanged(); break;
                case "AutoRefresh": OnAutoRefreshChanged(); break;
                case "IsBesftFitable": OnIsBesftFitableChanged(); break;
                case "IsBlockable": OnIsBlockableChanged(); break;
                case "IsChooseable": OnIsChooseableChanged(); break;
                case "IsDeletable": OnIsDeletableChanged(); break;
                case "IsEditable": OnIsEditableChanged(); break;
                case "IsExportable": OnIsExportableChanged(); break;
                case "IsFilterable": OnIsFilterableChanged(); break;
                case "IsFilterApprovable": OnIsFilterApprovableChanged(); break;
                case "IsRefreshable": OnIsRefreshableChanged(); break;
                case "IsSumable": OnIsSumableChanged(); break;
                case "IsSystemColumnsable": OnIsSystemColumnsableChanged(); break;
                case "IsTopable": OnIsTopableChanged(); break;
                case "IsUnblockable": OnIsUnblockableChanged(); break;
                case "TopRecordsCount": OnTopRecordsCountChanged(); break;

                default: base.OnOptionsPropertyChanged(sender, e); break;
            }
        }
        protected virtual void OnListFormStyleChanged()
        {
            if (OptionsListForm.AutoShowFilterPanel)
            {
                biFilterPanel.Checked = OptionsListForm.ListFormStyle == ListFormStyle.Choose && OptionsListForm.IsFilterable;
            }
        }
        protected virtual void OnIsAddableChanged()
        {
            biAdd.Visibility = OptionsListForm.IsAddable ? BarItemVisibility.Always : BarItemVisibility.Never;
        }
        protected virtual void OnIsApprovableChanged()
        {
            biApprove.Visibility = OptionsListForm.IsApprovable ? BarItemVisibility.Always : BarItemVisibility.Never;
        }
        protected virtual void OnAutoRefreshChanged()
        {
            biAutoRefresh.Visibility = OptionsListForm.AutoRefresh ? BarItemVisibility.Always : BarItemVisibility.Never;
        }
        protected virtual void OnIsBesftFitableChanged()
        {
            biBestFit.Visibility = OptionsListForm.IsBesftFitable ? BarItemVisibility.Always : BarItemVisibility.Never;
        }
        protected virtual void OnIsBlockableChanged()
        {
            miBlock.Visibility = OptionsListForm.IsBlockable ? BarItemVisibility.Always : BarItemVisibility.Never;
        }
        protected virtual void OnIsChooseableChanged()
        {
            biChoose.Visibility = OptionsListForm.IsChooseable ? BarItemVisibility.Always : BarItemVisibility.Never;
        }
        protected virtual void OnIsDeletableChanged()
        {
            biDelete.Visibility = OptionsListForm.IsDeletable ? BarItemVisibility.Always : BarItemVisibility.Never;
        }
        protected virtual void OnIsDisapprovableChanged()
        {
            miDisapprove.Visibility = OptionsListForm.IsDisapprovable ? BarItemVisibility.Always : BarItemVisibility.Never;
        }
        protected virtual void OnIsEditableChanged()
        {
            biEdit.Visibility = OptionsListForm.IsEditable ? BarItemVisibility.Always : BarItemVisibility.Never;
        }
        protected virtual void OnIsExportableChanged()
        {
            biExport.Visibility = OptionsListForm.IsExportable ? BarItemVisibility.Always : BarItemVisibility.Never;
        }
        protected virtual void OnIsFilterableChanged()
        {
            pnlFilter.Visible = OptionsListForm.IsFilterable && biFilter.Checked;
            btnFilter.Visible = OptionsListForm.IsFilterable;

            biFilterPanel.Visibility =
            biFilter.Visibility = OptionsListForm.IsFilterable ? BarItemVisibility.Always : BarItemVisibility.Never;
        }
        protected virtual void OnIsFilterApprovableChanged()
        {
            biFilterApprove.Visibility = OptionsListForm.IsFilterApprovable ? BarItemVisibility.Always : BarItemVisibility.Never;
        }
        protected virtual void OnIsRefreshableChanged()
        {
            biRefresh.Visibility = OptionsListForm.IsRefreshable ? BarItemVisibility.Always : BarItemVisibility.Never;
        }
        protected virtual void OnIsSumableChanged()
        {
            biSum.Visibility = OptionsListForm.IsSumable ? BarItemVisibility.Always : BarItemVisibility.Never;
        }
        protected virtual void OnIsSystemColumnsableChanged()
        {
            biSystemColumns.Visibility = OptionsListForm.IsSystemColumnsable ? BarItemVisibility.Always : BarItemVisibility.Never;
        }
        protected virtual void OnIsTopableChanged()
        {
            biTopRecords.Visibility = OptionsListForm.IsTopable ? BarItemVisibility.Always : BarItemVisibility.Never;
        }
        protected virtual void OnIsUnblockableChanged()
        {
            miUnblock.Visibility = OptionsListForm.IsBlockable ? BarItemVisibility.Always : BarItemVisibility.Never;
        }
        protected virtual void OnTopRecordsCountChanged()
        {
            biTopRecords.Caption = string.Format(Resources.TopRecordsCaptionFormat, OptionsGrid.TopRecordsCount);
            biTopRecords.Hint = string.Format(Resources.TopRecordsHintFormat, OptionsGrid.TopRecordsCount);
        }
        #endregion

        #region IsAllowed
        [Browsable(false)]
        protected virtual bool IsAllowedBlock
        {
            get
            {
                var allowed = OptionsListForm.IsBlockable && /*(!OptionsPermission.AutoInitPermission ||*/ IsPermitted(DatabaseAction.Block) && (OptionsGrid.GridView.SelectedRowsCount > 0);
                //        if (flag)
                //        {
                //            byte approvedStatus = (byte)Zek.Data.DatabaseStatus.Approved;
                //            foreach (int rowHandle in OptionsGrid.GridView.GetSelectedRows())
                //            {
                //                if (rowHandle < 0 || (byte)OptionsGrid.GridView.GetRowCellValue(rowHandle, OptionsGrid.ColumnStatusID) != approvedStatus)
                //                {
                //                    flag = false;
                //                    break;
                //                }
                //            }
                //        }

                return allowed;
            }
        }
        [Browsable(false)]
        protected virtual bool IsAllowedUnBlock
        {
            get
            {
                var allowed = OptionsListForm.IsBlockable && IsPermitted(DatabaseAction.Unblock) && (OptionsGrid.GridView.SelectedRowsCount > 0);
                //        if (flag)
                //        {
                //            byte status = (byte)Zek.Data.DatabaseStatus.Blocked;
                //            foreach (int rowHandle in OptionsGrid.GridView.GetSelectedRows())
                //            {
                //                if (rowHandle < 0 || (byte)OptionsGrid.GridView.GetRowCellValue(rowHandle, OptionsGrid.ColumnStatusID) != status)
                //                {
                //                    flag = false;
                //                    break;
                //                }
                //            }
                //        }

                return allowed;
            }
        }
        [Browsable(false)]
        protected virtual bool IsAllowedAdd => OptionsListForm.IsAddable && IsPermitted(DatabaseAction.Add);

        [Browsable(false)]
        protected virtual bool IsAllowedEdit => OptionsListForm.IsEditable && (IsPermitted(DatabaseAction.View) || IsPermitted(DatabaseAction.Edit)) && OptionsGrid.SelectedRowHandle >= 0;

        [Browsable(false)]
        protected virtual bool IsAllowedDelete
        {
            get
            {
                var allowed = OptionsListForm.IsDeletable && IsPermitted(DatabaseAction.Delete) && OptionsGrid.SelectedRowHandle >= 0;
                //if (allowed && OptionsListForm.IsApprovable)
                //{
                //    const byte status = (byte)DatabaseStatus.Pending;
                //    foreach (int rowHandle in OptionsGrid.GridView.GetSelectedRows())
                //    {
                //        if (rowHandle < 0 || (byte)OptionsGrid.GridView.GetRowCellValue(rowHandle, OptionsGrid.DataStatusName) != status)
                //        {
                //            allowed = false;
                //            break;
                //        }
                //    }
                //}

                return allowed;
            }
        }
        [Browsable(false)]
        protected virtual bool IsAllowedChoose => OptionsListForm.IsChooseable && (OptionsListForm.ListFormStyle == ListFormStyle.Choose) && (OptionsGrid.SelectedRowHandle >= 0);

        [Browsable(false)]
        protected virtual bool IsAllowedApprove
        {
            get
            {
                var allowed = OptionsListForm.IsApprovable && IsPermitted(DatabaseAction.Approve) && OptionsGrid.SelectedRowHandle >= 0;
                //if (allowed)
                //{
                //    const byte status = (byte)DatabaseStatus.Pending;
                //    foreach (var rowHandle in OptionsGrid.GridView.GetSelectedRows())
                //    {
                //        if (rowHandle < 0 || (byte)OptionsGrid.GridView.GetRowCellValue(rowHandle, OptionsGrid.DataStatusName) != status)
                //        {
                //            allowed = false;
                //            break;
                //        }
                //    }
                //}

                return allowed;
            }
        }
        [Browsable(false)]
        protected virtual bool IsAllowedDisapprove
        {
            get
            {
                var allowed = OptionsListForm.IsDisapprovable && IsPermitted(DatabaseAction.Disapprove) && OptionsGrid.SelectedRowHandle >= 0;
                //        if (flag)
                //        {
                //            byte status = (byte)Zek.Data.DatabaseStatus.Approved;
                //            foreach (int rowHandle in OptionsGrid.GridView.GetSelectedRows())
                //            {
                //                if (rowHandle < 0 || (byte)OptionsGrid.GridView.GetRowCellValue(rowHandle, OptionsGrid.ColumnStatusID) != status)
                //                {
                //                    flag = false;
                //                    break;
                //                }
                //            }
                //        }

                return allowed;
            }
        }
        [Browsable(false)]
        protected virtual bool IsAllowedExport => OptionsListForm.IsExportable && IsPermitted(DatabaseAction.Export);

        [Browsable(false)]
        protected virtual bool IsAllowedCopy => OptionsListForm.IsCopyable && IsPermitted(DatabaseAction.Copy);

        [Browsable(false)]
        protected virtual bool IsAllowedSystemColumns => OptionsListForm.IsSystemColumnsable && IsPermitted(DatabaseAction.HiddenColumn);

        #endregion


        [Category("Zek"), DefaultValue(false)]
        protected bool IsFilterPanelVisible
        {
            get { return biFilterPanel.Checked; }
            set { biFilterPanel.Checked = value; }
        }
        [Category("Zek"), DefaultValue(true)]
        protected bool IsTopRecordsChekhed
        {
            get { return biTopRecords.Checked; }
            set { biTopRecords.Checked = value; }
        }

        #region SQL
        [Browsable(false)]
        protected virtual string SelectSql => "SELECT" + (OptionsListForm.IsTopable && IsTopRecordsChekhed ? " TOP (" + OptionsGrid.TopRecordsCount + ")" : string.Empty);

        [Browsable(false)]
        protected virtual string SelectAllFromTable
        {
            get
            {
                ValidateOptionsGridViewName();
                return SelectSql + " *" + Environment.NewLine + "FROM " + OptionsGrid.ViewName + (OptionsGrid.NoLock ? " WITH (NOLOCK)" : string.Empty);
            }
        }

        private void ValidateOptionsGridViewName()
        {
            if (OptionsGrid.ViewName.Length == 0) throw new NullReferenceException("OptionsGrid.ViewName is empty.");
        }
        [Browsable(false)]
        protected virtual string SelectAllFromTableWhereStatus
        {
            get
            {
                var filter = FilterStatusSql;
                return SelectAllFromTable + (filter.Length != 0 ? Environment.NewLine + "WHERE " + filter : string.Empty);
            }
        }
        [Browsable(false)]
        protected virtual string FilterStatusSql
        {
            get
            {
                if (!OptionsListForm.IsFilterApprovable) return string.Empty;

                if (biFilterApprove.Checked && OptionsGrid.ApprovedWhereClause.Length > 0)
                    return OptionsGrid.ApprovedWhereClause;

                if (!biFilterApprove.Checked && OptionsGrid.PendingWhereClause.Length > 0)
                    return OptionsGrid.PendingWhereClause;

                return string.Empty;
            }
        }
        #endregion

        #region Bindings
        protected void TryBindGrid()
        {
            if (IsLoading || (biFilter.Checked && !IsValid)) return;

            try
            {
                Application.DoEvents();
                Cursor = Cursors.WaitCursor;

                BindGrid();
                InitToolbar();

                if (OptionsListForm.IsBesftFitable)
                    TryBestFitColumns();
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, "BindGrid", MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        protected virtual void BindGrid() { }
        #endregion

        //#region XtraGridView
        ////private Point mouseHitPoint;
        protected virtual void InitGrid(GridView view)
        {
            XtraGridHelper.InitListFormGrid(view);
            if (OptionsBaseForm.AutoInitText)
            {
                view.RowCountChanged += delegate (object sender, EventArgs e)
                {
                    var tmp = sender as GridView;
                    if (tmp != null)
                        Text = InternalText + " (" + tmp.RowCount + ")";
                };
            }

            //view.SelectionChanged += delegate(object sender, SelectionChangedEventArgs e)
            //{
            //    InitToolbar();
            //};
            view.SelectionChanged += (sender, e) => InitToolbar();
            //view.DoubleClick += delegate(object sender, EventArgs e)
            //{
            view.DoubleClick += delegate
            {
                //var tmp = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                //DevExpress.XtraGrid.Views.Grid.GridView gv = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                //DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hitInfo = gv.CalcHitInfo(mouseHitPoint);
                //if (!hitInfo.InRow || gv.IsGroupRow(hitInfo.RowHandle)) return;

                try
                {
                    Application.DoEvents();
                    Cursor = Cursors.WaitCursor;

                    if (OptionsGrid.SelectedRowHandle < 0 || !OptionsGrid.GridView.IsRowClicked()) return;
                    switch (OptionsListForm.ListFormStyle)
                    {
                        case ListFormStyle.Default:
                            TryEditRecord();
                            break;

                        case ListFormStyle.Choose:
                            TryChooseRecord();
                            break;
                    }
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            };
            //gridView.ShownEditor+=new EventHandler(gridView_ShownEditor);

            view.PopupMenuShowing += OnGridViewShowGridMenu;


            //OptionsGrid.InitStyleFormatCondition();
        }
        ////private void gridView_ShownEditor(object sender, EventArgs e)
        ////{
        ////    DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
        ////    if (view == null) return;
        ////    view.ActiveEditor.DoubleClick += new EventHandler(activeEditor_DoubleClick);
        ////}
        ////private void activeEditor_DoubleClick(object sender, EventArgs e)
        ////{
        ////    gridView_DoubleClick(GridView, e);
        ////}

        protected virtual void OnGridViewShowGridMenu(object sender, PopupMenuShowingEventArgs e)
        {
            var hitInfo = OptionsGrid.GridView.CalcHitInfo(e.Point);
            if (hitInfo.InRow)
            {
                //GridView.FocusedRowHandle = hitInfo.RowHandle;
                //GridView.FocusedColumn = hitInfo.Column;

                // Update enabled or visible states of popup menu items
                // based on value in focused row and column here.

                popupMenu.ShowPopup(MousePosition);
            }


            //სისტემური სვეტების მენიუს გათიშვა (თუ არ აქვს უფლება)
            switch (e.MenuType)
            {
                case GridMenuType.Column:
                    var miCustomize = XtraGridExtentions.GetItemByStringId(e.Menu, GridStringId.MenuColumnColumnCustomization);
                    if (miCustomize != null)
                        miCustomize.Enabled = IsAllowedSystemColumns;
                    break;
            }
        }
        //private void gridView_ShowAutoFilterRowCheckedChanged(object sender, EventArgs e)
        //{
        //    OptionsGrid.GridView.OptionsView.ShowAutoFilterRow = !OptionsGrid.GridView.OptionsView.ShowAutoFilterRow;
        //}
        //#endregion

        //#region Validations
        //protected bool IsPendingRecords(bool showWarning)
        //{
        //    bool flag = true;
        //    byte pendingStatus = (byte)Zek.Data.DatabaseStatus.Pending;
        //    foreach (int rowHandle in OptionsGrid.GridView.GetSelectedRows())
        //    {
        //        if ((byte)OptionsGrid.GridView.GetRowCellValue(rowHandle, OptionsGrid.ColumnStatusID) != pendingStatus)
        //        {
        //            if (showWarning)
        //                XtraMessageBox.Show(this, string.Format("ჩანაწერი №{0} უკვე დამოწმებულია.", rowHandle + 1), "გაფრთხილება", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        //            flag = false;
        //            break;
        //        }
        //    }

        //    return flag;
        //}
        //protected bool IsApprovedRecords(bool showWarning)
        //{
        //    bool flag = true;
        //    byte approvedStatus = (byte)Zek.Data.DatabaseStatus.Approved;
        //    foreach (int rowHandle in OptionsGrid.GridView.GetSelectedRows())
        //    {
        //        if ((byte)OptionsGrid.GridView.GetRowCellValue(rowHandle, OptionsGrid.ColumnStatusID) != approvedStatus)
        //        {
        //            if (showWarning)
        //                XtraMessageBox.Show(this, string.Format("ჩანაწერი №{0} არ არის დამოწმებული.", rowHandle + 1), "გაფრთხილება", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        //            flag = false;
        //            break;
        //        }
        //    }

        //    return flag;
        //}
        //#endregion

        protected virtual void OnGridControlProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
                e.Handled = !IsAllowedCopy;
        }
        protected virtual void OnGridViewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
                e.Handled = !IsAllowedCopy;
        }


        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected Enum FormEnum
        {
            get;
            set;
        }


        #region Methods
        //protected virtual void ActionRecord(int rowHandle, Zek.Data.DatabaseAction action)
        //{
        //    object value = OptionsGrid.GridView.GetRowCellValue(rowHandle, OptionsGrid.ColumnID);

        //    int paramInt = 0;
        //    string paramString = string.Empty;
        //    Guid paramGuid = Guid.Empty;
        //    DateTime paramDateTime = DateTime.Now;

        //    if (value is int)
        //        paramInt = (int)value;
        //    else if (value is string)
        //        paramString = (string)value;
        //    else if (value is Guid)
        //        paramGuid = (Guid)value;
        //    else if (value is DateTime)
        //        paramDateTime = (DateTime)value;

        //    ActionRecordEventArgs e = new ActionRecordEventArgs(paramInt, paramString, paramGuid, paramDateTime, action);
        //    ActionRecord(rowHandle, e);
        //}
        //protected virtual void ActionRecord(int rowHandle, ActionRecordEventArgs e)
        //{
        //    throw new NotImplementedException("მეთოდი არ არის აღწერილი (მეთოდის სახელი: ActionRecord).");
        //    //DictionaryManager.ActionRecord(TableName, paramInt, paramString, paramDateTime, paramGuid, 0, string.Empty, DateTime.Now, Guid.Empty, action, GlobalVariable.UserID);
        //}

        /// <summary>
        /// Try to block selected items in grid.
        /// </summary>
        protected void TryBlockRecords()
        {
            try
            {
                BlockRecords();
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, "BlockRecords", MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Block selected items in grid.
        /// </summary>
        protected virtual void BlockRecords()
        {
            if (!IsAllowedBlock) return;

            if (XtraMessageBox.Show(this, Exceptions.BlockRecordsQuestion, Resources.Block, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;


            var i = -1;
            var bindGrid = false;
            try
            {
                foreach (var rowHandle in OptionsGrid.GridView.GetSelectedRows())
                {
                    i = rowHandle;
                    BlockRecord(rowHandle);
                    bindGrid = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, string.Format(Exceptions.BlockRecordsErrorFormat, i + 1), MessageBoxIcon.Error);
            }

            if (bindGrid) TryBindGrid();
        }
        /// <summary>
        /// Block item.
        /// </summary>
        /// <param name="rowHandle">Row handle in grid.</param>
        protected virtual void BlockRecord(int rowHandle)
        {
            //    ActionRecord(rowHandle, Zek.Data.DatabaseAction.Block);
        }

        /// <summary>
        /// Try to unblock selected items in grid.
        /// </summary>
        protected void TryUnblockRecords()
        {
            try
            {
                UnblockRecords();
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, "UnblockRecords", MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Block selected items in grid.
        /// </summary>
        protected virtual void UnblockRecords()
        {
            if (!IsAllowedUnBlock) return;

            if (XtraMessageBox.Show(this, Exceptions.UnblockRecordsQuestion, Resources.Unblock, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;


            var i = -1;
            var bingGrid = false;
            try
            {
                foreach (var rowHandle in OptionsGrid.GridView.GetSelectedRows())
                {
                    i = rowHandle;
                    UnblockRecord(rowHandle);
                    bingGrid = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, string.Format(Exceptions.UnblockRecordsErrorFormat, i + 1), MessageBoxIcon.Error);
            }

            if (bingGrid) TryBindGrid();
        }
        /// <summary>
        ///  Block item in grid.
        /// </summary>
        /// <param name="rowHandle">Row handle in grid.</param>
        protected virtual void UnblockRecord(int rowHandle)
        {
            //    ActionRecord(rowHandle, Zek.Data.DatabaseAction.Unblock);
        }


        protected void TryAddRecord()
        {
            try
            {
                AddRecord();
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, "AddRecord", MessageBoxIcon.Error);
            }
        }
        protected virtual void AddRecord()
        {
            if (!IsAllowedAdd) return;

            switch (OptionsListForm.EditFormStyle)
            {
                case EditFormStyle.Default:
                    var contains = SingletonFormProvider.ContainsEditForm(FormEnum, IntPtr.Zero, null);
                    var frm = SingletonFormProvider.ShowEditForm(FormEnum, MdiParent, this);
                    if (!contains)
                        frm.FormClosed += ChildFormClosed;
                    break;


                case EditFormStyle.Dialog:
                    if (SingletonFormProvider.ShowDialogForm(FormEnum, MdiParent, this) == DialogResult.OK && OptionsListForm.AutoRefresh && biAutoRefresh.Checked)
                        TryBindGrid();
                    break;
            }
        }


        protected void TryEditRecord()
        {
            try
            {
                EditRecord();
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, "EditRecord", MessageBoxIcon.Error);
            }
        }
        protected virtual void EditRecord()
        {
            if (!IsAllowedEdit) return;

            var id = OptionsGrid.SelectedRecordID;
            switch (OptionsListForm.EditFormStyle)
            {
                case EditFormStyle.Default:
                    var contains = SingletonFormProvider.ContainsEditForm(FormEnum, IntPtr.Zero, id);
                    var frm = SingletonFormProvider.ShowEditForm(FormEnum, MdiParent, this, IntPtr.Zero, id);
                    if (!contains)
                        frm.FormClosed += ChildFormClosed;
                    break;

                case EditFormStyle.Dialog:
                    if (SingletonFormProvider.ShowDialogForm(FormEnum, MdiParent, this, IntPtr.Zero, id) == DialogResult.OK && OptionsListForm.AutoRefresh && biAutoRefresh.Checked)
                        TryBindGrid();
                    break;
            }
        }

        protected void TryDeleteRecords()
        {
            try
            {
                DeleteRecords();
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, "DeleteRecords", MessageBoxIcon.Error);
            }
        }
        protected virtual void DeleteRecords()
        {
            if (!IsAllowedDelete) return;

            if (XtraMessageBox.Show(this, Exceptions.DeleteRecordsQuestion, Resources.Delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;


            var i = -1;
            var bindGrid = false;
            try
            {
                foreach (var rowHandle in OptionsGrid.GridView.GetSelectedRows())
                {
                    i = rowHandle;
                    DeleteRecord(rowHandle);
                    bindGrid = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, string.Format(Exceptions.DeleteRecordsErrorFormat, i + 1), MessageBoxIcon.Error);
            }

            if (bindGrid)
                TryBindGrid();
        }
        protected virtual void DeleteRecord(int rowHandle)
        {
            //    ActionRecord(rowHandle, Zek.Data.DatabaseAction.Delete);
        }

        /// <summary>
        /// წარმოიშვებმა მაშინვე როგორც კი ChooseRecord.
        /// </summary>
        [Category("Events"), Description("Invokes when choosing record.")]
        public event CancelEventHandler Choose;

        protected void TryChooseRecord()
        {
            try
            {
                var e = new CancelEventArgs();
                ChooseRecord(e);
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, "ChooseRecord", MessageBoxIcon.Error);
            }
        }
        protected virtual void ChooseRecord(CancelEventArgs e)
        {
            if (OptionsListForm.ListFormStyle != ListFormStyle.Choose || !IsAllowedChoose || !OptionsGrid.GridView.IsFocusedView)
            {
                e.Cancel = true;
                return;
            }

            DialogResult = DialogResult.OK;

            if (Choose != null)
                Choose(this, e);

            if (e.Cancel) return;

            if (OptionsListForm.CloseAfterChoose)
                BeginInvoke(new MethodInvoker(delegate { Close(); }));
        }


        protected void TryApproveRecords()
        {
            try
            {
                ApproveRecords();
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, "ApproveRecords", MessageBoxIcon.Error);
            }


        }
        protected virtual void ApproveRecords()
        {
            if (!IsAllowedApprove) return;


            if (XtraMessageBox.Show(this, Exceptions.ApproveRecordsQuestion, Resources.Approve, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var i = -1;
            var bindGrid = false;
            try
            {
                foreach (var rowHandle in OptionsGrid.GridView.GetSelectedRows())
                {
                    i = rowHandle;
                    ApproveRecord(rowHandle);
                    bindGrid = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, string.Format(Exceptions.ApproveRecordsErrorFormat, i + 1), MessageBoxIcon.Error);
            }

            if (bindGrid)
                TryBindGrid();
        }
        protected virtual void ApproveRecord(int rowHandle)
        {
            //    ActionRecord(rowHandle, Zek.Data.DatabaseAction.Approve);
        }


        protected void TryDisapproveRecords()
        {
            try
            {
                DisapproveRecords();
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, "DisapproveRecords", MessageBoxIcon.Error);
            }
        }
        protected virtual void DisapproveRecords()
        {
            if (!IsAllowedDisapprove) return;

            if (XtraMessageBox.Show(this, Exceptions.DisapproveRecordsQuestion, Resources.Disapprove, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var i = -1;
            var bindGrid = false;
            try
            {
                foreach (var rowHandle in OptionsGrid.GridView.GetSelectedRows())
                {
                    i = rowHandle;
                    DisapproveRecord(rowHandle);
                    bindGrid = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, string.Format(Exceptions.DisapproveRecordsErrorFormat, i + 1), MessageBoxIcon.Error);
            }

            if (bindGrid)
                TryBindGrid();
        }
        protected virtual void DisapproveRecord(int rowHandle)
        {
            //ActionRecord(rowHandle, Zek.Data.DatabaseAction.Disapprove);
        }

        protected void TrySum()
        {
            try
            {
                Sum();
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, "Sum", MessageBoxIcon.Error);
            }
        }
        protected virtual void Sum()
        {
            if (OptionsGrid.GridView == null)
                throw new ArgumentNullException("OptionsGrid.GridView");
            if (OptionsGrid.GridView.RowCount == 0) return;

            var table = new DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Sum", typeof(double));

            foreach (GridColumn col in OptionsGrid.GridView.VisibleColumns)
            {
                if (col.ColumnType == null) continue;

                if (!col.ColumnType.IsEnum)
                {
                    if (col.ColumnType == typeof(decimal) || col.ColumnType == typeof(decimal?) ||
                       col.ColumnType == typeof(double) || col.ColumnType == typeof(double?) ||
                       col.ColumnType == typeof(float) || col.ColumnType == typeof(float?)
                       )
                    {
                        var name = !string.IsNullOrWhiteSpace(col.Caption) ? col.Caption : col.FieldName;
                        var sum = 0d;
                        foreach (var rowHandle in OptionsGrid.GridView.GetSelectedRows())
                        {
                            var obj = OptionsGrid.GridView.GetRowCellValue(rowHandle, col);
                            sum += obj != null && obj != DBNull.Value ? Convert.ToDouble(obj) : 0d;
                        }

                        table.Rows.Add(name, sum);
                        break;
                    }

                    //switch (Type.GetTypeCode(col.ColumnType))
                    //{
                    //    case TypeCode.SByte:
                    //    case TypeCode.Byte:
                    //    case TypeCode.Int16:
                    //    case TypeCode.UInt16:
                    //    case TypeCode.Int32:
                    //    case TypeCode.UInt32:
                    //    case TypeCode.Int64:
                    //    case TypeCode.UInt64:
                    //    case TypeCode.Single:
                    //    case TypeCode.Double:
                    //    case TypeCode.Decimal:
                    //        var name = !string.IsNullOrWhiteSpace(col.Caption) ? col.Caption : col.FieldName;
                    //        var sum = 0d;
                    //        foreach (var rowHandle in OptionsGrid.GridView.GetSelectedRows())
                    //        {
                    //            var obj = OptionsGrid.GridView.GetRowCellValue(rowHandle, col);
                    //            sum += (obj != null && obj != DBNull.Value ? Convert.ToDouble(obj) : 0d);
                    //        }

                    //        table.Rows.Add(name, sum);
                    //        break;
                    //}
                }
            }

            Form frm = null;
            try
            {
                frm = new Form
                {
                    Text = Resources.Count + @": " + OptionsGrid.GridView.SelectedRowsCount,
                    StartPosition = FormStartPosition.CenterScreen,
                    ShowIcon = false,
                    MaximizeBox = false,
                    ShowInTaskbar = false,
                    Height = Math.Min(64 + table.Rows.Count * 22, 600)
                };

                var grid = new DataGridView
                {
                    Font = OptionsGrid.GridView.Appearance.Row.Font,
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    SelectionMode = DataGridViewSelectionMode.CellSelect,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                };
                frm.Controls.Add(grid);
                grid.Dock = DockStyle.Fill;

                grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Name", DataPropertyName = "Name", HeaderText = Resources.Name, });
                grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Sum", DataPropertyName = "Sum", HeaderText = Resources.Sum });
                grid.Columns[1].DefaultCellStyle.Format = "N2";
                grid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


                grid.DataSource = table;

                frm.ShowDialog(this);
            }
            finally
            {
                frm?.Dispose();
            }
        }

        protected void TryExport()
        {
            try
            {
                Export();
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, "Export", MessageBoxIcon.Error);
            }
        }
        protected virtual void Export()
        {
            if (!IsAllowedExport) return;

            string fileName;
            using (var dlg = new SaveFileDialog { Filter = XtraGridHelper.GetSaveFileDialogFilter() })
            {
                if (dlg.ShowDialog(this) != DialogResult.OK) return;

                fileName = dlg.FileName;
                XtraGridHelper.Export(OptionsGrid.GridView, fileName);
            }

            if (File.Exists(fileName))
            {
                if (XtraMessageBox.Show(this, Exceptions.OpenAfterExportQuestion, Resources.Open, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                Process.Start(fileName);
            }
        }

        protected void TryRefreshGrid()
        {
            if (!OptionsListForm.IsRefreshable) return;

            TryBindGrid();
        }

        protected void TryFilterPanel()
        {
            try
            {
                FilterPanel();
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, "FilterPanel", MessageBoxIcon.Error);
            }
        }
        protected virtual void FilterPanel()
        {
            if (!OptionsListForm.IsFilterable)
            {
                pnlFilter.Visible = false;
                return;
            }

            pnlFilter.Visible = biFilterPanel.Checked;
        }

        protected void TryFilter()
        {
            if (!OptionsListForm.IsFilterable) return;

            TryBindGrid();
        }


        protected void TryBestFitColumns()
        {
            try
            {
                BestFitColumns();
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, "BestFitColumns", MessageBoxIcon.Error);
            }
        }
        protected virtual void BestFitColumns()
        {
            if (OptionsGrid.GridView == null) return;

            XtraGridHelper.BestFitColumns(OptionsGrid.GridView);
        }

        protected void TryShowHideSystemColumns()
        {
            try
            {
                ShowHideSystemColumns();
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, "ShowHideSystemColumns", MessageBoxIcon.Error);
            }
        }
        protected void ShowHideSystemColumns()
        {
            XtraGridHelper.ShowHideSystemColumns(OptionsGrid.GridView, biSystemColumns.Checked);

            if (OptionsListForm.IsBesftFitable)
                TryBestFitColumns();
        }

        protected override void Init()
        {
            base.Init();

            //todo Init()
            //biRefresh.Enabled = IsAllowedRefresh;
            //biTopRecords.Enabled = IsAllowedTop;
            biFilterPanel.Enabled = OptionsListForm.IsFilterable;
            biFilter.Enabled = OptionsListForm.IsFilterable;
            biFilterApprove.Enabled = OptionsListForm.IsFilterable;
            biSystemColumns.Enabled = IsAllowedSystemColumns;

            if (OptionsGrid.AutoInit && OptionsGrid.GridView != null)
                InitGrid(OptionsGrid.GridView);
        }
        protected virtual void InitToolbar()
        {
            biAdd.Enabled = IsAllowedAdd;
            biEdit.Enabled = IsAllowedEdit;
            biDelete.Enabled = IsAllowedDelete;
            biChoose.Enabled = IsAllowedChoose;
            biApprove.Enabled = IsAllowedApprove;

            biPrint.Enabled = IsAllowedPrint;
            biExport.Enabled = IsAllowedExport;

            miBlock.Enabled = IsAllowedBlock;
            miUnblock.Enabled = IsAllowedUnBlock;
            miDisapprove.Enabled = IsAllowedDisapprove;
        }

        //protected virtual void OnFilterApproveCheckedChanged() { }


        protected override void OnIsPrintableChanged()
        {
            base.OnIsPrintableChanged();
            biPrint.Visibility = OptionsBaseForm.IsPrintable ? BarItemVisibility.Always : BarItemVisibility.Never;
        }

        protected override void SetImageSize16x16()
        {
            biAdd.Glyph = Images.add_16x16;
            biEdit.Glyph = Images.tool_pencil_16x16;
            biDelete.Glyph = Images.delete_16x16;
            biChoose.Glyph = Images.select_16x16;
            biApprove.Glyph = Images.contract_16x16;
            biSum.Glyph = Images.sum_16x16;
            biPrint.Glyph = Images.print_16x16;
            biExport.Glyph = Images.table_export_16x16;
            biRefresh.Glyph = Images.refresh_update_16x16;
            biTopRecords.Glyph = Images.table_16x16;
            biFilterPanel.Glyph = Images.filter_16x16;
            biFilter.Glyph = Images.filter_ok_16x16;
            biFilterApprove.Glyph = Images.contract_ok_16x16;
            biBestFit.Glyph = Images.full_screen_16x16;
            biSystemColumns.Glyph = Images.table_field_ok_16x16;
            biAutoRefresh.Glyph = Images.reload_rotate_16x16;
        }
        protected override void SetImageSize24x24()
        {
            biAdd.Glyph = Images.add_24x24;
            biEdit.Glyph = Images.tool_pencil_24x24;
            biDelete.Glyph = Images.delete_24x24;
            biChoose.Glyph = Images.select_24x24;
            biApprove.Glyph = Images.contract_24x24;
            biSum.Glyph = Images.sum_24x24;
            biPrint.Glyph = Images.print_24x24;
            biExport.Glyph = Images.table_export_24x24;
            biRefresh.Glyph = Images.refresh_update_24x24;
            biTopRecords.Glyph = Images.table_24x24;
            biFilterPanel.Glyph = Images.filter_24x24;
            biFilter.Glyph = Images.filter_ok_24x24;
            biFilterApprove.Glyph = Images.contract_ok_24x24;
            biBestFit.Glyph = Images.full_screen_24x24;
            biSystemColumns.Glyph = Images.table_field_ok_24x24;
            biAutoRefresh.Glyph = Images.reload_rotate_24x24;
        }
        #endregion

        protected virtual void ChildFormClosed(object sender, FormClosedEventArgs e)
        {
            var frm = sender as Form;
            if (frm == null) return;

            frm.FormClosed -= ChildFormClosed;

            if (OptionsListForm.AutoRefresh && frm.DialogResult == DialogResult.OK)
            {
                TryBindGrid();
            }
        }



        private void ListForm_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            if (OptionsListForm.AutoRefreshOnLoad)
                TryBindGrid();
        }

        private void biAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Application.DoEvents();
                Cursor = Cursors.WaitCursor;
                TryAddRecord();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void biEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Application.DoEvents();
                Cursor = Cursors.WaitCursor;
                TryEditRecord();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void biDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Application.DoEvents();
                Cursor = Cursors.WaitCursor;
                TryDeleteRecords();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void biChoose_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Application.DoEvents();
                Cursor = Cursors.WaitCursor;
                TryChooseRecord();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void biApprove_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Application.DoEvents();
                Cursor = Cursors.WaitCursor;
                TryApproveRecords();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void biSum_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Application.DoEvents();
                Cursor = Cursors.WaitCursor;
                TrySum();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void biPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Application.DoEvents();
                Cursor = Cursors.WaitCursor;
                TryPrint();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void biExport_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Application.DoEvents();
                Cursor = Cursors.WaitCursor;
                TryExport();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void biRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            TryRefreshGrid();
        }

        private void biTopRecords_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            if (!OptionsListForm.IsTopable) return;

            TryBindGrid();
        }

        private void biFilterPanel_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            try
            {
                Application.DoEvents();
                Cursor = Cursors.WaitCursor;
                TryFilterPanel();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void biFilter_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            TryFilter();
        }

        private void biFilterApprove_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            //todo biFilterApprove_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
            //OnFilterApproveCheckedChanged();
            InitToolbar();
            TryBindGrid();
        }

        private void biBestFit_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Application.DoEvents();
                Cursor = Cursors.WaitCursor;
                TryBestFitColumns();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void biSystemColumns_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            try
            {
                Application.DoEvents();
                Cursor = Cursors.WaitCursor;
                TryShowHideSystemColumns();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void biBlock_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Application.DoEvents();
                Cursor = Cursors.WaitCursor;
                TryBlockRecords();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void biUnblock_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Application.DoEvents();
                Cursor = Cursors.WaitCursor;

                TryUnblockRecords();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void biDisapprove_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Application.DoEvents();
                Cursor = Cursors.WaitCursor;
                TryDisapproveRecords();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (!biFilter.Checked)
                biFilter.Checked = true;
            else
                TryFilter();
        }
    }
}