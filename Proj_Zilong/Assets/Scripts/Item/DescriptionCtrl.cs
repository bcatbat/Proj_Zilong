using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionCtrl : MonoBehaviour {
    private static DescriptionCtrl instance;
    
    private Text desText;

    private GameObject descriptionFrame;
    public static DescriptionCtrl Instance
    {
        get { return instance; }
    }   

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        descriptionFrame = GameObject.FindGameObjectWithTag("DescriptionFrame");
        desText = descriptionFrame.GetComponentInChildren<Text>();
        Hide();
    }

    public void Show(Image tar  )
    {
        SetPosition(tar);
        descriptionFrame.SetActive(true);
    }
    public void Hide()
    {
        descriptionFrame.SetActive(false);
    }

    public void SetText(string descriptionText)
    {
        desText.text = descriptionText;
    }

    private void SetPosition(Image tar)
    {
        //Debug.Log("set position"+ tar.name);

        float tarX = tar.transform.position.x; 
        float tarY = tar.transform.position.y;

        float tarWidth = tar.rectTransform.sizeDelta.x;
        //Debug.Log(tarWidth);

        float frameWidth = descriptionFrame.GetComponent<Image>().rectTransform.sizeDelta.x;
        float frameHeight = descriptionFrame.GetComponent<Image>().rectTransform.sizeDelta.y;

        float frameX = tarX + frameWidth / 2 + tarWidth/2;
        float frameY = tarY + frameHeight / 2;

        descriptionFrame.transform.position = new Vector3(frameX, frameY, 0f);
    }    
}
