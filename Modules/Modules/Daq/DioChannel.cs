using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    public class DiChannel : IDigitalInput
    {
        private readonly IDigitalDAQ daq;

        public DiChannel(IDigitalDAQ daq,int groupId, int channel)
        {
            this.daq = daq;
            this.GroupId = groupId;
            this.Channel = channel;

            Name = $"{daq} DI({groupId}-{channel})";
        }
        
        public int GroupId { get; }

        public int Channel { get; }

        public string Name { get; set; }

        public bool State => daq.ReadDigitalInput(GroupId, Channel);

    }
    public class DoChannel : IDigitalOutput
    {
        private readonly IDigitalDAQ daq;

        public DoChannel(IDigitalDAQ daq, int groupId, int channel)
        {
            this.daq = daq;
            this.GroupId = groupId;
            this.Channel = channel;

            Name = $"{daq} DI({groupId}-{channel})";
        }

        public int GroupId { get; }

        public int Channel { get; }

        public string Name { get; set; }

        public bool State 
        {
            get => daq.ReadDigitalOutput(GroupId, Channel);
            set => daq.WriteDigitalOutput(GroupId, Channel, value);
        }
    }
}
