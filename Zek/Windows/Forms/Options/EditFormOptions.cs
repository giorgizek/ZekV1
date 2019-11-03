using System.ComponentModel;

namespace Zek.Windows.Forms
{
    public class EditFormOptions : BaseOptions
    {
        private bool _promptSave = true;
        [Browsable(true), Category("Zek"), Description("ფორმის დახურვისას შეგეკითხებათ დაიხუროს თუ არა ფორმა."), DefaultValue(true)]
        public bool PromptSave
        {
            get { return _promptSave; }
            set
            {
                if (PromptSave == value) return;
                var oldValue = _promptSave;
                _promptSave = value;
                OnChanged(new BaseOptionChangedEventArgs("PromptSave", oldValue, value));
            }
        }

        private bool _isChanged;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsChanged
        {
            get { return _isChanged; }
            set
            {
                if (_isChanged == value) return;
                var oldValue = _isChanged;
                _isChanged = value;
                OnChanged(new BaseOptionChangedEventArgs("IsChanged", oldValue, value));
            }
        }


        //private string _TextFormat = "{0} [{1}]{2}";
        //[Browsable(true), Category("Zek"), Description("ფორმის ტექსტის ავტომატურად შეცვლის ფორმატი."), DefaultValue("{0} [{1}]{2}")]
        //public string TextFormat
        //{
        //    get { return _TextFormat; }
        //    set
        //    {
        //        if (value == null) value = string.Empty;

        //        if (Compare(_TextFormat, value)) return;
        //        string oldValue = _TextFormat;
        //        _TextFormat = value;
        //        OnChanged(new BaseOptionChangedEventArgs("TextFormat", oldValue, value));
        //    }
        //}


        //private Zek.Data.DatabaseStatus _Status = Zek.Data.DatabaseStatus.Pending;
        //[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public Zek.Data.DatabaseStatus Status
        //{
        //    get { return _Status; }
        //    set
        //    {
        //        if (_Status == value) return;
        //        Zek.Data.DatabaseStatus oldValue = _Status;
        //        _Status = value;
        //        OnChanged(new BaseOptionChangedEventArgs("Status", oldValue, value));
        //    }
        //}


        //private Zek.Data.DatabaseAction _Action = Zek.Data.DatabaseAction.Add;
        //[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public Zek.Data.DatabaseAction Action
        //{
        //    get { return _Action; }
        //    set
        //    {
        //        if (_Action == value) return;
        //        Zek.Data.DatabaseAction oldValue = _Action;
        //        _Action = value;
        //        OnChanged(new BaseOptionChangedEventArgs("Action", oldValue, value));
        //    }
        //}

        //private bool _AutoUpdateAction = true;
        //[Browsable(true), Category("Zek"), Description("ავტომატურად ცვლის Action-ს."), DefaultValue(true)]
        //public bool AutoUpdateAction
        //{
        //    get { return _AutoUpdateAction; }
        //    set
        //    {
        //        if (_AutoUpdateAction == value) return;
        //        bool oldValue = _AutoUpdateAction;
        //        _AutoUpdateAction = value;
        //        OnChanged(new BaseOptionChangedEventArgs("AutoUpdateAction", oldValue, value));
        //    }
        //}


        //private bool _IsSaveAndCloseExecuting = false;
        //[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public bool IsSaveAndCloseExecuting
        //{
        //    get { return _IsSaveAndCloseExecuting; }
        //    set
        //    {
        //        if (_IsSaveAndCloseExecuting == value) return;
        //        bool oldValue = _IsSaveAndCloseExecuting;
        //        _IsSaveAndCloseExecuting = value;
        //        OnChanged(new BaseOptionChangedEventArgs("IsSaveAndCloseExecuting", oldValue, value));
        //    }
        //}


        //[Browsable(false)]
        //public string ActionText
        //{
        //    get
        //    {
        //        switch (Action)
        //        {
        //            case Zek.Data.DatabaseAction.Add:
        //                return Resources.Add;

        //            case Zek.Data.DatabaseAction.Edit:
        //                return Resources.Edit;

        //            case Zek.Data.DatabaseAction.Delete:
        //                return Resources.Delete;
        //        }
        //        return string.Empty;
        //    }
        //}


        //internal void OnRecordIDChanged(object recordID)
        //{
        //    if (recordID != null && Action == Zek.Data.DatabaseAction.Add)
        //    {
        //        if (recordID is byte && (byte)recordID != 0)
        //            Action = Zek.Data.DatabaseAction.Edit;
        //        else if (recordID is short && (short)recordID != 0)
        //            Action = Zek.Data.DatabaseAction.Edit;
        //        else if (recordID is int && (int)recordID != 0)
        //            Action = Zek.Data.DatabaseAction.Edit;
        //        else if (recordID is long && (long)recordID != 0)
        //            Action = Zek.Data.DatabaseAction.Edit;
        //        else if (recordID is string && !string.IsNullOrEmpty((string)recordID))
        //            Action = Zek.Data.DatabaseAction.Edit;
        //        else if (recordID is Guid && (Guid)recordID != Guid.Empty)
        //            Action = Zek.Data.DatabaseAction.Edit;
        //        else if (recordID is DateTime)
        //            Action = Zek.Data.DatabaseAction.Edit;
        //    }
        //}


        public override void Assign(BaseOptions options)
        {
            BeginUpdate();
            try
            {
                base.Assign(options);
                var op = options as EditFormOptions;
                if (op != null)
                {
                    _promptSave = op.PromptSave;
                    //_TextFormat = opts.TextFormat;
                    _isChanged = op.IsChanged;
                    //_Status = opts.Status;
                    //_Action = opts.Action;
                    //_AutoUpdateAction = opts.AutoUpdateAction;
                }
            }
            finally
            {
                EndUpdate();
            }
        }
    }
}
