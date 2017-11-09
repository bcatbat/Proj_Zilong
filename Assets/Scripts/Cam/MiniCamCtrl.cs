using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCamCtrl : MonoBehaviour {
    //[SerializeField] private float fov = 8f;
    private Transform m_player;
    [SerializeField] private Vector3 m_offset;

    private void Awake()
    {
        // 锁定玩家
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();        
    }

    private void Start()
    {
        m_offset = new Vector3(0, 10, 0);
    }
    private void LateUpdate()
    {
        FollowTarget(Time.deltaTime);
    }

    private void FindAndTargetPlayer()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void FollowTarget(float deltatime)
    {
        if (m_player != null)
        {
            transform.position = m_player.position + m_offset;
        }
    }
}
