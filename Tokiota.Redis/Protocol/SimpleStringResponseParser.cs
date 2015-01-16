using Tokiota.Redis.Net;

namespace Tokiota.Redis.Protocol
{
    internal class SimpleStringResponseParser : IResponseParser<string>
    {
        public bool CheckExpetedHeader(int byteHeader)
        {
            return byteHeader == '+';
        }

        public bool TryParseResponse(RedisSocket socket, string textHeader, out string result)
        {
            result = textHeader;
            return true;
        }
    }
}
