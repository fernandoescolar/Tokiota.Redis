using System;
namespace Tokiota.Redis
{
    public interface IRedisSetsCommands
    {
        long SAdd(string setId, byte[] value);
        long SAdd(string setId, byte[][] values);
        long SAdd(string setId, string value);
        long SAdd(string setId, string[] values);
        long SCard(string setId);
        byte[][] SDiff(string fromSetId, params string[] withSetIds);
        string[] SDiffString(string fromSetId, params string[] withSetIds);
        void SDiffStore(string intoSetId, string fromSetId, params string[] withSetIds);
        byte[][] SInter(params string[] setIds);
        void SInterStore(string intoSetId, params string[] setIds);
        string[] SInterStrings(params string[] setIds);
        long SIsMember(string setId, byte[] value);
        long SIsMember(string setId, string value);
        byte[][] SMembers(string setId);
        string[] SMemberStrings(string setId);
        void SMove(string fromSetId, string toSetId, byte[] value);
        void SMove(string fromSetId, string toSetId, string value);
        byte[] SPop(string setId);
        string SPopString(string setId);
        byte[] SRandMember(string setId);
        byte[][] SRandMember(string setId, int count);
        string SRandMemberString(string setId);
        string[] SRandMemberString(string setId, int count);
        long SRem(string setId, byte[][] values);
        long SRem(string setId, string[] values);
        byte[][] SScan(string setId, ulong cursor, int count = 10, string match = null);
        byte[][] SUnion(params string[] setIds);
        void SUnionStore(string intoSetId, params string[] setIds);
        string[] SUnionStrings(params string[] setIds);
    }
}
