using System;
using Tokiota.Redis.Net;
using Tokiota.Redis.Utilities;

namespace Tokiota.Redis.Protocol
{
    internal class OperationRequest : IOperationRequest
    {
        private const int ReadBufferSize = 16 * 1024;
        private static readonly Pool<PooledByteBuffer> BufferPool = new Pool<PooledByteBuffer>(2, 20);
        private static readonly byte[] EndData = new byte[] { (byte)'\r', (byte)'\n' };

        public bool Send(RedisSocket redisSocket, byte[][] commands)
        {
            try
            {
                using (var buffer = BufferPool.GetObject())
                {
                    WriteInBuffer(buffer, commands);
                    SendBuffer(redisSocket, buffer);
                }
            }
            catch (Exception)
            {
                return false;
            }
          
            return true;
        }

        private static void WriteInBuffer(PooledByteBuffer buffer, byte[][] commands)
        {
            buffer.Write(("*" + commands.Length).ToByteArray());
            buffer.Write(EndData);
            foreach (var arg in commands)
            {
                buffer.Write(("$" + arg.Length).ToByteArray());
                buffer.Write(EndData);
                buffer.Write(arg);
                buffer.Write(EndData);
            }
        }

        private static void SendBuffer(RedisSocket socket, PooledByteBuffer buffer)
        {
            buffer.StartRead();
            var bytes = new byte[ReadBufferSize];
            var bytesRead = 0;
            while ((bytesRead = buffer.Read(bytes, 0, bytes.Length)) > 0)
            {
                socket.Write(bytes, 0, bytesRead);
            }
        }
    }
}
