using System;
using System.IO;
using System.Xml;

namespace NetPack.Services
{
    public class ConfigurationBuilder
    {
        private string configTemplate;
        private char delimitor;

        public ConfigurationBuilder(string file, char delimitor)
        {
            CanWriteToConsole = true;
            if (file != null && File.Exists(file))
            {
                var reader = new StreamReader(file);
                configTemplate = reader.ReadToEnd();
                this.delimitor = delimitor;
            }
        }

        public bool CanWriteToConsole { get; set; }

        public string StartBuilder()
        {
            var builtConfig = "";
            var parsed = configTemplate.Split(delimitor);

            for (var i = 0; i < parsed.Length; i++)
            {
                if (i % 2 == 0)
                {
                    Console.Out.Write(parsed[i] + ": ");
                    builtConfig += Console.ReadLine();
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