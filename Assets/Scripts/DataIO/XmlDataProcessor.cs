using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System;
using System.Reflection;

/// <summary>
/// xml文档处理. [已废弃]
/// </summary>
public static class XmlDataProcessor {

    #region buffProcessor

    private static XElement buffList;   // 增益库缓存

    // todo:读取Buff,根据xml文件
    public static void ReadBuffData()
    {
        // todo
        string buffPath = Application.streamingAssetsPath + "/buffListXml.xml";
        var buffFile = File.ReadAllText(buffPath);
        if (buffFile == null)
        {
            Debug.LogError("Not found buffList file!");
            return;
        }

        buffList = XElement.Load(buffFile);
    }

    // todo:从id读取增益效果
    public static Buff GetBuffByID(int id)
    {
        Debug.Log("GetBuffByID没写");
        return default(Buff);
    }
#endregion

    #region skillProcessor
    // todo:从id读取技能
    public static Skill GetSkillByID(int id)
    {
        Debug.Log("GetSkillByID没写");
        return default(Skill);
    }

    // todo:读取Skill,根据xml文件
    public static void ReadSkillData()
    {

    }
#endregion
}
