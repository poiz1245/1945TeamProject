using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LIntercepter : MonoBehaviour
{
    //내 기준 오른쪽 앞에서 나오는 스크립트 이름은 L

    public float Speed;

    public Transform Player;
    public Transform BossPos;
    public Transform PosA;
    public Transform PosB;
    public GameObject Effect;

    float time = 0;

    Vector3 myPos;
    Vector3 playerPos;


    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;

        PosA = GameObject.FindGameObjectWithTag("Elite").transform.Find("PosC");
        PosB = GameObject.FindGameObjectWithTag("Player").transform.Find("PosD");
        myPos = transform.position;
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
            ScoreManager.instance.monsterkill++;
            Destroy(gameObject);
            Instantiate(Effect, transform.position, Quaternion.identity);
        }
        
    }

}
