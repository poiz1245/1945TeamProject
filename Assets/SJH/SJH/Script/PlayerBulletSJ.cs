using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerBulletSJ : MonoBehaviour
{
    public float Speed = 1f;

    Vector2 vec2 = Vector2.down;
    public float enegy = 1;

    public GameObject effect; 

    GameObject player;
    public float pAtk;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pAtk = player.GetComponent<PlayerSJ>().AttackPower;
    }

    void Update()
    {
        transform.Translate(vec2 * Speed * Time.deltaTime);
    }
    public void OnMove(Vector2 vec)
    {
        vec2 = vec;
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy") || collision.CompareTag("Elite") || collision.CompareTag("InterCepter") ||
            collision.CompareTag("Monster") || collision.CompareTag("Boss") || collision.CompareTag("BossArm")
            || collision.CompareTag("Boss2") || collision.CompareTag("BossHelper") || collision.CompareTag("BossHelper2"))
        {
            if (collision.GetComponent<Monster_dm>() != null)
            {
                collision.GetComponent<Monster_dm>().Damage((int)pAtk);
            }
            else if (collision.GetComponent<Boss_dm>() != null)
            {
                collision.GetComponent<Boss_dm>().Damage((int)pAtk);
            }
            else if (collision.GetComponent<Octopus_dm>() != null)
            {
                collision.GetComponent<Octopus_dm>().Damage((int)pAtk);
            }
            else if (collision.GetComponent<BossArm_dm>() != null)
            {
                collision.GetComponent<BossArm_dm>().Damage((int)pAtk);

            }
            else if (collision.GetComponent<Boss>() != null)
            {
                collision.GetComponent<Boss>().Damage(pAtk);
              
            }
            else if (collision.GetComponent<LastBoss>() != null)
            {
                collision.GetComponent<LastBoss>().Damage(pAtk);
              
            }
            else if (collision.GetComponent<HelperBoss>() != null)
            {
                collision.GetComponent<HelperBoss>().Damage(pAtk);
               
            }
            else if (collision.GetComponent<HelperBoss2>() != null)
            {
                collision.GetComponent<HelperBoss2>().Damage(pAtk);
              

            }
            else if (collision.GetComponent<BossArmHp>() != null)
            {
                collision.GetComponent<BossArmHp>().Damage(pAtk);
               
            }
            else if (collision.GetComponent<Monster>() != null)
            {
                collision.GetComponent<Monster>().Damage(pAtk);
               

            }
           // Instantiate(effect, collision.transform.position, Quaternion.identity);
            player.GetComponent<PlayerSJ>().GazyPower(enegy);
            Destroy(gameObject);

        }
    }


}
