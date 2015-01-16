using System;
using Tokiota.Redis.Net;

namespace Tokiota.Redis.Protocol
{
    internal class OperationResponse<T> : IOperationResponse<T>
    {
        private static readonly IResponseParser<string> ErrorParser = new ErrorResponseParser();
        private readonly IResponseParser<T> parser;

        public OperationResponse(IResponseParser<T> parser)
        {
            this.parser = parser;
        }

        public T Receive(RedisSocket socket)
        {
            var byteHeader = socket.ReadByte();
            var textHeader = socket.ReadLine();

            CheckFailState(byteHeader, textHeader);
            if (!parser.CheckExpetedHeader(byteHeader))
            {
                ThrowUnknownReplyException(byteHeader, textHeader);
            }

            T result;
            if (!parser.TryParseResponse(socket, textHeader, out result))
            {
                ThrowUnknownReplyException(byteHeader, textHeader);
            }

            return result;
        }

        private static void ThrowUnknownReplyException(int byteHeader, string textHeader)
        {
            throw new Exception(string.Format("Unknown reply on {0} request: {1} {2}", typeof(T).Name, byteHeader, textHeader));
        }

        private static void CheckFailState(int byteHeader, string textHeader)
        {
            if (ErrorParser.CheckExpetedHeader(byteHeader))
            { 
                string message;
                if (ErrorParser.TryParseResponse(null, textHeader, out message))
                {
                    throw new Exception(message);
                }

                throw new Exception("Unknown error message response");
            }
        }
    }
}
