using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using Zek.Properties;

namespace Zek.Windows.Forms.DevEx
{
    public partial class BaseEditForm : BaseForm
    {
        public BaseEditForm()
        {
            InitializeComponent();

            _optionsEditForm.PropertyChanged += OnOptionsPropertyChanged;
        }

        #region Options
        private EditFormOptions _optionsEditForm = new EditFormOptions();
        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Zek")]
        public EditFormOptions OptionsEditForm => _optionsEditForm;

        protected override void OnOptionsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                //case "Action":
                //    OnActionChanged();
                //    break;

                case "IsChanged":
                    OnIsChangedChanged();
                    break;

                default:
                    base.OnOptionsPropertyChanged(sender, e);
                    break;
            }
        }

        [Browsable(true), Category("Zek"), Description("Occurs after IsChangedChanged changed.")]
        public event EventHandler IsChangedChanged;
        protected virtual void OnIsChangedChanged()
        {
            if (IsChangedChanged != null)
                IsChangedChanged(this, EventArgs.Empty);

            if (OptionsBaseForm.AutoInitText)
                Text = InternalText + (IsChanged ? "*" : string.Empty);
        }
        #endregion

        #region Fields
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected Enum FormEnum
        {
            get;
            set;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected bool IsChanged
        {
            get { return OptionsEditForm.IsChanged; }
            set { OptionsEditForm.IsChanged = value; }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected bool PromptSave
        {
            get { return OptionsEditForm.PromptSave; }
            set { OptionsEditForm.PromptSave = value; }
        }
        #endregion

        protected void TrySaveAndClose()
        {
            //OptionsEditForm.IsSaveAndCloseExecuting = true;
            //try
            //{
                if (TrySaveData())
                    Close();
            //}
            //finally
            //{
            //    //OptionsEditForm.IsSaveAndCloseExecuting = false;
            //}
        }
        protected bool TrySaveData()
        {
            if (ReadOnly || IsLoading || !IsValid) return false;

            var saved = false;
            try
            {
                IsLoading = true;
                saved = SaveData();
                if (saved)
                {
                    IsChanged = false;

                    //if (OptionsEditForm.AutoUpdateAction)
                    //{
                    //    if (Action == Zek.Data.DatabaseAction.Add)
                    //        Action = Zek.Data.DatabaseAction.Edit;
                    //}


                    DialogResult = DialogResult.OK;
                    Init();
                    return true;
                }
                else
                {
                    XtraMessageBox.Show(this, Exceptions.ChangesNotSavedErrorText, "SaveData", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                var message = saved ? Exceptions.ErrorAfterSaveDataExceptionCaption : Exceptions.SaveDataExceptionCaption;
                ExceptionHelper.Show(this, ex, message, MessageBoxIcon.Error);
            }
            finally
            {
                IsLoading = false;
            }

            return false;
        }
        protected virtual bool SaveData() { throw new NotImplementedException(); }

        private void BaseEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ReadOnly || !PromptSave || !IsChanged /*|| DialogResult == System.Windows.Forms.DialogResult.Cancel*/) return;

            var dr = XtraMessageBox.Show(this, Exceptions.SaveChangesQuestion, Resources.Question, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            switch (dr)
            {
                case DialogResult.Yes:
                    if (!TrySaveData())
                        e.Cancel = true;
                    break;

                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }
    }
}
