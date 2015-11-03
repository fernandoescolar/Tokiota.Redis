namespace Tokiota.Redis
{
    using System;

    public class RedisEndpoint
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string Password { get; set; }

        public bool UseSsl { get; set; }

        public TimeSpan Timeout { get; set; }
    }
}
