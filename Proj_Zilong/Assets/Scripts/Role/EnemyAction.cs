using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : RoleAction
{
    public EnemyAction(string name, Action initFunc, Func<float, Status> updateFunc, Action cleanUpFunc, RoleInfo info) : base(name, initFunc, updateFunc, cleanUpFunc, info)
    {
        actionName = name;
        initialzeFunction = initFunc;
        updateFunction = updateFunc;
        cleanUpFunction = cleanUpFunc;
        roleInfo = info;
    }

    public static void IdleCleanUp() { }    
    public static void IdleInit()
    {
        // todo:动画控制
    }   
    public static Status IdleUpdate(float deltaTime) { return Status.RUNNING; }

    public static void DieCleanUp() { }
    public static void DieInit() { }
    public static Status DieUpdate(float deltaTime) { return Status.RUNNING; }

    public static void SlashCleanUp() { }
    public static void SlashInit() { }
    public static Status SlashUpdate(float deltaTime) { return Status.RUNNING; }

    public static void ShootCleanUp() { }
    public static void ShootInit() { }
    public static Status ShootUpdate(float deltaTime) { return Status.RUNNING; }

    public static void MoveCleanUp() { }
    public static void MoveInit() { }
    public static Status MoveUpdate(float deltaTime) { return Status.RUNNING; }

    public static void PursueCleanUp() { }
    public static void PursueInit() { }
    public static Status PursueUpdate(float deltaTime) { return Status.RUNNING; }

    public static void FleeCleanUp() { }
    public static void FleeInit() { }
    public static Status FleeUpdate(float deltaTime) { return Status.RUNNING; }

    public static void FuryCleanUp() { }
    public static void FuryInit() { }
    public static Status FuryUpdate(float deltaTime) { return Status.RUNNING; }   

    public static void GoBackCleanUp() { }
    public static void GoBackInit() { }
    public static Status GoBackUpdate(float deltaTime) { return Status.RUNNING; }
}
