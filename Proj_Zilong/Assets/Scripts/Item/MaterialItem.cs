using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialItem : Item {
    public string testName;

    public MaterialItem()
    {
        testName = "Material";
        itemType = ItemType.material;
    }

    // 其他的一些特殊效果
}
