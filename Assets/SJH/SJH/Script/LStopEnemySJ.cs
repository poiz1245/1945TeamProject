using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class LStopEnemy3SJ : MonoBehaviour
{
    public float Speed =5;
    public float AttackSpeed = 1;
    int currentWayPoint = 0;

    public Transform gunPos;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreatBullet", 1, AttackSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWayPoint < 4)
        {
            Transform targetWayPoint = WayPointManagerSJ.instance.LeftStopWayPoint[currentWayPoint];
            transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, Speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, targetWayPoint.position) < 0.01f)
            {
                currentWayPoint++;
            }
        }

        if (currentWayPoint == 4)
        {
            Invoke("MoveEndPoint", 1.5f);
            if (Vector2.Distance(transform.position, WayPointManagerSJ.instance.LeftStopWayPoint[4].position) < 0.01f)
            {
                currentWayPoint++;
            }
            return;
        }

        if(currentWayPoint == 5)
        {
            Destroy(gameObject);
        }
    }
    void MoveEndPoint()
    {
        Transform targetWayPoint = WayPointManagerSJ.instance.LeftStopWayPoint[4];
        transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, Speed * Time.deltaTime);
    }


    void CreatBullet()
    {
        
        if (currentWayPoint == WayPointManagerSJ.instance.LeftStopWayPoint.Length-1 )
        {
            Instantiate(bullet, gunPos.position, Quaternion.identity);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Destroy(gameObject);
        }
    }

}
