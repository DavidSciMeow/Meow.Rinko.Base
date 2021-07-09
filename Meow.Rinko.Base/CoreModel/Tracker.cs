using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meow.Rinko.Core.Model
{
    /// <summary>
    /// 榜线类
    /// </summary>
    public class Tracker
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public long time { get; set; }
        /// <summary>
        /// 分数
        /// </summary>
        public long ep { get; set; }
    }
}
