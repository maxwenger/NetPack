using CommandLine;
using CommandLine.Text;

namespace NetPack.Options
{
    public class OptionsBase
    {
        protected OptionsBase()
        {
        }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
                current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}