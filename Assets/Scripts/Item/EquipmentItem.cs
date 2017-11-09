using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EquipmentItem : Item {
    public Stats attachStats;    // 附加属性 
    public bool isEquipped;      // 装备标识符

    public EquipmentItem()
    {        
        attachStats = new Stats();
        isEquipped = false;
    }

    // 装备上以后, 角色添加附带技能

    // 卸下来以后, 附带技能不可用.
}
