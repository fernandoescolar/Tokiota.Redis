namespace Tokiota.Redis
{
    public interface IRedisSortedSetsCommands
    {
        long ZAdd(string key, double score, byte[] member);
        long ZAdd(string key, double score, string member);
        long ZAdd(string key, double[] scores, byte[][] members);
        long ZAdd(string key, double[] scores, string[] members);
        long ZAdd(string key, int score, byte[] member);
        long ZAdd(string key, int score, string member);
        long ZAdd(string key, int[] scores, byte[][] members);
        long ZAdd(string key, int[] scores, string[] members);
        long ZAdd(string key, long score, byte[] member);
        long ZAdd(string key, long score, string member);
        long ZAdd(string key, long[] scores, byte[][] members);
        long ZAdd(string key, long[] scores, string[] members);
        long ZCard(string key);
        long ZCount(string key, double min, double max);
        long ZCount(string key, int min, int max);
        long ZCount(string key, long min, long max);
        double ZIncrBy(string key, double increment, byte[] member);
        double ZIncrBy(string key, double increment, string member);
        long ZIncrBy(string key, int increment, byte[] member);
        long ZIncrBy(string key, int increment, string member);
        long ZIncrBy(string key, long increment, byte[] member);
        long ZIncrBy(string key, long increment, string member);
        long ZInterStore(string intoKey, params string[] withKeys);
        long ZLexCount(string key, string min, string max);
        byte[][] ZRange(string key, int start, int stop, bool withScores);
        byte[][] ZRangeByLex(string key, string min, string max, int? offset = null, int? count = null);
        string[] ZRangeByLexString(string key, string min, string max, int? offset = null, int? count = null);
        byte[][] ZRangeByScore(string key, double min, double max, bool withScores, int? offset, int? count);
        byte[][] ZRangeByScore(string key, int min, int max, bool withScores, int? offset, int? count);
        byte[][] ZRangeByScore(string key, long min, long max, bool withScores, int? offset, int? count);
        string[] ZRangeByScoreString(string key, double min, double max, bool withScores, int? offset, int? count);
        string[] ZRangeByScoreString(string key, int min, int max, bool withScores, int? offset, int? count);
        string[] ZRangeByScoreString(string key, long min, long max, bool withScores, int? offset, int? count);
        string[] ZRangeString(string key, int min, int max);
        long ZRank(string key, byte[] member);
        long ZRank(string key, string member);
        long ZRem(string key, byte[] member);
        long ZRem(string key, byte[][] members);
        long ZRem(string key, string member);
        long ZRem(string key, string[] members);
        long ZRemRangeByLex(string key, string min, string max);
        long ZRemRangeByRank(string key, int start, int stop);
        long ZRemRangeByScore(string key, double min, double max);
        long ZRemRangeByScore(string key, int min, int max);
        long ZRemRangeByScore(string key, long min, long max);
        byte[][] ZRevRange(string key, int start, int stop, bool withScores);
        byte[][] ZRevRangeByLex(string key, string min, string max, int? offset = null, int? count = null);
        string[] ZRevRangeByLexString(string key, string min, string max, int? offset = null, int? count = null);
        byte[][] ZRevRangeByScore(string key, double min, double max, bool withScores, int? offset, int? count);
        byte[][] ZRevRangeByScore(string key, int min, int max, bool withScores, int? offset, int? count);
        byte[][] ZRevRangeByScore(string key, long min, long max, bool withScores, int? offset, int? count);
        string[] ZRevRangeByScoreString(string key, double min, double max, bool withScores, int? offset, int? count);
        string[] ZRevRangeByScoreString(string key, int min, int max, bool withScores, int? offset, int? count);
        string[] ZRevRangeByScoreString(string key, long min, long max, bool withScores, int? offset, int? count);
        string[] ZRevRangeString(string key, int start, int stop, bool withScores);
        long ZRevRank(string key, byte[] member);
        long ZRevRank(string key, string member);
        RedisScanResult ZScan(string key, ulong cursor, int count = 10, string match = null);
        byte[][] ZScanLoop(string key, int count = 10, string match = null);
        string[] ZScanLoopString(string key, int count = 10, string match = null);
        string ZScore(string key, byte[] member);
        string ZScore(string key, string member);
        long ZUnionStore(string intoKey, params string[] withKeys);
    }
}
