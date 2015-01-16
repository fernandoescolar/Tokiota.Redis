using System;

namespace Tokiota.Redis
{
    public interface IRedisKeysCommands
    {
        long Del(params string[] args);
        bool Del(string key);
        byte[] Dump(string key);
        bool Exists(string key);
        bool Expire(string key, int seconds);
        bool ExpireAt(string key, int time);
        byte[][] Keys(string pattern);
        void Migrate(string host, int port, string key, int destinationDb, long timeoutMs);
        bool Move(string key, int db);
        bool Persist(string key);
        bool PExpire(string key, long ttlMs);
        bool PExpireAt(string key, long unixTimeMs);
        long PTtl(string key);
        string RandomKey();
        void Rename(string oldKeyname, string newKeyname);
        bool RenameNx(string oldKeyname, string newKeyname);
        byte[] Restore(string key, long expireMs, byte[] dumpValue);
        byte[] Scan(ulong cursor, int count = 10, string match = null);
        byte[] Sort(string key, byte[][] ags);
        long Ttl(string key);
        string Type(string key);
    }
}
