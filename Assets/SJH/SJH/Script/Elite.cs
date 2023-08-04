using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Elite : MonoBehaviour
{
    public Transform SpawnPos1;
    public Transform SpawnPos2;
    public Transform SpawnPos3;
    public Transform SpawnPos4;
    public Transform gunPos;
    public Transform gun2Pos;
    public Transform gun3Pos;
    public Transform BossPos;


    public GameObject dieEffect;
    public GameObject Rintercept;
    public GameObject Lintercept;
    public GameObject Rintercept2;
    public GameObject Lintercept2;
    public GameObject Lazor;
    public GameObject bullet;
    public GameObject StarBullet;
    public float Speed = 5f;
    public float MaxHp = 10000;
    public float currunt_Hp = 1000;


    float per_Hp;
    Vector3 playerPos;
    GameObject beam;
    GameObject ranBullet;
    GameObject gliter;


    public bool check;
    bool check2;
    bool check_bullet;
    bool check_starbullet;
    bool check_Lazor;
    public bool check_smallstar;

    bool dead = false;
    private void Awake()
    {
        beam = GameObject.Find("EffBeam").transform.Find("beam").gameObject;
        ranBullet = GameObject.Find("EffBeam").transform.Find("ranBullet").gameObject;
        gliter = GameObject.Find("EffBeam").transform.Find("gliter").gameObject;
        currunt_Hp = MaxHp;

    }
    void Start()
    {
        CameraSJ.instance.AudioPlay();
        BossUI_dm.instance.StartSet_ver3(BossUI_dm.HP.body, (int)MaxHp);
        Invoke("Pattern1", 3);
    }

    private void Pattern1()
    {
        check = true;
        check_bullet = true;

        SpawnInterCepter();
        StartCoroutine(CreatBullet());

    }

    void Pattern2() //회전미사일, 인터셉터 스폰 스탑, 좌우 움직임
    {
        check = false;
        check_bullet = false;
        check_starbullet = true;
        check_Lazor = true;
        check_smallstar = true;
        StopCoroutine(CreatBullet());

        //기모으는 효과 글리터 활성화 시키고, 캐넌소환, 레이저 발사
        gliter.SetActive(true);
        LazorStart();
        //beam.SetActive(true);      
        StartCoroutine(CreatStarBullet());
    }
    void Pattern3() //레이저스톱, 인터셉터소환, 캐넌발사, 회전미사일발사 ,위치는 상단 중앙
    {
        check = true;
        check_bullet = true;
        check_Lazor = false;
        check_smallstar = true;

        SpawnInterCepter();
        StartCoroutine(CreatBullet());
        beam.SetActive(false);

    }

    void Pattern4()
    {
        check = false;
        check_starbullet = false;
        check_Lazor = false;
        check_bullet = false;
        check_smallstar = false;

        StopCoroutine(CreatBullet());
        ranBullet.SetActive(true);
        gliter.SetActive(false);
    }

    void SpawnInterCepter()
    {
        check2 = true;
        StartCoroutine(Spawn());
        Invoke("SpawnStop", 1f);

    }
    void SpawnStop()
    {
        check2 = false;
        StopCoroutine(Spawn());
        Invoke("SpawnInterCepter", 3f);
    }
    void LazorStart()
    {
        if (check_Lazor)
        {
            beam.SetActive(true);
            Invoke("LazorStop", 3);
        }
    }

    void LazorStop()
    {
        beam.SetActive(false);
        Invoke("LazorStart", 3);
    }

    private void Update()
    {

        per_Hp = (currunt_Hp / MaxHp) * 100;

        if (transform.position.y >= 3.6)
        {
            transform.Translate(Vector2.up * Speed * Time.deltaTime);
        }
        else if (per_Hp > 40 && per_Hp <= 70)
        {
            transform.Translate(Vector2.right * Speed * Time.deltaTime);

            if (check == true)
            {
                Pattern2();
            }
        }
        else if (per_Hp > 20 && per_Hp <= 40)
        {
            transform.position = new Vector3(0, 3.6f, 0);

            if (check == false)
            {
                Pattern3();
            }
        }
        else if (per_Hp <= 20)
        {

            transform.position = new Vector3(0, 0, 0);
            if (check == true)
            {
                Pattern4();
            }
        }

        if (currunt_Hp <= 0 && !dead)
        {
            dead = true;
            dieEffect.SetActive(true);
            Destroy(gameObject, 3);

            ScoreManager.instance.Bonus++;
        }
    }
    /*  IEnumerator Enemy2Spawn()
      {
          while (swi2)
          {
              yield return new WaitForSeconds(5);
              float X = Random.Range(startPos, endPos);
              Vector2 spawnSpot = new Vector2(X, transform.position.y);
              GameObject monster = GameManagerSJ.Instance.pool.Get(2);
              monster.transform.position = spawnSpot;
          }
      }*/
    void OnDestroy()
    {
        ScoreManager.instance.UpdateScore();
    }

    IEnumerator Spawn()
    {
        while (check)
        {
            while (check2)
            {
                yield return new WaitForSeconds(0.1f);
                GameObject intercepter = GameManagerSJ.Instance.pool.Get(7);
                GameObject intercepter1 = GameManagerSJ.Instance.pool.Get(8);
                GameObject intercepter2 = GameManagerSJ.Instance.pool.Get(9);
                GameObject intercepter3 = GameManagerSJ.Instance.pool.Get(10);

                intercepter.transform.position = SpawnPos1.position;
                intercepter1.transform.position = SpawnPos2.position;
                intercepter2.transform.position = SpawnPos3.position;
                intercepter3.transform.position = SpawnPos4.position;
                /*GameObject clone1 = Instantiate(Rintercept, SpawnPos1.position, Quaternion.identity);
                GameObject clone2 = Instantiate(Lintercept, SpawnPos2.position, Quaternion.identity);
                GameObject clone3 = Instantiate(Rintercept2, SpawnPos3.position, Quaternion.identity);
                GameObject clone4 = Instantiate(Lintercept2, SpawnPos4.position, Quaternion.identity);*/

            }
            yield return new WaitForSeconds(1f);

        }
        yield return null;

    }

    IEnumerator CreatBullet()
    {
        int count = 15;
        float intervalAngle = 360 / count;
        float weightAngle = 0;

        while (check_bullet)
        {
            for (int i = 0; i < count; ++i)
            {
                yield return new WaitForSeconds(0.1f);
                GameObject clone = Instantiate(bullet, gun2Pos.position, Quaternion.identity);
                float angle = weightAngle + intervalAngle * i;
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);
                clone.GetComponent<ArcBulletSJ>().Move(new Vector2(x, -y - 3));
            }

            yield return new WaitForSeconds(1f);

            for (int i = 0; i < count; ++i)
            {
                yield return new WaitForSeconds(0.1f);
                GameObject clone2 = Instantiate(bullet, gun3Pos.position, Quaternion.identity);
                float angle = weightAngle + intervalAngle * i;
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);
                clone2.GetComponent<ArcBulletSJ>().Move(new Vector2(x, -y - 3));
            }

            weightAngle += 1;

            yield return new WaitForSeconds(3);
        }
    }

    IEnumerator CreatStarBullet()
    {

        while (check_starbullet)
        {
            float scaleSpeed = 0.1f;

            yield return new WaitForSeconds(5f);

            GameObject clone = Instantiate(StarBullet, gunPos.position, Quaternion.identity);

            clone.transform.SetParent(gameObject.transform);


            while (clone.transform.localScale.x < 3f)
            {
                yield return new WaitForSeconds(0.1f);

                if (clone == null)
                {
                    break;
                }

                clone.transform.localScale = new Vector3(0.1f + scaleSpeed, 0.1f + scaleSpeed, 0.1f + scaleSpeed);
                scaleSpeed += 0.1f;

            }

            if (clone != null)
            {
                clone.transform.SetParent(null);
            }

            yield return null;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SideWall"))
            Speed *= -1;
        if (collision.gameObject.CompareTag("PlayerBullet"))
            currunt_Hp -= GameManagerSJ.Instance.player.AttackPower;
        if (collision.gameObject.CompareTag("HomingMissle"))
            currunt_Hp -= GameManagerSJ.Instance.player.AttackPower * 2;

        BossUI_dm.instance.Damage(BossUI_dm.HP.body, currunt_Hp);
    }

}
