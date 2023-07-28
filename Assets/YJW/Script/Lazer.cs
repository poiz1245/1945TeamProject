using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    public GameObject targetPos;

    public int Attack = 2;

    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = GameObject.Find("Player").transform.Find("pos").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos.transform.position, speed);

        //미사일 지우기
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {


        if (collision.tag == "Monster")
        {

          

            collision.gameObject.GetComponent<Monster>().Damage(Attack);


        }
        else if (collision.tag == "BossArm")
        {

            

            collision.gameObject.GetComponent<BossArmHp>().Damage(Attack);


        }
        else if (collision.tag == "Boss")
        {



            collision.gameObject.GetComponent<Boss>().Damage(Attack);


        }

        Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

}
