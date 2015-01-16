using Tokiota.Redis.Net;

namespace Tokiota.Redis.Protocol
{
    internal interface IOperationRequest
    {
        bool Send(RedisSocket redisSocket, byte[][] commands);
    }
}
