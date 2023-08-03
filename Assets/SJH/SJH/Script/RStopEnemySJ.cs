using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RStopEnemy3SJ : MonoBehaviour
{
    public float Speed = 5;
    public float AttackSpeed = 1;
    public float rotationSpeed = 5f;
    public int Hp = 100;

    bool fire = false;
    bool moveEnd = false;


    public Transform gunPos;
    public GameObject bullet;
    public GameObject Effect;
    GameObject target;

    void Start()
    {
        InvokeRepeating("CreatBullet", 1, AttackSpeed);
        target = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        transform.Translate(Vector2.down * Speed * Time.deltaTime);

        if (GameManagerSJ.Instance.player != null && transform.position.y - GameManagerSJ.Instance.player.transform.position.y <= 5)
        {
            Speed = 0;

            if (!fire)
            {
                StartCoroutine(CreatBullet());
                fire = true;
            }

            Vector2 direction = new Vector2(transform.position.x - target.transform.position.x,
                                    transform.position.y - target.transform.position.y);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotationSpeed * Time.deltaTime);
            transform.rotation = rotation;

            Invoke("MoveEndPoint", 2);

        }

        if (moveEnd)
        {
            //gameObject.SetActive(false);
            Destroy(gameObject, 2);
        }

        if (Hp <= 0)
        {
            Instantiate(Effect, transform.position, Quaternion.identity);
            //gameObject.SetActive(false);
            Destroy(gameObject);
            ScoreManager.instance.monsterkill++;

        }

    }
    void MoveEndPoint()
    {
        Transform targetWayPoint = WayPointManagerSJ.instance.RightStopWayPoint[4];
        transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, 10 * Time.deltaTime);
        moveEnd = true;
    }
    IEnumerator CreatBullet()
    {
        yield return new WaitForSeconds(AttackSpeed);
        Instantiate(bullet, gunPos.position, Quaternion.identity);
        fire = false;
    }
    private void OnBecameInvisible()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Hp -= GameManagerSJ.Instance.player.AttackPower;
        }
        if (collision.CompareTag("HomingMissle"))
        {
            Hp -= GameManagerSJ.Instance.player.AttackPower * 2;
        }
    }


}
