using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : EquipmentItem {
    public string testName;     // todo: deleted

    // 其他的一些特殊效果
    
    public WeaponItem()
    {
        testName = "Weapon";
        itemType = ItemType.weapon;
        itemDes = "Test Description for Type:" + itemType + " ID:" + itemID;
    }
}
