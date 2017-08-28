using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotInfo : RoleInfo {

    public override Stats BasicStats
    {
        get
        {
            basicStats.UpHp = 100 + level * (level - 1);
            basicStats.UpMp = 50 + 20 * level;
            basicStats.Atk = 10 + (level - 2) * level / 10;
            basicStats.Def = 10 + (level - 2) * level / 10;
            basicStats.Spd = 10 + (level - 2) * level / 10;
            basicStats.Luk = 10 + (level - 2) * level / 10;
            return basicStats;
        }
    }

    private void Awake()
    {
        id = 30;
        country = RoleBelonging.Wei;
        roleName = "木人";
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
