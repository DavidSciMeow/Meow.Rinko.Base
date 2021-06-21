using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meow.Rinko.Core.Model
{
    /// <summary>
    /// 卡池类
    /// </summary>
    public class Gacha
    {
        /// <summary>
        /// 卡池详细信息
        /// </summary>
        public Dictionary<int, DetailData>[]? details { get; set; }
        /// <summary>
        /// 详细信息类
        /// </summary>
        public class DetailData
        {
            /// <summary>
            /// 稀有度值
            /// </summary>
            public int rarityIndex { get; set; }
            /// <summary>
            /// 权重
            /// </summary>
            public int weight { get; set; }
            /// <summary>
            /// ??
            /// </summary>
            public bool pickup { get; set; }
        }
        /// <summary>
        /// 评分数据
        /// </summary>
        public Dictionary<int, RateData>[]? rates { get; set; }
        /// <summary>
        /// 评分数据类
        /// </summary>
        public class RateData
        {
            /// <summary>
            /// 分
            /// </summary>
            public int rate { get; set; }
            /// <summary>
            /// 总权重
            /// </summary>
            public int weightTotal { get; set; }
        }
        /// <summary>
        /// 获取方案
        /// </summary>
        public Paymentmethod[]? paymentMethods { get; set; }
        /// <summary>
        /// 获取方案类
        /// </summary>
        public class Paymentmethod
        {
            /// <summary>
            /// 抽奖id
            /// </summary>
            public int gachaId { get; set; }
            /// <summary>
            /// 获取方案
            /// </summary>
            public string? paymentMethod { get; set; }
            /// <summary>
            /// 数量
            /// </summary>
            public int quantity { get; set; }
            /// <summary>
            /// 获取方案id
            /// </summary>
            public int paymentMethodId { get; set; }
            /// <summary>
            /// 总计
            /// </summary>
            public int count { get; set; }
            /// <summary>
            /// 表现形
            /// </summary>
            public string? behavior { get; set; }
            /// <summary>
            /// 附赠?
            /// </summary>
            public bool pickup { get; set; }
            /// <summary>
            /// 扣除物品数量
            /// </summary>
            public int costItemQuantity { get; set; }
        }
        /// <summary>
        /// 资源包名
        /// </summary>
        public string? resourceName { get; set; }
        /// <summary>
        /// 标题图资源包名
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
        /// 结束时间
        /// </summary>
        public string[]? closedAt { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string[]? description { get; set; }
        /// <summary>
        /// 附加信息
        /// </summary>
        public string[]? annotation { get; set; }
        /// <summary>
        /// 卡池频度
        /// </summary>
        public string[]? gachaPeriod { get; set; }
        /// <summary>
        /// 卡池类型
        /// </summary>
        public string? gachaType { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string? type { get; set; }
        /// <summary>
        /// 新卡片(Object获取源转String自己解析)
        /// </summary>
        public object[]? newCards { get; set; }
        /// <summary>
        /// 卡池信息
        /// </summary>
        public Information? information { get; set; }
        /// <summary>
        /// 卡池信息类
        /// </summary>
        public class Information
        {
            /// <summary>
            /// 描述
            /// </summary>
            public object[]? description { get; set; }
            /// <summary>
            /// 期
            /// </summary>
            public string[]? term { get; set; }
            /// <summary>
            /// 新成员信息
            /// </summary>
            public string[]? newMemberInfo { get; set; }
            /// <summary>
            /// 备注
            /// </summary>
            public object[]? notice { get; set; }
        }
    }
}
