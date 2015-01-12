
using System;

namespace Tokiota.Redis
{
    public interface IRedisClient : IDisposable
    {
        IRedisConnectionCommands Connection { get; }

        IRedisKeysCommands Keys { get; }

        IRedisStringsCommands Strings { get; }

        IRedisHashesCommands Hashes { get; }

        IRedisListsCommands Lists { get; }

        IRedisSetsCommands Sets { get; }
    }
}
