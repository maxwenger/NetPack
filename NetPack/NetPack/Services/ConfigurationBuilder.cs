using System;
using System.IO;
using System.Xml;

namespace NetPack.Services
{
    public class ConfigurationBuilder
    {
        private string configTemplate;
        private char delimitor;
        private char defaultParamDel;

        public ConfigurationBuilder(string file, char delimitor, char defaultParamaterDelimator)
        {
            CanWriteToConsole = true;
            if (file != null && File.Exists(file))
            {
                var reader = new StreamReader(file);
                configTemplate = reader.ReadToEnd();
                this.delimitor = delimitor;
                this.defaultParamDel = defaultParamaterDelimator;
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

            Console.Out.WriteLine(builtConfig);
            return builtConfig;
        }
    }
}