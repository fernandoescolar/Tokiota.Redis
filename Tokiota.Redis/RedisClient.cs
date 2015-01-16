using System;
using Tokiota.Redis.Net;
using Tokiota.Redis.Protocol;

namespace Tokiota.Redis
{
    public class RedisClient : IRedisClient
    {
        private readonly IRedisConnection connection;
        private readonly RedisEndpoint endpoint;

        private IRedisConnectionCommands connections;
        private IRedisKeysCommands keys;
        private IRedisStringsCommands strings;
        private IRedisHashesCommands hashes;
        private IRedisListsCommands lists;
        private IRedisSetsCommands sets;

        public RedisClient(RedisEndpoint endpoint)
            : this(endpoint, new RedisConnection(endpoint.Host, endpoint.Port, endpoint.UseSsl, endpoint.Timeout))
        {            
        }

        public RedisClient(string host, int port, string password)
            : this(new RedisEndpoint { Host = host, Port = port, Password = password, UseSsl = port != 6379, Timeout = TimeSpan.FromSeconds(30) })
        {
        }

        public RedisClient(string host, int port)
            : this(host, port, null)
        {
        }

        public RedisClient(string host)
            : this(host, 6379)
        {
        }

        public RedisClient()
            : this("localhost", 6379)
        {
        }

        internal RedisClient(RedisEndpoint endpoint, IRedisConnection connection)
	    {
            this.endpoint = endpoint;
            this.connection = connection;
            this.connection.Connecting += OnConnecting;
            this.connection.Disconnecting += OnDisconnecting;
	    }
       
        public IRedisConnectionCommands Connection
        {
            get { return this.connections ?? (this.connections = new ConnectionCommands(this.connection)); }
        }

        public IRedisKeysCommands Keys
        {
            get { return this.keys ?? (this.keys = new KeysCommands(this.connection)); }
        }

        public IRedisStringsCommands Strings
        {
            get { return this.strings ?? (this.strings = new StringsCommands(this.connection)); }
        }

        public IRedisHashesCommands Hashes
        {
            get { return this.hashes ?? (this.hashes = new HashesCommands(this.connection)); }
        }

        public IRedisListsCommands Lists
        {
            get { return this.lists ?? (this.lists = new ListsComponent(this.connection)); }
        }

        public IRedisSetsCommands Sets
        {
            get { return this.sets ?? (this.sets = new SetsCommands(this.connection)); }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.connection.Connecting -= OnConnecting;
                this.connection.Disconnecting -= OnDisconnecting;
                this.connection.Dispose();
            }
        }
        
        private void OnConnecting(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.endpoint.Password))
            {
                this.Connection.Auth(this.endpoint.Password);
            }
        }

        private void OnDisconnecting(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.endpoint.Password))
            {
                this.Connection.Quit();
            }
        }
    }
}
