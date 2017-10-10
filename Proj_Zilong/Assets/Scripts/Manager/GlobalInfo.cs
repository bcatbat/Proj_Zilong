using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInfo{
    public static string nextScene; // 即将载入的下一个场景的名称
    public static string msgText;   // 消息框文字

    // todo
    private static int progressChapter;  // 剧情进度: chapter_id
    private static int progressEpisode;  // 剧情进度: episode_id
    private static int progressParagraph;    // 剧情进度: paragraph_id
    public static StoryProgress progress;   // 剧情进度
}
