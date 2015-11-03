namespace Tokiota.Redis
{
    using System.Net.Sockets;

    public class RedisRelayClientFactory : RedisClientFactory
    {
        private const int NetworkIsUnreachableCode = 10051;

        public RedisRelayClientFactory()
            : base()
        {
        }

        public RedisRelayClientFactory(int minActiveClients, int maxActiveClients)
            : base(minActiveClients, maxActiveClients)
        {
        }

        public RedisRelayClientFactory(string host)
            : base(host)
        {
        }

        public RedisRelayClientFactory(int minActiveClients, int maxActiveClients, string host)
            : base(minActiveClients, maxActiveClients, host)
        {
        }

        public RedisRelayClientFactory(string host, int port)
            : base(host, port)
        {
        }

        public RedisRelayClientFactory(int minActiveClients, int maxActiveClients, string host, int port)
            : base(minActiveClients, maxActiveClients, host, port, null)
        {
        }

        public RedisRelayClientFactory(string host, int port, string password)
            : base(host, port, password)
        {
        }

        public RedisRelayClientFactory(int minActiveClients, int maxActiveClients, string host, int port, string password)
            : base(minActiveClients, maxActiveClients, host, port, password)
        {
        }

        public RedisRelayClientFactory(RedisEndpoint endpoint)
            : base(endpoint)
        {
        }


        public RedisRelayClientFactory(int minActiveClients, int maxActiveClients, RedisEndpoint endpoint) 
            : base(minActiveClients, maxActiveClients, endpoint)
        {
        }

        public override IRedisClient GetCurrent()
        {
            for (var i = 0; i < this.Pool.MaximumPoolSize; i++)
            {
                try
                {
                    var client = base.GetCurrent();
                    if (client.Connection.Ping())
                        return client;
                }
                catch
                {
                    if (i == this.Pool.MaximumPoolSize - 1)
                        throw;
                }
              
            }

            throw new SocketException(NetworkIsUnreachableCode);
        }
    }
}
