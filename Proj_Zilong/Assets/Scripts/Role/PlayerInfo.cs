﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : RoleInfo {

    private static PlayerInfo m_instance;   // 单例

    private int m_UpExp;                // 经验上限
    private int m_Exp;                  // 当前经验

    public WeaponItem WeaponSlots;      // 武器槽
    public ArmorItem ArmorSlots;        // 护甲槽
    public TrinketItem TrinketSlots1;   // 饰品槽1
    public TrinketItem TrinketSlots2;   // 饰品槽2... 后续扩展数量

    public static PlayerInfo Instance{get {return m_instance;}}
    public int UpExp { get { return m_UpExp; } set { this.m_UpExp = value; } }
    public int Exp { get { return m_Exp; } set { this.m_Exp = value; } }

    public override Stats ActualStats
    {
        get
        {
            m_ActualStats = m_BasicStats + m_AdditionStats + m_PermanentStats;
            return m_ActualStats;
        }
    }

    public override Stats BasicStats
    {
        get
        {
            return m_BasicStats;
        }
    }

    public override Stats AdditionStats
    {
        get
        {
            m_AdditionStats =
                WeaponSlots.AttachStats +
                ArmorSlots.AttachStats +
                TrinketSlots1.AttachStats +
                TrinketSlots2.AttachStats;
            return m_AdditionStats;
        }
    }

    public override Stats PermanentStats
    {
        get
        {
            return m_PermanentStats;
        }
        set
        {
            m_PermanentStats = value;
        }
    }

    private void Awake()
    {
        m_instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // buff生效时,更新监视图标

    // debuff生效时,更新监视图标

}
