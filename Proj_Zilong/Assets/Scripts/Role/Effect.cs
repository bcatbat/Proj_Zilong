using System;
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
    private float m_actTime = 0f;        // 生效时间
    private float m_leftTime = 3600f;   // 剩余时间

    [SerializeField] private EffectType m_effectType;       // 效果类型
    [SerializeField]private int m_effectValue = 0;          // 效果数值
    [SerializeField]private float m_duration = 3600f;       // 总持续时间
    public EffectRunning m_effectRunning;

    private int m_tickValue = 0;        // 每一跳的数值(默认:1s 1跳)

    public bool isRunning = false;        // 是否生效

    // 特效置零
    public void Reset()
    {        
        m_tickTime = 0;                 // 每跳计时器
        m_actTime = 0;                  // 生效时间
        m_leftTime = m_duration;        // 剩余时间

        m_tickValue =(int)( m_effectValue / m_duration);      // 初始化每跳值

        isRunning = false;      // 不起作用
    }    

    // 特效刷新, 持续时间刷新.
    private void Refresh() { }
	
	// 占位符
	public void NA(){}

    // DirectHealing效果
    public void DirectHealling(RoleInfo role,float deltatime) {
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
    public void DirectDamage(RoleInfo role,float deltatime)
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
    public void HealingOverTimes(RoleInfo role, float deltatime) {
        if (isRunning)
        {
            m_actTime += deltatime;     // 生效计时器
            m_tickTime += deltatime;    // 每跳计时器
            m_leftTime = m_duration - m_actTime;    // 剩余时间计时器

            // 每跳生效, 重置每跳计时器
            if (m_tickTime >= 1 && m_actTime <= m_duration)
            {
                role.Hp += m_tickValue;
                m_tickTime = 0;
            }

            // 超时, 效果失效, 重置
            if (m_actTime > m_duration)
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
    public void DamageOverTimes(RoleInfo role,float deltatime) {
        if (isRunning)
        {
            m_actTime += deltatime;                 // 生效计时器
            m_tickTime += deltatime;                // 每跳计时器
            m_leftTime = m_duration - m_actTime;    // 剩余时间计时器

            // 每跳生效
            if (m_tickTime >= 1 && m_actTime <= m_duration)
            {
                role.Hp -= m_tickValue;
                m_tickTime = 0;
            }

            if (m_actTime > m_duration)
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
    public void AttackIncrease(RoleInfo role,float deltatime) {
        if (isRunning)
        {            
            // 生效后执行一次, 后面的时间保持.
            if (m_actTime < float.Epsilon)
                role.TempStats += new Stats(0, 0, m_effectValue, 0, 0, 0);

            m_actTime += deltatime;
            m_leftTime = m_duration - m_actTime;           

            // 到期后回归原状
            if( m_actTime > m_duration)
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
    public void AttackDecrease(RoleInfo role,float deltatime) {
        if (isRunning)
        {
            // 生效后执行一次, 后面的时间保持.
            if (m_actTime < float.Epsilon)
                role.TempStats -= new Stats(0, 0, m_effectValue, 0, 0, 0);

            m_actTime += deltatime;
            m_leftTime = m_duration - m_actTime;

            // 到期后回归原状
            if (m_actTime > m_duration)
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
    public void DefenseIncrease(RoleInfo role,float deltatime)
    {
        if (isRunning)
        {
            // 生效后执行一次, 后面的时间保持.
            if (m_actTime < float.Epsilon)
                role.TempStats += new Stats(0, 0, 0, m_effectValue, 0, 0);

            m_actTime += deltatime;
            m_leftTime = m_duration - m_actTime;

            // 到期后回归原状
            if (m_actTime > m_duration)
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
    public void DefenseDecrease(RoleInfo role,float deltatime)
    {
        if (isRunning)
        {
            // 生效后执行一次, 后面的时间保持.
            if (m_actTime < float.Epsilon)
                role.TempStats -= new Stats(0, 0, 0, m_effectValue, 0, 0);

            m_actTime += deltatime;
            m_leftTime = m_duration - m_actTime;

            // 到期后回归原状
            if (m_actTime > m_duration)
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
    public void SpeedIncrease(RoleInfo role,float deltatime)
    {
        if (isRunning)
        {
            if (m_actTime < float.Epsilon)
                role.TempStats += new Stats(0, 0, 0, 0, m_effectValue, 0);
            m_actTime += deltatime;
            m_leftTime = m_duration - m_actTime;

            if(m_actTime > m_duration)
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
    public void SpeedDecrease(RoleInfo role,float deltatime)
    {
        if (isRunning)
        {
            if (m_actTime < float.Epsilon)
                role.TempStats -= new Stats(0, 0, 0, 0, m_effectValue, 0);
            m_actTime += deltatime;
            m_leftTime = m_duration - m_actTime;

            if (m_actTime > m_duration)
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
    public void Stun(RoleInfo role,float deltatime)
    {
        if (isRunning)
        {
            role.UnderControl = false;
            m_actTime += deltatime;
            m_leftTime = m_duration - m_actTime;

            if(m_actTime > m_duration)
            {
                role.UnderControl = true;
                Reset();
            }
        }
        else
        {
            return;
        }
    }
}
