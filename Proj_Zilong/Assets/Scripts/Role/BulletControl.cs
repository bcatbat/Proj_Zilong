using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {
    private float lifeTime = 3f;    // 生命周期
    public  int atk;                // 击打威力
    public RoleBelonging country;   // 归属
    public RoleBelonging allyCountry;   // 友军归属
    public bool destroyable = true; // 默认可销毁    

    // Use this for initialization
    void Start () {
        Destroy(this.gameObject, lifeTime);
	}
    
    private void OnTriggerEnter(Collider other)
    {
        // 击中角色 
        RoleInfo ri = other.gameObject.GetComponent<RoleInfo>();
        if (ri != null)
        {            
            if(ri.country != country &&
                ri.country != allyCountry)
            {
                Debug.Log("hit~");
                ri.Hurt(atk);
            }            
            // 销毁
            Destroy(this.gameObject);
        }

        // 击中子弹. 
        BulletControl bc = other.gameObject.GetComponent<BulletControl>();
        if(bc != null)
        {
            if(country != bc.country)
            {
                Debug.Log("子弹怼了");
                Destroy(this.gameObject);
            }
        }
    }
}
