using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTraceBullet : MonoBehaviour
{
    public GameObject effect;
    public float Speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Speed * Time.deltaTime, Space.Self);
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //플레이어 지우기
            // Destroy(collision.gameObject);
            //미사일 지우기
            // Destroy(gameObject);
        }
        else if (collision.tag == "BombAir")
        {
            Instantiate(effect, collision.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
