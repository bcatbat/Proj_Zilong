using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgent : MonoBehaviour {
    // 动画控制
    private BehaviorTree enemyBT;// 行为树
    private RoleInfo roleinfo; // 角色信息

    public void Awake()
    {
        enemyBT = EnemyLogic.BuildBehaviorTree(roleinfo);
    }

    public void Update()
    {
        if(roleinfo.IsAlive)
        {
            enemyBT.Update(Time.deltaTime);
        }
        // todo: 动画控制
    }
}
