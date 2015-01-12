using System;
using System.Configuration;
using Tokiota.Redis.Console.Net;

namespace Tokiota.Redis.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var host = ConfigurationManager.AppSettings["Host"];
                var port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                var useSsl = bool.Parse(ConfigurationManager.AppSettings["UseSsl"]);

                using(var redis = new RedisConnection(host, port, 30, useSsl))
                {
                    redis.MessageReceived += OnMessageReceived;
                    do {
                        System.Console.Write("> ");
                        var line = System.Console.ReadLine();
                        if (!redis.SendCommand(CommandLineParser.Parse(line)))
                        {
                            System.Console.WriteLine("Error: could not send the command");
                        }
                        else 
                        {
                            if (line == "QUIT")
                            {
                                redis.MessageReceived -= OnMessageReceived;
                                break;
                            }
                        }
                    } while(redis.Connected);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error: " + ex.Message);
            }

            System.Console.WriteLine("Press enter to exit...");
            System.Console.ReadLine();
        }

        private static void OnMessageReceived(object sender, RedisMessageReceiveEventArgs args)
        {
            System.Console.WriteLine(args.Message);
        }
    }
}
