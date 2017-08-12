using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EquipmentGrid : Grid {
    // 挂载装备(武器, 护甲, 饰品)
    public EquipmentItem equipmentItem;


    // 右击:装备栏格子上点右键是卸下
    protected override void EventListener_OnMouseRightClick(GameObject gb)
    {        //    // 装备武器
        //    if (item.itemType == ItemType.weapon)
        //    {
        //        SetupWeapon();
        //    }

        //    // 装备护甲
        //    if (item.itemType == ItemType.armor)
        //    {
        //        SetupArmor();
        //    }

        //    // 装备饰品
        //    if (item.itemType == ItemType.trinket)
        //    {
        //        SetupTrinket();
        //    }

        //if(tag == "Equipment")
        //{
        //    // Debug.Log("这是装备栏 - " + itemInfo.GetType());
        //    // 卸下当前武器
        //    if (item.itemType == ItemType.weapon)
        //    {
        //        EquipmentControl.Instance.UnloadWeapon(item as WeaponItem);
        //    }
        

        //    // 卸下当前护甲
        //    if (item.itemType == ItemType.armor)
        //    {
        //        EquipmentControl.Instance.UnloadArmor(item as ArmorItem);
        //    }

        //    // 卸下当前饰品
        //    if (item.itemType == ItemType.trinket)
        //    {
        //        EquipmentControl.Instance.UnloadTrinket(item as TrinketItem);
        //    }
        //}
    }
}
