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

    public bool isHit = false;

   

    


    public GameObject mb;
    public GameObject mb2;
    public Transform tr;
    public Transform tr2;


    public GameObject BossP1;
    public GameObject BossP2;
    public GameObject BossP3;


    private void LateUpdate()
    {
        if (transform.position.x >= 0.75f)
            flag *= -1;
        if (transform.position.x <= -0.75f)
            flag *= -1;

        transform.Translate(flag * speed * Time.deltaTime, 0, 0);
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

    }

    // Start is called before the first frame update
    void Start()
    {
        
        //보스 나타나면 Hide함수 1초뒤 동작
        Invoke("Hide", 1);
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
            }
            //발사체가 생성되는 시작 각도 설정을 위한 변수
            weightAngle += 1;

            //attackRate 시간만큼 대기
            yield return new WaitForSeconds(attackRate); //3초마다 원형 미사일 발사

        }
    }

    IEnumerator Think() //패턴 코루틴 함수임
    {
        yield return new WaitForSeconds(0.1f);

        int rendAction = Random.Range(0, 3);
        switch (rendAction)
        {
            case 0:

            case 1:
                StartCoroutine(Bosspattern1());


                break;

            case 2:

            case 3:
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
