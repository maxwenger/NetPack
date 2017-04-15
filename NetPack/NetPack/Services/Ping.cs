using System;
using System.Collections.Generic;
using Net = System.Net.NetworkInformation;

namespace NetPack.Services
{
    public class Ping
    {
        private List<string> ips;

        public Ping(IEnumerable<string> ips)
        {
            WriteToConsole = true;
            this.ips = new List<string>(ips);
        }

        public bool WriteToConsole { get; set; }

        public void SendAll()
        {
            var ping = new Net.Ping();
            var replies = new Net.PingReply[ips.Count];
            var errors = new Net.PingException[ips.Count];

            for (var i = 0; i < ips.Count; i++)
            {
                var ip = ips[i];
                try
                {
                    replies[i] = (ping.Send(ip));
                }
                catch (Net.PingException e)
                {
                    errors[i]= e;
                }
            }

            if (WriteToConsole)
            {
                for (var i = 0; i < replies.Length; i++)
                {
                    var r = replies[i];
                    if (r == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Out.WriteLine($"{ips[i]} | {Net.IPStatus.TimedOut}");
                    }
                    else
                    {
                        Console.ForegroundColor = r.Status == Net.IPStatus.Success
                            ? ConsoleColor.Green
                            : ConsoleColor.Red;

                        Console.Out.WriteLine($"{r.Address} | {r.Options.Ttl} | {r.RoundtripTime} | {r.Status}");
                    }
                }
                Console.ResetColor();
            }

        }
    }
}