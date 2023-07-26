using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_dm : MonoBehaviour
{
    [SerializeField]
    Animator ani;
    public float moveSpeed = 5;
    GameObject curBullet = null;
    public GameObject bullet1 = null;
    public GameObject bullet3 = null;
    public GameObject lazerPrefab = null;
    public Transform pos = null; //미사일 발사
    float limitX = 2.556f;
    float limitY = 4.47f;
    int power = 10;
    public float curTime = 0;
    public bool isLazer = false;
    GameObject lazer = null;

    public Image Gage;
    public float gValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        //ani = GetComponent<Animator>();
        curBullet = bullet1;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float moveY = moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        //if (Input.GetAxis("Horizontal") >= 0.5f)
        //{
        //    ani.SetBool("Right", true);
        //}
        //else
        //{
        //    ani.SetBool("Right", false);
        //}

        //if (Input.GetAxis("Horizontal") <= -0.5f)
        //{
        //    ani.SetBool("Left", true);
        //}
        //else
        //{
        //    ani.SetBool("Left", false);
        //}

        if (Input.GetAxis("Vertical") >= 0.5f)
        {
            ani.SetBool("Up", true);
        }
        else
        {
            ani.SetBool("Up", false);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(curBullet, pos.position, Quaternion.identity).GetComponent<Bullet_dm>().attack = power; 

        }
        //스페이스바를 누르고 있을 때
        else if (Input.GetKey(KeyCode.Space))
        {
            gValue += 0.005f;

            if (gValue >= 1)
            {
                gValue = 1;

                //레이져 나가기
                if (!isLazer)
                {
                    isLazer = true;
                    lazer = Instantiate(lazerPrefab, pos.position, Quaternion.identity);
                    lazer.GetComponent<Lazer_dm>().playerFireTransform = pos;
                }
                
            }

            Gage.fillAmount = gValue;
        }
        else
        {
            gValue -= 0.005f;
            if (gValue <= 0)
                gValue = 0;
            Gage.fillAmount = gValue;

            if (isLazer)
            {
                isLazer = false;
                lazer.GetComponent<Lazer_dm>().isLazer = false;
                lazer = null;
            }
            
        }

        ////my레이저 발사
        //if (Input.GetKey(KeyCode.Space) && !isLazer)
        //{
        //    curTime += Time.deltaTime;

        //    if (curTime >= 3f)
        //    {
        //        //curTime = 3;
        //        isLazer = true;
        //        lazer = Instantiate(lazerPrefab, pos.position, Quaternion.identity);
        //        lazer.GetComponent<Lazer>().playerFireTransform = pos;
        //    }

        //}
        //if (Input.GetKeyUp(KeyCode.Space) && lazer != null)
        //{
        //    isLazer = false;
        //    lazer.GetComponent<Lazer>().isLazer = false;
        //    curTime = 0;
        //    lazer = null;
        //}


        transform.Translate(moveX, moveY, 0);

        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, -limitX, limitX),
            Mathf.Clamp(transform.position.y, -limitY, limitY));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ItemPower"))
        {
            power += 10;

            if (power >= 20)
                power = 20;

            curBullet = bullet3;
            Destroy(collision.gameObject);
        }
    }
}
