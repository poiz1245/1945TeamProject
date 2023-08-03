using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Boss : MonoBehaviour
{
    public float HP = 2000;

    public GameObject effect; //이 객체가 파괴 될때마다 호출될 이펙트
    public GameObject lastBoss; //이 객체가 완전히 파괴될때 호출될 오브젝트
    public GameObject self_destruct;
    public GameObject lazer;

    public GameObject HitedEffect;


    int speed = 2; //이 오브젝트에 대한 속도 변수
    Vector3 pos; //현재위치

    float delta = 1.0f; // 좌(우)로 이동가능한 (x)최대값

    public float timer = 0;


    //맞았는지에 대해 체크 하는 변수

    string bossMode = "mode1"; //보스의 1페이즈 mode를 스트링으로 정의하였음.  






    public GameObject mb;
    public GameObject mb2;
    public Transform tr;
    public Transform tr2;
    public Transform lazerZone;
    //보스 통상 패턴에 사용될 위치와 거기에 사용될 탄 오브젝트


    public GameObject BossP1;
    public GameObject BossP2;
    public GameObject BossP3;
    //보스 패턴에 사용될 오브젝트를 저장
    GameObject BossP4;

    

    
    public GameObject[] SummonsMon;
   
    public int breackStack = 0;

    public int summonStack = 0;

    public int destruckCount = 0;

    public int StartPaze2Conut = 0;

    public bool CoolAtk = false;

    public bool isHit = false;



    private void Awake()
    {
       
        //transform.Find("BPpos3") .gameObject.SetActive(false);
        BossP4 = transform.GetChild(7).gameObject;
        //BossP4.gameObject.SetActive(false);
        BossP3.gameObject.SetActive(false);

    }

    void Start()
    {
        pos = transform.position;
        //보스 나타나면 Hide함수 1초뒤 동작
        //Invoke("Hide", 1);

        BossUI_dm.instance.StartSet_ver2();

    }



    void Update()
    {
        timer += Time.deltaTime;
        if (HP <= 4000 && HP > 3000)
        {

            if (summonStack == 0)
            {

                Summons();

            }

            if(timer % 10 == 0)
            {
                summonStack = 0;
                timer = 0;
            }
        }
        else if (HP <= 3000 && HP > 2000)
        {
            //기존의 다른 패턴 적용요망.
            //한번만 발동되게 하는게 좋을듯.
            if (destruckCount == 0) 
            {
                Instantiate(self_destruct, transform.position, Quaternion.identity);
                destruckCount++;
            }
            if(timer % 10 == 0)
            {
                destruckCount = 0;
                timer = 0;
            }
            
        }
        else if(HP > 1000 && HP <= 2000)
        {

            BossP4.gameObject.SetActive(false);
            BossP3.gameObject.SetActive(true);
            if (StartPaze2Conut == 0)
            {
                Start2Paze();
                Instantiate(lazer, lazerZone.transform.position, Quaternion.identity);
                StartPaze2Conut++;
                timer = 0;
                
              
            }


            if (timer % 10 > 9.97f)
            {
                Instantiate(lazer, lazerZone.transform.position, Quaternion.identity);
            }

        }
        else if(HP>0 && HP<=1000)
        {
            if (CoolAtk == false)
            {
                StartCoroutine(Think());
                StartCoroutine(CoolTime());
            }

        }


    }


    void LateUpdate()
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

        BossUI_dm.instance.Damage(BossUI_dm.HP.body, HP);

       // Debug.Log("데미지 받았음");
        StartCoroutine(CoolHit());

        if (HP <= 0)
        {
            HP = 0;

            ScoreManager.instance.Bonus++;
            ScoreManager.instance.monsterkill++;


            Instantiate(lastBoss, transform.position, Quaternion.identity);
            BossUI_dm.instance.SetActiveFalseSlider(BossUI_dm.HP.body);
            BossUI_dm.instance.CorStartSliderSet(BossUI_dm.HP.octopus);

            Instantiate(effect, transform.position, Quaternion.identity);

            Destroy(gameObject);
            //    Destroy(effect, 0.5f);

        }
    }



    private void Start2Paze()
    {
        StartCoroutine(BossMissle()); //코루틴 실행 함수 동작
        StartCoroutine(CircleFire()); //코루틴 실행 함수 동작
    }

   /* void Hide()
    {
        //보스 텍스트 객체이름 검색해서 끄기
        GameObject.Find("BossWarning").SetActive(false);
        //GameObject.Find("TextBossWarning").SetActive(false);
    }*/

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
        yield return new WaitForSeconds(0.5f);

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






    }

    IEnumerator Bosspattern1()
    {

        BossP1.GetComponent<BossPattern1>().Shot();
        yield return new WaitForSeconds(1f);
        // StartCoroutine(Think());
    }

    IEnumerator Bosspattern2()
    {

        BossP2.GetComponent<BossPattern2>().Shot();
        yield return new WaitForSeconds(1f);
        //StartCoroutine(Think());
    }



    IEnumerator CoolTime()
    {
        CoolAtk = true;
        yield return new WaitForSeconds(0.5f);
        CoolAtk = false;
    }


    void Summons()
    {
        //Debug.Log("소환되었음");
        for (int i = 0; i < 4; i++)
        {
            Instantiate(SummonsMon[i], transform.position, Quaternion.identity);
        }
        summonStack++;
       
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
