namespace LightController
{
    /// <summary>
    /// 通道控制介面
    /// </summary>
    public interface IChannelController : IInitialize
    {
        /// <summary>
        /// 通道索引
        /// </summary>
        int Index { get; }
        /// <summary>
        /// 通道開關狀態，由於控制器不會主動同步狀態，因此初始值為null，在設定過一次之後才會有值。
        /// </summary>
        bool? IsOn { get; }
        /// <summary>
        /// 閃爍模式，由於控制器不會主動同步狀態，因此初始值為null，在設定過一次之後才會有值。
        /// </summary>
        StrobeModes? Mode { get; }
        /// <summary>
        /// 通道亮度，由於控制器不會主動同步狀態，因此初始值為null，在設定過一次之後才會有值。
        /// </summary>
        int? Intensity { get; }
        /// <summary>
        /// 所屬控制器
        /// </summary>
        /// <value></value>
        ILightingController Owner { get; }
        /// <summary>
        /// 送出指令
        /// </summary>
        /// <param name="command">指令</param>
        void SendCommand(string command);
        /// <summary>
        /// 設定亮度
        /// </summary>
        /// <param name="intensity">亮度，可設定值為0~255</param>
        void SetIntensity(int intensity);
        /// <summary>
        /// 設定閃爍模式
        /// </summary>
        /// <param name="mode">閃爍模式，請參考StrobeModes</param>
        void SetStrobeMode(StrobeModes mode);
        /// <summary>
        /// 設定開關
        /// </summary>
        /// <param name="mode">開關</param>
        void SetOnOff(bool mode);
    }
}