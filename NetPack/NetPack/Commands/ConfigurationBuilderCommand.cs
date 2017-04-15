using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetPack.Options;
using NetPack.Services;

namespace NetPack.Commands
{
    public class ConfigurationBuilderCommand
    {
        public void Execute(ConfigureationBuilderSubOptions options)
        {
            var builder = new ConfigurationBuilder(options.File, options.Delimitor, options.DeaaultParamDelimitor);
            var config = builder.StartBuilder();

            if (!string.IsNullOrWhiteSpace(options.AppendFile))
            {
                using (var writer = File.AppendText(options.AppendFile))
                {
                    writer.WriteLine(config);
                }
            }
        }
    }
}
