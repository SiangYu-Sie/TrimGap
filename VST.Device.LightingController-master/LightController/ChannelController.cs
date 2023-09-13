using System;

namespace LightController
{
    public class ChannelController : IChannelController
    {
        public int Index { get; }
        public bool? IsOn { get; private set; }
        public StrobeModes? Mode { get; private set; }
        public int? Intensity { get; private set; }

        public ILightingController Owner { get; }

        public ChannelController(ILightingController owner, int index)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            Index = index;
        }

        public void SendCommand(string command)
        {
            Owner.SendCommand(command);
        }

        public void SetIntensity(int intensity)
        {
            Owner.SetIntensity(Index, intensity);
            Intensity = intensity;
        }

        public void SetStrobeMode(StrobeModes mode)
        {
            Owner.SetStrobeMode(Index, mode);
            Mode = mode;
        }

        public void SetOnOff(bool mode)
        {
            Owner.SetOnOff(Index, mode);
            IsOn = mode;
        }

        public void Initialize()
        {
            SetStrobeMode(StrobeModes.F1);
            SetOnOff(false);
            SetIntensity(0);
        }
    }
}