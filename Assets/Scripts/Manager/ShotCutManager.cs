using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotCutManager : MonoBehaviour {
    // 单例一下
    private static ShotCutManager instance;
    public static ShotCutManager Instance
    {
        get { return instance; }
    }

    public List<ShotcutGrid> shotcuts;         // 数量固定8个的快捷栏
    public List<Item> items;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        DontDestroyOnLoad(this);
        
        items = new List<Item>(8);
    }

    private void Start()
    {
        Empty();
    }

    public bool Contains(Item item)
    {
        foreach(var i in items)
        {
            if(item == i)
            {
                return true;
            }            
        }
        return false;
    }

    public void Empty()
    {
        items.Clear();
        foreach(ShotcutGrid g in shotcuts)
        {
            items.Add(g.item);
        }
    }
    public Item this[int index]
    {
        get
        {
            return items[index];
        }
        set
        {
            items[index] = value;
        }
    }

    // 查重
    public void CheckDuplication(Item item,int index)
    {        
        for (int i = 0; i < 8; i++)
        {
            if(items[i]== item && i !=index && item.itemID!=0)
            {
                Debug.Log("找到重复了 " + i);                
                items[i] = new Item();
                shotcuts[i].item = items[i];    
                shotcuts[i].Refresh();
            }
        }
    }
}
