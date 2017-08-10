using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryControl : MonoBehaviour {

    public GameObject itemPrefabs;      // 物品格子的Prefab
    public GameObject itemScrollList;   // 物品滚动列表
    public Image choiceFrame;           // 选择框. 默认不显示
    public List<Grid> items;            // 物品

    private static InventoryControl instance;   // 单例之
    private List<GameObject> grids;             // 物品格子列表
    private Grid currentItem;                   // 当前选择的物品

    //private bool isShowAllItems = true;        // 是否正在显示所有物品


    // 单例之
    public static InventoryControl Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
        grids = new List<GameObject>();
        items = new List<Grid>();
    }

    private void Start()
    {
        // TODO: deleted,测试用
        //CreateTestItems();
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
        // 如果item在物品list中, 则设置currentItem
        if (items.Contains(item))
        {
            currentItem = item;
            SetChoiceFramePosition();
        }
        else // 否则提示错误
        {
            Debug.Log("该物品不在背包中..");
        }
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

    // TODO: 测试用, 随机生成若干物品
   /* public void CreateTestItems()
    {
        // TODO: deleted,测试用
        for (int i = 0; i < 10; i++)
        {
            var grid = Instantiate(itemPrefabs, itemScrollList.transform);            
            grids.Add(grid);

            // 改一下颜色
            var image = grid.GetComponent<Image>();            
            image.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f),Random.Range(0,1f));

            var item = grid.GetComponent<Grid>();

            item.item.itemType = (ItemType)Random.Range(0, 7);

            switch (item.item.itemType)
            {
                case ItemType.weapon:
                    item.item = new WeaponItem();
                    break;
                case ItemType.armor:
                    item.item = new ArmorItem();
                    break;
                case ItemType.trinket:
                    item.item = new TrinketItem();
                    break;
                case ItemType.consumable:
                    item.item = new ConsumableItem();
                    item.itemNum = Random.Range(1, 10);
                    break;
                case ItemType.tonic:
                    item.item = new TonicItem();
                    item.itemNum = Random.Range(1, 10);
                    break;
                case ItemType.material:
                    item.item = new MaterialItem();
                    item.itemNum = Random.Range(1, 10);
                    break;
                case ItemType.task:
                    item.item = new TaskItem();
                    break;
                default:
                    throw new System.Exception("出现未知类型");
            }

            item.item.itemID = Random.Range(1, 100);

            grid.name = item.item.GetType() + " " + item.item.itemID;

            item.item.itemIcon = image; // todo:delet

            items.Add(item);

            item.ShowNumber();          
            
            item.item.itemDes = "类型: " + item.item.itemType.ToString()+" ID:"+item.item.itemID;
        }
    }
    */
}
