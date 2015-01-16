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

        public long HDel(string key, string field)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (field == null)
                throw new ArgumentNullException("field");

            return this.Connection.SendExpectLong(Commands.HDel, key.ToByteArray(), field.ToByteArray());
        }

        public long HDel(string key, byte[] field)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (field == null)
                throw new ArgumentNullException("field");

            return this.Connection.SendExpectLong(Commands.HDel, key.ToByteArray(), field);
        }

        public long HDel(string key, string[] fields)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (fields == null)
                throw new ArgumentNullException("fields");
            if (fields.Length == 0)
                throw new ArgumentException("fields");

            return this.Connection.SendExpectLong(Commands.HDel.Merge(key.ToByteArray().Merge(fields)));
        }

        public long HDel(string key, byte[][] fields)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (fields == null)
                throw new ArgumentNullException("fields");
            if (fields.Length == 0)
                throw new ArgumentException("fields");

            return this.Connection.SendExpectLong(Commands.HDel.Merge(key.ToByteArray().Merge(fields)));
        }

        public long HExists(string key, string field)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (field == null)
                throw new ArgumentNullException("field");

            return this.Connection.SendExpectLong(Commands.HExists, key.ToByteArray(), field.ToByteArray());
        }

        public long HExists(string key, byte[] field)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (field == null)
                throw new ArgumentNullException("field");

            return this.Connection.SendExpectLong(Commands.HExists, key.ToByteArray(), field);
        }

        public byte[] HGet(string key, string field)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (field == null)
                throw new ArgumentNullException("field");

            return this.Connection.SendExpectData(Commands.HGet, key.ToByteArray(), field.ToByteArray());
        }

        public byte[] HGet(string key, byte[] field)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (field == null)
                throw new ArgumentNullException("field");

            return this.Connection.SendExpectData(Commands.HGet, key.ToByteArray(), field);
        }

        public string HGetString(string key, string field)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (field == null)
                throw new ArgumentNullException("field");

            return this.Connection.SendExpectData(Commands.HGet, key.ToByteArray(), field.ToByteArray()).ToUtf8String();
        }

        public byte[][] HGetAll(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectMultiData(Commands.HGetAll, key.ToByteArray());
        }

        public long HIncrBy(string key, byte[] field, int increment)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (field == null)
                throw new ArgumentNullException("field");

            return this.Connection.SendExpectLong(Commands.HIncrBy, key.ToByteArray(), field, increment.ToByteArray());
        }

        public long HIncrBy(string key, byte[] field, long increment)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (field == null)
                throw new ArgumentNullException("field");

            return this.Connection.SendExpectLong(Commands.HIncrBy, key.ToByteArray(), field, increment.ToByteArray());
        }

        public double HIncrByFloat(string key, byte[] field, double increment)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (field == null)
                throw new ArgumentNullException("field");

            return this.Connection.SendExpectDouble(Commands.HIncrByFloat, key.ToByteArray(), field, increment.ToByteArray());
        }

        public string[] HKeyStrings(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectMultiData(Commands.HKeys, key.ToByteArray()).ToUtf8Strings();
        }

        public byte[][] HKeys(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectMultiData(Commands.HKeys, key.ToByteArray());
        }

        public long HLen(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.HLen, key.ToByteArray());
        }

        public byte[][] HMGet(string key, params string[] fields)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (fields == null)
                throw new ArgumentNullException("fields");
            if (fields.Length == 0)
                throw new ArgumentNullException("fields");

            return this.Connection.SendExpectMultiData(Commands.HMGet.Merge(key.ToByteArray().Merge(fields)));
        }

        public byte[][] HMGet(string key, params byte[][] fields)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (fields == null)
                throw new ArgumentNullException("fields");
            if (fields.Length == 0)
                throw new ArgumentNullException("fields");

            return this.Connection.SendExpectMultiData(Commands.HMGet.Merge(key.ToByteArray().Merge(fields)));
        }

        public string[] HMGetStrings(string key, params string[] fields)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (fields == null)
                throw new ArgumentNullException("fields");
            if (fields.Length == 0)
                throw new ArgumentNullException("fields");

            return this.Connection.SendExpectMultiData(Commands.HMGet.Merge(key.ToByteArray().Merge(fields))).ToUtf8Strings();
        }

        public void HMSet(string key, byte[][] fields, byte[][] values)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (fields == null)
                throw new ArgumentNullException("fields");
            if (fields.Length == 0)
                throw new ArgumentNullException("fields");
            if (values == null)
                throw new ArgumentNullException("values");
            if (values.Length == 0)
                throw new ArgumentNullException("values");

            this.Connection.SendExpectSuccess(Commands.HMSet.Merge(key.ToByteArray().Combine(fields, values)));
        }

        public void HMSet(string key, string[] fields, byte[][] values)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (fields == null)
                throw new ArgumentNullException("fields");
            if (fields.Length == 0)
                throw new ArgumentNullException("fields");
            if (values == null)
                throw new ArgumentNullException("values");
            if (values.Length == 0)
                throw new ArgumentNullException("values");

            this.Connection.SendExpectSuccess(Commands.HMSet.Merge(key.ToByteArray().Combine(fields.ToByteArrays(), values)));
        }

        public void HMSet(string key, string[] fields, string[] values)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (fields == null)
                throw new ArgumentNullException("fields");
            if (fields.Length == 0)
                throw new ArgumentNullException("fields");
            if (values == null)
                throw new ArgumentNullException("values");
            if (values.Length == 0)
                throw new ArgumentNullException("values");

            this.Connection.SendExpectSuccess(Commands.HMSet.Merge(key.ToByteArray().Combine(fields.ToByteArrays(), values.ToByteArrays())));
        }

        public long HSet(string key, byte[] field, byte[] value)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (field == null)
                throw new ArgumentNullException("field");
            if (value == null)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.HSet, key.ToByteArray(), field, value);
        }

        public long HSet(string key, string field, byte[] value)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (field == null)
                throw new ArgumentNullException("field");
            if (value == null)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.HSet, key.ToByteArray(), field.ToByteArray(), value);
        }

        public long HSet(string key, string field, string value)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (field == null)
                throw new ArgumentNullException("field");
            if (value == null)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.HSet, key.ToByteArray(), field.ToByteArray(), value.ToByteArray());
        }

        public long HSetNx(string key, byte[] field, byte[] value)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (field == null)
                throw new ArgumentNullException("field");
            if (value == null)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.HSetNx, key.ToByteArray(), field, value);
        }

        public long HSetNx(string key, string field, byte[] value)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (field == null)
                throw new ArgumentNullException("field");
            if (value == null)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.HSetNx, key.ToByteArray(), field.ToByteArray(), value);
        }

        public long HSetNx(string key, string field, string value)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (field == null)
                throw new ArgumentNullException("field");
            if (value == null)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.HSetNx, key.ToByteArray(), field.ToByteArray(), value.ToByteArray());
        }

        public byte[][] HVals(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectMultiData(Commands.HVals, key.ToByteArray());
        }

        public string[] HValStrings(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectMultiData(Commands.HVals, key.ToByteArray()).ToUtf8Strings();
        }

        public byte[] HScan(string key, ulong cursor, int count = 10, string match = null)
        {
            throw new NotImplementedException();
        }
    }
}
