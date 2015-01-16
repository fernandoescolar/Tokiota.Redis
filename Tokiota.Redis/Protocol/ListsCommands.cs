using System;
using Tokiota.Redis.Net;
using Tokiota.Redis.Utilities;

namespace Tokiota.Redis.Protocol
{
    internal class ListsCommands : CommandsBase, IRedisListsCommands
    {
        public ListsCommands(IRedisConnection connection) : base(connection)
        {
        }

        public byte[][] BLPop(string key, int timeout)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            
            return this.Connection.SendExpectMultiData(Commands.BLPop, key.ToByteArray(), timeout.ToByteArray());
        }

        public byte[][] BRPop(string key, int timeout)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectMultiData(Commands.BRPop, key.ToByteArray(), timeout.ToByteArray());
        }

        public byte[] BRPopLPush(string source, string target)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (target == null)
                throw new ArgumentNullException("target");

            return this.Connection.SendExpectData(Commands.BRPopLPush, source.ToByteArray(), target.ToByteArray());
        }

        public string BRPopLPushString(string source, string target)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (target == null)
                throw new ArgumentNullException("target");

            return this.Connection.SendExpectData(Commands.BRPopLPush, source.ToByteArray(), target.ToByteArray()).ToUtf8String();
        }

        public byte[] LIndex(string key, int index)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectData(Commands.LIndex, key.ToByteArray(), index.ToByteArray());
        }

        public string LIndexString(string key, int index)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectData(Commands.LIndex, key.ToByteArray(), index.ToByteArray()).ToUtf8String();
        }

        public void LInsert(string key, bool insertBefore, byte[] pivot, byte[] value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            var numArray = insertBefore ? Commands.Before : Commands.After;
            this.Connection.SendExpectSuccess(Commands.LInsert, key.ToByteArray(), numArray, pivot, value);
        }

        public long LLen(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.LLen, key.ToByteArray());
        }

        public byte[] LPop(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectData(Commands.LPop, key.ToByteArray());
        }

        public string LPopString(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectData(Commands.LPop, key.ToByteArray()).ToUtf8String();
        }

        public long LPush(string key, byte[] value)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (value == null)
                throw new ArgumentNullException("value");
            if (value.Length == 0)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.LPush, key.ToByteArray(), value);
        }

        public long LPush(string key, string value)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (value == null)
                throw new ArgumentNullException("value");
            if (value.Length == 0)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.LPush, key.ToByteArray(), value.ToByteArray());
        }


        public long LPushX(string key, byte[] value)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (value == null)
                throw new ArgumentNullException("value");
            if (value.Length == 0)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.LPushX, key.ToByteArray(), value);
        }

        public long LPushX(string key, string value)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (value == null)
                throw new ArgumentNullException("value");
            if (value.Length == 0)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.LPushX, key.ToByteArray(), value.ToByteArray());
        }

        public byte[][] LRange(string key, int start, int stop)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectMultiData(Commands.LRange, key.ToByteArray(), start.ToByteArray(), stop.ToByteArray());
        }

        public long LRem(string key, int count, byte[] value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.LRem, key.ToByteArray(), count.ToByteArray(), value);
        }

        public long LRem(string key, int count, string value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.LRem, key.ToByteArray(), count.ToByteArray(), value.ToByteArray());
        }

        public void LSet(string key, int index, byte[] value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            this.Connection.SendExpectSuccess(Commands.LSet, key.ToByteArray(), index.ToByteArray(), value);
        }

        public void LSet(string key, int index, string value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            this.Connection.SendExpectSuccess(Commands.LSet, key.ToByteArray(), index.ToByteArray(), value.ToByteArray());
        }

        public void LTrim(string key, int start, int stop)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            this.Connection.SendExpectSuccess(Commands.LTrim, key.ToByteArray(), start.ToByteArray(), stop.ToByteArray());
        }

        public byte[] RPop(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectData(Commands.RPop, key.ToByteArray());
        }

        public string RPopString(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectData(Commands.RPop, key.ToByteArray()).ToUtf8String();
        }

        public byte[] RPopLPush(string source, string target)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (target == null)
                throw new ArgumentNullException("target");

            return this.Connection.SendExpectData(Commands.RPopLPush, source.ToByteArray(), target.ToByteArray());
        }

        public string RPopLPushString(string source, string target)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (target == null)
                throw new ArgumentNullException("target");

            return this.Connection.SendExpectData(Commands.RPopLPush, source.ToByteArray(), target.ToByteArray()).ToUtf8String();
        }


        public long RPush(string key, byte[][] values)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (values == null)
                throw new ArgumentNullException("values");
            if (values.Length == 0)
                throw new ArgumentException("values");

            return this.Connection.SendExpectLong(Commands.RPush.Merge(key.ToByteArray().Merge(values)));
        }

        public long RPush(string key, string[] values)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (values == null)
                throw new ArgumentNullException("values");
            if (values.Length == 0)
                throw new ArgumentException("values");

            return this.Connection.SendExpectLong(Commands.RPush.Merge(key.ToByteArray().Merge(values)));
        }

        public long RPushX(string key, byte[] value)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (value == null)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.RPushX, key.ToByteArray(), value);
        }

        public long RPushX(string key, string value)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (value == null)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.RPushX, key.ToByteArray(), value.ToByteArray());
        }
    }
}
