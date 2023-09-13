using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    /// <summary>
    /// 設備裝置 - 三色燈(含蜂鳴器)
    /// </summary>
    public sealed class SignalTower : IAsyncDisposable
    {
        private readonly AsyncLock asyncLock = new AsyncLock();
        private readonly Dictionary<SignalLihgts, IDigitalOutput> lights;
        private readonly IDigitalOutput doB;
        private IAsyncDisposable flash;

        public SignalTower(IDigitalOutput red, IDigitalOutput yellow, IDigitalOutput green, IDigitalOutput buzzer)
        {
            lights = new Dictionary<SignalLihgts, IDigitalOutput>()
            {
                [SignalLihgts.Red] = red,
                [SignalLihgts.Yellow] = yellow,
                [SignalLihgts.Green] = green
            };

            doB = buzzer;
        }

        /// <summary>
        /// 取得或設定燈光閃爍的時間間隔。
        /// </summary>
        public int FlashingInterval { get; set; } = 1000;

        public async Task DisposeAsync() => await TurnOffAsync();

        /// <summary>
        /// 開啟指定的色燈及蜂鳴器。
        /// </summary>
        /// <param name="light"></param>
        /// <param name="withbuzzer"></param>
        public void SwitchOn(SignalLihgts light, bool withbuzzer = false)
        {
            TurnOff();
            using (asyncLock.Lock())
            {
                lights[light].On();
                if (withbuzzer) doB.On();
            }
        }

        /// <summary>
        /// 使指定的色燈閃爍及蜂鳴器。
        /// </summary>
        /// <param name="light"></param>
        /// <param name="withbuzzer"></param>
        public void FlashOn(SignalLihgts light, bool withbuzzer)
        {
            TurnOff();
            using (asyncLock.Lock())
            {
                flash = WhileTask.Run(() =>
                {
                    lights[light].State = !lights[light].State;
                    if (withbuzzer) doB.State = !doB.State;
                },
                FlashingInterval);
            }
        }

        /// <summary>
        /// 關閉所有色燈及蜂鳴器。
        /// </summary>
        /// <returns></returns>
        public async Task TurnOffAsync()
        {
            using (await asyncLock.LockAsync())
            {
                if (flash != null) await flash.DisposeAsync();
                foreach (var light in lights.Values) light.Off();
                doB.Off();
            }
        }

        /// <summary>
        /// 關閉所有色燈及蜂鳴器。
        /// </summary>
        public void TurnOff() => Task.Run(TurnOffAsync).Wait();
    }

    /// <summary>
    /// 訊號燈
    /// </summary>
    public enum SignalLihgts
    {
        Red,

        Yellow,

        Green
    }

    public static partial class ModuleExtension
    {
        public static void TurnOnRed(this SignalTower tower, bool withbuzzer = false)
            => tower.SwitchOn(SignalLihgts.Red, withbuzzer);

        public static void TurnOnYellow(this SignalTower tower, bool withbuzzer = false)
            => tower.SwitchOn(SignalLihgts.Yellow, withbuzzer);

        public static void TurnOnGreen(this SignalTower tower, bool withbuzzer = false)
            => tower.SwitchOn(SignalLihgts.Green, withbuzzer);

        public static void FlashRed(this SignalTower tower, bool withbuzzer = false)
            => tower.FlashOn(SignalLihgts.Red, withbuzzer);

        public static void FlashYellow(this SignalTower tower, bool withbuzzer = false)
            => tower.FlashOn(SignalLihgts.Yellow, withbuzzer);

        public static void FlashGreen(this SignalTower tower, bool withbuzzer = false)
            => tower.FlashOn(SignalLihgts.Green, withbuzzer);
    }
}
