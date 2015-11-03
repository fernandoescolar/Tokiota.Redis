namespace Tokiota.Redis.Protocol
{
    using Net;

    internal interface IOperationResponse<T>
    {
        T Receive(RedisSocket socket);
    }
}
