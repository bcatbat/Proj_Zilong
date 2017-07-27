using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryControl : MonoBehaviour {

    public GameObject gridPrefab;
    public GameObject scrollList;
    List<GameObject> grids;
    List<ItemGrid> items;

    private void Awake()
    {
        grids = new List<GameObject>();
        items = new List<ItemGrid>();
    }

    private void Start()
    {
        // TODO: deleted,测试用
        for (int i = 0; i < 10; i++)
        {
            var grid = Instantiate(gridPrefab, scrollList.transform);
            grids.Add(grid);

            var item = grid.GetComponent<ItemGrid>();
            items.Add(item);

            item.itemDes = "这是测试物品ID" + i;
            item.itemNum = i;

            item.ShowNumber();
        }
    }

    // 物品数量为0时,清理物品.
    private void CheckEmpty()
    {
       
    }

    // 在背包里面减少一个物品.


    // 在背包里面添加一个物品(图标)


    // 查找某一id的物品


    // 排序功能


    // 只显示weapon功能


    // 只显示armor功能


    // 只显示药品功能


    // 只显示素材功能


    // 全部显示
}
