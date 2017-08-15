using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EquipmentGrid : Grid {
    // 挂载装备(武器, 护甲, 饰品)
    public EquipmentItem equipmentItem;

#region MouseEvent
    // 只接收物品栏. 快捷栏上使用就是装备->使用
    protected override void EventListener_OnMouseDrop(GameObject gb)
    {
        base.EventListener_OnMouseDrop(gb);
    }
    // 右击:装备栏格子上点右键是卸下
    protected override void EventListener_OnMouseRightClick(GameObject gb)
    {        
        // 卸下
        if(equipmentItem.GetType()== typeof(WeaponItem))
        {
            EquipmentManager.Instance.UnloadWeapon(equipmentItem as WeaponItem);
        }
        if(equipmentItem.GetType() == typeof(ArmorItem))
        {
            EquipmentManager.Instance.UnloadArmor(equipmentItem as ArmorItem);
        }
        if(equipmentItem.GetType() == typeof(TrinketItem))
        {
            EquipmentManager.Instance.UnloadTrinket(equipmentItem as TrinketItem);
        }
    }

    protected override void EventListener_OnMouseLeftClick(GameObject gb)
    {
        base.EventListener_OnMouseLeftClick(gb);
    }
#endregion
    protected override void ShowDescription()
    {
        string s = equipmentItem.itemID + "\n" +
            equipmentItem.itemName + "\n" +
            equipmentItem.itemType;
        DescriptionManager.Instance.Show(icon, s);
    }

    public override void UseItem()
    {
        // todo:使用装备附加技能
    }
}
