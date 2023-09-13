using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    public enum AxisStatus
    {
        /// <summary>
        /// 運動軸移動中。
        /// </summary>
        Moving,

        /// <summary>
        /// 運動軸已停止。
        /// </summary>
        Stopped,

        /// <summary>
        /// 運動軸因錯誤停止。
        /// </summary>
        ErrorStopped
    }
}
