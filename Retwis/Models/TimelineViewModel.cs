namespace Retwis.Models
{
    using System.Collections.Generic;

    public class TimelineViewModel
    {
        public IEnumerable<User> LastUsers { get; set; }

        public IEnumerable<Post> Posts { get; set; }

        public int Next { get; set; }

        public int Prev { get; set; }
    }
}