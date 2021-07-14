using Meow.Rinko.Core;
using Newtonsoft.Json;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace Meow.Rinko.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            _ = args;
            //var j = new Core.Api.Tracker(Country.cn, 104, 100);
            //var j = new Statics.Character();
            //var j = new Core.Api.BandoriPlayer(Country.cn, 1000871004, 0);
            //var j = new Gets.News();
            //var j = new BasicAnalyze.MultiTracker(104, Core.Country.cn, 100);
            var jj = new Core.Live2d.Live2dList();
            foreach(var ddx in jj.Data)
            {
                await ddx.Value.getLive2dPack().Data.DownloadModel("D:\\NF");
                Console.WriteLine(ddx.Value.assetBundleName);
            }
            //delegate pathe*
            //$"https://bestdori.com/assets/{country}/live2d/chara/{name}_rip/{filename}"
            Console.ReadLine();
        }
    }
}
