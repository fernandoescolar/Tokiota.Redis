using System;
using Tokiota.Redis.Net;
using Tokiota.Redis.Protocol;
using Tokiota.Redis.Utilities;

namespace Tokiota.Redis
{
    internal class RedisConnection : IRedisConnection
    {
        private static readonly IOperationFactory OperationFactory = new OperationFactory();
        private RedisSocket socket;

        public RedisConnection(string host, int port, bool useSsl, TimeSpan timeout)
        {
            this.Host = host;
            this.Port = port;
            this.UseSsl = useSsl;
            this.ConnectionTimeout = timeout;
            this.ReceiveTimeout = timeout;
        }

        public bool Connected { get { return this.socket != null && this.socket.IsAlive; } }

        public string Host { get; set; }

        public int Port { get; set; }

        public bool UseSsl { get; set; }

        public TimeSpan ConnectionTimeout { get; set; }

        public TimeSpan ReceiveTimeout { get; set; }

        public event EventHandler Connecting;

        public event EventHandler Disconnecting;

        public void SendExpectSuccess(params byte[][] commands)
        {
            this.CheckSocketStatus();
            this.ExecuteOperation(OperationFactory.CreateSuccessOperation(this.socket), commands);
        }

        public long SendExpectLong(params byte[][] commands)
        {
            this.CheckSocketStatus();
            return this.ExecuteOperation(OperationFactory.CreateInt64Operation(this.socket), commands);
        }

        public double SendExpectDouble(params byte[][] commands)
        {
            this.CheckSocketStatus();
            return this.ExecuteOperation(OperationFactory.CreateDoubleOperation(this.socket), commands);
        }

        public string SendExpectString(params byte[][] commands)
        {
            this.CheckSocketStatus();
            return this.ExecuteOperation(OperationFactory.CreateStringOperation(this.socket), commands);
        }

        public byte[] SendExpectData(params byte[][] commands)
        {
            this.CheckSocketStatus();
            return this.ExecuteOperation(OperationFactory.CreateDataOperation(this.socket), commands);
        }

        public byte[][] SendExpectMultiData(params byte[][] commands)
        {
            this.CheckSocketStatus();
            return this.ExecuteOperation(OperationFactory.CreateMultiDataOperation(this.socket), commands);
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.OnDisconnecting();
                this.socket.Dispose();
                this.socket = null;
            }
        }

        private void CheckSocketStatus()
        {
            if (this.Connected) return;

            if (this.socket != null)
            {
                this.socket.Dispose();
                this.socket = null;
            }

            this.socket = new RedisSocket(this.Host, this.Port, this.UseSsl, this.ConnectionTimeout, this.ConnectionTimeout);
            this.OnConnecting();
        }

        private T ExecuteOperation<T>(IOperation<T> operation, byte[][] commands)
        {
            return operation.Execute(commands);
        }

        private void OnConnecting()
        {
            var handler = this.Connecting;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        private void OnDisconnecting()
        {
            var handler = this.Disconnecting;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }
    }
}
