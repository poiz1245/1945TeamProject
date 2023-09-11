//using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RCornerEnemySJ : MonoBehaviour
{
    public float Speed = 3f;
    public int AttackPower = 10;
    public float AttackSpeed = 1;
    public float rotationSpeed = 5f;

    GameObject target;
    public Transform gunUpPos;
    public Transform gunDownPos;
    public GameObject Upbullet;
    public GameObject Downbullet;
    public GameObject Effect;
    int currentWayPoint = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreatBullet());
        target = GameObject.FindWithTag("RightCornerWayPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWayPoint < WayPointManagerSJ.instance.RightCornerWayPoint.Length)
        {
            Transform targetWayPoint = WayPointManagerSJ.instance.RightCornerWayPoint[currentWayPoint];
            transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, Speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, targetWayPoint.position) < 0.01f)
            {
                currentWayPoint++;
            }
        }
        else
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }

        Vector2 direction = new Vector2(transform.position.x - target.transform.position.x,
                                       transform.position.y - target.transform.position.y);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotationSpeed * Time.deltaTime);
        transform.rotation = rotation;
    }
    IEnumerator CreatBullet()
    {
        while (true)
        {
            Instantiate(Upbullet, gunUpPos.position, Quaternion.identity);
            Instantiate(Downbullet, gunDownPos.position, Quaternion.identity);
            yield return new WaitForSeconds(AttackSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            ScoreManager.instance.monsterkill++;
            Instantiate(Effect, transform.position, Quaternion.identity);
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
        if (collision.CompareTag("HomingMissle"))
        {
            ScoreManager.instance.monsterkill++;
            Instantiate(Effect, transform.position, Quaternion.identity);
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
