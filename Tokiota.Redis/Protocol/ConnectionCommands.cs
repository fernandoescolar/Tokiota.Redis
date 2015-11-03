namespace Tokiota.Redis.Protocol
{
    using Utilities;

    internal class ConnectionCommands : CommandsBase, IRedisConnectionCommands
    {
        private const string PingResponse = "PONG";

        public ConnectionCommands(IRedisConnection connection) : base(connection)
        {
        }

        public void Auth(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                this.Connection.SendExpectSuccess(Commands.Auth, password.ToByteArray());
            }
        }

        public string Echo(string text)
        {
            return this.Connection.SendExpectData(Commands.Echo, text.ToByteArray()).ToUtf8String();
        }

        public bool Ping()
        {
            return this.Connection.SendExpectString(Commands.Ping) == PingResponse;
        }

        public void Quit()
        {
            this.Connection.SendExpectSuccess(Commands.Quit);
        }

        public void Select(int db)
        {
            this.Connection.SendExpectSuccess(Commands.Select, db.ToByteArray());
        }
    }
}
