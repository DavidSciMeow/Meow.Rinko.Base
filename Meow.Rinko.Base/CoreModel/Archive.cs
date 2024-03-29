﻿using System.Collections.Generic;

namespace Meow.Rinko.Core.Model
{
    /// <summary>
    /// 历史档线模型
    /// </summary>
    public class Archive
    {
        /// <summary>
        /// 历史档线高
        /// </summary>
        public Dictionary<int,long>? cutoff { get; set; }
        /// <summary>
        /// 高档通知
        /// </summary>
        public object[][]? board { get; set; }
    }
}
