using System;
using System.Collections.Generic;
using Tokiota.Redis.Net;
using Tokiota.Redis.Utilities;

namespace Tokiota.Redis.Protocol
{
    internal class SetsCommands : CommandsBase, IRedisSetsCommands
    {
        public SetsCommands(IRedisConnection conneciton) : base(conneciton)
        {
        }

        public long SAdd(string setId, byte[] value)
        {
            if (setId == null)
                throw new ArgumentNullException("setId");
            if (value == null)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.SAdd, setId.ToByteArray(), value);
        }

        public long SAdd(string setId, string value)
        {
            if (setId == null)
                throw new ArgumentNullException("setId");
            if (value == null)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.SAdd, setId.ToByteArray(), value.ToByteArray());
        }

        public long SAdd(string setId, byte[][] values)
        {
            if (setId == null)
                throw new ArgumentNullException("setId");
            if (values == null)
                throw new ArgumentNullException("values");
            if (values.Length == 0)
                throw new ArgumentException("values");

            return this.Connection.SendExpectLong(Commands.SAdd.Merge(setId.ToByteArray().Merge(values)));
        }

        public long SAdd(string setId, string[] values)
        {
            if (setId == null)
                throw new ArgumentNullException("setId");
            if (values == null)
                throw new ArgumentNullException("values");
            if (values.Length == 0)
                throw new ArgumentException("values");

            return this.Connection.SendExpectLong(Commands.SAdd.Merge(setId.ToByteArray().Merge(values)));
        }

        public long SCard(string setId)
        {
            if (setId == null)
                throw new ArgumentNullException("setId");

            return this.Connection.SendExpectLong(Commands.SCard, setId.ToByteArray());
        }

        public byte[][] SDiff(string fromSetId, params string[] withSetIds)
        {
            if (fromSetId == null)
                throw new ArgumentNullException("fromSetId");
            if (withSetIds == null)
                throw new ArgumentNullException("withSetIds");

            return this.Connection.SendExpectMultiData(Commands.SDiff.Merge(fromSetId.ToByteArray().Merge(withSetIds)));
        }

        public string[] SDiffString(string fromSetId, params string[] withSetIds)
        {
            if (fromSetId == null)
                throw new ArgumentNullException("fromSetId");
            if (withSetIds == null)
                throw new ArgumentNullException("withSetIds");

            return this.Connection.SendExpectMultiData(Commands.SDiff.Merge(fromSetId.ToByteArray().Merge(withSetIds))).ToUtf8Strings();
        }

        public void SDiffStore(string intoSetId, string fromSetId, params string[] withSetIds)
        {
            if (intoSetId == null)
                throw new ArgumentNullException("intoSetId");
            if (fromSetId == null)
                throw new ArgumentNullException("fromSetId");
            if (withSetIds == null)
                throw new ArgumentNullException("withSetIds");
            
            var list = new List<string>(withSetIds);
            list.Insert(0, fromSetId);
            list.Insert(0, intoSetId);

            this.Connection.SendExpectSuccess(Commands.SDiffStore.Merge(list.ToArray()));
        }

        public byte[][] SInter(params string[] setIds)
        {
            if (setIds == null)
                throw new ArgumentNullException("setIds");

            return this.Connection.SendExpectMultiData(Commands.SInter.Merge(setIds));
        }

        public string[] SInterStrings(params string[] setIds)
        {
            if (setIds == null)
                throw new ArgumentNullException("setIds");

            return this.Connection.SendExpectMultiData(Commands.SInter.Merge(setIds)).ToUtf8Strings();
        }

        public void SInterStore(string intoSetId, params string[] setIds)
        {
            if (intoSetId == null)
                throw new ArgumentNullException("intoSetId");
            if (setIds == null)
                throw new ArgumentNullException("setIds");

            this.Connection.SendExpectSuccess(Commands.SInterStore.Merge(intoSetId.ToByteArray().Merge(setIds)));
        }

        public long SIsMember(string setId, byte[] value)
        {
            if (setId == null)
                throw new ArgumentNullException("setId");
            if (value == null)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.SIsMember, setId.ToByteArray(), value);
        }

        public long SIsMember(string setId, string value)
        {
            if (setId == null)
                throw new ArgumentNullException("setId");
            if (value == null)
                throw new ArgumentNullException("value");

            return this.Connection.SendExpectLong(Commands.SIsMember, setId.ToByteArray(), value.ToByteArray());
        }

        public byte[][] SMembers(string setId)
        {
            if (setId == null)
                throw new ArgumentNullException("setId");

            return this.Connection.SendExpectMultiData(Commands.SMembers, setId.ToByteArray());
        }

        public string[] SMemberStrings(string setId)
        {
            if (setId == null)
                throw new ArgumentNullException("setId");

            return this.Connection.SendExpectMultiData(Commands.SMembers, setId.ToByteArray()).ToUtf8Strings();
        }

        public void SMove(string fromSetId, string toSetId, byte[] value)
        {
            if (fromSetId == null)
                throw new ArgumentNullException("fromSetId");
            if (toSetId == null)
                throw new ArgumentNullException("toSetId");
            if (value == null)
                throw new ArgumentNullException("value");

            this.Connection.SendExpectSuccess(Commands.SMove, fromSetId.ToByteArray(), toSetId.ToByteArray(), value);
        }

        public void SMove(string fromSetId, string toSetId, string value)
        {
            if (fromSetId == null)
                throw new ArgumentNullException("fromSetId");
            if (toSetId == null)
                throw new ArgumentNullException("toSetId");
            if (value == null)
                throw new ArgumentNullException("value");

            this.Connection.SendExpectSuccess(Commands.SMove, fromSetId.ToByteArray(), toSetId.ToByteArray(), value.ToByteArray());
        }

        public byte[] SPop(string setId)
        {
            if (setId == null)
                throw new ArgumentNullException("setId");

            return this.Connection.SendExpectData(Commands.SPop, setId.ToByteArray());
        }

        public string SPopString(string setId)
        {
            if (setId == null)
                throw new ArgumentNullException("setId");

            return this.Connection.SendExpectData(Commands.SPop, setId.ToByteArray()).ToUtf8String();
        }

        public byte[] SRandMember(string setId)
        {
            if (setId == null)
                throw new ArgumentNullException("setId");

            return this.Connection.SendExpectData(Commands.SRandMember, setId.ToByteArray());
        }

        public byte[][] SRandMember(string setId, int count)
        {
            if (setId == null)
                throw new ArgumentNullException("setId");

            return this.Connection.SendExpectMultiData(Commands.SRandMember, setId.ToByteArray(), count.ToByteArray());
        }

        public string SRandMemberString(string setId)
        {
            if (setId == null)
                throw new ArgumentNullException("setId");

            return this.Connection.SendExpectData(Commands.SRandMember, setId.ToByteArray()).ToUtf8String();
        }

        public string[] SRandMemberString(string setId, int count)
        {
            if (setId == null)
                throw new ArgumentNullException("setId");

            return this.Connection.SendExpectMultiData(Commands.SRandMember, setId.ToByteArray(), count.ToByteArray()).ToUtf8Strings();
        }

        public long SRem(string setId, byte[][] values)
        {
            if (setId == null)
                throw new ArgumentNullException("setId");
            if (values == null)
                throw new ArgumentNullException("values");
            if (values.Length == 0)
                throw new ArgumentException("values");

            return this.Connection.SendExpectLong(Commands.SRem.Merge(setId.ToByteArray().Merge(values)));
        }

        public long SRem(string setId, string[] values)
        {
            if (setId == null)
                throw new ArgumentNullException("setId");
            if (values == null)
                throw new ArgumentNullException("values");
            if (values.Length == 0)
                throw new ArgumentException("values");

            return this.Connection.SendExpectLong(Commands.SRem.Merge(setId.ToByteArray().Merge(values)));
        }

        public byte[][] SUnion(params string[] setIds)
        {
            if (setIds == null)
                throw new ArgumentNullException("setIds");
            if (setIds.Length == 0)
                throw new ArgumentNullException("setIds");

            return this.Connection.SendExpectMultiData(Commands.SUnion.Merge(setIds));
        }

        public string[] SUnionStrings(params string[] setIds)
        {
            if (setIds == null)
                throw new ArgumentNullException("setIds");
            if (setIds.Length == 0)
                throw new ArgumentNullException("setIds");

            return this.Connection.SendExpectMultiData(Commands.SUnion.Merge(setIds)).ToUtf8Strings();
        }

        public void SUnionStore(string intoSetId, params string[] setIds)
        {
            if (intoSetId == null)
                throw new ArgumentNullException("intoSetId");
            if (setIds == null)
                throw new ArgumentNullException("setIds");
            if (setIds.Length == 0)
                throw new ArgumentNullException("setIds");

            this.Connection.SendExpectSuccess(Commands.SUnionStore.Merge(intoSetId.ToByteArray().Merge(setIds)));
        }

        public byte[][] SScan(string setId, ulong cursor, int count = 10, string match = null)
        {
            throw new NotImplementedException();
        }
    }
}
