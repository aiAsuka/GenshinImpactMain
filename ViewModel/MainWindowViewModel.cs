using System.Collections.ObjectModel;
using System.Net.Http;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GenshinImpactMain.Command;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GenshinImpactMain.ViewModel;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] ObservableCollection<CharacterData> _charList = new();

    [RelayCommand]
    async Task Loaded()
    {
        try
        {
            await LoadedCity("727");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    [RelayCommand]
    async Task LoadedCity(string cityId)
    {
        try
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(
                $"https://api-takumi-static.mihoyo.com/content_v2_user/app/16471662a82d418a/getContentList?iAppId=43&iChanId={cityId}&iPageSize=50&iPage=1&sLangKey=zh-cn&iOrder=6"
            );
            request.Method = HttpMethod.Get;

            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "Thunder Client (https://www.thunderclient.com)");

            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            var list = JObject.Parse(result)["data"]?["list"];

            if (list == null)
            {
                await Logger.LogErrorAsync("未获取到Url信息");

                return;
            }

            var roleItem = list.Select(c => new RoleItem()
            {
                Id = Convert.ToInt64(c["iInfoId"]),
                Name = Convert.ToString(c["sTitle"]) ?? "角色名称",
                Ext = Convert.ToString(c["sExt"]) ?? string.Empty,
            })
                .ToList();

            CharList.Clear();

            foreach (var role in roleItem)
            {
                long id = role.Id;

                string ext = role.Ext;

                var characterData = new CharacterData(id) { Name = role.Name };

                // 解析 JSON
                var mediaData = JsonConvert.DeserializeObject<Dictionary<string, object>>(ext);

                // 创建一个字典存储结果
                Dictionary<MediaType, string> mediaDictionary = new Dictionary<MediaType, string>();

                if (mediaData != null)
                    foreach (var kvp in mediaData)
                    {
                        // 解析键名，获取数字部分
                        var keyParts = kvp.Key.Split('_');
                        if (
                            keyParts.Length != 2
                            || !int.TryParse(keyParts[1], out int mediaTypeIndex)
                        )
                            continue;
                        // 转换为 MediaType 枚举
                        if (!Enum.IsDefined(typeof(MediaType), mediaTypeIndex))
                            continue;
                        MediaType mediaType = (MediaType)mediaTypeIndex;
                        string mediaItem = kvp.Value.ToString() ?? string.Empty;
                        if (!string.IsNullOrEmpty(mediaItem))
                            mediaDictionary[mediaType] = mediaItem; // 存入字典
                    }

                Dictionary<MediaType, List<MediaItem>> mediaDic = new();

                foreach (MediaType mediaType in mediaDictionary.Keys)
                {
                    switch (mediaType)
                    {
                        case MediaType.AvatarIcon:
                        case MediaType.Pc:
                        case MediaType.DialogueLines:
                        case MediaType.FirstMeetingInChinese:
                        case MediaType.Chatting1InChinese:
                        case MediaType.Chatting2InChinese:
                        case MediaType.FirstMeetingInJapanese:
                        case MediaType.Chatting1InJapanese:
                        case MediaType.Chatting2InJapanese:
                        case MediaType.Name:
                        case MediaType.Elements:
                        case MediaType.Mobile:
                            {
                                var mediaItems =
                                    JsonConvert.DeserializeObject<List<MediaItem>>(
                                        mediaDictionary[mediaType]
                                    )
                                    ??
                                    [
                                        new(
                                        "Error",
                                        "https://ys.mihoyo.com/main/_nuxt/img/logo-header-cut.f78aabc.png"
                                    )
                                    ];

                                mediaDic.Add(mediaType, mediaItems);
                            }
                            break;

                        case MediaType.Language:
                            characterData.Language = mediaDictionary[mediaType];
                            break;
                        case MediaType.Description:
                            characterData.Description = mediaDictionary[mediaType];
                            break;
                        case MediaType.ChineseDubbing:
                            characterData.ChineseVoiceActor = mediaDictionary[mediaType];
                            break;
                        case MediaType.JapaneseDubbing:
                            characterData.JapaneseVideoActor = mediaDictionary[mediaType];
                            break;
                        case MediaType.Batch:
                            characterData.Batch = Convert.ToInt32(mediaDictionary[mediaType]);
                            break;
                        default:
                            await Logger.LogErrorAsync("出现不可处理的媒体类型");
                            break;
                    }
                }

                characterData.Media = mediaDic;

                CharList.Add(characterData);
            }
        }
        catch (Exception ex)
        {
            await Logger.LogErrorAsync("");
        }
    }
}