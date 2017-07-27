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
    protected Func<RoleInfo,Status> initialzeFunction;
    protected Func<RoleInfo,float, Status> updateFunction;
    protected Action<RoleInfo> cleanUpFunction;   

    protected RoleInfo roleInfo;
    //private BTNode.NodeType type;
    public Status status;

    public RoleAction(string name, Func<RoleInfo,Status> initFunc, Func<RoleInfo,float,Status> updateFunc, Action<RoleInfo> cleanUpFunc, RoleInfo info)
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
                initialzeFunction(roleInfo);
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
                status = updateFunction(roleInfo, deltaTime);
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
                cleanUpFunction(roleInfo);
            }
        }
        status = Status.UNINITIALIZED;
    }    
}
