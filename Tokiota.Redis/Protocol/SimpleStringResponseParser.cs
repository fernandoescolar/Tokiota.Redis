namespace Tokiota.Redis.Protocol
{
    using Net;

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
