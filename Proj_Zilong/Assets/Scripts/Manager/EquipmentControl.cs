using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentControl : MonoBehaviour
{
    [SerializeField] private GameObject weaponSlot;       // 武器栏
    [SerializeField] private GameObject armorSlot;        // 护甲栏
    [SerializeField] private GameObject trinket1Slot;     // 饰品栏1
    [SerializeField] private GameObject trinket2Slot;     // 饰品栏2

    private static EquipmentControl instance;
    public static EquipmentControl Instance
    {
        get { return instance; }
    }
}
