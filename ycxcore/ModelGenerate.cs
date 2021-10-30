using Meow.Rinko.Core;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;

namespace ycxcore
{
    public class Models
    {
        /// <summary>
        /// 榜线预测轮询间隔 (分钟) 默认10分钟,超时时间为本时间一半
        /// </summary>
        public static int PEInterval = 10; //min
        /// <summary>
        /// 模型生成状态
        /// </summary>
        public static bool ModelStatus = false;
        /// <summary>
        /// 模型文件类
        /// </summary>
        public class VRT
        {
            /// <summary>
            /// 区服
            /// </summary>
            public int country;
            /// <summary>
            /// 榜线高度
            /// </summary>
            public int tiernum;
            /// <summary>
            /// 榜线高存储列
            /// </summary>
            public List<double>[] data = new List<double>[101];
            /// <summary>
            /// 最小泰勒阶
            /// </summary>
            public int order;
            /// <summary>
            /// 模型极值
            /// </summary>
            public double[] max;
            /// <summary>
            /// 模型均值
            /// </summary>
            public double[] avg;
            /// <summary>
            /// 模型最小值
            /// </summary>
            public double[] min;
            /// <summary>
            /// 初始模型
            /// </summary>
            public void _init()
            {
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = new List<double>();
                } //初始化存储档案
            }
            /// <summary>
            /// 计算模型
            /// </summary>
            public void CalModel(int _order = 5)
            {
                order = _order;
                List<double> Max = new();
                List<double> Avg = new();
                List<double> Min = new();
                List<double> x = new();
                for (int i = 0; i < data.Length; i++)
                {
                    var d = data[i];
                    if (d.Any())
                    {
                        x.Add(i);
                        Max.Add(d.Max());
                        Avg.Add(d.Average());
                        Min.Add(d.Min());
                    }
                }
                if (x.Any())
                {
                    max = MathNet.Numerics.Fit.Polynomial(x.ToArray(), Max.ToArray(), order);
                    avg = MathNet.Numerics.Fit.Polynomial(x.ToArray(), Avg.ToArray(), order);
                    min = MathNet.Numerics.Fit.Polynomial(x.ToArray(), Min.ToArray(), order);
                }
                else
                {
                    max = new double[order];
                    avg = new double[order];
                    min = new double[order];
                }
                File.WriteAllText(Path.Combine(Program.GeneralStoragePath, $"Analyze", $"{country}-{tiernum}.modelVRT.json"), JsonConvert.SerializeObject(this));
            }
        }
        /// <summary>
        /// 设置模型类
        /// </summary>
        public class InstallVRT
        {
            public List<VRT> Data = new();
            public InstallVRT()
            {
                foreach (var n in new int[] { 0, 1, 2, 3, 4 })
                {
                    for (int j = 0; j < Program.Tier[n].Length; j++)
                    {
                        Data.Add(new VRT() { country = n, tiernum = Program.Tier[n][j] });
                    }
                }
            }
            public VRT getVRT(int country, int tiernum)
            {
                return (from a in Data where a.country == country && a.tiernum == tiernum select a).First();
            }
        }
        /// <summary>
        /// 模型获取事件
        /// </summary>
        public static void GenerateModel(bool _forceCheckModel = false)
        {
            ModelStatus = true;
            var dts = DateTime.Now;
            var GeneralData = GetModel();
            if ((GeneralData == null) || _forceCheckModel)
            {
                var XGeneralData = new InstallVRT();
                var edata = new Meow.Rinko.Core.Gets.EventList().Data; // 获取所有活动
                Queue<Task> MGT = new();
                foreach (var n in new int[] { 0, 1, 2, 3, 4 })
                {
                    var x = from a in edata orderby a.Value.endAt[n] where a.Value.endAt[n] != null select a.Key;//获取当前服务器所有活动
                    foreach (var i in x)//每个活动
                    {
                        for (int j = 0; j < Program.Tier[n].Length; j++)
                        {
                            var ti = Program.Tier[n][j];
                            var t = new Task(() =>
                            {
                                var d = Program.GetRenderedBlock((Country)n, i, ti); //获取榜线
                                if (d != null)
                                {
                                    foreach (var dx in d.tracker)
                                    {
                                        if (dx.PCT < 0)
                                        {
                                            continue;
                                        }
                                        var vrt = XGeneralData.getVRT(n, ti);
                                        if (vrt.data == null)
                                        {
                                            vrt._init();
                                        }
                                        vrt.data[(int)Math.Floor((dx.PCT > 1 ? 1 : dx.PCT) * 100)].Add(dx.Score);
                                        //下归类右榜线位置(统一小边看齐)
                                    }
                                }
                            });
                            MGT.Enqueue(t);//加入任务族
                        }
                    }
                } //设定任务
                Console.WriteLine($"[PMCP] |TSK| TASK GENERATION COMPLETE--");
                var inlinetask = Program.Tier.Length;
                while (MGT.Any())
                {
                    Console.WriteLine($"[PMCP] |TSK| {MGT.Count}--");
                    List<Task> outbound = new();
                    for (int i = 0; i < inlinetask; i++)
                    {
                        if (MGT.Count > 0)
                        {
                            var tk = MGT.Dequeue();
                            tk.Start();
                            outbound.Add(tk);
                        }
                    }
                    Task.WaitAll(outbound.ToArray());
                    Task.Delay(500).Wait();
                }//执行任务
                var p1 = Path.Combine(Program.GeneralStoragePath, $"Analyze", "generalDatabase.rinkomodel");//模型存取位置
                File.WriteAllText(p1, JsonConvert.SerializeObject(XGeneralData));
            }
            foreach (var i in GeneralData)
            {
                i.CalModel();
            }//计算模型
            var dte = DateTime.Now;
            Console.WriteLine($"[PMCP] |TSK| Data Fetching/Generate Complete in {(dte - dts).TotalSeconds} Sec");
            ModelStatus = false;
        }
        /// <summary>
        /// 获取所有模型
        /// </summary>
        /// <returns></returns>
        public static List<VRT> GetModel()
        {
            var p1 = Path.Combine(Program.GeneralStoragePath, $"Analyze", "generalDatabase.rinkomodel");//模型存取位置
            if (File.Exists(p1))
            {
                var fss = File.ReadAllText(p1);
                var obj = JObject.Parse(fss);
                var objx = JsonConvert.DeserializeObject<List<VRT>>(obj["Data"].ToString());
                return objx;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取模型
        /// </summary>
        /// <param name="country"></param>
        /// <param name="tiernum"></param>
        /// <returns></returns>
        public static VRT GetModel(int country, int tiernum)
        {
            var p1 = Path.Combine(Program.GeneralStoragePath, $"Analyze", $"{country}-{tiernum}.modelVRT.json");
            if (File.Exists(p1))
            {
                var fss = File.ReadAllText(p1);
                return JsonConvert.DeserializeObject<VRT>(fss);
            }
            else
            {
                Console.WriteLine($"[MAIN] Model {country}-{tiernum} unGenerate, use /gm or /gm force, to Generate model");
                return null;
            }
        }
    }
}
