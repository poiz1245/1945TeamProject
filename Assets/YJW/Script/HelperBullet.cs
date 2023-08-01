using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class HelperBullet : MonoBehaviour
{
    public float Speed = 4.0f;

    public float Attack = 1;

    public GameObject effect;

    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }


    void Update()
    {
        //미사일이 위쪽방향으로 움직인다.
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.tag == "BossArm")
        {
            collision.gameObject.GetComponent<BossArmHp>().Damage(Attack);


          //  GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //미사일 지우기
            Destroy(gameObject);


        }
        else if (collision.tag == "Monster")
        {
            // collision.gameObject.GetComponent<Monster>().ItemDrop();

            //몬스터 충돌 지우기
            //Destroy(collision.gameObject);
            collision.gameObject.GetComponent<Monster>().Damage(Attack);




            //이펙트 생성하기
           // GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //이펙트 1초뒤에 지우기
            // Destroy(go, 1);


            //미사일 지우기
            Destroy(gameObject);


        }
        else if (collision.tag == "Boss")
        {
            // collision.gameObject.GetComponent<Monster>().ItemDrop();

            //몬스터 충돌 지우기
            //Destroy(collision.gameObject);
            collision.gameObject.GetComponent<Boss>().Damage(Attack);




            //이펙트 생성하기
            //GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //이펙트 1초뒤에 지우기
            // Destroy(go, 1);


            //미사일 지우기
            Destroy(gameObject);


        }
        else if (collision.tag == "BossHelper")
        {
            collision.gameObject.GetComponent<HelperBoss>().Damage(Attack);
    
            //미사일 지우기
            Destroy(gameObject);

        }
        else if (collision.tag == "BossHelper2")
        {
        
            collision.gameObject.GetComponent<HelperBoss2>().Damage(Attack);
            //미사일 지우기
            Destroy(gameObject);

        }
        else if (collision.tag == "Boss2")
        {
        
            collision.gameObject.GetComponent<Boss>().Damage(Attack);


            Destroy(gameObject);

        }
    }
}
