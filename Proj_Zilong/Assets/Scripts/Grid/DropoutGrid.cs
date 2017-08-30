using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropoutGrid : Grid {

    public Item item;   // 挂载物品

    private new void Awake()
    {
        MouseEventRegister();
    }

    #region MouseEvent
    protected override void EventListener_OnMouseBeginDrag(GameObject gb)
    {
        // null
    }
    protected override void EventListener_OnMouseDrag(GameObject gb)
    {
        // null
    }
    protected override void EventListener_OnMouseDrop(GameObject gb)
    {
        // null
    }
    protected override void EventListener_OnMouseEndDrag(GameObject gb)
    {
        // null
    }
    protected override void EventListener_OnMouseLeftClick(GameObject gb)
    {
        // 捡取
        InventoryManager.Instance.AddItem(item);
        // 声音
        HideDescription();
        // 销毁
        Destroy(this.gameObject);
    }
    #endregion

    protected override void ShowDescription()
    {
        if (item.itemID != 0)
        {
            string s = item.itemID + "\n" +
                item.itemName + "\n" +
                item.itemType;
            DescriptionManager.Instance.Show(this.transform, s);
        }
    }
}
