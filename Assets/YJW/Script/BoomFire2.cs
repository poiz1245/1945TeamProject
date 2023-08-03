using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomFire2 : MonoBehaviour
{
    public GameObject Boom3;

    public float Attack;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoolTimeTransBoom());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator CoolTimeTransBoom()
    {

        yield return new WaitForSeconds(0.5f);

        var copyboom2 = Instantiate(Boom3, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        //Destroy(copyboom2, 1f);
        Destroy(transform.gameObject);

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            collision.gameObject.GetComponent<Monster>().Damage(Attack);
        }
        else if (collision.tag == "Boss")
        {
            collision.gameObject.GetComponent<Boss>().Damage(Attack);
        }
        else if (collision.tag == "Boss2")
        {
            collision.gameObject.GetComponent<LastBoss>().Damage(Attack);
        }
        else if (collision.tag == "BossHelper")
        {
            collision.gameObject.GetComponent<HelperBoss>().Damage(Attack);
        }
        else if (collision.tag == "BossHelper2")
        {
            collision.gameObject.GetComponent<HelperBoss2>().Damage(Attack);
        }
        else if (collision.tag == "BossArm")
        {
            collision.gameObject.GetComponent<BossArmHp>().Damage(Attack);
        }
        else if (collision.tag == "EnemyBullet")
        {
            Destroy(collision.gameObject);
        }
    }
}
