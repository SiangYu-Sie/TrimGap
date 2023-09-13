using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modules
{
    public class FlowTokenSource : IDisposable
    {
        internal readonly PauseTokenSource pts = new PauseTokenSource();
        internal readonly CancellationTokenSource cts = new CancellationTokenSource();

        public FlowTokenSource(bool isAutoPause = false)
        {
            Token = new FlowToken(this);
            IsAutoPause = isAutoPause;

            PausedNotifyEvent = new AsyncAutoResetEvent(false);
        }

        public FlowToken Token { get; }

        public bool IsCompleted { get; private set; }

        public bool IsPaused => pts.IsPaused;

        public bool IsCancelled => cts.Token.IsCancellationRequested;

        public bool IsAutoPause { get; }

        internal AsyncAutoResetEvent PausedNotifyEvent { get; }

        public FlowTokenSource()
        {
            Token = new FlowToken(this);
            cts.Token.Register(Dispose);
        }

        public void Dispose()
        {
            pts.IsPaused = false;
            PausedNotifyEvent.Set();
            IsCompleted = true;
        }

        public void Cancel()
        {
            if (IsCompleted) return;
            cts.Cancel();
        }

        public async Task PasueAsync()
        {
            if (IsCompleted || IsPaused) return;
            pts.IsPaused = true;
            await PausedNotifyEvent.WaitAsync();
        }

        public void Resume()
        {
            if (!IsPaused) return;
            pts.IsPaused = false;
        }

        internal async Task WaitWhilePausedAsync()
        {

            if (IsAutoPause) pts.IsPaused = true;
            //pausedEv.Set();
            await pts.Token.WaitWhilePausedAsync(cts.Token);
        }
    }
}
