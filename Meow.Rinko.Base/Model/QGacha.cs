using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meow.Rinko.Core.Model
{
    /// <summary>
    /// 卡池列表
    /// </summary>
    public class QGacha
    {
        /// <summary>
        /// 资源名
        /// </summary>
        public string? resourceName { get; set; }
        /// <summary>
        /// 标题类
        /// </summary>
        public string? bannerAssetBundleName { get; set; }
        /// <summary>
        /// 卡池名
        /// </summary>
        public string[]? gachaName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string[]? publishedAt { get; set; }
        /// <summary>
        /// 关闭时间
        /// </summary>
        public string[]? closedAt { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string? type { get; set; }
        /// <summary>
        /// 新卡列表
        /// </summary>
        public object[]? newCards { get; set; }
    }
}
