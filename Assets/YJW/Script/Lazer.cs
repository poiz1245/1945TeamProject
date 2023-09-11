using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    public GameObject targetPos;

    public float Attack = 0.4f;

    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = GameObject.FindWithTag("Player").transform.Find("Gun").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos.transform.position, speed);

        //미사일 지우기
        Destroy(gameObject, 4f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {


        if (collision.CompareTag("Monster"))
        {


            if (collision.GetComponent<Monster_dm>() == null &&
                collision.GetComponent<Boss_dm>() == null &&
                collision.GetComponent<BossArm_dm>() == null &&
                collision.GetComponent<Octopus_dm>() == null)
                collision.gameObject.GetComponent<Monster>().Damage(Attack);


        }
        else if (collision.CompareTag("BossArm"))
        {

            

            collision.gameObject.GetComponent<BossArmHp>().Damage(Attack);


        }
        else if (collision.CompareTag("Boss"))
        {



            collision.gameObject.GetComponent<Boss>().Damage(Attack);


        }
        else if (collision.CompareTag("Boss2"))
        {



            collision.gameObject.GetComponent<LastBoss>().Damage(Attack);


        }
        else if(collision.CompareTag("BossHelper"))
        {
            collision.gameObject.GetComponent<HelperBoss>().Damage(Attack);
        }
        else if(collision.CompareTag("BossHelper2"))
        {
            collision.gameObject.GetComponent<HelperBoss2>().Damage(Attack);
        }


        Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

}
