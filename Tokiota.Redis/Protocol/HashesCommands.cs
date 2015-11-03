namespace Tokiota.Redis.Protocol
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities;

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

        public Hashtable HGetAllHashtable(string key)
        {
            var result = new Hashtable();
            string lastKey = null;
            foreach (var value in this.HGetAll(key).ToUtf8Strings())
            {
                if (lastKey == null)
                {
                    lastKey = value;
                }
                else
                {
                    result[lastKey] = value;
                    lastKey = null;
                }
            }

            return result;
        }

        public Dictionary<string, string> HGetAllDictionary(string key)
        {
            var result = new Dictionary<string, string>();
            string lastKey = null;
            foreach (var value in this.HGetAll(key).ToUtf8Strings())
            {
                if (lastKey == null)
                {
                    lastKey = value;
                }
                else
                {
                    result[lastKey] = value;
                    lastKey = null;
                }
            }

            return result;
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
        public void HMSet(string key, Hashtable values)
        {
            this.HMSet(key, values.Keys.Cast<object>().Select(o => o.ToString()).ToArray(), values.Values.Cast<object>().Select(o => o.ToString()).ToArray());
        }
        public void HMSet(string key, Dictionary<string, string> values)
        {
            this.HMSet(key, values.Keys.ToArray(), values.Values.ToArray());
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

        public RedisScanResult HScan(string key, ulong cursor, int count = 10, string match = null)
        {

            if (key == null)
                throw new ArgumentNullException("key");

            if (string.IsNullOrEmpty(match))
                return this.Connection.SendExpectScanResult(Commands.HScan, key.ToByteArray(), cursor.ToString().ToByteArray(), Commands.Count, count.ToByteArray());

            return this.Connection.SendExpectScanResult(Commands.HScan, key.ToByteArray(), cursor.ToString().ToByteArray(), Commands.Count, count.ToByteArray(), Commands.Match, match.ToByteArray());
        }

        public byte[][] HScanLoop(string key, int count = 10, string match = null)
        {

            var result = new byte[0][];
            var data = new RedisScanResult(0, null);
            do
            {
                data = this.HScan(key, data.NextPage, count, match);

                var aux = new byte[result.Length + data.Data.Length][];
                result.CopyTo(aux, 0);
                data.Data.CopyTo(aux, result.Length);
                result = aux;
            } while (data.NextPage != 0);

            return result;
        }

        public string[] HScanLoopString(string key, int count = 10, string match = null)
        {
            return this.HScanLoop(key, count, match).ToUtf8Strings();
        }
    }
}
