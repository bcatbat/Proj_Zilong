using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

// 属性列表
public struct Stats
{
    private int upHp;   // 生命上限
    private int upMp;   // 能量上限
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
    public override string ToString()
    {
        return "UpHp:" + upHp + " UpMp:" + upMp + " Atk:" + atk + " Def:" + def + " Spd:" + spd + " Luk:" + luk;
    }
}

public enum RoleBelonging
{
    other,      // 其他-未知
    Shu,        // 蜀汉
    Wu,         // 东吴
    Wei,        // 曹魏    
}

public class RoleInfo : MonoBehaviour {
    [Header("Basic Info")]
    [SerializeField] protected int id;                  // ID
    [SerializeField] protected string roleName;         // 名称
    [SerializeField] protected int level=1;             // 等级
    public RoleBelonging country;                       // 势力
    public RoleBelonging allyCountry;                   // 友军

    [SerializeField] protected int hp;                  // 当前-生命
    [SerializeField] protected int mp;                  // 当前-能量    
    [SerializeField] protected float baseSpeed = 4.5f;  // 角色基础移动速度
    protected float moveSpeed;                          // 当前-移动速度
    [SerializeField] protected int baseExp = 10;        // 击杀经验

    [SerializeField] protected Image portrait;           // 头像

    [Header("Prefabs")]
    public GameObject hpBarPrefab;                      // 随身血条预制
    private GameObject hpBar;                           // 随身血条
    public GameObject damagePopup;                      // 浮动伤害数字
    public GameObject dropoutPrefab;                      // 掉落的物品

    [Header("Enable")]
    [SerializeField] protected bool isControllable = true;  // 是否可控制. 用于ai控制, 或者是控制技能
    [SerializeField] protected bool isAlive = true;         // 是否活着
    protected float idleEndTime;                         // 停止发呆的时间
    [SerializeField] protected float idleDuration;          // 发呆时长

    [Header("Target")]
    [SerializeField]protected Transform target;            // 目标位置
    [SerializeField] protected Vector3 guardTarget;       // 护卫/跟随目标
    [SerializeField] protected Transform orderTarget;       // 指令目标
    [SerializeField] protected Transform captain;             // 队长
    
    [SerializeField] protected Stats actualStats;        // 实时属性值
    [SerializeField] protected Stats basicStats;         // 基础属性值
    [SerializeField] protected Stats additionStats;      // 装备附加的属性值
    [SerializeField] protected Stats permanentStats;     // 补品/锻炼永久增加的属性值
    [SerializeField] protected Stats tempStats;          // 临时增加的属性 

    [Header("Range")]
    [SerializeField] private float guardRange = 10f;
    [SerializeField] private float skillRange = 5f;
    [SerializeField] private float slashRange = 2f;
    [SerializeField] private float dodgeRange = .8f;

    [Header("Action Probability")]
    [SerializeField][Range(0,1)] protected float dodgeProbability = 0.4f;

    protected Skill[] skills;       // 拥有技能-skillinfo.cs

    [Header("Pool")]
    public List<Buff> buffPool;      // buff池. 相同的合并/刷新
    public List<Buff> debuffPool;    // debuff池. 

    public NavMeshAgent roleAgent;   // ai代理

    public string Name { get { return roleName; } set { this.roleName = value; } }

    public int Level {
        get { return level; }
        set
        {
            this.level = value;
            if (value <= 0)
                this.level = 1;
            else if (value > 50)
                this.level = 50;
        }
    }

    public bool IsControllable { get { return isControllable; } set { this.isControllable = value; } }

    public bool IsAlive { get { return isAlive; } set { this.isAlive = value; } }

    public float IdleEndTime { get { return idleEndTime; } set { idleEndTime = value; } }
    public float IdleDuration { get { return idleDuration; } }

    public Transform Target { get { return target; } set { this.target = value; } }
    public Vector3 GuardTarget { get { return guardTarget; } set { this.guardTarget = value; } }
    public Transform OrderTarget { get { return orderTarget; } set { orderTarget = value; } }

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
            if (hp > ActualStats.UpHp)
                hp = ActualStats.UpHp;
            if (hp <= 0)
            {
                hp = 0;
                Die();
            }     
        }
    }

    public int Mp {
        get { return mp; }
        set {
            this.mp = value;
            if (mp > ActualStats.UpMp)
                mp = ActualStats.UpMp;
            if (mp <= 0)
                mp = 0;            
        }
    }

    public virtual int KillExp
    {
        get { return baseExp + level / 5; }
    }

    public float MoveSpeed
    {
        get { moveSpeed =  baseSpeed * ((float)ActualStats.Spd / 1000 + 1); return moveSpeed; }
    }

    // 实时属性值
    public virtual Stats ActualStats
    {
        get {
            actualStats = BasicStats + AdditionStats + PermanentStats + TempStats;
            return actualStats; }
        set { actualStats = value; }
    }

    // 基础属性值(成长)
    public virtual Stats BasicStats
    {
        get{return basicStats;}
        set {basicStats = value; }
    }

    // 额外属性值(装备)
    public virtual Stats AdditionStats
    {
        get{return additionStats; }
        set { additionStats = value; }
    }

    // 永久增加属性值(补品)
    public virtual Stats PermanentStats
    {
        get{return permanentStats; }
        set{ permanentStats = value;}
    }

    // 临时属性值(Buff)
    public virtual Stats TempStats
    {
        get { return tempStats; }
        set { tempStats = value; }
    }

    private void InitBasicStats()
    {
        // 升级的数值从文件读取, 或者是内嵌到代码中.
    }

    // 死亡
    public virtual void Die()
    {
        // 经验值
        if (country != PlayerInfo.Instance.country && country != PlayerInfo.Instance.allyCountry)
        {
            PlayerInfo.Instance.Exp += KillExp;
        }

        // 掉落
        Dropout();

        // 功能清理
        IsAlive = false;
        IsControllable = false;
        Debug.Log(this.gameObject.name + "死亡");

        // 消除组件
        Destroy(hpBar);
        GetComponent<Collider>().enabled = false;
        Destroy(this.gameObject,1f);        
    }

    #region System
    protected virtual void Start()
    {
        // 池
        buffPool = new List<Buff>();
        debuffPool = new List<Buff>();

        // 代理
        roleAgent = GetComponent<NavMeshAgent>();   // 还得初始化agent的速度.
        if (roleAgent != null)
            roleAgent.speed = MoveSpeed;

        // 目标
        target = null;
        captain = null;
        guardTarget = transform.position;       // 当前位置为默认防御点 
        orderTarget = null;

        // 初始化一个血条
        InitHpBar();

        // 显示一下各个范围
        //DrawRange(GuardRange);
        //DrawRange(SlashRange);
        //DrawRange(skillRange);
    }

    private void FixedUpdate()
    {
        RunBuff(Time.deltaTime);
        RunDebuff(Time.deltaTime);
    }

    protected virtual void Update()
    {
        HpBarFollow();
        HpBarRefresh();
    }
    #endregion

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

    public bool HasBuff(int id)
    {
        foreach(var buff in buffPool)
        {
            if (buff.BuffID == id)
                return true;
        }
        return false;
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

    // 受伤
    public void Hurt(int atkpower)
    {
        float ap = atkpower;
        //Debug.Log(ap);
        float hurt = ap * (1 + Mathf.Log10(ap / ActualStats.Def));
        //Debug.Log(hurt);
        Hp -= (int)hurt;

        // 伤害浮动
        if(damagePopup != null)
        {
            GameObject pd = Instantiate(damagePopup, transform.position, Quaternion.identity);
            pd.GetComponent<DamagePopup>().damageValue = (int)hurt;
        }
        else
        {
            Debug.Log("没有浮动伤害组件");
        }
    }

    // 初始化血条
    private void InitHpBar()
    {
        //Debug.Log(hpBarPrefab.name);
        if (hpBarPrefab != null)
        {
            hpBar = Instantiate(hpBarPrefab);
            hpBar.name = roleName+"hpBar";
            hpBar.transform.parent = ShotCutManager.Instance.transform;            
            hpBar.transform.SetAsFirstSibling();
            HpBarFollow();
        }
        else
        {
            Debug.Log(roleName+ "未开启血条");
        }
    }

    private void HpBarFollow()
    {
        if(hpBarPrefab!=null && hpBar!=null)
            hpBar.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position);
    }

    private void HpBarRefresh()
    {
        if (hpBarPrefab != null && hpBar!=null)
        {
            Slider slider = hpBar.GetComponent<Slider>();
            if (slider == null)
                Debug.Log("错误.");
            slider.minValue = 0f;
            slider.maxValue = ActualStats.UpHp;
            slider.value = Hp;
        }
    }

    // 画圆. 范围指示用
    private void DrawRange(float radius)
    {
        float theta_scale = 0.1f;
        int size = (int)((2* Mathf.PI) / theta_scale);

        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        // material
        lineRenderer.startColor = Color.blue;
        lineRenderer.endColor = Color.blue;
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.positionCount = size;

        int i = 0;
        for(float theta = 0; theta<2*Mathf.PI;theta += theta_scale)
        {
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);

            Vector3 pos = new Vector3(x, y, 0);
            if (i < size)
                lineRenderer.SetPosition(i, pos);
            i += 1;
        }
    }
    
    //  扇形区域碰撞检测. angle-正面扇形角度,accurary-扫描精度(几根射线)
    public bool Look(float angle, float accuracy)
    {
        float rotatePerSeconds = 90f;
        // float accuracy = 3f;
        //float angle = 360f;
        float subAngle = angle / accuracy;

        for (int i = 0; i < accuracy; i++)
        {
            if (LookAround(Quaternion.Euler(
                0,
                -angle / 2 + i * subAngle + Mathf.Repeat(rotatePerSeconds * Time.time, subAngle),
                0)))
            {
                return true;
            }
        }
        return false;
    }
    private bool LookAround(Quaternion eulerAnger)
    {
        Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), eulerAnger * transform.forward * GuardRange, Color.blue);
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), eulerAnger * transform.forward, out hit, GuardRange))
        {
            //   RoleInfo ri = hit.collider.gameObject.GetComponent<RoleInfo>();
            RoleInfo ri = hit.collider.GetComponent<RoleInfo>();
            //if(ri!=null)
            //    Debug.Log(ri.name);

            if (ri != null)
            {
                if (ri.country != country && ri.country != allyCountry)
                {
                    //Debug.Log(true);
                    if(Target == null)
                        Target = ri.transform;
                    return true;
                }
            }
        }
        return false;
    }

    // todo:物品掉落
    void Dropout()
    {
        if(dropoutPrefab != null)
        {
            Debug.Log("有掉落");
            // todo: test
            GameObject dp =  Instantiate(dropoutPrefab, transform.position + new Vector3(0, 3f, 0), Quaternion.identity);
            Item di = dp.GetComponent<DropoutGrid>().item;
            di.itemID = 1;
            di.itemName = "包子";
            di.itemDes = "吃";
            dp.GetComponent<Rigidbody>().AddForce(RandomDirection()*100);

            dp = Instantiate(dropoutPrefab, transform.position + new Vector3(0, 3f, 0), Quaternion.identity);
            di = dp.GetComponent<DropoutGrid>().item;
            di.itemID = 3;
            di.itemName = "气血丹";
            di.itemDes = "吃吃看";
            dp.GetComponent<Rigidbody>().AddForce(RandomDirection() * 100);
        }
        else
        {
            Debug.Log("无掉落..");
        }        
    }
    // todo:从[掉落列表]中获取应该掉落的物品 

    // 随机方向...↖,↗
    static Vector3 RandomDirection()
    {
        Vector3 v = new Vector3();
        v.y = 0;
        v.x = Random.Range(-1f, 1f);
        v.z = Random.Range(-1f, 1f);
        return v.normalized;
    }
}