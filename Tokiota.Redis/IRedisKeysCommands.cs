using System;

namespace Tokiota.Redis
{
    public interface IRedisKeysCommands
    {
        long Del(params string[] keys);
        bool Del(string key);
        byte[] Dump(string key);
        bool Exists(string key);
        bool Expire(string key, int seconds);
        bool ExpireAt(string key, int timestamp);
        byte[][] Keys(string pattern);
        void Migrate(string host, int port, string key, int destinationDb, long timeoutMs);
        bool Move(string key, int db);
        bool Persist(string key);
        bool PExpire(string key, long milliseconds);
        bool PExpireAt(string key, long millisecondsTimestamp);
        long PTtl(string key);
        string RandomKey();
        void Rename(string key, string newKey);
        bool RenameNx(string key, string newKey);
        byte[] Restore(string key, long ttl, byte[] value);
        byte[] Scan(ulong cursor, int count = 10, string match = null);
        byte[] Sort(string key, byte[][] args);
        long Ttl(string key);
        string Type(string key);
    }
}
