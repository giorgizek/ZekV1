using System.ComponentModel;

namespace Zek.Windows.Forms
{
    public class ButtonBrowseOptions : BaseOptions
    {

        private bool _autoParsePrimaryKey = true;
        /// <summary>
        /// ავტომატურად გაპარსავს მნიშვნელობას (მაგ: თუ მივცემთ 0-ს ან ""-ს ან Guid.Empty-ს მაშინ ჩასვამს null-ს.
        /// </summary>
        [Browsable(true),
        Category("Zek"),
        Description("ავტომატურად გაპარსავს მნიშვნელობას (მაგ: თუ მივცემთ 0-ს ან \"\"-ს ან Guid.Empty-ს მაშინ ჩასვამს null-ს."),
        DefaultValue(true)]
        public bool AutoParsePrimaryKey
        {
            get { return _autoParsePrimaryKey; }
            set
            {
                if (_autoParsePrimaryKey == value) return;
                var oldValue = _autoParsePrimaryKey;
                _autoParsePrimaryKey = value;
                OnChanged(new BaseOptionChangedEventArgs("AutoParsePrimaryKey", oldValue, value));
            }
        }

        private bool _allowNullInput = true;
        /// <summary>
        /// Shift+Delete -ით  წაიშალოს თუ არა ჩანაწერი.
        /// </summary>
        [Browsable(true),
        Category("Zek"),
        Description("Shift+Delete -ით  წაიშალოს თუ არა ჩანაწერი."),
        DefaultValue(true)]
        public bool AllowNullInput
        {
            get { return _allowNullInput; }
            set
            {
                if (_allowNullInput == value) return;
                var oldValue = _allowNullInput;
                _allowNullInput = value;
                OnChanged(new BaseOptionChangedEventArgs("AllowNullInput", oldValue, value));
            }
        }

        //private bool _ServerMode = false;
        //[Browsable(true),
        //Category("Zek"),
        //DefaultValue(false)]
        //public bool ServerMode
        //{
        //    get { return _ServerMode; }
        //    set
        //    {
        //        if (_ServerMode == value) return;
        //        bool oldValue = _ServerMode;
        //        _ServerMode = value;
        //        OnChanged(new BaseOptionChangedEventArgs("ServerMode", oldValue, value));
        //    }
        //}

        private bool _allowBrowse = true;
        /// <summary>
        /// მივცეთ თუ არა ნება ახალი ფორმის გახნის.
        /// </summary>
        [Browsable(true),
        Category("Zek"),
        Description("მივცეთ თუ არა ნება ახალი ფორმის გახნის."),
        DefaultValue(true)]
        public bool AllowBrowse
        {
            get { return _allowBrowse; }
            set
            {
                if (_allowBrowse == value) return;
                var oldValue = _allowBrowse;
                _allowBrowse = value;
                OnChanged(new BaseOptionChangedEventArgs("AllowBrowse", oldValue, value));
            }
        }

        //private string _selectStoredProcedureName = "dbo.SP_GetButtonBrowse";
        ///// <summary>
        ///// მონაცემის ამოღების StoredProcedure-ის სახელი.
        ///// </summary>
        //[Browsable(true),
        //Category("Zek"),
        //Description("მონაცემის ამოღების StoredProcedure-ის სახელი."),
        //DefaultValue("dbo.SP_GetButtonBrowse")]
        //public string SelectStoredProcedureName
        //{
        //    get { return _selectStoredProcedureName; }
        //    set
        //    {
        //        if (Compare(_selectStoredProcedureName, value)) return;
        //        string oldValue = _selectStoredProcedureName;
        //        _selectStoredProcedureName = value;
        //        OnChanged(new BaseOptionChangedEventArgs("SelectStoredProcedureName", oldValue, value));
        //    }
        //}



        public override void Assign(BaseOptions options)
        {
            BeginUpdate();
            try
            {
                base.Assign(options);
                var op = options as ButtonBrowseOptions;
                if (op != null)
                {
                    _autoParsePrimaryKey = op.AutoParsePrimaryKey;
                    _allowNullInput = op.AllowNullInput;
                    //_ServerMode = opts.ServerMode;
                    _allowBrowse = op.AllowBrowse;
                    //_selectStoredProcedureName = opts.SelectStoredProcedureName;
                }
            }
            finally
            {
                EndUpdate();
            }
        }

        //private bool CompareRecordValue(object value)
        //{
        //    return ((_RecordValue == value) || CompareRecordValue(_RecordValue, value, AutoParseRecordValue));
        //}
        //private static bool CompareRecordValue(object val1, object val2, bool parse)
        //{
        //    if (parse)
        //    {
        //        val2 = DoParseRecordValue(val2);
        //    }
        //    return ((((val1 != null) && (val2 != null)) && val1.Equals(val2)) || (val1 == val2));
        //}
        //private static object DoParseRecordValue(object value)
        //{
        //    if (value == null || value == DBNull.Value)
        //        return null;
        //    else if (value is int && (int)value == 0)
        //        return null;
        //    else if (value is string && string.IsNullOrEmpty(((string)value).Trim()))
        //        return null;
        //    else if (value is Guid && (Guid)value == Guid.Empty)
        //        return null;
        //    else if (value is DateTime && (DateTime)value == Zek.Common.DateTimeHelper.MinDate)
        //        return null;

        //    return value;
        //}
    }
}
