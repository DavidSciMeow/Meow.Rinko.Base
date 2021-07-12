using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            string k = Bases.CostumeList().GetAwaiter().GetResult();
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
        public List<string> getList()
        {
            List<string> l = new();
            foreach (var k in Data ?? new Dictionary<int, Costume?>())
            {
                l.Add(k.Value?.assetBundleName ?? "");
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
            string k = Bases.GetDataAssets(name, c).GetAwaiter().GetResult();
            JObject jo = JObject.Parse(k);
            Data = Newtonsoft.Json.JsonConvert.DeserializeObject<ConvertCostume>(jo?["Base"]?.ToString() ?? "");
            for (int i = 0; i < Data.textures.Length; i++)
            {
                Data.textures[i].fileName = $"{Data.textures[i].fileName}.png";
            }
        }
        /// <summary>
        /// 获取当前可用的所有live2d列表
        /// </summary>
        /// <returns></returns>
        public static string GetLive2dJsonList()
        {
            var j = new Live2dList();
            List<string> l = new();
            foreach (var dk in j.Data ?? new Dictionary<int, Live2dList.Costume?>())
            {
                l.Add(dk.Value?.assetBundleName ?? "");
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(l.ToArray());
        }
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
                public string bundleName;
                public string fileName;
                /// <summary>
                /// 下载到指定目录根
                /// </summary>
                /// <param name="pathBase"></param>
                /// <param name="c"></param>
                /// <returns></returns>
                public Task<(string f, Util.Network.HttpFiles FileStatus)> DownloadFileInto(string pathBase,Country c = Country.jp)
                {
                    var t = Task.Factory.StartNew(() =>
                    {
                        var f = fileName.Replace(".bytes", "");
                        var p = Path.Combine(pathBase, bundleName, f);
                        var (_, FileStatus, _) = Util.Network.Http.Get.File(
                            $"https://bestdori.com/assets/{c}/{bundleName}_rip/{f}",
                            p.Replace(".png.png",".png"));
                        return (f, FileStatus);
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

            public async Task<List<(string f, Util.Network.HttpFiles FileStatus)>> DownloadModel(string p,Country c = Country.jp)
            {
                var t = Task.Factory.StartNew(async () =>
                {
                    List<(string f, Util.Network.HttpFiles FileStatus)> l = new();
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
                return await await t;
            }
        }
    }
}
