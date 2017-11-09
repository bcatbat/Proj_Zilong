using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragMe : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private GameObject draggingIcon;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag " + eventData.pointerDrag);

        // 找到画布
        var canvas = FindInParent<Canvas>(gameObject);
        if (canvas == null) return;

        draggingIcon = new GameObject("Dragging Icon");
        draggingIcon.transform.SetParent(canvas.transform, false);
        draggingIcon.transform.SetAsLastSibling();  // 放到最后一项, 最上方显示

        var image = draggingIcon.AddComponent<Image>();
        var curImage = GetComponent<Image>();
        image.sprite = curImage.sprite;
        image.color = curImage.color;
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        draggingIcon.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {        
        if(eventData != null)
        {
            Debug.Log("End Drag " + eventData.pointerDrag);
            Destroy(draggingIcon);
        }
    }
    
    private static T FindInParent<T>(GameObject go) where T : Component
    {
        // 目标空,返回空
        if (go == null) return null;

        // 目标身上已有T, 则返回T
        var component = go.GetComponent<T>();
        if (component != null) return component;

        // 循环找父项
        Transform t = go.transform.parent;
        // 循环条件. 父项存在, T不存在
        while (t != null && component == null)
        {
            component = t.gameObject.GetComponent<T>();
            t = t.parent;
        }
        return component;
    }
}
