using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryGrid : Grid {

    // 只挂载物品.
    public Item item;  

    private new void Awake()
    {
        // 注册事件
        base.Awake();
        // 同步item与image等信息.
    }

    private void Start()
    {
        // 显示数量和图标.
        RefreshMark();
    }

#region MouseEvent
    
    protected override void EventListener_OnMouseEndDrag(GameObject gb)
    {
        base.EventListener_OnMouseEndDrag(gb);
    }

    protected override void EventListener_OnMouseDrag(GameObject gb)
    {
        base.EventListener_OnMouseDrag(gb);
    }

    protected override void EventListener_OnMouseBeginDrag(GameObject gb)
    {
        base.EventListener_OnMouseBeginDrag(gb);
    }

    // 右击: 物品栏中点右键. 药品是使用, 装备是装上
    protected override void EventListener_OnMouseRightClick(GameObject gb)
    {
        //Debug.Log("右键点击" + item.itemID);
        if (item.itemType == ItemType.consumable || item.itemType == ItemType.tonic)
        {
            UseItem();
        }
        // 装备武器
        if (item.itemType == ItemType.weapon)
        {
            SetupWeapon();
        }

        // 装备护甲
        if (item.itemType == ItemType.armor)
        {
            SetupArmor();
        }

        // 装备饰品
        if (item.itemType == ItemType.trinket)
        {
            SetupTrinket();
        }
    }
    #endregion
    // 显示描述框
    protected override void ShowDescription()
    {
        if (item.itemID != 0)
        {
            string s = item.itemID + "\n" +
                item.itemName + "\n" +
                item.itemType;
            DescriptionManager.Instance.Show(icon, s);
        }
    }

    // 使用物品
    public override void UseItem()
    {
        if (InventoryManager.Instance.ContainsItem(item))
        {
            if (InventoryManager.Instance[item] > 0)
            {
                // 物品起效果
                item.UseItem();
                // 数量减少
                InventoryManager.Instance.ConsumeItem(item);
            }
            // 更新显示
            Refresh();
        }
    }

    // todo:装备武器
    private void SetupWeapon()
    {
        // 武器
        if (item.GetType() == typeof(WeaponItem))
        {
            WeaponItem weapon = EquipmentManager.Instance.weapon as WeaponItem;

            // 武器槽中是当前物品:卸下
            if (weapon == item)
            {
                //Debug.Log("卸下武器!");
                EquipmentManager.Instance.UnloadWeapon(item as WeaponItem);
            }
            else // 否则,武器槽中物品替换为当前物品
            {
                //Debug.Log("装备武器!");                
                EquipmentManager.Instance.EquipWeapon(item as WeaponItem);
            }
            InventoryManager.Instance.Refresh();            
        }
    }

    // todo:装备护甲
    private void SetupArmor()
    {
        // 护甲
        if (item.GetType() == typeof(ArmorItem))
        {
            ArmorItem armor = EquipmentManager.Instance.armor as ArmorItem;

            if (armor == item)
            {
                //Debug.Log("卸下护甲");
                EquipmentManager.Instance.UnloadArmor(item as ArmorItem);
            }
            else
            {
                //Debug.Log("装备护甲!");
                EquipmentManager.Instance.EquipArmor(item as ArmorItem);                
            }
            InventoryManager.Instance.Refresh();
        }
    }

    // todo:装备饰品
    private void SetupTrinket()
    {
        //饰品
        if (item.itemType == ItemType.trinket)
        {
            if (EquipmentManager.Instance.IsTrinketEquipped(item as TrinketItem))
            {
                // 装备栏中已有该物品
                //Debug.Log("卸下饰品");
                EquipmentManager.Instance.UnloadTrinket(item as TrinketItem);
            }
            else
            {
                //Debug.Log("装备饰品");
                EquipmentManager.Instance.EquipTrinket(item as TrinketItem);                
            }
            InventoryManager.Instance.Refresh();
        }
    }
        
    private void Refresh()
    {
        if (InventoryManager.Instance.ContainsItem(item))
        {
            int num = InventoryManager.Instance.inventory[item];
            if (num == 1)
            {
                mark.text = "";
            }
            else
            {
                if (num > 1)
                {
                    mark.text = num + "";
                }
            }
        }
    }

    // 显示标记
   private void RefreshMark()
    {
        Refresh();

        if(item.GetType() == typeof(WeaponItem) ||
            item.GetType() == typeof(ArmorItem) ||
            item.GetType() == typeof(TrinketItem))
        {
            EquipmentItem ei = item as EquipmentItem;
            if (ei.isEquipped)
                mark.text = "E";
            else
                mark.text = "";
        }
    }
}
