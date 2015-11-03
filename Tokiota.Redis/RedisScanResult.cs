namespace Tokiota.Redis
{
    using Utilities;

    public class RedisScanResult
    {
        public ulong NextPage { get; private set; }

        public byte[][] Data { get; private set; }

        public string[] DataString { get { return this.Data.ToUtf8Strings(); } }

        internal RedisScanResult() : this(0, new byte[0][])
        {
        }

        internal RedisScanResult(ulong nextPage, byte[][] data)
        {
            this.NextPage = nextPage;
            this.Data = data;
        }
    }
}
