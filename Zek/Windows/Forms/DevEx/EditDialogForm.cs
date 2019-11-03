using System;
using System.Windows.Forms;
using Zek.Properties;

namespace Zek.Windows.Forms.DevEx
{
    public partial class EditDialogForm : BaseEditForm
    {
        public EditDialogForm()
        {
            InitializeComponent();
        }

        protected override void SetImageSize16x16()
        {
            btnOk.Image = Images.save_16x16;
            btnCancel.Image = Images.cancel_16x16;
        }
        protected override void SetImageSize24x24()
        {
            btnOk.Image = Images.save_24x24;
            btnCancel.Image = Images.cancel_24x24;
        }
        protected override void OnReadOnlyChanged()
        {
            base.OnReadOnlyChanged();
            btnOk.Enabled = !ReadOnly;
        }

        private void btnOk_Click(object sender, EventArgs e)
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Application.DoEvents();
                Cursor = Cursors.WaitCursor;

                DialogResult = DialogResult.Cancel;
                Close();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}