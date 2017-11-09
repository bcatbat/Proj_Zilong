using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTreeNode {
    public enum NodeType
    {
        ACTION,
        CONDITION,
        SELECTOR,
        SEQUENCE
    };
        
    public RoleAction action;              // 行动
    public Func<RoleInfo,bool> evaluator;        // 评估(条件判定)
    public string nodeName;
    public List<BehaviorTreeNode> children;
    private BehaviorTreeNode parent;
    private NodeType type;

    public BehaviorTreeNode(string name,NodeType type)
    {
        nodeName = name;
        this.type = type;

        children = new List<BehaviorTreeNode>();        
    }

    public void AddChild(BehaviorTreeNode node)
    {
        if (children.Contains(node))
            return;
        else
        {
            children.Add(node);
            node.Parent = this;
        }
    }

    public int IndexOfChild(BehaviorTreeNode node)
    {
        return children.IndexOf(node);
    }

    public BehaviorTreeNode Child(int index)
    {
        return children[index];
    }

    public BehaviorTreeNode Parent
    {
        get { return parent; }
        set { parent = value; }
    }

    public NodeType Type
    {
        get { return type; }
        set { type = value; }
    }

    public int CountOfChildren()
    {
        return children.Count;
    }

    public void SetAction(RoleAction action)
    {
        this.action = action;
    }

    public void SetEvaluator(Func<RoleInfo,bool> evaluator)
    {
        this.evaluator = evaluator;
    }
}
