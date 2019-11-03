using System.ComponentModel;
using DevExpress.XtraGrid.Views.Grid;

namespace Zek.Windows.Forms
{
    public class BaseGridViewOptions : BaseOptions
    {
        [Browsable(false)]
        public virtual object[] SelectedRecordIDs => null;

        [Browsable(false)]
        public virtual int SelectedRowHandle => -1;

        [Browsable(false)]
        public virtual object SelectedRecordID => null;

        [Browsable(false)]
        public virtual object SelectedStatusID => null;


        private bool _autoInit = true;
        /// <summary>
        /// როცა true მაშინ ავტომატურად უკეთებს ინიციალიზაციას Grid-ს.
        /// </summary>
        [Browsable(true),
        Category("Zek"),
        Description("Auto init grid in list form."),
        DefaultValue(true)]
        public bool AutoInit
        {
            get { return _autoInit; }
            set
            {
                if (_autoInit == value) return;
                var oldValue = _autoInit;
                _autoInit = value;
                OnChanged(new BaseOptionChangedEventArgs("AutoInit", oldValue, value));
            }
        }

        //private bool _autoInitStyleFormat = true;
        //[Browsable(true), Category("Zek"), Description("როცა true მაშინ ავტომატურად უკეთებს ინიციალიზაციას Grid-ს."), DefaultValue(true)]
        //public bool AutoInitStyleFormat
        //{
        //    get { return _autoInitStyleFormat; }
        //    set
        //    {
        //        if (_autoInitStyleFormat == value) return;
        //        bool oldValue = _autoInitStyleFormat;
        //        _autoInitStyleFormat = value;
        //        OnChanged(new BaseOptionChangedEventArgs("AutoInitStyleFormat", oldValue, value));
        //    }
        //}


        private string _dataKeyName = "ID";
        [Browsable(true),
        Description("ID column name."),
        DefaultValue("ID")]
        public string DataKeyName
        {
            get { return _dataKeyName; }
            set
            {
                if (Compare(_dataKeyName, value)) return;
                var oldValue = _dataKeyName;
                _dataKeyName = value;
                OnChanged(new BaseOptionChangedEventArgs("DataKeyNames", oldValue, value));
            }
        }


        private string _dataStatusName = "StatusID";
        [Browsable(true),
        Description("Status column name."),
        DefaultValue("StatusID")]
        public string DataStatusName
        {
            get { return _dataStatusName; }
            set
            {
                if (value == null) value = string.Empty;

                if (Compare(_dataStatusName, value)) return;
                var oldValue = _dataStatusName;
                _dataStatusName = value;
                OnChanged(new BaseOptionChangedEventArgs("DataStatusName", oldValue, value));
            }
        }




        private int _topRecordsCount = 100;
        [Browsable(true), Category("Zek"), Description("SELECT TOP (X) FROM TABLE."), DefaultValue(100)]
        public int TopRecordsCount
        {
            get { return _topRecordsCount; }
            set
            {
                if (_topRecordsCount == value) return;
                var oldValue = _topRecordsCount;
                _topRecordsCount = value;
                OnChanged(new BaseOptionChangedEventArgs("TopRecordsCount", oldValue, value));
            }
        }

        private string _viewName = string.Empty;
        [Browsable(true), Category("Zek"), Description("View name from database."), DefaultValue("")]
        public string ViewName
        {
            get { return _viewName; }
            set
            {
                if (value == null) value = string.Empty;
                if (Compare(_viewName, value)) return;
                var oldValue = _viewName;
                _viewName = value;
                OnChanged(new BaseOptionChangedEventArgs("ViewName", oldValue, value));
            }
        }

        private bool _noLock = true;
        [Browsable(true), Category("Zek"), Description("Used for SelectAllFromTable  when it is true, then appends WITH (NOLOCK)."), DefaultValue(true)]
        public bool NoLock
        {
            get { return _noLock; }
            set
            {
                if (_noLock == value) return;
                var oldValue = _noLock;
                _noLock = value;
                OnChanged(new BaseOptionChangedEventArgs("NoLock", oldValue, value));
            }
        }


        private string _pendingWhereClause = "StatusID = 1";
        [Browsable(true), Category("Zek"), Description("Filter for pending items."), DefaultValue("StatusID = 1")]
        public string PendingWhereClause
        {
            get { return _pendingWhereClause; }
            set
            {
                if (value == null) value = string.Empty;
                if (Compare(_pendingWhereClause, value)) return;
                var oldValue = _pendingWhereClause;
                _pendingWhereClause = value;
                OnChanged(new BaseOptionChangedEventArgs("PendingWhereClause", oldValue, value));
            }
        }


        private string _approvedWhereClause = "StatusID <> 1";
        [Browsable(true), Category("Zek"), Description("Filter for approved items."), DefaultValue("StatusID <> 1")]
        public string ApprovedWhereClause
        {
            get { return _approvedWhereClause; }
            set
            {
                if (value == null) value = string.Empty;
                if (Compare(_approvedWhereClause, value)) return;
                var oldValue = _approvedWhereClause;
                _approvedWhereClause = value;
                OnChanged(new BaseOptionChangedEventArgs("ApprovedWhereClause", oldValue, value));
            }
        }


        //public virtual void InitStyleFormatCondition() { }

        public override void Assign(BaseOptions options)
        {
            BeginUpdate();
            try
            {
                base.Assign(options);
                var op = options as BaseGridViewOptions;
                if (op != null)
                {
                    _autoInit = op.AutoInit;
                    _dataKeyName = op.DataKeyName;
                    _dataStatusName = op.DataStatusName;


                    _topRecordsCount = op.TopRecordsCount;
                    _viewName = op.ViewName;
                    _noLock = op.NoLock;
                    _pendingWhereClause = op.PendingWhereClause;
                    _approvedWhereClause = op.ApprovedWhereClause;
                }
            }
            finally
            {
                EndUpdate();
            }
        }
    }










    //public class DataGridViewOptions : BaseGridViewOptions
    //{
    //    private DataGridView _gridView;
    //    [Browsable(true),
    //    Category("Zek"),
    //    Description("გრიდი, რომელზეც ხდება ოპერაციები."),
    //    DefaultValue(null)]
    //    public DataGridView GridView
    //    {
    //        get { return _gridView; }
    //        set
    //        {
    //            if (Compare(_gridView, value)) return;
    //            DataGridView oldValue = _gridView;
    //            _gridView = value;
    //            OnChanged(new BaseOptionChangedEventArgs("GridView", oldValue, value));
    //        }
    //    }

    //    [Browsable(false)]
    //    public override object[] SelectedRecordIDs
    //    {
    //        get
    //        {
    //            if (GridView.SelectedRows.Count == 0) return null;
    //            object[] recordIDs = new object[GridView.SelectedRows.Count];
    //            int colIndex = GridView.IndexOfColumn(DataKeyName);
    //            for (int i = 0; i < GridView.SelectedRows.Count; i++)
    //            {
    //                recordIDs[i] = GridView[colIndex, i].Value;
    //            }
    //            return recordIDs;
    //        }
    //    }
    //    [Browsable(false)]
    //    public override int SelectedRowHandle
    //    {
    //        get { return GridView.SelectedRows.Count == 0 ? -1 : GridView.SelectedRows[0].Index; }
    //    }
    //    [Browsable(false)]
    //    public override object SelectedRecordID
    //    {
    //        get
    //        {
    //            var rowHandle = SelectedRowHandle;
    //            return (rowHandle >= 0 ? GridView[GridView.IndexOfColumn(DataKeyName), rowHandle].Value : null);
    //        }
    //    }
    //    [Browsable(false)]
    //    public override object SelectedStatusID
    //    {
    //        get
    //        {
    //            var rowHandle = SelectedRowHandle;
    //            return (rowHandle >= 0 ? (GridView[GridView.IndexOfColumn(DataStatusName), rowHandle].Value) : null);
    //        }
    //    }

    ////    public override void InitStyleFormatCondition()
    ////    {
    ////        base.InitStyleFormatCondition();
    ////    }

    //    public override void Assign(BaseOptions options)
    //    {
    //        BeginUpdate();
    //        try
    //        {
    //            base.Assign(options);
    //            var op = options as DataGridViewOptions;
    //            if (op != null)
    //            {
    //                _gridView = op.GridView;
    //            }
    //        }
    //        finally
    //        {
    //            EndUpdate();
    //        }
    //    }
    //}






    public class XtraGridViewOptions : BaseGridViewOptions
    {
        private GridView _gridView;
        [Browsable(true),
        Description("გრიდი, რომელზეც ხდება ოპერაციები."),
        DefaultValue(null)]
        public GridView GridView
        {
            get { return _gridView; }
            set
            {
                if (Compare(_gridView, value)) return;
                var oldValue = _gridView;
                _gridView = value;
                OnChanged(new BaseOptionChangedEventArgs("GridView", oldValue, value));
            }
        }

        //public override void InitStyleFormatCondition()
        //{
        //    if (!AutoInitStyleFormat || string.IsNullOrEmpty(DataStatusName)) return;

        //    DevExpress.XtraGrid.StyleFormatCondition condition = new DevExpress.XtraGrid.StyleFormatCondition(DevExpress.XtraGrid.FormatConditionEnum.Equal, GridView.Columns[DataStatusName], null, (int)Zek.Data.DatabaseStatus.Blocked, null, true);
        //    condition.Appearance.BackColor = System.Drawing.Color.LightGray;
        //    condition.Appearance.Options.HighPriority = true;
        //    //condition.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
        //    GridView.FormatConditions.Add(condition);

        //    //condition = new DevExpress.XtraGrid.StyleFormatCondition(DevExpress.XtraGrid.FormatConditionEnum.Equal, ColumnStatus, null, (byte)Zek.Data.Statuses.Approved, null, true);
        //    //condition.Appearance.BackColor = Color.FromArgb(155,187,89);
        //    //gridView.FormatConditions.Add(condition);
        //}

        [Browsable(false)]
        public override object[] SelectedRecordIDs
        {
            get
            {
                if (GridView.SelectedRowsCount == 0) return null;
                var result = new object[GridView.SelectedRowsCount];
                var handles = GridView.GetSelectedRows();
                for (var i = 0; i < handles.Length; i++)
                {
                    result[i] = GridView.GetRowCellValue(handles[i], DataKeyName);
                }
                return result;
            }
        }
        [Browsable(false)]
        public override int SelectedRowHandle => GridView.SelectedRowsCount > 0 ? GridView.GetSelectedRows()[0] : -1;

        [Browsable(false)]
        public override object SelectedRecordID => SelectedRowHandle >= 0 ? GridView.GetRowCellValue(SelectedRowHandle, DataKeyName) : null;

        [Browsable(false)]
        public override object SelectedStatusID => SelectedRowHandle >= 0 ? GridView.GetRowCellValue(SelectedRowHandle, DataStatusName) : null;

        public override void Assign(BaseOptions options)
        {
            BeginUpdate();
            try
            {
                base.Assign(options);
                var op = options as XtraGridViewOptions;
                if (op != null)
                {
                    _gridView = op.GridView;
                }
            }
            finally
            {
                EndUpdate();
            }
        }
    }
}
