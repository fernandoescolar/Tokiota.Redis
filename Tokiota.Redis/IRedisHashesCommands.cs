using System;

namespace Tokiota.Redis
{
    public interface IRedisHashesCommands
    {
        long HDel(string hashId, byte[] key);
        long HDel(string hashId, byte[][] keys);
        long HDel(string hashId, string key);
        long HDel(string hashId, string[] keys);
        long HExists(string hashId, byte[] key);
        long HExists(string hashId, string key);
        byte[] HGet(string hashId, byte[] key);
        byte[] HGet(string hashId, string key);
        byte[][] HGetAll(string hashId);
        string HGetString(string hashId, string key);
        int HIncrBy(string hashId, byte[] key, int incrementBy);
        long HIncrBy(string hashId, byte[] key, long incrementBy);
        double HIncrByFloat(string hashId, byte[] key, double incrementBy);
        byte[][] HKeys(string hashId);
        string[] HKeyStrings(string hashId);
        long HLen(string hashId);
        byte[][] HMGet(string hashId, params byte[][] keys);
        byte[][] HMGet(string hashId, params string[] keys);
        string[] HMGetStrings(string hashId, params string[] keys);
        void HMSet(string hashId, byte[][] keys, byte[][] values);
        void HMSet(string hashId, string[] keys, byte[][] values);
        void HMSet(string hashId, string[] keys, string[] values);
        byte[] HScan(string hashId, ulong cursor, int count = 10, string match = null);
        long HSet(string hashId, byte[] key, byte[] value);
        long HSet(string hashId, string key, byte[] value);
        long HSet(string hashId, string key, string value);
        long HSetNx(string hashId, byte[] key, byte[] value);
        long HSetNx(string hashId, string key, byte[] value);
        long HSetNx(string hashId, string key, string value);
        byte[][] HVals(string hashId);
        string[] HValStrings(string hashId);
    }
}
