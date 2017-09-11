using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System;
using System.Reflection;

public static class XmlDataProcessor {
    private static XElement itemList;   // 物品库缓存
    private static XElement buffList;   // 增益库缓存

	// 读取Item
    public static void ReadItemData()
    {
        // 检索文件是否存在
        string itemPath = Application.dataPath + "/RawData/itemlist.xml";
        var itemFile = File.ReadAllText(itemPath);
        if (!File.Exists(itemPath))
        {
            Debug.LogError("没找到文件");
            return;
        }

        // 读取为XElement
        //itemList = new XElement(XElement.Load(itemFile));
        //itemList = XElement.Load(itemFile);
        itemList = XElement.Load(itemPath);
        Debug.Log("itemlist读取完毕");
    }

    // 读取Buff
    public static void ReadBuffData()
    {
        var buffFile = File.ReadAllText(Application.dataPath + "/RawData/bufflist.xml");
        if(buffFile == null)
        {
            Debug.LogError("Not found buffList file!");
            return;
        }

        buffList = XElement.Load(buffFile);
    }

    // 读取Skill
    public static void ReadSkillData()
    {
        
    }

    // 写入背包

    // 写入装备

    // 从id读取物品
    public static Item GetItemByID(this int id)
    {
        // 找到库中对应项
        if (itemList == null)
        {
            Debug.LogError("未读取列表");
            return null;
        }
        if (id < 0)
        {
            Debug.LogError("物品id小于0.");
            return null;
        }

        IEnumerable<XElement> queryItem = from el in itemList.Elements("item")
                                          where (int)el.Element("itemID") == id
                                          select el;
        var itemCount = queryItem.Count();
        if (itemCount < 1)
        {
            Debug.Log("未找到物品,id错误");
            return null;
        }
        else if (itemCount > 1)
        {
            Debug.Log("物品库错误, id重复");
            return null;
        }
        else
        {
            var it = queryItem.First();

            var tarItemType = (ItemType)Enum.Parse(typeof(ItemType), (string)it.Element("itemType"));


            string destype;
            int tarItemID = (int)it.Element("itemID");
            string tarItemName = (string)it.Element("itemName");
            int tarBuffID = (int)it.Element("buffID");
            var tarItemIcon = LoadImage(id, (string)it.Element("itemName"));

            Func<string, string> GetTypeDesText = (d) =>
             {
                 return "<size=20><color=blue>" + tarItemName + "</color></size>\n" +
                 "<size=15><color=green>" + d + "</color></size>\n" +
                 "<size=15><color=white>" + (string)it.Element("specialEffect") + "</color></size>" + "\n" +
                 "<size=15><color=yellow>" + "\"" + (string)it.Element("description") + "\"</color></size>";
             };
            switch (tarItemType)
            {
                case ItemType.consumable:
                    destype = "消耗品";
                    Item consumableItem = new ConsumableItem()
                    {
                        itemID = tarItemID,
                        itemName = tarItemName,
                        itemType = tarItemType,
                        buffID = tarBuffID,
                        itemIcon = tarItemIcon,
                        itemDes = GetTypeDesText(destype)
                    };
                    return consumableItem;
                case ItemType.tonic:
                    destype = "大补品";
                    Item tonicItem = new TonicItem()
                    {
                        itemID = tarItemID,
                        itemName = tarItemName,
                        itemType = tarItemType,
                        buffID = tarBuffID,
                        itemIcon = tarItemIcon,
                        itemDes = GetTypeDesText(destype)
                    };
                    return tonicItem;
                case ItemType.weapon:
                    destype = "武器";
                    Item weaponItem = new WeaponItem()
                    {
                        itemID = tarItemID,
                        itemName = tarItemName,
                        itemType = tarItemType,
                        buffID = tarBuffID,
                        itemIcon = tarItemIcon,
                        itemDes = GetTypeDesText(destype)
                    };
                    return weaponItem;
                case ItemType.armor:
                    destype = "护具";
                    Item armorItem = new ArmorItem()
                    {
                        itemID = tarItemID,
                        itemName = tarItemName,
                        itemType = tarItemType,
                        buffID = tarBuffID,
                        itemIcon = tarItemIcon,
                        itemDes = GetTypeDesText(destype)
                    };
                    return armorItem;
                case ItemType.trinket:
                    destype = "饰品";
                    Item trinketItem = new TrinketItem()
                    {
                        itemID = tarItemID,
                        itemName = tarItemName,
                        itemType = tarItemType,
                        buffID = tarBuffID,
                        itemIcon = tarItemIcon,
                        itemDes = GetTypeDesText(destype)
                    };
                    return trinketItem;
                case ItemType.material:
                    destype = "材料";
                    Item materialItem = new MaterialItem()
                    {
                        itemID = tarItemID,
                        itemName = tarItemName,
                        itemType = tarItemType,
                        buffID = tarBuffID,
                        itemIcon = tarItemIcon,
                        itemDes = GetTypeDesText(destype)
                    };
                    return materialItem;
                case ItemType.task:
                    destype = "任务物品";
                    Item taskItem = new TaskItem()
                    {
                        itemID = tarItemID,
                        itemName = tarItemName,
                        itemType = tarItemType,
                        buffID = tarBuffID,
                        itemIcon = tarItemIcon,
                        itemDes = GetTypeDesText(destype)
                    };
                    return taskItem;
                default:
                    destype = "";
                    return tonicItem = new Item();
            }
        }
    }
    
    // 读取id-name.bmp图片
    private static Sprite LoadImage(int id,string name)
    {
        var filePath = Application.dataPath +
            "/RawItemJpg/" +
            id + "-" + name + ".jpg"; // e.g. "1-包子.bmp"
        if (!File.Exists(filePath))
        {
            Debug.LogError("未找到图标");
            return null;
        }
        Debug.Log(id+"图标找到了");
        FileStream fileStream = new FileStream(filePath, FileMode.Open,FileAccess.Read);
        fileStream.Seek(0, SeekOrigin.Begin);
        byte[] imgByte = new byte[fileStream.Length];
        fileStream.Read(imgByte, 0, (int)fileStream.Length);
        fileStream.Close();
        fileStream.Dispose();
        fileStream = null;

        int width = 320; int height = 320;
        Texture2D tx = new Texture2D(width, height);
        Debug.Log(imgByte.Count());
        tx.LoadImage(imgByte);      // png/jpg格式方可, bmp格式未知错误.        
        Debug.Log(tx.format);
       
        Sprite sprite = Sprite.Create(tx, new Rect(0, 0, tx.width, tx.height), new Vector2(0.5f, 0.5f));        
        sprite.name = id + "-" + name;
        //Debug.Log("图标名为:"+sprite.name);
        return sprite;
    }  

    // 从id读取增益效果
    public static Buff GetBuffByID(int id)
    {
        Debug.Log("GetBuffByID没写");
        return default(Buff);
    }

    // 从id读取技能
    public static Skill GetSkillByID(int id)
    {
        Debug.Log("GetSkillByID没写");
        return default(Skill);
    }
}
