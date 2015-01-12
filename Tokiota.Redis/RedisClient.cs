using System;
using Tokiota.Redis.Net;
using Tokiota.Redis.Protocol;

namespace Tokiota.Redis
{
    public class RedisClient : IRedisClient
    {
        private readonly RedisEndpoint endpoint;
        private readonly IRedisConnection connection;

        private IRedisConnectionCommands connections;
        private IRedisKeysCommands keys;
        private IRedisStringsCommands strings;
        private IRedisHashesCommands hashes;
        private IRedisListsCommands lists;
        private IRedisSetsCommands sets;

        public RedisClient(RedisEndpoint endpoint) 
            : this(endpoint, new RedisConnection(endpoint.Host, endpoint.Port, endpoint.SendTimeout, endpoint.UseSsl))
        {            
        }

        public RedisClient(string host, int port, string password)
            : this(new RedisEndpoint { Host = host, Port = port, Password = password, UseSsl = port != 6379, SendTimeout = 30 })
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

        internal RedisClient (RedisEndpoint endpoint, IRedisConnection connection)
	    {
            this.endpoint = endpoint;
            this.connection = connection;
            this.connection.Connecting += this.OnConnecting;
            this.connection.Disconnecting += this.OnDisconnecting;
	    }
       
        public IRedisConnectionCommands Connection
        {
            get { return this.connections ?? (this.connections = new ConnecionComponent(this.connection)); }
        }

        public IRedisKeysCommands Keys
        {
            get { return this.keys ?? (this.keys = new KeysComponent(this.connection)); }
        }

        public IRedisStringsCommands Strings
        {
            get { return this.strings ?? (this.strings = new StringsComponent(this.connection)); }
        }

        public IRedisHashesCommands Hashes
        {
            get { return this.hashes ?? (this.hashes = new HashesComponent(this.connection)); }
        }

        public IRedisListsCommands Lists
        {
            get { return this.lists ?? (this.lists = new ListsComponent(this.connection)); }
        }

        public IRedisSetsCommands Sets
        {
            get { return this.sets ?? (this.sets = new SetsComponent(this.connection)); }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.connection.Connecting -= this.OnConnecting;
                this.connection.Dispose();
                this.connection.Disconnecting -= this.OnDisconnecting;
            }
        }

        private void OnConnecting(object sender, EventArgs e)
        {
            if (this.endpoint.Password != null)
                this.Connection.Auth(this.endpoint.Password);
        }

        private void OnDisconnecting(object sender, EventArgs e)
        {
            this.Connection.Quit();
        }
    }
}
