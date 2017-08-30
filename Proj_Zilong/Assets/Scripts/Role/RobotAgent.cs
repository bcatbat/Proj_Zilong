using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAgent : MonoBehaviour {
    // 动画控制
    private BehaviorTree robotBT;// 行为树
    private RoleInfo robotInfo; // 角色

    public void Awake()
    {
        robotInfo = GetComponent<RoleInfo>();

        robotBT = RobotLogic.BuildBehaviorTree(robotInfo);
    }

    public void Update()
    {
        if (robotInfo.IsAlive && robotInfo.IsControllable)
        {
            // todo 考虑一下脱控的问题.
            robotBT.Update(Time.deltaTime);
        }
        // todo: 动画控制        
    }
}
