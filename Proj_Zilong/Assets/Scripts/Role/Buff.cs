using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void EffectRunning(RoleInfo role, float deltatime);

[Serializable]
public class Buff{
    // 必有属性: 名称,id,图标,自己/他人,时间
    [SerializeField] private int m_buffID;          // ID
    [SerializeField] private string m_buffName;     // 名称
    [SerializeField] private Image m_buffIcon;      // 图标
    [SerializeField] private float m_buffDuration;  // 作用时间  
    private RoleInfo m_target; // 挂在的目标
    
    public int BuffID { get { return m_buffID; } }
    public string BuffName { get { return m_buffName; } }
    public Image BuffIcon { get { return m_buffIcon; } }

    public List<Effect> Effects;// 该buff的效果集合, 一个buff仅挂一个目标, 仅对目标起作用的特效

    // 刷新持续时间
    public void Refresh() { }   

}