using System;

namespace Tokiota.Redis
{
    public interface IRedisListsCommands
    {
        byte[][] BLPop(string listId, int timeOutSecs);
        byte[][] BRPop(string listId, int timeOutSecs);
        byte[] BRPopLPush(string fromListId, string toListId);
        string BRPopLPushString(string fromListId, string toListId);
        byte[] LIndex(string listId, int listIndex);
        string LIndexString(string listId, int listIndex);
        void LInsert(string listId, bool insertBefore, byte[] pivot, byte[] value);
        long LLen(string listId);
        byte[] LPop(string listId);
        string LPopString(string listId);
        long LPush(string listId, byte[] value);
        long LPush(string listId, string value);
        long LPushX(string listId, byte[] value);
        long LPushX(string listId, string value);
        byte[][] LRange(string listId, int startingFrom, int endingAt);
        long LRem(string listId, int removeNoOfMatches, byte[] value);
        long LRem(string listId, int removeNoOfMatches, string value);
        void LSet(string listId, int listIndex, byte[] value);
        void LSet(string listId, int listIndex, string value);
        void LTrim(string listId, int keepStartingFrom, int keepEndingAt);
        byte[] RPop(string listId);
        byte[] RPopLPush(string fromListId, string toListId);
        string RPopLPushString(string fromListId, string toListId);
        string RPopString(string listId);
        long RPush(string listId, byte[][] values);
        long RPush(string listId, string[] values);
        long RPushX(string listId, byte[] value);
        long RPushX(string listId, string value);
    }
}
