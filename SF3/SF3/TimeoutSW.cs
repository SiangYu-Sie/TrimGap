using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpTest
{
    class TimeoutSW
    {
        #region Members

        long timeLimitMillisecond = 0L;
        System.Diagnostics.Stopwatch sw;

        #endregion
        #region Constructors

        /// <summary>
        /// 構造函數
        /// </summary>
        public TimeoutSW()
        {
            this.sw = new System.Diagnostics.Stopwatch();
        }

        /// <summary>
        /// 干擾器
        /// </summary>
        ~TimeoutSW()
        {
            this.sw.Stop();
        }

        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="timeLimitSecond">制限時間（秒）</param>
        public TimeoutSW(double timeLimitSecond)
            : this()
        {
            this.timeLimitMillisecond = (long)(timeLimitSecond * 1000.0);
        }

        #endregion
        #region ITimeout実装

        /// <summary>
        /// 開始
        /// </summary>
        public void Start()
        {
            this.sw.Start();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            this.sw.Stop();
        }

        /// <summary>
        /// Restart
        /// </summary>
        public void Restart()
        {
            this.sw.Restart();
        }

        /// <summary>
        /// 超時檢查
        /// </summary>
        /// <returns>true:暫停</returns>
        public bool IsTimeout
        {
            get
            {
                if (this.timeLimitMillisecond > 0L)
                {
                    // 経過時間取得
                    long diffMSecond = this.sw.ElapsedMilliseconds;
                    // 比較
                    if (diffMSecond >= this.timeLimitMillisecond)
                        return true;
                }

                return false;
            }
        }

        /// <summary>
        /// 監視時間（秒）
        /// </summary>
        public double TimeLimitSecond
        {
            get { return this.timeLimitMillisecond / 1000; }
            set { this.timeLimitMillisecond = (long)(value * 1000.0); }
        }

        /// <summary>
        /// 監視時間（毫秒）
        /// </summary>
        public long TimeLimitMillisecond
        {
            get { return this.timeLimitMillisecond; }
            set { this.timeLimitMillisecond = value; }
        }

        #endregion
    }
}
