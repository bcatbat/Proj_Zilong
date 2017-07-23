using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    NA = 0,
    DirectHealling,
    DirectDamage,
    HealingOverTimes,
    DamageOverTimes,
    AttackIncrease,
    AttackDecrease,
    DefenseIncrease,
    DefenseDecrease,
    SpeedIncrease,
    SpeedDecrease,
    Stun
}

[Serializable]
public class Effect{
    private float m_tickTime = 0;       // 每一个效果的内置计时器;     
    private float m_leftTime = 3600f;   // 剩余时间
    private int m_tickValue = 0;        // 每一跳的数值(默认:1s 1跳)
    private float m_duration = 3600f;       // 总持续时间

    [SerializeField]private EffectType m_effectType;       // 效果类型
    [SerializeField]private int m_effectValue = 0;          // 效果数值
    
    public EffectDelegate EffectRunning;
    public bool isRunning = false;        // 是否生效

    public float Duration
    {
        get { return m_duration; }
        set { m_duration = value; }
    }

    public float LeftTime
    {
        get { return m_leftTime; }
        set { m_leftTime = value; }
    }

    // 初始化
    public void InitEffect()
    {           
        string enumName = m_effectType.ToString();
        MethodInfo mi = typeof(Effect).GetMethod(enumName);
        EffectRunning = (EffectDelegate)Delegate.CreateDelegate(typeof(EffectDelegate), this, mi);
    }


    // 特效置零
    private void Reset()
    {        
        m_tickTime = 0;                 // 每跳计时器
        m_leftTime = m_duration;        // 剩余时间

        m_tickValue =(int)( m_effectValue / m_duration);      // 初始化每跳值

        isRunning = false;      // 不起作用
    }    
	
	// 占位符
	private void NA(){}

    // DirectHealing效果
    private void DirectHealling(RoleInfo role,float deltatime) {
        if (isRunning)
        {
            role.Hp += m_effectValue;
            Reset();
        }
        else
        {
            return;
        }
    }

    // DirectDamage效果
    private void DirectDamage(RoleInfo role,float deltatime)
    {
        if (isRunning)
        {
            role.Hp -= m_effectValue;
            Reset();
        }
        else
        {
            return;
        }
    }

    // HOT效果
    private void HealingOverTimes(RoleInfo role, float deltatime) {
        if (isRunning)
        {            
            m_tickTime += deltatime;    // 每跳计时器
            m_leftTime -= deltatime;    // 剩余计时器

            // 每跳生效, 重置每跳计时器
            if (m_tickTime >= 1 && m_leftTime >= 0)
            {
                role.Hp += m_tickValue;
                m_tickTime = 0;
            }

            // 超时, 效果失效, 重置
            if (m_leftTime < 0)
            {
                Reset();
            }
        }
        else
        {
            return;
        }
    }

    // DOT效果
    private void DamageOverTimes(RoleInfo role,float deltatime) {
        if (isRunning)
        {
            m_tickTime += deltatime;    // 每跳计时器
            m_leftTime -= deltatime;    // 剩余计时器

            // 每跳生效
            if (m_tickTime >= 1 && m_leftTime >= 0)
            {
                role.Hp -= m_tickValue;
                m_tickTime = 0;
            }

            if (m_leftTime < 0)
            {
                Reset();
            }
        }
        else
        {
            return;
        }
    }

    // AtkInc效果
    private void AttackIncrease(RoleInfo role,float deltatime) {
        if (isRunning)
        {            
            // 生效后执行一次, 后面的时间保持.
            if (m_leftTime == m_duration)
                role.TempStats += new Stats(0, 0, m_effectValue, 0, 0, 0);

            m_tickTime += deltatime;    // 每跳计时器
            m_leftTime -= deltatime;    // 剩余计时器

            // 到期后回归原状
            if (m_leftTime < 0)
            {
                role.TempStats -= new Stats(0, 0, m_effectValue, 0, 0, 0);
                Reset();
            }
        }
        else
        {
            return;
        }
    }

    // AtkDec效果
    private void AttackDecrease(RoleInfo role,float deltatime) {
        if (isRunning)
        {
            // 生效后执行一次, 后面的时间保持.
            if (m_leftTime == m_duration)
                role.TempStats -= new Stats(0, 0, m_effectValue, 0, 0, 0);

            m_tickTime += deltatime;    // 每跳计时器
            m_leftTime -= deltatime;    // 剩余计时器

            // 到期后回归原状
            if (m_leftTime < 0)
            {
                role.TempStats += new Stats(0, 0, m_effectValue, 0, 0, 0);
                Reset();
            }
        }
        else
        {
            return;
        }
    }

    // DefInc效果
    private void DefenseIncrease(RoleInfo role,float deltatime)
    {
        if (isRunning)
        {
            // 生效后执行一次, 后面的时间保持.
            if (m_leftTime == m_duration)
                role.TempStats += new Stats(0, 0, 0, m_effectValue, 0, 0);

            m_tickTime += deltatime;    // 每跳计时器
            m_leftTime -= deltatime;    // 剩余计时器

            // 到期后回归原状
            if (m_leftTime < 0)
            {
                role.TempStats -= new Stats(0, 0, 0, m_effectValue, 0, 0);
                Reset();
            }
        }
        else
        {
            return;
        }
    }

    // DefDec效果
    private void DefenseDecrease(RoleInfo role,float deltatime)
    {
        if (isRunning)
        {
            // 生效后执行一次, 后面的时间保持.
            if (m_leftTime == m_duration)
                role.TempStats -= new Stats(0, 0, 0, m_effectValue, 0, 0);

            m_tickTime += deltatime;    // 每跳计时器
            m_leftTime -= deltatime;    // 剩余计时器

            // 到期后回归原状
            if (m_leftTime < 0)
            {
                role.TempStats += new Stats(0, 0, 0, m_effectValue, 0, 0);
                Reset();
            }
        }
        else
        {
            return;
        }
    }

    // SpdInc效果
    private void SpeedIncrease(RoleInfo role,float deltatime)
    {
        if (isRunning)
        {
            if (m_leftTime == m_duration)
                role.TempStats += new Stats(0, 0, 0, 0, m_effectValue, 0);

            m_tickTime += deltatime;    // 每跳计时器
            m_leftTime -= deltatime;    // 剩余计时器

            if (m_leftTime < 0)
            {
                role.TempStats -= new Stats(0, 0, 0, 0, m_effectValue, 0);
                Reset();
            }
        }
        else
        {
            return;
        }
    }

    // SpdDec效果
    private void SpeedDecrease(RoleInfo role,float deltatime)
    {
        if (isRunning)
        {
            if (m_leftTime == m_duration)
                role.TempStats -= new Stats(0, 0, 0, 0, m_effectValue, 0);

            m_tickTime += deltatime;    // 每跳计时器
            m_leftTime -= deltatime;    // 剩余计时器

            if (m_leftTime < 0)
            {
                role.TempStats += new Stats(0, 0, 0, 0, m_effectValue, 0);
                Reset();
            }
        }
        else
        {
            return;
        }
    }

    // Stun效果
    private void Stun(RoleInfo role,float deltatime)
    {
        if (isRunning)
        {
            role.IsControllable = false;

            m_tickTime += deltatime;    // 每跳计时器
            m_leftTime -= deltatime;    // 剩余计时器

            if (m_leftTime < 0)
            {
                role.IsControllable = true;
                Reset();
            }
        }
        else
        {
            return;
        }
    }
}
