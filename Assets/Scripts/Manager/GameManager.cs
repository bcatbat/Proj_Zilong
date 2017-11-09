using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private GameManager instance;
    public GameManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void ExitGame()
    {
        Application.Quit();        
    }

    public void StartGame()
    {
        GlobalInfo.nextScene = "base";
        SceneManager.LoadScene("loading01");
        //SceneManager.LoadScene("base");
    }

    public void MiniGame()
    {
        GlobalInfo.nextScene = "hacking game";
        SceneManager.LoadScene("loading01");
    }
}
