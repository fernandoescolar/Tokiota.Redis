namespace Retwis.Managers
{
    using System.Configuration;
    using Tokiota.Redis;

    public class RedisManager
    {
        private static RedisManager currentInstace;

        public static RedisManager Current
        {
            get 
            {
                if (currentInstace == null) currentInstace = new RedisManager(new RedisRelayClientFactory(ConfigurationManager.AppSettings["Redis.Host"], int.Parse(ConfigurationManager.AppSettings["Redis.Port"]), ConfigurationManager.AppSettings["Redis.Password"]));
                return currentInstace;
            }
        }

        private readonly IRedisClientFactory factory;
        
        private RedisManager(IRedisClientFactory factory)
        {
            this.factory = factory;
        }

        public IRedisClient GetClient()
        {
            return this.factory.GetCurrent();
        }
    }
}