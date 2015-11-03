namespace Tokiota.Redis.Protocol
{
    using Net;
    using System.IO;

    internal class ErrorResponseParser : IResponseParser<string>
    {
        public bool CheckExpetedHeader(int byteHeader)
        {
            if (byteHeader == -1)
                throw new IOException("No more data");

            return byteHeader == '-';

        }

        public bool TryParseResponse(RedisSocket socket, string textHeader, out string result)
        {
            result = textHeader.StartsWith("ERR ") ? textHeader.Substring(4) : textHeader;
            return true;
        }
    }
}
