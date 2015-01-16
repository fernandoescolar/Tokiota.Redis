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

        public long SAdd(string key, byte[] member)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (member == null)
                throw new ArgumentNullException("member");

            return this.Connection.SendExpectLong(Commands.SAdd, key.ToByteArray(), member);
        }

        public long SAdd(string key, string member)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (member == null)
                throw new ArgumentNullException("member");

            return this.Connection.SendExpectLong(Commands.SAdd, key.ToByteArray(), member.ToByteArray());
        }

        public long SAdd(string key, byte[][] members)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (members == null)
                throw new ArgumentNullException("members");
            if (members.Length == 0)
                throw new ArgumentException("members");

            return this.Connection.SendExpectLong(Commands.SAdd.Merge(key.ToByteArray().Merge(members)));
        }

        public long SAdd(string key, string[] members)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (members == null)
                throw new ArgumentNullException("members");
            if (members.Length == 0)
                throw new ArgumentException("members");

            return this.Connection.SendExpectLong(Commands.SAdd.Merge(key.ToByteArray().Merge(members)));
        }

        public long SCard(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.SCard, key.ToByteArray());
        }

        public byte[][] SDiff(string fromKey, params string[] withKeys)
        {
            if (fromKey == null)
                throw new ArgumentNullException("fromKey");
            if (withKeys == null)
                throw new ArgumentNullException("withKeys");

            return this.Connection.SendExpectMultiData(Commands.SDiff.Merge(fromKey.ToByteArray().Merge(withKeys)));
        }

        public string[] SDiffString(string fromKey, params string[] withKeys)
        {
            if (fromKey == null)
                throw new ArgumentNullException("fromKey");
            if (withKeys == null)
                throw new ArgumentNullException("withKeys");

            return this.Connection.SendExpectMultiData(Commands.SDiff.Merge(fromKey.ToByteArray().Merge(withKeys))).ToUtf8Strings();
        }

        public void SDiffStore(string intoKey, string fromKey, params string[] withKeys)
        {
            if (intoKey == null)
                throw new ArgumentNullException("intoKey");
            if (fromKey == null)
                throw new ArgumentNullException("fromKey");
            if (withKeys == null)
                throw new ArgumentNullException("withKeys");
            
            var list = new List<string>(withKeys);
            list.Insert(0, fromKey);
            list.Insert(0, intoKey);

            this.Connection.SendExpectSuccess(Commands.SDiffStore.Merge(list.ToArray()));
        }

        public byte[][] SInter(params string[] keys)
        {
            if (keys == null)
                throw new ArgumentNullException("keys");

            return this.Connection.SendExpectMultiData(Commands.SInter.Merge(keys));
        }

        public string[] SInterStrings(params string[] keys)
        {
            if (keys == null)
                throw new ArgumentNullException("keys");

            return this.Connection.SendExpectMultiData(Commands.SInter.Merge(keys)).ToUtf8Strings();
        }

        public void SInterStore(string intoKey, params string[] keys)
        {
            if (intoKey == null)
                throw new ArgumentNullException("intoKey");
            if (keys == null)
                throw new ArgumentNullException("keys");

            this.Connection.SendExpectSuccess(Commands.SInterStore.Merge(intoKey.ToByteArray().Merge(keys)));
        }

        public long SIsMember(string key, byte[] member)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (member == null)
                throw new ArgumentNullException("member");

            return this.Connection.SendExpectLong(Commands.SIsMember, key.ToByteArray(), member);
        }

        public long SIsMember(string key, string member)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (member == null)
                throw new ArgumentNullException("member");

            return this.Connection.SendExpectLong(Commands.SIsMember, key.ToByteArray(), member.ToByteArray());
        }

        public byte[][] SMembers(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectMultiData(Commands.SMembers, key.ToByteArray());
        }

        public string[] SMemberStrings(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectMultiData(Commands.SMembers, key.ToByteArray()).ToUtf8Strings();
        }

        public void SMove(string fromKey, string toKey, byte[] member)
        {
            if (fromKey == null)
                throw new ArgumentNullException("fromKey");
            if (toKey == null)
                throw new ArgumentNullException("toKey");
            if (member == null)
                throw new ArgumentNullException("member");

            this.Connection.SendExpectSuccess(Commands.SMove, fromKey.ToByteArray(), toKey.ToByteArray(), member);
        }

        public void SMove(string fromKey, string toKey, string member)
        {
            if (fromKey == null)
                throw new ArgumentNullException("fromKey");
            if (toKey == null)
                throw new ArgumentNullException("toKey");
            if (member == null)
                throw new ArgumentNullException("member");

            this.Connection.SendExpectSuccess(Commands.SMove, fromKey.ToByteArray(), toKey.ToByteArray(), member.ToByteArray());
        }

        public byte[] SPop(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectData(Commands.SPop, key.ToByteArray());
        }

        public string SPopString(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectData(Commands.SPop, key.ToByteArray()).ToUtf8String();
        }

        public byte[] SRandMember(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectData(Commands.SRandMember, key.ToByteArray());
        }

        public byte[][] SRandMember(string key, int count)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectMultiData(Commands.SRandMember, key.ToByteArray(), count.ToByteArray());
        }

        public string SRandMemberString(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectData(Commands.SRandMember, key.ToByteArray()).ToUtf8String();
        }

        public string[] SRandMemberString(string key, int count)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectMultiData(Commands.SRandMember, key.ToByteArray(), count.ToByteArray()).ToUtf8Strings();
        }

        public long SRem(string key, byte[][] members)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (members == null)
                throw new ArgumentNullException("members");
            if (members.Length == 0)
                throw new ArgumentException("members");

            return this.Connection.SendExpectLong(Commands.SRem.Merge(key.ToByteArray().Merge(members)));
        }

        public long SRem(string key, string[] members)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (members == null)
                throw new ArgumentNullException("members");
            if (members.Length == 0)
                throw new ArgumentException("members");

            return this.Connection.SendExpectLong(Commands.SRem.Merge(key.ToByteArray().Merge(members)));
        }

        public byte[][] SUnion(params string[] keys)
        {
            if (keys == null)
                throw new ArgumentNullException("keys");
            if (keys.Length == 0)
                throw new ArgumentNullException("keys");

            return this.Connection.SendExpectMultiData(Commands.SUnion.Merge(keys));
        }

        public string[] SUnionStrings(params string[] keys)
        {
            if (keys == null)
                throw new ArgumentNullException("keys");
            if (keys.Length == 0)
                throw new ArgumentNullException("keys");

            return this.Connection.SendExpectMultiData(Commands.SUnion.Merge(keys)).ToUtf8Strings();
        }

        public void SUnionStore(string intoKey, params string[] keys)
        {
            if (intoKey == null)
                throw new ArgumentNullException("intoKey");
            if (keys == null)
                throw new ArgumentNullException("keys");
            if (keys.Length == 0)
                throw new ArgumentNullException("keys");

            this.Connection.SendExpectSuccess(Commands.SUnionStore.Merge(intoKey.ToByteArray().Merge(keys)));
        }

        public byte[][] SScan(string key, ulong cursor, int count = 10, string match = null)
        {
            throw new NotImplementedException();
        }
    }
}
