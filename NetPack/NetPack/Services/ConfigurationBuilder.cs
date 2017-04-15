using System;
using System.IO;

namespace NetPack.Services
{
    public class ConfigurationBuilder
    {
        private readonly string configTemplate;
        private readonly char defaultParamDel;
        private readonly char delimitor;

        public ConfigurationBuilder(string file, char delimitor, char defaultParamaterDelimator)
        {
            CanWriteToConsole = true;
            if (file != null && File.Exists(file))
            {
                var reader = new StreamReader(file);
                configTemplate = reader.ReadToEnd();
                this.delimitor = delimitor;
                defaultParamDel = defaultParamaterDelimator;
            }
        }

        public bool CanWriteToConsole { get; set; }

        public string StartBuilder()
        {
            var builtConfig = "";
            var parsed = configTemplate.Split(delimitor);

            for (var i = 0; i < parsed.Length; i++)
            {
                if (i % 2 != 0)
                {
                    var options = parsed[i].Split(defaultParamDel);
                    if (options.Length > 1)
                    {
                        Console.Out.Write($"{options[0]} ({options[1]}?):");
                        var param = Console.ReadLine();
                        builtConfig += string.IsNullOrWhiteSpace(param) ? options[1] : param;
                    }
                    else
                    {
                        Console.Out.Write(parsed[i] + ": ");
                        builtConfig += Console.ReadLine() + "";
                    }
                }
                else
                {
                    builtConfig += parsed[i];
                }
            }

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Out.WriteLine(builtConfig);
            Console.ResetColor();
            return builtConfig;
        }
    }
}