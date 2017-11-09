using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBarControl : MonoBehaviour {
    public Slider myHpBar;
    public Slider myMpBar;
    public Slider myExpBar;
    public Text myHpBarText;
    public Text myMpBarText;
    

    private void Update()
    {
        myHpBar.maxValue = PlayerInfo.Instance.ActualStats.UpHp;
        myHpBar.minValue = 0f;
        myHpBar.value = PlayerInfo.Instance.Hp;

        myMpBar.maxValue = PlayerInfo.Instance.ActualStats.UpMp;        
        myMpBar.minValue = 0f;
        myMpBar.value = PlayerInfo.Instance.Mp;

        myExpBar.maxValue = PlayerInfo.Instance.UpExp;
        myExpBar.minValue = 0f;
        myExpBar.value = PlayerInfo.Instance.Exp;

        myHpBarText.text = myHpBar.value + "/" + myHpBar.maxValue;
        myMpBarText.text = myMpBar.value + "/" + myMpBar.maxValue;
    }
}
