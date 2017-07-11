using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInfo : MonoBehaviour {
    [SerializeField] private int m_skillID;         // id
    [SerializeField] private string m_skillName;    // 名称
    [SerializeField] private float m_skillCD;       // cd
    [SerializeField] private Image m_skillIcon;     // 图标(命名等同于技能名)

    [SerializeField] private List<Buff> m_OwnBuff;  // 给自己的buff/debuff
    [SerializeField] private List<Buff> m_TarBuff;  // 给目标的buff/debuff

    private RoleInfo m_self;                        // 自身
    private RoleInfo[] m_targets;                   // 技能影响的目标们

    // 初始化   

    // 产生Buff:增加buff, 目标:自身
    public void AddBuff(RoleInfo self) {
        foreach(Buff buff in m_OwnBuff)
        {
            // 初始化buff中每个effect的方法
            foreach(var effect in buff.Effects)
            {
                effect.InitEffect();
            }

            // 给自己buff池中添加buff
            self.AddBuff(buff);              
        }
    }

    // 产生buff:增加debuff, 目标:作用目标
    public void AddDebuf(RoleInfo target) {
        foreach (var debuff in m_TarBuff)
        {
            // 初始化debuff中每个effect的方法
            foreach(var effect in debuff.Effects)
            {
                effect.InitEffect();
            }

            // 给目标(们)debuff池中添加debuff
            target.AddDebuff(debuff);
        }
    }

    // 作用域: 我单体,扇形aoe敌, 圆形aoe敌, 穿刺aoe敌.
    public void GetTarges()
    {

    }

    // 特殊效果:单位位移效果.

    // 特殊效果:是否可叠加

    // 初始化
}
