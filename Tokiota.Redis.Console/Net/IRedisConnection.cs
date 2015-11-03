namespace Tokiota.Redis.Console.Net
{
    using System;

    internal interface IRedisConnection : IDisposable
    {
        bool Connected { get; }

        event EventHandler Connecting;

        event EventHandler Disconnecting;

        event RedisMessageReceiveEventHandler MessageReceived;

        string Host { get; set; }

        int Port { get; set; }

        int SendTimeout { get; set; }

        bool UseSsl { get; set; }

        bool SendCommand(params string[] args);

        bool SendCommand(params byte[][] args);
    }
}
