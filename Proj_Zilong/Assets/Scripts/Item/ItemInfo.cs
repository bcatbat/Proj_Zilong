using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

// 类别
public enum Slot
{
    consumable = 0,     // 常规消耗品
    tonic,              // 滋补品, 增加固定属性.
    weapon,             // 武器
    armor,              // 护甲
    trinket,            // 饰品
    task                // 任务物品
}

public class ItemInfo:MonoBehaviour {
    protected Slot m_slot;           // 类型
    [Header("基本属性")]
    public int itemID;          // ID
    public string itemName;     // 名称
    public Image itemIcon;      // 图标
    public string itemDes;      // 物品描述

    public virtual void UseItem()
    {

    }
}
