using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meow.Rinko.Core.Model
{
    /// <summary>
    /// 歌曲类
    /// </summary>
    public class Song
    {
        /// <summary>
        /// 标签
        /// </summary>
        public string? tag { get; set; }
        /// <summary>
        /// 乐队id
        /// </summary>
        public int bandId { get; set; }
        /// <summary>
        /// 专辑封面
        /// </summary>
        public string[]? jacketImage { get; set; }
        /// <summary>
        /// 歌曲名
        /// </summary>
        public string[]? musicTitle { get; set; }
        /// <summary>
        /// 发售时间
        /// </summary>
        public string[]? publishedAt { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string[]? closedAt { get; set; }
        /// <summary>
        /// 难度列
        /// </summary>
        public Dictionary<int,PlayLevel>? difficulty { get; set; }
        /// <summary>
        /// 长度值
        /// </summary>
        public float length { get; set; }
        /// <summary>
        /// 音符数
        /// </summary>
        public Dictionary<int,int>? notes { get; set; }
        /// <summary>
        /// Bpm倍率
        /// </summary>
        public Dictionary<int,Bpm[]>? bpm { get; set; }
        /// <summary>
        /// 难度列表
        /// </summary>
        public class PlayLevel
        {
            /// <summary>
            /// 难度
            /// </summary>
            public int playLevel { get; set; }
            /// <summary>
            /// 开始时间
            /// </summary>
            public long?[]? publishedAt { get; set; } //fordefault
        }
        /// <summary>
        /// BPM节奏比率
        /// </summary>
        public class Bpm
        {
            /// <summary>
            /// BPM
            /// </summary>
            public int bpm { get; set; }
            /// <summary>
            /// 开始于
            /// </summary>
            public int start { get; set; }
            /// <summary>
            /// 结束于
            /// </summary>
            public float end { get; set; }
        }
    }

}
