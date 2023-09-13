using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    /// <summary>
    /// 表示運動軸狀態。
    /// </summary>
    [Flags]
    public enum Sensors : int
    {
        /// <summary>
        /// 表示無任何訊號觸發。
        /// </summary>
        None = 0x0000,

        /// <summary>
        /// 表示Alarm訊號觸發。
        /// </summary>
        ALM = 0x0001,

        /// <summary>
        /// 表示緊急停止訊號觸發。
        /// </summary>
        EMG = 0x0002,

        /// <summary>
        /// 表示Ready訊號觸發。
        /// </summary>
        RDY = 0x0004,

        /// <summary>
        /// 表示位於原點訊號觸發。
        /// </summary>
        ORG = 0x0010,

        /// <summary>
        /// 表示負極限訊號觸發。
        /// </summary>
        NEL = 0x0020,

        /// <summary>
        /// 表示正極限訊號觸發。
        /// </summary>
        PEL = 0x0040,
    }
}
