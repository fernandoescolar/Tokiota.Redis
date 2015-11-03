namespace Retwis.Models
{
    using System;

    public class Post
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

        public DateTime Time { get; set; }

        public string Message { get; set; }
    }
}
