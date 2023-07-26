using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator ani; //애니메이터 가져올 변수
    public float moveSpeed = 5;
    public GameObject[] bullet; //미사일 
    public Transform pos = null; //미사일 발사

    public GameObject Lazer = null;

    public bool pBulletCheck = true;

    public int power =0;

    public int stack = 0;


    void Start()
    {
        ani = GetComponent<Animator>();
    }


    void Update()
    {
        float moveX = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float moveY = moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");


        if (Input.GetAxis("Horizontal") >= 0.5f)
        {
            ani.SetBool("right", true);
        }
        else
        {
            ani.SetBool("right", false);
        }

        if (Input.GetAxis("Horizontal") <= -0.5f)
        {
            ani.SetBool("left", true);
        }
        else
        {
            ani.SetBool("left", false);
        }


        if (Input.GetAxis("Vertical") >= 0.5f)
        {
            ani.SetBool("up", true);
        }
        else
        {
            ani.SetBool("up", false);
        }


        if (Input.GetKeyUp(KeyCode.Space) && stack >= 10)
        {
            stack = 0;
            Instantiate(Lazer,pos.transform.position,Quaternion.identity);
        }
        else if(Input.GetKeyUp(KeyCode.Space) && stack < 10)
        {
            stack = 0;
        }

        if (Input.GetKey(KeyCode.Space))
        {
           
          
            if (pBulletCheck)
            {
                pBulletCheck = false;
                if (power == 0)
                {
                    Instantiate(bullet[power], pos.position, Quaternion.identity);
                   
                }
                else if (power == 1)
                {
                    Instantiate(bullet[power], pos.position, Quaternion.identity);
                   
                }
                else if (power == 2)
                {
                    Instantiate(bullet[power], pos.position, Quaternion.identity);
                 
                }
                else if (power == 3)
                {
                    Instantiate(bullet[power], pos.position, Quaternion.identity);
                  
                }

                StartCoroutine(CoolTimeAtk());
            }


        }

        transform.Translate(moveX, moveY, 0);

        if(transform.position.x >= 2.5f)
        {
            transform.position = new Vector3(2.5f, transform.position.y, 0);
            
        }

        if (transform.position.x <= -2.5f)
        {
            transform.position = new Vector3(-2.5f,transform.position.y, 0);
        }

        if(transform.position.y >=4)
        {
            transform.position = new Vector3(transform.position.x, 4.0f, 0);
        }

        if (transform.position.y <= -4)
        {
            transform.position = new Vector3(transform.position.x, -4.0f, 0);
        }


    }

    IEnumerator CoolTimeAtk()
    {
        yield return new WaitForSeconds(0.1f);
        pBulletCheck = true;

        stack++;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Item")
        {
            power += 1;

           

            if (power >= 3)
                power = 3;

            //아이템 먹음 처리
            Destroy(collision.gameObject);

        }
    }

}
