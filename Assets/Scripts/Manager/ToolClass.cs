using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对话类.
/// </summary>
public class Dialog
{
    public int ID { get; set; }
    public string Role { get; set; }
    public string Content { get; set; }
    public Dialog(int id, string role, string content)
    {
        ID = id;
        Role = role;
        Content = content;
    }

    public override string ToString()
    {
        return ID + " " + Role + " " + Content;
    }
}

/// <summary>
/// 剧情进度类.
/// </summary>
public class StoryProgress
{
    public int progressChpater;         // 章. 目前仅有0,1
    public int progressEpisode;         // 节. 包含若干mainPara和splitPara, 前者为剧情流, 后者为挂载
    public int progressParagraph;       // 段. 包含dialog相关枚举

    public StoryProgress() { progressParagraph = 0; progressEpisode = 0; progressParagraph = 0; }
    public StoryProgress(int chapter, int episode, int para)
    {
        progressChpater = chapter;
        progressEpisode = episode;
        progressParagraph = para;
    }

    public override string ToString()
    {
        return progressChpater + " " + progressEpisode + " " + progressParagraph;
    }
}


/// <summary>
/// 物品类型
/// </summary>
public enum ItemType
{
    consumable = 0,     // 常规消耗品
    tonic,              // 滋补品, 增加固定属性.
    weapon,             // 武器
    armor,              // 护甲
    trinket,            // 饰品
    material,           // 素材
    task                // 任务物品
}
 
/// <summary>
/// 属性列表
/// </summary>
public struct Stats
{
    private int upHp;   // 生命上限
    private int upMp;   // 能量上限
    private int atk;    // 攻击力
    private int def;    // 防御力
    private int spd;    // 移动速度
    private int luk;    // 幸运值

    public int UpHp { get { return upHp; } set { this.upHp = value; } }
    public int UpMp { get { return upMp; } set { this.upMp = value; } }
    public int Atk { get { return atk; } set { this.atk = value; } }
    public int Def { get { return def; } set { this.def = value; } }
    public int Spd { get { return spd; } set { this.spd = value; } }
    public int Luk { get { return luk; } set { this.luk = value; } }

    public Stats(int uphp, int upmp, int atk, int def, int spd, int luk)
    {
        upHp = uphp; upMp = upmp;
        this.atk = atk;
        this.def = def;
        this.spd = spd; this.luk = luk;
    }

    public static Stats operator +(Stats s1, Stats s2)
    {
        return new Stats(s1.UpHp + s2.UpHp,
            s1.UpMp + s2.UpMp,
            s1.Atk + s2.Atk,
            s1.Def + s2.Def,
            s1.Spd + s2.Spd,
            s1.Luk + s2.Luk
            );
    }

    public static Stats operator -(Stats s1, Stats s2)
    {
        return new Stats(s1.UpHp - s2.UpHp,
            s1.UpMp - s2.UpMp,
            s1.Atk - s2.Atk,
            s1.Def - s2.Def,
            s1.Spd - s2.Spd,
            s1.Luk - s2.Luk
            );
    }
    public override string ToString()
    {
        return "UpHp:" + upHp + " UpMp:" + upMp + " Atk:" + atk + " Def:" + def + " Spd:" + spd + " Luk:" + luk;
    }
}

/// <summary>
/// 角色归属. 魏/蜀/吴/其他.
/// </summary>
public enum RoleBelonging
{
    other,      // 其他-未知
    Shu,        // 蜀汉
    Wu,         // 东吴
    Wei,        // 曹魏    
}

/// <summary>
/// 自定义延时类. 等待若干秒或是遇到点击终止
/// </summary>
public class WaitForSecondsUnlessClicked : CustomYieldInstruction
{
    private float totalTime;
    private float startTime;
    private string buttonName;
    public override bool keepWaiting
    {
        get
        {
            if (Input.GetButtonDown(buttonName))
                return false;   // 遇到点击,不再等待
            else if (Time.realtimeSinceStartup > startTime + totalTime)
                return false;   // 超时了,不在等待
            else
                return true;    // 其余, 都在等待
        }
    }
    public WaitForSecondsUnlessClicked(float seconds, string buttonName)
    {
        totalTime = seconds;
        startTime = Time.realtimeSinceStartup;
        this.buttonName = buttonName;
    }
}