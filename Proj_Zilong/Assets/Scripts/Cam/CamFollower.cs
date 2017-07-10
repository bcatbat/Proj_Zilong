using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollower : MonoBehaviour {
    private Transform m_Cam;    // 相机
    private Transform m_Pivot;  // 轴点
    private Transform m_Target; // 跟踪目标
    private bool m_AutoTargetPlayer = true; // 自动跟踪玩家角色
    private float m_fovKite = 10f;
    private float m_fovTps = 10f;
    private float m_smooth = 5f;
    private float m_smoothMouse = 5f;
    private float m_mouseX;
    private float m_mouseY;    
    private float m_anglePitchMin = 10f;
    private float m_anglePitchMax = 30f;
    private Vector3 m_offsetKite; 
    private Vector3 m_offsetTps;
    private Quaternion m_angleKite;
    private Quaternion m_angleTps;
    private Quaternion m_tarPivotRot;

    public enum CamMode
    {
        Kite = 0,
        Tps
    }
    public CamMode camMode = CamMode.Kite;
    public CamMode prevMode;
    public float XSensor = 2f;
    public float YSensor = 2f;

    void Awake()
    {
        // 找到相机和轴点
        m_Cam = GetComponentInChildren<Camera>().transform;
        m_Pivot = m_Cam.parent;

        // 初始化偏移量
        m_offsetKite = new Vector3(0, 0, -50);
        m_offsetTps = new Vector3(0, 0, -50);
        m_angleKite = Quaternion.Euler(30, 0, 0);
        m_angleTps = Quaternion.Euler(25, 0, 0);
        prevMode = camMode;
    }
	
	void Start () {
        // 开启目标跟踪
        if (m_AutoTargetPlayer)
        {
            m_Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        InitCamMode(camMode);
        m_tarPivotRot = m_Pivot.localRotation;
    }

    private void LateUpdate()
    {
        if(camMode != prevMode)
        {
            Debug.Log("change Camera Mode");
            InitCamMode(camMode);
            prevMode = camMode;
        }
        FollowTarget(Time.deltaTime,camMode);

        if (camMode == CamMode.Tps)
        {
            ViewRotation(Time.deltaTime);
        }
    }

    // 跟踪目标
    void FollowTarget(float deltaTime,CamMode mode)
    {
        // 跟踪玩家
        Vector3 tarPos = new Vector3(m_Target.position.x, m_Pivot.localPosition.y, m_Target.position.z);
        m_Pivot.localPosition = Vector3.Lerp(m_Pivot.localPosition, tarPos, m_smooth*deltaTime);
    }      

    void ViewRotation(float deltaTime)
    {
        m_mouseX = Input.GetAxis("Mouse X") * XSensor;
        m_mouseY = Input.GetAxis("Mouse Y") * YSensor;

        m_tarPivotRot.eulerAngles += new Vector3(-m_mouseY, m_mouseX, 0f);

        if (m_tarPivotRot.eulerAngles.x >= m_anglePitchMax)
            m_tarPivotRot.eulerAngles = new Vector3(m_anglePitchMax, m_tarPivotRot.eulerAngles.y, 0f);
        if (m_tarPivotRot.eulerAngles.x <= m_anglePitchMin)
            m_tarPivotRot.eulerAngles = new Vector3(m_anglePitchMin, m_tarPivotRot.eulerAngles.y, 0f);

        m_Pivot.localRotation = Quaternion.Slerp(m_Pivot.localRotation, m_tarPivotRot, m_smoothMouse * deltaTime);
    }

    void InitCamMode(CamMode mode)
    {
        switch (mode)
        {
            case CamMode.Kite:
                // 相机模式
                Camera.main.orthographic = true;
                Camera.main.orthographicSize = m_fovKite;
                // 轴点
                m_Pivot.localRotation = m_angleKite;
                m_Pivot.localPosition = new Vector3(0, 2f, 0);
                // 相机
                m_Cam.localPosition = m_offsetKite;
                break;
            case CamMode.Tps:
                Camera.main.orthographic = false;
                Camera.main.fieldOfView = m_fovTps;

                m_Pivot.localRotation = m_angleTps;
                m_Pivot.localPosition = new Vector3(0, 2f, 0);

                m_Cam.localPosition = m_offsetTps;                
                break;
        }
    }
}
