using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TonicItem : Item {
    public string testName;
    public Stats growthStats;       // 永久增加的属性值

    public TonicItem()
    {
        testName = "Tonic";
        itemType = ItemType.tonic;
        itemDes = "Test Description for Type:" + itemType + " ID:" + itemID;
        //isUsable = true;
    }

    public override void UseItem()
    {
        Debug.Log("Use:" + itemName + " -滋补品");
        PlayerInfo.Instance.PermanentStats += growthStats;
        InventoryManager.Instance.ConsumeItem(this);
    }
}
