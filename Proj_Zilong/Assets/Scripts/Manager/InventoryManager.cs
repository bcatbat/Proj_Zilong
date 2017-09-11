using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 监听背包变化事件


public class InventoryManager : MonoBehaviour {
    // 单例一下
    private static InventoryManager instance;
    public static InventoryManager Instance
    {
        get { return instance; }
    }

    public Dictionary<Item, int> inventory;     // 背包键值对. 键-物品,值-数量


    // todo:背包事件. 当IM增加,减小,销毁,排序时触发. 通知IP刷新
    public delegate void InventoryChanged();
    public event InventoryChanged OnItemChanged;    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        DontDestroyOnLoad(this);

        inventory = new Dictionary<Item, int>();

        XmlDataProcessor.ReadItemData();
    }

    private void Start()
    {
        CreateTestBags();
    }

    // 索引器
    public int this[Item index]
    {
        get { return inventory[index]; }
        set { inventory[index] = value; }
    }

    // 根据物品id,返回当前背包内的该物品
    public Item GetItemByID(int id)
    {
        foreach(var pair in inventory)
        {
            if(pair.Key.itemID == id)
            {
                return pair.Key;
            }
        }
        return null;
    }

    // 根据物品id, 返回物品数量
    public int GetNumByID(int id)
    {
        Item obj = GetItemByID(id);
        if (obj == null)
            return 0;
        else
            return inventory[obj];
    }

    // 包裹物品总数
    public int Count
    {
        get { return inventory.Count; }
    }

    // 排序
    public void SortbyID()
    {
        List<KeyValuePair<Item, int>> baglist = new List<KeyValuePair<Item, int>>(inventory);
        baglist.Sort(
            delegate(KeyValuePair<Item, int> p1, KeyValuePair<Item, int> p2){
                return p1.Key.CompareTo(p2.Key);
        });
        inventory.Clear();
        foreach(KeyValuePair<Item, int> pair in baglist)
        {
            inventory.Add(pair.Key, pair.Value);
        }
    }

    public void Refresh()
    {
        // 监听事件
        if (OnItemChanged != null)
            OnItemChanged();
    }

    // todo:增加一个物品...装备类不堆叠. 药物材料任务品均可堆叠. 按照item id来进行区分.
    public void AddItem(Item item)
    {
        if (item == null) return;

        if(item.itemType == ItemType.armor ||
            item.itemType == ItemType.weapon||
            item.itemType == ItemType.trinket)
        {
            // 装备类, 不可堆叠, 新添加一项
            inventory.Add(item, 1);
        }
        else
        {
            // 其他类别,需要堆叠
            if (ContainsItem(item))
            {
                // 如果包内有
                Item existItem = GetItemByID(item.itemID);
                inventory[existItem]++;
            }
            else
            {
                // 如果包内无
                inventory.Add(item, 1);
            }
        }
        // 监听事件
        if (OnItemChanged != null)
            OnItemChanged();
    }

    // todo:消耗了一个物品(吃了一个...). 缺少判定
    public void ConsumeItem(Item item)
    {
        if (item == null) return;

        if (inventory.ContainsKey(item))
        {
            if (inventory[item] > 1)
            {
                inventory[item]--;
            }
            else
            {
                Remove(item);
            }
            
        }
        else
        {
            Debug.Log("不存在该物品..");
        }

        // 监听背包变化事件
        if (OnItemChanged != null)
            OnItemChanged();
    }

    // 移除现有物品. 整个移除
    public void Remove(Item item)
    {
        if (inventory[item] < 0)
        {
            Debug.LogError("错误:物品数量不能为负");
        }
        inventory.Remove(item);

        // 监听背包变化事件
        if (OnItemChanged != null)
            OnItemChanged();
    }

    // 是否存在相同物品
    public bool ContainsItem(Item item)
    {
        foreach(var pair in inventory)
        {
            if(pair.Key.itemID == item.itemID)
            {
                return true;
            }
        }
        return false;
    }

    // 清理空物品,无效物品
    public void CheckEmpty()
    {
        int emptyItemNum = 0;

        foreach (var pair in inventory)
        {
            if(pair.Value < 0)
            {
                Debug.LogError("数量为负, 错误!!");
            }
            if (pair.Value == 0)
                emptyItemNum++;
        }

        for(int i = 0; i < emptyItemNum; i++)
        {
            foreach(var pair in inventory)
            {
                if(pair.Value == 0)
                {
                    inventory.Remove(pair.Key);
                    break;
                }
            }
        }
    }

    // 返回一个纯表.注:仅仅适用于子类
    public Dictionary<T,int> SingleDict<T>() where T:Item
    {
        Dictionary<T, int> dict = new Dictionary<T, int>();
        foreach(var pair in inventory)
        {
            if(pair.Key.GetType() == typeof(T))
            {
                dict.Add(pair.Key as T, pair.Value);
            }
        }
        return dict;
    }

    // todo:返回一个纯装备表.
    public Dictionary<EquipmentItem,int> OnlyEquipmentDict()
    {
        Dictionary<EquipmentItem, int> equipmentDict = new Dictionary<EquipmentItem, int>();
        foreach(KeyValuePair<Item,int> pair in inventory)
        {
            //if(pair.Key.GetType() == typeof(EquipmentItem))// 无法达成目标
            if(pair.Key.GetType() == typeof(WeaponItem) ||
                pair.Key.GetType() == typeof(ArmorItem)||
                pair.Key.GetType() == typeof(TrinketItem))
            {
                equipmentDict.Add(pair.Key as EquipmentItem, pair.Value);
            }
        }
        return equipmentDict;
    }   

    // todo: [d]初始化一个测试背包.包含各种物品3个..
    public void CreateTestBags()
    {
        for (int i = 0; i < 3; i++)
        {
            //Item c = new ConsumableItem(); c.itemID = 1;
            //Item c2 = new ConsumableItem(); c2.itemID = 11;
            //Item to = new TonicItem(); to.itemID = 2;
            //Item m = new MaterialItem();m.itemID = 3;
            //Item ta = new TaskItem();ta.itemID = 4;

            //Item w = new WeaponItem(); w.itemID = 5;
            //Item a = new ArmorItem();a.itemID = 6;
            //Item tr = new TrinketItem();tr.itemID = 7;

            //AddItem(c);
            //AddItem(c2);
            //AddItem(to);
            //AddItem(m);
            //AddItem(ta);
            //AddItem(w);
            //AddItem(a);
            //AddItem(tr);

            // 消耗品3样
            Item c1 = XmlDataProcessor.GetItemByID(2);AddItem(c1);
            Item c2 = (6).GetItemByID();AddItem(c2);
            Item c3 = (9).GetItemByID();AddItem(c3);

            // 补品1样
            Item t1 = (15).GetItemByID();AddItem(t1);

            // 装备各1样
            Item e1 = (20).GetItemByID();AddItem(e1);
            Item e2 = (27).GetItemByID();AddItem(e2);
            Item e3 = (34).GetItemByID();AddItem(e3);

            // 任务品1样
        }
        Item q1 = (44).GetItemByID();AddItem(q1);

       // Debug.Log("初始化背包完毕");
        //foreach (var pair in inventory)
        //{
        //    Debug.Log(pair.Key + " " + pair.Value);
        //}
    }
}
