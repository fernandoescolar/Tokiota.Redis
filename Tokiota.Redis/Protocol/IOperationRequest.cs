namespace Tokiota.Redis.Protocol
{
    using Net;

    internal interface IOperationRequest
    {
        bool Send(RedisSocket redisSocket, byte[][] commands);
    }
}
