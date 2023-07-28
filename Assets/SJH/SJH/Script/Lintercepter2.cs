using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lintercepter2 : MonoBehaviour
{
    //내 기준 오른쪽 뒤에서 나오는 인터셉터 스크립트 근데 이름은 L

    public float Speed;

    public Transform Player;
    public Transform BossPos;
    public Transform PosA;
    public Transform PosB;
    public GameObject Effect;

    Vector3 myPos;
    Vector3 playerPos;

    float time = 0;


    void Start()
    {
        myPos = transform.position;
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;

        PosA = GameObject.FindGameObjectWithTag("Elite").transform.Find("PosG");
        PosB = GameObject.FindGameObjectWithTag("Player").transform.Find("PosH");
    }
    void Update()
    {

        if (time > 1)
        {
            Destroy(gameObject);
        }
     

        Vector3 p1 = Vector3.Lerp(myPos, PosA.transform.position, time);
        Vector3 p2 = Vector3.Lerp(PosA.transform.position, PosB.transform.position, time);
        Vector3 p3 = Vector3.Lerp(PosB.transform.position, playerPos, time);

        Vector3 p4 = Vector3.Lerp(p1, p2, time);
        Vector3 p5 = Vector3.Lerp(p2, p3, time);

        transform.position = Vector3.Lerp(p4, p5, time);
        time += Time.deltaTime;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerBullet") ||
            collision.gameObject.CompareTag("HomingMissle"))
        {
            Destroy(gameObject);
            Instantiate(Effect, transform.position, Quaternion.identity);
        }
    }
}
