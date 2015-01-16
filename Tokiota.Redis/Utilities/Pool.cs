using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Tokiota.Redis.Utilities
{
    internal class Pool<T> : IDisposable where T : PooledObject 
    {
        private const int DefaultPoolMinimumSize = 5;
        private const int DefaultPoolMaximumSize = 100;

        private readonly ConcurrentQueue<T> pooledObjects;
        private Action<PooledObject, bool> returnToPoolAction;
        private int adjustPoolSizeIsInProgressCASFlag = 0;

        public Pool()
            : this(DefaultPoolMinimumSize, DefaultPoolMaximumSize, null)
        {
        }

        public Pool(int minimumPoolSize, int maximumPoolSize)
            : this(minimumPoolSize, maximumPoolSize, null)
        {
        }

        public Pool(Func<T> factoryMethod)
            : this(DefaultPoolMinimumSize, DefaultPoolMaximumSize, factoryMethod)
        {
        }

        public Pool(int minimumPoolSize, int maximumPoolSize, Func<T> factoryMethod)
        {
            ValidatePoolLimits(minimumPoolSize, maximumPoolSize);
            this.FactoryMethod = factoryMethod;
            this.MaximumPoolSize = maximumPoolSize;
            this.MinimumPoolSize = minimumPoolSize;
            this.pooledObjects = new ConcurrentQueue<T>();
            this.returnToPoolAction = ReturnObjectToPool;
            this.AdjustPoolSizeToBounds();
        }

        ~Pool()
        {
            this.Dispose(false);
            GC.SuppressFinalize(this);
        }

        public int ObjectsInPoolCount { get { return pooledObjects.Count; } }

        public int MinimumPoolSize { get; set; }

        public int MaximumPoolSize { get; set; }

        public Func<T> FactoryMethod { get; private set; }

        public T GetObject()
        {
            T dequeuedObject = null;
            if (this.pooledObjects.TryDequeue(out dequeuedObject))
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback((o) => this.AdjustPoolSizeToBounds()));
                return dequeuedObject;
            }
            else
            {
                return this.CreatePooledObject();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        internal void ReturnObjectToPool(PooledObject objectToReturnToPool, bool reRegisterForFinalization)
        {
            T returnedObject = (T)objectToReturnToPool;
            if (this.ObjectsInPoolCount < this.MaximumPoolSize)
            {
                if (!returnedObject.ResetState())
                {
                    this.DestroyPooledObject(returnedObject);
                    return;
                }

                if (reRegisterForFinalization)
                {
                    GC.ReRegisterForFinalize(returnedObject);
                }

                this.pooledObjects.Enqueue(returnedObject);
            }
            else
            {
                this.DestroyPooledObject(returnedObject);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                
            }

            foreach (var item in pooledObjects)
            {
                this.DestroyPooledObject(item);
            }
        }

        private void InitializePool(int minimumPoolSize, int maximumPoolSize, Func<T> factoryMethod)
        {
            
        }

        private void AdjustPoolSizeToBounds()
        {
            if (Interlocked.CompareExchange(ref this.adjustPoolSizeIsInProgressCASFlag, 1, 0) == 0)
            {
                while (this.ObjectsInPoolCount < this.MinimumPoolSize)
                {
                    this.pooledObjects.Enqueue(this.CreatePooledObject());
                }

                while (this.ObjectsInPoolCount > this.MaximumPoolSize)
                {
                    T dequeuedObjectToDestroy;
                    if (this.pooledObjects.TryDequeue(out dequeuedObjectToDestroy))
                    {
                        this.DestroyPooledObject(dequeuedObjectToDestroy);
                    }
                }

                this.adjustPoolSizeIsInProgressCASFlag = 0;
            }
        }

        private T CreatePooledObject()
        {
            T newObject;
            if (this.FactoryMethod != null)
            {
                newObject = this.FactoryMethod();
            }
            else
            {
                newObject = (T)Activator.CreateInstance(typeof(T));
            }


            newObject.ReturnToPool = (Action<PooledObject, bool>)this.returnToPoolAction;
            return newObject;
        }

        private void DestroyPooledObject(T objectToDestroy)
        {
            if (!objectToDestroy.Disposed)
            {
                objectToDestroy.ReleaseResources();
                objectToDestroy.Disposed = true;
            }

            GC.SuppressFinalize(objectToDestroy);
        }

        private static void ValidatePoolLimits(int minimumPoolSize, int maximumPoolSize)
        {
            if (minimumPoolSize < 0)
            {
                throw new ArgumentException("Minimum pool size must be greater or equals to zero.");
            }

            if (maximumPoolSize < 1)
            {
                throw new ArgumentException("Maximum pool size must be greater than zero.");
            }

            if (minimumPoolSize > maximumPoolSize)
            {
                throw new ArgumentException("Maximum pool size must be greater than the maximum pool size.");
            }
        }
    }
}
