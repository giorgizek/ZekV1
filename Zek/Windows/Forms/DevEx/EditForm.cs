using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using Zek.Properties;


namespace Zek.Windows.Forms.DevEx
{
        public partial class EditForm : BaseEditForm
        {
            public EditForm()
            {
                InitializeComponent();

                biSave.Caption = Resources.Save;
                biSaveAndClose.Caption = Resources.SaveAndClose;
                biCancel.Caption = Resources.Cancel;
                biPrint.Caption = Resources.Print;
                biValidate.Caption = Resources.Validate;
            }

            #region Methods
            protected override void SetImageSize16x16()
            {
                biSave.Glyph = Images.save_16x16;
                biSaveAndClose.Glyph = Images.save_import_16x16;
                biCancel.Glyph = Images.cancel_16x16;
                biPrint.Glyph = Images.print_16x16;
                biValidate.Glyph = Images.ok_16x16;
            }
            protected override void SetImageSize24x24()
            {
                biSave.Glyph = Images.save_24x24;
                biSaveAndClose.Glyph = Images.save_import_24x24;
                biCancel.Glyph = Images.cancel_24x24;
                biPrint.Glyph = Images.print_24x24;
                biValidate.Glyph = Images.ok_24x24;
            }
            //protected override void Init()
            //{
            //    base.Init();
            //    miPrint.Enabled = IsAllowedPrint;
            //}
            protected override void OnReadOnlyChanged()
            {
                base.OnReadOnlyChanged();
                biSave.Enabled =
                biSaveAndClose.Enabled = !ReadOnly;
            }
            protected override void OnIsPrintableChanged()
            {
                base.OnIsPrintableChanged();
                biPrint.Visibility = OptionsBaseForm.IsPrintable ? BarItemVisibility.Always : BarItemVisibility.Never;
            }
            protected override void OnIsValidableChanged()
            {
                base.OnIsValidableChanged();
                biValidate.Visibility = OptionsBaseForm.IsValidable ? BarItemVisibility.Always : BarItemVisibility.Never;
            }
            #endregion

            private void biSave_ItemClick(object sender, ItemClickEventArgs e)
            {
                try
                {
                    Application.DoEvents();
                    Cursor = Cursors.WaitCursor;

                    TrySaveData();
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }

            private void biSaveAndClose_ItemClick(object sender, ItemClickEventArgs e)
            {
                try
                {
                    Application.DoEvents();
                    Cursor = Cursors.WaitCursor;

                    TrySaveAndClose();
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }

            private void biCancel_ItemClick(object sender, ItemClickEventArgs e)
            {
                try
                {
                    Application.DoEvents();
                    Cursor = Cursors.WaitCursor;
                    TryCancel();
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

            private void biValidate_ItemClick(object sender, ItemClickEventArgs e)
            {
                try
                {
                    Application.DoEvents();
                    Cursor = Cursors.WaitCursor;
                    IsValidForm();
                }
                catch (Exception ex)
                {
                    ExceptionHelper.Show(this, ex, "Validate", MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }
}