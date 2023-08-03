using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RIntercepter2 : MonoBehaviour
{
    //내 기준 오른쪽 뒤에서 나오는 인터셉터 스크립트 근데 이름은 R

    public float Speed;

    public Transform Player;
    public Transform BossPos;
    public Transform PosA;
    public Transform PosB;
    public GameObject Effect;
    GameObject Elite;
    Vector3 myPos;
    Vector3 playerPos;

    float time = 0;



    void Start()
    {
        myPos = transform.position;
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;

        PosA = GameObject.FindGameObjectWithTag("Elite").transform.Find("PosE");
        PosB = GameObject.FindGameObjectWithTag("Player").transform.Find("PosF");
        Elite = GameObject.FindWithTag("Elite");
    }
    void Update()
    {

        if (time > 1)
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }

        if (myPos != null && PosA.transform.position != null && playerPos != null)
        {
            Vector3 p1 = Vector3.Lerp(myPos, PosA.transform.position, time);
            Vector3 p2 = Vector3.Lerp(PosA.transform.position, playerPos, time);

            transform.position = Vector3.Lerp(p1, p2, time);

            time += Time.deltaTime;
        }
        if (Elite.GetComponent<Elite>().check == false)
            //gameObject.SetActive(false);
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet") ||
            collision.gameObject.CompareTag("HomingMissle") || collision.gameObject.CompareTag("Player"))
        {
            ScoreManager.instance.monsterkill++;
            //gameObject.SetActive(false);
            Destroy(gameObject);
            Instantiate(Effect, transform.position, Quaternion.identity);
        }
    }


}
