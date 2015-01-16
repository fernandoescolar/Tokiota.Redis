using System;

namespace Tokiota.Redis
{
    public interface IRedisHashesCommands
    {
        long HDel(string key, byte[] field);
        long HDel(string key, byte[][] fields);
        long HDel(string key, string field);
        long HDel(string key, string[] fields);
        long HExists(string key, byte[] field);
        long HExists(string key, string field);
        byte[] HGet(string key, byte[] field);
        byte[] HGet(string key, string field);
        byte[][] HGetAll(string key);
        string HGetString(string key, string field);
        long HIncrBy(string key, byte[] field, int increment);
        long HIncrBy(string key, byte[] field, long increment);
        double HIncrByFloat(string key, byte[] field, double increment);
        byte[][] HKeys(string key);
        string[] HKeyStrings(string key);
        long HLen(string key);
        byte[][] HMGet(string key, params byte[][] fields);
        byte[][] HMGet(string key, params string[] fields);
        string[] HMGetStrings(string key, params string[] fields);
        void HMSet(string key, byte[][] fields, byte[][] values);
        void HMSet(string key, string[] fields, byte[][] values);
        void HMSet(string key, string[] fields, string[] values);
        byte[] HScan(string key, ulong cursor, int count = 10, string match = null);
        long HSet(string key, byte[] field, byte[] value);
        long HSet(string key, string field, byte[] value);
        long HSet(string key, string field, string value);
        long HSetNx(string key, byte[] field, byte[] value);
        long HSetNx(string key, string field, byte[] value);
        long HSetNx(string key, string field, string value);
        byte[][] HVals(string key);
        string[] HValStrings(string key);
    }
}
