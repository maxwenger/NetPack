using CommandLine;

namespace NetPack.Options
{
    public class InterfaceListSubOptions : OptionsBase
    {
        [Option('i', "ip", Required = false,
            HelpText = "Filter by IP address")]
        public string IpFilter { get; set; }
    }
}