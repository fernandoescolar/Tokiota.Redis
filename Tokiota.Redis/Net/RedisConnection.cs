using System;
using System.Globalization;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using Tokiota.Redis.Utilities;

namespace Tokiota.Redis.Net
{
    internal class RedisConnection : IRedisConnection
    {
        private const int BufferSize = 16 * 1024;

        private static readonly byte[] EndData = new byte[] { (byte)'\r', (byte)'\n' };

        private readonly ByteBuffer buffer = new ByteBuffer();

        private Socket socket;
        private SslStream sslStream;
        private BufferedStream bstream;

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
        
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool SendCommand(params byte[][] args)
        {
            if (this.socket == null)
                this.Connect();

            this.buffer.Write(("*" + args.Length).ToByteArray());
            this.buffer.Write(EndData);
            foreach (var arg in args)
            {
                this.buffer.Write(("$" + arg.Length).ToByteArray());
                this.buffer.Write(EndData);
                this.buffer.Write(arg);
                this.buffer.Write(EndData);
            }

            return this.SendBuffer();
        }

        public void SendExpectSuccess(params byte[][] args)
        {
            if (!this.SendCommand(args))
                throw new Exception("Unable to connect");

            this.ExpectSuccess();
        }

        public int SendExpectInt(params byte[][] args)
        {
            if (!this.SendCommand(args))
                throw new Exception("Unable to connect");

            return this.ExpectInt();
        }

        public long SendExpectLong(params byte[][] args)
        {
            if (!this.SendCommand(args))
                throw new Exception("Unable to connect");

            return this.ExpectLong();
        }

        public double SendExpectDouble(params byte[][] args)
        {
            if (!this.SendCommand(args))
                throw new Exception("Unable to connect");

            return this.ExpectDouble();
        }

        public string SendExpectString(params byte[][] args)
        {
            if (!this.SendCommand(args))
                throw new Exception("Unable to connect");

            return this.ExpectString();
        }

        public byte[] SendExpectData(params byte[][] args)
        {
            if (!this.SendCommand(args))
                throw new Exception("Unable to connect");

            return this.ReadData();
        }

        public byte[][] SendExpectMultiData(params byte[][] args)
        {
            if (!this.SendCommand(args))
                throw new Exception("Unable to connect");

            return this.ReadMultiData();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.OnDisconnecting();
                this.socket.Close();
                this.buffer.Dispose();
                this.socket = null;
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
        }

        private bool SendBuffer()
        {
            if (this.socket == null)
                this.Connect();
            if (this.socket == null)
                return false;

            try
            {
                this.buffer.StartRead();
                var bytes = new byte[BufferSize];
                var bytesRead = 0;
                while ((bytesRead = this.buffer.Read(bytes, 0, bytes.Length)) > 0)
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
                this.buffer.Clear();
            }

            return true;
        }

        private void ExpectSuccess()
        {
            var c = this.bstream.ReadByte();
            if (c == -1)
                throw new Exception("No more data");

            var s = this.ReadLine();
            if (c == '-')
                throw new Exception(s.StartsWith("ERR ") ? s.Substring(4) : s);
        }

        private int ExpectInt()
        {
            var c = this.bstream.ReadByte();
            if (c == -1)
                throw new Exception("No more data");

            var s = this.ReadLine();
            if (c == '-')
                throw new Exception(s.StartsWith("ERR ") ? s.Substring(4) : s);

            if (c == ':')
            {
                int i;
                if (int.TryParse(s, out i))
                    return i;
            }

            throw new Exception("Unknown reply on integer request: " + c + s);
        }

        private long ExpectLong()
        {
            var c = this.bstream.ReadByte();
            if (c == -1)
                throw new Exception("No more data");

            var s = this.ReadLine();
            if (c == '-')
                throw new Exception(s.StartsWith("ERR ") ? s.Substring(4) : s);

            if (c == ':')
            {
                long l;
                if (long.TryParse(s, out l))
                    return l;
            }

            throw new Exception("Unknown reply on long request: " + c + s);
        }

        private double ExpectDouble()
        {

            var c = this.bstream.ReadByte();
            if (c == -1)
                throw new Exception("No more data");

            var s = this.ReadLine();
            if (c == '-')
                throw new Exception(s.StartsWith("ERR ") ? s.Substring(4) : s);

            if (c == ':')
            {
                double d;
                if (double.TryParse(s, NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture.NumberFormat, out d))
                    return d;
            }

            throw new Exception("Unknown reply on double request: " + c + s);
        }

        private string ExpectString()
        {
            var c = this.bstream.ReadByte();
            if (c == -1)
                throw new Exception("No more data");

            var s = this.ReadLine();
            if (c == '-')
                throw new Exception(s.StartsWith("ERR ") ? s.Substring(4) : s);

            if (c == '+')
            {
                return s;
            }

            throw new Exception("Unknown reply on string request: " + c + s);
        }

        private string ReadLine()
        {
            var sb = new StringBuilder();
            int c;

            while ((c = this.bstream.ReadByte()) != -1)
            {
                if (c == '\r')
                    continue;
                if (c == '\n')
                    break;

                sb.Append((char)c);
            }

            return sb.ToString();
        }

        private byte[] ReadData()
        {
            var s = this.ReadLine();
            if (s.Length == 0)
                throw new Exception("Zero length respose");

            char c = s[0];
            if (c == '-')
                throw new Exception(s.StartsWith("-ERR ") ? s.Substring(5) : s.Substring(1));

            if (c == '$')
            {
                if (s == "$-1")
                    return null;
                int n;

                if (int.TryParse(s.Substring(1), out n))
                {
                    byte[] retbuf = new byte[n];

                    int bytesRead = 0;
                    do
                    {
                        int read = this.bstream.Read(retbuf, bytesRead, n - bytesRead);
                        if (read < 1)
                            throw new Exception("Invalid termination mid stream");
                        bytesRead += read;
                    }
                    while (bytesRead < n);
                    if (this.bstream.ReadByte() != '\r' || bstream.ReadByte() != '\n')
                        throw new Exception("Invalid termination");

                    return retbuf;
                }
                throw new Exception("Invalid length");
            }

            throw new Exception("Unexpected reply: " + s);
        }

        private byte[][] ReadMultiData()
        {
            var c = this.bstream.ReadByte();
            if (c == -1)
                throw new Exception("No more data");

            var s = this.ReadLine();
            if (c == '-')
                throw new Exception(s.StartsWith("ERR ") ? s.Substring(4) : s);

            if (c == '*')
            {
                int result;
                if (int.TryParse(s, out result))
                {
                    if (result == -1)
                        return new byte[0][];

                    var numArray = new byte[result][];
                    for (int index = 0; index < result; ++index)
                        numArray[index] = this.ReadData();

                    return numArray;
                }

                throw new Exception("Invalid length");
            }

            throw new Exception("Unexpected reply: " + s);
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
