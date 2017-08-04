using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryControl : MonoBehaviour {

    public GameObject itemPrefabs;      // 物品格子的Prefab
    public GameObject itemScrollList;   // 物品滚动列表

    private static InventoryControl instance;   // 单例之
    private List<GameObject> grids;             // 物品格子列表
    private List<Item> items;                   // 物品

    //private bool isShowAllItems = true;        // 是否正在显示所有物品

    public static InventoryControl Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
        grids = new List<GameObject>();
        items = new List<Item>();
    }

    private void Start()
    {
        // TODO: deleted,测试用
        CreateTestItems();
    }

    // 物品数量为0时,清理物品.
    public void CheckEmpty()
    {
        // 遍历查找空物品数量
        int emptyItemNum = 0;        

        foreach (var grid in grids)
        {
            var item = grid.GetComponent<Item>();
            if (item.itemNum < 0)
            {
                Debug.Log("负的数量,错误");
            }
            if (item.itemNum == 0)
            {
                emptyItemNum++;
            }
        }

        // 移除. 
        for(int i = 0; i < emptyItemNum; i++)
        {
            foreach(var grid in grids)
            {
                var item = grid.GetComponent<Item>();
                if(item.itemNum == 0)
                {
                    grids.Remove(grid);
                    items.Remove(item);
                    Destroy(grid);
                    break;
                }
            }
        }
        
        // 刷新        
    }

    // 在背包里面减少一个物品.
    public void RemoveItem()
    {
        // TODO: 这是实验内容 . 记着有空判断, 按照id减少物品
        Destroy(grids[0]);
        grids.RemoveAt(0);
        Debug.Log(grids[0].GetComponent<Item>().itemNum);
    }
    
    // 在背包里面添加一个物品(图标)
    public void AddItem()
    {
        // TODO: 这是试验内容. 未来补充: 按照id添加物品
        var grid = Instantiate(itemPrefabs, itemScrollList.transform);        
        grids.Add(grid);
    }


    // 查找某一id的物品


    // 排序功能, 物品按照id排序.


    // 筛选:只显示装备
    public void ShowEquipmentOnly()
    {
        foreach (var grid in grids)
        {
            var item = grid.GetComponent<Item>();
            //if (item.itemInfo.itemType == ItemType.weapon || item.itemInfo.itemType == ItemType.armor || item.itemInfo.itemType == ItemType.trinket)
            //{
            //    grid.SetActive(true);
            //}
            if(item.itemInfo.GetType() == typeof(WeaponItem)|| item.itemInfo.GetType() == typeof(ArmorItem)|| item.itemInfo.GetType() == typeof(TrinketItem))
            {
                grid.SetActive(true);
            }
            else
            {
                grid.SetActive(false);
            }
        }        
    } 

    // 筛选:只显示药品
    public void ShowConsumptionOnly()
    {
        foreach (var grid in grids)
        {
            var item = grid.GetComponent<Item>();
            if (item.itemInfo.itemType == ItemType.consumable || item.itemInfo.itemType == ItemType.tonic)
            {
                grid.SetActive(true);
            }
            else
            {
                grid.SetActive(false);
            }
        }        
    }

    // 筛选:只显示素材
    public void ShowMaterialOnly()
    {

        foreach (var grid in grids)
        {
            var item = grid.GetComponent<Item>();
            if (item.itemInfo.itemType == ItemType.material)
            {
                grid.SetActive(true);
            }
            else
            {
                grid.SetActive(false);
            }
        }        
    }

    // 筛选:只显示任务品
    public void ShowTastItemOnly()
    {

        foreach (var grid in grids)
        {
            var item = grid.GetComponent<Item>();
            if (item.itemInfo.itemType == ItemType.task)
            {
                grid.SetActive(true);
            }
            else
            {
                grid.SetActive(false);
            }
        }        
    }

    // 全部显示
    public void ShowAllItems()
    {
        foreach(var grid in grids)
        {
            grid.SetActive(true);
        }
    }

    // TODO: 测试用, 随机生成若干物品
    public void CreateTestItems()
    {
        // TODO: deleted,测试用
        for (int i = 0; i < 10; i++)
        {
            var grid = Instantiate(itemPrefabs, itemScrollList.transform);
            grids.Add(grid);

            var item = grid.GetComponent<Item>();
            items.Add(item);            

            item.itemNum = Random.Range(1, 10);

            item.ShowNumber();

            item.itemInfo.itemType = (ItemType)Random.Range(0, 7);

            switch (item.itemInfo.itemType)
            {
                case ItemType.weapon:
                    item.itemInfo = new WeaponItem();
                    break;
                case ItemType.armor:
                    item.itemInfo = new ArmorItem();
                    break;
                case ItemType.trinket:
                    item.itemInfo = new TrinketItem();
                    break;
                case ItemType.consumable:
                    item.itemInfo = new ConsumableItem();
                    break;
                case ItemType.tonic:
                    item.itemInfo = new TonicItem();
                    break;
                case ItemType.material:
                    item.itemInfo = new MaterialItem();
                    break;
                case ItemType.task:
                    item.itemInfo = new TaskItem();
                    break;
            }           

            item.itemInfo.itemDes = "类型: " + item.itemInfo.itemType.ToString();
        }
    }
}
