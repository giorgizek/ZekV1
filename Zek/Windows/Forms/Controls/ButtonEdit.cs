using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Zek.Windows.Forms
{
    [Designer("System.Windows.Forms.Design.TextBoxDesigner, System.Design")]
    public class ButtonEdit : System.Windows.Forms.Control
    {
        public ButtonEdit()
        {
            _textBox = new System.Windows.Forms.TextBox();
            _textBox.Name = "TexBox";
            _textBox.Top = 0;
            _textBox.Left = 0;
            _textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _textBox.SizeChanged += _textBox_SizeChanged;
            Controls.Add(_textBox);

            
            //Buttons.CollectionChanged += new CollectionChangeEventHandler(OnButtons_CollectionChanged);
        }

        private void _textBox_SizeChanged(object sender, EventArgs e)
        {
            var oldSize = Size;
            Size = new Size(oldSize.Width, _textBox.Height);
        }


        public virtual void CreateDefaultButton()
        {
            var btn = new Button();
            //btn.IsDefaultButton = true;
            //Buttons.Add(btn);
        }
        //public virtual ButtonCollection Buttons
        //{
        //    get { return _buttons; }
        //}


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_textBox != null)
                {
                    _textBox.Dispose();
                }
            }
            base.Dispose(disposing);


        }

        private System.Windows.Forms.TextBox _textBox;
    }
}
