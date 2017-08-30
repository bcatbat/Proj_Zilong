using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCtrl : MonoBehaviour {
    // prefab
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform gunPos;

    // self
    private Rigidbody rb;    

    // bullet
    private float bulletVelocity = 20f;    

    // move
    private float h;
    private float v;    
    private Vector3 moveTar;

    // atk
    private float atkTick = float.MaxValue;
    private float gcdTime = 0.3f;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();        
    }    

    // 每帧更新
    void FixedUpdate () {
        if (PlayerInfo.Instance.IsControllable)
        {
            //MoveByKey();
            MoveByMouse();

            atkTick += Time.deltaTime;
            //Debug.Log(atkTick);
            Shot();
        }
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
        Vector3 curPos = transform.position;
        float move_speed = PlayerInfo.Instance.MoveSpeed;
        Vector3 tarPos = curPos + new Vector3(h, 0, v).normalized * move_speed*Time.deltaTime;
        rb.MovePosition(tarPos);

        // 转向
        Quaternion curRot = transform.rotation;
        Quaternion tarRot;
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
        // shift驻足
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveTar = transform.position;   // 停下来
        }
        else if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && !EventSystem.current.IsPointerOverGameObject())
                {
                    moveTar = hit.point;
                    moveTar.y = 0;
                }
            }
        }
        Vector3 curPos = transform.position;
        Vector3 direction = moveTar - curPos;
        if (direction.magnitude > 0.1f)
        {
            float move_speed = PlayerInfo.Instance.MoveSpeed;
            Vector3 tarPos = curPos + direction.normalized * move_speed * Time.deltaTime;            
            rb.MovePosition(tarPos);
            rb.rotation = Quaternion.LookRotation(direction);
        }

    }

    private void CheckGuiRaycastObjects()
    {        
        List<RaycastResult> list = new List<RaycastResult>();
        
    }

    // 攻击行为


    // 射击
    void Shot()
{
        if (Input.GetKey(KeyCode.J) && atkTick >= gcdTime)
        {
            CreateBullet();            

            atkTick = 0;
        }

        if((Input.GetMouseButton(1) 
            ||(Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButton(0)))
            && atkTick >= gcdTime)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) 
                && !EventSystem.current.IsPointerOverGameObject())
            {
                if (hit.collider != null)
                {
                    Vector3 tarPos = hit.point;
                    tarPos.y = 0;
                    transform.rotation = Quaternion.LookRotation(tarPos - transform.position);
                }
                moveTar = transform.position;   // 停下来
                CreateBullet();                

                atkTick = 0;
            }
        }
    }

    void CreateBullet()
    {
        //// 消耗气力
        //if (PlayerInfo.Instance.Mp < 5) {
        //    Debug.Log("没力气了");
        //    return;
        //}
        //PlayerInfo.Instance.Mp -= 2;

        GameObject bullet = Instantiate(bulletPrefab, gunPos.position, new Quaternion());
        // 归属
        BulletControl bc = bullet.GetComponent<BulletControl>();
        bc.country = PlayerInfo.Instance.country;
        bc.allyCountry = PlayerInfo.Instance.allyCountry;

        // 威力
        bc.atk = PlayerInfo.Instance.ActualStats.Atk;

        // 运动
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();        
        bulletRb.velocity = gunPos.forward * bulletVelocity;
        //rb.AddForce(-gunPos.forward * 80);
    }
}
