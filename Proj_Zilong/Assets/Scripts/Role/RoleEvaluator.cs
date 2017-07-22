using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleEvaluator {

    protected string evalName;
    protected RoleInfo roleInfo;    
    protected Func<bool> function;
    public Func<bool> evaluate;

    public RoleEvaluator(string name, Func<bool> evalFunction, RoleInfo info)
    {
        evalName = name;
        function = evalFunction;
        roleInfo = info;
        evaluate = Evaluate();
    }


    public Func<bool> Evaluate() 
    {
        return function;
    }
}
