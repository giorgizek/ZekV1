using System;
using System.Web.UI;

/*using System;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Zek.Web;
using Zek.Common;
using System.ComponentModel;
using System.Reflection;
using Zek.Configuration;*/

namespace Zek.Web.UI
{
    //internal partial  class _BasePage : System.Web.UI.Page
    //{
    //    public _BasePage()
    //        : base()
    //    {
    //        //PreInit += new EventHandler(BasePage_PreInit);
    //        //Load += new EventHandler(BasePage_Load);
    //        LoadComplete += new EventHandler(BasePage_LoadComplete);
    //    }
    //    //~BasePage()
    //    //{
    //    //}

    //    protected override void OnInit(EventArgs e)
    //    {
    //        base.OnInit(e);

    //        //DoCheckLoggedIn();
    //    }


    //    #region Fields
    //    private static object sync = new object();

    //    //private string _httpHomeUrl;
    //    /// <summary>
    //    /// მთავარი გვერდის ლინკი.
    //    /// </summary>
    //    [Browsable(false)]
    //    protected virtual string HttpHomeUrl
    //    {
    //        get
    //        {
    //            return BaseWebConfig.HttpHomeUrl;
    //            //if (_httpHomeUrl == null)
    //            //{
    //            //    lock (sync)
    //            //    {
    //            //        if (_httpHomeUrl == null)
    //            //            _httpHomeUrl = HttpHelper.GetBaseURL();
    //            //    }
    //            //}
    //            //return _httpHomeUrl;
    //        }
    //    }

    //    //private string _currentPageUrl;
    //    /// <summary>
    //    /// მიმდინარე გვერდის ლინკი.
    //    /// </summary>
    //    [Browsable(false)]
    //    protected virtual string CurrentPageUrl
    //    {
    //        get
    //        {
    //            return HttpHelper.GetCurrentPageUrl(HttpHomeUrl);
    //            //if (_currentPageUrl == null)
    //            //{
    //            //    string baseUrl = HttpHomeUrl;
    //            //    lock (sync)
    //            //    {
    //            //        if (_currentPageUrl == null)
    //            //_currentPageUrl = HttpHelper.GetCurrentPageUrl(baseUrl);
    //            //    }
    //            //}
    //            //return _currentPageUrl;
    //        }
    //    }

    //    //private string _themeUrl;
    //    /// <summary>
    //    /// თემის ლინკი.
    //    /// </summary>
    //    [Browsable(false)]
    //    protected virtual string ThemeUrl
    //    {
    //        get
    //        {
    //            return HttpHelper.GetThemeUrl(HttpHomeUrl, Theme);
    //            //if (_themeUrl == null)
    //            //{
    //            //    string baseUrl = HttpHomeUrl;
    //            //    lock (sync)
    //            //    {
    //            //        if (_themeUrl == null)
    //            //            _themeUrl = HttpHelper.GetThemeUrl(baseUrl, Theme);
    //            //    }
    //            //}

    //            //return _themeUrl;
    //        }
    //    }



    //    //private BasePageOptions _optionsBasePage;
    //    //[Category("Zek")]
    //    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    //    //[PersistenceMode(PersistenceMode.InnerProperty)]
    //    //public BasePageOptions OptionsBasePage
    //    //{
    //    //    get
    //    //    {
    //    //        if (_optionsBasePage == null)
    //    //        {
    //    //            _optionsBasePage = new BasePageOptions();
    //    //            if (IsTrackingViewState)
    //    //            {
    //    //                ((IStateManager)_optionsBasePage).TrackViewState();
    //    //            }
    //    //        }
    //    //        return _optionsBasePage;
    //    //    }
    //    //    set{ _optionsBasePage = value; }
    //    //}


    //    private string _metaKeywords;
    //    /// <summary>
    //    /// <meta name="keywords" content="ააა, ბბბ, გგგ" /> 
    //    /// </summary>
    //    public virtual string MetaKeywords
    //    {
    //        get { return _metaKeywords; }
    //        set
    //        {
    //            if (value == null) value = string.Empty;
    //            _metaKeywords = value.CollapseSpaces();
    //        }
    //    }

    //    private string _metaDescription;
    //    /// <summary>
    //    /// <meta name="description" content="გვერდის მოკლე არწერილობა." />
    //    /// </summary>
    //    public virtual string MetaDescription
    //    {
    //        get { return _metaDescription; }
    //        set
    //        {
    //            if (value == null) value = string.Empty;
    //            _metaDescription = value.CollapseSpaces();
    //        }
    //    }


    //    //private object _RecordID = null;
    //    //public object RecordID
    //    //{
    //    //    get { return _RecordID; }
    //    //    set
    //    //    {
    //    //        if (!ConvertHelper.Compare(value, _RecordID))
    //    //        {
    //    //            _RecordID = value;
    //    //            OnRecordIDChanged();
    //    //        }
    //    //    }
    //    //}
    //    //public virtual void OnRecordIDChanged()
    //    //{
    //    //    //if (RecordID != null)
    //    //    //    Action = Zek.Data.DatabaseAction.Edit;
    //    //}


    //    //private int _ObjectID;
    //    ///// <summary>
    //    ///// ობიექტის იდენტიფიკატორი (გამოიყენება მონაცემთა ბაზიდან უფლებების ამოსაღებად).
    //    ///// </summary>
    //    //[Browsable(false), Category("Zek"), DefaultValue(0)]
    //    //public int ObjectID
    //    //{
    //    //    get { return _ObjectID; }
    //    //    set
    //    //    {
    //    //        if (value != _ObjectID)
    //    //        {
    //    //            _ObjectID = value;
    //    //            OnObjectIDChanged();
    //    //        }
    //    //    }
    //    //}
    //    //public virtual void OnObjectIDChanged() { }


    //    //private Zek.Data.DatabaseStatus _Status = Zek.Data.DatabaseStatus.Pending;
    //    ///// <summary>
    //    ///// გვერდის სტატუსი.
    //    ///// </summary>
    //    //public Zek.Data.DatabaseStatus Status
    //    //{
    //    //    get { return _Status; }
    //    //    set
    //    //    {
    //    //        if (value != Status)
    //    //        {
    //    //            _Status = value;
    //    //            OnStatusChanged();
    //    //        }
    //    //    }
    //    //}
    //    //public virtual void OnStatusChanged() { }

    //    ////private bool _IsClosing;
    //    /////// <summary>
    //    /////// როცა ფორმა იხურება მაშინ ენიჭება true.
    //    /////// </summary>
    //    ////[Browsable(false)]
    //    ////public bool IsClosing
    //    ////{
    //    ////    get { return _IsClosing; }
    //    ////    set
    //    ////    {
    //    ////        if (_IsClosing != value)
    //    ////        {
    //    ////            _IsClosing = value;
    //    ////            OnIsClosingChanged();
    //    ////        }
    //    ////    }
    //    ////}
    //    ////public virtual void OnIsClosingChanged() { }

    //    //private bool _IsLoading = true;
    //    ///// <summary>
    //    ///// როცა ფორმა იტვირთება, მაშინ არის true.
    //    ///// როცა ჩაიტვირთება, მაშინ გახდება false.
    //    ///// </summary>
    //    //[Browsable(false)]
    //    //public bool IsLoading
    //    //{
    //    //    get { return _IsLoading; }
    //    //    set
    //    //    {
    //    //        if (value != _IsLoading)
    //    //        {
    //    //            _IsLoading = value;
    //    //            OnIsLoadingChanged();
    //    //        }
    //    //    }
    //    //}
    //    //public virtual void OnIsLoadingChanged() { }

    //    //private bool _ReadOnly;
    //    //[Category("Zek"), DefaultValue(false)]
    //    //public virtual bool ReadOnly
    //    //{
    //    //    get { return _ReadOnly; }
    //    //    set
    //    //    {
    //    //        if (value != _ReadOnly)
    //    //        {
    //    //            _ReadOnly = value;
    //    //            OnReadOnlyChanged();
    //    //        }
    //    //    }
    //    //}
    //    //public virtual void OnReadOnlyChanged() { }



    //    //private bool _AutoPostBackDataOnPostBack = true;
    //    ///// <summary>
    //    ///// ავტომატურად გაეშვება DoPostBackData() თუ ხდება PostBack
    //    ///// </summary>
    //    //[Category("Zek"),
    //    //DefaultValue(true),
    //    //Description("ავტომატურად გაეშვება DoPostBackData() თუ ხდება PostBack.")]
    //    //public bool AutoPostBackDataOnPostBack
    //    //{
    //    //    get { return _AutoPostBackDataOnPostBack; }
    //    //    set { _AutoPostBackDataOnPostBack = value; }
    //    //}






    //    ///// <summary>
    //    ///// თუ მიმდინარე გვერდის გახნისას საჭიროა ავტორიზაციის გავლა მაშინ true=უნდა მივანიჭოთ.
    //    ///// </summary>
    //    //[Category("Zek"),
    //    //DefaultValue(false),
    //    //Description("თუ მიმდინარე გვერდის გახნისას საჭიროა ავტორიზაციის გავლა მაშინ true=უნდა მივანიჭოთ.")]
    //    //public bool IsSecuredPage
    //    //{
    //    //    get;
    //    //    set;
    //    //}


    //    //private bool _AutoInitPermission = true;
    //    ///// <summary>
    //    ///// ავტომატურად იღებს უფლებებს.
    //    ///// </summary>
    //    //[Category("Zek"), DefaultValue(true), Description("ავტომატურად იღებს უფლებებს.")]
    //    //public bool AutoInitPermission
    //    //{
    //    //    get { return _AutoInitPermission; }
    //    //    set
    //    //    {
    //    //        if (value != _AutoInitPermission)
    //    //        {
    //    //            _AutoInitPermission = value;
    //    //            OnAutoInitPermissionChanged();
    //    //        }
    //    //    }
    //    //}
    //    //public virtual void OnAutoInitPermissionChanged() { }


    //    //private bool _AutoCheckPermission = true;
    //    ///// <summary>
    //    ///// ავტომატურად ამოწმებს უფლებებს.
    //    ///// </summary>
    //    //[Category("Zek"), DefaultValue(true), Description("ავტომატურად ამოწმებს უფლებებს.")]
    //    //public bool AutoCheckPermission
    //    //{
    //    //    get { return _AutoCheckPermission; }
    //    //    set
    //    //    {
    //    //        if (value != _AutoCheckPermission)
    //    //        {
    //    //            _AutoCheckPermission = value;
    //    //            OnAutoCheckPermissionChanged();
    //    //        }
    //    //    }
    //    //}
    //    //public virtual void OnAutoCheckPermissionChanged() { }


    //    //private int _Permission;
    //    ///// <summary>
    //    ///// უფლებები.
    //    ///// </summary>
    //    //[Browsable(false)]
    //    //public int Permission
    //    //{
    //    //    get { return _Permission; }
    //    //    set
    //    //    {
    //    //        if (_Permission != value)
    //    //        {
    //    //            _Permission = value;
    //    //            OnPermissionChanged();
    //    //        }
    //    //    }
    //    //}
    //    //public virtual void OnPermissionChanged() { }


    //    ////[Browsable(false)]
    //    ////public int BindingFormControlsCount { get; private set; }
    //    ////[Browsable(false)]
    //    ////public int BindingDataCount { get; private set; }


    //    [Category("Zek"), DefaultValue("Zek_")]
    //    public virtual string Prefix
    //    {
    //        get { return BaseWebConfig.Prefix; }
    //    }
    //    //[Browsable(false)]
    //    //public virtual string AuthenticationCookieName
    //    //{
    //    //    get { return Prefix + "Authentication"; }
    //    //}


    //    //[Browsable(false)]
    //    //public virtual int SessionUserID
    //    //{
    //    //    get { return ConvertHelper.ToInt32(Session[Prefix + "UserID"]); }
    //    //    set { Session[Prefix + "UserID"] = value; }
    //    //}
    //    //[Browsable(false)]
    //    //public virtual string SessionUserName
    //    //{
    //    //    get { return ConvertHelper.ToString(Session[Prefix + "UserName"]); }
    //    //    set { Session[Prefix + "UserName"] = value; }
    //    //}
    //    //[Browsable(false)]
    //    //public virtual int SessionGroupID
    //    //{
    //    //    get { return ConvertHelper.ToInt32(Session[Prefix + "GroupID"]); }
    //    //    set { Session[Prefix + "GroupID"] = value; }
    //    //}
    //    //[Browsable(false)]
    //    //public virtual int SessionFailedPasswordAttemptCount
    //    //{
    //    //    get { return ConvertHelper.ToInt32(Session[Prefix + "FailedPasswordAttemptCount"]); }
    //    //    set { Session[Prefix + "FailedPasswordAttemptCount"] = value; }
    //    //}
    //    //[Browsable(false)]
    //    //public virtual string SessionPassword
    //    //{
    //    //    get { return ConvertHelper.ToString(Session[Prefix + "Password"]); }
    //    //    set { Session[Prefix + "Password"] = value; }
    //    //}
    //    //[Browsable(false)]
    //    //public virtual DateTime SessionLastActivityDate
    //    //{
    //    //    get { return Convert.ToDateTime(Session[Prefix + "LastActivityDate"]); }
    //    //    set { Session[Prefix + "LastActivityDate"] = value; }
    //    //}

    //    //[Browsable(false)]
    //    //public virtual bool IsAuthenticated
    //    //{
    //    //    get { return SessionUserID != 0; }
    //    //}

    //    //[Browsable(false)]
    //    //public virtual int CookieUserID
    //    //{
    //    //    get { return (Request.Cookies[AuthenticationCookieName] != null ? ConvertHelper.TryParseInt32(Request.Cookies[AuthenticationCookieName][Prefix + "UserID"]) : 0); }
    //    //    set
    //    //    {
    //    //        HttpCookie cookie = new HttpCookie(AuthenticationCookieName);
    //    //        cookie.Values.Add(Prefix + "UserID", value.ToString());
    //    //        cookie.Expires = DateTime.Now.AddYears(1);
    //    //        Response.Cookies.Add(cookie);
    //    //    }
    //    //}
    //    //[Browsable(false)]
    //    //public virtual string CookieUserName
    //    //{
    //    //    get { return (Request.Cookies[AuthenticationCookieName] != null ? ConvertHelper.ToString(Request.Cookies[AuthenticationCookieName][Prefix + "UserName"]) : string.Empty); }
    //    //    set
    //    //    {
    //    //        HttpCookie cookie = new HttpCookie(AuthenticationCookieName);
    //    //        cookie.Values.Add(Prefix + "UserName", value);
    //    //        cookie.Expires = DateTime.Now.AddYears(1);
    //    //        Response.Cookies.Add(cookie);
    //    //    }
    //    //}
    //    //[Browsable(false)]
    //    //public virtual string CookiePassword
    //    //{
    //    //    get { return (Request.Cookies[AuthenticationCookieName] != null ? ConvertHelper.ToString(Request.Cookies[AuthenticationCookieName][Prefix + "Password"]) : string.Empty); }
    //    //    set
    //    //    {
    //    //        HttpCookie cookie = new HttpCookie(AuthenticationCookieName);
    //    //        cookie.Values.Add(Prefix + "Password", value);
    //    //        cookie.Expires = DateTime.Now.AddYears(1);
    //    //        Response.Cookies.Add(cookie);
    //    //    }
    //    //}
    //    #endregion

    //    //#region Init
    //    ////private void BasePage_PreInit(object sender, System.EventArgs e)
    //    ////{
    //    ////    PageHelper.InitRequest(this);
    //    ////}
    //    //private void BasePage_Load(object sender, EventArgs e)
    //    //{
    //    //    AuthenticateRequest();

    //    //    if (IsSecuredPage)
    //    //        CheckAuthorization();

    //    //    RecordID = Request.QueryString["id"];


    //    //    DoInitPermission();
    //    //    DoChechPermission();

    //    //    if (IsPostBack)
    //    //    {
    //    //        IsLoading = false;
    //    //        if (AutoPostBackDataOnPostBack)
    //    //            DoPostBackData();
    //    //    }
    //    //    else
    //    //    {
    //    //        DoBindingFormControls();
    //    //        DoBindingData();
    //    //    }

    //    //    DoEnableDisableFormControls();
    //    //}
    //    private void BasePage_LoadComplete(object sender, EventArgs e)
    //    {
    //        if (!IsPostBack)
    //        {
    //            InitMeta();

    //            if (!string.IsNullOrEmpty(MetaKeywords))
    //                PageHelper.RegisterMeta(this, "keywords", MetaKeywords);

    //            if (!string.IsNullOrEmpty(MetaDescription))
    //                PageHelper.RegisterMeta(this, "description", MetaDescription);
    //        }

    //        //foreach (System.Collections.Generic.KeyValuePair<string, string> d in Meta)
    //        //{
    //        //    HtmlMeta tag = new HtmlMeta();
    //        //    tag.Attributes.Add("name", d.Key.ToString());
    //        //    tag.Attributes.Add("content", d.Value.ToString());
    //        //    Header.Controls.Add(tag);
    //        //}
    //    }
    //    /// <summary>
    //    /// ვატომატურად უკეთებს ინიციალიზაციას Title, Keywords, Description-ს კონფიგურაციიდან მნიშვნელობებს.
    //    /// </summary>
    //    protected virtual void InitMeta()
    //    {
    //        //if (!OptionsBasePage.AutoInitMeta) return;

    //        if (Title.Length == 0 && (Page.Header == null || string.IsNullOrEmpty(Page.Header.Title)))
    //            Title = HttpUtility.HtmlEncode(BaseWebConfig.HomeTitle);
    //        if (string.IsNullOrEmpty(MetaKeywords))
    //            MetaKeywords = HttpUtility.HtmlEncode(BaseWebConfig.Keywords);
    //        if (string.IsNullOrEmpty(MetaDescription))
    //            MetaDescription = HttpUtility.HtmlEncode(BaseWebConfig.Description);
    //    }
    //    //#endregion

    //    //#region Events
    //    ///// <summary>
    //    ///// სანამ დაიწყება ფორმის კონტროლების შევსება მაშინ გამოიძახება ეს ევენტი.
    //    ///// </summary>
    //    //[Category("Zek")]
    //    //public event EventHandler BeforeBindingFormControls;
    //    ///// <summary>
    //    ///// როცა მორჩება ფორმის კონტროლების შევსება მაშინ გამოიძახება ეს ევენტი.
    //    ///// </summary>
    //    //[Category("Zek")]
    //    //public event EventHandler AfterBindingFormControls;
    //    //#endregion

    //    //#region Bindings
    //    ///// <summary>
    //    ///// ავსებს კონტროლებს მონაცემებით.
    //    ///// მეთოდი იძახებს: OnBeforeBindingFormControls(), BindingData(), OnAfterBindingFormControls().
    //    ///// </summary>
    //    //public void DoBindingFormControls()
    //    //{
    //    //    try
    //    //    {
    //    //        OnBeforeBindingFormControls();
    //    //        BindingFormControls();
    //    //        //BindingFormControlsCount++;
    //    //        OnAfterBindingFormControls();
    //    //    }
    //    //    catch
    //    //    {
    //    //        throw;
    //    //    }
    //    //}
    //    //public virtual void OnBeforeBindingFormControls()
    //    //{
    //    //    if (BeforeBindingFormControls != null)
    //    //        BeforeBindingFormControls(this, EventArgs.Empty);
    //    //}
    //    //public virtual void BindingFormControls() { }
    //    //public virtual void OnAfterBindingFormControls()
    //    //{
    //    //    if (AfterBindingFormControls != null)
    //    //        AfterBindingFormControls(this, EventArgs.Empty);
    //    //}

    //    ///// <summary>
    //    ///// BaseForm.Load - ის დროს იძახებს ამ მეთოდს.
    //    ///// მეთოდი იძახებს: OnBeforeBindingData(), BindingData(), OnAfterBindingData().
    //    ///// </summary>
    //    //public void DoBindingData()
    //    //{
    //    //    OnBeforeBindingData();
    //    //    BindingData();
    //    //    //BindingDataCount++;
    //    //    OnAfterBindingData();
    //    //}
    //    //public virtual void OnBeforeBindingData() { }
    //    //public virtual void BindingData() { }
    //    //public virtual void OnAfterBindingData() { }
    //    //#endregion

    //    //#region Permissions
    //    ///// <summary>
    //    ///// ინიციალიზაციას უკეთებს უდლებებს.
    //    ///// გამოიძახება BasePage.Load-is
    //    ///// </summary>
    //    //public void DoInitPermission()
    //    //{
    //    //    if (!AutoInitPermission)
    //    //        return;

    //    //    OnBeforeInitPermission();
    //    //    InitPermission();
    //    //    OnAfterInitPermission();
    //    //}
    //    ///// <summary>
    //    ///// მეთოდი გამოიძახება InitPermission()-მდე.
    //    ///// ცარიელი მეთოდია.
    //    ///// </summary>
    //    //public virtual void OnBeforeInitPermission() { }
    //    ///// <summary>
    //    ///// AutoPermission = true მაშინ გამოიძახება ეს მეთოდი.
    //    ///// ცარიელი მეთოდია საჭიროებს override-ს.
    //    ///// </summary>
    //    //public virtual void InitPermission() { throw new NotImplementedException(); }
    //    ///// <summary>
    //    ///// მეთოდი გამოიძახება InitPermission()-ის შემდეგ.
    //    ///// ცარიელი მეთოდია.
    //    ///// </summary>
    //    //public virtual void OnAfterInitPermission() { }


    //    ///// <summary>
    //    ///// ამოწმებს უდლებებს.
    //    ///// გამოიძახება BasePage.Load-is
    //    ///// </summary>
    //    //public void DoChechPermission()
    //    //{
    //    //    if (!AutoCheckPermission)
    //    //        return;

    //    //    OnBeforeCheckPermission();
    //    //    ChechPermission();
    //    //    OnAfterCheckPermission();
    //    //}
    //    ///// <summary>
    //    ///// მეთოდი გამოიძახება ChechPermission()-მდე.
    //    ///// ცარიელი მეთოდია.
    //    ///// </summary>
    //    //public virtual void OnBeforeCheckPermission() { }
    //    ///// <summary>
    //    ///// AutoCheckPermission = true მაშინ გამოიძახება ეს მეთოდი.
    //    ///// ცარიელი მეთოდია საჭიროებს override-ს.
    //    ///// </summary>
    //    //public virtual void ChechPermission() { throw new NotImplementedException(); }
    //    ///// <summary>
    //    ///// მეთოდი გამოიძახება ChechPermission()-ის შემდეგ.
    //    ///// ცარიელი მეთოდია.
    //    ///// </summary>
    //    //public virtual void OnAfterCheckPermission() { }
    //    //#endregion

    //    //#region Validations
    //    //public bool DoIsValidFormControls()
    //    //{
    //    //    return IsValidFormControls();
    //    //}
    //    //public virtual bool IsValidFormControls() { return true; }
    //    //#endregion

    //    //#region Methods
    //    //public void AddKeyWords(string keywords)
    //    //{
    //    //    if (!Meta.ContainsKey("keywords"))
    //    //        Meta.Add("keywords", string.Empty);
    //    //    Meta["keywords"] = Meta["keywords"].ToString() + keywords;
    //    //}
    //    //public void AddDescription(string description)
    //    //{
    //    //    if (!Meta.ContainsKey("description"))
    //    //        Meta.Add("description", string.Empty);
    //    //    Meta["description"] = Meta["description"].ToString() + description;
    //    //}

    //    ///// <summary>
    //    ///// რთავს/თიშავს კონტროლებს.
    //    ///// </summary>
    //    //public void DoEnableDisableFormControls()
    //    //{
    //    //    EnableDisableFormControls();
    //    //}
    //    ///// <summary>
    //    ///// რთავს/თიშავს კონტროლებს.
    //    ///// </summary>
    //    //public virtual void EnableDisableFormControls() { }




    //    //public bool DoPostBackData()
    //    //{
    //    //    if (ReadOnly || IsLoading || !IsValidFormControls()) return false;

    //    //    return PostBackData();
    //    //}
    //    //public virtual bool PostBackData() { return true; }


    //    /// <summary>
    //    /// არეგისტრირებს script-ს გვერდის ჩატვირთვისას.
    //    /// </summary>
    //    /// <param name="key">სკრიპტის დასახელება.</param>
    //    /// <param name="scriptUrl">მისამართი სკრიპტის ფაილის.</param>
    //    protected virtual void RegisterScript(string key, string scriptUrl)
    //    {
    //        PageHelper.RegisterScript(this, key, scriptUrl);
    //    }
    //    /// <summary>
    //    /// ამატებს header-ში Css-ს
    //    /// </summary>
    //    /// <param name="url">CSS ფაილის ლინკი</param>
    //    protected virtual void RegisterCSSLink(string cssUrl)
    //    {
    //        PageHelper.RegisterCssLink(this, cssUrl);
    //    }
    //    /// <summary>
    //    /// არეგისტრირებს Meta ტეგს.
    //    /// </summary>
    //    /// <param name="name">ტეგის დასახელება.</param>
    //    /// <param name="content">კონტენტი.</param>
    //    protected virtual void RegisterMeta(string name, string content)
    //    {
    //        PageHelper.RegisterMeta(this, name, content);
    //    }
    //    //#endregion

    //    //#region Authentication
    //    ///// <summary>
    //    ///// როცა არაწორი პაროლით ან უზერით აპირებს შეცვლას, მაშინ უნდა გამოვიძახოთ ეს ფუნქცია.
    //    ///// </summary>
    //    //public virtual void FailedPasswordAttempt()
    //    //{
    //    //    SessionFailedPasswordAttemptCount++;
    //    //}
    //    ///// <summary>
    //    ///// როცა დამახსოვრებული აქვს Cookie-ში უზერის ID და პაროლის ჰეში,
    //    ///// მაშინ უნდა გავიაროთ აუტორიზაცია თავიდან და გადავამოწმოთ მართალი CookieUserID & CookiePassword არის თუ არა.
    //    ///// </summary>
    //    ///// <returns>აბრუნებს true-ს თუ სწორედ გაუარა აუტორიზაცია.</returns>
    //    //public virtual bool AuthenticateFromCookie()
    //    //{
    //    //    if (CookieUserID == 0 || CookiePassword.Trim().Length == 0) return false;

    //    //    return AuthenticateFromCookie(CookieUserID, CookiePassword);
    //    //}
    //    ///// <summary>
    //    ///// ცარიელი ფუნქციაა საჭიროა ამის ovveride.
    //    ///// </summary>
    //    ///// <param name="userID">მომხმარებლის ID.</param>
    //    ///// <param name="password">პაროლის ჰეში.</param>
    //    ///// <returns>აბრუნებს true-ს თუ სწორედ გაუარა აუტორიზაცია.</returns>
    //    //public virtual bool AuthenticateFromCookie(int userID, string password)
    //    //{
    //    //    return AuthenticateFromCookie(userID, string.Empty, password);
    //    //}
    //    ///// <summary>
    //    ///// ცარიელი ფუნქციაა საჭიროა ამის ovveride.
    //    ///// </summary>
    //    ///// <param name="userName">მომხმარებლის სახელი.</param>
    //    ///// <param name="password">პაროლის ჰეში.</param>
    //    ///// <returns>აბრუნებს true-ს თუ სწორედ გაუარა აუტორიზაცია.</returns>
    //    //public virtual bool AuthenticateFromCookie(string userName, string password)
    //    //{
    //    //    return AuthenticateFromCookie(0, userName, password);
    //    //}
    //    ///// <summary>
    //    ///// ცარიელი ფუნქციაა საჭიროა ამის ovveride.
    //    ///// </summary>
    //    ///// <param name="userID">მომხმარებლის ID.</param>
    //    ///// <param name="userName">მომხმარებლის სახელი.</param>
    //    ///// <param name="password">პაროლის ჰეში.</param>
    //    ///// <returns>აბრუნებს true-ს თუ სწორედ გაუარა აუტორიზაცია.</returns>
    //    //public virtual bool AuthenticateFromCookie(int userID, string userName, string password)
    //    //{
    //    //    throw new NotImplementedException();
    //    //}

    //    //public virtual void Authenticate(int userID, string password)
    //    //{
    //    //    Authenticate(userID, password, DateTimeHelper.MinDate);
    //    //}
    //    //public virtual void Authenticate(int userID, string password, DateTime lastActivityDate)
    //    //{
    //    //    Authenticate(userID, password, lastActivityDate, DateTime.Now.AddYears(1));
    //    //}
    //    //public virtual void Authenticate(int userID, string password, DateTime lastActivityDate, bool setAuthCookie)
    //    //{
    //    //    Authenticate(userID, password, lastActivityDate, DateTime.Now.AddYears(1), setAuthCookie);
    //    //}
    //    //public virtual void Authenticate(int userID, string password, DateTime lastActivityDate, bool setAuthCookie, bool setAuthSession)
    //    //{
    //    //    Authenticate(userID, password, lastActivityDate, DateTime.Now.AddYears(1), setAuthCookie, setAuthSession);
    //    //}
    //    //public virtual void Authenticate(int userID, string password, DateTime lastActivityDate, DateTime expires)
    //    //{
    //    //    Authenticate(userID, password, lastActivityDate, expires, true);
    //    //}
    //    //public virtual void Authenticate(int userID, string password, DateTime lastActivityDate, DateTime expires, bool setAuthCookie)
    //    //{
    //    //    Authenticate(userID, password, lastActivityDate, expires, setAuthCookie, true);
    //    //}
    //    //public virtual void Authenticate(int userID, string password, DateTime lastActivityDate, DateTime expires, bool setAuthCookie, bool setAuthSession)
    //    //{
    //    //    Authenticate(userID, string.Empty, password, lastActivityDate, expires, setAuthCookie, setAuthSession);
    //    //}

    //    //public virtual void Authenticate(string userName, string password)
    //    //{
    //    //    Authenticate(userName, password, DateTimeHelper.MinDate);
    //    //}
    //    //public virtual void Authenticate(string userName, string password, DateTime lastActivityDate)
    //    //{
    //    //    Authenticate(userName, password, lastActivityDate, DateTime.Now.AddYears(1));
    //    //}
    //    //public virtual void Authenticate(string userName, string password, DateTime lastActivityDate, bool setAuthCookie)
    //    //{
    //    //    Authenticate(userName, password, lastActivityDate, DateTime.Now.AddYears(1), setAuthCookie);
    //    //}
    //    //public virtual void Authenticate(string userName, string password, DateTime lastActivityDate, bool setAuthCookie, bool setAuthSession)
    //    //{
    //    //    Authenticate(userName, password, lastActivityDate, DateTime.Now.AddYears(1), setAuthCookie, setAuthSession);
    //    //}
    //    //public virtual void Authenticate(string userName, string password, DateTime lastActivityDate, DateTime expires)
    //    //{
    //    //    Authenticate(userName, password, lastActivityDate, expires, true);
    //    //}
    //    //public virtual void Authenticate(string userName, string password, DateTime lastActivityDate, DateTime expires, bool setAuthCookie)
    //    //{
    //    //    Authenticate(userName, password, lastActivityDate, expires, setAuthCookie, true);
    //    //}
    //    //public virtual void Authenticate(string userName, string password, DateTime lastActivityDate, DateTime expires, bool setAuthCookie, bool setAuthSession)
    //    //{
    //    //    Authenticate(0, userName, password, lastActivityDate, expires, setAuthCookie, setAuthSession);
    //    //}

    //    //public virtual void Authenticate(int userID, string userName, string password)
    //    //{
    //    //    Authenticate(userID, userName, password, DateTimeHelper.MinDate);
    //    //}
    //    //public virtual void Authenticate(int userID, string userName, string password, DateTime lastActivityDate)
    //    //{
    //    //    Authenticate(userID, userName, password, lastActivityDate, DateTime.Now.AddYears(1));
    //    //}
    //    //public virtual void Authenticate(int userID, string userName, string password, DateTime lastActivityDate, bool setAuthCookie)
    //    //{
    //    //    Authenticate(userID, userName, password, lastActivityDate, DateTime.Now.AddYears(1), setAuthCookie);
    //    //}
    //    //public virtual void Authenticate(int userID, string userName, string password, DateTime lastActivityDate, bool setAuthCookie, bool setAuthSession)
    //    //{
    //    //    Authenticate(userID, userName, password, lastActivityDate, DateTime.Now.AddYears(1), setAuthCookie, setAuthSession);
    //    //}
    //    //public virtual void Authenticate(int userID, string userName, string password, DateTime lastActivityDate, DateTime expires)
    //    //{
    //    //    Authenticate(userID, userName, password, lastActivityDate, expires, true);
    //    //}
    //    //public virtual void Authenticate(int userID, string userName, string password, DateTime lastActivityDate, DateTime expires, bool setAuthCookie)
    //    //{
    //    //    Authenticate(userID, userName, password, lastActivityDate, expires, setAuthCookie, true);
    //    //}
    //    //public virtual void Authenticate(int userID, string userName, string password, DateTime lastActivityDate, DateTime expires, bool setAuthCookie, bool setAuthSession)
    //    //{
    //    //    if (setAuthCookie)
    //    //    {
    //    //        HttpCookie cookie = new HttpCookie(AuthenticationCookieName);
    //    //        cookie.Values.Add(Prefix + "UserID", userID.ToString());
    //    //        cookie.Values.Add(Prefix + "UserName", userName);
    //    //        cookie.Values.Add(Prefix + "Password", password);
    //    //        cookie.Expires = expires;
    //    //        Response.Cookies.Add(cookie);
    //    //    }

    //    //    if (setAuthSession)
    //    //    {
    //    //        SessionUserID = userID;
    //    //        SessionUserName = userName;
    //    //        SessionPassword = password;
    //    //        SessionLastActivityDate = lastActivityDate;
    //    //        SessionFailedPasswordAttemptCount = 0;
    //    //    }
    //    //}

    //    ///// <summary>
    //    ///// ეს მეთოდის იძახებს შემდეგ მეთოდებს:
    //    ///// Response.Cookies.Remove(AuthenticationCookieName);
    //    ///// Session.RemoveAll();
    //    ///// Session.Abandon();
    //    ///// </summary>
    //    //public virtual void SignOut()
    //    //{
    //    //    Response.Cookies.Remove(AuthenticationCookieName);
    //    //    Session.RemoveAll();
    //    //    Session.Abandon();
    //    //}
    //    ///// <summary>
    //    ///// BasePage.Load ის დროს თუ IsSecuredPage არის გამოიძახებს ამ მეთოდს.
    //    ///// ცარიელი მეთოდია საჭიროებს override-ს.
    //    ///// </summary>
    //    //public virtual void CheckAuthorization() { throw new NotImplementedException(); }

    //    ///// <summary>
    //    ///// უფლების შემოწმება.
    //    ///// </summary>
    //    ///// <param name="action">ექშენი - რისი უფლების შემოწმებაც გვინდა.</param>
    //    ///// <returns>აბრუნებს true-ს, როცა უფლება აქვს.</returns>
    //    //public virtual bool IsPermitted(Zek.Data.DatabaseAction action)
    //    //{
    //    //    return IsPermitted((int)action);
    //    //}
    //    ///// <summary>
    //    ///// უფლების შემოწმება.
    //    ///// </summary>
    //    ///// <returns>აბრუნებს true-ს, როცა უფლება აქვს.</returns>
    //    //public virtual bool IsPermitted(int permissionToCheck)
    //    //{
    //    //    return IsPermitted(Permission, permissionToCheck);
    //    //}
    //    ///// <summary>
    //    ///// უფლების შემოწმება.
    //    ///// </summary>
    //    ///// <param name="permissions">უფლებები (ორობითი ბიტების ლოგიკური OR).</param>
    //    ///// <param name="permissionToCheck">რისი უფლების შემოწმებაც გვინდა.</param>
    //    ///// <returns>აბრუნებს true-ს, როცა უფლება აქვს.</returns>
    //    //public virtual bool IsPermitted(int permissions, int permissionToCheck)
    //    //{
    //    //    return BitwiseOperation.HasFlag(permissions, permissionToCheck);
    //    //}

    //    ///// <summary>
    //    ///// ეს მეთოდი იგივეა რაც Global.asax-ში void Application_AuthorizeRequest(object sender, EventArgs e).
    //    ///// იმიტომ გავაკეთე ეს რომ Session-ის კავშირი მქონოდა Global.asax-ში არ გვაქვს Session.
    //    ///// ცარიელი მეთოდია საჭიროებს override-ს.
    //    ///// </summary>
    //    //public virtual void AuthenticateRequest() { throw new NotImplementedException(); }
    //}

    public class BasePage : Page
    {
        public BasePage()
        {
            Load += BasePage_Load;
        }

        ///// <summary>
        ///// ამ ევენტის დროს კარგი იქნება ჯგუფების და უფლებების ინიციალიზაცია.
        ///// </summary>
        //public EventHandler AuthenticateRequest;

        /// <summary>
        /// წარმოიშვება ფორმის ჩატვირთვის დროს
        /// გამოიყენება როდესაც გვინდა UserData-ს არ რაიმე მსგავსი პარამეტრების შევსება.
        /// ან როცა ხდება Cookie-დან ავტორიზაცია და Session["UserID"]- არ გვაქვს.
        /// </summary>
        protected virtual void OnAuthenticateRequest()
        {
            //if (AuthenticateRequest != null)
            //    AuthenticateRequest(this, EventArgs.Empty);
        }
        private void BasePage_Load(object sender, EventArgs e)
        {
            OnAuthenticateRequest();
        }
    }
}