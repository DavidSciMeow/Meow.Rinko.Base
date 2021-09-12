using Meow.Rinko.BasicAnalyze;
using Meow.Rinko.Core;
using Newtonsoft.Json;
using System.Linq;
using System;
using System.IO;
using System.Collections.Generic;

namespace ycxcmd
{
    public class Program
    {
        public static void Main(string[] args)
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
            //args 1 and 2 is used to rectify the path./ and country 
            if(c != Country.undefined && c != null)
            {
                if (args.Length == 5)
                {
                    Console.WriteLine($"Altering Event");
                    if (!Directory.Exists(Path.Combine(path, "Tracker")))
                    {
                        Directory.CreateDirectory(Path.Combine(path, "Tracker"));
                    }
                    int i = 0;
                    int p = 0;
                    bool ebl = true;
                    try
                    {
                        i = int.Parse(args[2]);
                    }
                    catch
                    {
                        Console.WriteLine("Prog ERR by para [number] input err");
                        return;
                    }
                    try
                    {
                        p = int.Parse(args[3]);
                    }
                    catch
                    {
                        Console.WriteLine("Prog ERR by para [tier] input err");
                        return;
                    }
                    try
                    {
                        ebl = !args[4].Equals("f");
                    }
                    catch
                    {
                        Console.WriteLine("Prog ERR by para [enable] input err");
                        return;
                    }
                    var fsfl = Path.Combine(path, $"Tracker", $"{(int)c}-{i}-{p}.json");
                    var d = JsonConvert.DeserializeObject<AnalyzeTracker>(File.ReadAllText(fsfl));
                    d.Enabled = ebl;
                    Console.WriteLine($"{(int)c}-{i}-{p} event is {ebl}");
                    File.WriteAllText(fsfl, JsonConvert.SerializeObject(d));
                    return;
                }
                else
                {
                    if (!Directory.Exists(Path.Combine(path, "Tracker")))
                    {
                        Directory.CreateDirectory(Path.Combine(path, "Tracker"));
                    }
                    Console.WriteLine($"DIRC:{Path.Combine(path, "Tracker")}");
                    var l = new Meow.Rinko.Core.Gets.EventList().Data;
                    var r = (from a in l where a.Value.eventName[(int)c] != null orderby a.Key ascending select a);
                    foreach (var i in r)
                    {
                        RenderingBlock(c, path, i.Key);
                    }
                    Console.WriteLine("-- PROGRAM COMPUTING FINALIZE --");
                    return;
                }
            }
        }
        static void RenderingBlock(Country? c, string path, int i)
        {
            var p1 = Path.Combine(path, $"Tracker", $"{(int)c}-{i}-{100}.json");
            var p2 = Path.Combine(path, $"Tracker", $"{(int)c}-{i}-{1000}.json");
            var p3 = Path.Combine(path, $"Tracker", $"{(int)c}-{i}-{2000}.json");
            try
            {
                if (!File.Exists(p1))
                {
                    var bk1 = new AnalyzeTracker(i, 100, c ?? Country.cn);
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
                    var bk2 = new AnalyzeTracker(i, 1000, c ?? Country.cn);
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
                    var bk3 = new AnalyzeTracker(i, 2000, c ?? Country.cn);
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
    }
}
