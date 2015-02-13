using Retwis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Tokiota.Redis;

namespace Retwis.Managers
{
    public class RetwisManager : IRetwisManager
    {
        public void Register(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException("username");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("password");

            using (var redis = RedisManager.Current.GetClient())
            {
                var exists = !string.IsNullOrEmpty(redis.Hashes.HGetString("users", username));
                if (exists)
                {
                    throw new Exception("The user already exists");
                }

                var userId = redis.Strings.Incr("next_user_id");
                redis.Hashes.HSet("users", username, userId.ToString());
                redis.Hashes.HMSet("user:" + userId, new Dictionary<string, string> { 
                    {"username", username},
                    {"password", password}
                });

                redis.SortedSets.ZAdd("users_by_time", DateTime.Now.Ticks, username);
            }
        }

        public bool Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException("username");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("password");

            using (var redis = RedisManager.Current.GetClient())
            {
                var userId = redis.Hashes.HGetString("users", username);
                if (!string.IsNullOrEmpty(userId))
                {
                    var storedPassword = redis.Hashes.HGetString("user:" + userId, "password");
                    return password == storedPassword;
                }
            }

            return false;
        }

        public void Follow(string currentUserId, string userIdToFollow)
        {
            if (string.IsNullOrEmpty(currentUserId))
                throw new ArgumentNullException("currentUserId");
            if (string.IsNullOrEmpty(userIdToFollow))
                throw new ArgumentNullException("userIdToFollow");

            using (var redis = RedisManager.Current.GetClient())
            {
                redis.SortedSets.ZAdd("followers:" + userIdToFollow, DateTime.Now.Ticks, currentUserId);
                redis.SortedSets.ZAdd("following:" + currentUserId, DateTime.Now.Ticks, userIdToFollow);
            }
        }

        public void UnFollow(string currentUserId, string userIdToUnFollow)
        {
            if (string.IsNullOrEmpty(currentUserId))
                throw new ArgumentNullException("currentUserId");
            if (string.IsNullOrEmpty(userIdToUnFollow))
                throw new ArgumentNullException("userIdToFollow");

            using (var redis = RedisManager.Current.GetClient())
            {
                redis.SortedSets.ZRem("followers:" + userIdToUnFollow, currentUserId);
                redis.SortedSets.ZRem("following:" + currentUserId, userIdToUnFollow);
            }
        }

        public bool IsFollowing(string userId, string followingUserId)
        {
            using (var redis = RedisManager.Current.GetClient())
            {
                var score = redis.SortedSets.ZScore("following:" + userId, followingUserId);
                return !string.IsNullOrEmpty(score);
            }
        }

        public IEnumerable<User> GetLastUsers()
        {
            using (var redis = RedisManager.Current.GetClient())
            {
                var users = redis.SortedSets.ZRevRangeString("users_by_time", 0, 10, false);
                foreach (var username in users)
                {
                    yield return new User
                    {
                        UserId = redis.Hashes.HGetString("users", username),
                        Username = username
                    };
                }
            }
        }

        public string GetUserId(string username)
        {
            using (var redis = RedisManager.Current.GetClient())
            {
                return redis.Hashes.HGetString("users", username);
            }
        }

        public UserInfo GetUserInfo(string userId)
        {
            using (var redis = RedisManager.Current.GetClient())
            {
                return new UserInfo
                {
                    Username = redis.Hashes.HGetString("user:" + userId, "username"),
                    Followers = (int)redis.SortedSets.ZCard("followers:" + userId),
                    Following = (int)redis.SortedSets.ZCard("following:" + userId)
                };
            }
        }

        public void Post(string userId, string message)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException("userId");
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException("message");

            using (var redis = RedisManager.Current.GetClient())
            {
                var postId = redis.Strings.Incr("next_post_id");
                message = message.Replace("\n", string.Empty).Replace("\r", string.Empty);

                redis.Hashes.HMSet("post:" + postId, new Dictionary<string, string> { 
                    { "userid", userId },
                    { "time", DateTime.Now.ToString() },
                    { "message", message }
                });

                var followers = redis.SortedSets.ZRangeString("followers:" + userId, 0, -1).ToList();
                followers.Add(userId);

                foreach (var follower in followers)
                {
                    redis.Lists.LPush("posts:" + follower, postId.ToString());
                }

                redis.Lists.LPush("timeline", postId.ToString());
                redis.Lists.LTrim("timeline", 0, 1000);
            }
        }

        public Post GetPost(string postId)
        {
            if (string.IsNullOrEmpty(postId))
                throw new ArgumentNullException("postId");

            using (var redis = RedisManager.Current.GetClient())
            {
                return this.GetPost(redis, postId);
            }
        }

        public IEnumerable<Post> Timeline(int start, int count)
        {
            return this.Timeline(null, start, count);
        }

        public IEnumerable<Post> Timeline(string userId, int start, int count)
        {
            var key = string.IsNullOrEmpty(userId) ? "timeline" : "posts:" + userId;
            using (var redis = RedisManager.Current.GetClient())
            {
                string[] posts = redis.Lists.LRangeString(key, start, start + count); 
                foreach (var postId in posts)
                {
                    yield return this.GetPost(redis, postId);
                }
            }
        }

        private Post GetPost(IRedisClient redis, string postId)
        {
            if (string.IsNullOrEmpty(postId))
                throw new ArgumentNullException("postId");

            var post = redis.Hashes.HGetAllHashtable("post:" + postId);
            if (post == null || post.Count == 0)
                throw new Exception("Post not found");

            var userId = post["userid"] as string;
            return new Post
            {
                Id = postId,
                UserId = userId,
                Username = redis.Hashes.HGetString("user:" + userId, "username"),
                Time = DateTime.Parse(post["time"] as string),
                Message = post["message"] as string
            };
        }
    }
}