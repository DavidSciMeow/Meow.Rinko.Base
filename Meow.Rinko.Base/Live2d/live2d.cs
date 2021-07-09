using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        public Live2dList()
        {
            string k = Bases.CostumeList().GetAwaiter().GetResult();
            Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<int, Costume?>>(k);
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
        }
    }
}
