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
public class Item:IComparable {

    public ItemType itemType;   // 类型
    [Header("基本属性")]
    public int itemID;          // ID
    public string itemName;     // 名称
    public Image itemIcon;      // 图标
    public string itemDes;      // 物品描述

    public Item()
    {
        // 0号物品...占空符.
        itemID = 0; 
        itemName = "";
        itemIcon = null;
        itemDes = "";        
    }

    public virtual void UseItem() { }

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;
        var desItem = obj as Item;
        if (desItem != null)
        {
            if (this.itemType == desItem.itemType)
            {
                return itemID.CompareTo(desItem.itemID);
            }
            else
            {
                return itemType.CompareTo(desItem.itemType);
            }
        }
        else
        {
            throw new ArgumentException("object is not a item");
        }
    }
}
