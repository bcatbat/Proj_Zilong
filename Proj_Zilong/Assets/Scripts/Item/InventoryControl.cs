using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryControl : MonoBehaviour {

    public GameObject itemPrefabs;      // 物品格子的Prefab
    public GameObject itemScrollList;   // 物品滚动列表
    List<GameObject> grids;             // 物品格子列表
    //List<Item> items;                   // 物品

    private void Awake()
    {
        grids = new List<GameObject>();
      //  items = new List<Item>();
    }

    private void Start()
    {
        // TODO: deleted,测试用
        for (int i = 0; i < 10; i++)
        {
            var grid = Instantiate(itemPrefabs, itemScrollList.transform);
            grids.Add(grid);
            
            var item = grid.GetComponent<Item>();
           // items.Add(item);

            item.itemDes = "这是测试物品ID"+i;
            item.itemNum = Random.Range(0,3);

            item.ShowNumber();
        }
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
        // TODO: 这是实验内容 . 记着有空判断
        Destroy(grids[0]);
        grids.RemoveAt(0);
        Debug.Log(grids[0].GetComponent<Item>().itemNum);
    }


    // 在背包里面添加一个物品(图标)
    public void AddItem()
    {
        // TODO: 这是试验内容.
        var grid = Instantiate(itemPrefabs, itemScrollList.transform);
        grids.Add(grid);
    }


    // 查找某一id的物品


    // 排序功能


    // 只显示weapon功能


    // 只显示armor功能


    // 只显示药品功能


    // 只显示素材功能


    // 全部显示
}
