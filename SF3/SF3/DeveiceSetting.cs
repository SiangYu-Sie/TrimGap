using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpTest
{
    public sealed class DeviceSetting
    {
        // Ethernet
        public Int32 TcpPort { get; set; }
        public string Address { get; set; }
        public int Timeout { get; set; }
        public bool UseServer { get; set; }
    }
}
