using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TonicItem : ItemInfo {
    public Stats GrowthStats;       // 永久增加的属性值

    private void Start()
    {
        itemType = ItemType.tonic;
    }

    public override void UseItem()
    {
        Debug.Log("Use:" + itemName + " -滋补品");
        PlayerInfo.Instance.PermanentStats += GrowthStats;
    }
}
