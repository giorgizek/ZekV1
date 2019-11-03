using System;

namespace Zek
{
    public abstract class DisposableObject : IDisposable
    {
        private bool _disposed;
        protected bool Disposed => _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                DisposeResources();
            }
            DisposeUnmanagedResources();

            _disposed = true;

        }

        protected abstract void DisposeResources();
        protected virtual void DisposeUnmanagedResources() { }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DisposableObject()
        {
            Dispose(false);
        }
    }
}
