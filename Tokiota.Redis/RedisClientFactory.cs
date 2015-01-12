using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Tokiota.Redis.Utilities;

namespace Tokiota.Redis
{
    public class RedisClientFactory : IRedisClientFactory
    {
        private const int DefaultNumberOfClients = 3;

        private List<IRedisClient> clientList;
        private BlockingQueue<IRedisClient> clientQueue;
        private RedisEndpoint endpoint;
        private int poolSize;

        public RedisClientFactory(int size, RedisEndpoint endpoint)
        {
            if (endpoint.Host == null)
                throw new ArgumentNullException("host");

            this.clientList = new List<IRedisClient>();
            this.clientQueue = new BlockingQueue<IRedisClient>(size);
            this.endpoint = endpoint;
            this.poolSize = size;
        }

        public RedisClientFactory(RedisEndpoint endpoint)
            : this(DefaultNumberOfClients, endpoint)
        {
        }

        public RedisClientFactory(int size, string host, int port, string password)
            : this(size, new RedisEndpoint { Host = host, Port = port, Password = password, UseSsl = port != 6379, SendTimeout = 30 })
        {
        }

        public RedisClientFactory(string host, int port, string password)
            : this(DefaultNumberOfClients, host, port, password)
        {
        }

        public RedisClientFactory(int size, string host, int port)
            : this(size, host, port, null)
        {
        }

        public RedisClientFactory(string host, int port)
            : this(DefaultNumberOfClients, host, port, null)
        {
        }

        public RedisClientFactory(int size, string host)
            : this(size, host, 6379)
        {
        }

        public RedisClientFactory(string host)
            : this(host, 6379)
        {
        }

        public RedisClientFactory(int size)
            : this(size, "localhost")
        {
        }

        public RedisClientFactory()
            : this(DefaultNumberOfClients)
        {
        }

        ~RedisClientFactory()
        {
            this.Dispose(false);
        }

        public IRedisClient GetCurrent()
        {
            return new RedisClientProxy(this.GetClient(), proxy => this.ReleaseClient(proxy));
        }
      
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach(var connection in this.clientList)
                {
                    connection.Dispose();
                }

                this.clientList.Clear();
                this.clientQueue.Close();

                this.clientList = null;
                this.clientQueue = null;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private IRedisClient CreateClient()
        {
            var client = new RedisClient(this.endpoint);
            this.clientList.Add(client);

            return client;
        }

        private void ReleaseClient(IRedisClient client)
        {
            this.clientQueue.Enqueue(client);
        }

        private IRedisClient GetClient()
        {
            IRedisClient client;

            if (this.clientList.Count >= this.poolSize)
            {
                try
                {
                        if (!this.clientQueue.TryDequeue(out client, new TimeSpan(0, 0, 10)))
                    {
                        throw new TimeoutException("Couldn't get connection after 10 seconds.");
                    }
                }
                catch (TimeoutException e)
                {
                    throw e;
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
            else
            {
                client = this.CreateClient();
            }

            return client;
        }
    }
}