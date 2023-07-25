using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSJ : MonoBehaviour
{
    public float Speed = 1f;
    public GameObject bullet;
    public Transform gunPos;
    public Transform gunPos2;
    public int AttackPower = 10;
    public int Hp = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float moveVertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
        transform.Translate(moveHorizontal, moveVertical , 0);

        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(CreatBullet());
            transform.Find("LShot").gameObject.SetActive(true);
            transform.Find("RShot").gameObject.SetActive(true);

        }
        else
        {
            transform.Find("LShot").gameObject.SetActive(false);
            transform.Find("RShot").gameObject.SetActive(false);
        }
    }

    IEnumerator CreatBullet()
    {
        yield return new WaitForSeconds(1);
        Instantiate(bullet, gunPos.position, Quaternion.identity);
        Instantiate(bullet, gunPos2.position, Quaternion.identity);
    }
}
