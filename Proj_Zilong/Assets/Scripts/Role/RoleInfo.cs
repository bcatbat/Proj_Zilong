using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 属性列表
public struct Stats
{
    private int upHp;   // 生命上限
    private int upMp;   // 能量上线
    private int atk;    // 攻击力
    private int def;    // 防御力
    private int spd;    // 移动速度
    private int luk;    // 幸运值

    public int UpHp { get { return upHp; } set { this.upHp = value; } }
    public int UpMp { get { return upMp; } set { this.upMp = value; } }
    public int Atk { get { return atk; } set { this.atk = value; } }
    public int Def { get { return def; } set { this.def = value; } }
    public int Spd { get { return spd; } set { this.spd = value; } }
    public int Luk { get { return luk; } set { this.luk = value; } }

    public Stats(int uphp, int upmp, int atk, int def, int spd, int luk)
    {
        upHp = uphp;upMp = upmp;
        this.atk = atk;
        this.def = def;
        this.spd = spd; this.luk = luk;
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
    protected int id;                   // ID
    protected string roleName;          // 名称
    protected int level;                // 等级

    protected int hp;                   // 当前生命
    protected int mp;                   // 当前能量

    protected Image portrait;           // 头像

    protected bool isControllable = true; // 是否可控制. 用于ai控制, 或者是控制技能
    protected bool isAlive = true;      // 是否活着
    protected Transform target;         // 目标位置
    protected Transform guardTarget;    // 护卫/跟随目标

    protected SkillInfo[] skills;       // 拥有技能-skillinfo.cs

    protected List<Buff> buffPool;      // buff池. 相同的合并/刷新
    protected List<Buff> debuffPool;    // debuff池. 
    
    protected Stats actualStats;        // 实时属性值
    protected Stats basicStats;         // 基础属性值
    protected Stats additionStats;      // 装备附加的属性值
    protected Stats permanentStats;     // 补品/锻炼永久增加的属性值
    protected Stats tempStats;          // 临时增加的属性 

    private float guardRange = 30f;
    private float skillRange = 5f;
    private float slashRange = 2f;
    private float dodgeRange = .8f;

    private float dodgeProbability = 0.4f;

    private Transform captain;
    

    public string Name { get { return roleName; } set { this.roleName = value; } }

    public int Level { get { return level; } set { this.level = value; } }

    public bool IsControllable { get { return isControllable; } set { this.isControllable = value; } }

    public bool IsAlive { get { return isAlive; } set { this.isAlive = value; } }

    public Transform Target { get { return target; } set { this.target = value; } }
    public Transform GuardTarget { get { return guardTarget; } }

    public float GuardRange { get { return guardRange; } }
    public float SkillRange { get { return skillRange; } }
    public float SlashRange { get { return slashRange; } }
    public float DodgeRange { get { return dodgeRange; } }

    public float DodgeProbability { get { return dodgeProbability; } set { dodgeProbability = value; } }

    public Transform Captain { get { return captain; } }

    public int Hp {
        get { return hp; }
        set {
            this.hp = value;
            if (hp > actualStats.UpHp)
                hp = actualStats.UpHp;
            if (hp <= 0)
            {
                hp = 0;
            }     
        }
    }
    public int Mp {
        get { return mp; }
        set {
            this.mp = value;
            if (mp > actualStats.UpMp)
                mp = actualStats.UpMp;
            if (mp <= 0)
                mp = 0;            
        }
    }

    public virtual Stats ActualStats
    {
        get { return actualStats; }
        set { actualStats = value;}
    }

    public virtual Stats BasicStats
    {
        get{return basicStats;}
        set {basicStats = value; }
    }

    public virtual Stats AdditionStats
    {
        get{return additionStats; }
        set { additionStats = value; }
    }

    public virtual Stats PermanentStats
    {
        get{return permanentStats; }
        set{ permanentStats = value;}
    }

    public virtual Stats TempStats
    {
        get { return tempStats; }
        set { tempStats = value; }
    }

    private void Start()
    {
        buffPool = new List<Buff>();
        debuffPool = new List<Buff>();

        guardTarget = captain;
    }

    void InitBasicStats()
    {
        // 升级的数值从文件读取, 或者是内嵌到代码中.
    }

    private void FixedUpdate()
    {
        RunBuff(Time.deltaTime);
        RunDebuff(Time.deltaTime);
    }

    // 向自身buff池中添加buff
    public void AddBuff(Buff buff)
    {
        // 如果buffPool中已经存在此buff, 则刷新持续时间
        if (buffPool.Contains(buff))
        {
            buff.Refresh();
        }
        else
        {// 如果buffPool中无此buff存在, 则添加
            buff.Refresh();         // 重置剩余时间
            buffPool.Add(buff);   //
        }
    }

    // 向自身debuff池中添加debuff
    public void AddDebuff(Buff debuff)
    {        
        if (debuffPool.Contains(debuff))
        {
            // 如果debuffPool中已经存在此buff, 则刷新持续时间
            debuff.Refresh();           // 重置剩余时间
        }
        else
        {
            // 如果debuffPool中无此buff存在, 则添加
            debuff.Refresh();           // 重置剩余时间
            debuffPool.Add(debuff);   // 
        }
    }

    // 自身Buffs生效过程
    protected void RunBuff(float deltatime) {
        foreach (var buff in buffPool)
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
        foreach (var debuff in debuffPool)
        {
            foreach (var effect in debuff.Effects)
            {
                effect.EffectRunning(this, deltatime);
            }
        }        
    }
}
