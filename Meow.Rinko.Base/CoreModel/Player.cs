using System.Collections.Generic;

namespace Meow.Rinko.Core.Model
{
    /// <summary>
    /// 玩家类
    /// </summary>
    public class Player
    {
        /// <summary>
        /// 是否缓存数据
        /// </summary>
        public bool cache { get; set; }
        /// <summary>
        /// 缓存时间(Unix时间戳)
        /// </summary>
        public long time { get; set; }
        /// <summary>
        /// 个人资料
        /// </summary>
        public Profile? profile { get; set; }
        /// <summary>
        /// 个人资料类
        /// </summary>
        public class Profile
        {
            /// <summary>
            /// 用户id
            /// </summary>
            public string? userId { get; set; }
            /// <summary>
            /// 用户名
            /// </summary>
            public string? userName { get; set; }
            /// <summary>
            /// 用户等级
            /// </summary>
            public int rank { get; set; }
            /// <summary>
            /// 用户单项最高等级
            /// </summary>
            public int degree { get; set; }
            /// <summary>
            /// 个人简介
            /// </summary>
            public string? introduction { get; set; }
            /// <summary>
            /// 是否可以查看用户的所有乐队
            /// </summary>
            public bool publishTotalDeckPowerFlg { get; set; }
            /// <summary>
            /// 是否可以查看用户主乐队
            /// </summary>
            public bool publishBandRankFlg { get; set; }
            /// <summary>
            /// 是否可查看用户已经通过(Clear)的乐曲
            /// </summary>
            public bool publishMusicClearedFlg { get; set; }
            /// <summary>
            /// 是否可查看用户已经全连(FullCombo)的乐曲
            /// </summary>
            public bool publishMusicFullComboFlg { get; set; }
            /// <summary>
            /// 是否可查看用户的最高分
            /// </summary>
            public bool publishHighScoreRatingFlg { get; set; }
            /// <summary>
            /// 是否可以查找用户id
            /// </summary>
            public bool publishUserIdFlg { get; set; }
            /// <summary>
            /// 是否可搜索用户
            /// </summary>
            public bool searchableFlg { get; set; }
            /// <summary>
            /// 是否可以查看用户最后登陆时间
            /// </summary>
            public bool publishUpdatedAtFlg { get; set; }
            /// <summary>
            /// 用户是否接受好友
            /// </summary>
            public bool friendApplicableFlg { get; set; }
            /// <summary>
            /// 主乐队操作
            /// </summary>
            public Maindeckusersituations? mainDeckUserSituations { get; set; }
            /// <summary>
            /// 主乐队操作类
            /// </summary>
            public class Maindeckusersituations
            {
                /// <summary>
                /// 数据组
                /// </summary>
                public Data[]? entries { get; set; }
                /// <summary>
                /// 数据组类
                /// </summary>
                public class Data
                {
                    /// <summary>
                    /// 经验值
                    /// </summary>
                    public int exp { get; set; }
                    /// <summary>
                    /// 等级
                    /// </summary>
                    public int level { get; set; }
                    /// <summary>
                    /// 距离下一级
                    /// </summary>
                    public int addExp { get; set; }
                    /// <summary>
                    /// 插图
                    /// </summary>
                    public string? illust { get; set; }
                    /// <summary>
                    /// 用户名
                    /// </summary>
                    public string? userId { get; set; }
                    /// <summary>
                    /// 技巧值经验
                    /// </summary>
                    public int skillExp { get; set; }
                    /// <summary>
                    /// 创立于
                    /// </summary>
                    public string? createdAt { get; set; }
                    /// <summary>
                    /// 技巧值等级
                    /// </summary>
                    public int skillLevel { get; set; }
                    /// <summary>
                    /// 情景(套装)id
                    /// </summary>
                    public int situationId { get; set; }
                    /// <summary>
                    /// 重复数
                    /// </summary>
                    public int duplicateCount { get; set; }
                    /// <summary>
                    /// 训练状态
                    /// </summary>
                    public string? trainingStatus { get; set; }
                }
            }
            /// <summary>
            /// 乐队等级列表
            /// </summary>
            public Bandrankmap? bandRankMap { get; set; }
            /// <summary>
            /// 乐队等级列表类
            /// </summary>
            public class Bandrankmap
            {
                /// <summary>
                /// 乐队等级查询列
                /// </summary>
                public Dictionary<int, int>? entries { get; set; }
            }
            /// <summary>
            /// 完成乐曲列表
            /// </summary>
            public Clearedmusiccountmap? clearedMusicCountMap { get; set; }
            /// <summary>
            /// 完成乐曲列表类
            /// </summary>
            public class Clearedmusiccountmap
            {
                /// <summary>
                /// 数据列
                /// </summary>
                public Data? entries { get; set; }
                /// <summary>
                /// 完成乐曲数据类
                /// </summary>
                public class Data
                {
                    /// <summary>
                    /// 简单(easy)
                    /// </summary>
                    public string? easy { get; set; }
                    /// <summary>
                    /// 困难(Hard)
                    /// </summary>
                    public string? hard { get; set; }
                    /// <summary>
                    /// 专家(Expert)
                    /// </summary>
                    public string? expert { get; set; }
                    /// <summary>
                    /// 普通(Normal)
                    /// </summary>
                    public string? normal { get; set; }
                    /// <summary>
                    /// 特殊(Special)
                    /// </summary>
                    public string? special { get; set; }
                }
            }
            /// <summary>
            /// FC乐曲数目
            /// </summary>
            public Fullcombomusiccountmap? fullComboMusicCountMap { get; set; }
            /// <summary>
            /// FC乐曲类
            /// </summary>
            public class Fullcombomusiccountmap
            {
                /// <summary>
                /// 数据表
                /// </summary>
                public Data? entries { get; set; }
                /// <summary>
                /// 数据类
                /// </summary>
                public class Data
                {
                    /// <summary>
                    /// 简单(Easy)难度
                    /// </summary>
                    public string? easy { get; set; }
                    /// <summary>
                    /// 困难(Hard)难度
                    /// </summary>
                    public string? hard { get; set; }
                    /// <summary>
                    /// 专家(Expert)难度
                    /// </summary>
                    public string? expert { get; set; }
                    /// <summary>
                    /// 普通(Normal)难度
                    /// </summary>
                    public string? normal { get; set; }
                }
            }
            /// <summary>
            /// 用户最高分
            /// </summary>
            public Userhighscorerating? userHighScoreRating { get; set; }
            /// <summary>
            /// 用户最高分列表类
            /// </summary>
            public class Userhighscorerating
            {
                /// <summary>
                /// 其他最高分音乐列表
                /// </summary>
                public Userotherhighscoremusiclist? userOtherHighScoreMusicList { get; set; }
                /// <summary>
                /// R最高分音乐列表
                /// </summary>
                public Userroseliahighscoremusiclist? userRoseliaHighScoreMusicList { get; set; }
                /// <summary>
                /// AG最高分音乐列表
                /// </summary>
                public Userafterglowhighscoremusiclist? userAfterglowHighScoreMusicList { get; set; }
                /// <summary>
                /// PPP最高分音乐列表
                /// </summary>
                public Userpoppinpartyhighscoremusiclist? userPoppinPartyHighScoreMusicList { get; set; }
                /// <summary>
                /// PP最高分音乐列表
                /// </summary>
                public Userpastelpaletteshighscoremusiclist? userPastelPalettesHighScoreMusicList { get; set; }
                /// <summary>
                /// HHW最高分音乐列表
                /// </summary>
                public Userhellohappyworldhighscoremusiclist? userHelloHappyWorldHighScoreMusicList { get; set; }
                /// <summary>
                /// 其他最高分音乐列表
                /// </summary>
                public class Userotherhighscoremusiclist
                {
                    /// <summary>
                    /// 数据
                    /// </summary>
                    public HighScoreMusicList[]? entries { get; set; }
                }
                /// <summary>
                /// R最高分音乐列表
                /// </summary>
                public class Userroseliahighscoremusiclist
                {
                    /// <summary>
                    /// 数据
                    /// </summary>
                    public HighScoreMusicList[]? entries { get; set; }
                }
                /// <summary>
                /// AG最高分音乐列表
                /// </summary>
                public class Userafterglowhighscoremusiclist
                {
                    /// <summary>
                    /// 数据
                    /// </summary>
                    public HighScoreMusicList[]? entries { get; set; }
                }
                /// <summary>
                /// PPP最高分音乐列表
                /// </summary>
                public class Userpoppinpartyhighscoremusiclist
                {
                    /// <summary>
                    /// 数据
                    /// </summary>
                    public HighScoreMusicList[]? entries { get; set; }
                }
                /// <summary>
                /// PP最高分音乐列表
                /// </summary>
                public class Userpastelpaletteshighscoremusiclist
                {
                    /// <summary>
                    /// 数据
                    /// </summary>
                    public HighScoreMusicList[]? entries { get; set; }
                }
                /// <summary>
                /// HHW最高分音乐列表
                /// </summary>
                public class Userhellohappyworldhighscoremusiclist
                {
                    /// <summary>
                    /// 数据
                    /// </summary>
                    public HighScoreMusicList[]? entries { get; set; }
                }
                /// <summary>
                /// 最高分音乐列表类
                /// </summary>
                public class HighScoreMusicList
                {
                    /// <summary>
                    /// 评分
                    /// </summary>
                    public int rating { get; set; }
                    /// <summary>
                    /// 音乐id
                    /// </summary>
                    public int musicId { get; set; }
                    /// <summary>
                    /// 难度
                    /// </summary>
                    public string? difficulty { get; set; }
                }
            }
            /// <summary>
            /// 用户主乐队
            /// </summary>
            public Mainuserdeck? mainUserDeck { get; set; }
            /// <summary>
            /// 用户主乐队类
            /// </summary>
            public class Mainuserdeck
            {
                /// <summary>
                /// 主乐队编号
                /// </summary>
                public int deckId { get; set; }
                /// <summary>
                /// 队长
                /// </summary>
                public int leader { get; set; }
                /// <summary>
                /// 成员1
                /// </summary>
                public int member1 { get; set; }
                /// <summary>
                /// 成员2
                /// </summary>
                public int member2 { get; set; }
                /// <summary>
                /// 成员3
                /// </summary>
                public int member3 { get; set; }
                /// <summary>
                /// 成员4
                /// </summary>
                public int member4 { get; set; }
                /// <summary>
                /// 卡槽名称
                /// </summary>
                public string? deckName { get; set; }
                /// <summary>
                /// 效果加成id
                /// </summary>
                public int[]? bondsEffectIds { get; set; }
            }
            /// <summary>
            /// 用户个人资料状态
            /// </summary>
            public Userprofilesituation? userProfileSituation { get; set; }
            /// <summary>
            /// 用户个人资料状态类
            /// </summary>
            public class Userprofilesituation
            {
                /// <summary>
                /// 插图渲染性
                /// </summary>
                public string? illust { get; set; }
                /// <summary>
                /// 用户id
                /// </summary>
                public string? userId { get; set; }
                /// <summary>
                /// 情景(套装)id
                /// </summary>
                public int situationId { get; set; }
                /// <summary>
                /// 用户状态可用性
                /// </summary>
                public string? viewProfileSituationStatus { get; set; }
            }
            /// <summary>
            /// 用户下挂标
            /// </summary>
            public Userprofiledegreemap? userProfileDegreeMap { get; set; }
            /// <summary>
            /// 用户下挂标类
            /// </summary>
            public class Userprofiledegreemap
            {
                /// <summary>
                /// 数据
                /// </summary>
                public Data? entries { get; set; }
                /// <summary>
                /// 数据类型
                /// </summary>
                public class Data
                {
                    /// <summary>
                    /// 第一个标
                    /// </summary>
                    public Degree? first { get; set; }
                    /// <summary>
                    /// 第二个标
                    /// </summary>
                    public Degree? second { get; set; }
                    /// <summary>
                    /// 标志类
                    /// </summary>
                    public class Degree
                    {
                        /// <summary>
                        /// 用户id
                        /// </summary>
                        public string? userId { get; set; }
                        /// <summary>
                        /// 标志id
                        /// </summary>
                        public int degreeId { get; set; }
                        /// <summary>
                        /// 标志类型
                        /// </summary>
                        public string? profileDegreeType { get; set; }
                    }

                }
            }
            /// <summary>
            /// 用户使用的道具
            /// </summary>
            public Enableduserareaitems? enabledUserAreaItems { get; set; }
            /// <summary>
            /// 用户的道具类
            /// </summary>
            public class Enableduserareaitems
            {
                /// <summary>
                /// 数据存储类
                /// </summary>
                public Data[]? entries { get; set; }
                /// <summary>
                /// 数据
                /// </summary>
                public class Data
                {
                    /// <summary>
                    /// 等级
                    /// </summary>
                    public int level { get; set; }
                    /// <summary>
                    /// 用户id
                    /// </summary>
                    public string? userId { get; set; }
                    /// <summary>
                    /// 道具id
                    /// </summary>
                    public int areaItemId { get; set; }
                    /// <summary>
                    /// 道具从属类型
                    /// </summary>
                    public int areaItemCategory { get; set; }
                }
            }
            /// <summary>
            /// 用户推特
            /// </summary>
            public object? userTwitter { get; set; }
        }
    }
}
