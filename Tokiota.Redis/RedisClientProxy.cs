using System;

namespace Tokiota.Redis
{
    internal class RedisClientProxy : IRedisClient
    {
        private readonly IRedisClient innerClient;
        private readonly Action<RedisClientProxy> onDispose;

        public RedisClientProxy(IRedisClient innerClient, Action<RedisClientProxy> onDispose)
        {
            if (innerClient == null)
                throw new ArgumentNullException("innerClient");

            this.innerClient = innerClient;
            this.onDispose = onDispose;
        }

        public IRedisConnectionCommands Connection
        {
            get { return this.innerClient.Connection; }
        }

        public IRedisKeysCommands Keys
        {
            get { return this.innerClient.Keys; }
        }

        public IRedisStringsCommands Strings
        {
            get { return this.innerClient.Strings; }
        }

        public IRedisHashesCommands Hashes
        {
            get { return this.innerClient.Hashes; }
        }

        public IRedisListsCommands Lists
        {
            get { return this.innerClient.Lists; }
        }

        public IRedisSetsCommands Sets
        {
            get { return this.innerClient.Sets; }
        }

        public void Dispose()
        {
            if (this.onDispose != null)
            {
                this.onDispose(this);
            }
        }
    }
}
