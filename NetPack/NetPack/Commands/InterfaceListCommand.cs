using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPack.Commands
{
    public class InterfaceListCommand : ICommand
    {
        public void Execute(string[] args)
        {
            Console.Out.Write(new DeviceInformation());
        }
    }
}
