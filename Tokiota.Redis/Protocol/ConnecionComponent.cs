using Tokiota.Redis.Net;
using Tokiota.Redis.Utilities;

namespace Tokiota.Redis.Protocol
{
    internal class ConnecionComponent : ComponentBase, IRedisConnectionCommands
    {
        public ConnecionComponent(IRedisConnection connection) : base(connection)
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
            return this.Connection.SendExpectString(Commands.Ping) == "PONG";
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
