using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : ItemInfo {
    public string testName;
    public Stats AttachStats;    // 附加属性

    // 其他的一些特殊效果

    //
    public WeaponItem()
    {
        testName = "Weapon";
        itemType = ItemType.weapon;
    }
}
