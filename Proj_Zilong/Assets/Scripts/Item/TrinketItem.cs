using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrinketItem : EquipmentItem {
    public string testName;     // todo: deleted

    public TrinketItem()
    {
        testName = "Trinket";
        itemType = ItemType.trinket;
        itemDes = "Test Description for Type:" + itemType + " ID:" + itemID;
    }

    // 其他的一些特殊效果. 如重生后破碎等.
}
