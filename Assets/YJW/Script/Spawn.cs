using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public float ss = -2;   //몬스터 생성 x값 처음
    public float es = 2;    //몬스터 생성 x값 끝
    public float StartTime = 1;  //시작
    public float StartTime2 = 3;  //시작
    public float SpawnStop = 10; //스폰끝내는 시간

    public float StartTime3 = 9.5f;  //시작
    public GameObject monster;
    public GameObject monster2;
    public GameObject monster3;

    bool swi = true;
    bool swi2 = true;

    void Start()
    {
        StartCoroutine("RandomSpawn");
        Invoke("Stop", SpawnStop);


        StartCoroutine("RandomSpawn2");
        Invoke("Stop2", SpawnStop);

        StartCoroutine("RandomSpawn3");
        Invoke("Stop3", SpawnStop);

    }

    void Stop() 
    {
        swi = false;
        StopCoroutine("RandomSpawn");


        //두번째 몬스터 코루틴
        StartCoroutine("RandomSpawn2");

        //30초 뒤에 2번째 몬스터스폰을 멈추기

        Invoke("Stop2", SpawnStop + 20);

        
    }



    void Stop2()
    {
        swi2 = false;
        StopCoroutine("RandomSpawn2");
    }


    void Stop3()
    {
        swi = false;
        StopCoroutine("RandomSpawn");


    }


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
    IEnumerator RandomSpawn3()
    {
        while(swi)
        {
            //1초마다
            yield return new WaitForSeconds(StartTime3);
            //x값 랜덤
            
            //X값 랜덤값 y값 자기자신값
            Vector2 r = new Vector2(transform.position.x, transform.position.y);
            //몬스터 생성
            Instantiate(monster3,r,Quaternion.identity);
        }
    }
    IEnumerator RandomSpawn2()
    {
        while (swi2)
        {
            //1초마다
            yield return new WaitForSeconds(StartTime2);
            //x값 랜덤
            float X = Random.Range(ss, es);
            //X값 랜덤값 y값 자기자신값
            Vector2 r = new Vector2(X, transform.position.y);
            //몬스터 생성
            Instantiate(monster2, r, Quaternion.identity);
        }
    }



}
