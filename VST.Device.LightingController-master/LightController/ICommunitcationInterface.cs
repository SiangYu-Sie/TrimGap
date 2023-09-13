using System.IO.Ports;
using System.Net.Sockets;

namespace LightController
{
    public interface ICommunicationInterfaces   
    {

    }

    public interface IRS232 : ICommunicationInterfaces
    {
        SerialPort Port { get; }
    }

    public interface IEthernet : ICommunicationInterfaces
    {
        TcpClient Client { get; }

        bool ChangeIP(string ip);
    }
}