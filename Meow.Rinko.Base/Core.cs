using System.Net;

namespace Meow.Rinko.Core
{
    /// <summary>
    /// 枚举的区服类
    /// </summary>
    public enum Country
    {
        /// <summary>
        /// 日本
        /// </summary>
        jp = 0,
        /// <summary>
        /// 国际服
        /// </summary>
        en = 1,
        /// <summary>
        /// 台湾服
        /// </summary>
        tw = 2,
        /// <summary>
        /// 国服
        /// </summary>
        cn = 3,
        /// <summary>
        /// 韩国服
        /// </summary>
        kr = 4,
        /// <summary>
        /// 未定义(保留字段)
        /// </summary>
        undefined = -1
    }
    /// <summary>
    /// 原始的Url库
    /// </summary>
    public static class Url
    {
        /// <summary>
        /// 最近更新
        /// </summary>
        public readonly static string basenews = "https://bestdori.com/api/news/all.5.json";
        /// <summary>
        /// 所有歌曲
        /// </summary>
        public readonly static string basesongs = "https://bestdori.com/api/songs/all.7.json";
        /// <summary>
        /// 所有卡池
        /// </summary>
        public readonly static string basegachas = "https://bestdori.com/api/gacha/all.5.json";
        /// <summary>
        /// 所有活动
        /// </summary>
        public readonly static string baseevents = "https://bestdori.com/api/events/all.5.json";
        /// <summary>
        /// 所有角色
        /// </summary>
        public readonly static string basecharacters = "https://bestdori.com/api/characters/all.2.json";
        /// <summary>
        /// 所有资源
        /// </summary>
        public readonly static string basearchives = "https://bestdori.com/api/archives/all.5.json";
        /// <summary>
        /// 所有歌曲计算
        /// </summary>
        public readonly static string baseallsongs = "https://bestdori.com/api/songs/meta/all.5.json";
        /// <summary>
        /// 所有漫画
        /// </summary>
        public readonly static string baseallcomic = "https://bestdori.com/api/comics/all.5.json";
        /// <summary>
        /// 所有标题图
        /// </summary>
        public readonly static string basealltitle = "https://bestdori.com/api/misc/titles.5.json";
        /// <summary>
        /// 获取某区服的漫画
        /// </summary>
        /// <param name="c">区服</param>
        /// <param name="assetname">漫画</param>
        /// <returns></returns>
        public static string Comic(Country c, string assetname) => $"https://bestdori.com/assets/{c}/comic/{(assetname.Contains("fourframe") ? "comic_fourframe" : "comic_singleframe")}/{assetname}_rip/{assetname}.png";
        /// <summary>
        /// 获取某服务器某档线高的一个档线追踪器
        /// </summary>
        /// <param name="server">服务器</param>
        /// <param name="ex">活动编号</param>
        /// <param name="tier">榜线高</param>
        /// <returns></returns>
        public static string TrackerURL(Country server, int ex, int tier) => $"https://bestdori.com/api/tracker/data?server={(int)server}&event={ex}&tier={tier}";
        /// <summary>
        /// 获取某个固定活动的URL
        /// </summary>
        /// <param name="data">编号</param>
        /// <returns></returns>
        public static string EventURL(int data) => $"https://bestdori.com/api/events/{data}.json";
        /// <summary>
        /// 获得某个固定编号卡池的数据
        /// </summary>
        /// <param name="data">编号</param>
        /// <returns></returns>
        public static string GachaURL(int data) => $"https://bestdori.com/api/gacha/{data}.json";
        /// <summary>
        /// 获得某个固定的卡的数据
        /// </summary>
        /// <param name="data">编号</param>
        /// <returns></returns>
        public static string CardsURL(int data) => $"https://bestdori.com/api/cards/{data}.json";
        /// <summary>
        /// 获得某个固定角色的数据
        /// </summary>
        /// <param name="data">编号</param>
        /// <returns></returns>
        public static string CharactersURL(int data) => $"https://bestdori.com/api/characters/{data}.json";
        /// <summary>
        /// 搜索某个服务器的某个固定玩家的数据
        /// </summary>
        /// <param name="server">服务器id</param>
        /// <param name="playerId">玩家id</param>
        /// <param name="mode">0 / 2 (fresh)</param>
        /// <returns></returns>
        public static string SearchPlayer(Country server, long playerId, int mode = 0) => $"https://bestdori.com/api/player/{server}/{playerId}?mode={mode}";
        /// <summary>
        /// 同步Bestdori玩家
        /// </summary>
        /// <param name="server">服务器</param>
        /// <param name="stat">状态类</param>
        /// <param name="limit">限制高</param>
        /// <param name="offset">偏移</param>
        /// <returns></returns>
        public static string SyncPlayer(Country server, int stat, int limit, int offset) => $"https://bestdori.com/api/sync/list/player?server={(int)server}&stats={stat}&limit={limit}&offset={offset}";
    }
    /// <summary>
    /// 直接获取
    /// </summary>
    public static class Bases
    {
        /// <summary>
        /// 漫画列表
        /// </summary>
        /// <returns></returns>
        public static string BaseComic() => String(Url.baseallcomic);
        /// <summary>
        /// 最近更新
        /// </summary>
        public static string Basenews() => String(Url.basenews);
        /// <summary>
        /// 所有歌曲
        /// </summary>
        public static string Basesongs() => String(Url.basesongs);
        /// <summary>
        /// 所有卡池
        /// </summary>
        public static string Basegachas() => String(Url.basegachas);
        /// <summary>
        /// 所有活动
        /// </summary>
        public static string Baseevents() => String(Url.baseevents);
        /// <summary>
        /// 所有角色
        /// </summary>
        public static string Basecharacters() => String(Url.basecharacters);
        /// <summary>
        /// 所有资源
        /// </summary>
        public static string Basearchives() => String(Url.basearchives);
        /// <summary>
        /// 所有歌曲计算
        /// </summary>
        public static string Baseallsongs() => String(Url.baseallsongs);
        /// <summary>
        /// 获取某服务器某档线高的一个档线追踪器
        /// </summary>
        /// <param name="server">服务器</param>
        /// <param name="ex">活动编号</param>
        /// <param name="tier">榜线高</param>
        /// <returns></returns>
        public static string Tracker(Country server, int ex, int tier) => String(Url.TrackerURL(server, ex, tier));
        /// <summary>
        /// 同步Bestdori玩家
        /// </summary>
        /// <param name="server">服务器</param>
        /// <param name="stat">状态类</param>
        /// <param name="limit">限制高</param>
        /// <param name="offset">偏移</param>
        /// <returns></returns>
        public static string SyncPlayer(Country server, int stat, int limit, int offset) => String(Url.SyncPlayer(server, stat, limit, offset));
        /// <summary>
        /// 获取某个固定活动的URL
        /// </summary>
        /// <param name="data">编号</param>
        /// <returns></returns>
        public static string Event(int data) => String(Url.EventURL(data));
        /// <summary>
        /// 获得某个固定编号卡池的数据
        /// </summary>
        /// <param name="data">编号</param>
        /// <returns></returns>
        public static string Gacha(int data) => String(Url.GachaURL(data));
        /// <summary>
        /// 获得某个固定的卡的数据
        /// </summary>
        /// <param name="data">编号</param>
        /// <returns></returns>
        public static string Cards(int data) => String(Url.CardsURL(data));
        /// <summary>
        /// 获得某个固定角色的数据
        /// </summary>
        /// <param name="data">编号</param>
        /// <returns></returns>
        public static string Characters(int data) => String(Url.CharactersURL(data));
        /// <summary>
        /// 搜索某个服务器的某个固定玩家的数据
        /// </summary>
        /// <param name="server">服务器id</param>
        /// <param name="playerId">玩家id</param>
        /// <param name="mode">0 / 2 (fresh)</param>
        /// <returns></returns>
        public static string SearchPlayer(Country server, long playerId, int mode = 0) => String(Url.SearchPlayer(server, playerId, mode));
        /// <summary>
        /// 获取服装列表(原始live2d)
        /// </summary>
        /// <returns></returns>
        public static string CostumeList() => String("https://bestdori.com/api/costumes/all.5.json");
        /// <summary>
        /// 获得服装具体作用
        /// </summary>
        /// <param name="num">服装号</param>
        /// <returns></returns>
        public static string Costumes(int num) => String($"https://bestdori.com/api/costumes/{num}.json");
        /// <summary>
        /// 获取具体区服的具体服装位
        /// </summary>
        /// <param name="datastring">服装名</param>
        /// <param name="c">具体区服 (默认日本服)</param>
        /// <returns></returns>
        public static string GetDataAssets(string datastring, Country c = Country.jp) => String($"https://bestdori.com/assets/{c}/live2d/chara/{datastring}_rip/buildData.asset");
        private static string String(string url)
        {
            return Util.Network.Http.Client.Basic.GetStringAsync(url).GetAwaiter().GetResult();
        }
    }
}
