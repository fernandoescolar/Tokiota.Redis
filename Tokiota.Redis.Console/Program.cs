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
                var host = GetArgumentValue("Host", "localhost", args, 0);
                var port = int.Parse(GetArgumentValue("Port", "6379", args, 1));
                var password = GetArgumentValue("Password", string.Empty, args, 2);
                var useSsl = port != 6379;

                using (var redis = new RedisConnection(host, port, 30, useSsl))
                {
                    redis.MessageReceived += OnMessageReceived;
                    if (!string.IsNullOrEmpty(password))
                    {
                        AutoAuth(redis, password);
                    }

                    CommandLoop(redis);
                    redis.MessageReceived -= OnMessageReceived;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error: " + ex.Message);
            }
        }

        private static string GetArgumentValue(string name, string defaultValue, string[] args, int index)
        {
            if (args.Length > index)
            {
                return args[index];
            }

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[name]))
            {
                return ConfigurationManager.AppSettings[name];
            }

            return defaultValue;
        }

        private static void AutoAuth(IRedisConnection redis, string password)
        {
            System.Console.WriteLine("> AUTH ***********");
            redis.SendCommand("AUTH", password);
        }

        private static void CommandLoop(IRedisConnection redis)
        {
            do
            {
                WritePrompt();
                var line = System.Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }
                else if (!redis.SendCommand(CommandLineParser.Parse(line)))
                {
                    System.Console.WriteLine("Error: could not send the command");
                }
                else if (line == "QUIT")
                {
                    break;
                }
            } while (redis.Connected);
        }

        private static void WritePrompt()
        {
            System.Console.Write("> ");
        }

        private static void OnMessageReceived(object sender, RedisMessageReceiveEventArgs args)
        {
            var rewritePrompt = System.Console.CursorLeft > 0;
            System.Console.SetCursorPosition(0, System.Console.CursorTop);

            var color = System.Console.ForegroundColor;

            if (args.Message.StartsWith("-")) System.Console.ForegroundColor = ConsoleColor.Red;
            else if (args.Message.StartsWith("(nil)")) System.Console.ForegroundColor = ConsoleColor.Yellow;
            else if (args.Message.StartsWith("+")) System.Console.ForegroundColor = ConsoleColor.Green;
            else System.Console.ForegroundColor = ConsoleColor.White;

            System.Console.WriteLine(args.Message);

            System.Console.ForegroundColor = color;
            if (rewritePrompt)
            {
                WritePrompt();
            }
        }
    }
}
