namespace Tokiota.Redis.Net
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net.Security;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;

    internal class RedisSocket : IDisposable
    {
        private bool isAlive;
        private Socket socket;
        private Stream outputStream;
        private Stream inputStream;

        public RedisSocket(string host, int port, bool useSsl, TimeSpan connectionTimeout, TimeSpan receiveTimeout)
        {
            this.isAlive = true;

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.NoDelay = true;

            var timeout = connectionTimeout == TimeSpan.MaxValue
                            ? Timeout.Infinite
                            : (int)connectionTimeout.TotalMilliseconds;

            var rcv = receiveTimeout == TimeSpan.MaxValue
                ? Timeout.Infinite
                : (int)receiveTimeout.TotalMilliseconds;

            socket.ReceiveTimeout = rcv;
            socket.SendTimeout = rcv;
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);

            ConnectWithTimeout(socket, host, port, timeout);

            Stream stream = new RedisNetworkStream(socket);
            if (useSsl) 
            {
                var sslStream = new SslStream(stream, false, null, null);
                sslStream.AuthenticateAsClient(host);

                if (!sslStream.IsEncrypted)
                {
                    throw new Exception(string.Format("Could not establish an encrypted connection to {0}", host));
                }

                stream = sslStream;
            }

            this.socket = socket;
            this.outputStream = stream;
            this.inputStream = new BufferedStream(stream);
        }

        ~RedisSocket()
        {
            try 
            { 
                this.Dispose(true); 
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error disposing RedisSocket: " + ex.Message);
            }
        }

        public bool IsAlive
        {
            get { return this.isAlive; }
        }

        public bool Connected
        {
            get { return !((this.socket.Poll(1000, SelectMode.SelectRead) && (this.socket.Available == 0)) || !this.socket.Connected || !this.IsAlive); }
        }

        public int ReadByte()
        {
            this.CheckDisposed();

            try
            {
                return this.inputStream.ReadByte();
            }
            catch (IOException)
            {
                this.isAlive = false;
                throw;
            }
        }

        public void Read(byte[] buffer, int offset, int count)
        {
            this.CheckDisposed();

            int read = 0;
            int shouldRead = count;

            while (read < count)
            {
                try
                {
                    int currentRead = this.inputStream.Read(buffer, offset, shouldRead);
                    if (currentRead < 1)
                        continue;

                    read += currentRead;
                    offset += currentRead;
                    shouldRead -= currentRead;
                }
                catch (IOException)
                {
                    this.isAlive = false;
                    throw;
                }
            }
        }

        public string ReadLine()
        {
            var sb = new StringBuilder();
            int c;

            while ((c = this.ReadByte()) != -1)
            {
                if (c == '\r')
                    continue;
                if (c == '\n')
                    break;

                sb.Append((char)c);
            }

            return sb.ToString();
        }

        public void Write(byte[] data, int offset, int length)
        {
            this.CheckDisposed();
            this.outputStream.Write(data, offset, length);
        }

        public void Reset()
        {
            this.inputStream.Flush();

            int available = this.socket.Available;

            if (available > 0)
            {
                var data = new byte[available];

                this.Read(data, 0, available);
            }
        }

        public void Dispose()
        {
            this.Dispose(false);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                GC.SuppressFinalize(this);

                try
                {
                    if (socket != null)
                        try { this.socket.Close(); }
                        catch (Exception ex) { Trace.TraceError("Error disposing RedisSocket: " + ex.Message); }

                    if (this.outputStream != null)
                        this.outputStream.Dispose();
                    if (this.inputStream != null)
                        this.inputStream.Dispose();

                    this.outputStream = null;
                    this.inputStream = null;
                    this.socket = null;
                }
                catch (Exception ex)
                {
                    Trace.TraceError("Error disposing RedisSocket: " + ex.Message);
                }
            }
        }

        private void CheckDisposed()
        {
            if (this.socket == null)
                throw new ObjectDisposedException("RedisSocket");
        }

        private static void ConnectWithTimeout(Socket socket, string host, int port, int timeout)
        {
            var mre = new ManualResetEvent(false);

            socket.BeginConnect(host, port, iar =>
            {
                try { using (iar.AsyncWaitHandle) socket.EndConnect(iar); }
                catch(Exception ex) { Trace.TraceError("Error connectiong RedisSocket: " + ex.Message); }

                mre.Set();
            }, null);

            if (!mre.WaitOne(timeout) || !socket.Connected)
                using (socket)
                    throw new TimeoutException(string.Format("Could not connect to {0}:{1}", host, port));
        }
    }
}
