using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGrid : MonoBehaviour {    
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

    // 显示数量标记
    public void ShowNumber()
    {
        if(itemNum <= 0)
        {
            //清空..
        }
        if(itemNum == 1)
        {
            itemMark.text = "";   // 数量为1时, 不显示数字;
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
}
