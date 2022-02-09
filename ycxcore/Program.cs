using Meow.Rinko.BasicAnalyze;
using Meow.Rinko.Core;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Meow.Rinko.BasicAnalyze.Model;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ycxcore
{
    public class Program
    {

        /// <summary>
        /// 应用存储根目录
        /// </summary>
        public static string GeneralStoragePath = "./";
        /// <summary>
        /// 当前活动
        /// </summary>
        public static int?[] eventNow = { null, null, null, null, null };
        /// <summary>
        /// 服务器容纳的榜线高
        /// </summary>
        public static int[][] Tier = {
            new[] {100,500,1000,2000,5000,10000 }, //jp
            new[] {100,1000,2500 }, //en
            new[] {100,500 }, //tw
            new[] {50,100,300,500,1000,2000 }, //cn
            new[] {100 }};//kr
        /// <summary>
        /// 最小泰勒展开阶
        /// </summary>
        public static int _minOrder = 4;
        public static int retry_timing = 1000 * 5;

        /// <summary>
        /// 主程序注入
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            _ = args;
            if (!Directory.Exists(Path.Combine(GeneralStoragePath, $"TrackerNow")))
            {
                Directory.CreateDirectory(Path.Combine(GeneralStoragePath, $"TrackerNow"));
            }
            if (!Directory.Exists(Path.Combine(GeneralStoragePath, $"Trackers")))
            {
                Directory.CreateDirectory(Path.Combine(GeneralStoragePath, $"Trackers"));
            }
            if (!Directory.Exists(Path.Combine(GeneralStoragePath, $"Analyze")))
            {
                Directory.CreateDirectory(Path.Combine(GeneralStoragePath, $"Analyze"));
            }
            if (!Directory.Exists(Path.Combine(GeneralStoragePath, $"PredictNow")))
            {
                Directory.CreateDirectory(Path.Combine(GeneralStoragePath, $"PredictNow"));
            }
            File.WriteAllText(Path.Combine(GeneralStoragePath, $"PredictNow", "tier.json"), JsonConvert.SerializeObject(Tier));
            if (args.Length > 0)
            {
                if(args[0] == "gm")
                {
                    Console.WriteLine($"[Main] Model Trainning Start");
                    Models.GenerateModel();
                    return;
                }
                else if(args[0] == "gmf")
                {
                    Console.WriteLine($"[Main] Model Trainning Start");
                    Models.GenerateModel(true);
                    return;
                }
            }
            while (true)
            {
                _doGetBDData();
                _doFetch();
                _doPredict();
                Console.WriteLine("[BDCP] -- FETCHING Next 30 Min --");
                Thread.Sleep(1000 * 60 * 30);
            }
        }
        public static void _doGetBDData()
        {
            Console.WriteLine("[BDCP] -- FETCHING BESTDORI DATA --");
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    var s = new Meow.Rinko.Core.Gets.EventList().EventNow((Country)i).inbound;
                    if (s.Length > 0)
                    {
                        eventNow[i] = s[0];
                        Console.WriteLine($"[BDCP] -- SERVER {(Country)i} ON EVENT {eventNow[i]} --");
                    }
                    else
                    {
                        eventNow[i] = null;
                        Console.WriteLine($"[BDCP] -- SERVER {(Country)i} NOW DONT HAVE EVENT ONGOING --");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[MAIN.GetBDData] err : {ex.Message}");
                    Console.WriteLine($"[MAIN.GetBDData] Retrying in {retry_timing/1000} sec ");
                    Thread.Sleep(retry_timing);
                    continue;
                }

            }
            Console.WriteLine("[BDCP] -- FETCHING EVENTNOW COMPLETE --");
        }
        public static void _doFetch()
        {
            for (int i = 0; i < 5; i++)
            {
                while (true)
                {
                    try
                    {
                        if (eventNow[i] != null)
                        {
                            foreach (var r in Tier[i])
                            {
                                RenderingBlock((Country)i, eventNow[i] ?? 0, r, true);
                            }
                            Console.WriteLine($"[BDCP] -- RENDER COMP {(Country)i} {eventNow[i]} --");
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"[BDCP] -- NO EVENT ON {(Country)i} {eventNow[i]} --");
                            break;
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"[MAIN.Fetch] err : {ex.Message}");
                        Console.WriteLine($"[MAIN.GetBDData] Retrying in {retry_timing / 1000} sec ");
                        Thread.Sleep(retry_timing);
                        continue;
                    }
                    
                }
            }
        }
        public static void _doPredict()
        {
            foreach (var n in new int[] { 0, 1, 2, 3, 4 })
            {
                for (int j = 0; j < Tier[n].Length; j++)
                {
                    var tier = Tier[n][j];
                    while (true)
                    {
                        try
                        {
                            var k = new SM_Now(n, tier, (eventNow[n] != null)).Predict();
                            File.WriteAllText(Path.Combine(GeneralStoragePath, $"PredictNow", $"{n}-{tier}.now.json"), JsonConvert.SerializeObject(k));
                            Console.WriteLine($"[GENT] |COM| {n} {tier} COMPLETE");
                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[GENT] |-ER| {n} {tier} ERR : {ex}");
                            Console.WriteLine($"[MAIN.GetBDData] Retrying in {retry_timing / 1000} sec ");
                            Thread.Sleep(retry_timing);
                            continue;
                        }
                    }
                }
            }
        }
        public static void _doPredict(int country, int lineheight)
        {
            try
            {
                var k = new SM_Now(country, lineheight, (eventNow[country] != null)).Predict();
                File.WriteAllText(Path.Combine(GeneralStoragePath, $"PredictNow", $"{country}-{lineheight}.now.json"), JsonConvert.SerializeObject(k));
                Console.WriteLine($"[GENT] |COM| {country} {lineheight} COMPLETE");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GENT] |-ER| {country} {lineheight} ERR : {ex}");
            }
        }
        public class SM_Now
        {
            /// <summary>
            /// 当前预测生成时间
            /// </summary>
            public DateTime PresentTime;
            /// <summary>
            /// 当前活动开始时间
            /// </summary>
            public long? EventStartTime;
            /// <summary>
            /// 当前活动结束时间
            /// </summary>
            public long? EventEndTime;
            /// <summary>
            /// 区服
            /// </summary>
            public int Country;
            /// <summary>
            /// 榜线高
            /// </summary>
            public int Tier;
            /// <summary>
            /// 是否有活动
            /// </summary>
            public bool IsOnEvent;
            /// <summary>
            /// 当前榜线
            /// </summary>
            public Tracker[] TrackerNow;
            /// <summary>
            /// 线性回归抬升速率
            /// </summary>
            public double Accdy;

            /// <summary>
            /// SMR模型预测
            /// </summary>
            public bool SMRenable = false;
            /// <summary>
            /// 预测模型
            /// </summary>
            public double[] SMRModel;
            /// <summary>
            /// 拟合阶
            /// </summary>
            public int SMROrder;
            /// <summary>
            /// 预测-二维展开列
            /// </summary>
            public double[,] SMRPredict;
            /// <summary>
            /// 抬升高度
            /// </summary>
            public double SMRdy;
            
            /// <summary>
            /// 斜率回归模型
            /// </summary>
            public bool LRenable = false;
            /// <summary>
            /// 线性回归截距 a
            /// </summary>
            public double LRinterceptor;
            /// <summary>
            /// 线性回归斜率 b
            /// </summary>
            public double LRSlope;
            /// <summary>
            /// 线性回归拟合值 => y = ax+b
            /// </summary>
            public double[,] LRpredict;
            /// <summary>
            /// 线性回归抬升高度
            /// </summary>
            public double LRdy;
            

            public SM_Now(int country,int tier, bool _isOn)
            {
                IsOnEvent = _isOn;
                Console.WriteLine($"[PMCP] -{country},{tier},{_isOn}");
                PresentTime = DateTime.Now;
                Country = country;
                Tier = tier;
                var a = Models.GetModel(country, tier);
                if (a != null)
                {
                    SMRModel = a.avg;
                    SMROrder = a.order;
                }
                var dss = GetRenderedBlock((Country)country, tier);
                TrackerNow = dss?.tracker;
                EventStartTime = dss?.eventDetail.startAt;
                EventEndTime = dss?.eventDetail.endAt;
            }
            public SM_Now Predict()
            {
                var tn = TrackerNow;
                List<double> x = new();
                List<double> y = new();

                if (tn != null)
                {
                    foreach (var i in tn)
                    {
                        x.Add(i.PCT * 100);
                        y.Add(i.Score);
                    }
                    if (TrackerNow.Length > 0 && SMRModel != null)
                    {
                        SMRdy = f(TrackerNow[^1].PCT * 100, SMRModel) - TrackerNow[^1].Score;
                    }
                }

                if (x.Count > 2)
                {
                    LRenable = true;
                    var (a, b) = MathNet.Numerics.LinearRegression.SimpleRegression.Fit(x.ToArray(), y.ToArray());
                    LRinterceptor = a;
                    LRSlope = b;
                    LRpredict = yab(a, b);
                    LRdy = (a + b * TrackerNow[^1].PCT * 100) - TrackerNow[^1].PCT;
                    Accdy = (TrackerNow[^1].Score - TrackerNow[^2].Score) / (TrackerNow[^1].PCT * 100 - TrackerNow[^2].PCT * 100);
                }
                if (SMRModel != null)
                {
                    SMRenable = true;
                    SMRPredict = new double[101, 2];
                    foreach (var xi in Enumerable.Range(0, 101))
                    {
                        SMRPredict[xi, 0] = xi;
                        SMRPredict[xi, 1] = f(xi, SMRModel);
                    }
                }
                return this;
            }
        }
        /// <summary>
        /// 默认的预测运算公式
        /// </summary>
        static Func<double, double[], double> f = new( (x,seq) =>
        {
            double y = 0;
            for (int i = 0; i < seq.Length; i++)
            {
                y += seq[i] * Math.Pow(x, i);
            }
            return y;
        });
        /// <summary>
        /// 斜率预测运算公式
        /// </summary>
        static Func<double, double, double[,]> yab = new((a, b) =>
        {
            double[,] xy = new double[101, 2];
            for (int i = 0; i < 101; i++)
            {
                xy[i, 0] = i;
                xy[i, 1] = a + b * i;
            }
            return xy;
        });
        /// <summary>
        /// 一级存储结构
        /// </summary>
        public class FileTracker
        {
            /// <summary>
            /// 是否启用
            /// </summary>
            public bool enable = true;
            /// <summary>
            /// 是否当前榜线
            /// </summary>
            public bool now = false;
            /// <summary>
            /// 是否和模型使用一致
            /// </summary>
            public bool matchModelRequire = false;
            /// <summary>
            /// 唯一识别号 [区服,活动,榜线高]
            /// </summary>
            public int[] id;
            /// <summary>
            /// 活动具体信息
            /// </summary>
            public CountryEvent eventDetail;
            /// <summary>
            /// 榜线集合
            /// </summary>
            public Tracker[] tracker = null;
        }
        /// <summary>
        /// "渲染"一个完整的榜线格
        /// </summary>
        /// <param name="c">服务器</param>
        /// <param name="eventnum">活动号</param>
        /// <param name="lineheight">榜线高</param>
        public static bool RenderingBlock(Country c, int eventnum, int lineheight, bool _now = false)
        {
            var p1 = Path.Combine(GeneralStoragePath, $"{(_now?"TrackerNow":"Trackers")}", $"{(int)c}-{(_now ? "" : $"{eventnum}-")}{lineheight}.json");
            try
            {
                var t = new MultiTracker(eventnum, c, lineheight);
                Tracker[] at = t.Tracker;
                CountryEvent ed = t.@event;
                var ft = new FileTracker()
                {
                    now = _now,
                    id = new int[] { (int)c, eventnum, lineheight },
                    eventDetail = ed,
                };
                if (at != null)
                {
                    ft.tracker = at;
                    if (at?.Length > _minOrder)
                    {
                        ft.matchModelRequire = true;
                    }
                }
                var ss = JsonConvert.SerializeObject(ft);
                if (File.Exists(p1))
                {
                    var str = File.ReadAllText(p1);
                    if (!str.Equals(ss))
                    {
                        File.WriteAllText(p1, ss);
                        Console.WriteLine($"[RNDR] |-U-| {(int)c}-{(_now ? "" : $"{eventnum}-")}{lineheight}");
                    }
                    else
                    {
                        Console.WriteLine($"[RNDR] |-E-| {(int)c}-{(_now ? "" : $"{eventnum}-")}{lineheight}");
                    }
                    return true;
                }
                else
                {
                    File.WriteAllText(p1, ss);
                    Console.WriteLine($"[RNDR] |-C-| {(int)c}-{(_now ? "" : $"{eventnum}-")}{lineheight}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RNDR] |-ER| {(int)c}-{(_now ? "" : $"{eventnum}-")}{lineheight} : {ex.Message}--");
                return false;
            }
        }
        /// <summary>
        /// 获取一个 当前 榜线高
        /// </summary>
        /// <param name="c">区服</param>
        /// <param name="eventnum">活动号</param>
        /// <param name="lineheight">榜线高</param>
        /// <returns></returns>
        public static FileTracker GetRenderedBlock(Country c, int lineheight)
        {
            try
            {
                var p1 = Path.Combine(GeneralStoragePath, $"TrackerNow", $"{(int)c}-{lineheight}.json");
                var fr = File.ReadAllText(p1);
                return JObject.Parse(fr).ToObject<FileTracker>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GNTK] --- Get Tracker RENDER ERR {c},{lineheight} \n :{ex.Message}--");
                return null;
            }
        }
        /// <summary>
        /// 获取一个 历史 榜线高
        /// </summary>
        /// <param name="c">区服</param>
        /// <param name="eventnum">活动号</param>
        /// <param name="lineheight">榜线高</param>
        /// <returns></returns>
        public static FileTracker GetRenderedBlock(Country c,int eventnum, int lineheight, bool _fromFile = false)
        {
            try
            {
                var p1 = Path.Combine(GeneralStoragePath, $"Trackers", $"{(int)c}-{eventnum}-{lineheight}.json");
                if( _fromFile || RenderingBlock(c, eventnum, lineheight, false))
                {
                    var fr = File.ReadAllText(p1);
                    return JObject.Parse(fr).ToObject<FileTracker>();
                }
                else
                {
                    Console.WriteLine($"[GHTK] --- Get Tracker RENDER ERR {c},{lineheight} - NOT AVAILABLE--");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GHTK] --- Get Tracker RENDER ERR {c},{lineheight} \n :{ex.Message}--");
                return null;
            }
        }
    }
}
