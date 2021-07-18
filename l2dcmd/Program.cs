using System;
using System.Linq;
using System.Threading.Tasks;

namespace l2dcmd
{
    class Program
    {
        static string help = "命令模式: 第一参数 第二参数 ... 路径\n" +
            "command mode: para1 para2 para3 .... Path \n" +
            "例子(Example):\n" +
            "下载所有模型到本程序集所在文件夹(download all to this folder) `l2dcmd -d ./`\n" +
            "------------------可用参数-------------------\n" +
            "-d (*download) [下载live2d] \n" +
            "-f (*force update) [强制文件完整性检查] \n" +
            "-v (*verbose mode) [详细模式] \n" +
            "-l (*list live2d) [列表所有可用模型] \n" +
            "-find (*find live2d) [寻找live2d]";
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
                        
                        if(k == "-l" && args.Length == 1)
                        {
                            var j = new Meow.Rinko.Core.Live2d.Live2dList();
                            foreach(var d in j.Data)
                            {
                                Console.WriteLine($"{d.Value.assetBundleName} :: {d.Key}");
                            }
                            Console.WriteLine("列表模式结束 [List mode complete]");
                            return;
                        }
                        else if(k == "-find" && args.Length == 1)
                        {
                            Console.WriteLine("请确认查找模式 | 1 [按角色号对位] | 2 [按包名称对位] | 3 [按描述对位]");
                            Console.WriteLine("Select Mode as | 1 [By charaID] | 2 [By AssetbundleName] | 3 [By Pharse]");
                            switch( Console.ReadLine() switch
                            {
                                "1" => 1,
                                "2" => 2,
                                "3" => 3,
                                _ => 0
                            })
                            {
                                case 1: { }break;
                                case 2: { }break;
                                case 3: { }break;
                                default: Console.WriteLine("未取得有效输入,程序退出 [Programm Exit as no correct parameter present]"); return;
                            }
                            var j = new Meow.Rinko.Core.Live2d.Live2dList();
                            foreach (var d in j.Data)
                            {
                                Console.WriteLine($"{d.Value.assetBundleName} :: {d.Key}");
                            }
                            Console.WriteLine("查找模式已列出所有可能 [Search list complete]");
                            return;
                        }
                        if (k == "-d")
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
                            Console.WriteLine("无下载路径 重选中 [No Download Path]");
                        }
                    }
                    else
                    {
                        Console.WriteLine("未发现 -d 参数, 不进行下载 [parameter -d isn't present, download cancelled]");
                    }
                }
                else
                {
                    Console.WriteLine("无操作 - 没有第一参数 [1st Parameter not present]");
                }
            }
            else
            {
                Console.WriteLine("无输入 - 请携带参数 [No Input Parameter]");
            }
        }

        private static void Download(string[] args)
        {
            int num = 0;
            int numx = 0;
            Console.WriteLine($"下载路径 [PATH] : {args[^1]}");
            Task.Factory.StartNew(async () =>
            {
                var j = new Meow.Rinko.Core.Live2d.Live2dList();
                Console.WriteLine($"执行下载中 [On Download] - 总计 [Total]:{j.Data.Count}");
                foreach (var x in j.Data)
                {
                    Console.WriteLine("输入clear清空显示 [Type `clear` to clear the console prompt]");
                    if (forcecheck && System.IO.Directory.Exists(System.IO.Path.Combine(args[^1], "live2d", "chara", x.Value.assetBundleName)))
                    {
                        Console.Clear();
                        Console.WriteLine($"{num++} / {j.Data.Count} [{x.Value.assetBundleName}]");
                        continue;
                    }
                    else
                    {
                        try
                        {
                            var ax = await x.Value.getLive2dPack().Data.DownloadModel(args[^1]);
                            ax.ForEach((k) =>
                            {
                                if (verbose)
                                {
                                    Console.WriteLine($"{k.FileStatus} :: {k.f}");
                                }
                                numx++;
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[{num}] [{x.Value.assetBundleName}] : {ex.Message}");
                        }
                        Console.WriteLine($"{num} / {j.Data.Count} [{numx}] [{x.Value.assetBundleName}]");
                        num++;
                    }
                }
                Console.WriteLine();
                Console.WriteLine("---已完成 [Complete] ---");
            });
            Console.WriteLine("执行下载 [On Download]");
            Console.WriteLine("");
            while (true) { 
                if(Console.ReadLine() == "clear")
                {
                    Console.Clear();
                }
            }
        }
    }
}
