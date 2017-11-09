using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RobotLogic {
    // 特殊:射击动作
    private static RoleAction ShootAction(RoleInfo info)
    {
        //return new RoleAction("shoot", EnemyAction.ShootInit, EnemyAction.ShootUpdate, EnemyAction.ShootCleanUp, info);
        return new RoleAction("shoot", RobotAction.ShootInit, RobotAction.ShootUpdate, RobotAction.ShootCleanUp, info);
    }

    public static BehaviorTree BuildBehaviorTree(RoleInfo info)
    {
        BehaviorTree tree = new BehaviorTree(info);
        BehaviorTreeNode node;
        BehaviorTreeNode child;

        // 根节点
        node = EnemyLogic.CreateSelector();
        tree.SetNode(node);

        // die
        child = EnemyLogic.CreateSequence();
        node.AddChild(child);
        node = child;

        child = EnemyLogic.CreateCondition("is not alive", EnemyEvaluators.IsNotAlive);
        node.AddChild(child);
        node = child;

        node = node.Parent;
        child = EnemyLogic.CreateAction("die", EnemyLogic.DieAction(info));
        node.AddChild(child);
        node = child;

        // flee 
        node = node.Parent;
        node = node.Parent;
        child = EnemyLogic.CreateSequence();
        node.AddChild(child);
        node = child;

        child = EnemyLogic.CreateCondition("has critical health", EnemyEvaluators.HasCriticalHealth);
        node.AddChild(child);
        node = child;

        node = node.Parent;
        child = EnemyLogic.CreateAction("flee", EnemyLogic.FleeAction(info));
        node.AddChild(child);
        node = child;

        // out of guard range
        node = node.Parent;
        node = node.Parent;
        child = EnemyLogic.CreateSequence();
        node.AddChild(child);
        node = child;

        child = EnemyLogic.CreateCondition("out of guard range", EnemyEvaluators.OutOfGuardRange);
        node.AddChild(child);
        node = child;

        node = node.Parent;
        child = EnemyLogic.CreateAction("go back", EnemyLogic.GoBackAction(info));
        node.AddChild(child);
        node = child;

        // has enemy
        node = node.Parent;
        node = node.Parent;
        child = EnemyLogic.CreateSequence();
        node.AddChild(child);
        node = child;

        child = EnemyLogic.CreateCondition("has enemy", EnemyEvaluators.HasEnemy);
        node.AddChild(child);
        node = child;

        node = node.Parent;
        child = EnemyLogic.CreateSelector();
        node.AddChild(child);
        node = child;

        child = EnemyLogic.CreateSequence();
        node.AddChild(child);
        node = child;

        child = EnemyLogic.CreateCondition("within shoot range", EnemyEvaluators.WithinSlashRange);
        node.AddChild(child);
        node = child;

        node = node.Parent;
        child = EnemyLogic.CreateAction("shoot", ShootAction(info));    // 特殊动作:双枪射击
        node.AddChild(child);
        node = child;

        node = node.Parent;
        node = node.Parent;
        child = EnemyLogic.CreateAction("pursue", EnemyLogic.PursueAction(info));
        node.AddChild(child);
        node = child;

        // 发呆
        node = node.Parent;
        node = node.Parent;
        node = node.Parent;
        child = EnemyLogic.CreateAction("idle", EnemyLogic.IdleAction(info));
        node.AddChild(child);
        node = child;

        return tree;
    }
}
