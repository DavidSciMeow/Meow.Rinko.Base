using Meow.Util.Network.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                public string? m_PathID;
            }
            public class CosFile
            {
                /// <summary>
                /// 路径包
                /// </summary>
                public string? bundleName;
                /// <summary>
                /// 文件名
                /// </summary>
                public string? fileName;
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
                            var (_, FileStatus, _) = Get.File($"https://bestdori.com/assets/{c}/{bundleName}_rip/{f}",p);
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
            public m_File? m_GameObject;
            public int m_Enabled;
            public string? m_Name;
            public CosFile? model;
            public CosFile? physics;
            public CosFile[]? textures;
            public CosFile? transition;
            public CosFile[]? motions;
            public Param? praramGeneralA;
            public Param? paramLoop;
            /// <summary>
            /// 下载整个模型
            /// </summary>
            /// <param name="p"></param>
            /// <param name="c"></param>
            /// <returns></returns>
            public async Task<List<(string f, HttpFiles FileStatus)>> DownloadModel(string p, Country c = Country.jp) 
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
                var k = Path.Combine(p, "l2o.json");
                if (!File.Exists(p))
                {
                    var jo = ConvertModelToLive2dObject();
                    var path = Path.Combine(p, model.bundleName, "l2o.json");//moc 和 json在一起
                    File.WriteAllText(path, Newtonsoft.Json.JsonConvert.SerializeObject(jo));
                    l.Add(("l2o.json", HttpFiles.Success_Download));
                }
                else
                {
                    l.Add(("l2o.json", HttpFiles.Already_Exist));
                }
                return l;
            }
            /// <summary>
            /// 转换到Live2dObjectModel
            /// </summary>
            /// <returns></returns>
            public Live2dObject ConvertModelToLive2dObject()
            {
                List<string> textures = new();
                foreach(var f in this.textures)
                {
                    textures.Add(f.fileName);
                }
                return new Live2dObject()
                {
                    model = $"{this.model.fileName.Replace(".moc.bytes",".moc")}",
                    physics = $"{this.physics.fileName}",
                    textures = textures.ToArray(),
                };
            }
            /// <summary>
            /// 转换到Json序列
            /// </summary>
            /// <returns></returns>
            public string ConvertModelToLive2dObjectJson() => Newtonsoft.Json.JsonConvert.SerializeObject(ConvertModelToLive2dObject());
        }
    }
    /// <summary>
    /// 标准化live2dJson
    /// </summary>
    public class Live2dObject
    {
        /// <summary>
        /// live2d类型
        /// </summary>
        public string? type { get; set; } = "RinkoBot - Live2D Model Settings";
        /// <summary>
        /// 名称
        /// </summary>
        public string? name { get; set; } = "";
        /// <summary>
        /// 模型类型
        /// </summary>
        public string? model { get; set; } = "";
        /// <summary>
        /// 材质列表
        /// </summary>
        public string?[]? textures { get; set; } = Array.Empty<string>();
        /// <summary>
        /// 物理
        /// </summary>
        public string? physics { get; set; } = "";
        /// <summary>
        /// 表情
        /// </summary>
        public Expression?[]? expressions { get; set; }
        /// <summary>
        /// 点击位置
        /// </summary>
        public Hit_Areas?[]? hit_areas { get; set; }
        /// <summary>
        /// 动作
        /// </summary>
        public Motions? motions { get; set; }
        /// <summary>
        /// 动作文件类
        /// </summary>
        public class Motions
        {
            public File[]? idle { get; set; }
            public File[]? tap_body { get; set; }
            public File[]? pinch_in { get; set; }
            public File[]? pinch_out { get; set; }
            public File[]? shake { get; set; }
            public File[]? flick_head { get; set; }
            public class File
            {
                public string? file { get; set; }
                public string? sound { get; set; }
                public int? fade_in { get; set; }
                public int? fade_out { get; set; }
            }

        }
        /// <summary>
        /// 表情类
        /// </summary>
        public class Expression
        {
            public string? name { get; set; }
            public string? file { get; set; }
        }
        /// <summary>
        /// 点击区域类
        /// </summary>
        public class Hit_Areas
        {
            public string? name { get; set; }
            public string? id { get; set; }
        }
    }
}
