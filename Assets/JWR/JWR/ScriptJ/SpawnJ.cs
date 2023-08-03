using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnJ : MonoBehaviour
{
    public float ss = -2;   //몬스터 생성 x값 처음
    public float es = 2;    //몬스터 생성 x값 끝
    public float StartTime = 1;  //시작
    public float SpawnStop = 40; //스폰끝내는 시간
    public GameObject monster;
    public GameObject monster2;
    public GameObject BossJ;


    bool swi = true;
    bool swi2 = true;

    [SerializeField]
    GameObject Intro; //텍스트 오브젝트
    public GameObject BossWarning;
    //public GameObject GameClear;

    private void Awake()
    {
        BossWarning.SetActive(false);
        //GameClear.SetActive(false);
        //StartCoroutine(ShowIntroAndHide());
    }

    /*IEnumerator ShowIntroAndHide()
    {
        //Intro.SetActive(true); // Intro를 보이게 설정

        // 5초 대기
        //yield return new WaitForSeconds(3);

        //Intro.SetActive(false); // Intro를 숨기기 설정
    }*/


    void Start()
    {
        StartCoroutine("RandomSpawn");
        Invoke("Stop", SpawnStop);
    }

    void Stop()
    {
        swi = false;

        StopCoroutine("RandomSpawn");

        //두번째 몬스터 코루틴
        StartCoroutine("RandomSpawn2");

        //30초뒤에 2번째 몬스터스폰을 멈추기
        Invoke("Stop2", SpawnStop + 40);



    }
    void WarningAct()
    {
        BossWarning.SetActive(false);
    }
    void Stop2()
    {
        swi2 = false;
        StopCoroutine("RandomSpawn2");

        Vector3 pos = new Vector3(0, 3.0f, 0);

        BossWarning.SetActive(true);

        //보스출현
        Instantiate(BossJ, pos, Quaternion.identity);
        Invoke("WarningAct", 1.5f);




    }
    //코루틴으로 랜덤하게 생성하기
    IEnumerator RandomSpawn()
    {
        while (swi)
        {
            //1초마다
            yield return new WaitForSeconds(StartTime);
            //x값 랜덤
            float X = Random.Range(ss, es);
            //X값 랜덤값 y값 자기자신값
            Vector2 r = new Vector2(X, transform.position.y);
            //몬스터 생성
            Instantiate(monster, r, Quaternion.identity);
        }
    }
    //코루틴으로 랜덤하게 생성하기
    IEnumerator RandomSpawn2()
    {
        while (swi2)
        {
            //1초마다
            yield return new WaitForSeconds(StartTime + 1);
            //x값 랜덤
            float X = Random.Range(ss, es);
            //X값 랜덤값 y값 자기자신값
            Vector2 r = new Vector2(X, transform.position.y);
            //몬스터2 생성
            Instantiate(monster2, r, Quaternion.identity);
        }
    }
}
