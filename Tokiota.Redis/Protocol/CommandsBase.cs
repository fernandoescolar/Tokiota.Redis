using Tokiota.Redis.Net;

namespace Tokiota.Redis.Protocol
{
    internal class CommandsBase
    {
        private readonly IRedisConnection connection;

        protected IRedisConnection Connection { get { return this.connection; } }

        public CommandsBase(IRedisConnection connection)
        {
            this.connection = connection;
        }
    }
}
