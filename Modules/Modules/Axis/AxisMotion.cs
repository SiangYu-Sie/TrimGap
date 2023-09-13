using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    public class AxisMotion
    {
        private static readonly TaskFactory tf = new TaskFactory(TaskCreationOptions.AttachedToParent, TaskContinuationOptions.AttachedToParent);
        private readonly TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        private Task task = Task.CompletedTask;
        public bool isStopping;

        private readonly Action move;
        private readonly Action stop;
        private readonly Action wait;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="move">開始動作。</param>
        /// <param name="stop">停止動作。</param>
        /// <param name="wait">等待完成。</param>
        public AxisMotion(Action move, Action stop, Action wait)
        {
            this.move = move;
            this.stop = stop;
            this.wait = wait;
        }

        private AxisMotion()
        {
            tcs.SetResult(true);
        }

        public static AxisMotion CompletedMotion => new AxisMotion();

        public bool IsCompleted => tcs.Task.IsCompleted;

        public bool IsPaused { get; private set; }

        public bool IsCanceled { get; private set; }

        public ConfiguredTaskAwaitable<bool> ConfigureAwait(bool continueOnCapturedContext)
            => tcs.Task.ConfigureAwait(continueOnCapturedContext);

        public TaskAwaiter<bool> GetAwaiter() => tcs.Task.GetAwaiter();

        public async Task DisposeAsync()
        {
            if (IsCompleted) return;

            isStopping = true;
            stop();
            await task.ConfigureAwait(false);
            tcs.TrySetCanceled();
        }

        public async Task PauseAsync()
        {
            // IsPaused 狀態必須先改為 true 讓停止後的狀態檢查可以判斷停止原因是呼叫 Pause 方法所引起的，
            // 注意：暫停並不會將 IsRunning 修改為 false， 狀態會維持為 true。 
            if (IsCompleted || IsPaused || isStopping) return;

            isStopping = true;
            stop();
            await task.ConfigureAwait(false);
            IsPaused = true;
        }

        public void Run()
        {
            if (IsCompleted) throw new InvalidOperationException("Motion was completed.");
            if (IsPaused) IsPaused = false;
            try
            {
                move();
                task = tf.StartNew(() =>
                {
                    try
                    {
                        wait();
                        if (isStopping) { isStopping = false; return; }
                        tcs.TrySetResult(true);
                    }
                    catch (Exception ex)
                    {
                        tcs.SetException(ex);
                    }
                });
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }

        }
    }
}
