using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    public class AxisConfig
    {
        /// <summary>
        /// 取得或設定軸正極限
        /// </summary>
        public double? AxisPEL { get; set; }
        /// <summary>
        /// 取得或設定軸負極限
        /// </summary>
        public double? AxisNEL { get; set; }
        /// <summary>
        /// 取得或設定軸工作速度
        /// </summary>
        public VelocityParams CommonWorkingVelocity { get; set; }
        /// <summary>
        /// 取得或設定軸工作速度(特殊)
        /// </summary>
        public VelocityParams SpecificWorkingVelocity { get; set; }
        /// <summary>
        /// 取得或設定初始化參數
        /// </summary>
        public HomeConfig[] HomeConfigs { get; set; }
        /// <summary>
        /// 取得或設定位置單位的因子數
        /// </summary>
        public double Unitfactor { get; set; }
    }

    public class HomeConfig
    {
        /// <summary>
        /// 取得或設定初始化模式
        /// </summary>
        public HomeModes HomeMode { get; set; }
        /// <summary>
        /// 取得或設定初始化方向
        /// </summary>
        public MotionDirections Direction { get; set; }
        /// <summary>
        ///  取得或設定軸的初始化速度
        /// </summary>
        public VelocityParams HomeVelocity { get; set; }

        /// <summary>
        /// 取得或設定初始化後位置
        /// </summary>
        public double PositionAfterHoming { get; set; }
    }
}
