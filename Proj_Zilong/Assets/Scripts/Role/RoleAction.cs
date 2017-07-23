using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleAction {
    public enum Status
    {
        RUNNING,
        TERMINATED,
        UNINITIALIZED
    }

    protected string actionName;
    protected Action initialzeFunction;
    protected Func<float,Status> updateFunction;
    protected Action cleanUpFunction;   

    protected RoleInfo roleInfo;
    //private BTNode.NodeType type;
    public Status status;

    public RoleAction(string name, Action initFunc, Func<float,Status> updateFunc, Action cleanUpFunc, RoleInfo info)
    {
        actionName = name;

        initialzeFunction = initFunc;
        updateFunction = updateFunc;
        cleanUpFunction = cleanUpFunc;

        roleInfo = info;

        status = Status.UNINITIALIZED;
    }

    public void Initialize()
    {
        if(status == Status.UNINITIALIZED)
        {
            if(initialzeFunction != null)
            {
                initialzeFunction();
            }
        }
        status = Status.RUNNING;
    }

    public Status Update(float deltaTime)
    {
        if (status == Status.TERMINATED)
        {
            return Status.TERMINATED;
        }
        else if (status == Status.RUNNING)
        {
            if(updateFunction != null)
            {
                status = updateFunction(deltaTime);
                Debug.Log(status);
            }
            else
            {
                status = Status.TERMINATED;
            }
        }
        return status;
    }

    public void CleanUp()
    {
        if(status == Status.TERMINATED)
        {
            if(cleanUpFunction != null)
            {
                cleanUpFunction();
            }
        }
        status = Status.UNINITIALIZED;
    }    
}
