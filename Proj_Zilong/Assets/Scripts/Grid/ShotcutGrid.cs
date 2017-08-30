using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotcutGrid : Grid {
    public int shotcutIndex;    // 数量固定, 给个编号.
    // 可以挂载物品(药物,补品,装备),技能,装备(与装备不同则更换, 相同则使用附加的技能). 仅仅一个有效
    public Skill skill;
    public Item item;    

    protected override void Awake()
    {
        base.Awake();
        item = new Item();  //默认,id=0,空, todo:特别设定
    }

    private void Start()
    {
        InventoryManager.Instance.OnItemChanged += Refresh;
    }

    #region MouseEvent
    // 大部分接收...接收物品栏,技能栏,快捷栏. 仅接受可以使用的
    protected override void EventListener_OnMouseDrop(GameObject gb)
    {
        // 物品栏-仅可以使用者
        InventoryGrid inventoryGrid = gb.GetComponent<InventoryGrid>();
        if(inventoryGrid != null)
        {
            if(item.itemID == 0)
            {
                Debug.Log("shotcut 放置");
                item = gb.GetComponent<InventoryGrid>().item;
            }
            else
            {
                Debug.Log("shotcut 替换");
                InventoryGrid temp = inventoryGrid;
                //inventoryGrid.item = item;
                item = temp.item;
            }
            ShotCutManager.Instance[shotcutIndex] = item;
            ShotCutManager.Instance.CheckDuplication(item,shotcutIndex);
            Refresh();
            return;
        }

        //// 装备栏-取消
        //EquipmentGrid equipmentGrid = gb.GetComponent<EquipmentGrid>();
        //if(equipmentGrid != null)
        //{
        //    if(item.itemID == 0)
        //    {
        //        Debug.Log("shotcut 放置");
        //        item = gb.GetComponent<EquipmentGrid>().equipmentItem;
        //    }
        //    else
        //    {
        //        Debug.Log("shotcut 替换");
        //        item = equipmentGrid.equipmentItem;
        //    }
        //    ShotCutManager.Instance[shotcutIndex] = item;
        //    ShotCutManager.Instance.CheckDuplication(item, shotcutIndex);
        //    Refresh();
        //    return;
        //}

        // todo:技能栏
        SkillGrid skillGrid = gb.GetComponent<SkillGrid>();
        if(skillGrid != null)
        {
            return;
        }

        // 快捷栏
        ShotcutGrid shotcutGrid = gb.GetComponent<ShotcutGrid>();
        if (shotcutGrid != null)
        {
            if (item.itemID == 0)
            {
                Debug.Log("shotcut 放置");
                item = gb.GetComponent<ShotcutGrid>().item;
            }
            else
            {
                Debug.Log("shotcut 替换");
                ShotcutGrid temp = shotcutGrid;
                shotcutGrid.item = item;
                item = temp.item;

            }
            ShotCutManager.Instance[shotcutIndex] = item;
            ShotCutManager.Instance.CheckDuplication(item, shotcutIndex);
            Refresh();
            return;
        }

        Refresh();
    }

    protected override void EventListener_OnMouseEndDrag(GameObject gb)
    {
        // 清除临时图标
        base.EventListener_OnMouseEndDrag(gb);
        // 清空当前项目
        item = new Item();
        Refresh();
    }

    protected override void EventListener_OnMouseBeginDrag(GameObject gb)
    {
        if(item.itemID != 0)
            base.EventListener_OnMouseBeginDrag(gb);
    }

    protected override void EventListener_OnMouseLeftClick(GameObject gb)
    {
        if (skill.skillID != 0)
        {
            Debug.Log("使用技能.");
        }
        if(item.itemID != 0)
        {
            if (item.itemType == ItemType.consumable ||
                item.itemType == ItemType.tonic)
            {
                UseItem();
            }
        }
    }

    protected override void EventListener_OnMouseRightClick(GameObject gb)
    {
        EventListener_OnMouseLeftClick(gb);
    }
    #endregion

    protected override void HideDescription()
    {
        base.HideDescription();
    }

    // todo: 修改一下显示内容
    protected override void ShowDescription()
    {
        if(item.itemID != 0)
        {
            string s = item.itemName + "\n" + item.itemType + "\n" + item.itemID;
            DescriptionManager.Instance.Show(icon, s);
        }
    }

    // todo:刷新图标
    public void Refresh()
    {
        if(skill.skillID != 0)
        {
            // todo:设定技能图示
        }
        if(item.itemID != 0)
        {
            // todo:设定物品图示
            //icon = item.itemIcon;
            //cdBoard = null;
            if (InventoryManager.Instance.ContainsItem(item))
            {
                mark.text = "" + InventoryManager.Instance[item];
                cdTick.text = "";
            }
            else
            {
                mark.text = "0";
            }
        }
        else
        {
            mark.text = "";
        }
    }

    // 使用物品
    public override void UseItem()
    {
        // 使用品
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
}
