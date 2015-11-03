namespace Tokiota.Redis.Protocol
{
    using Net;
    using System;

    internal class BulkByteArrayResponseParser : IResponseParser<byte[][]>
    {
        private static readonly IResponseParser<byte[]> BulkByteParser = new BulkByteResponseParser();

        public bool CheckExpetedHeader(int byteHeader)
        {
            return byteHeader == '*';
        }

        public bool TryParseResponse(RedisSocket socket, string textHeader, out byte[][] result)
        {
            int size;
            if (!int.TryParse(textHeader, out size))
            {
                throw new Exception("Invalid length");
            }

            if (size == -1)
            {
                result = new byte[0][];
                return true;
            }

            result = new byte[size][];
            for (int index = 0; index < size; index++)
            {
                var bHeader = socket.ReadByte();
                var sHeader = socket.ReadLine();
                if (!BulkByteParser.CheckExpetedHeader(bHeader))
                {
                    throw new Exception("Invalid byte array content");
                }

                if (!BulkByteParser.TryParseResponse(socket, sHeader, out result[index]))
                {
                    throw new Exception("Invalid byte array content");
                }
            }

            return true;
        }
    }
}
