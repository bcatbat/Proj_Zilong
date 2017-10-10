using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

/// <summary>
/// story数据处理类. 静态
/// </summary>
public static class StoryDataProcessor {
    #region StoryProcessor
    private static List<StoryProgress> progressList;
    private static XElement curStory;         // 故事, 根节点. 包含章枚举
    private static XElement curChapter;       // 章. 包含id(progress), name, 节枚举
    private static XElement curEpisode;       // 节. 包含id(progress), name, title, 段枚举(main & split)
    private static XElement curPara;          // 段(仅考虑main). 包含id(progress), 对话枚举

    private static AssetBundle portraitSpriteAssetBundle;   // 角色头像Sprite资源包

    // 读取story xml文件
    private static XElement ReadStoryData()
    {
        // load root-stroy
        string storyPath = Application.streamingAssetsPath + "/storyXml.xml";
        curStory = XElement.Load(storyPath);
        if (curStory == null)
        {
            Debug.LogError("未找到story文件!!");
        }
        Debug.Log("读取story文件完毕");
        InitPortraitSpriteAssetBundle();    // 读取头像
        return curStory;
    }

    // 读取title of epsisode, 用来设置标题栏. 注: 调用时机, 必须为节开始进度  
    public static string GetTitleOfEpisode()
    {
        string title = (string)curEpisode.Element("title");

        // 检测是否在节头. 在节头的时机, 方可调用
        if (GlobalInfo.progress.progressParagraph != 0)
        {            
            Debug.LogError("未在节的开始调用获取节标题方法!");
        }

        return title;
    }

    // 获取当前剧情对话段落. StoryProgress->dialog Enu. 载入/读取时可用
    private static IEnumerable<Dialog> GetCurrentMainParagraph(int progressChapter, int progressEpisode, int progressParagraph)
    {
        // 总体检测
        if (curStory == null)
        {
            ReadStoryData();
        }

        // 读取章
        //chapterEl
        var chapterElements = from el in curStory.Elements("chapter")
                              where (int)el.Element("chapterID") == progressChapter
                              select el;
        // 如果读取结果是空, 则抛出读取失败
        if (chapterElements == null)
            Debug.LogError("没有找到" + progressChapter + "章");            
        curChapter = chapterElements.First();

        // 读取节
        var episodeElements = from el in curChapter.Elements("episode")
                              where (int)el.Element("episodeID") == progressEpisode
                              select el;
        // 如果读取结果为空, 则抛出读取失败
        if (episodeElements == null)
            Debug.LogError("没有找到" + progressEpisode + "节");
        curEpisode = episodeElements.First();

        // 读取段落
        var paraElements = from el in episodeElements.Elements("mainParagraph")
                           where (int)el.Element("paraID") == progressParagraph
                           select el;
        // 如果读取结果为空, 则抛出读取失败.
        if (paraElements == null)
            Debug.LogError("没有找到" + progressParagraph + "段");
        curPara = paraElements.First();

        // 提取出对话的枚举
        var dialogsElements = from el in paraElements.Elements("dialog")
                              select new Dialog(
                                  (int)el.Element("id"),
                                  (string)el.Element("role"),
                                  (string)el.Element("content"));


        // 如果读取结果为空, 则抛出读取失败
        if (dialogsElements == null)
            Debug.LogError("段落" + progressParagraph + "中, 未找到对话流");

        // 返回对话的dialog枚举.
        return dialogsElements;
    }

    // 获取当前剧情对话段落. 重用.
    public static IEnumerable<Dialog> GetCurrentMainParagraph(StoryProgress sp)
    {
        return GetCurrentMainParagraph(sp.progressChpater, sp.progressEpisode, sp.progressParagraph);
    }

    // 生成一个List<StoryProgress>. (0,0,0)->(1,0,0)->(1,1,0)->...
    private static List<StoryProgress> CreateProgressList()
    {
        List<StoryProgress> pEnum = new List<StoryProgress>();

        if (curStory == null)
            ReadStoryData();

        foreach (var chapter in curStory.Elements("chapter"))
        {
            foreach (var episode in chapter.Elements("episode"))
            {
                foreach (var mpara in episode.Elements("mainParagraph"))
                {
                    StoryProgress temp = new StoryProgress(
                        (int)chapter.Element("chapterID"),
                        (int)episode.Element("episodeID"),
                        (int)mpara.Element("paraID"));

                    // 确保当前全局进度是包含在内置进度中的.
                    if(temp.progressChpater == GlobalInfo.progress.progressChpater &&
                        temp.progressEpisode == GlobalInfo.progress.progressEpisode &&
                        temp.progressParagraph == GlobalInfo.progress.progressParagraph)
                    {
                        GlobalInfo.progress = temp;
                    }
                    pEnum.Add(temp);
                }
            }
        }
        return pEnum;
    }

    // 初始化头像资源包
    private static void InitPortraitSpriteAssetBundle()
    {
        if(portraitSpriteAssetBundle == null)
        {
            portraitSpriteAssetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/rolejpg");
        }
    }

    // 读取头像sprite. 不匹配的话就返回默认"未知头像"
    public static Sprite LoadSpriteFromAssetBundle(string name)
    {
        string filename = name;
        if(portraitSpriteAssetBundle == null)
        {
            Debug.LogError("portrait asset bundle not found");
            return null;
        }
        bool isContained = portraitSpriteAssetBundle.Contains(filename);
        Sprite loadedSprite;
        if (isContained)
        {
            loadedSprite = portraitSpriteAssetBundle.LoadAsset<Sprite>(filename);
        }
        else
        {
            // 默认返回一个未知头像.
            loadedSprite = portraitSpriteAssetBundle.LoadAsset<Sprite>("0-未知");
        }        
        return loadedSprite;
    }

    #endregion

    // 剧情进度向前推进.[T]
    public static bool StoryForward()
    {
        // paraID++ || paraID=0&epsisodeID++ || paraID=0&episodeID=0&chapterID++ || over            
        if (progressList == null)
            progressList = CreateProgressList();

        var index = progressList.IndexOf(GlobalInfo.progress); //  -1
        //Debug.Log(index);
        if (index == progressList.Count - 1)
        {
            // WriteLine("剧终");
            Debug.Log(" 剧终");
            return false;
        }

        GlobalInfo.progress = progressList[index + 1];
        //WriteLine(progress);    // test print
        return true;
    }
}
