

namespace Meow.Rinko.Test
{
    class Program
    {
        static async void Main(string[] args)
        {
            var k = new Meow.Rinko.Core.Live2d.Live2dList().Data;
            foreach(var d in k)
            {
                System.Console.WriteLine($"{d.Key}:{d.Value}");
                var dd = d.Value.getLive2dPack().Data;
                await dd.DownloadModel("path");
            }
        }
    }
}
