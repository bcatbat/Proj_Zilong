using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HackingGameManager : MonoBehaviour {
    // singleton
    private static HackingGameManager instance;
    public static HackingGameManager Instance{ get { return instance; } }

    // prefabs
    public GameObject planePrefab;
    public GameObject wallCubePrefab;    
    public GameObject playerPrefab;
    public GameObject bossPrefab;
    public GameObject enemyPrefab;

    // environment
    public int iWidth;
    public int iHeight;

    // walls
    private GameObject walls;

    //// role list
    //public HackingPlayerControl player;
    //public List<HackingBossControl> bossList;
    //public List<HackingEnemyControl> enemyList;    

    private void Awake()
    {
        instance = this;
        ReadConditions();   // 从其他位置读取信息
        walls = new GameObject("environment");
        //enemyList = new List<HackingEnemyControl>();

        InitEnvironment(iWidth,iHeight);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            ResetGame();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            ExitGame();
        }
    }

    private void InitEnvironment(int width, int height  )
    {
        // plane
        InitPlane(width, height);
        // walls
        InitWalls(width, height);
        // player
        InitPlayer();
        // enemy
        InitEnemy();
        // boss
        InitBoss();
    }

    private void InitPlane(int width, int height)
    {
        var p = Instantiate(planePrefab);
        p.transform.localScale = new Vector3(width / 5, 1, height / 5);

        // 
        Camera.main.transform.position = new Vector3(0, width, 0);

    }

    private void InitWalls(int width, int height)
    {
        float w = width;
        float h = height;
        if(width<=0 || height <= 0)
        {
            Debug.LogError("场景尺寸不能为负");
        }

        for (int i = 0; i < width - 1; ++i)
        {
            Vector3 curPos = new Vector3(-(w - 1) / 2 + i, 0, -(h - 1) / 2);
            GameObject curWallCube = Instantiate(wallCubePrefab, curPos, new Quaternion());
            curWallCube.transform.parent = walls.transform;
        }
        for (int j = 0; j < height - 1; ++j)
        {
            Vector3 curPos = new Vector3((w - 1) / 2, 0, -(h - 1) / 2 + j);
            GameObject curWallCube = Instantiate(wallCubePrefab, curPos, new Quaternion());
            curWallCube.transform.parent = walls.transform;
        }
        for (int i = width - 1; i > 0; --i)
        {
            Vector3 curPos = new Vector3(-(w - 1) / 2 + i, 0, (h - 1) / 2);
            GameObject curWallCube = Instantiate(wallCubePrefab, curPos, new Quaternion());
            curWallCube.transform.parent = walls.transform;
        }
        for (int j = height - 1; j > 0; --j)
        {
            Vector3 curPos = new Vector3(-(w - 1) / 2, 0, -(h - 1) / 2 + j);
            GameObject curWallCube = Instantiate(wallCubePrefab, curPos, new Quaternion());
            curWallCube.transform.parent = walls.transform;
        }
    }

    private void InitPlayer()
    {
        Vector3 spawnPoint = new Vector3(0, 0, -iHeight / 4);
        Instantiate(playerPrefab, spawnPoint, new Quaternion());
    //    player = p.GetComponent<HackingPlayerControl>();
    }

    private void InitBoss()
    {
        Vector3 spawnPoint = new Vector3(0, 0, iHeight / 4);
        Instantiate(bossPrefab, spawnPoint, new Quaternion());
      //  bossList.Add(b.GetComponent<HackingBossControl>());
    }

    private void InitEnemy()
    {
        Vector3 spawnPoint = new Vector3(-iWidth / 4, 0, iHeight / 4);
        Instantiate(enemyPrefab, spawnPoint, new Quaternion());
        spawnPoint = new Vector3(iWidth / 4, 0, iHeight / 4);
        Instantiate(enemyPrefab, spawnPoint, new Quaternion());
        //enemyList.Add(e.GetComponent<HackingEnemyControl>());
    }

    // 读取小游戏的基本初始条件
    public void ReadConditions()
    {

    }

    // 退出 or 胜利.

    // 重开
    public void ResetGame()
    {
        SceneManager.LoadScene("hacking game");
    }

    // 退出
    public void ExitGame()
    {
        SceneManager.LoadScene("0-StartTitle");
    }
}
