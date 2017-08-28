using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {
    private float lifeTime = 3f;    // 生命周期
    public  int atk;                // 击打威力
    public RoleBelonging country;   // 归属

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, lifeTime);
	}
    
    private void OnTriggerEnter(Collider other)
    {
        RoleInfo ri = other.gameObject.GetComponent<RoleInfo>();
        if (ri != null)
        {            
            if(country != ri.country &&country != ri.allyCountry)
            {
                Debug.Log("hit~");
                ri.Hurt(atk);
            }            
            // 销毁
            Destroy(this.gameObject);
        }       
    }
}
