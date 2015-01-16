using System;
namespace Tokiota.Redis
{
    public interface IRedisSetsCommands
    {
        long SAdd(string key, byte[] member);
        long SAdd(string key, byte[][] members);
        long SAdd(string key, string member);
        long SAdd(string key, string[] members);
        long SCard(string key);
        byte[][] SDiff(string fromKey, params string[] withKeys);
        string[] SDiffString(string fromKey, params string[] withKeys);
        void SDiffStore(string intoKey, string fromKey, params string[] withKeys);
        byte[][] SInter(params string[] keys);
        void SInterStore(string intoKey, params string[] keys);
        string[] SInterStrings(params string[] keys);
        long SIsMember(string key, byte[] member);
        long SIsMember(string key, string member);
        byte[][] SMembers(string key);
        string[] SMemberStrings(string key);
        void SMove(string fromKey, string toKey, byte[] member);
        void SMove(string fromKey, string toKey, string member);
        byte[] SPop(string key);
        string SPopString(string key);
        byte[] SRandMember(string key);
        byte[][] SRandMember(string key, int count);
        string SRandMemberString(string key);
        string[] SRandMemberString(string key, int count);
        long SRem(string key, byte[][] members);
        long SRem(string key, string[] members);
        byte[][] SScan(string key, ulong cursor, int count = 10, string match = null);
        byte[][] SUnion(params string[] keys);
        void SUnionStore(string intoKey, params string[] keys);
        string[] SUnionStrings(params string[] keys);
    }
}
