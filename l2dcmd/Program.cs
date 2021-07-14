using System;
using System.Linq;
using System.Threading.Tasks;

namespace l2dcmd
{
    class Program
    {
        static string help = "" +
            "-d (*download) [下载live2d] \n" +
            "-f (*force update) [强制文件完整性检查] \n" +
            "-v (*verbose mode) [详细模式] \n" +
            "-l (*list live2d)[列表所有可用模型] \n";
        static bool verbose = false;
        static bool download = false;
        static bool forcecheck = false;
        static void Main(string[] args)
        {
            if(args.Length > 0)
            {
                if (args[0] != null)
                {
                    var a = args[0];
                    if (a == "-h")
                    {
                        Console.WriteLine(help); 
                        return;
                    }
                    foreach(var k in args)
                    {
                        if(k == "-d")
                        {
                            download = true;
                            Console.WriteLine("已启用下载 [Task Download Start]");
                        }
                        if (k == "-v")
                        {
                            verbose = true;
                            Console.WriteLine("详细信息模式已开启 [verbose mode on]");
                        }
                        if (k == "-f")
                        {
                            forcecheck = true;
                            Console.WriteLine("强制文件完整性筛查已启用 [force Update mode on]");
                        }
                        if(k == "-l")
                        {
                            var j = new Meow.Rinko.Core.Live2d.Live2dList();
                            foreach(var d in j.Data)
                            {
                                Console.WriteLine($"{d.Value.assetBundleName} :: {d.Key}");
                            }
                            Console.WriteLine("列表模式结束 [List mode complete]");
                            return;
                        }
                    }
                    if (download)
                    {
                        if (args.Last() != null)
                        {
                            Download(args);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("无下载路径 重选中");
                            Console.WriteLine("No Download Path");
                        }
                    }
                    else
                    {
                        Console.WriteLine("未发现 -d 参数, 不进行下载");
                        Console.WriteLine("parameter -d isn't present, download cancelled");
                    }
                }
                else
                {
                    Console.WriteLine("无操作 - 没有第一参数");
                    Console.WriteLine("1st Parameter not present");
                }
            }
            else
            {
                Console.WriteLine("无输入 - 请携带参数");
                Console.WriteLine("No Input Parameter");
            }
        }

        private static void Download(string[] args)
        {
            int num = 0;
            int numx = 0;
            Task.Factory.StartNew(() =>
            {
                var j = new Meow.Rinko.Core.Live2d.Live2dList();
                Console.WriteLine($"On Download - Total:{j.Data.Count}");
                Console.WriteLine($"执行下载中 - 总计:{j.Data.Count}");
                foreach (var x in j.Data)
                {
                    if (forcecheck && System.IO.Directory.Exists(System.IO.Path.Combine(args.Last(), "live2d", "chara", x.Value.assetBundleName)))
                    {
                        Console.Clear();
                        Console.WriteLine($"{num++} / {j.Data.Count} [{x.Value.assetBundleName}]");
                        continue;
                    }
                    else
                    {
                        try
                        {
                            var ax = x.Value.getLive2dPack().Data.DownloadModel(args[1]);
                            ax.RunSynchronously();
                            var lax = ax.GetAwaiter().GetResult();
                            lax.ForEach((k) =>
                            {
                                if (verbose)
                                {
                                    Console.WriteLine($"{k.FileStatus} :: {k.f}");
                                }
                                numx++;
                            });
                        }
                        catch
                        {

                        }
                        Console.WriteLine($"{num} / {j.Data.Count} [{numx}] [{x.Value.assetBundleName}]");
                        if (num % 5 == 0)
                        {
                            Console.Clear();
                        }
                        num++;
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("---Complete---");
            });
            Console.WriteLine("On Download");
            Console.WriteLine("执行下载");
            while (true) { }
        }
    }
}
