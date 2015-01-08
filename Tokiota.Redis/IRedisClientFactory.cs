using System;

namespace Tokiota.Redis
{
    public interface IRedisClientFactory : IDisposable
    {
        IRedisClient GetCurrent();
    }
}
