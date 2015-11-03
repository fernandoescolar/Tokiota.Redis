namespace Tokiota.Redis
{
    using System;

    internal interface IRedisConnection : IDisposable
    {
        bool Connected { get; }

        string Host { get; set; }

        int Port { get; set; }

        bool UseSsl { get; set; }

        TimeSpan ConnectionTimeout { get; set; }

        TimeSpan ReceiveTimeout { get; set; }

        event EventHandler Connecting;

        event EventHandler Disconnecting;

        void SendExpectSuccess(params byte[][] commands);

        long SendExpectLong(params byte[][] commands);

        double SendExpectDouble(params byte[][] commands);

        string SendExpectString(params byte[][] commands);

        byte[] SendExpectData(params byte[][] commands);

        byte[][] SendExpectMultiData(params byte[][] commands);

        RedisScanResult SendExpectScanResult(params byte[][] commands);
    }
}
