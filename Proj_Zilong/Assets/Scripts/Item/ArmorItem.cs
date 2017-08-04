using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 护甲物品类, 提供属性加成
/// </summary>
public class ArmorItem : ItemInfo {
    public string testName;
    public Stats AttachStats;    // 附加属性

    // 其他的一些特殊效果: 如自动回血, 自动回蓝, 重生技能, 无敌技能...等

    public ArmorItem()
    {
        testName = "Armor";
        itemType = ItemType.armor;
    }
}
