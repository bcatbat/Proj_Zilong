using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentControl : MonoBehaviour {
    public ItemInfo weaponItem;
    public ItemInfo armorItem;
    public ItemInfo trinket1Item;
    public ItemInfo trinket2Item;

    [SerializeField] private GameObject weaponSlot;       // 武器栏
    [SerializeField] private GameObject armorSlot;        // 护甲栏
    [SerializeField] private GameObject trinket1Slot;     // 饰品栏1
    [SerializeField] private GameObject trinket2Slot;     // 饰品栏2

    private static EquipmentControl instance;
    public static EquipmentControl Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;        
    }

    private void Start()
    {
        weaponItem = new WeaponItem();
        armorItem = new ArmorItem();
        trinket1Item = new TrinketItem();
        trinket2Item = new TrinketItem();
    }

    private void WipeFormerEquipmentMark(ItemInfo former)
    {
        foreach (Item item in InventoryControl.Instance.items)
        {
            if (item.itemInfo == former)
            {
                item.WipeEquipedMark();
                return;
            }
        }
    }

    public void EquipWeapon(WeaponItem weapon)
    {
        // 原有武器卸下
        WipeFormerEquipmentMark(weaponItem);

        // 现有武器装上
        weaponItem = weapon;

        // 刷新显示
        RefreshDisplay();
    }

    public void UnloadWeapon(WeaponItem weapon)
    {
        if (weaponItem == weapon)
        {
            //Debug.Log("unload weapon");
            WipeFormerEquipmentMark(weapon);
            weaponItem = new WeaponItem();
            
        }
        else
        {
            Debug.LogError("并未装备此武器!!!");
        }
        RefreshDisplay();
    }

    public void EquipArmor(ArmorItem armor)
    {
        WipeFormerEquipmentMark(armorItem);

        armorItem = armor;

        RefreshDisplay();
    }

    public void UnloadArmor(ArmorItem armor)
    {
        if(armorItem == armor)
        {
            //Debug.Log("unload armor");
            WipeFormerEquipmentMark(armor);
            armorItem = new ArmorItem();
        }
        else
        {
            Debug.LogError("并未装备此护甲!!!");
        }
        RefreshDisplay();
    }

    public void EquipTrinket(TrinketItem trinket)
    {        
        if (trinket1Item.itemID == 0)
        {
            // 1槽无->装备在1槽
            //Debug.Log("饰品槽1 空-> 装上");
            trinket1Item = trinket;
        }
        else if (trinket2Item.itemID == 0)
        {
            // 1槽有2槽无->装备在2槽
            //Debug.Log("饰品槽2 空->装上");
            trinket2Item = trinket;
        }
        else
        {
            //Debug.Log("饰品1被挤掉了");
            WipeFormerEquipmentMark(trinket1Item);
            // 1槽有2槽有->装备在1槽
            trinket1Item = trinket;            
        }
        RefreshDisplay();
    }

    public void UnloadTrinket(TrinketItem trinket)
    {
        if(trinket1Item == trinket)
        {
            //Debug.Log("unload trinket 1");
            WipeFormerEquipmentMark(trinket);
            trinket1Item = new TrinketItem();
        }else if(trinket2Item == trinket)
        {
            //Debug.Log("unload trinket 2");
            WipeFormerEquipmentMark(trinket);
            trinket2Item = new TrinketItem();
        }
        else
        {
            Debug.LogError("未装备该饰品!!!");
        }
        RefreshDisplay();
    }

    public bool IsTrinketEquiped(TrinketItem trinket)
    {
        return trinket1Item == trinket ? true :
            trinket2Item == trinket ? true :
            false;
    }

    // 刷新装备栏显示
    private void RefreshDisplay()
    {
        // 物品信息
        weaponSlot.GetComponent<Item>().itemInfo = weaponItem;
        armorSlot.GetComponent<Item>().itemInfo = armorItem;
        trinket1Slot.GetComponent<Item>().itemInfo = trinket1Item;
        trinket2Slot.GetComponent<Item>().itemInfo = trinket2Item;

        // TODO:Delete  暂时显示图片
        Image wi = weaponSlot.GetComponent<Image>();
        Image ai = armorSlot.GetComponent<Image>();
        Image t1i = trinket1Slot.GetComponent<Image>();
        Image t2i = trinket2Slot.GetComponent<Image>();

        // TODO:Delete. show sprite and color
        if (weaponItem.itemIcon != null)
        {
            wi.sprite = weaponItem.itemIcon.sprite;
            wi.color = weaponItem.itemIcon.color;
            //wi = weaponItem.itemIcon;
        }
        if (armorItem.itemIcon != null)
        {
            ai.sprite = armorItem.itemIcon.sprite;
            ai.color = armorItem.itemIcon.color;

        }
        if (trinket1Item.itemIcon != null)
        {
            t1i.sprite = trinket1Item.itemIcon.sprite;
            t1i.color = trinket1Item.itemIcon.color;
        }
        if (trinket2Item.itemIcon != null)
        {
            t2i.sprite = trinket2Item.itemIcon.sprite;
            t2i.color = trinket2Item.itemIcon.color;
        }
    }       
}
