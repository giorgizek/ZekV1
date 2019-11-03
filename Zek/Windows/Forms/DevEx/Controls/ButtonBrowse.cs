using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Zek.Extensions;
using ChangingEventArgs = Zek.Core.ChangingEventArgs;
using ChangingEventHandler = Zek.Core.ChangingEventHandler;
using Images = Zek.Properties.Images;

namespace Zek.Windows.Forms.DevEx
{
    [ToolboxItem(false)]
    [DefaultEvent("PrimaryKeyChanged"), DefaultBindingProperty("PrimaryKey")]
    public class ButtonBrowse : ButtonEdit
    {
        #region Constructors
        static ButtonBrowse()
        {
            //RepositoryItemButtonBrowse.Register();
        }
        public ButtonBrowse()
        {
            //Font = new System.Drawing.Font("BPG Glaho Arial", 9.75F, System.Drawing.FontStyle.Regular);
            EnterMoveNextControl = true;

            InitButtons();

            Properties.EditValueChanging += delegate(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
            {
                if (_lockText) e.Cancel = true;
            };

            ButtonClick += delegate(object sender, ButtonPressedEventArgs e)
            {
                switch (e.Button.Kind)
                {
                    case ButtonPredefines.Ellipsis:
                        Browse();
                        break;

                    case ButtonPredefines.Delete:
                        Delete();
                        break;
                }
            };


            KeyDown += delegate(object sender, KeyEventArgs e)
            {
                if (!Enabled || Properties.ReadOnly)
                    return;

                if (!e.Alt && !e.Control && e.KeyCode == Keys.Delete)
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    Delete();
                }
                else if (!e.Alt && e.Control && e.KeyCode == Keys.Return)
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    Browse();
                }
            };

            InitMenu();
        }
        #endregion

        private void InitButtons()
        {
            Properties.Buttons.Clear();
            Properties.Buttons.AddRange(new[] {
                    new EditorButton(ButtonPredefines.Ellipsis, "Open", -1, true, true, false, ImageLocation.MiddleCenter, null, new KeyShortcut(Keys.F4), "Open (F4)"),
                    new EditorButton(ButtonPredefines.Delete, "Delete", -1, true, true, false, ImageLocation.MiddleCenter, null, new KeyShortcut(Keys.Shift | Keys.Delete), "Delete (Del)")
                });
        }

        #region Options
        private ButtonBrowseOptions _optionsButtonBrowse = new ButtonBrowseOptions();
        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Zek"), Description("ButtonBrowse ის ფროფერთები.")]
        public ButtonBrowseOptions OptionsButtonBrowse => _optionsButtonBrowse;

        #endregion

        #region Menu
        private DXMenuItem _miCopyPrimaryKey;
        private DXMenuItem _miRefresh;
        private DXMenuItem _miEdit;
        private void InitMenu()
        {
            foreach (DXMenuItem item in Menu.Items)
            {
                if (item.Caption != "&Copy")
                {
                    item.Visible = false;
                    item.Enabled = false;
                }
            }

            _miCopyPrimaryKey = new DXMenuItem("Copy ID", OnCopyPrimaryKeyClick, Images.clipboard_copy_16x16) { Enabled = false };
            Menu.Items.Add(_miCopyPrimaryKey);

            _miRefresh = new DXMenuItem("Refresh", OnRefreshRecordClick, Images.refresh_update_16x16) { Enabled = false };
            Menu.Items.Add(_miRefresh);

            _miEdit = new DXMenuItem("Edit", OnEditRecordClick, Images.tool_pencil_16x16) { Enabled = false };
            Menu.Items.Add(_miEdit);
        }

        private void OnCopyPrimaryKeyClick(object sender, EventArgs e)
        {
            CopyPrimaryKey();
        }
        private void OnEditRecordClick(object sender, EventArgs e)
        {
            EditRecord();
        }
        private void OnRefreshRecordClick(object sender, EventArgs e)
        {
            RefreshRecord();
        }
        #endregion

        #region Fields
        /// <summary>
        /// ObjectNames-დან რომელიმე დასახელება. ეს აუცილებლად უნა იყოს შეყვანილი თორე ისე უმოქმედო იქნება კომპონენტი.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected Enum FormEnum { get; set; }
        //[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //protected string DatabaseObjectName;


        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object OldPrimaryKey { get; private set; }
        private object _primaryKey;
        /// <summary>
        /// ჩანაწერის მნიშვნელობა.
        /// </summary>
        [Bindable(true), RefreshProperties(RefreshProperties.All),
        Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object PrimaryKey
        {
            get { return _primaryKey; }
            set
            {
                if (!ComparePrimaryKey(value))
                {
                    OnPrimaryKeyChanging(new ChangingEventArgs(_primaryKey, value));
                }
            }
        }


        private bool _lockText = true;
        #endregion

        #region Events
        [Category("Zek"), Description("წარმოიშვებმა მაშინვე როგორც კი RecordValue შეიცვლება.")]
        public event EventHandler PrimaryKeyChanged;

        [Category("Zek"), Description("Fires when an end-user starts to modify the Record's value value.")]
        public event ChangingEventHandler PrimaryKeyChanging;
        #endregion

        #region Methods
        protected virtual void Browse()
        {
            if (!OptionsButtonBrowse.AllowBrowse) return;

            try
            {
                if (FormEnum == null)
                    throw new ArgumentNullException(@"FormEnum");

                Application.DoEvents();
                Cursor = Cursors.WaitCursor;


                var contains = SingletonFormProvider.ContainsListForm(FormEnum, Handle, null);
                var frm = SingletonFormProvider.ShowListForm(FormEnum, FindForm().MdiParent, null, Handle);
                if (!contains && frm != null)
                {
                    if (frm.IsDisposed) return;
                    //((IBrowseForm)frm).OptionsBrowseForm.ServerMode = OptionsButtonBrowse.ServerMode;

                    var frmList = frm as ListForm;
                    if (frmList != null)
                        frmList.OptionsListForm.ListFormStyle = ListFormStyle.Choose;

                    if (!Properties.ReadOnly)
                    {
                        frm.FormClosed -= FormClosed;
                        frm.FormClosed += FormClosed;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, "ButtonBrowse.Browse()", MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        protected virtual void CopyPrimaryKey()
        {
            if (PrimaryKey != null)
                Clipboard.SetText(PrimaryKey.ToString());
        }
        protected virtual void RefreshRecord()
        {
            RefreshRecord(false);
        }
        protected virtual void RefreshRecord(bool throwError)
        {
            try
            {
                //BindingDataEventArgs e = GetBindingDataEventArgs();
                //OnBindingData(this, e);
                BindData();
                //SetText(e.Text);
            }
            catch (Exception ex)
            {
                if (throwError) throw;
                ExceptionHelper.Show(this, ex, "ButtonBrowse.RefreshRecord(bool throwError)", MessageBoxIcon.Error);
            }
        }
        protected virtual void EditRecord()
        {
            if (!OptionsButtonBrowse.AllowBrowse || PrimaryKey == null) return;

            try
            {
                if (FormEnum == null)
                    throw new ArgumentNullException("FormEnum");

                Application.DoEvents();
                Cursor = Cursors.WaitCursor;

                SingletonFormProvider.ShowEditForm(FormEnum, FindForm().MdiParent, null, IntPtr.Zero, PrimaryKey);
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, "ButtonBrowse.EditRecord", MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        public virtual void Delete()
        {
            if (!Enabled || Properties.ReadOnly)
                return;

            if (OptionsButtonBrowse.AllowNullInput)
                PrimaryKey = null;
        }



        private bool ComparePrimaryKey(object value)
        {
            return (_primaryKey == value) || ComparePrimaryKey(_primaryKey, value, OptionsButtonBrowse.AutoParsePrimaryKey);
        }
        private bool ComparePrimaryKey(object val1, object val2, bool parse)
        {
            if (parse) { val2 = DoParsePrimaryKey(val2); }
            return val1.Compare(val2);
        }

        //private BindingDataEventArgs GetBindingDataEventArgs()
        //{
        //    var paramInt = 0;
        //    var paramString = string.Empty;
        //    var paramGuid = Guid.Empty;
        //    var paramDateTime = DateTimeExtensions.MinDate;

        //    if (_primaryKey is int)
        //        paramInt = (int)_primaryKey;
        //    else if (_primaryKey is string)
        //        paramString = ((string)_primaryKey).Trim();
        //    else if (_primaryKey is Guid)
        //        paramGuid = (Guid)_primaryKey;
        //    else if (_primaryKey is DateTime)
        //    {
        //        paramDateTime = (DateTime)_primaryKey;
        //    }

        //    return new BindingDataEventArgs(paramInt, paramString, paramGuid, paramDateTime);
        //}

        private static object DoParsePrimaryKey(object value)
        {
            return value.NullIfDefault();
        }
        protected virtual void OnPrimaryKeyChanged()
        {
            try
            {
                if (_primaryKey == null)
                {
                    SetText(null);
                }
                else
                {
                    RefreshRecord(true);
                }

                PrimaryKeyChanged?.Invoke(FindForm(), EventArgs.Empty);

                _miRefresh.Enabled = _miEdit.Enabled = _miCopyPrimaryKey.Enabled = PrimaryKey != null;
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(this, ex, "ButtonBrowse.OnPrimaryKeyChanged()", MessageBoxIcon.Error);
            }
        }
        protected virtual void OnPrimaryKeyChanging(ChangingEventArgs e)
        {
            if (IsLoading)
            {
                OldPrimaryKey = _primaryKey = e.NewValue;
                if (!ComparePrimaryKey(e.NewValue))
                    OnPrimaryKeyChanged();
            }
            else
            {
                if (!ComparePrimaryKey(_primaryKey, e.NewValue, false))
                {
                    if (PrimaryKeyChanging != null)
                        PrimaryKeyChanging(FindForm(), e);
                    if (e.Cancel) return;

                    OldPrimaryKey = _primaryKey;
                    _primaryKey = e.NewValue;

                    OnPrimaryKeyChanged();
                }
            }
        }



        protected virtual void BindData()
        {
        }
        #endregion


        public override string ToolTip => @"ID = " + (PrimaryKey != null ? PrimaryKey.ToString() : "null");

        protected virtual void OnFormClosed(Form frm, FormClosedEventArgs e)
        {
            try
            {
                var frmList = frm as ListForm;
                if (frmList == null) return;

                if (frmList.OptionsGrid.SelectedRecordID == null)
                    throw new NullReferenceException("frm.OptionsGrid.SelectedRecordID == null");

                PrimaryKey = frmList.OptionsGrid.SelectedRecordID;
            }
            catch (Exception ex)
            {
                ExceptionHelper.Show(ex, "ButtonBrowse.OnFormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)", MessageBoxIcon.Error);
            }
        }
        private void FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!Enabled || Properties.ReadOnly)
                return;

            var frm = sender as Form;
            if (frm == null) return;
            frm.FormClosed -= FormClosed;

            if (frm.DialogResult != DialogResult.OK)
                return;

            OnFormClosed(frm, e);
        }

        public void SetText(object value)
        {
            try
            {
                _lockText = false;
                EditValue = value;
            }
            finally
            {
                _lockText = true;
            }
        }
    }


    /*[UserRepositoryItem("Register")]
    public class RepositoryItemButtonBrowse : DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    {
        static RepositoryItemButtonBrowse()
        {
            Register();
        }
        public RepositoryItemButtonBrowse()
        {
            EditValueChanging += delegate(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
            {
                if (_isLockedText) e.Cancel = true;
            };

            ButtonClick += delegate(object sender, ButtonPressedEventArgs e)
            {
                switch (e.Button.Kind)
                {
                    case ButtonPredefines.Ellipsis:
                        //Browse();
                        break;

                    case ButtonPredefines.Delete:
                        //Delete();
                        break;
                }
            };


            KeyDown += delegate(object sender, System.Windows.Forms.KeyEventArgs e)
            {
                if (ReadOnly) return;

                if (!e.Alt && !e.Control && e.KeyCode == System.Windows.Forms.Keys.Delete)
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    //Delete();
                }
                else if (!e.Alt && e.Control && e.KeyCode == System.Windows.Forms.Keys.Return)
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    //Browse();
                }
            };
        }

        #region Fieldd
        private bool _isLockedText = true;

        public override string EditorTypeName
        {
            get { return typeof(ButtonBrowse).Name; }
        }
        #endregion

        #region Methods
        public static void Register()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(typeof(ButtonBrowse).Name, typeof(ButtonBrowse),
                                                       typeof(RepositoryItemButtonBrowse), typeof(DevExpress.XtraEditors.ViewInfo.ButtonEditViewInfo),
                                                       new DevExpress.XtraEditors.Drawing.ButtonEditPainter(), true, null)
                                                       );
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                var source = item as RepositoryItemButtonBrowse;
                if (source == null) return;
            }
            finally
            {
                EndUpdate();
            }
        }
        public override void CreateDefaultButton()
        {
            Buttons.Clear();
            Buttons.AddRange(new EditorButton[] {
                    new EditorButton(ButtonPredefines.Ellipsis, "Open", -1, true, true, false, ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)), "Open (Ctrl+Enter)"),
                    new EditorButton(ButtonPredefines.Delete, "Delete", -1, true, true, false, ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)), "Delete (Del)")
                });
        }
        #endregion
    }*/
}
