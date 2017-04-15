using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetPack.Options;
using NetPack.Services;

namespace NetPack.Commands
{
    public class PingCommand
    {
        public void Execute(PingSubOptions options)
        {
            var pinger = new Ping(options.IpAddresses);
            pinger.SendAll();
        }

    }
}
