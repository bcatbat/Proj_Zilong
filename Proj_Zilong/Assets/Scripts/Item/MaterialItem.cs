using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialItem : ItemInfo {
    public string testName;

    public MaterialItem()
    {
        testName = "Material";
        itemType = ItemType.material;
    }

    // 其他的一些特殊效果
}
