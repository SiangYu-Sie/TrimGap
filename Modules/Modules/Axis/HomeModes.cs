using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    /// <summary>
    /// 表示執行 Home 時的動作模式。
    /// </summary>
    public enum HomeModes
    {
        /// <summary>
        /// 移動至原點感應器後完成 Home 動作。
        /// </summary>
        ORG,

        /// <summary>
        /// 移動至極限感應器後完成 Home 動作。
        /// </summary>
        EL,

        /// <summary>
        /// 移動至觸發馬達 Z 相訊號或光學尺上第一個 Index 訊號後完成 Home 動作。
        /// </summary>
        Index,

        /// <summary>
        /// 移動至硬體止擋後完成 Home 動作。
        /// </summary>
        Block,

        /// <summary>
        /// 當前位置 Home動作
        /// </summary>
        CurPos,

        /// <summary>
        /// 移動至極限感應器後, 反方向搜尋Index完成 Home 動作。
        /// </summary>
        ELAndIndex
    }
}
