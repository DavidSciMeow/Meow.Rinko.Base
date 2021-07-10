using Meow.Rinko.Core;
using Newtonsoft.Json;
using System.Linq;
using System;

namespace Meow.Rinko.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            _ = args;
            //var j = new Core.Api.Tracker(Country.cn, 104, 100);
            //var j = new Statics.Character();
            //var j = new Core.Api.BandoriPlayer(Country.cn, 1000871004, 0);
            //var j = new Gets.News();
            //var j = new BasicAnalyze.MultiTracker(104, Core.Country.cn, 100);

            var j = new Core.Live2d.Live2dList(Country.cn).getList();
            foreach(var dd in j)
            {
                var k = Core.Live2d.Live2dSingle.GetExcatNameLive2dPack(dd).Data;
                var d = k.DownloadModel("D:\\NF").GetAwaiter().GetResult();
                foreach (var kk in d)
                {
                    Console.WriteLine($"{kk.f} :: {kk.FileStatus}");
                }
            }

            //delegate pathe*
            //$"https://bestdori.com/assets/{country}/live2d/chara/{name}_rip/{filename}"
            Console.ReadLine();
        }
    }
}
