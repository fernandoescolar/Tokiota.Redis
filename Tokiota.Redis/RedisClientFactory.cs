namespace Tokiota.Redis
{
    using System;
    using Utilities;

    public class RedisClientFactory : IRedisClientFactory
    {
        private const int DefaultMinNumberOfClients = 2;
        private const int DefaultMaxNumberOfClients = 6;
        private const int DefaultPort = 6379;
        private const int DefaultSslPort = 6380;
        private const string DefaultHost = "localhost";

        private readonly Pool<PooledRedisClient> clientPool;
        private readonly RedisEndpoint endpoint;

        internal Pool<PooledRedisClient> Pool { get { return clientPool; } }

        public RedisClientFactory()
            : this(DefaultMinNumberOfClients, DefaultMaxNumberOfClients)
        {
        }

        public RedisClientFactory(int minActiveClients, int maxActiveClients)
            : this(minActiveClients, maxActiveClients, DefaultHost)
        {
        }

        public RedisClientFactory(string host)
            : this(host, DefaultPort)
        {
        }

        public RedisClientFactory(int minActiveClients, int maxActiveClients, string host)
            : this(minActiveClients, maxActiveClients, host, DefaultPort)
        {
        }

        public RedisClientFactory(string host, int port)
            : this(DefaultMinNumberOfClients, DefaultMaxNumberOfClients, host, port, null)
        {
        }

        public RedisClientFactory(int minActiveClients, int maxActiveClients, string host, int port)
            : this(minActiveClients, maxActiveClients, host, port, null)
        {
        }

        public RedisClientFactory(string host, int port, string password)
            : this(DefaultMinNumberOfClients, DefaultMaxNumberOfClients, host, port, password)
        {
        }

        public RedisClientFactory(int minActiveClients, int maxActiveClients, string host, int port, string password)
            : this(minActiveClients, maxActiveClients, new RedisEndpoint { Host = host, Port = port, Password = password, UseSsl = port == DefaultSslPort, Timeout = TimeSpan.FromSeconds(30) })
        {
        }

        public RedisClientFactory(RedisEndpoint endpoint)
            : this(DefaultMinNumberOfClients, DefaultMaxNumberOfClients, endpoint)
        {
        }


        public RedisClientFactory(int minActiveClients, int maxActiveClients, RedisEndpoint endpoint)
        {
            if (endpoint.Host == null)
                throw new ArgumentNullException("host");

            this.endpoint = endpoint;
            this.clientPool = new Pool<PooledRedisClient>(minActiveClients, maxActiveClients, this.CreateRedisClient);
        }

        ~RedisClientFactory()
        {
            this.Dispose(false);
        }

        public virtual IRedisClient GetCurrent()
        {
            return this.clientPool.GetObject();
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
                this.clientPool.Dispose();
            }
        }

        private PooledRedisClient CreateRedisClient()
        {
            var client = new RedisClient(this.endpoint);
            var wrapper = new PooledRedisClient(client);
            return wrapper;
        }
    }
}
