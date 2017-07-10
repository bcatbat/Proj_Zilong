using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// 类别
public enum Slot
{
    consumable = 0,     // 常规消耗品
    tonic,              // 滋补品, 增加固定属性.
    weapon,             // 武器
    armor,              // 护甲
    trinket,            // 饰品
    task                // 任务物品
}

public class ItemInfo : MonoBehaviour {
    protected Slot m_slot;           // 类型
    [Header("基本属性")]
    public int itemID;          // ID
    public string itemName;     // 名称
    public string itemDes;      // 物品描述

    public Buff[] itemBuff;    // 物品提供的效果


    public virtual void UseItem()
    {
        Debug.Log("Base Class: Use Item Method.");
        // 使用以后, 
    }
}

/*
[CustomEditor(typeof(ItemInfo))]
public class ItemInfoInspector : Editor
{
    private SerializedObject obj;
    private ItemInfo itemInfo;
    private SerializedProperty itemID;
    private SerializedProperty itemName;
    private SerializedProperty itemDes;


    private void OnEnable()
    {
        obj = new SerializedObject(target);

        // 公用属性
        itemID = obj.FindProperty("itemID");
        itemName = obj.FindProperty("itemName");
        itemDes = obj.FindProperty("itemDes");
    }

    public override void OnInspectorGUI()
    {
        itemInfo = (ItemInfo)target;
        itemInfo.slot = (Slot)EditorGUILayout.EnumPopup("物品类型", itemInfo.slot);

        EditorGUILayout.PropertyField(itemID);
        EditorGUILayout.PropertyField(itemName);
        EditorGUILayout.PropertyField(itemDes);

        switch (itemInfo.slot)
        {
            case Slot.consumable:
                break;
            case Slot.tonic:
                break;
            case Slot.weapon:
                break;
            case Slot.armor:
                break;
            case Slot.trinket:
                break;
            case Slot.task:
                break;
            default:
                Debug.Log("error");
                break;
        }
    }
}
*/
