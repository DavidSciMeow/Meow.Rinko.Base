﻿using Meow.Rinko.Core;
using Newtonsoft.Json;
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
            var j = new Core.Live2d.Live2dList();
            var p = j.Data[40].getLive2dPack();
            Console.WriteLine(j);
            Console.WriteLine(p);
            Console.ReadLine();
        }
    }
}
