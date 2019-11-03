using System;
using System.Windows.Forms;
using System.ComponentModel;
using Zek.Extensions;

namespace Zek.Windows.Forms
{
    public class TextBoxEx : TextBox
    {
        public TextBoxEx()
        {
            OldText = string.Empty;
        }

        [Category("Behavior")]
        public event CancelEventHandler TextChanging;

        [Browsable(false)]
        public string OldText { get; private set; }

        protected virtual void OnTextChanging(CancelEventArgs e)
        {
            if (TextChanging != null)
                TextChanging(this, e);
        }
        protected override void OnTextChanged(EventArgs e)
        {

            if (!OldText.Compare(Text))
            {
                var cancelEventArgs = new CancelEventArgs();
                OnTextChanging(cancelEventArgs);
                if (!cancelEventArgs.Cancel)
                {
                    OldText = Text;
                    base.OnTextChanged(e);
                }
                else
                    Text = OldText;
            }
        }
    }
}
