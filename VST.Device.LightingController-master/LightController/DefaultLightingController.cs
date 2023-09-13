using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController
{
    public abstract class DefaultLightingController : ILightingController
    {
        protected Dictionary<int, IChannelController> _channelControllers;
        protected StrobeModes strobeMode;

        public abstract EventHandler<string> CommandSent { get; set; }
        public abstract string HostName { get; protected set; }
        public abstract int Delay { get; protected set; }
        public abstract bool AutoUpdate { get; set; }
        public abstract bool IsConnected { get; }
        public virtual string ModelName { get; protected set; } = "Default";
        public StrobeModes StrobeMode => StrobeModes.None;
        public abstract CommunicationInterfaces CommunicationInterface { get; protected set; }

        public IChannelController this[int index] => _channelControllers[index];
        public int ChannelCount => _channelControllers.Count;
        public string CommandHeaderStr => "@";
        public string CommandEndStr => "\r\n";

        public DefaultLightingController(string modelName, int channels = 0)
        {
            ModelName = modelName;
            _channelControllers = new Dictionary<int, IChannelController>();
            for (var i = 0; i < channels; i++)
            {
                var channel = new ChannelController(this, i);
                _channelControllers.Add(i, channel);
            }
        }

        public abstract void SendCommand(string command);
        public abstract void Open(string host);
        public abstract void Close();

        public abstract void SetIntensity(int channel, int intensity);


        public void SetIntensity(IChannelController channel, int intensity)
        {
            SetIntensity(channel.Index, intensity);
        }

        public abstract void SetStrobeMode(int channel, StrobeModes mode);
        //{
        //    var modeInt = (int)mode;
        //    var message = $"{CommandHeaderStr}{channel:D2}S{modeInt:D2}";
        //    var command = $"{message}{CheckSum(message)}{CommandEndStr}";
        //    SendCommand(command);
        //}

        public void SetStrobeMode(IChannelController channel, StrobeModes mode)
        {
            SetStrobeMode(channel.Index, mode);
        }

        public abstract void SetOnOff(int channel, bool mode);
        //{
        //    var modeInt = (mode) ? 1 : 0;
        //    var message = $"{CommandHeaderStr}{channel:D2}L{modeInt:D1}";
        //    var command = $"{message}{CheckSum(message)}{CommandEndStr}";
        //    SendCommand(command);
        //}

        public void SetOnOff(IChannelController channel, bool mode)
        {
            SetOnOff(channel.Index, mode);
        }

        public string CheckSum(string command)
        {
            var num1 = Encoding.ASCII.GetBytes(command).Aggregate<byte, byte>(0, (current, num2) => (byte)(current + num2));
            return $"{num1:X2}";
        }

        public abstract void Initialize();

    }
}