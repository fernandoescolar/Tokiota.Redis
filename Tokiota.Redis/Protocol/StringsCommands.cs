using System;
using Tokiota.Redis.Net;
using Tokiota.Redis.Utilities;

namespace Tokiota.Redis.Protocol
{
    internal class StringsCommands : CommandsBase, IRedisStringsCommands
    {
        public StringsCommands(IRedisConnection connection)
            : base(connection)
        {
        }

        public long Append(string key, byte[] value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.Append, key.ToByteArray(), value);
        }

        public long BitCount(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.BitCount, key.ToByteArray());
        }

        public long Decr(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.Decr, key.ToByteArray());
        }

        public long DecrBy(string key, int count)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.DecrBy, key.ToByteArray(), count.ToByteArray());
        }

        public long DecrBy(string key, long count)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.DecrBy, key.ToByteArray(), count.ToByteArray());
        }

        public byte[] Get(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectData(Commands.Get, key.ToByteArray());
        }

        public string GetString(string key)
        {
            return this.Get(key).ToUtf8String();
        }

        public long GetBit(string key, int offset)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.GetBit, key.ToByteArray(), offset.ToByteArray());
        }

        private byte[] GetRange(string key, int min, int max)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");


            return this.Connection.SendExpectData(Commands.GetRange, key.ToByteArray(), min.ToByteArray(), max.ToByteArray());
        }

        private string GetRangeString(string key, int min, int max)
        {
            return this.GetRange(key, min, max).ToUtf8String();
        }

        public byte[] GetSet(string key, byte[] value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (value == null)
                throw new ArgumentNullException("value");

            if (value.Length > 1073741824)
                throw new ArgumentException("value exceeds 1G", "value");

            return this.Connection.SendExpectData(Commands.GetSet, key.ToByteArray(), value);
        }

        public string GetSet(string key, string value)
        {
            return this.GetSet(key, value.ToByteArray()).ToUtf8String();
        }

        public long Incr(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.Incr, key.ToByteArray());
        }

        public long IncrBy(string key, int count)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.IncrBy, key.ToByteArray(), count.ToByteArray());
        }

        public long IncrBy(string key, long count)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.IncrBy, key.ToByteArray(), count.ToByteArray());
        }

        public double IncrByFloat(string key, double incrBy)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectDouble(Commands.IncrByFloat, key.ToByteArray(), incrBy.ToByteArray());
        }

        public byte[][] MGet(params byte[][] keys)
        {
            if (keys == null)
                throw new ArgumentNullException("keys");

            if (keys.Length == 0)
                throw new ArgumentException("keys");

            return this.Connection.SendExpectMultiData(Commands.MGet.Merge(keys));
        }

        public string[] MGet(params string[] keys)
        {
            if (keys == null)
                throw new ArgumentNullException("keys");

            if (keys.Length == 0)
                throw new ArgumentException("keys");

            return this.Connection.SendExpectMultiData(Commands.MGet.Merge(keys)).ToUtf8Strings();
        }

        public void MSet(byte[][] keys, byte[][] values)
        {
            if (keys == null)
                throw new ArgumentNullException("keys");

            if (keys.Length == 0)
                throw new ArgumentException("keys");

            if (values == null)
                throw new ArgumentNullException("values");

            if (values.Length == 0)
                throw new ArgumentException("values");

            this.Connection.SendExpectSuccess(Commands.MSet.Combine(keys, values));
        }

        public void MSet(string[] keys, string[] values)
        {
            if (keys == null)
                throw new ArgumentNullException("keys");

            if (keys.Length == 0)
                throw new ArgumentException("keys");

            if (values == null)
                throw new ArgumentNullException("values");

            if (values.Length == 0)
                throw new ArgumentException("values");

            this.Connection.SendExpectSuccess(Commands.MSet.Combine(keys.ToByteArrays(), values.ToByteArrays()));
        }

        public bool MSetNx(byte[][] keys, byte[][] values)
        {
            if (keys == null)
                throw new ArgumentNullException("keys");

            if (keys.Length == 0)
                throw new ArgumentException("keys");

            if (values == null)
                throw new ArgumentNullException("values");

            if (values.Length == 0)
                throw new ArgumentException("values");

            return this.Connection.SendExpectLong(Commands.MSet.Combine(keys, values)) == 1;
        }

        public bool MSetNx(string[] keys, string[] values)
        {
            if (keys == null)
                throw new ArgumentNullException("keys");

            if (keys.Length == 0)
                throw new ArgumentException("keys");

            if (values == null)
                throw new ArgumentNullException("values");

            if (values.Length == 0)
                throw new ArgumentException("values");

            return this.Connection.SendExpectLong(Commands.MSet.Combine(keys.ToByteArrays(), values.ToByteArrays())) == 1;
        }

        public void PSetEx(string key, long expireInMs, byte[] value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            this.Connection.SendExpectSuccess(Commands.PSetEx, key.ToByteArray(), expireInMs.ToByteArray(), value);
        }

        public void Set(string key, string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            this.Set(key, value.ToByteArray());
        }

        public void Set(string key, byte[] value)
        {
            this.Set(key, value, 0);
        }

        public void Set(string key, byte[] value, int expirySeconds, long expiryMs = 0L)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (value == null)
                throw new ArgumentNullException("value");
            if (value.Length > 1073741824)
                throw new ArgumentException("value exceeds 1G", "value");

            if (expirySeconds > 0)
                this.Connection.SendExpectSuccess(Commands.Set, key.ToByteArray(), value, Commands.Ex, expirySeconds.ToByteArray());
            else if (expiryMs > 0L)
                this.Connection.SendExpectSuccess(Commands.Set, key.ToByteArray(), value, Commands.Px, expiryMs.ToByteArray());
            else
                this.Connection.SendExpectSuccess(Commands.Set, key.ToByteArray(), value);
        }

        public long SetBit(string key, int offset, int value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (value > 1 || value < 0)
                throw new ArgumentException("value is out of range");

            return this.Connection.SendExpectLong(Commands.SetBit, key.ToByteArray(), offset.ToByteArray(), value.ToByteArray());
        }

        public void SetEx(string key, int expireInSeconds, byte[] value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (value == null)
                throw new ArgumentNullException("value");

            if (value.Length > 1073741824)
                throw new ArgumentException("value exceeds 1G", "value");

            this.Connection.SendExpectSuccess(Commands.SetEx, key.ToByteArray(), expireInSeconds.ToByteArray(), value);
        }

        public void SetEx(string key, int expireInSeconds, string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            this.SetEx(key, expireInSeconds, value.ToByteArray());
        }

        public long SetNx(string key, byte[] value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (value == null)
                throw new ArgumentNullException("value");

            if (value.Length > 1073741824)
                throw new ArgumentException("value exceeds 1G", "value");

            return this.Connection.SendExpectLong(Commands.SetNx, key.ToByteArray(), value);
        }

        public long SetNx(string key, string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            return this.SetNx(key, value.ToByteArray());
        }

        public long SetRange(string key, int offset, byte[] value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.SetRange, key.ToByteArray(), offset.ToByteArray(), value);
        }

        public long StrLen(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.StrLen, key.ToByteArray());
        }
    }
}
