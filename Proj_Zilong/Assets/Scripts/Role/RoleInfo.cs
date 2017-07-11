using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 属性列表
public struct Stats
{
    private int m_UpHp;   // 生命上限
    private int m_UpMp;   // 能量上线
    private int m_Atk;    // 攻击力
    private int m_Def;    // 防御力
    private int m_Spd;    // 移动速度
    private int m_Luk;    // 幸运值

    public int UpHp { get { return m_UpHp; } set { this.m_UpHp = value; } }
    public int UpMp { get { return m_UpMp; } set { this.m_UpMp = value; } }
    public int Atk { get { return m_Atk; } set { this.m_Atk = value; } }
    public int Def { get { return m_Def; } set { this.m_Def = value; } }
    public int Spd { get { return m_Spd; } set { this.m_Spd = value; } }
    public int Luk { get { return m_Luk; } set { this.m_Luk = value; } }

    public Stats(int uphp, int upmp, int atk, int def, int spd, int luk)
    {
        m_UpHp = uphp;m_UpMp = upmp;
        m_Atk = atk;m_Def = def;
        m_Spd = spd; m_Luk = luk;
    }

    public static Stats operator + (Stats s1, Stats s2)
    {
        return new Stats(s1.UpHp+s2.UpHp,
            s1.UpMp+s2.UpMp,
            s1.Atk+s2.Atk,
            s1.Def+s2.Def,
            s1.Spd+s2.Spd,
            s1.Luk+s2.Luk
            );
    }

    public static Stats operator -(Stats s1, Stats s2)
    {
        return new Stats(s1.UpHp - s2.UpHp,
            s1.UpMp - s2.UpMp,
            s1.Atk - s2.Atk,
            s1.Def - s2.Def,
            s1.Spd - s2.Spd,
            s1.Luk - s2.Luk
            );
    }
}

public class RoleInfo : MonoBehaviour {
    // 用来存放各类角色的信息
    protected int m_ID;                   // ID
    protected string m_Name;              // 名称
    protected int m_Level;                // 等级

    protected int m_Hp;                   // 当前生命
    protected int m_Mp;                   // 当前能量

    protected Image m_Portrait;           // 头像
    protected bool m_UnderControl = true; // 是否可控制. 用于ai控制, 或者是控制技能
    protected SkillInfo[] skills;         // 拥有技能-skillinfo.cs

    protected List<Buff> m_BuffPool;      // buff池. 相同的合并/刷新
    protected List<Buff> m_DebuffPool;    // debuff池. 
    
    protected Stats m_ActualStats;       // 实时属性值
    protected Stats m_BasicStats;        // 基础属性值
    protected Stats m_AdditionStats;     // 装备附加的属性值
    protected Stats m_PermanentStats;    // 补品/锻炼永久增加的属性值
    protected Stats m_TempStats;         // 临时增加的属性
    
    public string Name { get { return m_Name; } set { this.m_Name = value; } }
    public int Level { get { return m_Level; } set { this.m_Level = value; } }
    public bool UnderControl { get { return m_UnderControl; } set { this.m_UnderControl = value; } }

    public int Hp {
        get { return m_Hp; }
        set {
            this.m_Hp = value;
            if (m_Hp > m_ActualStats.UpHp)
                m_Hp = m_ActualStats.UpHp;
            if (m_Hp <= 0)
            {
                m_Hp = 0;
                Die();
            }     
        }
    }
    public int Mp {
        get { return m_Mp; }
        set {
            this.m_Mp = value;
            if (m_Mp > m_ActualStats.UpMp)
                m_Mp = m_ActualStats.UpMp;
            if (m_Mp <= 0)
                m_Mp = 0;            
        }
    }

    public virtual Stats ActualStats
    {
        get { return m_ActualStats; }
        set { m_ActualStats = value;}
    }

    public virtual Stats BasicStats
    {
        get{return m_BasicStats;}
        set {m_BasicStats = value; }
    }

    public virtual Stats AdditionStats
    {
        get{return m_AdditionStats; }
        set { m_AdditionStats = value; }
    }
    public virtual Stats PermanentStats
    {
        get{return m_PermanentStats; }
        set{ m_PermanentStats = value;}
    }

    public virtual Stats TempStats
    {
        get { return m_TempStats; }
        set { m_TempStats = value; }
    }

    private void Start()
    {
        m_BuffPool = new List<Buff>();
        m_DebuffPool = new List<Buff>();
    }

    private void FixedUpdate()
    {
        RunBuff(Time.deltaTime);
        RunDebuff(Time.deltaTime);
    }

    void InitBasicStats()
    {
        // 升级的数值从文件读取, 或者是内嵌到代码中.
    }

    // 向自身buff池中添加buff
    public void AddBuff(Buff buff)
    {
        // 如果buffPool中已经存在此buff, 则刷新持续时间
        if (m_BuffPool.Contains(buff))
        {
            buff.Refresh();
        }
        else
        {// 如果buffPool中无此buff存在, 则添加
            buff.Refresh();         // 重置剩余时间
            m_BuffPool.Add(buff);   //
        }
    }

    // 向自身debuff池中添加debuff
    public void AddDebuff(Buff debuff)
    {        
        if (m_DebuffPool.Contains(debuff))
        {
            // 如果debuffPool中已经存在此buff, 则刷新持续时间
            debuff.Refresh();           // 重置剩余时间
        }
        else
        {
            // 如果debuffPool中无此buff存在, 则添加
            debuff.Refresh();           // 重置剩余时间
            m_DebuffPool.Add(debuff);   // 
        }
    }

    // 自身Buffs生效过程
    protected void RunBuff(float deltatime) {
        foreach (var buff in m_BuffPool)
        {
            foreach (var effect in buff.Effects)
            {
                effect.EffectRunning(this, deltatime);
            }
        }
    }

    // 自身debuffs生效过程
    protected void RunDebuff(float deltatime)
    {
        foreach (var debuff in m_DebuffPool)
        {
            foreach (var effect in debuff.Effects)
            {
                effect.EffectRunning(this, deltatime);
            }
        }        
    }

    // 角色死亡
    public void Die()
    {
        Debug.Log(m_ID + " " + m_Name + ": Die!");
    }
}
