using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentControl : MonoBehaviour {
    public GameObject weaponSlot;       // 武器栏
    public GameObject armorSlot;        // 护甲栏
    public GameObject trinketSlot1;     // 饰品栏
    public GameObject trinketSlot2;     // 饰品栏


    private static EquipmentControl instance;
    public static EquipmentControl Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }
}
