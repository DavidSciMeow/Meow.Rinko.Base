using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meow.Rinko.Core.Model
{
    /// <summary>
    /// 活动类
    /// </summary>
    public class Event
    {
        /// <summary>
        /// 活动类型
        /// </summary>
        public string? eventType { get; set; }
        /// <summary>
        /// 活动名
        /// </summary>
        public string[]? eventName { get; set; }
        /// <summary>
        /// 资源包名
        /// </summary>
        public string? assetBundleName { get; set; }
        /// <summary>
        /// 标题图资源包名
        /// </summary>
        public string? bannerAssetBundleName { get; set; }
        /// <summary>
        /// 启动时间
        /// </summary>
        public long?[]? startAt { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public long?[]? endAt { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool?[]? enableFlag { get; set; }
        /// <summary>
        /// 确定开始时间
        /// </summary>
        public long?[]? publicStartAt { get; set; }
        /// <summary>
        /// 确定结束时间
        /// </summary>
        public long?[]? publicEndAt { get; set; }
        /// <summary>
        /// 奖励分发时间
        /// </summary>
        public long?[]? distributionStartAt { get; set; }
        /// <summary>
        /// 奖励分发结束时间
        /// </summary>
        public long?[]? distributionEndAt { get; set; }
        /// <summary>
        /// BGM资源包
        /// </summary>
        public string? bgmAssetBundleName { get; set; }
        /// <summary>
        /// BGM文件名
        /// </summary>
        public string? bgmFileName { get; set; }
        /// <summary>
        /// 合并结束时间
        /// </summary>
        public long?[]? aggregateEndAt { get; set; }
        /// <summary>
        /// 交换结束时间
        /// </summary>
        public long?[]? exchangeEndAt { get; set; }
        /// <summary>
        /// 分数奖励
        /// </summary>
        public Pointreward[][]? pointRewards { get; set; }
        /// <summary>
        /// 分数奖励类
        /// </summary>
        public class Pointreward
        {
            /// <summary>
            /// 分数
            /// </summary>
            public object? point { get; set; }
            /// <summary>
            /// 奖励类型
            /// </summary>
            public string? rewardType { get; set; }
            /// <summary>
            /// 奖励数量
            /// </summary>
            public int rewardQuantity { get; set; }
            /// <summary>
            /// 奖励物品id
            /// </summary>
            public int rewardId { get; set; }
        }
        /// <summary>
        /// 排名奖励
        /// </summary>
        public Rankingreward[][]? rankingRewards { get; set; }
        /// <summary>
        /// 排名奖励类
        /// </summary>
        public class Rankingreward
        {
            /// <summary>
            /// 从名次
            /// </summary>
            public object? fromRank { get; set; }
            /// <summary>
            /// 到名次
            /// </summary>
            public object? toRank { get; set; }
            /// <summary>
            /// 奖励类型
            /// </summary>
            public string? rewardType { get; set; }
            /// <summary>
            /// 奖励物品id
            /// </summary>
            public object? rewardId { get; set; }
            /// <summary>
            /// 奖励数量
            /// </summary>
            public object? rewardQuantity { get; set; }
        }
        /// <summary>
        /// 加成属性
        /// </summary>
        public Attribute[]? attributes { get; set; }
        /// <summary>
        /// 加成属性类
        /// </summary>
        public class Attribute
        {
            /// <summary>
            /// 属性
            /// </summary>
            public string? attribute { get; set; }
            /// <summary>
            /// 加成百分比
            /// </summary>
            public int percent { get; set; }
        }
        /// <summary>
        /// 加成角色
        /// </summary>
        public Character[]? characters { get; set; }
        /// <summary>
        /// 加成角色类
        /// </summary>
        public class Character
        {
            /// <summary>
            /// 角色id
            /// </summary>
            public int characterId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int percent { get; set; }
        }
        /// <summary>
        /// 故事列表
        /// </summary>
        public Story[]? stories { get; set; }
        /// <summary>
        /// 故事类
        /// </summary>
        public class Story
        {
            /// <summary>
            /// 情景id
            /// </summary>
            public string? scenarioId { get; set; }
            /// <summary>
            /// 背景图
            /// </summary>
            public string? coverImage { get; set; }
            /// <summary>
            /// 背景图片
            /// </summary>
            public string? backgroundImage { get; set; }
            /// <summary>
            /// 释放分数
            /// </summary>
            public string? releasePt { get; set; }
            /// <summary>
            /// 奖励
            /// </summary>
            public Reward[]? rewards { get; set; }
            /// <summary>
            /// 奖励类
            /// </summary>
            public class Reward
            {
                /// <summary>
                /// 奖励类型
                /// </summary>
                public string? rewardType { get; set; }
                /// <summary>
                /// 奖励物品id
                /// </summary>
                public string? rewardId { get; set; }
                /// <summary>
                /// 奖励数量
                /// </summary>
                public string? rewardQuantity { get; set; }
            }
            /// <summary>
            /// 大标题
            /// </summary>
            public string[]? caption { get; set; }
            /// <summary>
            /// 标题
            /// </summary>
            public string[]? title { get; set; }
            /// <summary>
            /// 概要字串
            /// </summary>
            public string[]? synopsis { get; set; }
            /// <summary>
            /// 达成条件
            /// </summary>
            public string[]? releaseConditions { get; set; }
        }
        /// <summary>
        /// 奖励卡片
        /// </summary>
        public int[]? rewardCards { get; set; }
    }
}
