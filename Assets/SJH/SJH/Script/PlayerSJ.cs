using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerSJ : MonoBehaviour
{
    public float Speed = 1f;
    public GameObject bullet;
    public GameObject HomingMissle;

    public Transform gunPos;
    public Transform gunPos2;
    public Transform gunPos3;
    public Transform SgunPos1;
    public Transform SgunPos2;
    public Transform SgunPos3;
    public Transform SgunPos4;
    public Transform SgunPos5;

    public int AttackPower = 10;
    public int MaxItemCount = 4;
    public int MaxItem2Count = 3;
    public int ItemCount = 0;
    public int ItemCount2 = 0;
    public int Heart = 3;
    bool volumeCheck = false;
    bool check = false;
    bool check2 = false;

    private void Start()
    {
        StartCoroutine(VolumeUp());
    }
    void Update()
    {


        float moveHorizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float moveVertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
        transform.Translate(moveHorizontal, moveVertical, 0);

        if (Input.GetKey(KeyCode.Space) && !check)
        {
            if (!check2)
            {
                if (ItemCount2 == 1)
                {
                    StartCoroutine(CreatMissle());
                    Instantiate(HomingMissle, gunPos.position, Quaternion.identity);
                }
                else if (ItemCount2 == 2)
                {
                    StartCoroutine(CreatMissle());
                    Instantiate(HomingMissle, gunPos2.position, Quaternion.identity);
                    Instantiate(HomingMissle, gunPos3.position, Quaternion.identity);
                }
                else if (ItemCount2 == 3)
                {
                    StartCoroutine(CreatMissle());
                    Instantiate(HomingMissle, gunPos.position, Quaternion.identity);
                    Instantiate(HomingMissle, gunPos2.position, Quaternion.identity);
                    Instantiate(HomingMissle, gunPos3.position, Quaternion.identity);
                }
                else if (ItemCount2 == 3)
                {
                    StartCoroutine(CreatMissle());
                    Instantiate(HomingMissle, SgunPos2.position, Quaternion.identity);
                    Instantiate(HomingMissle, SgunPos3.position, Quaternion.identity);
                    Instantiate(HomingMissle, SgunPos4.position, Quaternion.identity);
                    Instantiate(HomingMissle, SgunPos5.position, Quaternion.identity);
                }
            }


            if (ItemCount == 0)
            {
                StartCoroutine(CreatBullet());
                GameObject clone1 = Instantiate(bullet, gunPos.position, Quaternion.identity);
                clone1.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0, 1));
            }
            else if (ItemCount == 1)
            {
                StartCoroutine(CreatBullet());
                GameObject clone1 = Instantiate(bullet, gunPos2.position, Quaternion.identity);
                GameObject clone2 = Instantiate(bullet, gunPos3.position, Quaternion.identity);
                clone1.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0, 1));
                clone2.GetComponent<PlayerBulletSJ>().OnMove(new Vector2(0, 1));
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
            }


        }
        else
        {
            transform.Find("CShot").gameObject.SetActive(false);
            transform.Find("LShot").gameObject.SetActive(false);
            transform.Find("RShot").gameObject.SetActive(false);
        }


        if (Heart < 1)
        {
            Destroy(gameObject);
            Heart = 0;
        }

    }

    IEnumerator CreatBullet()
    {
        check = true;
        yield return new WaitForSeconds(0.1f);
        check = false;

    }
    IEnumerator CreatMissle()
    {
        check2 = true;
        yield return new WaitForSeconds(1f);
        check2 = false;
    }
    IEnumerator VolumeUp()
    {

        float scaleSpeed = 0.01f;
        yield return new WaitForSeconds(2.5f);

        while (gameObject.transform.localScale.x < 0.45f)
        {
            yield return new WaitForSeconds(0.1f);
            gameObject.transform.localScale = new Vector3(0.25f + scaleSpeed,
                0.25f + scaleSpeed, 0.25f + scaleSpeed);
            scaleSpeed += 0.01f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item1"))
        {
            ItemCount++;

            if (ItemCount >= MaxItemCount)
            {
                ItemCount = MaxItemCount;
            }
        }

        if (collision.gameObject.CompareTag("Item2"))
        {
            ItemCount2++;

            if (ItemCount2 >= MaxItem2Count)
            {
                ItemCount2 = MaxItem2Count;
            }
        }

        if (collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("InterCepter"))
        {
            Heart -= 1;
        }
    }
}
