namespace Retwis.Models
{
    using System.Collections.Generic;

    public class ProfileViewModel
    {
        public string UserId { get; set; }
        
        public string Username { get; set; }

        public bool IsMe { get; set; }

        public bool IsFollowing { get; set; }

        public int Followers { get; set; }

        public int Following { get; set; }

        public IEnumerable<Post> Posts { get; set; }

        public int Next { get; set; }

        public int Prev { get; set; }
    }
}