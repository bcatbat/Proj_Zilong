using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 消耗物品类. 使用后给本体提供一个增益.
/// </summary>
public class ConsumableItem : ItemInfo {
    // 附带的技能/效果

    private void Start()
    {
        m_slot = Slot.consumable;
    }

    public override void UseItem()
    {
        Debug.Log("Use:" + itemName + " -消耗品");
    }
}
