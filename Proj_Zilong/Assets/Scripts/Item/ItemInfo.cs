using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

// 类别
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

[Serializable]
public class ItemInfo {

    public ItemType itemType;   // 类型
    [Header("基本属性")]
    public int itemID;          // ID
    public string itemName;     // 名称
    public Image itemIcon;      // 图标
    public string itemDes;      // 物品描述

    public Stats AttachStats;   // 附加属性
    public Stats GrowthStats;   // 永久增加的属性值

    public virtual void UseItem() { }
}
