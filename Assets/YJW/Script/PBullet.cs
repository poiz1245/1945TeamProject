using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBullet : MonoBehaviour
{
    public float Speed = 4.0f;

    public float Attack = 1;
    public float enegy = 1;

    public GameObject effect;

    GameObject player;



    private void Awake()
    {
        player = GameObject.Find("Player");
    }



    void Update()
    {
        //미사일이 위쪽방향으로 움직인다.
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }


    //해당코드를 설명하시오.
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }



    // public GameObject effect;
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.tag == "BossArm")
        {
            collision.gameObject.GetComponent<BossArmHp>().Damage(Attack);
            player.GetComponent<Player>().GazyPower(enegy);

            Instantiate(effect, transform.position, Quaternion.identity);
            //미사일 지우기
            Destroy(gameObject);


        }
        else if (collision.tag == "Monster")
        {
            // collision.gameObject.GetComponent<Monster>().ItemDrop();

            //몬스터 충돌 지우기
            //Destroy(collision.gameObject);
            collision.gameObject.GetComponent<Monster>().Damage(Attack);
            player.GetComponent<Player>().GazyPower(enegy);
            Instantiate(effect, transform.position, Quaternion.identity);



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
            player.GetComponent<Player>().GazyPower(enegy);
            Instantiate(effect, transform.position, Quaternion.identity);



            //이펙트 생성하기
            // GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //이펙트 1초뒤에 지우기
            // Destroy(go, 1);


            //미사일 지우기
            Destroy(gameObject);


        }
        else if (collision.tag == "Boss2")
        {
            // collision.gameObject.GetComponent<Monster>().ItemDrop();

            //몬스터 충돌 지우기
            //Destroy(collision.gameObject);
            collision.gameObject.GetComponent<LastBoss>().Damage(Attack);
            player.GetComponent<Player>().GazyPower(enegy);
            Instantiate(effect, transform.position, Quaternion.identity);



            //이펙트 생성하기
            // GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //이펙트 1초뒤에 지우기
            // Destroy(go, 1);


            //미사일 지우기
            Destroy(gameObject);


        }
        else if(collision.tag =="BossHelper")
        {
            collision.gameObject.GetComponent<HelperBoss>().Damage(Attack);
            //collision.gameObject.GetComponent<HelperBoss2>().Damage(Attack);
            player.GetComponent<Player>().GazyPower(enegy);
            Instantiate(effect, transform.position, Quaternion.identity);

            //미사일 지우기
            Destroy(gameObject);

        }
        else if (collision.tag == "BossHelper2")
        {
            //collision.gameObject.GetComponent<HelperBoss>().Damage(Attack);
            collision.gameObject.GetComponent<HelperBoss2>().Damage(Attack);
            player.GetComponent<Player>().GazyPower(enegy);
            Instantiate(effect, transform.position, Quaternion.identity);

            //미사일 지우기
            Destroy(gameObject);

        }
    }




    //삭제될때 호출되는 함수
    private void OnDestroy()
    {
      
    }






}
