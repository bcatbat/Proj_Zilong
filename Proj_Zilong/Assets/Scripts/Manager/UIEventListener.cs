using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UIEventListener : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler,IDragHandler,IBeginDragHandler,IEndDragHandler,IDropHandler
{
    public delegate void UIEventProxy(GameObject gb);

    public event UIEventProxy OnMouseLeftClick;
    public event UIEventProxy OnMouseRightClick;
    public event UIEventProxy OnMouseEnter;
    public event UIEventProxy OnMouseExit;
    public event UIEventProxy OnMouseBeginDrag;
    public event UIEventProxy OnMouseEndDrag;
    public event UIEventProxy OnMouseDrag;
    public event UIEventProxy OnMouseDrop;

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("OnPointerClick");
        if(eventData.pointerId == -1)
        {
            if (OnMouseLeftClick != null)
                OnMouseLeftClick(gameObject);
        }else if(eventData.pointerId == -2)
        {
            if (OnMouseRightClick != null)
                OnMouseRightClick(gameObject);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointerEnter");
        if (OnMouseEnter != null)
            OnMouseEnter(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("OnPointerExit");
        if (OnMouseExit != null)
            OnMouseExit(gameObject);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (OnMouseDrag != null)
            OnMouseDrag(gameObject);
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        // 只允许用左键拖拽
        if (eventData.pointerId == -1)
        {
            if (OnMouseBeginDrag != null)
                OnMouseBeginDrag(gameObject);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(OnMouseDrop != null)
        {
            OnMouseDrop(eventData.pointerDrag);
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (OnMouseEndDrag != null)
            OnMouseEndDrag(gameObject);
    }
}
