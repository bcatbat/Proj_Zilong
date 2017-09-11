using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {
    private static EquipmentManager instance;
    public static EquipmentManager Instance
    {
        get { return instance; }
    }

    public EquipmentItem weapon;
    public EquipmentItem armor;
    public EquipmentItem trinket1;
    public EquipmentItem trinket2;

    public EquipmentGrid weaponSlot;       // 武器栏
    public EquipmentGrid armorSlot;        // 护甲栏
    public EquipmentGrid trinket1Slot;     // 饰品栏1
    public EquipmentGrid trinket2Slot;     // 饰品栏2

    private static Color DEFAULT_BG_COLOR = new Color(.8f, .8f, .8f, .8f);
    private static Color ICON_BG_COLOR = Color.white;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        weapon = new WeaponItem();
        armor = new ArmorItem();
        trinket1 = new TrinketItem();
        trinket2 = new TrinketItem();
        
        RefreshEquipment();
    }

    private void RefreshEquipment()
    {
        weaponSlot.equipmentItem = weapon;
        if(weapon.itemID != 0)
        {
            weaponSlot.icon.sprite = weapon.itemIcon;
            weaponSlot.icon.color = ICON_BG_COLOR;
        }
        else
        {
            weaponSlot.icon.sprite = null;
            weaponSlot.icon.color = DEFAULT_BG_COLOR;
        }

        armorSlot.equipmentItem = armor as EquipmentItem;
        if(armor.itemID != 0)
        {
            armorSlot.icon.sprite = armor.itemIcon;
            armorSlot.icon.color = ICON_BG_COLOR;
        }
        else
        {
            armorSlot.icon.sprite = null;
            armorSlot.icon.color = DEFAULT_BG_COLOR;
        }

        trinket1Slot.equipmentItem = trinket1 as EquipmentItem;
        if (trinket1.itemID != 0)
        {
            trinket1Slot.icon.sprite = trinket1.itemIcon;
            trinket1Slot.icon.color = ICON_BG_COLOR;
        }
        else
        {
            trinket1Slot.icon.sprite = null;
            trinket1Slot.icon.color = DEFAULT_BG_COLOR;
        }

        trinket2Slot.equipmentItem = trinket2 as EquipmentItem;
        if (trinket2.itemID != 0)
        {
            trinket2Slot.icon.sprite = trinket2.itemIcon;
            trinket2Slot.icon.color = ICON_BG_COLOR;
        }
        else
        {
            trinket2Slot.icon.sprite = null;
            trinket2Slot.icon.color = DEFAULT_BG_COLOR;
        }
    }

    public void EquipWeapon(WeaponItem desWeapon)
    {
        // 卸下原武器
        weapon.isEquipped = false;
        // 换武器
        weapon = desWeapon;
        weapon.isEquipped = true;
        desWeapon.isEquipped = true;

        RefreshEquipment();
    }

    public void UnloadWeapon(WeaponItem desWeapon)
    {
        if(weapon == desWeapon)
        {
            weapon.isEquipped = false;
            desWeapon.isEquipped = false;
            weapon = new WeaponItem();
        }
        else
        {
            Debug.LogError("并未装备此武器");
        }
        RefreshEquipment();
    }

    public void EquipArmor(ArmorItem desArmor)
    {
        armor.isEquipped = false;
        armor = desArmor;
        armor.isEquipped = true;
        desArmor.isEquipped = true;

        RefreshEquipment();
    }

    public void UnloadArmor(ArmorItem desArmor)
    {
        if(armor == desArmor)
        {
            armor.isEquipped = false;
            desArmor.isEquipped = false;
            armor = new ArmorItem();
        }
        else
        {
            Debug.LogError("并未装备此护甲!!!");
        }
        RefreshEquipment();
    }

    public void EquipTrinket(TrinketItem desTrinket)
    {
        // 饰品孔1为空, 装在1上
        if(trinket1.itemID == 0)
        {
            trinket1 = desTrinket;
            trinket1.isEquipped = true;
            desTrinket.isEquipped = true;
        }
        // 或者 饰品孔1不为空 孔2为空, 装在2上
        else if(trinket2.itemID == 0)
        {
            trinket2 = desTrinket;
            trinket2.isEquipped = true;
            desTrinket.isEquipped = true;
        }
        // 或者 饰品孔两个都不为空, 替换在1上
        else
        {
            trinket1.isEquipped = false;
            trinket1 = desTrinket;
            trinket1.isEquipped = true;
            desTrinket.isEquipped = true;
        }

        RefreshEquipment();
    }

    public void UnloadTrinket(TrinketItem desTrinket)
    {
        // 目标饰品装备在孔1上.
        if(trinket1 == desTrinket)
        {
            trinket1.isEquipped = false;
            desTrinket.isEquipped = false;
            trinket1 = new TrinketItem();
        }
        // 或者 目标饰品装备在孔2上
        else if(trinket2 == desTrinket)
        {
            trinket2.isEquipped = false;
            desTrinket.isEquipped = false;
            trinket2 = new TrinketItem();
        }
        else
        {
            Debug.LogError("未装备该饰品!!!");
        }
        RefreshEquipment();
    }

    public bool IsTrinketEquipped(TrinketItem trinket)
    {
        return trinket1 == trinket ? true :
            trinket2 == trinket ? true :
            false;
    }
}
