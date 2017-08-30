using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAction : MonoBehaviour {

    // shoot action
    public static void ShootCleanUp(RoleInfo info) { }

    public static RoleAction.Status ShootInit(RoleInfo info)
    {
        return RoleAction.Status.RUNNING;
    }

    public static RoleAction.Status ShootUpdate(RoleInfo info, float deltaTime)
    {

        (info as RobotInfo).atkTick += deltaTime;
        // 死亡, 终止
        if (info.Hp <= info.ActualStats.UpHp * 0.1)
            return RoleAction.Status.TERMINATED;

        if (info.Target == null)
            return RoleAction.Status.TERMINATED;

        // 距离超出射程
        if ((Vector3.Distance(info.transform.position, info.Target.position) > info.SlashRange))
            return RoleAction.Status.TERMINATED;

        // 射击
        if (info.IsAlive)
        {
            Debug.Log("Robot Shoot!!!");
            // 朝向玩家
            //info.transform.LookAt(PlayerInfo.Instance.gameObject.transform);
            info.transform.LookAt(info.Target);
            // info.transform.rotation = Quaternion.LookRotation(PlayerInfo.Instance.transform.position - info.transform.position);
            Shot(info as RobotInfo);
        }

        return RoleAction.Status.RUNNING;
    }
        
    static void Shot(RobotInfo info)
    {
        if (info.atkTick >= info.gcdTime)
        {
            if (info.isLeftGun)
            {
                GunFire(info.gunL,info);
                info.isLeftGun = false;
            }
            else
            {
                GunFire(info.gunR,info);
                info.isLeftGun = true;
            }

            info.atkTick = 0;
        }
    }

    static void GunFire(Transform gun,RobotInfo info)
    {
        

        GameObject bullet = Instantiate(info.bulletPrefab, gun.position, new Quaternion());
        // 归属
        BulletControl bc = bullet.GetComponent<BulletControl>();
        bc.country = info.country;
        bc.allyCountry = info.allyCountry;

        // 威力
        bc.atk = info.ActualStats.Atk;

        // 运动
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = gun.forward * info.bulletVelocity;
        //rb.AddForce(-gunPos.forward * 80);
    }
}
