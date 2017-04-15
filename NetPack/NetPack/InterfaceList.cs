using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Core;

namespace NetPack
{
    public class DeviceInformation
    {
        private IEnumerable<LivePacketDevice> _devices;

        public void LoadAllLocalMachiene() =>
            _devices = LivePacketDevice.AllLocalMachine;

        public void MatchIp(string ip) =>
            _devices = _devices.Where(d => d.Addresses.Any(a => a.Address.ToString().Contains(ip)));
        

        public override string ToString() =>
            _devices == null ? "" : _devices.Aggregate("", (current, device) => current + GetOutput(device));
        
        private static string GetOutput(LivePacketDevice device)
        {
            var output = "";
            output += $"  Name  . . . . . . : {device.Name}\n";
            output += $"    Description . . . : {device?.Description}\n";
            output += "    Loopback  . . . . : " +
                          (((device.Attributes & DeviceAttributes.Loopback) == DeviceAttributes.Loopback)
                               ? "yes"
                               : "no") + "\n";
            foreach (var t in device.Addresses)
            {
                output += $"\n    {t?.Address.Family}\n";
                output += $"\t      Address . . . . . : {t?.Address}\n";
                output += $"\t      Netmask . . . . . : {t?.Netmask}\n";
                output += $"\t      Broadcast . . . . : {t?.Broadcast}\n";
                output += $"\t      Destination . . . : {t?.Destination}\n";
            }
            output += "\n\n";
            return output;
        }
    }
}
