using System;
using System.IO;
using System.Net.Sockets;

namespace Tokiota.Redis.Net
{
    internal class RedisNetworkStream : Stream
    {
        private readonly Socket socket;

        public RedisNetworkStream(Socket socket)
        {
            this.socket = socket;
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Flush()
        {
        }

        public override long Length
        {
            get { throw new NotSupportedException(); }
        }

        public override long Position
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            SocketError errorCode;
            var retval = this.socket.BeginReceive(buffer, offset, count, SocketFlags.None, out errorCode, callback, state);

            if (errorCode == SocketError.Success)
                return retval;

            throw new IOException(string.Format("Failed to read from the socket '{0}'. Error: {1}", this.socket.RemoteEndPoint, errorCode));
        }

        public override int EndRead(IAsyncResult asyncResult)
        {
            SocketError errorCode;
            var retval = this.socket.EndReceive(asyncResult, out errorCode);

            if (errorCode == SocketError.Success && retval > 0)
                return retval;

            throw new IOException(string.Format("Failed to read from the socket '{0}'. Error: {1}", this.socket.RemoteEndPoint, errorCode));
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            SocketError errorCode;
            var retval = this.socket.Receive(buffer, offset, count, SocketFlags.None, out errorCode);

            if (errorCode == SocketError.Success && retval > 0)
                return retval;

            throw new IOException(string.Format("Failed to read from the socket '{0}'. Error: {1}", this.socket.RemoteEndPoint, errorCode == SocketError.Success ? "?" : errorCode.ToString()));
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            SocketError errorCode;
            var retval = this.socket.BeginSend(buffer, offset, count, SocketFlags.None, out errorCode, callback, state);

            if (errorCode == SocketError.Success)
                return retval;

            throw new IOException(string.Format("Failed to write in the socket '{0}'. Error: {1}", this.socket.RemoteEndPoint, errorCode));
        }

        public override void EndWrite(IAsyncResult asyncResult)
        {
            SocketError errorCode;
            this.socket.EndSend(asyncResult, out errorCode);

            if (errorCode != SocketError.Success)
            {
                throw new IOException(string.Format("Failed to write in the socket '{0}'. Error: {1}", this.socket.RemoteEndPoint, errorCode));
            }
  
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            SocketError errorCode;
            this.socket.Send(buffer, offset, count, SocketFlags.None, out errorCode);

            if (errorCode != SocketError.Success)
            {
                throw new IOException(string.Format("Failed to write in the socket '{0}'. Error: {1}", this.socket.RemoteEndPoint, errorCode == SocketError.Success ? "?" : errorCode.ToString()));
            }
        }
    }
}
