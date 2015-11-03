namespace Tokiota.Redis.Protocol
{
    using Net;

    internal interface IResponseParser<T>
    {
        bool CheckExpetedHeader(int byteHeader);

        bool TryParseResponse(RedisSocket socket, string textHeader, out T result);
    }
}
