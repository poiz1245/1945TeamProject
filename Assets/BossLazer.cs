using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLazer : MonoBehaviour
{
    public GameObject targetPos;
    public GameObject destroyEffect;


    public float speed = 5f;

    void Start()
    {
        targetPos = GameObject.Find("Boss").transform.Find("BPPos5Lazer").gameObject;

        //∑π¿Ã¿˙
        Destroy(gameObject, 5f);
    }



    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos.transform.position, speed);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;

        if (collision.CompareTag("Player"))
        {
            Instantiate(destroyEffect,collision.transform.position, Quaternion.identity);
        }
    }
}

