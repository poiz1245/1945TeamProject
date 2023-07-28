using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OneJ : MonoBehaviour
{
    public GameObject effect;
    Transform pos;
    public float Speed = 500;
    int Attack = 100;
    // Start is called before the first frame update
    void Start()
    {
        pos = GameObject.Find("PlayerJ").GetComponent<PlayerConstrolJ>().pos;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector2.up * Speed * Time.deltaTime);
        transform.Rotate(0, 0, 1 * Speed * Time.deltaTime);
        transform.position = pos.position;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {



        //if (collision.tag == "EnermyJ")
        //{
        //    collision.gameObject.GetComponent<EnermyJ1>().Damage(Attack++);
        //    //이펙트 생성하기
        //    GameObject go = Instantiate(effect, collision.gameObject.transform.position, Quaternion.identity);
        //    //이펙트 1초뒤에 지우기
        //    Destroy(go, 1);
        //}
        //if (collision.tag == "EnermyJ2")
        //{
        //    collision.gameObject.GetComponent<EnermyJ2>().Damage(Attack++);
        //    //이펙트 생성하기
        //    GameObject go = Instantiate(effect, collision.gameObject.transform.position, Quaternion.identity);
        //    //이펙트 1초뒤에 지우기
        //    Destroy(go, 1);
        //}
        //if (collision.CompareTag("BossJ"))
        //{
        //    collision.gameObject.GetComponent<BossJ>().Damage(Attack++);
        //    //이펙트 생성하기
        //    GameObject go = Instantiate(effect, collision.gameObject.transform.position, Quaternion.identity);
        //    //이펙트 1초뒤에 지우기
        //    Destroy(go, 1);

        //}
        if (collision.CompareTag("EBulletJ"))
        {
            collision.gameObject.GetComponent<EBulletJ>().Damage(Attack++);
            //이펙트 생성하기
            GameObject go = Instantiate(effect, collision.gameObject.transform.position, Quaternion.identity);
            //이펙트 1초뒤에 지우기
            Destroy(go, 1);

        }
        
        if (collision.CompareTag("BossBressJ"))
        {
            collision.gameObject.GetComponent<BossBressJ>().Damage(Attack++);
            //이펙트 생성하기
            GameObject go = Instantiate(effect, collision.gameObject.transform.position, Quaternion.identity);
            //이펙트 1초뒤에 지우기
            Destroy(go, 1);

        }
    }
}
