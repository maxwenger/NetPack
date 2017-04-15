using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using Net = System.Net.NetworkInformation;

namespace NetPack.Services
{
    public class Ping
    {
        private readonly List<InterfaceProfile> interfaces;
        private Timer timer;

        public Ping(IEnumerable<string> ips)
        {
            WriteToConsole = true;
            interfaces = new List<InterfaceProfile>();
            foreach (var ip in ips)
            {
                interfaces.Add(new InterfaceProfile(ip));
            }
        }

        public Ping(string file)
        {
            WriteToConsole = true;

            if (!File.Exists(file))
            {
                Console.Out.WriteLine("Broken file!");
                return;
            }

            var reader = new StreamReader(file);
            interfaces = new List<InterfaceProfile>();
            while (!reader.EndOfStream)
            {
                var a = reader.ReadLine().Split('\t');
                switch (a.Length)
                {
                    case 2:
                        interfaces.Add(new InterfaceProfile(a[1], a[0]));
                        break;
                    case 1:
                        interfaces.Add(new InterfaceProfile(a[0]));
                        break;
                }
            }

        }

        public bool WriteToConsole { get; set; }

        public void SendContinious(int interval)
        {
            timer = new Timer();
            timer.Elapsed += OnTimedEvent;
            timer.Interval = interval;
            timer.Enabled = true;

            Console.ReadKey();
            timer.Enabled = false;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            SendAll();
        }

        public void SendAll()
        {
            var ping = new Net.Ping();
            var replies = new Net.PingReply[interfaces.Count];
            var errors = new Net.PingException[interfaces.Count];

            for (var i = 0; i < interfaces.Count; i++)
            {
                var ip = interfaces[i];
                try
                {
                    replies[i] = ping.Send(ip.Address);
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
                if (timer.Enabled)
                {
                    Console.Clear();
                    Console.Out.WriteLine("Press any key to exit.");

                }
                Console.Out.WriteLine($"{"Interface",-20} {"IP",-45} {"TTL",-10} {"Time",-10} {"Status",-10}");
                for (var i = 0; i < replies.Length; i++)
                {
                    var r = replies[i];
                    if (r == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Out.WriteLine(
                            $"{interfaces[i].Name,-20} {interfaces[i].Address,-45} {"",-10} {"",-10} {Net.IPStatus.Unknown,-10}");
                    }
                    else
                    {
                        Console.ForegroundColor = r.Status == Net.IPStatus.Success
                            ? ConsoleColor.Green
                            : ConsoleColor.Red;

                        Console.Out.WriteLine(
                            $"{interfaces[i].Name,-20} {interfaces[i].Address,-45} {r?.Options?.Ttl,-10} {r?.RoundtripTime,-10} {r.Status,-10}");
                    }
                }
                Console.ResetColor();
            }
        }

        internal class InterfaceProfile
        {
            public InterfaceProfile(string ip)
            {
                Address = ip;
            }

            public InterfaceProfile(string ip, string name) : this(ip)
            {
                Name = name;
            }

            public string Name { get; set; }
            public string Address { get; set; }
        }
    }
}