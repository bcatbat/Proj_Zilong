using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class Item:IComparable {    
    [Header("基本属性")]
    public int itemID;          // ID
    public string itemName;     // 名称
    public ItemType itemType;   // 类型
    public string itemDes;      // 物品描述
    public Sprite itemIcon;      // 图标
    public int buffID;          // 携带的buffid, 0为空

    //public bool isUsable;       // 是否可以使用    

    public Item()
    {
        // 0号物品...占空符.
        itemID = 0; 
        itemName = "";
        itemIcon = null;
        itemDes = "";
        //isUsable = false;
    }

    public virtual void UseItem()
    {
        Debug.Log("使用物品");
        // 数量减少
        InventoryManager.Instance.ConsumeItem(this);
    }

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
