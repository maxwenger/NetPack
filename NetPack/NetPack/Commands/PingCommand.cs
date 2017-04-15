using System.Runtime.InteropServices;
using NetPack.Options;
using NetPack.Services;

namespace NetPack.Commands
{
    public class PingCommand
    {
        public void Execute(PingSubOptions options)
        {
            Ping pinger;
            if (options.IpAddresses != null)
            {
                pinger = new Ping(options.IpAddresses);
            } else if (options.File != null)
            {
                pinger = new Ping(options.File);
            }
            else
            {
                return;
            }


            if (options.Interval >= 1)
            {
                pinger.SendContinious(options.Interval);
            }
            else
            {
                pinger.SendAll();
            }
        }

    }
}
