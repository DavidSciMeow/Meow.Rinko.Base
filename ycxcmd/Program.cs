using Meow.Rinko.BasicAnalyze;
using Meow.Rinko.Core;
using Newtonsoft.Json;
using System.Linq;
using System;
using System.IO;
using System.Collections.Generic;

namespace ycxcmd
{
    class Program
    {
        static Func<List<AnalyzeTracker>, (double[], double[], double[])> finalfunc = (List<AnalyzeTracker> ln) =>
        {
            List<double> ParaAvg = new();
            List<double> ParaMax = new();
            List<double> ParaMin = new();
            for (int i = 0; i < ln.First().P.Length; i++)
            {
                ParaAvg.Add((from a in ln select a.P[i]).Average());
                ParaMax.Add((from a in ln select a.P[i]).Max());
                ParaMin.Add((from a in ln select a.P[i]).Min());
            }
            return (ParaMax.ToArray(), ParaAvg.ToArray(), ParaMin.ToArray());
        };
        static Func<List<AnalyzeTracker>, double[]> finalX = (List<AnalyzeTracker> ln) =>
        {
            Dictionary<double,double> xy = new();
            foreach (var k in ln)
            {
                for (int i = 0; i < k.x.Count; i++)
                {
                    xy.Add(k.x[i], k.y[i]);
                }
            }
            var paraxy = (from ir in xy orderby ir.Key ascending select ir).Distinct();
            var x = from i in paraxy select i.Key;
            var y = from i in paraxy select i.Value;
            var P = MathNet.Numerics.Fit.Polynomial(x.ToArray(), y.ToArray(), 5);
            return P;
        };
        static void Main(string[] args)
        {
            Country? c = Country.undefined;
            try
            {
                c = Enum.Parse<Country>(args[0]);
            }
            catch
            {
                Console.WriteLine("Country un-specified program exits [-122]");
                return;
            }
            string path = "./";
            try
            {
                path = args[1];
            }
            catch
            {
                Console.WriteLine("path para null [in default './']");
            }
            if (c != Country.undefined || c != null)
            {
                if (!Directory.Exists(Path.Combine(path, "Tracker")))
                {
                    Directory.CreateDirectory(Path.Combine(path, "Tracker"));
                }

                Console.WriteLine($"DIRC:{Path.Combine(path, "Tracker")}");
                var l = new Meow.Rinko.Core.Gets.EventList().Data;
                var r = (from a in l where a.Value.eventName[(int)c] != null orderby a.Key ascending select a);
                List<AnalyzeTracker> lbk1 = new();
                List<AnalyzeTracker> lbk2 = new();
                List<AnalyzeTracker> lbk3 = new();

                if (File.Exists(Path.Combine(path, $"Tracker", "Core.listdata")))
                {
                    Console.WriteLine("FAST FORWARD READING INTERNAL SETS");
                    var t = File.ReadAllText(Path.Combine(path, $"Tracker", "Core.listdata"));
                    var x = JsonConvert.DeserializeObject<Restore>(t);
                    lbk1 = x.lbk1;
                    lbk2 = x.lbk2;
                    lbk3 = x.lbk3;
                    Console.WriteLine($"Restore Data Complete Data SPC:{x.dt}");
                }

                foreach (var i in r)
                {
                    var p1 = Path.Combine(path, $"Tracker", $"{(int)c}-{i.Key}-{100}.json");
                    var p2 = Path.Combine(path, $"Tracker", $"{(int)c}-{i.Key}-{1000}.json");
                    var p3 = Path.Combine(path, $"Tracker", $"{(int)c}-{i.Key}-{2000}.json");
                    try
                    {
                        if (!File.Exists(p1))
                        {
                            var bk1 = new AnalyzeTracker(i.Key, 100, c ?? Country.cn);
                            lbk1.Add(bk1);
                            File.WriteAllText(p1, JsonConvert.SerializeObject(bk1));
                            Console.WriteLine($"{p1}->DLCBP");
                            System.Threading.Thread.Sleep(500);
                        }
                        else
                        {
                            Console.WriteLine($"{p1}->EXIST");
                        }
                    }
                    catch
                    {
                        Console.WriteLine($"err-{p1}");
                    }
                    try
                    {
                        if (!File.Exists(p2))
                        {
                            var bk2 = new AnalyzeTracker(i.Key, 1000, c ?? Country.cn);
                            lbk2.Add(bk2);
                            File.WriteAllText(p2, JsonConvert.SerializeObject(bk2));
                            Console.WriteLine($"{p2}->DLCBP");
                            System.Threading.Thread.Sleep(500);
                        }
                        else
                        {
                            Console.WriteLine($"{p2}->EXIST");
                        }
                    }
                    catch
                    {
                        Console.WriteLine($"err-{p2}");
                    }
                    try
                    {
                        if (!File.Exists(p3))
                        {
                            var bk3 = new AnalyzeTracker(i.Key, 2000, c ?? Country.cn);
                            lbk3.Add(bk3);
                            File.WriteAllText(p3, JsonConvert.SerializeObject(bk3));
                            Console.WriteLine($"{p3}->DLCBP");
                            System.Threading.Thread.Sleep(500);
                        }
                        else
                        {
                            Console.WriteLine($"{p3}->EXIST");
                        }
                    }
                    catch
                    {
                        Console.WriteLine($"err-{p3}");
                    }
                }

                Restore rr = new(lbk1, lbk2, lbk3, DateTime.Now);
                File.WriteAllText(Path.Combine(path, $"Tracker", "Core.listdata"), JsonConvert.SerializeObject(rr));
                Console.WriteLine("-- PROGRAM COMPUTING FINALIZE --");
                var k1 = new Result_Final(finalfunc(lbk1));
                var k2 = new Result_Final(finalfunc(lbk2));
                var k3 = new Result_Final(finalfunc(lbk3));
                File.WriteAllText(Path.Combine(path, $"Tracker", $"{(int)c}-FG-100.json"), JsonConvert.SerializeObject(k1));
                File.WriteAllText(Path.Combine(path, $"Tracker", $"{(int)c}-FG-1000.json"), JsonConvert.SerializeObject(k2));
                File.WriteAllText(Path.Combine(path, $"Tracker", $"{(int)c}-FG-2000.json"), JsonConvert.SerializeObject(k3));
                Console.WriteLine("Programm Final Updated Complete - FG");
                var kk1 = new Result(finalX(lbk1));
                var kk2 = new Result(finalX(lbk2));
                var kk3 = new Result(finalX(lbk3));
                File.WriteAllText(Path.Combine(path, $"Tracker", $"{(int)c}-FX-100.json"), JsonConvert.SerializeObject(kk1));
                File.WriteAllText(Path.Combine(path, $"Tracker", $"{(int)c}-FX-1000.json"), JsonConvert.SerializeObject(kk2));
                File.WriteAllText(Path.Combine(path, $"Tracker", $"{(int)c}-FX-2000.json"), JsonConvert.SerializeObject(kk3));
                Console.WriteLine("Programm Final Updated Complete - FX");
                Console.WriteLine("Programm Final Updated Complete");
            }
        }
    }
    public class Result
    {
        public double[] Parameter;
        public int order = 5;
        public DateTime PresentTime;
        public Result(double[] parameter)
        {
            Parameter = parameter;
            PresentTime = DateTime.Now;
        }
    }
    public class Result_Final 
    {
        public double[] Max;
        public double[] Avg;
        public double[] Min;
        public DateTime PresentTime;
        public Result_Final((double[] max, double[] avg, double[] min) a)
        {
            Max = a.max;
            Avg = a.avg;
            Min = a.min;
            PresentTime = DateTime.Now;
        }
    }
    public class Restore
    {
        public List<AnalyzeTracker> lbk1 = new();
        public List<AnalyzeTracker> lbk2 = new();
        public List<AnalyzeTracker> lbk3 = new();
        public DateTime dt = new();
        public Restore(List<AnalyzeTracker> lbk1, List<AnalyzeTracker> lbk2, List<AnalyzeTracker> lbk3, DateTime dt)
        {
            this.lbk1 = lbk1;
            this.lbk2 = lbk2;
            this.lbk3 = lbk3;
            this.dt = dt;
        }
    }
}
