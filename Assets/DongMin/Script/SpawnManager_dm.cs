using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager_dm : MonoBehaviour
{
    float ss = -2.3f; //몬스터 생성 x값 처음
    float es = 2.3f; //몬스터 생성 y값 처음
    public float delayTime = 2; //시작
    public float spawnStop = 5; //스폰 끝내는 시간
    public GameObject monster;
    public GameObject monster2;
    public GameObject Boss;

    bool swi = true;
    bool swi2 = true;

    [SerializeField]
    GameObject textBossWarning; //보스 등장 텍스트 오브젝트

    private void Awake()
    {
        //보스 등장 텍스트 비활성화
        textBossWarning.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine("RandomSpawn");
        Invoke("Stop", spawnStop);

        //RandomSpawn3();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Stop()
    {
        swi = false;
        StopCoroutine("RandomSpawn");

        //두번째 몬스터 코루틴
        StartCoroutine("RandomSpawn2");

        //30초뒤에 2번째 몬스터 스폰을 멈추기
        Invoke("Stop2", spawnStop + 5);
    }

    void Stop2()
    {
        swi2 = false;
        StopCoroutine("RandomSpawn2");

        //보스 출현
        textBossWarning.SetActive(true);
        RandomSpawn3();
    }

    IEnumerator RandomSpawn()
    {
        yield return new WaitForSeconds(5f);

        while (swi)
        {
            //1초마다
            yield return new WaitForSeconds(0.7f);
            //x값 랜덤
            float x = Random.Range(ss, es);
            //x값:랜덤값 y값:자기자신값
            Vector2 r = new Vector2(x, transform.position.y);
            //몬스터 생성
            Instantiate(monster, r, Quaternion.identity);
        }
    }

    IEnumerator RandomSpawn2()
    {
        while (swi2)
        {
            //1초마다
            yield return new WaitForSeconds(0.7f);
            //x값 랜덤
            float x = Random.Range(ss, es);
            //x값:랜덤값 y값:자기자신값
            Vector2 r = new Vector2(x, transform.position.y);
            //몬스터 생성
            Instantiate(monster2, r, Quaternion.identity);
        }
    }
    void RandomSpawn3()
    {
        Instantiate(Boss, new Vector2(transform.position.x, transform.position.y + 3f), Quaternion.identity);
    }
}
