using System;
using System.Collections.Generic;
using System.Timers;
using Net = System.Net.NetworkInformation;

namespace NetPack.Services
{
    public class Ping
    {
        private readonly List<string> ips;
        private Timer timer;

        public Ping(IEnumerable<string> ips)
        {
            WriteToConsole = true;
            this.ips = new List<string>(ips);
        }

        public bool WriteToConsole { get; set; }

        public void SendContinious(int interval)
        {
            if (WriteToConsole)
            {
                Console.Out.WriteLine("\nPress any key to exit.");
            }

            timer = new Timer
            {
                Interval = interval,
                Enabled = true
            };
            timer.Elapsed += OnTimedEvent;

            Console.ReadKey();
            timer.Enabled = false;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (WriteToConsole)
            {
                Console.Clear();
            }
            SendAll();
        }

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
                    replies[i] = ping.Send(ip);
                }
                catch (Net.PingException e)
                {
                    errors[i] = e;
                }
            }

            UpdateConsole(replies);
        }

        private void UpdateConsole(Net.PingReply[] replies)
        {
            if (WriteToConsole)
            {
                Console.Out.WriteLine($"{"Interface",-20} {"IP",-45} {"TTL",-10} {"Time",-10} {"Status",-10}");
                for (var i = 0; i < replies.Length; i++)
                {
                    var r = replies[i];
                    if (r == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Out.WriteLine($"{"",-20} {ips[i],-45} {"",-10} {"",-10} {Net.IPStatus.Unknown,-10}");
                    }
                    else
                    {
                        Console.ForegroundColor = r.Status == Net.IPStatus.Success
                            ? ConsoleColor.Green
                            : ConsoleColor.Red;

                        Console.Out.WriteLine(
                            $"{"",-20} {ips[i],-45} {r?.Options?.Ttl,-10} {r?.RoundtripTime,-10} {r.Status,-10}");
                    }
                }
                Console.ResetColor();
            }
        }
    }
}