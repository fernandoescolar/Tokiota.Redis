namespace Tokiota.Redis.Console
{
    using System.Collections.Generic;

    internal class CommandLineParser
    {
        public static string[] Parse(string line)
        {
            var result = new List<string>();
            var current = string.Empty;
            var block = false;
            for (var i = 0; i < line.Length; i++)
            {
                var c = line[i];

                if (c == '"')
                {
                    block = !block;
                }
                else if (c == ' ' && !block)
                {
                    result.Add(current);
                    current = string.Empty;
                }
                else
                {
                    current += c;
                }
            }

            if (!string.IsNullOrEmpty(current)) result.Add(current);
            
            return result.ToArray();
        }
    }
}
