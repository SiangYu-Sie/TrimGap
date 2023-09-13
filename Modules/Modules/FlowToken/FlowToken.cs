using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modules
{
    public struct FlowToken
    {
        private readonly FlowTokenSource fts;

        internal FlowToken(FlowTokenSource fts)
        {
            this.fts = fts;
        }

        public CancellationToken CancellationToken => fts.cts.Token;

        public PauseToken PauseToken => fts.pts.Token;

        public static FlowToken None => new FlowToken();

        public static implicit operator CancellationToken(FlowToken flowToken)
        {
            return flowToken.CancellationToken;
        }

        public async Task WaitWhilePausedAsync()
        {
            if (fts == null) return;
            CancellationToken.ThrowIfCancellationRequested();

            fts.PausedNotifyEvent.Set(); // 用來通知 Pause 方法已經暫停。
            await fts.WaitWhilePausedAsync();
        }
    }
}
