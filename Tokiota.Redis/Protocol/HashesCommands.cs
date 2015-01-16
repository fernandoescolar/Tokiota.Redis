using System;
using Tokiota.Redis.Net;
using Tokiota.Redis.Utilities;

namespace Tokiota.Redis.Protocol
{
    internal class HashesCommands : CommandsBase, IRedisHashesCommands
    {
        public HashesCommands(IRedisConnection connection) : base(connection)
        {
        }

        public long HDel(string hashId, string key)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.HDel, hashId.ToByteArray(), key.ToByteArray());
        }

        public long HDel(string hashId, byte[] key)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.HDel, hashId.ToByteArray(), key);
        }

        public long HDel(string hashId, string[] keys)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");
            if (keys == null)
                throw new ArgumentNullException("keys");
            if (keys.Length == 0)
                throw new ArgumentException("keys");

            return this.Connection.SendExpectLong(Commands.HDel.Merge(hashId.ToByteArray().Merge(keys)));
        }

        public long HDel(string hashId, byte[][] keys)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");
            if (keys == null)
                throw new ArgumentNullException("keys");
            if (keys.Length == 0)
                throw new ArgumentException("keys");

            return this.Connection.SendExpectLong(Commands.HDel.Merge(hashId.ToByteArray().Merge(keys)));
        }

        public long HExists(string hashId, string key)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.HExists, hashId.ToByteArray(), key.ToByteArray());
        }

        public long HExists(string hashId, byte[] key)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.HExists, hashId.ToByteArray(), key);
        }

        public byte[] HGet(string hashId, string key)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectData(Commands.HGet, hashId.ToByteArray(), key.ToByteArray());
        }

        public byte[] HGet(string hashId, byte[] key)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectData(Commands.HGet, hashId.ToByteArray(), key);
        }

        public string HGetString(string hashId, string key)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectData(Commands.HGet, hashId.ToByteArray(), key.ToByteArray()).ToUtf8String();
        }

        public byte[][] HGetAll(string hashId)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");

            return this.Connection.SendExpectMultiData(Commands.HGetAll, hashId.ToByteArray());
        }

        public long HIncrBy(string hashId, byte[] key, int incrementBy)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.HIncrBy, hashId.ToByteArray(), key, incrementBy.ToByteArray());
        }

        public long HIncrBy(string hashId, byte[] key, long incrementBy)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.HIncrBy, hashId.ToByteArray(), key, incrementBy.ToByteArray());
        }

        public double HIncrByFloat(string hashId, byte[] key, double incrementBy)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectDouble(Commands.HIncrByFloat, hashId.ToByteArray(), key, incrementBy.ToByteArray());
        }

        public string[] HKeyStrings(string hashId)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");

            return this.Connection.SendExpectMultiData(Commands.HKeys, hashId.ToByteArray()).ToUtf8Strings();
        }

        public byte[][] HKeys(string hashId)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");

            return this.Connection.SendExpectMultiData(Commands.HKeys, hashId.ToByteArray());
        }

        public long HLen(string hashId)
        {
            if (string.IsNullOrEmpty(hashId))
                throw new ArgumentNullException("hashId");

            return this.Connection.SendExpectLong(Commands.HLen, hashId.ToByteArray());
        }

        public byte[][] HMGet(string hashId, params string[] keys)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");
            if (keys == null)
                throw new ArgumentNullException("keys");
            if (keys.Length == 0)
                throw new ArgumentNullException("keys");

            return this.Connection.SendExpectMultiData(Commands.HMGet.Merge(hashId.ToByteArray().Merge(keys)));
        }

        public byte[][] HMGet(string hashId, params byte[][] keys)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");
            if (keys == null)
                throw new ArgumentNullException("keys");
            if (keys.Length == 0)
                throw new ArgumentNullException("keys");

            return this.Connection.SendExpectMultiData(Commands.HMGet.Merge(hashId.ToByteArray().Merge(keys)));
        }

        public string[] HMGetStrings(string hashId, params string[] keys)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");
            if (keys == null)
                throw new ArgumentNullException("keys");
            if (keys.Length == 0)
                throw new ArgumentNullException("keys");

            return this.Connection.SendExpectMultiData(Commands.HMGet.Merge(hashId.ToByteArray().Merge(keys))).ToUtf8Strings();
        }

        public void HMSet(string hashId, byte[][] keys, byte[][] values)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");
            if (keys == null)
                throw new ArgumentNullException("keys");
            if (keys.Length == 0)
                throw new ArgumentNullException("keys");
            if (values == null)
                throw new ArgumentNullException("values");
            if (values.Length == 0)
                throw new ArgumentNullException("values");

            this.Connection.SendExpectSuccess(Commands.HMSet.Merge(hashId.ToByteArray().Combine(keys, values)));
        }

        public void HMSet(string hashId, string[] keys, byte[][] values)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");
            if (keys == null)
                throw new ArgumentNullException("keys");
            if (keys.Length == 0)
                throw new ArgumentNullException("keys");
            if (values == null)
                throw new ArgumentNullException("values");
            if (values.Length == 0)
                throw new ArgumentNullException("values");

            this.Connection.SendExpectSuccess(Commands.HMSet.Merge(hashId.ToByteArray().Combine(keys.ToByteArrays(), values)));
        }

        public void HMSet(string hashId, string[] keys, string[] values)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");
            if (keys == null)
                throw new ArgumentNullException("keys");
            if (keys.Length == 0)
                throw new ArgumentNullException("keys");
            if (values == null)
                throw new ArgumentNullException("values");
            if (values.Length == 0)
                throw new ArgumentNullException("values");

            this.Connection.SendExpectSuccess(Commands.HMSet.Merge(hashId.ToByteArray().Combine(keys.ToByteArrays(), values.ToByteArrays())));
        }

        public long HSet(string hashId, byte[] key, byte[] value)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");
            if (key == null)
                throw new ArgumentNullException("key");
            if (value == null)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.HSet, hashId.ToByteArray(), key, value);
        }

        public long HSet(string hashId, string key, byte[] value)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");
            if (key == null)
                throw new ArgumentNullException("key");
            if (value == null)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.HSet, hashId.ToByteArray(), key.ToByteArray(), value);
        }

        public long HSet(string hashId, string key, string value)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");
            if (key == null)
                throw new ArgumentNullException("key");
            if (value == null)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.HSet, hashId.ToByteArray(), key.ToByteArray(), value.ToByteArray());
        }

        public long HSetNx(string hashId, byte[] key, byte[] value)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");
            if (key == null)
                throw new ArgumentNullException("key");
            if (value == null)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.HSetNx, hashId.ToByteArray(), key, value);
        }

        public long HSetNx(string hashId, string key, byte[] value)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");
            if (key == null)
                throw new ArgumentNullException("key");
            if (value == null)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.HSetNx, hashId.ToByteArray(), key.ToByteArray(), value);
        }

        public long HSetNx(string hashId, string key, string value)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");
            if (key == null)
                throw new ArgumentNullException("key");
            if (value == null)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.HSetNx, hashId.ToByteArray(), key.ToByteArray(), value.ToByteArray());
        }

        public byte[][] HVals(string hashId)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");

            return this.Connection.SendExpectMultiData(Commands.HVals, hashId.ToByteArray());
        }

        public string[] HValStrings(string hashId)
        {
            if (hashId == null)
                throw new ArgumentNullException("hashId");

            return this.Connection.SendExpectMultiData(Commands.HVals, hashId.ToByteArray()).ToUtf8Strings();
        }

        public byte[] HScan(string hashId, ulong cursor, int count = 10, string match = null)
        {
            throw new NotImplementedException();
        }
    }
}
