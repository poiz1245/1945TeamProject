using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class EBulletJ : MonoBehaviour
{
    public float Speed = 3f;
    public float ZigzagDistance = 0.75f;
    int flag = 1;
    public int HP = 1;


    void Start()
    {
        ZigzagDistance = Random.Range(-1.5f, 2.5f);

    }
   
    // Update is called once per frame
    void Update()
    {
        // 'flag' 변수를 기반으로 움직임 방향을 계산
        Vector3 direction = Vector3.down * Speed * Time.deltaTime;
        if (flag == 1)
            direction += Vector3.right * Speed * Time.deltaTime;
        else
            direction += Vector3.left * Speed * Time.deltaTime;

        // 오브젝트를 이동시킵니다.
        transform.Translate(direction);

        // 수평 경계에 도달하면 움직임 방향을 바꾸기
        if (transform.position.x >= ZigzagDistance)
            flag = -1;
        if (transform.position.x <= -ZigzagDistance)
            flag = 1;
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerJ")
        {
            //플레이어 지우기
            Destroy(collision.gameObject);
            //미사일 지우기
            Destroy(gameObject);
        }
        if (collision.tag == "OneJ")
        {
            //플레이어 지우기
            Destroy(collision.gameObject);
            //미사일 지우기
            Destroy(gameObject);
        }
    }
    public void Damage(int attack)
    {
        HP -= attack;

        if (HP <= 0)
        {
            //ItemDrop();
            Destroy(gameObject);
        }
    }
}
