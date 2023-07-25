using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RStopEnemy3SJ : MonoBehaviour
{
    public float Speed = 5;
    public float rotationSpeed = 5f;
    public float AttackSpeed = 1;

    int currentWayPoint = 0;

    public GameObject bullet;
    public Transform target;
    public Transform gunPos;

    void Start()
    {
        InvokeRepeating("CreatBullet", 1, AttackSpeed);
    }

    void Update()
    {
        if (currentWayPoint < 4)
        {
            Transform targetWayPoint = WayPointManagerSJ.instance.RightStopWayPoint[currentWayPoint];
            transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, Speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, targetWayPoint.position) < 0.01f)
            {
                currentWayPoint++;
            }
        }

        if (currentWayPoint == 4)
        {
            Invoke("MoveEndPoint", 1.5f);
            if (Vector2.Distance(transform.position, WayPointManagerSJ.instance.RightStopWayPoint[4].position) < 0.01f)
            {
                currentWayPoint++;
            }
            return;
        }

        if (currentWayPoint == 5)
        {
            Destroy(gameObject);
        }
    }

    void CreatBullet()
    {

        if (currentWayPoint == WayPointManagerSJ.instance.RightStopWayPoint.Length - 1)
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

    void MoveEndPoint()
    {
        Transform targetWayPoint = WayPointManagerSJ.instance.RightStopWayPoint[4];
        transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, Speed * Time.deltaTime);
    }
}
