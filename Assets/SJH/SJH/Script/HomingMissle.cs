using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public float count  = 0;
    void Start()
    {
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
        }
        else
        {
            //Destroy(gameObject, count );
            //ScanTargets();
            transform.Translate(Vector2.up*Speed*Time.deltaTime);

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
        if (collision.gameObject.CompareTag("Enemy") || collision.CompareTag("Elite") || collision.CompareTag("InterCepter"))
            //Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}
