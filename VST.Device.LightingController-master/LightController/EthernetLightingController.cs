using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LightController
{
    public class EthernetLightingController : DefaultLightingController, IEthernet
    {
        public EthernetLightingController(string modelName, int channels = 0) : base(modelName, channels)
        {
            Client = new TcpClient();
        }

        public override EventHandler<string> CommandSent { get; set; }
        public override string HostName { get; protected set; } = "192.168.11.20";

        public override int Delay { get; protected set; } = 10;
        public override bool AutoUpdate { get; set; }

        public override bool IsConnected => Client.Connected;
        public TcpClient Client { get; private set; }
        public override CommunicationInterfaces CommunicationInterface { get; protected set; } = CommunicationInterfaces.Ethernet;

        public bool isOK = false;

        public override void Open(string ip = "192.168.11.20")
        {
            if (IsConnected) Close();
            HostName = ip;
            Client = new TcpClient();
            var result = Client.BeginConnect(ip, 1000, null, null);
            var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));
            if (!success || !Client.Connected)
            {
                isOK = false;
                //throw new Exception("Failed to connect.");
            }
            else
            {
                isOK = true;
            }
        }

        public bool ChangeIP(string ip)
        {
            if (!ip.StartsWith("192.168.11"))
                return false;
            if (!IPAddress.TryParse(ip, out var ipAddress))
                return false;
            if (ipAddress.AddressFamily != AddressFamily.InterNetwork)
                return false;
            var ips = ipAddress.ToString().Split('.');
            var last = long.Parse(ips[3]);
            var message = $"@00E01192.168.011.{last:D3}.";
            var command = $"{message}{CheckSum(message)}{CommandEndStr}";
            SendCommand(command);
            return true;
        }

        public override void Close()
        {
            if (!IsConnected) return;
            var stream = Client.GetStream();
            stream.Close();
            Client.Close();
        }

        public override void SendCommand(string command)
        {
            if (!IsConnected) return;
            var stream = Client.GetStream();
            var data = Encoding.ASCII.GetBytes(command);
            stream.Write(data, 0, data.Length);
            CommandSent?.Invoke(this, command);
            Thread.Sleep(Delay);
        }

        public override void SetIntensity(int channel, int intensity)
        {
            var message = $"{CommandHeaderStr}{channel:D2}F{intensity:D3}";
            var command = $"{message}{CheckSum(message)}{CommandEndStr}";
            SendCommand(command);
            if (AutoUpdate)
                SetOnOff(channel, true);
        }

        public override void SetStrobeMode(int channel, StrobeModes mode)
        {
            var modeInt = (int)mode;
            var message = $"{CommandHeaderStr}{channel:D2}S{modeInt:D2}";
            var command = $"{message}{CheckSum(message)}{CommandEndStr}";
            SendCommand(command);
            if (AutoUpdate)
                SetOnOff(channel, true);
        }

        public override void SetOnOff(int channel, bool mode)
        {
            var modeInt = mode ? 1 : 0;
            var message = $"{CommandHeaderStr}{channel:D2}L{modeInt:D1}";
            var command = $"{message}{CheckSum(message)}{CommandEndStr}";
            SendCommand(command);
        }

        public override void Initialize()
        {
            //// Set all channel strobe F1
            //var msg = $"@FFS00";
            //SendCommand($"{msg}{CheckSum(msg)}{CommandEndStr}");

            //// Set all channel off
            //msg = "@FFL0";
            //SendCommand($"{msg}{CheckSum(msg)}{CommandEndStr}");

            //// Set all channel intensity 0
            //msg = "@FFF000";
            //SendCommand($"{msg}{CheckSum(msg)}{CommandEndStr}");

            foreach (var channel in _channelControllers.Values) channel.Initialize();
        }
    }
}