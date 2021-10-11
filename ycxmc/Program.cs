using Meow.Rinko.BasicAnalyze.Model;
using Meow.Rinko.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// ./ycxcmd cn ./ 1 2000 t

namespace ycxpred
{
    class Program
    {
        public class retval
        {
            public bool enable = false;
            public string Method = "S-M-Recursive";
            public double[] Para;
            public double pctNow;
            public DateTime modelTime;
            public DateTime predictTime;
            public double dy;
            public double[] modelVal;
            public Tracker[] Tracker;
        }
        static string path = "./Tracker";
        static int eventNow = -1;
        static bool kernelStatus = true;
        static bool kernelRefixStatus = true;
        static DateTime upTime;
        static async Task Main(string[] args)
        {
            while (true)
            {
                try
                {
                    if (args.Length == 1)
                    {
                        var c = Enum.Parse<Country>(args[0], true);
                        var eventNow = -1;
                        try
                        {
                            eventNow = new Meow.Rinko.Core.Gets.EventList().EventNow(c).inbound[0];
                        }
                        catch
                        {
                            eventNow = -1;
                        }
                        if (eventNow != -1)
                        {
                            kernelStatus = true;
                            kernelRefixStatus = true;
                            var _1 = await GenerateFile(c, 100);
                            var _2 = await GenerateFile(c, 1000);
                            var _3 = await GenerateFile(c, 2000);
                            Console.WriteLine($"{DateTime.Now} Complete {_1}|{_2}|{_3}");
                        }
                        else
                        {
                            if (kernelStatus)
                            {
                                DeleteFile(c, 100);
                                DeleteFile(c, 1000);
                                DeleteFile(c, 2000);
                                Console.WriteLine($"Complete Reset");
                                Console.WriteLine("NO Active Event Calculate Matrix");
                                ycxcmd.Program.Main(new string[] { args[0], "./" ,"S"});
                                ycxcal.Program.Main(new string[] { args[0], "./" });
                                upTime = DateTime.Now;
                                kernelStatus = false;
                            }
                            else
                            {
                                Console.WriteLine("NO Active Event Calculate Matrix");
                                Console.WriteLine($"Update Complete on {upTime}");
                                Console.WriteLine($"Thread Sleeping");
                                Console.WriteLine($"THIS MESSAGE GENERATE ON {DateTime.Now}");
                                System.Threading.Thread.Sleep(new TimeSpan(0, 55, 0));
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Err Langth * Para err");
                        return;
                    }
                    Console.WriteLine($"{DateTime.Now} - pracompl");
                    Console.WriteLine($"{DateTime.Now} - Sleep");
                    System.Threading.Thread.Sleep(new TimeSpan(0, 15, 0));
                    Console.Clear();
                    Console.WriteLine($"{DateTime.Now} - Reconstruct");
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"{DateTime.Now} : ERR on \n{ex.Message}");
                }
            }
        }
        static async Task<bool> GenerateFile(Country c, int k)
        {
            Tracker[] t = null;
            double pctx = 0;
            try
            {
                var sx = new Meow.Rinko.BasicAnalyze.MultiTracker(eventNow, c, k);
                t = sx.Tracker;
                var sat = sx.@event.startAt;
                var eat = sx.@event.endAt;
                var timenow = new Meow.Util.Basic.TimeX.DateTimeX(DateTime.Now).ToMiSecTimeStamp();
                pctx = (timenow - (double)sat) / ((double)eat - (double)sat);
            }
            catch
            {
                try
                {
                    var ss = await Meow.Util.Network.Http.Get.String($"https://app.rinko.com.cn/Query/TrackerNow/{(int)c}/{k}");
                    t = JObject.Parse(ss)["tracker"].ToObject<Tracker[]>();
                    pctx = JObject.Parse(ss)["pct"].ToObject<double>();
                    Console.WriteLine($"{DateTime.Now} {(int)c}-{k} Event Tracker Networked");
                }
                catch
                {
                    Console.WriteLine($"{DateTime.Now} {(int)c}-{k} Event Tracker Null");
                    return false;
                }
            }
            var pct = t.Last().PCT;
            if (t.Length < 0)
            {
                pct = -1;
            }
            var (predictTime, list, Pavg, prednow) = await Calc(c, k, pct);
            var dy = t.Last().Score - prednow;
            var x = new retval() { pctNow = pctx, modelTime = predictTime, modelVal = list, Para = Pavg, Tracker = t, dy = dy, predictTime = DateTime.Now };
            Directory.CreateDirectory(path);
            try
            {
                File.WriteAllText($"{path}/prednow-{(int)c}-{k}.json", Newtonsoft.Json.JsonConvert.SerializeObject(x, Newtonsoft.Json.Formatting.Indented));
                Console.WriteLine($"{path}/{(int)c}-{k} is complete");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} - {ex.Message}");
                return false;
            }
        }
        static bool DeleteFile(Country c,int k)
        {
            try
            {
                var x = new retval() { modelTime = DateTime.Now, modelVal = null, Para = null, Tracker = null, dy = 0, predictTime = DateTime.Now };
                File.WriteAllText($"{path}/prednow-{(int)c}-{k}.json", Newtonsoft.Json.JsonConvert.SerializeObject(x, Newtonsoft.Json.Formatting.Indented));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} - {ex.Message}");
                return false;
            }
        }
        static async Task<(double[] Avg, DateTime Dt)> GetPara(Country c, int k)
        {
            string s = await Meow.Util.Network.Http.Get.String($"https://ycx.rinko.com.cn/{(int)c}-F-{k}.json");
            JObject jo = JObject.Parse(s);
            var Avg = jo["Avg"].ToObject<double[]>();
            var Dt = jo["presentTime"].ToObject<DateTime>();
            return (Avg, Dt);
        }
        static Func<double, double[], double> ef = (x, avg) =>
        {
            double r = 0;
            for (int j = 0; j < avg.Length; j++)
            {
                r += avg[j] * Math.Pow(x, j);
            }
            return r;
        };
        static Func<double[],double[]> exp = (avg) =>
        {
            List<double> ret = new();
            for (int i = 0; i < 101; i++)
            {
                ret.Add(ef(i,avg));
            }
            return ret.ToArray();
        };
        static async Task<(DateTime predictTime,double[] list,double[] Pavg,double prednow)> Calc(Country c, int tier,double pctnow)
        {
            var (Avg, Dt) = await GetPara(c, tier);
            var l = exp(Avg);
            if (pctnow == -1)
            {
                return (Dt, l, Avg, 0);
            }
            var predl = ef(pctnow * 100, Avg);
            return (Dt, l, Avg, predl);
        }
    }
}
