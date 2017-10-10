using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 协程延时自定义 : MonoBehaviour {
    private void Start()
    {
        StartCoroutine(WaitOrPass());
    }

    //private void Update()
    //{
    //if (Input.GetMouseButtonUp(0))
    //{
    //    Debug.Log("left mouse button up");
    //    StartCoroutine(WaitForMouseDown());
    //}
    //}

    public IEnumerator WaitForMouseDown()
    {
        yield return new WaitForMouseDown();
        Debug.Log("Right mouse button pressed");
    }
    public IEnumerator WaitOrPass()
    {
        Debug.Log("start:" + Time.time);
        yield return new WaitForSecondsUnlessClicked(5f,"Fire1");
        Debug.Log("over:"+Time.time);
    }        
}

public class WaitForMouseDown : CustomYieldInstruction
{
    public override bool keepWaiting
    {
        get
        {
            return !Input.GetMouseButtonDown(1);
        }
    }

    public WaitForMouseDown()
    {
        Debug.Log("Waiting for Mouse right button down");
    }
}

public class WaitWhile : CustomYieldInstruction
{
    Func<bool> m_Predicate;
    public override bool keepWaiting
    {
        get
        {
            return m_Predicate();            
        }
    }
    public WaitWhile(Func<bool> predicate)
    {
        m_Predicate = predicate;
    }
}

