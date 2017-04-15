using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetPack.Commands;

namespace NetPack
{
    class Program
    {
        private static Dictionary<string, ICommand> Commands = new Dictionary<string, ICommand>()
        {
            {"iflist", new InterfaceListCommand()}
        };

        static void Main(string[] args)
        {
            var active = args.Length <= 0;

            if (active)
            {
                Console.Out.Write("Type exit to quit NetPack.");
            }

            do
            {

                /*
                var commandInput = "";
                if (active)
                {
                    Console.Out.Write("\nNetPack: ");
                    commandInput = Console.ReadLine();
                } else if (args.Length >= 2)
                {
                    commandInput = args[1];
                }

                if(string.IsNullOrWhiteSpace(commandInput))
                {
                    Console.Out.Write(Commands.Keys.ToArray().ToString());
                }

                if (Commands.TryGetValue(commandInput, out ICommand command))
                {
                    command.Execute(args);
                }
                else
                {
                    Console.Out.WriteLine($"Bad input : {commandInput}");
                }*/
                if (active)
                {
                    Console.Out.Write("NetPack: ");
                    var input = Console.ReadLine();

                    if (input == null || input.ToLower().Equals("exit"))
                    {
                        active = false;
                    }
                    else
                    {
                        args = ParseArguments(input);
                    }
                }

                if (args.Length <= 0)
                {
                    Console.Out.WriteLine("Avalible Commands: ");
                    foreach (var k in Commands.Keys)
                    {
                        Console.Out.WriteLine(k);
                    }
                    Console.Out.WriteLine();
                } else if (Commands.TryGetValue(args[0], out ICommand command))
                {
                    command.Execute(args);
                }
                else
                {
                    Console.Out.WriteLine($"Bad input : {args[0]}");
                }

            } while (active);
        }

        static string[] ParseArguments(string commandLine)
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
            return (new string(parmChars)).Split('\n');
        }
    }
}
