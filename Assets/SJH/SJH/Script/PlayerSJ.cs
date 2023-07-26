using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSJ : MonoBehaviour
{
    public float Speed = 1f;
    public GameObject bullet;

    public Transform gunPos;
    public Transform gunPos2;
    public Transform gunPos3;
    public Transform SgunPos1;
    public Transform SgunPos2;
    public Transform SgunPos3;
    public Transform SgunPos4;
    public Transform SgunPos5;

    public int AttackPower = 10;
    public int Hp = 100;
    public int MaxItemCount = 4;
    public int ItemCount = 0;
    public int ItemCount2 = 0;


    bool check = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(ItemCount);
        float moveHorizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float moveVertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
        transform.Translate(moveHorizontal, moveVertical , 0);

        if (Input.GetKey(KeyCode.Space) && !check)
        {
            if (ItemCount ==0)
            {
                StartCoroutine(CreatBullet());
                GameObject clone1 = Instantiate(bullet, gunPos.position, Quaternion.identity);
                clone1.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0,1));
                transform.Find("CShot").gameObject.SetActive(true);
            }
            else if(ItemCount ==1)
            {
                StartCoroutine(CreatBullet());
                GameObject clone1 = Instantiate(bullet, gunPos2.position, Quaternion.identity);
                GameObject clone2 = Instantiate(bullet, gunPos3.position, Quaternion.identity);
                clone1.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0, 1));
                clone2.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0, 1));
                transform.Find("LShot").gameObject.SetActive(true);
                transform.Find("RShot").gameObject.SetActive(true);
                transform.Find("CShot").gameObject.SetActive(false);
            }
            else if (ItemCount == 2)
            {
                StartCoroutine(CreatBullet());
                GameObject clone1 = Instantiate(bullet, gunPos.position, Quaternion.identity);
                GameObject clone2 = Instantiate(bullet, gunPos2.position, Quaternion.identity);
                GameObject clone3 = Instantiate(bullet, gunPos3.position, Quaternion.identity);
                clone1.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0, 1));
                clone2.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0, 1));
                clone3.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0, 1));
                transform.Find("LShot").gameObject.SetActive(true);
                transform.Find("RShot").gameObject.SetActive(true);
                transform.Find("CShot").gameObject.SetActive(true);
            }
            else if (ItemCount == 3)
            {
                StartCoroutine(CreatBullet());
                GameObject clone1 = Instantiate(bullet, SgunPos2.position, Quaternion.identity);
                GameObject clone2 = Instantiate(bullet, SgunPos3.position, Quaternion.identity);
                GameObject clone3 = Instantiate(bullet, SgunPos4.position, Quaternion.identity);
                GameObject clone4 = Instantiate(bullet, SgunPos5.position, Quaternion.identity);
                    
                clone1.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(-0.25f, 1));
                clone2.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0, 1));
                clone3.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0, 1));
                clone4.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0.25f, 1));
                transform.Find("LShot").gameObject.SetActive(true);
                transform.Find("RShot").gameObject.SetActive(true);
                transform.Find("CShot").gameObject.SetActive(true);
            }
            else if (ItemCount == 4)
            {
                StartCoroutine(CreatBullet());
                GameObject clone1 = Instantiate(bullet, SgunPos1.position, Quaternion.identity);
                GameObject clone2 = Instantiate(bullet, SgunPos2.position, Quaternion.identity);
                GameObject clone3 = Instantiate(bullet, SgunPos3.position, Quaternion.identity);
                GameObject clone4 = Instantiate(bullet, SgunPos4.position, Quaternion.identity);
                GameObject clone5 = Instantiate(bullet, SgunPos5.position, Quaternion.identity);
                clone1.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0, 1));
                clone2.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(-0.5f, 1));
                clone3.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(-0.25f, 1));
                clone4.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0.25f, 1));
                clone5.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0.5f, 1));
                transform.Find("LShot").gameObject.SetActive(true);
                transform.Find("RShot").gameObject.SetActive(true);
                transform.Find("CShot").gameObject.SetActive(true);
            }
        }
        else
        {
            transform.Find("LShot").gameObject.SetActive(false);
            transform.Find("RShot").gameObject.SetActive(false);
        }
    }

    IEnumerator CreatBullet()
    {
        check = true;
        yield return new WaitForSeconds(0.1f);
        check = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item1"))
        {
            ItemCount++;

            if(ItemCount >= MaxItemCount)
            {
                ItemCount = MaxItemCount;
            }
        }

        if (collision.gameObject.CompareTag("Item2"))
        {
            ItemCount2++;

            if (ItemCount2 >= MaxItemCount)
            {
                ItemCount2 = MaxItemCount;
            }
        }
    }
}
