using System.ComponentModel;

namespace Zek.Windows.Forms
{
    public class ListFormOptions : BaseOptions
    {
        //public string GetFormattedText(string text, System.Windows.Forms.DataGridView dataGridView)
        //{
        //    return GetFormattedText(text, dataGridView != null ? dataGridView.RowCount.ToString() : string.Empty);
        //}
        //public string GetFormattedText(string text, string arg1)
        //{
        //    return AutoFormatText ? string.Format(TextFormat, text, arg1) : text;
        //}


        //private string _actionRecordStoredProcedureName = "dbo.SP_ActionRecord";
        ///// <summary>
        ///// ActionRecord-ის პროცედურის სახელი (გამოიყენება: დადასტურებისას, წაშლისას, დაბლოკვისას...).
        ///// </summary>
        //[Browsable(true), Category("Zek"), Description("ActionRecord-ის პროცედურის სახელი (გამოიყენება: დადასტურებისას, წაშლისას, დაბლოკვისას...)."), DefaultValue("dbo.SP_ActionRecord")]
        //public string ActionRecordStoredProcedureName
        //{
        //    get { return _actionRecordStoredProcedureName; }
        //    set
        //    {
        //        if (Compare(_actionRecordStoredProcedureName, value)) return;
        //        string oldValue = _actionRecordStoredProcedureName;
        //        _actionRecordStoredProcedureName = value;
        //        OnChanged(new BaseOptionChangedEventArgs("ActionRecordStoredProcedureName", oldValue, value));
        //    }
        //}

        
        private bool _autoRefresh = true;
        [Browsable(true), Category("Zek"), Description("Auto refresh grid after edit form closed."), DefaultValue(true)]
        public bool AutoRefresh
        {
            get { return _autoRefresh; }
            set
            {
                if (_autoRefresh == value) return;
                var oldValue = _autoRefresh;
                _autoRefresh = value;
                OnChanged(new BaseOptionChangedEventArgs("AutoRefresh", oldValue, value));
            }
        }

        private bool _autoRefreshOnLoad = true;
        [Browsable(true), Category("Zek"), Description("Auto refresh grid on form load."), DefaultValue(true)]
        public bool AutoRefreshOnLoad
        {
            get { return _autoRefreshOnLoad; }
            set
            {
                if (_autoRefreshOnLoad == value) return;
                var oldValue = _autoRefreshOnLoad;
                _autoRefreshOnLoad = value;
                OnChanged(new BaseOptionChangedEventArgs("AutoRefreshOnLoad", oldValue, value));
            }
        }
        
        private bool _autoShowFilterPanel = true;
        [Browsable(true), Category("Zek"), Description("Auto shot filter panel when Lis."), DefaultValue(true)]
        public bool AutoShowFilterPanel
        {
            get { return _autoShowFilterPanel; }
            set
            {
                if (_autoShowFilterPanel == value) return;
                var oldValue = _autoShowFilterPanel;
                _autoShowFilterPanel = value;
                OnChanged(new BaseOptionChangedEventArgs("AutoShowFilterPanel", oldValue, value));
            }
        }

        
        private ListFormStyle _listFormStyle = ListFormStyle.Default;
        [Browsable(true), Category("Zek"), Description("List form style."), DefaultValue(ListFormStyle.Default)]
        public ListFormStyle ListFormStyle
        {
            get { return _listFormStyle; }
            set
            {
                if (_listFormStyle == value) return;
                var oldValue = _listFormStyle;
                _listFormStyle = value;
                OnChanged(new BaseOptionChangedEventArgs("ListFormStyle", oldValue, value));
            }
        }


        private bool _closeAfterChoose = true;
        [Browsable(true), Category("Zek"), Description("ცხრილში ჩანაწერის არჩევის შემდეგ ფორმა დაიხუროს თუ არა."), DefaultValue(true)]
        public bool CloseAfterChoose
        {
            get { return _closeAfterChoose; }
            set
            {
                if (_closeAfterChoose == value) return;
                var oldValue = _closeAfterChoose;
                _closeAfterChoose = value;
                OnChanged(new BaseOptionChangedEventArgs("CloseAfterChoose", oldValue, value));
            }
        }
        
        private EditFormStyle _editFormStyle = EditFormStyle.Default;
        [Browsable(true), Category("Zek"), Description("ამით ეთითება, თუ როგორი ფორმა გაიხსნას დიალოგური თუ სტანდარტული."), DefaultValue(EditFormStyle.Default)]
        public EditFormStyle EditFormStyle
        {
            get { return _editFormStyle; }
            set
            {
                if (_editFormStyle == value) return;
                var oldValue = _editFormStyle;
                _editFormStyle = value;
                OnChanged(new BaseOptionChangedEventArgs("EditFormStyle", oldValue, value));
            }
        }


        private bool _isAddable = true;
        [Browsable(true), Category("Zek"), Description("ცხრილში ჩანაწერის დამატება შეიძლება თუ არა."), DefaultValue(true)]
        public bool IsAddable
        {
            get { return _isAddable; }
            set
            {
                if (_isAddable == value) return;
                var oldValue = _isAddable;
                _isAddable = value;
                OnChanged(new BaseOptionChangedEventArgs("IsAddable", oldValue, value));
            }
        }


        private bool _isApprovable = true;
        [Browsable(true), Category("Zek"), Description("Form is approvable (show/hide approve button)."), DefaultValue(true)]
        public bool IsApprovable
        {
            get { return _isApprovable; }
            set
            {
                if (_isApprovable == value) return;
                var oldValue = _isApprovable;
                _isApprovable = value;
                OnChanged(new BaseOptionChangedEventArgs("IsApprovable", oldValue, value));
            }
        }


        private bool _isBesftFitable = true;
        [Browsable(true), Category("Zek"), Description("საუკეთესო ზომის კნოპკის ჩართვა/გამორთვა."), DefaultValue(true)]
        public bool IsBesftFitable
        {
            get { return _isBesftFitable; }
            set
            {
                if (_isBesftFitable == value) return;
                var oldValue = _isBesftFitable;
                _isBesftFitable = value;
                OnChanged(new BaseOptionChangedEventArgs("IsBesftFitable", oldValue, value));
            }
        }



        private bool _isBlockable = true;
        [Browsable(true), Category("Zek"), Description("ცხრილში ჩანაწერის დაბლოკვა შეიძლება თუ არა."), DefaultValue(true)]
        public bool IsBlockable
        {
            get { return _isBlockable; }
            set
            {
                if (_isBlockable == value) return;
                var oldValue = _isBlockable;
                _isBlockable = value;
                OnChanged(new BaseOptionChangedEventArgs("IsBlockable", oldValue, value));
            }
        }


        private bool _isChooseable = true;
        [Browsable(true), Category("Zek"), Description("ცხრილში ჩანაწერის არჩევა შეიძლება თუ არა."), DefaultValue(true)]
        public bool IsChooseable
        {
            get { return _isChooseable; }
            set
            {
                if (_isChooseable == value) return;
                var oldValue = _isChooseable;
                _isChooseable = value;
                OnChanged(new BaseOptionChangedEventArgs("IsChooseable", oldValue, value));
            }
        }


        private bool _isDeletable = true;
        [Browsable(true), Category("Zek"), Description("ცხრილში ჩანაწერის წაშლა შეიძლება თუ არა."), DefaultValue(true)]
        public bool IsDeletable
        {
            get { return _isDeletable; }
            set
            {
                if (_isDeletable == value) return;
                var oldValue = _isDeletable;
                _isDeletable = value;
                OnChanged(new BaseOptionChangedEventArgs("IsDeletable", oldValue, value));
            }
        }


        private bool _dsDisapprovable = true;
        [Browsable(true), Category("Zek"), Description("ცხრილში ჩანაწერის დამოწმების მოხსნა შეიძლება თუ არა."), DefaultValue(true)]
        public bool IsDisapprovable
        {
            get { return _dsDisapprovable; }
            set
            {
                if (_dsDisapprovable == value) return;
                var oldValue = _dsDisapprovable;
                _dsDisapprovable = value;
                OnChanged(new BaseOptionChangedEventArgs("IsDisapprovable", oldValue, value));
            }
        }


        private bool _isEditable = true;
        [Browsable(true), Category("Zek"), Description("ცხრილში ჩანაწერის შეცვლა შეიძლება თუ არა."), DefaultValue(true)]
        public bool IsEditable
        {
            get { return _isEditable; }
            set
            {
                if (_isEditable == value) return;
                var oldValue = _isEditable;
                _isEditable = value;
                OnChanged(new BaseOptionChangedEventArgs("IsEditable", oldValue, value));
            }
        }


        private bool _isExportable = true;
        [Browsable(true), Category("Zek"), Description("ცხრილის ექსპორტირება შეიძლება თუ არა."), DefaultValue(true)]
        public bool IsExportable
        {
            get { return _isExportable; }
            set
            {
                if (_isExportable == value) return;
                var oldValue = _isExportable;
                _isExportable = value;
                OnChanged(new BaseOptionChangedEventArgs("IsExportable", oldValue, value));
            }
        }

        private bool _isCopyable = true;
        [Browsable(true), Category("Zek"), Description("ცხრილის კოპირება შეიძლება თუ არა."), DefaultValue(true)]
        public bool IsCopyable
        {
            get { return _isCopyable; }
            set
            {
                if (_isCopyable == value) return;
                var oldValue = _isCopyable;
                _isCopyable = value;
                OnChanged(new BaseOptionChangedEventArgs("IsCopyable", oldValue, value));
            }
        }


        private bool _isFilterable = true;
        [Browsable(true), Category("Zek"), Description("ფილრრის ღილაკის ჩართა/გამორთვა."), DefaultValue(true)]
        public bool IsFilterable
        {
            get { return _isFilterable; }
            set
            {
                if (_isFilterable == value) return;
                var oldValue = _isFilterable;
                _isFilterable = value;
                OnChanged(new BaseOptionChangedEventArgs("IsFilterable", oldValue, value));
            }
        }

        private bool _isFilterApprovable = true;
        [Browsable(true), Category("Zek"), Description("დამოწმებული/დაუმოწმებული ღილაკის ჩართა/გამორთვა."), DefaultValue(true)]
        public bool IsFilterApprovable
        {
            get { return _isFilterApprovable; }
            set
            {
                if (_isFilterApprovable == value) return;
                var oldValue = _isFilterApprovable;
                _isFilterApprovable = value;
                OnChanged(new BaseOptionChangedEventArgs("IsFilterApprovable", oldValue, value));
            }
        }


        private bool _isRefreshable = true;
        [Browsable(true), Category("Zek"), Description("ცხრილის სიის განახლება შეიძლება თუ არა."), DefaultValue(true)]
        public bool IsRefreshable
        {
            get { return _isRefreshable; }
            set
            {
                if (_isRefreshable == value) return;
                var oldValue = _isRefreshable;
                _isRefreshable = value;
                OnChanged(new BaseOptionChangedEventArgs("IsRefreshable", oldValue, value));
            }
        }

        private bool _isSumable = true;
        [Browsable(true), Category("Zek"), Description("Sum grid button show/hide."), DefaultValue(true)]
        public bool IsSumable
        {
            get { return _isSumable; }
            set
            {
                if (_isSumable == value) return;
                var oldValue = _isSumable;
                _isSumable = value;
                OnChanged(new BaseOptionChangedEventArgs("IsSumable", oldValue, value));
            }
        }

        private bool _isSystemColumnsable = true;
        [Browsable(true), Category("Zek"), Description("სისტემური ველების კნოპკის დამალვა გამოჩენა."), DefaultValue(true)]
        public bool IsSystemColumnsable
        {
            get { return _isSystemColumnsable; }
            set
            {
                if (_isSystemColumnsable == value) return;
                var oldValue = _isSystemColumnsable;
                _isSystemColumnsable = value;
                OnChanged(new BaseOptionChangedEventArgs("IsSystemColumnsable", oldValue, value));
            }
        }


        private bool _isTopable = true;
        [Browsable(true), Category("Zek"), Description("ცხრილში ჩანაწერების ლიმიტის დაწესება/გაუქმება."), DefaultValue(true)]
        public bool IsTopable
        {
            get { return _isTopable; }
            set
            {
                if (_isTopable == value) return;
                var oldValue = _isTopable;
                _isTopable = value;
                OnChanged(new BaseOptionChangedEventArgs("IsTopable", oldValue, value));
            }
        }


        private bool _isUnblockable = true;
        [Browsable(true), Category("Zek"), Description("ცხრილში ჩანაწერის დაბლოკვა დაბლოკვის მოხსნა შეიძლება თუ არა."), DefaultValue(true)]
        public bool IsUnblockable
        {
            get { return _isUnblockable; }
            set
            {
                if (_isUnblockable == value) return;
                var oldValue = _isUnblockable;
                _isUnblockable = value;
                OnChanged(new BaseOptionChangedEventArgs("IsUnblockable", oldValue, value));
            }
        }


        public override void Assign(BaseOptions options)
        {
            BeginUpdate();
            try
            {
                base.Assign(options);
                var op = options as ListFormOptions;
                if (op != null)
                {
                    _autoRefresh = op.AutoRefresh;
                    _autoRefreshOnLoad = op.AutoRefreshOnLoad;
                    _autoShowFilterPanel = op.AutoShowFilterPanel;
                    _closeAfterChoose = op.CloseAfterChoose;
                    _editFormStyle = op.EditFormStyle;
                    _isAddable = op.IsAddable;
                    _isApprovable = op.IsApprovable;
                    _isBesftFitable = op.IsBesftFitable;
                    _isBlockable = op.IsBlockable;
                    _isChooseable = op.IsChooseable;
                    _isDeletable = op.IsDeletable;
                    _dsDisapprovable = op.IsDisapprovable;
                    _isEditable = op.IsEditable;
                    _isExportable = op.IsExportable;
                    _isCopyable = op.IsCopyable;
                    _isFilterable = op.IsFilterable;
                    _isFilterApprovable = op.IsFilterApprovable;
                    _isRefreshable = op.IsRefreshable;
                    _isSumable = op.IsSumable;
                    _isSystemColumnsable = op.IsSystemColumnsable;
                    _isTopable = op.IsTopable;
                    _isUnblockable = op.IsUnblockable;

                    _listFormStyle = op.ListFormStyle;
                }
            }
            finally
            {
                EndUpdate();
            }
        }
    }
}
