using System.Windows.Forms;
using System.ComponentModel;

namespace Zek.Windows.Forms
{
    public class BaseFormOptions : BaseOptions
    {

        private bool _autoInitText = true;
        [Browsable(true), Category("Zek"), Description("ავტომატურაც ცვლის ფორმის სათაურს."), DefaultValue(true)]
        public bool AutoInitText
        {
            get { return _autoInitText; }
            set
            {
                if (_autoInitText == value) return;
                var oldValue = _autoInitText;
                _autoInitText = value;
                OnChanged(new BaseOptionChangedEventArgs("AutoInitText", oldValue, value));
            }
        }


        private FormWindowState _autoWindowState = FormWindowState.Normal;
        /// <summary>
        /// ავტომატურად ცვლის Page Load-ის დროს WindowState-ს.
        /// </summary>
        [Browsable(true),
        Category("Zek"),
        Description("ავტომატურად ცვლის Page Load-ის დროს WindowState-ს."),
        DefaultValue(FormWindowState.Normal)]
        public FormWindowState AutoWindowState
        {
            get { return _autoWindowState; }
            set
            {
                if (_autoWindowState == value) return;
                var oldValue = _autoWindowState;
                _autoWindowState = value;
                OnChanged(new BaseOptionChangedEventArgs("AutoWindowState", oldValue, value));
            }
        }


        //private bool _AutoChangeFormID = true;
        //[Browsable(true),
        //Category("Zek"),
        //Description("ავტომატურად ცვლის Page Load-ის დროს FormID-ს."),
        //DefaultValue(true)]
        //public bool AutoChangeFormID
        //{
        //    get { return _AutoChangeFormID; }
        //    set
        //    {
        //        if (_AutoChangeFormID == value) return;
        //        bool oldValue = _AutoChangeFormID;
        //        _AutoChangeFormID = value;
        //        OnChanged(new BaseOptionChangedEventArgs("AutoChangeFormID", oldValue, value));
        //    }
        //}


        //private string _FormID;
        ///// <summary>
        ///// ფორმის იდენტიფიკატორი. გამოიყენება ორჯერ რომ არ ჩაირთოს ერთი და იგივე ფორმა.
        ///// </summary>
        //[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public string FormID
        //{
        //    get { return _FormID; }
        //    set
        //    {
        //        if (Compare(_FormID, value)) return;
        //        string oldValue = _FormID;
        //        _FormID = value;
        //        OnChanged(new BaseOptionChangedEventArgs("FormID", oldValue, value));
        //    }
        //}


        //private string _Text;
        ///// <summary>
        ///// ფორმის იდენტიფიკატორი. გამოიყენება ორჯერ რომ არ ჩაირთოს ერთი და იგივე ფორმა.
        ///// </summary>
        //[Browsable(true),
        //Category("Zek"),
        //Description("ფორმის სტატიკური ტექსტი."),
        //DefaultValue(null)]
        //public string Text
        //{
        //    get { return _Text; }
        //    set
        //    {
        //        if (Compare(_Text, value)) return;
        //        string oldValue = _Text;
        //        _Text = value;
        //        OnChanged(new BaseOptionChangedEventArgs("Text", oldValue, value));
        //    }
        //}



        //private IntPtr _SenderHandle = IntPtr.Zero;
        ///// <summary>
        ///// ფორმის ჩამრთველის Handle (ანუ რომელმა კონტროლმა ან ფორმამ გამოიძახა იმის Handle).
        ///// </summary>
        //[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public IntPtr SenderHandle
        //{
        //    get { return _SenderHandle; }
        //    set
        //    {
        //        if (_SenderHandle == value) return;
        //        IntPtr oldValue = _SenderHandle;
        //        _SenderHandle = value;
        //        OnChanged(new BaseOptionChangedEventArgs("SenderHandle", oldValue, value));
        //    }
        //}


        //private FormStyle _FormStyle = FormStyle.Edit;
        ///// <summary>
        ///// ფორმის სტილი. გამოიყენება იმისთვის რომ მივხვდეთ როგორი ფორმმა (Browse, Edit...).
        ///// </summary>
        //[Browsable(true),
        //Category("Zek"),
        //Description("ფორმის სტილი."),
        //DefaultValue(FormStyle.Edit)]
        //public FormStyle FormStyle
        //{
        //    get { return _FormStyle; }
        //    set
        //    {
        //        if (_FormStyle == value) return;
        //        FormStyle oldValue = _FormStyle;
        //        _FormStyle = value;
        //        OnChanged(new BaseOptionChangedEventArgs("FormStyle", oldValue, value));
        //    }
        //}


        private bool _isClosing;
        /// <summary>
        /// როცა ფორმა იხურება მაშინ ენიჭება true.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsClosing
        {
            get { return _isClosing; }
            set
            {
                if (_isClosing == value) return;
                var oldValue = _isClosing;
                _isClosing = value;
                OnChanged(new BaseOptionChangedEventArgs("IsClosing", oldValue, value));
            }
        }


        private bool _isLoading = true;
        /// <summary>
        /// როცა ფორმა იტვირთება, მაშინ არის true.
        /// როცა ჩაიტვირთება, მაშინ გახდება false.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (_isLoading == value) return;
                var oldValue = _isLoading;
                _isLoading = value;
                OnChanged(new BaseOptionChangedEventArgs("IsLoading", oldValue, value));
            }
        }


        
        //private bool _IsRefreshableFormControls = true;
        //[Browsable(true),
        //Category("Zek"),
        //Description("Control+F5 ავტომატურად შეავსებს კონტროლებს."),
        //DefaultValue(true)]
        //public bool IsRefreshableFormControls
        //{
        //    get { return _IsRefreshableFormControls; }
        //    set
        //    {
        //        if (_IsRefreshableFormControls == value) return;
        //        bool oldValue = _IsRefreshableFormControls;
        //        _IsRefreshableFormControls = value;
        //        OnChanged(new BaseOptionChangedEventArgs("IsRefreshableFormControls", oldValue, value));
        //    }
        //}


        private bool _isPrintable;
        [Category("Zek"), DefaultValue(false)]
        public virtual bool IsPrintable
        {
            get { return _isPrintable; }
            set
            {
                if (_isPrintable == value) return;
                var oldValue = _isPrintable;
                _isPrintable = value;
                OnChanged(new BaseOptionChangedEventArgs("IsPrintable", oldValue, value));
            }
        }

        private bool _isValidable;
        [Category("Zek"), DefaultValue(false)]
        public virtual bool IsValidable
        {
            get { return _isValidable; }
            set
            {
                if (_isValidable == value) return;
                var oldValue = _isValidable;
                _isValidable = value;
                OnChanged(new BaseOptionChangedEventArgs("IsValidable", oldValue, value));
            }
        }


        private bool _readOnly;
        [Browsable(true),
        Category("Zek"),
        Description("ფორმის დაბლოკვა/განბლოკვა."),
        DefaultValue(false)]
        public virtual bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                if (_readOnly == value) return;
                var oldValue = _readOnly;
                _readOnly = value;
                OnChanged(new BaseOptionChangedEventArgs("ReadOnly", oldValue, value));
            }
        }


        //private object _RecordID;
        //[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public object RecordID
        //{
        //    get { return _RecordID; }
        //    set
        //    {
        //        if (Compare(_RecordID, value)) return;

        //        object oldValue = _RecordID;
        //        _RecordID = value;
        //        OnChanged(new BaseOptionChangedEventArgs("RecordID", oldValue, value));
        //    }
        //}
        //internal void ChangeFormID(Type formType)
        //{
        //    if (AutoChangeFormID)
        //        FormID = FormHelper.GetFormID(formType, SenderHandle, RecordID);
        //}


        //[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public int BindingFormControlsCount { get; internal set; }
        //[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public int BindingDataCount { get; internal set; }

        public override void Assign(BaseOptions options)
        {
            BeginUpdate();
            try
            {
                base.Assign(options);
                var op = options as BaseFormOptions;
                if (op != null)
                {
                    _autoInitText = op.AutoInitText;
                    _autoWindowState = op.AutoWindowState;
                    //_AutoChangeFormID = opts.AutoChangeFormID;
                    //_FormID = opts.FormID;
                    //_Text = opts.Text;
                    //_SenderHandle = opts.SenderHandle;
                    //_FormStyle = opts.FormStyle;
                    _isClosing = op.IsClosing;
                    _isLoading = op.IsLoading;
                    //_IsRefreshableFormControls = opts.IsRefreshableFormControls;
                    _isPrintable = op.IsPrintable;
                    _isValidable = op.IsValidable;
                    _readOnly = op.ReadOnly;
                    //_RecordID = opts.RecordID;
                }
            }
            finally
            {
                EndUpdate();
            }
        }


    }
}
