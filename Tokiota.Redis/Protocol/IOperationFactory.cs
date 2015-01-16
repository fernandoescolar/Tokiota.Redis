using Tokiota.Redis.Net;

namespace Tokiota.Redis.Protocol
{
    internal interface IOperationFactory
    {
        IOperation<byte[]> CreateDataOperation(RedisSocket socket);
        IOperation<double> CreateDoubleOperation(RedisSocket socket);
        IOperation<long> CreateInt64Operation(RedisSocket socket);
        IOperation<byte[][]> CreateMultiDataOperation(RedisSocket socket);
        IOperation<string> CreateStringOperation(RedisSocket socket);
        IOperation<bool> CreateSuccessOperation(RedisSocket socket);
    }
}
