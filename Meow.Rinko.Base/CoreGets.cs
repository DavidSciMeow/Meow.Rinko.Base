using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Collections.Generic;
using Meow.Util;

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
                Data = JObject.Parse(Bases.Baseevents()).ToObject<Dictionary<int, Model.QEvent>>();
            }
            catch(Exception ex)
            {
                throw new($"E0010 EventList:: {ex}");
            }
        }
        /// <summary>
        /// 获取当前活动
        /// </summary>
        /// <param name="c">区服</param>
        /// <returns></returns>
        public (int[] inbound,int[] outbound) EventNow(Country c)
        {
            var nts = DateTime.Now.ToMiSecTimeStamp();
            var ntsmax = DateTime.Now.AddDays(1).ToMiSecTimeStamp();
            var inbound = from a in Data 
                          where a.Value?.startAt?[(int)c] != null && 
                            long.Parse(a.Value?.startAt?[(int)c]??"0") < nts && 
                            long.Parse(a.Value?.endAt?[(int)c] ?? "0") > nts 
                          select a.Key;
            var outbound = from a in Data 
                           where a.Value?.startAt?[(int)c] != null && 
                            long.Parse(a.Value?.startAt?[(int)c]??"0") > ntsmax 
                           select a.Key;
            return (inbound.ToArray(), outbound.ToArray());
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
                Data = JObject.Parse(Bases.Basegachas()).ToObject<Dictionary<int, Model.QGacha>>();
            }
            catch (Exception ex)
            {
                throw new($"E0011 GachaList:: {ex}");
            }
        }
        /// <summary>
        /// 正在进行的招募
        /// </summary>
        /// <param name="c">区服</param>
        /// <returns></returns>
        public (Model.QGacha[] inbound, Model.QGacha[] outbound) GachaNow(Country c)
        {
            var nts = DateTime.Now.ToMiSecTimeStamp();
            var ntsmax = DateTime.Now.AddDays(1).ToMiSecTimeStamp();
            var inbound = from a in Data
                          where a.Value?.publishedAt?[(int)c] != null &&
                            long.Parse(a.Value.publishedAt[(int)c] ?? "0") < nts &&
                            long.Parse(a.Value?.closedAt?[(int)c] ?? "0") > nts
                          select a.Value;
            var outbound = from a in Data
                           where a.Value?.publishedAt?[(int)c] != null &&
                            long.Parse(a.Value?.publishedAt?[(int)c] ?? "0") > ntsmax
                           select a.Value;
            return (inbound.ToArray(), outbound.ToArray());
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
                Data = JObject.Parse(Bases.Basesongs()).ToObject<Dictionary<int, Model.Song>>();
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
                Data = JObject.Parse(Bases.Basecharacters()).ToObject<Dictionary<int, Model.Character>>();
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
                Data = JObject.Parse(Bases.Basearchives()).ToObject<Dictionary<int, Model.Archive>>();
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
                Data = JObject.Parse(Bases.Basenews()).ToObject<Dictionary<int, Model.News>>();
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
                Data = JObject.Parse(Bases.Baseallsongs()).ToObject<Dictionary<int, Dictionary<int, Dictionary<double, double[]>>>>();
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
                Data = JObject.Parse(Bases.Event(num)).ToObject<Model.Event>();
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
                Data = JObject.Parse(Bases.Gacha(num)).ToObject<Model.Gacha>();
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
                Data = JObject.Parse(Bases.Cards(num)).ToObject<Model.Card>();
            }
            catch (Exception ex)
            {
                throw new($"E0019 Card:: {ex}");
            }
        }
    }
}
