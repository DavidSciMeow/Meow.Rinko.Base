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
    /// <summary>
    /// 多功能分析式榜线高度转换
    /// </summary>
    public class AnalyzeTracker
    {
        /// <summary>
        /// 活动号
        /// </summary>
        public int EventNum;
        /// <summary>
        /// 活动榜线高
        /// </summary>
        public int EventType;
        /// <summary>
        /// 区服
        /// </summary>
        public Country Country;
        /// <summary>
        /// 最终线
        /// </summary>
        public double? Final;
        /// <summary>
        /// 拟合最终
        /// </summary>
        public double PFinal;
        /// <summary>
        /// 最终差
        /// </summary>
        public double? DPFinal;
        /// <summary>
        /// 拟合参数
        /// </summary>
        public double[]? P = null;
        /// <summary>
        /// 阶
        /// </summary>
        public int Order { get; }
        /// <summary>
        /// x(活动进度)
        /// </summary>
        public List<double> x = new();
        /// <summary>
        /// y(分数)
        /// </summary>
        public List<double> y = new();
        private static Func<double[], double, double> pf = (double[] para, double x) => {
            double r = 0;
            for (int i = 0; i < para.Length; i++)
            {
                r += Math.Pow(x, i) * para[i];
            }
            return r;
        };
        private List<(double x, double y)> xy = new();
        /// <summary>
        /// 多功能分析式榜线高度转换
        /// </summary>
        /// <param name="eventnum">活动序列号</param>
        /// <param name="eventtype">活动榜线类型</param>
        /// <param name="country">区服</param>
        /// <param name="order">阶(默认为5)</param>
        public AnalyzeTracker(int eventnum, int eventtype, Country country, int order = 5)
        {
            Order = order;
            EventNum = eventnum;
            EventType = eventtype;
            Country = country;
            var s = new MultiTracker(eventnum, country, eventtype);
            x.Add(0);
            y.Add(0);
            xy.Add((0, 0));
            foreach (var a in s.Tracker)
            {
                var xx = (a.PCT > 1 ? 1 : a.PCT) * 100;
                var yy = a.Score;
                x.Add(xx);
                y.Add(yy);
                xy.Add((xx, yy));
            }
            if (xy.Count > order)
            {
                P = MathNet.Numerics.Fit.Polynomial(x.ToArray(), y.ToArray(), order);
                PFinal = pf(P, 100);
                if (x.Last() == 100)
                {
                    Final = y.Last();
                    DPFinal = Final - PFinal;
                }
                else
                {
                    Final = null;
                    DPFinal = null;
                }
            }
            else
            {
                throw new($"ERR Data Length too Short to predict {xy.Count} -> {order-1} needed");
            }
        }
    }
}
