using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryGrid : Grid {

    // 只挂载物品.
    public Item item;

    // todo:事件. 物品 使用时, 销毁时, 贩卖时, 购买时, 拾取时触发
    public event EventHandler InventoryChanged;

    private new void Awake()
    {
        // 注册事件
        base.Awake();
        // 同步item与image等信息.
    }

    protected override void EventListener_OnMouseDrop(GameObject gb)
    {
        // gb为被拖拽的grid,接收drop的为自身inventory grid. 可接收inventory grid

        var targetGrid = gb.GetComponent<InventoryGrid>();
        if (targetGrid != null)
        {
            Debug.Log("释放物体是inventory格子,可继续");
            item = targetGrid.item;
        }        
    }

    // 右击: 物品栏中点右键. 药品是使用, 装备是装上
    protected override void EventListener_OnMouseRightClick(GameObject gb)
    {
        //if (tag == "Inventory")
        //{
        //    //Debug.Log(gb.name+"右键点击");
        //    // 选框
        //    InventoryControl.Instance.SetCurrentItem(this);

        //    // 使用物品
        //    if (item.itemType == ItemType.consumable || item.itemType == ItemType.tonic)
        //    {
        //        UseItem();
        //    }

        //    // 装备武器
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
        //}

    }

    // 使用物品
    public override void UseItem()
    {
        base.UseItem();
        // 类型检测.
    }
}
