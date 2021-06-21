using Newtonsoft.Json;
using System;

namespace Meow.Rinko.Core.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            _ = args;
            //var job = new Api.Tracker(Country.cn, 104, 100);
            //var j = new Statics.Character();
            //var j = new Api.BandoriPlayer(Country.cn, 1000871004, 0);
            var j = new Gets.News();
            Console.WriteLine(j);
            Console.ReadLine();
        }
    }
}
