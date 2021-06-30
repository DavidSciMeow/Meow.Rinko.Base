using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Meow.Rinko.Core.Gets
{
    /// <summary>
    /// 活动列表
    /// </summary>
    public class EventList
    {
        /// <summary>
        /// 所有活动列表数据
        /// </summary>
        public Dictionary<int, Model.QEvent>? Data { get; }
        /// <summary>
        /// 所有初始化活动列表
        /// </summary>
        public EventList()
        {
            try
            {
                Data = JObject.Parse(Bases.Baseevents().GetAwaiter().GetResult()).ToObject<Dictionary<int, Model.QEvent>>();
            }
            catch(Exception ex)
            {
                throw new($"E0010 EventList:: {ex}");
            }
        }
    }
    /// <summary>
    /// 卡池列表
    /// </summary>
    public class GachaList
    {
        /// <summary>
        /// 所有卡池列表
        /// </summary>
        public Dictionary<int, Model.QGacha>? Data { get; }
        /// <summary>
        /// 初始化所有卡池列表
        /// </summary>
        public GachaList()
        {
            try
            {
                Data = JObject.Parse(Bases.Basegachas().GetAwaiter().GetResult()).ToObject<Dictionary<int, Model.QGacha>>();
            }
            catch (Exception ex)
            {
                throw new($"E0011 GachaList:: {ex}");
            }
        }
    }
    /// <summary>
    /// 乐曲
    /// </summary>
    public class Song
    {
        /// <summary>
        /// 所有乐曲列表
        /// </summary>
        public Dictionary<int, Model.Song>? Data { get; }
        /// <summary>
        /// 初始化所有乐曲列表
        /// </summary>
        public Song()
        {
            try
            {
                Data = JObject.Parse(Bases.Basesongs().GetAwaiter().GetResult()).ToObject<Dictionary<int, Model.Song>>();
            }
            catch (Exception ex)
            {
                throw new($"E0012 Song:: {ex}");
            }
        }
    }
    /// <summary>
    /// 角色
    /// </summary>
    public class Character
    {
        /// <summary>
        /// 所有角色数据
        /// </summary>
        public Dictionary<int, Model.Character>? Data { get; }
        /// <summary>
        /// 初始化所有角色
        /// </summary>
        public Character()
        {
            try
            {
                Data = JObject.Parse(Bases.Basecharacters().GetAwaiter().GetResult()).ToObject<Dictionary<int, Model.Character>>();
            }
            catch (Exception ex)
            {
                throw new($"E0013 Character:: {ex}");
            }
        }
    }
    /// <summary>
    /// 榜线最高统计
    /// </summary>
    public class Archive
    {
        /// <summary>
        /// 所有榜线最高统计数据
        /// </summary>
        public Dictionary<int, Model.Archive>? Data { get; }
        /// <summary>
        /// 初始化榜线最高统计数据
        /// </summary>
        public Archive()
        {
            try
            {
                Data = JObject.Parse(Bases.Basearchives().GetAwaiter().GetResult()).ToObject<Dictionary<int, Model.Archive>>();
            }
            catch (Exception ex)
            {
                throw new($"E0014 Archive:: {ex}");
            }
        }
    }
    /// <summary>
    /// 更新信息
    /// </summary>
    public class News
    {
        /// <summary>
        /// 所有更新信息数据
        /// </summary>
        public Dictionary<int, Model.News>? Data { get; }
        /// <summary>
        /// 初始化所有更新信息数据
        /// </summary>
        public News()
        {
            try
            {
                Data = JObject.Parse(Bases.Basenews().GetAwaiter().GetResult()).ToObject<Dictionary<int, Model.News>>();
            }
            catch (Exception ex)
            {
                throw new($"E0015 News:: {ex}");
            }
        }
    }
    /// <summary>
    /// 歌曲分数计算
    /// </summary>
    public class SongPcts
    {
        /// <summary>
        /// 歌曲计算分数公式数据
        /// </summary>
        public Dictionary<int, Dictionary<int, Dictionary<double, double[]>>>? Data { get; }
        /// <summary>
        /// 初始化歌曲分数计算
        /// </summary>
        public SongPcts()
        {
            try
            {
                Data = JObject.Parse(Bases.Baseallsongs().GetAwaiter().GetResult()).ToObject<Dictionary<int, Dictionary<int, Dictionary<double, double[]>>>>();
            }
            catch (Exception ex)
            {
                throw new($"E0016 SongPcts:: {ex}");
            }
        }
    }
    /// <summary>
    /// 某特定活动
    /// </summary>
    public class Event
    {
        /// <summary>
        /// 活动数据
        /// </summary>
        public Model.Event? Data { get; }
        /// <summary>
        /// 初始化活动数据
        /// </summary>
        /// <param name="num">活动号</param>
        public Event(int num)
        {
            try
            {
                Data = JObject.Parse(Bases.Event(num).GetAwaiter().GetResult()).ToObject<Model.Event>();
            }
            catch (Exception ex)
            {
                throw new($"E0017 Event:: {ex}");
            }
        }
    }
    /// <summary>
    /// 某特定卡池
    /// </summary>
    public class Gacha
    {
        /// <summary>
        /// 卡池数据
        /// </summary>
        public Model.Gacha? Data { get; }
        /// <summary>
        /// 初始化某卡池
        /// </summary>
        /// <param name="num">卡池编号</param>
        public Gacha(int num)
        {
            try
            {
                Data = JObject.Parse(Bases.Gacha(num).GetAwaiter().GetResult()).ToObject<Model.Gacha>();
            }
            catch (Exception ex)
            {
                throw new($"E0018 Gacha:: {ex}");
            }
        }
    }
    /// <summary>
    /// 某卡片
    /// </summary>
    public class Card
    {
        /// <summary>
        /// 卡片数据
        /// </summary>
        public Model.Card? Data { get; }
        /// <summary>
        /// 初始化某卡片
        /// </summary>
        /// <param name="num"></param>
        public Card(int num)
        {
            try
            {
                Data = JObject.Parse(Bases.Cards(num).GetAwaiter().GetResult()).ToObject<Model.Card>();
            }
            catch (Exception ex)
            {
                throw new($"E0019 Card:: {ex}");
            }
        }
    }
}
