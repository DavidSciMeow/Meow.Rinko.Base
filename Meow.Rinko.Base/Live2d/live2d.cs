using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Meow.Rinko.Core.Live2d
{
    /// <summary>
    /// 标准服装列表
    /// </summary>
    public class Live2dList
    {
        /// <summary>
        /// 数据组
        /// </summary>
        public Dictionary<int, Costume?>? Data = new();
        /// <summary>
        /// 获得一个标准的服装列表
        /// </summary>
        public Live2dList(Country c = Country.undefined)
        {
            string k = Bases.CostumeList();
            var puredata = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<int, Costume?>>(k);
            if (c == Country.undefined)
            {
                Data = puredata;
            }
            else
            {
                var a = from d in puredata where d.Value?.publishedAt?[(int)c] != null select d;
                Dictionary<int, Costume?> dt = new();
                foreach (var b in a)
                {
                    dt.Add(b.Key, b.Value);
                }
                Data = dt;
            }
        }
        /// <summary>
        /// 获得Json列表
        /// </summary>
        /// <returns></returns>
        public List<(string name, Country country)> getList()
        {
            List<(string,Country)> l = new();
            foreach (var k in Data ?? new Dictionary<int, Costume?>())
            {
                Country c = Country.undefined;
                var p = k.Value?.publishedAt;
                if (p?[0] != null)
                {
                    c = 0;
                }
                else if(p?[1] != null)
                {
                    c = (Country)1;
                }
                else if(p?[2] != null)
                {
                    c = (Country)2;
                }
                else if(p?[3] != null)
                {
                    c = (Country)3;
                }
                else if(p?[4] != null)
                {
                    c = (Country)4;
                }
                l.Add((k.Value?.assetBundleName ?? "",c));
            }
            return l;
        }
        /// <summary>
        /// 服装详细信息
        /// </summary>
        public class Costume
        {
            /// <summary>
            /// 角色编号
            /// </summary>
            public int characterId;
            /// <summary>
            /// 资源包名(记得加_rip)
            /// </summary>
            public string? assetBundleName;
            /// <summary>
            /// 详细描述
            /// </summary>
            public string?[]? description;
            /// <summary>
            /// 发布时间
            /// </summary>
            public string?[]? publishedAt;
            /// <summary>
            /// 获得live2d包
            /// </summary>
            /// <returns></returns>
            public Live2dSingle getLive2dPack()
            {
                if(publishedAt?[0] is not null)
                {
                    if(this.assetBundleName is not null)
                    {
                        return new Live2dSingle(this.assetBundleName);
                    }
                    else
                    {
                        throw new("ERR on: No AssetBundleName");
                    }
                }
                if (publishedAt?[1] is not null)
                {
                    if (this.assetBundleName is not null)
                    {
                        return new Live2dSingle(this.assetBundleName,(Country)1);
                    }
                    else
                    {
                        throw new("ERR on: No AssetBundleName");
                    }
                }
                if (publishedAt?[2] is not null)
                {
                    if (this.assetBundleName is not null)
                    {
                        return new Live2dSingle(this.assetBundleName, (Country)2);
                    }
                    else
                    {
                        throw new("ERR on: No AssetBundleName");
                    }
                }
                if (publishedAt?[3] is not null)
                {
                    if (this.assetBundleName is not null)
                    {
                        return new Live2dSingle(this.assetBundleName, (Country)3);
                    }
                    else
                    {
                        throw new("ERR on: No AssetBundleName");
                    }
                }
                if (publishedAt?[4] is not null)
                {
                    if (this.assetBundleName is not null)
                    {
                        return new Live2dSingle(this.assetBundleName, (Country)4);
                    }
                    else
                    {
                        throw new("ERR on: No AssetBundleName");
                    }
                }
                if (publishedAt?[5] is not null)
                {
                    if (this.assetBundleName is not null)
                    {
                        return new Live2dSingle(this.assetBundleName, (Country)5);
                    }
                    else
                    {
                        throw new("ERR on: No AssetBundleName");
                    }
                }
                else
                {
                    throw new("ERR on: No Country Exist");
                }
            }
        }
    }
    /// <summary>
    /// 单live2d获取
    /// </summary>
    public class Live2dSingle
    {
        /// <summary>
        /// 数据集合
        /// </summary>
        public ConvertCostume? Data;
        /// <summary>
        /// 单Live2d
        /// </summary>
        /// <param name="name">包名</param>
        /// <param name="c">国别</param>
        public Live2dSingle(string name, Country c = Country.jp)
        {
            string k = Bases.GetDataAssets(name, c);
            JObject jo = JObject.Parse(k);
            Data = Newtonsoft.Json.JsonConvert.DeserializeObject<ConvertCostume>(jo?["Base"]?.ToString() ?? "") ?? new();
            for (int i = 0; i < Data.textures.Length; i++)
            {
                if (!(Data.textures[i].fileName.Contains(".png")))
                {
                    Data.textures[i].fileName = $"{Data.textures[i].fileName}.png";
                }
                if (Data.textures[i].fileName.EndsWith(".png.png"))
                {
                    Data.textures[i].fileName = $"{Data.textures[i].fileName}".Replace(".png.png",".png");
                }
            }
        }
        /// <summary>
        /// 获取当前可用的所有live2d列表
        /// </summary>
        /// <returns></returns>
        public static string GetLive2dJsonList() => Newtonsoft.Json.JsonConvert.SerializeObject(new Live2dList().getList());
        /// <summary>
        /// 获取live2d的详细信息
        /// </summary>
        /// <param name="name">名</param>
        /// <param name="c">区服(默认日服)</param>
        /// <returns></returns>
        public static Live2dSingle? GetExcatNameLive2dPack(string name,Country c = Country.jp)
        {
            //"003_live_default"
            var j = new Live2dList();
            var d = from a in j.Data where (a.Value.assetBundleName == name) && (a.Value?.publishedAt?[(int)c] != null) select a;
            return d.First().Value.getLive2dPack();
        }
        /// <summary>
        /// 内部转换类
        /// </summary>
        public class ConvertCostume
        {
            public class m_File
            {
                public int m_FileID;
                public string m_PathID;
            }
            public class CosFile
            {
                /// <summary>
                /// 路径包
                /// </summary>
                public string bundleName;
                /// <summary>
                /// 文件名
                /// </summary>
                public string fileName;
                /// <summary>
                /// 下载到指定目录根
                /// </summary>
                /// <param name="pathBase"></param>
                /// <param name="c"></param>
                /// <returns></returns>
                public Task<(string f, HttpFiles FileStatus)> DownloadFileInto(string pathBase,Country c = Country.jp)
                {
                    var t = Task.Factory.StartNew(() =>
                    {
                        var f = fileName.Replace(".bytes", "");
                        var p = Path.Combine(pathBase, bundleName, f);
                        try
                        {
                            var (_, FileStatus, _) = File($"https://bestdori.com/assets/{c}/{bundleName}_rip/{f}",p);
                            return (f, FileStatus);
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine($"{f} :: {ex}");
                        }
                        return (f, HttpFiles.PROGRESS_FAIL);

                    });
                    return t;
                }
            }
            public class Param
            {
                public int enabled;
                public int type;
                public int duration;
            }
            public m_File m_GameObject;
            public int m_Enabled;
            public string m_Name;
            public CosFile model;
            public CosFile physics;
            public CosFile[] textures;
            public CosFile transition;
            public CosFile[] motions;
            public Param praramGeneralA;
            public Param paramLoop;
            /// <summary>
            /// 下载整个模型
            /// </summary>
            /// <param name="p"></param>
            /// <param name="c"></param>
            /// <returns></returns>
            public async Task<List<(string f, HttpFiles FileStatus)>> DownloadModel(string p, Country c = Country.jp) 
                => await await Task.Factory.StartNew(async () =>
                    {
                        List<(string f, HttpFiles FileStatus)> l = new();
                        l.Add(await model.DownloadFileInto(p, c));
                        l.Add(await physics.DownloadFileInto(p, c));
                        l.Add(await transition.DownloadFileInto(p, c));
                        foreach (var d in textures)
                        {
                            l.Add(await d.DownloadFileInto(p, c));
                        }
                        foreach (var d in motions)
                        {
                            l.Add(await d.DownloadFileInto(p, c));
                        }
                        return l;
                    });
        }
        public enum HttpFiles
        {
            /// <summary>
            /// 下载成功
            /// </summary>
            Success_Download = 0,
            /// <summary>
            /// 下载失败
            /// </summary>
            Fail_Download = 1,
            /// <summary>
            /// 已存在
            /// </summary>
            Already_Exist = 2,
            /// <summary>
            /// 程序出错
            /// </summary>
            PROGRESS_FAIL = 3
        }
        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="url">网络地址</param>
        /// <param name="path">本地地址</param>
        /// <returns></returns>
        public static (bool Status, HttpFiles FileStatus, string ErrorString) File(string url, string path)
        {
            if (System.IO.File.Exists(Path.GetFullPath(path)))
            {
                return (true, HttpFiles.Already_Exist, "");
            }
            else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                try
                {
                    using var wc = new WebClient();
                    wc.Headers.Add("connection", "close");
                    wc.DownloadFile(url, path);
                    return (true, HttpFiles.Success_Download, "");
                }
                catch (Exception ex)
                {
                    return (false, HttpFiles.PROGRESS_FAIL, ex.Message);
                }
            }
        }
    }
}
