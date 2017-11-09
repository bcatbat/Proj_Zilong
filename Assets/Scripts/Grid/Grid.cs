using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour {
#region membership
    public Image icon;             // 图标
    public Image cdBoard;          // 冷却板
    public Text mark;              // 角标
    public Text cdTick;            // 冷却时间    
#endregion
    public GameObject draggingIcon;    // 拖拽图标

    // todo:初始化
    protected virtual void Awake()
    {
        MouseEventRegister();
        mark.text = "";
        cdTick.text = "";
    }

    // todo:
    private void Start()
    {
        
    }
    
    #region Event Listener
    protected void MouseEventRegister()
    {
        UIEventListener eventListener = gameObject.AddComponent<UIEventListener>();
        eventListener.OnMouseLeftClick += EventListener_OnMouseLeftClick;
        eventListener.OnMouseRightClick += EventListener_OnMouseRightClick;
        eventListener.OnMouseEnter += EventListener_OnMouseEnter;
        eventListener.OnMouseExit += EventListener_OnMouseExit;
        eventListener.OnMouseBeginDrag += EventListener_OnMouseBeginDrag;
        eventListener.OnMouseDrag += EventListener_OnMouseDrag;
        eventListener.OnMouseEndDrag += EventListener_OnMouseEndDrag;
        eventListener.OnMouseDrop += EventListener_OnMouseDrop;
    }

    protected virtual void EventListener_OnMouseDrop(GameObject gb)
    {
        // 默认什么都不接收.
    }

    protected virtual void EventListener_OnMouseEndDrag(GameObject gb)
    {
        // 销毁
        if (draggingIcon != null)
            Destroy(draggingIcon);
    }

    protected virtual void EventListener_OnMouseDrag(GameObject gb)
    {
        // 图标跟随
        if (draggingIcon != null)
            SetDraggedPosition();
    }

    protected virtual void EventListener_OnMouseBeginDrag(GameObject gb)
    {
        // 找到画布
        var canvas = FindInParent<Canvas>(gameObject);
        if (canvas == null) return;

        // 新建一个图标
        draggingIcon = new GameObject("Dragging Icon");
        draggingIcon.transform.SetParent(canvas.transform, false);
        draggingIcon.transform.SetAsLastSibling();  // 放到最后一项, 最上方显示

        // 拖拽图标与当前图标同步.
        var image = draggingIcon.AddComponent<Image>(); // 添加image组件
        var curImage = icon;           // 当前图标
        image.sprite = curImage.sprite;                 // 精灵
        image.color = curImage.color;                   // 颜色
        image.raycastTarget = false;                    // 射线检测关闭.以免遮挡引起drop无法检测
        //image.SetNativeSize();                        // 图片原始尺寸.
    }

    protected void SetDraggedPosition()
    {
        // 跟随鼠标
        draggingIcon.transform.position = Input.mousePosition;
    }

    protected static T FindInParent<T>(GameObject go) where T : Component
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

    protected virtual void EventListener_OnMouseExit(GameObject gb)
    {
        HideDescription();
    }

    protected virtual void EventListener_OnMouseEnter(GameObject gb)
    {
        ShowDescription();        
    }

    protected virtual void EventListener_OnMouseRightClick(GameObject gb)
    {
        Debug.Log("Grid Event: Mouse Right Click");
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

    protected virtual void EventListener_OnMouseLeftClick(GameObject gb)
    {
        Debug.Log("Grid Event: Mouse Left Click");
        //// 出现点选框.
        //if (gameObject.tag == "Inventory")
        //{
        //    //Debug.Log(gb.name+"左键点击!");
        //    InventoryControl.Instance.SetCurrentItem(this);
        //}
    }
#endregion 

    // todo:显示描述框
    protected virtual void ShowDescription()
    {
        DescriptionManager.Instance.Show(icon, "<size=50>Hello</size>");
    }

    // todo:隐藏描述框
    protected virtual void HideDescription()
    {
        DescriptionManager.Instance.Hide();
    }

    // todo: 使用物品
    public virtual void UseItem()
    {
        //// 右键点击触发. 物品数量减1,物品起作用->敌我等.
        //Debug.Log("eat: "+item.GetType() +item.itemID);
        //itemNum--;
        //if (mark != null)
        //{
        //    ShowNumber();
        //}
    }
}
