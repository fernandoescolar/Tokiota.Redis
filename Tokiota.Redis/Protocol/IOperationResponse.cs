using Tokiota.Redis.Net;

namespace Tokiota.Redis.Protocol
{
    internal interface IOperationResponse<T>
    {
        T Receive(RedisSocket socket);
    }
}
