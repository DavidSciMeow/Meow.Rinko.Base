using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meow.Rinko.Core.Model
{
    /// <summary>
    /// 基准卡类
    /// </summary>
    public class Card
    {
        /// <summary>
        /// 人物ID
        /// </summary>
        public int? characterId { get; set; }
        /// <summary>
        /// 稀有度
        /// </summary>
        public int? rarity { get; set; }
        /// <summary>
        /// 加成类型
        /// </summary>
        public string? attribute { get; set; }
        /// <summary>
        /// 等级限制
        /// </summary>
        public int? levelLimit { get; set; }
        /// <summary>
        /// 资源包组名
        /// </summary>
        public string? resourceSetName { get; set; }
        /// <summary>
        /// LiveSD资源包组名
        /// </summary>
        public string? sdResourceName { get; set; }
        /// <summary>
        /// 剧情集
        /// </summary>
        public Episodes? episodes { get; set; }
        /// <summary>
        /// 剧情类
        /// </summary>
        public class Episodes
        {
            /// <summary>
            /// 剧情列表
            /// </summary>
            public EpisodesData[]? entries { get; set; }
            /// <summary>
            /// 剧情类
            /// </summary>
            public class EpisodesData
            {
                /// <summary>
                /// 剧情id
                /// </summary>
                public int? episodeId { get; set; }
                /// <summary>
                /// 剧情类
                /// </summary>
                public string? episodeType { get; set; }
                /// <summary>
                /// 场序id
                /// </summary>
                public int? situationId { get; set; }
                /// <summary>
                /// 分剧情Id
                /// </summary>
                public string? scenarioId { get; set; }
                /// <summary>
                /// 解锁后添加的演奏值
                /// </summary>
                public int? appendPerformance { get; set; }
                /// <summary>
                /// 解锁后添加的技巧值
                /// </summary>
                public int? appendTechnique { get; set; }
                /// <summary>
                /// 解锁添加的视觉值
                /// </summary>
                public int? appendVisual { get; set; }
                /// <summary>
                /// 获取时等级
                /// </summary>
                public int? releaseLevel { get; set; }
                /// <summary>
                /// 解锁花费类型
                /// </summary>
                public Costs? costs { get; set; }
                /// <summary>
                /// 解锁花费类
                /// </summary>
                public class Costs
                {
                    /// <summary>
                    /// 花费列表
                    /// </summary>
                    public CostsData[]? entries { get; set; }
                    /// <summary>
                    /// 花费类
                    /// </summary>
                    public class CostsData
                    {
                        /// <summary>
                        /// 资源类型id
                        /// </summary>
                        public int? resourceId { get; set; }
                        /// <summary>
                        /// 资源类型
                        /// </summary>
                        public string? resourceType { get; set; }
                        /// <summary>
                        /// 数量
                        /// </summary>
                        public int? quantity { get; set; }
                        /// <summary>
                        /// 最低数量
                        /// </summary>
                        public int? lbBonus { get; set; }
                    }
                }
                /// <summary>
                /// 角色奖励
                /// </summary>
                public Rewards? rewards { get; set; }
                /// <summary>
                /// 奖励类
                /// </summary>
                public class Rewards
                {
                    /// <summary>
                    /// 奖励数据列表
                    /// </summary>
                    public RewardsData[]? entries { get; set; }
                    /// <summary>
                    /// 奖励类
                    /// </summary>
                    public class RewardsData
                    {
                        /// <summary>
                        /// 奖励类型
                        /// </summary>
                        public string? resourceType { get; set; }
                        /// <summary>
                        /// 奖励数量
                        /// </summary>
                        public int? quantity { get; set; }
                        /// <summary>
                        /// 最低奖励
                        /// </summary>
                        public int? lbBonus { get; set; }
                    }
                }
                /// <summary>
                /// 剧情名
                /// </summary>
                public string[]? title { get; set; }
                /// <summary>
                /// 角色id
                /// </summary>
                public int? characterId { get; set; }
            }
        }
        /// <summary>
        /// 服装id
        /// </summary>
        public int? costumeId { get; set; }
        /// <summary>
        /// 卡池文字
        /// </summary>
        public object[]? gachaText { get; set; }
        /// <summary>
        /// 前缀
        /// </summary>
        public string[]? prefix { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        public string[]? releasedAt { get; set; }
        /// <summary>
        /// 技能名
        /// </summary>
        public string[]? skillName { get; set; }
        /// <summary>
        /// 技能id
        /// </summary>
        public int? skillId { get; set; }
        /// <summary>
        /// 源
        /// </summary>
        public object[]? source { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string? type { get; set; }
        /// <summary>
        /// 综合力数据
        /// </summary>
        public Dictionary<string,StatData>? stat { get; set; }
        /// <summary>
        /// 状态数据类
        /// </summary>
        public class StatData
        {
            /// <summary>
            /// 表演值
            /// </summary>
            public int? performance { get; set; }
            /// <summary>
            /// 技巧值
            /// </summary>
            public int? technique { get; set; }
            /// <summary>
            /// 视觉值
            /// </summary>
            public int? visual { get; set; }
        }
    }
}
