using DevExpress.XtraEditors;
using System.ComponentModel;

namespace Zek.Windows.Forms.DevEx
{
    public class CardReaderTextEdit : TextEdit, IInternalTextControl
    {
        public CardReaderTextEdit()
        {
            EditValueChanging += CardReader_EditValueChanging;
            _optionsCardReader = new CardReaderOptions(this);
            _optionsCardReader.PropertyChanged += OnOptionsPropertyChanged;
        }

        [Browsable(false)]
        public string InternalText
        {
            get { return Text; }
            set
            {
                try
                {
                    _optionsCardReader.Islocked = false;
                    Text = value;
                }
                finally
                {
                    _optionsCardReader.Islocked = true;
                }
            }
        }

        #region Options
        private CardReaderOptions _optionsCardReader;
        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Zek"), Description("ბარათის წამკითხევლის ის ფროფერთები.")]
        public CardReaderOptions OptionsCardReader => _optionsCardReader;

        protected virtual void OnOptionsPropertyChanged(object sender, PropertyChangedEventArgs e) { }
        #endregion

        protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (!Visible || !Enabled || Properties.ReadOnly) return;

            OptionsCardReader.OnKeyDown(e);
        }

        private void CardReader_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (DesignMode) return;

            if (OptionsCardReader.Islocked && !OptionsCardReader.AllowHandInput) e.Cancel = true;
        }
    }
}
