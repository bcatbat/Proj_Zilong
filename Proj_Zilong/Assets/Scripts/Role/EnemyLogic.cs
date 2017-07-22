using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyLogic {

    public static RoleAction IdleAction(RoleInfo info) 
    {
        return new RoleAction("idle",EnemyAction.IdleInit,EnemyAction.IdleUpdate,EnemyAction.IdleCleanUp,info);
    }

    public static RoleAction DieAction(RoleInfo info)
    {
        return new RoleAction("die", EnemyAction.DieInit, EnemyAction.DieUpdate, EnemyAction.DieCleanUp, info);
    }
    
    public static RoleAction FleeAction(RoleInfo info)
    {
        return new RoleAction("flee", EnemyAction.FleeInit, EnemyAction.FleeUpdate, EnemyAction.FleeCleanUp, info);
    }

    public static RoleAction GoBackAction(RoleInfo info)
    {
        return new RoleAction("go back", EnemyAction.GoBackInit, EnemyAction.GoBackUpdate, EnemyAction.GoBackCleanUp, info);
    }
    
    private static BehaviorTreeNode CreateAction(string name,RoleAction action)
    {
        BehaviorTreeNode node = new BehaviorTreeNode(name, BehaviorTreeNode.NodeType.ACTION);
        node.SetAction(action);
        return node;
    }

    private static BehaviorTreeNode CreateCondition(string name, Func<RoleInfo, bool> evaluator)
    {
        BehaviorTreeNode condition = new BehaviorTreeNode(name, BehaviorTreeNode.NodeType.CONDITION);
        condition.SetEvaluator(evaluator);
        return condition;
    }

    private static BehaviorTreeNode CreateSelector()
    {
        BehaviorTreeNode selector = new BehaviorTreeNode("selector", BehaviorTreeNode.NodeType.SELECTOR);
        return selector;
    }

    private static BehaviorTreeNode CreateSequence()
    {
        BehaviorTreeNode sequence = new BehaviorTreeNode("sequence", BehaviorTreeNode.NodeType.SEQUENCE);
        return sequence;
    }

    public static BehaviorTree BuildBehaviorTree(RoleInfo info)
    {
        BehaviorTree tree = new BehaviorTree(info);
        BehaviorTreeNode node;
        BehaviorTreeNode child;

        // 根节点
        node = CreateSelector();
        tree.SetNode(node);

        // die
        child = CreateSequence();
        node.AddChild(child);
        node = child;
                
        child = CreateCondition("is not alive", EnemyEvaluators.IsAlive);
        node.AddChild(child);
        node = child;

        node = child.Parent;
        child = CreateAction("die", DieAction(info));
        node.AddChild(child);
        node = child;

        // flee 
        node = node.Parent;
        node = node.Parent;
        child = CreateSequence();
        node.AddChild(child);
        node = child;

        child = CreateCondition("has critical health", EnemyEvaluators.HasCriticalHealth);
        node.AddChild(child);
        node = child;

        node = node.Parent;
        child = CreateAction("flee", FleeAction(info));
        node.AddChild(child);
        node = child;

        // out of guard range
        node = node.Parent;
        node = node.Parent;
        child = CreateSequence();
        node.AddChild(child);
        node = child;

        child = CreateCondition("out of guard range", EnemyEvaluators.OutOfGuardRange);
        node.AddChild(child);
        node = child;

        node = node.Parent;
        child = CreateAction("go back", GoBackAction(info));
        node.AddChild(child);
        node = child;

        // has enemy
        node = node.Parent;
        node = node.Parent;

        return tree;
    }
}
