namespace Tokiota.Redis.Protocol
{
    internal interface IOperation<T>
    {
        T Execute(params byte[][] commands);
    }
}
