using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Zek.Data;

namespace Zek.Windows.Forms.DevEx
{
    public partial class BaseForm : XtraForm
    {
        public BaseForm()
        {
            InitializeComponent();
            _optionsBaseForm.PropertyChanged += OnOptionsPropertyChanged;
        }

        #region Options
        private BaseFormOptions _optionsBaseForm = new BaseFormOptions();
        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Zek"), Description("BaseForm properties.")]
        public BaseFormOptions OptionsBaseForm => _optionsBaseForm;

        protected virtual void OnOptionsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsLoading":
                    OnIsLoadingChanged();
                    break;

                case "ReadOnly":
                    OnReadOnlyChanged();
                    break;

                case "IsPrintable":
                    OnIsPrintableChanged();
                    break;


                case "IsValidable":
                    OnIsValidableChanged();
                    break;

                //case "RecordID":
                //    OnRecordIDChanged();
                //    break;
            }
        }

        [Browsable(true), Category("Zek"), Description("Occurs after IsLoading changed.")]
        public event EventHandler IsLoadingChanged;
        protected virtual void OnIsLoadingChanged()
        {
            if (IsLoadingChanged != null)
                IsLoadingChanged(this, EventArgs.Empty);
        }

        [Browsable(true), Category("Zek"), Description("Occurs after ReadOnly changed.")]
        public event EventHandler ReadOnlyChanged;
        protected virtual void OnReadOnlyChanged()
        {
            if (ReadOnlyChanged != null)
                ReadOnlyChanged(this, EventArgs.Empty);
        }

        [Browsable(true), Category("Zek"), Description("Occurs after IsPrintable changed.")]
        public event EventHandler IsPrintableChanged;
        protected virtual void OnIsPrintableChanged()
        {
            if (IsPrintableChanged != null)
                IsPrintableChanged(this, EventArgs.Empty);
        }

        [Browsable(true), Category("Zek"), Description("Occurs after IsValidable changed.")]
        public event EventHandler IsValidableChanged;
        protected virtual void OnIsValidableChanged()
        {
            if (IsValidableChanged != null)
                IsValidableChanged(this, EventArgs.Empty);
        }
        #endregion

        #region Fields
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected object PrimaryKey { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected IntPtr SenderHandle { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected string InternalText { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected bool IsLoading
        {
            get { return OptionsBaseForm.IsLoading; }
            set { OptionsBaseForm.IsLoading = value; }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected bool IsClosing
        {
            get { return OptionsBaseForm.IsClosing; }
            set { OptionsBaseForm.IsClosing = value; }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected virtual bool ReadOnly
        {
            get { return OptionsBaseForm.ReadOnly; }
            set { OptionsBaseForm.ReadOnly = value; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected virtual bool IsValid
        {
            get
            {
                try
                {
                    return IsValidForm();
                }
                catch (Exception ex)
                {
                    ExceptionHelper.Show(this, ex, "IsValidForm", MessageBoxIcon.Error);
                    return false;
                }
            }
        }
        protected virtual bool IsValidForm() { return true; }
        #endregion

        #region Permissions
        protected void TryInitPermission()
        {
            try
            {
                InitPermission();
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, "InitPermission", MessageBoxIcon.Error);
            }
        }
        protected virtual void InitPermission() { }

        protected void TryCheckPermission()
        {
            try
            {
                CheckPermission();
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, "CheckPermission", MessageBoxIcon.Error);
            }
        }
        protected virtual void CheckPermission() { }

        protected virtual bool IsAllowedPrint => IsPermitted(DatabaseAction.Print);

        protected virtual bool IsPermitted(DatabaseAction action)
        {
            return true;
        }
        #endregion

        #region Binding
        protected void TryBindControls()
        {
            try
            {
                BindControls();
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, "BindControls", MessageBoxIcon.Error);
                IsClosing = true;
            }
        }
        protected virtual void BindControls() { }

        protected void TryBindData()
        {
            try
            {
                BindData();
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, "BindData", MessageBoxIcon.Error);
                IsClosing = true;
            }
        }
        protected virtual void BindData() { }

        protected void TryInit()
        {
            try
            {
                Init();
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, "Init", MessageBoxIcon.Error);
            }
        }
        protected virtual void Init() { }
        #endregion

        #region Validations
        /// <summary>
        /// Clear all errors.
        /// </summary>
        protected virtual void ClearErrors()
        {
            dxErrorProvider.ClearErrors();
        }
        /// <summary>
        /// Clears control error.
        /// </summary>
        /// <param name="control">Control to clear error.</param>
        protected virtual void ClearError(Control control)
        {
            SetError(control, string.Empty);
        }
        /// <summary>
        /// Set control error.
        /// </summary>
        /// <param name="control">Control to set error.</param>
        /// <param name="errorText">Error text.</param>
        protected virtual void SetError(Control control, string errorText)
        {
            dxErrorProvider.SetError(control, errorText);
        }
        #endregion

        #region LockUpdate
        protected virtual void BeginUpdate() { _optionsBaseForm.BeginUpdate(); }
        protected virtual void EndUpdate() { _optionsBaseForm.EndUpdate(); }
        protected virtual void CancelUpdate() { _optionsBaseForm.CancelUpdate(); }
        protected virtual bool IsLockUpdate => _optionsBaseForm.IsLockUpdate;

        #endregion

        #region Methods
        //public virtual void InvokeAction(string actionName, params object[] args) { }
        protected virtual DialogResult ShowException(Exception ex, string caption = null, MessageBoxIcon icon = MessageBoxIcon.Error)
        {
            return ExceptionHelper.Show(this, ex, caption, icon);
        }
        protected virtual void TryFinallyStart()
        {
            Application.DoEvents();
            Cursor = Cursors.WaitCursor;
        }
        protected virtual void TryFinallyEnd()
        {
            Application.DoEvents();
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Sets DialogResult to the form and closes.
        /// </summary>
        /// <param name="dialogResult"></param>
        protected void Close(DialogResult dialogResult)
        {
            DialogResult = dialogResult;
            Close();
        }
        protected void TryCancel()
        {
            try
            {
                Cancel();
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, "Cancel", MessageBoxIcon.Error);
            }
        }
        protected virtual void Cancel()
        {
            Close(DialogResult.Cancel);
        }

        protected void TryPrint()
        {
            try
            {
                Print();
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, "Print", MessageBoxIcon.Error);
                IsClosing = true;
            }
        }

        protected virtual void Print() { throw new NotImplementedException(); }

        protected virtual void SetImageSize16x16() { throw new NotImplementedException(); }
        protected virtual void SetImageSize24x24() { throw new NotImplementedException(); }

        protected virtual void ShowFilterFields(string filterType, Control ctrl1, Control ctrl2 = null)
        {
            FilterHelper.ShowFilterFields(filterType, ctrl1, ctrl2);
        }
        //protected DataTable GetDataTable(string commandText, CommandType commandType = CommandType.Text)
        //{
        //    return SqlHelper.ExecuteDataTable(BaseAppConfig.ConnectionString, commandType, commandText);
        //}
        //protected List<T> ExecuteList<T>(string commandText, CommandType commandType = CommandType.Text)
        //{
        //    return SqlHelper.ExecuteList<T>(BaseAppConfig.ConnectionString, commandType, commandText);
        //}
        #endregion

        private void BaseForm_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            InternalText = Text;

            TryInitPermission();
            TryCheckPermission();

            if (IsClosing) return;
            if (OptionsBaseForm.AutoWindowState != FormWindowState.Normal) WindowState = OptionsBaseForm.AutoWindowState;

            TryBindControls();
            if (IsClosing) return;

            TryBindData();
            if (IsClosing) return;

            if (IsClosing) return;
            TryInit();
            IsLoading = false;
        }

        private void BaseForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            IsLoading = true;
            IsClosing = true;
        }

        private void BaseForm_Shown(object sender, EventArgs e)
        {
            if (IsClosing)
                Close();
        }

        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    base.OnKeyDown(e);
        //    if (e.Handled || e.SuppressKeyPress) return;


        //    switch (e.KeyCode)
        //    {
        //        case Keys.F11:
        //            if (e.Control || e.Shift || e.Alt) return;
        //            e.SuppressKeyPress = true;
        //            e.Handled = true;
        //            TryCancel();
        //            break;

        //        case Keys.F5:
        //            if (!e.Control || e.Shift || e.Alt || !OptionsBaseForm.IsRefreshableFormControls || OptionsBaseForm.ReadOnly) return;
        //            e.SuppressKeyPress = true;
        //            e.Handled = true;
        //            TryBindingFormControls();
        //            break;
        //    }
        //}
    }
}