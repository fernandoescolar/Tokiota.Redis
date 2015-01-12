using System;
using System.IO;
using System.Text;

namespace Tokiota.Redis.Utilities
{
    internal sealed class ByteBuffer : IDisposable
    {
        private const int DefaultBufferSize = 2 * 1024 * 1024; //2Mb

        private readonly MemoryStream buffer;
        private readonly BinaryReader reader;
        private readonly BinaryWriter writer;

        private int length = 0;

        public ByteBuffer()
            : this(DefaultBufferSize)
        {
        }

        public ByteBuffer(int size)
        {
            this.buffer = new MemoryStream(size);
            this.reader = new BinaryReader(this.buffer);
            this.writer = new BinaryWriter(this.buffer);
        }

        public int Length { get { return this.length; } }

        public void Clear()
        {
            this.buffer.Position = 0;
            this.length = 0;
        }

        public void Write(byte[] source)
        {
            this.Write(source, 0, source.Length);
        }

        public void Write(byte[] source, int offset, int length)
        {
            this.writer.Write(source, offset, length);
            this.length += length;
        }

        public void StartRead()
        {
            this.buffer.Position = 0;
        }

        public int Read(byte[] source, int offset, int length)
        {
            if (this.buffer.Position >= this.length) return -1;
            if (this.buffer.Position + length >= this.length)
            {
                length = (int)(this.length - this.buffer.Position);
            }

            return this.reader.Read(source, offset, length);
        }

        public string ReadString()
        { 
            var sb = new StringBuilder();
            int c;

            while ((c = this.reader.ReadByte()) != -1)
            {
                if (c == '\r')
                    continue;
                if (c == '\n')
                    break;
                if (this.buffer.Position > this.length)
                    break;

                sb.Append((char)c);
            }

            return sb.ToString();
        }

        public void Dispose()
        {
            buffer.Dispose();
        }
    }
}
