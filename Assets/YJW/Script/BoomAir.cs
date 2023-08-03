using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomAir : MonoBehaviour
{
    public GameObject Boom1;

    public GameObject colTransfromEffect;

    public float DeadTime = 5;

    public float boomAirSpeed = 1; //폭탄 사용시 호출될 비행기의 속도
    public float startFirstBoomDonw;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FirstBoomDown());
        Destroy(gameObject, DeadTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * boomAirSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyBullet")
        {
            Instantiate(colTransfromEffect, collision.transform.position, Quaternion.identity);
        }
    }


    IEnumerator FirstBoomDown()
    {
        yield return new WaitForSeconds(startFirstBoomDonw);
        var copyboom = Instantiate(Boom1,transform.position,Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Destroy(copyboom, 1f);

    }

}
