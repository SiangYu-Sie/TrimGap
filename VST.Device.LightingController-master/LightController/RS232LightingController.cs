using System;
using System.IO.Ports;
using System.Security.Cryptography;
using System.Threading;

namespace LightController
{
    public class Rs232LightingController : DefaultLightingController, IRS232
    {
        private readonly int _baudRate = 19200;
        private readonly int _dataBits = 8;
        private readonly StopBits _stopBits = StopBits.One;
        private Handshake _handshake;
        private Parity _parity;

        public Rs232LightingController(string modelName, int channels = 0) : base(modelName, channels)
        {
            Port = new SerialPort();
        }


        public override bool IsConnected => Port.IsOpen;

        public override CommunicationInterfaces CommunicationInterface { get; protected set; } =
            CommunicationInterfaces.RS232;

        public override EventHandler<string> CommandSent { get; set; }
        public override string HostName { get; protected set; }
        public override int Delay { get; protected set; } = 20;
        public override bool AutoUpdate { get; set; }

        public SerialPort Port { get; private set; }

        public bool isOK = false;
        public bool isCOM = false;
        public override void Open(string portName)
        {
            try
            {
                if (IsConnected) Close();
                HostName = portName;
                Port = new SerialPort
                {
                    BaudRate = _baudRate,
                    DataBits = _dataBits,
                    Parity = _parity,
                    PortName = HostName,
                    StopBits = _stopBits,
                    Handshake = _handshake
                };
                Port.Open();
                if (Port.IsOpen)
                {
                    isOK = true;
                }
                else
                {
                    isOK = false;
                }

                SetIntensity(1, 0);
                string Portreply = Port.ReadExisting();
                if (Portreply == ":010600010000F8\r\n")
                {
                    isCOM = true;
                }
                else
                {
                    isCOM = false;
                }
            }
            catch (Exception)
            {
                isOK = false;
                isCOM = false;
            }
        }

        public override void Close()
        {
            Port.Close();
            Port.Dispose();
        }

        public override void SendCommand(string command)
        {
            if (!Port.IsOpen) return;
            Port.Write(command);
            CommandSent?.Invoke(this, command);
            Thread.Sleep(Delay);
        }

        public override void SetIntensity(int channel, int intensity)
        {
            var message = $":0106{channel:D4}{intensity.ToString("X4")}";
            var command = $"{message}{Sum(channel, intensity)}{CommandEndStr}";
            SendCommand(command);
            //var message = $":0106{channel:D4}{intensity.ToString("X4")}";
            //var command = $"{message}{CheckSum(message)}{CommandEndStr}";
            //var command = $":01060001008078{CommandEndStr}";
            //SendCommand(command);
            //if (AutoUpdate)
            //    SetOnOff(channel, true);
        }
        public string Sum(int channel, int intensity)
        {
            int total = 1 + 6 + channel + intensity;
            if (total > 255)
            {
                total -= 255;
            }
            int Sum = 255 - total + 1;
            return Sum.ToString("X");
        }

        public override void SetStrobeMode(int channel, StrobeModes mode)
        {
            var modeInt = (int)mode;
            var message = $"{CommandHeaderStr}{channel:D2}S{modeInt:D2}{0U:X2}";
            var command = $"{message}{CheckSum(message)}{CommandEndStr}";
            SendCommand(command);
            if (AutoUpdate)
                SetOnOff(channel, true);
        }

        public override void SetOnOff(int channel, bool mode)
        {
            var modeInt = mode ? 1 : 0;
            var message = $"{CommandHeaderStr}{channel:D2}L{modeInt:D1}{0U:X2}";
            var command = $"{message}{CheckSum(message)}{CommandEndStr}";
            SendCommand(command);
        }

        public override void Initialize()
        {
            foreach (var channel in _channelControllers.Values) channel.Initialize();
        }
    }
}