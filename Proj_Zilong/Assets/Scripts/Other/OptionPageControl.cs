using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPageControl : MonoBehaviour {
    public Scrollbar scrollbar;
    public ScrollRect scrollrect;
    
    private float tarValue;

    [SerializeField]
    private float smoothtime = 0.2f;
    [SerializeField]
    private float moveSpeed = 0f;

    private bool needMove = false;

    public void OnPointerDown()
    {
        needMove = false;
    }
    public void OnPointerUp()
    {
        if (scrollbar.value <= 0.25f)
        {
            tarValue = 0;
        }
        else if(scrollbar.value <= 0.50f)
        {
            tarValue = 0.33f;
        }else if(scrollbar.value <= 0.75f)
        {
            tarValue = 0.67f;
        }
        else
        {
            tarValue = 1f;
        }
        needMove = true;
        moveSpeed = 0f;
    }

    public void OnButtonClick(int value)
    {
        switch (value)
        {
            case 1:
                tarValue = 0;
                break;
            case 2:
                tarValue = 0.33f;
                break;
            case 3:
                tarValue = 0.67f;
                break;
            case 4:
                tarValue = 1.0f;
                break;            
            default:
                break;
        }
        needMove = true;
    }   

    private void Update()
    {
        if (needMove)
        {
            if (Mathf.Abs(scrollbar.value - tarValue) < 0.01f)
            {
                scrollbar.value = tarValue;
                needMove = false;
                return;
            }
            scrollbar.value = Mathf.SmoothDamp(scrollbar.value, tarValue, ref moveSpeed, smoothtime);
        }
    }
}
