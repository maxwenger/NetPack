using NetPack.Options;
using NetPack.Services;

namespace NetPack.Commands
{
    public class PingCommand
    {
        public void Execute(PingSubOptions options)
        {
            var pinger = new Ping(options.IpAddresses);
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
