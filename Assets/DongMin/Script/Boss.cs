using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int hp = 1000;
    float speed = 1; 
    float Delay = 1.5f;
    public Transform ms;
    public GameObject bullet;
    float limitX = 1.4f;
    //float limitY = 4.47f;
    float x = 1;
    float y = 0;

    // Start is called before the first frame update
    void Start()
    {
        //보스 나타나면 Hide함수 1초 뒤 동작
        Invoke("Hide", 1);

        //StartCoroutine("BossBullet");
        StartCoroutine("CircleFire");

        ////한번 호출
        //Invoke("CreateBullet", 0.1f);
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
                clone.GetComponent<MonsterBullet>().Move(new Vector2(x, y));
            }
            //발사체가 생성되는 시작 각도 설정을 위한 변수
            weightAngle += 1;

            //attackRate 시간만큼 대기
            yield return new WaitForSeconds(attackRate); //3초마다 원형 미사일 발사
        }
    }

    // Update is called once per frame
    void Update()
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

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void Damage(int attack)
    {
        //hp -= attack;

        //if (hp <= 0)
        //{
        //    ItemDrop();
        //    Destroy(gameObject);
        //}
    }

    void Hide()
    {
        //보스 텍스트 객체이름 검색해서 끄기
        GameObject.Find("TextBossWarning").SetActive(false);
    }

    IEnumerator BossBullet()
    {
        while (true)
        {
            for(int i = 0; i < 36; i++)
            {
                ms.transform.Rotate(0, 0, 10);
                Instantiate(bullet, ms.position, ms.transform.rotation);
            }

            //Instantiate(bullet, ms.position, Quaternion.identity);
            //Instantiate(bullet, ms2.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }
}
