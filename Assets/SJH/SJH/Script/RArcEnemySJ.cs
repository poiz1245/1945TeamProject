using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RArcEnemySJ : MonoBehaviour
{
    public float Speed = 3f;
    public int AttackPower = 10;
    public float AttackSpeed = 1;

    public Transform player;
    public Transform gunPos1;
    public GameObject bullet;
    public GameObject Effect;
    int currentWayPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreatBullet());
    }

    void Update()
    {
        
        if (currentWayPoint < WayPointManagerSJ.instance.RightWayPoint.Length)
        {
            Transform targetWayPoint = WayPointManagerSJ.instance.RightWayPoint[currentWayPoint];
            transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, Speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, targetWayPoint.position) < 0.01f)
            {
                currentWayPoint++;
            }
        }
        else
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }

    }
    IEnumerator CreatBullet()
    {
        while(true)
        { 
            Instantiate(bullet, gunPos1.position, Quaternion.identity);
            yield return new WaitForSeconds(AttackSpeed);
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            ScoreManager.instance.monsterkill++;
            Instantiate(Effect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
        if (collision.CompareTag("HomingMissle"))
        {
            ScoreManager.instance.monsterkill++;
            Instantiate(Effect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }

}
