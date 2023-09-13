using IO4;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskBand;

namespace Modules
{
    public class PCI_1756 : IDaqDevice
    {
        private BDaq mIO;
        public PCI_1756()
        {
            mIO = new BDaq("PCI-1756#0");
            mIO.initial(@"D:\");

            DiChannels = Enumerable.Range(0, mIO.In.Count())
            .Select(i => new DiChannel(this, 0, i))
               .ToList();
            DoChannels = Enumerable.Range(0, mIO.Out.Count())
            .Select(i => new DoChannel(this, 0, i))
                .ToList();
        }


        public IReadOnlyList<IDigitalInput> DiChannels { get; private set; }

        public IReadOnlyList<IDigitalOutput> DoChannels { get; private set; }
        public bool ReadDigitalInput(int groupId, int channel)
        {
            mIO.ReadIn();
            return mIO.In[channel];
        }

        public bool ReadDigitalOutput(int groupId, int channel)
        {
            mIO.ReadOut();
            return mIO.Out[channel];
        }

        public void WriteDigitalOutput(int groupId, int channel, bool state)
        {
            var arr = (bool[])mIO.Out.Clone();
            arr[channel] = state;
            mIO.WriteOut(arr);
        }
    }
}
