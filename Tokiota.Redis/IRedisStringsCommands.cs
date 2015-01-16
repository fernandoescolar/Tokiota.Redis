using System;

namespace Tokiota.Redis
{
    public interface IRedisStringsCommands
    {
        long Append(string key, byte[] value);
        long BitCount(string key);
        long Decr(string key);
        long DecrBy(string key, int count);
        long DecrBy(string key, long count);
        byte[] Get(string key);
        long GetBit(string key, int offset);
        byte[] GetRange(string key, int start, int end);
        string GetRangeString(string key, int start, int end);
        byte[] GetSet(string key, byte[] value);
        string GetSet(string key, string value);
        string GetString(string key);
        long Incr(string key);
        long IncrBy(string key, int count);
        long IncrBy(string key, long count);
        double IncrByFloat(string key, double increment);
        byte[][] MGet(params byte[][] keys);
        string[] MGet(params string[] keys);
        void MSet(byte[][] keys, byte[][] values);
        void MSet(string[] keys, string[] values);
        bool MSetNx(byte[][] keys, byte[][] values);
        bool MSetNx(string[] keys, string[] values);
        void PSetEx(string key, long milliseconds, byte[] value);
        void Set(string key, byte[] value);
        void Set(string key, byte[] value, int expirySeconds, long expiryMilliseconds = 0L);
        void Set(string key, string value);
        long SetBit(string key, int offset, int value);
        void SetEx(string key, int seconds, byte[] value);
        void SetEx(string key, int seconds, string value);
        long SetNx(string key, byte[] value);
        long SetNx(string key, string value);
        long SetRange(string key, int offset, byte[] value);
        long StrLen(string key);
    }
}
