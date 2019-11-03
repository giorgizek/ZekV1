﻿using System;

namespace Zek.Core
{
    public delegate void CancelEventHandler(object sender, CancelEventArgs e);

    public class CancelEventArgs : EventArgs
    {
        public CancelEventArgs()
            : this(false)
        {
        }

        public CancelEventArgs(bool cancel)
        {
            Cancel = cancel;
        }

        public bool Cancel { get; set; }
    }
}
