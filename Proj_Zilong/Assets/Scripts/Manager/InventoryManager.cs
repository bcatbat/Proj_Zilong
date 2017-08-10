using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    // 单例一下
    private static InventoryManager instance;
    public static InventoryManager Instance
    {
        get { return instance; }
    }

    public Dictionary<Item, int> inventory;     // 背包键值对. 键-物品,值-数量

    private void Awake()
    {
        if (instance == null)
            instance = this;
        DontDestroyOnLoad(this);
    }

    // 索引器
    public int this[Item index]
    {
        get { return inventory[index]; }
        set { inventory[index] = value; }
    }
    // 包裹总数
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

    // 增加一个物品
    public void AddItem(Item item)
    {
        if (item == null) return;
        if (inventory.ContainsKey(item))
        {
            inventory[item]++;
        }
        else
        {
            inventory.Add(item, 1);
        }
    }

    // 消耗了一个物品(吃了一个...)
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
    }

    // 移除现有物品. 整个移除
    public void Remove(Item item)
    {
        if (inventory[item] < 0)
        {
            Debug.LogError("错误:物品数量不能为负");
        }
        inventory.Remove(item);
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

    // 返回一个纯装备表
    public Dictionary<EquipmentItem,int> OnlyEquipmentDict()
    {
        Dictionary<EquipmentItem, int> equipmentDict = new Dictionary<EquipmentItem, int>();
        foreach(KeyValuePair<Item,int> pair in inventory)
        {
            if(pair.Key.GetType() == typeof(EquipmentItem))
            {
                equipmentDict.Add(pair.Key as EquipmentItem, pair.Value);
            }
        }
        return equipmentDict;
    }   
}
