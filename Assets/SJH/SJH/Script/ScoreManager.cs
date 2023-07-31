using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEditor;


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

    public GameObject stageclear;
    public GameObject killtext;
    public GameObject timetext;
    public GameObject bonustext;
    public GameObject totaltext;

    void Start()
    {
        if (instance == null)
            instance = this;
    }

    public void UpdateScore()
    {
        
        StartCoroutine(Result());

        Kill.GetComponent<TextMeshProUGUI>().text = monsterkill.ToString();
        Time.GetComponent<TextMeshProUGUI>().text = Mathf.FloorToInt(GameManagerSJ.Instance.GameTime).ToString();


        //timescore = 1000 - Mathf.FloorToInt(GameManagerSJ.Instance.GameTime); //시간 적을수록 추가 점수
        //monsterscore = monsterkill * 10;
        //Bonus = Bonus + GameManagerSJ.Instance.player.Heart; //보스잡아서 올라간 보너스점수에 플레이어 남은 목숨 더하기
        //totalscore = (timescore + monsterscore) * Bonus; //시간점수, 몬스터점수 더하고 보너스 곱하기

        //TimeScore.GetComponent<TextMeshProUGUI>().text = timescore.ToString();
        //KillScore.GetComponent<TextMeshProUGUI>().text = monsterscore.ToString();
        //BonusScore.GetComponent<TextMeshProUGUI>().text = Bonus.ToString();
        //TotalScore.GetComponent<TextMeshProUGUI>().text = totalscore.ToString();
    }

    IEnumerator Result()
    {
        stageclear.SetActive(true);
        yield return new WaitForSeconds(3);
        killtext.SetActive(true);
        StartCoroutine(Count(monsterkill * 10, monsterscore));
        yield return new WaitForSeconds(3);
        timetext.SetActive(true);
        StartCoroutine(TimeCount(1000 - Mathf.FloorToInt(GameManagerSJ.Instance.GameTime), timescore));
        yield return new WaitForSeconds(3);
        bonustext.SetActive(true);
        StartCoroutine(BonusCount(Bonus + GameManagerSJ.Instance.player.Heart, Bonus));
        yield return new WaitForSeconds(3);
        totaltext.SetActive(true);
        StartCoroutine(TotalCount((timescore + monsterscore) * Bonus, totalscore));
    }

    IEnumerator Count(float target, float current)
    {
        float duration = 3f;
        float offset = (target - current) / duration;

        while (current < target)
        {
            current += offset * 0.01f;
            KillScore.GetComponent<TextMeshProUGUI>().text = ((int)current).ToString();
            yield return null;
        }

        current = target;
        KillScore.GetComponent<TextMeshProUGUI>().text = ((int)current).ToString();
    }
    IEnumerator TimeCount(float target, float current)
    {
        float duration = 3f;
        float offset = (target - current) / duration;

        while (current < target)
        {
            current += offset * 0.01f;
            TimeScore.GetComponent<TextMeshProUGUI>().text = ((int)current).ToString();
            yield return null;
        }

        current = target;
        TimeScore.GetComponent<TextMeshProUGUI>().text = ((int)current).ToString();
    }
    IEnumerator BonusCount(float target, float current)
    {
        float duration = 0.1f;
        float offset = (target - current) / duration;

        while (current < target)
        {
            current += offset * 0.01f;
            BonusScore.GetComponent<TextMeshProUGUI>().text = ((int)current).ToString();
            yield return null;
        }

        current = target;
        BonusScore.GetComponent<TextMeshProUGUI>().text = ((int)current).ToString();
    }
    IEnumerator TotalCount(float target, float current)
    {
        float duration = 3f;
        float offset = (target - current) / duration;

        while (current < target)
        {
            current += offset * 0.01f;
            TotalScore.GetComponent<TextMeshProUGUI>().text = ((int)current).ToString();
            yield return null;
        }

        current = target;
        TotalScore.GetComponent<TextMeshProUGUI>().text = ((int)current).ToString();
    }
}
