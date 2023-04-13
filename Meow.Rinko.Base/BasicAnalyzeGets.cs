using Meow.Rinko.Core;

namespace Meow.Rinko.BasicAnalyze
{
    /// <summary>
    /// 获取基础处理信息
    /// </summary>
    public class Gets
    {
        /// <summary>
        /// 某服务器区域的活动信息
        /// </summary>
        public class CountryEvent
        {
            /// <summary>
            /// 数据
            /// </summary>
            public Model.CountryEvent Data { get; }
            /// <summary>
            /// 某服务器区域的活动信息
            /// </summary>
            /// <param name="eventnum">活动号</param>
            /// <param name="c">区服</param>
            public CountryEvent(int eventnum, Country c)
            {
                var e = new Core.Gets.Event(eventnum).Data;
                this.Data = new()
                {
                    eventType = e?.eventType,
                    eventName = e?.eventName?[(int)c],
                    assetBundleName = e?.assetBundleName,
                    bannerAssetBundleName = e?.bannerAssetBundleName,
                    startAt = e?.startAt?[(int)c],
                    endAt = e?.endAt?[(int)c],
                    enableFlag = e?.enableFlag?[(int)c],
                    publicStartAt = e?.publicStartAt?[(int)c],
                    publicEndAt = e?.publicEndAt?[(int)c],
                    distributionStartAt = e?.distributionStartAt?[(int)c],
                    distributionEndAt = e?.distributionEndAt?[(int)c],
                    bgmAssetBundleName = e?.bgmAssetBundleName,
                    bgmFileName = e?.bgmFileName,
                    aggregateEndAt = e?.aggregateEndAt?[(int)c],
                    exchangeEndAt = e?.exchangeEndAt?[(int)c],
                    pointRewards = e?.pointRewards,
                    rankingRewards = e?.rankingRewards,
                    attributes = e?.attributes,
                    characters = e?.characters,
                    stories = e?.stories,
                    rewardCards = e?.rewardCards,
                };
            }
        }
    }
}
