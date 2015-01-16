using Tokiota.Redis.Utilities;

namespace Tokiota.Redis
{
    internal class PooledRedisClient : PooledObject, IRedisClient
    {
        private readonly IRedisClient wrappedClient;

        public PooledRedisClient(IRedisClient clientToWrap)
        {
            this.wrappedClient = clientToWrap;
        }

        public IRedisConnectionCommands Connection { get { return this.wrappedClient.Connection; } }

        public IRedisKeysCommands Keys { get { return this.wrappedClient.Keys; } }

        public IRedisStringsCommands Strings { get { return this.wrappedClient.Strings; } }

        public IRedisHashesCommands Hashes { get { return this.wrappedClient.Hashes; } }

        public IRedisListsCommands Lists { get { return this.wrappedClient.Lists; } }

        public IRedisSetsCommands Sets { get { return this.wrappedClient.Sets; } }

        public IRedisSortedSetsCommands SortedSets { get { return this.wrappedClient.SortedSets; } }

        protected override void OnReleaseResources()
        {
            this.wrappedClient.Dispose();
            base.OnReleaseResources();
        }
    }
}
