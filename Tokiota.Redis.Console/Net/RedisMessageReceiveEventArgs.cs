namespace Tokiota.Redis.Console.Net
{
    using System;

    delegate void RedisMessageReceiveEventHandler(object sender, RedisMessageReceiveEventArgs args);

    public class RedisMessageReceiveEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public RedisMessageReceiveEventArgs(string message)
        {
            this.Message = message;
        }
    }
}
