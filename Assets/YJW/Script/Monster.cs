//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float HP = 150;
    public float Speed = 3;
    public float Delay = 1f;
    public Transform ms;
    public Transform ms2;
    public GameObject bullet;
    public bool isHit = false;


    public GameObject effect;

    public GameObject Item1 = null;
    public GameObject Item2 = null;

    void Start()
    {

        //한번 호출
        Invoke("CreateBullet", Delay);
    }

    void CreateBullet()
    {
        Instantiate(bullet, ms.position, Quaternion.identity);
        Instantiate(bullet, ms2.position, Quaternion.identity);
        Invoke("CreateBullet", Delay);
    }

    void Update()
    {
        //아래방향으로 움직여라
        // transform.Translate(Vector2.down * Speed * Time.deltaTime);
    }

    public void ItemDrop()
    {
        float rnd = 0;
        rnd = Random.Range(0, 100);
        if (rnd <= 50)
            Instantiate(Item1, transform.position, Quaternion.identity);
        if (rnd >= 50)
            Instantiate(Item2, transform.position, Quaternion.identity);
    }



    public void Damage(float attack)
    {


        HP -= attack;
        //Debug.Log("데미지 받았음");
        StartCoroutine(CoolHit());
        if (HP <= 0)
        {
            HP = 0;

            ScoreManager.instance.monsterkill++;
            Destroy(gameObject);
            ItemDrop();

            Instantiate(effect, transform.position, Quaternion.identity);

            //    Destroy(effect, 0.5f);


        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    IEnumerator CoolHit()
    {
        var hit = transform.GetComponent<SpriteRenderer>();
        isHit = true;
        hit.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        hit.color = Color.white;
        isHit = false;
    }



}
