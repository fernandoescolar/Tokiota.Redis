using System;

namespace Tokiota.Redis.Net
{
    internal interface IRedisConnection : IDisposable
    {
        bool Connected { get; }

        event EventHandler Connecting;

        event EventHandler Disconnecting;

        string Host { get; set; }

        int Port { get; set; }

        bool SendCommand(params byte[][] args);

        byte[] SendExpectData(params byte[][] args);

        byte[][] SendExpectMultiData(params byte[][] args);

        int SendExpectInt(params byte[][] args);

        long SendExpectLong(params byte[][] args);

        double SendExpectDouble(params byte[][] args);

        string SendExpectString(params byte[][] args);

        void SendExpectSuccess(params byte[][] args);

        int SendTimeout { get; set; }

        bool UseSsl { get; set; }
    }
}
