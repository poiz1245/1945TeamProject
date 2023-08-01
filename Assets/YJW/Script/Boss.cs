using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using static UnityEditor.PlayerSettings;

public class Boss : MonoBehaviour
{
    public float HP = 2000;

    public GameObject effect;

    int flag = 1;
    int speed = 2;
    Vector3 pos; //현재위치

    float delta = 1.0f; // 좌(우)로 이동가능한 (x)최대값


    public bool isHit = false;

   string bossMode = "mode1"; //보스의 1페이즈 mode를 스트링으로 정의하였음.  

        

    


    public GameObject mb;
    public GameObject mb2;
    public Transform tr;
    public Transform tr2;


    public GameObject BossP1;
    public GameObject BossP2;
    public GameObject BossP3;


    private void LateUpdate()
    {
       
        //2페이지 이동으로 구현.
        Vector3 v = pos;

        v.x += delta * Mathf.Sin(Time.time * speed);

        // 좌우 이동의 최대치 및 반전 처리를 이렇게 한줄에 멋있게 하네요.

        transform.position = v;
    }

    public void Damage(float attack)
    {
        HP -= attack;

        Debug.Log("데미지 받았음");
        StartCoroutine(CoolHit());

        if (HP <= 0)
        {
            HP = 0;

            Destroy(gameObject);


            Instantiate(effect, transform.position, Quaternion.identity);

            //    Destroy(effect, 0.5f);

        }
    }


    private void Awake()
    {
        StartCoroutine(Think());
       //transform.Find("BPpos3") .gameObject.SetActive(false);

    }

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        //보스 나타나면 Hide함수 1초뒤 동작
        Invoke("Hide", 1);
      
            Start2Paze();
        
    }

    private void Start2Paze()
    {
        StartCoroutine(BossMissle()); //코루틴 실행 함수 동작
        StartCoroutine(CircleFire()); //코루틴 실행 함수 동작
    }

    void Hide()
    {
        //보스 텍스트 객체이름 검색해서 끄기
        GameObject.Find("TextBossWarning").SetActive(false);
    }

    IEnumerator BossMissle()
    {
        while (true)
        {
            //미사일 두개 
            Instantiate(mb, tr.position, Quaternion.identity);
            Instantiate(mb, tr2.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }

    //원방향으로 미사일 발사
    IEnumerator CircleFire()
    {
        float attackRate = 3;//공격주기
        int count = 30;    //발사체 생성 갯수
        float intervalAngle = 360 / count;  //발사체 사이의 각도
        float weightAngle = 0; //가중되는 각도 (항상 같은 위치로 발사하지 않도록 설정)


        //원 형태로 방사하는 발사체 생성 (count 개수 만큼)
        while (true)
        {
            for (int i = 0; i < count; ++i)
            {
                //발사체 생성
                GameObject clone = Instantiate(mb2, transform.position, Quaternion.identity);
                //발사체 이동 방향(각도)
                float angle = weightAngle + intervalAngle * i;
                //발사체 이동 방향 (벡터)
                //Cos(각도), 라디안 단위의 각도 표현을 위해 PI/180을 곱함
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                //Sin(각도), 라디안 단위의 각도 표현을 위해 PI/180을 곱함
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);
                //발사체 이동 방향 설정
                //clone.GetComponent<BossBullet>().Move(new Vector2(x, y));

                Debug.Log("발사되었음");
            }
            //발사체가 생성되는 시작 각도 설정을 위한 변수
            weightAngle += 1;

            //attackRate 시간만큼 대기
            yield return new WaitForSeconds(attackRate); //3초마다 원형 미사일 발사

        }
    }

    IEnumerator Think() //패턴 코루틴 함수임 2페이지에서 사용될 거임.
    {
        yield return new WaitForSeconds(0.1f);

        int rendAction = Random.Range(0, 5);
        switch (rendAction)
        {
            case 0:

            case 1:
                StartCoroutine(Bosspattern1());

                break;

            case 2:

            case 3:

            case 4:
                StartCoroutine(Bosspattern2());

                break;
           



        }

        IEnumerator Bosspattern1()
        {
           
            BossP1.GetComponent<BossPattern1>().Shot();
            yield return new WaitForSeconds(1f);
            StartCoroutine(Think());
        }

        IEnumerator Bosspattern2()
        {
            
            BossP2.GetComponent<BossPattern2>().Shot();
            yield return new WaitForSeconds(1f);
            StartCoroutine(Think());
        }

      





    }

    IEnumerator CoolHit()
    {
        var hit = transform.GetChild(0).transform.GetComponent<SpriteRenderer>();
        isHit = true;
        hit.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        hit.color = Color.white;
        isHit = false;
    }
    
}
