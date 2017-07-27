using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAgent : MonoBehaviour {
    // 动画控制
    private BehaviorTree enemyBT;// 行为树
    private RoleInfo roleinfo; // 角色

    public void Awake()
    {
        enemyBT = EnemyLogic.BuildBehaviorTree(roleinfo);

        roleinfo = GetComponent<RoleInfo>();
    }

    public void Update()
    {
        if(roleinfo.IsAlive)
        {
            // todo 考虑一下脱控的问题.
            enemyBT.Update(Time.deltaTime);
        }
        // todo: 动画控制
    }
}
