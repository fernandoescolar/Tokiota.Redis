namespace Tokiota.Redis.Utilities
{
    using System.IO;

    internal sealed class PooledByteBuffer : PooledObject
    {
        private const int DefaultBufferSize = 2 * 1024 * 1024; //2Mb

        private readonly MemoryStream buffer;
        private readonly BinaryReader reader;
        private readonly BinaryWriter writer;

        private int length = 0;

        public PooledByteBuffer()
            : this(DefaultBufferSize)
        {
        }

        public PooledByteBuffer(int size)
        {
            this.buffer = new MemoryStream(size);
            this.reader = new BinaryReader(this.buffer);
            this.writer = new BinaryWriter(this.buffer);
        }

        public int Length { get { return this.length; } }
        
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

        protected override void OnResetState()
        {
            this.buffer.Position = 0;
            this.length = 0;

            base.OnResetState();
        }

        protected override void OnReleaseResources()
        {
            this.buffer.Dispose();
            this.reader.Dispose();
            this.writer.Dispose();

            base.OnReleaseResources();
        }
    }
}
