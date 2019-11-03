using System;

namespace Zek.Localization
{
    public class DisposableBaseType : IDisposable
    {
        private bool _Disposed;
        protected bool Disposed
        {
            get
            {
                lock (this)
                {
                    return _Disposed;
                }
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            lock (this)
            {
                if (_Disposed == false)
                {
                    Cleanup();
                    _Disposed = true;

                    GC.SuppressFinalize(this);
                }
            }
        }

        #endregion

        protected virtual void Cleanup()
        {
            // override to provide cleanup
        }

        ~DisposableBaseType()
        {
            Cleanup();
        }
    }
}
