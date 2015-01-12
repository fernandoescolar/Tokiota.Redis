using System;
using Tokiota.Redis.Net;
using Tokiota.Redis.Utilities;

namespace Tokiota.Redis.Protocol
{
    internal class ListsComponent : ComponentBase, IRedisListsCommands
    {
        public ListsComponent(IRedisConnection connection) : base(connection)
        {
        }

        public byte[][] BLPop(string listId, int timeOutSecs)
        {
            if (listId == null)
                throw new ArgumentNullException("listId");
            
            return this.Connection.SendExpectMultiData(Commands.BLPop, listId.ToByteArray(), timeOutSecs.ToByteArray());
        }

        public byte[][] BRPop(string listId, int timeOutSecs)
        {
            if (listId == null)
                throw new ArgumentNullException("listId");

            return this.Connection.SendExpectMultiData(Commands.BRPop, listId.ToByteArray(), timeOutSecs.ToByteArray());
        }

        public byte[] BRPopLPush(string fromListId, string toListId)
        {
            if (fromListId == null)
                throw new ArgumentNullException("fromListId");
            if (toListId == null)
                throw new ArgumentNullException("toListId");

            return this.Connection.SendExpectData(Commands.BRPopLPush, fromListId.ToByteArray(), toListId.ToByteArray());
        }

        public string BRPopLPushString(string fromListId, string toListId)
        {
            if (fromListId == null)
                throw new ArgumentNullException("fromListId");
            if (toListId == null)
                throw new ArgumentNullException("toListId");

            return this.Connection.SendExpectData(Commands.BRPopLPush, fromListId.ToByteArray(), toListId.ToByteArray()).ToUtf8String();
        }

        public byte[] LIndex(string listId, int listIndex)
        {
            if (listId == null)
                throw new ArgumentNullException("listId");

            return this.Connection.SendExpectData(Commands.LIndex, listId.ToByteArray(), listIndex.ToByteArray());
        }

        public string LIndexString(string listId, int listIndex)
        {
            if (listId == null)
                throw new ArgumentNullException("listId");

            return this.Connection.SendExpectData(Commands.LIndex, listId.ToByteArray(), listIndex.ToByteArray()).ToUtf8String();
        }

        public void LInsert(string listId, bool insertBefore, byte[] pivot, byte[] value)
        {
            if (listId == null)
                throw new ArgumentNullException("listId");

            var numArray = insertBefore ? Commands.Before : Commands.After;
            this.Connection.SendExpectSuccess(Commands.LInsert, listId.ToByteArray(), numArray, pivot, value);
        }

        public long LLen(string listId)
        {
            if (listId == null)
                throw new ArgumentNullException("listId");

            return this.Connection.SendExpectLong(Commands.LLen, listId.ToByteArray());
        }

        public byte[] LPop(string listId)
        {
            if (listId == null)
                throw new ArgumentNullException("listId");

            return this.Connection.SendExpectData(Commands.LPop, listId.ToByteArray());
        }

        public string LPopString(string listId)
        {
            if (listId == null)
                throw new ArgumentNullException("listId");

            return this.Connection.SendExpectData(Commands.LPop, listId.ToByteArray()).ToUtf8String();
        }

        public long LPush(string listId, byte[] value)
        {
            if (listId == null)
                throw new ArgumentNullException("listId");
            if (value == null)
                throw new ArgumentNullException("value");
            if (value.Length == 0)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.LPush, listId.ToByteArray(), value);
        }

        public long LPush(string listId, string value)
        {
            if (listId == null)
                throw new ArgumentNullException("listId");
            if (value == null)
                throw new ArgumentNullException("value");
            if (value.Length == 0)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.LPush, listId.ToByteArray(), value.ToByteArray());
        }


        public long LPushX(string listId, byte[] value)
        {
            if (listId == null)
                throw new ArgumentNullException("listId");
            if (value == null)
                throw new ArgumentNullException("value");
            if (value.Length == 0)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.LPushX, listId.ToByteArray(), value);
        }

        public long LPushX(string listId, string value)
        {
            if (listId == null)
                throw new ArgumentNullException("listId");
            if (value == null)
                throw new ArgumentNullException("value");
            if (value.Length == 0)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.LPushX, listId.ToByteArray(), value.ToByteArray());
        }

        public byte[][] LRange(string listId, int startingFrom, int endingAt)
        {
            if (listId == null)
                throw new ArgumentNullException("listId");

            return this.Connection.SendExpectMultiData(Commands.LRange, listId.ToByteArray(), startingFrom.ToByteArray(), endingAt.ToByteArray());
        }

        public long LRem(string listId, int removeNoOfMatches, byte[] value)
        {
            if (listId == null)
                throw new ArgumentNullException("listId");

            return this.Connection.SendExpectLong(Commands.LRem, listId.ToByteArray(), removeNoOfMatches.ToByteArray(), value);
        }

        public long LRem(string listId, int removeNoOfMatches, string value)
        {
            if (listId == null)
                throw new ArgumentNullException("listId");

            return this.Connection.SendExpectLong(Commands.LRem, listId.ToByteArray(), removeNoOfMatches.ToByteArray(), value.ToByteArray());
        }

        public void LSet(string listId, int listIndex, byte[] value)
        {
            if (listId == null)
                throw new ArgumentNullException("listId");

            this.Connection.SendExpectSuccess(Commands.LSet, listId.ToByteArray(), listIndex.ToByteArray(), value);
        }

        public void LSet(string listId, int listIndex, string value)
        {
            if (listId == null)
                throw new ArgumentNullException("listId");

            this.Connection.SendExpectSuccess(Commands.LSet, listId.ToByteArray(), listIndex.ToByteArray(), value.ToByteArray());
        }

        public void LTrim(string listId, int keepStartingFrom, int keepEndingAt)
        {
            if (listId == null)
                throw new ArgumentNullException("listId");

            this.Connection.SendExpectSuccess(Commands.LTrim, listId.ToByteArray(), keepStartingFrom.ToByteArray(), keepEndingAt.ToByteArray());
        }

        public byte[] RPop(string listId)
        {
            if (listId == null)
                throw new ArgumentNullException("listId");

            return this.Connection.SendExpectData(Commands.RPop, listId.ToByteArray());
        }

        public string RPopString(string listId)
        {
            if (listId == null)
                throw new ArgumentNullException("listId");

            return this.Connection.SendExpectData(Commands.RPop, listId.ToByteArray()).ToUtf8String();
        }

        public byte[] RPopLPush(string fromListId, string toListId)
        {
            if (fromListId == null)
                throw new ArgumentNullException("fromListId");
            if (toListId == null)
                throw new ArgumentNullException("toListId");

            return this.Connection.SendExpectData(Commands.RPopLPush, fromListId.ToByteArray(), toListId.ToByteArray());
        }

        public string RPopLPushString(string fromListId, string toListId)
        {
            if (fromListId == null)
                throw new ArgumentNullException("fromListId");
            if (toListId == null)
                throw new ArgumentNullException("toListId");

            return this.Connection.SendExpectData(Commands.RPopLPush, fromListId.ToByteArray(), toListId.ToByteArray()).ToUtf8String();
        }


        public long RPush(string listId, byte[][] values)
        {
            if (listId == null)
                throw new ArgumentNullException("listId");
            if (values == null)
                throw new ArgumentNullException("values");
            if (values.Length == 0)
                throw new ArgumentException("values");

            return this.Connection.SendExpectLong(Commands.RPush.Merge(listId.ToByteArray().Merge(values)));
        }

        public long RPush(string listId, string[] values)
        {
            if (listId == null)
                throw new ArgumentNullException("listId");
            if (values == null)
                throw new ArgumentNullException("values");
            if (values.Length == 0)
                throw new ArgumentException("values");

            return this.Connection.SendExpectLong(Commands.RPush.Merge(listId.ToByteArray().Merge(values)));
        }

        public long RPushX(string listId, byte[] value)
        {
            if (listId == null)
                throw new ArgumentNullException("listId");
            if (value == null)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.RPushX, listId.ToByteArray(), value);
        }

        public long RPushX(string listId, string value)
        {
            if (listId == null)
                throw new ArgumentNullException("listId");
            if (value == null)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.RPushX, listId.ToByteArray(), value.ToByteArray());
        }
    }
}
