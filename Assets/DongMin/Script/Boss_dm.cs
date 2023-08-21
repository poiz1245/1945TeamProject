using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class Boss_dm : MonoBehaviour
{
    public int hp = 1000;
    int maxHp;
    float speed = 0.35f;
    float Delay = 1.5f;
    public Transform ms;
    public GameObject bullet;
    float limitX = 0.3f;
    //float limitY = 4.47f;
    float x = 1;
    float y = 0;

    bool isLazer = false;
    public bool isBattle = false;
    bool isBodyActive = false;

    public int destroyArmCount = 0;


    [SerializeField]
    GameObject[] LazerWarningArea;
    [SerializeField]
    GameObject BossLazer;

    [SerializeField]
    GameObject bossHead;

    [SerializeField]
    GameObject octopus;
    [SerializeField]
    int octopusHp = 1000;
    int maxOctopusHp;
    [SerializeField]
    SpriteRenderer[] octopusSprite;
    [SerializeField]
    GameObject shadow1;
    [SerializeField]
    GameObject shadow2;
    [SerializeField]
    Image bossUI;
    [SerializeField]
    GameObject exprosion;

    [SerializeField]
    SpriteRenderer electricHead;
    [SerializeField]
    GameObject electricBall;
    [SerializeField]
    GameObject leftLeg;
    [SerializeField]
    GameObject rightLeg;

    [SerializeField]
    GameObject electricMiniBall;


    Coroutine corBossBullet;
    Coroutine AttackCoroutine;
    Coroutine corBossPattern;
    Coroutine corOctopusPattern;
    Coroutine corOctoSkill;
    Coroutine corElectricAttack;

    //[SerializeField]
    //GameObject FireWarningArea;
    //[SerializeField]
    //GameObject BossFire;

    // Start is called before the first frame update
    void Start()
    {
        CameraShake.instance.AudioPlay();

        transform.GetComponent<BoxCollider2D>().enabled = false;

        LazerWarningArea[0].SetActive(false);
        BossLazer.SetActive(false);

        bossUI = GameObject.Find("BossUI").GetComponent<Image>();

        maxHp = hp;
        maxOctopusHp = octopusHp;

        BossUI_dm.instance.StartSet(BossUI_dm.HP.body, maxHp);
        BossUI_dm.instance.StartSet(BossUI_dm.HP.octopus, maxOctopusHp);

        //문어괴물 패턴 시작
        //corOctopusPattern = StartCoroutine(OctopusPattern());

        StartCoroutine(BossStart());


    }

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
                GameObject clone = Instantiate(bullet, transform.position, Quaternion.identity);
                //발사체 이동 방향(각도)
                float angle = weightAngle + intervalAngle * i;
                //발사체 이동 방향 (벡터)
                //Cos(각도), 라디안 단위의 각도 표현을 위해 PI/180을 곱함
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f); // Mathf.PI / 180.0f == Mathf.Deg2Rad
                //Sin(각도), 라디안 단위의 각도 표현을 위해 PI/180을 곱함
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);
                //발사체 이동 방향 설정
                clone.GetComponent<MonsterBullet_dm>().Move(new Vector2(x, y));
            }
            //발사체가 생성되는 시작 각도 설정을 위한 변수
            weightAngle += 1;

            //attackRate 시간만큼 대기
            yield return new WaitForSeconds(attackRate); //3초마다 원형 미사일 발사
        }
    }

    IEnumerator ElectricAttack()
    {
        float attackRate = 1;//공격주기
        int count = 30;    //발사체 생성 갯수
        float intervalAngle = 360 / count;  //발사체 사이의 각도
        float weightAngle = 0; //가중되는 각도 (항상 같은 위치로 발사하지 않도록 설정)
        int maxAttackCount = 5;
        int attackCount = 0;

        //원 형태로 방사하는 발사체 생성 (count 개수 만큼)
        while (attackCount< maxAttackCount)
        {
            for (int i = 0; i < count; ++i)
            {
                //발사체 생성
                GameObject clone = Instantiate(electricMiniBall, electricBall.transform.position, Quaternion.identity);
                //발사체 이동 방향(각도)
                float angle = weightAngle + intervalAngle * i;
                //발사체 이동 방향 (벡터)
                //Cos(각도), 라디안 단위의 각도 표현을 위해 PI/180을 곱함
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f); // Mathf.PI / 180.0f == Mathf.Deg2Rad
                //Sin(각도), 라디안 단위의 각도 표현을 위해 PI/180을 곱함
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);
                //발사체 이동 방향 설정
                clone.GetComponent<MonsterBullet_dm>().Move(new Vector2(x, y));
            }
            //발사체가 생성되는 시작 각도 설정을 위한 변수
            weightAngle += 1;

            attackCount++;

            //attackRate 시간만큼 대기
            yield return new WaitForSeconds(attackRate); //3초마다 원형 미사일 발사
        }

        leftLeg.SetActive(false);
        rightLeg.SetActive(false);
        electricBall.SetActive(false);
        leftLeg.SetActive(true);
        rightLeg.SetActive(true);
        CameraShake.instance.ShakeSwitchOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBattle)
        {
            transform.Translate(new Vector2(x, y) * speed * Time.deltaTime);

            transform.position = new Vector2(
                Mathf.Clamp(transform.position.x, -limitX, limitX),
                transform.position.y);

            if (transform.position.x >= limitX || transform.position.x <= -limitX)
            {
                x = -x;
            }
        }

        if (destroyArmCount >= 2 && !isBodyActive)
        {
            isBodyActive = true;

            BossUI_dm.instance.SetActiveFalseSlider(BossUI_dm.HP.leftArm);
            BossUI_dm.instance.SetActiveFalseSlider(BossUI_dm.HP.rightArm);

            BossUI_dm.instance.CorStartSliderSet(BossUI_dm.HP.body);

            transform.GetComponent<BoxCollider2D>().enabled = true;

            corBossPattern = StartCoroutine(BossPattern());
        }
    }

    //private void OnBecameInvisible()
    //{
    //    Destroy(gameObject);
    //}

    IEnumerator BossStart()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.down * 11f, ForceMode2D.Impulse);

        float curTime = 0;

        while (true)
        {
            curTime += Time.deltaTime;

            if (curTime >= 0.1f)
            {
                curTime = 0;
                rb.velocity = rb.velocity * 0.8f;

                //if (rb.velocity.y >= -0.01f)
                if (transform.position.y <= 2.63)
                {
                    rb.velocity = Vector2.zero;
                    break;
                }
            }

            yield return null;
        }

        //보스 나타나면 Hide함수 1초 뒤 동작
        Invoke("Hide", 1);
        corBossBullet = StartCoroutine("BossBullet");

    }

    public void Damage(int attack)
    {
        hp -= attack;

        BossUI_dm.instance.Damage(BossUI_dm.HP.body, hp);

        if (hp <= 0)
        {
            //Destroy(gameObject);
            isBattle = false;
            transform.GetComponent<BoxCollider2D>().enabled = false;

            if (AttackCoroutine != null)
            {
                StopCoroutine(AttackCoroutine);
            }
            if (corBossPattern != null)
            {
                StopCoroutine(corBossPattern);
            }
            if (corBossBullet != null)
            {
                StopCoroutine(corBossBullet);
            }

            bossHead.SetActive(false);
            for (int i = 0; i < LazerWarningArea.Length; i++)
            {
                LazerWarningArea[i].SetActive(false);
            }
            BossLazer.SetActive(false);

            exprosion.SetActive(true);

            //CameraShake.instance.ShakeSwitchOff();

            //문어괴물 패턴 시작
            corOctopusPattern = StartCoroutine(OctopusPattern());
        }
    }

    public void OctopusDamage(int attack)
    {
        octopusHp -= attack;

        BossUI_dm.instance.Damage(BossUI_dm.HP.octopus, octopusHp);

        if (octopusHp <= 0)
        {
            isBattle = false;
            octopus.GetComponent<CapsuleCollider2D>().enabled = false;

            exprosion.SetActive(true);

            StartCoroutine(OctopusDead());
        }
    }

    IEnumerator OctopusDead()
    {
        //float curTime = 0;
        CameraShake.instance.ShakeSwitchOff();

        StopCoroutine(corOctoSkill);
        StopCoroutine(corElectricAttack);


        yield return new WaitForSeconds(2f);
        exprosion.SetActive(false);

        while (true)
        {
            //보스 점점 작이지다 사라짐
            transform.localScale = new Vector3(transform.localScale.x - (0.4f * Time.deltaTime), transform.localScale.y - (0.4f * Time.deltaTime), 1);

            if (transform.localScale.x <= 0)
            {
                transform.localScale = new Vector3(0, 0, 1);
                
                break;
            }

            yield return null;
        }
        CameraShake.instance.ShakeSwitchOff();

        ScoreManager.instance.Bonus++;
        BossUI_dm.instance.StageClear();

        gameObject.SetActive(false);
    }

    void Hide()
    {
        //보스 텍스트 객체이름 검색해서 끄기
        if (GameObject.Find("BossWarning") != null)
        {
            GameObject.Find("BossWarning").SetActive(false);
        }

        isBattle = true;
    }

    IEnumerator BossBullet()
    {
        float weightAngle = 1;

        while (true)
        {
            ms.transform.Rotate(0, 0, weightAngle);

            for (int i = 0; i < 18; i++)
            {
                ms.transform.Rotate(0, 0, 20);
                Instantiate(bullet, ms.position, ms.transform.rotation);
            }

            //Instantiate(bullet, ms.position, Quaternion.identity);
            //Instantiate(bullet, ms2.position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
    }

    public void corBossBulletStart()
    {
        corBossBullet = StartCoroutine("BossBullet");
    }

    public IEnumerator AttackWarning(GameObject[] attackArea, GameObject BossAttack)
    {
        SpriteRenderer[] attackAreaSprite = new SpriteRenderer[attackArea.Length];

        for (int i = 0; i < attackArea.Length; i++)
        {
            attackArea[i].SetActive(true);
            attackAreaSprite[i] = attackArea[i].GetComponent<SpriteRenderer>();
        }


        float curTime = 0;
        //int count = 10;
        //float wfSeconds = 1 / (float)count;
        //int curCount = 0;
        float maxColorA = 80 / 255.0f;
        float curColorA = 0;
        int blinkCount = 3;

        for (int i = 0; i < blinkCount; i++)
        {
            while (curTime < 1f)
            {
                curTime += Time.deltaTime;
                curColorA = maxColorA * curTime;
                for (int j = 0; j < attackAreaSprite.Length; j++)
                {
                    attackAreaSprite[j].color = new Color(attackAreaSprite[j].color.r,
                        attackAreaSprite[j].color.g, attackAreaSprite[j].color.b, curColorA);
                }

                yield return null;
            }
            curTime = 0;

            while (curTime < 1f)
            {
                curTime += Time.deltaTime;
                curColorA = maxColorA * (1.0f - curTime);
                for (int j = 0; j < attackAreaSprite.Length; j++)
                {
                    attackAreaSprite[j].color = new Color(attackAreaSprite[j].color.r,
                        attackAreaSprite[j].color.g, attackAreaSprite[j].color.b, curColorA);
                }

                yield return null;
            }
            curTime = 0;
        }

        //Debug.Log(Time.time);
        for (int i = 0; i < attackArea.Length; i++)
        {
            attackArea[i].SetActive(false);
        }

        StopCoroutine(corBossBullet);

        BossAttackOn(BossAttack);
    }


    void BossAttackOn(GameObject BossAttack)
    {
        BossAttack.SetActive(true);
    }

    IEnumerator BossPattern()
    {
        yield return new WaitForSeconds(5f);
        while (true)
        {
            AttackCoroutine = StartCoroutine(AttackWarning(LazerWarningArea, BossLazer));

            yield return new WaitForSeconds(15f);
        }
    }

    IEnumerator OctopusPattern()
    {
        float shadow1Speed = 18f;
        float shadow2Speed = 13f;
        float curTime = 0;
        float color = 0;
        float downTime = 5f;
        float octStartScale = 8.5602f;
        bool warningTextOn = false;

        //CameraShake.instance.ShakeSwitchOff();

        yield return new WaitForSeconds(2f);

        exprosion.SetActive(false);

        yield return new WaitForSeconds(1.3f);

        while (true)
        {
            curTime += Time.deltaTime;

            shadow1.transform.Translate(Vector2.down * shadow1Speed * Time.deltaTime);

            if (curTime >= 2.5f)
            {
                break;
            }

            yield return null;
        }

        curTime = 0;
        while (true)
        {
            curTime += Time.deltaTime;

            shadow2.transform.Translate(Vector2.up * shadow2Speed * Time.deltaTime);

            if (curTime >= 2.5f)
            {
                break;
            }

            yield return null;
        }

        shadow1.SetActive(false);
        shadow2.SetActive(false);

        bossUI.color = Color.black;
        octopus.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        curTime = 0;
        while (true)
        {
            curTime += Time.deltaTime;
            color = curTime / downTime;

            for (int i = 0; i < octopusSprite.Length; i++)
            {   //문어 검은색에서 점점 밝아짐
                octopusSprite[i].color = new Color(color, color, color);
            }

            //UI패널 페이드 아웃
            bossUI.color = new Color(0, 0, 0, 1 - color);

            //문어 크기 축소
            octopus.transform.localScale = new Vector3(octStartScale / (color * 10), octStartScale / (color * 10), 1);

            if (curTime >= downTime * 0.7f && !warningTextOn)
            {
                warningTextOn = true;
                BossUI_dm.instance.OctopusWarning();
                BossUI_dm.instance.SetActiveFalseSlider(BossUI_dm.HP.body);

                BossUI_dm.instance.CorStartSliderSet(BossUI_dm.HP.octopus);
            }
            else if (curTime >= downTime)
            {
                octopus.GetComponent<CapsuleCollider2D>().enabled = true;

                yield return new WaitForSeconds(1f);

                isBattle = true;

                break;
            }


            yield return null;
        }

        yield return new WaitForSeconds(7f);
        while (true)
        {
            corOctoSkill = StartCoroutine(OctoSkill());

            yield return new WaitForSeconds(15f);
        }
    }

    IEnumerator OctoSkill()
    {
        float curTime = 0;
        float colorA = 0;
        float chargeTime = 3f;

        while (true)
        {
            curTime += Time.deltaTime;

            colorA = curTime / chargeTime;

            electricHead.color = new Color(electricHead.color.r, electricHead.color.g, electricHead.color.b, colorA);

            if (curTime >= chargeTime)
            {
                leftLeg.GetComponent<LegMove2_dm>().isSkill = true;
                rightLeg.GetComponent<LegMove2_dm>().isSkill = true;
                electricBall.SetActive(true);
                CameraShake.instance.ShakeSwitch();

                corElectricAttack = StartCoroutine(ElectricAttack());

                break;
            }

            yield return null;
        }
        yield return new WaitForSeconds(5f);
        
        electricHead.color = new Color(electricHead.color.r, electricHead.color.g, electricHead.color.b, 0);
    }

    private void OnDisable()
    {
        if (corBossBullet != null)
        {
            StopCoroutine(corBossBullet);
        }
        if (AttackCoroutine != null)
        {
            StopCoroutine(AttackCoroutine);
        }
        if (corBossPattern != null)
        {
            StopCoroutine(corBossPattern);
        }

    }
}
