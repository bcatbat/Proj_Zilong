using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {
    private Rigidbody rb;

    private float h;
    private float v;
    private float move_speed = 5f;
 //   private float turn_speed = 0.1f;
    private Vector3 curPos;
    private Vector3 tarPos;
    private Quaternion curRot;
    private Quaternion tarRot;   
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        curPos = transform.position;
        tarPos = curPos;
        curRot = transform.rotation;
        tarRot = curRot;
    }

    void Start () {
		
	}
		
    // 每帧更新
	void FixedUpdate () {
        MoveByKey();
	}    

    
    void TurnByKey()
    {

    } 

    // 平面移动
    void MoveByKey()
    {
        // 获取输入
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        // 移动位置(h,0,v)
        curPos = transform.position;
        tarPos = curPos + new Vector3(h, 0, v) * move_speed*Time.deltaTime;
        rb.MovePosition(tarPos);

        // 转向
        curRot = transform.rotation;
        if (h != 0 || v != 0)
        {
            tarRot = Quaternion.LookRotation(new Vector3(h, 0, v));
        }
        else
        {
            tarRot = curRot;
        }
        rb.MoveRotation(tarRot);

    }

    // 鼠标点击行走.
    void MoveByMouse()
    {
        
    }

    // 攻击行为
}
