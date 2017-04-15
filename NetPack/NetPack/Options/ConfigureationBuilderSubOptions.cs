using CommandLine;

namespace NetPack.Options
{
    public class ConfigureationBuilderSubOptions
    {
        [Option('t', "template-file", Required = true, HelpText = "Deliminated template file to parse through")]
        public string File { get; set; }

        [Option('a', "append-file", Required = false,
            HelpText = "Append or create given file with finished configureation")]
        public string AppendFile { get; set; }

        [Option('d', "delimtor", DefaultValue = '?', Required = false, HelpText = "Message Deliminator")]
        public char Delimitor { get; set; }

        [Option("default-param-delimtor", DefaultValue = ':', Required = false,
            HelpText = "Default paramater delimitor.")]
        public char DeaaultParamDelimitor { get; set; }
    }
}