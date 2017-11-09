using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MsgControl : MonoBehaviour {
    private static MsgControl instance;
    public static MsgControl Instance
    {
        get { return instance; }
    }

    public Text ConsoleText;
    public Scrollbar vScrollBar;
    public bool autoBottom = true;
    private string content;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start ()
    {
        if(ConsoleText == null)
        {
            Debug.LogError("消息面板未设置");
        }
        if(vScrollBar == null)
        {
            Debug.LogError("滑块未设置");
        }
        content = "开始!\n";
        RefreshText();
    }

    // 清空控制台数据
    public void Clear()
    {
        content = "";
        RefreshText();
    }

    // 在原文基础上添加一条显示
    public void Log(string text)
    {
        content += text+"\n";
        RefreshText();
    }

    private void RefreshText()
    {
        ConsoleText.text = content;
        if(autoBottom)
            vScrollBar.value = 0f;
    }
}
