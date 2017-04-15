using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace NetPack.Options
{
    public class PingSubOptions : OptionsBase
    {
        [OptionArray('l', "list", Required = true, HelpText = "List of target IPs")]
        public string[] IpAddresses { get; set; }

        [Option('t', "time", Required = false, HelpText = "Interval of time to continiously ping in ms.")]
        public int Interval { get; set; }
    }
}
