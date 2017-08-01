using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {    
    private ItemInfo itemInfo;  // 物品的信息

    private Image itemIcon;     // 物品的图标
    public string itemDes;     // 物品的描述

    private Text itemMark;      // 角标
    public int itemNum = 1;    // 物品的数量

    // 初始化
    private void Awake()
    {
        itemIcon = GetComponent<Image>();
        itemMark = GetComponentInChildren<Text>();        
    }

    private void Start()
    {
        if (itemMark != null)
        {
            Debug.Log("显示在背包中");
            ShowNumber();
        }
        else
        {
            Debug.Log("未显示在背包中");
        }
    }

    // 显示数量标记
    public void ShowNumber()
    {
        if(itemNum <= 0)
        {
            //清空..
            itemMark.text = "null";
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
        DescriptionCtrl.Instance.SetText(itemDes);
        DescriptionCtrl.Instance.Show(itemIcon);
    }

    // 隐藏描述框
    public  void HideDescription()
    {
        DescriptionCtrl.Instance.Hide();
    }

    // 显示装备标记
    public void SetEquipedMark()
    {
        if(itemNum == 1)
        {
            itemMark.text = "E";
        }
        if(itemNum > 1)
        {
            itemMark.text = "E "+itemNum;
        }
    }

    // 清空格子
    


    // 使用物品
    public void UseItem()
    {
        
    }

    // 装上
    public void Equip()
    {

    }

    // 卸下
    public void UnEquip()
    {

    }

    // 拖拽, 到使用栏... 不用


    // 右键点击, 使用


    // 左键点击, 选中(或可以与OnCover合并)

}
