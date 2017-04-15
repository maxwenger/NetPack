using System;
using System.Linq;
using CommandLine;
using CommandLine.Text;

namespace NetPack.Options
{
    public class Options
    {
        [ParserState]
        public IParserState LastParserState { get; set; }

        [VerbOption("iflist", HelpText = "Lists interface information.")]
        public InterfaceListSubOptions IfListVerb { get; set; }

        [VerbOption("ping", HelpText = "Pings group of specified IPs.")]
        public PingSubOptions PingVerb { get; set; }

        [HelpVerbOption]
        public string GetUsage(string verb)
        {
            var help = new HelpText();

            if (LastParserState?.Errors.Any() == true)
            {
                var errors = help.RenderParsingErrorsText(this, 2);

                if (!string.IsNullOrEmpty(errors))
                {
                    help.AddPreOptionsLine(string.Concat(Environment.NewLine, "ERROR(S):"));
                    help.AddPreOptionsLine(errors);
                }
            }
            else
            {
                help = HelpText.AutoBuild(this, verb);
            }

            return help;
        }
    }
}
