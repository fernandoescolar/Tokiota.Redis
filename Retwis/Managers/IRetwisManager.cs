namespace Retwis.Managers
{
    using Models;
    using System.Collections.Generic;

    public interface IRetwisManager
    {
        IEnumerable<User> GetLastUsers();

        Post GetPost(string postId);

        bool Login(string username, string password);

        void Post(string userId, string message);

        void Register(string username, string password);

        void Follow(string currentUserId, string userIdToFollow);

        void UnFollow(string currentUserId, string userIdToUnFollow);

        bool IsFollowing(string userId, string followingUserId);

        string GetUserId(string username);

        UserInfo GetUserInfo(string userId);

        IEnumerable<Post> Timeline(int start, int count);

        IEnumerable<Post> Timeline(string userId, int start, int count);
    }
}
