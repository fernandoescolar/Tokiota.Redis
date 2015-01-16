using System;
using Tokiota.Redis.Utilities;

namespace Tokiota.Redis.Protocol
{
    internal class KeysCommands : CommandsBase, IRedisKeysCommands
    {
        public KeysCommands(IRedisConnection connection) : base(connection)
        {
        }

        public bool Del(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.Del, key.ToByteArray()) == 1;
        }

        public long Del(params string[] args)
        {
            if (args == null)
                throw new ArgumentNullException("args");

            var cmds = new byte[args.Length + 1][];
            cmds[0] = Commands.Del;

            var index = 1;
            foreach (var arg in args)
            {
                cmds[index++] = arg.ToByteArray();
            }

            return this.Connection.SendExpectLong(cmds);
        }

        public byte[] Dump(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectData(Commands.Dump, key.ToByteArray());
        }

        public bool Exists(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.Exists, key.ToByteArray()) == 1;
        }

        public bool Expire(string key, int seconds)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.Expire, key.ToByteArray(), seconds.ToByteArray()) == 1;
        }

        public bool ExpireAt(string key, int time)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.ExpireAt, key.ToByteArray(), time.ToByteArray()) == 1;
        }

        public byte[][] Keys(string pattern)
        {
            if (pattern == null)
                throw new ArgumentNullException("pattern");

            return this.Connection.SendExpectMultiData(Commands.Keys, pattern.ToByteArray());
        }

        public void Migrate(string host, int port, string key, int destinationDb, long timeoutMs)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            this.Connection.SendExpectSuccess(Commands.Migrate, host.ToByteArray(), port.ToByteArray(), key.ToByteArray(), destinationDb.ToByteArray(), timeoutMs.ToByteArray());
        }

        public bool Move(string key, int db)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.Move, key.ToByteArray(), db.ToByteArray()) == 1;
        }

        public bool Persist(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.Persist, key.ToByteArray()) == 1;
        }

        public bool PExpire(string key, long ttlMs)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.PExpire, key.ToByteArray(), ttlMs.ToByteArray()) == 1;
        }

        public bool PExpireAt(string key, long unixTimeMs)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.PExpireAt, key.ToByteArray(), unixTimeMs.ToByteArray()) == 1;
        }

        public long PTtl(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.PTtl, key.ToByteArray());
        }

        public string RandomKey()
        {
            return this.Connection.SendExpectData(Commands.RandomKey).ToUtf8String();
        }

        public void Rename(string oldKeyname, string newKeyname)
        {
            if (oldKeyname == null)
                throw new ArgumentNullException("oldKeyname");

            if (newKeyname == null)
                throw new ArgumentNullException("newKeyname");

            this.Connection.SendExpectSuccess(Commands.Rename, oldKeyname.ToByteArray(), newKeyname.ToByteArray());
        }

        public bool RenameNx(string oldKeyname, string newKeyname)
        {
            if (oldKeyname == null)
                throw new ArgumentNullException("oldKeyname");

            if (newKeyname == null)
                throw new ArgumentNullException("newKeyname");

            return this.Connection.SendExpectLong(Commands.RenameNx, oldKeyname.ToByteArray(), newKeyname.ToByteArray()) == 1;
        }

        public byte[] Restore(string key, long expireMs, byte[] dumpValue)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectData(Commands.Restore, key.ToByteArray(), expireMs.ToByteArray(), dumpValue);
        }

        public byte[] Sort(string key, byte[][] ags)
        {
            throw new NotImplementedException();
        }

        public long Ttl(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectLong(Commands.Ttl, key.ToByteArray());
        }

        public string Type(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return this.Connection.SendExpectData(Commands.Type, key.ToByteArray()).ToUtf8String();
        }

        public byte[] Scan(ulong cursor, int count = 10, string match = null)
        {
            throw new NotImplementedException();
        }
    }
}
