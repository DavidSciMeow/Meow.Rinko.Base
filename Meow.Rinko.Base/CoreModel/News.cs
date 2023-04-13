namespace Meow.Rinko.Core.Model
{
    /// <summary>
    /// 新闻类
    /// </summary>
    public class News
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string? title { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string[]? authors { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public long? timestamp { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string[]? tags { get; set; }
    }
}
