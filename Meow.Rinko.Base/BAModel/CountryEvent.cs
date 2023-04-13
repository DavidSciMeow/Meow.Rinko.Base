namespace Meow.Rinko.BasicAnalyze.Model
{
    /// <summary>
    /// 确定区域的活动查询
    /// </summary>
    public class CountryEvent
    {
        /// <summary>
        /// 活动类型
        /// </summary>
        public string? eventType { get; set; }
        /// <summary>
        /// 活动名
        /// </summary>
        public string? eventName { get; set; }
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
        public long? startAt { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public long? endAt { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? enableFlag { get; set; }
        /// <summary>
        /// 确定开始时间
        /// </summary>
        public long? publicStartAt { get; set; }
        /// <summary>
        /// 确定结束时间
        /// </summary>
        public long? publicEndAt { get; set; }
        /// <summary>
        /// 奖励分发时间
        /// </summary>
        public long? distributionStartAt { get; set; }
        /// <summary>
        /// 奖励分发结束时间
        /// </summary>
        public long? distributionEndAt { get; set; }
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
        public long? aggregateEndAt { get; set; }
        /// <summary>
        /// 交换结束时间
        /// </summary>
        public long? exchangeEndAt { get; set; }
        /// <summary>
        /// 分数奖励
        /// </summary>
        public Core.Model.Event.Pointreward[][]? pointRewards { get; set; }
        /// <summary>
        /// 排名奖励
        /// </summary>
        public Core.Model.Event.Rankingreward[][]? rankingRewards { get; set; }
        /// <summary>
        /// 加成属性
        /// </summary>
        public Core.Model.Event.Attribute[]? attributes { get; set; }
        /// <summary>
        /// 加成角色
        /// </summary>
        public Core.Model.Event.Character[]? characters { get; set; }
        /// <summary>
        /// 故事列表
        /// </summary>
        public Core.Model.Event.Story[]? stories { get; set; }
        /// <summary>
        /// 奖励卡片
        /// </summary>
        public int[]? rewardCards { get; set; }
    }
}
