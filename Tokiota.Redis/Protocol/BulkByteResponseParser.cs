namespace Tokiota.Redis.Protocol
{
    using Net;
    using System;

    internal class BulkByteResponseParser : IResponseParser<byte[]>
    {
        public bool CheckExpetedHeader(int byteHeader)
        {
            return byteHeader == '$';
        }

        public bool TryParseResponse(RedisSocket socket, string textHeader, out byte[] result)
        {
            int size;
            if (!int.TryParse(textHeader, out size))
            {
                result = null;
                return false;
            }

            if (size < 0)
            {
                result = null;
                return true;
            }

            result = new byte[size];
            socket.Read(result, 0, size);

            if (socket.ReadByte() != '\r' || socket.ReadByte() != '\n')
            {
                throw new Exception("Invalid termination");
            }

            return true;
        }
    }
}
