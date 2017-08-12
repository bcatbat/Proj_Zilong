using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotcutGrid : Grid {

    // 可以挂载物品(药物,补品,装备),技能,装备(与装备不同则更换, 相同则使用附加的技能). 仅仅一个有效
    public Skill skill;
    public Item item;

    protected override void EventListener_OnMouseLeftClick(GameObject gb)
    {
        Debug.Log("使用技能/物品.");
    }
    protected override void EventListener_OnMouseRightClick(GameObject gb)
    {
        EventListener_OnMouseLeftClick(gb);
    }

}
