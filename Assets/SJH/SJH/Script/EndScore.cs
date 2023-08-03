using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEditor;


public class EndScore : MonoBehaviour
{
   

    public static EndScore instance;


    public GameObject GameClear;
    public GameObject Stage1;
    public GameObject Stage2;
    public GameObject Stage3;
    public GameObject Total;

    public float stage1score = 0 ;      //ÃÑÇÕ Á¡¼ö
    public float stage2score = 0 ;      //ÃÑÇÕ Á¡¼ö
    public float stage3score = 0 ;      //ÃÑÇÕ Á¡¼ö
    public float totalscore = 0 ;      //ÃÑÇÕ Á¡¼ö


    public GameObject Stage1Score; //ÃÑÇÕ Á¡¼ö
    public GameObject Stage2Score; //ÃÑÇÕ Á¡¼ö
    public GameObject Stage3Score; //ÃÑÇÕ Á¡¼ö
    public GameObject TotalScore; //ÃÑÇÕ Á¡¼ö

    public GameObject stage1text;
    public GameObject stage2text;
    public GameObject stage3text;
    public GameObject totaltext;



    void Start()
    {

        if (instance == null)
            instance = this;

        StartCoroutine(Result());
    }
    private void Update()
    {
    }
    public void UpdateScore()
    {

        //StartCoroutine(Result());
    }

    IEnumerator Result()
    {
        GameClear.SetActive(true);
        yield return new WaitForSeconds(3);
        Stage1.SetActive(true);
        StartCoroutine(MonsterCount(PlayerPrefs.GetFloat("Totalscore1"), stage1score));
        yield return new WaitForSeconds(3);
        Stage2.SetActive(true);
        StartCoroutine(TimeCount(PlayerPrefs.GetFloat("Totalscore2"), stage2score));
        yield return new WaitForSeconds(3);
        Stage3.SetActive(true);
        StartCoroutine(BonusCount(PlayerPrefs.GetFloat("Totalscore3"), stage3score));
        yield return new WaitForSeconds(3);
        Total.SetActive(true);
        StartCoroutine(TotalCount(stage1score+ stage2score + stage3score, totalscore));
    }

    IEnumerator MonsterCount(float target, float current)
    {
        float duration = 3f;
        float offset = (target - current) / duration;

        while (current < target)
        {
            current += offset * 0.01f;
            Stage1Score.GetComponent<TextMeshProUGUI>().text = ((int)current).ToString();
            yield return null;
        }

        stage1score = target;
        Stage1Score.GetComponent<TextMeshProUGUI>().text = ((int)current).ToString();
    }
    IEnumerator TimeCount(float target, float current)
    {
        float duration = 3f;
        float offset = (target - current) / duration;

        while (current < target)
        {
            current += offset * 0.01f;
            Stage2Score.GetComponent<TextMeshProUGUI>().text = ((int)current).ToString();
            yield return null;
        }

        stage2score = target;
        Stage2Score.GetComponent<TextMeshProUGUI>().text = ((int)current).ToString();
    }
    IEnumerator BonusCount(float target, float current)
    {
        float duration = 3f;
        float offset = (target - current) / duration;

        while (current < target)
        {
            current += offset * 0.01f;
            Stage3Score.GetComponent<TextMeshProUGUI>().text = ((int)current).ToString();
            yield return null;
        }

        stage3score = target;
        Stage3Score.GetComponent<TextMeshProUGUI>().text = ((int)current).ToString();
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


        TotalScore.GetComponent<TextMeshProUGUI>().text = ((int)current).ToString();
    }
}
