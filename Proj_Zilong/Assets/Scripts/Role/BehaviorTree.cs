using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree {
    private BehaviorTreeNode node;
    private BehaviorTreeNode currentNode;
    private RoleInfo roleInfo;

    public void SetNode(BehaviorTreeNode node)
    {
        this.node = node;
    }

    public BehaviorTree(RoleInfo info)
    {
        node = null;
        currentNode = null;
        roleInfo = info;
    }
    
    public bool EvaluateSelector(BehaviorTreeNode node,float deltaTime, out BehaviorTreeNode outputNode)
    {
        // 遍历左右子节点, 直到找到一个可以运行的子节点
        int count = node.children.Count;
        outputNode = null;
        BehaviorTreeNode on;

        for (int i = 0;i < count; i++)
        {
            BehaviorTreeNode child = node.children[i];

            if(child.Type == BehaviorTreeNode.NodeType.ACTION)
            {
                outputNode = child;
                return true;
            }else if(child.Type == BehaviorTreeNode.NodeType.CONDITION)
            {
                Debug.Log("单一的条件节点是不对的");
                return false;
            }else if(child.Type == BehaviorTreeNode.NodeType.SELECTOR)
            {

                bool result = EvaluateSelector(child, deltaTime,out on);
                if (result)
                {
                    return true;
                }
            }else if(child.Type == BehaviorTreeNode.NodeType.SEQUENCE)
            {
                bool result = EvaluateSequence(child, deltaTime, out on);
                if(result)
                {
                    return true;
                }
            }
        }        
        return false;
    }

    public bool EvaluateSequence(BehaviorTreeNode node, float deltaTime, out BehaviorTreeNode outputNode, int index = 0)
    {
        int count = node.children.Count;
        BehaviorTreeNode on;
        outputNode = null;

        for(int i = index; i < count; i++)
        {
            BehaviorTreeNode child = node.children[i];

            if (child.Type == BehaviorTreeNode.NodeType.ACTION)
            {
                outputNode = child;
                return true; 
            }
            else if (child.Type == BehaviorTreeNode.NodeType.CONDITION)
            {
                bool result = child.evaluator(roleInfo);

                if(!result)
                {
                    return false;
                }                
            }
            else if (child.Type == BehaviorTreeNode.NodeType.SELECTOR)
            {
                bool result = EvaluateSelector(child, deltaTime,out on);
                if (!result)
                {
                    return false;
                }else if(result && on != null)
                {
                    return result;
                }
            }
            else if (child.Type == BehaviorTreeNode.NodeType.SEQUENCE)
            {
                bool result = EvaluateSequence(child, deltaTime, out on);

                if (!result)
                {
                    return false;
                }else if(result && on != null)
                {
                    return result;
                }
            }
        }
        return true;
    }

    public BehaviorTreeNode EvaluateNode(BehaviorTreeNode node, float deltaTime)
    {
        BehaviorTreeNode on;

        if (node.Type == BehaviorTreeNode.NodeType.ACTION)
        {
            return node;
        }
        else if (node.Type == BehaviorTreeNode.NodeType.CONDITION)
        {
            Debug.Log("根节点是条件, 有误. 理应已经在Sequence中处理了");
            return null;
        }
        else if (node.Type == BehaviorTreeNode.NodeType.SELECTOR)
        {

            bool result = EvaluateSelector(node, deltaTime, out on);
            if (result)
            {
                return on;
            }
        }
        else if (node.Type == BehaviorTreeNode.NodeType.SEQUENCE)
        {
            bool result = EvaluateSequence(node, deltaTime, out on);
            if (result)
            {
                return on;
            }
        }
        return null;
    }

    public BehaviorTreeNode ContinueEvaluation(BehaviorTreeNode node,float deltaTime)
    {
        BehaviorTreeNode parentNode = node.Parent;
        BehaviorTreeNode childNode = node;

        while(parentNode!= null)
        {
            if(parentNode.Type == BehaviorTreeNode.NodeType.SEQUENCE)
            {
                int childIndex = parentNode.IndexOfChild(childNode);

                if(childIndex < parentNode.CountOfChildren())
                {
                    BehaviorTreeNode on;
                    EvaluateSequence(parentNode, deltaTime, out on, childIndex + 1);
                    return on;
                }
            }

            childNode = parentNode;
            parentNode = childNode.Parent;
        }
        return null;
    }

    public void Update(float deltaTime) {
        if(currentNode == null)
        {
            currentNode = EvaluateNode(node, deltaTime);
        }

        if(currentNode != null)
        {
            RoleAction.Status status = currentNode.action.status;
            if(status == RoleAction.Status.UNINITIALIZED)
            {
                currentNode.action.Initialize();
            }else if(status == RoleAction.Status.TERMINATED)
            {
                currentNode.action.CleanUp();

                currentNode = ContinueEvaluation(currentNode, deltaTime);
            }else if(status == RoleAction.Status.RUNNING)
            {
                currentNode.action.Update(deltaTime);
            }
        }
    }
}
