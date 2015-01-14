using System;
using System.Globalization;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Tokiota.Redis.Utilities;

namespace Tokiota.Redis.Console.Net
{
    internal class RedisConnection : IRedisConnection
    {
        private const int BufferSize = 16 * 1024;

        private static readonly byte[] EndData = new byte[] { (byte)'\r', (byte)'\n' };

        private readonly ByteBuffer outBuffer = new ByteBuffer();
        private readonly ByteBuffer inBuffer = new ByteBuffer();

        private Socket socket;
        private SslStream sslStream;
        private BufferedStream bstream;

        private int indentCount = 0;

        public RedisConnection(string host, int port, int sendTimeout, bool useSsl)
        {
            this.Host = host;
            this.Port = port;
            this.SendTimeout = sendTimeout;
            this.UseSsl = useSsl;
        }

        public string Host { get; set; }

        public int Port { get; set; }

        public int SendTimeout { get; set; }

        public bool UseSsl { get; set; }

        public bool Connected { get { return this.socket != null ? this.socket.Connected : false; } }

        public event EventHandler Connecting;

        public event EventHandler Disconnecting;

        public event RedisMessageReceiveEventHandler MessageReceived;

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool SendCommand(params string[] args)
        {
            return this.SendCommand(args.ToByteArrays());
        }

        public bool SendCommand(params byte[][] args)
        {
            if (this.socket == null)
                this.Connect();

            this.outBuffer.Write(("*" + args.Length).ToByteArray());
            this.outBuffer.Write(EndData);
            foreach (var arg in args)
            {
                this.outBuffer.Write(("$" + arg.Length).ToByteArray());
                this.outBuffer.Write(EndData);
                this.outBuffer.Write(arg);
                this.outBuffer.Write(EndData);
            }

            return this.SendBuffer();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.OnDisconnecting();
                this.socket.Close();
                this.outBuffer.Dispose();
                this.socket = null;
                this.bstream.Dispose();
                this.bstream = null;
            }
        }

        private void Connect()
        {
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.socket.NoDelay = true;
            this.socket.SendTimeout = SendTimeout;
            this.socket.Connect(Host, Port);
            if (!this.socket.Connected)
            {
                this.socket.Close();
                this.socket = null;
                return;
            }

            var stream = (Stream)new NetworkStream(this.socket);
            if (this.UseSsl)
            {
                this.sslStream = new SslStream(stream, false, null, null);
                this.sslStream.AuthenticateAsClient(this.Host);

                if (!this.sslStream.IsEncrypted)
                    throw new Exception("Could not establish an encrypted connection to " + this.Host);

                stream = (Stream)this.sslStream;
            }

            this.bstream = new BufferedStream(stream, BufferSize);
            this.OnConnecting();
            this.BeginReceive();
        }

        private bool SendBuffer()
        {
            if (this.socket == null)
                this.Connect();
            if (this.socket == null)
                return false;

            try
            {
                this.outBuffer.StartRead();
                var bytes = new byte[BufferSize];
                var bytesRead = 0;
                while ((bytesRead = this.outBuffer.Read(bytes, 0, bytes.Length)) > 0)
                {
                    if (this.sslStream == null)
                        this.socket.Send(bytes, 0, bytesRead, SocketFlags.None);
                    else
                        this.sslStream.Write(bytes, 0, bytesRead);
                }
            }
            catch (SocketException)
            {
                this.socket.Close();
                this.socket = null;

                return false;
            }
            finally
            {
                this.outBuffer.Clear();
            }

            return true;
        }

        private void BeginReceive()
        {
            var buffer = new byte[BufferSize];
            if (this.bstream != null)
            {
                this.bstream.BeginRead(buffer, 0, buffer.Length, EndReceive, buffer);
            }
        }

        private void EndReceive(IAsyncResult ar)
        {
            if (this.bstream == null) return;

            var buffer = ar.AsyncState as byte[];
            if (buffer != null)
            {
                try
                {
                    var read = this.bstream.EndRead(ar);
                    this.inBuffer.Write(buffer, 0, read);

                    if (read < buffer.Length)
                    {
                        if (this.inBuffer.Length > 0)
                        {
                            this.inBuffer.StartRead();
                            var message = this.ParseLine(this.inBuffer.ReadString());
                            this.OnMessageReceive(message);
                            this.inBuffer.Clear();
                        }
                    }
                    else
                    {
                        this.BeginReceive();
                    }
                }
                catch (IOException)
                {
                }
            }

            this.BeginReceive();
        }

        private string ParseLine(string line)
        {
            var sb = new StringBuilder();
            if (line.StartsWith("*"))
            {
                var size = int.Parse(line.Substring(1));
                sb.AppendLine("Array[" + size + "]");
                for (int i = 0; i < size; i++)
                {
                    this.indentCount++;
                    sb.AppendLine(new String(' ', this.indentCount * 2) + (i + 1) + ") " + this.ParseLine(this.inBuffer.ReadString()));
                    this.indentCount--;
                }
            }
            else if (line.StartsWith("$-1"))
            {
                sb.Append("(nil)");
            }
            else if (line.StartsWith("$"))
            {
                sb.Append(this.ParseLine(this.inBuffer.ReadString()));
            }
            else if (line.StartsWith(":"))
            {
                sb.Append(line.Substring(1));
            }
            else
            {
                sb.Append(line);
            }

            return sb.ToString();
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

        private void OnMessageReceive(string message)
        {
            var handler = this.MessageReceived;
            if (handler != null)
            {
                handler(this, new RedisMessageReceiveEventArgs(message));
            }
        }
    }
}
