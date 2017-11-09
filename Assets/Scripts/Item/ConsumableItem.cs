using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 消耗物品类. 使用后给本体提供一个增益.
/// </summary>
public class ConsumableItem : Item {
    public string testName;
    // 附带的技能/效果
    [SerializeField] private List<Buff> m_OwnBuff;  // 给自己的buff/debuff
    [SerializeField] private List<Buff> m_TarBuff;  // 给目标的buff/debuff

    public ConsumableItem()
    {
        testName = "ConsumableItem";
        itemType = ItemType.consumable;
        itemDes = "Test Description for Type:" + itemType + "\nID:" + itemID;
        //isUsable = true;
    }

    public override void UseItem()
    {
        Debug.Log("Use:" + itemID + " " + itemName + " -消耗品");
        InventoryManager.Instance.ConsumeItem(this);
    }

    // 产生Buff:增加buff, 目标:自身
    public void AddBuff(RoleInfo self)
    {
        foreach (Buff buff in m_OwnBuff)
        {
            foreach (var effect in buff.Effects)
            {
                effect.InitEffect();
            }
            self.AddBuff(buff);
        }
    }

    // 产生buff:增加debuff, 目标:作用目标
    public void AddDebuf(RoleInfo target)
    {
        foreach (var buff in m_TarBuff)
        {
            foreach( var effect in buff.Effects)
            {
                effect.InitEffect();
            }
            target.AddDebuff(buff);
        }
    }
}
