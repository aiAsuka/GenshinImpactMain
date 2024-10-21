namespace GenshinImpactMain.Command
{


    /// <summary>
    /// 角色信息
    /// </summary>
    public class RoleItem
    {
        public long Id { get; set; }

        public string Name { get; set; } = "角色名称";

        public string Ext { get; set; } = string.Empty;
    }

    // 用于存储媒体项的类
    public class MediaItem(string name, string url)
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = name;

        /// <summary>
        /// 链接
        /// </summary>
        public string Url { get; set; } = url;
    }


    // 用于存储整个 JSON 数据的类
    public class CharacterData(long id)
    {

        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; } = id;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = "角色名称";

        /// <summary>
        /// 媒体信息
        /// </summary>
        public Dictionary<MediaType, List<MediaItem>> Media { get; set; } = new();

        /// <summary>
        /// 语言
        /// </summary>
        public string Language { get; set; } = "中/日";

        /// <summary>
        /// 中文配音
        /// </summary>
        public string ChineseVoiceActor { get; set; } = "暂无";

        /// <summary>
        /// 日文配音
        /// </summary>
        public string JapaneseVideoActor { get; set; } = "暂无";

        /// <summary>
        /// 介绍
        /// </summary>
        public string Description { get; set; } = "暂无";

        /// <summary>
        /// 批次
        /// </summary>
        public int Batch { get; set; } = 1;
    }

    /// <summary>
    /// 媒体类型
    /// </summary>
    public enum MediaType
    {
        /// <summary>
        /// 图标
        /// </summary>
        AvatarIcon = 0,

        /// <summary>
        /// PC端
        /// </summary>
        Pc = 1,

        /// <summary>
        /// 名字
        /// </summary>
        Name = 2,

        /// <summary>
        /// 元素
        /// </summary>
        Elements = 3,

        /// <summary>
        /// 语言
        /// </summary>
        Language = 4,

        /// <summary>
        /// 中文配音
        /// </summary>
        ChineseDubbing = 5,

        /// <summary>
        /// 日文配音
        /// </summary>
        JapaneseDubbing = 6,

        /// <summary>
        /// 介绍
        /// </summary>
        Description = 7,

        /// <summary>
        /// 台词
        /// </summary>
        DialogueLines = 8,

        /// <summary>
        /// 中文-初次见面
        /// </summary>
        FirstMeetingInChinese = 9,

        /// <summary>
        /// 中文-闲聊1
        /// </summary>
        Chatting1InChinese = 10,

        /// <summary>
        /// 中文-闲聊2
        /// </summary>
        Chatting2InChinese = 11,

        /// <summary>
        /// 日文-初次见面
        /// </summary>
        FirstMeetingInJapanese = 12,

        /// <summary>
        /// 日文-闲聊1
        /// </summary>
        Chatting1InJapanese = 13,

        /// <summary>
        /// 日文-闲聊2
        /// </summary>
        Chatting2InJapanese = 14,

        /// <summary>
        /// 移动端
        /// </summary>
        Mobile = 15,

        /// <summary>
        /// 添加批次
        /// </summary>
        Batch = 16
    }
}
