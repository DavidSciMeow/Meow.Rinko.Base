using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meow.Rinko.BasicAnalyze.Model
{
    /// <summary>
    /// 基础分析榜线类
    /// </summary>
    public class Tracker
    {
        /// <summary>
        /// 活动相对百分比
        /// </summary>
        public double PCT { get; set; }
        /// <summary>
        /// 分数
        /// </summary>
        public long Score { get; set; }
        /// <summary>
        /// 原始时间戳
        /// <para>OTS is a abbreviation `OriginalTimeStamp`</para>
        /// </summary>
        public long OTS { get; set; }
        /// <summary>
        /// 当时时间
        /// </summary>
        public DateTime Time { get; set; }
    }
}
