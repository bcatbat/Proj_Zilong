using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotKeyManager : MonoBehaviour {
    public GameObject mainMenuPanel;    // 主菜单框

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SetupMainMenuPanel();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShutdownWindows();
        }
    }

    private void SetupMainMenuPanel()
    {
        if (mainMenuPanel.activeSelf == false)
        {
            mainMenuPanel.SetActive(true);
            //Time.timeScale = 0;
        }
        else
        {
            mainMenuPanel.SetActive(false);
            //Time.timeScale = 1f;
            DescriptionManager.Instance.Hide();
        }
    }

    private void ShutdownWindows()
    {
        mainMenuPanel.SetActive(false);
    }
}
