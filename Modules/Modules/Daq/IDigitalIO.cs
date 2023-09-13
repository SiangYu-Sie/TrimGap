using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modules
{
    /// <summary>
    /// 數位輸入
    /// </summary>
    public interface IDigitalInput
    {
        string Name { get; set; }
        bool State { get; }
    }
    /// <summary>
    /// 數位輸出
    /// </summary>
    public interface IDigitalOutput
    {
        string Name { get; set; }
        bool State { get; set; }
    }

    public static partial class ModuleExtension
    {
        public static async Task UntilAsync(this IDigitalInput di, bool state, int millisecondTimeout, CancellationToken cancellationToken = default)
        {
            bool isWaited = await Task.Run(() =>
            SpinWait.SpinUntil(() =>
            di.State == state || cancellationToken.IsCancellationRequested, millisecondTimeout));

            if (!isWaited) throw new TimeoutException($"{di.Name} wait state({di.State}) timeout.");
            if (cancellationToken.IsCancellationRequested) throw new OperationCanceledException($"{di.Name} wait state({di.State}) be cancelled.");
        }

        public static async Task UntilOnAsync(this IDigitalInput di, int millisecondTimeout = 30000)
            => await di.UntilAsync(true, millisecondTimeout);
        public static async Task UntilOnAsync(this IDigitalInput di, int millisecondTimeout = 30000, CancellationToken cancellationToken = default)
            => await di.UntilAsync(true, millisecondTimeout, cancellationToken);
        public static async Task UntilOffAsync(this IDigitalInput di, int millisecondTimeout = 30000)
            => await di.UntilAsync(false, millisecondTimeout);
        public static async Task UntilOffAsync(this IDigitalInput di, int millisecondTimeout = 30000, CancellationToken cancellationToken = default)
            => await di.UntilAsync(false, millisecondTimeout, cancellationToken);

        public static void On(this IDigitalOutput digOutput)
        {
            digOutput.State = true;
        }
        public static void Off(this IDigitalOutput digOutput)
        {
            digOutput.State = false;
        }


        /// <summary>
        /// 訂閱數位輸入的狀態變換，並在指定的調度器上呼叫指派的動作。
        /// </summary>
        /// <param name="di"></param>
        /// <param name="action">當數位輸入狀態為 true 時須執行的動作。</param>
        /// <param name="scheduler">指定執行 action 的調度器。</param>
        /// <param name="interval">詢問數位輸入狀態的時間間隔。</param>
        /// <returns>可取消此訂閱的物件。</returns>
        public static IDisposable SubscribeStateChanged(this IDigitalInput di, Action<IDigitalInput> action, IScheduler scheduler, int interval = 50)
            => Observable
            .Interval(TimeSpan.FromMilliseconds(interval), NewThreadScheduler.Default)
            .Select(_ => di.State)
            .DistinctUntilChanged()
            .ObserveOn(scheduler)
            .Subscribe(_ => action(di));

        /// <summary>
        /// 訂閱數位輸入的狀態變換，並在指定的調度器上呼叫指派的動作。
        /// </summary>
        /// <param name="do"></param>
        /// <param name="action">當數位輸入狀態為 true 時須執行的動作。</param>
        /// <param name="scheduler">指定執行 action 的調度器。</param>
        /// <param name="interval">詢問數位輸入狀態的時間間隔。</param>
        /// <returns>可取消此訂閱的物件。</returns>
        public static IDisposable SubscribeStateChanged(this IDigitalOutput _do, Action<IDigitalOutput> action, IScheduler scheduler, int interval = 50)
            => Observable
            .Interval(TimeSpan.FromMilliseconds(interval), NewThreadScheduler.Default)
            .Select(_ => _do.State)
            .DistinctUntilChanged()
            .ObserveOn(scheduler)
            .Subscribe(_ => action(_do));
    }
}
