namespace Retwis.Models
{
    using System.Collections.Generic;

    public class HomeViewModel
    {
        public int Followers { get; set; }

        public int Following { get; set; }

        public IEnumerable<Post> Posts { get; set; }

        public int Next { get; set; }

        public int Prev { get; set; }
    }
}