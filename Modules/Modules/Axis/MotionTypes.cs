using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    /// <summary>
    /// 運動的模式。
    /// </summary>
    public enum MotionTypes
    {
        /// <summary>
        /// 沒有動作。
        /// </summary>
        None = 0x00,

        /// <summary>
        /// 復歸運動。
        /// </summary>
        Home = 0x01,

        /// <summary>
        /// 絕對位置移動。
        /// </summary>
        Absolute = 0x02,

        /// <summary>
        /// 相對位置移動。
        /// </summary>
        Relative = 0x04,

        /// <summary>
        /// 連續移動。
        /// </summary>
        Continuous = 0x08,

        /// <summary>
        /// 插補運動。
        /// </summary>
        Interpolation = 0x10
    }
}
