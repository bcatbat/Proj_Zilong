using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {    
    
    public ItemInfo itemInfo;  // 物品的信息
    public int itemNum = 1;    // 物品的数量

    private Image itemIcon;     // 物品的图标
    //private string itemDes;     // 物品的描述

    private Text itemMark;      // 角标

    // 初始化
    private void Awake()
    {
        itemIcon = GetComponent<Image>();
        itemMark = GetComponentInChildren<Text>();
       // itemDes = itemInfo.itemDes;
    }

    private void Start()
    {
        UIEventListener eventListener = gameObject.AddComponent<UIEventListener>();
        eventListener.OnMouseLeftClick += EventListener_OnMouseLeftClick;        
        eventListener.OnMouseRightClick += EventListener_OnMouseRightClick;
        eventListener.OnMouseEnter += EventListener_OnMouseEnter;
        eventListener.OnMouseExit += EventListener_OnMouseExit;

        if (itemMark != null)
        {
            //Debug.Log("显示在背包中");
            ShowNumber();
        }
        else
        {
            //Debug.Log("未显示在背包中");
        }
    }

    // 鼠标划出
    private void EventListener_OnMouseExit(GameObject gb)
    {
        //Debug.Log(gb.name+"鼠标划出");
        HideDescription();        
    }

    // 鼠标划入
    private void EventListener_OnMouseEnter(GameObject gb)
    {
        //Debug.Log(gb.name+"鼠标划入");
        ShowDescription();        
    }

    // 右键点击
    private void EventListener_OnMouseRightClick(GameObject gb)
    {
        //Debug.Log(gb.name+"右键点击");
        if (itemInfo.itemType == ItemType.consumable || itemInfo.itemType == ItemType.tonic)
        {
            UseItem();
        }

        if (itemInfo.itemType == ItemType.weapon || itemInfo.itemType == ItemType.armor || itemInfo.itemType == ItemType.trinket)
        {
            Equip();
        }
    }

    // 左键点击, 选中(或可以与OnCover合并)
    private void EventListener_OnMouseLeftClick(GameObject gb)
    {
        Debug.Log(gb.name+"左键点击!");
    }

    // 显示数量标记
    public void ShowNumber()
    {
        if(itemNum <= 0)
        {
            //清空..
            //itemMark.text = "null";
            InventoryControl.Instance.CheckEmpty();
        }
        if(itemNum == 1)
        {
            itemMark.text = " ";   // 数量为1时, 不显示数字;
        }
        if(itemNum > 1)
        {
            itemMark.text = "" + itemNum; // 数量大于1时, 显示数字;
        }
    }     

    // 显示描述框
    public void ShowDescription()
    {        
        DescriptionCtrl.Instance.SetText(itemInfo.itemDes);
        DescriptionCtrl.Instance.Show(itemIcon);
    }

    // 隐藏描述框
    public void HideDescription()
    {
        DescriptionCtrl.Instance.Hide();
    }

    // 清空格子


    // 使用物品
    public void UseItem()
    {
        // 右键点击触发. 物品数量减1,物品起作用->敌我等.
        Debug.Log("eat");
        itemNum--;
        ShowNumber();
    }

    // 装上
    public void Equip()
    {
        // 角标标注E. 物品栏替代, player slot替换
        SetEquipedMark();

        // 武器
        if(itemInfo.itemType == ItemType.weapon)
        {
            // 替换
            var weapon = EquipmentControl.Instance.weaponSlot.GetComponent<Item>();
            
        }
        // 护甲
        if (itemInfo.itemType == ItemType.armor)
        {
            var armor = EquipmentControl.Instance.armorSlot.GetComponent<Item>();
        }

        // 饰品
        if(itemInfo.itemType == ItemType.trinket)
        {
            var trinket1 = EquipmentControl.Instance.trinketSlot1.GetComponent<Item>();
            var trinket2 = EquipmentControl.Instance.trinketSlot2.GetComponent<Item>();
        }
    }

    // 卸下
    public void UnEquip()
    {
        // 角标标注E取消, 物品栏置空. player Slot置空
        ShowNumber();
    }

    // 显示装备标记
    public void SetEquipedMark()
    {
        if (itemNum == 1)
        {
            itemMark.text = "E";
        }
        if (itemNum > 1)
        {
            itemMark.text = itemNum + " E";
        }
    }

    // 拖拽, 到使用栏...  暂时不用.

    // 根据id设置物品属性
    public void SetItemProperties(int id)
    {
        // 从一个库中读取数据

        // 遍历查找对应id的数据

        // 设置iteminfo数值, 并刷新
    }
}
