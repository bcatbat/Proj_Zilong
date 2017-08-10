using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionManager : MonoBehaviour {
    // 单例之
    private static DescriptionManager instance;
    public static DescriptionManager Instance
    {
        get { return instance; }
    }

    private Text descriptionText;           // 描述文字
    [SerializeField]private GameObject descriptionFrame;    // 描述框,手动赋值

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
        descriptionText = descriptionFrame.GetComponentInChildren<Text>();
    }

    private void Start()
    {         
        Hide(); // 初始隐藏
    }

    public void Show(Image img)
    {
        // 
        SetDescriptionFramePosition(img);
        descriptionFrame.SetActive(true);
    }
    public void Hide()
    {
        descriptionFrame.SetActive(false);
    }

    public void SetDescription(string descriptionText)
    {
        this.descriptionText.text = descriptionText;
    }

    private void SetDescriptionFramePosition(Image tar)
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
