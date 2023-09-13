using System;

namespace LightController
{
    /// <summary>
    /// Hello
    /// </summary>
    public enum StrobeModes
    {
        /// <summary>
        /// 
        /// </summary>
        None,
        /// <summary>
        /// 
        /// </summary>
        F1,
        /// <summary>
        /// 
        /// </summary>
        F2,
        /// <summary>
        /// 
        /// </summary>
        F3,
        /// <summary>
        /// 
        /// </summary>
        F4,
        /// <summary>
        /// 
        /// </summary>
        F5,
        /// <summary>
        /// 
        /// </summary>
        F6,
        /// <summary>
        /// 
        /// </summary>
        F7,
        /// <summary>
        /// 
        /// </summary>
        F8,
        /// <summary>
        /// 
        /// </summary>
        F9,
        /// <summary>
        /// 
        /// </summary>
        F10,
    }

    public enum CommunicationInterfaces
    {
        None,
        Ethernet,
        RS232
    }

    /// <summary>
    /// 提供一般控制器功能的Interface
    /// </summary>
    public interface ILightingController : IInitialize
    {
        EventHandler<string> CommandSent { get; set; }
        string HostName { get; }
        int Delay { get; }
        bool AutoUpdate { get; set; }
        bool IsConnected { get; }
        string ModelName { get; }
        StrobeModes StrobeMode { get; }
        CommunicationInterfaces CommunicationInterface { get; }
        IChannelController this[int index] { get; }
        int ChannelCount { get; }
        string CommandHeaderStr { get; }
        string CommandEndStr { get; }
        void SendCommand(string command);
        void Open(string host);
        void Close();
        void SetIntensity(int channel, int intensity);
        void SetIntensity(IChannelController channel, int intensity);
        void SetStrobeMode(int channel, StrobeModes mode);
        void SetStrobeMode(IChannelController channel, StrobeModes mode);
        void SetOnOff(int channel, bool mode);
        void SetOnOff(IChannelController channel, bool mode);
        string CheckSum(string command);
    }
}