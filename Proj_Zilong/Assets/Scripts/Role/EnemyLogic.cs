using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyLogic {
    // 通用:发呆动作
    public static RoleAction IdleAction(RoleInfo info) 
    {
        return new RoleAction("idle",EnemyAction.IdleInit,EnemyAction.IdleUpdate,EnemyAction.IdleCleanUp,info);
    }

    // 通用:死亡动作
    public static RoleAction DieAction(RoleInfo info)
    {
        return new RoleAction("die", EnemyAction.DieInit, EnemyAction.DieUpdate, EnemyAction.DieCleanUp, info);
    }
    
    // 通用:逃跑动作
    public static RoleAction FleeAction(RoleInfo info)
    {
        return new RoleAction("flee", EnemyAction.FleeInit, EnemyAction.FleeUpdate, EnemyAction.FleeCleanUp, info);
    }

    // 通用:返回动作
    public static RoleAction GoBackAction(RoleInfo info)
    {
        return new RoleAction("go back", EnemyAction.GoBackInit, EnemyAction.GoBackUpdate, EnemyAction.GoBackCleanUp, info);
    }

    // 通用:追赶动作
    public static RoleAction PursueAction(RoleInfo info)
    {
        return new RoleAction("pursue", EnemyAction.PursueInit, EnemyAction.PursueUpdate, EnemyAction.PursueCleanUp, info);
    }

    //// 特殊:射击动作
    //public static RoleAction ShootAction(RoleInfo info)
    //{
    //    //return new RoleAction("shoot", EnemyAction.ShootInit, EnemyAction.ShootUpdate, EnemyAction.ShootCleanUp, info);
    //    return new RoleAction("shoot", RobotAction.ShootInit, RobotAction.ShootUpdate, RobotAction.ShootCleanUp,info);
    //}        
    
    // 通用:建立动作节点
    public static BehaviorTreeNode CreateAction(string name, RoleAction action)
    {
        BehaviorTreeNode node = new BehaviorTreeNode(name, BehaviorTreeNode.NodeType.ACTION);
        node.SetAction(action);
        return node;
    }

    // 通用:建立条件节点
    public static BehaviorTreeNode CreateCondition(string name, Func<RoleInfo, bool> evaluator)
    {
        BehaviorTreeNode condition = new BehaviorTreeNode(name, BehaviorTreeNode.NodeType.CONDITION);
        condition.SetEvaluator(evaluator);
        return condition;
    }

    // 通用:建立选择节点
    public static BehaviorTreeNode CreateSelector()
    {
        BehaviorTreeNode selector = new BehaviorTreeNode("selector", BehaviorTreeNode.NodeType.SELECTOR);
        return selector;
    }

    // 通用:建立序列节点
    public static BehaviorTreeNode CreateSequence()
    {
        BehaviorTreeNode sequence = new BehaviorTreeNode("sequence", BehaviorTreeNode.NodeType.SEQUENCE);
        return sequence;
    }
}
