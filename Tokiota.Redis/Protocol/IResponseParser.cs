using Tokiota.Redis.Net;

namespace Tokiota.Redis.Protocol
{
    internal interface IResponseParser<T>
    {
        bool CheckExpetedHeader(int byteHeader);

        bool TryParseResponse(RedisSocket socket, string textHeader, out T result);
    }
}
