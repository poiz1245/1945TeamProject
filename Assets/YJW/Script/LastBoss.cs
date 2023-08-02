using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class LastBoss : MonoBehaviour
{
    public float hp = 1500;

    public GameObject playerDied;
    GameObject target;

    //총알을 생성후 Target에게 날아갈 변수
  //  public GameObject Target;
    public GameObject bossPos;

    public GameObject helper1;
    public GameObject helper2;

    public GameObject targetbullet;

    public GameObject effect;


    float rightMax = 1.0f; //좌로 이동가능한 (x)최대값

    float leftMax = -1.0f; //우로 이동가능한 (x)최대값

    float currentPosition; //현재 위치(x) 저장

    float direction = 3.0f; //이동속도+방향

    bool isDownAttack = false;
    bool isfell = false;

    bool isShot = false;

    bool isLastShot = false;

    public bool isHit = false;


    float leftSpeed = 2;
    float downSpeed = 4;

    int SpawnCount = 0;

    //Vector3 pos; //현재위치

    float delta = 1.0f; // 좌(우)로 이동가능한 (x)최대값


    [SerializeField] public GameObject missile;
    [SerializeField] public GameObject targetPos;

    [SerializeField] public float spd;
    [SerializeField] public int shot = 12;



    void Start()
    {
        currentPosition = transform.position.x;
        target = GameObject.FindGameObjectWithTag("Player");
        bossPos = GameObject.Find("BossRollBackPos");
        //pos = transform.position;
       // BossHelperSpawn();

       
    }

   
    void Update()
    {

        Debug.Log(hp);

        BossMoving();

        if (hp <= 1000 && hp > 600)
        {
            if (isShot == false)
            {
                StartCoroutine(CreateMissile());
            }
        }
        else if (hp <= 600 && hp > 300)
        {
            //보스와 플레이어의 x축의 값이 절대값으로 -해서 0.1 정도의 차이가 난다면
            if (Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(target.transform.position.x)) <= 0.1)
            {
                isDownAttack = true;

            }



            if (isDownAttack && !isfell)
            {

                BossStop();

                transform.position =
                Vector3.MoveTowards(transform.position, target.transform.position, downSpeed * Time.deltaTime);

                if (transform.position.y < -3)
                {
                    isDownAttack = false;
                    //Debug.Log("y의 값이 작아졌음.");
                    isfell = true;
                }



            }

            if (!isDownAttack && isfell)
            {


                transform.position =
                Vector3.MoveTowards(transform.position, bossPos.transform.position, downSpeed * Time.deltaTime);
                if (transform.position.y == bossPos.transform.position.y)
                {

                    isfell = false;
                    // isDownAttack = true;

                }




            }
            else if (isDownAttack && isfell)
            {
                transform.position =
               Vector3.MoveTowards(transform.position, bossPos.transform.position, downSpeed * Time.deltaTime);
                if (transform.position.y == bossPos.transform.position.y)
                {

                    isfell = false;
                    // isDownAttack = true;

                }

            }
        }
        else if(hp>100 &&hp <= 300)
        {
            transform.position =
            Vector3.MoveTowards(transform.position, bossPos.transform.position, downSpeed * Time.deltaTime);
            if (SpawnCount == 0)
            {
                BossHelperSpawn();
                SpawnCount++;
            }
           
            if(helper1 == null && helper2 == null && SpawnCount == 1 && hp > 100)
            {
                BossHelperSpawn();
            }
        }
        else if(hp <= 100)
        {
            //발악 패턴 넣으면 끝.

            StartCoroutine(Shot());
            if (hp <=0)
            {
                hp = 0;
              
            }
        }



        Debug.Log("isDdwonAttack :" + isDownAttack);
        Debug.Log("isfell :" + isDownAttack);


    }

    void BossHelperSpawn()
    {
       Instantiate(helper1, transform.position, Quaternion.identity);
       Instantiate(helper2, transform.position, Quaternion.identity);
    }



    private void BossMoving()
    {
       
        currentPosition += Time.deltaTime * direction;

        if (currentPosition >= rightMax)
        {
            direction *= -1;
            currentPosition = rightMax;
            //현재 위치(x)가 우로 이동가능한 (x)최대값보다 크거나 같다면
            //이동속도+방향에 -1을 곱해 반전을 해주고 현재위치를 우로 이동가능한 (x)최대값으로 설정
        }
        else if (currentPosition <= leftMax)
        {
            direction *= -1;
            currentPosition = leftMax;
            //현재 위치(x)가 좌로 이동가능한 (x)최대값보다 크거나 같다면
            //이동속도+방향에 -1을 곱해 반전을 해주고 현재위치를 좌로 이동가능한 (x)최대값으로 설정
        }
        transform.position = new Vector3(currentPosition, transform.position.y, 0);
    }

    void BossStop()
    {
       // direction = 0;
        currentPosition = target.transform.position.x;
    }

    public void Damage(float attack)
    {
        hp -= attack;

        Debug.Log("데미지 받았음");
        StartCoroutine(CoolHit());

        if (hp <= 0)
        {
            hp = 0;

            Destroy(gameObject);


            Instantiate(effect, transform.position, Quaternion.identity);

            //    Destroy(effect, 0.5f);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Destroy(collision.gameObject);
            Instantiate(playerDied, collision.transform.position, Quaternion.identity);
            Debug.Log("플레이어가 맞았음");
        }
    }

    IEnumerator CreateMissile()
    {
       
        int _shot = shot;
        while (_shot > 0)
        {   
            _shot--;
            GameObject bullet = Instantiate(missile, transform.position,Quaternion.identity);
            bullet.GetComponent<BeazierBullet>().master = gameObject;
            bullet.GetComponent<BeazierBullet>().enemy = target;
            isShot = true;
            yield return new WaitForSeconds(0.1f);
            isShot = false;
        }
        yield return null;

       
    }


    IEnumerator Shot()
    {
        Debug.Log("1번패턴이 발동되었음");
        //Target방향으로 발사될 오브젝트 수록
        List<Transform> bullets = new List<Transform>();

        for (int i = 0; i < 360; i += 13)
        {

            yield return new WaitForSeconds(0.5f);
            //총알 생성
            GameObject temp = Instantiate(targetbullet, transform.position, Quaternion.identity);

            //2초후 삭제
            Destroy(temp, 2f);

            //총알 생성 위치를 (0,0) 좌표로 한다.
            //temp.transform.position = Vector2.zero;

            //?초후에 Target에게 날아갈 오브젝트 수록
            bullets.Add(temp.transform);

            //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
            temp.transform.rotation = Quaternion.Euler(0, 0, i);
        }

        yield return new WaitForSeconds(0.4f);
        //총알을 Target 방향으로 이동시킨다.
        StartCoroutine(BulletToTarget(bullets));
    }

    private IEnumerator BulletToTarget(IList<Transform> objects)
    {
        //0.5초 후에 시작
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < objects.Count; i++)
        {

            //현재 총알의 위치에서 플레이의 위치의 벡터값을 뻴셈하여 방향을 구함
            Vector3 targetDirection = target.transform.position - objects[i].position;

            //x,y의 값을 조합하여 Z방향 값으로 변형함. -> ~도 단위로 변형
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

            //Target 방향으로 이동
            objects[i].rotation = Quaternion.Euler(0, 0, angle);


        }

        //데이터 해제
        objects.Clear();
    }

    IEnumerator CoolHit()
    {
        var hit = transform.GetComponent<SpriteRenderer>();
        isHit = true;
        hit.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        hit.color = Color.white;
        isHit = false;
    }



}
