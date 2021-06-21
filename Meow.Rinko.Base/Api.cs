using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meow.Rinko.Core.Api
{
    /// <summary>
    /// 所有带参数查询的返回基类
    /// </summary>
    public class BDR
    {
        /// <summary>
        /// Api回送的状态
        /// </summary>
        public bool ResultStatus = false;
        /// <summary>
        /// 过滤Api回送
        /// </summary>
        /// <param name="ResultMessage">回送消息</param>
        /// <returns></returns>
        protected JObject? DoRectify(string? ResultMessage)
        {
            if (string.IsNullOrEmpty(ResultMessage))
            {
                throw new($"E0000 NoReturnValue");
            }
            var ResultSet = JObject.Parse(ResultMessage);
            ResultStatus = ResultSet?["result"]?.ToObject<bool>() ?? false;
            if (!ResultStatus)
            {
                throw new($"E0001 {ResultSet?["code"]}");
            }
            return ResultSet;
        }
    }
    /// <summary>
    /// 榜线高度查询
    /// </summary>
    public class Tracker : BDR
    {
        /// <summary>
        /// 档线高数据
        /// </summary>
        public Model.Tracker?[]? cutoffs { get; set; }
        /// <summary>
        /// 榜线高实例
        /// </summary>
        /// <param name="server">服务器</param>
        /// <param name="ex">活动id</param>
        /// <param name="tier">榜线类型</param>
        public Tracker(Country server, int ex, int tier) => 
            cutoffs = JArray.Parse(
                DoRectify(
                    Bases.Tracker(server, ex, tier)
                    .GetAwaiter().GetResult()
                    )?[nameof(cutoffs)]?.ToString() ?? "[]"
                )?.ToObject<Model.Tracker[]>();
    }
    /// <summary>
    /// 查询一个玩家的信息
    /// </summary>
    public class BandoriPlayer : BDR
    {
        /// <summary>
        /// 玩家数据
        /// </summary>
        public Model.Player? Data { get; set; }
        
        /// <summary>
        /// 初始化一个玩家
        /// </summary>
        /// <param name="server">服务器</param>
        /// <param name="PlayerId">玩家ID</param>
        /// <param name="Mode">是[0]/否[2] 获取缓存数据</param>
        public BandoriPlayer(Country server, long PlayerId, int Mode = 0) => 
            Data = DoRectify(
                Bases.SearchPlayer(server, PlayerId, Mode)
                .GetAwaiter().GetResult()
                )?["data"]?.ToObject<Model.Player>();
    }
}

