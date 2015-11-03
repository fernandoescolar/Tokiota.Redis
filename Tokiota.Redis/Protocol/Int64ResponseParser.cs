namespace Tokiota.Redis.Protocol
{
    using Net;

    internal class Int64ResponseParser : IResponseParser<long>
    {
        public bool CheckExpetedHeader(int byteHeader)
        {
            return byteHeader == ':';
        }

        public bool TryParseResponse(RedisSocket socket, string textHeader, out long result)
        {
            return long.TryParse(textHeader, out result);
        }
    }
}
