namespace Tokiota.Redis
{
    using System;

    public interface IRedisClientFactory : IDisposable
    {
        IRedisClient GetCurrent();
    }
}
