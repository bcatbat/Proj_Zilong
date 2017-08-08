using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void DescriptionFrameChangedDelegate();

public class Item : MonoBehaviour {    
    
    public ItemInfo itemInfo;  // 物品的信息
    public int itemNum = 1;    // 物品的数量(默认数量)

    private Image itemIcon;     // 物品的图标
    //private string itemDes;     // 物品的描述
    private Text itemMark;      // 角标

    private GameObject draggingIcon;    // 拖拽图标
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
        eventListener.OnBeginDrag += EventListener_OnBeginDrag;
        eventListener.OnDrag += EventListener_OnDrag;
        eventListener.OnEndDrag += EventListener_OnEndDrag;

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

    private void EventListener_OnEndDrag(GameObject gb)
    {
        // 销毁
        if (draggingIcon != null)
            Destroy(draggingIcon);
        // 如果目标是匹配的装备栏. 则装备

        // 如果是腰带. 则放到腰带上.
    }

    private void EventListener_OnDrag(GameObject gb)
    {
        // 图标跟随
        if (draggingIcon != null)
            SetDraggedPosition();
    }

    private void EventListener_OnBeginDrag(GameObject gb)
    {
        // 找到画布
        var canvas = FindInParent<Canvas>(gameObject);
        if (canvas == null) return;

        // 新建一个图标
        draggingIcon = new GameObject("Dragging Icon");
        draggingIcon.transform.SetParent(canvas.transform, false);
        draggingIcon.transform.SetAsLastSibling();  // 放到最后一项, 最上方显示

        var image = draggingIcon.AddComponent<Image>();
        var curImage = GetComponent<Image>();
        image.sprite = curImage.sprite;
        image.color = curImage.color;
        //image.SetNativeSize();       

    }

    private void SetDraggedPosition()
    {
        // 跟随鼠标
        draggingIcon.transform.position = Input.mousePosition;
    }

    private static T FindInParent<T>(GameObject go) where T : Component
    {
        // 目标空,返回空
        if (go == null) return null;

        // 目标身上已有T, 则返回T
        var component = go.GetComponent<T>();
        if (component != null) return component;

        // 循环找父项
        Transform t = go.transform.parent;
        // 循环条件. 父项存在, T不存在
        while(t!=null && component == null)
        {
            component = t.gameObject.GetComponent<T>();
            t = t.parent;
        }
        return component;
    }

    // 鼠标划出
    private void EventListener_OnMouseExit(GameObject gb)
    {        
        HideDescription();        
    }

    // 鼠标划入
    private void EventListener_OnMouseEnter(GameObject gb)
    {
        // 仅在非空物品上显示
        if (itemInfo.itemID != 0)
        {
            ShowDescription();
        }
    }

    // 右键点击
    private void EventListener_OnMouseRightClick(GameObject gb)
    {
        if (tag == "Inventory")
        {
            //Debug.Log(gb.name+"右键点击");
            // 选框
            InventoryControl.Instance.SetCurrentItem(this);

            // 使用物品
            if (itemInfo.itemType == ItemType.consumable || itemInfo.itemType == ItemType.tonic)
            {
                UseItem();
            }

            // 装备武器
            if (itemInfo.itemType == ItemType.weapon)
            {
                SetupWeapon();
            }

            // 装备护甲
            if (itemInfo.itemType == ItemType.armor)
            {
                SetupArmor();
            }

            // 装备饰品
            if (itemInfo.itemType == ItemType.trinket)
            {
                SetupTrinket();
            }
        }
        if(tag == "Equipment")
        {
            // Debug.Log("这是装备栏 - " + itemInfo.GetType());
            // 卸下当前武器
            if (itemInfo.itemType == ItemType.weapon)
            {
                EquipmentControl.Instance.UnloadWeapon(itemInfo as WeaponItem);
            }

            // 卸下当前护甲
            if (itemInfo.itemType == ItemType.armor)
            {
                EquipmentControl.Instance.UnloadArmor(itemInfo as ArmorItem);
            }

            // 卸下当前饰品
            if (itemInfo.itemType == ItemType.trinket)
            {
                EquipmentControl.Instance.UnloadTrinket(itemInfo as TrinketItem);
            }
        }
    }

    // 左键点击, 选中(或可以与OnCover合并)
    private void EventListener_OnMouseLeftClick(GameObject gb)
    {        
        if (gameObject.tag == "Inventory")
        {
            //Debug.Log(gb.name+"左键点击!");
            InventoryControl.Instance.SetCurrentItem(this);
        }
    }

    // 显示数量标记. 非装备品可调用
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
    private void ShowDescription()
    {        
        DescriptionCtrl.Instance.SetText(itemInfo.itemDes);
        DescriptionCtrl.Instance.Show(itemIcon);
    }

    // 隐藏描述框
    private void HideDescription()
    {
        DescriptionCtrl.Instance.Hide();
    }

    // 清空格子


    // 使用物品
    private void UseItem()
    {
        // 右键点击触发. 物品数量减1,物品起作用->敌我等.
        Debug.Log("eat");
        itemNum--;
        ShowNumber();
    }

    // 装备武器
    private void SetupWeapon()
    {       
        // 武器
        if (itemInfo.GetType() == typeof(WeaponItem))
        {
            WeaponItem weapon = EquipmentControl.Instance.weaponItem as WeaponItem;

            // 武器槽中是当前物品:卸下
            if(weapon == itemInfo)
            {
                //Debug.Log("卸下武器!");
                EquipmentControl.Instance.UnloadWeapon(itemInfo as WeaponItem);
                //WipeEquipedMark();
            }
            else // 否则,武器槽中物品替换为当前物品
            {
                //Debug.Log("装备武器!");
                EquipmentControl.Instance.EquipWeapon(itemInfo as WeaponItem);
                SetupEquipedMark();
            }            
        }
    }

    // 装备护甲
    private void SetupArmor()
    {
        // 护甲
        if (itemInfo.GetType() == typeof(ArmorItem))
        {
            ArmorItem armor = EquipmentControl.Instance.armorItem as ArmorItem;

            if(armor == itemInfo)
            {
                //Debug.Log("卸下护甲");
                EquipmentControl.Instance.UnloadArmor(itemInfo as ArmorItem);
                //WipeEquipedMark();
            }
            else
            {
                //Debug.Log("装备护甲!");
                EquipmentControl.Instance.EquipArmor(itemInfo as ArmorItem);
                SetupEquipedMark();
            }            
        }
    }

    // 装备饰品
    private void SetupTrinket()
    {
        // 饰品
        if (itemInfo.itemType == ItemType.trinket)
        {
            if(EquipmentControl.Instance.IsTrinketEquiped(itemInfo as TrinketItem))
            {
                // 装备栏中已有该物品
                //Debug.Log("卸下饰品");
                EquipmentControl.Instance.UnloadTrinket(itemInfo as TrinketItem);
                //WipeEquipedMark();
            }
            else
            {
                //Debug.Log("装备饰品");
                EquipmentControl.Instance.EquipTrinket(itemInfo as TrinketItem);
                SetupEquipedMark();
            }
        }
    }

    // 显示装备标记
    public void SetupEquipedMark()
    {
        if (itemNum == 1)
        {
            itemMark.text = "E";
        }else if (itemNum > 1)
        {
            Debug.LogError("错误:装备物品无法堆叠!!");
        }else
        {
            Debug.LogError("错误:物品不存在!!");
        }
    }

    // 擦去装备标记
    public void WipeEquipedMark()
    {
        if (itemNum == 1)
        {
            itemMark.text = "";
        }
        else if (itemNum > 1)
        {
            Debug.LogError("错误:装备物品无法堆叠!!");
        }
        else
        {
            Debug.LogError("错误:物品不存在!!");
        }
    }

    // 拖拽, 到使用栏...  暂时不用.

    // 根据id设置物品属性
    public void ReadItemInformation(int id)
    {
        // 从一个库中读取数据

        // 遍历查找对应id的数据

        // 设置iteminfo数值, 并刷新
    }
}
