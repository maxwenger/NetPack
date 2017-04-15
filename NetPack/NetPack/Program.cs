using System;
using CommandLine;
using NetPack.Commands;
using NetPack.Options;

namespace NetPack
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var active = args.Length <= 0;

            if (active)
            {
                Console.Out.WriteLine("Type exit to quit NetPack.");
            }

            do
            {
                if (active)
                {
                    GetActiveStateInput(ref args, ref active);
                }

                ParseAruments(args);
            } while (active);
        }

        private static void GetActiveStateInput(ref string[] args, ref bool active)
        {
            Console.Out.Write("NetPack: ");
            var input = Console.ReadLine();

            if (input == null || input.ToLower().Equals("exit"))
            {
                active = false;
            }
            else
            {
                args = StringToArguments(input);
            }
        }

        private static string[] StringToArguments(string commandLine)
        {
            if (string.IsNullOrWhiteSpace(commandLine))
            {
                return new string[0];
            }

            var parmChars = commandLine.ToCharArray();
            var inQuote = false;
            for (var index = 0; index < parmChars.Length; index++)
            {
                if (parmChars[index] == '"')
                {
                    inQuote = !inQuote;
                }

                if (!inQuote && parmChars[index] == ' ')
                {
                    parmChars[index] = '\n';
                }
            }
            return new string(parmChars).Split('\n');
        }

        private static void ParseAruments(string[] args)
        {
            var invokedVerb = "";
            var invokedVerbInstance = new object();

            var options = new Options.Options();
            if (Parser.Default.ParseArguments(args, options,
                (verb, subOptions) =>
                {
                    invokedVerb = verb;
                    invokedVerbInstance = subOptions;
                }))
            {
                SendToExecuter(invokedVerb, invokedVerbInstance);
            }
        }

        private static void SendToExecuter(string invokedVerb, object invokedVerbInstance)
        {
            switch (invokedVerb)
            {
                case "iflist":
                    new InterfaceListCommand().Execute((InterfaceListSubOptions) invokedVerbInstance);
                    break;
                case "ping":
                    new PingCommand().Execute((PingSubOptions)invokedVerbInstance);
                    break;
                case "confbuilder":
                    new ConfigurationBuilderCommand().Execute((ConfigureationBuilderSubOptions)invokedVerbInstance);
                    break;
                case "clear":
                    Console.Clear();
                    break;
            }
        }
    }
}