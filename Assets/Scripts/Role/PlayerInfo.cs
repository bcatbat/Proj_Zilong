using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : RoleInfo {

    private static PlayerInfo m_instance;   // 单例
    public static PlayerInfo Instance{get {return m_instance;}}    

    [Header("Exp")]
    [SerializeField]private int m_UpExp = 100;                // 经验上限
    [SerializeField]private int m_Exp =0;                  // 当前经验

    private EquipmentItem weaponSlots;      // 武器槽
    private EquipmentItem armorSlots;        // 护甲槽
    private EquipmentItem trinketSlots1;   // 饰品槽1
    private EquipmentItem trinketSlots2;   // 饰品槽2... 后续扩展数量

    private float regenTick = float.MaxValue;        // 回复计时器
    
    public int UpExp {
        get {
            m_UpExp = 100 * level;
            return m_UpExp;
        }
    }
    public int Exp {
        get { return m_Exp; }
        set {             
            this.m_Exp = value;
            if(m_Exp >= UpExp)
            {
                m_Exp = m_Exp - UpExp;  // 经验重置
                LevelUp();
            }
        }
    }

    public override Stats ActualStats
    {
        get
        {
            actualStats = BasicStats + AdditionStats + PermanentStats+TempStats;
            return actualStats;
        }
    }

    public override Stats BasicStats
    {
        get
        {
            basicStats.UpHp = 100 + level * (level - 1);
            basicStats.UpMp = 50 + 20 * level;
            basicStats.Atk = 10 + (level - 2) * level / 10;
            basicStats.Def = 10 + (level - 2) * level / 10;
            basicStats.Spd = 10 + (level - 2) * level / 10;
            basicStats.Luk = 10 + (level - 2) * level / 10;
            return basicStats;
        }
    }

    public override Stats AdditionStats
    {
        get
        {
            weaponSlots = EquipmentManager.Instance.weaponSlot.equipmentItem;
            armorSlots = EquipmentManager.Instance.armorSlot.equipmentItem;
            trinketSlots1 = EquipmentManager.Instance.trinket1Slot.equipmentItem;
            trinketSlots2 = EquipmentManager.Instance.trinket2Slot.equipmentItem;

            additionStats =
                weaponSlots.attachStats +
                armorSlots.attachStats +
                trinketSlots1.attachStats +
                trinketSlots2.attachStats;
            return additionStats;
        }
    }

    public override Stats PermanentStats
    {
        get
        {
            return permanentStats;
        }
        set
        {
            permanentStats = value;
        }
    }

    private void LevelUp()
    {
        level++;
        hp = ActualStats.UpHp;
        MsgControl.Instance.Log("升级到" +"<color=blue><b>" +level+ "</b></color>"+ "了!");
      //  Debug.Log(UpExp + "  " + Exp);
    }

    private void Awake()
    {
        m_instance = this;
        DontDestroyOnLoad(gameObject);
    }

    protected override void Start()
    {
        base.Start();
        country = RoleBelonging.Shu;    // 归属
        allyCountry = RoleBelonging.Wu; // 友军
        //Debug.Log(BasicStats);

        // 初始化数值
        Hp = BasicStats.UpHp;
        Mp = BasicStats.UpMp;
    }
    // buff生效时,更新监视图标

    // debuff生效时,更新监视图标

    protected override void Update()
    {
        base.Update();
        regenTick += Time.deltaTime;
        MpRegen(2);
    }

    private void MpRegen(int rate)
    {
        if (regenTick > 2.0f)
        {
            Mp += rate;
            regenTick = 0f;
        }
    }
}
