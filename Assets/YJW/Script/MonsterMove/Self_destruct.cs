using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self_destruct : MonoBehaviour
{


    public float HP = 1000;
    public float Speed = 3;
    public float Delay = 1f;

    public GameObject bullet;

    public GameObject centerPos;


    public GameObject effect;

    public GameObject Item = null;

    void Start()
    {

        //한번 호출
        Invoke("CreateBullet", Delay);
        centerPos = GameObject.Find("CenterPos");
    }

    void CreateBullet()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
        Instantiate(bullet, transform.position, Quaternion.identity);
        Invoke("CreateBullet", Delay);
    }

    void Update()
    {
       
        transform.position =
           Vector3.MoveTowards(transform.position, centerPos.transform.position, Speed * Time.deltaTime);

        if(transform.position.y == centerPos.transform.position.y )
        {
            Instantiate(Item, transform.position, Quaternion.identity);
            Instantiate(effect, transform.position, Quaternion.identity);
            //Destroy(transform.gameObject);
           
        }
    }

    public void ItemDrop()
    {
        Instantiate(Item, transform.position, Quaternion.identity);
    }



    public void Damage(float attack)
    {


        HP -= attack;
        Debug.Log("데미지 받았음");

        if (HP <= 0)
        {
            HP = 0;

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


}
