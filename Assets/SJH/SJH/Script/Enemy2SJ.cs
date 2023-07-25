using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy2SJ : MonoBehaviour
{
    public float Speed = 1f;
    public int AttackPower = 10;
    public int Hp = 50;
    public float AttackSpeed = 2f;

    public Transform gunPos1;
    public Transform gunPos2;
    public GameObject bullet;
    public GameObject item;




    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreatBullet());
    }



    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Speed * Time.deltaTime);
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }


    }
    IEnumerator CreatBullet()
    {
        int count = 3;
        float intervalAngle = 90 / count;
        float weightAngle = 0;

        while(true)
        {
            for (int i = 0; i < count; ++i)
            {
                yield return new WaitForSeconds(0.1f);
                GameObject clone = Instantiate(bullet, gunPos1.position, Quaternion.identity);
                GameObject clone2 = Instantiate(bullet, gunPos2.position, Quaternion.identity);
                float angle = weightAngle + intervalAngle*i;
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);
                clone.GetComponent<ArcBulletSJ>().Move(new Vector2(x, -y-3));
                clone2.GetComponent<ArcBulletSJ>().Move(new Vector2(-x, -y-3));
            }
            weightAngle += 1;

            yield return new WaitForSeconds(AttackSpeed);
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
            Hp -= GameManagerSJ.Instance.player.AttackPower;
        }
    }

    private void OnDestroy()
    {
        Instantiate(item, transform.position, Quaternion.identity);
    }



}

