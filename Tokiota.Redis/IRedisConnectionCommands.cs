namespace Tokiota.Redis
{
    using System;

    public interface IRedisConnectionCommands
    {
        void Auth(string password);
        string Echo(string text);
        bool Ping();
        void Quit();
        void Select(int db);
    }
}
