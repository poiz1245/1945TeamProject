using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArm_dm : MonoBehaviour
{
    public int hp = 500;
    float Delay = 0.5f;
    public GameObject bullet;
    public Transform ms;

    // Start is called before the first frame update
    void Start()
    {
        //한번 호출
        Invoke("CreateBullet", 0.1f);
    }

    void CreateBullet()
    {
        Instantiate(bullet, ms.position, Quaternion.identity);
        Invoke("CreateBullet", Delay);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void Damage(int attack)
    {
        hp -= attack;

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

}
