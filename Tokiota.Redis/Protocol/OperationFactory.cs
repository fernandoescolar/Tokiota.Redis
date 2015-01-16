using Tokiota.Redis.Net;

namespace Tokiota.Redis.Protocol
{
    internal class OperationFactory : IOperationFactory
    {
        private static readonly IResponseParser<bool> SuccessParser = new SuccessResponseParser();
        private static readonly IResponseParser<long> Int64Parser = new Int64ResponseParser();
        private static readonly IResponseParser<double> DoubleParser = new DoubleResponseParser();
        private static readonly IResponseParser<string> SimpleStringParser = new SimpleStringResponseParser();
        private static readonly IResponseParser<byte[]> BulkByteParser = new BulkByteResponseParser();
        private static readonly IResponseParser<byte[][]> BulkByteArrayParser = new BulkByteArrayResponseParser();

        public IOperation<bool> CreateSuccessOperation(RedisSocket socket)
        {
            return new Operation<bool>(socket, new OperationRequest(), new OperationResponse<bool>(SuccessParser));
        }

        public IOperation<long> CreateInt64Operation(RedisSocket socket)
        {
            return new Operation<long>(socket, new OperationRequest(), new OperationResponse<long>(Int64Parser));
        }

        public IOperation<double> CreateDoubleOperation(RedisSocket socket)
        {
            return new Operation<double>(socket, new OperationRequest(), new OperationResponse<double>(DoubleParser));
        }

        public IOperation<string> CreateStringOperation(RedisSocket socket)
        {
            return new Operation<string>(socket, new OperationRequest(), new OperationResponse<string>(SimpleStringParser));
        }

        public IOperation<byte[]> CreateDataOperation(RedisSocket socket)
        {
            return new Operation<byte[]>(socket, new OperationRequest(), new OperationResponse<byte[]>(BulkByteParser));
        }

        public IOperation<byte[][]> CreateMultiDataOperation(RedisSocket socket)
        {
            return new Operation<byte[][]>(socket, new OperationRequest(), new OperationResponse<byte[][]>(BulkByteArrayParser));
        }
    }
}
