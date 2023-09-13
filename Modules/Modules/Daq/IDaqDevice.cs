using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    /// <summary>
    /// 資料擷取卡, IO卡基底類別
    /// </summary>
    public interface IDigitalDAQ
    {
        bool ReadDigitalInput(int groupId, int channel);

        bool ReadDigitalOutput(int groupId, int channel);

        void WriteDigitalOutput(int groupId, int channel, bool state);
    }

    /// <summary>
    /// IO卡
    /// </summary>
    public interface IDaqDevice : IDigitalDAQ
    {
        IReadOnlyList<IDigitalInput> DiChannels { get; }

        IReadOnlyList<IDigitalOutput> DoChannels { get; }
    }
}
