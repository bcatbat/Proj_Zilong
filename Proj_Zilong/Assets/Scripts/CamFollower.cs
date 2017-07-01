using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollower : MonoBehaviour {
    private Transform m_Cam;    // 相机
    private Transform m_Pivot;  // 轴点
    private Transform m_Target; // 跟踪目标
    private bool m_AutoTargetPlayer = true; // 自动跟踪玩家角色
    private Vector3 offsetKite; // 
    private Vector3 offsetTps;
    private Quaternion angleKite;
    private Quaternion angleTps;


    public enum CamMode
    {
        Kite = 0,
        Tps
    }
    public CamMode camMode = CamMode.Kite;  

    void Awake()
    {
        // 找到相机和轴点
        m_Cam = GetComponentInChildren<Camera>().transform;
        m_Pivot = m_Cam.parent;

        // 初始化偏移量
        offsetKite = new Vector3(0, 0, -10);
        offsetTps = new Vector3(0, 0, -6);
        angleKite = Quaternion.Euler(45, 0, 0);
        angleTps = Quaternion.Euler(25, 0, 0);
    }
	
	void Start () {
        // 开启目标跟踪
        if (m_AutoTargetPlayer)
        {
            FindAndTargetPlayer();
        }
	}

    private void LateUpdate()
    {
        if (m_AutoTargetPlayer)
            FindAndTargetPlayer();
        FollowTarget(Time.deltaTime);
    }

    // 找到并锁定玩家
    void FindAndTargetPlayer()
    {
        var tar = GameObject.FindGameObjectWithTag("Player");
        if (tar)
        {
            m_Target = tar.transform;
        }
    }

    // 跟踪目标
    void FollowTarget(float deltaTime)
    {
        // 俯瞰模式下(Kite), 锁定轴点及摄像机相对位置.
        if(camMode == CamMode.Kite)
        {

        }

        // 第三人称模式下(TPS). 可绕轴点俯仰偏航, 摄像机相对位置固定
        if(camMode == CamMode.Tps)
        {

        }
    }

    void InitCamMode(CamMode mode)
    {
        if (mode==CamMode.Kite)
        {

        }
        if(mode == CamMode.Tps)
        {

        }
    }
}
