using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEvaluators
{
    public static bool True(RoleInfo info)
    {
        return true;
    }

    public static bool False(RoleInfo info)
    {        
        return false;
    }

    public static bool IsAlive(RoleInfo info)
    {
        if (info.Hp <= 0)
        {
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool HasCriticalHealth(RoleInfo info)
    {
        // todo:数字需要替换
        return info.Hp < info.ActualStats.UpHp * 0.2;
    }

    public static bool HasEnemy(RoleInfo info)
    {
        // todo 以当前角色位中心, 进行范围碰撞检测, 然后从检测到的目标中选择一个非本阵营的角色作为目标

        // player
        Vector3 myPos = info.transform.position;
        Vector3 playerPos = PlayerInfo.Instance.transform.position;

        float mtoP = Vector3.Distance(myPos, playerPos);
        if (mtoP < info.GuardRange)
        {
            info.Target = PlayerInfo.Instance.transform;
            return true;
        }

        // ally
        GameObject[] allies = GameObject.FindGameObjectsWithTag("Ally");
        float min_mtoA = float.MaxValue;
        Transform target = null;
        foreach (var ally in allies)
        {
            float mtoA = Vector3.Distance(myPos, ally.transform.position);

            if (mtoA < min_mtoA)
            {
                min_mtoA = mtoA;
                target = ally.transform;
            }
        }

        if (min_mtoA <= info.GuardRange && target != null)
        {
            info.Target = target;
            return true;
        }

        return false;
    }

    private static bool IsTargetWithinRange(RoleInfo info, Vector3 tarPos, float range)
    {
        Vector3 myPos = info.transform.position;

        float distance = Vector3.Distance(tarPos, myPos);

        if (distance >= range)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static bool OutOfGuardRange(RoleInfo info)
    {
        if (info.GuardTarget == null)
            return false;
        else
            return !IsTargetWithinRange(info, info.GuardTarget.position, info.GuardRange);
    }

    public static bool FarAwayFromCaptain(RoleInfo info)
    {
        if (info.Captain == null)
            return false;
        else
            return !IsTargetWithinRange(info, info.Captain.position, info.GuardRange);
    }

    public static bool WithinSkillRange(RoleInfo info)
    {
        if (info.Target == null)
            return false;
        else
            return IsTargetWithinRange(info, info.Target.position, info.SkillRange);
    }      

    public static bool TooClose(RoleInfo info)
    {
        if (info.Target == null)
            return false;
        else
            return IsTargetWithinRange(info, info.Target.position, info.DodgeRange);
    }

    public static bool WithinSlashRange(RoleInfo info)
    {
        if (info.Target == null)
            return false;
        else
            return IsTargetWithinRange(info, info.Target.position, info.SlashRange);
    }

    public static bool DodgeDice(RoleInfo info) {
        float res  = UnityEngine.Random.Range(0, 1);

        return res <= info.DodgeProbability;
    }

    public static bool HasTargetPostion(RoleInfo info)
    {
        if (info.OrderTarget == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static bool HasFury(RoleInfo info)
    {
        // fury的id需要改.
        return info.HasBuff(12313);
    }
}
