﻿using System;
using Tokiota.Redis.Utilities;

namespace Tokiota.Redis.Protocol
{
    internal class SortedSetsCommands : CommandsBase, IRedisSortedSetsCommands
    {
        public SortedSetsCommands(IRedisConnection connection)
            : base(connection)
        {
        }

        public long ZAdd(string key, double score, byte[] member)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (member == null)
                throw new ArgumentNullException("member");

            return this.Connection.SendExpectLong(Commands.ZAdd, key.ToByteArray(), score.ToByteArray(), member);
        }

        public long ZAdd(string key, int score, byte[] member)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (member == null)
                throw new ArgumentNullException("member");

            return this.Connection.SendExpectLong(Commands.ZAdd, key.ToByteArray(), score.ToByteArray(), member);
        }

        public long ZAdd(string key, long score, byte[] member)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (member == null)
                throw new ArgumentNullException("member");

            return this.Connection.SendExpectLong(Commands.ZAdd, key.ToByteArray(), score.ToByteArray(), member);
        }

        public long ZAdd(string key, double score, string member)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (member == null)
                throw new ArgumentNullException("member");

            return this.Connection.SendExpectLong(Commands.ZAdd, key.ToByteArray(), score.ToByteArray(), member.ToByteArray());
        }

        public long ZAdd(string key, int score, string member)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (member == null)
                throw new ArgumentNullException("member");

            return this.Connection.SendExpectLong(Commands.ZAdd, key.ToByteArray(), score.ToByteArray(), member.ToByteArray());
        }

        public long ZAdd(string key, long score, string member)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (member == null)
                throw new ArgumentNullException("member");

            return this.Connection.SendExpectLong(Commands.ZAdd, key.ToByteArray(), score.ToByteArray(), member.ToByteArray());
        }

        public long ZAdd(string key, double[] scores, byte[][] members)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (scores == null)
                throw new ArgumentNullException("scores");

            if (scores.Length == 0)
                throw new ArgumentOutOfRangeException("scores");

            if (members == null)
                throw new ArgumentNullException("members");

            if (members.Length == 0)
                throw new ArgumentOutOfRangeException("members");

            return this.Connection.SendExpectLong(Commands.ZAdd.Merge(key.ToByteArray().Combine(scores.ToByteArrays(), members)));
        }

        public long ZAdd(string key, int[] scores, byte[][] members)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (scores == null)
                throw new ArgumentNullException("scores");

            if (scores.Length == 0)
                throw new ArgumentOutOfRangeException("scores");

            if (members == null)
                throw new ArgumentNullException("members");

            if (members.Length == 0)
                throw new ArgumentOutOfRangeException("members");

            return this.Connection.SendExpectLong(Commands.ZAdd.Merge(key.ToByteArray().Combine(scores.ToByteArrays(), members)));
        }

        public long ZAdd(string key, long[] scores, byte[][] members)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (scores == null)
                throw new ArgumentNullException("scores");

            if (scores.Length == 0)
                throw new ArgumentOutOfRangeException("scores");

            if (members == null)
                throw new ArgumentNullException("members");

            if (members.Length == 0)
                throw new ArgumentOutOfRangeException("members");

            return this.Connection.SendExpectLong(Commands.ZAdd.Merge(key.ToByteArray().Combine(scores.ToByteArrays(), members)));
        }

        public long ZAdd(string key, double[] scores, string[] members)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (scores == null)
                throw new ArgumentNullException("scores");

            if (scores.Length == 0)
                throw new ArgumentOutOfRangeException("scores");

            if (members == null)
                throw new ArgumentNullException("members");

            if (members.Length == 0)
                throw new ArgumentOutOfRangeException("members");

            return this.Connection.SendExpectLong(Commands.ZAdd.Merge(key.ToByteArray().Combine(scores.ToByteArrays(), members.ToByteArrays())));
        }

        public long ZAdd(string key, int[] scores, string[] members)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (scores == null)
                throw new ArgumentNullException("scores");

            if (scores.Length == 0)
                throw new ArgumentOutOfRangeException("scores");

            if (members == null)
                throw new ArgumentNullException("members");

            if (members.Length == 0)
                throw new ArgumentOutOfRangeException("members");

            return this.Connection.SendExpectLong(Commands.ZAdd.Merge(key.ToByteArray().Combine(scores.ToByteArrays(), members.ToByteArrays())));
        }

        public long ZAdd(string key, long[] scores, string[] members)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (scores == null)
                throw new ArgumentNullException("scores");

            if (scores.Length == 0)
                throw new ArgumentOutOfRangeException("scores");

            if (members == null)
                throw new ArgumentNullException("members");

            if (members.Length == 0)
                throw new ArgumentOutOfRangeException("members");

            return this.Connection.SendExpectLong(Commands.ZAdd.Merge(key.ToByteArray().Combine(scores.ToByteArrays(), members.ToByteArrays())));
        }

        public long ZCard(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.ZCard, key.ToByteArray());
        }

        public long ZCount(string key, double min, double max)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.ZCount, key.ToByteArray(), min.ToByteArray(), max.ToByteArray());
        }

        public long ZCount(string key, int min, int max)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.ZCount, key.ToByteArray(), min.ToByteArray(), max.ToByteArray());

        }

        public long ZCount(string key, long min, long max)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.ZCount, key.ToByteArray(), min.ToByteArray(), max.ToByteArray());
        }

        public double ZIncrBy(string key, double increment, byte[] member)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (member == null)
                throw new ArgumentNullException("member");

            return this.Connection.SendExpectDouble(Commands.ZIncrBy, key.ToByteArray(), increment.ToByteArray(), member);
        }

        public long ZIncrBy(string key, int increment, byte[] member)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (member == null)
                throw new ArgumentNullException("member");

            return this.Connection.SendExpectLong(Commands.ZIncrBy, key.ToByteArray(), increment.ToByteArray(), member);
        }

        public long ZIncrBy(string key, long increment, byte[] member)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (member == null)
                throw new ArgumentNullException("member");

            return this.Connection.SendExpectLong(Commands.ZIncrBy, key.ToByteArray(), increment.ToByteArray(), member);
        }

        public double ZIncrBy(string key, double increment, string member)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (member == null)
                throw new ArgumentNullException("member");

            return this.Connection.SendExpectDouble(Commands.ZIncrBy, key.ToByteArray(), increment.ToByteArray(), member.ToByteArray());
        }

        public long ZIncrBy(string key, int increment, string member)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (member == null)
                throw new ArgumentNullException("member");

            return this.Connection.SendExpectLong(Commands.ZIncrBy, key.ToByteArray(), increment.ToByteArray(), member.ToByteArray());
        }

        public long ZIncrBy(string key, long increment, string member)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (member == null)
                throw new ArgumentNullException("member");

            return this.Connection.SendExpectLong(Commands.ZIncrBy, key.ToByteArray(), increment.ToByteArray(), member.ToByteArray());
        }

        public long ZInterStore(string intoKey, params string[] withKeys)
        {
            if (intoKey == null)
                throw new ArgumentNullException("intoKey");

            if (withKeys == null)
                throw new ArgumentNullException("withKeys");

            if (withKeys.Length == 0)
                throw new ArgumentNullException("withKeys");

            return this.Connection.SendExpectLong(Commands.ZInterStore.Merge(intoKey.ToByteArray(), withKeys.ToByteArrays()));
        }

        public long ZLexCount(string key, string min, string max)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.ZLexCount, key.ToByteArray(), min.ToByteArray(), max.ToByteArray());
        }

        public byte[][] ZRange(string key, int start, int stop, bool withScores)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (withScores)
                return this.Connection.SendExpectMultiData(Commands.ZRange, key.ToByteArray(), start.ToByteArray(), stop.ToByteArray(), Commands.WithScores);

            return this.Connection.SendExpectMultiData(Commands.ZRange, key.ToByteArray(), start.ToByteArray(), stop.ToByteArray());
        }

        public string[] ZRangeString(string key, int min, int max)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectMultiData(Commands.ZRange, key.ToByteArray(), min.ToByteArray(), max.ToByteArray()).ToUtf8Strings();
        }

        public byte[][] ZRangeByLex(string key, string min, string max, int? offset = null, int? count = null)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (offset.HasValue || count.HasValue)
            {
                return this.Connection.SendExpectMultiData(Commands.ZRangeByLex, key.ToByteArray(), min.ToByteArray(), max.ToByteArray(), Commands.Limit, offset.GetValueOrDefault(0).ToByteArray(), count.GetValueOrDefault(0).ToByteArray());
            }

            return this.Connection.SendExpectMultiData(Commands.ZRangeByLex, key.ToByteArray(), min.ToByteArray(), max.ToByteArray());
        }

        public string[] ZRangeByLexString(string key, string min, string max, int? offset = null, int? count = null)
        {
            return this.ZRangeByLex(key, min, max, offset, count).ToUtf8Strings();
        }

        public byte[][] ZRevRangeByLex(string key, string min, string max, int? offset = null, int? count = null)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (offset.HasValue || count.HasValue)
            {
                return this.Connection.SendExpectMultiData(Commands.ZRevRangeByLex, key.ToByteArray(), min.ToByteArray(), max.ToByteArray(), Commands.Limit, offset.GetValueOrDefault(0).ToByteArray(), count.GetValueOrDefault(0).ToByteArray());
            }

            return this.Connection.SendExpectMultiData(Commands.ZRevRangeByLex, key.ToByteArray(), min.ToByteArray(), max.ToByteArray());
        }

        public string[] ZRevRangeByLexString(string key, string min, string max, int? offset = null, int? count = null)
        {
            return this.ZRevRangeByLex(key, min, max, offset, count).ToUtf8Strings();
        }

        public byte[][] ZRangeByScore(string key, double min, double max, bool withScores, int? offset, int? count)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (offset.HasValue || count.HasValue)
            {
                if (withScores)
                    return this.Connection.SendExpectMultiData(Commands.ZRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray(), Commands.WithScores, Commands.Limit, offset.GetValueOrDefault(0).ToByteArray(), count.GetValueOrDefault(0).ToByteArray());

                return this.Connection.SendExpectMultiData(Commands.ZRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray(), Commands.Limit, offset.GetValueOrDefault(0).ToByteArray(), count.GetValueOrDefault(0).ToByteArray());
            }

            if (withScores)
                return this.Connection.SendExpectMultiData(Commands.ZRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray(), Commands.WithScores);

            return this.Connection.SendExpectMultiData(Commands.ZRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray());
        }

        public byte[][] ZRangeByScore(string key, int min, int max, bool withScores, int? offset, int? count)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (offset.HasValue || count.HasValue)
            {
                if (withScores)
                    return this.Connection.SendExpectMultiData(Commands.ZRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray(), Commands.WithScores, Commands.Limit, offset.GetValueOrDefault(0).ToByteArray(), count.GetValueOrDefault(0).ToByteArray());

                return this.Connection.SendExpectMultiData(Commands.ZRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray(), Commands.Limit, offset.GetValueOrDefault(0).ToByteArray(), count.GetValueOrDefault(0).ToByteArray());
            }

            if (withScores)
                return this.Connection.SendExpectMultiData(Commands.ZRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray(), Commands.WithScores);

            return this.Connection.SendExpectMultiData(Commands.ZRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray());
        }

        public byte[][] ZRangeByScore(string key, long min, long max, bool withScores, int? offset, int? count)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (offset.HasValue || count.HasValue)
            {
                if (withScores)
                    return this.Connection.SendExpectMultiData(Commands.ZRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray(), Commands.WithScores, Commands.Limit, offset.GetValueOrDefault(0).ToByteArray(), count.GetValueOrDefault(0).ToByteArray());

                return this.Connection.SendExpectMultiData(Commands.ZRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray(), Commands.Limit, offset.GetValueOrDefault(0).ToByteArray(), count.GetValueOrDefault(0).ToByteArray());
            }

            if (withScores)
                return this.Connection.SendExpectMultiData(Commands.ZRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray(), Commands.WithScores);

            return this.Connection.SendExpectMultiData(Commands.ZRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray());
        }

        public string[] ZRangeByScoreString(string key, double min, double max, bool withScores, int? offset, int? count)
        {
            return this.ZRangeByScore(key, min, max, withScores, offset, count).ToUtf8Strings();
        }

        public string[] ZRangeByScoreString(string key, int min, int max, bool withScores, int? offset, int? count)
        {
            return this.ZRangeByScore(key, min, max, withScores, offset, count).ToUtf8Strings();
        }

        public string[] ZRangeByScoreString(string key, long min, long max, bool withScores, int? offset, int? count)
        {
            return this.ZRangeByScore(key, min, max, withScores, offset, count).ToUtf8Strings();
        }

        public long ZRank(string key, byte[] member)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (member == null)
                throw new ArgumentNullException("member");

            return this.Connection.SendExpectLong(Commands.ZRank, key.ToByteArray(), member);
        }

        public long ZRank(string key, string member)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (member == null)
                throw new ArgumentNullException("member");

            return this.Connection.SendExpectLong(Commands.ZRank, key.ToByteArray(), member.ToByteArray());
        }

        public long ZRem(string key, byte[] member)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (member == null)
                throw new ArgumentNullException("member");

            return this.Connection.SendExpectLong(Commands.ZRem, key.ToByteArray(), member);
        }

        public long ZRem(string key, string member)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (member == null)
                throw new ArgumentNullException("member");

            return this.Connection.SendExpectLong(Commands.ZRem, key.ToByteArray(), member.ToByteArray());
        }

        public long ZRem(string key, byte[][] members)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (members == null)
                throw new ArgumentNullException("values");

            if (members.Length == 0)
                throw new ArgumentException("values");

            return this.Connection.SendExpectLong(Commands.ZRem.Merge(key.ToByteArray(), members));
        }

        public long ZRem(string key, string[] members)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (members == null)
                throw new ArgumentNullException("values");

            if (members.Length == 0)
                throw new ArgumentException("values");

            return this.Connection.SendExpectLong(Commands.ZRem.Merge(key.ToByteArray(), members.ToByteArrays()));
        }

        public long ZRemRangeByLex(string key, string min, string max)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.ZRemRangeByLex, key.ToByteArray(), min.ToByteArray(), max.ToByteArray());
        }

        public long ZRemRangeByRank(string key, int start, int stop)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.ZRemRangeByRank, key.ToByteArray(), start.ToByteArray(), stop.ToByteArray());
        }

        public long ZRemRangeByScore(string key, double min, double max)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.ZRemRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray());
        }

        public long ZRemRangeByScore(string key, int min, int max)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.ZRemRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray());
        }

        public long ZRemRangeByScore(string key, long min, long max)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.ZRemRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray());
        }

        public byte[][] ZRevRange(string key, int start, int stop, bool withScores)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (withScores)
                return this.Connection.SendExpectMultiData(Commands.ZRevRange, key.ToByteArray(), start.ToByteArray(), stop.ToByteArray(), Commands.WithScores);

            return this.Connection.SendExpectMultiData(Commands.ZRevRange, key.ToByteArray(), start.ToByteArray(), stop.ToByteArray());
        }

        public string[] ZRevRangeString(string key, int start, int stop, bool withScores)
        {
            return this.ZRevRange(key, start, stop, withScores).ToUtf8Strings();
        }

        public byte[][] ZRevRangeByScore(string key, double min, double max, bool withScores, int? offset, int? count)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (offset.HasValue || count.HasValue)
            {
                if (withScores)
                    return this.Connection.SendExpectMultiData(Commands.ZRevRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray(), Commands.WithScores, Commands.Limit, offset.GetValueOrDefault(0).ToByteArray(), count.GetValueOrDefault(0).ToByteArray());

                return this.Connection.SendExpectMultiData(Commands.ZRevRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray(), Commands.Limit, offset.GetValueOrDefault(0).ToByteArray(), count.GetValueOrDefault(0).ToByteArray());
            }

            if (withScores)
                return this.Connection.SendExpectMultiData(Commands.ZRevRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray(), Commands.WithScores);

            return this.Connection.SendExpectMultiData(Commands.ZRevRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray());
        }

        public byte[][] ZRevRangeByScore(string key, int min, int max, bool withScores, int? offset, int? count)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (offset.HasValue || count.HasValue)
            {
                if (withScores)
                    return this.Connection.SendExpectMultiData(Commands.ZRevRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray(), Commands.WithScores, Commands.Limit, offset.GetValueOrDefault(0).ToByteArray(), count.GetValueOrDefault(0).ToByteArray());

                return this.Connection.SendExpectMultiData(Commands.ZRevRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray(), Commands.Limit, offset.GetValueOrDefault(0).ToByteArray(), count.GetValueOrDefault(0).ToByteArray());
            }

            if (withScores)
                return this.Connection.SendExpectMultiData(Commands.ZRevRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray(), Commands.WithScores);

            return this.Connection.SendExpectMultiData(Commands.ZRevRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray());
        }

        public byte[][] ZRevRangeByScore(string key, long min, long max, bool withScores, int? offset, int? count)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (offset.HasValue || count.HasValue)
            {
                if (withScores)
                    return this.Connection.SendExpectMultiData(Commands.ZRevRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray(), Commands.WithScores, Commands.Limit, offset.GetValueOrDefault(0).ToByteArray(), count.GetValueOrDefault(0).ToByteArray());

                return this.Connection.SendExpectMultiData(Commands.ZRevRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray(), Commands.Limit, offset.GetValueOrDefault(0).ToByteArray(), count.GetValueOrDefault(0).ToByteArray());
            }

            if (withScores)
                return this.Connection.SendExpectMultiData(Commands.ZRevRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray(), Commands.WithScores);

            return this.Connection.SendExpectMultiData(Commands.ZRevRangeByScore, key.ToByteArray(), min.ToByteArray(), max.ToByteArray());
        }

        public string[] ZRevRangeByScoreString(string key, double min, double max, bool withScores, int? offset, int? count)
        {
            return this.ZRevRangeByScore(key, min, max, withScores, offset, count).ToUtf8Strings();
        }

        public string[] ZRevRangeByScoreString(string key, int min, int max, bool withScores, int? offset, int? count)
        {
            return this.ZRevRangeByScore(key, min, max, withScores, offset, count).ToUtf8Strings();
        }

        public string[] ZRevRangeByScoreString(string key, long min, long max, bool withScores, int? offset, int? count)
        {
            return this.ZRevRangeByScore(key, min, max, withScores, offset, count).ToUtf8Strings();
        }

        public long ZRevRank(string key, byte[] member)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (member == null)
                throw new ArgumentNullException("member");

            return this.Connection.SendExpectLong(Commands.ZRevRank, key.ToByteArray(), member);
        }

        public long ZRevRank(string key, string member)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (member == null)
                throw new ArgumentNullException("member");

            return this.Connection.SendExpectLong(Commands.ZRevRank, key.ToByteArray(), member.ToByteArray());
        }

        public double ZScore(string key, byte[] member)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (member == null)
                throw new ArgumentNullException("member");

            return this.Connection.SendExpectDouble(Commands.ZScore, key.ToByteArray(), member);
        }

        public double ZScoreDouble(string key, string member)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (member == null)
                throw new ArgumentNullException("member");

            return this.Connection.SendExpectDouble(Commands.ZScore, key.ToByteArray(), member.ToByteArray());
        }

        public long ZScore(string key, string member)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (member == null)
                throw new ArgumentNullException("member");

            return this.Connection.SendExpectLong(Commands.ZScore, key.ToByteArray(), member.ToByteArray());
        }

        public long ZUnionStore(string intoKey, params string[] withKeys)
        {

            if (intoKey == null)
                throw new ArgumentNullException("intoKey");

            if (withKeys == null)
                throw new ArgumentNullException("withKeys");

            if (withKeys.Length == 0)
                throw new ArgumentNullException("withKeys");

            return this.Connection.SendExpectLong(Commands.ZUnionStore.Merge(intoKey.ToByteArray(), withKeys.ToByteArrays()));
        }

        public byte[][] ZScan(string key, ulong cursor, int count = 10, string match = null)
        {
            throw new NotImplementedException();
        }
    }
}
