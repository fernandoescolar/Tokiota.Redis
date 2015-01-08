using Tokiota.Redis.Net;

namespace Tokiota.Redis.Protocol
{
    internal class ComponentBase
    {
        private readonly IRedisConnection connection;

        protected IRedisConnection Connection { get { return this.connection; } }

        public ComponentBase(IRedisConnection connection)
        {
            this.connection = connection;
        }
    }
}
