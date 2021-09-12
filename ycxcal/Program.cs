using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Meow.Rinko.Core;
using Meow.Util.Network.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ycxcal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            _ = args;
            Country c = Country.undefined;
            try
            {
                c = Enum.Parse<Country>(args[0]);
            }
            catch
            {
                Console.WriteLine("Country un-specified program exits [-122]");
                return;
            }
            List<double[]> GC1 = new();
            List<double[]> GC2 = new();
            List<double[]> GC3 = new();
            List<Task> task = new();
            var d = new DirectoryInfo("./Tracker");
            var fs = d.GetFiles();
            foreach (var f in fs)
            {
                var name = f.FullName;
                var t = new Task<bool>(() =>
                {
                    var d = JObject.Parse(File.ReadAllText(name));
                    if (d.TryGetValue("Enabled", out var eb)) // check if have stack of enable
                    {
                        if (d.TryGetValue("P", out var jr) && (eb?.ToObject<bool>() ?? false))
                        {
                            double[] db = jr.ToObject<double[]>();
                            if (f.Name.EndsWith("-100.json"))
                            {
                                GC1.Add(db);
                            }
                            else if (f.Name.EndsWith("-1000.json"))
                            {
                                GC2.Add(db);
                            }
                            else if (f.Name.EndsWith("-2000.json"))
                            {
                                GC3.Add(db);
                            }
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{f.Name} With DE flag ignore");
                        return false;
                    }
                });
                t.Start();
                task.Add(t);
            }
            Task.WaitAll(task.ToArray());
            Console.WriteLine($"CAL START");
            CAL(GC1,c,100);
            Console.WriteLine("100 COMP");
            CAL(GC2,c,1000);
            Console.WriteLine("1000 COMP");
            CAL(GC3,c,2000);
            Console.WriteLine("2000 COMP");
        }

        private static void CAL(List<double[]> GC,Country c,int tier)
        {
            double[] pMax = new double[6];
            double[] pAvg = new double[6];
            double[] pMin = new double[6];

            var p0 = (from a in GC select a[0]);
            var p1 = (from a in GC select a[1]);
            var p2 = (from a in GC select a[2]);
            var p3 = (from a in GC select a[3]);
            var p4 = (from a in GC select a[4]);
            var p5 = (from a in GC select a[5]);

            pMax[0] = p0.Max();
            pMax[1] = p1.Max();
            pMax[2] = p2.Max();
            pMax[3] = p3.Max();
            pMax[4] = p4.Max();
            pMax[5] = p5.Max();

            pMin[0] = p0.Min();
            pMin[1] = p1.Min();
            pMin[2] = p2.Min();
            pMin[3] = p3.Min();
            pMin[4] = p4.Min();
            pMin[5] = p5.Min();

            pAvg[0] = p0.Average();
            pAvg[1] = p1.Average();
            pAvg[2] = p2.Average();
            pAvg[3] = p3.Average();
            pAvg[4] = p4.Average();
            pAvg[5] = p5.Average();

            var ext = new Extrior(pMax, pMin, pAvg);
            string s = Newtonsoft.Json.JsonConvert.SerializeObject(ext);
            File.WriteAllText($"./Tracker/{(int)c}-F-{tier}.json", s);
        }

        public class Extrior
        {
            public double[] Max;
            public double[] Min;
            public double[] Avg;
            public DateTime presentTime;

            public Extrior(double[] max, double[] min, double[] avg)
            {
                Max = max;
                Min = min;
                Avg = avg;
                presentTime = DateTime.Now;
            }
        }
    }
}
