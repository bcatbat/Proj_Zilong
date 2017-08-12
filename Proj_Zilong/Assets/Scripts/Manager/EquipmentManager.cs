﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {
    private static EquipmentManager instance;
    public static EquipmentManager Instance
    {
        get { return instance; }
    }

    public WeaponItem weapon;
    public ArmorItem armor;
    public TrinketItem trinket1;
    public TrinketItem trinket2;

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
    }

    public void EquipWeapon(WeaponItem desWeapon)
    {
        // 卸下原武器
        weapon.isEquipped = false;
        // 换武器
        weapon = desWeapon;
        weapon.isEquipped = true;
    }

    public void UnloadWeapon(WeaponItem desWeapon)
    {
        if(weapon == desWeapon)
        {
            weapon.isEquipped = false;
            weapon = new WeaponItem();
        }
        else
        {
            Debug.LogError("并未装备此武器");
        }
    }

    public void EquipArmor(ArmorItem desArmor)
    {
        armor.isEquipped = false;
        armor = desArmor;
        armor.isEquipped = true;
    }

    public void UnloadArmor(ArmorItem desArmor)
    {
        if(armor == desArmor)
        {
            armor.isEquipped = false;
            armor = new ArmorItem();
        }
        else
        {
            Debug.LogError("并未装备此护甲!!!");
        }
    }

    public void EquipTrinket(TrinketItem desTrinket)
    {
        if(trinket1.itemID == 0)
        {
            trinket1 = desTrinket;
            trinket1.isEquipped = true;
        }else if(trinket2.itemID == 0)
        {
            trinket2 = desTrinket;
            trinket2.isEquipped = true;
        }
        else
        {
            trinket1.isEquipped = false;
            trinket1 = desTrinket;
            trinket1.isEquipped = true;
        }
    }

    public void UnloadTrinket(TrinketItem desTrinket)
    {
        if(trinket1 == desTrinket)
        {
            trinket1.isEquipped = false;            
            trinket1 = new TrinketItem();
        }else if(trinket2 == desTrinket)
        {
            trinket2.isEquipped = false;
            trinket2 = new TrinketItem();
        }
        else
        {
            Debug.LogError("未装备该饰品!!!");
        }
    }

    public bool IsTrinketEquipped(TrinketItem trinket)
    {
        return trinket1 == trinket ? true :
            trinket2 == trinket ? true :
            false;
    }
}