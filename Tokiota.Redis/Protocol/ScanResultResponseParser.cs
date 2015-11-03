namespace Tokiota.Redis.Protocol
{
    using Net;
    using System;
    using Utilities;

    internal class ScanResultResponseParser : IResponseParser<RedisScanResult>
    {
        private static readonly IResponseParser<byte[]> BulkByteParser = new BulkByteResponseParser();
        private static readonly IResponseParser<byte[][]> BulkByteArrayParser = new BulkByteArrayResponseParser();

        public bool CheckExpetedHeader(int byteHeader)
        {
            return byteHeader == '*';
        }

        public bool TryParseResponse(RedisSocket socket, string textHeader, out RedisScanResult result)
        {
            int size;
            if (!int.TryParse(textHeader, out size))
            {
                throw new Exception("Invalid length");
            }

            if (size != 2)
            {
                throw new Exception("Invalid scan result content");
            }

            var nextPointer = ReadNextPointer(socket);
            var arrayResult = ReadArrayResult(socket);
            result = new RedisScanResult(nextPointer, arrayResult);
            return true;
        }

        private static ulong ReadNextPointer(RedisSocket socket)
        {
            var bHeader = socket.ReadByte();
            var sHeader = socket.ReadLine();
            if (!BulkByteParser.CheckExpetedHeader(bHeader))
            {
                throw new Exception("Invalid pointer content");
            }

            byte[] buffer;
            if (!BulkByteParser.TryParseResponse(socket, sHeader, out buffer))
            {
                throw new Exception("Invalid pointer content");
            }

            var number = buffer.ToUtf8String();
            ulong result;
            if (ulong.TryParse(number, out result))
            {

            }

            return result;
        }

        private static byte[][] ReadArrayResult(RedisSocket socket)
        {
            var bHeader = socket.ReadByte();
            var sHeader = socket.ReadLine();
            if (!BulkByteArrayParser.CheckExpetedHeader(bHeader))
            {
                throw new Exception("Invalid data content");
            }

            byte[][] result;
            if (!BulkByteArrayParser.TryParseResponse(socket, sHeader, out result))
            {
                throw new Exception("Invalid data content");
            }

            return result;
        }
    }
}
