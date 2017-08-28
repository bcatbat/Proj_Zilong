using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyInfo : RoleInfo {

    public override Stats BasicStats
    {
        get
        {
            basicStats.UpHp = 100;
            basicStats.UpMp = 0;
            basicStats.Atk = 0;
            basicStats.Def = 10 + level * (level - 2) / 10;
            basicStats.Spd = 0;
            basicStats.Luk = 0;
            return basicStats;
        }
    }

    // 木桩打不死
    protected override void Die()
    {        
        hp = ActualStats.UpHp;
    }

    private void Awake()
    {
        id = 20;
        country = RoleBelonging.other;
        roleName = "木桩";
    }

    protected override void Start()
    {
        base.Start();
        Level = PlayerInfo.Instance.Level;
        //Debug.Log(ActualStats);

        Hp = BasicStats.UpHp;
        Mp = BasicStats.UpMp;
    }
}
