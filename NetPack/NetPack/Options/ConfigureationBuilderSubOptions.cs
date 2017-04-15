
using CommandLine;

namespace NetPack.Options
{
    public class ConfigureationBuilderSubOptions
    {
        [Option('t', "template-file", Required = true, HelpText = "Deliminated template file to parse through")]
        public string File { get; set; }

        [Option('d', "delimtor", DefaultValue = '?' ,Required = false, HelpText = "Deliminated template file to parse through")]
        public char Delimitor { get; set; }


    }
}
