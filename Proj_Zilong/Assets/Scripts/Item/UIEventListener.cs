using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UIEventListener : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    public delegate void UIEventProxy(GameObject gb);

    public event UIEventProxy OnMouseLeftClick;
    public event UIEventProxy OnMouseRightClick;
    public event UIEventProxy OnMouseEnter;
    public event UIEventProxy OnMouseExit;
    public event UIEventProxy OnBeginDrag;
    public event UIEventProxy OnEndDrag;
    public event UIEventProxy OnDrag;

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
        if (OnDrag != null)
            OnDrag(gameObject);
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (OnBeginDrag != null)
            OnBeginDrag(gameObject);
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (OnEndDrag != null)
            OnEndDrag(gameObject);
    }
}
