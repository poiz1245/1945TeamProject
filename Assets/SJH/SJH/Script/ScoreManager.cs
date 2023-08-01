using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    //public float time = 0; // 경과 시간
    public float monsterkill = 0; // 몬스터 잡은 갯수
    public float timescore = 0; //시간에 따른 점수
    public float monsterscore = 0; // 몬스터 파괴 점수
    public float Bonus = 0; //보너스 점수, 남은 목숨갯수, 보스킬
    public float totalscore = 0; //총합 점수


    public GameObject Time; // 경과 시간
    public GameObject Kill; // 몬스터 잡은 갯수
    public GameObject TimeScore; //시간에 따른 점수
    public GameObject KillScore; // 몬스터 파괴 점수
    public GameObject BonusScore; //보너스 점수, 남은 목숨갯수, 보스킬
    public GameObject TotalScore; //총합 점수


    void Start()
    {
        if (instance == null)
            instance = this;

        //time = GameManagerSJ.Instance.GameTime;
    }


    public void UpdateScore()
    {
        timescore = 1000- GameManagerSJ.Instance.GameTime; //시간 적을수록 추가 점수
        monsterscore = monsterkill * 10;
        Bonus = Bonus + GameManagerSJ.Instance.player.Heart; //보스잡아서 올라간 보너스점수에 플레이어 남은 목숨 더하기
        totalscore = (timescore + monsterscore) * Bonus; //시간점수, 몬스터점수 더하고 보너스 곱하기


        Kill.GetComponent<Text>().text = monsterkill.ToString();
        Time.GetComponent<Text>().text = GameManagerSJ.Instance.GameTime.ToString();
        TimeScore.GetComponent<Text>().text = timescore.ToString();
        KillScore.GetComponent<Text>().text = monsterscore.ToString();
        BonusScore.GetComponent<Text>().text = Bonus.ToString();
        TotalScore.GetComponent<Text>().text = totalscore.ToString();
    }
    void Update()
    {
        Debug.Log(timescore);
        
    }
}
