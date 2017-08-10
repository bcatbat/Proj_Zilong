using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropMe : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData != null)
        {
            Debug.Log("Drop " + eventData);
            // 换一下图片
            Image srcImage = eventData.pointerDrag.GetComponent<Image>();
            Image desImage = GetComponent<Image>();
            ExchangeImage(srcImage, desImage);
        }
    }

    private void ExchangeImage(Image src, Image des)
    {
        Sprite tempSprite;
        Color tempColor;

        tempSprite = des.sprite;
        tempColor = des.color;
        des.sprite = src.sprite;
        des.color = src.color;
        src.sprite = tempSprite;
        src.color = tempColor;
    }
}
