using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrinketItem : ItemInfo {
    public string testName;     // todo: deleted
    public Stats AttachStats;    // 附加属性

    public TrinketItem()
    {
        testName = "Trinket";
        itemType = ItemType.trinket;
    }

    // 其他的一些特殊效果. 如重生后破碎等.
}
