using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEvaluators : RoleEvaluator
{
    public EnemyEvaluators(string name, Func<bool> evalFunction, RoleInfo info) : base(name, evalFunction, info)
    {
        evalName = name;
        function = evalFunction;
        roleInfo = info;
        evaluate = Evaluate();
    }

    public static bool True(RoleInfo info)
    {
        return true;
    }
    public static bool False(RoleInfo info)
    {        
        return false;
    }

    public static bool HasCriticalHealth(RoleInfo info)
    {
        // todo:数字需要替换
        return info.Hp < info.ActualStats.UpHp * 0.2;
    }

    public static bool HasEnemy(RoleInfo info)
    {
        // todo
        return false;
    }

    public static bool HasTargetPostion(RoleInfo info)
    {
        // todo
        return false;        
    }

    public static bool IsAlive(RoleInfo info)
    {
        // todo
        return false;
    }

    public static bool OutOfGuardRange(RoleInfo info)
    {
        // todo
        return false;
    }
}
