using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyAction
{
    public static void IdleCleanUp() { }    
    public static void IdleInit()
    {
        // todo:动画控制
    }
    public static RoleAction.Status IdleUpdate(float deltaTime)
    {

        return RoleAction.Status.RUNNING;
    }

    public static void DieCleanUp() { }
    public static void DieInit() { }
    public static RoleAction.Status DieUpdate(float deltaTime)
    {
        return RoleAction.Status.RUNNING;
    }

    public static void SlashCleanUp() { }
    public static void SlashInit() { }
    public static RoleAction.Status SlashUpdate(float deltaTime)
    {
        return RoleAction.Status.RUNNING;
    }

    public static void ShootCleanUp() { }
    public static void ShootInit() { }
    public static RoleAction.Status ShootUpdate(float deltaTime) { return RoleAction.Status.RUNNING; }

    public static void MoveCleanUp() { }
    public static void MoveInit() { }
    public static RoleAction.Status MoveUpdate(float deltaTime) { return RoleAction.Status.RUNNING; }

    public static void PursueCleanUp() { }
    public static void PursueInit() { }
    public static RoleAction.Status PursueUpdate(float deltaTime) { return RoleAction.Status.RUNNING; }

    public static void FleeCleanUp() { }
    public static void FleeInit() { }
    public static RoleAction.Status FleeUpdate(float deltaTime) { return RoleAction.Status.RUNNING; }

    public static void FuryCleanUp() { }
    public static void FuryInit() { }
    public static RoleAction.Status FuryUpdate(float deltaTime) { return RoleAction.Status.RUNNING; }   

    public static void GoBackCleanUp() { }
    public static void GoBackInit() { }
    public static RoleAction.Status GoBackUpdate(float deltaTime) { return RoleAction.Status.RUNNING; }
}
