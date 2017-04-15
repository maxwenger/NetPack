using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Core;

namespace NetPack
{
    public class DeviceInformation
    {
        private IList<LivePacketDevice> devices;

        public DeviceInformation()
        {
            devices = LivePacketDevice.AllLocalMachine;
        }

        public override string ToString()
        {
            var output = "";

            foreach (var device in devices)
            {
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
            }

            return output;
        }

    }
}
