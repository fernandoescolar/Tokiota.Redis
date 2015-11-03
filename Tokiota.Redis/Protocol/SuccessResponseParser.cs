namespace Tokiota.Redis.Protocol
{
    using Net;

    internal class SuccessResponseParser : IResponseParser<bool>
    {
        public bool CheckExpetedHeader(int byteHeader)
        {
            return true;
        }

        public bool TryParseResponse(RedisSocket socket, string textHeader, out bool result)
        {
            result = true;
            return result;
        }
    }
}
