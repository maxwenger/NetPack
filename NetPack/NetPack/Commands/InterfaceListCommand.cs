using System;
using CommandLine;
using NetPack.Options;

namespace NetPack.Commands
{
    public class InterfaceListCommand
    {
        public void Execute(InterfaceListSubOptions options)
        {
            var devices = new DeviceInformation();
            devices.LoadAllLocalMachiene();

             if (!string.IsNullOrWhiteSpace(options.IpFilter))
             {
                devices.MatchIp(options.IpFilter);
             }
            
            Console.Out.Write(devices);
        }
    }
}