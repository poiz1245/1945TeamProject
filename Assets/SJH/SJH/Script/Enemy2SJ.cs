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
    public int rnd;

    public Transform gunPos1;
    public Transform gunPos2;
    public GameObject bullet;
    public GameObject item1;
    public GameObject item2;
    public GameObject Effect;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreatBullet());
        rnd = Random.Range(1, 16);
        
    }



    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Speed * Time.deltaTime);
        if (Hp <= 0)
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
            Instantiate(Effect, transform.position, Quaternion.identity);
            ScoreManager.instance.monsterkill++;

            if (rnd >= 1 && rnd <= 5)
            {
                Instantiate(item1, new Vector3(transform.position.x, transform.position.y - 2), Quaternion.identity);
            }
            else if (rnd >= 6 && rnd <= 10)
            {
                Instantiate(item2, new Vector3(transform.position.x, transform.position.y - 2), Quaternion.identity);
            }
            else
                return;
        }


    }
    IEnumerator CreatBullet()
    {
        int count = 3;
        float intervalAngle = 90 / count;
        float weightAngle = 0;

        while (true)
        {
            for (int i = 0; i < count; ++i)
            {
                yield return new WaitForSeconds(0.1f);
                GameObject clone = Instantiate(bullet, gunPos1.position, Quaternion.identity);
                GameObject clone2 = Instantiate(bullet, gunPos2.position, Quaternion.identity);
                float angle = weightAngle + intervalAngle * i;
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);
                clone.GetComponent<ArcBulletSJ>().Move(new Vector2(x, -y - 3));
                clone2.GetComponent<ArcBulletSJ>().Move(new Vector2(-x, -y - 3));
            }
            weightAngle += 1;

            yield return new WaitForSeconds(AttackSpeed);
        }
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

