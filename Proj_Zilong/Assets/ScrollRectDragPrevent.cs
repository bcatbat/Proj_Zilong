using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollRectDragPrevent : MonoBehaviour {
    public void OnDrag()
    {
        // 什么都不写 看看效果
        Debug.Log("OnDrag事件触发");
    }
    public void OnClick()
    {
        Debug.Log("点击事件");
    }
}
