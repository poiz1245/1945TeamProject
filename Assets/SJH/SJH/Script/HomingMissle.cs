using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class HomingMissle : MonoBehaviour
{
    public float Speed = 10;
    public float ScanRange;
    public float rotationSpeed = 5f;
    
    public LayerMask targetLayer;
    //public GameObject effect;
    private Transform nearestTarget;
    private bool isScanning = true;
    public float count  = 2;

    GameObject player;
    float pAtk;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pAtk = player.GetComponent<PlayerSJ>().AttackPower;
        ScanTargets();
    }

    void Update()
    {
        if (isScanning)
        {
            nearestTarget = GetNearest();
            isScanning = false;
        }

        if (nearestTarget != null)
        {
            Vector2 dir = nearestTarget.position - transform.position;
            Vector2 dirno = dir.normalized;

            transform.Translate(dir.normalized * Speed * Time.deltaTime);
            Vector2 direction = new Vector2(transform.position.x - nearestTarget.position.x,
                                            transform.position.y - nearestTarget.position.y);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotationSpeed * Time.deltaTime);
            transform.rotation = rotation;
            Destroy(gameObject, 3);
        }
        else
        {
            /*Destroy(gameObject, count );
            ScanTargets();*/
            transform.Translate(Vector2.up*Speed*Time.deltaTime);
            Destroy(gameObject, 3);
        }


    }

    void ScanTargets()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, ScanRange, targetLayer);
        if (targets.Length > 0)
        {
            float closestDistance = Mathf.Infinity;
            Transform closestTarget = null;

            foreach (Collider2D target in targets)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target.transform;
                }
            }

            nearestTarget = closestTarget;
        }
        else
        {
            nearestTarget = null;
        }

        isScanning = false;
    }
    Transform GetNearest()
    {
        return nearestTarget;
    }

    private void OnEnable()
    {
        isScanning = true;
    }

    private void OnDisable()
    {
        isScanning = false;
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

            Destroy(gameObject);
        }
      

    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
