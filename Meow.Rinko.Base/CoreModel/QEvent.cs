namespace Meow.Rinko.Core.Model
{
    /// <summary>
    /// 活动列表查询
    /// </summary>
    public class QEvent
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
        /// 标题图名
        /// </summary>
        public string? bannerAssetBundleName { get; set; }
        /// <summary>
        /// 活动开始于
        /// </summary>
        public string[]? startAt { get; set; }
        /// <summary>
        /// 活动结束于
        /// </summary>
        public string[]? endAt { get; set; }
        /// <summary>
        /// 加成属性
        /// </summary>
        public Attribute[]? attributes { get; set; }
        /// <summary>
        /// 加成角色
        /// </summary>
        public Character[]? characters { get; set; }
        /// <summary>
        /// 奖励卡片
        /// </summary>
        public int[]? rewardCards { get; set; }
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
        /// 加成角色类
        /// </summary>
        public class Character
        {
            /// <summary>
            /// 加成角色id
            /// </summary>
            public int characterId { get; set; }
            /// <summary>
            /// 加成百分比
            /// </summary>
            public int percent { get; set; }
        }
    }
}
