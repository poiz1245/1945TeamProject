using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_dm : MonoBehaviour
{
    public float speed = 4.0f;
    [SerializeField]
    GameObject exprosionPrefab;
    [SerializeField]
    GameObject itemPrefab;
    public int attack = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            GameObject exprosion = Instantiate(exprosionPrefab, transform.position, Quaternion.identity);
            Destroy(exprosion, 0.4f);
            //아이템 생성
            //Instantiate(itemPrefab, collision.transform.position, Quaternion.identity);
            //collision.GetComponent<Monster>().ItemDrop();

            //몬스터 충돌 지우기
            //Destroy(collision.gameObject);
            //collision.GetComponent<Monster_dm>().Damage(attack);

            if (collision.GetComponent<Monster_dm>() != null)
                collision.GetComponent<Monster_dm>().Damage(attack);
            else if (collision.GetComponent<BossArm_dm>() != null)
                collision.GetComponent<BossArm_dm>().Damage(attack);
            else if (collision.GetComponent<Boss_dm>() != null)
                collision.GetComponent<Boss_dm>().Damage(attack);
            else if (collision.GetComponent<Octopus_dm>() != null)
                collision.GetComponent<Octopus_dm>().Damage(attack);

            //미사일 지우기
            Destroy(gameObject);

        }
    }
}
