using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction
{
    // 通用:发呆动作
    public static void IdleCleanUp(RoleInfo info)
    {
        // 无
    }
        
    public static RoleAction.Status IdleInit(RoleInfo info)
    {
        // todo:动画控制-idle状态

        // 设定停止发呆时间
        info.IdleEndTime = Time.timeSinceLevelLoad + info.IdleDuration;
        
        return RoleAction.Status.RUNNING;
    }

    public static RoleAction.Status IdleUpdate(RoleInfo info,float deltaTime)
    {
        // 如果死亡了 则终止
        if (info.Hp <= 0)
        {
            return RoleAction.Status.TERMINATED;
        }

        // 发呆完毕了
        if (Time.timeSinceLevelLoad > info.IdleEndTime)
            return RoleAction.Status.TERMINATED;

        return RoleAction.Status.RUNNING;
    }

    // 通用:死亡动作
    public static void DieCleanUp(RoleInfo info) { }

    public static RoleAction.Status DieInit(RoleInfo info)
    {
        // todo: 动画

        // 一定时间后消除

        // 脱控
        info.Die();   

        return RoleAction.Status.RUNNING;
    }

    public static RoleAction.Status DieUpdate(RoleInfo info,float deltaTime)
    {
        return RoleAction.Status.TERMINATED;
    }

    // 通用:移动动作
    public static void MoveCleanUp(RoleInfo info) { }

    public static RoleAction.Status MoveInit(RoleInfo info)
    {
        return RoleAction.Status.RUNNING;
    }

    public static RoleAction.Status MoveUpdate(RoleInfo info,float deltaTime) { return RoleAction.Status.RUNNING; }

    // 通用:追赶动作
    public static void PursueCleanUp(RoleInfo info) { }

    public static RoleAction.Status PursueInit(RoleInfo info)
    {      
        return RoleAction.Status.RUNNING;
    }
    public static RoleAction.Status PursueUpdate(RoleInfo info, float deltaTime)
    {
        // 如果死亡了 则终止
        if (info.Hp <= 0)
        {
            return RoleAction.Status.TERMINATED;
        }

        // 如果到达了确定位置(distance < slashRange), 则终止        
        info.roleAgent.destination = info.Target.position;

        if (Vector3.Distance(info.transform.position, info.Target.position) < info.SlashRange)
        {
            return RoleAction.Status.TERMINATED;
        }
        //Debug.Log("Pursue");
        return RoleAction.Status.RUNNING;
    }

    // 通用:逃跑动作
    public static void FleeCleanUp(RoleInfo info) {
        // 没什么要写的
    }

    public static RoleAction.Status FleeInit(RoleInfo info)
    {
        if (info.Target == null)
            return RoleAction.Status.TERMINATED;
        else
        {
            // 找一个安全的地方, 至少大于guardRange. 然后会将命令目标覆盖.
            Vector3 offset = info.transform.position - info.Target.position;
            offset.y = 0;
            info.GuardTarget = offset.normalized * info.GuardRange + info.transform.position;

            // todo:增加一个溃散buff, 体力迅速流失

            return RoleAction.Status.RUNNING;
        }
    }

    public static RoleAction.Status FleeUpdate(RoleInfo info,float deltaTime)
    {
        // 如果死亡了 则终止
        if (info.Hp <= 0)
        {
            return RoleAction.Status.TERMINATED;
        }
        // 没体力了, 终止
        if(info.Mp<= 0.1 * info.ActualStats.UpMp)
        {
            return RoleAction.Status.TERMINATED;
        }

        // 向着已经确定的目标位置运动
        // 如果到达了确定位置(distance < tooclose), 则终止
        info.roleAgent.destination = info.GuardTarget;

        if(Vector3.Distance(info.transform.position,info.GuardTarget) < 0.5f)
        {
            return RoleAction.Status.TERMINATED;
        }

        //info.Mp -= info.ActualStats.UpMp /10;   // 逃跑过程中体力迅速流失.
        return RoleAction.Status.RUNNING;
    }

    // 特殊(待改写):狂暴动作
    public static void FuryCleanUp(RoleInfo info) { }

    public static RoleAction.Status FuryInit(RoleInfo info)
    {        
        if (info.Mp > 0.5 * info.ActualStats.UpMp)
        {
            // 给自己添加一个狂暴的buff
            Debug.Log("Fury");
            return RoleAction.Status.RUNNING;
        }
        return RoleAction.Status.TERMINATED;
    }

    public static RoleAction.Status FuryUpdate(RoleInfo info, float deltaTime)
    {
        return RoleAction.Status.TERMINATED;
    }


    // 特殊(待改写):返回动作
    public static void GoBackCleanUp(RoleInfo info) { }

    public static RoleAction.Status GoBackInit(RoleInfo info)
    {        
        //info.OrderTarget.position = info.GuardTarget;        
        return RoleAction.Status.RUNNING;
    }

    public static RoleAction.Status GoBackUpdate(RoleInfo info, float deltaTime)
    {
        // 终止条件. 到达守护位置范围
        if (Vector3.Distance(info.transform.position, info.GuardTarget) < info.GuardRange)
            return RoleAction.Status.TERMINATED;
        info.roleAgent.destination = info.GuardTarget;
        return RoleAction.Status.RUNNING;
    }

    // 特殊(待改写):平砍动作
    public static void SlashCleanUp(RoleInfo info) { }

    public static RoleAction.Status SlashInit(RoleInfo info)
    {
        // todo: 动画开启
        return RoleAction.Status.RUNNING;
    }

    public static RoleAction.Status SlashUpdate(RoleInfo info, float deltaTime)
    {
        // TODO 如果动画未播放完毕 则继续
        return RoleAction.Status.TERMINATED;
    }

    // 特殊(待改写):射击动作
    public static void ShootCleanUp(RoleInfo info) { }

    public static RoleAction.Status ShootInit(RoleInfo info)
    {
        return RoleAction.Status.RUNNING;
    }

    public static RoleAction.Status ShootUpdate(RoleInfo info, float deltaTime)
    {
        // 死亡, 终止
        if (info.Hp <= 0)
            return RoleAction.Status.TERMINATED;

        // 距离超出射程
        if ((Vector3.Distance(info.transform.position, info.Target.position) > info.SlashRange))
            return RoleAction.Status.TERMINATED;

        // 射击


        return RoleAction.Status.RUNNING;
    }
}
