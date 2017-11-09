using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public static class ItemDataProcessor{
    #region ItemProcessor        
    private static XElement itemList;   // 物品库缓存

    private static AssetBundle itemSpriteAssetBundle;   // 物品Sprite资源包

    // 读取Item,根据xml文件
    public static bool ReadItemData()
    {
        // 检索文件是否存在        
        string itemPath = Application.streamingAssetsPath + "/itemListXml.xml";

        itemList = XElement.Load(itemPath);
        if(itemList == null)
        {
            Debug.LogError("未找到物品文件");
            return false;
        }        
        InitItemSpriteAssetBundle();
        return true;
    }

    // 读取物品,根据id. 扩展方法
    public static Item GetItemByID(this int id)
    {
        // 找到库中对应项
        if (itemList == null)   // 未读取列表
        {
            //Debug.LogError("未读取列表");
            // return null;
            ReadItemData();
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
            //var tarItemIcon = LoadImageFromJpgFiles(id, (string)it.Element("itemName"));
            var tarItemIcon = LoadSpriteFromAssetBundle(id, (string)it.Element("itemName"));

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

    // 读取sprite, 从资源包中,根据id+name
    private static Sprite LoadSpriteFromAssetBundle(int id, string name)
    {
        string fileName = id + "-" + name;
        if (itemSpriteAssetBundle == null)
        {
            Debug.LogError("item sprite asset bundle not found!!");
            return null;
        }
        var loadedSprite = itemSpriteAssetBundle.LoadAsset<Sprite>(fileName);
        return loadedSprite;
    }

    // 初始化物品图片的资源包
    private static void InitItemSpriteAssetBundle()
    {
        if (itemSpriteAssetBundle == null)
        {
            itemSpriteAssetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/itemjpg");
        }
    }

    #endregion
}
