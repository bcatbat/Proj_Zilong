﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelControl : MonoBehaviour {

    public GameObject gridPrefab;                 // 物品格子的Prefab
    public GameObject scrollList;           // 物品滚动列表
    public Image choiceFrame;               // 选择框. 默认不显示
    public List<InventoryGrid> grids;       // 物品列表    
    
    private Grid currentItem;                   // 当前选择的物品
    //private bool isShowAllItems = true;        // 是否正在显示所有物品        

    private void Awake()
    {
        grids = new List<InventoryGrid>();
    }

    private void Start()
    {
        InventoryManager.Instance.OnItemChanged += Inventory_OnItemChanged;        
    }

    private void Inventory_OnItemChanged()
    {
        Debug.Log("背包信息变化, 检查刷新");
    }

    // 检查是否变化, true=无变化, false= 有变化.
    private bool CheckInventory()
    {
        if (grids.Count != InventoryManager.Instance.Count)
            return false;
        else
        {
            for(int i = 0; i < grids.Count; i++)
            {
                //
            }
        }
        return false;
    }

    // 重载背包
    private void ReloadInventory()
    {
        
    }

    

    // 添加一个物品
    private void AddItem()
    {
        var newObj = Instantiate<GameObject>(gridPrefab, scrollList.transform);
        InventoryGrid grid = newObj.AddComponent<InventoryGrid>();

    }

   /* #region filter function
    public void ShowEquipmentOnly()
    {
        foreach (var grid in grids)
        {
            var item = grid.GetComponent<Grid>();
            //if (item.itemInfo.itemType == ItemType.weapon || item.itemInfo.itemType == ItemType.armor || item.itemInfo.itemType == ItemType.trinket)
            //{
            //    grid.SetActive(true);
            //}
            if(item.item.GetType() == typeof(WeaponItem)|| item.item.GetType() == typeof(ArmorItem)|| item.item.GetType() == typeof(TrinketItem))
            {
                grid.SetActive(true);
            }
            else
            {
                grid.SetActive(false);
            }
        }
        HideChoiceFrame();
    } 

    // 筛选:只显示药品
    public void ShowConsumptionOnly()
    {
        foreach (var grid in grids)
        {
            var item = grid.GetComponent<Grid>();
            if (item.item.itemType == ItemType.consumable || item.item.itemType == ItemType.tonic)
            {
                grid.SetActive(true);
            }
            else
            {
                grid.SetActive(false);
            }
        }
        HideChoiceFrame();
    }

    // 筛选:只显示素材
    public void ShowMaterialOnly()
    {

        foreach (var grid in grids)
        {
            var item = grid.GetComponent<Grid>();
            if (item.item.itemType == ItemType.material)
            {
                grid.SetActive(true);
            }
            else
            {
                grid.SetActive(false);
            }
        }
        HideChoiceFrame();
    }

    // 筛选:只显示任务品
    public void ShowTastItemOnly()
    {

        foreach (var grid in grids)
        {
            var item = grid.GetComponent<Grid>();
            if (item.item.itemType == ItemType.task)
            {
                grid.SetActive(true);
            }
            else
            {
                grid.SetActive(false);
            }
        }
        HideChoiceFrame();
    }

    // 筛选:全部显示
    public void ShowAllItems()
    {
        foreach(var grid in grids)
        {
            grid.SetActive(true);
        }
        HideChoiceFrame();
    }
    #endregion*/

    // todo:设置当前选择的物品
    public void SetCurrentItem(Grid item)
    {
        //// 如果item在物品list中, 则设置currentItem
        //if (items.Contains(item))
        //{
        //    currentItem = item;
        //    SetChoiceFramePosition();
        //}
        //else // 否则提示错误
        //{
        //    Debug.Log("该物品不在背包中..");
        //}
    }

    // todo:移动选择框到当前物品上面
    private void SetChoiceFramePosition()
    {
        if(currentItem != null)
        {
            Vector3 curItemPos = currentItem.gameObject.transform.position;
            choiceFrame.transform.position = curItemPos;
            ShowChoiceFrame();
        }
    }

    // todo;显示选框
    private void ShowChoiceFrame()
    {
        choiceFrame.gameObject.SetActive(true);
    }

    // todo:隐藏选框
    public void HideChoiceFrame()
    {
        choiceFrame.gameObject.SetActive(false);
    }

}