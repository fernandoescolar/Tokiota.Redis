using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Retwis.Models
{
    public class TimelineViewModel
    {
        public IEnumerable<User> LastUsers { get; set; }

        public IEnumerable<Post> Posts { get; set; }

        public int Next { get; set; }

        public int Prev { get; set; }
    }
}