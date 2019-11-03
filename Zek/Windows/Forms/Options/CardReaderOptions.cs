using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace Zek.Windows.Forms
{
    public interface IInternalTextControl
    {
        string InternalText { get; set; }
    }

    public class CardReaderOptions : BaseOptions
    {
        public CardReaderOptions(Control ctrl)
        {
            _ctrl = ctrl;
        }

        private Keys _readStartKey = Keys.OemSemicolon;
        [Browsable(true),
        Category("Zek"),
        Description("ბარათის წაკითხვის დაწყების სიმბოლო."),
        DefaultValue(Keys.OemSemicolon)]
        public Keys ReadStartKey
        {
            get { return _readStartKey; }
            set { _readStartKey = value; }
        }

        private Keys _readEndKey = Keys.OemQuestion;
        [Browsable(true),
        Category("Zek"),
        Description("ბარათის წაკითხვის დასრულების სიმბოლო."),
        DefaultValue(Keys.OemQuestion)]
        public Keys ReadEndKey
        {
            get { return _readEndKey; }
            set { _readEndKey = value; }
        }

        private long _cardReadSpeed = 100L;
        [Browsable(true),
        Category("Zek"),
        Description("ასოების აკრეფის სისწრაფე მილიწამებში."),
        DefaultValue(100L)]
        public long CardReadSpeed
        {
            get { return _cardReadSpeed; }
            set { _cardReadSpeed = value; }
        }

        [Browsable(true), Category("Zek"), Description("ხელით შეყვანის ნების დართვა."), DefaultValue(false)]
        public bool AllowHandInput { get; set; }


        private readonly Control _ctrl;
        private readonly System.Diagnostics.Stopwatch _stopwatch = new System.Diagnostics.Stopwatch();
        private bool _isReading;
        private string _cardReadData = string.Empty;
        internal bool Islocked = true;


        public event EventHandler EndRead;

        protected virtual void OnBeginRead()
        {
            _isReading = true;
            _cardReadData = string.Empty;
            _stopwatch.Start();
        }
        protected virtual void OnEndRead()
        {
            _isReading = false;
            _stopwatch.Reset();

            if (_cardReadData.Trim().Length == 0) return;

            ((IInternalTextControl)_ctrl).InternalText = _cardReadData;

            if (EndRead != null)
                EndRead(this, EventArgs.Empty);
        }
        protected virtual void CancelRead()
        {
            _isReading = false;
            _cardReadData = string.Empty;
            _stopwatch.Reset();
        }
        protected virtual void Restart()
        {
            _stopwatch.Reset();
            _stopwatch.Start();
        }
        internal void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == ReadStartKey)
            {
                OnBeginRead();
            }
            else if (e.KeyCode == ReadEndKey)
            {
                OnEndRead();
            }
            else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                ((IInternalTextControl)_ctrl).InternalText = string.Empty;
            }
            else
            {
                if (!_isReading) return;

                if (_stopwatch.ElapsedMilliseconds > CardReadSpeed)
                {
                    CancelRead();
                    return;
                }

                if ((e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z) || (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9))
                    _cardReadData += Convert.ToChar(e.KeyValue);
                else if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
                    _cardReadData += Convert.ToChar(e.KeyValue - 48);

                Restart();
            }
        }


        public override void Assign(BaseOptions options)
        {
            BeginUpdate();
            try
            {
                base.Assign(options);
                var op = options as CardReaderOptions;
                if (op != null)
                {
                    _readStartKey = op.ReadStartKey;
                    _readEndKey = op.ReadEndKey;
                    _cardReadSpeed = op.CardReadSpeed;
                }
            }
            finally
            {
                EndUpdate();
            }
        }
    }
}
