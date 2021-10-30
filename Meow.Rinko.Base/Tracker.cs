using Meow.Rinko.Core;
using Meow.Rinko.Core.Api;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Meow.Rinko.BasicAnalyze
{
    /// <summary>
    /// 多功能榜线高度转换器
    /// </summary>
    public class MultiTracker
    {
        /// <summary>
        /// 活动总体信息
        /// </summary>
        public Model.CountryEvent @event;
        /// <summary>
        /// 榜线高
        /// </summary>
        public Model.Tracker[] Tracker;
        /// <summary>
        /// 区服
        /// </summary>
        public Country C;
        /// <summary>
        /// 默认的构造
        /// </summary>
        public MultiTracker() { }
        /// <summary>
        /// 多功能榜线高度转换
        /// </summary>
        /// <param name="eventnum">活动号</param>
        /// <param name="c">区服</param>
        /// <param name="tier">高度</param>
        public MultiTracker(int eventnum,Country c,int tier)
        {
            C = c;
            this.@event = new Gets.CountryEvent(eventnum, c).Data;
            var sat = @event.startAt;
            var eat = @event.endAt;
            var t = new Tracker(c, eventnum, tier).cutoffs;
            var l = new List<Model.Tracker>();
            foreach(var d in t ?? Array.Empty<Core.Model.Tracker>())
            {
                l.Add(new()
                {
                    PCT = ((d?.time - sat) ?? 0d) / ((eat - sat) ?? 0d),
                    Score = d?.ep ?? 0,
                    OTS = d?.time ?? 0L,
                    Time = new DateTime(621355968000000000).AddMilliseconds(d?.time ?? 0)
                });
            }
            Tracker = l.ToArray();
        }
    }
}
