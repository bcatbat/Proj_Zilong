using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public static class XMLReader { }


public static class JsonModel{
    private static ItemListJsonModel itemList;
    private static BuffListJsonModel buffList;

    public static void InitRawData()
    {
        string itemsJson = File.ReadAllText(Application.dataPath + "/RawData/" + "itemlist.json");
        itemList = JsonUtility.FromJson<ItemListJsonModel>(itemsJson);

        string buffsJson = File.ReadAllText(Application.dataPath + "/RawData/" + "bufflist.json");
        buffList = JsonUtility.FromJson<BuffListJsonModel>(buffsJson);

        Debug.Log("rawdata loaded");
    }

    public static Buff GetBuffByID(int id)
    {
        return default(Buff);
    }

    public static Item GetItemByID(int id)
    {
        return default(Item);
    }
}

[Serializable]
public class ItemJsonModel
{
    public int itemID;          
    public string itemName;     
    public ItemType itemType;   

    public int buffID;          // buff, 消耗品有

    public string specialEffect;    // special描述, 部分有
    public string description;

    public StatsJsonModel growthStats;    // 补品有
    public StatsJsonModel attachStats;     // 武器有
}

[Serializable]
public class ItemListJsonModel
{
    public List<ItemJsonModel> itemList;
}

[Serializable]
public class StatsJsonModel
{
    public int UpHp;
    public int UpMp;
    public int Atk;
    public int Def;
    public int Spd;
    public int Luk;
}

[Serializable]
public class BuffJsonModel
{
    public int buffID;
    public string buffName;
    public float buffDuration;
    public List<EffectJsonModel> buffEffects;
    public string buffDescription;
}

[Serializable]
public class BuffListJsonModel
{
    public List<BuffJsonModel> buffList;
}

[Serializable]
public class EffectJsonModel
{
    public string effectType;
    public int effectValue;
}


