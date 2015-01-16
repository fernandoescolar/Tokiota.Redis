using System;
using System.Threading;

namespace Tokiota.Redis.Utilities
{
    internal abstract class PooledObject : IDisposable
    {
        internal Action<PooledObject, bool> ReturnToPool { get; set; }

        internal bool Disposed { get; set; }

        internal bool ReleaseResources()
        {
            bool successFlag = true;

            try
            {
                this.OnReleaseResources();
            }
            catch (Exception)
            {
                successFlag = false;

            }

            return successFlag;
        }

        internal bool ResetState()
        {
            bool successFlag = true;

            try
            {
                this.OnResetState();
            }
            catch (Exception)
            {
                successFlag = false;
            }

            return successFlag;
        }

        protected virtual void OnResetState()
        {
        }

        protected virtual void OnReleaseResources()
        {
        }

        private void HandleReAddingToPool(bool reRegisterForFinalization)
        {
            if (!this.Disposed)
            {
                try
                {
                    this.ReturnToPool(this, reRegisterForFinalization);
                }
                catch (Exception)
                {
                    this.Disposed = true;
                    this.ReleaseResources();
                }
            }
        }

        ~PooledObject()
        {
            this.HandleReAddingToPool(true);
        }

        public void Dispose()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback((o) => HandleReAddingToPool(false)));
        }
    }
}
